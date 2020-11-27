using System;
using System.Collections.Generic;
using System.Text;

namespace websocketschat.Core.Services.Interfaces
{
    public interface IBotManager
    {
        public string ProcessRussian(string command);
        public string ProcessEnglish(string command);
    }
}
