using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sekwencja
{
    /// <summary>
    /// Klasa zawierająca informacje o grze i jej obecnym stanie.
    /// </summary>
    public class game_info
    {
        /// <summary>
        /// Tablica przechowująca kolejne ruchy w formie tekstowej.
        /// </summary>
        public string[] seq = new string[13];
        /// <summary>
        /// Startowa liczba ruchów.
        /// </summary>
        public int number_of_moves = 3; //startowa liczba ruchów
        /// <summary>
        /// Tablica przechowująca 36 labeli na których wyświetlana jest sekwencja.
        /// </summary>
        public Label[] labels = new Label[36];
        /// <summary>
        /// Informacja o obecnym poziomie.
        /// </summary>
        public int level = 1;
        /// <summary>
        /// Maksymalna liczba ruchów w danym poziomie
        /// </summary>
        public int max_moves = 5; //maksymalna liczba ruchów w danym poziomie
        /// <summary>
        /// Zmienna zawierająca punktację.
        /// </summary>
        public int score = 0;
        /// <summary>
        /// Stoper do pomiaru czasu trwania poziomu.
        /// </summary>
        public Stopwatch level_time = new Stopwatch();
        /// <summary>
        /// Zmienna przechowująca informację o tym, czy rozpoczęto już pomiar czasu poziomu.
        /// </summary>
        public bool level_stopwatch_started = false;

        //reset informacji o grze
        /// <summary>
        /// Metoda resetująca informację o grze do stanu początkowego.
        /// </summary>
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

        /// <summary>
        /// Metoda generująca sekwencję w sposób pseudolosowy.
        /// </summary>
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

            //wykonuj dopóki tablica z ruchami nie jest pełna i generator się nie zablokował
            while (check_if_full(table)==0 && check_if_stuck(count_iterations) == false)
            {
                random = rnd.Next(1,4); //1 - lewo, 2 - prawo, 3 - góra, 4 - dół
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
            //sprawdzenie, czy generator "utknął" i nie może wygenerować kolejnego ruchu
            if(check_if_stuck(count_iterations) == true)
            {
                generate_again();
            }
        }

        /// <summary>
        /// Metoda sprawdzająca czy kolejny wygenerowany ruch nie przechodzi przez użyte już pola (nieprawidłowy ruch).
        /// </summary>
        /// <param name="table"></param> Tablica zawierająca numery wszystkich wykorzystanych pól.
        /// <param name="next_pos"></param> Zmienna przechowująca numer kolejnego pola.
        /// <returns>
        /// Zwraca wartość 1 jeśli wygenerowany ruch nie jest prawidłowy. W przeciwnym przypadku zwraca wartość 0.
        /// </returns>
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

        /// <summary>
        /// Metoda sprawdzająca czy wszystkie ruchy zostały już wygenerowane.
        /// </summary>
        /// <param name="table"></param>Tablica zawierająca numery wszystkich wykorzystanych pól.
        /// <returns>
        /// Zwraca wartość 1 jeśli wszystkie ruchy zostały wygenerowane. Jeśli tak nie jest to zwraca 0.
        /// </returns>
        private int check_if_full(int[] table)
        {
            if (table[13] != 0)
                return 1; //wszystkie ruchy wygenerowane
            else
                return 0; //nie wszystkie ruchy wygenerowane
        }

        /// <summary>
        /// Metoda sprawdzająca czy pseudolosowy generator sekwencji "utknął" i nie może wygenerować kolejnego ruchu.
        /// Generator nie jest bezbłędny i potrafi doprowadzić do sytuacji, w której nie może wygenerować kolejnego ruchu
        /// ze względu na ograniczenia krawędzi planszy lub brak możliwości przejścia przez poprzednio wykorzystane pola.
        /// </summary>
        /// <param name="count_iterations"></param>Zmienna zawierająca liczbę iteracji pętli generatora.
        /// Jeśli przekroczy ona wartość 10000 iteracji to uznaję, że generator "utknął" i trzeba ponownie wygenerować sekwencję.
        /// <returns>
        /// Zwraca wartość prawda, jeśli liczba iteracji jest większa niż 10000. W przeciwnym wypadku zwraca wartość fałsz.
        /// </returns>
        private bool check_if_stuck(int count_iterations)
        {
            if(count_iterations>10000)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Ponowne uruchomienie metody generate_seq() w przypadku 'utknięcia' generatora.
        /// </summary>
        private void generate_again()
        {
            for(int i=0;i<13;i++)
                this.seq[i] = "";
            generate_seq();
        }
    }
}
