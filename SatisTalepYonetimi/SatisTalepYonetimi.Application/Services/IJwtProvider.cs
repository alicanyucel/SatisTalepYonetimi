using SatisTalepYonetimi.Application.Features.Auth.Login;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}
