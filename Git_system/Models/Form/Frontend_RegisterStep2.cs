using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git_system.Models.Form
{
    public class Frontend_RegisterStep2
    {
        public enum CountryType
        {
            Thailand = 213
        };

        public Frontend_RegisterStep2()
        {
            this.Country = CountryType.Thailand.GetHashCode();
            this.BirthDay = DateTime.Now.AddYears(-23);
            this.RegisterDate = DateTime.Now;
            this.ExperienceInGem = false;
        }

        //For Membership

        public int Id { get; set; }

        public short MembershipRegisterTypeId { get; set; }

        public Nullable<System.DateTime> RegisterDate { get; set; }

        public Nullable<System.DateTime> RegisterDateExp { get; set; }

        public virtual ICollection<Pay> Pays { get; set; }

        public virtual MembershipRegisterType MembershipRegisterType { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredFirstname")]
        public string Firstname { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredLastname")]
        public string Lastname { get; set; }

        public string FirstnameTH { get; set; }

        public string LastnameTH { get; set; }

        public string FirstnameEN { get; set; }

        public string LastnameEN { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Company { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_TelNoCorrect")]
        public string WorkTel { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_FaxNoCorrect")]
        public string WorkFax { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_FaxNoCorrect")]
        public string ExtWorkFax { get; set; }

        public string JobTitle { get; set; }

        public System.DateTime BirthDay { get; set; }

        public string Religion { get; set; }

        public string Question { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredQuestion")]
        public int QuestionListId { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredAnswer")]
        public string Answer { get; set; }

        public Boolean Active { get; set; }

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

        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_ZipcodeCorrect")]
        public string Zipcode { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_TelNoCorrect")]
        public string TelNo { get; set; }

        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "RequiredField")]
        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_MobileNoCorrect")]
        public string MobileNo { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_FaxNoCorrect")]
        public string FaxNo { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Validation_FaxNoCorrect")]
        public string ExtFaxNo { get; set; }

        public virtual QuestionList QuestionList { get; set; }

        //Membership CRM

        public string IdCRM { get; set; }

        public string GroupCRM { get; set; }

        public string MobileCRM { get; set; }

        public Nullable<System.DateTime> ExpCRM { get; set; }

        //Membership CRM
        [Required(ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Identification_no_field_is_required")]
        [RegularExpression("^[0-9]+$", ErrorMessageResourceType = typeof(MultiResources.Multi), ErrorMessageResourceName = "Identification_no_must_be_numeric")]
        public string IdentificationNo { get; set; }

        public string Occupation { get; set; }

        public string Education { get; set; }

        public bool ExperienceInGem { get; set; }
    }

    public class AddressMe
    {
        public AddressMe()
        {
            this.Province = new Province();
            this.Amphur = new Amphur();
            this.District = new District();
            this.Country = new Country();
        }

        public int AddressType { get; set; }

        public string Address { get; set; }

        public string Road { get; set; }

        //TH
        public Province Province { get; set; }

        public Amphur Amphur { get; set; }

        public District District { get; set; }

        //EN
        public string City { get; set; }

        public string State { get; set; }

        public Country Country { get; set; }

        public string Zipcode { get; set; }
    }

    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Province
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Amphur
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class District
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public static partial class ExtensionMethod
    {
        private static string getFaxFormLongFax(string faxNo)
        {
            var spFax = (faxNo ?? "").Split(new[] { "Ext." }, StringSplitOptions.RemoveEmptyEntries);
            if (spFax.Length > 0)
            {
                return spFax[0].Trim();
            }
            return null;
        }

        private static string getExtFaxFormLongFax(string faxNo)
        {
            var spFax = (faxNo ?? "").Split(new[] { "Ext." }, StringSplitOptions.RemoveEmptyEntries);
            if (spFax.Length == 2)
            {
                return spFax[1].Trim();
            }
            return null;
        }

        private static string getLongFax(string faxNo, string extNo)
        {
            if (!string.IsNullOrWhiteSpace(faxNo) && string.IsNullOrWhiteSpace(extNo))
                return faxNo;

            if (!string.IsNullOrWhiteSpace(faxNo) && !string.IsNullOrWhiteSpace(extNo))
                return string.Format("{0} Ext. {1}", faxNo, extNo);

            return null;
        }

        public static Membership toMembership(this Frontend_RegisterStep2 RegisterStep2)
        {
            Membership content = new Membership();

            content.Id = RegisterStep2.Id;
            content.MembershipRegisterTypeId = RegisterStep2.MembershipRegisterTypeId;
            content.RegisterDate = RegisterStep2.RegisterDate;
            content.RegisterDateExp = RegisterStep2.RegisterDateExp;
            content.Pays = RegisterStep2.Pays;
            content.MembershipRegisterType = RegisterStep2.MembershipRegisterType;

            content.Firstname = RegisterStep2.Firstname;
            content.Lastname = RegisterStep2.Lastname;

            content.FirstnameEN = RegisterStep2.FirstnameEN;
            content.FirstnameTH = RegisterStep2.FirstnameTH;
            content.LastnameEN = RegisterStep2.LastnameEN;
            content.LastnameTH = RegisterStep2.LastnameTH;
            content.Password = RegisterStep2.Password;
            content.Email = RegisterStep2.Email;
            content.Company = RegisterStep2.Company;

            content.WorkTel = RegisterStep2.WorkTel;
            content.WorkFax = getLongFax(RegisterStep2.WorkFax, RegisterStep2.ExtWorkFax);

            content.JobTitle = RegisterStep2.JobTitle;
            content.IdCRM = RegisterStep2.IdCRM;
            content.GroupCRM = RegisterStep2.GroupCRM;
            content.MobileCRM = RegisterStep2.MobileCRM;
            content.ExpCRM = RegisterStep2.ExpCRM;

            content.Fax = getLongFax(RegisterStep2.FaxNo, RegisterStep2.ExtFaxNo);
            content.Tel = RegisterStep2.TelNo;
            content.Mobile = RegisterStep2.MobileNo;
            content.BirthDay = RegisterStep2.BirthDay;
            content.Religion = RegisterStep2.Religion;
            content.Question = RegisterStep2.Question;
            content.Answer = RegisterStep2.Answer;

            content.QuestionListId = RegisterStep2.QuestionListId;

            content.Active = RegisterStep2.Active;

            content.IdentificationNo = RegisterStep2.IdentificationNo;
            content.Occupation = RegisterStep2.Occupation;
            content.Education = RegisterStep2.Education;
            content.ExperienceInGem = RegisterStep2.ExperienceInGem;

            Database_MainEntities1 db = new Database_MainEntities1();

            AddressMe address = new AddressMe();
            address.Address = RegisterStep2.Address;
            if (RegisterStep2.Amphur != 0)
            {
                address.Amphur.Id = RegisterStep2.Amphur;
                address.Amphur.Name = db.Amphurs.Find(RegisterStep2.Amphur).AMPHUR_NAME;
            }
            address.City = RegisterStep2.City;
            if (RegisterStep2.Country != 0)
            {
                address.Country.Id = RegisterStep2.Country;
                address.Country.Name = db.Countries.Find(RegisterStep2.Country).Name;
            }
            if (RegisterStep2.District != 0)
            {
                address.District.Id = RegisterStep2.District;
                address.District.Name = db.Districts.Find(RegisterStep2.District).DISTRICT_NAME;
            }
            if (RegisterStep2.Province != 0)
            {
                address.Province.Id = RegisterStep2.Province;
                address.Province.Name = db.Provinces.Find(RegisterStep2.Province).PROVINCE_NAME;
            }

            address.Road = RegisterStep2.Road;
            address.State = RegisterStep2.State;
            address.Zipcode = RegisterStep2.Zipcode;

            content.Address = Newtonsoft.Json.JsonConvert.SerializeObject(address, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });

            content.QuestionList = RegisterStep2.QuestionList;
            return content;
        }

        public static Frontend_RegisterStep2 toFrontend_RegisterStep2(this Membership membership)
        {
            Frontend_RegisterStep2 content = new Frontend_RegisterStep2();

            content.Id = membership.Id;
            content.MembershipRegisterTypeId = membership.MembershipRegisterTypeId;
            content.RegisterDate = membership.RegisterDate;
            content.RegisterDateExp = membership.RegisterDateExp;
            content.Pays = membership.Pays;
            content.MembershipRegisterType = membership.MembershipRegisterType;

            content.Firstname = membership.Firstname;
            content.Lastname = membership.Lastname;

            content.FirstnameEN = membership.FirstnameEN;
            content.FirstnameTH = membership.FirstnameTH;
            content.LastnameEN = membership.LastnameEN;
            content.LastnameTH = membership.LastnameTH;
            content.Password = membership.Password;
            content.Email = membership.Email;
            content.Company = membership.Company;

            content.WorkTel = membership.WorkTel;
            content.WorkFax = getFaxFormLongFax(membership.WorkFax);
            content.ExtWorkFax = getExtFaxFormLongFax(membership.WorkFax);

            content.JobTitle = membership.JobTitle;
            content.IdCRM = membership.IdCRM;
            content.GroupCRM = membership.GroupCRM;
            content.MobileCRM = membership.MobileCRM;
            content.ExpCRM = membership.ExpCRM;

            content.FaxNo = getFaxFormLongFax(membership.Fax);
            content.ExtFaxNo = getExtFaxFormLongFax(membership.Fax);
            content.TelNo = membership.Tel;
            content.MobileNo = membership.Mobile;
            content.BirthDay = Convert.ToDateTime(membership.BirthDay);
            content.Religion = membership.Religion;
            content.Question = membership.Question;
            content.Answer = membership.Answer;

            content.QuestionListId = membership.QuestionListId;

            content.Active = membership.Active;

            content.IdentificationNo = membership.IdentificationNo;
            content.Occupation = membership.Occupation;
            content.Education = membership.Education;
            content.ExperienceInGem = membership.ExperienceInGem;

            AddressMe address = new AddressMe();
            address = Newtonsoft.Json.JsonConvert.DeserializeObject<AddressMe>(membership.Address);

            content.Address = address.Address;
            content.Amphur = address.Amphur.Id;
            content.City = address.City;
            content.Country = address.Country.Id;
            content.District = address.District.Id;
            content.Province = address.Province.Id;

            if (address.Amphur.Id != 0)
            {
                content.AmphurName = address.Amphur.Name;
            }
            if (address.Country.Id != 0)
            {
                content.CountryName = address.Country.Name;
            }
            if (address.District.Id != 0)
            {
                content.DistrictName = address.District.Name;
            }
            if (address.Province.Id != 0)
            {
                content.ProvinceName = address.Province.Name;
            }

            content.Road = address.Road;
            content.State = address.State;
            content.Zipcode = address.Zipcode;

            content.QuestionList = membership.QuestionList;
            return content;
        }
    }
}