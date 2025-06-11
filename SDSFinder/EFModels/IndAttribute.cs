using System;
using System.Collections.Generic;

namespace SDSFinder.EFModels;

public partial class IndAttribute
{
    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime RecordDate { get; set; }

    public Guid RowPointer { get; set; }

    public byte NoteExistsFlag { get; set; }

    public byte InWorkflow { get; set; }

    public int GroupId { get; set; }

    public int AttrId { get; set; }

    public string? AttrDesc { get; set; }

    public string? AttrType { get; set; }

    public string SiteRef { get; set; } = null!;
}
