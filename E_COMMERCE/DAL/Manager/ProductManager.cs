using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using System.Data.Entity;


namespace DAL.Manager
{
    public class ProductManager
    {
        Model1 dbhelper = new Model1();


        //Insert
        public string productInsert(PRODUCT product)
        {
            int result = 0;
            var objProduct = dbhelper.PRODUCTS.Where(e => e.PRO_ID == product.PRO_ID && e.PRO_STATUS != "D").SingleOrDefault();
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
                objProduct.PROCAT_ID = prd.PROCAT_ID;
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

        //Delete
        public string productDelete(int productId)
        {
            var objProduct = dbhelper.PRODUCTS.Where(e => e.PRO_ID == productId && e.PRO_STATUS != "D").SingleOrDefault();
            if (objProduct != null)
            {
                objProduct.PRO_STATUS = "D";
                objProduct.PRO_MODIBY = "Admin";
                objProduct.PRO_MODIDATE = DateTime.Now.ToString();
                dbhelper.SaveChanges();

                return "Product Deleted Successfully";
            }
            else
            {
                return "Product Not Found";
            }
        }

        //ProductById
        public PRODUCT productbyId(int productId)
        {
            PRODUCT pobj = new PRODUCT();
            return pobj = dbhelper.PRODUCTS.Where(e => e.PRO_ID == productId && e.PRO_STATUS != "D").SingleOrDefault();
        }

        //ProductSearch
        public List<PRODUCT> searchProducts(string keyname)
        {
            if (!string.IsNullOrWhiteSpace(keyname))
            {
                var products = dbhelper.PRODUCTS.OrderBy(e => e.PRO_NAME)
                    .Where(e => e.PRO_NAME.Contains(keyname) && e.PRO_STATUS != "D").ToList();    // || keyname == null
                return products;
            }
            else
            {
                return new List<PRODUCT>();
            }
        }

        //Filter Products By Category
        public List<PRODUCT> filterProducts(int? category)
        {
            if (category != null)
            {
                var products = dbhelper.PRODUCTS.OrderBy(e => e.PRO_ID).Where(e => e.PROCAT_ID == category && e.PRO_STATUS != "D").ToList();
                return products;
            }
            else
            {
                return new List<PRODUCT>();
            }
        }

        //Display All Products
        public List<PRODUCT> allProducts()
        {
            return dbhelper.PRODUCTS.OrderBy(e => e.PRO_ID).Where(e => e.PRO_STATUS != "D").ToList();
        }

    }
}
