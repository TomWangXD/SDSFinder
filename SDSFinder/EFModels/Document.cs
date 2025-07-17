using System;
using System.Collections.Generic;

namespace SDSFinder.EFModels;

public partial class Document
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? SafetyDocumentId { get; set; }

    public string FileName { get; set; } = null!;

    public string? FileLocation { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}
