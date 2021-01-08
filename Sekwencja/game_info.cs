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
using System.Runtime;

namespace Sekwencja
{
    public class game_info
    {
        public string[] seq = new string[13];
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
            int current_pos = 35;
            int next_pos = new int();
            int[] table = new int[14];
            for (int i = 0; i < 14; i++)
                table[i] = 0;
            table[0] = 35;
            int counter = 0;
            Random rnd = new Random();
            int random = new int();
            int count_iterations = 0;

            while (check_if_full(table)==0 && check_if_stuck(count_iterations) == false)
            {
                random = (Guid.NewGuid().GetHashCode())%4 + 1; //1 - lewo, 2 - prawo, 3 - góra, 4 - dół
                count_iterations += 1;
                switch(random)
                {
                    case 1: //lewo
                        {
                            next_pos = current_pos - 1;
                            if ((current_pos != 0 && current_pos != 6 && current_pos != 12 &&
                            current_pos != 18 && current_pos != 24 && current_pos != 30) && (check_previous_seq(table, next_pos) == 0))
                            {
                                current_pos = next_pos;
                                this.seq[counter] = "left";
                                counter += 1;
                                table[counter] = current_pos;
                            }
                            break;
                        }
                    case 2: //prawo
                        {
                            next_pos = current_pos + 1;
                            if ((current_pos != 5 && current_pos != 11 && current_pos != 17 &&
                            current_pos != 23 && current_pos != 29 && current_pos != 35) && (check_previous_seq(table, next_pos) == 0))
                            {
                                current_pos = next_pos;
                                this.seq[counter] = "right";
                                counter += 1;
                                table[counter] = current_pos;
                            }
                            break;
                        }
                    case 3: //góra
                        {
                            next_pos = current_pos - 6;
                            if ((current_pos > 5 ) && (check_previous_seq(table, next_pos) == 0))
                            {
                                current_pos = next_pos;
                                this.seq[counter] = "up";
                                counter += 1;
                                table[counter] = current_pos;
                            }
                            break;
                        }
                    case 4: //dół
                        {
                            next_pos = current_pos + 6;
                            if ((current_pos < 30) && (check_previous_seq(table, next_pos) == 0))
                            {
                                current_pos = next_pos;
                                this.seq[counter] = "down";
                                counter += 1;
                                table[counter] = current_pos;
                            }
                            break;
                        }
                }

            }
            if(check_if_stuck(count_iterations) == true)
            {
                generate_again();
            }
        }

        private int check_previous_seq(int[] table, int next_pos)
        {
            bool invalid = false;
            for(int i=0;i<14;i++)
            {
                if (table[i] == next_pos)
                    invalid = true;
            }
            if (invalid == true)
                return 1;
            else
                return 0;
        }

        private int check_if_full(int[] table)
        {
            if (table[13] != 0)
                return 1; //wszystkie ruchy wygenerowane
            else
                return 0; //nie wszystkie ruchy wygenerowane
        }

        private bool check_if_stuck(int count_iterations)
        {
            if(count_iterations>10000)
                return true;
            else
                return false;
        }

        private void generate_again()
        {
            for(int i=0;i<13;i++)
                this.seq[i] = "";
            generate_seq();
        }
    }
}
