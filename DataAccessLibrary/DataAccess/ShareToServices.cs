using DataAccessLibrary.SqlDataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.DataAccess;
using System.Data;
using DataAccessLibrary.Util;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary.DataAccess
{
    public class ShareToServices: IShareToServices
    {
        private readonly ISqlDataAcess _db;
        private IConfiguration Configuration;
        public ShareToServices(ISqlDataAcess db, IConfiguration configuration)
        {
            _db = db;
            Configuration = configuration;
        }

        public async Task<bool> DeleteData(int id)
        {
            try
            {
                await _db.SaveData("SP_SHARETO", new { @Id = id, @mCallType = "DELETE_SHRT" });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ShareTo>> GetAllData()
        {
            IEnumerable<ShareTo> shareto;
            string query = "SP_SHARETO";
            shareto = await _db.GetData<ShareTo, dynamic>(query, new { @mCallType = "SHOWAll_SHRT" });
            return shareto;
        }

        public async Task<IEnumerable<ShareTo>> GetAllDataSHR(int id)
        {
            IEnumerable<ShareTo> shareto;
            string query = "SP_SHARETO";
            shareto = await _db.GetData<ShareTo, dynamic>(query, new { @AppntId=id, @mCallType = "SHOWAllSHR_SHRT" });
            return shareto;
        }
        public List<ShareTo> GetAllDataDropDown()

        {
            List<ShareTo> sharetolist = new List<ShareTo>();
            string constr = this.Configuration.GetConnectionString("conn");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, AppntId, Insrtuctions, Remarks FROM ShareTo order by AppntId"))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            sharetolist.Add(new ShareTo
                            {
                                //int.Parse(SDR[0].ToString());
                                Id = int.Parse(sdr["Id"].ToString()),
                                AppntId = int.Parse(sdr["AppntId"].ToString()),
                                Insrtuctions = sdr["Insrtuctions"].ToString(),
                                Remarks = sdr["Remarks"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return sharetolist;
        }
        public async Task<ShareTo> GetDataById(int id)
        {
            IEnumerable<ShareTo> result = await _db.GetData<ShareTo, dynamic>("SP_SHARETO", new { @Id = id, @mCallType = "SHOWIND_SHRT" });
            return result.FirstOrDefault();
        }
        public async Task<bool> SaveDataDetails(ShareTo shareto, int apmnId)
        {
            try
            {
                if (shareto.IsUpdate)
                {
                    await _db.SaveData("SP_SHARETO", new
                    {
                        @Id = shareto.Id,
                        @AppntId = apmnId,
                        @EmpId = shareto.EmpId,
                        @Insrtuctions = shareto.Insrtuctions,
                        @Remarks = shareto.Remarks,
                        @mCallType = "UPDATE_SHRT"
                    });
                }
                else
                {
                    await _db.SaveData("SP_SHARETO", new
                    {
                        @Id = shareto.Id,
                        @AppntId = apmnId,
                        @EmpId = shareto.EmpId,
                        @Insrtuctions = shareto.Insrtuctions,
                        @Remarks = shareto.Remarks,
                        @mCallType = "INSERT_SHRT"
                    });
                }

            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        //public async Task<bool> SaveDataDetails(ShareTo stareto)
        //{
        //    try
        //    {
        //        if (stareto.IsUpdate)
        //        {
        //            //await conn.ExecuteAsync("SP_EMPLOYEE", new { @Id = employee.Id, @Name = employee.Name, @EmailAddress = employee.EmailAddress, @PhoneNumer = employee.PhoneNumer, @CreditCardNumer = employee.CreditCardNumer, @mCallType = "UPDATE_EMPL" }, commandType: CommandType.StoredProcedure);
        //            await _db.SaveData("SP_SHARETO", new
        //            {
        //                @Id = stareto.Id,
        //                @AppntId = stareto.AppntId,
        //                @Insrtuctions = stareto.Insrtuctions,
        //                @Remarks = stareto.Remarks,
        //                @mCallType = "UPDATE_SHRT"
        //            });
        //        }
        //        else
        //        {
        //            //await conn.ExecuteAsync("SP_EMPLOYEE", new { @Id = employee.Id, @Name = employee.Name, @EmailAddress = employee.EmailAddress, @PhoneNumer = employee.PhoneNumer, @CreditCardNumer = employee.CreditCardNumer, @mCallType = "INSERT_EMPL" }, commandType: CommandType.StoredProcedure);
        //            await _db.SaveData("SP_SHARETO", new
        //            {
        //                @Id = stareto.Id,
        //                @AppntId = stareto.AppntId,
        //                @Insrtuctions = stareto.Insrtuctions,
        //                @Remarks = stareto.Remarks,
        //                @mCallType = "INSERT_SHRT"
        //            });
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return true;
        //}

    }
}
