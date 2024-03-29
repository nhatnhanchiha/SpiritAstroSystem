﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.ViewModels.Zodiac
{

    public class ZodiacModel
    {
        public static string[] Fields =
        {
            "Id", "Name", "Description", "Date"
        };
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string Date { get; set; }
    }
}
