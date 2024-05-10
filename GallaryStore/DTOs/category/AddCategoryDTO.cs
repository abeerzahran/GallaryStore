namespace GallaryStore.DTOs.category
{
    public class AddCategoryDTO
    {
        public AddCategoryDTO(string name)
        {
            this.name = name;
        }
       
        public string name { get; set; }
    }
}
