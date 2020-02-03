using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories {
    public class Repository<T> : IRepository<T> where T : Entity {

        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _DbSet;

        public Repository (AppDbContext context) {
            _context = context;
            _DbSet = _context.Set<T> ();
        }

        public async Task Add (T entity) {
            _DbSet.Add (entity);
            await SaveChanges ();
        }

        public async Task Update (T entity) {
            _DbSet.Update (entity);
            await SaveChanges ();
        }

        public async Task Delete (T entity) {
            _DbSet.Remove (entity);
            await SaveChanges ();
        }

        public void Dispose () {
            _context?.Dispose ();
        }

        public async Task<IEnumerable<T>> GetAll () {
            return await _DbSet.ToListAsync ();
        }

        public async Task<IEnumerable<T>> GetAll (Expression<Func<T, bool>> where) {
            return await _DbSet.Where (where).ToListAsync ();
        }

        public async Task<IEnumerable<T>> GetAll (Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "") {
            IQueryable<T> query = _DbSet;

            foreach (var includeProperty in includeProperties.Split (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include (includeProperty);
            }

            if (orderBy != null) {
                return await orderBy (query).ToListAsync ();
            } else {
                return await query.ToListAsync ();
            }

        }

        public async Task<T> GetById (Guid id) {
            return await _DbSet.FindAsync (id);
        }

        public async Task<int> SaveChanges () {
            return await _context.SaveChangesAsync ();
        }

    }
}