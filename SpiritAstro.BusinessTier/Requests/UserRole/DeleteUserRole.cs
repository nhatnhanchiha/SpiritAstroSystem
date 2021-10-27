namespace SpiritAstro.BusinessTier.Requests.UserRole
{
    public class DeleteUserRoleRequest
    {
        public long UserId { get; set; }
        public string RoleId { get; set; }
    }
}