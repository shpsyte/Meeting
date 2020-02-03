using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;
namespace Business.Interfaces {
    public interface IMeetingRepository : IRepository<Meeting> {
        Task<IEnumerable<Meeting>> GellAllMeeting ();

    }
}