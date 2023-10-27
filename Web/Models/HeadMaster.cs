using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class HeadMaster
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImgFiles { get; set; }
    }
}
