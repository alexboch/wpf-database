using System;

namespace TestTechnoStar.Database
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual DataEntry Data { get; set; }

    }
}
