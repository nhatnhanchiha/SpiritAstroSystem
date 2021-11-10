using System.Collections.Generic;

namespace SpiritAstro.BusinessTier.Services
{
    public interface IAstroOnlineService
    {
        bool IsLock();
        void SwitchLock();
        void AddAstroId(long astroId);
        void RemoveAstroId(long astroId);
        HashSet<long> GetSetAstroOnline();
    }
    
    public class AstroOnlineService : IAstroOnlineService
    {
        public AstroOnlineService()
        {
            SetAstroOnline = new HashSet<long>();
            IsLook = false;
        }

        public HashSet<long> SetAstroOnline { get; set; }

        public HashSet<long> GetSetAstroOnline()
        {
            return SetAstroOnline;
        }

        public bool IsLook { get; set; }

        public void SwitchLock()
        {
            IsLook = !IsLook;
        }

        public bool IsLock()
        {
            return IsLook;
        }

        public void AddAstroId(long astroId)
        {
            SetAstroOnline.Add(astroId);
        }

        public void RemoveAstroId(long astroId)
        {
            SetAstroOnline.Remove(astroId);
        }
    }
}