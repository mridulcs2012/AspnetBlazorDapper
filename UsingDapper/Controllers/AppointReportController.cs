using AspNetCore.Reporting;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.DataReportModels;
using DataAccessLibrary.Models;
using DataAccessLibrary.ReportDataSet;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Ocsp;
using System.Data;
using System.Text;

namespace BlazorDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointReportController : Controller
    {
        private readonly IWebHostEnvironment _iwebhostenvironment;
        //EmployeeServices employeeService = new EmployeeServices();
        //private readonly IEmployeeServices employeeService;

        //****************************************************************************
        //public AppointReportController(IWebHostEnvironment iwebhostenvironment, IEmployeeServices _employeeService)
        //{
        //    this._iwebhostenvironment = iwebhostenvironment;
        //    employeeService = _employeeService;
        //    //System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //}

        ////[HttpGet]
        ////[Route("AppointmentReport12")]
        ////public IActionResult AppointmentReport12(int empID, string dtmDate1, string dtmDate2, string empName, string dtRange)
        //{
        //    //empID = 2;
        //    var dt = new List<Employee>();
        //    dt = employeeService.GetAppointmentInfo12(empID, dtmDate1, dtmDate2);

        //    string mimtype = "";
        //    int extension = 1;
        //    var Filepath = $"{this._iwebhostenvironment.WebRootPath}\\Reports\\AppointmentRpt.rdlc";
        //    //********************************************************************

        //    //*****************************************************************************
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("ReportParameter1", "APPOINTMENT LIST OF ");
        //    parameters.Add("ReportParameter2", empName);
        //    parameters.Add("ReportParameter3", dtRange);
        //    LocalReport localReport = new LocalReport(Filepath);

        //    localReport.AddDataSource("DataSetReport", dt);
        //    var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
        //    return File(result.MainStream, "application/pdf");
        //}


    }
}
