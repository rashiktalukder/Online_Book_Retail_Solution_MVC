using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        //Create, Get, Delete are implemented to the repository. ICompanyRepos is inheriting all methods from IRepository. so here we need to implement only which is not present in the iRepository
        void Update(Company company);
    }
}
