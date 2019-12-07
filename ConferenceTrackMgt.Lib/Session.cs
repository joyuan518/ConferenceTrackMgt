using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackMgt.Lib
{
    public class Session
    {
        private readonly TimeSpan _maxDuration;
        private readonly List<(TimeSpan StartTime, Talk Talk)> _sections = new List<(TimeSpan StartTime, Talk Talk)>();

        public IEnumerable<(TimeSpan StartTime, Talk Talk)> Sections
        {
            get
            {
                return _sections;
            }
        }

        public TimeSpan Duration
        {
            get;
            private set;
        }

        public Session(TimeSpan startTime, TimeSpan endTime)
        {
            if (startTime.TotalDays >= 1 || startTime.Hours > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(startTime));
            }

            if (endTime.TotalDays >= 1 || endTime.Hours > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(endTime));
            }

            if (startTime >= endTime)
            {
                throw new ArgumentException($"{nameof(startTime)} can't be equal or later than {nameof(endTime)}");
            }

            _maxDuration = endTime - startTime;
            Duration = startTime;
        }

        public bool TryAddTalk(Talk talk, out AddTalkErrorType? errorType)
        {
            if (Duration + talk.Duration <= _maxDuration)
            {
                _sections.Add((Duration, talk));
                Duration += talk.Duration;

                errorType = null;
                return true;
            }

            errorType = AddTalkErrorType.NoEnoughTimeSlot;
            return false;
        }
    }
}
