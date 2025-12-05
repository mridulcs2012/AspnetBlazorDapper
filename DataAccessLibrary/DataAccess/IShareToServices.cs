using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public interface IShareToServices
    {
        Task<IEnumerable<ShareTo>> GetAllData();
        Task<IEnumerable<ShareTo>> GetAllDataSHR(int id);
        
        List<ShareTo> GetAllDataDropDown();
        //Task<bool> SaveDataDetails(ShareTo shareto);
        Task<bool> SaveDataDetails(ShareTo shareto, int apmnId);
        Task<ShareTo> GetDataById(int id);
        Task<bool> DeleteData(int id);
    }
}
