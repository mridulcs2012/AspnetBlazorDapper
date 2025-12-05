using DataAccessLibrary.SqlDataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.DataAccess;
using System.Data;
using Microsoft.VisualBasic;

namespace DataAccessLibrary.DataAccess
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly ISqlDataAcess _db;

        public AppointmentServices(ISqlDataAcess db)
        {
            _db = db;
        }
        public async Task<bool> DeleteData(int id)
        {
            try
            {
                await _db.SaveData("SP_APPOINTMENT", new { @Param01 = id, @mCallType = "DELETE_APMNT" });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //using (var conn = new SqlConnection(_connectionString))
            //{
            //    if (conn.State == ConnectionState.Closed)
            //        conn.Open();
            //    try
            //    {
            //        await conn.ExecuteAsync("SP_APPOINTMENT", new { @Param01 = id, @mCallType = "DELETE_APMNT" }, commandType: CommandType.StoredProcedure);
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
            //    finally
            //    {
            //        if (conn.State == ConnectionState.Open)
            //            conn.Close();
            //    }
            //}
            //return true;
        }
        public async Task<IEnumerable<Appointment>> GetAllData()
        {
            IEnumerable<Appointment> appointment;
            string query = "SP_APPOINTMENT";
            appointment = await _db.GetData<Appointment, dynamic>(query, new { @mCallType = "SHOWAll_APMNT" });
            return appointment;

            //IEnumerable<Appointment> appointmentEntries;
            //using (var conn = new SqlConnection(_connectionString))
            //{
            //    if (conn.State == ConnectionState.Closed)
            //        conn.Open();
            //    try
            //    {
            //        appointmentEntries = await conn.QueryAsync<Appointment>("SP_APPOINTMENT", new { @mCallType = "SHOWAll_APMNT" }, commandType: CommandType.StoredProcedure);
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
            //    finally
            //    {
            //        if (conn.State == ConnectionState.Open)
            //            conn.Close();
            //    }
            //}
            //return appointmentEntries;
        }
		public async Task<IEnumerable<Appointment>> GetAllDataUsr(string dtmDate,int empID)
		{
			IEnumerable<Appointment> appointment;
			string query = "SP_APPOINTMENT";
			appointment = await _db.GetData<Appointment, dynamic>(query, new { @Param02 = dtmDate, @Param03 = empID, @mCallType = "SHOWAll_APMNTUSR" });
			return appointment;
		}
        public async Task<IEnumerable<Appointment>> GetAllDataOtherApmnt(string dtmDate, int empID)
        {
            IEnumerable<Appointment> appointment;
            string query = "SP_APPOINTMENT";
            appointment = await _db.GetData<Appointment, dynamic>(query, new { @Param02 = dtmDate, @Param03 = empID, @mCallType = "SHOWAll_APMNTSHARE" });
            return appointment;
        }
        public async Task<Appointment> GetDataById(int id)
        {
            IEnumerable<Appointment> result = await _db.GetData<Appointment, dynamic>("SP_APPOINTMENT", new { @Param01 = id, @mCallType = "SHOWIND_APMNT" });
            return result.FirstOrDefault();
        }
        public async Task<bool> SaveDataDetails(Appointment appointment,int empid)
        {
            try
            {
                if (appointment.IsUpdate)
                {
                    await _db.SaveData("SP_APPOINTMENT", new
                    {
                        @Param01 = appointment.Id,
                        @Param02 = appointment.wheredate,
                        @Param03 = empid,
                        //@Param03 = appointment.pslct,
                        @Param04 = appointment.slno,
                        @Param05 = appointment.fromtime,
                        @Param06 = appointment.appointwith,
                        @Param07 = appointment.remarks,
                        @mCallType = "UPDATE_APMNT"
                    });
                }
                else
                {
                    await _db.SaveData("SP_APPOINTMENT", new
                    {
                        @Param01 = appointment.Id,
                        @Param02 = appointment.wheredate,
                        @Param03 = empid,
                        //@Param03 = appointment.pslct,
                        @Param04 = appointment.slno,
                        @Param05 = appointment.fromtime,
                        @Param06 = appointment.appointwith,
                        @Param07 = appointment.remarks,
                        @mCallType = "INSERT_APMNT"
                    });
                }

            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        public async Task<IEnumerable<Appointment>> GetDataByDate(string dtmDate)
        {
            IEnumerable<Appointment> appointment;
            string query = "SP_APPOINTMENT";
            appointment = await _db.GetData<Appointment, dynamic>(query, new { @Param02 = dtmDate, @mCallType = "SHOWAllDT_APMNT" });
            return appointment;

            //Appointment appointment = new Appointment();

            //IEnumerable<Appointment> appointmentEntries;
            //using (var conn = new SqlConnection(_connectionString))
            //{
            //    if (conn.State == ConnectionState.Closed)
            //        conn.Open();
            //    try
            //    {
            //        appointmentEntries = await conn.QueryAsync<Appointment>("SP_APPOINTMENT", new { @Param02 = dtmDate, @mCallType = "SHOWAllDT_APMNT" }, commandType: CommandType.StoredProcedure);
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
            //    finally
            //    {
            //        if (conn.State == ConnectionState.Open)
            //            conn.Close();
            //    }
            //}
            //return appointmentEntries;
        }
    }
}
