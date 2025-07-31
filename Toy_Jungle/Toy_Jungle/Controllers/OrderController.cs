using ThePlayCastle.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ThePlayCastle.Controllers
{
    
    public class OrderController : ControllerBase
    {
        E_CommerceDB1Context db = new E_CommerceDB1Context();
        Hashtable Output = new Hashtable();
        // GET: OrderController
        [Route("api/orders")]
        public ActionResult Index()
        {
            var orders = db.Orders.ToList();
            foreach (Orders order in orders)
            {
                order.Items = db.OrderInfo.Where(oi => oi.OrderNumber == order.OrderNumber).ToList();
            }
            return Ok(orders);
        }


        [Route("api/orders/SearchByOrderNumber/{OrderNumber}")]
        public ActionResult SearchByOrderNumber(string OrderNumber)
        {
            var order = db.Orders.Where(x => x.OrderNumber.ToString() == OrderNumber).FirstOrDefault();
            order.Items = db.OrderInfo.Where(oi => oi.OrderNumber == order.OrderNumber).ToList();
            return Ok(order);
        }

        [Route("api/orders/SearchByUID/{UID}")]
        public ActionResult SearchByUID(string UID)
        {
            var orders = db.Orders.Where(x => x.Uid.ToString() == UID).ToList();
            foreach (Orders order in orders)
            {
                order.Items = db.OrderInfo.Where(oi => oi.OrderNumber == order.OrderNumber).ToList();
            }
            return Ok(orders);
        }

        [Route("api/orders/SearchByMobile/{Mobile}")]
        public ActionResult SearchByMobile(string Mobile)
        {
            var user = db.Users.Where(x => x.Mobile == Mobile).FirstOrDefault();
            if (user == null) return NotFound();
            return SearchByUID(user.Uid.ToString());
        }


        public ActionResult Details(int id)
        {
            return View();
        }

        private ActionResult View()
        {
            throw new NotImplementedException();
        }

        [Route("api/orders/create")]
        [HttpPost]
        // GET: OrderController/Create
        public ActionResult Create([FromBody] Orders order)
        {
            decimal OrderNumber = 100001;
            if (db.Orders.ToList().Count > 0)
                OrderNumber = db.Orders.Max(x => x.OrderNumber) + 1;
            foreach (OrderInfo oi in order.Items) oi.OrderNumber = OrderNumber;
            order.OrderNumber = OrderNumber;

            if (ModelState.IsValid)
            {
                List<OrderInfo> items = order.Items;
                db.OrderInfo.AddRange(order.Items);

                db.SaveChanges();

                order.Items = null;
                db.Orders.Add(order);
                db.SaveChanges();
                Output.Add("Status", "Success");
                Output.Add("Data", order);
                return Ok(Output);
            }
            Output.Add("Status", "Error");
            Output.Add("Message", "Invalid Parameters");
            return BadRequest(Output);
        }

        [Route("api/OrderInfo/create")]
        [HttpPost]
        public ActionResult test([FromBody] List<OrderInfo> od)
        {
            db.AddRange(od);
            db.SaveChanges();

            return Ok(od);
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
    }
}
