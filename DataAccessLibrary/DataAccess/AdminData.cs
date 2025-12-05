using DataAccessLibrary.SqlDataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Util;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;



namespace DataAccessLibrary.DataAccess
{

    public class AdminData : IAdminData
    {
        private readonly ISqlDataAcess _db;
		private IConfiguration Configuration;
		public AdminData(ISqlDataAcess db, IConfiguration _configuration)
        {
            _db = db;
            Configuration = _configuration;

        }


        public List<AdminModel> GetAllData()

        {
			List<AdminModel> adminList = new List<AdminModel>();
			string constr = this.Configuration.GetConnectionString("conn");
			using (SqlConnection con = new SqlConnection(constr))
			{
				using (SqlCommand cmd = new SqlCommand("SELECT a.id,a.Username,a.Email,a.Mobile,a.Password,a.EmployeeId,Name=Isnull(b.Name,'Guest User') FROM (AdminTb a left join Employee b on a.EmployeeId=isnull(b.Id,0)) order by a.id DESC"))
				{
					cmd.Connection = con;
					con.Open();
					using (SqlDataReader sdr = cmd.ExecuteReader())
					{
						while (sdr.Read())
						{
							adminList.Add(new AdminModel
							{
								//int.Parse(SDR[0].ToString());
								Id = int.Parse(sdr["id"].ToString()),
								Username = sdr["Username"].ToString(),
								Email = sdr["Email"].ToString(),
								Mobile = sdr["Mobile"].ToString(),
								Password = sdr["Password"].ToString(),
                                EmployeeID = int.Parse(sdr["EmployeeId"].ToString()),
                                Name = sdr["Name"].ToString()
                                //DecryptedPassword = DecryptString(sdr["Password"].ToString(), "1")

                            });
						}
					}
					con.Close();
				}
			}
            return adminList;
		}
        public List<AdminModel> GetAllDataGuest()

