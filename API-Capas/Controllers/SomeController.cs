using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API_Capas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult Sync()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            Thread.Sleep(3000);
            Console.WriteLine("Conexión a BD terminada");

            Thread.Sleep(3000);
            Console.WriteLine("Envío de correo finalizado");

            Console.WriteLine("Todo ha terminado");
            Console.WriteLine("************************************");

            sw.Stop();

            return Ok(sw.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            var task_1 = new Task<int>(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Conexión a BD terminada");
                return new Random().Next(5000);
            });

            task_1.Start();

            var task_2 = new Task<int>(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Envío de correo finalizado");
                return new Random().Next(5000);
            });

            task_2.Start();

            Console.WriteLine("Haciendo otra cosa");

            int bd = await task_1;
            int mail = await task_2;

            Console.WriteLine("Todo ha terminado");
            Console.WriteLine("***********************************");
            sw.Stop();

            return Ok(new{ bd, mail, Time = sw.Elapsed });
        }
    }
}
