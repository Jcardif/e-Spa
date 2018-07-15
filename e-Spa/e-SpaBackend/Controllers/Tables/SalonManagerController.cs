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
    public class SalonManagerController : TableController<SalonManager>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<SalonManager>(context, Request);
        }

        // GET tables/SalonManager
        public IQueryable<SalonManager> GetAllSalonManager()
        {
            return Query(); 
        }

        // GET tables/SalonManager/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<SalonManager> GetSalonManager(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/SalonManager/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<SalonManager> PatchSalonManager(string id, Delta<SalonManager> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/SalonManager
        public async Task<IHttpActionResult> PostSalonManager(SalonManager item)
        {
            SalonManager current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/SalonManager/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSalonManager(string id)
        {
             return DeleteAsync(id);
        }
    }
}
