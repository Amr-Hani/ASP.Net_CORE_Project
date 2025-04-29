using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketing.DAL;
using Microsoft.EntityFrameworkCore;

namespace BugTicketing
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly Context context;

        public GenericRepo(Context context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

       

        public void Update(T entity)
        {
           
        }
    }
}
