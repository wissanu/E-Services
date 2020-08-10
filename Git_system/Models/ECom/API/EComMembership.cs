using Git_system.Models.Form;
using System;

namespace Git_system.Models.ECom.API
{
    public class EComMembershipAPI
    {
        public int Id { get; set; }

        public string TitleName { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string FirstnameTH { get; set; }

        public string LastnameTH { get; set; }

        public string FirstnameEN { get; set; }

        public string LastnameEN { get; set; }

        public string Company { get; set; }

        public string WorkTel { get; set; }

        public string WorkFax { get; set; }

        public string JobTitle { get; set; }

        //For Membership

        public string Address { get; set; }

        public string Road { get; set; }

        //TH
        public int Province { get; set; }

        public int Amphur { get; set; }

        public int District { get; set; }

        public string ProvinceName { get; set; }

        public string AmphurName { get; set; }

        public string DistrictName { get; set; }

        //EN
        public string City { get; set; }

        public string State { get; set; }

        public int Country { get; set; }

        public string CountryName { get; set; }

        public string Zipcode { get; set; }

        // End for membership

        public string Email { get; set; }

        public string Tel { get; set; }

        public string Mobile { get; set; }

        public string Fax { get; set; }

        //public string Password { get; set; }

        public Nullable<System.DateTime> RegisterDate { get; set; }

        public Nullable<System.DateTime> RegisterDateExp { get; set; }

        public short MembershipRegisterTypeId { get; set; }

        public string IdCRM { get; set; }

        public string GroupCRM { get; set; }

        public Nullable<System.DateTime> BirthDay { get; set; }

        public string Religion { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public bool Active { get; set; }

        public int QuestionListId { get; set; }

        public string MobileCRM { get; set; }

        public Nullable<System.DateTime> ExpCRM { get; set; }

        public string IdentificationNo { get; set; }

        public string Occupation { get; set; }

        public string Education { get; set; }

        public bool ExperienceInGem { get; set; }
    }

    public static partial class ExtensionMethod
    {
        public static EComMembershipAPI toAPI(this Membership content)
        {
            EComMembershipAPI rcontent = new EComMembershipAPI();

            rcontent.Id = content.Id;
            rcontent.MembershipRegisterTypeId = content.MembershipRegisterTypeId;
            rcontent.RegisterDate = content.RegisterDate;
            rcontent.RegisterDateExp = content.RegisterDateExp;
            //rcontent.Pays = content.Pays;
            //rcontent.MembershipRegisterType = content.MembershipRegisterType;

            rcontent.Firstname = content.Firstname;
            rcontent.Lastname = content.Lastname;

            rcontent.FirstnameEN = content.FirstnameEN;
            rcontent.FirstnameTH = content.FirstnameTH;
            rcontent.LastnameEN = content.LastnameEN;
            rcontent.LastnameTH = content.LastnameTH;
            //rcontent.Password = content.Password;
            rcontent.Email = content.Email;
            rcontent.Company = content.Company;

            rcontent.WorkTel = content.WorkTel;
            rcontent.WorkFax = content.WorkFax;

            rcontent.JobTitle = content.JobTitle;
            rcontent.IdCRM = content.IdCRM;
            rcontent.GroupCRM = content.GroupCRM;
            rcontent.MobileCRM = content.MobileCRM;
            rcontent.ExpCRM = content.ExpCRM;

            rcontent.Fax = content.Fax;
            rcontent.Tel = content.Tel;
            rcontent.Mobile = content.Mobile;
            rcontent.BirthDay = Convert.ToDateTime(content.BirthDay);
            rcontent.Religion = content.Religion;
            rcontent.Question = content.Question;
            rcontent.Answer = content.Answer;

            rcontent.QuestionListId = content.QuestionListId;

            rcontent.Active = content.Active;

            rcontent.IdentificationNo = content.IdentificationNo;
            rcontent.Occupation = content.Occupation;
            rcontent.Education = content.Education;
            rcontent.ExperienceInGem = content.ExperienceInGem;

            AddressMe address = new AddressMe();
            address = Newtonsoft.Json.JsonConvert.DeserializeObject<AddressMe>(content.Address);

            rcontent.Address = address.Address;
            rcontent.Amphur = address.Amphur.Id;
            rcontent.City = address.City;
            rcontent.Country = address.Country.Id;
            rcontent.District = address.District.Id;
            rcontent.Province = address.Province.Id;

            if (address.Amphur.Id != 0)
            {
                rcontent.AmphurName = address.Amphur.Name;
            }
            if (address.Country.Id != 0)
            {
                rcontent.CountryName = address.Country.Name;
            }
            if (address.District.Id != 0)
            {
                rcontent.DistrictName = address.District.Name;
            }
            if (address.Province.Id != 0)
            {
                rcontent.ProvinceName = address.Province.Name;
            }

            rcontent.Road = address.Road;
            rcontent.State = address.State;
            rcontent.Zipcode = address.Zipcode;

            return rcontent;
        }
    }
}