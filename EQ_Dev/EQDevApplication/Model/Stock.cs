using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQDevApplication.Model
{
    public class Stock : IDataErrorInfo
    {
         #region Creation

        public static Stock CreateNewStock()
        {
            return new Stock();
        }

        public static Stock CreateStock(
            string type,
            decimal price,
            int quantity
            )
        {
            return new Stock
            {
                Type = type,
                Price = price,
                Quantity = quantity,
            };
        }

        protected Stock()
        {
        }

        #endregion // Creation

        #region State Properties

        /// <summary>
        /// Gets/sets the type of the stock.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets/sets the stocks's price.  
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets/sets the stocks's quantity.
        /// </summary>
        public int Quantity { get; set; }

        #endregion // State Properties

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        #endregion // IDataErrorInfo Members

        #region Validation

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;

                return true;
            }
        }

        static readonly string[] ValidatedProperties = 
        { 
            "Type",
            "Price", 
            "Quantity" 
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "Type":
                    error = this.ValidateType();
                    break;

                case "Price":
                    error = this.ValidatePrice();
                    break;

                case "Quantity":
                    error = this.ValidateQuantity();
                    break;

                default:
                    Debug.Fail("Unexpected property being validated on Stock: " + propertyName);
                    break;
            }

            return error;
        }

        string ValidateType()
        {
            if (IsStringMissing(this.Type) || this.Type == Strings.StockViewModel_StockTypeOption_NotSpecified)
            {
                return Strings.StockViewModel_Error_MissingStockType;
            }

            return null;
        }

        string ValidatePrice()
        {
            if (IsValueMissing(this.Price))
            {
                return Strings.Stock_Error_MissingPrice;
            }
            return null;
        }

        string ValidateQuantity()
        {
            if(IsValueMissing(this.Quantity))
            {
                return Strings.Stock_Error_MissingQuantity;
            }
            return null;
        }

        static bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        static bool IsValueMissing(int value)
        {
            return IsValueMissing((decimal) value);
        }

        static bool IsValueMissing(decimal value)
        {
            return
                value <= 0;
        }

        #endregion // Validation
    }
}
