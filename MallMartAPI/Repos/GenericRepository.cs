using MallMartDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMartAPI.Repos
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private MallMartDBContext _context = null;
        private DbSet<T> table = null;
        public GenericRepository(MallMartDBContext context)
        {
            this._context = context;
            table = context.Set<T>();
        }
        //public GenericRepository(MallMartDBContext _context)
        //{
        //    this._context = _context;
        //    table = _context.Set<T>();
        //}
        public async Task<IEnumerable<T>> GetAll()
        {
            var records = await table.ToListAsync();
            return records;
        }
        public async Task<T> GetById(object id)
        {
            var record = await table.FindAsync(id);
            return record;
        }
        public async Task<T> Insert(T obj)
        {
            _context.Entry(obj).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task<bool> Update(object id, T obj)
        {
            T exists = await table.FindAsync(id);

            if (exists is null)
            {
                return false;
            }
            else
            {
                _context.Entry(exists).State = EntityState.Detached; // Before this line EF tracks 2 entities with the
                                                                     // same Id: "exists" and "obj" and it fails to save. So
                                                                     // I ordered it to stop tracking "exists".
                                                                     // The new information is in "obj"
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
        }
        public async Task<bool> Delete(object id)
        {
            T exists = await table.FindAsync(id);

            if (exists is null)
            {
                return false;
            }
            else
            {
                table.Remove(exists);
                await _context.SaveChangesAsync();

                return true;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
