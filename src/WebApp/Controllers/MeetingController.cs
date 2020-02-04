using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Controllers {
    public class MeetingController : Controller {
        private readonly IMeetingServices _meeting;

        public MeetingController (IMeetingServices meeting) {
            _meeting = meeting;
        }

        public async Task<IActionResult> Index () {
            var data = await _meeting.GetAll<MeetingViewModel> ();
            return View (data);

        }

        // // GET: Meeting/Details/5
        // public async Task<IActionResult> Details (string id) {
        //     if (id == null) {
        //         return NotFound ();
        //     }

        //     var meetingViewModel = await _context.MeetingViewModel
        //         .FirstOrDefaultAsync (m => m.Guid == id);
        //     if (meetingViewModel == null) {
        //         return NotFound ();
        //     }

        //     return View (meetingViewModel);
        // }

        // // GET: Meeting/Create
        // public IActionResult Create () {
        //     return View ();
        // }

        // // POST: Meeting/Create
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create ([Bind ("Guid,Data,Name,Email,Active")] MeetingViewModel meetingViewModel) {
        //     if (ModelState.IsValid) {
        //         _context.Add (meetingViewModel);
        //         await _context.SaveChangesAsync ();
        //         return RedirectToAction (nameof (Index));
        //     }
        //     return View (meetingViewModel);
        // }

        // // GET: Meeting/Edit/5
        // public async Task<IActionResult> Edit (string id) {
        //     if (id == null) {
        //         return NotFound ();
        //     }

        //     var meetingViewModel = await _context.MeetingViewModel.FindAsync (id);
        //     if (meetingViewModel == null) {
        //         return NotFound ();
        //     }
        //     return View (meetingViewModel);
        // }

        // // POST: Meeting/Edit/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit (string id, [Bind ("Guid,Data,Name,Email,Active")] MeetingViewModel meetingViewModel) {
        //     if (id != meetingViewModel.Guid) {
        //         return NotFound ();
        //     }

        //     if (ModelState.IsValid) {
        //         try {
        //             _context.Update (meetingViewModel);
        //             await _context.SaveChangesAsync ();
        //         } catch (DbUpdateConcurrencyException) {
        //             if (!MeetingViewModelExists (meetingViewModel.Guid)) {
        //                 return NotFound ();
        //             } else {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction (nameof (Index));
        //     }
        //     return View (meetingViewModel);
        // }

        // // GET: Meeting/Delete/5
        // public async Task<IActionResult> Delete (string id) {
        //     if (id == null) {
        //         return NotFound ();
        //     }

        //     var meetingViewModel = await _context.MeetingViewModel
        //         .FirstOrDefaultAsync (m => m.Guid == id);
        //     if (meetingViewModel == null) {
        //         return NotFound ();
        //     }

        //     return View (meetingViewModel);
        // }

        // // POST: Meeting/Delete/5
        // [HttpPost, ActionName ("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed (string id) {
        //     var meetingViewModel = await _context.MeetingViewModel.FindAsync (id);
        //     _context.MeetingViewModel.Remove (meetingViewModel);
        //     await _context.SaveChangesAsync ();
        //     return RedirectToAction (nameof (Index));
        // }

        // private bool MeetingViewModelExists (string id) {
        //     return _context.MeetingViewModel.Any (e => e.Guid == id);
        // }
    }
}