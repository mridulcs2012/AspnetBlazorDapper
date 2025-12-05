using DataAccessLibrary.SqlDataAccess;
using DataAccessLibrary.Models;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary.DataAccess
{

    public class AdminLogin : IAdminLogin
    {
        private readonly ISqlDataAcess _db;
		private IConfiguration Configuration;
		public AdminLogin(ISqlDataAcess db, IConfiguration _configuration)
        {
            _db = db;
			Configuration = _configuration;
        }
        public async Task<IEnumerable<AdminModel>> GetAllData()
        {
            IEnumerable<AdminModel> adminList;
            string query = "SP_PSWRD";
            adminList = await _db.GetData<AdminModel, dynamic>(query, new { @mCallType = "SHOWAll_PSWRD" });
            return adminList;
        }
		public async Task<IEnumerable<AdminModel>> GetAllData1(string usrName)
		{
			IEnumerable<AdminModel> adminList;
			string query = "SP_PSWRD";
			adminList = await _db.GetData<AdminModel, dynamic>(query, new { @Username=usrName, @mCallType = "SHOWAll_PSWRDIND" });
			return adminList;
		}
		public List<AdminModel> GetAllDataInd(string userNm)
		{
			List<AdminModel> adminList = new List<AdminModel>();
			string constr = this.Configuration.GetConnectionString("conn");
			using (SqlConnection con = new SqlConnection(constr))
			{
				//using (SqlCommand cmd = new SqlCommand("SELECT id, Username, Email, Mobile, Password FROM AdminTb where userName='" + userNm + "'"))
				using (SqlCommand cmd = new SqlCommand("SELECT a.id,a.Username,a.Email,a.Mobile,a.Password,a.EmployeeId,Name=Isnull(b.Name,'Guest User'),a.UserRoleId FROM (AdminTb a left join Employee b on a.EmployeeId=isnull(b.Id,0)) where userName='" + userNm + "'"))
				{
					cmd.Connection = con;
					con.Open();
					using (SqlDataReader sdr = cmd.ExecuteReader())
					{
						while (sdr.Read())
						{
							adminList.Add(new AdminModel
							{
								Id = int.Parse(sdr["id"].ToString()),
								Username = sdr["Username"].ToString(),
								Email = sdr["Email"].ToString(),
								Mobile = sdr["Mobile"].ToString(),
								Password = sdr["Password"].ToString(),
								EmployeeID = int.Parse(sdr["EmployeeId"].ToString()),
								Name = sdr["Name"].ToString(),
								UserRoleId = int.Parse(sdr["UserRoleId"].ToString())
								//OTP = int.Parse(sdr["UserRoleId"].ToString())
							});
						}
					}
					con.Close();
				}
			}
			return adminList;
		}
		public List<AdminModel> GetAllDataIndOTP(string userNm, string strOTP)
		{
			List<AdminModel> adminList = new List<AdminModel>();
			string constr = this.Configuration.GetConnectionString("conn");
			using (SqlConnection con = new SqlConnection(constr))
			{
				//using (SqlCommand cmd = new SqlCommand("SELECT id, Username, Email, Mobile, Password FROM AdminTb where userName='" + userNm + "'"))
				using (SqlCommand cmd = new SqlCommand("SELECT a.id,a.Username,a.Email,a.Mobile,a.Password,a.EmployeeId,Name=Isnull(b.Name,'Guest User'),a.UserRoleId FROM (AdminTb a left join Employee b on a.EmployeeId=isnull(b.Id,0)) where userName='" + userNm + "'"))
				{
					cmd.Connection = con;
					con.Open();
					using (SqlDataReader sdr = cmd.ExecuteReader())
					{
						while (sdr.Read())
						{
							adminList.Add(new AdminModel
							{
								Id = int.Parse(sdr["id"].ToString()),
								Username = sdr["Username"].ToString(),
								Email = sdr["Email"].ToString(),
								Mobile = sdr["Mobile"].ToString(),
								Password = sdr["Password"].ToString(),
								EmployeeID = int.Parse(sdr["EmployeeId"].ToString()),
								Name = sdr["Name"].ToString(),
								UserRoleId = int.Parse(sdr["UserRoleId"].ToString()),
								OTP = strOTP
							});
						}
					}
					con.Close();
				}
			}
			return adminList;
		}
		//public async Task<bool> SaveDataDetails(AdminModel adminmodel)
		//{
		//    try
		//    {
		//        if (adminmodel.IsUpdate)
		//        {
		//            await _db.SaveData("SP_PSWRD", new
		//            {
		//                @id = adminmodel.Id,
		//                @Username = adminmodel.Username,
		//                @Email = adminmodel.Email,
		//                @Mobile = adminmodel.Mobile,
		//                @Password = Encrypt(adminmodel.EncryptedPassword),
		//                @mCallType = "INSERT_PSWRD"
		//            });
		//        }
		//        else
		//        {
		//            await _db.SaveData("SP_PSWRD", new
		//            {
		//                @id = adminmodel.Id,
		//                @Username = adminmodel.Username,
		//                @Email = adminmodel.Email,
		//                @Mobile = adminmodel.Mobile,
		//                @Password = Encrypt(adminmodel.EncryptedPassword),
		//                @mCallType = "UPDATE_PSWRD"
		//            });
		//        }

		//    }
		//    catch (Exception)
		//    {
		//        throw;
		//    }
		//    return true;
		//}
		//public async Task<AdminModel> GetDataById(int id)
		//{
		//    IEnumerable<AdminModel> result = await _db.GetData<AdminModel, dynamic>("SP_PSWRD", new { @Id = id, @mCallType = "SHOWIND_PSWRD" });
		//    return result.FirstOrDefault();
		//}
		//public async Task<bool> DeleteData(int id)
		//{
		//    try
		//    {
		//        await _db.SaveData("SP_PSWRD", new { @Id = id, @mCallType = "DELETE_PSWRD" });
		//        return true;
		//    }
		//    catch (Exception ex)
		//    {
		//        return false;
		//    }
		//}
		//private string Encrypt(string clearText)
		//{
		//    string encryptionKey = "MAKV2SPBNI99212";
		//    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
		//    using (Aes encryptor = Aes.Create())
		//    {
		//        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
		//        encryptor.Key = pdb.GetBytes(32);
		//        encryptor.IV = pdb.GetBytes(16);
		//        using (MemoryStream ms = new MemoryStream())
		//        {
		//            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
		//            {
		//                cs.Write(clearBytes, 0, clearBytes.Length);
		//                cs.Close();
		//            }
		//            clearText = Convert.ToBase64String(ms.ToArray());
		//        }
		//    }

		//    return clearText;
		//}

	}
}
