using TwitterAnalysis.Application.Messages.Response;
using TwitterAnalysis.App.Service.Model.Data_Training;

namespace TwitterAnalysis.Application.Mapper
{
    public static class DataAlgorithmMapper
    {
        public static DataTrainingResponse MapperDataTrainingToResponse(DataTrainingResult racistModelData)
        {
            return new DataTrainingResponse()
            {
                Data = racistModelData,
            }
            .StatusMessageOperation(App.Service.Common.OperationMessageStatusEnum.SucessfullOperation);
        }
    }
}
