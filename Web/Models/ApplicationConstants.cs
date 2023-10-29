using System.ComponentModel;

namespace Web.Models
{
    public class ApplicationConstants
    {
        public enum DesignationType
        {
            [Description("প্রধান শিক্ষক")]
            HeadMaster = 1,
            [Description("সহকারী শিক্ষক")]
            Assistant_Teacher = 2,
            [Description("শিক্ষক")]
            Teacher = 3,
            [Description("কর্মকর্তা")]
            Employee = 4,
            [Description("কর্মচারী")]
            Officials = 5,
            [Description("সভাপতি")]
            Chairman = 6,
            [Description("সহ-সভাপতি")]
            Assistant_Chairman = 7,
            [Description("সদস্য")]
            Committe_Member = 8
        }
    }
}
