using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DataAccessLibrary.SqlDataAccess;
using Dapper;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.DataReportModels
{
    public class EmployeeServiceRpt
    {
        //private readonly ISqlDataAcess _db;
        private IConfiguration Configuration;

        //public EmployeeServiceRpt(IConfiguration _configuration)
        //{
        //    //_db = db;
        //    Configuration = _configuration;

        //}

        //public EmployeeServiceRpt()
        //{

        //}

        //string constr = this.Configuration.GetConnectionString("conn");

        //string constr = "data source =DESKTOP-R9HREF8\\MSSQL2K14; initial catalog = DapperDemo; Trusted_Connection = True; MultipleActiveResultSets = true; integrated security =  true; encrypt = false;";
        //string constr = "data source =DESKTOP-R9HREF8\\MSSQL2K14; initial catalog = DapperDemo; Trusted_Connection = True; MultipleActiveResultSets = true; integrated security =  true; encrypt = false;";
        string constr = "Server=DESKTOP-R9HREF8\\MSSQL2K14; Database=DapperDemo; User Id=sa; Password=sa123; Pooling=true; Persist Security Info=true; MultipleActiveResultSets = true; encrypt = false;";
		//"conn": "data source =DESKTOP-R9HREF8\\MSSQL2K14; initial catalog = DapperDemo; Trusted_Connection = True; MultipleActiveResultSets = true; integrated security =  true; encrypt = false;"

		public DataTable GetEmployeeInfo()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Columns.Add("EmpName", typeof(string));
            dt.Columns.Add("Department", typeof(string));
            dt.Columns.Add("Designation", typeof(string));
            dt.Columns.Add("JoinDate", typeof(DateTime));

            DataRow dr;
            for (int i = 1; i <= 50; i++)
            {
                dr = dt.NewRow();
                dr["EmpId"] = i;
                dr["EmpName"] = "Mozammel 1" + i;
                dr["Department"] = "Accounts 1";
                dr["Designation"] = "Executive Director";
                dr["JoinDate"] = DateTime.Now.AddYears(-5).AddDays(1);
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable GetAppointmentInfo(int empID, string dtmDate1, string dtmDate2)
        {

            ///***********************************************************************
            /*empID = 2*/;
            //string constr = this.Configuration.GetConnectionString("conn");

            var dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SP_APPOINTMENT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlParameter param = new SqlParameter("@mCallType", SqlDbType.VarChar);
                param.Value = "SHOWAll_APMNTRPT";
                SqlParameter param1 = new SqlParameter("@Param03", SqlDbType.Int);
                param1.Value = empID;
                SqlParameter param2 = new SqlParameter("@Param02", SqlDbType.VarChar);
                param2.Value = dtmDate1;
                SqlParameter param3 = new SqlParameter("@Param07", SqlDbType.VarChar);
                param3.Value = dtmDate2;
               

                cmd.Parameters.Add(param);
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                cmd.Parameters.Add(param3);
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return dt;
        }

        public DataTable GetAppShareInfo(int empID, string dtmDate1, string dtmDate2)
        {
            //string constr = this.Configuration.GetConnectionString("conn");

            ///***********************************************************************
            /*empID = 2*/
            ;
            var dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SP_APPOINTMENT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlParameter param = new SqlParameter("@mCallType", SqlDbType.VarChar);
                param.Value = "SHOWAll_APMNTUSRRPT";
                SqlParameter param1 = new SqlParameter("@Param03", SqlDbType.Int);
                param1.Value = empID;
                SqlParameter param2 = new SqlParameter("@Param02", SqlDbType.VarChar);
                param2.Value = dtmDate1;
                SqlParameter param3 = new SqlParameter("@Param07", SqlDbType.VarChar);
                param3.Value = dtmDate2;



                cmd.Parameters.Add(param);
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                cmd.Parameters.Add(param3);
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return dt;
        }

        public DataTable GetAppShareToMe(int empID, string dtmDate1, string dtmDate2)
        {
            //string constr = this.Configuration.GetConnectionString("conn");

            ///***********************************************************************
            /*empID = 2*/
            ;
            var dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SP_APPOINTMENT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlParameter param = new SqlParameter("@mCallType", SqlDbType.VarChar);
                param.Value = "SHOWAll_APMNTSHARERPT";
                SqlParameter param1 = new SqlParameter("@Param03", SqlDbType.Int);
                param1.Value = empID;
                SqlParameter param2 = new SqlParameter("@Param02", SqlDbType.VarChar);
                param2.Value = dtmDate1;
                SqlParameter param3 = new SqlParameter("@Param07", SqlDbType.VarChar);
                param3.Value = dtmDate2;



                cmd.Parameters.Add(param);
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                cmd.Parameters.Add(param3);
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return dt;
        }

        public List<Employee> GetAppointmentInfo12(int empID, string dtmDate1, string dtmDate2)
        {
            string constr = this.Configuration.GetConnectionString("conn");
            using (var connection = new SqlConnection(constr))
            {
                var parameters = new DynamicParameters();
                string CallType = "SHOWAll_APMNTRPT";
                int empID1 = empID;
                string dtmDate11 = dtmDate1;
                string dtmDate21 = dtmDate2;
                parameters.Add("@mCallType", CallType, DbType.String);
                parameters.Add("@Param03", empID1, DbType.Int32);
                parameters.Add("@Param02", dtmDate11, DbType.String);
                parameters.Add("@Param07", dtmDate21, DbType.String);
                var employeelist = connection.Query<Employee>("SP_APPOINTMENT", parameters, commandType: CommandType.StoredProcedure);
                return employeelist.ToList();
            }
        }
    }
}
