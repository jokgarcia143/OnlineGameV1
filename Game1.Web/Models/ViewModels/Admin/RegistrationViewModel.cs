namespace Game1.Web.Models.ViewModels.Admin
{
    public class RegistrationViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Contact { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPass { get; set; }

        public string? Role { get; set; }
        public string? referralLink { get; set; }
        public int Level { get; set; }
    }
}
