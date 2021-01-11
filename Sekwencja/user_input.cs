
namespace Sekwencja
{
    /// <summary>
    /// Klasa wykorzystana do zapisania informacji o ruchach wykonanych przez gracza.
    /// </summary>
    public class user_input
    {
        /// <summary>
        /// Tablica, w której zapisane są wszystkie ruchy gracza.
        /// </summary>
        public string[] keyboard_input = new string[13];
        /// <summary>
        /// Zliczanie ruchów wykonanych przez gracza.
        /// </summary>
        public int count_input = 0;
        /// <summary>
        /// Aktualna pozycja gracza na planszy. Prawy dolny róg to 35 (pozycja startowa).
        /// </summary>
        public int current_pos = 35;
        /// <summary>
        /// Informacja o tym czy wprowadzanie ruchów przez gracza jest włączone.
        /// </summary>
        public bool enabled = false;

        /// <summary>
        /// Reset zapisanych informacji o ruchach gracza.
        /// </summary>
        public void reset_input()
        {
            for (int i = 0; i < 13; i++)
                this.keyboard_input[i] = "";
            this.count_input = 0;
            this.current_pos = 35;
            this.enabled = false;
        }
    }
}
