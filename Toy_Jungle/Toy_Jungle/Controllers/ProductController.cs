using ThePlayCastle.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace ThePlayCastle.Controllers
{
   
    public class ProductController : ControllerBase
    {
        E_CommerceDB1Context db = new E_CommerceDB1Context();
        Hashtable Output = new Hashtable();
        // GET: ProductController
        [Route("api/products")]
        public ActionResult Index()
        {
            return Ok(db.Products.ToList());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        private ActionResult View()
        {
            throw new NotImplementedException();
        }

        [Route("api/products/search/{Keyword}")]
        public ActionResult Search(string Keyword)
        {
            var products = db.Products.Where(x => x.ProductName.Contains(Keyword) || x.Brand.Contains(Keyword) || x.Description.Contains(Keyword));
            return Ok(products);
        }
        [Route("api/products/testcreate")]
        public ActionResult Create()
        {
            Products p = new Products() { ProductName = "New Product", Brand = "Brand", Description = "Description", Price = 200 };
            db.Products.Add(p);
            db.SaveChanges();
            return Ok(db.Products.ToList());

        }

        [Route("api/products/create")]
        [HttpPost]

        public ActionResult Create([FromForm] Products product, IFormFile file)
        {
            if (file == null) return BadRequest();
            //if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                var pid = product.Pid;
                if (file != null && file.Length > 0)
                {
                    var filePath = "Images/Products/" + pid + ".jpg";
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok(product);
            }

            return BadRequest();
        }
        [Route("api/products/update")]
        [HttpPost]

        public ActionResult Update([FromForm] Products product, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                var pid = product.Pid;
                if (file != null && file.Length > 0)
                {
                    var filePath = "Images/Products/" + pid + ".jpg";
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok(product);
            }

            return BadRequest();
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("api/products/delete/{id}")]
        // GET: ProductController/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                Output.Add("Status", "Error");
                Output.Add("Message", "Undefined Product ID");
                return BadRequest(Output);
            }
            Products product = db.Products.Find(id);
            if (product == null)
            {
                Output.Add("Status", "Error");
                Output.Add("Message", "Error");
                return BadRequest(Output);
            }

            new FileInfo("Images/Products/" + id + ".jpg").Delete();
            product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            Output.Add("Status", "Success");
            Output.Add("Message", "Record Deleted");

            return Ok(Output);
        }
        [Route("api/products/images/{PID}")]
        public IActionResult GetProductImage(string PID)
        {
            if (!System.IO.File.Exists(@"Images/products/" + PID + ".jpg")) { PID = "0"; }
            Byte[] b = System.IO.File.ReadAllBytes(@"Images/products/" + PID + ".jpg");   // You can use your own method over here.         
            return File(b, "image/jpeg");
        }
    }
}

