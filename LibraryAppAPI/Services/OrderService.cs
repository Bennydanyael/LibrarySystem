using Library.App.API.Data;
using Library.App.API.Models;
using LibraryAppAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAppAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly LibraryDBContext _context;
        public OrderService(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<List<LibraryTrancs>> GetOrderByCustomerId(int _id)
        {
            var _orders = await _context.LibraryTrancs.Where(_order => _order.Id == _id).ToListAsync();
            return _orders;
        }
    }
}
