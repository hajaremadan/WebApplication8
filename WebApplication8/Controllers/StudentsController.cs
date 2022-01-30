using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.OData.Query;
//using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace WebApplication8.Controllers
{
    public class StudentsController : ODataController
    {

        private Models.AppDbContext db;

        public StudentsController(Models.AppDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(db.Student);
        }
    }
}
