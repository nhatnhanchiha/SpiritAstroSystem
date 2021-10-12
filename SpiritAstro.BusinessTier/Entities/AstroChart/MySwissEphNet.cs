using System;
using System.Text;
using SpiritAstro.BusinessTier.Requests.AstroChart;
using SwissEphNet;

namespace SpiritAstro.BusinessTier.Entities.AstroChart
{
    public class MySwissEphNet
    {
        const int BUFLEN = 8000;
        const string MY_ODEGREE_STRING = "°";
        const string progname = "Swisseph Test Program";

        static string[] etut = new string[] { "UT", "ET" };
        static string[] lat_n_s = new string[] { "N", "S" };
        static string[] lon_e_w = new string[] { "E", "W" };
        const int NEPHE = 3;
        static string[] ephe = new string[] { "Swiss Ephemeris", "JPL Ephemeris DE406", "Moshier Ephemeris" };
        const int NPLANSEL = 3;
        static string[] plansel = new string[] { "main planets", "with asteroids", "with hyp. bodies" };
        const int NCENTERS = 6;

        static string[] ctr = new string[]
            { "geocentric", "topocentric", "heliocentric", "barycentric", "sidereal Fagan", "sidereal Lahiri" };

        const int NHSYS = 8;

        static string[] hsysname = new string[]
            { "Placidus", "Campanus", "Regiomontanus", "Koch", "Equal", "Vehlow equal", "Horizon", "B=Alcabitus" };

        static double square_sum(double[] x)
        {
            return (x[0] * x[0] + x[1] * x[1] + x[2] * x[2]);
        }
        //#define SEFLG_EPHMASK   (SEFLG_JPLEPH|SEFLG_SWIEPH|SEFLG_MOSEPH)

        const int BIT_ROUND_SEC = 1;
        const int BIT_ROUND_MIN = 2;
        const int BIT_ZODIAC = 4;
        const string PLSEL_D = "0123456789mtABC";
        const string PLSEL_P = "0123456789mtABCDEFGHI";
        const string PLSEL_H = "JKLMNOPQRSTUVWX";
        const string PLSEL_A = "0123456789mtABCDEFGHIJKLMNOPQRSTUVWX";

        //extern char FAR *pgmptr;
        static string[] zod_nam = new string[]
            { "ar", "ta", "ge", "cn", "le", "vi", "li", "sc", "sa", "cp", "aq", "pi" };

        public MySwissEphNet(GetNatalChartRequest request)
        {
            sweph = new SwissEph();
            init_data(request);
        }

        public class cpd
        {
            public cpd Clone()
            {
                return new cpd()
                {
                    etut = this.etut,
                    lon_e_w = this.lon_e_w,
                    lat_n_s = this.lat_n_s,
                    ephe = this.ephe,
                    plansel = this.plansel,
                    ctr = this.ctr,
                    hsysname = this.hsysname,
                    sast = this.sast,
                    mday = this.mday,
                    mon = this.mon,
                    hour = this.hour,
                    min = this.min,
                    sec = this.sec,
                    year = this.year,
                    lon_deg = this.lon_deg,
                    lon_min = this.lon_min,
                    lon_sec = this.lon_sec,
                    lat_deg = this.lat_deg,
                    lat_min = this.lat_min,
                    lat_sec = this.lat_sec,
                    alt = this.alt
                };
            }

            public string etut = null;
            public string lon_e_w = null;
            public string lat_n_s = null;
            public string ephe = null;
            public string plansel = null;
            public string ctr = null;
            public string hsysname = null;
            public string sast = null;
            public uint mday = 0, mon = 0, hour = 0, min = 0, sec = 0;
            public int year = 0;
            public uint lon_deg = 0, lon_min = 0, lon_sec = 0;
            public uint lat_deg = 0, lat_min = 0, lat_sec = 0;
            public int alt = 0;
        }

        static cpd pd = new cpd(), old_pd;

        SwissEph sweph;


