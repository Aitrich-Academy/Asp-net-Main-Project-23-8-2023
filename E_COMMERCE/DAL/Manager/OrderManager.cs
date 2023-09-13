using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public class OrderManager
    {
        Model1 dbhelper = new Model1();

        #region Place Order
        public string placeOrder(ORDER ordObj)
        {
            int result = 0;

            dbhelper.ORDERS.Add(ordObj);
            result = dbhelper.SaveChanges();
            int odId;
            if (result > 0)
            {
                odId = ordObj.ORD_ID;
                AdminNotify(odId);
                return odId.ToString();
            }
            else
            {
                return "Error ";
            }

        }

        public ORDER orderDetails(int id)
        {
            ORDER returnObj = new ORDER();
            return returnObj = dbhelper.ORDERS.Where(e => e.ORD_ID == id && e.ORD_STATUS != "D").SingleOrDefault();
        }
        public List<ORDER> OrderDetails(int id)
        {
            return dbhelper.ORDERS.Where(e => e.ORD_ID == id).ToList();
        }
        #endregion


        #region Display all order

        public List<ORDER> displayAllOrders()
        {
            return dbhelper.ORDERS.OrderByDescending(e => e.ORD_ID).Where(e => e.ORD_STATUS != "D").ToList();
        }
        #endregion

        #region Orderby Id
        public ORDER orderById(int id)
        {
            ORDER ord_obj = new ORDER();
            return ord_obj = dbhelper.ORDERS.Where(e => e.ORD_ID == id && e.ORD_STATUS != "D").SingleOrDefault();
        }
        #endregion


        #region Order by Date
        public List<ORDER> orderByDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {

                var ord = dbhelper.ORDERS.Where(e => e.ORD_CREATEDATE.Contains(date) && e.ORD_STATUS != "D").ToList();
                return ord;
            }
            else
            {
                return new List<ORDER>();
            }

        }
        #endregion

        #region User Order by Date 
        public List<ORDER> UserOrderById(int uid)
        {
            if (uid != null)
            {
                var ord_obj = dbhelper.ORDERS.Where(e => e.ORD_USERID == uid && e.ORD_STATUS != "D").ToList();
                return ord_obj;
            }
            else
            {
                return new List<ORDER>();
            }
        }
        #endregion

        #region Order Cancel
        public string OrderCancel(int oid)
        {
            var ordDel = dbhelper.ORDERS.Where(e => e.ORD_ID == oid && e.ORD_STATUS != "D").SingleOrDefault();
            if (ordDel != null)
            {
                ordDel.ORD_STATUS = "D";
                ordDel.ORD_MODIBY = "User";
                ordDel.ORD_MODIDATE = DateTime.Now.ToString();
                dbhelper.SaveChanges();
                return "Deleted";
            }
            return "Not Found";

        }
        #endregion


        #region Admin Notify
        public string AdminNotify(int id)
        {
            string proName = ""; int odId; int oQty; int oTotal; int oUserid; string ouName = ""; string oUemail = ""; string oUdoneby = "";

            var ordDetails = dbhelper.ORDERS.FirstOrDefault(e => e.ORD_ID == id && e.ORD_STATUS != "D");
            if (ordDetails != null)
            {

                int productid = ordDetails.ORD_PROID.Value;
                var prod = dbhelper.PRODUCTS.FirstOrDefault(e => e.PRO_ID == productid && e.PRO_STATUS == "A");// ordDetails.ORD_PROID.ToString();
                if (prod != null)
                {
                    //proId = prod.PRO_ID;
                    proName = prod.PRO_NAME.ToString();
                }
                odId = ordDetails.ORD_ID;
                oQty = ordDetails.ORD_QTY;
                oTotal = ordDetails.ORD_TOTAL;
                oUserid = ordDetails.ORD_USERID.Value;
                var userdetails = dbhelper.USERs.FirstOrDefault(e => e.USER_ID == oUserid && e.USER_STATUS == "A");
                if (userdetails != null)
                {
                    ouName = userdetails.USER_NAME.ToString();
                    oUemail = userdetails.USER_EMAIL.ToString();
                    oUdoneby = userdetails.USER_CREATEBY.ToString();
                }

                string bodyofMail = "new order" + "\nOrder Id:" + odId + "\nProduct id:" + productid + "\nProduct Name :" + proName
                    + "\nQuantity:" + oQty + "\nTotal:" + oTotal + "\nUser Name:" + ouName + "\nEmail Address:" + oUemail + "\nDone by:" + oUdoneby;
                string fromemailid = "trofiadmit1@gmail.com";
                string fromapppass = "oufjybsvlwqamlpm";
                MailMessage newMail = new MailMessage();
                newMail.From = new MailAddress(fromemailid);
                newMail.Subject = "New Order";
                newMail.To.Add(new MailAddress(fromemailid));  // use session state email in this object user email
                newMail.Body = bodyofMail;
                var smtpclient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromemailid, fromapppass),
                    EnableSsl = true,
                };
                smtpclient.Send(newMail);

            }
            return "success";
        }
        #endregion


        #region user Order Mail
        public string UserOrderEmail(int id)
        {
            var ordDetails = dbhelper.ORDERS.SingleOrDefault(e => e.ORD_ID == id && e.ORD_STATUS == "A");
            if (ordDetails != null)
            {
                int uId = ordDetails.ORD_USERID.Value;
                var emailDetails = dbhelper.USERs.FirstOrDefault(e => e.USER_ID == uId && e.USER_STATUS != "D");
                if (emailDetails != null)
                {
                    string fromemailid = "trofiadmit1@gmail.com";
                    string fromapppass = "oufjybsvlwqamlpm";
                    MailMessage newMail = new MailMessage();
                    newMail.From = new MailAddress(fromemailid);
                    newMail.Subject = "Order Successfull";
                    newMail.To.Add(new MailAddress(emailDetails.USER_EMAIL));  // use session state email in this object user email
                    newMail.Body = "Thank you for your order from us";
                    var smtpclient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromemailid, fromapppass),
                        EnableSsl = true,
                    };
                    smtpclient.Send(newMail);

                }
            }
            return "success";
        }
        #endregion
    }
}
