using System;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Interfaces;
using System.Threading.Tasks;

namespace TwitterAnalysis.App.Services.AuthLoginProcessor
{
    public class AuthLoginProcessor : IAuthLoginService
    {
        private readonly ILoginRepository _repository;
        public AuthLoginProcessor(ILoginRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckUserExistence(string name, string password)
        {
            return await _repository.CheckLogin(name, password);
        }
    }
}
