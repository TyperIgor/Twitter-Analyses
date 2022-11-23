using System;
using System.Collections.Generic;
using TwitterAnalysis.App.Service.Common;
using TwitterAnalysis.App.Service.Common.Extension;
using TwitterAnalysis.App.Service.Model.Data_Training;
using TwitterAnalysis.Application.Messages.Response.Abstract;

namespace TwitterAnalysis.Application.Messages.Response
{
    public class DataTrainingResponse : AbstractResponse<DataTrainingResponse>
    {
        public DataTrainingResult Data { get; set; }

        public override DataTrainingResponse StatusMessageOperation(OperationMessageStatusEnum statusEnum)
        {
            Message = statusEnum.GetDescription();
            return this;
        }
    }
}
