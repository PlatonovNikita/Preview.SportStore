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
            
            
            
            long c2Id = categoryEfRepository.AddCategory(new Category {Name = "Велотренажёр", NikName = "ExerciseBike"});
            
            long gp2Id = categoryEfRepository.AddPropertyGroup(new GroupProperty {Name = "Спицификации", CategoryId = c2Id});

            long prop21Id = categoryEfRepository.AddProperty(new Property
                {Name = "Вес", PropType = PropertyType.Double, GroupPropertyId = gp2Id});

            long prop22Id = categoryEfRepository.AddProperty(new Property
                {Name = "Особенности", PropType = PropertyType.Str, GroupPropertyId = gp2Id});

            long prop23Id = categoryEfRepository.AddProperty(new Property
                {Name = "Мультифункциональный?", PropType = PropertyType.Bool, GroupPropertyId = gp2Id});

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
        }
    }
}