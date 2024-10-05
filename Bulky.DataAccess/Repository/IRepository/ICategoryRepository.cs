using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        //Create, Get, Delete are implemented to the repository. ICategoryReos is inheriting all methods or IRepository. so here we need to implement only which is not present in the iRepository
        void Update(Category category);
    }
}
