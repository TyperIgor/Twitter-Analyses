using Microsoft.ML.Data;
using System;

namespace TwitterAnalysis.App.Service.Model
{
    public class RacistModelData
    {
        [LoadColumnName("Label")]
        [LoadColumn(0)]
        public string Text  { get; set; }

        [LoadColumn(1)]
        public bool ActiveRacist { get; set; }
    }
}
