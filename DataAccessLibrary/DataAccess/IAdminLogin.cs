using DataAccessLibrary.Models;

namespace DataAccessLibrary.DataAccess
{
    public interface IAdminLogin
    {
        Task<IEnumerable<AdminModel>> GetAllData();
		Task<IEnumerable<AdminModel>> GetAllData1(string usrName);
		List<AdminModel> GetAllDataInd(string usrName);
		List<AdminModel> GetAllDataIndOTP(string usrName,string strOTP);
		//Task<bool> SaveDataDetails(AdminModel adminmodel);

		//Task<bool> DeleteData(int id);

		//Task<bool> UpdateData(AdminModel adminmodel);
		//Task<bool> DeleteData(int id);
		//Task<AdminModel?> GetByIdData(int id);

	}


}