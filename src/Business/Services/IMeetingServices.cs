using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services {
    public interface IMeetingServices {
        Task Add (Meeting entity);
        Task Update (Meeting entity);
        Task Delete (Meeting entity);

        Task<Meeting> GetById (Guid id);
        Task<IEnumerable<Meeting>> GetAll<T> ();
        Task<IEnumerable<Meeting>> GetAll (Expression<Func<Meeting, bool>> where);
        Task<IEnumerable<Meeting>> GetAll (Expression<Func<Meeting, bool>> where = null, Func<IQueryable<Meeting>, IOrderedQueryable<Meeting>> orderBy = null,
            string includeProperties = "");

        Task<int> SaveChanges ();

    }
}