using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Event;

namespace InternConnect.Service.Main
{
    public interface IEventService
    {
        public IEnumerable<EventDto.ReadEvent> GetAll(int adminId);
        public EventDto.ReadEvent GetbyId(int id);
        public void AddEvent(EventDto.AddEvent payload);
        public void UpdateEvent(EventDto.UpdateEvent payload);
    }

    public class EventService : IEventService
    {
        private readonly InternConnectContext _context;
        private readonly IEventRepository _eventsRepository;
        private readonly IMapper _mapper;

        public EventService(IMapper mapper, InternConnectContext context, IEventRepository events)
        {
            _mapper = mapper;
            _context = context;
            _eventsRepository = events;
        }

        public void AddEvent(EventDto.AddEvent payload)
        {
            _eventsRepository.Add(_mapper.Map<Event>(payload));
            _context.SaveChanges();
        }

        public IEnumerable<EventDto.ReadEvent> GetAll(int adminId)
        {
            var eventsList = _eventsRepository.GetAll().Where(e => e.AdminId == adminId);
            var mappedList = new List<EventDto.ReadEvent>();

            foreach (var eventElement in eventsList) mappedList.Add(_mapper.Map<EventDto.ReadEvent>(eventElement));

            return mappedList;
        }

        public EventDto.ReadEvent GetbyId(int id)
        {
            return _mapper.Map<EventDto.ReadEvent>(_eventsRepository.Get(id));
        }

        public void UpdateEvent(EventDto.UpdateEvent payload)
        {
            var eventData = _eventsRepository.Get(payload.Id);
            _mapper.Map(payload, eventData);
            _context.SaveChanges();
        }
    }
}