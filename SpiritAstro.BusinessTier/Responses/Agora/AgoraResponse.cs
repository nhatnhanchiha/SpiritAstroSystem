using System;

namespace SpiritAstro.BusinessTier.Responses.Agora
{
    public class AgoraObject
    {
        public string Chanel { get; set; }
        public string Token { get; set; }

        public bool IsLock { get; set; }

        public void SwitchLock()
        {
            IsLock = !IsLock;
        }

        public DateTime EndTime { get; set; }
    }
}