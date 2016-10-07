using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Linq;
using EQDevApplication.Model;

namespace EQDevApplication.DataAccess
{
    /// <summary>
    /// Represents a source of stocks in the application.
    /// </summary>
    public class StockRepository
    {
        #region Fields

        readonly List<Stock> _stocks;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of stocks.
        /// </summary>
        /// <param name="stockDataFile">The relative path to an XML resource file that contains stock data.</param>
        public StockRepository(string stockDataFile)
        {
            _stocks = LoadStocks(stockDataFile);
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a stock is placed into the repository.
        /// </summary>
        public event EventHandler<StockAddedEventArgs> StockAdded;

        /// <summary>
        /// Places the specified stock into the repository.
        /// If the stock is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void AddStock(Stock stock)
        {
            if (stock == null)
                throw new ArgumentNullException("stock");

            if (!_stocks.Contains(stock))
            {
                _stocks.Add(stock);

                if (this.StockAdded != null)
                    this.StockAdded(this, new StockAddedEventArgs(stock));
            }
        }

        /// <summary>
        /// Returns true if the specified stock exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsStock(Stock stock)
        {
            if (stock == null)
                throw new ArgumentNullException("stock");

            return _stocks.Contains(stock);
        }

        /// <summary>
        /// Returns a shallow-copied list of all stocks in the repository.
        /// </summary>
        public List<Stock> GetStocks()
        {
            return new List<Stock>(_stocks);
        }

        #endregion // Public Interface

        #region Private Helpers

        static List<Stock> LoadStocks(string stockDataFile)
        {
            // In a real application, the data would come from an external source,
            // but for this app let's keep things simple and use a resource file.
            using (Stream stream = GetResourceStream(stockDataFile))
            using (XmlReader xmlRdr = new XmlTextReader(stream))
                return
                    (from stockElem in XDocument.Load(xmlRdr).Element("stocks").Elements("stock")
                     select Stock.CreateStock(
                        (string)stockElem.Attribute("type"),
                        (decimal)stockElem.Attribute("price"),
                        (int)stockElem.Attribute("quantity")
                         )).ToList();
        }

        static Stream GetResourceStream(string resourceFile)
        {
            Uri uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);

            StreamResourceInfo info = Application.GetResourceStream(uri);
            if (info == null || info.Stream == null)
                throw new ApplicationException("Missing resource file: " + resourceFile);

            return info.Stream;
        }

        #endregion // Private Helpers
    }
}
