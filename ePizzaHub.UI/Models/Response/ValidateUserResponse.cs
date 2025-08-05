namespace ePizzaHub.UI.Models.Response
{
    public class ValidateUserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public List<string> Roles { get; set; }
        public string AccessToken { get; set; }

        public int TokenExpiryInMinutes { get; set; }


    }
}
