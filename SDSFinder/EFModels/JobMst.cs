using System;
using System.Collections.Generic;

namespace SDSFinder.EFModels;

public partial class JobMst
{
    public string SiteRef { get; set; } = null!;

    public string? Type { get; set; }

    public string Job { get; set; } = null!;

    public short Suffix { get; set; }

    public DateTime? JobDate { get; set; }

    public string? CustNum { get; set; }

    public string? OrdType { get; set; }

    public string? OrdNum { get; set; }

    public short? OrdLine { get; set; }

    public short? OrdRelease { get; set; }

    public string? EstJob { get; set; }

    public short? EstSuf { get; set; }

    public string Item { get; set; } = null!;

    public decimal QtyReleased { get; set; }

    public decimal? QtyComplete { get; set; }

    public decimal? QtyScrapped { get; set; }

    public string? Stat { get; set; }

    public DateTime? LstTrxDate { get; set; }

    public string? RootJob { get; set; }

    public short? RootSuf { get; set; }

    public string? RefJob { get; set; }

    public short RefSuf { get; set; }

    public int RefOper { get; set; }

    public short? RefSeq { get; set; }

    public byte? LowLevel { get; set; }

    public DateTime? EffectDate { get; set; }

    public string? WipAcct { get; set; }

    public decimal? WipTotal { get; set; }

    public decimal? WipComplete { get; set; }

    public decimal? WipSpecial { get; set; }

    public string? Revision { get; set; }

    public byte? Picked { get; set; }

    public string? Whse { get; set; }

    public string? JcbAcct { get; set; }

    public string? PsNum { get; set; }

    public string? WipLbrAcct { get; set; }

    public string? WipFovhdAcct { get; set; }

    public string? WipVovhdAcct { get; set; }

    public string? WipOutAcct { get; set; }

    public decimal? WipMatlTotal { get; set; }

    public decimal? WipLbrTotal { get; set; }

    public decimal? WipFovhdTotal { get; set; }

    public decimal? WipVovhdTotal { get; set; }

    public decimal? WipOutTotal { get; set; }

    public decimal? WipMatlComp { get; set; }

    public decimal? WipLbrComp { get; set; }

    public decimal? WipFovhdComp { get; set; }

    public decimal? WipVovhdComp { get; set; }

    public decimal? WipOutComp { get; set; }

    public DateTime? RollupDate { get; set; }

    public string? WipAcctUnit1 { get; set; }

    public string? WipAcctUnit2 { get; set; }

    public string? WipAcctUnit3 { get; set; }

    public string? WipAcctUnit4 { get; set; }

    public string? JcbAcctUnit1 { get; set; }

    public string? JcbAcctUnit2 { get; set; }

    public string? JcbAcctUnit3 { get; set; }

    public string? JcbAcctUnit4 { get; set; }

    public string? WipLbrAcctUnit1 { get; set; }

    public string? WipLbrAcctUnit2 { get; set; }

    public string? WipLbrAcctUnit3 { get; set; }

    public string? WipLbrAcctUnit4 { get; set; }

    public string? WipFovhdAcctUnit1 { get; set; }

    public string? WipFovhdAcctUnit2 { get; set; }

    public string? WipFovhdAcctUnit3 { get; set; }

    public string? WipFovhdAcctUnit4 { get; set; }

    public string? WipVovhdAcctUnit1 { get; set; }

    public string? WipVovhdAcctUnit2 { get; set; }

    public string? WipVovhdAcctUnit3 { get; set; }

    public string? WipVovhdAcctUnit4 { get; set; }

    public string? WipOutAcctUnit1 { get; set; }

    public string? WipOutAcctUnit2 { get; set; }

    public string? WipOutAcctUnit3 { get; set; }

    public string? WipOutAcctUnit4 { get; set; }

    public string? ProdMix { get; set; }

    public byte NoteExistsFlag { get; set; }

    public DateTime RecordDate { get; set; }

    public Guid RowPointer { get; set; }

    public string? Description { get; set; }

    public string? ConfigId { get; set; }

    public byte? CoProductMix { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public byte InWorkflow { get; set; }

    public byte Scheduled { get; set; }

    public string RcptRqmt { get; set; } = null!;

    public string ExportType { get; set; } = null!;

    public byte ContainsTaxFreeMatl { get; set; }

    public DateTime? MidnightOfJobSchEndDate { get; set; }

    public DateTime? MidnightOfJobSchCompdate { get; set; }

    public byte Rework { get; set; }

    public byte UnlinkedXref { get; set; }

    public string? ProspectId { get; set; }

    public byte IsExternal { get; set; }

    public byte PreassignLots { get; set; }

    public byte PreassignSerials { get; set; }

    public string? ConfigDocId { get; set; }

    public string? MoBomAlternateId { get; set; }

    public string? MoBomAlternateDescription { get; set; }

    public byte MoCoJob { get; set; }

    public int? MoProductCycle { get; set; }

    public decimal? MoQtyPerCycle { get; set; }

    public string? MoJobDescription { get; set; }

    public string? IndCustNum { get; set; }

    public int? IndCustSeq { get; set; }

    public string? IndCustItem { get; set; }

    public string? IndSite { get; set; }

    public string? IndPackProcess { get; set; }

    public int? IndQtyrequiredOfScrap { get; set; }
}
