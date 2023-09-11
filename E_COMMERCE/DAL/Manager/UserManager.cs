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



        //Insert
        public string productInsert(PRODUCT product)
        {
            int result = 0;
            var objProduct=dbhelper.PRODUCTS.Where(e=>e.PRO_ID == product.PRO_ID && e.PRO_STATUS!="D").SingleOrDefault();
            if (objProduct == null)
            {
                product.PRO_STATUS = "A";
                dbhelper.PRODUCTS.Add(product);
                result = dbhelper.SaveChanges();

                return "Product Inserted Successfully";
            }
            else
            {
                return "Cannot be Inserted";
            }
        }

        //Update
        public string productUpdate(PRODUCT prd)
        {
            int result = 0;
            var objProduct = dbhelper.PRODUCTS.Where(e => e.PRO_ID == prd.PRO_ID && e.PRO_STATUS != "D").FirstOrDefault();
            //PRODUCT pro = product;

            if (objProduct != null)
            {
                objProduct.PRO_NAME = prd.PRO_NAME;
                objProduct.PROCAT_ID= prd.PROCAT_ID;
                objProduct.PRO_DESC = prd.PRO_DESC;
                objProduct.PRO_STOCK = prd.PRO_STOCK;
                objProduct.PRO_IMAGE = prd.PRO_IMAGE;
                objProduct.PRO_PRICE = prd.PRO_PRICE;
                objProduct.PRO_STATUS = "A";
                objProduct.PRO_MODIBY = "Admin";
                objProduct.PRO_MODIDATE = DateTime.Now.ToString();

                dbhelper.Entry(objProduct).State = EntityState.Modified;
                result = dbhelper.SaveChanges();

                return "Product Updated Successfully.";

            }
            else
            {
                return "Product not found.";
            }
        }

      
    }
}
