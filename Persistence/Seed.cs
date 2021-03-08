using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext dataContext, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        UserName = "Norbert",
                        Email = "norbert@test.pl",
                    },

                    new AppUser
                    {
                        UserName = "Janek",
                        Email = "jan@test.pl",
                    },

                    new AppUser
                    {
                        UserName = "wojtek",
                        Email = "wojtek@test.pl",
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "!Password1");
                }

            }

            if (dataContext.Posts.Any()) return;

            var usersFromDb = dataContext.Users.ToList();

            var posts = new List<Post>
            {
                new Post
                {
                    Id = Guid.Parse("330ee2cd-f1dd-40b8-807d-08d8d725d360"),
                    Title = "Dog",
                    CreateDate = DateTime.Now.AddMonths(-2),
                    Description ="The domestic dog (Canis familiaris when considered a separate species or Canis lupus familiaris when considered a subspecies of the wolf) is a wolf-like canid that can be found distributed around the world." +
                    " The dog descended from an ancient, now-extinct wolf with the modern wolf being the dog's nearest living relative. The dog was the first species to be domesticated by hunter–gatherers more than 15,000 years ago, which predates agriculture." +
                    " Their long association with humans has led dogs to be uniquely attuned to human behavior and they can thrive on a starch-rich diet that would be inadequate for other canids." +
                    " The dog has been selectively bred over millennia for various behaviors, sensory capabilities and physical attributes." +
                    " Dogs vary widely in shape, size and color. They perform many roles for humans, such as hunting, herding, pulling loads, protection, assisting police and the military, companionship and, more recently, aiding disabled people and therapeutic roles. " +
                    "This influence on human society has given them the sobriquet of man's best friend.",
                    Category="Animals",
                    PostOwner = usersFromDb[0]
                },

                new Post
                {
                    Id = Guid.Parse("EC10C5B2-3CE3-4A9D-3248-08D8DE349543"),
                    Title = "Cat",
                    CreateDate = DateTime.Now.AddDays(-1),
                    Description = "The cat (Felis catus) is a domestic species of small carnivorous mammal." +
                    " It is the only domesticated species in the family Felidae and is often referred to as the domestic cat to distinguish it from the wild members of the family." +
                    " A cat can either be a house cat, a farm cat or a feral cat; the latter ranges freely and avoids human contact. Domestic cats are valued by humans for companionship and their ability to hunt rodents." +
                    " About 60 cat breeds are recognized by various cat registries.",
                    Category = "Animals",
                    PostOwner = usersFromDb[0]
                },

                new Post
                {
                    Title = "Mobile Phone",
                    CreateDate = DateTime.Now.AddDays(-7),
                    Description = "A mobile phone, cellular phone, cell phone, cellphone, handphone, or hand phone, sometimes shortened to simply mobile, cell or just phone, is a portable telephone that can make and receive calls over a radio frequency link while the user is moving within a telephone service area. The radio frequency link establishes a connection to the switching systems of a mobile phone operator, which provides access to the public switched telephone network (PSTN). Modern mobile telephone services use a cellular network architecture and, therefore, mobile telephones are called cellular telephones or cell phones in North America. In addition to telephony, digital mobile phones (2G) support a variety of other services, such as text messaging, MMS, email, Internet access, short-range wireless communications (infrared, Bluetooth), business applications, video games and digital photography. Mobile phones offering only those capabilities are known as feature phones; mobile phones which offer greatly advanced computing capabilities are referred to as smartphones.",
                    Category = "Technology",
                    PostOwner = usersFromDb[2]
                },

                new Post
                {
                    Title = "Computer",
                    CreateDate = DateTime.Now.AddDays(-20),
                    Description = "A computer is a machine that can be instructed to carry out sequences of arithmetic or logical operations automatically via computer programming. Modern computers have the ability to follow generalized sets of operations, called programs. These programs enable computers to perform an extremely wide range of tasks. A complete computer including the hardware, the operating system (main software), and peripheral equipment required and used for full operation can be referred to as a computer system. This term may as well be used for a group of computers that are connected and work together, in particular a computer network or computer cluster.",
                    Category = "Technology",
                    PostOwner = usersFromDb[1]
                },

                new Post
                {
                    Title = "Android OS",
                    CreateDate = DateTime.Now.AddDays(-18),
                    Description = "Android is a mobile operating system based on a modified version of the Linux kernel and other open source software, designed primarily for touchscreen mobile devices such as smartphones and tablets. Android is developed by a consortium of developers known as the Open Handset Alliance and commercially sponsored by Google. It was unveiled in November 2007, with the first commercial Android device launched in September 2008.",
                    Category = "Technology",
                    PostOwner = usersFromDb[2]
                },

                new Post
                {
                    Title = "Penguin",
                    CreateDate = DateTime.Now.AddDays(-33),
                    Description = "Penguins are a group of aquatic flightless birds. They live almost exclusively in the Southern Hemisphere, with only one species, the Galápagos penguin, found north of the Equator. Highly adapted for life in the water, penguins have countershaded dark and white plumage and flippers for swimming. Most penguins feed on krill, fish, squid and other forms of sea life which they catch while swimming underwater. They spend roughly half of their lives on land and the other half in the sea.",
                    Category = "Animals",
                    PostOwner = usersFromDb[0]
                },

                new Post
                {
                    Title = "Raven",
                    CreateDate = DateTime.Now.AddHours(-1),
                    Description = "A raven is one of several larger-bodied species of the genus Corvus. These species do not form a single taxonomic group within the genus. " +
                    "There is no consistent distinction between crows and ravens, and these appellations have been assigned to different species chiefly on the basis of their size, crows generally being smaller than ravens." +
                    "The largest raven species are the common raven and the thick-billed raven.",
                    Category = "Animals",
                    PostOwner = usersFromDb[2]
                },
            };

            await dataContext.Posts.AddRangeAsync(posts);

            var postLikers = new List<PostLiker>
            {
                new PostLiker
                {
                    PostId = posts[0].Id,
                    AppUserId = usersFromDb[0].Id
                },

                new PostLiker
                {
                    PostId = posts[0].Id,
                    AppUserId = usersFromDb[1].Id
                },

                new PostLiker
                {
                    PostId = posts[2].Id,
                    AppUserId = usersFromDb[1].Id
                },

                new PostLiker
                {
                    PostId = posts[2].Id,
                    AppUserId = usersFromDb[2].Id
                },

                new PostLiker
                {
                    PostId = posts[3].Id,
                    AppUserId = usersFromDb[0].Id
                },

                new PostLiker
                {
                    PostId = posts[5].Id,
                    AppUserId = usersFromDb[2].Id
                },

                new PostLiker
                {
                    PostId = posts[6].Id,
                    AppUserId = usersFromDb[1].Id
                },
            };

            await dataContext.PostLikers.AddRangeAsync(postLikers);
            await dataContext.SaveChangesAsync();
        }
    }
}