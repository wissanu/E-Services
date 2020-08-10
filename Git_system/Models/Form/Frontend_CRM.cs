namespace Git_system.Models.Form
{
    public class Frontend_CRM
    {
        public Frontend_CRM()
        {
            this.IsSuccess = false;
        }

        public int Id { get; set; }

        public string Code { get; set; }

        public string Prefix { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Alphabet { get; set; }

        public string NameEng { get; set; }

        public string SurnameEng { get; set; }

        public int GroupId { get; set; }

        public int SubgroupId { get; set; }

        public string ContactUs { get; set; }

        public string AddNo { get; set; }

        public string AddSoi { get; set; }

        public string AddZone { get; set; }

        public string AddArea { get; set; }

        public string Zipcode { get; set; }

        public string Province { get; set; }

        public string Tel { get; set; }

        public string Fax { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public bool IsActive { get; set; }

        public bool IsExpired { get; set; }

        public string ExpireDate { get; set; }

        public string Image { get; set; }

        public bool IsSuccess { get; set; }
    }
}