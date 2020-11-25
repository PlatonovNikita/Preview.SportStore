using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ServerApp.Models
{
    public class SeedData
    {
        public static void EnsureCreated(StoreContext context)
        {
            if (context.Products.Any()) return;
            var c1 = new Category {
                Name = "Treadmill",
                GroupProperties = new [] {
                    new GroupProperty {
                        Name = "specifications",
                        Properties = new [] {
                            new Property {
                                Name = "Max Speed",
                                PropType = PropertyType.Double
                            },
                            new Property {
                                Name = "Weight",
                                PropType = PropertyType.Double
                            }, 
                        }
                    } 
                }
            };
                
            var c2 = new Category {
                Name = "Exercise bike",
                GroupProperties = new [] {
                    new GroupProperty {
                        Name = "specifications",
                        Properties = new [] {
                            new Property {
                                Name = "Weight",
                                PropType = PropertyType.Double
                            },
                            new Property {
                                Name = "Features",
                                PropType = PropertyType.Str
                            }, 
                        }
                    }, 
                }
            };
                
            context.AddRange(c1, c2);
            context.SaveChanges();

            var groupC1 = c1.GroupProperties.First();
                
            var p1 = new Product {
                Name = "Ultra Treadmill",
                Description = "This is Ultra Treadmill",
                Price = 89.99m,
                CategoryId = c1.Id,
                GroupsValues = new [] {
                    new GroupValues {
                        GroupPropertyId = groupC1.Id,
                        DoubleProps = new [] {
                            new DoubleLine {
                                Value = 15,
                                PropertyId = groupC1.Properties.First(p => p.Name == "Max Speed").Id
                            }, 
                            new DoubleLine {
                                Value = 40,
                                PropertyId = groupC1.Properties.First(p => p.Name == "Weight").Id
                            }, 
                        }
                    }, 
                }
            };
                
            var p2 = new Product {
                Name = "Supper Treadmill",
                Description = "This is Supper Treadmill",
                Price = 189.99m,
                CategoryId = c1.Id,
                GroupsValues = new [] {
                    new GroupValues {
                        GroupPropertyId = groupC1.Id,
                        DoubleProps = new [] {
                            new DoubleLine {
                                Value = 30,
                                PropertyId = groupC1.Properties.First(p => p.Name == "Max Speed").Id
                            }, 
                            new DoubleLine {
                                Value = 20,
                                PropertyId = groupC1.Properties.First(p => p.Name == "Weight").Id
                            }, 
                        }
                    }, 
                }
            };

            var groupC2 = c2.GroupProperties.First();
                
            var p3 = new Product {
                Name = "Supper Exercise bike",
                Description = "This is Supper Exercise bike",
                Price = 99.99m,
                CategoryId = c2.Id,
                GroupsValues = new [] {
                    new GroupValues {
                        GroupPropertyId = groupC2.Id,
                        DoubleProps = new [] {
                            new DoubleLine {
                                Value = 20,
                                PropertyId = groupC2.Properties.First(p => p.Name == "Weight").Id
                            },
                        },
                        StrProps = new [] {
                            new StrLine {
                                Value = "Supper",
                                PropertyId = groupC2.Properties.First(p => p.Name == "Features").Id
                            }, 
                        }
                    }, 
                }
            };
                
            var p4 = new Product {
                Name = "Ultra Exercise bike",
                Description = "This is Ultra Exercise bike",
                Price = 49.99m,
                CategoryId = c2.Id,
                GroupsValues = new [] {
                    new GroupValues {
                        GroupPropertyId = groupC2.Id,
                        DoubleProps = new [] {
                            new DoubleLine {
                                Value = 27,
                                PropertyId = groupC2.Properties.First(p => p.Name == "Weight").Id
                            },
                        },
                        StrProps = new [] {
                            new StrLine {
                                Value = "Ultra",
                                PropertyId = groupC2.Properties.First(p => p.Name == "Features").Id
                            }, 
                        }
                    }, 
                }
            };
                
            context.AddRange(p1, p2, p3, p4);
            context.SaveChanges();
        }
    }
}