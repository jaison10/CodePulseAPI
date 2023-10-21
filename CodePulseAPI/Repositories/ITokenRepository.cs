using Microsoft.AspNetCore.Identity;

namespace CodePulseAPI.Repositories
{
    public interface ITokenRepository
    {
        public string CreateJwnToken(IdentityUser user, List<string> roles);
    }
}
