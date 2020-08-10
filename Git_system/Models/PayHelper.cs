using System;
using System.Linq;

namespace Git_system.Models
{
    public static partial class PayHelper
    {
        private static Database_MainEntities1 db = new Database_MainEntities1();

        /// <summary>
        /// Give true if all eletronic file only in order
        /// </summary>
        /// <param name="order">order</param>
        /// <returns>eletronic file order</returns>
        public static bool checkEletronicOrder(EComOrder order)
        {
            if (order == null)
                return false;
            else
                return Array.TrueForAll(order.EComOrderItems.ToArray(), o => (o.EComProduct.ElectronicFileStatus == true && o.EComProduct.DeliverStatus == false));
        }

        /// <summary>
        /// Give status form payment
        /// </summary>
        /// <param name="pay">payment that add exp data to membership</param>
        /// <returns>status of save</returns>
        public static bool comfirmRegisterMembership(Pay pay)
        {
            if (pay.ProcessStatusTypeId == 2 && pay.Type == 1)
            {
                short MembershipRegisterTypeId = getMembershipRegisterInProduct(pay);

                DateTime membershipRegisterDateExp = DateTime.Now;
                if (!(db.Memberships.Where(m => m.Id == pay.MembershipId).Select(m => m.RegisterDateExp.HasValue).FirstOrDefault()))
                {
                    membershipRegisterDateExp = membershipRegisterDateExp.AddYears(1);//ไม่เคยสมัคร
                }
                else
                {
                    DateTime dateExp = db.Memberships.Where(m => m.Id == pay.MembershipId).Select(m => m.RegisterDateExp.Value).FirstOrDefault();
                    if (dateExp <= membershipRegisterDateExp)//ต่ออายุ
                    {
                        membershipRegisterDateExp = membershipRegisterDateExp.AddYears(1);//หมดอายุ
                    }
                    else
                    {
                        membershipRegisterDateExp = dateExp.AddYears(1);//ไม่หมดอายุ (ต่อเพิ่ม)
                    }
                }

                Membership membershipOriginalValues = db.Memberships.Find(pay.MembershipId);
                Membership membership = new Membership();
                membership = membershipOriginalValues;
                membership.MembershipRegisterTypeId = MembershipRegisterTypeId;
                membership.RegisterDateExp = membershipRegisterDateExp;

                db.Entry(membershipOriginalValues).CurrentValues.SetValues(membership);
                db.SaveChanges();

                //Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name.ToString(), "แก้ไขสถานะการจ่ายเงิน PayId = " + pay.Id + " และ แก้ไขสถาณะของผู้ลงทะเบียน MembershipId = " + membership.Id, 1);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Give membership type from payment
        /// </summary>
        /// <param name="pay">payment will check membership type</param>
        /// <returns>membership type form payment</returns>
        private static short getMembershipRegisterInProduct(Pay pay)
        {
            short MembershipRegisterTypeId = 0;
            switch (pay.PayItems.FirstOrDefault().Course.ProductId)
            {
                case 1:
                    MembershipRegisterTypeId = 3;
                    break;

                case 3:
                    MembershipRegisterTypeId = 4;
                    break;

                case 2:
                    MembershipRegisterTypeId = 5;
                    break;

                case 4:
                    MembershipRegisterTypeId = 6;
                    break;

                default:
                    MembershipRegisterTypeId = 1;
                    break;
            }
            return MembershipRegisterTypeId;
        }
    }
}