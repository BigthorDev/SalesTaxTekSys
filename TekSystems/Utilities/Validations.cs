using System;
using System.Collections.Generic;
using System.Text;

namespace TekSystems.Utilities
{
    public class Validations : IValidations
    {
        private readonly ILogger _logger;
        public Validations(ILogger logger)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            this._logger = logger;
        }
        public int ValidateNumericValue(object value)
        {
            bool isNumeric = false;
            int response = -1;
            do
            {
                
                if (int.TryParse(value.ToString(), out int tmpResp))
                {
                    response = tmpResp;
                    isNumeric = true;
                }
                else
                {
                    _logger.Log("Value must be numeric and higher than zero");
                    value = Console.ReadLine();
                }
            }
            while (!isNumeric);

            return response;
        }

        public int ValidateListIDValue(object value)
        {
            bool isNumeric = false;
            int response = -1;
            do
            {
                if (int.TryParse(value.ToString(), out int tmpResp))
                {
                    if (tmpResp <= 3 && tmpResp > 0)
                    {
                        response = tmpResp;
                        isNumeric = true;
                    }
                    else
                    {
                        _logger.Log("Value must be 1, 2 or 3");
                        value = Console.ReadLine();
                    }                    
                }
                else
                {
                    _logger.Log("Value must be numeric and higher than zero");
                    value = Console.ReadLine();
                }
            }
            while (!isNumeric);

            return response;
        }
    }
}
