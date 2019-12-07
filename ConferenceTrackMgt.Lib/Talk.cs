using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackMgt.Lib
{
    public class Talk
    {
        /// <summary>
        /// Talk duration in minutes
        /// </summary>
        public TimeSpan Duration
        {
            get;
            private set;
        }

        public string Title
        {
            get;
            private set;
        }

        public Talk(TimeSpan duration, string title)
        {
            Duration = duration;
            Title = title;
        }
    }
}
