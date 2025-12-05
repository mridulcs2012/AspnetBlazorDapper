using AspNetCore.Reporting;
using DataAccessLibrary.DataReportModels;
using DataAccessLibrary.Models;
using DataAccessLibrary.ReportDataSet;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using Microsoft.VisualBasic;
//using Org.BouncyCastle.Ocsp;
using System.Data;
using System.Text;


namespace BlazorDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IWebHostEnvironment _iwebhostenvironment;
        EmployeeServiceRpt employeeService = new EmployeeServiceRpt();

        //****************************************************************************
        public EmployeeController(IWebHostEnvironment iwebhostenvironment)
        {
            this._iwebhostenvironment=iwebhostenvironment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        [Route("EmployeeReport")]
        public IActionResult EmployeeReport()
        {
            var dt = new DataTable();
            dt = employeeService.GetEmployeeInfo();
            string mimtype = "";
            int extension = 1;
            var Filepath = $"{this._iwebhostenvironment.WebRootPath}\\Reports\\Report1.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportParameter1", "WELCOME TO MY CODING WORLD, ANOTHER REPORT/ DATA WILL COME HERE");
            parameters.Add("ReportParameter2", "My Name is Mozammel");
            LocalReport localReport = new LocalReport(Filepath);

            localReport.AddDataSource("dsEmployeeInfo", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }

        [HttpGet]
        [Route("AppointmentReport")]
        public IActionResult AppointmentReport(int empID, string dtmDate1, string dtmDate2, string empName, string dtRange)
        {
            //empID = 2;
            var dt = new DataTable();
            dt = employeeService.GetAppointmentInfo(empID, dtmDate1, dtmDate2);

            string mimtype = "";
            int extension = 1;
            var Filepath = $"{this._iwebhostenvironment.WebRootPath}\\Reports\\AppointmentRpt.rdlc";
            //********************************************************************

            //*****************************************************************************
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportParameter1", "APPOINTMENT LIST OF ");
            parameters.Add("ReportParameter2", empName);
            parameters.Add("ReportParameter3", dtRange);
            LocalReport localReport = new LocalReport(Filepath);

            localReport.AddDataSource("DataSetReport", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }

        [HttpGet]
        [Route("AppShareReport")]
        public IActionResult AppShareReport(int empID, string dtmDate1, string dtmDate2, string empName, string dtRange)
        {
            var dt = new DataTable();
            dt = employeeService.GetAppShareInfo(empID, dtmDate1, dtmDate2);

            string mimtype = "";
            int extension = 1;
            var Filepath = $"{this._iwebhostenvironment.WebRootPath}\\Reports\\AppShareRpt.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportParameter1", "APPOINTMENTS SHARE TO OTHERS");
            parameters.Add("ReportParameter2", "Appointments of "+empName);
            parameters.Add("ReportParameter3", dtRange);
            LocalReport localReport = new LocalReport(Filepath);

            localReport.AddDataSource("DataSet1", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }

        [HttpGet]
        [Route("AppShareToMe")]
        public IActionResult AppShareToMe(int empID, string dtmDate1, string dtmDate2, string empName, string dtRange)
        {
            var dt = new DataTable();
            dt = employeeService.GetAppShareToMe(empID, dtmDate1, dtmDate2);

            string mimtype = "";
            int extension = 1;
            var Filepath = $"{this._iwebhostenvironment.WebRootPath}\\Reports\\AppShareToMe.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportParameter1", "APPOINTMENTS SHARE TO ME");
            parameters.Add("ReportParameter2", "Appointments share to " + empName);
            parameters.Add("ReportParameter3", dtRange);
            LocalReport localReport = new LocalReport(Filepath);

            localReport.AddDataSource("DataSet1", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }


		[HttpGet]
		[Route("AppointmentReport2")]
		public string AppointmentReport2(int empID, string dtmDate1, string dtmDate2, string empName, string dtRange)
		{
			//empID = 2;
			var dt = new DataTable();
            
			dt = employeeService.GetAppointmentInfo(empID, dtmDate1, dtmDate2);

            string JSONString = JsonConvert.SerializeObject(dt);
		
            return JSONString;
			//return File(JSONString, "application/txt");
			//string mimtype = "";
			//int extension = 1;
			//var Filepath = $"{this._iwebhostenvironment.WebRootPath}\\Reports\\AppointmentRpt.rdlc";
			////********************************************************************
			//return File(Filepath, "application/txt");
			////*****************************************************************************
			//Dictionary<string, string> parameters = new Dictionary<string, string>();
			//parameters.Add("ReportParameter1", "APPOINTMENT LIST OF ");
			//parameters.Add("ReportParameter2", empName);
			//parameters.Add("ReportParameter3", dtRange);
			//LocalReport localReport = new LocalReport(Filepath);

			//localReport.AddDataSource("DataSetReport", dt);
			//var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
			//return File(result.MainStream, "application/pdf");
		}

        [HttpGet]
        [Route("AppointmentReport12")]
        public IActionResult AppointmentReport12(int empID, string dtmDate1, string dtmDate2, string empName, string dtRange)
        {
            //empID = 2;
            var dt = new List<Employee>();
            dt = employeeService.GetAppointmentInfo12(empID, dtmDate1, dtmDate2);

            string mimtype = "";
            int extension = 1;
            var Filepath = $"{this._iwebhostenvironment.WebRootPath}\\Reports\\AppointmentRpt.rdlc";
            //********************************************************************

            //*****************************************************************************
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportParameter1", "APPOINTMENT LIST OF ");
            parameters.Add("ReportParameter2", empName);
            parameters.Add("ReportParameter3", dtRange);
            LocalReport localReport = new LocalReport(Filepath);

            localReport.AddDataSource("DataSetReport", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }


        //
    }
}
