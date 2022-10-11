using System;
using System.Collections.Generic;

namespace TwitterAnalysis.App.Service.Model
{
    public class AlgorithmMetricsSummary
    {
        public double Accuracy { get; set; }

        public double PositivePrecision { get; set; }

        public double NegativePrecision { get; set; }

        public double AreaUnderCurve { get; set; }

        public double F1Score { get; set; }
    }
}
