namespace MusikApp;

// Din data-klass: ett Song-objekt = en rad i din låt-tabell.
//
// TODO: lägg till en egenskap per kolumn du vill visa i appen,
// t.ex. Id, Titel och Längd. Namn och typer ska passa din databas.
//
// Behöver du fler klasser (Album, Artist) för din app? Skapa dem i egna filer.
public class Song
{
    public int Id { get; set; }
    private string Title { get; set; }
    public int DurationSeconds { get; set; }
    public int AlbumId { get; set; }
}
