using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Naringskollen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchFoodsController : ControllerBase
    {
        // Only GetAll and GetById
        //Obs! FoodId will be the internal dbs Id if isSystem = true, otherwise it will be LivsmedelverketId.

        //GetById - no dto.
        //Id in Route, others in query: [FromQuery] bool isSystem, [FromQuery] double quantity, [FromQuery] string unit
    }
}
