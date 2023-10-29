using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Notice
    {
        [Key]
        public int Id { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string CreatedDate { get; set; }
    }
}
