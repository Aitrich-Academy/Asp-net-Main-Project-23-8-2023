using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Manager
{

    public class UserManager
    {
        Model1 dbhelper = new Model1();
        public string AddUser(USER usr)
        {
            int result = 0;

            dbhelper.USERs.Add(usr);
            result = dbhelper.SaveChanges();

            if (result > 0)
            {
                return usr.USER_ID.ToString();
            }
            else
            {
                return "Error ";
            }
        }
        public USER userDetails(int Id)
        {
            USER return_Obj = new USER();
            return return_Obj = dbhelper.USERs.Where(e => e.USER_ID == Id && e.USER_STATUS != "D").SingleOrDefault();
        }
        public List<USER> UserDetails(int id)
        {
            return dbhelper.USERs.Where(p => p.USER_ID == id).ToList();
        }




        public USER UserLogin(USER ur)
        {
            var log = dbhelper.USERs.Where(x => x.USER_EMAIL.Equals(ur.USER_EMAIL) &&
                                  x.USER_PASSWORD.Equals(ur.USER_PASSWORD)).FirstOrDefault();

            return log;
        }


        //n
        public USER getUserByID(int id)//d3
        {
            USER newUser = new USER();
            return newUser = dbhelper.USERs.Where(e => e.USER_ID == id && e.USER_STATUS != "D").SingleOrDefault();
        }
        public List<ORDER> userOrders(int id)//d5
        {
            List<ORDER> history = dbhelper.ORDERS.Where(e => e.ORD_USERID == id && e.ORD_STATUS != "D").ToList();
            return history;
        }
        public List<USER> AllUsers()//d2
        {
            return dbhelper.USERs.Where(e => e.USER_STATUS != "D").ToList();
        }

        public string UpdateUser(int id, USER usr) //d1
        {
            int result = 0;

            // Load the existing user from the database
            var existingUser = dbhelper.USERs.FirstOrDefault(x => x.USER_ID == id && x.USER_STATUS != "D");

            if (existingUser != null)
            {
                // Compare the original values of the existing user with the values in usr
                dbhelper.Entry(existingUser).OriginalValues["USER_NAME"] = existingUser.USER_NAME;
                dbhelper.Entry(existingUser).OriginalValues["USER_EMAIL"] = existingUser.USER_EMAIL;
                dbhelper.Entry(existingUser).OriginalValues["USER_PASSWORD"] = existingUser.USER_PASSWORD;
                dbhelper.Entry(existingUser).OriginalValues["USER_PHONE"] = existingUser.USER_PHONE;
                dbhelper.Entry(existingUser).OriginalValues["USER_ADDRESS"] = existingUser.USER_ADDRESS;
                dbhelper.Entry(existingUser).OriginalValues["USER_IMAGE"] = existingUser.USER_IMAGE;
                //dbhelper.Entry(existingUser).OriginalValues["USER_ROLE"] = existingUser.USER_ROLE;
                dbhelper.Entry(existingUser).OriginalValues["USER_STATUS"] = existingUser.USER_STATUS;
                dbhelper.Entry(existingUser).OriginalValues["USER_CREATEBY"] = existingUser.USER_CREATEBY;
                //dbhelper.Entry(existingUser).OriginalValues["USER_CREATEDATE"] = existingUser.USER_CREATEDATE;
                dbhelper.Entry(existingUser).OriginalValues["USER_MODIBY"] = existingUser.USER_MODIBY;
                dbhelper.Entry(existingUser).OriginalValues["USER_MODIDATE"] = existingUser.USER_MODIDATE;
                // Add other properties as needed...

                // Update the properties you want to change
                existingUser.USER_NAME = usr.USER_NAME;
                existingUser.USER_EMAIL = usr.USER_EMAIL;
                existingUser.USER_PASSWORD = usr.USER_PASSWORD;
                existingUser.USER_PHONE = usr.USER_PHONE;
                existingUser.USER_ADDRESS = usr.USER_ADDRESS;
                existingUser.USER_IMAGE = usr.USER_IMAGE;
                //existingUser.USER_ROLE = usr.USER_ROLE;
                existingUser.USER_STATUS = usr.USER_STATUS;
                existingUser.USER_CREATEBY = usr.USER_CREATEBY;
                //existingUser.USER_CREATEDATE = usr.USER_CREATEDATE;
                existingUser.USER_MODIBY = usr.USER_MODIBY;
                existingUser.USER_MODIDATE = usr.USER_MODIDATE;
                // Update other properties as needed...

                try
                {
                    result = dbhelper.SaveChanges();
                    if (result > 0)
                    {
                        return "success";
                    }
                    else
                    {
                        return "error";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency conflict here
                    // You can retry the operation, inform the user, or implement a specific strategy
                    return "concurrency conflict";
                }
            }
            else
            {
                return "user not exist";
            }
        }

        public string userDelete(int Id)//d4
        {

            try
            {

                var usr = dbhelper.USERs.Where(e => e.USER_ID == Id && e.USER_STATUS != "D").SingleOrDefault();
                if (usr != null)
                {
                    usr.USER_STATUS = "D";
                    usr.USER_MODIDATE = DateTime.Now.ToString();
                    dbhelper.SaveChanges();
                    return "user deleted";
                }
                else
                {
                    return "user not found";
                }
                //dbhelper.USERs.Remove(usr);
                //dbhelper.SaveChanges();
                //return usr;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}


    



