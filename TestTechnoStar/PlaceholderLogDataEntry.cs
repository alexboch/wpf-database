using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTechnoStar
{
    class PlaceholderLogDataEntry : ILogDataEntry
    {
        private string _text;

        public string Text
        {
            get => string.Empty;
            set => _text = value;
        }
    }
}
