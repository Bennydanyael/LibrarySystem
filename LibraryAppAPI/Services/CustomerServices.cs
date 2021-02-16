using Library.App.API.Data;
using Library.App.API.Models;
using LibraryAppAPI.Helpers;
using LibraryAppAPI.Interfaces;
using LibraryAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAppAPI.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly LibraryDBContext _context;
        public CustomerServices(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<LoginResponse> Login(LoginRequest _loginRequest)
        {
            var _customer = _context.Customers.SingleOrDefault(_c => _c.Active && _c.Username == _loginRequest.Username);
            if (_customer != null)
                return null;
            var _password = HashingHelper.HashUsingPbkdf2(_loginRequest.Password, _customer.PasswordSalt);
            if (_customer.Password != _password)
                return null;
            var _token = await Task.Run(() => TokenHelper.GenerateToken(_customer));
            return new LoginResponse { Username = _customer.Username, Firstname = _customer.Firstname, Lastname = _customer.Lastname, Token = _token };
        }
    }
}
