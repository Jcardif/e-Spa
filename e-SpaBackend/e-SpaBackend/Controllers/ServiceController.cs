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
    public class ServiceController : TableController<Service>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Service>(context, Request);
        }

        // GET tables/Service
        public IQueryable<Service> GetAllService()
        {
            return Query(); 
        }

        // GET tables/Service/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Service> GetService(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Service/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Service> PatchService(string id, Delta<Service> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Service
        public async Task<IHttpActionResult> PostService(Service item)
        {
            Service current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Service/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteService(string id)
        {
             return DeleteAsync(id);
        }
    }
}
