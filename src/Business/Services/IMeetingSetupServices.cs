using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services {
    public interface IMeetingSetupServices : IDisposable {
        Task Add (MeetingSetup entity);
        Task Update (MeetingSetup entity);
        Task Delete (MeetingSetup entity);

        Task<MeetingSetup> GetById (Guid id);
        Task<IEnumerable<MeetingSetup>> GetAll ();
        Task<IEnumerable<MeetingSetup>> GetAll (Expression<Func<MeetingSetup, bool>> where);
        Task<IEnumerable<MeetingSetup>> GetAll (Expression<Func<MeetingSetup, bool>> where = null, Func<IQueryable<MeetingSetup>, IOrderedQueryable<MeetingSetup>> orderBy = null,
            string includeProperties = "");

        Task<MeetingSetup> GetAtualMeeting ();

        Task<int> SaveChanges ();

    }
}