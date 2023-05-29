using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataAccess.Concrete.EntityFramework.Context;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal: EfEntityRepositoryBase<Customer, RentACarContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetailDto(Expression<Func<Customer, bool>> filter = null)
        {
            using (ReCapDbContext context = new ReCapDbContext())
            {
                var result = from customer in filter is null ? context.Customers : context.Customers.Where(filter)

                             join user in context.Users on customer.UserId equals user.Id
                             select new CustomerDetailDto()
                             {
                                 CustomerId = customer.Id,
                                 CompanyName = customer.CompanyName,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 FindexScore = customer.FindexScore
                             };
                return result.ToList();
            }
        }
    }
}
