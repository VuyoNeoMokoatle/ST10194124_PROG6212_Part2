using System;

namespace PART2_PROG622.Models
{
    public class SystemFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Extention { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
