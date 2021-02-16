using Library.App.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.App.API.Services
{
    public class EFCoreRepository<IEntity, TContext> : IRepository<IEntity>
        where IEntity : class, Models.IEntity
        where TContext : LibraryDBContext
    {
        private readonly TContext _context;
        public EFCoreRepository(TContext context)
        {
            _context = context;
        }

        public async Task<IEntity> Add(IEntity _entity)
        {
            _context.Set<IEntity>().Add(_entity);
            await _context.SaveChangesAsync();
            return _entity;
        }

        public async Task<IEntity> Delete(int _id)
        {
            var _entity = await _context.Set<IEntity>().FindAsync(_id);
            if (_entity == null)
                return _entity;
            _context.Set<IEntity>().Remove(_entity);
            await _context.SaveChangesAsync();
            return _entity;
        }

        public async Task<IEntity> Get(int _id)
        {
            return await _context.Set<IEntity>().FindAsync(_id);
        }

        public List<IEntity> GetAll()
        {
            return _context.Set<IEntity>().ToList();
        }

        public async Task<IEntity> Update(IEntity _entity)
        {
            _context.Entry(_entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return _entity;
        }
    }
}
