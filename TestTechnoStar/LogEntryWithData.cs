using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTechnoStar.Database;

namespace TestTechnoStar
{
    internal class LogEntryWithData
    {
        public LogEntry LogEntryObject { get; set; }
        public DataEntry DataEntryObject { get; set; }

        public LogEntryWithData(LogEntry logEntry, DataEntry dataEntry)
        {
            LogEntryObject = logEntry;
            DataEntryObject = dataEntry;
        }
    }
}
