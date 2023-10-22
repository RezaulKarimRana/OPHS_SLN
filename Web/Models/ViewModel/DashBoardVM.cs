namespace Web.Models.ViewModel
{
    public class DashBoardVM
    {
        public string AboutOurself { get; set; }
        public string History { get; set; }
        public string Aims { get; set; }
        public string InstitutionalStructure { get; set; }
        public string InstituteName { get; set; }
        public string Banner1Src { get; set; }
        public string Banner2Src { get; set; }
        public string Banner3Src { get; set; }
        public string Banner4Src { get; set; }
        public string Banner5Src { get; set; }
        public string Banner6Src { get; set; }
        public string HeadMasterName { get; set; }
        public string HeadMasterDetails { get; set; }
        public string HeadMasterImage { get; set; }
        public string ChairmanName { get; set; }
        public string ChairmanDetails { get; set; }
        public string ChairmanImage { get; set; }
        public List<NoticeVM> Notices { get; set; }
        public NoticeVM Notice { get; set; }
    }
}
