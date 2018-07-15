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
    public class SalonServiceController : TableController<SalonService>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<SalonService>(context, Request);
        }

        // GET tables/SalonService
        public IQueryable<SalonService> GetAllSalonService()
        {
            return Query(); 
        }

        // GET tables/SalonService/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<SalonService> GetSalonService(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/SalonService/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<SalonService> PatchSalonService(string id, Delta<SalonService> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/SalonService
        public async Task<IHttpActionResult> PostSalonService(SalonService item)
        {
            SalonService current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/SalonService/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSalonService(string id)
        {
             return DeleteAsync(id);
        }
    }
}
