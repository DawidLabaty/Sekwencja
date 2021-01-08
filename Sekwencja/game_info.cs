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
    public class game_info
    {
        public string[] seq = new string[10];
        public int number_of_moves = 3; //startowa liczba ruchów
        public Label[] labels = new Label[36];
        public int level = 1;
        public int max_moves = 5; //maksymalna liczba ruchów w poziomie
        public int score = 0;
        public Stopwatch level_time = new Stopwatch();
        public bool level_stopwatch_started = false;

        //reset informacji o grze
        public void reset_game_info()
        {
        this.number_of_moves = 3;
        this.level = 1;
        this.max_moves = 5;
        this.score = 0;
        this.level_time.Stop();
        this.level_time.Reset();
        this.level_stopwatch_started = false;
        }

        //generowanie sekwencji
        public void generate_seq()
        {
            this.seq = new string[12] { "up", "left", "up", "left", "down", "left", "left", "up", "up", "right", "up", "left"};
        }
    }
}
