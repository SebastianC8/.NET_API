using Core.Contracts;
using Repository.Contracts;
using Repository.Data;
using Repository.Data.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Implementation
{
    public class PeopleImplCore: IPeopleCore
    {
        private static IPeopleRepository peopleRepository;

        public PeopleImplCore()
        {
            peopleRepository = (IPeopleRepository)CoreServiceProvider.Provider.GetService(typeof(IPeopleRepository));
        }

        public List<User> GetAll()
        {
            try
            {
                var response = peopleRepository.GetAll();
                List<User> users = new List<User>();

                foreach (DataRow row in response.Rows)
                {
                    User user = new User();
                    user.ID = (int)row["ID"];
                    user.DESCRIPTION = row["DESCRIPTION"].ToString();

                    users.Add(user);
                }

                return users;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Save(StorePeopleDTO people)
        {
            User user = new User();
            
            user.ID = (int) people.ID;
            user.DESCRIPTION = people.DESCRIPTION;

            return peopleRepository.Save(user);
        }

        public StorePeopleDTO Find(string DESCRIPTION, string PASSWORD)
        {
            try
            {
                var response = peopleRepository.Find(DESCRIPTION, PASSWORD);
                return (response != null) ? response : null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
