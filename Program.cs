using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    // Define a database context for our Rhythm database.
    // It derives from (has a parent of) DbContext so we get all the
    // abilities of a database context from EF Core.
    class RhythmsGonnaGetYouContext : DbContext
    {
        // Define a Songs property that is a DbSet.
        public DbSet<Song> Songs { get; set; }
        // Define a Albums property that is a DbSet.
        public DbSet<Album> Albums { get; set; }
        // Define a Bands property that is a DbSet.
        public DbSet<Band> Bands { get; set; }

        // Define a method required by EF that will configure our connection
        // to the database.
        //
        // DbContextOptionsBuilder is provided to us. We then tell that object
        // we want to connect to a postgres database named suncoast_movies on
        // our local machine.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=Rhythm");
        }
    }

    // create class band to mirror table Bands
    class Band
    {
        // assign a var to the each list of properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public string IsSigned { get; set; }
        public string ContactName { get; set; }
        public int ContactPhoneNumber { get; set; }
    }
    // create class Album to mirror table Albums
    class Album
    {
        // assign a var to the each list of properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string IsExplicit { get; set; }
        public string ReleaseDate { get; set; }
        public int BandId { get; set; }

        // realted list of Bands
        public Band Band { get; set; }

    }
    // create class Song to mirror table Songs
    class Song
    {
        // assign a var to the each list of properties
        public int Id { get; set; }
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public int AlbumId { get; set; }

        // related list of Albums
        public Album Album { get; set; }

    }
    class Program
    {
        static void DisplayWelcome()
        {
            // create greeting to show that the program is running
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("--- The Evergrowing database for Bands/Artists! ---");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("");
        }
        // create method to prompt for string response
        static string PromptForString(string prompt)
        {
            // writeline prompt
            Console.WriteLine(prompt);
            // read line and set it as UserInput
            var userInput = Console.ReadLine();
            // return response
            return userInput;
        }
        // create method to prompt for int response
        static int PromptForInterger(string prompt)
        {
            // writeline prompt
            Console.WriteLine(prompt);
            int userInput;
            // read line and set it as userInput
            var goodInput = Int32.TryParse(Console.ReadLine(), out userInput);
            if (goodInput)
            {
                // return response
                return userInput;
            }
            else
            {
                // if input not a number then default to 0
                Console.WriteLine("Invalid number default to 0");
                return 0;
            }
        }
        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();

            // display greeting
            DisplayWelcome();

            // create bool statement to determine if user want to contine with app
            var keepGoing = true;
            // if keepGoing = true
            while (keepGoing)
            {
                Console.WriteLine(new String('-', 66));
                // create main menu
                Console.WriteLine("Main Menu: [A]dd. [V]iew. [U]pdate. [S]earch. [Q]uit.");
                Console.WriteLine(new String('-', 66));
                // choice = user input
                var choice = Console.ReadLine().ToUpper();

                // start switch loop
                switch (choice)
                {
                    // if choice == Q
                    case "Q":
                        Console.WriteLine("Goodbye...");
                        // keepGoing = false. app ends
                        keepGoing = false;
                        break;

                    // if choice == A
                    case "A":
                        {
                            Console.WriteLine(new String('-', 28));
                            // add menu: add Band, Album, or Song
                            var answer = PromptForString("Add: [B]and. [A]lbum. [S]ong").ToUpper();
                            // if answer == B
                            if (answer == "B")
                            {
                                // assign newBand to a new Band 
                                var newBand = new Band();
                                // create string of questions to get band info. must match the band table/class
                                // name of band
                                newBand.Name = PromptForString("Band/Artist name?");
                                // country of origin
                                newBand.CountryOfOrigin = PromptForString("What country of origin?");
                                // number of members?
                                newBand.NumberOfMembers = PromptForInterger("Number of members?");
                                // band URL?
                                newBand.Website = PromptForString("Website URL?");
                                // genre?
                                newBand.Style = PromptForString("Genre?");
                                // T/F are they signed to a label?
                                newBand.IsSigned = PromptForString("[True] or [False]: Are they signed to a label?");
                                // name of point of contact?
                                newBand.ContactName = PromptForString("Point of contact name?");
                                // POC phone number?
                                newBand.ContactPhoneNumber = PromptForInterger("Point of contact phone number?");
                                // add and save band info
                                context.Bands.Add(newBand);
                                context.SaveChanges();
                            }
                            // if answer == A
                            else if (answer == "A")
                            {
                                // assign newAlbum to a new Album
                                var newAlbum = new Album();
                                // create string of question to get album info. must match the album table/class
                                // title of album
                                newAlbum.Title = PromptForString("Album title?");
                                // is it explicit?
                                newAlbum.IsExplicit = PromptForString("[True] or [False]: Is it explicit?");
                                // release date?
                                newAlbum.ReleaseDate = PromptForString("Released date? (MM-DD-YYYY)");
                                // prompt for band name and have the app assign the correct BandId
                                var bandName = PromptForString("What is the name of the band/artist?");
                                // foundBand = band.Name that is = bandId (user input)
                                var foundBand = context.Bands.FirstOrDefault(band => band.Name == bandName);
                                // if no band by that name found then null. writeline no band by that name
                                if (foundBand == null)
                                {
                                    Console.WriteLine("There is no band/artist by that name in the database");
                                }
                                // else if there is a band by that name we need to attach the band's Id to the album
                                else
                                {
                                    newAlbum.BandId = foundBand.Id;
                                }
                                context.Albums.Add(newAlbum);
                                context.SaveChanges();
                            }
                            // if answer == S
                            else if (answer == "S")
                            {
                                // assign newSong to a new Song
                                var newSong = new Song();
                                // create string of question to get song info. must match the song table/class
                                // track number?
                                newSong.TrackNumber = PromptForInterger("Track number?");
                                // title of song?
                                newSong.Title = PromptForString("Song title?");
                                // duration of song?
                                newSong.Duration = PromptForInterger("Duration of song?(seconds)");

                                // might be working but should add a view songs option to display all songs AND band***********

                                // prompt for album title and have the app assign the correct AlbumId
                                var albumTitle = PromptForString("What is the title of the album?");
                                // foundAlbum = album.Title that is = albumId (user input)
                                var foundAlbum = context.Albums.FirstOrDefault(album => album.Title == albumTitle);
                                // if no album by that title found then null. writeline no album by that title
                                if (foundAlbum == null)
                                {
                                    Console.WriteLine("There is no album by that title in the database");
                                }
                                // else if there is a album by that title we need to attach the album's Id to the song
                                else
                                {
                                    newSong.AlbumId = foundAlbum.Id;
                                }
                                // add and save song
                                context.Songs.Add(newSong);
                                context.SaveChanges();
                            }
                            break;
                        }
                    // if choice == V
                    case "V":
                        {
                            Console.WriteLine(new String('-', 37));
                            // View menu: view all [B]ands or view all [A]lbums
                            var answer = PromptForString("View all:[B]ands. [A]lbums. [S]ongs.").ToUpper();
                            {
                                // if answer == B
                                if (answer == "B")
                                {
                                    // display the count of bands in database
                                    // bandsCount = context.Bands.Count
                                    var bandsCount = context.Bands.Count();
                                    Console.WriteLine($"Number of Bands in database: {bandsCount}");
                                    Console.WriteLine(new String('-', 28));

                                    // bandList = context.Bands
                                    var bandList = context.Bands;
                                    // foreach band in bandList
                                    foreach (var band in bandList)
                                    {
                                        // writeline band.Name
                                        Console.WriteLine(band.Name);
                                    }
                                }
                                // if answer == A
                                else if (answer == "A")
                                {
                                    // display count of albums
                                    // albumsCount = context.Albums.Count
                                    var albumsCount = context.Albums.Count();
                                    Console.WriteLine($"Number of Albums in database: {albumsCount}");

                                    // display the albums.TItle order.by release date
                                    // releaseDate = albums and include the band associated with it
                                    // order by the album's release date
                                    var releaseDate = context.Albums.Include(album => album.Band).OrderBy(album => album.ReleaseDate);
                                    // foreach album in releaseDate
                                    foreach (var album in releaseDate)
                                    {
                                        // writeline album.Title by album.Band.Name was release on album.ReleaseDate
                                        Console.WriteLine($"The album {album.Title} by {album.Band.Name} was released on {album.ReleaseDate}");
                                    }
                                }
                                else if (answer == "S")
                                {
                                    // get the count of all songs in list
                                    var songsCount = context.Songs.Count();
                                    Console.WriteLine($"Number of Songs in database: {songsCount}");
                                    // include the album Id and the Band Id so that we can display the band name with the song
                                    // order by song title
                                    var songList = context.Songs.Include(song => song.Album).ThenInclude(album => album.Band).OrderBy(song => song.Title);
                                    foreach (var song in songList)
                                    {
                                        Console.WriteLine($"{song.Title} by {song.Album.Band.Name}");
                                    }
                                }
                            }
                        }
                        break;
                    // if choice == U
                    case "U":
                        {
                            // name = promptforstring what is the name of the band you want to update?
                            var name = PromptForString("What is the name of the band you want to update?");

                            // // if the {name} == to the name of a band in the database
                            var foundBand = context.Bands.FirstOrDefault(band => band.Name == name);
                            // if no band by that name then null
                            if (foundBand == null)
                            {
                                // writeline no band by that name in that database
                                Console.WriteLine("There is no band by that name in the database");
                            }
                            else
                            {
                                // create isSigned menu: do you want to [S]ign or [D]rop?
                                var isSignedOrNot = PromptForString($"Do you want to [S]ign or [D]rop {name}").ToUpper();
                                // if answer == D
                                if (isSignedOrNot == "D")
                                {
                                    // writeline you dropped {name}
                                    Console.WriteLine($"You dropped --{name}--");
                                    // change answer to false
                                    isSignedOrNot = "False";
                                    // make answer = band IsSigned
                                    foundBand.IsSigned = isSignedOrNot;
                                    // save changes
                                    context.SaveChanges();
                                }
                                // if answer == D
                                else if (isSignedOrNot == "D")
                                {
                                    // writeline you signed {name}
                                    Console.WriteLine($"You signed --{name}--");
                                    // change answer to true
                                    isSignedOrNot = "True";
                                    // make answer = band IsSigned
                                    foundBand.IsSigned = isSignedOrNot;
                                    // save changes
                                    context.SaveChanges();
                                }
                            }
                        }
                        break;
                    // if choice == S
                    case "S":
                        {
                            // name = promptforstring enter name of band
                            var name = PromptForString("Search database: Enter name of band");
                            // if the {name} == to the name of a band in the database
                            var foundBand = context.Bands.FirstOrDefault(band => band.Name == name);
                            // if no band by that name then null
                            if (foundBand == null)
                            {
                                // write line there is no one by that name
                                Console.WriteLine("There is no band/artist by that name");
                            }
                            else
                            {
                                // else writeline all album by {name}
                                Console.WriteLine($"All albums by {name}");

                                //  link albums to Include their assoc bands
                                // albums = albums and their band
                                var albums = context.Albums.Include(album => album.Band);

                                // filter out the albums title from combined list
                                var albumList = albums.Where(album => album.Band.Name == name);
                                // foreach album in albumList
                                foreach (var album in albumList)
                                {
                                    // writeline {album.Title}
                                    Console.WriteLine($"{album.Title}");
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
