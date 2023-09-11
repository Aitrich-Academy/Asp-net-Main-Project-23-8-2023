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


namespace E_COMMERCE.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/category")]
    public class categoryController : ApiController
    {



        UserManager mgr = new UserManager();
        [System.Web.Http.HttpGet]
        [Route("InsertCategory")]
        [HttpPost]
        public HttpResponseMessage InsertCategory(Ent_Category cat)
        {
            if (cat != null && ModelState.IsValid)
            {
                Ent_Category ent = cat;
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






        }

    }


