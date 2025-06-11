using System;
using System.Collections.Generic;

namespace SDSFinder.EFModels;

public partial class ItemGlbl
{
    public string Item { get; set; } = null!;

    public string? Description { get; set; }

    public string? UM { get; set; }

    public string? ShipSite { get; set; }

    public byte NoteExistsFlag { get; set; }

    public DateTime RecordDate { get; set; }

    public Guid RowPointer { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public byte InWorkflow { get; set; }

    public string? IndShapeCode { get; set; }

    public string? IndShapeDescription { get; set; }

    public string? IndAlloyCode { get; set; }

    public string? IndParticleCode { get; set; }

    public string? IndFluxCode { get; set; }

    public string? IndDangerGoodCode { get; set; }

    public decimal IndMetalPercent { get; set; }

    public decimal IndWeight { get; set; }

    public string? IndWeightUnits { get; set; }

    public byte IndEngineeringReq { get; set; }

    public byte IndMinMax { get; set; }

    public int? IndShelfLife { get; set; }

    public string? IndStorageReq { get; set; }

    public string? IndSalesCodeCode { get; set; }

    public decimal IndFluxPercent { get; set; }

    public byte IndFluxFused { get; set; }

    public string? IndHeatSpringCode { get; set; }

    public string? IndToolNumber { get; set; }

    public int? IndToolUps { get; set; }

    public decimal? IndRibbonWidth { get; set; }

    public decimal IndFluxTolPlus { get; set; }

    public decimal IndFluxTolMinus { get; set; }

    public string? IndPackagingItem { get; set; }

    public string? IndPackagingDescription { get; set; }

    public decimal IndPackagingFillQty { get; set; }

    public string? IndPackagingFillUm { get; set; }

    public byte? IndLeadFree { get; set; }

    public string? IndBaseItem { get; set; }

    public string? IndConfigurationId { get; set; }

    public string? IndSize { get; set; }

    public string? IndTools { get; set; }

    public byte IndConfigMustConfig { get; set; }

    public byte IndConfidentialMaterial { get; set; }

    public decimal? IndWeightPlastic { get; set; }

    public string? IndWeightPlasticUnits { get; set; }

    public string? IndBrandName { get; set; }

    public byte? IndIncludePlasticTax { get; set; }

    public decimal? IndPitch { get; set; }

    public decimal? IndWidth { get; set; }

    public string? IndShapeMmdescription { get; set; }

    public string? IndUfi { get; set; }

    public string? IndSds { get; set; }

    public virtual ItemGlbl? IndPackagingItemNavigation { get; set; }

    public virtual ICollection<ItemGlbl> InverseIndPackagingItemNavigation { get; set; } = new List<ItemGlbl>();
}
