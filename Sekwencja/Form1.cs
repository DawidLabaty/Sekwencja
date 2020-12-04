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
    public partial class Form1 : Form
    {
        game_info game = new game_info();
        user_input input = new user_input();
        public Form1()
        {
            InitializeComponent();
            game.labels = new Label[36] {label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12,
                label13, label14, label15, label16, label17, label18, label19, label20, label21, label22, label23, label24,
                label25, label26, label27, label28, label29, label30, label31, label32, label33, label34, label35, label36};
            show_seq(game);
        }

        public void wait(int ms)
        {
            Stopwatch wait = new Stopwatch();
            wait.Start();
            while (wait.ElapsedMilliseconds <= ms)
            {

            }
            wait.Stop();
            wait.Reset();
        }

        public void show_seq(game_info game)
        {
            //start to zawsze prawy dolny róg, czyli labels[35]
            int current_pos;
            current_pos = 35;
            game.labels[35].BackColor = Color.Green;
            timer2.Interval = 500 * game.number_of_moves;
            game.level_time.Start();
            for (int i = 0; i < game.number_of_moves; i++)
            {
                    switch (game.seq[i])
                    {
                        case "up":
                            game.labels[current_pos].Text = "\u2191";    //strzałka w górę
                            current_pos -= 6;
                            game.labels[current_pos].BackColor = Color.Green;
                            break;
                        case "down":
                            game.labels[current_pos].Text = "\u2193";    //strzałka w dół
                            current_pos += 6;
                            game.labels[current_pos].BackColor = Color.Green;
                            break;
                        case "left":
                            game.labels[current_pos].Text = "\u2190";    //strzałka w lewo
                            current_pos -= 1;
                            game.labels[current_pos].BackColor = Color.Green;
                        break;
                        case "right":
                            game.labels[current_pos].Text = "\u2192";    //strzałka w prawo
                            current_pos += 1;
                            game.labels[current_pos].BackColor = Color.Green;
                        break;
                    }
            }
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            clear_screen(game);
            timer2.Stop();
        }

        public void clear_screen(game_info game)
        {
            for (int i = 0; i < 36; i++)
            {
                game.labels[i].BackColor = Color.SteelBlue;
                game.labels[i].Text = "";
            }
            game.labels[35].BackColor = Color.Green;
            input.enabled = true;
        }

        public void check_input(game_info game)
        {
            bool flag;
            flag = true;
            for (int i=0; i<game.number_of_moves;i++)
                {
                    if (input.input[i] != game.seq[i])
                    {
                        flag = false;
                    }
                }
            if (flag == true)
            {
                if (game.number_of_moves == game.max_moves)
                {
                    //wyświetlenie na ekranie punktacji
                    game.score += game.number_of_moves + 25;
                    points_label.Text = "PUNKTY: " + game.score.ToString();
                    //pomiar czasu poziomu i wyświetlenie go na ekranie
                    game.level_time.Stop();
                    TimeSpan ts = new TimeSpan();
                    ts = game.level_time.Elapsed;
                    string elapsedTime = String.Format("{0:00}.{1:00}",ts.Seconds, ts.Milliseconds/10);
                    game.level_time.Reset();
                    level_time_label.Text = "CZAS: "+elapsedTime;
                    //przygotowanie do kolejnego poziomu
                    game.number_of_moves = 3;
                    game.level += 1;
                    game.max_moves += 5;
                    input.count_input = 0;
                    input.current_pos = 35;
                }
                else
                {
                    //wyświetlenie na ekranie punktacji
                    game.score += game.number_of_moves;
                    points_label.Text = "PUNKTY: " + game.score.ToString();
                    //zwiększenie liczby ruchów
                    game.number_of_moves += 1;
                    input.count_input = 0;
                    input.current_pos = 35;
                    clear_screen(game);
                    show_seq(game);
                }
            }
            else
                MessageBox.Show("Nieprawidłowa sekwencja");

        }

        //odczyt klawiszy strzałek
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool invalid = false;
            if (input.enabled == true)
            {
                //zapisywanie ruchów gracza i pokazywanie ich na ekranie
                switch (keyData)
                {
                    case Keys.Down:
                        if (input.current_pos >= 30)
                            invalid = true;
                        else
                        {
                            input.input[input.count_input] = "down";
                            game.labels[input.current_pos].Text = "\u2193";
                            input.current_pos += 6;
                            game.labels[input.current_pos].BackColor = Color.Green;
                        }
                        break;
                    case Keys.Up:
                        if (input.current_pos <= 5)
                            invalid = true;
                        else
                        {
                            input.input[input.count_input] = "up";
                            game.labels[input.current_pos].Text = "\u2191";
                            input.current_pos -= 6;
                            game.labels[input.current_pos].BackColor = Color.Green;
                        }
                        break;
                    case Keys.Right:
                        if (input.current_pos == 5 || input.current_pos == 11 || input.current_pos == 17 ||
                            input.current_pos == 23 || input.current_pos == 29 || input.current_pos == 35)
                            invalid = true;
                        else
                        {
                            input.input[input.count_input] = "right";
                            game.labels[input.current_pos].Text = "\u2192";
                            input.current_pos += 1;
                            game.labels[input.current_pos].BackColor = Color.Green;
                        }
                        break;
                    case Keys.Left:
                        if (input.current_pos == 0 || input.current_pos == 6 || input.current_pos == 12 ||
                            input.current_pos == 18 || input.current_pos == 24 || input.current_pos == 31)
                            invalid = true;
                        else
                        {
                            input.input[input.count_input] = "left";
                            game.labels[input.current_pos].Text = "\u2190";
                            input.current_pos -= 1;
                            game.labels[input.current_pos].BackColor = Color.Green;
                        }
                        break;
                }
                if (invalid == false) //wykonaj tylko jeśli ruch jest prawidłowy
                {
                    input.count_input += 1;
                    if (input.count_input == game.number_of_moves)
                    {
                        input.enabled = false;
                        check_input(game);
                    }
                }
            }

                return base.ProcessCmdKey(ref msg, keyData);
        }

        public void restart_game()
        {

        }

        public void end_game()
        {

        }

        private void menu_label_Click(object sender, EventArgs e)
        {
            koniec_label.Visible = true;
            koniec_label.Enabled = true;
            restart_label.Visible = true;
            restart_label.Enabled = true;
        }

        private void restart_label_Click(object sender, EventArgs e)
        {
            restart_game();
            koniec_label.Visible = false;
            koniec_label.Enabled = false;
            restart_label.Visible = false;
            restart_label.Enabled = false;
        }

        private void koniec_label_Click(object sender, EventArgs e)
        {
            end_game();
            koniec_label.Visible = false;
            koniec_label.Enabled = false;
            restart_label.Visible = false;
            restart_label.Enabled = false;
        }
    }
}
