using TwitterAnalysis.Application.Messages.Response;
using TwitterAnalysis.App.Service.Model.Data_Training;
using TwitterAnalysis.App.Service.Common;

namespace TwitterAnalysis.Application.Mapper
{
    public static class DataAlgorithmMapper
    {

        public static DataTrainingResponse MapperDataColletionTrainingToResponse(DataTrainingResult racistModelData)
        {
            return new DataTrainingResponse()
            {
                Data = racistModelData,
            }
            .StatusMessageOperation(OperationMessageStatusEnum.SucessfullOperation);
        }


        public static DataTrainingResponse MapperTrainingResponseBaseOperation()
        {
            return new DataTrainingResponse().StatusMessageOperation(OperationMessageStatusEnum.SucessfullOperation);
        }
    }
}
