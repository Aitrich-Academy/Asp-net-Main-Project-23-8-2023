using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public string AddCategory(CATEGORY cat)
        {
            int result = 0;
            dbhelper.CATEGORies.Add(cat);
            result = dbhelper.SaveChanges();
            if (result > 0)
            {
                return cat.CAT_ID.ToString();
            }
            else
            {
                return "Error";
            }

        }

      
                
}




