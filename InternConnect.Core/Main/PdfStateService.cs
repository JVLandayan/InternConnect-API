using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.PdfState;

namespace InternConnect.Service.Main
{
    public interface IPdfStateService
    {
        public void UpdatePdfState(PdfStateDto.UpdatePdfState payload);
        public PdfStateDto.ReadPdfState GetPdfState();

        public PdfStateDto.ReadPdfState AddPdfState(PdfStateDto.AddPdfState payload);
    }

    public class PdfStateService : IPdfStateService
    {
        private readonly InternConnectContext _context;
        private readonly IMapper _mapper;
        private readonly IPdfStateRepository _pdfStateRepository;

        public PdfStateService(IMapper mapper, InternConnectContext context, IPdfStateRepository pdfState)
        {
            _context = context;
            _mapper = mapper;
            _pdfStateRepository = pdfState;
        }

        public PdfStateDto.ReadPdfState AddPdfState(PdfStateDto.AddPdfState payload)
        {
            if (_pdfStateRepository.GetAll().Count() == 0)
            {
                _pdfStateRepository.Add(_mapper.Map<PdfState>(payload));
                _context.SaveChanges();
                return null;
            }
            return new PdfStateDto.ReadPdfState();
        }

        public PdfStateDto.ReadPdfState GetPdfState()
        {
            return _mapper.Map<PdfStateDto.ReadPdfState>(_pdfStateRepository.GetAll().First());
        }

        public void UpdatePdfState(PdfStateDto.UpdatePdfState payload)
        {
            var pdfStateData = _pdfStateRepository.Get(payload.Id);
            _mapper.Map(payload, pdfStateData);
            _context.SaveChanges();
        }
    }
}