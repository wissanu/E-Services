namespace Git_system.Models.Form
{
    public class ProductSKUOut
    {
        public ProductSKUOut()
        {
        }

        public ProductSKUOut(int id, string nameTh, string nameEn, double price)
        {
            this.Id = id;
            this.NameTH = nameTh;
            this.NameEN = nameEn;
            this.Price = price;
        }

        public int Id { get; set; }

        public string NameTH { get; set; }

        public string NameEN { get; set; }

        public double Price { get; set; }
    }
}