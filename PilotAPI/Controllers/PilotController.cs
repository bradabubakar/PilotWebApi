using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using PilotAPI.Models;

namespace PilotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;

        public PilotController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get() 
        {
            string query = @"
                   select PilotId, PilotName, 
                    convert(varchar(10),Schedule,120) as Schedule, 
                    Origin, Destination from Pilot";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PilotAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Pilot pil)
        {
            string query = @"
                           insert into dbo.Pilot 
                            (PilotName,Schedule,Origin,Destination)
                           values
                            (
                           '" + pil.PilotName + @"'
                           ,'" + pil.Schedule + @"'
                           ,'" + pil.Origin + @"'
                           ,'" + pil.Destination + @"'
                            )
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PilotAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Successfully added!");
        }

        [HttpPut]
        public JsonResult Put(Pilot pil)
        {
            string query = @"
                           update dbo.Pilot set
                           PilotName = '" + pil.PilotName + @"'
                           ,Schedule = '" + pil.Schedule + @"'
                           ,Origin = '" + pil.Origin + @"'
                           ,Destination = '" + pil.Destination + @"'
                           where PilotId = " + pil.PilotId + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PilotAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Successfully updated!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Pilot
                           where PilotId = " + id + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PilotAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Successfully deleted!");
        }

        [Route("GetAllOrigin")]
        public JsonResult GetAllOrigin()
        {
            string query = @"
                    select Origin=Originname from dbo.Origin
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PilotAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        
        [Route("GetAllDestination")]
        public JsonResult GetAllDestination()
        {
            string query = @"
                    select Destination=DestinationName from dbo.Destination
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PilotAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


    }
}