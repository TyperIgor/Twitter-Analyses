using System.Threading.Tasks;
using System.Collections.Generic;
using TwitterAnalysis.App.Service.Model;

namespace TwitterAnalysis.Infrastructure.Data.Interfaces
{
    public interface ITweetRepository
    {
        Task<IEnumerable<RacistModelData>> GetRacistsPhrasesToModelEnter();
    }
}
