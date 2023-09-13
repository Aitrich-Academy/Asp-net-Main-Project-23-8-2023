using DAL.Manager;
using DAL.Models;
using E_COMMERCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using E_COMMERCE.Utils;
using System.Web;
using System.IO;

namespace E_COMMERCE.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/category")]
    public class categoryController : ApiController
    {



        CategoryManager mgr = new CategoryManager();
        [System.Web.Http.HttpGet]
        [Route("InsertCategory")]
        [HttpPost]
        public HttpResponseMessage InsertCategory(Ent_Category cat)
        {
            Ent_Category ent = cat;
            if (string.IsNullOrEmpty(cat.Catname))
            {
                return Request.CreateResponse("Category Name is required");
            }

            if (cat.Catid <= 0)
            {
                return Request.CreateResponse("Category is required");
            }

            if (string.IsNullOrEmpty(cat.Catdesc))
            {
                return Request.CreateResponse("Category  Description is required");
            }



            //if (product.productimage == null || product.productimage.Length == 0)
            //{
            //    return Request.CreateResponse("Image is required");
            //}













            if (cat != null && ModelState.IsValid)
            {

                CATEGORY caty = new CATEGORY();
                caty.CAT_NAME = ent.Catname;
                caty.CAT_DESC = ent.Catdesc;
                caty.CAT_IMAGE = ent.Catimage;
                caty.CAT_STATUS = "A";
                caty.CAT_CREATEDBY = "Admin";
                caty.CAT_CREATEDDATE = DateTime.Now.ToString();
                caty.CAT_MODIBY = "Admin";
                caty.CAT_MODIDATE = DateTime.Now.ToString();

                return Request.CreateResponse(HttpStatusCode.OK, mgr.AddCategory(caty));

            }
            else
            {
                IEnumerable<System.Web.Http.ModelBinding.ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Errors = UtilsConfig.GetErrorListFromModelState(ModelState)
                });
            }


        }
        [System.Web.Http.HttpGet]
        [Route("UpdateCategory")]
        [HttpPost]

        public HttpResponseMessage UpdateCategory([FromUri] string id, Ent_Category upcategory)
        {
            Ent_Category ent_Category = upcategory;
            if (string.IsNullOrEmpty(ent_Category.Catname))
            {
                return Request.CreateResponse("Category  Name is required");
            }

            if (ent_Category.Catid <= 0)
            {
                return Request.CreateResponse("Category is required");
            }

            if (string.IsNullOrEmpty(ent_Category.Catdesc))
            {
                return Request.CreateResponse("Category  Description is required");
            }



            //if (product.productimage == null || product.productimage.Length == 0)
            //{
            //    return Request.CreateResponse("Image is required");
            //}

            if (id != null || !ModelState.IsValid)
            {

                CATEGORY caty = new CATEGORY();
                caty.CAT_NAME = ent_Category.Catname;
                caty.CAT_NAME = ent_Category.Catname;
                caty.CAT_DESC = ent_Category.Catdesc;
                caty.CAT_IMAGE = ent_Category.Catimage;
                caty.CAT_STATUS = "A";
                caty.CAT_CREATEDBY = "Admin";
                caty.CAT_CREATEDDATE = DateTime.Now.ToString();
                caty.CAT_MODIBY = "Admin";
                caty.CAT_MODIDATE = DateTime.Now.ToString();



                var response = mgr.updateCategory(Convert.ToInt32(id), caty);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                IEnumerable<System.Web.Http.ModelBinding.ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Errors = UtilsConfig.GetErrorListFromModelState(ModelState)
                });

            }
        }

        [System.Web.Http.HttpGet]
        [Route("DeleteCategory/{categoryid}")]
        [HttpPost]

        public HttpResponseMessage DeleteCategory(int categoryid)
        {
            if (ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.OK, mgr.deleteCategory(categoryid));

            }
            else
            {
                IEnumerable<System.Web.Http.ModelBinding.ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Errors = UtilsConfig.GetErrorListFromModelState(ModelState)
                });

            }
        }


        [System.Web.Http.HttpGet]
        [Route("CategoryById/{id}")]

        public HttpResponseMessage CategoryById(string id)
        {
            CategoryManager mgr = new CategoryManager();
            Ent_Category categorydetails = new Ent_Category();
            if (id != null)
            {
                CATEGORY catobj = mgr.getId(Convert.ToInt32(id));
                if (catobj != null)
                {
                    categorydetails.Catid = catobj.CAT_ID;
                    categorydetails.Catname = catobj.CAT_NAME;
                    categorydetails.Catdesc = catobj.CAT_DESC;
                    categorydetails.Catimage = catobj.CAT_IMAGE;
                    categorydetails.Catstatus = catobj.CAT_STATUS;
                    categorydetails.Catcreatedby = catobj.CAT_CREATEDBY;
                    categorydetails.Catcreateddate = catobj.CAT_CREATEDDATE;
                    categorydetails.Catmodifiedby = catobj.CAT_MODIBY;
                    categorydetails.Catmodifieddate = catobj.CAT_MODIDATE;
                    return Request.CreateResponse(categorydetails);

                }
                else
                {
                    return Request.CreateResponse("Enter valid id");
                }

            }
            else
            {
                return Request.CreateResponse("Enter valid id");
            }
        }

        [System.Web.Http.HttpGet]
        [Route("DisplayAllCategory")]
        public List<Ent_Category> DisplayAllCategory()
        {
            CategoryManager mgr = new CategoryManager();
            List<Ent_Category> return_list = new List<Ent_Category>();
            List<CATEGORY> catobj = mgr.allCategory();
            if (catobj.Count != 0)
            {
                foreach (var obj in catobj)
                {
                    return_list.Add(new Ent_Category
                    {
                        Catid = obj.CAT_ID,
                        Catname = obj.CAT_NAME,
                        Catdesc = obj.CAT_DESC,
                        Catimage = obj.CAT_IMAGE,
                        Catstatus = obj.CAT_STATUS,
                        Catcreatedby = obj.CAT_CREATEDBY,
                        Catcreateddate = Convert.ToDateTime(obj.CAT_CREATEDDATE).ToShortDateString(),
                        Catmodifiedby = obj.CAT_MODIBY,
                        Catmodifieddate = Convert.ToDateTime(obj.CAT_MODIDATE).ToShortDateString()
                    }
                        );
                }
            }
            return return_list;
        }


        [System.Web.Http.HttpGet]
        [HttpPost]
        [Route("FileUploadCat")]
        public IHttpActionResult FileUploadCat()
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
                var filePath = HttpContext.Current.Server.MapPath("~/categoryImage/" + fileName);

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
    }
}


