using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
