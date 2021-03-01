using Domain;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}
