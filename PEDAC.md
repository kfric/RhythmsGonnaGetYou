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

// Define a database context for our Rhythm database.
// It derives from (has a parent of) DbContext so we get all the
// abilities of a database context from EF Core.
// Define a Songs property that is a DbSet.
// Define a Albums property that is a DbSet.
// Define a Bands property that is a DbSet.
// Define a method required by EF that will configure our connection
// to the database.
// DbContextOptionsBuilder is provided to us. We then tell that object
// we want to connect to a postgres database named suncoast_movies on
// our local machine.
// create class band to mirror table Bands
// assign a var to the each list of properties
// create class Album to mirror table Albums
// assign a var to the each list of properties
// realted list of Bands
// create class Song to mirror table Songs
// assign a var to the each list of properties
// related list of Albums
// create greeting to show that the program is running
// create method to prompt for string response
// writeline prompt
// read line and set it as UserInput
// return response
// create method to prompt for int response
// writeline prompt
// read line and set it as userInput
// return response
// if input not a number then default to 0
// display greeting
// create bool statement to determine if user want to contine with app
// if keepGoing = true
// create main menu (add as you go...)
// choice = user input
// start switch loop
// if choice == Q
// keepGoing = false. app ends
// if choice == A
// add menu: add Band, Album, or Song
// if answer == B
// assign newBand to a new Band
// create string of questions to get band info. must match the band table/class
// name of band
// country of origin
// number of members?
// band URL?
// genre?
// T/F are they signed to a label?
// name of point of contact?
// POC phone number?
// add and save band info
// if answer == A
// assign newAlbum to a new Album
// create string of question to get album info. must match the album table/class
// title of album
// is it explicit?
// release date?
// what is the band ID? from table
// add and save album
// if answer == S
// assign newSong to a new Song
// create string of question to get song info. must match the song table/class
// track number?
// title of song?
// duration of song?
// album ID? from table
// add and save song
// if choice == V
// View menu: view all [B]ands or view all [A]lbums
// if answer == B
// display the count of bands in database
// bandsCount = context.Bands.Count
// bandList = context.Bands
// foreach band in bandList
// writeline band.Name
// if answer == A
// display count of albums
// albumsCount = context.Albums.Count
// display the albums.TItle order.by release date
// releaseDate = context.Albums.Include(album => album.Band).OrderBy(album => album.ReleaseDate)
// foreach album in releaseDate
// writeline album.Title by album.Band.Name was release on album.ReleaseDate
// if choice == U
// name = promptforstring what is the name of the band you want to update?
// // if the {name} == to the name of a band in the database
// if no band by that name then null
// writeline no band by that name in that database
// create isSigned menu: do you want to [S]ign or [D]rop?
// if answer == D
// writeline you dropped {name}
// change answer to false
// make answer = band IsSigned
// save changes
// if answer == D
// writeline you signed {name}
// change answer to true
// make answer = band IsSigned
// save changes
// if choice == S
// name = promptforstring enter name of band
// if the {name} == to the name of a band in the database
// if no band by that name then null
// write line there is no one by that name
// else writeline all album by {name}
// link albums to Include their assoc bands
// albums = albums and their band
// filter out the albums title from combined list
// foreach album in albumList
// writeline {album.Title}
