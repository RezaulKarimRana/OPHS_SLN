using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
    }
}
