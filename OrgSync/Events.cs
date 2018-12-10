using System;
using System.Collections.Generic;
using System.Text;

namespace OrgSync
{
    class Events
    {
        public string EventType { get; set; }

        public string Location { get; set; }

        public DateTime DayTime { get; set; }

        public Events()
        {

        }

    public Events(string type, string location, DateTime time)
    {
        EventType = type;
        Location = location;
        DayTime = time;
    }
    }

}
