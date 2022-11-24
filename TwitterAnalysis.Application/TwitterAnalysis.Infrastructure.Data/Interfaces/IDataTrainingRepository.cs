using System.Threading.Tasks;
using System.Collections.Generic;
using TwitterAnalysis.App.Service.Model;

namespace TwitterAnalysis.Infrastructure.Data.Interfaces
{
    public interface IDataTrainingRepository
    {
        Task<IEnumerable<RacistModelData>> GetRacistsPhrases();

        Task InsertRacistPhraseAlgorithmTraining(RacistModelData modelData);
    }
}
