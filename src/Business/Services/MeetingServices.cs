using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Notifications;
using Business.Validations;

namespace Business.Services {
    public class MeetingServices : BaseServices, IMeetingServices {

        private IMeetingRepository _meeting;

        public MeetingServices (IMeetingRepository meetingRepository, INotificador notificador) : base (notificador) {
            _meeting = meetingRepository;
        }

        public async Task Add (Meeting entity) {
            if (!ExecutarValidacao (new MeetingValidation (), entity)) return;

            await _meeting.Add (entity);
        }

        public async Task Delete (Meeting entity) {
            await _meeting.Delete (entity);
        }

        public async Task<IEnumerable<T>> GetAll<T> () {
            IMapper map = new Mapper (null);

            var res = map.Map<IEnumerable<T>> (await _meeting.GetAll ());
            return res;
        }

        public async Task<IEnumerable<Meeting>> GetAll (Expression<Func<Meeting, bool>> where) {
            return await _meeting.GetAll (where);
        }

        public async Task<IEnumerable<Meeting>> GetAll (Expression<Func<Meeting, bool>> where = null, Func<IQueryable<Meeting>, IOrderedQueryable<Meeting>> orderBy = null, string includeProperties = "") {
            return await _meeting.GetAll (where, orderBy, includeProperties);
        }

        public async Task<Meeting> GetById (Guid id) {
            return await _meeting.GetById (id);
        }

        public async Task<int> SaveChanges () {
            return await _meeting.SaveChanges ();
        }

        public async Task Update (Meeting entity) {
            await _meeting.Update (entity);
        }
    }
}