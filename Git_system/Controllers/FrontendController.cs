using Git_system.Models;
using Git_system.Models.Form;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Git_system.Controllers
{
    public class Frontend_AuthorizeAttrinbite : FilterAttribute, IAuthorizationFilter
    {
        // For web
        // http://stackoverflow.com/questions/17116494/how-to-create-a-custom-authorizeattribute-that-redirects
        // http://www.codeproject.com/Articles/421639/Customizing-the-Authorize-attribute
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpCookie facAut = filterContext.HttpContext.Request.Cookies[".FOENAUTH"];
            System.Web.Security.FormsAuthenticationTicket _FOENAUTH = facAut == null ? new System.Web.Security.FormsAuthenticationTicket(null, true, 0) : System.Web.Security.FormsAuthentication.Decrypt(facAut.Value);

            if (_FOENAUTH.Name != null)
            {
                Git_system.Models.Form.UserAuthorize userAuthorize = Newtonsoft.Json.JsonConvert.DeserializeObject<Git_system.Models.Form.UserAuthorize>(_FOENAUTH.UserData);

                if (!userAuthorize.Type.Equals("User"))//ตรวจสอบว่าเป็น user ประเภท User หรือไม่
                {
                    filterContext.Result = new RedirectToRouteResult(new
                                                RouteValueDictionary(new { controller = "Home", action = "Index" }));
                    System.Web.Security.FormsAuthentication.SignOut();
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                            RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
        }
    }

    public class FrontendController : Controller
    {
        protected Database_MainEntities1 db = new Database_MainEntities1();

        protected int CurrentUserId { get; set; }
        protected string _LanguageType { get; set; }

        #region Helpers

        protected void changeLanguage(string language)
        {
            language = language != null ? language : "";

            System.Globalization.CultureInfo currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (language == "")
            {
                if (currentCulture.Name == "th")
                    language = "th";
                else
                    language = "en";
            }

            string CultureNameLanguageTypeValue = null;
            CultureNameLanguageTypeValue = language.ToUpper() == "EN" ? "f11b8154674d1f97d062be097d3a5ccab0fc27ee" : "aee1594f51ff82ced59dd1b2ad1a8c6d11b32a45";

            HttpCookie CultureNameLanguageType = new HttpCookie("FCNLT");
            CultureNameLanguageType.Value = CultureNameLanguageTypeValue;
            CultureNameLanguageType.HttpOnly = true;
            Response.Cookies.Add(CultureNameLanguageType);

            string cultureName = language.ToUpper() == "EN" ? "en-GB" : "th";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            this._LanguageType = Git_system.MultiResources.Multi.__LanguageType;
        }

        protected void frontEndLogout()
        {
            HttpCookie facAut = new HttpCookie(".FOENAUTH");
            facAut.Expires = DateTime.Now.AddDays(-1d);
            facAut.HttpOnly = true;
            Response.Cookies.Add(facAut);

            HttpCookie frontendCookiePayItem = new HttpCookie("PIM");
            frontendCookiePayItem.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(frontendCookiePayItem);
        }

        public ActionResult Logout()
        {
            frontEndLogout();

            return RedirectToAction("Index");
        }

        protected Pay SavePayment(int membershipId, short payType, short PayTypeId, List<PayItem> PayItems, string fullname, string address)
        {
            //
            //Modified
            Membership membershipView = db.Memberships.Find(CurrentUserId);
            membershipView = membershipView.CheckMembershipType();

            DateTime now = DateTime.Now;

            //
            // add Pay
            Pay pay = new Pay();
            pay.Date = now;
            pay.MembershipId = membershipId;
            pay.PayTypeId = PayTypeId;
            pay.ProcessStatusTypeId = 1;
            pay.Type = payType;

            pay.Fullname = fullname;
            pay.Address = address;

            pay.Currency = Git_system.Models.Helper.getCurrency(membershipView);

            db.Pays.Add(pay);
            db.SaveChanges();
            // add Pay
            //

            //
            // add PayItem
            List<PayItem> payItems = PayItems;
            foreach (var item in payItems)
            {
                item.PayId = pay.Id;
                db.PayItems.Add(item);
                db.SaveChanges();
            }
            // add PayItem
            //
            pay.Price = APICheckPromotionWithVatAndTotalPrice(membershipView.MembershipRegisterTypeId, PayItems).TotalPrice;
            pay.ProcessStatusTypeId = pay.Price == 0 ? Convert.ToInt16(2) : pay.ProcessStatusTypeId;
            db.Entry(pay).State = EntityState.Modified;
            //db.Entry(pay).CurrentValues.SetValues(payM);
            db.SaveChanges();
            initCourseInPayItems(pay.PayItems.ToList());
            //Modified
            //
            return pay;
        }

        protected Pay SavePayment(int membershipId, short payType, short PayTypeId, List<PayItem> PayItems)
        {
            return SavePayment(membershipId, payType, PayTypeId, PayItems, "", "");
        }

        public static List<Git_system.Models.Form.ProductSKUOut> check_promotion(int Membership, List<PayItem> Payitems)
        {
            return new FrontendController().APICheckPromotion(Membership, Payitems);
        }

        public static double getVatForPayments(Membership membership, List<PayItem> payItems)
        {
            var promotionWithVatAndTotalPrice = new FrontendController().APICheckPromotionWithVatAndTotalPrice(membership.MembershipRegisterTypeId, payItems).Vat;
            return promotionWithVatAndTotalPrice;
        }

        public static double getSumPriceForPayments(Membership membership, List<PayItem> payItems)
        {
            var promotionWithVatAndTotalPrice = new FrontendController().APICheckPromotionWithVatAndTotalPrice(membership.MembershipRegisterTypeId, payItems).Price;
            return promotionWithVatAndTotalPrice;
        }

        private List<PayItem> initCourseInPayItems(List<PayItem> payItems)
        {
            Database_MainEntities1 connectInScope = new Database_MainEntities1();
            foreach (var item in payItems)
            {
                item.Course = new Course();
                Course course = connectInScope.Courses.Find(item.CourseId);
                item.Course = course;
            }
            return payItems;
        }

        private List<ProductSKU> getProductSKUForMembersip(List<ProductSKU> productSKUs, Membership membership)
        {
            switch (membership.MembershipRegisterTypeId)
            {
                case 1:
                    productSKUs = productSKUs.Where(p => p.isGuest == true).ToList();
                    break;

                case 2:
                    productSKUs = productSKUs.Where(p => p.isGuest_Inter == true).ToList();
                    break;

                case 3:
                    productSKUs = productSKUs.Where(p => p.isIndividual == true).ToList();
                    break;

                case 4:
                    productSKUs = productSKUs.Where(p => p.isIndividual_Inter == true).ToList();
                    break;

                case 5:
                    productSKUs = productSKUs.Where(p => p.isCorporate == true).ToList();
                    break;

                case 6:
                    productSKUs = productSKUs.Where(p => p.isCorporate_Inter == true).ToList();
                    break;
            }
            return productSKUs;
        }

        private void removePayItem(List<PayItem> originalPayItems, List<PayItem> removePayItems)
        {
            originalPayItems.RemoveAll(p => removePayItems.Select(pr => pr.CourseId).Any(prx => prx == p.CourseId));
        }

        private void removePayItem(List<PayItem> originalPayItems, PayItem removePayItem)
        {
            originalPayItems.Remove(removePayItem);
        }

        private List<ProductSKUOut> addToProductSKUs(ProductSKU pSKU, List<ProductSKUOut> pSKUsO, Membership m, List<PayItem> pI)
        {
            double pricePayment = 0;
            pricePayment = pI.Sum(p => p.Quantity * p.Course.Product.toPriceForMembership(m));

            if (((pSKU.Percent / 100) * pricePayment) > 0)
            {
                pSKUsO.Add(new Git_system.Models.Form.ProductSKUOut(pSKU.Id, pSKU.NameTH, pSKU.NameEN, (pSKU.Percent / 100) * pricePayment));
                changePriceOfPayItems(pI, pSKU.Percent);
            }

            return pSKUsO;
        }

        private void changePriceOfPayItems(List<PayItem> payItems, double discount)
        {
            var products = payItems.Where(p => p.Course.Product.ProductSKUActive == true).Select(p => p.Course.Product).ToList();
            var productsDistinct = products.Distinct();
            foreach (Product product in productsDistinct)
            {
                product.Price = product.Price * (1 - discount / 100);
                product.PriceCorporate = product.PriceCorporate * (1 - discount / 100);
                product.PriceCorporateInter = product.PriceCorporateInter * (1 - discount / 100);
                product.PriceIndividual = product.PriceIndividual * (1 - discount / 100);
                product.PriceIndividualInter = product.PriceIndividualInter * (1 - discount / 100);
                product.PriceInter = product.PriceInter * (1 - discount / 100);
                product.ProductSKUActive = false;
            }
        }

        // Type 1
        private List<ProductSKUOut> discountForMembership(List<ProductSKU> productSKUs, List<PayItem> payItems, Membership membership, List<ProductSKUOut> productSKUOuts)
        {
            // Get Max discount when productSKU type equal 1.
            ProductSKU productSKU = new ProductSKU();
            productSKU = productSKUs.Where(p => p.Type == 1).OrderByDescending(p => p.Percent).FirstOrDefault();
            if (null != productSKU)
            {
                addToProductSKUs(productSKU, productSKUOuts, membership, payItems.Where(p => p.Course.Product.ProductSKUActive == true).ToList());
                //removePayItem(payItems, payItems);
            }

            return productSKUOuts;
        }

        //Type 2
        private List<ProductSKUOut> discountByQuantity(List<ProductSKU> productSKUs, List<PayItem> payItems, Membership membership, List<ProductSKUOut> productSKUOuts)
        {
            int countQuantity = payItems.Where(p => p.Course.Product.ProductSKUActive == true).Sum(p => p.Quantity);

            // Get Max Operator when productSKU type equal 2
            ProductSKU productSKU = new ProductSKU();
            productSKU = productSKUs.Where(p => p.Type == 2 && p.Operator <= countQuantity).OrderByDescending(p => p.Operator).FirstOrDefault();

            if (null != productSKU)
            {
                addToProductSKUs(productSKU, productSKUOuts, membership, payItems.Where(p => p.Course.Product.ProductSKUActive == true).ToList());
                //removePayItem(payItems, payItems);
            }

            return productSKUOuts;
        }

        //Type 3
        private List<ProductSKUOut> discountByCouse(List<ProductSKU> productSKUs, List<PayItem> payItems, Membership membership, List<ProductSKUOut> productSKUOuts)
        {
            int countQuantity = payItems.Where(p => p.Course.Product.ProductSKUActive == true).ToList().Sum(p => p.Quantity);

            var productSKUsWithType3 = new List<ProductSKU>();
            productSKUsWithType3 = productSKUs.Where(p => p.Type == 3).ToList();
            foreach (var productSKUWithType3 in productSKUsWithType3)
            {
                var courseMatch = payItems.Where(p => p.CourseId.Equals(productSKUWithType3.CourseId) && p.Course.Product.ProductSKUActive == true).ToList();
                if (courseMatch.Count() > 0)
                {
                    addToProductSKUs(productSKUWithType3, productSKUOuts, membership, courseMatch);
                    //removePayItem(payItems, courseMatch);
                }
            }

            return productSKUOuts;
        }

        //Type 4
        private List<ProductSKUOut> discountByCouseWithQuantity(List<ProductSKU> productSKUs, List<PayItem> payItems, Membership membership, List<ProductSKUOut> productSKUOuts)
        {
            int countQuantity = payItems.Where(p => p.Course.Product.ProductSKUActive == true).Sum(p => p.Quantity);

            var productSKUsWithType4 = new List<ProductSKU>();
            productSKUsWithType4 = productSKUs.Where(p => p.Type == 4).ToList();

            foreach (var payItem in payItems.Where(p => p.Course.Product.ProductSKUActive == true))
            {
                var productSKUMatch = productSKUsWithType4.Where(p => p.CourseId.Equals(payItem.CourseId) && p.Operator <= payItem.Quantity).OrderByDescending(p => p.Operator).FirstOrDefault();
                if (null != productSKUMatch)
                {
                    addToProductSKUs(productSKUMatch, productSKUOuts, membership, new List<PayItem>() { payItem });
                    //removePayItem(payItems, payItem);
                }
            }

            return productSKUOuts;
        }

        //Type 5
        private List<ProductSKUOut> discountByProductWithQuantity(List<ProductSKU> productSKUs, List<PayItem> payItems, Membership membership, List<ProductSKUOut> productSKUOuts)
        {
            int countQuantity = payItems.Where(p => p.Course.Product.ProductSKUActive == true).Count();

            ProductSKU productSKU = new ProductSKU();
            productSKU = productSKUs.Where(p => p.Type == 5 && p.Operator <= countQuantity).OrderByDescending(p => p.Operator).FirstOrDefault();

            if (null != productSKU)
            {
                addToProductSKUs(productSKU, productSKUOuts, membership, payItems.Where(p => p.Course.Product.ProductSKUActive == true).ToList());
                //removePayItem(payItems, payItems);
            }

            return productSKUOuts;
        }

        protected List<Git_system.Models.Form.ProductSKUOut> APICheckPromotion(int Membership, List<PayItem> Payitems)
        {
            var promotionWithVatAndTotalPrice = APICheckPromotionWithVatAndTotalPrice(Membership, Payitems);
            return promotionWithVatAndTotalPrice.ProductSKUOuts;
        }

        protected PromotionWithVatAndTotalPrice APICheckPromotionWithVatAndTotalPrice(int Membership, List<PayItem> originalPayitems)
        {
            List<PayItem> Payitems = new List<PayItem>();
            Payitems = originalPayitems.ConvertAll(p => (PayItem)p.Clone()).ToList();
            List<PayItem> payItems = new List<PayItem>();
            try
            {
                var x = Payitems.Select(p => p.Course).FirstOrDefault();
                Payitems = initCourseInPayItems(Payitems);
                payItems = Payitems.ConvertAll(p => (PayItem)p.Clone()).ToList();
            }
            catch
            {
                Payitems = originalPayitems;
                payItems = Payitems;
            }

            Membership membership = new Membership();
            membership.MembershipRegisterTypeId = (short)(Membership);

            List<ProductSKU> productSKUs = db.ProductSKUs.Where(p => p.Active == true).ToList();

            productSKUs = getProductSKUForMembersip(productSKUs, membership); // Get productSKU for membership

            var fl = new List<Func<List<ProductSKU>, List<PayItem>, Git_system.Models.Membership, List<ProductSKUOut>, List<ProductSKUOut>>>{
                discountByCouse,
                discountByCouseWithQuantity,
                discountByProductWithQuantity,
                discountByQuantity,
                discountForMembership
            };
            List<Git_system.Models.Form.ProductSKUOut> productSKUOuts = new List<Git_system.Models.Form.ProductSKUOut>();
            foreach (var f in fl)
            {
                var payItemsActive = payItems.Where(p => p.Course.Product.ProductSKUActive == true && p.Course.ProductSKUActive == true).ToList();
                productSKUOuts = f(productSKUs, payItemsActive, membership, productSKUOuts);
            }

            var sumPriceWithOutPromotion = initCourseInPayItems(Payitems).Select(p => p.Course.Product.toPriceForMembership(membership) * p.Quantity).Sum();
            var sumPrice = payItems.Select(p => p.Course.Product.toPriceForMembership(membership) * p.Quantity).Sum();
            var sumVat = payItems.Select(p => p.Course.Product.toVatPriceForMembership(membership) * p.Quantity).Sum();
            var sumTotalPrice = payItems.Select(p => p.Course.Product.toTotalPriceForMembership(membership) * p.Quantity).Sum();

            var promotionWithVatAndTotalPrice = new PromotionWithVatAndTotalPrice(sumPriceWithOutPromotion, sumPrice, sumVat, sumTotalPrice, productSKUOuts);
            return promotionWithVatAndTotalPrice;
        }

        protected class PromotionWithVatAndTotalPrice
        {
            public double PriceWithOutPromotion { get; set; }
            public double Price { get; set; }
            public double Vat { get; set; }
            public double TotalPrice { get; set; }
            public List<ProductSKUOut> ProductSKUOuts { get; set; }

            public PromotionWithVatAndTotalPrice(double priceWithOutPromotion, double price, double vat, double totalPrice, List<ProductSKUOut> productSKUOuts)
            {
                this.PriceWithOutPromotion = priceWithOutPromotion;
                this.Price = price;
                this.Vat = vat;
                this.TotalPrice = totalPrice;
                this.ProductSKUOuts = productSKUOuts;
            }
        }

        #endregion Helpers
    }
}