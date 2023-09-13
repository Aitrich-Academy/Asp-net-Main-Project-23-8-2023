﻿using DAL.Manager;
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


        //[System.Web.Http.HttpGet]
        //[System.Web.Http.HttpPost]
        //[Route("FileUpload")]

        //public async Task<IHttpActionResult> FileUpload()
        //{
        //    //var context = HttpContext.Current;
        //    //var root = context.Server.MapPath("~/ProductImages");
        //    //var provider= new MultipartFormDataStreamProvider(root);

        //    //try
        //    //{
        //    //   await Request.Content.ReadAsMultipartAsync(provider);
        //    //    foreach(var file in provider.FileData)
        //    //    {
        //    //        var name = file.Headers.ContentDisposition.FileName;
        //    //        name = name.Trim('"');

        //    //        var localfile = file.LocalFileName;
        //    //        var filepath=Path.Combine(root, name);

        //    //        File.Move(localfile,filepath);
        //    //    }
        //    //    return Ok("File Uploaded Successfully");
        //    //}
        //    //catch (Exception ex) 
        //    //{
        //    //    return InternalServerError(ex);
        //    //}
        //}

        #endregion

    }
}
