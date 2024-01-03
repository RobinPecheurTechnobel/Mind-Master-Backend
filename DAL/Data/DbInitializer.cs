using DAL.Entities;
using DAL.Entities.Enums;
using DAL.Entities.Relations;
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

            if(!context.Thinkers.Any())
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
            if(!context.Groups.Any())
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
            if(!context.Ideas.Any())
            {
                IdeaEntity[] ideas = new IdeaEntity[]
                {
                    new IdeaEntity
                    {
                        format = FormatEntity.txt,
                        creationDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Source = null,
                        ThinkerId = 1,
                        Content = "Bienvenue à vous."
                    },
                    new IdeaEntity
                    {
                        format = FormatEntity.txt,
                        creationDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Source = null,
                        ThinkerId = 1,
                        Content = "Ici, vous pourrez écrire ce que vous souhaitez !"
                    },
                    new IdeaEntity
                    {
                        format = FormatEntity.txt,
                        creationDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Source = null,
                        ThinkerId = 2,
                        Content = "Un truc à moi ..."
                    },

                };
                context.Ideas.AddRange(ideas);
                context.SaveChanges();
            }
            if(!context.Concepts.Any())
            {
                ConceptEntity[] concepts = new ConceptEntity[]
                {
                        new ConceptEntity()
                        {
                            Title = "Message pour les nouveaux arrivants"
                        },
                        new ConceptEntity()
                        {
                            Title = "Document perso"
                        }
                };
                context.Concepts.AddRange(concepts);
                context.SaveChanges();
            }
            if(!context.Assemblies.Any())
            {
                AssemblyEntity[] assemblies = new AssemblyEntity[]
                {
                        new AssemblyEntity
                        {
                            Title = "Message pour les nouveaux arrivants"
                        }
                };
                context.Assemblies.AddRange(assemblies);
                context.SaveChanges();
            }
            if(!context.ConceptAssemblies.Any())
            {
                ConceptAssemblyEntity[] conceptAssemblies = new ConceptAssemblyEntity[]
                {
                        new ConceptAssemblyEntity
                        {
                            AssemblyId = 1,
                            ConceptId = 1,
                            Order = 1
                        }

                };
                context.ConceptAssemblies.AddRange(conceptAssemblies);
                context.SaveChanges();
            }
            if(!context.ConceptIdeas.Any())
            {
                ConceptIdeaEntity[] conceptIdeaEntities = new ConceptIdeaEntity[]
                {
                    new ConceptIdeaEntity
                    {
                        IdeaId = 1,
                        Order = 1,
                        ConceptId = 1
                    },
                    new ConceptIdeaEntity
                    {
                        IdeaId = 2,
                        Order = 2,
                        ConceptId = 1
                    },
                    new ConceptIdeaEntity
                    {
                        IdeaId = 3,
                        Order = 1,
                        ConceptId = 2
                    },
                };
                
                context.ConceptIdeas.AddRange(conceptIdeaEntities);
                context.SaveChanges();
            }
            if(!context.ConceptGroups.Any())
            {
                ConceptGroupEntity[] conceptGroupEntities = new ConceptGroupEntity[]
                {
                    new ConceptGroupEntity
                    {
                        ConceptId = 1,
                        GroupId = 1
                    },
                    new ConceptGroupEntity
                    {
                        ConceptId = 2,
                        GroupId = 2
                    }
                };

                context.ConceptGroups.AddRange(conceptGroupEntities);
                context.SaveChanges();
            }
            if(!context.Labels.Any())
            {
                LabelEntity[] labels = new LabelEntity[]{
                    new LabelEntity
                    {
                        Title = "Bienvenue"
                    },
                    new LabelEntity
                    {
                        Title = "Message"
                    }
                };
                context.Labels.AddRange(labels);
                context.SaveChanges();
            }
            if(!context.LabelConcepts.Any())
            {
                LabelConceptEntity[] labelConcepts = new LabelConceptEntity[]
                {
                    new LabelConceptEntity
                    {
                        ConceptId = 1,
                        LabelId = 1
                    },
                    new LabelConceptEntity
                    {
                        ConceptId = 1,
                        LabelId = 2
                    },
                    new LabelConceptEntity
                    {
                        ConceptId = 2,
                        LabelId = 2
                    }
                };

                context.LabelConcepts.AddRange(labelConcepts);
                context.SaveChanges();
            }
            if(!context.LabelAssemblies.Any())
            {
                LabelAssemblyEntity[] labelAssemblies = new LabelAssemblyEntity[]
                {
                    new LabelAssemblyEntity
                    {
                        AssemblyId = 1,
                        LabelId = 1
                    },
                    new LabelAssemblyEntity
                    {
                        AssemblyId = 1,
                        LabelId = 2
                    }
                };

                context.LabelAssemblies.AddRange(labelAssemblies);
                context.SaveChanges();
            }
            if(!context.GroupAssemblies.Any())
            {
                GroupAssemblyEntity[] groupAssemblies = new GroupAssemblyEntity[]
                {
                    new GroupAssemblyEntity
                    {
                        AssemblyId = 1,
                        GroupId = 1
                    }
                };

                context.GroupAssemblies.AddRange(groupAssemblies);
                context.SaveChanges();
            }
        }
    }
}
