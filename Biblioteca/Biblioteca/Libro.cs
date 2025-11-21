using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca


{
    internal class Libro
    {
        public string Autore { get; set; } = string.Empty;
        public string Titolo { get; set; } = string.Empty;
        public int AnnoPubblicazione { get; set; }
        public string Editore { get; set; } = string.Empty;

        private int _pagine;
        public int Pagine
        {
            get => _pagine;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Pagine), "Il numero di pagine non può essere negativo.");
                _pagine = value;
            }
        }

        public Libro() { }

        public Libro(string autore, string titolo, int annoPubblicazione, string editore, int pagine)
        {
            Autore = autore ?? string.Empty;
            Titolo = titolo ?? string.Empty;
            AnnoPubblicazione = annoPubblicazione;
            Editore = editore ?? string.Empty;
            Pagine = pagine;
        }

        // Restituisce una stringa con tutti i dati dell'oggetto
        public override string ToString()
        {
            return $"{Titolo} — {Autore}; {Editore} ({AnnoPubblicazione}) — {Pagine} pagine";
        }

        // Restituisce il tempo di lettura stimato secondo le regole richieste
        public string readingTime()
        {
            if (Pagine < 100) return "1h";
            if (Pagine <= 200) return "2h";
            return "più di 2h";
        }
    }
}