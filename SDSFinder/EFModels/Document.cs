using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SDSFinder.EFModels;

[Table("Document")]
public partial class Document
{
    public int Id { get; set; }

    public string SafetyDocumentId { get; set; } = null!;
    [Key]
    public int DocumentId { get; set; }

    [StringLength(100)]
    public string FileName { get; set; } = null!;

    [StringLength(300)]
    public string? FileLocation { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public string? IsDeleted { get; set; }
}
