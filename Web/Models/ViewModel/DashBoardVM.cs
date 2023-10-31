namespace Web.Models.ViewModel
{
    public class DashBoardVM
    {
        public string AboutOurself { get; set; }
        public string History { get; set; }
        public string Aims { get; set; }
        public string InstitutionalStructure { get; set; }
        public string InstituteName { get; set; }
        public string InstituteAddress { get; set; }
        public string InstitutePostalAddress { get; set; }
        public string Banner1Src { get; set; }
        public string Banner2Src { get; set; }
        public string Banner3Src { get; set; }
        public string Banner4Src { get; set; }
        public string Banner5Src { get; set; }
        public string Banner6Src { get; set; }
        public string HeadMasterName { get; set; }
        public string HeadMasterSpeech { get; set; }
        public string HeadMasterImage { get; set; }
        public string ChairmanName { get; set; }
        public string ChairmanSpeech { get; set; }
        public string ChairmanImage { get; set; }
        public List<Notice> Notices { get; set; }
        public Notice Notice { get; set; }
        public List<Member> GoverningBody { get; set; }
        public List<Member> Teacher { get; set; }
        public List<Member> Officials { get; set; }
        public List<Member> Employees { get; set; }
    }
}
