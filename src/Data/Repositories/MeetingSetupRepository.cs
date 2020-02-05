using Business.Interfaces;
using Business.Models;
using Data.Context;

namespace Data.Repositories {
    public class MeetingSetupRepository : Repository<MeetingSetup>, IMeetingSetupRepository {
        public MeetingSetupRepository (AppDbContext context) : base (context) { }
    }
}