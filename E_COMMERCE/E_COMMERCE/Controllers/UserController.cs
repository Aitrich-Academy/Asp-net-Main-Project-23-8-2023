using ECOMMERSE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DAL.Models;
using DAL.Manager;
using System.Web.Configuration;
using System.Web.Http;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using System.Net.Http;
using System.Net;
using System.IO;
using ECOMMERSE.Utils;
using System.Net.Http.Headers;
using System.Web.Http.ModelBinding;
using E_COMMERCE.Utils;
using System.Web.Http.Cors;

namespace ECOMMERSE.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        UserManager mgr = new UserManager();

        [System.Web.Http.HttpGet]
        [Route("UserRegister")]
        [HttpPost]
        public HttpResponseMessage UserRegister(Ent_User user)
        {
            if (user != null && ModelState.IsValid)
            {
                Ent_User ent = user;
            USER usr = new USER();
            usr.USER_NAME = ent.Name;
            usr.USER_EMAIL = ent.email;
            usr.USER_PASSWORD = ent.password;
            usr.USER_PHONE = ent.phone;
            usr.USER_ADDRESS = ent.address;
            usr.USER_IMAGE = ent.image;
            usr.USER_ROLE = "USER";
            usr.USER_STATUS = "A";
            usr.USER_CREATEBY = ent.Name;
            usr.USER_CREATEDATE = DateTime.Now.ToString();
            usr.USER_MODIBY = ent.Name;
            usr.USER_MODIDATE = DateTime.Now.ToString();

                // UserManager mgr = new UserManager();
                return Request.CreateResponse(HttpStatusCode.OK, mgr.AddUser(usr));
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




		[Route("Login")]
		[HttpPost]
		public HttpResponseMessage Login(Ent_User user)
		{
			if (user != null && ModelState.IsValid)
			{
				Ent_User ent = user;
				USER usr = new USER();

				usr.USER_EMAIL = ent.email;
				usr.USER_PASSWORD = ent.password;

				USER result = mgr.UserLogin(usr);

				if (result != null)
				{
					String token = TokenManager.GenerateToken(result);
					LoginResponseDTO loginResponseDTO = new LoginResponseDTO();
					loginResponseDTO.Token = token;
					loginResponseDTO.email = result.USER_EMAIL;
					loginResponseDTO.user_id = result.USER_ID;
					loginResponseDTO.address = result.USER_ADDRESS;
					loginResponseDTO.phone = result.USER_PHONE;
					loginResponseDTO.role = result.USER_ROLE;
					loginResponseDTO.name = result.USER_NAME;

					ResponseDataDTO response = new ResponseDataDTO(true, "Success", loginResponseDTO);
					return Request.CreateResponse(HttpStatusCode.OK, response);
					//return Request.CreateErrorResponse(HttpStatusCode.OK, result);

					return Request.CreateErrorResponse(HttpStatusCode.OK, "Success");
				}
				else
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid User name and password !");
				}
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


	}
}