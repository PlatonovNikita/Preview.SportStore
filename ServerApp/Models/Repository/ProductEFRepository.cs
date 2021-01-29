using System;
using System.Collections.Generic;
using ServerApp.Models;
using ServerApp.Models.BindingTargets;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace ServerApp.Models
{
    public class ProductEfRepository : IProductRepository
    {
        private StoreContext context;

        public ProductEfRepository(StoreContext _context)
            => context = _context;
        
        public Product GetProduct(long id)
        {
            var product = context.Products
                .Include(p => p.Description)
                .Include(p => p.Category)
                .ThenInclude(c => c.GroupProperties)
                .ThenInclude(gp => gp.Properties)
                .Include(p => p.GroupsValues)
                .ThenInclude(gv => gv.DoubleProps)
                .Include(p => p.GroupsValues)
                .ThenInclude(gv => gv.StrProps)
                .Include(p => p.GroupsValues)
                .ThenInclude(gv => gv.BoolProps)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new ProductNotFound("This id is not valid");
            }
            if (product.Category != null)
            {
                product.Category.Products = null;
                product.Category.GroupProperties = null;
            }
            if (product.GroupsValues != null)
            {
                foreach (var groupValuese in product.GroupsValues)
                {
                    if (groupValuese.GroupProperty != null)
                    {
                        groupValuese.GroupProperty.Properties = null;
                    }
                }
            }

            return product;
        }
        
        public long AddProduct(Product product)
        {
            if (context.Categories
                .Any(c => c.Id == product.CategoryId))
            {
                var groupProperties = context.Set<GroupProperty>()
                    .Where(gp => gp.CategoryId == product.CategoryId);
                var groupVal = new List<GroupValues>();
                foreach (var gp in groupProperties)
                {
                    groupVal.Add(new GroupValues {GroupPropertyId = gp.Id});
                }
                
                product.GroupsValues = groupVal;
                context.Add(product);
                context.SaveChanges();
                return product.Id;
            }
            throw new RelatedCategoryNotFound("Inner categoryId not valid");
        }
        
        public void ReplaceProduct(long id, Product product)
        {
            var prod = context.Products.FirstOrDefault(p => p.Id == id);
            if (prod == null) throw new ProductNotFound("This id is not valid");
            
            prod.Name = product.Name;
            prod.Price = product.Price;
            if(product.InStock != null) prod.InStock = product.InStock;
            if (product.Description != null) prod.Description = product.Description;
            context.SaveChanges();
        }

        public void ReplaceDescription(long productId, string value)
        {
            if (!context.Products.Any(p => p.Id == productId))
            {
                throw new ProductNotFound("Inner product id is not valid");
            }
            
            Description description = context.Set<Description>().FirstOrDefault(d => d.ProductId == productId) 
                                      ?? throw new DescriptionNotFound("Description did not found");

            description.Value = value;
            context.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            context.Products.Remove(new Product {Id = id});
            context.SaveChanges();
        }
        
        public void DeleteProductsByCategory(long categoryId)
        {
            var products = context.Products
                .Where(p => p.CategoryId == categoryId);

            if (products != null)
            {
                context.Products.RemoveRange(products);
                context.SaveChanges();
            }
        }

        public void DeleteGroup(long groupId)
        {
            var group = context.Set<GroupValues>()
                .Where(gv => gv.GroupPropertyId == groupId);
            context.RemoveRange(group);
            context.SaveChanges();
        }

        public IEnumerable<Product> GetFilteredProducts(int? pageSize = null, int? pageNumber = null,
            string search = null, long? categoryId = null, bool? inStock = null, decimal? minPrice = null,
            decimal? maxPrice = null, SearchLines searchByProperty = null)
        {
            IQueryable<Product> query = GetQuery(search, 
                categoryId, inStock, minPrice, maxPrice);

            if (searchByProperty != null)
            {
                var doubleSearches = searchByProperty.DSearch;
                if (doubleSearches != null)
                {
                    foreach (var dSearch in doubleSearches)
                    {
                        query = query.Where(p => p
                            .GroupsValues
                            .Any(gv => gv
                                .DoubleProps
                                .Any(i => i.PropertyId == dSearch.PropertyId
                                          && (dSearch.Min == null || i.Value >= dSearch.Min)
                                          && (dSearch.Max == null || i.Value <= dSearch.Max)
                                )
                            )
                        );
                    }
                }

                var boolSearches = searchByProperty.BSearch;
                if (boolSearches != null)
                {
                    foreach (var bLine in boolSearches)
                    {
                        query = query.Where(p => p
                            .GroupsValues
                            .Any(gv => gv
                                .BoolProps
                                .Any(b => b.PropertyId == bLine.PropertyId
                                          && (b.Value == bLine.Value)
                                )
                            )
                        );
                    }
                }

                var strSearches = searchByProperty.StrSearch;
                if (strSearches != null)
                {
                    foreach (var strLine in strSearches)
                    {
                        query = query.Where(p => p
                            .GroupsValues
                            .Any(gv => gv
                                .StrProps
                                .Any(s => s.PropertyId == strLine.PropertyId
                                          && strLine.Strings.Contains(s.Value))));
                    }
                }
            }

            pageSize ??= 4;
            pageNumber ??= 1;

            return query.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize);
        }

        private IQueryable<Product> GetQuery(string search = null, 
            long? categoryId = null, bool? inStock = null,
            decimal? minPrice = null, decimal? maxPrice = null)
        {
            IQueryable<Product> query = context.Products;
            if (inStock != null)
            {
                query = query.Where(p => p.InStock == inStock);
            }

            if (minPrice != null)
            {
                query = query.Where(p => p.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }

            if (categoryId != null)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(lowerSearch));
                if (categoryId == null)
                {
                    query = query.Where(p => p.Category.Name.ToLower().Contains(lowerSearch));
                }
            }

            return query;
        }
        
        public long CreatePropertyDouble(DoubleLineData doubleLineData)
        {
            var groupValuesId = context.Set<GroupValues>()
                .First(gv => gv.GroupPropertyId == doubleLineData.GroupPropertyId
                             && gv.ProductId == doubleLineData.ProductId)
                .Id;
            var doubleLine = doubleLineData.DoubleLine;
            doubleLine.GroupValuesId = groupValuesId;
            context.Add(doubleLine);
            context.SaveChanges();
            return doubleLine.Id;
        }

        public void ReplacePropertyDouble(long id, double value)
        {
            var intProp = context.Set<DoubleLine>()
                .FirstOrDefault(i => i.Id == id);
            if (intProp == null) throw new RelatedPropertyNotFound("Id is not valid");
                
            intProp.Value = value;
            context.Update(intProp);
            context.SaveChanges();
        }

        public long CreatePropertyBool(BoolLineData boolLineData)
        {
            var groupValuesId = context.Set<GroupValues>()
                .First(gv => gv.GroupPropertyId == boolLineData.GroupPropertyId 
                             && gv.ProductId == boolLineData.ProductId)
                .Id;
            var boolProp = boolLineData.BoolLine;
            boolProp.GroupValuesId = groupValuesId;
            context.Add(boolProp);
            context.SaveChanges();
            return boolProp.Id;
        }

        public void ReplacePropertyBool(long id, bool value)
        {
            var boolProp = context.Set<BoolLine>()
                .FirstOrDefault(i => i.Id == id);
            if (boolProp == null) throw new RelatedPropertyNotFound("Id is not valid");
                
            boolProp.Value = value;
            context.Update(boolProp);
            context.SaveChanges();
        }

        public long CreatePropertyStr(StrLineData strLineData)
        {
            var groupValuesId = context.Set<GroupValues>()
                .First(gv => gv.GroupPropertyId == strLineData.GroupPropertyId 
                             && gv.ProductId == strLineData.ProductId)
                .Id;
            var strProp = strLineData.StrLine;
            strProp.GroupValuesId = groupValuesId;
            context.Add(strProp);
            context.SaveChanges();
            if (strProp.PropertyId != null)
            {
                AddUniueString(strProp.Value, (long) strProp.PropertyId);
            }
            return strProp.Id;
        }

        void AddUniueString(string value, long propertyId){
            if (!context.Set<UniqueString>().Any(s => s.Value == value && s.PropertyId == propertyId))
            {
                context.Set<UniqueString>().Add(new UniqueString {Value = value, PropertyId = propertyId});
                context.SaveChanges();
            }
        }

        public void ReplacePropertyStr(long id, string value)
        {
            var strProp = context.Set<StrLine>()
                .FirstOrDefault(i => i.Id == id);
            if (strProp == null) throw new RelatedPropertyNotFound("Id is not valid");

            string oldS = strProp.Value;
            strProp.Value = value;
            context.SaveChanges();
            if (strProp.PropertyId != null) 
                ReplaceUniueString((long) strProp.PropertyId, oldS, strProp.Value);
        }

        void ReplaceUniueString(long propertyId, string oldS, string newS)
        {
            if (!context.Set<StrLine>().Any(pr => pr.PropertyId == propertyId && pr.Value == oldS))
            {
                var uniqueString = context.Set<UniqueString>().FirstOrDefault(s => s.PropertyId == propertyId && s.Value == oldS);
                if (uniqueString != null) uniqueString.Value = newS;
                context.SaveChanges();
            }
            else
            {
                AddUniueString(newS, propertyId);
            }
        }

        public void DeleteProperty(long propertyId)
        {
            var dl = context.Set<DoubleLine>().
                Where(d => d.PropertyId == propertyId);
            var bl = context.Set<BoolLine>()
                .Where(b => b.PropertyId == propertyId);
            var sl = context.Set<StrLine>()
                .Where(s => s.PropertyId == propertyId);
            
            context.RemoveRange(bl);
            context.RemoveRange(dl);
            context.RemoveRange(sl);
            context.SaveChanges();
        }
        
        public void CheckValidProperty(BaseLineData data)
        {
            if (!context.Set<GroupProperty>()
                .Any(gv => gv.Id == data.GroupPropertyId))
            {
                throw new RelatedGroupNotFound("Group id is not valid");
            }

            if (!context.Products.Any(p => p.Id == data.ProductId))
            {
                throw new ProductNotFound("Product id is not valid");
            }
                
            if (!context.Set<Property>()
                .Any(p => p.Id == data.PropertyId))
            {
                throw new RelatedPropertyNotFound("Property id is not valid");
            }

            var group = context.Set<GroupValues>()
                .FirstOrDefault(gv => gv.GroupPropertyId == data.GroupPropertyId 
                                      && gv.ProductId == data.ProductId);

            if (context.Set<StrLine>().Any(s => s.PropertyId == data.PropertyId 
                                                && s.GroupValuesId == group.Id)
                || context.Set<DoubleLine>().Any(d => d.PropertyId == data.PropertyId 
                                                      && d.GroupValuesId == group.Id)
                || context.Set<BoolLine>().Any(b => b.PropertyId == data.PropertyId 
                                                    && b.GroupValuesId == group.Id))
            {
                throw new PropertyAlreadyExists("This property already exists");
            }
        }
    }
}