using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EQDevApplication.ViewModel;

namespace UnitTestsProj
{
    [TestClass]
    public class ViewModelBaseTests
    {
        #region Boilerplate Code

        public ViewModelBaseTests()
        {
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

        #endregion

        [TestMethod]
        public void TestPropertyChangedIsRaisedCorrectly()
        {
            TestViewModel target = new TestViewModel();
            bool eventWasRaised = false;
            target.PropertyChanged += (sender, e) => eventWasRaised = e.PropertyName == "GoodProperty";
            target.GoodProperty = "Some new value...";
            Assert.IsTrue(eventWasRaised, "PropertyChanged event was not raised correctly.");
        }

        [TestMethod]
        public void TestExceptionIsThrownOnInvalidPropertyName()
        {
            TestViewModel target = new TestViewModel();
            try
            {
                target.BadProperty = "Some new value...";
#if DEBUG
                Assert.Fail("Exception was not thrown when invalid property name was used in DEBUG build.");
#endif
            }
            catch (Exception)
            {
#if !DEBUG
                Assert.Fail("Exception was thrown when invalid property name was used in RELEASE build.");
#endif
            }
        }

        private class TestViewModel : ViewModelBase
        {
            protected override bool ThrowOnInvalidPropertyName
            {
                get { return true; }
            }

            public string GoodProperty
            {
                get { return null; }
                set
                {
                    base.OnPropertyChanged("GoodProperty");
                }
            }

            public string BadProperty
            {
                get { return null; }
                set
                {
                    base.OnPropertyChanged("ThisIsAnInvalidPropertyName!");
                }
            }
        }
    }    
}
