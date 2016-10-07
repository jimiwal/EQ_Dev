using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EQDevApplication.DataAccess;
using EQDevApplication.Model;

namespace UnitTestsProj
{
    /// <summary>
    /// Summary description for StockRepositoryTests
    /// </summary>
    [TestClass]
    public class StockRepositoryTests
    {
        public StockRepositoryTests()
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
        public void TestAllStocksAreLoaded()
        {
            StockRepository target = new StockRepository(Constants.STOCK_DATA_FILE);
            Assert.AreEqual(4, target.GetStocks().Count, "Invalid number of stocks in repository.");
        }

        [TestMethod]
        public void TestNewStockIsAddedProperly()
        {
            StockRepository target = new StockRepository(Constants.STOCK_DATA_FILE);
            Stock stock = Stock.CreateNewStock();

            bool eventArgIsValid = false;
            target.StockAdded += (sender, e) => eventArgIsValid = (e.NewStock == stock);
            target.AddStock(stock);

            Assert.IsTrue(eventArgIsValid, "Invalid NewStock property");
            Assert.IsTrue(target.ContainsStock(stock), "New stock was not added");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "A userId of null was inappropriately allowed.")]
        public void TestAddNullStockThrowException()
        {
            StockRepository target = new StockRepository(Constants.STOCK_DATA_FILE);
            Stock stock = null;
            target.AddStock(stock);
        }

        [TestMethod]
        public void TestCantAddTwoTheSameStockInstances()
        {
            StockRepository target = new StockRepository(Constants.STOCK_DATA_FILE);
            Stock stock = Stock.CreateNewStock();
            int stockAdded = 0;
            bool eventArgIsValid = false;
            target.StockAdded += (sender, e) => 
            {
                eventArgIsValid = (e.NewStock == stock);
                stockAdded++;
            };
            target.AddStock(stock);
            target.AddStock(stock);

            Assert.AreEqual(1, stockAdded, "You can add only one instance of given stock");           
        }
    }
}
