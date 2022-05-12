namespace PSP.Api.Contracts.Identity {

    public class Login {

        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}