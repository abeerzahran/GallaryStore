namespace GallaryStore.DTOs.product
{
    public class AddProductDTO
    {
        public AddProductDTO(string name, string description, decimal price, int quantity, double rate, string img,int categoryID)
        {
            
            this.name = name;
            this.description = description;
            this.price = price;
            this.quantity = quantity;
            this.rate = rate;
            this.img = img;
            this.categoryID = categoryID;

        }
       
        public string name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public double rate { get; set; }
        public string img {  get; set; }
        public int categoryID { get; set; }
    }
}
