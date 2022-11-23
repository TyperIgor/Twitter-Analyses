using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Common;

namespace TwitterAnalysis.Application.Messages.Response.Abstract
{
    public abstract class AbstractResponse<T>
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public abstract T StatusMessageOperation(OperationMessageStatusEnum statusEnum);
    }
}
