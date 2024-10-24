using Microsoft.AspNetCore.Mvc;
using PART2_PROG622.Models;
using PART2_PROG622.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PART2_PROG622.Controllers
{
    public class FileCreationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileCreationController> _logger;

        public FileCreationController(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            ILogger<FileCreationController> logger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public IActionResult CreateFile()
        {
            return View(new FileUploadViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFile(FileUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Validate file size (e.g., 10MB max)
                    if (model.File.Length > 10 * 1024 * 1024)
                    {
                        ModelState.AddModelError("File", "File size must be less than 10MB");
                        return View(model);
                    }

                    // Validate file type
                    var allowedTypes = new[] { ".pdf", ".docx", ".xlsx", ".doc", ".xls" };
                    var fileExtension = Path.GetExtension(model.File.FileName).ToLowerInvariant();
                    if (!allowedTypes.Contains(fileExtension))
                    {
                        ModelState.AddModelError("File", "Invalid file type");
                        return View(model);
                    }

                    // Create upload directory if it doesn't exist
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate unique filename
                    var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    // Create file record
                    var fileCreation = new FileCreation
                    {
                        FileName = model.File.FileName,
                        FilePath = uniqueFileName,
                        Description = model.Description,
                        UploadedBy = User.Identity?.Name ?? "Anonymous",
                        UploadDate = DateTime.UtcNow,
                        ClaimId = model.ClaimId,
                        FileType = fileExtension,
                        FileSize = model.File.Length,
                        IsConfidential = model.IsConfidential,
                        ContentType = model.File.ContentType
                    };

                    _context.FileCreations.Add(fileCreation);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "File uploaded successfully!";
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error uploading file");
                    ModelState.AddModelError("", "Error uploading file. Please try again.");
                }
            }

            return View(model);
        }


        // GET: FileCreation/ManageFiles
        public async Task<IActionResult> ManageFiles()
        {
            var files = await _context.FileCreations.ToListAsync(); // Get all uploaded files
            return View(files);
        }

        public async Task<IActionResult> Download(int id)
        {
            var file = await _context.FileCreations.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", file.FilePath);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, file.ContentType, file.FileName);
        }

        // POST: FileCreation/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var file = await _context.FileCreations.FindAsync(id);
            if (file != null)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", file.FilePath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath); // Delete file from server
                }

                _context.FileCreations.Remove(file);
                await _context.SaveChangesAsync();
                TempData["Success"] = "File deleted successfully!";
            }
            else
            {
                TempData["Error"] = "File not found.";
            }
            return RedirectToAction(nameof(ManageFiles));
        }
    }
}
