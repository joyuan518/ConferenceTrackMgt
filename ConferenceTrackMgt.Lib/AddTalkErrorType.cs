using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceTrackMgt.Lib
{
    [Flags]
    public enum AddTalkErrorType
    {
        NoEnoughTimeSlot = 1,
        TalkDurationExceedTheSessionLimit = 2
    }
}
