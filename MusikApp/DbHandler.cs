using Microsoft.Data.Sqlite;

namespace MusikApp;

/// <summary>
/// ALL databaskod bor här. Program.cs ska aldrig innehålla SQL eller SqliteConnection.
///
/// Det här är ett skelett: metoderna finns, kommentarerna visar vad varje metod ska göra,
/// men koden är din. Alla metoder kastar NotImplementedException tills du har skrivit dem.
///
/// ADO.NET i fem steg (samma i varje metod som pratar med databasen):
///   1. Öppna en anslutning        -> new SqliteConnection(...) och connection.Open()
///   2. Skapa ett kommando         -> connection.CreateCommand()
///   3. Skriv SQL:en               -> command.CommandText = "..."
///   4. Skicka in värden säkert    -> command.Parameters.AddWithValue("@namn", värde)
///   5. Kör kommandot              -> ExecuteReader() för SELECT, ExecuteNonQuery() för INSERT/DELETE
///
/// Använd "using var" på anslutning och kommando så stängs de av sig själva.
/// Bygg SQL:en med parametrar (@titel), aldrig genom att klistra ihop strängar.
/// </summary>
public class DbHandler
{
    private readonly string _connectionString;

    public DbHandler(string connectionString)
    {
        _connectionString = connectionString;
    }

    // ---------------------------------------------------------------
    // G-nivå: du behöver minst tre av de fem här (välj själv vilka)
    // ---------------------------------------------------------------

    /// <summary>Hämtar alla låtar, sorterade på titel.</summary>
    public List<Song> GetAllSongs()
    {
        List<Song> songs = new List<Song>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id, title, duration_seconds, album_id FROM songs ORDER BY TITLE ASC";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Song song = new Song()
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                DurationSeconds = reader.GetInt32(2),
                AlbumId = reader.GetInt32(3)
            };
            songs.Add(song);
        }

        return songs;
    }


    /// <summary>Hämtar en specifik låt. Returnerar null om den inte finns.</summary>
    public Song? GetSongById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id, title, duration_seconds, album_id FROM songs WHERE id = @id";

        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Song()
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                DurationSeconds = reader.GetInt32(2),
                AlbumId = reader.GetInt32(3)
            };
        }

        return null;
    }

    /// <summary>Söker låtar där titeln innehåller söktexten.</summary>
    public List<Song> SearchSongs(string searchText)
    {

        List<Song> songs = new List<Song>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id, title, duration_seconds, album_id FROM songs WHERE title LIKE @sok";
        command.Parameters.AddWithValue("@sok", "%" + searchText + "%");

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Song song = new Song
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                DurationSeconds = reader.GetInt32(2),
                AlbumId = reader.GetInt32(3)
            };
            songs.Add(song);
        }

        return songs;
    }


    /// <summary>Lägger till en ny låt.</summary>
    public void AddSong(Song song)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO songs (title, duration_seconds, album_id) VALUES (@titel, @langd, @albumId)";

        command.Parameters.AddWithValue("@titel", song.Title);
        command.Parameters.AddWithValue("@langd", song.DurationSeconds);
        command.Parameters.AddWithValue("@albumId", song.AlbumId);

        command.ExecuteNonQuery();
    }

    /// <summary>Tar bort en låt. Returnerar true om en rad faktiskt togs bort.</summary>
    public bool DeleteSong(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM songs WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        int RowsAffected = command.ExecuteNonQuery();
        return RowsAffected > 0;
    }
}