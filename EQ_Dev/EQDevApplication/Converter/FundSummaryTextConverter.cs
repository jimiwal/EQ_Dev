using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using EQDevApplication.ViewModel;

namespace EQDevApplication.Converter
{
    [ValueConversion(typeof(AllStockViewModel), typeof(string))]
    public class FundSummaryTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string stockType = (string)parameter;
            AllStockViewModel allStockViewModel = value as AllStockViewModel;         
            string output = string.Empty;

            output = string.Format("Total Number = {0}, Total Stock Weight = {1}, Total Market Value = {2}",
                    allStockViewModel.AllStocks.Where(p => p.StockType == stockType).Sum(p => p.Quantity),
                    allStockViewModel.AllStocks.Where(p => p.StockType == stockType).Sum(p => p.StockWeight),
                    allStockViewModel.AllStocks.Where(p => p.StockType == stockType).Sum(p => p.MarketValue));

            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
