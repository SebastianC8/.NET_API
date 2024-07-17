using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Data;
using Repository.Data.DTO;

namespace Repository.Contracts
{
    public interface IPeopleRepository
    {
        public DataTable GetAll();
        public bool Save(User user);
        public StorePeopleDTO? Find(string DESCRIPTION, string PASSWORD);
    }
}
