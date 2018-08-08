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
    public class SocialPlatformIDController : TableController<SocialPlatformID>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<SocialPlatformID>(context, Request);
        }

        // GET tables/SocialPlatformID
        public IQueryable<SocialPlatformID> GetAllSocialPlatformID()
        {
            return Query(); 
        }

        // GET tables/SocialPlatformID/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<SocialPlatformID> GetSocialPlatformID(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/SocialPlatformID/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<SocialPlatformID> PatchSocialPlatformID(string id, Delta<SocialPlatformID> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/SocialPlatformID
        public async Task<IHttpActionResult> PostSocialPlatformID(SocialPlatformID item)
        {
            SocialPlatformID current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/SocialPlatformID/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSocialPlatformID(string id)
        {
             return DeleteAsync(id);
        }
    }
}
