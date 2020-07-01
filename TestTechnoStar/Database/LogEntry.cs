using System;

namespace TestTechnoStar.Database
{
    public class LogEntry
    {
        public DateTime CreatedDate { get; set; }
        public virtual DataEntry Data { get; set; }
    }
}
