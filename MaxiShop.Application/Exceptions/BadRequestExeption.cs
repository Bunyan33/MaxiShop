using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Exceptions
{
    public class BadRequestExeption : Exception
    {
        public IDictionary<string, string[]> ValidationsErrors { get; set; }

        public BadRequestExeption(string message): base(message) 
        { 
        }

        public BadRequestExeption(string message, ValidationResult validationResult): base(message)
        {
            ValidationsErrors = validationResult.ToDictionary();
        }
    }
}
