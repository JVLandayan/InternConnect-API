using System;
using System.IO;
using System.Linq;
using InternConnect.Data.Interfaces;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Service.ThirdParty
{
    public interface IPdfService
    {
        public IActionResult GeneratePdf(ControllerBase controller, int submissionId);
        public ActionResult PreviewPdf(ControllerBase controller, int adminId);
        public MemoryStream AddPdf(int submissionId);
    }

    public class PdfService : IPdfService
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ICompanyRepository _companyRepository;

        private readonly IPdfStateRepository _pdfStateRepository;
        private readonly IProgramRepository _programRepository;
        private readonly ISectionRepository _sectionRepository;

        private readonly ISubmissionRepository _submissionRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IWebHostEnvironment _webHost;


        public PdfService(IAdminRepository adminRepository, ISubmissionRepository submissionRepository,
            IAcademicYearRepository academicYear, IPdfStateRepository pdfStateRepository,
            IWebHostEnvironment webHostEnvironment, ITrackRepository trackRepository,
            IProgramRepository programRepository, ICompanyRepository companyRepository,
            ISectionRepository sectionRepository)
        {
            _adminRepository = adminRepository;
            _submissionRepository = submissionRepository;
            _academicYearRepository = academicYear;
            _pdfStateRepository = pdfStateRepository;
            _webHost = webHostEnvironment;
            _trackRepository = trackRepository;
            _programRepository = programRepository;
            _companyRepository = companyRepository;
            _sectionRepository = sectionRepository;
        }


        public IActionResult GeneratePdf(ControllerBase controller, int submissionId)
        {
            var submissionData = _submissionRepository.GetAllRelatedData().First(s => s.Id == submissionId);
            var trackData = _trackRepository.Get(submissionData.TrackId);
            var companyData = _companyRepository.Get(submissionData.CompanyId);
            var academicYearData = _academicYearRepository.GetAll().First();
            var pdfStateData = _pdfStateRepository.GetAll().First();
            var deanData = _adminRepository.GetAll().Where(a => a.AuthId == 1).First();
            var coordinatorData = _adminRepository.GetAll().Where(a => a.SectionId != null).ToList()
                .Find(a => a.SectionId == submissionData.Student.SectionId);


            var contentType = "application/pdf";
            var fileName =
                $"{submissionData.Student.Section.Name}-{submissionData.LastName}_{submissionData.FirstName}-{submissionData.StudentNumber}-Endorsement-Letter.pdf";

            try
            {
                using (var ms = new MemoryStream())
                {
                    var writer = new PdfWriter(ms);
                    var pdf = new PdfDocument(writer);

                    var document = new Document(pdf, PageSize.LETTER);
                    document.SetMargins(36, 72, 72, 72);
                    document.SetProperty(Property.LEADING, new Leading(Leading.MULTIPLIED, 0.9f));
                    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackGroundColorEvent());

                    #region Fonts

                    var headProgram =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/fonts/candara-bold.ttf");
                    var subHeadProgram =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/fonts/TCM_____.TTF");
                    var normalBold =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/fonts/arial-bold.ttf");

                    var headerFont = PdfFontFactory.CreateFont(headProgram, PdfEncodings.WINANSI, true);
                    var subHeaderFont = PdfFontFactory.CreateFont(subHeadProgram, PdfEncodings.WINANSI, true);
                    var normalBoldFont = PdfFontFactory.CreateFont(normalBold, PdfEncodings.WINANSI, true);

                    #endregion

                    #region Header

                    var header = new Text("University Of Santo Tomas").SetFontSize(14).SetFont(headerFont);
                    var subHeaderOne = new Text($"{academicYearData.CollegeName}").SetFontSize(11)
                        .SetFont(headerFont);
                    var subHeaderTwo =
                        new Text(
                                $"Department Of {submissionData.Student.Program.Name}")
                            .SetFont(subHeaderFont);
                    var pHead = new Paragraph().Add(header).SetTextAlignment(TextAlignment.CENTER);
                    var pSubHeadOne = new Paragraph().Add(subHeaderOne).SetTextAlignment(TextAlignment.CENTER);
                    var pSubHeadTwo = new Paragraph().Add(subHeaderTwo).SetTextAlignment(TextAlignment.CENTER);

                    document.Add(pHead.SetMarginBottom(0f).SetFixedLeading(9.0f));
                    document.Add(pSubHeadOne.SetMarginBottom(0f).SetFixedLeading(9.0f));
                    document.Add(pSubHeadTwo.SetMarginBottom(40.0f).SetFixedLeading(9.0f));


                    #region Logos

                    var iicsLogoPath = _webHost.ContentRootPath +
                                       $"/images/logo/{pdfStateData.CollegeLogoFileName}";
                    var ustLogoPath = _webHost.ContentRootPath +
                                      $"/images/logo/{pdfStateData.UstLogoFileName}";


                    var iicsLogoData = ImageDataFactory.Create(iicsLogoPath);
                    var ustLogoData = ImageDataFactory.Create(ustLogoPath);

                    var imgIics = new Image(iicsLogoData);
                    var imgUst = new Image(ustLogoData);

                    imgUst.SetFixedPosition(105, 705).ScaleAbsolute(60f, 60f);
                    imgIics.SetFixedPosition(450, 705).ScaleAbsolute(60f, 60f);
                    document.Add(imgUst);
                    document.Add(imgIics);

                    #endregion

                    #endregion

                    #region Body

                    #region Texts

                    var isoCode =
                        new Text(
                            $"UST:A022-0{submissionData.Student.Program.IsoCodeProgramNumber}-LE{submissionData.IsoCode}");
                    var academicYear =
                        new Text(
                            $"AY {academicYearData.StartDate.ToString("yyyy")} - {academicYearData.EndDate.ToString("yyyy")}");
                    var isoCodep = new Paragraph().Add(isoCode).SetTextAlignment(TextAlignment.LEFT).SetFontSize(11);
                    var academicYearp = new Paragraph().Add(academicYear).SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(11);
                    document.Add(isoCodep.SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(academicYearp.SetMarginBottom(11.0f).SetFixedLeading(7.0f));


                    document.Add(new Paragraph()
                        .Add(new Text($"{DateTime.Now.ToString("MMMM dd, yyyy")}").SetFontSize(11))
                        .SetMarginBottom(11.0f)
                        .SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                                $"{submissionData.ContactPersonTitle} {submissionData.ContactPersonFirstName} {submissionData.ContactPersonLastName}")
                            .SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(new Paragraph()
                        .Add(new Text($"{submissionData.ContactPersonPosition}").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(new Paragraph().Add(new Text($"{companyData.Name}").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));

                    if (companyData.AddressThree != null)
                    {
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressOne}").SetFontSize(11))
                            .SetMarginBottom(0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressTwo}").SetFontSize(11))
                            .SetMarginBottom(0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressThree}").SetFontSize(11))
                            .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    }
                    else if (companyData.AddressTwo != null)
                    {
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressOne}").SetFontSize(11))
                            .SetMarginBottom(0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressTwo}").SetFontSize(11))
                            .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    }
                    else
                    {
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressOne}").SetFontSize(11))
                            .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    }

                    //document.Add(new Paragraph().Add(new Text("AddressOne").SetFontSize(11))
                    //    .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    //document.Add(new Paragraph().Add(new Text("AddressTwo").SetFontSize(11))
                    //    .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    //document.Add(new Paragraph().Add(new Text("AddressThree").SetFontSize(11))
                    //    .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                                $"Dear {submissionData.ContactPersonTitle}. {submissionData.ContactPersonLastName}")
                            .SetFontSize(11))
                        .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT));

                    document.Add(new Paragraph().Add(new Text(
                            $"This is to recommend {submissionData.StudentTitle} {submissionData.FirstName} {submissionData.MiddleInitial} {submissionData.LastName}, " +
                            $"a bona fide student of section {submissionData.Student.Section.Name} of the {academicYearData.CollegeName} of the University  of Santo Tomas to take an Internship or  Practicum  Course  " +
                            $"this  {academicYearData.StartDate.ToString("MMMM")}  –  {academicYearData.EndDate.ToString("MMMM")}  {academicYearData.EndDate.ToString("yyyy")}  in  your  reputable  company.  As  part  of  our Outcomes-Based Education curriculum requirements " +
                            $"in the  B. S. {submissionData.Student.Program.Name} program, said  student  must  undertake  a  minimum  of  {submissionData.Student.Program.NumberOfHours}  hours  of  relevant  company  " +
                            "or  industry immersion. ").SetFontSize(11)).SetTextAlignment(TextAlignment.JUSTIFIED)
                        .SetFixedLeading(11.0f).SetMarginBottom(11.0f));


                    document.Add(new Paragraph()
                        .Add(new Text("In  particular,  we  hope  you  can  place  said  student  " +
                                      "in  an  environment  that  can  further  show his ability  " +
                                      "to solve  computing problems; and/or  to  work  and  communicate  " +
                                      "effectively  in  a project involving a multidisciplinary team; and " +
                                      "to be updated with the latest trends and emerging  technologies in " +
                                      $"his field of study. Since said student is pursuing a track in {trackData.Name}, please assign said student in any combination of activities " +
                                      "in line with the same track and other computer related activities except " +
                                      "encoding or clerical work. If possible, please allow our student to consult with his " +
                                      "assigned supervisor to conceptualize an industry-based research problem. This will enable our " +
                                      "students to conduct researches which are focused on their area and relevant to the actual needs of " +
                                      "the IT industry. ").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.JUSTIFIED)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "If you have further inquiries or concern, please contact us by " +
                            $"email at {academicYearData.IgaarpEmail} or at our land line " +
                            "number 786-1878 or 406-1611 extension 8678").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Thank you very much.").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Sincerely yours,").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            $"{pdfStateData.IgaarpName}").SetFontSize(11).SetFont(normalBoldFont))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f).SetMarginBottom(0f));
                    document.Add(new Paragraph()
                        .Add(new Text(
                            "IGAARP Coordinator").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Endorsed By:").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            $"{pdfStateData.DeanName}").SetFontSize(11).SetFont(normalBoldFont))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f).SetMarginBottom(0f));
                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Dean").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f));

                    #endregion

                    #region Signatures

                    //coordSignature.SetFixedPosition(50, 238).ScaleAbsolute(100, 30); IGAARP SIGNATURE
                    var coordSignaturePath = _webHost.ContentRootPath +
                                             $"/images/signatures/{coordinatorData.StampFileName}";
                    var deanSignaturePath = _webHost.ContentRootPath +
                                            $"/images/signatures/{deanData.StampFileName}";

                    var coordSignatureData = ImageDataFactory.Create(coordSignaturePath);
                    var coordSignature = new Image(coordSignatureData);
                    var deanSignatureData = ImageDataFactory.Create(deanSignaturePath);
                    var deanSignature = new Image(deanSignatureData);

                    if (companyData.AddressThree != null)
                    {
                        coordSignature.SetFixedPosition(180, 158).ScaleAbsolute(100, 30);
                        deanSignature.SetFixedPosition(50, 158).ScaleAbsolute(100, 30);
                    }
                    else if (companyData.AddressTwo != null)
                    {
                        coordSignature.SetFixedPosition(180, 169).ScaleAbsolute(100, 30);
                        deanSignature.SetFixedPosition(50, 169).ScaleAbsolute(100, 30);
                    }
                    else
                    {
                        coordSignature.SetFixedPosition(180, 180).ScaleAbsolute(100, 30);
                        deanSignature.SetFixedPosition(50, 180).ScaleAbsolute(100, 30);
                    }

                    document.Add(coordSignature);
                    document.Add(deanSignature);

                    #endregion

                    var companyResponseBoxPath = _webHost.ContentRootPath +
                                                 "/resources/images/companyresponsebox.png";
                    var companyResponseBoxData = ImageDataFactory.Create(companyResponseBoxPath);
                    var imgcompanyResponseBox = new Image(companyResponseBoxData);
                    imgcompanyResponseBox.SetFixedPosition(333, 200).ScaleAbsolute(210, 65);
                    document.Add(imgcompanyResponseBox);

                    #endregion

                    document.Close();
                    writer.Close();
                    var content = ms.ToArray();
                    return controller.File(content, contentType, fileName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public MemoryStream AddPdf(int submissionId)
        {
            var submissionData = _submissionRepository.GetAllRelatedData().First(s => s.Id == submissionId);
            var trackData = _trackRepository.Get(submissionData.TrackId);
            var programList = _programRepository.GetAll();
            var companyData = _companyRepository.Get(submissionData.CompanyId);
            var academicYearData = _academicYearRepository.GetAll().First();
            var pdfStateData = _pdfStateRepository.GetAll().First();
            var sectionData = _sectionRepository.Get(submissionData.Student.SectionId);
            var deanData = _adminRepository.GetAll().Where(a => a.AuthId == 1).First();
            var coordinatorList = _adminRepository.GetAll().Where(a => a.SectionId != null).ToList();
            var coordinatorData = coordinatorList.Find(a => a.SectionId == submissionData.Student.SectionId);


            var contentType = "application/pdf";
            var fileName =
                $"{submissionData.StudentNumber}-Endorsement-Letter-{DateTime.Now.ToString("MM/dd/yyyy")}.pdf";

            try
            {
                using (var ms = new MemoryStream())
                {
                    var writer = new PdfWriter(ms);
                    var pdf = new PdfDocument(writer);

                    var document = new Document(pdf, PageSize.LETTER);
                    document.SetMargins(36, 72, 72, 72);
                    document.SetProperty(Property.LEADING, new Leading(Leading.MULTIPLIED, 0.9f));
                    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackGroundColorEvent());

                    #region Fonts

                    var headProgram =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/fonts/candara-bold.ttf");
                    var subHeadProgram =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/fonts/TCM_____.TTF");
                    var normalBold =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/fonts/arial-bold.ttf");

                    var headerFont = PdfFontFactory.CreateFont(headProgram, PdfEncodings.WINANSI, true);
                    var subHeaderFont = PdfFontFactory.CreateFont(subHeadProgram, PdfEncodings.WINANSI, true);
                    var normalBoldFont = PdfFontFactory.CreateFont(normalBold, PdfEncodings.WINANSI, true);

                    #endregion

                    #region Header

                    var header = new Text("University Of Santo Tomas").SetFontSize(14).SetFont(headerFont);
                    var subHeaderOne = new Text($"{academicYearData.CollegeName}").SetFontSize(11)
                        .SetFont(headerFont);
                    var subHeaderTwo =
                        new Text(
                                $"Department Of {programList.First(p => p.Id == submissionData.Student.ProgramId).Name}")
                            .SetFont(subHeaderFont);
                    var pHead = new Paragraph().Add(header).SetTextAlignment(TextAlignment.CENTER);
                    var pSubHeadOne = new Paragraph().Add(subHeaderOne).SetTextAlignment(TextAlignment.CENTER);
                    var pSubHeadTwo = new Paragraph().Add(subHeaderTwo).SetTextAlignment(TextAlignment.CENTER);

                    document.Add(pHead.SetMarginBottom(0f).SetFixedLeading(9.0f));
                    document.Add(pSubHeadOne.SetMarginBottom(0f).SetFixedLeading(9.0f));
                    document.Add(pSubHeadTwo.SetMarginBottom(40.0f).SetFixedLeading(9.0f));


                    #region Logos

                    var iicsLogoPath = _webHost.ContentRootPath +
                                       $"/images/logo/{pdfStateData.CollegeLogoFileName}";
                    var ustLogoPath = _webHost.ContentRootPath +
                                      $"/images/logo/{pdfStateData.UstLogoFileName}";


                    var iicsLogoData = ImageDataFactory.Create(iicsLogoPath);
                    var ustLogoData = ImageDataFactory.Create(ustLogoPath);

                    var imgIics = new Image(iicsLogoData);
                    var imgUst = new Image(ustLogoData);

                    imgUst.SetFixedPosition(105, 705).ScaleAbsolute(60f, 60f);
                    imgIics.SetFixedPosition(450, 705).ScaleAbsolute(60f, 60f);
                    document.Add(imgUst);
                    document.Add(imgIics);

                    #endregion

                    #endregion

                    #region Body

                    #region Texts

                    var isoCode =
                        new Text(
                            $"UST:A022-{programList.First(p => p.Id == submissionData.Student.ProgramId).IsoCodeProgramNumber}-LE{programList.First(p => p.Id == submissionData.Student.ProgramId).IsoCode}");
                    var academicYear =
                        new Text(
                            $"AY {academicYearData.StartDate.ToString("yyyy")} - {academicYearData.EndDate.ToString("yyyy")}");
                    var isoCodep = new Paragraph().Add(isoCode).SetTextAlignment(TextAlignment.LEFT).SetFontSize(11);
                    var academicYearp = new Paragraph().Add(academicYear).SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(11);
                    document.Add(isoCodep.SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(academicYearp.SetMarginBottom(11.0f).SetFixedLeading(7.0f));


                    document.Add(new Paragraph()
                        .Add(new Text($"{DateTime.Now.ToString("MMMM dd, yyyy")}").SetFontSize(11))
                        .SetMarginBottom(11.0f)
                        .SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                                $"{submissionData.ContactPersonTitle}. {submissionData.ContactPersonFirstName} {submissionData.ContactPersonLastName}")
                            .SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(new Paragraph()
                        .Add(new Text($"{submissionData.ContactPersonPosition}").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(new Paragraph().Add(new Text($"{companyData.Name}").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));

                    if (companyData.AddressThree != null)
                    {
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressOne}").SetFontSize(11))
                            .SetMarginBottom(0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressTwo}").SetFontSize(11))
                            .SetMarginBottom(0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressThree}").SetFontSize(11))
                            .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    }
                    else if (companyData.AddressTwo != null)
                    {
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressOne}").SetFontSize(11))
                            .SetMarginBottom(0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressTwo}").SetFontSize(11))
                            .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    }
                    else
                    {
                        document.Add(new Paragraph().Add(new Text($"{companyData.AddressOne}").SetFontSize(11))
                            .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    }

                    //document.Add(new Paragraph().Add(new Text("AddressOne").SetFontSize(11))
                    //    .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    //document.Add(new Paragraph().Add(new Text("AddressTwo").SetFontSize(11))
                    //    .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    //document.Add(new Paragraph().Add(new Text("AddressThree").SetFontSize(11))
                    //    .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                                $"Dear {submissionData.ContactPersonTitle}. {submissionData.ContactPersonLastName}")
                            .SetFontSize(11))
                        .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT));

                    document.Add(new Paragraph().Add(new Text(
                            $"This is to recommend {submissionData.StudentTitle}. {submissionData.FirstName} {submissionData.MiddleInitial}. {submissionData.LastName}, " +
                            $"a bona fide student of section {sectionData.Name} of the {academicYearData.CollegeName} of the University  of Santo Tomas to take an Internship or  Practicum  Course  " +
                            $"this  {academicYearData.StartDate.ToString("MMMM")}  –  {academicYearData.EndDate.ToString("MMMM")}  {academicYearData.EndDate.ToString("yyyy")}  in  your  reputable  company.  As  part  of  our Outcomes-Based Education curriculum requirements " +
                            $"in the  B. S. {programList.First(p => p.Id == submissionData.Student.ProgramId).Name} program, said  student  must  undertake  a  minimum  of  {programList.First(p => p.Id == submissionData.Student.ProgramId).NumberOfHours}  hours  of  relevant  company  " +
                            "or  industry immersion. ").SetFontSize(11)).SetTextAlignment(TextAlignment.JUSTIFIED)
                        .SetFixedLeading(11.0f).SetMarginBottom(11.0f));


                    document.Add(new Paragraph()
                        .Add(new Text("In  particular,  we  hope  you  can  place  said  student  " +
                                      "in  an  environment  that  can  further  show his ability  " +
                                      "to solve  computing problems; and/or  to  work  and  communicate  " +
                                      "effectively  in  a project involving a multidisciplinary team; and " +
                                      "to be updated with the latest trends and emerging  technologies in " +
                                      $"his field of study. Since said student is pursuing a track in {trackData.Name}, please assign said student in any combination of activities " +
                                      "in line with the same track and other computer related activities except " +
                                      "encoding or clerical work. If possible, please allow our student to consult with his " +
                                      "assigned supervisor to conceptualize an industry-based research problem. This will enable our " +
                                      "students to conduct researches which are focused on their area and relevant to the actual needs of " +
                                      "the IT industry. ").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.JUSTIFIED)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "If you have further inquiries or concern, please contact us by " +
                            $"email at {academicYearData.IgaarpEmail} or at our land line " +
                            "number 786-1878 or 406-1611 extension 8678").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Thank you very much.").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Sincerely yours,").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            $"{pdfStateData.IgaarpName}").SetFontSize(11).SetFont(normalBoldFont))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f).SetMarginBottom(0f));
                    document.Add(new Paragraph()
                        .Add(new Text(
                            "IGAARP Coordinator").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Endorsed By:").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            $"{pdfStateData.DeanName}").SetFontSize(11).SetFont(normalBoldFont))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f).SetMarginBottom(0f));
                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Dean").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f));

                    #endregion

                    #region Signatures

                    //coordSignature.SetFixedPosition(50, 238).ScaleAbsolute(100, 30); IGAARP SIGNATURE
                    var coordSignaturePath = _webHost.ContentRootPath +
                                             $"/images/signatures/{coordinatorData.StampFileName}";
                    var deanSignaturePath = _webHost.ContentRootPath +
                                            $"/images/signatures/{deanData.StampFileName}";

                    var coordSignatureData = ImageDataFactory.Create(coordSignaturePath);
                    var coordSignature = new Image(coordSignatureData);
                    var deanSignatureData = ImageDataFactory.Create(deanSignaturePath);
                    var deanSignature = new Image(deanSignatureData);

                    if (companyData.AddressThree != null)
                    {
                        coordSignature.SetFixedPosition(165, 158).ScaleAbsolute(100, 30);
                        deanSignature.SetFixedPosition(50, 158).ScaleAbsolute(100, 30);
                    }
                    else if (companyData.AddressTwo != null)
                    {
                        coordSignature.SetFixedPosition(165, 169).ScaleAbsolute(100, 30);
                        deanSignature.SetFixedPosition(50, 169).ScaleAbsolute(100, 30);
                    }
                    else
                    {
                        coordSignature.SetFixedPosition(165, 180).ScaleAbsolute(100, 30);
                        deanSignature.SetFixedPosition(50, 180).ScaleAbsolute(100, 30);
                    }

                    document.Add(coordSignature);
                    document.Add(deanSignature);

                    #endregion

                    var companyResponseBoxPath = _webHost.ContentRootPath +
                                                 "/resources/images/companyresponsebox.png";
                    var companyResponseBoxData = ImageDataFactory.Create(companyResponseBoxPath);
                    var imgcompanyResponseBox = new Image(companyResponseBoxData);
                    imgcompanyResponseBox.SetFixedPosition(333, 200).ScaleAbsolute(210, 65);
                    document.Add(imgcompanyResponseBox);

                    #endregion

                    document.Close();
                    writer.Close();
                    return ms;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ActionResult PreviewPdf(ControllerBase controller, int adminId)
        {
            var adminData = _adminRepository.GetAllAdminsWithRelatedData().First(a => a.Id == adminId);
            var academicYearData = _academicYearRepository.GetAll().First();
            var pdfStateData = _pdfStateRepository.GetAll().First();

            var contentType = "application/pdf";
            var fileName =
                $"TestPDF-Endorsement-Letter-{DateTime.Now.ToString("MM/dd/yyyy")}.pdf";

            try
            {
                using (var ms = new MemoryStream())
                {
                    var writer = new PdfWriter(ms);
                    var pdf = new PdfDocument(writer);

                    var document = new Document(pdf, PageSize.LETTER);
                    document.SetMargins(36, 72, 72, 72);
                    document.SetProperty(Property.LEADING, new Leading(Leading.MULTIPLIED, 0.9f));
                    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackGroundColorEvent());

                    #region Fonts

                    var headProgram =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/fonts/candara-bold.ttf");
                    var subHeadProgram =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/fonts/TCM_____.TTF");
                    var normalBold =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/fonts/arial-bold.ttf");

                    var headerFont = PdfFontFactory.CreateFont(headProgram, PdfEncodings.WINANSI, true);
                    var subHeaderFont = PdfFontFactory.CreateFont(subHeadProgram, PdfEncodings.WINANSI, true);
                    var normalBoldFont = PdfFontFactory.CreateFont(normalBold, PdfEncodings.WINANSI, true);

                    #endregion

                    #region Header

                    var header = new Text("University Of Santo Tomas").SetFontSize(14).SetFont(headerFont);
                    var subHeaderOne = new Text("College of Information and Computing Sciences").SetFontSize(11)
                        .SetFont(headerFont);
                    var subHeaderTwo =
                        new Text(
                                "Department Of Information Technology")
                            .SetFont(subHeaderFont);
                    var pHead = new Paragraph().Add(header).SetTextAlignment(TextAlignment.CENTER);
                    var pSubHeadOne = new Paragraph().Add(subHeaderOne).SetTextAlignment(TextAlignment.CENTER);
                    var pSubHeadTwo = new Paragraph().Add(subHeaderTwo).SetTextAlignment(TextAlignment.CENTER);

                    document.Add(pHead.SetMarginBottom(0f).SetFixedLeading(9.0f));
                    document.Add(pSubHeadOne.SetMarginBottom(0f).SetFixedLeading(9.0f));
                    document.Add(pSubHeadTwo.SetMarginBottom(40.0f).SetFixedLeading(9.0f));


                    #region Logos

                    var iicsLogoPath = _webHost.ContentRootPath +
                                       $"/images/logo/{pdfStateData.CollegeLogoFileName}";
                    var ustLogoPath = _webHost.ContentRootPath +
                                      $"/images/logo/{pdfStateData.UstLogoFileName}";


                    var iicsLogoData = ImageDataFactory.Create(iicsLogoPath);
                    var ustLogoData = ImageDataFactory.Create(ustLogoPath);

                    var imgIics = new Image(iicsLogoData);
                    var imgUst = new Image(ustLogoData);

                    imgUst.SetFixedPosition(105, 705).ScaleAbsolute(60f, 60f);
                    imgIics.SetFixedPosition(450, 705).ScaleAbsolute(60f, 60f);
                    document.Add(imgUst);
                    document.Add(imgIics);

                    #endregion

                    #endregion

                    #region Body

                    #region Texts

                    var isoCode =
                        new Text(
                            "UST:A022-09-LE100");
                    var academicYear =
                        new Text(
                            $"AY {academicYearData.StartDate.ToString("yyyy")} - {academicYearData.EndDate.ToString("yyyy")}");
                    var isoCodep = new Paragraph().Add(isoCode).SetTextAlignment(TextAlignment.LEFT).SetFontSize(11);
                    var academicYearp = new Paragraph().Add(academicYear).SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(11);
                    document.Add(isoCodep.SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(academicYearp.SetMarginBottom(11.0f).SetFixedLeading(7.0f));


                    document.Add(new Paragraph()
                        .Add(new Text($"{DateTime.Now.ToString("MMMM dd, yyyy")}").SetFontSize(11))
                        .SetMarginBottom(11.0f)
                        .SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                                "Mr. Juana Delos Reyes")
                            .SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(new Paragraph()
                        .Add(new Text("Hr Manager").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(new Paragraph().Add(new Text("Accenture").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));


                    document.Add(new Paragraph().Add(new Text("Address One").SetFontSize(11))
                        .SetMarginBottom(0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    document.Add(new Paragraph().Add(new Text("Address Two").SetFontSize(11))
                        .SetMarginBottom(0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    document.Add(new Paragraph().Add(new Text("Address Three").SetFontSize(11))
                        .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));

                    //document.Add(new Paragraph().Add(new Text("AddressOne").SetFontSize(11))
                    //    .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    //document.Add(new Paragraph().Add(new Text("AddressTwo").SetFontSize(11))
                    //    .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));
                    //document.Add(new Paragraph().Add(new Text("AddressThree").SetFontSize(11))
                    //    .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                                "Dear Mr. Delos Reyes")
                            .SetFontSize(11))
                        .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT));

                    document.Add(new Paragraph().Add(new Text(
                            "This is to recommend Mr. Juan Rizal. Kapitana, " +
                            "a bona fide student of section 4ITH of the College of Information and Computing Sciences of the University of Santo Tomas to take an Internship or  Practicum  Course  " +
                            $"this  {academicYearData.StartDate.ToString("MMMM")}  –  {academicYearData.EndDate.ToString("MMMM")}  {academicYearData.EndDate.ToString("yyyy")}  in  your  reputable  company.  As  part  of  our Outcomes-Based Education curriculum requirements " +
                            "in the  B. S. Information Technology program, said  student  must  undertake  a  minimum  of  750  hours  of  relevant  company  " +
                            "or  industry immersion. ").SetFontSize(11)).SetTextAlignment(TextAlignment.JUSTIFIED)
                        .SetFixedLeading(11.0f).SetMarginBottom(11.0f));


                    document.Add(new Paragraph()
                        .Add(new Text("In  particular,  we  hope  you  can  place  said  student  " +
                                      "in  an  environment  that  can  further  show his ability  " +
                                      "to solve  computing problems; and/or  to  work  and  communicate  " +
                                      "effectively  in  a project involving a multidisciplinary team; and " +
                                      "to be updated with the latest trends and emerging  technologies in " +
                                      "his field of study. Since said student is pursuing a track in Web and Mobile Development, please assign said student in any combination of activities " +
                                      "in line with the same track and other computer related activities except " +
                                      "encoding or clerical work. If possible, please allow our student to consult with his " +
                                      "assigned supervisor to conceptualize an industry-based research problem. This will enable our " +
                                      "students to conduct researches which are focused on their area and relevant to the actual needs of " +
                                      "the IT industry. ").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.JUSTIFIED)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "If you have further inquiries or concern, please contact us by " +
                            $"email at {academicYearData.IgaarpEmail} or at our land line " +
                            "number 786-1878 or 406-1611 extension 8678").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Thank you very much.").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Sincerely yours,").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            $"{pdfStateData.IgaarpName}").SetFontSize(11).SetFont(normalBoldFont))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f).SetMarginBottom(0f));
                    document.Add(new Paragraph()
                        .Add(new Text(
                            "IGAARP Coordinator").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Endorsed By:").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f)
                        .SetMarginBottom(11.0f));

                    document.Add(new Paragraph()
                        .Add(new Text(
                            $"{pdfStateData.DeanName}").SetFontSize(11).SetFont(normalBoldFont))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f).SetMarginBottom(0f));
                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Dean").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f));

                    #endregion

                    #region Signatures

                    //coordSignature.SetFixedPosition(50, 238).ScaleAbsolute(100, 30); IGAARP SIGNATURE


                    if (adminData.AuthId == 1)
                    {
                        var deanSignaturePath = _webHost.ContentRootPath +
                                                $"/images/signatures/{adminData.StampFileName}";
                        var deanSignatureData = ImageDataFactory.Create(deanSignaturePath);
                        var deanSignature = new Image(deanSignatureData);
                        deanSignature.SetFixedPosition(50, 158).ScaleAbsolute(100, 30);
                        document.Add(deanSignature);
                    }
                    else
                    {
                        var coordSignaturePath = _webHost.ContentRootPath +
                                                 $"/images/signatures/{adminData.StampFileName}";
                        var coordSignatureData = ImageDataFactory.Create(coordSignaturePath);
                        var coordSignature = new Image(coordSignatureData);
                        coordSignature.SetFixedPosition(165, 158).ScaleAbsolute(100, 30);
                        document.Add(coordSignature);
                    }

                    #endregion

                    var companyResponseBoxPath = _webHost.ContentRootPath +
                                                 "/resources/images/companyresponsebox.png";
                    var companyResponseBoxData = ImageDataFactory.Create(companyResponseBoxPath);
                    var imgcompanyResponseBox = new Image(companyResponseBoxData);
                    imgcompanyResponseBox.SetFixedPosition(333, 200).ScaleAbsolute(210, 65);
                    document.Add(imgcompanyResponseBox);

                    #endregion

                    document.Close();
                    writer.Close();
                    var content = ms.ToArray();
                    return new FileContentResult(content, contentType);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public class BackGroundColorEvent : IEventHandler
        {
            private readonly Color SolidColor;

            public BackGroundColorEvent()
            {
                SolidColor = new DeviceRgb(248, 245, 238);
            }

            public void HandleEvent(Event @event)
            {
                var pdfDoc = (PdfDocumentEvent) @event;
                var pdf = pdfDoc.GetDocument();
                var page = pdfDoc.GetPage();

                var pageSize = page.GetPageSize();
                var pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);

                pdfCanvas.SaveState()
                    .SetFillColor(SolidColor)
                    .Rectangle(pageSize.GetLeft(), 0, pageSize.GetWidth(), pageSize.GetHeight())
                    .Fill().RestoreState();

                pdfCanvas.Release();
            }
        }
    }
}