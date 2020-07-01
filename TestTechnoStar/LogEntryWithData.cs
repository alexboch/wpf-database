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
            get;
            set;
        }

        public void Save()
        {
            using (var context = new Context())
            {

                DataEntryObject.Text = Text;
                context.DataEntries.Attach(DataEntryObject);
                context.Entry(DataEntryObject).Property(x => x.Text).IsModified = true;
                context.SaveChanges();

            }
        }

        public DateTime CreationDate => LogEntryObject.CreatedDate;

        public LogEntryWithData(LogEntry logEntry, DataEntry dataEntry)
        {
            LogEntryObject = logEntry;
            DataEntryObject = dataEntry;
            Text = DataEntryObject.Text;
        }
    }
}
