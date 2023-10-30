using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Speech
    {
        [Key]
        public int Id { get; set; }
        public string HeadMasterSpeech { get; set; }
        public string ChairmanSpeech { get; set; }
    }
}
