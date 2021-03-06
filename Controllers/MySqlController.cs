using GopalAPIDemo.Class;
using GopalAPIDemo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GopalAPIDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MySqlController : ControllerBase
    {
        public ILogger<MySqlController> Logger;
        public IConfiguration Configuration { get; set; }
        public MySqlController(IConfiguration configuration, ILogger<MySqlController> logger)
        {
            Configuration = configuration;
            Logger = logger;
            string conn = string.Empty;
            try
            {
                conn = configuration.GetSection("ConnectionStrings")["MySql"];
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, "Cannot fetch the connection string");
            }

        }

        [HttpGet]
        [Route("GetTableData")]
        public ActionResult<List<Contacts>> GetTableData()
        {
            try
            {
                Logger.LogInformation("Fetching table data");
                string conn = Configuration.GetConnectionString("MySql");
                return Ok(new BLOrmMySql(conn).GetContacts());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex.StackTrace, "Error in fetching all data from table");
                return null;
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        [HttpPost]
        [Route("InsertContact")]
        public ActionResult<List<Contacts>> InsertContact([FromBody] Contacts contact)
        {
            try
            {
                Logger.LogInformation("Inserting contact data");
                string conn = Configuration.GetConnectionString("MySql");
                return Ok(new BLOrmMySql(conn).AddContact(contact));
            }
            catch 
            {
                return null;
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}
