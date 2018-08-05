using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using e_SpaBackend.DataObjects;
using e_SpaBackend.Models;

namespace e_SpaBackend.Controllers
{
    public class PlatformIDController : TableController<PlatformID>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<PlatformID>(context, Request);
        }

        // GET tables/PlatformID
        public IQueryable<PlatformID> GetAllPlatformID()
        {
            return Query(); 
        }

        // GET tables/PlatformID/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<PlatformID> GetPlatformID(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/PlatformID/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<PlatformID> PatchPlatformID(string id, Delta<PlatformID> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/PlatformID
        public async Task<IHttpActionResult> PostPlatformID(PlatformID item)
        {
            PlatformID current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/PlatformID/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePlatformID(string id)
        {
             return DeleteAsync(id);
        }
    }
}
