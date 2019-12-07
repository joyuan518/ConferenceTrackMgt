using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackMgt.Lib
{
    public class Track
    {
        private const int MORNING_START_HOUR = 9;
        private const int LUNCH_START_HOUR = 12;

        private const int AFTERNOON_START_HOUR = 1;
        private const int NETWORK_EVENT_EARLIST_START_HOUR = 4;
        private const int NETWORK_EVENT_LATEST_START_HOUR = 5;

        private readonly Session _morningSession = new Session(new TimeSpan(MORNING_START_HOUR, 0, 0), new TimeSpan(LUNCH_START_HOUR, 0, 0));
        private readonly Session _afternoonSession = new Session(new TimeSpan(AFTERNOON_START_HOUR, 0, 0), new TimeSpan(NETWORK_EVENT_LATEST_START_HOUR, 0, 0));

        public bool TryAddTalk(Talk talk, out AddTalkErrorType? errorType)
        {
            if (_morningSession.TryAddTalk(talk, out errorType))
            {
                errorType = null;
                return true;
            }

            if (errorType == AddTalkErrorType.NoEnoughTimeSlot)
            {
                if (_afternoonSession.TryAddTalk(talk, out errorType))
                {
                    errorType = null;
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var section in _morningSession.Sections)
            {
                sb.AppendLine($"{section.StartTime.Hours.ToString("D2")}:{section.StartTime.Minutes.ToString("D2")}AM {section.Talk.Title} {section.Talk.Duration.TotalMinutes}min");
            }

            sb.AppendLine($"{LUNCH_START_HOUR.ToString("D2")}:00PM Lunch");

            foreach (var section in _afternoonSession.Sections)
            {
                sb.AppendLine($"{section.StartTime.Hours.ToString("D2")}:{section.StartTime.Minutes.ToString("D2")}PM {section.Talk.Title} {section.Talk.Duration.TotalMinutes}min");
            }

            var networkEventStartHour = _afternoonSession.Duration.Hours <= NETWORK_EVENT_EARLIST_START_HOUR ? NETWORK_EVENT_EARLIST_START_HOUR : NETWORK_EVENT_LATEST_START_HOUR;
            sb.AppendLine($"{networkEventStartHour.ToString("D2")}:00PM Networking Event");

            return sb.ToString();
        }
    }
}
