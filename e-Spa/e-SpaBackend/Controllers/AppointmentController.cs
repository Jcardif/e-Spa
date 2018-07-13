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
    public class AppointmentController : TableController<Appointment>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Appointment>(context, Request);
        }

        // GET tables/Appointment
        public IQueryable<Appointment> GetAllAppointment()
        {
            return Query(); 
        }

        // GET tables/Appointment/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Appointment> GetAppointment(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Appointment/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Appointment> PatchAppointment(string id, Delta<Appointment> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Appointment
        public async Task<IHttpActionResult> PostAppointment(Appointment item)
        {
            Appointment current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Appointment/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAppointment(string id)
        {
             return DeleteAsync(id);
        }
    }
}
