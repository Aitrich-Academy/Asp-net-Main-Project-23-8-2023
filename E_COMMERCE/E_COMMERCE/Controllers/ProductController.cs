using DAL.Manager;
using DAL.Models;
using E_COMMERCE.Utils;
using ECOMMERSE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;


using System.Web.Configuration;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using System.IO;
using ECOMMERSE.Utils;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
using System.Net.Http.Handlers;
using E_COMMERCE.Models;
using System.Web.Mvc;
using ModelError = System.Web.Http.ModelBinding.ModelError;
using System.Threading.Tasks;
using System.Web.Razor.Tokenizer;
using Newtonsoft.Json;
using System.Web;

namespace E_COMMERCE.Controllers
{

    [RoutePrefix("api/Product")]

    public class ProductController : ApiController
    {
        ProductManager mgr = new ProductManager();

        #region PRODUCT INSERT

        [System.Web.Http.HttpGet]
        [Route("ProductInsert")]
        [HttpPost]
        public HttpResponseMessage ProductInsert(Ent_Product product)
        {
            Ent_Product entpro = product;

            if (string.IsNullOrEmpty(product.productname))
            {
                return Request.CreateResponse( "Product Name is required");
            }

            if (product.procatid <= 0)
            {
                return Request.CreateResponse("Category is required");
            }
            
            if (string.IsNullOrEmpty(product.productdescription))
            {
                return Request.CreateResponse("Product Description is required");
            }

            if (product.productstock == 0)
            {
                return Request.CreateResponse("Stock is required");
            }
            else if(product.productstock < 0)
            {
                return Request.CreateResponse("Stock cannot be negative");
            }

            //if (product.productimage == null || product.productimage.Length == 0)
            //{
            //    return Request.CreateResponse("Image is required");
            //}

            if (product.productprice == 0)
            {
                return Request.CreateResponse("Price is required");
            }
            else if(product.productprice < 0)
            {
                return Request.CreateResponse("Price cannot be negative");
            }

            if (product != null && ModelState.IsValid)
            {
                PRODUCT prd = new PRODUCT();
                prd.PRO_NAME = entpro.productname;
                prd.PROCAT_ID = entpro.procatid;
                prd.PRO_DESC = entpro.productdescription;
                prd.PRO_STOCK = entpro.productstock;
                prd.PRO_IMAGE = entpro.productimage;
                prd.PRO_PRICE = entpro.productprice;
                prd.PRO_STATUS = "A";
                prd.PRO_CREATEBY = "Admin";
                prd.PRO_CREATEDATE = DateTime.Now.ToString();
                prd.PRO_MODIBY = "Admin";
                prd.PRO_MODIDATE = DateTime.Now.ToString();

                return Request.CreateResponse(HttpStatusCode.OK, mgr.productInsert(prd));
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Errors = UtilsConfig.GetErrorListFromModelState(ModelState)
                });
            }
        }

        #endregion

        #region PRODUCT UPDATE

        [System.Web.Http.HttpGet]
        [Route("ProductUpdate/{productId}")]
        [System.Web.Http.HttpPut]
        public HttpResponseMessage ProductUpdate(int productId, Ent_Product product)
        {
            Ent_Product entpro = product;

            if (string.IsNullOrEmpty(product.productname))
            {
                return Request.CreateResponse("Product Name is required");
            }

            if (product.procatid <= 0)
            {
                return Request.CreateResponse("Category is required");
            }

            if (string.IsNullOrEmpty(product.productdescription))
            {
                return Request.CreateResponse("Product Description is required");
            }

            if (product.productstock == 0)
            {
                return Request.CreateResponse("Stock is required");
            }
            else if (product.productstock < 0)
            {
                return Request.CreateResponse("Stock cannot be negative");
            }

            //if (product.productimage == null || product.productimage.Length == 0)
            //{
            //    return Request.CreateResponse("Image is required");
            //}

            if (product.productprice == 0)
            {
                return Request.CreateResponse("Price is required");
            }
            else if (product.productprice < 0)
            {
                return Request.CreateResponse("Price cannot be negative");
            }

            if (product != null && ModelState.IsValid)
            {
                PRODUCT prd = new PRODUCT();


                prd.PRO_ID = productId;  //lookatme
                prd.PRO_NAME = product.productname;
                prd.PROCAT_ID = product.procatid;
                prd.PRO_DESC = product.productdescription;
                prd.PRO_STOCK = product.productstock;
                prd.PRO_IMAGE = product.productimage;
                prd.PRO_PRICE = product.productprice;
                //prd.PRO_STATUS = "A";

                return Request.CreateResponse(HttpStatusCode.OK, mgr.productUpdate(prd));
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Errors = UtilsConfig.GetErrorListFromModelState(ModelState)
                });
            }
        }

        #endregion

        #region PRODUCT DELETE

        [System.Web.Http.HttpGet]
        [Route("ProductDelete/{productId}")]
        [System.Web.Http.HttpDelete]
        public HttpResponseMessage ProductDelete(int productId)
        {
            if (ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.OK, mgr.productDelete(productId));
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Errors = UtilsConfig.GetErrorListFromModelState(ModelState)
                });
            }
        }

        #endregion

        #region PRODUCT BY ID

        [System.Web.Http.HttpGet]
        [Route("ProductById/{productId}")]
        public HttpResponseMessage ProductById(string productId)
        {
            Ent_Product prd = new Ent_Product();

            if (productId != null)
            {
                PRODUCT product = mgr.productbyId(Convert.ToInt32(productId));
                if (product != null)
                {
                    prd.productid = product.PRO_ID;
                    prd.productname = product.PRO_NAME;
                    prd.procatid = (int)product.PROCAT_ID;
                    prd.productdescription = product.PRO_DESC;
                    prd.productstock = product.PRO_STOCK;
                    prd.productimage = product.PRO_IMAGE;
                    prd.productprice = product.PRO_PRICE;
                    prd.productstatus = "A";
                    prd.productcreatedby = "Admin";
                    prd.productcreateddate = DateTime.Now.ToString();
                    prd.productmodifiedby = "Admin";
                    prd.productmodifieddate = DateTime.Now.ToString();
                }
                return Request.CreateResponse(HttpStatusCode.OK, (prd));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product Not Found");
            }
        }

        #endregion

        #region PRODUCT SEARCH

        [System.Web.Http.HttpGet]
        [Route("ProductSearch/{keyname}")]
        public IHttpActionResult ProductSearch(string keyname)
        {
            try
            {
                var products = mgr.searchProducts(keyname);

                if (products.Any())
                {
                    var startsWithKeyName = products.Where(e => e.PRO_NAME.StartsWith(keyname, StringComparison.OrdinalIgnoreCase)); //List of products containing keyName
                    var remainingProducts = products.Except(startsWithKeyName);  //List of remaining products 

                    var allProducts = startsWithKeyName.Concat(remainingProducts); // Concatenate above lists to display products starting with 'keyName' first

                    var result = allProducts.Select(e => new Ent_Product
                    {
                        productname = e.PRO_NAME,
                        productimage = e.PRO_IMAGE,
                        productprice = e.PRO_PRICE,
                    }).ToList();                          // To select and display specified fields

                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region UPLOAD FILE

        [System.Web.Http.HttpGet]
        [HttpPost]
        [Route("FileUpload")]
        public IHttpActionResult FileUpload()
        {
            try
            {

                string[] AllowedFileExt = new string[] { ".jpg", ".png" };

                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count == 0)                    //Checks if there is a file to upload
                {
                    return BadRequest("No file to upload");
                }

                //Get the uploaded file
                var postedFile = httpRequest.Files[0];

                if (!AllowedFileExt.Contains(postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("."))))
                {
                    return BadRequest("Invalid File Format");
                }

                var fileName = Path.GetFileName(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/ProductImages/" + fileName);

                if (File.Exists(filePath))
                {
                    return BadRequest("A file with the same name already exists.");
                }

                //Save the file to the server
                postedFile.SaveAs(filePath);

                return Ok("File Uploaded Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region FILTER BY CATEGORY

        [System.Web.Http.HttpGet]
        [Route("FilterProducts/{category}")]
        public IHttpActionResult FilterProducts(int? category)
        {
            try
            {
                var products = mgr.filterProducts(category);

                var result = products.Select(e => new Ent_Product
                {
                    productname = e.PRO_NAME,
                    productimage = e.PRO_IMAGE,
                    productprice = e.PRO_PRICE,
                }).ToList();                          // To select and display specified fields

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        #endregion

        #region DISPLAY ALL PRODUCTS

        [System.Web.Http.HttpGet]
        [Route("DisplayProducts")]
        public HttpResponseMessage DisplayProducts()
        {
            List<Ent_Product> entprolist = new List<Ent_Product>();
            List<PRODUCT> prolist = mgr.allProducts();

            if (prolist.Count != 0)
            {
                foreach (var product in prolist)
                {
                    entprolist.Add(new Ent_Product
                    {
                        productname = product.PRO_NAME,
                        productimage = product.PRO_IMAGE,
                        productprice = product.PRO_PRICE,
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, (entprolist));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product Not Found");
            }
        }

        #endregion

    }
}
