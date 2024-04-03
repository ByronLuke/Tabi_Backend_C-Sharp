using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Products;
using Sabio.Models.Requests.Products;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Products
{
    public class ProductsService : IProductsService              
    {
        IDataProvider _data = null;
        ILookUpService _lookUp = null;
       
        #region DataProvider & LookUp Service
        public ProductsService(IDataProvider data, ILookUpService lookUp)
        {
            _data = data;
            _lookUp = lookUp;

        }
        #endregion 

        #region Products_Delete_ById (Update isActive)
        public void Products_Delete_ById(int id)
        {
            string procName = "[dbo].[Products_Delete_ById]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@Id", id);
                }, returnParameters: null);
        }
        #endregion

        #region Products_Insert
        public int Products_Insert(ProductAddRequest request)
        {
            int id = 0;

            string procName = "[dbo].[Products_Insert]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                AddCommonParams(request, collection);
                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                collection.Add(idOut);

            }, returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@Id"].Value;
                int.TryParse(oId.ToString(), out id);
            });
            return id;
        }
        #endregion

        #region Products_Update 
        public void Products_Update(ProductUpdateRequest updateRequest)
        {
            string procName = "[dbo].[Products_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                AddCommonParams(updateRequest, collection);

                collection.AddWithValue("@Id", updateRequest.Id);

            }, returnParameters: null);
        }
        #endregion

        #region Products_Select_ById
        public Product Products_Select_ById(int id)
        {
            string procName = "[dbo].[Products_Select_ById]";

            Product aProduct = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection collection)
            {
                collection.AddWithValue("@Id", id);
            }, delegate (IDataReader reader, short set)
            {
                aProduct = MapProduct(reader);
            });
            return aProduct;
        }
        #endregion

        #region Products_SelectAll
        public Paged<Product> Products_SelectAll(int pageIndex, int pageSize)
        {
            Paged<Product> pagedList = null;
            List<Product> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Products_SelectAll]";

            _data.ExecuteCmd(procName,
                delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@PageIndex", pageIndex);
                    collection.AddWithValue("@PageSize", pageSize);
                }, delegate (IDataReader reader, short set)
                {
                    Product aOwner = MapProduct(reader);
                    totalCount = reader.GetSafeInt32(12);

                    if (list == null)
                    {
                        list = new List<Product>();
                    }
                    list.Add(aOwner);
                });
            if (list != null)
            {
                pagedList = new Paged<Product>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;

        }
        #endregion

        #region Products_Search_Pagination
        public Paged<Product> Products_Search_Pagination(int pageIndex, int pageSize, string Query)
        {
            Paged<Product> pagedList = null;
            List<Product> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Products_Search_Pagination]";

            _data.ExecuteCmd(procName,
                delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@PageIndex", pageIndex);
                    collection.AddWithValue("@PageSize", pageSize);
                    collection.AddWithValue("@Query", Query);
                }, delegate (IDataReader reader, short set)
                {
                    Product aProduct = MapProduct(reader);
                    totalCount = reader.GetSafeInt32(12);

                    if (list == null)
                    {
                        list = new List<Product>();
                    }
                    list.Add(aProduct); 
                });
            if (list != null)
            {
                pagedList = new Paged<Product>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        #endregion
                                                    
        #region Products_Select_ByCreatedBy
        public Paged<Product> Select_ByCreatedBy(int pageIndex, int pageSize, string CreatedBy)
        {
            Paged<Product> pagedList = null;
            List<Product> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Products_Select_ByCreatedBy]";

            _data.ExecuteCmd(procName,
                delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@PageIndex", pageIndex);
                    collection.AddWithValue("@PageSize", pageSize);
                    collection.AddWithValue("@CreatedBy", CreatedBy);
                }, delegate (IDataReader reader, short set)
                {
                    Product aProduct = MapProduct(reader);
                    totalCount = reader.GetSafeInt32(12);

                    if (list == null)
                    {
                        list = new List<Product>();
                    }
                    list.Add(aProduct);
                });
            if (list != null)
            {
                pagedList = new Paged<Product>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        #endregion

        #region Products_SelectAll_Products_ByType
        public Paged<Product> Products_SelectAll_Products_ByType(int pageIndex, int pageSize, string ProductTypeName)
        {
            Paged<Product> pagedList = null;
            List<Product> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Products_SelectAll_Products_ByType]";

            _data.ExecuteCmd(procName,
                delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@PageIndex", pageIndex);
                    collection.AddWithValue("@PageSize", pageSize);
                    collection.AddWithValue("@ProductTypeName", ProductTypeName);
                }, delegate (IDataReader reader, short set)
                {
                    Product aProduct = MapProduct(reader);
                    totalCount = reader.GetSafeInt32(12);

                    if (list == null)
                    {
                        list = new List<Product>();
                    }
                    list.Add(aProduct);
                });
            if (list != null)
            {
                pagedList = new Paged<Product>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        #endregion

        #region Mapper
        private Product MapProduct(IDataReader reader)
        {
            Product aProduct = new Product();
            int startingIndex = 0;

            aProduct.Id = reader.GetSafeInt32(startingIndex++);
            aProduct.SKU = reader.GetSafeString(startingIndex++);
            aProduct.Name = reader.GetSafeString(startingIndex++);
            aProduct.Description = reader.GetSafeString(startingIndex++);

            aProduct.ProductType = _lookUp.MapSingleLookUp(reader, ref startingIndex);

            aProduct.VenueId = reader.GetSafeInt32(startingIndex++);
            aProduct.isVisible = reader.GetBoolean(startingIndex++);
            aProduct.isActive = reader.GetBoolean(startingIndex++);
            aProduct.PrimaryImageId = reader.GetSafeInt32(startingIndex++);

            aProduct.CreatedBy = reader.DeserializeObject<BaseUser>(startingIndex++);
            aProduct.ModifiedBy = reader.DeserializeObject<BaseUser>(startingIndex++);

            return aProduct;
        }
        #endregion

        #region AddCommonParams
        private static void AddCommonParams(ProductAddRequest request, SqlParameterCollection collection)
        {
            collection.AddWithValue("@SKU", request.SKU);
            collection.AddWithValue("@Name", request.Name);
            collection.AddWithValue("@Description", request.Description);
            collection.AddWithValue("@ProductTypeId", request.ProductTypeId);
            collection.AddWithValue("@VenueId", request.VenueId);
            collection.AddWithValue("@isVisible ", request.isVisible);
            collection.AddWithValue("@isActive", request.isActive);
            collection.AddWithValue("@PrimaryImageId", request.PrimaryImageId);
            collection.AddWithValue("@CreatedBy", request.CreatedBy);
            collection.AddWithValue("@ModifiedBy", request.ModifiedBy);
        }
        #endregion
    }
}
  