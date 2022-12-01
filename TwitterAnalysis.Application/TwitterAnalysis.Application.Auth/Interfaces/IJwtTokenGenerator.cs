using TwitterAnalysis.Application.Messages.Request.Auth;

namespace TwitterAnalysis.Application.Auth.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(LoginAuthRequest login);
    }
}
