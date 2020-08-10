using Git_system.MultiResources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git_system.Models.ECom.API
{
    public class DataAndStatus
    {
        public DataAndStatus authen(int membershipId, Func<DataAndStatus> fun)
        {
            if (membershipId == 0)
                return new DataAndStatus(201, null);
            else
                return fun();
        }

        /// <summary>
        /// ตัวแปรใช้เก็บข้อความ
        /// </summary>
        private Dictionary<int, string> messageDictionary = new Dictionary<int, string>();

        /// <summary>
        /// ประกาศค่าเริ่มต้น
        /// </summary>
        public DataAndStatus()
        {
            this.message = null;
            this.status = 0;
            this.data = null;
        }

        /// <summary>
        /// ประกาศค่า และมีการประกาศข้อความอัตโนมัน
        /// </summary>
        /// <param name="_status">สถานะ</param>
        /// <param name="_data">ข้อมูล</param>
        public DataAndStatus(int _status, dynamic _data)
        {
            this.messageDictionary.Add(1, "Success");// สำเร็จ
            this.messageDictionary.Add(101, "Order incorrect");// ไม่มีรหัสสินค้าตาม Order ที่ส่งเข้ามา
            this.messageDictionary.Add(102, "Add order err");// เกิดข้อผิดพลาดขณะทำการเพิ่มข้อมูล Order
            this.messageDictionary.Add(103, "Add orderitem err");// เกิดข้อผิดพลาดขณะทำการเพิ่มข้อมูล OrderItem
            this.messageDictionary.Add(104, "Empty orderitem");// OrderItem ว่าง
            this.messageDictionary.Add(111, "No number of order");// ไม่มีรหัส Order ที่รับเข้ามา
            this.messageDictionary.Add(112, "Date invalid");// วันเวลาผิด
            this.messageDictionary.Add(113, "Membership id invalid");
            this.messageDictionary.Add(114, Multi.RequiredFile);
            this.messageDictionary.Add(201, "Not login");// ยังไม่ได้ทำการ login
            this.messageDictionary.Add(301, Multi.CheckMembershipActive);// ยังไม่ได้ทำการทำการยืนยันอีเมล
            this.messageDictionary.Add(302, Multi.CheckEmailOrPassword);// อีเมลหรือรหัสผ่านผิด
            this.messageDictionary.Add(303, Multi.CheckEmailOrPassword);// อีเมลหรือรหัสผ่านผิด

            this.status = _status;
            if (messageDictionary.Where(m => m.Key == _status).Count() == 1)
            {
                this.message = messageDictionary.Where(m => m.Key == _status).FirstOrDefault().Value;
            }
            else
            {
                this.message = null;
            }
            this.data = _data;
        }

        /// <summary>
        /// ประกาศค่า และมีการกำหนดสถานะ และข้อความเอง
        /// </summary>
        /// <param name="_status">สถานะ</param>
        /// <param name="_message">ข้อความ</param>
        /// <param name="_data">ข้อมูล</param>
        public DataAndStatus(int _status, string _message, dynamic _data)
        {
            this.status = _status;
            if (_status.Equals(1))
            {
                this.message = "Success";
            }
            this.data = _data;
        }

        /// <summary>
        /// เก็บข้อความ
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// เก็บสถานะ
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// เก็บข้อมูล
        /// </summary>
        public dynamic data { get; set; }
    }
}