using Microsoft.IdentityModel.Tokens;

namespace ForumSystemApi1.Jwt
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "api/users/login";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(15);

        public SigningCredentials SignigCredentials { get; set; }
    }
}
