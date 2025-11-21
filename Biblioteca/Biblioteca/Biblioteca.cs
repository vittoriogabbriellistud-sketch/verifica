using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Biblioteca
{
    internal class Biblioteca
    {
        public string Nome { get; set; } = string.Empty;
        public string Indirizzo { get; set; } = string.Empty;

        
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

        
        public void AggiungiLibro(Libro libro)
        {
            if (libro is null) throw new ArgumentNullException(nameof(libro));
            _libri.Add(libro);
        }


        public Libro? CercaPerTitolo(string titolo)
        {
            if (string.IsNullOrWhiteSpace(titolo)) return null;
            return _libri.FirstOrDefault(l => string.Equals(l.Titolo, titolo, StringComparison.OrdinalIgnoreCase));
        }

       
        public List<Libro> CercaPerAutore(string autore)
        {
            if (string.IsNullOrWhiteSpace(autore)) return new List<Libro>();
            return _libri
                .Where(l => string.Equals(l.Autore, autore, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        public int ContaLibri() => _libri.Count;

        public bool EOpenAlle(TimeOnly orario)
        {
    
            if (OrarioApertura <= OrarioChiusura)
                return orario >= OrarioApertura && orario < OrarioChiusura;

         
            return orario >= OrarioApertura || orario < OrarioChiusura;
        }


    }
}