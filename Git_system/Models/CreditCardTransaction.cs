using Git_system.Models.Form;
using Git_system.MultiResources;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Models
{
    internal class TransactionDecline : Exception
    {
        public TransactionDecline(string message)
            : base(message)
        {
        }
    }

    internal class TransactionEComDecline : Exception
    {
        public TransactionEComDecline(string message)
            : base(message)
        {
        }
    }

    internal class TransactionRefFail : Exception
    {
        public TransactionRefFail(string message)
            : base(message)
        {
        }
    }

    internal class TransactionSignatureFail : Exception
    {
        public TransactionSignatureFail(string message)
            : base(message)
        {
        }
    }

    public class CreditCardTransaction
    {
        protected Database_MainEntities1 db = new Database_MainEntities1();

        public FormCollection collection { get; private set; }
        public string signature { get; private set; }
        public string req_reference_number { get; private set; }
        public string req_currency { get; private set; }
        public string req_amount { get; private set; }
        public DateTime? signed_date_time { get; private set; }
        public string req_bill_to_forename { get; private set; }
        public string req_bill_to_surname { get; private set; }
        public string transaction_id { get; private set; }
        public string decision { get; private set; }

        private System.Collections.Hashtable allParameters { get; set; }

        public CreditCardTransaction(FormCollection collection = null, string signature = "", string req_reference_number = "", string req_currency = "", string req_amount = "", DateTime? signed_date_time = null, string req_bill_to_forename = "", string req_bill_to_surname = "", string transaction_id = null, string decision = "DECLINE")
        {
            this.collection = collection;
            this.signature = signature;
            this.req_reference_number = req_reference_number;
            this.req_currency = req_currency;
            this.req_amount = req_amount;
            this.signed_date_time = signed_date_time;
            this.req_bill_to_forename = req_bill_to_forename;
            this.req_bill_to_surname = req_bill_to_surname;
            this.transaction_id = transaction_id;
            this.decision = decision;
        }

        private void checkSignature(FormCollection collection = null, string signature = "")
        {
            var parameters = new System.Collections.Hashtable();
            foreach (var key in collection.AllKeys)
            {
                parameters.Add(key, collection[key]);
            }
            this.allParameters = parameters;

            var reSignature = Git_system.Helper.CreditCard.sign(this.allParameters);

            bool checkSignature = reSignature == signature;

            if (!checkSignature)
                throw new TransactionSignatureFail("");
        }

        public Frontend_ShowMessage process(String languageType)
        {
            Func<String, String, Frontend_ShowMessage, Frontend_ShowMessage> log = (s, d, m) =>
            {
                Git_system.Helper.ErrLogHelper.logTransaction(this.allParameters, this.transaction_id, s, d);
                return m;
            };

            try
            {
                this.checkSignature(this.collection, this.signature);

                //s is e-service, e is e-commerce
                var payType = this.req_reference_number.Substring(0, 1);
                var referenceNumber = this.req_reference_number.Substring(1);

                if (payType == "e")
                {
                    return log("Success", "ECommerce pay success", eComTransaction(referenceNumber));
                }
                else if (payType == "s")
                {
                    return log("Success", "EService pay success", eServiceTransaction(referenceNumber, languageType));
                }

                throw new TransactionRefFail("");
            }
            catch (TransactionSignatureFail e)
            {
                return log("Fail", "Signature fail", getTransactionMessage(Multi.Failure_in_payment));
            }
            catch (TransactionRefFail e)
            {
                return log("Fail", "Reference number invalid", getTransactionMessage(Multi.Failure_in_payment));
            }
            catch (TransactionEComDecline e)
            {
                return log("Fail", "Decline of ECommerce product", getTransactionMessage(Multi.transaction_declined, "ProductView"));
            }
            catch (TransactionDecline e)
            {
                return log("Fail", "Decline of EService product", getTransactionMessage(Multi.transaction_declined));
            }
            catch (Exception e)
            {
                Git_system.Helper.ErrLogHelper.logTransaction(this.allParameters, e, this.transaction_id, "Fail", "Unhandle fail");
                return getTransactionMessage(Multi.Failure_in_payment);
            }
        }

        private Frontend_ShowMessage getTransactionMessage(String message, String action = null)
        {
            return new Frontend_ShowMessage(Multi.ActionPaymentShowMessageHead, Multi.ActionPaymentShowMessageHead, message, action ?? "Home");
        }

        private Frontend_ShowMessage handelTransaction(Func<Frontend_ShowMessage> fnDecline, Func<Frontend_ShowMessage> fnAccept)
        {
            if (this.decision.ToUpper() == "DECLINE")
                return fnDecline();

            if (this.decision.ToUpper() == "ACCEPT")
                return fnAccept();

            throw new TransactionRefFail("");
        }

        private Frontend_ShowMessage eComTransaction(String referenceNumber)
        {
            return handelTransaction(
                fnDecline: () => { throw new TransactionEComDecline(""); },
                fnAccept: () =>
                {
                    // E-ComMessage
                    var redirectECom = new Frontend_ShowMessage(Multi.ActionPaymentShowMessageHead, Multi.ActionPaymentShowMessageHead, Multi.Payment_Completed, "ProductView");

                    Func<Boolean> sameRef = () => db.EComConfirmOrders.Where(o => o.CreditCardRefNo.Equals(transaction_id)).Count() > 0;

                    if (sameRef())
                        return redirectECom;

                    var order = setEcomCreditCard(referenceNumber, signed_date_time, req_amount, transaction_id, req_currency);

                    Git_system.Helper.EmailMethod.sendEmailForEComConfirmOrderSuccess(order);
                    Git_system.Helper.EmailMethod.sendEmailForEComPaymentToAdmin(order);
                    Git_system.Helper.EmailMethod.sendEmailForEComOrderSuccessToAdmin(order);

                    Git_system.App_Code.LogManageDatabase.add_to_database("System credit-card", "แก้ไขสถานะการชำระเงินของการสั่งซื้อสินค้าหมายเลข " + order.Id + " เป็น " + order.PaymentProcessStatu.NameTH, 1);//add to logfile

                    return redirectECom;
                });
        }

        private Frontend_ShowMessage eServiceTransaction(String referenceNumber, String languageType)
        {
            return handelTransaction(
                fnDecline: () => { throw new TransactionDecline(""); },
                fnAccept: () =>
                {
                    // E-ServiceMessage
                    var redirectEService = new Frontend_ShowMessage(Multi.ActionPaymentShowMessageHead, Multi.ActionPaymentShowMessageHead, Multi.Payment_Completed);

                    Func<Boolean> sameRef = () => db.ConfirmPayments.Where(o => o.CreditCardRefNo.Equals(transaction_id)).Count() > 0;

                    if (sameRef())
                        return redirectEService;

                    var pay = setEServiceCreditCard(referenceNumber, signed_date_time, req_amount, transaction_id, req_currency, req_bill_to_forename, req_bill_to_surname);

                    Git_system.App_Code.LogManageDatabase.add_to_database("System credit-card", "แก้ไขสถานะการชำระเงินของการสั่งซื้อหมายเลข " + pay.Id + " เป็น " + pay.ProcessStatusType.Name, 1);//add to logfile

                    Git_system.Helper.EmailMethod.SendEmailForPaymentSuccessToAdmin(pay);
                    Git_system.Helper.EmailMethod.SendEmailForPaymentSuccess(pay, languageType);

                    if (PayHelper.comfirmRegisterMembership(pay))
                    {
                        Git_system.App_Code.LogManageDatabase.add_to_database("System credit-card", "แก้ไขสถานะการจ่ายเงิน PayId = " + pay.Id + " และ แก้ไขสถาณะของผู้ลงทะเบียน MembershipId = " + pay.MembershipId, 1);
                    }

                    return redirectEService;
                });
        }

        private Pay setEServiceCreditCard(string referenceNumber, DateTime? signed_date_time, string req_amount, string auth_trans_ref_no, string req_currency, string req_bill_to_forename, string req_bill_to_surname)
        {
            const int paymentSuccess = 2;
            //referenceNumber = "12";
            Pay pay = db.Pays.Find(Convert.ToInt32(referenceNumber));
            pay.ProcessStatusTypeId = paymentSuccess;

            ConfirmPayment confirmPayment = new ConfirmPayment();
            confirmPayment.Name = string.Format("{0} {1}", req_bill_to_forename, req_bill_to_surname);
            confirmPayment.Currency = req_currency;
            confirmPayment.Total = Convert.ToDouble(req_amount);
            confirmPayment.Date = signed_date_time.GetValueOrDefault(pay.Date);
            confirmPayment.CreditCardRefNo = auth_trans_ref_no;

            pay.ConfirmPayments.Add(confirmPayment);

            db.Entry(pay).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return pay;
        }

        private EComOrder setEcomCreditCard(string referenceNumber, DateTime? signed_date_time, string req_amount, string auth_trans_ref_no, string req_currency)
        {
            const int paymentSuccess = 3;
            const int deliverShipped = 2;

            EComOrder order = db.EComOrders.Find(Convert.ToInt32(referenceNumber));
            order.PaymentProcessStatusId = paymentSuccess;
            if (PayHelper.checkEletronicOrder(order))
                order.DeliverProcessStatusId = deliverShipped;

            EComConfirmOrder co = new EComConfirmOrder();
            co.Datetime = signed_date_time.GetValueOrDefault(order.Datetime);
            co.Price = Convert.ToDouble(req_amount);
            co.EComOrderId = order.Id;
            co.FilenameConfirm = "[]";
            co.FilenamePayment = "[]";
            co.CreditCardRefNo = auth_trans_ref_no;
            co.Currency = req_currency;
            order.EComConfirmOrders.Add(co);

            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return order;
        }
    }
}