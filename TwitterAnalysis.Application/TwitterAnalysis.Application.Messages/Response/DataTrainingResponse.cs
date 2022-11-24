using System.Text.Json.Serialization;
using TwitterAnalysis.App.Service.Common;
using TwitterAnalysis.App.Service.Common.Extension;
using TwitterAnalysis.App.Service.Model.Data_Training;
using TwitterAnalysis.Application.Messages.Response.Abstract;

namespace TwitterAnalysis.Application.Messages.Response
{
    public record DataTrainingResponse : AbstractResponse
    {
        [JsonPropertyName("data")]
        public DataTrainingResult Data { get; set; }

        public DataTrainingResponse StatusMessageOperation(OperationMessageStatusEnum statusEnum)
        {
            Message = statusEnum.GetDescription();
            return this;
        }
    }
}
