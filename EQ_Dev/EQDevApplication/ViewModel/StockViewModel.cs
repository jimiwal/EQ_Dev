using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EQDevApplication.DataAccess;
using EQDevApplication.Model;

namespace EQDevApplication.ViewModel
{
    public class StockViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Fields

        readonly Stock _stock;
        readonly StockRepository _stockRepository;
        string[] _stockTypeOptions;
        bool _isSelected;
        decimal _stockWeight;
        RelayCommand _saveCommand;

        #endregion // Fields

        #region Constructor

        public StockViewModel(Stock stock, StockRepository stockRepository)
        {
            if (stock == null)
                throw new ArgumentNullException("stock");

            if (stockRepository == null)
                throw new ArgumentNullException("stockRepository");

            _stock = stock;
            _stockRepository = stockRepository;
        }

        #endregion // Constructor

        #region Stock Properties

        public decimal Price
        {
            get { return _stock.Price; }
            set
            {
                if (value == _stock.Price)
                    return;

                _stock.Price = value;

                base.OnPropertyChanged("Price");
            }
        }

        public int Quantity
        {
            get { return _stock.Quantity; }
            set
            {
                if (value == _stock.Quantity)
                    return;

                _stock.Quantity = value;

                base.OnPropertyChanged("Quantity");
            }
        }

        #endregion // Stock Properties

        #region Presentation Properties

        /// <summary>
        /// Gets/sets a value that indicates what type of stock this is.
        /// </summary>
        public string StockType
        {
            get 
            {
                return _stock.Type; 
            }
            set
            {
                _stock.Type = value;
                base.OnPropertyChanged("StockType");
            }
        }

        /// <summary>
        /// Returns a market value of the stock. Calculated as Price * Quantity
        /// </summary>
        public decimal MarketValue
        {
            get
            {
                return _stock.Price * _stock.Quantity;
            }
        }

        /// <summary>
        /// Returns a transaction cost of the stock. Calculated as MarketValue * 0,5% for Equity or MarketValue * 2% for Bond.
        /// </summary>
        public decimal TransactionCost
        {
            get
            {
                if (_stock.Type == Strings.StockViewModel_FundTypeOption_Equity)
                {
                    return decimal.Round(MarketValue * (decimal)0.005, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    return decimal.Round(MarketValue * (decimal)0.02, 2, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// Returns a transaction cost of the stock. Calculated as MarketValue * 0,5% for Equity or MarketValue * 2% for Bond.
        /// </summary>
        public decimal StockWeight
        {
            get
            {
                return _stockWeight;
            }
            set
            {
                _stockWeight = value;
                base.OnPropertyChanged("StockWeight");
            }
        }

        /// <summary>
        /// Returns a list of strings used to populate the Stock Type selector.
        /// </summary>
        public string[] StockTypeOptions
        {
            get
            {
                if (_stockTypeOptions == null)
                {
                    _stockTypeOptions = new string[]
                    {
                        Strings.StockViewModel_StockTypeOption_NotSpecified,
                        Strings.StockViewModel_FundTypeOption_Equity,
                        Strings.StockViewModel_FundTypeOption_Bond
                    };
                }
                return _stockTypeOptions;
            }
        }

        public override string DisplayName
        {
            get
            {
                if (this.IsNewStock)
                {
                    return Strings.StockViewModel_DisplayName;
                }
                else
                {
                    return String.Format("{0}{1}", _stock.Type, _stock.Quantity);
                }
            }
        }

        /// <summary>
        /// Gets/sets whether this stock is selected in the UI.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                    return;

                _isSelected = value;

                base.OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Returns a command that saves the stock.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.Save(),
                        param => this.CanSave
                        );
                }
                return _saveCommand;
            }
        }

        #endregion // Presentation Properties

        #region Public Methods

        /// <summary>
        /// Saves the stock to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save()
        {
            if (!_stock.IsValid)
                throw new InvalidOperationException(Strings.StockViewModel_Exception_CannotSave);

            if (this.IsNewStock)
                _stockRepository.AddStock(_stock);
            
            base.OnPropertyChanged("DisplayName");
        }

        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this stock was created by the user and it has not yet
        /// been saved to the stock repository.
        /// </summary>
        bool IsNewStock
        {
            get { return !_stockRepository.ContainsStock(_stock); }
        }

        /// <summary>
        /// Returns true if the stock is valid and can be saved.
        /// </summary>
        bool CanSave
        {
            get { return String.IsNullOrEmpty(this.ValidateStockType()) && _stock.IsValid; }
        }

        #endregion // Private Helpers

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return (_stock as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                if (propertyName == "StockType")
                {
                    // The IsCompany property of the Stock class 
                    // is Boolean, so it has no concept of being in
                    // an "unselected" state.  The StockViewModel
                    // class handles this mapping and validation.
                    error = this.ValidateStockType();
                }
                else
                {
                    error = (_stock as IDataErrorInfo)[propertyName];
                }

                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }

        string ValidateStockType()
        {
            if (this.StockType == Strings.StockViewModel_FundTypeOption_Bond ||
               this.StockType == Strings.StockViewModel_FundTypeOption_Equity)
                return null;

            return Strings.StockViewModel_Error_MissingStockType;
        }

        #endregion // IDataErrorInfo Members
    }
}
