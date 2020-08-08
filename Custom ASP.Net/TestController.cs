using Microsoft.AspNetCore.Mvc;

namespace Custom_ASP.Net
{
    [Route("Test")]
    public class TestController : ControllerBase
    {
        [Route("Test")]
        public Derg Test()
        {
            return new Derg
            {
                Cuteness = "Extremely",
                Name = "Calvin",
                Smartness = "Very",
                Dunce = "Sometimes",
                Evaluation = "Best boi"
            };
        }

        [Route("SomethingElse")]
        public IActionResult SomethingElse()
        {
            return Ok("jhwadajd");
        }
    }
}