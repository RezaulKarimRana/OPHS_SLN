namespace Web.Models.ViewModel
{
    public class HeadMasterVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public IFormFile ImgFiles { get; set; }
    }
}
