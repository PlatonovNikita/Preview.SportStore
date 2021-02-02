using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using ServerApp.Models.BindingTargets;

namespace ServerApp.Models
{
    public class SeedData
    {
        public static void EnsureCreated(IProductRepository productEfRepository, 
            ICategoryRepository categoryEfRepository)
        {
            if (categoryEfRepository.GetFilteredCategories().Any()) return;

            long c1Id = categoryEfRepository.AddCategory(new Category {Name = "Беговые дорожки", NikName = "treadmill"});

            long globalGp1Id = categoryEfRepository.GetCategory(c1Id).GroupProperties.First(gp => gp.Name == "_global").Id;
            
            long prop13Id = categoryEfRepository.AddProperty(new Property {Name = "Максимальная скорость", PropType = PropertyType.Double, GroupPropertyId = globalGp1Id});

            long prop14Id = categoryEfRepository.AddProperty(new Property {Name = "Вес", PropType = PropertyType.Double, GroupPropertyId = globalGp1Id});

            long pg1Id = categoryEfRepository.AddPropertyGroup(new GroupProperty {Name = "Спицификации", CategoryId = c1Id});

            long prop11Id = categoryEfRepository.AddProperty(new Property {Name = "Максимальная скорость", PropType = PropertyType.Double, GroupPropertyId = pg1Id});

            long prop12Id = categoryEfRepository.AddProperty(new Property {Name = "Вес", PropType = PropertyType.Double, GroupPropertyId = pg1Id});

            long p1Id = productEfRepository.AddProduct(new Product
            {
                Name = "Ультра беговая дорожка",
                CategoryId = c1Id,
                Price = 89.99m,
                Description = new Description("Это ультра беговая дорожка")
            });

            productEfRepository.CreatePropertyDouble(new DoubleLineData
                {Value = 15, ProductId = p1Id, PropertyId = prop11Id, GroupPropertyId = pg1Id});

            productEfRepository.CreatePropertyDouble(new DoubleLineData
                {Value = 40, ProductId = p1Id, PropertyId = prop12Id, GroupPropertyId = pg1Id});
            
            productEfRepository.CreatePropertyDouble(new DoubleLineData
                {Value = 15, ProductId = p1Id, PropertyId = prop13Id, GroupPropertyId = globalGp1Id});

            productEfRepository.CreatePropertyDouble(new DoubleLineData
                {Value = 40, ProductId = p1Id, PropertyId = prop14Id, GroupPropertyId = globalGp1Id});

            long p2Id = productEfRepository.AddProduct(new Product
            {
                Name = "Супер беговая дорожка",
                CategoryId = c1Id,
                Price = 189.99m,
                Description = new Description("Это супер беговая дорожка")
            });

            productEfRepository.CreatePropertyDouble(new DoubleLineData
                {Value = 30, ProductId = p2Id, PropertyId = prop11Id, GroupPropertyId = pg1Id});

            productEfRepository.CreatePropertyDouble(new DoubleLineData
                {Value = 20, ProductId = p2Id, PropertyId = prop12Id, GroupPropertyId = pg1Id});
            
            productEfRepository.CreatePropertyDouble(new DoubleLineData
                {Value = 30, ProductId = p2Id, PropertyId = prop13Id, GroupPropertyId = globalGp1Id});

            productEfRepository.CreatePropertyDouble(new DoubleLineData
                {Value = 20, ProductId = p2Id, PropertyId = prop14Id, GroupPropertyId = globalGp1Id});
            
            
            
            long c2Id = categoryEfRepository.AddCategory(new Category {Name = "Велотренажёр", NikName = "ExerciseBike"});

            long globalPg2Id = categoryEfRepository.GetCategory(c2Id).GroupProperties.First(gp => gp.Name == "_global").Id;
            
            long prop24Id = categoryEfRepository.AddProperty(new Property
                {Name = "Вес", PropType = PropertyType.Double, GroupPropertyId = globalPg2Id});

            long prop25Id = categoryEfRepository.AddProperty(new Property
                {Name = "Особенности", PropType = PropertyType.Str, GroupPropertyId = globalPg2Id});

            long prop26Id = categoryEfRepository.AddProperty(new Property
                {Name = "Мультифункциональный?", PropType = PropertyType.Bool, GroupPropertyId = globalPg2Id});

            long gp2Id = categoryEfRepository.AddPropertyGroup(new GroupProperty {Name = "Спицификации", CategoryId = c2Id});

            long prop21Id = categoryEfRepository.AddProperty(new Property
                {Name = "Вес", PropType = PropertyType.Double, GroupPropertyId = gp2Id});

            long prop22Id = categoryEfRepository.AddProperty(new Property
                {Name = "Особенности", PropType = PropertyType.Str, GroupPropertyId = gp2Id});

            long prop23Id = categoryEfRepository.AddProperty(new Property
                {Name = "Мультифункциональный?", PropType = PropertyType.Bool, GroupPropertyId = gp2Id});

            {
                long p3Id = productEfRepository.AddProduct(new Product
                {
                    Name = "Супер велотренажёр",
                    Description = new Description("Это cупер велотренажёр"),
                    Price = 99.99m,
                    CategoryId = c2Id
                });

                productEfRepository.CreatePropertyDouble(new DoubleLineData
                    {Value = 20, ProductId = p3Id, GroupPropertyId = gp2Id, PropertyId = prop21Id});

                productEfRepository.CreatePropertyStr(new StrLineData
                    {Value = "Складной", ProductId = p3Id, GroupPropertyId = gp2Id, PropertyId = prop22Id});

                productEfRepository.CreatePropertyBool(new BoolLineData
                    {Value = true, ProductId = p3Id, GroupPropertyId = gp2Id, PropertyId = prop23Id});
                
                productEfRepository.CreatePropertyDouble(new DoubleLineData
                    {Value = 20, ProductId = p3Id, GroupPropertyId = globalPg2Id, PropertyId = prop24Id});

                productEfRepository.CreatePropertyStr(new StrLineData
                    {Value = "Складной", ProductId = p3Id, GroupPropertyId = globalPg2Id, PropertyId = prop25Id});

                productEfRepository.CreatePropertyBool(new BoolLineData
                    {Value = true, ProductId = p3Id, GroupPropertyId = globalPg2Id, PropertyId = prop26Id});
            }

            {
                long p4Id = productEfRepository.AddProduct(new Product
                {
                    Name = "Ультра велотренажёр",
                    Description = new Description("Это ультра велотренажёр"),
                    Price = 49.99m,
                    CategoryId = c2Id
                });

                productEfRepository.CreatePropertyDouble(new DoubleLineData
                    {Value = 27, ProductId = p4Id, GroupPropertyId = gp2Id, PropertyId = prop21Id});

                productEfRepository.CreatePropertyStr(new StrLineData
                    {Value = "Раскладной", ProductId = p4Id, GroupPropertyId = gp2Id, PropertyId = prop22Id});

                productEfRepository.CreatePropertyBool(new BoolLineData
                    {Value = false, ProductId = p4Id, GroupPropertyId = gp2Id, PropertyId = prop23Id});
                
                productEfRepository.CreatePropertyDouble(new DoubleLineData
                    {Value = 27, ProductId = p4Id, GroupPropertyId = globalPg2Id, PropertyId = prop24Id});

                productEfRepository.CreatePropertyStr(new StrLineData
                    {Value = "Раскладной", ProductId = p4Id, GroupPropertyId = globalPg2Id, PropertyId = prop25Id});

                productEfRepository.CreatePropertyBool(new BoolLineData
                    {Value = false, ProductId = p4Id, GroupPropertyId = globalPg2Id, PropertyId = prop26Id});
            }

            for (int i = 0; i < 98; i++)
            {
                long newProductId = productEfRepository.AddProduct(new Product
                {
                    Name = $"Велотренажёр {i}",
                    Description = new Description("Это велотренажёр"),
                    Price = 0.99m * i,
                    CategoryId = c2Id
                });

                productEfRepository.CreatePropertyDouble(new DoubleLineData
                    {Value = 27, ProductId = newProductId, GroupPropertyId = gp2Id, PropertyId = prop21Id});

                productEfRepository.CreatePropertyStr(new StrLineData
                    {Value = i % 2 == 0 ? "Раскладной" : "Складной", ProductId = newProductId, GroupPropertyId = gp2Id, PropertyId = prop22Id});

                productEfRepository.CreatePropertyBool(new BoolLineData
                    {Value = i % 2 == 1, ProductId = newProductId, GroupPropertyId = gp2Id, PropertyId = prop23Id});
                
                productEfRepository.CreatePropertyDouble(new DoubleLineData
                    {Value = 27, ProductId = newProductId, GroupPropertyId = globalPg2Id, PropertyId = prop24Id});

                productEfRepository.CreatePropertyStr(new StrLineData
                    {Value = i % 2 == 0 ? "Раскладной" : "Складной", ProductId = newProductId, GroupPropertyId = globalPg2Id, PropertyId = prop25Id});

                productEfRepository.CreatePropertyBool(new BoolLineData
                    {Value = i % 2 == 1, ProductId = newProductId, GroupPropertyId = globalPg2Id, PropertyId = prop26Id});
            }
        }
    }
}