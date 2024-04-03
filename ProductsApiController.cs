using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.Products;
using Sabio.Models.Requests.Products;
using Sabio.Services;
using Sabio.Services.Products;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sabio.Web.Api.Controllers.Products
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : BaseApiController
    {
        private IProductsService _service = null;
        private IAuthenticationService<int> _authService = null;
        public ProductsApiController(IProductsService productsService, ILogger<ProductsApiController> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _service = productsService;
            _authService = authService;
        }

        #region Products_SelectAll
        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<Product>>> Products_SelectAll(int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<Product> paged = _service.Products_SelectAll(pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("No records found"));
                }
                else
                {
                    ItemResponse<Paged<Product>> response = new ItemResponse<Paged<Product>>();
                    response.Item = paged;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }
            return result;
        }
        #endregion

        #region Products_Select_ById
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Product>> GetById(int id)
        {

            int code = 200;
            BaseResponse response = null;

            try
            {
                Product product = _service.Products_Select_ById(id);

                if (product == null)
                {
                    code = 404;
                    response = new ErrorResponse("Product not found");
                }
                else
                {
                    response = new ItemResponse<Product> { Item = product };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }
        #endregion

        #region Products_Delete_ById (Update isActive)
        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.Products_Delete_ById(id);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }
        #endregion

        #region Products_Insert
        [HttpPost]
        public ActionResult<ItemResponse<int>> Products_Insert(ProductAddRequest request)
        {
            ObjectResult result = null;

            try
            {
                int id = _service.Products_Insert(request);

                ItemResponse<int> response = new ItemResponse<int>() { Item = id };

                result = Created201(response);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }
            return result;
        }

        #endregion

        #region Products_Update 
         [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Products_Update(ProductUpdateRequest model)
        {
            
            int code= 200;
            BaseResponse response = null;
            try
            {
                _service.Products_Update(model);
                response = new SuccessResponse();

            }
            catch(Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response) ;
        }



        #endregion

        #region SearchPagination
        [HttpGet("paginate/search")]
        public ActionResult<ItemResponse<Paged<Product>>> SearchPaginationQuestion(int pageIndex, int pageSize, string query)
        {
            ActionResult result = null;
            try
            {
                Paged<Product> paged = _service.Products_Search_Pagination(pageIndex, pageSize, query);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("No records found"));
                }
                else 
                {
                    ItemResponse<Paged<Product>> response = new ItemResponse<Paged<Product>>();
                    response.Item = paged;
                    result = Ok200(response);
                }   
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }
            return result;
        }
        #endregion

        #region Products_Select_ByCreatedBy
        [HttpGet("paginate/createdby")]
        public ActionResult<ItemResponse<Paged<Product>>> Products_Select_ByCreatedBy(int pageIndex, int pageSize, string createdby)
        {
            ActionResult result = null;
            try
            {
                Paged<Product> paged = _service.Select_ByCreatedBy(pageIndex, pageSize, createdby);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("No records found"));
                }
                else
                {
                    ItemResponse<Paged<Product>> response = new ItemResponse<Paged<Product>>();
                    response.Item = paged;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }
            return result;
        }
        #endregion

        #region Products_SelectAll_Products_ByType
        [HttpGet("paginate/producttype")]
        public ActionResult<ItemResponse<Paged<Product>>> Products_SelectAll_Products_ByType(int pageIndex, int pageSize, string ProductTypeName)
        {
            ActionResult result = null;
            try
            {
                Paged<Product> paged = _service.Products_SelectAll_Products_ByType(pageIndex, pageSize, ProductTypeName);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("No records found"));
                }
                else
                {
                    ItemResponse<Paged<Product>> response = new ItemResponse<Paged<Product>>();
                    response.Item = paged;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }
            return result;
        }
        #endregion
    }
}
       