using System.Threading.Tasks;

namespace TwitterAnalysis.App.Services.Interfaces
{
    public interface IAuthLoginService
    {
        Task<bool> CheckUserExistence(string name, string password);
    }
}
