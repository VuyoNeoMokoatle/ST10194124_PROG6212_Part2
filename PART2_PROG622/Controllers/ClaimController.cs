using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PART2_PROG622.Models;
using PART2_PROG622.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace PART2_PROG622.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string? submit;

        public ClaimController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Submit()
        {
            return View(new Claim());  // Pass a new Claim object to the view
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Claim claim, IFormFile supportingDocument)
        {
            if (ModelState.IsValid)
            {
                if (supportingDocument != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + supportingDocument.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await supportingDocument.CopyToAsync(fileStream);
                    }

                    claim.SupportingDocumentPath = uniqueFileName;
                }

                claim.DateSubmitted = DateTime.Now;
                claim.Status = ClaimStatus.Pending;
                claim.LecturerId = User.Identity.Name;

                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageClaims));
            }
            return View(claim);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int ClaimId, ClaimStatus status, string reviewerComments)
        {
            var claim = await _context.Claims.FindAsync(ClaimId);
            if (claim == null)
            {
                return NotFound();
            }

            claim.Status = status;
            claim.ReviewerComments = reviewerComments;
            claim.ReviewDate = DateTime.Now;
            claim.ReviewerId = User.Identity.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageClaims));
        }
        public async Task<IActionResult> ManageClaims()
        {
            var claims = await _context.Claims.ToListAsync(); // Adjust according to your DbSet
            return View(claims);
        }
    }
}
