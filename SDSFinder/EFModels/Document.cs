using System;
using System.Collections.Generic;

namespace SDSFinder.EFModels;

public partial class Document
{
    public int Id { get; set; }

    public string SafetyDocumentId { get; set; } = null!;
    [Key]
    public int DocumentId { get; set; }

    public string FileName { get; set; } = null!;

    public string? FileLocation { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? IsDeleted { get; set; }
}
