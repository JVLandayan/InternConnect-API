using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Track;

namespace InternConnect.Service.Main
{
    public interface ITrackService
    {
        public TrackDto.ReadTrack AddTrack(TrackDto.AddTrack payload);
        public void UpdateTrack(TrackDto.UpdateTrack payload);
        public TrackDto.ReadTrack GetTrack(int id);
        public IEnumerable<TrackDto.ReadTrack> GetAllTracks();
        public void DeleteTrack(int id);
    }

    public class TrackService : ITrackService
    {
        private readonly InternConnectContext _context;
        private readonly IMapper _mapper;
        private readonly ITrackRepository _trackRepository;

        public TrackService(IMapper mapper, InternConnectContext context, ITrackRepository track)
        {
            _mapper = mapper;
            _context = context;
            _trackRepository = track;
        }

        public TrackDto.ReadTrack AddTrack(TrackDto.AddTrack payload)
        {
            var payloadData = _mapper.Map<Track>(payload);
            _trackRepository.Add(payloadData);
            payloadData.IsActive = true;
            _context.SaveChanges();
            return _mapper.Map<TrackDto.ReadTrack>(payloadData);
        }

        public IEnumerable<TrackDto.ReadTrack> GetAllTracks()
        {
            var trackList = _trackRepository.GetAll();
            var mappedList = new List<TrackDto.ReadTrack>();
            foreach (var track in trackList) mappedList.Add(_mapper.Map<TrackDto.ReadTrack>(track));

            return mappedList;
        }

        public TrackDto.ReadTrack GetTrack(int id)
        {
            var trackData = _trackRepository.Get(id);
            if (trackData.IsActive == false)
            {
                return null;
            }
            return _mapper.Map<TrackDto.ReadTrack>(trackData);
        }

        public void UpdateTrack(TrackDto.UpdateTrack payload)
        {
            var trackData = _trackRepository.Get(payload.Id);
            _mapper.Map(payload, trackData);
            trackData.IsActive = true;
            _context.SaveChanges();
        }

        public void DeleteTrack(int id)
        {
            _trackRepository.Get(id).IsActive = false;
            _context.SaveChanges();
        }
    }
}