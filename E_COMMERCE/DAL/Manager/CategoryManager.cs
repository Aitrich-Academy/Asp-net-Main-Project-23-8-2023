using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public class CategoryManager
    {
        Model1 dbhelper = new Model1();



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

        public string updateCategory(int id, CATEGORY cat)
        {
            int result = 0;
            var extCategory = dbhelper.CATEGORies.FirstOrDefault(x => x.CAT_ID == id && x.CAT_STATUS != "D");
            if (extCategory != null)
            {
                dbhelper.Entry(extCategory).OriginalValues["CAT_NAME"] = extCategory.CAT_NAME;
                dbhelper.Entry(extCategory).OriginalValues["CAT_DESC"] = extCategory.CAT_DESC;
                dbhelper.Entry(extCategory).OriginalValues["CAT_IMAGE"] = extCategory.CAT_IMAGE;
                dbhelper.Entry(extCategory).OriginalValues["CAT_STATUS"] = extCategory.CAT_STATUS;
                dbhelper.Entry(extCategory).OriginalValues["CAT_CREATEDBY"] = extCategory.CAT_CREATEDBY;
                dbhelper.Entry(extCategory).OriginalValues["CAT_CREATEDDATE"] = extCategory.CAT_CREATEDDATE;
                dbhelper.Entry(extCategory).OriginalValues["CAT_MODIBY"] = extCategory.CAT_MODIBY;
                dbhelper.Entry(extCategory).OriginalValues["CAT_MODIDATE"] = extCategory.CAT_MODIDATE;



                extCategory.CAT_NAME = cat.CAT_NAME;
                extCategory.CAT_DESC = cat.CAT_DESC;
                extCategory.CAT_IMAGE = cat.CAT_IMAGE;
                extCategory.CAT_STATUS = cat.CAT_STATUS;
                extCategory.CAT_CREATEDBY = cat.CAT_CREATEDBY;
                extCategory.CAT_CREATEDDATE = cat.CAT_CREATEDDATE;
                extCategory.CAT_MODIBY = cat.CAT_MODIBY;
                extCategory.CAT_MODIDATE = cat.CAT_MODIDATE;
                dbhelper.Entry(extCategory).State = EntityState.Modified;
                result = dbhelper.SaveChanges();
            }
            else
            {
                return "Category does not found";
            }
            if (result > 0)
            {
                return "Category updated successfully";
            }
            else { return "Error"; }
        }

        public string deleteCategory(int categoryid)
        {
            var objcat = dbhelper.CATEGORies.Where(e => e.CAT_ID == categoryid && e.CAT_STATUS != "D").SingleOrDefault();
            if (objcat != null)
            {
                objcat.CAT_STATUS = "D";
                objcat.CAT_MODIBY = "Admin";
                objcat.CAT_MODIDATE = DateTime.Now.ToString();
                dbhelper.SaveChanges();
                return "Category deleted successfully";
            }
            else
            {
                return "Error";
            }
        }


        public CATEGORY getId(int id)
        {
            CATEGORY cat=new CATEGORY();
            return cat=dbhelper.CATEGORies.Where(e=>e.CAT_ID==id&&e.CAT_STATUS!="D").SingleOrDefault();

        }

        public List<CATEGORY> allCategory()
        {
            return dbhelper.CATEGORies.Where(e => e.CAT_STATUS != "D").ToList();
        }



    }
}
