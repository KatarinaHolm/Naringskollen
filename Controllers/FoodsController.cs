using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Naringskollen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        //All CRUDs
        //Authorize: Admin -  on Create, Put, Patch och Delete.

        //Obs! isSystem = true means the food is not from Livsmedelverkets database.

        //GetAll() - SummaryDto

        //GetById - no dto.
        //Id in Route, others in query: [FromQuery] bool isSystem, [FromQuery] double quantity, [FromQuery] string unit
    }
}
