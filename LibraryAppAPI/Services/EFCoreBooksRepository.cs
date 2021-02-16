using Library.App.API.Data;
using Library.App.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.App.API.Services
{
    public class EFCoreBooksRepository : EFCoreRepository<Books, LibraryDBContext>
    {
        public EFCoreBooksRepository(LibraryDBContext context):base(context)
        {
        }
    }
}
