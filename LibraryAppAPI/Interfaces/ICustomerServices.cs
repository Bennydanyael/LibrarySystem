using LibraryAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAppAPI.Interfaces
{
    public interface ICustomerServices
    {
        Task<LoginResponse> Login(LoginRequest _loginRequest);
    }
}
