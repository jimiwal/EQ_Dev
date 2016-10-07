using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EQDevApplication.DataAccess;

namespace EQDevApplication.ViewModel
{

    /// <summary>
    /// Represents a container of StockViewModel objects
    /// that has support for staying synchronized with the
    /// StockRepository.  This class also provides information
    /// related to multiple selected stocks.
    /// </summary>
    public class AllStockViewModel : WorkspaceViewModel
    {
        #region Fields

        readonly StockRepository _stockRepository;

        #endregion // Fields

        #region Constructor

        public AllStockViewModel(StockRepository stockRepository)
        {
            if (stockRepository == null)
                throw new ArgumentNullException("stockRepository");

            base.DisplayName = Strings.AllStocksViewModel_DisplayName;            

            _stockRepository = stockRepository;

            // Subscribe for notifications of when a new stock is saved.
            _stockRepository.StockAdded += this.OnStockAddedToRepository;

            // Populate the AllStocks collection with StockViewModels.
            this.CreateAllStocks();              
        }

        void CreateAllStocks()
        {
            List<StockViewModel> all =
                (from stock in _stockRepository.GetStocks()
                 select new StockViewModel(stock, _stockRepository)).ToList();

            decimal totalMarketValue = all.Sum(p => p.MarketValue);

            foreach (StockViewModel cvm in all)
            {
                cvm.PropertyChanged += this.OnStockViewModelPropertyChanged;
                cvm.StockWeight = decimal.Round((cvm.MarketValue / totalMarketValue) * 100, 2, MidpointRounding.AwayFromZero);
            }

            this.AllStocks = new ObservableCollection<StockViewModel>(all);
            this.AllStocks.CollectionChanged += this.OnCollectionChanged;
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Returns a collection of all the StockViewModel objects.
        /// </summary>
        public ObservableCollection<StockViewModel> AllStocks { get; private set; }

        /// <summary>
        /// Returns the total price sum of all selected stocks.
        /// </summary>
        public string TotalSelectedSummary
        {
            get
            {
                return string.Format("Total Number = {0}, Total Stock Weight = {1}, Total Market Value = {2}",
                    AllStocks.Where(p => p.IsSelected).Sum(p => p.Quantity),
                    AllStocks.Where(p => p.IsSelected).Sum(p => p.StockWeight),
                    AllStocks.Where(p => p.IsSelected).Sum(p => p.MarketValue));                   
            }
        }

        #endregion // Public Interface

        #region  Base Class Overrides

        protected override void OnDispose()
        {
            foreach (StockViewModel stockVM in this.AllStocks)
                stockVM.Dispose();

            this.AllStocks.Clear();
            this.AllStocks.CollectionChanged -= this.OnCollectionChanged;

            _stockRepository.StockAdded -= this.OnStockAddedToRepository;
        }

        #endregion // Base Class Overrides

        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (StockViewModel stockVM in e.NewItems)
                    stockVM.PropertyChanged += this.OnStockViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (StockViewModel stockVM in e.OldItems)
                    stockVM.PropertyChanged -= this.OnStockViewModelPropertyChanged;
        }

        void OnStockViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as StockViewModel).VerifyPropertyName(IsSelected);

            // When a stock is selected or unselected, we must let the
            // world know that the TotalSelectedSales property has changed,
            // so that it will be queried again for a new value.
            if (e.PropertyName == IsSelected)
                this.OnPropertyChanged("TotalSelectedSummary");
        }

        void OnStockAddedToRepository(object sender, StockAddedEventArgs e)
        {
            var viewModel = new StockViewModel(e.NewStock, _stockRepository);
            this.AllStocks.Add(viewModel);

            // Calculate new value of total market value
            decimal totalMarketValue = this.AllStocks.Sum(vm => vm.MarketValue);
            foreach (var cvm in AllStocks)
            {
                cvm.StockWeight = decimal.Round((cvm.MarketValue / totalMarketValue) * 100, 2, MidpointRounding.AwayFromZero);
            }
        }

        #endregion // Event Handling Methods
    }
}
