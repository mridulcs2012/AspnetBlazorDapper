using DataAccessLibrary.Models;

namespace DataAccessLibrary.DataAccess
{
    public interface IEmployeeServices
    {
        //Followings are for Dropdown List*************************************************************
        //GetAllDataDropDown1 and GetAllDataDropDown are same********************************************
        //GetAllDataDropDown is using Store Procedure and Dapper***************************************
        List<Employee> GetAllDataDropDown1();
        List<Employee> GetAllDataDropDown(); // is using Store procedure and Dapper
        List<Employee> GetAllDataDropDownEmpforApmnt(int empID);
        List<Employee> GetAllDataDropDownUserRole();
        //********************************************************************************************

        //Foloowing are for Show, ADD/Save, Show Individual and Delete Individual****************************
        Task<IEnumerable<Employee>> GetAllData();
        Task<bool> SaveDataDetails(Employee employee);
        Task<Employee> GetDataById(int id);
        Task<bool> DeleteData(int id);
    }
}
