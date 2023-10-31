using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Web.Models.ApplicationConstants;

namespace Web.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int DesignationId { get; set; }
        [NotMapped]
        public string DesignationName
        {
            get
            {
                if(DesignationId == 0)
                    return string.Empty;
                return EnumUtility.GetDescriptionFromEnumValue((DesignationType)DesignationId);
            }
        }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string CreatedDate { get; set; }
        [NotMapped]
        public IFormFile ImgFiles { get; set; }
        [NotMapped]
        public string Base64Image { get; set; }
    }
}
