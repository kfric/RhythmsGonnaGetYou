﻿using System;
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
        public string ReleaseDate { get; set; } // was "date" in SQL...
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

            // create bool statement to determine if user want to contine with app
            var keepGoing = true;
            // if keepGoing = true
            while (keepGoing)
            {
                // create main menu
                Console.WriteLine(new String('-', 66));
                Console.WriteLine("Main Menu: [A]dd. [V]iew. [U]pdate. [S]earch. [Q]uit. [T]est code.");
                Console.WriteLine(new String('-', 66));
                var choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "Q":
                        Console.WriteLine("Goodbye...");
                        keepGoing = false;
                        break;

                    case "A":
                        {
                            Console.WriteLine(new String('-', 28));
                            var answer = PromptForString("Add: [B]and. [A]lbum. [S]ong").ToUpper();

                            if (answer == "B")
                            {
                                var newBand = new Band();
                                newBand.Name = PromptForString("Band/Artist name?");
                                newBand.CountryOfOrigin = PromptForString("What country of origin?");
                                newBand.NumberOfMembers = PromptForInterger("Number of members?");
                                newBand.Website = PromptForString("Website URL?");
                                newBand.Style = PromptForString("Genre?");
                                newBand.IsSigned = PromptForString("[True] or [False]: Are they signed to a label?");
                                newBand.ContactName = PromptForString("Point of contact name?");
                                newBand.ContactPhoneNumber = PromptForInterger("Point of contact phone number?");

                                context.Bands.Add(newBand);
                                context.SaveChanges();
                            }
                            else if (answer == "A")
                            {
                                var newAlbum = new Album();
                                newAlbum.Title = PromptForString("Album title?");
                                newAlbum.IsExplicit = PromptForString("Is it explicit?");
                                newAlbum.ReleaseDate = PromptForString("Released date? (MM-DD-YYYY)");
                                newAlbum.BandId = PromptForInterger("What is the band ID?");

                                context.Albums.Add(newAlbum);
                                context.SaveChanges();
                            }
                            else if (answer == "S")
                            {
                                var newSong = new Song();
                                newSong.TrackNumber = PromptForInterger("Track number?");
                                newSong.Title = PromptForString("Song title??");
                                newSong.Duration = PromptForInterger("Duration of song?(seconds)");
                                newSong.AlbumId = PromptForInterger("What is the album ID");

                                context.Songs.Add(newSong);
                                context.SaveChanges();
                            }

                            break;
                        }
                    case "V":
                        {
                            Console.WriteLine(new String('-', 37));
                            var answer = PromptForString("View all [B]ands or view all [A]lbums").ToUpper();
                            {
                                // to view all bands
                                if (answer == "B")
                                {
                                    var bandsCount = context.Bands.Count();
                                    Console.WriteLine($"Number of Bands in database: {bandsCount}");
                                    Console.WriteLine(new String('-', 28));

                                    var bandList = context.Bands;
                                    foreach (var band in bandList)
                                    {
                                        Console.WriteLine(band.Name);
                                    }
                                }
                                else if (answer == "A")
                                {
                                    var albumsCount = context.Albums.Count();
                                    Console.WriteLine($"Number of Albums in database: {albumsCount}");

                                    var releaseDate = context.Albums.Include(album => album.Band).OrderBy(album => album.ReleaseDate);
                                    foreach (var album in releaseDate)
                                    {
                                        Console.WriteLine($"The album {album.Title} by {album.Band.Name} was released on {album.ReleaseDate}");
                                    }
                                }
                            }
                        }
                        break;

                    case "U":
                        {
                            var name = PromptForString("What is the name of the band you want to update?");

                            Band foundBand = context.Bands.FirstOrDefault(band => band.Name == name);
                            if (foundBand == null)
                            {
                                Console.WriteLine("There is no band by that name in the database");
                            }
                            else
                            {
                                var isSignedOrNot = PromptForString($"Do you want to [S]ign or [D]rop {name}").ToUpper();
                                if (isSignedOrNot == "D")
                                {
                                    Console.WriteLine($"You droped --{name}--");
                                    isSignedOrNot = "False";
                                    foundBand.IsSigned = isSignedOrNot;
                                    context.SaveChanges();
                                }
                                else if (isSignedOrNot == "D")
                                {
                                    Console.WriteLine($"You signed --{name}--");
                                    isSignedOrNot = "True";
                                    foundBand.IsSigned = isSignedOrNot;
                                    context.SaveChanges();
                                }
                            }
                        }
                        break;
                    case "S":
                        {
                            var name = PromptForString("Search database: Enter name of band");
                            Band foundBand = context.Bands.FirstOrDefault(band => band.Name == name);
                            if (foundBand == null)
                            {
                                Console.WriteLine("There is no band/artist by that name");
                            }
                            else
                            {
                                Console.WriteLine($"All albums by {name}");

                                var albums = context.Albums.Include(album => album.Band);

                                var albumList = albums.Where(album => album.Band.Name == name);
                                foreach (var album in albumList)
                                {
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
