using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarsPartsReconstruccion.ValidationAttributes
{
    public class ValidateStockPriceAttribute : ValidationAttribute
    {
        private readonly int _referencePrice;
        public ValidateStockPriceAttribute(int referencePrice)
            : base("The {0} is not valid.")
        {
            _referencePrice = referencePrice;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //return base.IsValid(value, validationContext);
            if (value != null)
            {
                var price = Convert.ToDecimal(value);
                if (price > 2 * _referencePrice)
                {
                    var errorMessage = string.Format("The {0} is too {1}.", validationContext.DisplayName, "high");
                    return new ValidationResult(errorMessage);
                }
                else if (price < _referencePrice / 2)
                {
                    var errorMessage = string.Format("The {0} is too {1}.", validationContext.DisplayName, "low");
                    return new ValidationResult(errorMessage);
                }

            }

            return ValidationResult.Success;
        }
    }
}