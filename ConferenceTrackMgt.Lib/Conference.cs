using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConferenceTrackMgt.Lib
{
    public class Conference
    {
        private readonly List<Track> _tracks = new List<Track>();

        public bool TryAddTalk(Talk talk, out AddTalkErrorType? errorType)
        {

            if (_tracks.Count == 0)
            {
                _tracks.Add(new Track());
            }

            var track = _tracks.Last();

            if (track.TryAddTalk(talk, out errorType))
            {
                errorType = null;
                return true;
            }

            if (errorType == AddTalkErrorType.NoEnoughTimeSlot)
            {
                track = new Track();

                if (track.TryAddTalk(talk, out errorType))
                {
                    _tracks.Add(track);

                    errorType = null;
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var counter = 0;

            foreach (var track in _tracks)
            {
                sb.AppendLine($"Track {++counter}:");
                sb.Append(track.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
