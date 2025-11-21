using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca
{
    internal class Biblioteca
    {
        public string Nome { get; set; } = string.Empty;
        public string Indirizzo { get; set; } = string.Empty;

        // Orari giornalieri: uso TimeOnly (.NET 6+) per rappresentare orari
        public TimeOnly OrarioApertura { get; set; } = new TimeOnly(9, 0);
        public TimeOnly OrarioChiusura { get; set; } = new TimeOnly(18, 0);

        private readonly List<Libro> _libri = new();

        public IReadOnlyList<Libro> Libri => _libri.AsReadOnly();

        public Biblioteca() { }

        public Biblioteca(string nome, string indirizzo, TimeOnly apertura, TimeOnly chiusura)
        {
            Nome = nome ?? string.Empty;
            Indirizzo = indirizzo ?? string.Empty;
            OrarioApertura = apertura;
            OrarioChiusura = chiusura;
        }

        // Aggiunge un nuovo libro alla biblioteca
        public void AggiungiLibro(Libro libro)
        {
            if (libro is null) throw new ArgumentNullException(nameof(libro));
            _libri.Add(libro);
        }

        // Ricerca di un libro a partire dal titolo (prima occorrenza, confronto case-insensitive)
        public Libro? CercaPerTitolo(string titolo)
        {
            if (string.IsNullOrWhiteSpace(titolo)) return null;
            return _libri.FirstOrDefault(l => string.Equals(l.Titolo, titolo, StringComparison.OrdinalIgnoreCase));
        }

        // Ricerca di tutti i libri di uno specifico autore (confronto case-insensitive)
        public List<Libro> CercaPerAutore(string autore)
        {
            if (string.IsNullOrWhiteSpace(autore)) return new List<Libro>();
            return _libri
                .Where(l => string.Equals(l.Autore, autore, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Determina il numero dei libri presenti
        public int ContaLibri() => _libri.Count;

        // Utility: verifica se la biblioteca è aperta a un dato orario
        public bool EOpenAlle(TimeOnly orario)
        {
            // Se apertura <= chiusura (stesso giorno)
            if (OrarioApertura <= OrarioChiusura)
                return orario >= OrarioApertura && orario < OrarioChiusura;

            // Apertura e chiusura attraversano la mezzanotte (es. 22:00 - 02:00)
            return orario >= OrarioApertura || orario < OrarioChiusura;
        }
    }
}