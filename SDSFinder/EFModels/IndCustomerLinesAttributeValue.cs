using System;
using System.Collections.Generic;

namespace SDSFinder.EFModels;

public partial class IndCustomerLinesAttributeValue
{
    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime RecordDate { get; set; }

    public Guid RowPointer { get; set; }

    public byte NoteExistsFlag { get; set; }

    public byte InWorkflow { get; set; }

    public string SiteRef { get; set; } = null!;

    public int AttrId { get; set; }

    public string? AttrValue { get; set; }

    public int? CustSeq { get; set; }

    public int GroupId { get; set; }

    public string? Item { get; set; }

    public int Seqnum { get; set; }

    public string? CustNum { get; set; }

    public string? ProductCode { get; set; }

    public string? FamilyCode { get; set; }

    public string Source { get; set; } = null!;

    public string CoNum { get; set; } = null!;

    public short CoLine { get; set; }

    public string? CustItem { get; set; }

    public short CoRelease { get; set; }

    public byte? Active { get; set; }

    public string? Approvedby { get; set; }

    public DateTime? Effectivedate { get; set; }

    public string? Reason { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? Zip { get; set; }
}
