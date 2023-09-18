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

namespace E_COMMERCE.Controllers
{
    public class OrdersController : ApiController
    {

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [RoutePrefix("api/Order")]
        public class OrderController : ApiController
        {
            // GET: orders
            OrderManager mngr = new OrderManager();

            #region Place order


            [System.Web.Http.HttpGet]
            [Route("PlaceOrder")]
            [System.Web.Http.HttpPost]

            public HttpResponseMessage PlaceOrder(Ent_Orders order)
            {
                Ent_Orders ent = order;
                ORDER ord = new ORDER();
                int total = mngr.GetPrice(ord);
                if (order != null )
                {
                   
                    ord.ORD_USERID = ent.Ord_userId;
                    ord.ORD_PROID = ent.Ord_proId;
                    ord.ORD_QTY = ent.Ord_qty;
                    int productPrice = mngr.GetPrice(ord);
                    ord.ORD_TOTAL = order.Ord_qty * productPrice;
                    ord.ORD_STATUS = "A";
                    ord.ORD_CREATEBY = "user";// ent.Ord_createBy;
                    ord.ORD_CREATEDATE = DateTime.Now.ToString();
                    ord.ORD_MODIBY = "user";// ent.Ord_modiBy;
                    ord.ORD_MODIDATE = DateTime.Now.ToString();


                    return Request.CreateResponse(HttpStatusCode.OK, mngr.placeOrder(ord));

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


            #region Display All Orders


            [System.Web.Http.HttpGet]
            [Route("DisplayAllOrders")]
            [System.Web.Http.HttpPost]

            public List<Ent_Orders> DisplayAllOrders()
            {

                List<Ent_Orders> ent = new List<Ent_Orders>();
                List<ORDER> ord_Obj = mngr.displayAllOrders();
                AuthenticationHeaderValue authorization = Request.Headers.Authorization;
                if (authorization != null)
                {



                    Ent_User usersDTO = TokenManager.ValidateToken(authorization.Parameter);


                    if (usersDTO.Id != null && usersDTO.role == "Admin")
                    {



                        if (ord_Obj.Count != 0)
                        {

                            foreach (var obj in ord_Obj)
                            {
                                ent.Add(new Ent_Orders
                                {
                                    orderId = obj.ORD_ID,
                                    Ord_userId = Convert.ToInt32(obj.ORD_USERID),
                                    Ord_proId = Convert.ToInt32(obj.ORD_PROID),
                                    Ord_qty = obj.ORD_QTY,
                                    Ord_total = obj.ORD_TOTAL,
                                    Ord_createBy = obj.ORD_CREATEBY,
                                    Ord_createDate = DateTime.Now.ToString(),
                                    Ord_modiBy = obj.ORD_MODIBY,
                                    Ord_modiDate = DateTime.Now.ToString()
                                });

                            }
                        }

                    }


                }
                return ent;
            }

            #endregion


            #region Order by Id

            [System.Web.Http.HttpGet]
            [Route("OrderById")]

            public Ent_Orders OrderById(string id)
            {
                Ent_Orders ent = new Ent_Orders();
                ORDER ord_obj = mngr.orderById(Convert.ToInt32(id));

                if (ord_obj != null)
                {
                    ent.orderId = ord_obj.ORD_ID;
                    ent.Ord_userId = Convert.ToInt32(ord_obj.ORD_USERID);
                    ent.Ord_proId = Convert.ToInt32(ord_obj.ORD_PROID);
                    ent.Ord_qty = ord_obj.ORD_QTY;
                    ent.Ord_total = ord_obj.ORD_TOTAL;
                    ent.Ord_createBy = ord_obj.ORD_CREATEBY;
                    ent.Ord_createDate = Convert.ToDateTime(ord_obj.ORD_CREATEDATE).ToShortDateString();
                    ent.Ord_modiBy = ord_obj.ORD_MODIBY;
                    ent.Ord_modiDate = Convert.ToDateTime(ord_obj.ORD_MODIDATE).ToShortDateString();
                }
                return ent;
            }

            #endregion


            #region Order by Date

            [System.Web.Http.HttpGet]
            [Route("OrderByDate")]

            public IHttpActionResult OrderByDate(string date)
            {
                Ent_Orders ent = new Ent_Orders();
                var bydate = mngr.orderByDate(date);
                // ORDER ord_obj = mngr.orderByDate();
                if (date.Any())
                {
                    //  var byDate = mngr.orderByDate(date);
                    var result = bydate.Select(e => new Ent_Orders
                    {

                        orderId = e.ORD_ID,
                        Ord_userId = Convert.ToInt32(e.ORD_USERID),
                        Ord_proId = Convert.ToInt32(e.ORD_PROID),
                        Ord_qty = e.ORD_QTY,
                        Ord_total = e.ORD_TOTAL,
                        Ord_createBy = e.ORD_CREATEBY,
                        Ord_createDate = Convert.ToDateTime(e.ORD_CREATEDATE).ToShortDateString(),
                        Ord_modiBy = e.ORD_MODIBY,
                        Ord_modiDate = Convert.ToDateTime(e.ORD_MODIDATE).ToShortDateString()
                    }).ToList();
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }

            #endregion


            #region User Order by Id


            [System.Web.Http.HttpGet]
            [Route("UserOrderById")]
            public IHttpActionResult UserOrderById(string uid)
            {
                Ent_Orders ent = new Ent_Orders();
                ORDER ord = new ORDER();
                var ord_obj = mngr.UserOrderById(Convert.ToInt32(uid));

                var result = ord_obj.Select(e => new Ent_Orders
                {
                    orderId = e.ORD_ID,
                    Ord_userId = Convert.ToInt32(e.ORD_USERID),
                    Ord_proId = Convert.ToInt32(e.ORD_PROID),
                    Ord_qty = e.ORD_QTY,
                    Ord_total = e.ORD_TOTAL,
                    Ord_createBy = e.ORD_CREATEBY,
                    Ord_createDate = Convert.ToDateTime(e.ORD_CREATEDATE).ToShortDateString(),
                    Ord_modiBy = e.ORD_MODIBY,
                    Ord_modiDate = Convert.ToDateTime(e.ORD_MODIDATE).ToShortDateString()
                }).ToList();
                return Ok(result);
            }

            #endregion


            #region Order Cancel

            [System.Web.Http.HttpGet]
            [Route("OrderCancel")]
            [System.Web.Http.HttpDelete]

            public HttpResponseMessage OrderCancel(int oid)
            {
                if (ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, mngr.OrderCancel(oid));
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


            #region Order Confirm Mail


            [System.Web.Http.HttpGet]
            [Route("orderConfirmMail/{id}")]
            [HttpPost]
            public HttpResponseMessage orderConfirmMail(int id)
            {
                string result;
                if (id != 0 && ModelState.IsValid)
                {
                    result = mngr.UserOrderEmail(id);
                }
                return Request.CreateResponse(HttpStatusCode.OK, "success");
            }

            #endregion
        }
    }
}
