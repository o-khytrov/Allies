using System;
using System.Collections.Generic;
using System.Text;

namespace Allies
{
    public class Quest
    {
        public string Word { get; set; }
        public bool Result { get; set; }

        public string ResultDisplay { get { return Result? "✔️" : "❌"; } }
    }
}
