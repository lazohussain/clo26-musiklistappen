CREATE TABLE IF NOT EXISTS artists (
                                       id   INTEGER PRIMARY KEY AUTOINCREMENT,
                                       name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS albums (
                                      id           INTEGER PRIMARY KEY,
                                      title        TEXT NOT NULL,
                                      release_year INTEGER,
                                      artist_id    INTEGER
);

CREATE TABLE IF NOT EXISTS songs (
                                     id               INTEGER PRIMARY KEY AUTOINCREMENT,
                                     title            TEXT NOT NULL,
                                     duration_seconds INTEGER,
                                     album_id         INTEGER
);

INSERT INTO artists (id, name) VALUES (1, 'Michael Jackson');

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