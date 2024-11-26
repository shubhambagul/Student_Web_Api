using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Student_Web_Api.Model;
using Newtonsoft.Json;

namespace Student_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _config;
        public StudentController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public ActionResult getStudents()
        {
            SqlConnection con = new SqlConnection(_config.GetConnectionString("studentInfo"));
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Student", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
           
            Responce responce = new Responce();
            List<Student> studList = new List<Student>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Student stud = new Student();
                    stud.studentId = Convert.ToInt32(dt.Rows[i]["studentId"]);
                    stud.firstName = Convert.ToString(dt.Rows[i]["firstName"]);
                    stud.lastName = Convert.ToString(dt.Rows[i]["lastName"]);
                    stud.dateOfBirth = Convert.ToDateTime(dt.Rows[i]["dateOfBirth"]);
                    stud.branch = Convert.ToString(dt.Rows[i]["branch"]);
                    stud.cgpa = (float)(double)dt.Rows[i]["cgpa"];
                    studList.Add(stud);
                }
            }
            if (studList.Count > 0) {
            // return   JsonConvert.SerializeObject(studList);
            return Ok(studList);
            }
            else
            {
                responce.statusCode = 404;
                responce.errorMessage = "Not Found";
            // return   JsonConvert.SerializeObject(responce);
            return Ok(responce);
            }
        }
    }

}
