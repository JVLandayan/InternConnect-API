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
        public EventDto.ReadEvent AddEvent(EventDto.AddEvent payload);
        public void UpdateEvent(EventDto.UpdateEvent payload);

        public void DeleteEvent(int id);
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

        public EventDto.ReadEvent AddEvent(EventDto.AddEvent payload)
        {
            var payloadData = _mapper.Map<Event>(payload);
            _eventsRepository.Add(payloadData);
            _context.SaveChanges();

            return _mapper.Map<EventDto.ReadEvent>(payloadData);
        }

        public void DeleteEvent(int id)
        {
            _eventsRepository.Remove(_eventsRepository.Get(id));
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