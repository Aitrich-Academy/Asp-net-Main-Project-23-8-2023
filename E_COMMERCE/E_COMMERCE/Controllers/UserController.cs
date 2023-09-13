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
using E_COMMERCE.Models;

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

					//return Request.CreateErrorResponse(HttpStatusCode.OK, "Success");
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


        //n


        [System.Web.Http.AcceptVerbs("Get", "Post")]
        [System.Web.Http.HttpGet]
        [Route("GetUser")]
        [System.Web.Http.HttpPost]
        //api/User/GetUser?id=1
        public HttpResponseMessage GetUser(string id) //d3
        {
            //UserManager mgObj = new UserManager();
            Ent_User userDet = new Ent_User();

            if (id != null)
            {
                USER usrObj = mgr.getUserByID(Convert.ToInt32(id));
                if (usrObj != null)
                {

                    userDet.Id = usrObj.USER_ID;
                    userDet.Name = usrObj.USER_NAME;
                    userDet.email = usrObj.USER_EMAIL;
                    userDet.phone = usrObj.USER_PHONE;
                    userDet.address = usrObj.USER_ADDRESS;
                    userDet.image = usrObj.USER_IMAGE;
                    userDet.role = usrObj.USER_ROLE;
                    userDet.status = usrObj.USER_STATUS;
                    userDet.createBy = usrObj.USER_CREATEBY;
                    userDet.createdate = usrObj.USER_CREATEDATE;
                    userDet.modiBy = usrObj.USER_MODIBY;
                    userDet.modiDate = usrObj.USER_MODIDATE;
                    userDet.password = usrObj.USER_PASSWORD;
                }
                return Request.CreateResponse(userDet);

            }
            else
            {
                return Request.CreateResponse("enter valid id");
            }
        }


        [Route("ProfileUpdate")]
        [HttpPost]
        //api/User/ProfileUpdate?id=1
        public HttpResponseMessage ProfileUpdate([FromUri] string id, Ent_User updUser) //d1
        {
            if (id != null || !ModelState.IsValid)
            {

                Ent_User entusr = updUser;
                USER usr = new USER();
                if (string.IsNullOrEmpty(updUser.Name))
                {
                    return Request.CreateResponse("Name is Required");
                }
                if (string.IsNullOrEmpty(updUser.email))
                {
                    return Request.CreateResponse("email is Required");
                }
                if (string.IsNullOrEmpty(updUser.password))
                {
                    return Request.CreateResponse("password is Required");
                }
                if (string.IsNullOrEmpty(updUser.phone))
                {
                    return Request.CreateResponse("phone is Required");
                }
                if (string.IsNullOrEmpty(updUser.address))
                {
                    return Request.CreateResponse("address is Required");
                }
                usr.USER_NAME = entusr.Name;
                usr.USER_EMAIL = entusr.email;
                usr.USER_PASSWORD = entusr.password;
                usr.USER_PHONE = entusr.phone;
                usr.USER_ADDRESS = entusr.address;
                usr.USER_IMAGE = entusr.image;
                usr.USER_ROLE = "USER";
                usr.USER_STATUS = "A";
                usr.USER_CREATEBY = entusr.Name;
                usr.USER_CREATEDATE = DateTime.Now.ToString();
                usr.USER_MODIBY = entusr.Name;
                usr.USER_MODIDATE = DateTime.Now.ToString();

                var response = mgr.UpdateUser(Convert.ToInt32(id), usr);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                //IEnumerable<System.Web.Http.ModelBinding.ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                //return Request.CreateResponse(HttpStatusCode.BadRequest, new
                //{
                //    Errors = UtilsConfig.GetErrorListFromModelState(ModelState)
                //});
                return Request.CreateResponse(HttpStatusCode.BadRequest, "not possible");
            }
        }


        [System.Web.Http.HttpGet]
        [Route("UserDelete/{id}")]
        [System.Web.Http.HttpDelete]
        //api/User/UserDelete?id=1
        public HttpResponseMessage UserDelete(string id)//d4
        {
            //USER uObj=mgr.userDelete(Convert.ToInt32(id));
            if (ModelState.IsValid)
            {
                int Uid = Convert.ToInt32(id);
                return Request.CreateResponse(HttpStatusCode.OK, mgr.userDelete(Uid));
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


        //api/User/GetAllUsers
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetAllUsers")]
        [HttpPost]
        public List<Ent_User> GetAllUsers()//d2
        {
            //UserManager mgObj= new UserManager();
            List<Ent_User> users = new List<Ent_User>();
            List<USER> userList = mgr.AllUsers();
            if (userList != null && userList.Count > 0)
            {
                foreach (var userDetails in userList)
                {
                    users.Add(new Ent_User
                    {
                        Id = userDetails.USER_ID,
                        Name = userDetails.USER_NAME,
                        email = userDetails.USER_EMAIL,
                        phone = userDetails.USER_PHONE,
                        address = userDetails.USER_ADDRESS,
                        image = userDetails.USER_IMAGE,
                        role = userDetails.USER_ROLE,
                        status = userDetails.USER_STATUS,
                        createBy = userDetails.USER_CREATEBY,
                        createdate = userDetails.USER_CREATEDATE,
                        modiBy = userDetails.USER_MODIBY,
                        modiDate = userDetails.USER_MODIDATE,
                        password = userDetails.USER_PASSWORD,

                    });
                }
                return users;
            }
            else
            {
                return new List<Ent_User>();
                //return  ("error");
            }
        }


        [System.Web.Http.AcceptVerbs("Get", "Post")]
        [System.Web.Http.HttpGet]
        [Route("GetOrdersOfUser")]
        [System.Web.Http.HttpPost]
        //api/User/GetOrdersOfUser?id=1
        public HttpResponseMessage GetOrdersOfUser(string id)//d5
        {
            //UserManager mgObj= new UserManager();
            List<Ent_Orders> ordDetails = new List<Ent_Orders>();
            if (id != null)
            {
                List<ORDER> orderHistory = mgr.userOrders(Convert.ToInt32(id));
                if (orderHistory != null && orderHistory.Count > 0)
                {
                    foreach (ORDER order in orderHistory)
                    {
                        Ent_Orders ordDet = new Ent_Orders();
                        ordDet.orderId = order.ORD_ID;
                        ordDet.Ord_proId = order.ORD_PROID.Value;
                        ordDet.Ord_qty = order.ORD_QTY;
                        ordDet.Ord_total = order.ORD_TOTAL;
                        ordDet.Ord_status = order.ORD_STATUS;
                        ordDet.Ord_createBy = order.ORD_CREATEBY;
                        ordDet.Ord_createDate = order.ORD_CREATEDATE;
                        ordDet.Ord_modiBy = order.ORD_MODIBY;
                        ordDet.Ord_modiDate = order.ORD_MODIDATE;

                        ordDetails.Add(ordDet);
                    }
                }
                return Request.CreateResponse(ordDetails);
            }
            else
            {
                return Request.CreateResponse("Error");
            }
        }


        [System.Web.Http.HttpGet]
        [HttpPost]
        [Route("ProfileUpload")]
        public IHttpActionResult ProfileUpload()//d6
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
                var filePath = HttpContext.Current.Server.MapPath("~/ProfileImages/" + fileName);

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
	
