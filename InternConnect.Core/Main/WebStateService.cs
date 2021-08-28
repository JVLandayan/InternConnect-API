using AutoMapper;
using InternConnect.Context;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.WebState;

namespace InternConnect.Service.Main
{
    public interface IWebStateService
    {
        public void UpdateWebState(WebStateDto.UpdateWebState payload);
        public WebStateDto.ReadWebState GetWebState(int id);
    }

    public class WebStateService : IWebStateService
    {
        private readonly InternConnectContext _context;
        private readonly IMapper _mapper;
        private readonly IWebStateRepository _webStateRepository;

        public WebStateService(InternConnectContext context, IMapper mapper, IWebStateRepository webState)
        {
            _context = context;
            _mapper = mapper;
            _webStateRepository = webState;
        }

        public WebStateDto.ReadWebState GetWebState(int id)
        {
            return _mapper.Map<WebStateDto.ReadWebState>(_webStateRepository.Get(id));
        }

        public void UpdateWebState(WebStateDto.UpdateWebState payload)
        {
            var webStateData = _webStateRepository.Get(payload.Id);
            _mapper.Map(payload, webStateData);
            _context.SaveChanges();
        }
    }
}