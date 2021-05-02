-- Problem --
For this project, we will model and create a database. We are starting a record label company, and we a place to store our bands, albums, and eventually songs. You are creating a console app that stores our information in a database.

Create a menu system that shows the following options to the user until they choose to quit your program

Add a new band
View all the bands
Add an album for a band
Add a song to an album
Let a band go (update isSigned to false)
Resign a band (update isSigned to true)
Prompt for a band name and view all their albums
View all albums ordered by ReleaseDate
View all bands that are signed
View all bands that are not signed
Quit the program

-- Example --
user selects [A]dd...next...[B]and, [A]lbum, [S]ong... user wants to add new band. user must enter the band Name, NumberOfMembers, Website, Style, IsSigned, ContactName, ContactPhoneNumber.

user selects [V]iew options. user will select from view all [B]ands, [A]lbum ordered by release date, [S]igned, [N]ot signed.

user selects [L]et band go....user will type in the bands name to update isSigned to false.

user selects [R]esign band....user will type in the bands name to update isSigned to true.

user selects [S]earch for albums. user will type in the bands name to display all the albums from that band.

user selects [Q]uit. app will end.

-- Data --
nouns:
band
album
song

behaviors:
add
-bands
-songs
-albums
view
-bands
-songs
-albums
let go
-boolean
resign
-boolean
search
-return all the albums for the input band
quit
-end application

-- Algorithm --

create greeting to show that the program is running

create Menu:
[A]dd: add new band
add album for band
add song to an album

[V]iew: view all bands
view all albums (ordered by ReleaseDate)
view all bands that are signed
view all bands that are NOT signed

[U]pdate
IsSigned? (true or false)
[S]earch for a band
[Q]uit
