namespace Web.Models.ViewModel
{
    public class NoticeVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string CreatedDate { get; set; }
        public IFormFile ImgFiles { get; set; }
    }
}