        {
            List<AdminModel> adminList = new List<AdminModel>();
            string constr = this.Configuration.GetConnectionString("conn");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT a.id,a.Username,a.Email,a.Mobile,a.Password,a.EmployeeId,Name=Isnull(b.Name,'Guest User') FROM (AdminTb a left join Employee b on a.EmployeeId=isnull(b.Id,0)) where isnull(b.Id,0)=" + 0))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            adminList.Add(new AdminModel
                            {
                                //int.Parse(SDR[0].ToString());
                                Id = int.Parse(sdr["id"].ToString()),
                                Username = sdr["Username"].ToString(),
                                Email = sdr["Email"].ToString(),
                                Mobile = sdr["Mobile"].ToString(),
                                Password = sdr["Password"].ToString(),
                                EmployeeID = int.Parse(sdr["EmployeeId"].ToString()),
                                Name = sdr["Name"].ToString()
                                //DecryptedPassword = DecryptString(sdr["Password"].ToString(), "1")

                            });
                        }
                    }
                    con.Close();
                }
            }
            return adminList;
        }
        public List<AdminModel> GetAllDataInd(string userNm)

        {
            List<AdminModel> adminList = new List<AdminModel>();
            string constr = this.Configuration.GetConnectionString("conn");
            using (SqlConnection con = new SqlConnection(constr))
            {
                //using (SqlCommand cmd = new SqlCommand("SELECT id, Username, Email, Mobile, Password FROM AdminTb where userName='" + userNm + "'"))
                using (SqlCommand cmd = new SqlCommand("SELECT a.id,a.Username,a.Email,a.Mobile,a.Password,a.EmployeeId,Name=Isnull(b.Name,'Guest User') FROM (AdminTb a left join Employee b on a.EmployeeId=isnull(b.Id,0)) where userName='" + userNm + "'"))
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
                                Name = sdr["Name"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return adminList;
        }
        public async Task<bool> SaveDataDetails(AdminModel adminmodel)
        {
			Encrypt encrypt1 = new Encrypt();

			try
			{
                if (adminmodel.IsUpdate)
                {
                    await _db.SaveData("SP_PSWRD", new
                    {
                        @id = adminmodel.Id,
                        @Username = adminmodel.Username,
                        @Email = adminmodel.Email,
                        @Mobile = adminmodel.Mobile,
                        //@Password = adminmodel.Password,
                        @Password = encrypt1.EncryptString(adminmodel.Password, "1"),
						@EmployeeId = adminmodel.EmployeeID,
                        @UserRoleId = adminmodel.UserRoleId,
                        @mCallType = "UPDATE_PSWRD"
                    });
                }
                else
                {
                    await _db.SaveData("SP_PSWRD", new
                    {
                        @id = adminmodel.Id,
                        @Username = adminmodel.Username,
                        @Email = adminmodel.Email,
                        @Mobile = adminmodel.Mobile,
                        //@Password = adminmodel.Password,
                        @Password = encrypt1.EncryptString(adminmodel.Password, "1"),
                        @EmployeeId = adminmodel.EmployeeID,
                        //@Password = adminmodel.Password,
                        @mCallType = "INSERT_PSWRD"
                    });
                }

            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        public async Task<bool> SaveDataDetailsUsr(AdminModel adminmodel)
        {
            Encrypt encrypt1 = new Encrypt();
            if (adminmodel.Password == adminmodel.ConPassword)
            {
                try 
                {                
                    if (adminmodel.IsUpdate)
                    {
                        await _db.SaveData("SP_PSWRD", new
                        {
                            @id = adminmodel.Id,
                            @Username = adminmodel.Username,
                            @Email = adminmodel.Email,
                            @Mobile = adminmodel.Mobile,
                            @Password = encrypt1.EncryptString(adminmodel.Password, "1"),
                            //@ConPassword = adminmodel.Password,
                            @mCallType = "UPDATE_PSWRD1"
                        });
                    }
                    else
                    {
                        await _db.SaveData("SP_PSWRD", new
                        {
                            @id = adminmodel.Id,
                            @Username = adminmodel.Username,
                            @Email = adminmodel.Email,
                            @Mobile = adminmodel.Mobile,
                            @Password = encrypt1.EncryptString(adminmodel.Password, "1"),
                            //@Password = adminmodel.Password,
                            @mCallType = "INSERT_PSWRD1"
                        });
                    }

                }
                catch (Exception)
                {
                    throw;
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> SaveDataRegistrationUsr(AdminModel adminmodel)
        {
            Encrypt encrypt1 = new Encrypt();
            if (adminmodel.Password == adminmodel.ConPassword)
            {
                try
                {

                        await _db.SaveData("SP_PSWRD", new
                        {
                            @id = adminmodel.Id,
                            @Username = adminmodel.Username,
                            @Email = adminmodel.Email,
                            @Mobile = adminmodel.Mobile,
                            @Password = encrypt1.EncryptString(adminmodel.Password, "1"),
                            @mCallType = "INSERT_PSWRDREGI"
                        });
                    

                }
                catch (Exception)
                {
                    throw;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<AdminModel> GetDataById(int id)
        {
            IEnumerable<AdminModel> result = await _db.GetData<AdminModel, dynamic>("SP_PSWRD", new { @Id = id, @mCallType = "SHOWIND_PSWRD" });
            return result.FirstOrDefault();
        }
  
        public async Task<bool> DeleteData(int id)
        {
            try
            {
                await _db.SaveData("SP_PSWRD", new { @Id = id, @mCallType = "DELETE_PSWRD" });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //// This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        //// 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        //private const string initVector = "pemgail9uzpgzl88";
        //// This constant is used to determine the keysize of the encryption algorithm.
        //private const int keysize = 256;
        ////Encrypt
        //public string EncryptString(string plainText, string passPhrase)
        //{
        //    byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        //    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        //    PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        //    byte[] keyBytes = password.GetBytes(keysize / 8);
        //    RijndaelManaged symmetricKey = new RijndaelManaged();
        //    symmetricKey.Mode = CipherMode.CBC;
        //    ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        //    MemoryStream memoryStream = new MemoryStream();
        //    CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        //    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        //    cryptoStream.FlushFinalBlock();
        //    byte[] cipherTextBytes = memoryStream.ToArray();
        //    memoryStream.Close();
        //    cryptoStream.Close();
        //    return Convert.ToBase64String(cipherTextBytes);
        //}
        //public string DecryptString(string cipherText, string passPhrase)
        //{
        //    byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        //    byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        //    PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        //    byte[] keyBytes = password.GetBytes(keysize / 8);
        //    RijndaelManaged symmetricKey = new RijndaelManaged();
        //    symmetricKey.Mode = CipherMode.CBC;
        //    ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        //    MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        //    CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        //    byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        //    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        //    memoryStream.Close();
        //    cryptoStream.Close();
        //    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        //}
    }
}
