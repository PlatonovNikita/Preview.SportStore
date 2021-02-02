using System;
using System.Collections.Generic;

namespace ServerApp.Models
{
    public interface ICategoryRepository
    {
        Category GetCategory(long id);

        Category GetCategoryByNickName(string nickName);
        
        long AddCategory(Category category);
        
        void DeleteCategory(long id);
        
        IEnumerable<Category> GetFilteredCategories(string search = null);

        void UpdateCategory(long id, Category category);

        long AddPropertyGroup(GroupProperty groupProperty);

        void UpdateGroup(long id, GroupProperty groupProperty);

        void DeleteGroup(long groupId);

        long AddProperty(Property property);

        void UpdateProperty(long id, Property property);

        void DeleteProperty(long propertyId);

        List<UniqueString> GetUniqueStrings(long propertyId);
    }

    public class CategoryNotFound : ArgumentException
    {
        public  CategoryNotFound(string message)
            : base(message) { }
    }

    public class GroupCategoryNotFound : ArgumentException
    {
        public GroupCategoryNotFound(string message)
            : base(message) { }
    }

    public class PropertyNotFound : ArgumentException
    {
        public PropertyNotFound(string message) 
            : base(message) { }
    }

    public class UnacceptableNameGroup : ArgumentException
    {
        public UnacceptableNameGroup(string message)
            : base(message) { }
    }

    public class ExcessGlobalGroupProperties : ArgumentException
    {
        public ExcessGlobalGroupProperties(string message)
            : base(message) { }
    }
}