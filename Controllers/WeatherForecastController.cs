using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace netpostgres.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "addempleado")]
        public Empleado AddEmpleado(Empleado emp)
        {
            Empleado rt = new Empleado();
            using (PostgresdbContext ctx = new PostgresdbContext())
            {
                var idMax = ctx.Empleados.Max(z => z.Id);
                emp.Id = idMax + 1;
                ctx.Empleados.Add(emp);
                ctx.SaveChanges();
               
                rt = (from x in ctx.Empleados.Where(z=>z.Id == emp.Id)
                     select x).First();
            }
            
            return rt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "getempleado")]
        public List<Empleado> getEmpleado()
        {
            List<Empleado> emp = new List<Empleado>();

            using (PostgresdbContext ctx = new PostgresdbContext())
            {
                emp = (from x in ctx.Empleados
                       select x).ToList();

            }

            return emp;
        }

    }
}