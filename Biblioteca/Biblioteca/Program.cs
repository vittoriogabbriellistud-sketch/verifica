using System;
using System.Collections.Generic;
using System.Globalization;

// Biblioteca Scolastica - Gestione di libri e informazioni
//vittorio gabbrielli 4 i 
namespace Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("=== Gestione Biblioteca Scolastica ===");
                Console.WriteLine("1) Crea/Configura biblioteca");
                Console.WriteLine("2) Mostra info biblioteca");
                Console.WriteLine("3) Aggiungi libro");
                Console.WriteLine("4) Aggiungi alcuni libri di esempio");
                Console.WriteLine("5) Cerca libro per titolo");
                Console.WriteLine("6) Cerca libri per autore");
                Console.WriteLine("7) Mostra tutti i libri");
                Console.WriteLine("8) Conta libri");
                Console.WriteLine("9) Mostra tempo di lettura di un libro (per titolo)");
                Console.WriteLine("0) Esci");
                Console.Write("Seleziona un'opzione: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1":
                        ConfiguraBiblioteca(biblioteca);
                        break;
                    case "2":
                        StampaInfoBiblioteca(biblioteca);
                        break;
                    case "3":
                        AggiungiLibroInterattivo(biblioteca);
                        break;
                    case "4":
                        AggiungiEsempi(biblioteca);
                        break;
                    case "5":
                        CercaPerTitoloInterattivo(biblioteca);
                        break;
                    case "6":
                        CercaPerAutoreInterattivo(biblioteca);
                        break;
                    case "7":
                        MostraTuttiLibri(biblioteca);
                        break;
                    case "8":
                        Console.WriteLine($"Numero di libri: {biblioteca.ContaLibri()}");
                        break;
                    case "9":
                        MostraReadingTime(biblioteca);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opzione non valida.");
                        break;
                }
            }
        }

        static void ConfiguraBiblioteca(Biblioteca b)
        {
            Console.Write("Nome biblioteca: ");
            b.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Indirizzo: ");
            b.Indirizzo = Console.ReadLine() ?? string.Empty;

            b.OrarioApertura = ReadTime("Orario apertura (HH:mm, es. 09:00): ", new TimeOnly(9, 0));
            b.OrarioChiusura = ReadTime("Orario chiusura (HH:mm, es. 18:00): ", new TimeOnly(18, 0));

            Console.WriteLine("Biblioteca configurata.");
        }

        static TimeOnly ReadTime(string prompt, TimeOnly defaultValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    return defaultValue;

                if (TimeOnly.TryParseExact(input.Trim(), "H:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var t) ||
                    TimeOnly.TryParse(input.Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out t))
                    return t;

                Console.WriteLine("Formato orario non valido. Riprova.");
            }
        }

        static void AggiungiLibroInterattivo(Biblioteca b)
        {
            var libro = new Libro();

            Console.Write("Titolo: ");
            libro.Titolo = Console.ReadLine() ?? string.Empty;

            Console.Write("Autore: ");
            libro.Autore = Console.ReadLine() ?? string.Empty;

            libro.AnnoPubblicazione = ReadInt("Anno pubblicazione (es. 2020): ", 0);
            Console.Write("Editore: ");
            libro.Editore = Console.ReadLine() ?? string.Empty;

            libro.Pagine = ReadInt("Numero pagine: ", 0);

            b.AggiungiLibro(libro);
            Console.WriteLine("Libro aggiunto:");
            Console.WriteLine(libro.ToString());
        }

        static int ReadInt(string prompt, int defaultValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string? s = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(s)) return defaultValue;
                if (int.TryParse(s.Trim(), out int v)) return v;
                Console.WriteLine("Numero non valido. Riprova.");
            }
        }

        static void AggiungiEsempi(Biblioteca b)
        {
            var esempi = new List<Libro>
            {
                new Libro("Giovanni Rossi", "Storia d'Italia", 2015, "Editore A", 320),
                new Libro("Mario Bianchi", "Matematica facile", 2018, "Editore B", 150),
                new Libro("Giovanni Rossi", "Appunti di storia", 2020, "Editore C", 90),
            };

            foreach (var l in esempi) b.AggiungiLibro(l);
            Console.WriteLine($"{esempi.Count} libri di esempio aggiunti.");
        }

        static void CercaPerTitoloInterattivo(Biblioteca b)
        {
            Console.Write("Titolo da cercare: ");
            string? titolo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(titolo))
            {
                Console.WriteLine("Titolo vuoto.");
                return;
            }

            var trovato = b.CercaPerTitolo(titolo.Trim());
            if (trovato is null)
                Console.WriteLine("Nessun libro trovato con quel titolo.");
            else
                Console.WriteLine(trovato.ToString());
        }

        static void CercaPerAutoreInterattivo(Biblioteca b)
        {
            Console.Write("Autore da cercare: ");
            string? autore = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(autore))
            {
                Console.WriteLine("Autore vuoto.");
                return;
            }

            var risultati = b.CercaPerAutore(autore.Trim());
            if (risultati.Count == 0)
            {
                Console.WriteLine("Nessun libro trovato per quell'autore.");
                return;
            }

            Console.WriteLine($"Trovati {risultati.Count} libri:");
            foreach (var l in risultati) Console.WriteLine(l.ToString());
        }

        static void MostraTuttiLibri(Biblioteca b)
        {
            if (b.ContaLibri() == 0)
            {
                Console.WriteLine("Nessun libro presente.");
                return;
            }

            Console.WriteLine("Elenco libri:");
            foreach (var l in b.Libri) Console.WriteLine(l.ToString());
        }

        static void MostraInfoBiblioteca(Biblioteca b)
        {
            Console.WriteLine($"Nome: {b.Nome}");
            Console.WriteLine($"Indirizzo: {b.Indirizzo}");
            Console.WriteLine($"Apertura: {b.OrarioApertura:HH:mm} - Chiusura: {b.OrarioChiusura:HH:mm}");
            Console.WriteLine($"Libri presenti: {b.ContaLibri()}");
        }

        static void MostraReadingTime(Biblioteca b)
        {
            Console.Write("Titolo del libro per ottenere il tempo di lettura: ");
            string? titolo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(titolo))
            {
                Console.WriteLine("Titolo vuoto.");
                return;
            }

            var libro = b.CercaPerTitolo(titolo.Trim());
            if (libro is null)
            {
                Console.WriteLine("Libro non trovato.");
                return;
            }

            Console.WriteLine($"Libro: {libro.Titolo} — Tempo stimato di lettura: {libro.readingTime()}");
        }
    }
}