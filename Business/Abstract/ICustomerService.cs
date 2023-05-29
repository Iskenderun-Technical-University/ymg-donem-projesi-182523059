using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        //CRUD : CREATE + READ + UPDATE + DELETE
       
        IResult Add(Customer customer);
        IResult Update(Customer customer);
        IResult Delete(Customer customer);

        IDataResult<List<Customer>> GetCustomers();
        IDataResult<Customer> GetById(int id);
        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
        IDataResult<List<CustomerDetailDto>> GetCustomerDetailByUserId(int userId);


    }
}
