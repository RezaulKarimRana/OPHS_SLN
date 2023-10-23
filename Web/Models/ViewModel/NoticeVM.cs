namespace Web.Models.ViewModel
{
    public class NoticeVM
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string CreatedDate { get; set; }
        public IFormFile ImgFiles { get; set; }
    }
}
