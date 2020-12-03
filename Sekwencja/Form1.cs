using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sekwencja
{
    public partial class Form1 : Form
    {
        game_info game = new game_info();
        public Form1()
        {
            InitializeComponent();
            game.labels = new Label[36] {label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12,
                label13, label14, label15, label16, label17, label18, label19, label20, label21, label22, label23, label24,
                label25, label26, label27, label28, label29, label30, label31, label32, label33, label34, label35, label36};
            show_seq(game);
        }

        public void show_seq(game_info game)
        {
            //start to zawsze prawy dolny róg, czyli labels[35]
            int current_pos = 35;
            game.labels[35].BackColor = Color.Green;
            timer2.Interval = 500 * game.number_of_moves;
            for (int i = 0; i < game.number_of_moves; i++)
            {
                    switch (game.seq[i])
                    {
                        case "up":
                            game.labels[current_pos].Text = "\u2191";    //strzałka w górę
                            game.labels[current_pos].Font = new Font("Arial", 48);
                            current_pos -= 6;
                            game.labels[current_pos].BackColor = Color.Green;
                            break;
                        case "down":
                            game.labels[current_pos].Text = "\u2193";    //strzałka w dół
                            game.labels[current_pos].Font = new Font("Arial", 48);
                            current_pos += 6;
                            game.labels[current_pos].BackColor = Color.Green;
                            break;
                        case "left":
                            game.labels[current_pos].Text = "\u2190";    //strzałka w lewo
                            game.labels[current_pos].Font = new Font("Arial", 48);
                            current_pos -= 1;
                            game.labels[current_pos].BackColor = Color.Green;
                            break;
                        case "right":
                            game.labels[current_pos].Text = "\u2192";    //strzałka w prawo
                            game.labels[current_pos].Font = new Font("Arial", 48);
                            current_pos += 1;
                            game.labels[current_pos].BackColor = Color.Green;
                            break;
                    }
            }
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            clear_screen(game.labels);
            timer2.Stop();
        }

        public void clear_screen(Label[] labels)
        {
            for (int i = 0; i < 36; i++)
            {
                labels[i].BackColor = Color.Gray;
                labels[i].Text = "";
            }
                
        }

        public class game_info
        {
            public string[] seq = new string[10] { "up", "left", "up", "right", "up", "left", "", "", "", "" };
            public int number_of_moves = 3; //startowa liczba ruchów
            public Label[] labels = new Label[36];
        }

    }
}
