using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Notice
    {
        [Key]
        public int Id { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
    }
}
