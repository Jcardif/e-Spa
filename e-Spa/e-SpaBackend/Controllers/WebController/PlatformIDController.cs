using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using e_SpaBackend.DataObjects;
using e_SpaBackend.Models;
using PlatformID = e_SpaBackend.DataObjects.PlatformID;

namespace e_SpaBackend.Controllers.WebController
{
    public class PlatformIDController : ApiController
    {
        private MobileServiceContext db = new MobileServiceContext();

        // GET: api/PlatformID
        public IQueryable<PlatformID> GetPlatformIDs()
        {
            return db.PlatformIDs;
        }

        // GET: api/PlatformID/5
        [ResponseType(typeof(PlatformID))]
        public async Task<IHttpActionResult> GetPlatformID(string id)
        {
            PlatformID platformID = await db.PlatformIDs.FindAsync(id);
            if (platformID == null)
            {
                return NotFound();
            }

            return Ok(platformID);
        }

        // PUT: api/PlatformID/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlatformID(string id, PlatformID platformID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != platformID.Id)
            {
                return BadRequest();
            }

            db.Entry(platformID).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformIDExists(id))
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

        // POST: api/PlatformID
        [ResponseType(typeof(PlatformID))]
        public async Task<IHttpActionResult> PostPlatformID(PlatformID platformID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlatformIDs.Add(platformID);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlatformIDExists(platformID.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = platformID.Id }, platformID);
        }

        // DELETE: api/PlatformID/5
        [ResponseType(typeof(PlatformID))]
        public async Task<IHttpActionResult> DeletePlatformID(string id)
        {
            PlatformID platformID = await db.PlatformIDs.FindAsync(id);
            if (platformID == null)
            {
                return NotFound();
            }

            db.PlatformIDs.Remove(platformID);
            await db.SaveChangesAsync();

            return Ok(platformID);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlatformIDExists(string id)
        {
            return db.PlatformIDs.Count(e => e.Id == id) > 0;
        }
    }
}