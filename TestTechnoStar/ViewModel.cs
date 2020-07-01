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
                //Если выбрано значение в списке, то редактируем его
                EditSelectedEntry();
            }
            else
            {
                //Если значение в списке не выбрано, добавим новую запись
                AddNewEntry();
            }
        }

        private void AddNewEntry()
        {
            using (var context = new Context())
            {
                var dataEntry = new DataEntry {Text = TextData};
                context.DataEntries.Add(dataEntry);
                var logEntry = new LogEntry {CreatedDate = DateTime.Now, Data = dataEntry};
                context.LogEntries.Add(logEntry);
                var logDataEntry = new LogEntryWithData(logEntry, dataEntry);
                LogDataEntries.Add(logDataEntry);
                context.SaveChanges();
            }
        }

        private void EditSelectedEntry()
        {
            using (var context = new Context())
            {
                SelectedLogDataEntry.DataEntryObject.Text = TextData;
                context.SaveChanges();
            }
        }

        private void RefreshList()
        {
            LogDataEntries.Clear();
            using (var context = new Context())
            {
                foreach (var logEntry in context.LogEntries)
                {
                    var dataLogEntry = new LogEntryWithData(logEntry, logEntry.Data);
                    LogDataEntries.Add(dataLogEntry);
                }
            }
        }

        public ViewModel()
        {
            RefreshList();
        }


    }
}
