using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        private readonly PostgresdbContext ctx;
        private readonly IConfiguration _config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration confg, PostgresdbContext _postgresdbContext)
        {
            _logger = logger;
            ctx = _postgresdbContext;
            _config = confg;
        }

        [HttpPost(Name = "addempleado")]
        public Empleado AddEmpleado(Empleado emp)
        {
            Empleado rt = new Empleado();
            {
                var idMax = 0;
                try
                {
                    idMax = ctx.Empleados.Max(z => z.Id);
                }
                catch (Exception ex) { }


                emp.Id = idMax + 1;
                ctx.Empleados.Add(emp);
                ctx.SaveChanges();

                rt = (from x in ctx.Empleados.Where(z => z.Id == emp.Id)
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

            //using (PostgresdbContext ctx = new PostgresdbContext())
            {
                emp = (from x in ctx.Empleados
                       select x).ToList();

            }

            return emp;
        }


        [HttpPut(Name = "InitDatabase")]
        public bool initDataBase() {
            return ctx.Database.EnsureCreated();
        }


    }
}