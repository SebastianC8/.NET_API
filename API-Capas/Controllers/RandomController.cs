using Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Capas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        public IRandomCore _randomServiceSingleton;
        public IRandomCore _randomServiceScoped;
        public IRandomCore _randomServiceTransient;

        public IRandomCore _randomService2Singleton;
        public IRandomCore _randomService2Scoped;
        public IRandomCore _randomService2Transient;

        public RandomController(
            [FromKeyedServices("randomSingleton")] IRandomCore randomServiceSingleton,
            [FromKeyedServices("randomScoped")] IRandomCore randomServiceScoped,
            [FromKeyedServices("randomTransient")] IRandomCore randomServiceTransient,
            [FromKeyedServices("randomSingleton")] IRandomCore randomService2Singleton,
            [FromKeyedServices("randomScoped")] IRandomCore randomService2Scoped,
            [FromKeyedServices("randomTransient")] IRandomCore randomService2Transient
        )
        {
            _randomServiceSingleton = randomServiceSingleton;
            _randomServiceScoped = randomServiceScoped;
            _randomServiceTransient = randomServiceTransient;
            _randomService2Singleton = randomService2Singleton;
            _randomService2Scoped = randomService2Scoped;
            _randomService2Transient = randomService2Transient;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();

            result.Add("Singleton #1: ", _randomServiceSingleton.Value);
            result.Add("Scoped #1: ", _randomServiceScoped.Value);
            result.Add("Transient #1: ", _randomServiceTransient.Value);
            result.Add("Singleton #2: ", _randomService2Singleton.Value);
            result.Add("Scoped #2: ", _randomService2Scoped.Value);
            result.Add("Transient #2: ", _randomService2Transient.Value);

            return result;
        }

    }
}
