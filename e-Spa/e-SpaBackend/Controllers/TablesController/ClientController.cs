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
    public class ClientController : TableController<Client>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Client>(context, Request);
        }

        // GET tables/Client
        public IQueryable<Client> GetAllClient()
        {
            return Query(); 
        }

        // GET tables/Client/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Client> GetClient(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Client/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Client> PatchClient(string id, Delta<Client> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Client
        public async Task<IHttpActionResult> PostClient(Client item)
        {
            Client current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Client/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteClient(string id)
        {
             return DeleteAsync(id);
        }
    }
}
