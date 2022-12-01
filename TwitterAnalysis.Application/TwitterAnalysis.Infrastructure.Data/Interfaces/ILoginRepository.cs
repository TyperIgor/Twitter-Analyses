using System.Threading.Tasks;

namespace TwitterAnalysis.Infrastructure.Data.Interfaces
{
    public interface ILoginRepository
    {
        Task<bool> CheckLogin(string username, string password);
    }
}
