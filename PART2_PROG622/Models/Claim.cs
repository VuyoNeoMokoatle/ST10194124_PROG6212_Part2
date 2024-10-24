using System;

namespace PART2_PROG622.Models
{
    public class Claim
    {
        // Unique identifier for the claim
        public int Id { get; set; }
        public int ClaimId { get; set; }

        // ID of the lecturer submitting the claim
        public string LecturerId { get; set; }

        // Name of the lecturer submitting the claim (optional if you get it from a user service)
        public string LecturerName { get; set; }

        // Date and time the claim was submitted
        public DateTime DateSubmitted { get; set; }

        // Status of the claim
        public ClaimStatus Status { get; set; }

        // Additional notes related to the claim
        public string Notes { get; set; }

        // Path to the supporting document uploaded with the claim
        public string SupportingDocumentPath { get; set; }

        // Total hours worked for this claim
        public decimal HoursWorked { get; set; }

        // Hourly rate associated with this claim
        public decimal HourlyRate { get; set; }

        // ID of the reviewer who reviews the claim
        public string ReviewerId { get; set; }

        // Comments from the reviewer about the claim
        public string ReviewerComments { get; set; }

        // Date and time when the claim was reviewed
        public DateTime? ReviewDate { get; set; }
        public string Title { get; internal set; }
        public int Amount { get; internal set; }
    }

    public enum ClaimStatus
    {
        Pending, // Claim is waiting for review
        Approved, // Claim has been approved
        Rejected // Claim has been rejected
    }
}


