using DataAccessLibrary.SqlDataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.DataAccess;
using System.Data;
using Dapper;
using DataAccessLibrary.Util;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DataAccessLibrary.DataAccess
{
    public class ReportServices : IReportServices
    {
        private readonly ISqlDataAcess _db;
        private IConfiguration Configuration;
        public ReportServices(ISqlDataAcess db, IConfiguration configuration)
        {
            _db = db;
            Configuration = configuration;
        }

        public List<AppointReport> GetAppointmentInfo(int empID, string dtmDate1, string dtmDate2)
        {
            List<AppointReport> result = new List<AppointReport>();
            string constr = this.Configuration.GetConnectionString("conn");
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


                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dt);

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        result.Add(new AppointReport
                        {
                            //int.Parse(SDR[0].ToString());
                            Id = int.Parse(sdr["Id"].ToString()),
                            wheredate =Convert.ToDateTime(sdr["wheredate"].ToString()),
                            pslct = int.Parse(sdr["pslct"].ToString()),
                            slno = int.Parse(sdr["slno"].ToString()),
                            fromtime = sdr["fromtime"].ToString(),
                            appointwith = sdr["appointwith"].ToString(),
                            remarks = sdr["remarks"].ToString(),
                            Name = sdr["Name"].ToString()

                            //************************************************************************
                            //Id,wheredate,pslct,slno,fromtime,appointwith,remarks,Name 

                            //************************************************************************
                        });
                    }
                }
                con.Close();

                //using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, PhoneNumer FROM Employee where Id<>" + empID))
                //{
                //    cmd.Connection = con;
                //    con.Open();
                //    using (SqlDataReader sdr = cmd.ExecuteReader())
                //    {
                //        while (sdr.Read())
                //        {
                //            result.Add(new AppointReport
                //            {
                //                //int.Parse(SDR[0].ToString());
                //                Id = int.Parse(sdr["Id"].ToString()),
                //                Name = sdr["Name"].ToString()
                //                //PhoneNumer = sdr["PhoneNumer"].ToString()
                //            });
                //        }
                //    }
                //    con.Close();
                //}
            }
            return result;
        }

    
        public async Task<AppointReport> GetAppointmentInfo1(int empID)
        {
            string constr = this.Configuration.GetConnectionString("conn");
            var sql = "SELECT * FROM tblappointment WHERE Id = @Id";
            using (var connection = new SqlConnection(constr))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<AppointReport>(sql, new { Id = empID });
                return result;
            }
        }

        public List<AppointReport> GetAppShareInfo(int empID)
        {
            List<AppointReport> employeelist = new List<AppointReport>();
            string constr = this.Configuration.GetConnectionString("conn");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, PhoneNumer FROM Employee where Id<>" + empID))
                //using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, PhoneNumer FROM Employee order by Name"))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            employeelist.Add(new AppointReport
                            {
                                //int.Parse(SDR[0].ToString());
                                Id = int.Parse(sdr["Id"].ToString()),
                                Name = sdr["Name"].ToString()
                                //PhoneNumer = sdr["PhoneNumer"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return employeelist;
        }

        public List<AppointReport> GetAppShareToMe(int empID)
        {
            List<AppointReport> employeelist = new List<AppointReport>();
            string constr = this.Configuration.GetConnectionString("conn");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, PhoneNumer FROM Employee where Id<>" + empID))
                //using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, PhoneNumer FROM Employee order by Name"))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            employeelist.Add(new AppointReport
                            {
                                //int.Parse(SDR[0].ToString());
                                Id = int.Parse(sdr["Id"].ToString()),
                                Name = sdr["Name"].ToString()
                                //PhoneNumer = sdr["PhoneNumer"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return employeelist;
        }
    }
}
