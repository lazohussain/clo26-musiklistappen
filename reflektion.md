# Reflektion — Musiklistappen

**Namn:** [Ditt namn]
**Datum:** [Datum]

---

## Vad var svårast?
Det som var svårast var att fixa att musik.db kopplas till rider.  Jag fick över exempel musik.db som våran lärare skapade utifall man inte hade kvar sin diskografi. Jag hade kvar den och hade redan skrivit koden med kopplingar till diskografi vilket gör att jag inte behöver lärarens exempel, men hur mycket jag än försökte så kunde jag inte få in min egna diskografi / musik.db från sql browser. jag har filen i min mapp men får den inte i själva rider.

---

## Vad lärde du dig?
Jag lärde mig väldigt mycket av denna uppgiften. Det som var nytt var att man ska koppla c# och sql programmet så att dom samarbetar, men det var rätt så logiskt men ändå tufft att kombinera dessa två.


---

## Hur använde du ADO.NET i praktiken — vad kändes naturligt och vad var förvirrande?

Tänk på de fem stegen: öppna anslutning, skapa kommando, skriv SQL, skicka in värden, kör.

Det som kändes naturligt var att skriva SQL-frågor och använda dom för att hämta, lägga till och ta bort låtar. Det som var lite förvirrande var hur anslutningen till databasen fungerar och hur man skickar in värden med parametrar. De 5 stegen blev tydligare ju mer jag arbetade med appen.


---

## Varför ska man använda parameteriserade queries?

Vad kan hända om man klistrar ihop SQL med strängar i stället? (Tänk på Bobby Tables.)

Man ska använda parameteriserade queries för att skydda databasen mot SQL-injection. Om man klistrar ihop SQL med vanliga strängar kan någon skriva in skadlig sql-kod, till exempel Bobby Tables, som kan göra att data ändras eller raderas. Med parametrar behandlas användarens inmatning som ett värde och inte som sql-kod.

---


---

## Om du hade mer tid — vad hade du lagt till eller gjort annorlunda?

Jag hade fixat musik.db in i filen på rider DIREKT. Jag har i mer än två timmar försökt få över den till rider och lyckats med det men det var tydligen fel fil med samma namn, sen lyckas jag inte få in rätt fil, tycker det var svårast med hela uppgiften

---

## (VG) Varför delade du upp koden som du gjorde?

Resonera kring ditt designval: varför ligger databaskoden i `DbHandler` och inte i `Program.cs`? Vad hade blivit svårare om allt låg i samma fil?

[Skriv här]
