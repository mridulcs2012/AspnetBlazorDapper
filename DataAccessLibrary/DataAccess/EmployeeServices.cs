using DataAccessLibrary.SqlDataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.DataAccess;
using System.Data;

using DataAccessLibrary.Util;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace DataAccessLibrary.DataAccess
{
    public class EmployeeServices: IEmployeeServices
    {
        private readonly ISqlDataAcess _db;
        private IConfiguration Configuration;
        public EmployeeServices(ISqlDataAcess db, IConfiguration configuration)
        {
            _db = db;
            Configuration = configuration;
        }

        //Followings are for Dropdown List*************************************************************
        // GetAllDataDropDown1 and GetAllDataDropDown are Same *************************************
        // GetAllDataDropDown is using Store Procedure and Dapper**********************************
        public List<Employee> GetAllDataDropDown1()
        {
            List<Employee> employeelist = new List<Employee>();
            string constr = this.Configuration.GetConnectionString("conn");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, PhoneNumer FROM Employee order by Name"))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            employeelist.Add(new Employee { Id = int.Parse(sdr["Id"].ToString()),
                                Name = sdr["Name"].ToString(), PhoneNumer = sdr["PhoneNumer"].ToString() });
                        }
                    }
                    con.Close();
                }
            }
            return employeelist;
        }
        public List<Employee> GetAllDataDropDown()
        {
            string constr = this.Configuration.GetConnectionString("conn");
            using (var connection = new SqlConnection(constr))
            {
                var parameters = new DynamicParameters();
                string CallType = "SHOWAll_EMPL";
                parameters.Add("@mCallType", CallType, DbType.String);
                var employeelist = connection.Query<Employee>("SP_EMPLOYEE", parameters, commandType: CommandType.StoredProcedure);
                return employeelist.ToList();
            }
        }
        public List<Employee> GetAllDataDropDownEmpforApmnt(int empID)
        {
            List<Employee> employeelist = new List<Employee>();
            string constr = this.Configuration.GetConnectionString("conn");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, PhoneNumer FROM Employee where Id<>"+empID))
                //using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, PhoneNumer FROM Employee order by Name"))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            employeelist.Add(new Employee
                            {
                                //int.Parse(SDR[0].ToString());
                                Id = int.Parse(sdr["Id"].ToString()),
                                Name = sdr["Name"].ToString(),
                                PhoneNumer = sdr["PhoneNumer"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return employeelist;
        }
        public List<Employee> GetAllDataDropDownUserRole()
        {
            List<Employee> userRole = new List<Employee>();
            string constr = this.Configuration.GetConnectionString("conn");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT RoleId, UserRole FROM UserRoleTb order by RoleId"))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            userRole.Add(new Employee
                            {
                                Id = int.Parse(sdr["RoleId"].ToString()),
                                Name = sdr["UserRole"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return userRole;
        }
        //********************************************************************************************

        //Foloowing are for Show, ADD/Save, Show Individual and Delete Individual****************************
        public async Task<IEnumerable<Employee>> GetAllData()
        {
            IEnumerable<Employee> employee;
            string query = "SP_EMPLOYEE";
            employee = await _db.GetData<Employee, dynamic>(query, new { @mCallType = "SHOWAll_EMPL" });
            return employee;
        }

        public async Task<bool> SaveDataDetails(Employee employee)
        {
            try
            {
                if (employee.IsUpdate)
                {
                    //await conn.ExecuteAsync("SP_EMPLOYEE", new { @Id = employee.Id, @Name = employee.Name, @EmailAddress = employee.EmailAddress, @PhoneNumer = employee.PhoneNumer, @CreditCardNumer = employee.CreditCardNumer, @mCallType = "UPDATE_EMPL" }, commandType: CommandType.StoredProcedure);
                    await _db.SaveData("SP_EMPLOYEE", new
                    {
                        @Id = employee.Id,
                        @Name = employee.Name,
                        @EmailAddress = employee.EmailAddress,
                        @PhoneNumer = employee.PhoneNumer,
                        @CreditCardNumer = employee.CreditCardNumer,
                        @mCallType = "UPDATE_EMPL"
                    });
                }
                else
                {
                    //await conn.ExecuteAsync("SP_EMPLOYEE", new { @Id = employee.Id, @Name = employee.Name, @EmailAddress = employee.EmailAddress, @PhoneNumer = employee.PhoneNumer, @CreditCardNumer = employee.CreditCardNumer, @mCallType = "INSERT_EMPL" }, commandType: CommandType.StoredProcedure);
                    await _db.SaveData("SP_EMPLOYEE", new
                    {
                        @Id = employee.Id,
                        @Name = employee.Name,
                        @EmailAddress = employee.EmailAddress,
                        @PhoneNumer = employee.PhoneNumer,
                        @CreditCardNumer = employee.CreditCardNumer,
                        @mCallType = "INSERT_EMPL"
                    });
                }

            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        public async Task<Employee> GetDataById(int id)
        {
            IEnumerable<Employee> result = await _db.GetData<Employee, dynamic>("SP_EMPLOYEE", new { @Id = id, @mCallType = "SHOWIND_EMPL" });
            return result.FirstOrDefault();
        }
        public async Task<bool> DeleteData(int id)
        {
            try
            {
                await _db.SaveData("SP_EMPLOYEE", new { @Id = id, @mCallType = "DELETE_EMPL" });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
        //**********************************************************************************************
    }
}
