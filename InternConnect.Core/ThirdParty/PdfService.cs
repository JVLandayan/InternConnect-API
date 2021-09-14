using System;
using System.IO;
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
        public IActionResult GeneratePdf(ControllerBase controller);
    }

    public class PdfService : IPdfService
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IAdminRepository _adminRepository;

        private readonly IPdfStateRepository _pdfStateRepository;

        private readonly ISubmissionRepository _submissionRepository;
        private readonly IWebHostEnvironment _webHost;

        public PdfService(IAdminRepository adminRepository, ISubmissionRepository submissionRepository,
            IAcademicYearRepository academicYear, IPdfStateRepository pdfStateRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _adminRepository = adminRepository;
            _submissionRepository = submissionRepository;
            _academicYearRepository = academicYear;
            _pdfStateRepository = pdfStateRepository;
            _webHost = webHostEnvironment;
        }


        public IActionResult GeneratePdf(ControllerBase controller)
        {
            var contentType = "pdf/application";
            var fileName = $"{DateTime.Now}+{Guid.NewGuid()}.pdf";

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
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/candara-bold.ttf");
                    var subHeadProgram =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/TCM_____.TTF");
                    var normalBold =
                        FontProgramFactory.CreateFont(_webHost.ContentRootPath + "/resources/arial-bold.ttf");

                    var headerFont = PdfFontFactory.CreateFont(headProgram, PdfEncodings.WINANSI, true);
                    var subHeaderFont = PdfFontFactory.CreateFont(subHeadProgram, PdfEncodings.WINANSI, true);
                    var normalBoldFont = PdfFontFactory.CreateFont(normalBold, PdfEncodings.WINANSI, true);

                    #endregion

                    #region Header

                    var header = new Text("University Of Santo Tomas").SetFontSize(14).SetFont(headerFont);
                    var subHeaderOne = new Text("College of Information and Computing Sciences").SetFontSize(11)
                        .SetFont(headerFont);
                    var subHeaderTwo = new Text("--DepartmentName--").SetFont(subHeaderFont);
                    var pHead = new Paragraph().Add(header).SetTextAlignment(TextAlignment.CENTER);
                    var pSubHeadOne = new Paragraph().Add(subHeaderOne).SetTextAlignment(TextAlignment.CENTER);
                    var pSubHeadTwo = new Paragraph().Add(subHeaderTwo).SetTextAlignment(TextAlignment.CENTER);

                    document.Add(pHead.SetMarginBottom(0f).SetFixedLeading(9.0f));
                    document.Add(pSubHeadOne.SetMarginBottom(0f).SetFixedLeading(9.0f));
                    document.Add(pSubHeadTwo.SetMarginBottom(40.0f).SetFixedLeading(9.0f));


                    #region Logos

                    var iicsLogoPath = _webHost.ContentRootPath +
                                       "/images/logo/UST-Seal-Institute-of-Information-Computing-Sciences-2014-Present-868x1024.png";
                    var ustLogoPath = _webHost.ContentRootPath +
                                      "/images/logo/Ust-logo.png";


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

                    var isoCode = new Text("UST:A022-(programId)-LE(isoCode)");
                    var academicYear = new Text("(startYear) - (endYear)");
                    var isoCodep = new Paragraph().Add(isoCode).SetTextAlignment(TextAlignment.LEFT).SetFontSize(11);
                    var academicYearp = new Paragraph().Add(academicYear).SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(11);
                    document.Add(isoCodep.SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(academicYearp.SetMarginBottom(11.0f).SetFixedLeading(7.0f));


                    document.Add(new Paragraph().Add(new Text($"{DateTime.Now}").SetFontSize(11)).SetMarginBottom(11.0f)
                        .SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));

                    document.Add(new Paragraph().Add(new Text("Title, ContactName").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(new Paragraph().Add(new Text("ContactNamePosition").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(new Paragraph().Add(new Text("CompanyName").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(0f).SetFixedLeading(7.0f));
                    document.Add(new Paragraph().Add(new Text("AddressOne, AddressTwo, AddressThree").SetFontSize(11))
                        .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT).SetFixedLeading(7.0f));

                    document.Add(new Paragraph().Add(new Text("Dear Mr. (LastNameContact)").SetFontSize(11))
                        .SetMarginBottom(11.0f).SetTextAlignment(TextAlignment.LEFT));

                    document.Add(new Paragraph().Add(new Text(
                            "This is to recommend Mr. Jovan S. Caballero, a bona fide student of section  3ISA of the Institute " +
                            "of Information and Computing Sciences of the University  of Santo Tomas to take an Internship or  Practicum  Course  " +
                            "this  June  –  July  2020  in  your  reputable  company.  As  part  of  our Outcomes-Based Education curriculum requirements " +
                            "in the  B. S. Information Systems program, said  student  must  undertake  a  minimum  of  300  hours  of  relevant  company  " +
                            "or  industry immersion. ").SetFontSize(11)).SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(11.0f).SetMarginBottom(11.0f));


                    document.Add(new Paragraph()
                        .Add(new Text("In  particular,  we  hope  you  can  place  said  student  " +
                                      "in  an  environment  that  can  further  show his ability  " +
                                      "to solve  computing problems; and/or  to  work  and  communicate  " +
                                      "effectively  in  a project involving a multidisciplinary team; and " +
                                      "to be updated with the latest trends and emerging  technologies in " +
                                      "his field of study. Since said student is pursuing a track in Service " +
                                      "Management, please assign said student in any combination of activities " +
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
                            "email at igaarp.ust.eng@gmail.com or at our land line " +
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
                            "--IGAARP NAME--").SetFontSize(11).SetFont(normalBoldFont))
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
                            "--Dean Name--").SetFontSize(11).SetFont(normalBoldFont))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f).SetMarginBottom(0f));
                    document.Add(new Paragraph()
                        .Add(new Text(
                            "Dean").SetFontSize(11))
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFixedLeading(7.0f));

                    #endregion

                    var companyResponseBoxPath = _webHost.ContentRootPath +
                                                 "/images/logo/companyresponsebox.png";
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