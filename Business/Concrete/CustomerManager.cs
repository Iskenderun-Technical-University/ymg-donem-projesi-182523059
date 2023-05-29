using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDAL _customerDAL;
        Random _random;
        public CustomerManager(ICustomerDAL customerDAL)
        {
            _customerDAL = customerDAL;
            _random = new Random();

        }

        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("ICustomerService.Get")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            customer.FindexScore = _random.Next(0, 1900);
            _customerDAL.Add(customer);
            return new SuccessResult();
        }

        [SecuredOperation("customers.add,admin")]
        [CacheRemoveAspect("ICustomerService.Get")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Delete(Customer customer)
        {
            _customerDAL.Delete(customer);
            return new SuccessResult();
        }

        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("ICustomerService.Get")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            _customerDAL.Update(customer);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<Customer>> GetCustomers()
        {
            return new SuccessDataResult<List<Customer>>(_customerDAL.GetAll());
        }

        [CacheAspect]
        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDAL.Get(c => c.Id == id));
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDAL.GetCustomerDetailDto());
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetailByUserId(int userId)
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDAL.GetCustomerDetailDto(u => u.UserId == userId));
        }
    }
}
