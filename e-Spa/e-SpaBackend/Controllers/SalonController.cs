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
    public class SalonController : TableController<Salon>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Salon>(context, Request);
        }

        // GET tables/Salon
        public IQueryable<Salon> GetAllSalon()
        {
            return Query(); 
        }

        // GET tables/Salon/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Salon> GetSalon(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Salon/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Salon> PatchSalon(string id, Delta<Salon> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Salon
        public async Task<IHttpActionResult> PostSalon(Salon item)
        {
            Salon current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Salon/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSalon(string id)
        {
             return DeleteAsync(id);
        }
    }
}
