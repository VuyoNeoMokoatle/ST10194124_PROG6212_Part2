using System.ComponentModel.DataAnnotations;

namespace PART2_PROG622.Models
{
    public class FileUploadViewModel
    {
        [Required(ErrorMessage = "Please select a file")]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Please provide a description")]
        public string Description { get; set; }

        public string FileType { get; set; }

        [Display(Name = "Related Claim ID")]
        public int? ClaimId { get; set; }

        public bool IsConfidential { get; set; }
        public Claim Claim { get; internal set; }
    }
}

