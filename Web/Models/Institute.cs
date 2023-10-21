using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Institute
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
