using Repository.Data;
using Repository.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IPeopleCore
    {
        public List<User> GetAll();

        public bool Save(StorePeopleDTO user);

        public StorePeopleDTO Find(string DESCRIPTION, string PASSWORD);
    }
}
