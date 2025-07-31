using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ThePlayCastle.Models;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace ThePlayCastle.Controllers
{
    
    public class UserController : ControllerBase
    {
        E_CommerceDB1Context db = new E_CommerceDB1Context();
        Hashtable Output = new Hashtable();
        [Route("api/users")]
        [HttpGet]
        public ActionResult Index()
        {

            return Ok(db.Users.ToList());
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        private ActionResult View()
        {
            throw new NotImplementedException();
        }

        [Route("api/createAdmin")]
        public object Create()
        {
            Users user = new Users();
            user.UserName = "Admin";
            user.Email = "admin@gmail.com";
            user.Mobile = "9490491722";
            user.Pwd = "aa";
            db.Users.Add(user);
            db.SaveChanges();
            return Ok(user);
        }

        // POST: HomeController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        [Route("api/users/create")]
        [HttpPost]

        public IActionResult Create([FromBody] Users user)
        {
            var check = db.Users.Where(x => x.Email == user.Email).SingleOrDefault();
            if (check != null)
            {
                Output.Add("Status", "Error");
                Output.Add("Message", "Email Already Exist");
                return Ok(Output);
            }
            check = db.Users.Where(x => x.Mobile == user.Mobile).SingleOrDefault();
            if (check != null)
            {
                Output.Add("Status", "Error");
                Output.Add("Message", "Mobile Number Already Exist");
                return Ok(Output);
            }

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                Output.Add("Status", "Success");
                Output.Add("Data", user);

                //return RedirectToAction("Index");
                return Ok(Output);
            }
            Output.Add("Status", "Error");
            Output.Add("Message", "Error Creating User");

            return BadRequest(Output);
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [Route("api/users/update")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Update([FromBody] Users user)
        {
            if (ModelState.IsValid)
            {
                //Users u = (from x in db.Users where x.Uid == user.Uid select x).FirstOrDefault();
                //u.Address = user.Address;   
                //u.UserName = user.UserName; 
                //u.Mobile = user.Mobile;
                //u.Address= user.Address;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Output.Add("Status", "Success");
                Output.Add("Data", user);
                return Ok(Output);
            }
            return BadRequest();
        }

        // GET: HomeController/Delete/5
        [Route("api/users/delete/{id}")]
        // GET: ProductController/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                Output.Add("Status", "Error");
                Output.Add("Message", "Undefined User ID");
                return BadRequest(Output);
            }
            Users user = db.Users.Find(id);
            if (user == null)
            {
                Output.Add("Status", "Error");
                Output.Add("Message", "Error");
                return BadRequest(Output);
            }


            user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            Output.Add("Status", "Success");
            Output.Add("Message", "Record Deleted");

            return Ok(Output);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
        [Route("api/login")]
        [HttpPost]
        public ActionResult Login([FromBody] Users user)
        {

            var User = db.Users.Where(x => x.Email.Equals(user.Email) && x.Pwd.Equals(user.Pwd)).FirstOrDefault();
            if (User == null)
            {
                Output.Add("Status", "Error");
                Output.Add("Message", "Invalid Credentials");

                return NotFound(Output);
            }
            Output.Add("Status", "Success");
            Output.Add("Data", (User));
            return Ok(Output);

        }


    }
}

