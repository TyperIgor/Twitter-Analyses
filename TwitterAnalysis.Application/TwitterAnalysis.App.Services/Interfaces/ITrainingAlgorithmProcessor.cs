using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model.Data_Training;

namespace TwitterAnalysis.App.Services.Interfaces
{
    public interface ITrainingAlgorithmProcessor
    {
        Task<DataTrainingResult> GetRacistsPhrasesForDataTraining();

    }
}
