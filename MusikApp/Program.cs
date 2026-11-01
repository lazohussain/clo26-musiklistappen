using Microsoft.Extensions.Configuration;
using MusikApp;
// Läser inställningar från appsettings.json (samma sätt som i hero_databas).
// Databasens sökväg ligger där, så du slipper skriva den i koden.
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Hittar ingen connection string 'Default' i appsettings.json.");

// Börja här. Den här raden visar att inställningarna lästes in, ta bort den när du vill.
Console.WriteLine($"Musiklistappen: börja med att få menyn att fungera!  (databas: {connectionString})");


DbHandler db = new DbHandler(connectionString);

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("=== MUSIKLISTAPPEN ===");
    Console.WriteLine("1. Visa alla låtar");
    Console.WriteLine("2. Hitta låt via ID");
    Console.WriteLine("3. Sök låt via titel");
    Console.WriteLine("4. Lägg till låt");
    Console.WriteLine("5. Ta bort låt");
    Console.WriteLine("6. Avsluta");
    Console.Write("Välj ett alternativ: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            var songs = db.GetAllSongs();

            if (songs.Count == 0)
                Console.WriteLine("Det finns inga låtar.");
            else
            {
                foreach (var song in songs)
                {
                    Console.WriteLine(
                        $"ID: {song.Id} | Titel: {song.Title} | " +
                        $"Längd: {song.DurationSeconds} sekunder | Album-ID: {song.AlbumId}");
                }
            }
            break;

        case "2":
            Console.Write("Skriv låtens ID: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var song = db.GetSongById(id);

                if (song != null)
                    Console.WriteLine($"Titel: {song.Title}");
                else
                    Console.WriteLine("Låten hittades inte.");
            }
            else
                Console.WriteLine("Ogiltigt ID.");
            break;

        case "3":
            Console.Write("Skriv titel att söka efter: ");
            string searchText = Console.ReadLine() ?? "";

            var results = db.SearchSongs(searchText);

            if (results.Count == 0)
                Console.WriteLine("Inga låtar hittades.");

            foreach (var song in results)
                Console.WriteLine($"{song.Id}: {song.Title}");
            break;

        case "4":
            Console.Write("Titel: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Längd i sekunder: ");
            if (!int.TryParse(Console.ReadLine(), out int duration))
            {
                Console.WriteLine("Ogiltig längd.");
                break;
            }

            Console.Write("Album-ID: ");
            if (!int.TryParse(Console.ReadLine(), out int albumId))
            {
                Console.WriteLine("Ogiltigt album-ID.");
                break;
            }

            Song newSong = new Song
            {
                Title = title,
                DurationSeconds = duration,
                AlbumId = albumId
            };

            db.AddSong(newSong);
            Console.WriteLine("Låten har lagts till!");
            break;

        case "5":
            Console.Write("Ange ID på låten du vill ta bort: ");

            if (int.TryParse(Console.ReadLine(), out int deleteId))
            {
                Console.Write("Är du säker? (j/n): ");
                string answer = Console.ReadLine() ?? "";

                if (answer.ToLower() == "j")
                {
                    bool deleted = db.DeleteSong(deleteId);

                    if (deleted)
                        Console.WriteLine("Låten har tagits bort.");
                    else
                        Console.WriteLine("Låten hittades inte.");
                }
                else
                    Console.WriteLine("Borttagningen avbröts.");
            }
            else
                Console.WriteLine("Ogiltigt ID.");
            break;

        case "6":
            running = false;
            Console.WriteLine("Programmet avslutas.");
            break;

        default:
            Console.WriteLine("Välj ett alternativ mellan 1 och 6.");
            break;
    }
}
