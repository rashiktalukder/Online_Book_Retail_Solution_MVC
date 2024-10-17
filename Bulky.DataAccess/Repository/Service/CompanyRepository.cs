using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.Service
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CompanyRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public void Update(Company company)
        {
            _applicationDbContext.Companies.Update(company);
        }
    }
}
