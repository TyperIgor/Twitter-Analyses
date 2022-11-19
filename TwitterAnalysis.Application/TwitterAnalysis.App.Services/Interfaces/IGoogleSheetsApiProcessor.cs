using System.Threading.Tasks;
using System.Collections.Generic;
using TwitterAnalysis.App.Service.Model;

namespace TwitterAnalysis.App.Services.Interfaces
{
    public interface IGoogleSheetsApiProcessor
    {
        Task<IEnumerable<RacistModelData>> ExtractSheetsContent();
    }
}
