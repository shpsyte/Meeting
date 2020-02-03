using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories {
    public class MeetingRepository : Repository<Meeting>, IMeetingRepository {
        public MeetingRepository (AppDbContext context) : base (context) { }

        public async Task<IEnumerable<Meeting>> GellAllMeeting () {
            return await _DbSet.ToListAsync ();
        }
    }
}