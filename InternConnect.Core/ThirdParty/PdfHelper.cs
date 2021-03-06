namespace InternConnect.Service.ThirdParty
{
    public class PdfHelper
    {
        //Company
        public string CompanyName { get; set; }
        public string CompanyAddressOne { get; set; }
        public string CompanyAddressTwo { get; set; }
        public string CompanyAddressThree { get; set; }

        //Student
        public string StudentTitle { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentMiddleInitial { get; set; }
        public string StudentSection { get; set; }
        public string StudentTrack { get; set; }
        public string StudentProgram { get; set; }

        //Contact Person
        public string ContactPersonTitle { get; set; }
        public string ContactPersonFirstName { get; set; }
        public string ContactPersonLastName { get; set; }
        public string ContactPersonRole { get; set; }

        //Admins
        public string IgaarpName { get; set; }
        public string DeanName { get; set; }
        public string DeanSignatureFileName { get; set; }

        //PdfState
        public string CollegeName { get; set; }
        public int IsoCode { get; set; }
        public string IsoCodeProgramNumber { get; set; }
        public int CurriculumHours { get; set; }
        public string UniversityLogoFileName { get; set; }
        public string CollegeLogoFileName { get; set; }
        public string CoordinatorSignatureFileName { get; set; }
    }
}