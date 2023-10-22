using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class AboutOurs
    {
        [Key]
        public int Id { get; set; }
        public string AboutOurself { get; set; }
        public string History { get; set; }
        public string Aims { get; set; }
        public string InstitutionalStructure { get; set; }
    }
}
