using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ShopListsController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/ShopLists
        public IQueryable<ShopList> GetShopLists()
        {
            return db.ShopLists;
        }

        // GET: api/ShopLists/5
        [ResponseType(typeof(ShopList))]
        public IHttpActionResult GetShopList(int id)
        {
            ShopList shopList = db.ShopLists.Find(id);
            if (shopList == null)
            {
                return NotFound();
            }

            return Ok(shopList);
        }

        // PUT: api/ShopLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShopList(int id, ShopList shopList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shopList.ID)
            {
                return BadRequest();
            }

            db.Entry(shopList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ShopLists
        [ResponseType(typeof(ShopList))]
        public IHttpActionResult PostShopList(ShopList shopList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShopLists.Add(shopList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shopList.ID }, shopList);
        }

        // DELETE: api/ShopLists/5
        [ResponseType(typeof(ShopList))]
        public IHttpActionResult DeleteShopList(int id)
        {
            ShopList shopList = db.ShopLists.Find(id);
            if (shopList == null)
            {
                return NotFound();
            }

            db.ShopLists.Remove(shopList);
            db.SaveChanges();

            return Ok(shopList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShopListExists(int id)
        {
            return db.ShopLists.Count(e => e.ID == id) > 0;
        }
    }
}