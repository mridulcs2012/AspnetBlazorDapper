using DataAccessLibrary.Models;

namespace DataAccessLibrary.DataAccess
{
    public interface IReportServices
    {
        List<AppointReport> GetAppointmentInfo(int empID, string dtmDate1, string dtmDate2);
        List<AppointReport> GetAppShareInfo(int empID);
        List<AppointReport> GetAppShareToMe(int empID);

        Task<AppointReport> GetAppointmentInfo1(int empID);
    }
}
