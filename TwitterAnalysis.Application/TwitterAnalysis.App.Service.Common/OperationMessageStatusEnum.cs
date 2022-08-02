using System.ComponentModel;

namespace TwitterAnalysis.App.Service.Common
{
    public enum OperationMessageStatusEnum
    {
        [Description("Completed Sucessfully")]
        SucessfullOperation = 1,

        [Description("Error Ocurred")]
        ErrorFounded = 2,
    }
}
