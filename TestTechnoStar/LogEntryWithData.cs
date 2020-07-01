using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTechnoStar.Database;

namespace TestTechnoStar
{
    internal class LogEntryWithData : ILogDataEntry
    {
        public LogEntry LogEntryObject { get; set; }
        public DataEntry DataEntryObject { get; set; }

        public string Text
        {
            get => DataEntryObject.Text;
            set => DataEntryObject.Text = value;
        }

        public DateTime CreationDate => LogEntryObject.CreatedDate;

        public LogEntryWithData(LogEntry logEntry, DataEntry dataEntry)
        {
            LogEntryObject = logEntry;
            DataEntryObject = dataEntry;
        }
    }
}
