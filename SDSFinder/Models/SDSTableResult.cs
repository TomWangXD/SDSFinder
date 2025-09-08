namespace SDSFinder.Models
{
    public class SDSTableResult
    {
        public string? ItemNumber { get; set; }
        [Required]
        public string? ItemDescription { get; set; }
        [Required]
        public string PdfLocation { get; set; }
        public string? Language { get; set; }
        public string? Region { get; set; }
    }
}
