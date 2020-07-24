﻿using Auto.IRepository;
using Auto.IRepository.IRepositories;
using Auto.IService.IServices;
using Auto.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auto.Service.Services
{
    public class EmployeeService : ServiceBase<Employee>, IEmployeeService
    {
        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {
        }
    }
}
