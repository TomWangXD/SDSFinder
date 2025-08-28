using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SDSFinder.EFModels;

namespace SDSFinder.EFContexts;

public partial class IndAppContext : DbContext
{
    public IndAppContext()
    {
    }

    public IndAppContext(DbContextOptions<IndAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<IndAttribute> IndAttributes { get; set; }

    public virtual DbSet<IndCountryLanguage> IndCountryLanguages { get; set; }

    public virtual DbSet<IndCustomerLinesAttributeValue> IndCustomerLinesAttributeValues { get; set; }

    public virtual DbSet<ItemGlbl> ItemGlbls { get; set; }

    public virtual DbSet<JobMst> JobMsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sl10-dev-db.ica.com; Database=IND_App;Integrated Security=true;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IndAttribute>(entity =>
        {
            entity.HasKey(e => new { e.SiteRef, e.GroupId, e.AttrId });

            entity.ToTable("IND_Attributes", "dbo", tb =>
                {
                    tb.HasTrigger("IND_AttributesDel");
                    tb.HasTrigger("IND_AttributesInsert");
                    tb.HasTrigger("IND_AttributesUpdatePenultimate");
                });

            entity.HasIndex(e => e.RowPointer, "IX_IND_Attributes_RowPointer").IsUnique();

            entity.Property(e => e.SiteRef)
                .HasMaxLength(8)
                .HasDefaultValueSql("(rtrim(CONVERT([nvarchar](8),context_info(),(0))))")
                .HasColumnName("site_ref");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.AttrId).HasColumnName("AttrID");
            entity.Property(e => e.AttrDesc)
                .HasMaxLength(60)
                .HasColumnName("Attr_Desc");
            entity.Property(e => e.AttrType)
                .HasMaxLength(10)
                .HasColumnName("Attr_Type");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RowPointer).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasDefaultValueSql("(suser_sname())");
        });

        modelBuilder.Entity<IndCountryLanguage>(entity =>
        {
            entity.HasKey(e => new { e.IndCountry, e.IndLanguageCode }).IsClustered(false);

            entity.ToTable("IND_CountryLanguage", "dbo", tb =>
                {
                    tb.HasTrigger("IND_CountryLanguageDel");
                    tb.HasTrigger("IND_CountryLanguageInsert");
                    tb.HasTrigger("IND_CountryLanguageUpdatePenultimate");
                });

            entity.HasIndex(e => e.RowPointer, "IX_IND_CountryLanguage_RowPointer").IsUnique();

            entity.Property(e => e.IndCountry)
                .HasMaxLength(30)
                .HasColumnName("IND_Country");
            entity.Property(e => e.IndLanguageCode)
                .HasMaxLength(6)
                .HasColumnName("IND_LanguageCode");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.IndSdslanguage)
                .HasMaxLength(3)
                .HasColumnName("IND_SDSLanguage");
            entity.Property(e => e.IndSdsregion)
                .HasMaxLength(20)
                .HasColumnName("IND_SDSRegion");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RowPointer).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasDefaultValueSql("(suser_sname())");
        });

        modelBuilder.Entity<IndCustomerLinesAttributeValue>(entity =>
        {
            entity.HasKey(e => new { e.SiteRef, e.Seqnum, e.GroupId, e.AttrId, e.CoNum, e.CoLine, e.CoRelease });

            entity.ToTable("IND_CustomerLines_AttributeValue", "dbo", tb =>
                {
                    tb.HasTrigger("IND_CustomerLines_AttributeValueInsert");
                    tb.HasTrigger("IND_CustomerLines_AttributeValueUpdatePenultimate");
                });

            entity.HasIndex(e => e.RowPointer, "IX_IND_CustomerLines_AttributeValue_RowPointer").IsUnique();

            entity.Property(e => e.SiteRef)
                .HasMaxLength(8)
                .HasDefaultValueSql("(rtrim(CONVERT([nvarchar](8),context_info(),(0))))")
                .HasColumnName("site_ref");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.AttrId).HasColumnName("AttrID");
            entity.Property(e => e.CoNum)
                .HasMaxLength(10)
                .HasColumnName("co_num");
            entity.Property(e => e.CoLine).HasColumnName("co_line");
            entity.Property(e => e.CoRelease).HasColumnName("co_release");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Approvedby)
                .HasMaxLength(128)
                .HasColumnName("approvedby");
            entity.Property(e => e.AttrValue).HasMaxLength(200);
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .HasColumnName("country");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.CustItem)
                .HasMaxLength(30)
                .HasColumnName("cust_item");
            entity.Property(e => e.CustNum)
                .HasMaxLength(7)
                .HasColumnName("cust_num");
            entity.Property(e => e.CustSeq).HasColumnName("cust_seq");
            entity.Property(e => e.Effectivedate)
                .HasColumnType("datetime")
                .HasColumnName("effectivedate");
            entity.Property(e => e.FamilyCode)
                .HasMaxLength(10)
                .HasColumnName("family_code");
            entity.Property(e => e.Item).HasMaxLength(30);
            entity.Property(e => e.ProductCode)
                .HasMaxLength(10)
                .HasColumnName("product_code");
            entity.Property(e => e.Reason)
                .HasMaxLength(60)
                .HasColumnName("reason");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RowPointer).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Source).HasMaxLength(60);
            entity.Property(e => e.State)
                .HasMaxLength(5)
                .HasColumnName("state");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.Zip)
                .HasMaxLength(10)
                .HasColumnName("zip");
        });

        modelBuilder.Entity<ItemGlbl>(entity =>
        {
            entity.HasKey(e => e.Item);

            entity.ToTable("item_glbl", "dbo", tb =>
                {
                    tb.HasTrigger("IND_item_glblIup");
                    tb.HasTrigger("item_glblDel");
                    tb.HasTrigger("item_glblInsert");
                    tb.HasTrigger("item_glblUpdatePenultimate");
                });

            entity.HasIndex(e => new { e.IndAlloyCode, e.RowPointer }, "IND_IX_item_glbl_AlloyCode");

            entity.HasIndex(e => new { e.IndDangerGoodCode, e.RowPointer }, "IND_IX_item_glbl_DangerGoodCode");

            entity.HasIndex(e => e.IndFluxCode, "IND_IX_item_glbl_FluxCode");

            entity.HasIndex(e => e.IndPackagingDescription, "IND_IX_item_glbl_PackagingDescription");

            entity.HasIndex(e => e.IndPackagingItem, "IND_IX_item_glbl_PackagingItem");

            entity.HasIndex(e => new { e.IndParticleCode, e.RowPointer }, "IND_IX_item_glbl_ParticleCode");

            entity.HasIndex(e => e.IndSalesCodeCode, "IND_IX_item_glbl_SalesCodeCode");

            entity.HasIndex(e => new { e.IndShapeCode, e.RowPointer }, "IND_IX_item_glbl_ShapeCode");

            entity.HasIndex(e => e.RowPointer, "IX_item_glbl_RowPointer").IsUnique();

            entity.HasIndex(e => e.Description, "IX_item_glbl_desc");

            entity.HasIndex(e => e.UM, "IX_item_glbl_u_m");

            entity.Property(e => e.Item)
                .HasMaxLength(30)
                .HasColumnName("item");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.Description)
                .HasMaxLength(40)
                .HasColumnName("description");
            entity.Property(e => e.IndAlloyCode)
                .HasMaxLength(40)
                .HasColumnName("IND_AlloyCode");
            entity.Property(e => e.IndBaseItem)
                .HasMaxLength(30)
                .HasColumnName("IND_BaseItem");
            entity.Property(e => e.IndBrandName)
                .HasMaxLength(40)
                .HasColumnName("IND_brand_name");
            entity.Property(e => e.IndConfidentialMaterial).HasColumnName("IND_ConfidentialMaterial");
            entity.Property(e => e.IndConfigMustConfig).HasColumnName("IND_ConfigMustConfig");
            entity.Property(e => e.IndConfigurationId)
                .HasMaxLength(30)
                .HasColumnName("IND_ConfigurationID");
            entity.Property(e => e.IndDangerGoodCode)
                .HasMaxLength(40)
                .HasColumnName("IND_DangerGoodCode");
            entity.Property(e => e.IndEngineeringReq).HasColumnName("IND_EngineeringReq");
            entity.Property(e => e.IndFluxCode)
                .HasMaxLength(40)
                .HasColumnName("IND_FluxCode");
            entity.Property(e => e.IndFluxFused).HasColumnName("IND_FluxFused");
            entity.Property(e => e.IndFluxPercent)
                .HasColumnType("decimal(30, 6)")
                .HasColumnName("IND_FluxPercent");
            entity.Property(e => e.IndFluxTolMinus)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("IND_FluxTolMinus");
            entity.Property(e => e.IndFluxTolPlus)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("IND_FluxTolPlus");
            entity.Property(e => e.IndHeatSpringCode)
                .HasMaxLength(20)
                .HasColumnName("IND_HeatSpringCode");
            entity.Property(e => e.IndIncludePlasticTax).HasColumnName("IND_IncludePlasticTax");
            entity.Property(e => e.IndLeadFree)
                .HasDefaultValue((byte)0)
                .HasColumnName("IND_LeadFree");
            entity.Property(e => e.IndMetalPercent)
                .HasColumnType("decimal(30, 6)")
                .HasColumnName("IND_MetalPercent");
            entity.Property(e => e.IndMinMax).HasColumnName("IND_MinMax");
            entity.Property(e => e.IndPackagingDescription)
                .HasMaxLength(255)
                .HasColumnName("IND_PackagingDescription");
            entity.Property(e => e.IndPackagingFillQty)
                .HasColumnType("decimal(19, 8)")
                .HasColumnName("IND_PackagingFillQty");
            entity.Property(e => e.IndPackagingFillUm)
                .HasMaxLength(3)
                .HasColumnName("IND_PackagingFillUM");
            entity.Property(e => e.IndPackagingItem)
                .HasMaxLength(30)
                .HasColumnName("IND_PackagingItem");
            entity.Property(e => e.IndParticleCode)
                .HasMaxLength(40)
                .HasColumnName("IND_ParticleCode");
            entity.Property(e => e.IndPitch)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IND_Pitch");
            entity.Property(e => e.IndRibbonWidth)
                .HasColumnType("decimal(30, 6)")
                .HasColumnName("IND_RibbonWidth");
            entity.Property(e => e.IndSalesCodeCode)
                .HasMaxLength(40)
                .HasColumnName("IND_SalesCodeCode");
            entity.Property(e => e.IndSds)
                .HasMaxLength(60)
                .HasColumnName("IND_SDS");
            entity.Property(e => e.IndShapeCode)
                .HasMaxLength(15)
                .HasColumnName("IND_ShapeCode");
            entity.Property(e => e.IndShapeDescription)
                .HasMaxLength(255)
                .HasColumnName("IND_ShapeDescription");
            entity.Property(e => e.IndShapeMmdescription)
                .HasMaxLength(255)
                .HasColumnName("IND_ShapeMMDescription");
            entity.Property(e => e.IndShelfLife).HasColumnName("IND_ShelfLife");
            entity.Property(e => e.IndSize)
                .HasMaxLength(40)
                .HasColumnName("IND_Size");
            entity.Property(e => e.IndStorageReq)
                .HasMaxLength(40)
                .HasColumnName("IND_StorageReq");
            entity.Property(e => e.IndToolNumber)
                .HasMaxLength(30)
                .HasColumnName("IND_ToolNumber");
            entity.Property(e => e.IndToolUps).HasColumnName("IND_ToolUps");
            entity.Property(e => e.IndTools)
                .HasMaxLength(255)
                .HasColumnName("IND_Tools");
            entity.Property(e => e.IndUfi)
                .HasMaxLength(30)
                .HasColumnName("IND_UFI");
            entity.Property(e => e.IndWeight)
                .HasColumnType("decimal(11, 5)")
                .HasColumnName("IND_Weight");
            entity.Property(e => e.IndWeightPlastic)
                .HasColumnType("decimal(11, 5)")
                .HasColumnName("IND_WeightPlastic");
            entity.Property(e => e.IndWeightPlasticUnits)
                .HasMaxLength(3)
                .HasColumnName("IND_WeightPlasticUnits");
            entity.Property(e => e.IndWeightUnits)
                .HasMaxLength(3)
                .HasColumnName("IND_WeightUnits");
            entity.Property(e => e.IndWidth)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IND_Width");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RowPointer).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ShipSite)
                .HasMaxLength(8)
                .HasColumnName("ship_site");
            entity.Property(e => e.UM)
                .HasMaxLength(3)
                .HasColumnName("u_m");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasDefaultValueSql("(suser_sname())");

            entity.HasOne(d => d.IndPackagingItemNavigation).WithMany(p => p.InverseIndPackagingItemNavigation)
                .HasForeignKey(d => d.IndPackagingItem)
                .HasConstraintName("IND_item_glblPackagingItemFK");
        });

        modelBuilder.Entity<JobMst>(entity =>
        {
            entity.HasKey(e => new { e.Job, e.Suffix, e.SiteRef });

            entity.ToTable("job_mst", "dbo", tb =>
                {
                    tb.HasTrigger("IND_job_mstIup");
                    tb.HasTrigger("job_mstDel");
                    tb.HasTrigger("job_mstInsert");
                    tb.HasTrigger("job_mstIup");
                    tb.HasTrigger("job_mstPreDel");
                    tb.HasTrigger("job_mstUpdatePenultimate");
                });

            entity.HasIndex(e => new { e.RowPointer, e.SiteRef }, "IX_job_mst_RowPointer").IsUnique();

            entity.HasIndex(e => new { e.CustNum, e.SiteRef }, "IX_job_mst_cust").HasFillFactor(90);

            entity.HasIndex(e => new { e.Item, e.Job, e.Suffix, e.Type, e.Stat, e.SiteRef }, "IX_job_mst_item_job").HasFillFactor(90);

            entity.HasIndex(e => new { e.Type, e.Item, e.MidnightOfJobSchCompdate, e.RcptRqmt, e.RowPointer, e.SiteRef }, "IX_job_mst_item_midnight_of_job_sch_compdate").IsUnique();

            entity.HasIndex(e => new { e.Type, e.Item, e.MidnightOfJobSchEndDate, e.RcptRqmt, e.RowPointer, e.SiteRef }, "IX_job_mst_item_midnight_of_job_sch_end_date").IsUnique();

            entity.HasIndex(e => new { e.Item, e.Suffix, e.Type, e.SiteRef }, "IX_job_mst_item_suffix").HasFillFactor(90);

            entity.HasIndex(e => new { e.OrdNum, e.OrdLine, e.OrdRelease, e.SiteRef }, "IX_job_mst_ord_num").HasFillFactor(90);

            entity.HasIndex(e => new { e.PsNum, e.Type, e.SiteRef }, "IX_job_mst_ps_num_type").HasFillFactor(90);

            entity.HasIndex(e => new { e.RootJob, e.RootSuf, e.SiteRef }, "IX_job_mst_root_job").HasFillFactor(90);

            entity.HasIndex(e => new { e.Whse, e.SiteRef }, "IX_job_mst_whse").HasFillFactor(90);

            entity.Property(e => e.Job)
                .HasMaxLength(20)
                .HasColumnName("job");
            entity.Property(e => e.Suffix).HasColumnName("suffix");
            entity.Property(e => e.SiteRef)
                .HasMaxLength(8)
                .HasDefaultValueSql("(rtrim(CONVERT([nvarchar](8),context_info(),(0))))")
                .HasColumnName("site_ref");
            entity.Property(e => e.CoProductMix)
                .HasDefaultValue((byte)0)
                .HasColumnName("co_product_mix");
            entity.Property(e => e.ConfigDocId)
                .HasMaxLength(32)
                .HasColumnName("config_doc_id");
            entity.Property(e => e.ConfigId)
                .HasMaxLength(32)
                .HasColumnName("config_id");
            entity.Property(e => e.ConsumesFill).HasColumnName("consumes_fill");
            entity.Property(e => e.ContainsTaxFreeMatl).HasColumnName("contains_tax_free_matl");
            entity.Property(e => e.CopiedFromPmfFormulaRowPointer).HasColumnName("copied_from_pmf_formula_RowPointer");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.CustNum)
                .HasMaxLength(7)
                .HasColumnName("cust_num");
            entity.Property(e => e.Description)
                .HasMaxLength(40)
                .HasColumnName("description");
            entity.Property(e => e.EffectDate)
                .HasColumnType("datetime")
                .HasColumnName("effect_date");
            entity.Property(e => e.EstJob)
                .HasMaxLength(20)
                .HasColumnName("est_job");
            entity.Property(e => e.EstSuf)
                .HasDefaultValue((short)0)
                .HasColumnName("est_suf");
            entity.Property(e => e.ExportType)
                .HasMaxLength(1)
                .HasDefaultValue("N")
                .IsFixedLength()
                .HasColumnName("export_type");
            entity.Property(e => e.IndCustItem)
                .HasMaxLength(30)
                .HasColumnName("IND_cust_item");
            entity.Property(e => e.IndCustNum)
                .HasMaxLength(7)
                .HasColumnName("IND_cust_num");
            entity.Property(e => e.IndCustSeq).HasColumnName("IND_cust_seq");
            entity.Property(e => e.IndPackProcess)
                .HasMaxLength(40)
                .HasColumnName("IND_PackProcess");
            entity.Property(e => e.IndQtyrequiredOfScrap).HasColumnName("IND_QTYRequiredOfScrap");
            entity.Property(e => e.IndSite)
                .HasMaxLength(8)
                .HasColumnName("IND_Site");
            entity.Property(e => e.IsExternal).HasColumnName("is_external");
            entity.Property(e => e.Item)
                .HasMaxLength(30)
                .HasColumnName("item");
            entity.Property(e => e.JcbAcct)
                .HasMaxLength(12)
                .HasColumnName("jcb_acct");
            entity.Property(e => e.JcbAcctUnit1)
                .HasMaxLength(4)
                .HasColumnName("jcb_acct_unit1");
            entity.Property(e => e.JcbAcctUnit2)
                .HasMaxLength(4)
                .HasColumnName("jcb_acct_unit2");
            entity.Property(e => e.JcbAcctUnit3)
                .HasMaxLength(4)
                .HasColumnName("jcb_acct_unit3");
            entity.Property(e => e.JcbAcctUnit4)
                .HasMaxLength(4)
                .HasColumnName("jcb_acct_unit4");
            entity.Property(e => e.JobDate)
                .HasColumnType("datetime")
                .HasColumnName("job_date");
            entity.Property(e => e.LowLevel)
                .HasDefaultValue((byte)0)
                .HasColumnName("low_level");
            entity.Property(e => e.LstTrxDate)
                .HasColumnType("datetime")
                .HasColumnName("lst_trx_date");
            entity.Property(e => e.MidnightOfJobSchCompdate)
                .HasColumnType("datetime")
                .HasColumnName("midnight_of_job_sch_compdate");
            entity.Property(e => e.MidnightOfJobSchEndDate)
                .HasColumnType("datetime")
                .HasColumnName("midnight_of_job_sch_end_date");
            entity.Property(e => e.MoBomAlternateDescription)
                .HasMaxLength(40)
                .HasColumnName("MO_bom_alternate_description");
            entity.Property(e => e.MoBomAlternateId)
                .HasMaxLength(30)
                .HasColumnName("MO_bom_alternate_id");
            entity.Property(e => e.MoCoJob).HasColumnName("MO_co_job");
            entity.Property(e => e.MoJobDescription)
                .HasMaxLength(40)
                .HasColumnName("MO_job_description");
            entity.Property(e => e.MoProductCycle)
                .HasDefaultValue(0)
                .HasColumnName("MO_product_cycle");
            entity.Property(e => e.MoQtyPerCycle)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(19, 8)")
                .HasColumnName("MO_qty_per_cycle");
            entity.Property(e => e.OrdLine)
                .HasDefaultValue((short)0)
                .HasColumnName("ord_line");
            entity.Property(e => e.OrdNum)
                .HasMaxLength(10)
                .HasColumnName("ord_num");
            entity.Property(e => e.OrdRelease)
                .HasDefaultValue((short)0)
                .HasColumnName("ord_release");
            entity.Property(e => e.OrdType)
                .HasMaxLength(1)
                .HasDefaultValue("I")
                .IsFixedLength()
                .HasColumnName("ord_type");
            entity.Property(e => e.Picked)
                .HasDefaultValue((byte)0)
                .HasColumnName("picked");
            entity.Property(e => e.Plant)
                .HasMaxLength(8)
                .HasColumnName("plant");
            entity.Property(e => e.PmfFormulaRowPointer).HasColumnName("pmf_formula_RowPointer");
            entity.Property(e => e.PmfMfgSpecOrderRowPointer).HasColumnName("pmf_mfg_spec_order_RowPointer");
            entity.Property(e => e.PmfPnBatchRowPointer).HasColumnName("pmf_pn_batch_RowPointer");
            entity.Property(e => e.PnBatchJobType).HasColumnName("pn_batch_job_type");
            entity.Property(e => e.PreassignLots).HasColumnName("preassign_lots");
            entity.Property(e => e.PreassignSerials).HasColumnName("preassign_serials");
            entity.Property(e => e.ProdMix)
                .HasMaxLength(7)
                .HasColumnName("prod_mix");
            entity.Property(e => e.ProducesFill).HasColumnName("produces_fill");
            entity.Property(e => e.ProspectId)
                .HasMaxLength(7)
                .HasColumnName("prospect_id");
            entity.Property(e => e.PsNum)
                .HasMaxLength(10)
                .HasColumnName("ps_num");
            entity.Property(e => e.QtyComplete)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(19, 8)")
                .HasColumnName("qty_complete");
            entity.Property(e => e.QtyReleased)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(19, 8)")
                .HasColumnName("qty_released");
            entity.Property(e => e.QtyScrapped)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(19, 8)")
                .HasColumnName("qty_scrapped");
            entity.Property(e => e.RcptRqmt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasComputedColumnSql("('C')", false)
                .HasColumnName("rcpt_rqmt");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RefJob)
                .HasMaxLength(20)
                .HasColumnName("ref_job");
            entity.Property(e => e.RefOper).HasColumnName("ref_oper");
            entity.Property(e => e.RefSeq)
                .HasDefaultValue((short)0)
                .HasColumnName("ref_seq");
            entity.Property(e => e.RefSuf).HasColumnName("ref_suf");
            entity.Property(e => e.Revision)
                .HasMaxLength(8)
                .HasColumnName("revision");
            entity.Property(e => e.Rework).HasColumnName("rework");
            entity.Property(e => e.RollupDate)
                .HasColumnType("datetime")
                .HasColumnName("rollup_date");
            entity.Property(e => e.RootJob)
                .HasMaxLength(20)
                .HasColumnName("root_job");
            entity.Property(e => e.RootSuf)
                .HasDefaultValue((short)0)
                .HasColumnName("root_suf");
            entity.Property(e => e.RowPointer).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Scheduled).HasColumnName("scheduled");
            entity.Property(e => e.Stat)
                .HasMaxLength(1)
                .HasDefaultValue("F")
                .IsFixedLength()
                .HasColumnName("stat");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .HasDefaultValue("E")
                .IsFixedLength()
                .HasColumnName("type");
            entity.Property(e => e.UnlinkedXref).HasColumnName("unlinked_xref");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasDefaultValueSql("(suser_sname())");
            entity.Property(e => e.Whse)
                .HasMaxLength(4)
                .HasColumnName("whse");
            entity.Property(e => e.WipAcct)
                .HasMaxLength(12)
                .HasColumnName("wip_acct");
            entity.Property(e => e.WipAcctUnit1)
                .HasMaxLength(4)
                .HasColumnName("wip_acct_unit1");
            entity.Property(e => e.WipAcctUnit2)
                .HasMaxLength(4)
                .HasColumnName("wip_acct_unit2");
            entity.Property(e => e.WipAcctUnit3)
                .HasMaxLength(4)
                .HasColumnName("wip_acct_unit3");
            entity.Property(e => e.WipAcctUnit4)
                .HasMaxLength(4)
                .HasColumnName("wip_acct_unit4");
            entity.Property(e => e.WipComplete)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_complete");
            entity.Property(e => e.WipFovhdAcct)
                .HasMaxLength(12)
                .HasColumnName("wip_fovhd_acct");
            entity.Property(e => e.WipFovhdAcctUnit1)
                .HasMaxLength(4)
                .HasColumnName("wip_fovhd_acct_unit1");
            entity.Property(e => e.WipFovhdAcctUnit2)
                .HasMaxLength(4)
                .HasColumnName("wip_fovhd_acct_unit2");
            entity.Property(e => e.WipFovhdAcctUnit3)
                .HasMaxLength(4)
                .HasColumnName("wip_fovhd_acct_unit3");
            entity.Property(e => e.WipFovhdAcctUnit4)
                .HasMaxLength(4)
                .HasColumnName("wip_fovhd_acct_unit4");
            entity.Property(e => e.WipFovhdComp)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_fovhd_comp");
            entity.Property(e => e.WipFovhdTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_fovhd_total");
            entity.Property(e => e.WipLbrAcct)
                .HasMaxLength(12)
                .HasColumnName("wip_lbr_acct");
            entity.Property(e => e.WipLbrAcctUnit1)
                .HasMaxLength(4)
                .HasColumnName("wip_lbr_acct_unit1");
            entity.Property(e => e.WipLbrAcctUnit2)
                .HasMaxLength(4)
                .HasColumnName("wip_lbr_acct_unit2");
            entity.Property(e => e.WipLbrAcctUnit3)
                .HasMaxLength(4)
                .HasColumnName("wip_lbr_acct_unit3");
            entity.Property(e => e.WipLbrAcctUnit4)
                .HasMaxLength(4)
                .HasColumnName("wip_lbr_acct_unit4");
            entity.Property(e => e.WipLbrComp)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_lbr_comp");
            entity.Property(e => e.WipLbrTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_lbr_total");
            entity.Property(e => e.WipMatlComp)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_matl_comp");
            entity.Property(e => e.WipMatlTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_matl_total");
            entity.Property(e => e.WipOutAcct)
                .HasMaxLength(12)
                .HasColumnName("wip_out_acct");
            entity.Property(e => e.WipOutAcctUnit1)
                .HasMaxLength(4)
                .HasColumnName("wip_out_acct_unit1");
            entity.Property(e => e.WipOutAcctUnit2)
                .HasMaxLength(4)
                .HasColumnName("wip_out_acct_unit2");
            entity.Property(e => e.WipOutAcctUnit3)
                .HasMaxLength(4)
                .HasColumnName("wip_out_acct_unit3");
            entity.Property(e => e.WipOutAcctUnit4)
                .HasMaxLength(4)
                .HasColumnName("wip_out_acct_unit4");
            entity.Property(e => e.WipOutComp)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_out_comp");
            entity.Property(e => e.WipOutTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_out_total");
            entity.Property(e => e.WipSpecial)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_special");
            entity.Property(e => e.WipTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_total");
            entity.Property(e => e.WipVovhdAcct)
                .HasMaxLength(12)
                .HasColumnName("wip_vovhd_acct");
            entity.Property(e => e.WipVovhdAcctUnit1)
                .HasMaxLength(4)
                .HasColumnName("wip_vovhd_acct_unit1");
            entity.Property(e => e.WipVovhdAcctUnit2)
                .HasMaxLength(4)
                .HasColumnName("wip_vovhd_acct_unit2");
            entity.Property(e => e.WipVovhdAcctUnit3)
                .HasMaxLength(4)
                .HasColumnName("wip_vovhd_acct_unit3");
            entity.Property(e => e.WipVovhdAcctUnit4)
                .HasMaxLength(4)
                .HasColumnName("wip_vovhd_acct_unit4");
            entity.Property(e => e.WipVovhdComp)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_vovhd_comp");
            entity.Property(e => e.WipVovhdTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(23, 8)")
                .HasColumnName("wip_vovhd_total");
        });
        modelBuilder.HasSequence("IND_EDI_AckNum", "dbo");
        modelBuilder.HasSequence("IND_EDI_AsnNum", "dbo");
        modelBuilder.HasSequence("IND_LabSampleLabelSequence", "dbo")
            .StartsAt(580000L)
            .HasMin(0L);
        modelBuilder.HasSequence("IND_SpecHeaderSequence", "dbo")
            .StartsAt(370L)
            .HasMin(370L);
        modelBuilder.HasSequence<decimal>("VariationIDSequence", "dbo")
            .StartsAt(63854411899713L)
            .HasMin(-9223372036854775808L)
            .HasMax(9223372036854775807L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
