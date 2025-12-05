using DataAccessLibrary.Models;

namespace DataAccessLibrary.DataAccess
{
    public interface IAppointmentServices
    {
        //Task<IEnumerable<Appointment>> GetAppointment();
        //Task<IEnumerable<Appointment>> GetAppointmentByDate(string dtmDate);
        //Task<bool> SaveAppointmentDetails(Appointment appointment);
        //Task<Appointment> GetAppointmentById(int id);
        //Task<bool> DeleteAppointment(int id);
        Task<IEnumerable<Appointment>> GetAllData();
		Task<IEnumerable<Appointment>> GetAllDataUsr(string dtmDate, int empID);
        Task<IEnumerable<Appointment>> GetAllDataOtherApmnt(string dtmDate, int empID);

        Task<bool> SaveDataDetails(Appointment appointment,int empid);
        Task<Appointment> GetDataById(int id);
        Task<bool> DeleteData(int id);
        Task<IEnumerable<Appointment>> GetDataByDate(string dtmDate);

    }
}
