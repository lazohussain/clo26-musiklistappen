CREATE TABLE "musik.db" (
                            "field1"	TEXT,
                            "field2"	TEXT,
                            "field3"	TEXT,
                            "field4"	TEXT,
                            "field5"	TEXT
)
INSERT INTO albums (id, title, release_year, artist_id)
VALUES
(1, 'Off the Wall', 1979, 1),
(2, 'Thriller', 1982, 1),
(3, 'Bad', 1987, 1),
(4, 'Dangerous', 1991, 1),
(5, 'HIStory', 1995, 1);

INSERT INTO songs (title, duration_seconds, album_id)
VALUES
('Dont Stop Til You Get Enough', 360, 1),
('Rock with You', 220, 1),
('Billie Jean', 294, 2),
('Beat It', 258, 2),
('Thriller', 357, 2),
('Bad', 247, 3),
('Man in the Mirror', 319, 3),
('Smooth Criminal', 258, 4),
('Black or White', 253, 4),
('You Are Not Alone', 353, 5);
SELECT songs.title
FROM songs
         JOIN albums ON songs.album_id = albums.id
WHERE albums.title = 'Thriller';
SELECT albums.title, COUNT(songs.id) AS antal_latar
FROM albums
         LEFT JOIN songs ON albums.id = songs.album_id
GROUP BY albums.id, albums.title;
public void Seed()
    {
        using var connection = new SqliteConnection(_connectionString);
connection.Open();

using var check = connection.CreateCommand();
check.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='songs'";
if ((long)check.ExecuteScalar()! > 0) return;

var statements = File.ReadAllText("musik.sql")
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

foreach (var statement in statements)
        {
            using var command = connection.CreateCommand();
command.CommandText = statement;
command.ExecuteNonQuery();
}
    }