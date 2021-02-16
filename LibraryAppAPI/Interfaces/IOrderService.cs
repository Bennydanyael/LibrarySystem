using Library.App.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAppAPI.Interfaces
{
    public interface IOrderService
    {
        Task<List<LibraryTrancs>> GetOrderByCustomerId(int _id);
    }
}
