using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EQDevApplication.DataAccess;
using EQDevApplication.ViewModel;
using EQDevApplication.Model;
using EQDevApplication;
using System.Windows;

namespace UnitTestsProj
{
    /// <summary>
    /// Summary description for AllStockViewModelTests
    /// </summary>
    [TestClass]
    public class AllStockViewModelTests
    {
        public AllStockViewModelTests()
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            if (Application.ResourceAssembly == null)
            {
                Application.ResourceAssembly = typeof(MainWindow).Assembly;
            }
        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestTotalSelectedSales()
        {
            StockRepository repos = new StockRepository(Constants.STOCK_DATA_FILE);
            AllStockViewModel target = new AllStockViewModel(repos);

            int notifications = 0;
            target.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "TotalSelectedSummary")
                    ++notifications;
            };

            Assert.AreEqual("Total Number = 0, Total Stock Weight = 0, Total Market Value = 0", target.TotalSelectedSummary, "Should be zero when no stocks are selected");

            StockViewModel firstStock = target.AllStocks[0];

            firstStock.IsSelected = true;
            Assert.AreEqual(1, notifications, "TotalSelectedSales change notification was not raised");
            Assert.AreEqual("Total Number = 20, Total Stock Weight = 33,92, Total Market Value = 4400", target.TotalSelectedSummary);

            StockViewModel secondStock = target.AllStocks[1];
            secondStock.IsSelected = true;
            Assert.AreEqual(2, notifications, "TotalSelectedSales change notification was not raised again");
            Assert.AreEqual("Total Number = 30, Total Stock Weight = 45,49, Total Market Value = 5900", target.TotalSelectedSummary);
        }

        [TestMethod]
        public void TestNewStockIsAdded()
        {
            StockRepository repos = new StockRepository(Constants.STOCK_DATA_FILE);
            AllStockViewModel target = new AllStockViewModel(repos);

            Assert.AreEqual(4, target.AllStocks.Count, "Test data includes three stocks");

            repos.AddStock(Stock.CreateStock(Strings.StockViewModel_FundTypeOption_Equity, (decimal)15, 50));

            Assert.AreEqual(5, target.AllStocks.Count, "Adding a stock to the repository increase the Count");
        }

        [TestMethod]
        public void TestNewStockIsAddedThenTotalWeightIsChanged()
        {
            StockRepository repos = new StockRepository(Constants.STOCK_DATA_FILE);
            AllStockViewModel target = new AllStockViewModel(repos);

            StockViewModel firstStock = target.AllStocks[0];
            Assert.AreEqual((decimal)33.92, firstStock.StockWeight, "StockWeight Should be calculated");

            repos.AddStock(Stock.CreateStock(Strings.StockViewModel_FundTypeOption_Equity, (decimal)15, 50));
            Assert.AreEqual((decimal)32.07, firstStock.StockWeight, "StockWeight Should be calculated");
        }

        [TestMethod]
        public void TestNewStock_DisplayName()
        {
            StockRepository repos = new StockRepository(Constants.STOCK_DATA_FILE);
            AllStockViewModel target = new AllStockViewModel(repos);
            StockViewModel stockVm = new StockViewModel(Stock.CreateStock(Strings.StockViewModel_FundTypeOption_Equity, (decimal)15, 50), repos);

            Assert.AreEqual(Strings.StockViewModel_DisplayName, stockVm.DisplayName, "StockWeight Should be calculated");
        }

        [TestMethod]
        public void TestSavedStock_DisplayName()
        {
            StockRepository repos = new StockRepository(Constants.STOCK_DATA_FILE);
            AllStockViewModel target = new AllStockViewModel(repos);
            StockViewModel stockVm = new StockViewModel(Stock.CreateStock(Strings.StockViewModel_FundTypeOption_Equity, (decimal)15, 50), repos);
            stockVm.Save();

            Assert.AreEqual(string.Format("{0}{1}", Strings.StockViewModel_FundTypeOption_Equity, 50), stockVm.DisplayName, "StockWeight Should be calculated");
        }
    }
}
