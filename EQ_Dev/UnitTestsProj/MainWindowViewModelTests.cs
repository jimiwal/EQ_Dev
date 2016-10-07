using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EQDevApplication.ViewModel;
using EQDevApplication;
using System.Windows.Data;
using System.Windows;
using System.Windows.Resources;
using System.Reflection;

namespace UnitTestsProj
{
    /// <summary>
    /// Summary description for MainWindowViewModelTests
    /// </summary>
    [TestClass]
    public class MainWindowViewModelTests
    {
        public MainWindowViewModelTests()
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
        
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestMethod]
        public void CanLoadResource()
        {
            Uri uri = new Uri("Data/stocks.xml", UriKind.RelativeOrAbsolute);
            StreamResourceInfo info = Application.GetResourceStream(uri);
        }

        [TestMethod]
        public void TestViewAllStocks()
        {
            MainWindowViewModel target = new MainWindowViewModel(Constants.STOCK_DATA_FILE);
            CommandViewModel commandVM = target.Commands.First(cvm => cvm.DisplayName == Strings.MainWindowViewModel_Command_ViewAllStocks);
            commandVM.Command.Execute(null);

            var collectionView = CollectionViewSource.GetDefaultView(target.Workspaces);
            Assert.IsTrue(collectionView.CurrentItem is AllStockViewModel, "Invalid current item.");
        }

        [TestMethod]
        public void TestCreateNewStock()
        {
            MainWindowViewModel target = new MainWindowViewModel(Constants.STOCK_DATA_FILE);
            CommandViewModel commandVM = target.Commands.First(cvm => cvm.DisplayName == Strings.MainWindowViewModel_Command_CreateNewStock);
            commandVM.Command.Execute(null);

            var collectionView = CollectionViewSource.GetDefaultView(target.Workspaces);
            Assert.IsTrue(collectionView.CurrentItem is StockViewModel, "Invalid current item.");
        }

        [TestMethod]
        public void TestCannotViewAllStocksTwice()
        {
            MainWindowViewModel target = new MainWindowViewModel(Constants.STOCK_DATA_FILE);
            CommandViewModel commandVM = target.Commands.First(cvm => cvm.DisplayName == Strings.MainWindowViewModel_Command_ViewAllStocks);
            // Tell the ViewModel to show all customers twice.
            commandVM.Command.Execute(null);
            commandVM.Command.Execute(null);

            var collectionView = CollectionViewSource.GetDefaultView(target.Workspaces);
            Assert.IsTrue(collectionView.CurrentItem is AllStockViewModel, "Invalid current item.");
            Assert.IsTrue(target.Workspaces.Count == 1, "Invalid number of view models.");
        }

        [TestMethod]
        public void TestCloseAllStocksWorkspace()
        {
            // Create the MainWindowViewModel, but not the MainWindow.
            MainWindowViewModel target =
                new MainWindowViewModel(Constants.STOCK_DATA_FILE);

            Assert.AreEqual(0, target.Workspaces.Count, "Workspaces isn't empty.");

            // Find the command that opens the "All Stocks" workspace.
            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == Strings.MainWindowViewModel_Command_ViewAllStocks);

            // Open the "All Stocks" workspace.
            commandVM.Command.Execute(null);
            Assert.AreEqual(1, target.Workspaces.Count, "Did not create viewmodel.");

            // Ensure the correct type of workspace was created.
            var allStocksVM = target.Workspaces[0] as AllStockViewModel;
            Assert.IsNotNull(allStocksVM, "Wrong viewmodel type created.");

            // Tell the "All Stocks" workspace to close.
            allStocksVM.CloseCommand.Execute(null);
            Assert.AreEqual(0, target.Workspaces.Count, "Did not close viewmodel.");
        }
    }
}
