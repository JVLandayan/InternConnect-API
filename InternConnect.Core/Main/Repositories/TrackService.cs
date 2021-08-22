using System.Collections.Generic;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Track;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Service.Main.Repositories
{
    public interface ITrackService
    {
        public void AddTrack(TrackDto.AddTrack payload);
        public void UpdateTrack(TrackDto.UpdateTrack payload);
        public TrackDto.ReadTrack GetTrack(int id);
        public IEnumerable<TrackDto.ReadTrack> GetAllTracks();
    }
    public class TrackService : ITrackService
    {
        private readonly IMapper _mapper;
        private readonly InternConnectContext _context;
        private readonly ITrackRepository _trackRepository;

        public TrackService(IMapper mapper, InternConnectContext context, ITrackRepository track)
        {
            _mapper = mapper;
            _context = context;
            _trackRepository = track;
        }
        public void AddTrack(TrackDto.AddTrack payload)
        {
           _trackRepository.Add(_mapper.Map<Track>(payload));
           _context.SaveChanges();
        }

        public IEnumerable<TrackDto.ReadTrack> GetAllTracks()
        {
            var trackList = _trackRepository.GetAll();
            var mappedList = new List<TrackDto.ReadTrack>();
            foreach (var track in trackList)
            {
             mappedList.Add(_mapper.Map<TrackDto.ReadTrack>(track));   
            }

            return mappedList;
;        }

        public TrackDto.ReadTrack GetTrack(int id)
        {
            return _mapper.Map<TrackDto.ReadTrack>(_trackRepository.Get(id));
        }

        public void UpdateTrack(TrackDto.UpdateTrack payload)
        {
            var trackData = _trackRepository.Get(payload.Id);
            _mapper.Map(payload, trackData);
            _context.SaveChanges();
        }
    }
}
