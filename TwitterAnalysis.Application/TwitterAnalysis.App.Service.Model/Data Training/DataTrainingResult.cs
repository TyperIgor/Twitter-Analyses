using System.Collections.Generic;

namespace TwitterAnalysis.App.Service.Model.Data_Training
{
    public class DataTrainingResult
    {
        public DataTrainingResult()
        {
            DataList = new List<RacistModelData>();
        }

        public IEnumerable<RacistModelData> DataList { get; set; }
    }
}
