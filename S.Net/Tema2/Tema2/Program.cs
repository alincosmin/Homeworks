using System;
using System.Linq;
using P10Proj;
using P11Proj;
using P12Proj;
using P3Proj;
using P6Proj;
using P9Proj;

namespace Tema2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("/// P3 Test");
            P3Test();

            Console.WriteLine("\n\n/// P6 Test");
            P6Test();

            Console.WriteLine("\n\n/// P9 Test");
            P9Test();

            Console.WriteLine("\n\n/// P10 Test");
            P10Test();

            Console.WriteLine("\n\n/// P11 Test");
            P11Test();

            Console.WriteLine("\n\n/// P12 Test");
            P12Test();
        }

        private static void P10Test()
        {
            using (var context = new P10Entities())
            {
                if (!context.Employees.Any())
                {
                    var fte = new FullTimeEmployee
                            {
                                FirstName = "Jane",
                                LastName = "Doe",
                                Salary = 71500M
                            };
                    context.Employees.Add(fte);
                    fte = new FullTimeEmployee
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        Salary = 62500M
                    };
                    context.Employees.Add(fte);
                    var hourly = new HourlyEmployee
                    {
                        FirstName = "Tom",
                        LastName = "Jones",
                        Wage = 8.75M
                    };
                    context.Employees.Add(hourly);
                    context.SaveChanges(); 
                }
            }

            using (var context = new P10Entities())
            {
                Console.WriteLine("--- All Employees ---");
                foreach (var emp in context.Employees)
                {
                    bool fullTime = emp is HourlyEmployee ? false : true;
                    Console.WriteLine("{0} {1} ({2})", emp.FirstName, emp.LastName,
                    fullTime ? "Full Time" : "Hourly");
                }
                Console.WriteLine("--- Full Time ---");
                foreach (var fte in context.Employees.OfType<FullTimeEmployee>())
                {
                    Console.WriteLine("{0} {1}", fte.FirstName, fte.LastName);
                }
                Console.WriteLine("--- Hourly ---");
                foreach (var hourly in context.Employees.OfType<HourlyEmployee>())
                {
                    Console.WriteLine("{0} {1}", hourly.FirstName, hourly.LastName);
                }
            }
        }

        private static void P11Test()
        {
            using (var context = new P11Entities())
            {
                if (!context.Locations.Any())
                {
                    var park = new Park
                            {
                                Name = "11th Street Park",
                                Address = "801 11th Street",
                                City = "Aledo",
                                State = "TX",
                                ZipCode = 76106
                            };
                    var loc = new Location
                    {
                        Address = "501 Main",
                        City = "Weatherford",
                        State = "TX",
                        ZipCode = 76201
                    };
                    park.Office = loc;
                    context.Locations.Add(park);
                    park = new Park
                    {
                        Name = "Overland Park",
                        Address = "101 High Drive",
                        City = "Springtown",
                        State = "TX",
                        ZipCode = 76081
                    };
                    loc = new Location
                    {
                        Address = "8705 Range Lane",
                        City = "Springtown",
                        State = "TX",
                        ZipCode = 76081
                    };
                    park.Office = loc;
                    context.Locations.Add(park);
                    context.SaveChanges(); 
                }
            }

            using (var context = new P11Entities())
            {
                context.Configuration.LazyLoadingEnabled = true;
                Console.WriteLine("-- All Locations -- ");
                foreach (var l in context.Locations)
                {
                    Console.WriteLine("{0}, {1}, {2} {3}", l.Address, l.City,
                    l.State, l.ZipCode);
                }
                Console.WriteLine("--- Parks ---");
                foreach (var p in context.Locations.OfType<Park>())
                {
                    Console.WriteLine("{0} is at {1} in {2}", p.Name, p.Address, p.City);
                    Console.WriteLine("\tOffice: {0}, {1}, {2} {3}", p.Office.Address,
                    p.Office.City, p.Office.State, p.Office.ZipCode);
                }
            }
        }

        private static void P12Test()
        {
            using (var context = new P12Entities())
            {
                if (!context.Agents.Any())
                {
                    var name1 = new Name {FirstName = "Robin", LastName = "Rosen"};
                    var name2 = new Name {FirstName = "Alex", LastName = "St. James"};
                    var address1 = new Address
                    {
                        AddressLine1 = "510 N. Grant",
                        AddressLine2 = "Apt. 8",
                        City = "Raytown",
                        State = "MO",
                        ZipCode = 64133
                    };
                    var address2 = new Address
                    {
                        AddressLine1 = "222 Baker St.",
                        AddressLine2 = "Apt.22B",
                        City = "Raytown",
                        State = "MO",
                        ZipCode = 64133
                    };
                    context.Agents.Add(new Agent {Name = name1, Address = address1});
                    context.Agents.Add(new Agent {Name = name2, Address = address2});
                    context.SaveChanges();
                }
            }

            using (var context = new P12Entities())
            {
                Console.WriteLine("Agents");
                foreach (var agent in context.Agents)
                {
                    Console.WriteLine("{0} {1}", agent.Name.FirstName, agent.Name.LastName);
                    Console.WriteLine("{0}", agent.Address.AddressLine1);
                    Console.WriteLine("{0}", agent.Address.AddressLine2);
                    Console.WriteLine("{0}, {1} {2}", agent.Address.City,
                        agent.Address.State, agent.Address.ZipCode);
                    Console.WriteLine();
                }
            }
        }

        private static void P9Test()
        {
            using (var context = new P9Proj.Entities())
            {
                context.Database.ExecuteSqlCommand(@"INSERT INTO P9.FilteredObject
(DeletedOn,Description) values ('2/10/2009','Nulled date')");

                var account = new FilteredObject { Description = "Description A" };
                context.FilteredObjects.Add(account);
                account = new FilteredObject { Description = "Description B" };
                context.FilteredObjects.Add(account);
                account = new FilteredObject { Description = "Description C" };
                context.FilteredObjects.Add(account);
                context.SaveChanges();
            }
            using (var context = new P9Proj.Entities())
            {
                Console.WriteLine("Objects:");
                foreach (var obj in context.FilteredObjects)
                {
                    Console.WriteLine("{0}) {1}", obj.Id, obj.Description);
                }
            }
        }

        private static void P3Test()
        {
            using (var context = new P3Entities())
            {
                if (!context.Artists.Any() && !context.Albums.Any())
                {
                    Console.WriteLine("Will add info");

                    // add an artist with two albums
                    var artist = new Artist {FirstName = "Alan", LastName = "Jackson"};
                    var album1 = new Album {Name = "Drive"};
                    var album2 = new Album {Name = "Live at Texas Stadium"};
                    artist.Albums.Add(album1);
                    artist.Albums.Add(album2);
                    context.Artists.Add(artist);
                    // add an album for two artists
                    var artist1 = new Artist {FirstName = "Tobby", LastName = "Keith"};
                    var artist2 = new Artist {FirstName = "Merle", LastName = "Haggard"};
                    var album = new Album {Name = "Honkytonk University"};
                    artist1.Albums.Add(album);
                    artist2.Albums.Add(album);
                    context.Albums.Add(album);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("DB already has info");
                }
            }

            using (var context = new P3Entities())
            {
                Console.WriteLine("Artists and their albums...");
                var artists = context.Artists;
                foreach (var artist in artists)
                {
                    Console.WriteLine("{0} {1}", artist.FirstName, artist.LastName);
                    foreach (var album in artist.Albums)
                    {
                        Console.WriteLine("\t{0}", album.Name);
                    }
                }
                Console.WriteLine("\nAlbums and their artists...");
                var albums = context.Albums;
                foreach (var album in albums)
                {
                    Console.WriteLine("{0}", album.Name);
                    foreach (var artist in album.Artists)
                    {
                        Console.WriteLine("\t{0} {1}", artist.FirstName, artist.LastName);
                    }
                }
            }
        }

        private static void P6Test()
        {
            using (var context = new P6Entities())
            {
                if (!context.Products.Any())
                {
                    Console.WriteLine("Will add info");

                    var product = new Product
                            {
                                SKU = 147,
                                Description = "Expandable Hydration Pack",
                                Price = 19.97M,
                                ImageURL = "/pack147.jpg"
                            };
                    context.Products.Add(product);
                    product = new Product
                    {
                        SKU = 178,
                        Description = "Rugged Ranger Duffel Bag",
                        Price = 39.97M,
                        ImageURL = "/pack178.jpg"
                    };
                    context.Products.Add(product);
                    product = new Product
                    {
                        SKU = 186,
                        Description = "Range Field Pack",
                        Price = 98.97M,
                        ImageURL = "/noimage.jp"
                    };
                    context.Products.Add(product);
                    product = new Product
                    {
                        SKU = 202,
                        Description = "Small Deployment Back Pack",
                        Price = 29.97M,
                        ImageURL = "/pack202.jpg"
                    };
                    context.Products.Add(product);
                    context.SaveChanges(); 
                }
                else
                {
                    Console.WriteLine("DB already has info");
                }
            }
            using (var context = new P6Entities())
            {
                foreach (var p in context.Products)
                {
                    Console.WriteLine("{0} {1} {2} {3}", p.SKU, p.Description,
                    p.Price.ToString("C"), p.ImageURL);
                }
            }
        }
    }
}
