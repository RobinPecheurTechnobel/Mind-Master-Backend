using DAL.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MindMasterContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Thinkers.Any())
            {
                ThinkerEntity[] thinkers = new ThinkerEntity[]
                {
                    new ThinkerEntity
                    {
                        Email = null,
                        Pseudo = "Admin",
                        Login = "Admin",
                        Role = "Admin",
                        HashPassword = "$argon2id$v=19$m=16,t=2,p=1$VHcwQnJAMW5DM2xscw$EeOAw2dw8t6VpxLj+ciZP9bZcDkp5FpttLEHUmqG/7TtrirM2NsvWLoytunM7AK6Uoc9m/UWQW2nGU8aztqpTu9o4ZJ42dzM0AvkLQFQJGZFU4seLEHLKi0ii1ZxC2URuVNe+w"
                    },
                    new ThinkerEntity
                    {
                        Email = null,
                        Pseudo = "User",
                        Login = "User",
                        Role = "User",
                        HashPassword = "$argon2id$v=19$m=16,t=2,p=1$VHcwQnJAMW5DM2xscw$EeOAw2dw8t6VpxLj+ciZP9bZcDkp5FpttLEHUmqG/7TtrirM2NsvWLoytunM7AK6Uoc9m/UWQW2nGU8aztqpTu9o4ZJ42dzM0AvkLQFQJGZFU4seLEHLKi0ii1ZxC2URuVNe+w"
                    }
                };

                context.Thinkers.AddRange(thinkers);
                context.SaveChanges();
            }
            if (!context.Groups.Any())
            {
                GroupEntity[] groups = new GroupEntity[]
                {
                    new GroupEntity
                    {
                        Name = "Public",
                        Description = "Groupe Accessible à tous"
                    },
                    new GroupEntity
                    {
                        Name = "Espace de User",
                        Description= "Espace personnel de User"
                    },
                    new GroupEntity
                    {
                        Name = "Espace de Admin",
                        Description= "Espace personnel de Admin"
                    },

                    new GroupEntity
                    {
                        Name = "Groupe d'étude",
                        Description= "Groupe pour la session du blocus 2023"
                    }
                };
                context.Groups.AddRange(groups);
                context.SaveChanges();
            }
            if(!context.GroupThinkers.Any())
            {
                GroupThinkerEntity[] groupthinkers = new GroupThinkerEntity[]
                {
                    new GroupThinkerEntity
                    {
                        GroupId = 1,
                        ThinkerId = 1
                    },
                    new GroupThinkerEntity
                    {
                        GroupId = 1,
                        ThinkerId = 2
                    },
                    new GroupThinkerEntity
                    {
                        GroupId = 2,
                        ThinkerId = 2
                    },

                    new GroupThinkerEntity
                    {
                        GroupId = 3,
                        ThinkerId = 1
                    },
                    new GroupThinkerEntity
                    {
                        GroupId = 4,
                        ThinkerId = 2
                    }
                };
                context.GroupThinkers.AddRange(groupthinkers);
                context.SaveChanges();
            }
        }
    }
}
