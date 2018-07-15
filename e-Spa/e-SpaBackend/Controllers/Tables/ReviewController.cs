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
    public class ReviewController : TableController<Review>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Review>(context, Request);
        }

        // GET tables/Review
        public IQueryable<Review> GetAllReview()
        {
            return Query(); 
        }

        // GET tables/Review/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Review> GetReview(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Review/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Review> PatchReview(string id, Delta<Review> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Review
        public async Task<IHttpActionResult> PostReview(Review item)
        {
            Review current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Review/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteReview(string id)
        {
             return DeleteAsync(id);
        }
    }
}
