using System.ComponentModel;

namespace TwitterAnalysis.App.Service.Common
{
    public enum OperationMessageStatusEnum
    {
        [Description("Completed Sucessfully")]
        SuccessfullOperation = 1,

        [Description("An error was occurred")]
        ErrorFounded = 2,
    }
}
