using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTechnoStar.Database;

namespace TestTechnoStar
{
    internal class ViewModel
    {

        public ObservableCollection<LogEntryWithData> LogDataEntries { get; set; } = new ObservableCollection<LogEntryWithData>();

        /// <summary>
        /// Вводимые текстовые данные
        /// </summary>
        public string TextData { get; set; }

        /// <summary>
        /// Выбранное в списке значение
        /// </summary>
        public LogEntryWithData SelectedLogDataEntry { get; set; }

        public void SetText()
        {
            if (SelectedLogDataEntry != null)
            {
                EditSelectedEntry();
            }
            else
            {
                AddNewEntry();
            }
        }

        public void AddNewEntry()
        {
            using (var context = new Context())
            {
                var dataEntry = new DataEntry {Text = TextData};
                context.DataEntries.Add(dataEntry);
                var logEntry = new LogEntry {CreatedDate = DateTime.Now, Data = dataEntry};
                context.LogEntries.Add(logEntry);
                context.SaveChanges();
            }
        }

        public void EditSelectedEntry()
        {
            using (var context = new Context())
            {

                context.SaveChanges();
            }
        }




    }
}
