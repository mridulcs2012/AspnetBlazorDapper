using DataAccessLibrary.Models;

namespace DataAccessLibrary.DataAccess
{
    public interface IAdminData
    {
        //Task<IEnumerable<AdminModel>> GetAllData();
        List<AdminModel> GetAllData();
        List<AdminModel> GetAllDataGuest();
        
        List<AdminModel> GetAllDataInd(string userNm);
        Task<bool> SaveDataDetails(AdminModel adminmodel);
        Task<bool> SaveDataDetailsUsr(AdminModel adminmodel);
        Task<bool> SaveDataRegistrationUsr(AdminModel adminmodel);
        
        //Task<bool> SaveDataInd(string userName, string email, string mobile, string password);
        Task<AdminModel> GetDataById(int id);
        Task<bool> DeleteData(int id);

        //Task<bool> UpdateData(AdminModel adminmodel);
        //Task<bool> DeleteData(int id);
        //Task<AdminModel?> GetByIdData(int id);
     
	}


}