        void init_data(GetNatalChartRequest request)
        {
            var time_of_day = request.TimeOfBirthInUtcTime;
            pd.mday = (uint)time_of_day.Day;
            pd.mon = (uint)time_of_day.Month;
            pd.year = time_of_day.Year;
            pd.hour = (uint)time_of_day.Hour;
            pd.min = (uint)time_of_day.Minute;
            pd.sec = (uint)time_of_day.Second;
            var coordinates = request.Coordinates;
            pd.lon_deg = coordinates.LonDeg;
            pd.lon_min = coordinates.LonMin;
            pd.lon_sec = coordinates.LonSec;
            pd.lat_deg = coordinates.LatDeg;
            pd.lat_min = coordinates.LatMin;
            pd.lat_sec = coordinates.LatSec;
            pd.alt = 0;
            pd.etut = etut[0];
            pd.lat_n_s = coordinates.LatNS;
            pd.lon_e_w = coordinates.LonEW;
            pd.ephe = ephe[0];
            pd.plansel = plansel[0];
            pd.ctr = ctr[0];
            pd.hsysname = hsysname[0];
            pd.sast = "433, 3045, 7066";
        }


        public int Swisseph(StringBuilder buf)
        {
            string serr = String.Empty, serr_save = String.Empty, serr_warn = String.Empty;
            string s, s1, s2;
            string star = String.Empty;
            //  char *sp, *sp2;
            string se_pname;
            string spnam, spnam2 = "";
            string fmt = "PZBRS";
            string plsel = String.Empty;
            char psp;
            string gap = " ";
            double jut = 0.0, y_frac;
            int i, j;
            double hpos = 0;
            int jday, jmon, jyear, jhour, jmin, jsec;
            int ipl, ipldiff = SwissEph.SE_SUN;
            double[] x = new double[6], xequ = new double[6], xcart = new double[6], xcartq = new double[6];
            double[] cusp = new double[12 + 1]; /* cusp[0] + 12 houses */
            double[] ascmc = new double[10]; /* asc, mc, vertex ...*/
            double ar, sinp;
            double a, sidt, armc, lon, lat;
            double eps_true, eps_mean, nutl, nuto;
            //string ephepath;
            string fname = String.Empty;
            //string splan = null, sast = null;
            int nast, iast;
            int[] astno = new int[100];
            int iflag = 0, iflag2; /* external flag: helio, geo... */
            int iflgret;
            var whicheph = SwissEph.SEFLG_SWIEPH;
            bool universal_time = false;
            bool calc_house_pos = false;
            int gregflag;
            bool diff_mode = false;
            int round_flag = 0;
            double tjd_ut = 2415020.5;
            double tjd_et, t2;
            double delt;
            string bc;
            string jul;
            char hsys = pd.hsysname[0];
            //  *serr = *serr_save = *serr_warn = '\0';
            //ephepath = ".;sweph";
            if (String.Compare(pd.ephe, ephe[1]) == 0)
            {
                whicheph = SwissEph.SEFLG_JPLEPH;
                fname = SwissEph.SE_FNAME_DE406;
            }
            else if (String.Compare(pd.ephe, ephe[0]) == 0)
                whicheph = SwissEph.SEFLG_SWIEPH;
            else
                whicheph = SwissEph.SEFLG_MOSEPH;

            if (String.Compare(pd.etut, "UT") == 0)
                universal_time = true;
            if (String.Compare(pd.plansel, plansel[0]) == 0)
            {
                plsel = PLSEL_D;
            }
            else if (String.Compare(pd.plansel, plansel[1]) == 0)
            {
                plsel = PLSEL_P;
            }
            else if (String.Compare(pd.plansel, plansel[2]) == 0)
            {
                plsel = PLSEL_A;
            }

            if (String.Compare(pd.ctr, ctr[0]) == 0)
                calc_house_pos = true;
            else if (String.Compare(pd.ctr, ctr[1]) == 0)
            {
                iflag |= SwissEph.SEFLG_TOPOCTR;
                calc_house_pos = true;
            }
            else if (String.Compare(pd.ctr, ctr[2]) == 0)
            {
                iflag |= SwissEph.SEFLG_HELCTR;
            }
            else if (String.Compare(pd.ctr, ctr[3]) == 0)
            {
                iflag |= SwissEph.SEFLG_BARYCTR;
            }
            else if (String.Compare(pd.ctr, ctr[4]) == 0)
            {
                iflag |= SwissEph.SEFLG_SIDEREAL;
                sweph.swe_set_sid_mode(SwissEph.SE_SIDM_FAGAN_BRADLEY, 0, 0);
            }
            else if (String.Compare(pd.ctr, ctr[5]) == 0)
            {
                iflag |= SwissEph.SEFLG_SIDEREAL;
                sweph.swe_set_sid_mode(SwissEph.SE_SIDM_LAHIRI, 0, 0);
                //#if 0
                //  } else {
                //    iflag &= ~(SEFLG_HELCTR | SEFLG_BARYCTR | SEFLG_TOPOCTR);
                //#endif
            }

            lon = pd.lon_deg + pd.lon_min / 60.0 + pd.lon_sec / 3600.0;
            if (pd.lon_e_w.StartsWith("W"))
                lon = -lon;
            lat = pd.lat_deg + pd.lat_min / 60.0 + pd.lat_sec / 3600.0;
            if (pd.lat_n_s.StartsWith("S"))
                lat = -lat;
            do_print(buf, C.sprintf("Planet Positions from %s \n\n", pd.ephe));
            if ((whicheph & SwissEph.SEFLG_JPLEPH) != 0)
                sweph.swe_set_jpl_file(fname);
            iflag = (iflag & ~SwissEph.SEFLG_EPHMASK) | whicheph;
            iflag |= SwissEph.SEFLG_SPEED;
            //#if 0
            //  if (pd.helio) iflag |= SEFLG_HELCTR;
            //#endif
            if (pd.year * 10000 + pd.mon * 100 + pd.mday < 15821015)
                gregflag = SwissEph.SE_JUL_CAL;
            else
                gregflag = SwissEph.SE_GREG_CAL;
            jday = (int)pd.mday;
            jmon = (int)pd.mon;
            jyear = pd.year;
            jhour = (int)pd.hour;
            jmin = (int)pd.min;
            jsec = (int)pd.sec;
            jut = jhour + (jmin / 60.0) + (jsec / 3600.0);
            tjd_ut = sweph.swe_julday(jyear, jmon, jday, jut, gregflag);
            sweph.swe_revjul(tjd_ut, gregflag, ref jyear, ref jmon, ref jday, ref jut);
            jut += 0.5 / 3600;
            jhour = (int)jut;
            jmin = (int)((jut * 60.0) % 60.0);
            jsec = (int)((jut * 3600.0) % 60.0);
            bc = String.Empty;
            if (pd.year <= 0)
                bc = C.sprintf("(%d B.C.)", 1 - jyear);
            if (jyear * 10000L + jmon * 100L + jday <= 15821004)
                jul = "jul.";
            else
                jul = "";
            do_print(buf, C.sprintf("%d.%d.%d %s %s    %#02d:%#02d:%#02d %s\n",
                jday, jmon, jyear, bc, jul,
                jhour, jmin, jsec, pd.etut));
            jut = jhour + jmin / 60.0 + jsec / 3600.0;
            if (universal_time)
            {
                delt = sweph.swe_deltat(tjd_ut);
                do_print(buf, C.sprintf(" delta t: %f sec", delt * 86400.0));
                tjd_et = tjd_ut + delt;
            }
            else
                tjd_et = tjd_ut;

            do_print(buf, C.sprintf(" jd (ET) = %f\n", tjd_et));
            iflgret = sweph.swe_calc(tjd_et, SwissEph.SE_ECL_NUT, iflag, x, ref serr);
            eps_true = x[0];
            eps_mean = x[1];
            s1 = dms(eps_true, round_flag);
            s2 = dms(eps_mean, round_flag);
            do_print(buf, C.sprintf("\n%-15s %s%s%s    (true, mean)", "Ecl. obl.", s1, gap, s2));
            nutl = x[2];
            nuto = x[3];
            s1 = dms(nutl, round_flag);
            s2 = dms(nuto, round_flag);
            do_print(buf, C.sprintf("\n%-15s %s%s%s    (dpsi, deps)", "Nutation", s1, gap, s2));
            do_print(buf, "\n\n");
            do_print(buf, "               ecl. long.       ecl. lat.   ");
            do_print(buf, "    dist.          speed");
            if (calc_house_pos)
                do_print(buf, "          house");
            do_print(buf, "\n");
            if ((iflag & SwissEph.SEFLG_TOPOCTR) != 0)
                sweph.swe_set_topo(lon, lat, pd.alt);
            sidt = sweph.swe_sidtime(tjd_ut) + lon / 15;
            if (sidt >= 24)
                sidt -= 24;
            if (sidt < 0)
                sidt += 24;
            armc = sidt * 15;
            /* additional asteroids */
            //splan = plsel;
            if (String.Compare(plsel, PLSEL_P) == 0)
            {
                var cpos = pd.sast.Split(",;. \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                j = cpos.Length;
                for (i = 0, nast = 0; i < j; i++)
                {
                    if ((astno[nast] = int.Parse(cpos[i])) > 0)
                    {
                        nast++;
                        plsel += "+";
                    }
                }
            }

            int pspi;
            for (pspi = 0, iast = 0; pspi < plsel.Length; pspi++)
            {
                psp = plsel[pspi];
                if (psp == '+')
                {
                    ipl = SwissEph.SE_AST_OFFSET + (int)astno[iast];
                    iast++;
                }
                else
                    ipl = letter_to_ipl(psp);

                if ((iflag & SwissEph.SEFLG_HELCTR) != 0)
                {
                    if (ipl == SwissEph.SE_SUN
                        || ipl == SwissEph.SE_MEAN_NODE || ipl == SwissEph.SE_TRUE_NODE
                        || ipl == SwissEph.SE_MEAN_APOG || ipl == SwissEph.SE_OSCU_APOG)
                        continue;
                }
                else if ((iflag & SwissEph.SEFLG_BARYCTR) != 0)
                {
                    if (ipl == SwissEph.SE_MEAN_NODE || ipl == SwissEph.SE_TRUE_NODE
                                                     || ipl == SwissEph.SE_MEAN_APOG || ipl == SwissEph.SE_OSCU_APOG)
                        continue;
                }
                else /* geocentric */
                if (ipl == SwissEph.SE_EARTH)
                    continue;

                /* ecliptic position */
                if (ipl == SwissEph.SE_FIXSTAR)
                {
                    iflgret = sweph.swe_fixstar(ref star, tjd_et, iflag, x, ref serr);
                    se_pname = star;
                }
                else
                {
                    iflgret = sweph.swe_calc(tjd_et, ipl, iflag, x, ref serr);
                    se_pname = sweph.swe_get_planet_name(ipl);
                    if (ipl > SwissEph.SE_AST_OFFSET)
                    {
                        s = C.sprintf("#%d", (int)astno[iast - 1]);
                        se_pname += new String(' ', 11 - s.Length) + s;
                    }
                }

                if (iflgret >= 0)
                {
                    if (calc_house_pos)
                    {
                        hpos = sweph.swe_house_pos(armc, lat, eps_true, hsys, x, ref serr);
                        if (hpos == 0)
                            iflgret = SwissEph.ERR;
                    }
                }

                if (iflgret < 0)
                {
                    if (!String.IsNullOrEmpty(serr) && String.Compare(serr, serr_save) != 0)
                    {
                        serr_save = serr;
                        do_print(buf, "error: ");
                        do_print(buf, serr);
                        do_print(buf, "\n");
                    }
                }
                else if (!String.IsNullOrEmpty(serr) && String.IsNullOrEmpty(serr_warn))
                    serr_warn = serr;

                /* equator position */
                if (fmt.IndexOfAny("aADdQ".ToCharArray()) >= 0)
                {
                    iflag2 = iflag | SwissEph.SEFLG_EQUATORIAL;
                    if (ipl == SwissEph.SE_FIXSTAR)
                        iflgret = sweph.swe_fixstar(ref star, tjd_et, iflag2, xequ, ref serr);
                    else
                        iflgret = sweph.swe_calc(tjd_et, ipl, iflag2, xequ, ref serr);
                }

                /* ecliptic cartesian position */
                if (fmt.IndexOfAny("XU".ToCharArray()) >= 0)
                {
                    iflag2 = iflag | SwissEph.SEFLG_XYZ;
                    if (ipl == SwissEph.SE_FIXSTAR)
                        iflgret = sweph.swe_fixstar(ref star, tjd_et, iflag2, xcart, ref serr);
                    else
                        iflgret = sweph.swe_calc(tjd_et, ipl, iflag2, xcart, ref serr);
                }

                /* equator cartesian position */
                if (fmt.IndexOfAny("xu".ToCharArray()) >= 0)
                {
                    iflag2 = iflag | SwissEph.SEFLG_XYZ | SwissEph.SEFLG_EQUATORIAL;
                    if (ipl == SwissEph.SE_FIXSTAR)
                        iflgret = sweph.swe_fixstar(ref star, tjd_et, iflag2, xcartq, ref serr);
                    else
                        iflgret = sweph.swe_calc(tjd_et, ipl, iflag2, xcartq, ref serr);
                }

                spnam = se_pname;
                /*
                 * The string fmt contains a sequence of format specifiers;
                 * each character in fmt creates a column, the columns are
                 * sparated by the gap string.
                 */
                int spi = 0;
                for (spi = 0; spi < fmt.Length; spi++)
                {
                    char sp = fmt[spi];
                    if (spi > 0)
                        do_print(buf, gap);
                    switch (sp)
                    {
                        case 'y':
                            do_print(buf, "%d", jyear);
                            break;
                        case 'Y':
                            jut = 0;
                            t2 = sweph.swe_julday(jyear, 1, 1, jut, gregflag);
                            y_frac = (tjd_ut - t2) / 365.0;
                            do_print(buf, "%.2lf", jyear + y_frac);
                            break;
                        case 'p':
                            if (diff_mode)
                                do_print(buf, "%d-%d", ipl, ipldiff);
                            else
                                do_print(buf, "%d", ipl);
                            break;
                        case 'P':
                            if (diff_mode)
                                do_print(buf, "%.3s-%.3s", spnam, spnam2);
                            else
                                do_print(buf, "%-11s", spnam);
                            break;
                        case 'J':
                        case 'j':
                            do_print(buf, "%.2f", tjd_ut);
                            break;
                        case 'T':
                            do_print(buf, "%02d.%02d.%d", jday, jmon, jyear);
                            break;
                        case 't':
                            do_print(buf, "%02d%02d%02d", jyear % 100, jmon, jday);
                            break;
                        case 'L':
                            do_print(buf, dms(x[0], round_flag));
                            break;
                        case 'l':
                            do_print(buf, "%# 11.7f", x[0]);
                            break;
                        case 'Z':
                            do_print(buf, dms(x[0], round_flag | BIT_ZODIAC));
                            break;
                        case 'S':
                        case 's':
                            var sp2i = spi + 1;
                            char sp2 = fmt.Length <= sp2i ? '\0' : fmt[sp2i];
                            if (sp2 == 'S' || sp2 == 's' || fmt.IndexOfAny("XUxu".ToCharArray()) >= 0)
                            {
                                for (sp2i = 0; sp2i < fmt.Length; sp2i++)
                                {
                                    sp2 = fmt[sp2i];
                                    if (sp2i > 0)
                                        do_print(buf, gap);
                                    switch (sp2)
                                    {
                                        case 'L': /* speed! */
                                        case 'Z': /* speed! */
                                            do_print(buf, dms(x[3], round_flag));
                                            break;
                                        case 'l': /* speed! */
                                            do_print(buf, "%11.7f", x[3]);
                                            break;
                                        case 'B': /* speed! */
                                            do_print(buf, dms(x[4], round_flag));
                                            break;
                                        case 'b': /* speed! */
                                            do_print(buf, "%11.7f", x[4]);
                                            break;
                                        case 'A': /* speed! */
                                            do_print(buf, dms(xequ[3] / 15, round_flag | SwissEph.SEFLG_EQUATORIAL));
                                            break;
                                        case 'a': /* speed! */
                                            do_print(buf, "%11.7f", xequ[3]);
                                            break;
                                        case 'D': /* speed! */
                                            do_print(buf, dms(xequ[4], round_flag));
                                            break;
                                        case 'd': /* speed! */
                                            do_print(buf, "%11.7f", xequ[4]);
                                            break;
                                        case 'R': /* speed! */
                                        case 'r': /* speed! */
                                            do_print(buf, "%# 14.9f", x[5]);
                                            break;
                                        case 'U': /* speed! */
                                        case 'X': /* speed! */
                                            if (sp == 'U')
                                                ar = Math.Sqrt(square_sum(xcart));
                                            else
                                                ar = 1;
                                            do_print(buf, "%# 14.9f%s", xcart[3] / ar, gap);
                                            do_print(buf, "%# 14.9f%s", xcart[4] / ar, gap);
                                            do_print(buf, "%# 14.9f", xcart[5] / ar);
                                            break;
                                        case 'u': /* speed! */
                                        case 'x': /* speed! */
                                            if (sp == 'u')
                                                ar = Math.Sqrt(square_sum(xcartq));
                                            else
                                                ar = 1;
                                            do_print(buf, "%# 14.9f%s", xcartq[3] / ar, gap);
                                            do_print(buf, "%# 14.9f%s", xcartq[4] / ar, gap);
                                            do_print(buf, "%# 14.9f", xcartq[5] / ar);
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                if (fmt.Length <= spi + 1 && (fmt[spi + 1] == 'S' || fmt[sp + 1] == 's'))
                                {
                                    spi++;
                                    sp = fmt[spi];
                                }
                            }
                            else
                            {
                                do_print(buf, dms(x[3], round_flag));
                            }

                            break;
                        case 'B':
                            do_print(buf, dms(x[1], round_flag));
                            break;
                        case 'b':
                            do_print(buf, "%# 11.7f", x[1]);
                            break;
                        case 'A': /* rectascensio */
                            do_print(buf, dms(xequ[0] / 15, round_flag | SwissEph.SEFLG_EQUATORIAL));
                            break;
                        case 'a': /* rectascensio */
                            do_print(buf, "%# 11.7f", xequ[0]);
                            break;
                        case 'D': /* declination */
                            do_print(buf, dms(xequ[1], round_flag));
                            break;
                        case 'd': /* declination */
                            do_print(buf, "%# 11.7f", xequ[1]);
                            break;
                        case 'R':
                            do_print(buf, "%# 14.9f", x[2]);
                            break;
                        case 'r':
                            if (ipl == SwissEph.SE_MOON)
                            {
                                /* for moon print parallax */
                                sinp = 8.794 / x[2]; /* in seconds of arc */
                                ar = sinp * (1 + sinp * sinp * 3.917402e-12);
                                /* the factor is 1 / (3600^2 * (180/pi)^2 * 6) */
                                do_print(buf, "%# 13.5f\"", ar);
                            }
                            else
                            {
                                do_print(buf, "%# 14.9f", x[2]);
                            }

                            break;
                        case 'U':
                        case 'X':
                            if (sp == 'U')
                                ar = Math.Sqrt(square_sum(xcart));
                            else
                                ar = 1;
                            do_print(buf, "%# 14.9f%s", xcart[0] / ar, gap);
                            do_print(buf, "%# 14.9f%s", xcart[1] / ar, gap);
                            do_print(buf, "%# 14.9f", xcart[2] / ar);
                            break;
                        case 'u':
                        case 'x':
                            if (sp == 'u')
                                ar = Math.Sqrt(square_sum(xcartq));
                            else
                                ar = 1;
                            do_print(buf, "%# 14.9f%s", xcartq[0] / ar, gap);
                            do_print(buf, "%# 14.9f%s", xcartq[1] / ar, gap);
                            do_print(buf, "%# 14.9f", xcartq[2] / ar);
                            break;
                        case 'Q':
                            do_print(buf, "%-15s", spnam);
                            do_print(buf, dms(x[0], round_flag));
                            do_print(buf, dms(x[1], round_flag));
                            do_print(buf, "  %# 14.9f", x[2]);
                            do_print(buf, dms(x[3], round_flag));
                            do_print(buf, dms(x[4], round_flag));
                            do_print(buf, "  %# 14.9f\n", x[5]);
                            do_print(buf, "               %s", dms(xequ[0], round_flag));
                            do_print(buf, dms(xequ[1], round_flag));
                            do_print(buf, "                %s", dms(xequ[3], round_flag));
                            do_print(buf, dms(xequ[4], round_flag));
                            break;
                    } /* switch */
                } /* for sp */

                if (calc_house_pos)
                {
                    //sprintf(s, "  %# 6.4f", hpos);
                    do_print(buf, "%# 9.4f", hpos);
                }

                do_print(buf, "\n");
            } /* for psp */

            if (!String.IsNullOrEmpty(serr_warn))
            {
                do_print(buf, "\nwarning: ");
                do_print(buf, serr_warn);
                do_print(buf, "\n");
            }

            /* houses */
            do_print(buf, C.sprintf("\nHouse Cusps (%s)\n\n", pd.hsysname));
            a = sidt + 0.5 / 3600;
            do_print(buf, C.sprintf("sid. time : %4d:%#02d:%#02d  ", (int)a,
                (int)((a * 60.0) % 60.0),
                (int)((a * 3600.0) % 60.0))
            );
            a = armc + 0.5 / 3600;
            do_print(buf, "armc      : %4d%s%#02d'%#02d\"\n",
                (int)armc, MY_ODEGREE_STRING,
                (int)((armc * 60.0) % 60.0),
                (int)((a * 3600.0) % 60.0));
            do_print(buf, "geo. lat. : %4d%c%#02d'%#02d\" ",
                pd.lat_deg, pd.lat_n_s[0], pd.lat_min, pd.lat_sec);
            do_print(buf, "geo. long.: %4d%c%#02d'%#02d\"\n\n",
                pd.lon_deg, pd.lon_e_w[0], pd.lon_min, pd.lon_sec);
            sweph.swe_houses_ex(tjd_ut, iflag, lat, lon, hsys, cusp, ascmc);
            round_flag |= BIT_ROUND_SEC;
            //#if FALSE
            //  sprintf(s, "AC        : %s\n", dms(ascmc[0], round_flag));
            //  do_print(buf, s);
            //  sprintf(s, "MC        : %s\n", dms(ascmc[1], round_flag));
            //  do_print(buf, s);
            //  for (i = 1; i <= 12; i++) {
            //    sprintf(s, "house   %2d: %s\n", i, dms(cusp[i], round_flag));
            //    do_print(buf, s);
            //  }
            //  sprintf(s, "Vertex    : %s\n", dms(ascmc[3], round_flag));
            //  do_print(buf, s);
            //#else
            do_print(buf, C.sprintf("AC        : %s\n", dms(ascmc[0], round_flag | BIT_ZODIAC)));
            do_print(buf, C.sprintf("MC        : %s\n", dms(ascmc[1], round_flag | BIT_ZODIAC)));
            for (i = 1; i <= 12; i++)
            {
                do_print(buf, C.sprintf("house   %2d: %s\n", i, dms(cusp[i], round_flag | BIT_ZODIAC)));
            }

            do_print(buf, C.sprintf("Vertex    : %s\n", dms(ascmc[3], round_flag | BIT_ZODIAC)));
            //#endif  
            return 0;
        }

        static string dms(double x, long iflag)
        {
            int izod;
            long k, kdeg, kmin, ksec;
            string c = MY_ODEGREE_STRING, s1;
            //char *sp, s1[50];
            //static char s[50];
            int sgn;
            string s = String.Empty;
            if ((iflag & SwissEph.SEFLG_EQUATORIAL) != 0)
                c = "h";
            if (x < 0)
            {
                x = -x;
                sgn = -1;
            }
            else
                sgn = 1;

            if ((iflag & BIT_ROUND_MIN) != 0)
                x += 0.5 / 60;
            if ((iflag & BIT_ROUND_SEC) != 0)
                x += 0.5 / 3600;
            if ((iflag & BIT_ZODIAC) != 0)
            {
                izod = (int)(x / 30);
                x = (x % 30.0);
                kdeg = (long)x;
                s = C.sprintf(" %2ld %s ", kdeg, zod_nam[izod]);
            }
            else
            {
                kdeg = (long)x;
                s = C.sprintf("%3ld%s", kdeg, c);
            }

            x -= kdeg;
            x *= 60;
            kmin = (long)x;
            if ((iflag & BIT_ZODIAC) != 0 && (iflag & BIT_ROUND_MIN) != 0)
                s1 = C.sprintf("%2ld", kmin);
            else
                s1 = C.sprintf("%2ld'", kmin);
            s += s1;
            if ((iflag & BIT_ROUND_MIN) != 0)
                goto return_dms;
            x -= kmin;
            x *= 60;
            ksec = (long)x;
            if ((iflag & BIT_ROUND_SEC) != 0)
                s1 = C.sprintf("%2ld\"", ksec);
            else
                s1 = C.sprintf("%2ld", ksec);
            s += s1;
            if ((iflag & BIT_ROUND_SEC) != 0)
                goto return_dms;
            x -= ksec;
            k = (long)(x * 10000);
            s1 = C.sprintf(".%04ld", k);
            s += s1;
            return_dms: ;
            if (sgn < 0)
            {
                int spi = s.IndexOfAny("0123456789".ToCharArray());
                s = String.Concat(s.Substring(0, spi - 1), "-", s.Substring(spi));
            }

            return (s);
        }

        static void do_print(ref string target, string info)
        {
            if (String.IsNullOrWhiteSpace(target))
                target = " ";
            target += info.Replace("\n", "\r\n");
        }

        static void do_print(StringBuilder target, string info, params object[] args)
        {
            if (target.Length == 0)
                target.Append(" ");
            if (args != null)
                info = C.sprintf(info, args);
            target.Append(info.Replace("\n", "\r\n"));
        }

        static int letter_to_ipl(char letter)
        {
            if (letter >= '0' && letter <= '9')
                return letter - '0' + SwissEph.SE_SUN;
            if (letter >= 'A' && letter <= 'I')
                return letter - 'A' + SwissEph.SE_MEAN_APOG;
            if (letter >= 'J' && letter <= 'X')
                return letter - 'J' + SwissEph.SE_CUPIDO;
            switch (letter)
            {
                case 'm': return SwissEph.SE_MEAN_NODE;
                case 'n':
                case 'o': return SwissEph.SE_ECL_NUT;
                case 't': return SwissEph.SE_TRUE_NODE;
                case 'f': return SwissEph.SE_FIXSTAR;
            }

            return -1;
        }

        static int atoulng(string s, ref uint lng)
        {
            if (uint.TryParse(s, out lng))
                return SwissEph.OK;
            else
                return SwissEph.ERR;
        }

        static int atoslng(string s, ref int lng)
        {
            if (int.TryParse(s, out lng))
                return SwissEph.OK;
            else
                return SwissEph.ERR;
        }

        /* make_ephemeris_path().
         * ephemeris path includes
         *   current working directory
         *   + program directory
         *   + default path from swephexp.h on current drive
         *   +                              on program drive
         *   +                              on drive C:
         */
        int make_ephemeris_path(long iflag, ref string argv0)
        {
            //char path[AS_MAXCH], s[AS_MAXCH];
            string path, s;
            //string sp;
            int spi;
            var dirglue = SwissEph.DIR_GLUE;
            int pathlen;
            /* moshier needs no ephemeris path */
            if ((iflag & SwissEph.SEFLG_MOSEPH) != 0)
                return SwissEph.OK;
            /* current working directory */
            path = C.sprintf(".%c", SwissEph.PATH_SEPARATOR);
            /* program directory */
            spi = argv0.LastIndexOf(dirglue);
            if (spi >= 0)
            {
                pathlen = spi;
                path = argv0.Substring(0, pathlen) + SwissEph.PATH_SEPARATOR;
            }

            //#if MSDOS
            //{
            string[] cpos;
            //char s[2 * AS_MAXCH], *s1 = s + AS_MAXCH;
            string s1;
            string[] sp = new string[3];
            int i, j, np;
            s1 = ".;sweph";
            cpos = s1.Split(new char[] { SwissEph.PATH_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            np = cpos.Length;
            /* 
             * default path from swephexp.h
             * - current drive
             * - program drive
             * - drive C
             */
            s = null;
            /* current working drive */
            sp[0] = Environment.CurrentDirectory;
            if (sp[0] == null)
            {
                /*do_printf("error in getcwd()\n");*/
                return SwissEph.ERR;
            }

            if (sp[0][0] == 'C')
                sp[0] = null;
            /* program drive */
            if (argv0[0] != 'C' && (sp[0] == null || sp[0][0] != argv0[0]))
                sp[1] = argv0;
            else
                sp[1] = null;
            /* drive C */
            sp[2] = "C";
            for (i = 0; i < np; i++)
            {
                s = cpos[i];
                if (!String.IsNullOrWhiteSpace(s) && s[0] == '.') /* current directory */
                    continue;
                if (s != null && s.Length > 1 && s[1] == ':') /* drive already there */
                    continue;
                for (j = 0; j < 3; j++)
                {
                    if (sp[j] != null)
                        path += C.sprintf("%c:%s%c", sp[j][0], s, ';');
                }
            }

            //}
            //#else
            //    if (strlen(path) + pathlen < AS_MAXCH-1)
            //      strcat(path, SE_EPHE_PATH);
            //#endif
            return SwissEph.OK;
        }

        ///**************************************************************
//cut the string s at any char in cutlist; put pointers to partial strings
//into cpos[0..n-1], return number of partial strings;
//if less than nmax fields are found, the first empty pointer is
//set to NULL.
//More than one character of cutlist in direct sequence count as one
//separator only! cut_str_any("word,,,word2",","..) cuts only two parts,
//cpos[0] = "word" and cpos[1] = "word2".
//If more than nmax fields are found, nmax is returned and the
//last field nmax-1 rmains un-cut.
//**************************************************************/
//static int cut_str_any(char *s, char *cutlist, char *cpos[], int nmax)
//{
//  int n = 1;
//  cpos [0] = s;
//  while (*s != '\0') {
//    if ((strchr(cutlist, (int) *s) != NULL) && n < nmax) {
//      *s = '\0';
//      while (*(s + 1) != '\0' && strchr (cutlist, (int) *(s + 1)) != NULL) s++;
//      cpos[n++] = s + 1;
//    }
//    if (*s == '\n' || *s == '\r') {	/* treat nl or cr like end of string */
//      *s = '\0';
//      break;
//    }
//    s++;
//  }
//  if (n < nmax) cpos[n] = NULL;
//  return (n);
//}	/* cutstr */
    }
}