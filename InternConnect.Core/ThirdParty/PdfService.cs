using System.Collections.Generic;
using InternConnect.Data.Interfaces;

namespace InternConnect.Service.ThirdParty
{
    public interface IPdfService
    {

    }

    public class PdfService : IPdfService
    {
        public PdfService(IAdminRepository adminRepository, ISubmissionRepository submissionRepository,
            IAcademicYearRepository academicYear, IPdfStateRepository pdfStateRepository)
        {

            
            
        }
    }
}