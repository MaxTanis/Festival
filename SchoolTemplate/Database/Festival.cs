using System;

namespace SchoolTemplate.Database
{
    public class Festival
    {
        public int id { get; set; }

        public string naam { get; set; }

        public string plaats { get; set; }

        public DateTime start_dt { get; set; }

        public DateTime eind_dt { get; set; }

        public string image { get; set; }
    }

}
