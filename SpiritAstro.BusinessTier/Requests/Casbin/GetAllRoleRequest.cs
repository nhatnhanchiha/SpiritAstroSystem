using System.ComponentModel.DataAnnotations;

namespace SpiritAstro.BusinessTier.Requests.Casbin
{
    public class GetAllPermissionsRequest
    {
        [Required]
        public string Role { get; set; }
    }
}