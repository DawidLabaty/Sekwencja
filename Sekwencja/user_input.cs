using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;


namespace Sekwencja
{
    public class user_input
    {
        public string[] keyboard_input = new string[10];
        public int count_input = 0;
        public int current_pos = 35;
        public bool enabled = false;
    }
}
