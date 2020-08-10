using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest_Git_system
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(true, true, "Data not match.");
        }

        [TestMethod]//Test Git_system.Models.Helper.DateToSting
        public void DateToString()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            DateTime d1 = new DateTime(2015, 10, 15);
            DateTime d2 = new DateTime(2015, 10, 15);
            Assert.AreEqual("15 Oct 2015", Git_system.Models.Helper.DateToSting(d1, d2), "Case 1, Date not match.");

            DateTime d3 = new DateTime(2016, 7, 4);
            DateTime d4 = new DateTime(2016, 7, 16);
            Assert.AreEqual("4 - 16 Jul 2016", Git_system.Models.Helper.DateToSting(d3, d4), "Case 2, Date not match.");

            DateTime d5 = new DateTime(2016, 7, 2);
            DateTime d6 = new DateTime(2016, 12, 4);
            Assert.AreEqual("2 Jul - 4 Dec 2016", Git_system.Models.Helper.DateToSting(d5, d6), "Case 3, Date not match.");

            DateTime d7 = new DateTime(2015, 10, 2);
            DateTime d8 = new DateTime(2016, 1, 4);
            Assert.AreEqual("2 Oct 2015 - 4 Jan 2016", Git_system.Models.Helper.DateToSting(d7, d8), "Case 4, Date not match.");

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th");
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            DateTime d9 = new DateTime(2015, 10, 15);
            DateTime d10 = new DateTime(2015, 10, 15);
            Assert.AreEqual("15 ต.ค. 2558", Git_system.Models.Helper.DateToSting(d9, d10), "Case 5, Date not match.");

            DateTime d11 = new DateTime(2016, 7, 4);
            DateTime d12 = new DateTime(2016, 7, 16);
            Assert.AreEqual("4 - 16 ก.ค. 2559", Git_system.Models.Helper.DateToSting(d11, d12), "Case 6, Date not match.");

            DateTime d13 = new DateTime(2016, 7, 2);
            DateTime d14 = new DateTime(2016, 12, 4);
            Assert.AreEqual("2 ก.ค. - 4 ธ.ค. 2559", Git_system.Models.Helper.DateToSting(d13, d14), "Case 7, Date not match.");

            DateTime d15 = new DateTime(2015, 10, 2);
            DateTime d16 = new DateTime(2016, 1, 4);
            Assert.AreEqual("2 ต.ค. 2558 - 4 ม.ค. 2559", Git_system.Models.Helper.DateToSting(d15, d16), "Case 8, Date not match.");

            Assert.AreEqual("2 Oct 2015 - 4 Jan 2016", Git_system.Models.Helper.DateToSting(d15, d16, false, "en-GB"), "Case 9, Date not match.");
            Assert.AreEqual("2 ต.ค. 2558 - 4 ม.ค. 2559", Git_system.Models.Helper.DateToSting(d15, d16, false, "th"), "Case 10, Date not match.");
            Assert.AreEqual("2/10/2015 - 4/1/2016", Git_system.Models.Helper.DateToSting(d15, d16, true, "en-GB"), "Case 11, Date not match.");
            Assert.AreEqual("2/10/2558 - 4/1/2559", Git_system.Models.Helper.DateToSting(d15, d16, true, "th"), "Case 12, Date not match.");
        }

        [TestMethod]
        public void PriceAndCurrency()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            Assert.AreEqual("1,111.00 THB", Git_system.Models.Helper.PriceAndCurrency(1111.00, "thb"), "Data not match.");

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th");
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            Assert.AreEqual("1,000.12 บาท", Git_system.Models.Helper.PriceAndCurrency(1000.12, "thb"), "Data not match.");

            Assert.AreEqual("1,000.43 THB", Git_system.Models.Helper.PriceAndCurrency(1000.43, "THB", culture: "en-GB"), "Data not match.");
            Assert.AreEqual("1,000.22 บาท", Git_system.Models.Helper.PriceAndCurrency(1000.2234, "THB", culture: "th"), "Data not match.");
            Assert.AreEqual("1,000.65 USD", Git_system.Models.Helper.PriceAndCurrency(1000.6466, "USD", culture: "en-GB"), "Data not match.");
            Assert.AreEqual("1,000.23 USD", Git_system.Models.Helper.PriceAndCurrency(1000.23, "USD", culture: "th"), "Data not match.");

            Assert.AreEqual("0", Git_system.Models.Helper.PriceAndCurrency(0, null, true), "Data not match.");
            Assert.AreEqual("Free", Git_system.Models.Helper.PriceAndCurrency(0, "USD", culture: "en-GB"), "Data not match.");
            Assert.AreEqual("Free", Git_system.Models.Helper.PriceAndCurrency(0, "THB", culture: "en-GB"), "Data not match.");
        }
    }
}