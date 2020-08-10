using Git_system.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest_Git_system
{
    [TestFixture]
    internal class ProductTest
    {
        private Product courseProduct()
        {
            Product p1 = new Product();
            p1.Price = 100;
            p1.PriceCorporate = 100;
            p1.PriceIndividual = 100;
            p1.PriceInter = 3;
            p1.PriceCorporateInter = 3;
            p1.PriceIndividualInter = 3;
            p1.TitleTH = "สินค้าตัวแรก";
            p1.TitleEN = "First product";
            p1.VatTypeId = 2;

            return p1;
        }

        private Product membershipProduct()
        {
            Product p1 = new Product();
            p1.Price = 100;
            p1.PriceCorporate = 100;
            p1.PriceIndividual = 100;
            p1.PriceInter = 3;
            p1.PriceCorporateInter = 3;
            p1.PriceIndividualInter = 3;
            p1.TitleTH = "สมาชิก";
            p1.TitleEN = "membership";
            p1.VatTypeId = 1;

            return p1;
        }

        private List<Product> initListProduct()
        {
            List<Product> lp = new List<Product>();
            Product p1 = new Product();
            p1.Price = 100;
            p1.PriceCorporate = 100;
            p1.PriceIndividual = 100;
            p1.PriceInter = 3;
            p1.PriceCorporateInter = 3;
            p1.PriceIndividualInter = 3;
            p1.TitleTH = "สินค้าตัวแรก";
            p1.TitleEN = "First product";
            p1.VatTypeId = 2;

            lp.Add(p1);
            lp.Add(p1);

            return lp;
        }

        private Membership initMembership()
        {
            var mem = new Membership();
            mem.MembershipRegisterTypeId = 1;
            return mem;
        }

        [Test]
        public void getOriginVat()
        {
            var vat = Git_system.Models.Form.ExtensionMethod.getBackendVatSetting();
            Assert.AreEqual(7, vat);
        }

        [Test]
        public void getCoursePrice()
        {
            Assert.AreEqual(100d, courseProduct().toPriceForMembership(initMembership()));
        }

        [Test]
        public void getCourseVatPrice()
        {
            Assert.AreEqual(7, (int)courseProduct().toVatPriceForMembership(initMembership()));
        }

        [Test]
        public void getCourseTotalPrice()
        {
            Assert.AreEqual(107d, courseProduct().toTotalPriceForMembership(initMembership()));
        }

        [Test]
        public void getMemberPrice()
        {
            Assert.AreEqual(100d / 1.07, membershipProduct().toPriceForMembership(initMembership()));
        }

        [Test]
        public void getMemberVatPrice()
        {
            Assert.AreEqual((decimal)(100 - 100 / 1.07), (decimal)membershipProduct().toVatPriceForMembership(initMembership()));
        }

        [Test]
        public void getMemberTotalPrice()
        {
            Assert.AreEqual(100d, membershipProduct().toTotalPriceForMembership(initMembership()));
        }

        [Test]
        public void getListTotoalPrice()
        {
            List<Product> lp = new List<Product>();
            lp.Add(membershipProduct());
            lp.Add(courseProduct());
            double lpTotal = lp.Sum(l => l.toTotalPriceForMembership(initMembership()));
            Assert.AreEqual(207d, lpTotal);
        }
    }
}