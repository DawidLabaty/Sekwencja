using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sekwencja
{
    /// <summary>
    /// Główna klasa projektu. Odpowiedzialna za utworzenie okna z grą, obsługę całej gry i odczytywania ruchów gracza z klawiatury.
    /// </summary>
    public partial class Form1 : Form
    {
        game_info game = new game_info();
        user_input input = new user_input();
        /// <summary>
        /// Konstruktor klasy Form1. Przypisywane są w nim wszystkie labele (bloki planszy) i uruchamiana metoda startująca grę.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            game.labels = new Label[36] {label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12,
                label13, label14, label15, label16, label17, label18, label19, label20, label21, label22, label23, label24,
                label25, label26, label27, label28, label29, label30, label31, label32, label33, label34, label35, label36};
            start_game();
        }

        /// <summary>
        /// Uruchomienie metody generowania sekwencji i pokazanie jej na ekranie (rozpoczęcie gry).
        /// </summary>
        private void start_game()
        {
            game.generate_seq();
            show_seq();
        }

        /// <summary>
        /// Pokazanie sekwencji na ekranie
        /// </summary>
        private void show_seq()
        {
            input.enabled = false;
            int current_pos;
            current_pos = 35;   //start to zawsze prawy dolny róg, czyli labels[35]
            game.labels[35].BackColor = Color.Green;
            //czas na zapamiętanie sekwencji zależny od ilości ruchów
            timer2.Interval = 500 * game.number_of_moves;
            if (game.level_stopwatch_started == false)  //sprawdzenie, czy rozpoczęto już pomiar czasu
            {
                game.level_time.Start();    //rozpoczęcie pomiaru czasu poziomu
                game.level_stopwatch_started = true;
            }
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

        /// <summary>
        /// Funkcja wymazująca sekwencję z planszy
        /// </summary>
        private void clear_screen()
        {
            for (int i = 0; i < 36; i++)
            {
                game.labels[i].BackColor = Color.SteelBlue;
                game.labels[i].Text = "";
            }
            game.labels[35].BackColor = Color.Green;
            input.enabled = true;
        }

        /// <summary>
        /// Funkcja sprawdzająca poprawność ruchów gracza i ukończenie poziomu.
        /// </summary>
        private void check_input()
        {
            bool flag;
            flag = true;
            //sprawdzenie, czy gracz popełnił błąd
            for (int i=0; i<game.number_of_moves;i++)
                {
                    if (input.keyboard_input[i] != game.seq[i])
                    {
                        flag = false;
                    }
                }
            //jeśli gracz poprawnie wykonał wszystkie ruchy
            if (flag == true)
            {
                //sprawdzenie czy skończony został cały poziom
                if (game.number_of_moves == game.max_moves)
                {
                    //zaktualizowanie wyświetlanej na ekranie punktacji
                    game.score += game.number_of_moves + 25;
                    points_label.Text = "PUNKTY: " + game.score.ToString();
                    //powiadomienie o ukończeniu poziomu
                    notification_label.ForeColor = Color.Green;
                    notification_label.Text = "Ukończono poziom " + game.level.ToString()+". Dodano 25 punktów.";
                    //wyświetlenie czasu przejścia zakończonego poziomu na ekranie
                    game.level_time.Stop();
                    TimeSpan ts = new TimeSpan();
                    ts = game.level_time.Elapsed;
                    string elapsedTime = String.Format("{0}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    level_time_label.Text = "CZAS: " + elapsedTime;
                    //reset stopera odmierzającego czas poziomu
                    game.level_time.Reset();
                    game.level_stopwatch_started = false;
                    //przygotowanie do kolejnego poziomu
                    if(game.level == 1 || game.level == 2)
                    {
                        game.number_of_moves = 3;
                        game.level += 1;
                        game.max_moves += 4;
                        input.count_input = 0;
                        input.current_pos = 35;
                        level_label.Text = "POZIOM: " + game.level;
                        //timer3 i wewnątrz niego clear_screen()
                        timer3.Interval = 500;
                        timer3.Start();
                        //timer4 i wewnątrz niego start_game()
                        timer4.Interval = 1500;
                        timer4.Start();
                    }
                    else if(game.level == 3)
                    {
                        //zaktualizowanie tekstu w obszarze powiadomień
                        notification_label.ForeColor = Color.Green;
                        notification_label.Text = "Gratulacje! Ukończono wszystkie poziomy.";
                    }
                }
                //jeśli nie ukończono jeszcze poziomu
                else
                {
                    //wyświetlenie na ekranie punktacji
                    game.score += game.number_of_moves;
                    points_label.Text = "PUNKTY: " + game.score.ToString();
                    //zwiększenie liczby ruchów
                    game.number_of_moves += 1;
                    input.count_input = 0;
                    input.current_pos = 35;
                    //timer do wyczyszczenia ekranu
                    timer3.Interval = 500;
                    timer3.Start();
                    //timer do pokazania sekwencji
                    timer1.Interval = 1000;
                    timer1.Start();
                }
            }
            //jeśli sekwencja jest nieprawidłowa
            else
            {
                //zaktualizowanie tekstu w obszarze powiadomień
                notification_label.ForeColor = Color.Crimson;
                notification_label.Text = "Nieprawidłowa sekwencja! Możesz zrestartować lub zakończyć grę używając menu poniżej.";
            }

        }

        //odczyt klawiszy strzałek
        /// <summary>
        /// "Odczyt" klawiszy strzałek - wymagany do działania ProcessCmdKey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        //nadpisanie ProcessCmdKey aby możliwe było odczytywanie klawiszy strzałek
        /// <summary>
        /// Nadpisanie ProcessCmdKey aby możliwe było bezpośrednie odczytywanie klawiszy strzałek.
        /// Przekazuje ona do zapisu ruchy gracza i dzięki niej wyświetlane są w czasie rzeczywistym ruchy gracza na planszy.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool invalid = false;
            if (input.enabled == true)
            {
                //zapisywanie ruchów gracza i pokazywanie ich na ekranie
                switch (keyData)
                {
                    case Keys.Down:
                        //sprawdzenie czy ruch jest nieprawidłowy
                        if (input.current_pos >= 30)
                            invalid = true;
                        else
                        {
                            input.keyboard_input[input.count_input] = "down";
                            game.labels[input.current_pos].Text = "\u2193";
                            input.current_pos += 6;
                            game.labels[input.current_pos].BackColor = Color.Green;
                        }
                        break;
                    case Keys.Up:
                        //sprawdzenie czy ruch jest nieprawidłowy
                        if (input.current_pos <= 5)
                            invalid = true;
                        else
                        {
                            input.keyboard_input[input.count_input] = "up";
                            game.labels[input.current_pos].Text = "\u2191";
                            input.current_pos -= 6;
                            game.labels[input.current_pos].BackColor = Color.Green;
                        }
                        break;
                    case Keys.Right:
                        //sprawdzenie czy ruch jest nieprawidłowy
                        if (input.current_pos == 5 || input.current_pos == 11 || input.current_pos == 17 ||
                            input.current_pos == 23 || input.current_pos == 29 || input.current_pos == 35)
                            invalid = true;
                        else
                        {
                            input.keyboard_input[input.count_input] = "right";
                            game.labels[input.current_pos].Text = "\u2192";
                            input.current_pos += 1;
                            game.labels[input.current_pos].BackColor = Color.Green;
                        }
                        break;
                    case Keys.Left:
                        //sprawdzenie czy ruch jest nieprawidłowy
                        if (input.current_pos == 0 || input.current_pos == 6 || input.current_pos == 12 ||
                            input.current_pos == 18 || input.current_pos == 24 || input.current_pos == 30)
                            invalid = true;
                        else
                        {
                            input.keyboard_input[input.count_input] = "left";
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
                        check_input();
                    }
                }
            }

                return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Metoda służąca do restartu gry. (Reset informacji o grze, zatrzymanie timerów itp.)
        /// </summary>
        private void restart_game()
        {
            //reset informacji o grze (czas poziomu, poziom, liczba ruchów itp.)
            game.reset_game_info();
            //zatrzymanie timerów
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            //zresetowanie wszystkich ruchów gracza
            input.reset_input();
            //wyczyszczenie ekranu
            clear_screen();
            //przywrócenie domyślnych wyświetlanych wartości (czas, poziom, punkty)
            level_time_label.Text = "CZAS: X";
            level_label.Text = "POZIOM: 1";
            points_label.Text = "PUNKTY: 0";
            notification_label.Text = "";
            //ponowne rozpoczęcie gry
            start_game();

        }

        /// <summary>
        /// Zakończenie działania aplikacji.
        /// </summary>
        private void end_game()
        {
            Application.Exit();
        }

        /// <summary>
        /// Metoda uruchamiana po kliknięciu na napis menu. Powoduje pokazanie opcji w menu (koniec i restart).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_label_Click(object sender, EventArgs e)
        {
            koniec_label.Visible = true;
            koniec_label.Enabled = true;
            restart_label.Visible = true;
            restart_label.Enabled = true;
        }

        /// <summary>
        /// Metoda uruchamiana po kliknięciu na napis restart. Uruchamia ona metodę restart_game() i ukrywa opcję menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restart_label_Click(object sender, EventArgs e)
        {
            //uruchomienie procedury restartu gry
            restart_game();
            koniec_label.Visible = false;
            koniec_label.Enabled = false;
            restart_label.Visible = false;
            restart_label.Enabled = false;
        }

        /// <summary>
        /// Metoda uruchamiana po kliknięciu na napis koniec. Uruchamia metodę end_game().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void koniec_label_Click(object sender, EventArgs e)
        {
            //uruchomienie procedury zakończenia gry
            end_game();
            koniec_label.Visible = false;
            koniec_label.Enabled = false;
            restart_label.Visible = false;
            restart_label.Enabled = false;
        }

        /// <summary>
        /// Timer do pokazania sekwencji. Uruchamia metodę show_seq().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            show_seq();
        }

        /// <summary>
        /// Timer do wymazania sekwencji z ekranu. Uruchamia metodę clear_screen().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            clear_screen();
            timer2.Stop();
        }

        /// <summary>
        /// Timer do wymazania sekwencji z ekranu przed przejściem do kolejnego poziomu. Uruchamia metodę clear_screen().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Stop();
            clear_screen();
        }
        /// <summary>
        /// Timer do uruchomienia metody start_game() startującej kolejny poziom. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Stop();
            //usunięcie powiadomienia o ukończeniu poziomu
            notification_label.Text = "";
            start_game();
        }
    }
}
