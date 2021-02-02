using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Query;
using ServerApp.Models.BindingTargets;

namespace ServerApp.Models
{
    public interface IProductRepository
    {
        long AddProduct(Product product);
        
        void DeleteProduct(long id);
        
        Product GetProduct(long id);

        int GetPagesCount(int? pageSize = null, string search = null,
            long? categoryId = null, bool? inStock = null,
            decimal? minPrice = null, decimal? maxPrice = null, SearchLines searchByProperty = null);
        
        IEnumerable<Product> GetFilteredProducts( 
            int? pageSize = null, int? pageNumber = null, string search = null, 
            long? categoryId = null, bool? inStock = null, 
            decimal? minPrice = null, decimal? maxPrice = null, SearchLines searchByProperty = null);

        void DeleteProductsByCategory(long categoryId);

        void DeleteGroup(long groupId);

        void ReplaceProduct(long id, Product product);

        void ReplaceDescription(long productId, string value);

        long CreatePropertyDouble(DoubleLineData doubleLineData);

        void ReplacePropertyDouble(long id, double value);
        
        long CreatePropertyBool(BoolLineData boolLineData);

        void ReplacePropertyBool(long id, bool value);
        
        long CreatePropertyStr(StrLineData strLineData);

        void ReplacePropertyStr(long id, string value);

        void DeleteProperty(long propertyId);

        void CheckValidProperty(BaseLineData data);
    }

    public class RelatedCategoryNotFound : ArgumentException
    {
        public RelatedCategoryNotFound(string message)
            : base(message) { }
    }

    public class ProductNotFound : ArgumentException
    {
        public ProductNotFound(string message)
            : base(message) { }
    }

    public class RelatedGroupNotFound : ArgumentException
    {
        public RelatedGroupNotFound(string message)
            : base(message) { }
    }

    public class RelatedPropertyNotFound : ArgumentException
    {
        public RelatedPropertyNotFound(string message)
            : base(message) { }
    }

    public class DescriptionNotFound : ArgumentException
    {
        public DescriptionNotFound(string message)
            : base(message) { }
    }

    public class PropertyAlreadyExists : ArgumentException
    {
        public PropertyAlreadyExists(string message)
            : base(message) { }
    }
}