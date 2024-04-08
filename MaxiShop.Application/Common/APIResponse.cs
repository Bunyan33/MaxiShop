using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Common
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }

        public bool IsSuccess { get; set; } = false;
        public object? Result { get; set; }
        public string DisplayMessage { get; set; } = "";

        public List<APIError> Errors { get; set; } = new();

        public List<APIWarning> Warnings { get; set; } = new();

        public void AddError(string errorMessage)
        {
            APIError error = new APIError(description:errorMessage);
            Errors.Add(error);
        }

        public void AddWarning(string WarningMessage)
        {
            APIWarning warning = new APIWarning(description: WarningMessage);
            Warnings.Add(warning);
        }
    }
}
