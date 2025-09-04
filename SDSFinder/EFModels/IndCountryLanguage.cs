using System;
using System.Collections.Generic;

namespace SDSFinder.EFModels;

public partial class IndCountryLanguage
{
    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime RecordDate { get; set; }

    public Guid RowPointer { get; set; }

    public byte NoteExistsFlag { get; set; }

    public byte InWorkflow { get; set; }

    public string IndCountry { get; set; } = null!;

    public string IndLanguageCode { get; set; } = null!;

    public string? IndSdslanguage { get; set; }

    public string? IndSdsregion { get; set; }
}
