using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTechnoStar.Database
{
    public class Context: DbContext
    {
        public DbSet<DataEntry> DataEntries { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
    }
}
