using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using EQDevApplication.ViewModel;

namespace EQDevApplication.Converter
{

    /// <summary>
    /// This class is used to change color of the stock names to RED if the TransactionCost > tolerance or MarketValue < 0
    /// </summary>
    [ValueConversion(typeof(StockViewModel), typeof(Boolean))]
    public class TransiactionalCostConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StockViewModel stock = value as StockViewModel;
            if (stock == null)
                return false;

            decimal tolerance = 100;

            if (stock.StockType == Strings.StockViewModel_FundTypeOption_Bond)
            {
                tolerance = 100;
            }
            else
            {
                tolerance = 200;
            }
            return (stock.TransactionCost > tolerance) || (stock.MarketValue < 0);
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
