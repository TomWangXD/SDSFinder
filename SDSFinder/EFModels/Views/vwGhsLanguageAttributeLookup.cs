using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SDSFinder.EFModels;

[Keyless]
public partial class vwGhsLanguageAttributeLookup
{
    [Column("site_ref")]
    [StringLength(8)]
    public string SiteRef { get; set; } = null!;

    [Column("AttrID")]
    public int AttrId { get; set; }

    [Column("attr_desc")]
    [StringLength(60)]
    public string? AttrDesc { get; set; }

    [StringLength(200)]
    public string? AttrValue { get; set; }

    [Column("co_num")]
    [StringLength(10)]
    public string CoNum { get; set; } = null!;

    [Column("co_line")]
    public short CoLine { get; set; }

    [Column("co_release")]
    public short CoRelease { get; set; }
}
