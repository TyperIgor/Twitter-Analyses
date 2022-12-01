using System.Threading.Tasks;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.App.Services.Interfaces;

namespace TwitterAnalysis.Application.Services
{
    public class LoginAuthProcessor : ILoginAuthProcessor
    {
        private readonly IAuthLoginService _authLoginService;
        public LoginAuthProcessor(IAuthLoginService authLoginService)
        {
            _authLoginService = authLoginService;
        }
        public async Task<bool> IsRegistered(string name, string secret)
        {
            return await _authLoginService.CheckUserExistence(name, secret);
        }
    }
}
