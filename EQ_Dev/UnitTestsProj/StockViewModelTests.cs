using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EQDevApplication.Model;
using EQDevApplication.DataAccess;
using EQDevApplication.ViewModel;
using EQDevApplication;
using System.ComponentModel;

namespace UnitTestsProj
{
    /// <summary>
    /// Summary description for StockViewModelTests
    /// </summary>
    [TestClass]
    public class StockViewModelTests
    {
        public StockViewModelTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestStockType()
        {
            Stock stock = Stock.CreateNewStock();
            StockRepository repos = new StockRepository(Constants.STOCK_DATA_FILE);
            StockViewModel target = new StockViewModel(stock, repos);

            target.StockType = Strings.StockViewModel_FundTypeOption_Equity;
            Assert.AreEqual("Equity", stock.Type, "Should be a Equity");

            target.StockType = Strings.StockViewModel_FundTypeOption_Bond;
            Assert.AreEqual("Bond", stock.Type, "Should be a Bond");

            target.StockType = Strings.StockViewModel_StockTypeOption_NotSpecified;
            string error = (target as IDataErrorInfo)["StockType"];
            Assert.IsFalse(String.IsNullOrEmpty(error), "Error message should be returned");
        }

        [TestMethod]
        public void TestStockMarketValue()
        {
            Stock stock = Stock.CreateStock(Strings.StockViewModel_FundTypeOption_Equity, (decimal)20, 10);
            StockRepository repos = new StockRepository(Constants.STOCK_DATA_FILE);
            StockViewModel target = new StockViewModel(stock, repos);

            Assert.AreEqual((decimal)200, target.MarketValue, "Market value should be calculated.");
        }

        [TestMethod]
        public void TestStockTransactionCost_Equity()
        {
            Stock stock = Stock.CreateStock(Strings.StockViewModel_FundTypeOption_Equity, (decimal)20, 10);
            StockRepository repos = new StockRepository(Constants.STOCK_DATA_FILE);
            StockViewModel target = new StockViewModel(stock, repos);

            Assert.AreEqual((decimal)1, target.TransactionCost, "Transactional cost should be calculated.");
        }

        [TestMethod]
        public void TestStockTransactionCost_Bond()
        {
            Stock stock = Stock.CreateStock(Strings.StockViewModel_FundTypeOption_Bond, (decimal)20, 10);
            StockRepository repos = new StockRepository(Constants.STOCK_DATA_FILE);
            StockViewModel target = new StockViewModel(stock, repos);

            Assert.AreEqual((decimal)4, target.TransactionCost, "Transactional cost should be calculated.");
        }
    }
}
