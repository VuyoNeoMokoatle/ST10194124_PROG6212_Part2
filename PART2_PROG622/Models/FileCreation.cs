using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PART2_PROG622.Models
{
    public class FileCreation
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        public string Description { get; set; }

        [Required]
        public string UploadedBy { get; set; }

        public string UploadedOn { get; set; }

        public DateTime UploadDate { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public int? ClaimId { get; set; }
        public virtual Claim Claim { get; set; }

        public string FileType { get; set; }

        public long FileSize { get; set; }

        public bool IsConfidential { get; set; }

        public string ContentType { get; set; }

        // Adding Status property
        public FileStatus Status { get; set; }

        // Adding File property for upload handling
        [NotMapped] // This ensures the property isn't mapped to the database
        public IFormFile File { get; set; }
    }

    public enum FileStatus
    {
        Pending,
        Processed,
        Rejected,
        Archived
    }
}
