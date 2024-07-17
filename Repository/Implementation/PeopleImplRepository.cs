using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Repository.Contracts;
using Repository.Data;
using Repository.Data.DTO;
using Repository.Utilities.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class PeopleImplRepository : IPeopleRepository
    {
        public DataTable GetAll()
        {
			try
			{
				DataTable people = new DataTable();

				using(SqlConnection sqlConnection = ConnectionDB.Connection())
				{
					sqlConnection.Open();
					SqlCommand sqlCommand = sqlConnection.CreateCommand();
					sqlCommand.CommandText = "SP_GET_ALL_PEOPLE";
					sqlCommand.CommandTimeout = 0;
					sqlCommand.CommandType = CommandType.StoredProcedure;

					people.Load(sqlCommand.ExecuteReader());
					return people;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }

		public bool Save(User user)
		{
            try
            {
                bool response = false;

                using (SqlConnection sqlConnection = ConnectionDB.Connection())
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandText = "SP_CREATE_PEOPLE";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", user.ID));
                    sqlCommand.Parameters.Add(new SqlParameter("@NAME", user.DESCRIPTION));
                    sqlCommand.CommandTimeout = 0;

                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StorePeopleDTO? Find(string DESCRIPTION, string PASSWORD)
        {
            try
            {
                string jsonFile = @"C:\Users\SEBASTIAN\source\repos\Capacitacion\DB\Data\JSON\users-data.json";
                string jsonContent = File.ReadAllText(jsonFile);

                List<StorePeopleDTO>? people = JsonConvert.DeserializeObject<List<StorePeopleDTO>>(jsonContent);

                StorePeopleDTO? find = (
                    from person in people
                    where person.DESCRIPTION == DESCRIPTION && person.PASSWORD == PASSWORD
                    select person
                ).FirstOrDefault();

                return find;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
