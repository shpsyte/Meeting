using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using Business.Notifications;
using Business.Validations;

namespace Business.Services {
    public class MeetingServices : BaseServices, IMeetingServices {

        private readonly IMeetingRepository _meeting;

        public MeetingServices (IMeetingRepository meetingRepository, INotificador notificador) : base (notificador) {
            _meeting = meetingRepository;

        }

        public async Task Add (Meeting entity) {
            if (!ExecutarValidacao (new MeetingValidation (), entity)) return;

            //Valid if the exists the same email in this day
            var any = (await _meeting.GetAll (a => a.Email == entity.Email && a.Data.ToSql () == DateTime.Now.ToSql ())).Any ();
            if (any) {
                Notificar ("This Email is already registered");
            }

            await _meeting.Add (entity);
        }

        public async Task Delete (Meeting entity) {
            await _meeting.Delete (entity);
        }

        public async Task<IEnumerable<Meeting>> GetAll () {

            return await _meeting.GetAll ();
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

        public void Dispose () {
            _meeting?.Dispose ();
        }

        public async Task<bool> CheckIfIsAlreadyRegistered (Meeting entity) {
            return (await GetAll (a => a.Data.ToSql () == DateTime.Now.ToSql () && a.Email == entity.Email)).Any ();
        }

        public async Task<IEnumerable<Meeting>> GetAllParticipantsToday () {
            return (await GetAll (a => a.Data.ToSql () == DateTime.Now.ToSql ()));
        }

        public async Task<Meeting> GetNewMeeting () {
            var data = await Task.FromResult (
                new Meeting () {
                    Data = DateTime.UtcNow,
                        Id = Guid.NewGuid ()
                }
            );

            return data;
        }

    }
}