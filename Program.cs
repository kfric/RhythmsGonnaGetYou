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
        public DateTime ReleaseDate { get; set; } // was "date" in SQL...
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
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("--- The Evergrowing Band Database! ---");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("");
        }
        // create method to prompt for string response
        static string PromptForString(string prompt)
        {
            // write line
            Console.WriteLine(prompt);
            // read line and set it as UserInput
            var userInput = Console.ReadLine();
            // return response
            return userInput;
        }
        // create method to prompt for int response
        static int PromptForInterger(string prompt)
        {
            // write line
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


            var albumCount = context.Albums.Count();
            Console.WriteLine($"There are {albumCount} albums!");

            foreach (var album in context.Albums)
            {
                Console.WriteLine($"There is an album named {album.Title}");
            }

            var albumsWithBandsAndSongs = context.Albums.
                                                        Include(album => album.Band).
                                                        ThenInclude(song => song.Album);

            // const albumsWithBands = context.Albums.Include(album => album.Band);
            foreach (var album in albumsWithBandsAndSongs)
            {
                if (album.Band == null)
                {
                    Console.WriteLine($"There is an album named {album.Title} by {album.Band.Name}");
                }
                foreach (var song in album.Songs)
                {
                    Console.WriteLine($"{song.Title} is played by {song.Band.Name}");
                }
            }
        }
    }
}

