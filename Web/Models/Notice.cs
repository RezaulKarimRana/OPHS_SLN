using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Notice
    {
        [Key]
        public int Id { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string CreatedDate { get; set; }
        [NotMapped]
        public IFormFile ImgFiles { get; set; }
    }
}
