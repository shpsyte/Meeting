using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Business.Notifications;
using Business.Validations;

namespace Business.Services {
    public class MeetingSetupServices : BaseServices, IMeetingSetupServices {

        private readonly IMeetingSetupRepository _meettingSetup;

        public MeetingSetupServices (IMeetingSetupRepository meettingSetup, INotificador notificador) : base (notificador) {
            _meettingSetup = meettingSetup;
        }

        public async Task Add (MeetingSetup entity) {
            if (!ExecutarValidacao (new MeetingSetupValidation (), entity)) return;

            await _meettingSetup.Add (entity);
        }

        public async Task Delete (MeetingSetup entity) {
            await _meettingSetup.Delete (entity);
        }

        public async Task<IEnumerable<MeetingSetup>> GetAll () {

            return await _meettingSetup.GetAll ();
        }

        public async Task<IEnumerable<MeetingSetup>> GetAll (Expression<Func<MeetingSetup, bool>> where) {
            return await _meettingSetup.GetAll (where);
        }

        public async Task<IEnumerable<MeetingSetup>> GetAll (Expression<Func<MeetingSetup, bool>> where = null, Func<IQueryable<MeetingSetup>, IOrderedQueryable<MeetingSetup>> orderBy = null, string includeProperties = "") {
            return await _meettingSetup.GetAll (where, orderBy, includeProperties);
        }

        public async Task<MeetingSetup> GetById (Guid id) {
            return await _meettingSetup.GetById (id);
        }

        public async Task<int> SaveChanges () {
            return await _meettingSetup.SaveChanges ();
        }

        public async Task Update (MeetingSetup entity) {
            await _meettingSetup.Update (entity);
        }
    }
}