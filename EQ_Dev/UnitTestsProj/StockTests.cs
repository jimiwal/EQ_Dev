using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EQDevApplication.Model;
using EQDevApplication;

namespace UnitTestsProj
{
    /// <summary>
    /// Summary description for StockTests
    /// </summary>
    [TestClass]
    public class StockTests
    {
        public StockTests()
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
        public void TestIsValid_Equity()
        {
            Stock target = Stock.CreateNewStock();
            target.Type = Strings.StockViewModel_FundTypeOption_Equity;
            target.Price = 166;
            target.Quantity = 34;

            Assert.IsTrue(target.IsValid, "Should be valid");
        }

        [TestMethod]
        public void TestIsValid_Bond()
        {
            Stock target = Stock.CreateNewStock();
            target.Type = Strings.StockViewModel_FundTypeOption_Bond;
            target.Price = 166;
            target.Quantity = 34;

            Assert.IsTrue(target.IsValid, "Should be valid");
        }

        [TestMethod]
        public void TestIsInValid_UnspecifiedType()
        {
            Stock target = Stock.CreateNewStock();
            target.Type = Strings.StockViewModel_StockTypeOption_NotSpecified;
            target.Price = 166;
            target.Quantity = 34;

            Assert.IsFalse(target.IsValid, "Should be valid");
        }

        [TestMethod]
        public void TestIsInvalidStockIfEmpty()
        {
            Stock target = Stock.CreateNewStock();

            Assert.IsFalse(target.IsValid, "Should be invalid");
        }

        [TestMethod]
        public void TestIsInvalidStockIfPriceNotExists()
        {
            Stock target = Stock.CreateNewStock();
            target.Type = Strings.StockViewModel_FundTypeOption_Equity;

            target.Price = -150;
            target.Quantity = 44;
            Assert.IsFalse(target.IsValid, "Should be invalid");

            target.Price = 0;
            target.Quantity = 44;
            Assert.IsFalse(target.IsValid, "Should be invalid");
        }

        [TestMethod]
        public void TestIsInvalidStockIfQuantityNotExists()
        {
            Stock target = Stock.CreateNewStock();
            target.Type = Strings.StockViewModel_FundTypeOption_Equity;

            target.Price = 80;
            target.Quantity = -5;
            Assert.IsFalse(target.IsValid, "Should be invalid");

            target.Price = 80;
            target.Quantity = 0;
            Assert.IsFalse(target.IsValid, "Should be invalid");
        }
    }
}
