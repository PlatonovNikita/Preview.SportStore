using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ServerApp.Models
{
    public class CategoryEfRepository : ICategoryRepository
    {
        private StoreContext context;

        public CategoryEfRepository(StoreContext _context)
            => context = _context;
        
        public Category GetCategory(long id)
        {
            return context.Categories
                .Include(c => c.GroupProperties)
                .ThenInclude(gp => gp.Properties)
                .FirstOrDefault(c => c.Id == id) 
                   ?? throw new CategoryNotFound("This id is not valid");
        }

        public Category GetCategoryByNickName(string nickName)
        {
            return context.Categories
                .FirstOrDefault(cat => cat.NikName == nickName)
                    ?? throw new CategoryNotFound("Category with this nickname not found");
        }

        public long AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            context.Set<GroupProperty>().Add(new GroupProperty {Name = "_global", CategoryId = category.Id});
            context.SaveChanges();
            return category.Id;
        }

        public void DeleteCategory(long id)
        {
            context.Categories.Remove(new Category {Id = id});
            context.SaveChanges();
        }

        public IEnumerable<Category> GetFilteredCategories(string search = null)
        {
            IQueryable<Category> query = context.Categories
                .Include(c => c.GroupProperties)
                .ThenInclude(gp => gp.Properties);

            if (search != null)
            {
                var lowerSearch = search.ToLower();
                query = query
                    .Where(c => 
                        c.Name.ToLower().Contains(lowerSearch)
                        || c.GroupProperties.Any(gv =>
                            gv.Name.ToLower().Contains(lowerSearch)));
            }

            return query;

        }

        public void UpdateCategory(long id, Category category)
        {
            Category cat = context.Categories
                .FirstOrDefault(c => c.Id == id);
            if (cat == null) throw new CategoryNotFound("This id is not valid");

            cat.Name = category.Name;
            cat.NikName = category.NikName;
            context.SaveChanges();
        }

        public long AddPropertyGroup(GroupProperty groupProperty)
        {
            if (groupProperty.Name == "_global")
            {
                throw new UnacceptableNameGroup("This name is unacceptable");
            }
            if (context.Categories.Any(c => c.Id == groupProperty.CategoryId))
            {
                context.Add(groupProperty);
                IEnumerable<long> productsId = context.Products
                    .Where(p => p.CategoryId == groupProperty.CategoryId)
                    .Select(p => p.Id).ToArray();
                foreach (var id in productsId)
                {
                    context.Set<GroupValues>().Add(new GroupValues
                        {ProductId = id, GroupPropertyId = groupProperty.Id});
                }
                context.SaveChanges();
                return groupProperty.Id;
            }
            throw new CategoryNotFound("Inner category id is not valid");
        }

        public void UpdateGroup(long id, GroupProperty groupProperty)
        {
            GroupProperty groupProp = context.Set<GroupProperty>()
                .FirstOrDefault(gp => gp.Id == id);
            if (groupProp == null) throw new GroupCategoryNotFound("This id is nod valid");

            groupProp.Name = groupProperty.Name;
            context.SaveChanges();
        }

        public void DeleteGroup(long groupId)
        {
            context.Set<GroupProperty>()
                .Remove(new GroupProperty {Id = groupId});
            context.SaveChanges();
        }

        public long AddProperty(Property property)
        {
            var groupProperty = context.Set<GroupProperty>()
                .FirstOrDefault(gp => gp.Id == property.GroupPropertyId);
            if (groupProperty != null)
            {
                if (groupProperty.Name == "_global")
                {
                    if (context.Set<Property>()
                        .Count(prop => prop.GroupPropertyId == property.GroupPropertyId) >= 6)
                    {
                        throw new ExcessGlobalGroupProperties("Exceeded the number of global group properties!");
                    }
                }
                context.Add(property);
                context.SaveChanges();
                return property.Id;
            }
            throw new GroupCategoryNotFound("Inner group id is not valid");
        }

        public void UpdateProperty(long id, Property property)
        {
            Property prop = context.Set<Property>()
                .FirstOrDefault(p => p.Id == id);
            if (prop == null) throw new PropertyNotFound("This id is not valid");

            prop.Name = property.Name;
            prop.PropType = property.PropType;
            context.SaveChanges();
        }

        public void DeleteProperty(long propertyId)
        {
            context.Set<Property>()
                .Remove(new Property {Id = propertyId});
            context.SaveChanges();
        }

        public List<UniqueString> GetUniqueStrings(long propertyId)
        {
            IQueryable<UniqueString> uniqueStrings = context.Set<UniqueString>().Where(us => us.PropertyId == propertyId);
            if (uniqueStrings == null) throw new CategoryNotFound("Inner category id is not valid");
            return uniqueStrings.ToList();
        }
    }
}