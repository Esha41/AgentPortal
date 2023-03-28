using Intelli.AgentPortal.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

#nullable disable

namespace Intelli.AgentPortal.Domain.Database
{
    public partial class AgentPortalContext
    {
        public AgentPortalContext()
        {
        }

        public AgentPortalContext(DbContextOptions<AgentPortalContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("SqlConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public virtual DbSet<AdditionalDocumentsOrdering> AdditionalDocumentsOrderings { get; set; }
        public virtual DbSet<AdvancedLogging> AdvancedLoggings { get; set; }
        public virtual DbSet<AdvancedLogging2> AdvancedLogging2s { get; set; }
        public virtual DbSet<AppPage> AppPages { get; set; }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetReseller> AspNetResellers { get; set; }
      
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<ColumnPreference> ColumnPreferences { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DocumentClasses> DocumentClasses { get; set; }
        public virtual DbSet<DocumentGroupName> DocumentGroupNames { get; set; }
        public virtual DbSet<DocumentsPerCompany> DocumentsPerCompany { get; set; }
        public virtual DbSet<PasswordHistory> PasswordHistories { get; set; }
        public virtual DbSet<RoleScreen> RoleScreens { get; set; }
        public virtual DbSet<RoleScreenColumn> RoleScreenColumns { get; set; }
        public virtual DbSet<RoleScreenElement> RoleScreenElements { get; set; }
        public virtual DbSet<Screen> Screens { get; set; }
        public virtual DbSet<ScreenColumn> ScreenColumns { get; set; }
        public virtual DbSet<ScreenElement> ScreenElements { get; set; }
        public virtual DbSet<SystemRole> SystemRoles { get; set; }
        public virtual DbSet<SystemUser> SystemUsers { get; set; }
        public virtual DbSet<SystemUserCountry> SystemUserCountries { get; set; }
        public virtual DbSet<SystemUserRole> SystemUserRoles { get; set; }
        public virtual DbSet<UserCompany> UserCompanies { get; set; }
        public virtual DbSet<UserPreference> UserPreferences { get; set; }
        public virtual DbSet<UserSession> UserSessions { get; set; }

        public virtual DbSet<Audit> Audits { get; set; }
       
        public virtual DbSet<BatchAppPagesHistory> BatchAppPagesHistories { get; set; }
        public virtual DbSet<BatchContractSignConfig> BatchContractSignConfigs { get; set; }
        public virtual DbSet<BatchHistory> BatchHistories { get; set; }
        public virtual DbSet<BatchHistoryExpectedDocument> BatchHistoryExpectedDocuments { get; set; }
        public virtual DbSet<BatchHistoryItem> BatchHistoryItems { get; set; }
        public virtual DbSet<BatchHistoryItemField> BatchHistoryItemFields { get; set; }
        public virtual DbSet<BatchHistoryItemPage> BatchHistoryItemPages { get; set; }
        public virtual DbSet<BatchHistoryJsonResult> BatchHistoryJsonResults { get; set; }
        public virtual DbSet<BatchHistoryMetum> BatchHistoryMeta { get; set; }
        public virtual DbSet<BatchItem> BatchItems { get; set; }
        public virtual DbSet<BatchItemField> BatchItemFields { get; set; }
        public virtual DbSet<BatchItemPage> BatchItemPages { get; set; }
        public virtual DbSet<BatchItemStatus> BatchItemStatuses { get; set; }
        public virtual DbSet<BatchMetum> BatchMeta { get; set; }
        public virtual DbSet<BatchSource> BatchSources { get; set; }
        public virtual DbSet<BatchSourceAppPage> BatchSourceAppPages { get; set; }
        public virtual DbSet<BatchSourceUploadDoc> BatchSourceUploadDocs { get; set; }
        public virtual DbSet<BatchStatus> BatchStatuses { get; set; }
        public virtual DbSet<BatchVideoPriority> BatchVideoPriorities { get; set; }
        public virtual DbSet<BatchVideoStatus> BatchVideoStatuses { get; set; }
        public virtual DbSet<BatchWithReseller> BatchWithResellers { get; set; }
        public virtual DbSet<BatchmetaToDelete> BatchmetaToDeletes { get; set; }
        public virtual DbSet<BopConfig> BopConfigs { get; set; }
        public virtual DbSet<BopDictionariesMissingValue> BopDictionariesMissingValues { get; set; }
        public virtual DbSet<BopDictionary> BopDictionaries { get; set; }
       
        public virtual DbSet<CompaniesMonitoringCofiguration> CompaniesMonitoringCofigurations { get; set; }
       
        
        public virtual DbSet<CountriesPerCompany> CountriesPerCompanies { get; set; }
        
        public virtual DbSet<DboVwDocumentGroupNamesForBatchHistory> DboVwDocumentGroupNamesForBatchHistories { get; set; }
        public virtual DbSet<DictionaryType> DictionaryTypes { get; set; }
       
        public virtual DbSet<DocumentClassField> DocumentClassFields { get; set; }
        public virtual DbSet<DocumentClassFieldType> DocumentClassFieldTypes { get; set; }
        
        public virtual DbSet<DocumentRejectionReason> DocumentRejectionReasons { get; set; }
        public virtual DbSet<DocumentSignConfig> DocumentSignConfigs { get; set; }
        public virtual DbSet<DocumentsPerBatchSource> DocumentsPerBatchSources { get; set; }
      
        public virtual DbSet<InfoDocClassField> InfoDocClassFields { get; set; }
        public virtual DbSet<Nlog> Nlogs { get; set; }
       
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceLanguage> ResourceLanguages { get; set; }
       
        public virtual DbSet<ViewBatchesToBeDeleted> ViewBatchesToBeDeleteds { get; set; }
        public virtual DbSet<VwAgentStatsPerCompanyMonthly> VwAgentStatsPerCompanyMonthlies { get; set; }
        public virtual DbSet<VwAlphaBankReport> VwAlphaBankReports { get; set; }
        public virtual DbSet<VwAnalysisForFailedTransaction> VwAnalysisForFailedTransactions { get; set; }
        public virtual DbSet<VwAppPagesWorkflowPerBatchSource> VwAppPagesWorkflowPerBatchSources { get; set; }
        public virtual DbSet<VwAppPagesWorkflowStepsPerBatchSource> VwAppPagesWorkflowStepsPerBatchSources { get; set; }
        public virtual DbSet<VwBatchContractSignConfig> VwBatchContractSignConfigs { get; set; }
        public virtual DbSet<VwBatchStatusForTransactionsInVideoCallQueue> VwBatchStatusForTransactionsInVideoCallQueues { get; set; }
        public virtual DbSet<VwCompaniesWithOtpmessageMonthly> VwCompaniesWithOtpmessageMonthlies { get; set; }
        public virtual DbSet<VwDocumentGroupNamePerBatchSource> VwDocumentGroupNamePerBatchSources { get; set; }
        public virtual DbSet<VwDocumentGroupNamePerBatchSourceBatchHistoryItem> VwDocumentGroupNamePerBatchSourceBatchHistoryItems { get; set; }
        public virtual DbSet<VwDocumentGroupNamesFormBatchHistory> VwDocumentGroupNamesFormBatchHistories { get; set; }
        public virtual DbSet<VwFlowOrdering> VwFlowOrderings { get; set; }
        public virtual DbSet<VwFriendlyBatchSourceAppPage> VwFriendlyBatchSourceAppPages { get; set; }
        public virtual DbSet<VwGetBatchHistoryItemField> VwGetBatchHistoryItemFields { get; set; }
        public virtual DbSet<VwGetBatchHistoryMeta> VwGetBatchHistoryMetas { get; set; }
        public virtual DbSet<VwGetBatchItemField> VwGetBatchItemFields { get; set; }
        public virtual DbSet<VwGetBatchesForBatchHistoryHubNotification> VwGetBatchesForBatchHistoryHubNotifications { get; set; }
        public virtual DbSet<VwGetBatchesForPublishing> VwGetBatchesForPublishings { get; set; }
        public virtual DbSet<VwGetBatchesForPublishingOld> VwGetBatchesForPublishingOlds { get; set; }
        public virtual DbSet<VwGetBatchesForRunningBusinessRule> VwGetBatchesForRunningBusinessRules { get; set; }
        public virtual DbSet<VwGetBatchesForVideoPublishing> VwGetBatchesForVideoPublishings { get; set; }
        public virtual DbSet<VwIncompletedRequestsAnalysis> VwIncompletedRequestsAnalyses { get; set; }
        public virtual DbSet<VwInfoDocClassField> VwInfoDocClassFields { get; set; }
        public virtual DbSet<VwLastStepPerBatch> VwLastStepPerBatches { get; set; }
        public virtual DbSet<VwRegisterPersonalInfoField> VwRegisterPersonalInfoFields { get; set; }
        public virtual DbSet<VwReportingBatchSourceAppPagesProgress> VwReportingBatchSourceAppPagesProgresses { get; set; }
        public virtual DbSet<VwResultStatusInVideoCall> VwResultStatusInVideoCalls { get; set; }
        public virtual DbSet<VwRetriesForCompletedTransaction> VwRetriesForCompletedTransactions { get; set; }
        public virtual DbSet<VwRetriesForPendingTransaction> VwRetriesForPendingTransactions { get; set; }
        public virtual DbSet<VwStatsForTransactionsInVideoCallQueue> VwStatsForTransactionsInVideoCallQueues { get; set; }
        public virtual DbSet<VwTimeUntilPublish> VwTimeUntilPublishes { get; set; }
        public virtual DbSet<VwTransactionsSignedButNotCompleted> VwTransactionsSignedButNotCompleteds { get; set; }
        public virtual DbSet<VwTransactionsStartedStat> VwTransactionsStartedStats { get; set; }
        public virtual DbSet<WebpagesMembership> WebpagesMemberships { get; set; }
        public virtual DbSet<WebpagesOauthMembership> WebpagesOauthMemberships { get; set; }
        public virtual DbSet<WebpagesRole> WebpagesRoles { get; set; }
        public virtual DbSet<WebpagesUsersInRole> WebpagesUsersInRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdditionalDocumentsOrdering>(entity =>
            {
                entity.ToTable("AdditionalDocumentsOrdering");

                entity.Property(e => e.DocumentTemplateName)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<AdvancedLogging>(entity =>
            {
                entity.ToTable("AdvancedLogging");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action).HasMaxLength(100);

                entity.Property(e => e.Browser).HasMaxLength(100);

                entity.Property(e => e.Controller).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Device).HasMaxLength(500);

                entity.Property(e => e.ExitDate).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("IP");

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RequestTime).HasColumnType("datetime");

                entity.Property(e => e.RequestUrl)
                    .HasMaxLength(3000)
                    .HasColumnName("RequestURL");

                entity.Property(e => e.ResponceStatus).HasMaxLength(50);

                entity.Property(e => e.ResponceTime).HasColumnType("datetime");

                entity.Property(e => e.System).HasMaxLength(100);
            });

            modelBuilder.Entity<AdvancedLogging2>(entity =>
            {
                entity.ToTable("AdvancedLogging_2");

                entity.HasIndex(e => e.ParentId, "IDX_ParentID");

                entity.HasIndex(e => e.RequestId, "IDX_RequestId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action).HasMaxLength(100);

                entity.Property(e => e.Browser).HasMaxLength(100);

                entity.Property(e => e.Controller).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Device).HasMaxLength(500);

                entity.Property(e => e.ExitDate).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("IP");

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RequestTime).HasColumnType("datetime");

                entity.Property(e => e.RequestUrl)
                    .HasMaxLength(3000)
                    .HasColumnName("RequestURL");

                entity.Property(e => e.ResponceStatus).HasMaxLength(50);

                entity.Property(e => e.ResponceTime).HasColumnType("datetime");

                entity.Property(e => e.System).HasMaxLength(100);
            });

            modelBuilder.Entity<AppPage>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ControllerPathAction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FriendlyName).HasMaxLength(100);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.SystemUserId, "IX_AspNetUsers_SystemUserId");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
               

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.SystemUserId);

                entity.HasOne(d => d.NativeLanguage)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.NativeLanguageId)
                    .HasConstraintName("FK_AspNetUsers_Countries");
            });


            modelBuilder.Entity<AspNetReseller>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.AspNetResellers)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AspNetRes__Compa__3DE82FB7");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.AuditType)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.AuditUser)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ChangedColumns).IsRequired();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.KeyValues).IsRequired();

                entity.Property(e => e.NewValues).IsRequired();

                entity.Property(e => e.OldValues).IsRequired();

                entity.Property(e => e.TableName).IsRequired();
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.HasIndex(e => e.RequestId, "IX_Batches");

                entity.HasIndex(e => e.Token, "IX_Batches_Token")
                    .IsUnique();

                entity.Property(e => e.AppliedGdpr).HasColumnName("AppliedGDPR");

                entity.Property(e => e.BatchSourceId).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentOtp)
                    .HasMaxLength(50)
                    .HasColumnName("CurrentOTP");

                entity.Property(e => e.DisconnectionTime).HasColumnType("datetime");

                entity.Property(e => e.DisconnectionsCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.EndVideoCallDate).HasColumnType("datetime");

                entity.Property(e => e.GetInVideoQueueDate).HasColumnType("datetime");

                entity.Property(e => e.LastRepublishDate).HasColumnType("datetime");

                entity.Property(e => e.Otpcounter).HasColumnName("OTPCounter");

                entity.Property(e => e.OtpvalidUntil)
                    .HasColumnType("datetime")
                    .HasColumnName("OTPValidUntil");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RepublishEndDate).HasColumnType("datetime");

                entity.Property(e => e.RepublishTriesCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RetriesCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartProcessDate).HasColumnType("datetime");

                entity.Property(e => e.StartVideoCallDate).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(50);

                entity.Property(e => e.UpponGdprpublishRetriesCount)
                    .HasColumnName("UpponGDPRPublishRetriesCount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpponGdprpublishStatus).HasColumnName("UpponGDPRPublishStatus");

                entity.Property(e => e.VerifiedDate).HasColumnType("datetime");

                entity.Property(e => e.VideoStatus).HasDefaultValueSql("((-10))");

                entity.HasOne(d => d.BatchSource)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.BatchSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batches_BatchSources");

                entity.HasOne(d => d.BatchStatus)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.BatchStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batches_BatchStatuses1");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batches_Companies");

                entity.HasOne(d => d.VideoStatusNavigation)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.VideoStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Batches__VideoSt__15A53433");
            });

            modelBuilder.Entity<BatchAppPagesHistory>(entity =>
            {
                entity.ToTable("BatchAppPagesHistory");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchAppPagesHistories)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchAppPagesHistory_Batches");

                entity.HasOne(d => d.BatchSourceAppPage)
                    .WithMany(p => p.BatchAppPagesHistories)
                    .HasForeignKey(d => d.BatchSourceAppPageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchAppPagesHistory_BatchSourceAppPages");
            });

            modelBuilder.Entity<BatchContractSignConfig>(entity =>
            {
                entity.ToTable("BatchContractSignConfig");

                entity.Property(e => e.Pdfname)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("PDFName");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchContractSignConfigs)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchContractSignConfig_Batches");

                entity.HasOne(d => d.DocumentSignConfig)
                    .WithMany(p => p.BatchContractSignConfigs)
                    .HasForeignKey(d => d.DocumentSignConfigId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchContractSignConfig_DocumentSignConfig");
            });

            modelBuilder.Entity<BatchHistory>(entity =>
            {
                entity.ToTable("BatchHistory");

                entity.HasIndex(e => e.BatchId, "IX_BatchHistory_batchid");

                entity.HasIndex(e => new { e.BatchId, e.IsLast }, "idx_batchHitory_IsLast")
                    .IsUnique()
                    .HasFilter("([IsLast]=(1))");

                entity.Property(e => e.Agent).HasMaxLength(100);

                entity.Property(e => e.AgentVerificationStatusDate).HasColumnType("datetime");

                entity.Property(e => e.FaceMatching).HasMaxLength(50);

                entity.Property(e => e.IsAlive).HasMaxLength(50);

                entity.Property(e => e.IsLast)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RegisterDate).HasColumnType("datetime");

                entity.Property(e => e.ResponseDate).HasColumnType("datetime");

                entity.Property(e => e.RetriesCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartProcessDate).HasColumnType("datetime");

                entity.Property(e => e.SynchordiaRequestId).HasMaxLength(200);

                entity.Property(e => e.VerificationRejectionReason).HasMaxLength(500);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchHistories)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchHistory_Batches");
            });

            modelBuilder.Entity<BatchHistoryExpectedDocument>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.BatchHistoryId, "NonClusteredIndex-20220301-090652")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.PoA).HasMaxLength(100);

                entity.Property(e => e.PoI).HasMaxLength(100);

                entity.Property(e => e.PoO).HasMaxLength(100);

                entity.Property(e => e.PoP).HasMaxLength(100);

                entity.Property(e => e.PoT).HasMaxLength(100);

                entity.HasOne(d => d.BatchHistory)
                    .WithOne()
                    .HasForeignKey<BatchHistoryExpectedDocument>(d => d.BatchHistoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchHistoryExpectedDocuments_Batches");
            });

            modelBuilder.Entity<BatchHistoryItem>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_BatchItems");

                entity.Property(e => e.BatchSourceUploadDocId)
                    .HasDefaultValueSql("((0))")
                    .HasComment("True if the documentclass belongs to BatchSourceUploadDocs else false");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IncludeInOnboarding)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsValid).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<BatchHistoryItemField>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.RegisteredFieldValue).HasMaxLength(2000);

                entity.HasOne(d => d.BatchHistoryItem)
                    .WithMany(p => p.BatchHistoryItemFields)
                    .HasForeignKey(d => d.BatchHistoryItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItemFields_BatchItems");

                entity.HasOne(d => d.DocumentClassField)
                    .WithMany(p => p.BatchHistoryItemFields)
                    .HasForeignKey(d => d.DocumentClassFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItemFields_DocumentClassFields");
            });

            modelBuilder.Entity<BatchHistoryItemPage>(entity =>
            {
                entity.HasIndex(e => new { e.FileName, e.BatchHistoryItemId }, "UX_BatchItemPages_FileName")
                    .IsUnique();

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsLast)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OriginalFileName).HasMaxLength(200);

                entity.HasOne(d => d.BatchHistoryItem)
                    .WithMany(p => p.BatchHistoryItemPages)
                    .HasForeignKey(d => d.BatchHistoryItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItemPages_BatchItems");
            });

            modelBuilder.Entity<BatchHistoryJsonResult>(entity =>
            {
                entity.ToTable("BatchHistoryJsonResult");

                entity.Property(e => e.JsonResult).IsRequired();

                entity.HasOne(d => d.BatchHistory)
                    .WithMany(p => p.BatchHistoryJsonResults)
                    .HasForeignKey(d => d.BatchHistoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchHistoryJsonResult_BatchHistory");
            });

            modelBuilder.Entity<BatchHistoryMetum>(entity =>
            {
                entity.HasIndex(e => new { e.BatchHistoryId, e.DocumentClassFieldId }, "NonClusteredIndex-BatchHistoryId-DocumentClassFieldId");

                entity.HasOne(d => d.BatchHistory)
                    .WithMany(p => p.BatchHistoryMeta)
                    .HasForeignKey(d => d.BatchHistoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BatchHist__Field__7C1A6C5A");

                entity.HasOne(d => d.DocumentClassField)
                    .WithMany(p => p.BatchHistoryMeta)
                    .HasForeignKey(d => d.DocumentClassFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BatchHist__Docum__7D0E9093");
            });

            modelBuilder.Entity<BatchItem>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IncludeInOnboarding).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchItems)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItem_Batches");

                entity.HasOne(d => d.BatchItemStatus)
                    .WithMany(p => p.BatchItems)
                    .HasForeignKey(d => d.BatchItemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItem_BatchItemStatuses");

                entity.HasOne(d => d.DocumentClass)
                    .WithMany(p => p.BatchItems)
                    .HasForeignKey(d => d.DocumentClassId)
                    .HasConstraintName("FK_BatchItem_DocumentClasses");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_BatchItem_BatchItem_PARENT");
            });

            modelBuilder.Entity<BatchItemField>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DictionaryValueIdOld).HasColumnName("DictionaryValueId_old");

                entity.Property(e => e.IsLast)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RegisteredFieldValue).HasMaxLength(2000);

                entity.Property(e => e.RegisteredFieldValueOld)
                    .HasMaxLength(255)
                    .HasColumnName("RegisteredFieldValue_old");

                entity.HasOne(d => d.BatchItem)
                    .WithMany(p => p.BatchItemFields)
                    .HasForeignKey(d => d.BatchItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItemField_BatchItem");

                entity.HasOne(d => d.CreatedByAspNetUser)
                    .WithMany(p => p.BatchItemFields)
                    .HasForeignKey(d => d.CreatedByAspNetUserId)
                    .HasConstraintName("FK_BatchItemFields_AspNetUsers");

                entity.HasOne(d => d.DictionaryValue)
                    .WithMany(p => p.BatchItemFields)
                    .HasForeignKey(d => d.DictionaryValueId)
                    .HasConstraintName("FK_BatchItemFields_BopDictionaries");

                entity.HasOne(d => d.DocumentClassField)
                    .WithMany(p => p.BatchItemFields)
                    .HasForeignKey(d => d.DocumentClassFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItemField_DocumentClassField");
            });

            modelBuilder.Entity<BatchItemPage>(entity =>
            {
                entity.HasIndex(e => new { e.FileName, e.BatchItemId }, "UX_BatchItemPage_FileName")
                    .IsUnique();

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.BatchItem)
                    .WithMany(p => p.BatchItemPages)
                    .HasForeignKey(d => d.BatchItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItemPage_BatchItem");
            });

            modelBuilder.Entity<BatchItemStatus>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.BatchItemStatus1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("BatchItemStatus");

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<BatchMetum>(entity =>
            {
                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchMeta)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchMeta_Batches");
            });

            modelBuilder.Entity<BatchSource>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BatchSource1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BatchSource");

                entity.Property(e => e.BatchSourceCode).HasMaxLength(30);

                entity.Property(e => e.Comments).HasMaxLength(50);

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RedirectLink).HasMaxLength(200);

                entity.Property(e => e.ValidationApiUrl)
                    .HasMaxLength(100)
                    .HasColumnName("ValidationApiURL");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.BatchSources)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__BatchSour__Compa__58671BC9");
            });

            modelBuilder.Entity<BatchSourceAppPage>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.BatchSource)
                    .WithMany(p => p.BatchSourceAppPages)
                    .HasForeignKey(d => d.BatchSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchSourceAppPages_BatchSources");

                entity.HasOne(d => d.CurrentAppPage)
                    .WithMany(p => p.BatchSourceAppPages)
                    .HasForeignKey(d => d.CurrentAppPageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchSourceAppPages_AppPages");

                entity.HasOne(d => d.DocumentSignConfig)
                    .WithMany(p => p.BatchSourceAppPages)
                    .HasForeignKey(d => d.DocumentSignConfigId)
                    .HasConstraintName("FK_BatchSourceAppPages_DocumentSignConfig");
            });

            modelBuilder.Entity<BatchSourceUploadDoc>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.BatchSource)
                    .WithMany(p => p.BatchSourceUploadDocs)
                    .HasForeignKey(d => d.BatchSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchSourceUploadDocs_BatchSourceUploadDocs");

                entity.HasOne(d => d.DocumentGroupName)
                    .WithMany(p => p.BatchSourceUploadDocs)
                    .HasForeignKey(d => d.DocumentGroupNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchSourceUploadDocs_DocumentGroupNames");
            });

            modelBuilder.Entity<BatchStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BatchStatus1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BatchStatus");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BatchVideoPriority>(entity =>
            {
                entity.HasKey(e => e.BatchId);

                entity.ToTable("BatchVideoPriority");

                entity.Property(e => e.BatchId).ValueGeneratedNever();

                entity.Property(e => e.CountryOfOrigin).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId).HasMaxLength(50);
            });

            modelBuilder.Entity<BatchVideoStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BatchStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BatchWithReseller>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("BatchWithReseller");

                entity.Property(e => e.AppliedGdpr).HasColumnName("AppliedGDPR");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentOtp)
                    .HasMaxLength(50)
                    .HasColumnName("CurrentOTP");

                entity.Property(e => e.Otpcounter).HasColumnName("OTPCounter");

                entity.Property(e => e.OtpvalidUntil)
                    .HasColumnType("datetime")
                    .HasColumnName("OTPValidUntil");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartProcessDate).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(50);

                entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BatchmetaToDelete>(entity =>
            {
                entity.ToTable("batchmeta_to_Delete");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchmetaToDeletes)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_batchmeta_to_Delete_Batches");
            });

            modelBuilder.Entity<BopConfig>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Setting)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<BopDictionariesMissingValue>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Value).HasMaxLength(150);
            });

            modelBuilder.Entity<BopDictionary>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.DictionaryType)
                    .WithMany(p => p.BopDictionaries)
                    .HasForeignKey(d => d.DictionaryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dictionaries_DictionaryTypes");
            });

            modelBuilder.Entity<ColumnPreference>(entity =>
            {
                entity.HasIndex(e => e.ScreenId, "IX_ColumnPreferences_ScreenId");

                entity.HasIndex(e => e.SystemUserId, "IX_ColumnPreferences_SystemUserId");

                entity.Property(e => e.ColumnName).HasMaxLength(50);

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.ColumnPreferences)
                    .HasForeignKey(d => d.ScreenId);

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.ColumnPreferences)
                    .HasForeignKey(d => d.SystemUserId);
            });

            modelBuilder.Entity<CompaniesMonitoringCofiguration>(entity =>
            {
                entity.HasIndex(e => e.CompanyId, "IX_CompaniesMonitoringCofigurations")
                    .IsUnique();

                entity.Property(e => e.Endpoint).HasMaxLength(150);

                entity.Property(e => e.Jwt)
                    .HasMaxLength(50)
                    .HasColumnName("JWT");

                entity.Property(e => e.LogsRetentionDays).HasDefaultValueSql("((10))");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithOne(p => p.CompaniesMonitoringCofiguration)
                    .HasForeignKey<CompaniesMonitoringCofiguration>(d => d.CompanyId)
                    .HasConstraintName("FK_CompaniesMonitoringCofigurations_Companies");
            });


            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.Code, "UC_Companies_Code")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AgentController).HasMaxLength(100);

                entity.Property(e => e.AgentPortalLogo).HasMaxLength(200);

                entity.Property(e => e.BasicAuthPassword).HasMaxLength(100);

                entity.Property(e => e.BasicAuthUserName).HasMaxLength(100);

                entity.Property(e => e.BearerAuthUrl)
                    .HasMaxLength(250)
                    .HasColumnName("BearerAuthURL");

                entity.Property(e => e.BearerPassword).HasMaxLength(100);

                entity.Property(e => e.BearerUserName).HasMaxLength(100);

                entity.Property(e => e.CallBackConfiguration).IsUnicode(false);

                entity.Property(e => e.CallBackUrl)
                    .HasMaxLength(250)
                    .HasColumnName("CallBackURL");

                entity.Property(e => e.CloudStorage).IsUnicode(false);

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.DocumentsDownloadAccessEndpoint).HasMaxLength(100);

                entity.Property(e => e.DocumentsDownloadAccessFetchUrl).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.ExpirationDaysToBeKept).HasDefaultValueSql("((0))");

                entity.Property(e => e.FtpDirectory).HasMaxLength(150);

                entity.Property(e => e.FtpHostName).HasMaxLength(50);

                entity.Property(e => e.FtpPassword).HasMaxLength(50);

                entity.Property(e => e.FtpResponseDirectory).HasMaxLength(150);

                entity.Property(e => e.FtpResponseHostName).HasMaxLength(50);

                entity.Property(e => e.FtpResponsePassword).HasMaxLength(50);

                entity.Property(e => e.FtpResponseUserName).HasMaxLength(50);

                entity.Property(e => e.FtpUserName).HasMaxLength(50);

                entity.Property(e => e.GdprdaysToBeKept).HasColumnName("GDPRDaysToBeKept");

                entity.Property(e => e.GrandType).HasMaxLength(100);

                entity.Property(e => e.HawkAppId)
                    .HasMaxLength(50)
                    .HasColumnName("HawkAppID");

                entity.Property(e => e.HawkSecret).HasMaxLength(50);

                entity.Property(e => e.HawkUser).HasMaxLength(50);

                entity.Property(e => e.MaxCallTime).HasColumnName("MaxCallTIme");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PriorityQueueNotification).IsUnicode(false);

                entity.Property(e => e.SendRejectionReasonAsCode)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SendResponseUpponGdpr)
                    .HasColumnName("SendResponseUpponGDPR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SlabatchQuantity).HasColumnName("SLABatchQuantity");

                entity.Property(e => e.Slaimportance).HasColumnName("SLAImportance");

                entity.Property(e => e.Slaminutes).HasColumnName("SLAMinutes");

                entity.Property(e => e.Smsprovider).HasColumnName("SMSProvider");

                entity.Property(e => e.Smtpfrom)
                    .HasMaxLength(100)
                    .HasColumnName("SMTPFrom");

                entity.Property(e => e.SmtpfromPassword)
                    .HasMaxLength(100)
                    .HasColumnName("SMTPFromPassword");

                entity.Property(e => e.SmtpfromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("SMTPFromUsername");

                entity.Property(e => e.Smtphost)
                    .HasMaxLength(100)
                    .HasColumnName("SMTPHost");

                entity.Property(e => e.Smtpport).HasColumnName("SMTPPort");

                entity.Property(e => e.Smtpsubject)
                    .HasMaxLength(100)
                    .HasColumnName("SMTPSubject");

                entity.Property(e => e.SoapCallback).IsUnicode(false);

                entity.Property(e => e.VideoCallBackUrl).HasMaxLength(250);
            });


            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Code2D)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code3D)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.MobileCode)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            
            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<CountriesPerCompany>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.BatchSourceId, e.CountryId });

                entity.ToTable("CountriesPerCompany");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.BatchSource)
                    .WithMany(p => p.CountriesPerCompanies)
                    .HasForeignKey(d => d.BatchSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountriesPerCompany_BatchSources");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CountriesPerCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountriesPerCompany_Companies");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountriesPerCompanies)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountriesPerCompany_Countries");
            });

            modelBuilder.Entity<DboVwDocumentGroupNamesForBatchHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("dbo.vw_DocumentGroupNamesForBatchHistory");

                entity.Property(e => e.DocumentGroupName).HasMaxLength(253);

                entity.Property(e => e.FileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DictionaryType>(entity =>
            {
                entity.HasIndex(e => e.DictionaryType1, "IX_DictionaryTypes")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DictionaryType1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("DictionaryType");
            });

            modelBuilder.Entity<DocumentClasses>(entity =>
            {
                entity.Property(e => e.DocumentClass)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FriendlyName).HasMaxLength(50);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.DocumentGroupName)
                    .WithMany(p => p.DocumentClasses)
                    .HasForeignKey(d => d.DocumentGroupNameId)
                    .HasConstraintName("FK_DocumentClasses_DocumentGroupNames");
            });

            modelBuilder.Entity<DocumentClassField>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CorrectiveActionMappedName).HasMaxLength(50);

                entity.Property(e => e.DocumentClassFieldTypeId).HasDefaultValueSql("((1))");

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'-')");

                entity.Property(e => e.IsMandatory)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MappedName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PublishEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Uilabel)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("UILabel")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Uisort).HasColumnName("UISort");

                entity.HasOne(d => d.DocumentClass)
                    .WithMany(p => p.DocumentClassFields)
                    .HasForeignKey(d => d.DocumentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentClassFields_DocumentClasses");
            });

            modelBuilder.Entity<DocumentClassFieldType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DocumentGroupName>(entity =>
            {
                entity.HasIndex(e => e.Code, "Unique_Code")
                  .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentGroupName1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("DocumentGroupName");
            });

            modelBuilder.Entity<DocumentRejectionReason>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(70);

                entity.Property(e => e.Descr).HasMaxLength(70);
            });

            modelBuilder.Entity<DocumentSignConfig>(entity =>
            {
                entity.ToTable("DocumentSignConfig");

                entity.Property(e => e.DocumentTemplateName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SignatureConfiguration)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<DocumentsPerBatchSource>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DocumentsPerBatchSource");

                entity.HasIndex(e => new { e.DocumentClassId, e.BatchSourceId, e.CompanyId, e.DocumentGroupNameId }, "IX_DocumentsPerBatchSource")
                    .IsUnique();

                entity.HasOne(d => d.BatchSource)
                    .WithMany()
                    .HasForeignKey(d => d.BatchSourceId)
                    .HasConstraintName("FK_DocumentsPerBatchSource_BatchSources");

                entity.HasOne(d => d.Company)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentsPerBatchSource_Companies");

                entity.HasOne(d => d.DocumentClass)
                    .WithMany()
                    .HasForeignKey(d => d.DocumentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentsPerBatchSource_DocumentClasses");

                entity.HasOne(d => d.DocumentGroupName)
                    .WithMany()
                    .HasForeignKey(d => d.DocumentGroupNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentsPerBatchSource_DocumentGroupNames");
            });

            modelBuilder.Entity<DocumentsPerCompany>(entity =>
            {
                entity.HasIndex(e => e.CompanyId, "IX_DocumentsPerCompany_CompanyId");

                entity.HasIndex(e => e.DocumentClassId, "IX_DocumentsPerCompany_DocumentClassId");

                entity.HasIndex(e => e.DocumentGroupId, "IX_DocumentsPerCompany_DocumentGroupId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.DocumentsPerCompany)
                    .HasForeignKey(d => d.CompanyId);

                entity.HasOne(d => d.DocumentClass)
                    .WithMany(p => p.DocumentsPerCompany)
                    .HasForeignKey(d => d.DocumentClassId);

                entity.HasOne(d => d.DocumentGroup)
                    .WithMany(p => p.DocumentsPerCompany)
                    .HasForeignKey(d => d.DocumentGroupId);
            });

            modelBuilder.Entity<InfoDocClassField>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.BatchSource)
                    .WithMany(p => p.InfoDocClassFields)
                    .HasForeignKey(d => d.BatchSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InfoDocClassFields_BatchSources1");

                entity.HasOne(d => d.DocumentClassField)
                    .WithMany(p => p.InfoDocClassFields)
                    .HasForeignKey(d => d.DocumentClassFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InfoDocClassFields_DocumentClassFields1");
            });

            modelBuilder.Entity<Nlog>(entity =>
            {
                entity.ToTable("NLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassName).HasMaxLength(100);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Logged).HasColumnType("datetime");

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.RequestId).HasMaxLength(100);
            });

            modelBuilder.Entity<PasswordHistory>(entity =>
            {
                entity.ToTable("PasswordHistory");

                entity.HasIndex(e => e.SystemUserId, "IX_PasswordHistory_SystemUserId");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.PasswordHistories)
                    .HasForeignKey(d => d.SystemUserId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PasswordH__Syste__6CA31EA0");
            });
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasIndex(e => new { e.CompanyId, e.PageId, e.ResourceLanguageId, e.FlowId, e.KeyDescription }, "IX_UniqueNonDefault")
                    .IsUnique();

                entity.Property(e => e.KeyDescription)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.ResourceLanguage)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.ResourceLanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resources_ResourceLanguages");
            });

            modelBuilder.Entity<ResourceLanguage>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Cultrure).HasMaxLength(15);

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(3);
            });
            modelBuilder.Entity<RoleScreen>(entity =>
            {
                entity.HasIndex(e => e.ScreenId, "IX_RoleScreens_ScreenId");

                entity.HasIndex(e => e.SystemRoleId, "IX_RoleScreens_SystemRoleId");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.RoleScreens)
                    .HasForeignKey(d => d.ScreenId);

                entity.HasOne(d => d.SystemRole)
                    .WithMany(p => p.RoleScreens)
                    .HasForeignKey(d => d.SystemRoleId);
            });

            modelBuilder.Entity<RoleScreenColumn>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_RoleScreenColumns_RoleId");

                entity.HasIndex(e => e.ScreenColumnId, "IX_RoleScreenColumns_ScreenColumnId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleScreenColumns)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.ScreenColumn)
                    .WithMany(p => p.RoleScreenColumns)
                    .HasForeignKey(d => d.ScreenColumnId);
            });

            modelBuilder.Entity<RoleScreenElement>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_RoleScreenElements_RoleId");

                entity.HasIndex(e => e.ScreenElementId, "IX_RoleScreenElements_ScreenElementId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleScreenElements)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.ScreenElement)
                    .WithMany(p => p.RoleScreenElements)
                    .HasForeignKey(d => d.ScreenElementId);
            });

            modelBuilder.Entity<Screen>(entity =>
            {
                entity.Property(e => e.ScreenName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ScreenColumn>(entity =>
            {
                entity.HasIndex(e => e.ScreenId, "IX_ScreenColumns_ScreenId");

                entity.Property(e => e.ColumnName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.ScreenColumns)
                    .HasForeignKey(d => d.ScreenId);
            });

            modelBuilder.Entity<ScreenElement>(entity =>
            {
                entity.HasIndex(e => e.ScreenId, "IX_ScreenElements_ScreenId");

                entity.Property(e => e.ScreenElementName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.ScreenElements)
                    .HasForeignKey(d => d.ScreenId);
            });

            modelBuilder.Entity<SystemRole>(entity =>
            {
                entity.HasIndex(e => e.Name, "UC_SystemRoles_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.HasIndex(e => e.Email, "UC_SystemUsers_Email")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(50);
                entity.Property(e => e.Jmbg).HasMaxLength(50);
            });

            modelBuilder.Entity<SystemUserCountry>(entity =>
            {
                entity.HasIndex(e => e.CountryId, "IX_SystemUserCountries_CountryId");

                entity.HasIndex(e => e.SystemUserId, "IX_SystemUserCountries_SystemUserId");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.SystemUserCountries)
                    .HasForeignKey(d => d.CountryId);

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.SystemUserCountries)
                    .HasForeignKey(d => d.SystemUserId);
            });

            modelBuilder.Entity<SystemUserRole>(entity =>
            {
                entity.HasIndex(e => e.SystemRoleId, "IX_SystemUserRoles_SystemRoleId");

                entity.HasIndex(e => e.SystemUserId, "IX_SystemUserRoles_SystemUserId");

                entity.HasOne(d => d.SystemRole)
                    .WithMany(p => p.SystemUserRoles)
                    .HasForeignKey(d => d.SystemRoleId);

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.SystemUserRoles)
                    .HasForeignKey(d => d.SystemUserId);
            });
            modelBuilder.Entity<SystemUsersView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SystemUsersView");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companyies_Companyid");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.UserCompanies)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemUserId");
            });

            modelBuilder.Entity<UserPreference>(entity =>
            {
                entity.HasIndex(e => e.SystemUserId, "IX_UserPreferences_SystemUserId");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.UserPreferences)
                    .HasForeignKey(d => d.SystemUserId);
            });

            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.HasKey(e => e.SystemUserId);

                entity.HasIndex(e => e.SystemUserId, "IX_UserSessions_SystemUserId");

                entity.Property(e => e.SystemUserId).ValueGeneratedNever();

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.SystemUser)
                    .WithOne(p => p.UserSession)
                    .HasForeignKey<UserSession>(d => d.SystemUserId);
            });

            modelBuilder.Entity<ViewBatchesToBeDeleted>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_BatchesToBeDeleted");

                entity.Property(e => e.AppliedGdpr).HasColumnName("AppliedGDPR");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SendResponseUpponGdpr).HasColumnName("SendResponseUpponGDPR");
            });

            modelBuilder.Entity<VwAgentStatsPerCompanyMonthly>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwAgentStatsPerCompanyMonthly");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.MonthOfStartVideoCall).HasMaxLength(35);
            });

            modelBuilder.Entity<VwAlphaBankReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_AlphaBankReport");

                entity.Property(e => e.Agent).HasMaxLength(50);

                entity.Property(e => e.BatchStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BatchVideoStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransactionTime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.VideoCallTime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.WaitingTime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<VwAnalysisForFailedTransaction>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwAnalysisForFailedTransactions");

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwAppPagesWorkflowPerBatchSource>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_AppPagesWorkflowPerBatchSource");

                entity.Property(e => e.CurrentAppPage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DocumentTemplateName).HasMaxLength(50);

                entity.Property(e => e.NextAppPage).HasMaxLength(50);

                entity.Property(e => e.PreviousAppPage).HasMaxLength(50);

                entity.Property(e => e.SignatureConfiguration).HasMaxLength(1000);
            });

            modelBuilder.Entity<VwAppPagesWorkflowStepsPerBatchSource>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_AppPagesWorkflowStepsPerBatchSource");

                entity.Property(e => e.CurrentAppPage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Steps).HasMaxLength(54);
            });

            modelBuilder.Entity<VwBatchContractSignConfig>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_BatchContractSignConfig");

                entity.Property(e => e.DocumentTemplateName).HasMaxLength(50);

                entity.Property(e => e.Pdfname)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("PDFName");

                entity.Property(e => e.SignatureConfiguration).HasMaxLength(1000);
            });

            modelBuilder.Entity<VwBatchStatusForTransactionsInVideoCallQueue>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwBatchStatusForTransactionsInVideoCallQueue");

                entity.Property(e => e.Agent).HasMaxLength(50);

                entity.Property(e => e.BatchStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BatchVideoStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DisconnectionTime).HasColumnType("datetime");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransactionTime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.VideoCallTime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.WaitingTime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<VwCompaniesWithOtpmessageMonthly>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwCompaniesWithOTPMessageMonthly");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MonthOfTransaction).HasMaxLength(35);
            });

            modelBuilder.Entity<VwDocumentGroupNamePerBatchSource>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_DocumentGroupNamePerBatchSource");

                entity.Property(e => e.BatchSourceCode).HasMaxLength(30);

                entity.Property(e => e.DocumentGroupName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwDocumentGroupNamePerBatchSourceBatchHistoryItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_DocumentGroupNamePerBatchSourceBatchHistoryItems");

                entity.Property(e => e.BatchSourceCode).HasMaxLength(30);

                entity.Property(e => e.DocumentClassIds).HasMaxLength(4000);

                entity.Property(e => e.DocumentGroupName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwDocumentGroupNamesFormBatchHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_DocumentGroupNamesFormBatchHistory");

                entity.Property(e => e.DocumentGroupName).HasMaxLength(253);

                entity.Property(e => e.FileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwFlowOrdering>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_FlowOrdering");

                entity.Property(e => e.BatchSource)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ControllerPathAction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FriendlyName).HasMaxLength(100);
            });

            modelBuilder.Entity<VwFriendlyBatchSourceAppPage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_FriendlyBatchSOurceAppPages");

                entity.Property(e => e.BackAppPage).HasMaxLength(50);

                entity.Property(e => e.BatchSource)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CurrentAppPage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NextAppPage).HasMaxLength(50);
            });

            modelBuilder.Entity<VwGetBatchHistoryItemField>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_GetBatchHistoryItemFields");

                entity.Property(e => e.MappedName).HasMaxLength(50);

                entity.Property(e => e.RegisteredFieldValue).HasMaxLength(2000);

                entity.Property(e => e.Uilabel)
                    .HasMaxLength(128)
                    .HasColumnName("UILabel");
            });

            modelBuilder.Entity<VwGetBatchHistoryMeta>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_GetBatchHistoryMetas");

                entity.Property(e => e.Uilabel)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("UILabel");
            });

            modelBuilder.Entity<VwGetBatchItemField>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_GetBatchItemFields");

                entity.Property(e => e.MappedName).HasMaxLength(50);

                entity.Property(e => e.RegisteredFieldValue).HasMaxLength(2000);

                entity.Property(e => e.Uilabel)
                    .HasMaxLength(128)
                    .HasColumnName("UILabel");
            });

            modelBuilder.Entity<VwGetBatchesForBatchHistoryHubNotification>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_GetBatchesForBatchHistoryHubNotification");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwGetBatchesForPublishing>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_GetBatchesForPublishing");

                entity.Property(e => e.AppliedGdpr).HasColumnName("AppliedGDPR");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentOtp)
                    .HasMaxLength(50)
                    .HasColumnName("CurrentOTP");

                entity.Property(e => e.Otpcounter).HasColumnName("OTPCounter");

                entity.Property(e => e.OtpvalidUntil)
                    .HasColumnType("datetime")
                    .HasColumnName("OTPValidUntil");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartProcessDate).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(50);

                entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwGetBatchesForPublishingOld>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_GetBatchesForPublishing_old");

                entity.Property(e => e.AppliedGdpr).HasColumnName("AppliedGDPR");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentOtp)
                    .HasMaxLength(50)
                    .HasColumnName("CurrentOTP");

                entity.Property(e => e.Otpcounter).HasColumnName("OTPCounter");

                entity.Property(e => e.OtpvalidUntil)
                    .HasColumnType("datetime")
                    .HasColumnName("OTPValidUntil");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartProcessDate).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(50);

                entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwGetBatchesForRunningBusinessRule>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_GetBatchesForRunningBusinessRules");

                entity.Property(e => e.AppliedGdpr).HasColumnName("AppliedGDPR");

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentOtp)
                    .HasMaxLength(50)
                    .HasColumnName("CurrentOTP");

                entity.Property(e => e.Otpcounter).HasColumnName("OTPCounter");

                entity.Property(e => e.OtpvalidUntil)
                    .HasColumnType("datetime")
                    .HasColumnName("OTPValidUntil");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartProcessDate).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(50);

                entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwGetBatchesForVideoPublishing>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_GetBatchesForVideoPublishing");

                entity.Property(e => e.AppliedGdpr).HasColumnName("AppliedGDPR");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentOtp)
                    .HasMaxLength(50)
                    .HasColumnName("CurrentOTP");

                entity.Property(e => e.Otpcounter).HasColumnName("OTPCounter");

                entity.Property(e => e.OtpvalidUntil)
                    .HasColumnType("datetime")
                    .HasColumnName("OTPValidUntil");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartProcessDate).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(50);

                entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwIncompletedRequestsAnalysis>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwIncompletedRequestsAnalysis");

                entity.Property(e => e.Company).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentPage).HasMaxLength(50);

                entity.Property(e => e.NextPage).HasMaxLength(50);
            });

            modelBuilder.Entity<VwInfoDocClassField>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_InfoDocClassFields");

                entity.Property(e => e.EnumValue).HasMaxLength(50);
            });

            modelBuilder.Entity<VwLastStepPerBatch>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_LastStepPerBatch");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwRegisterPersonalInfoField>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_RegisterPersonalInfoFields");

                entity.Property(e => e.EnumValue).HasMaxLength(50);
            });

            modelBuilder.Entity<VwReportingBatchSourceAppPagesProgress>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Reporting_BatchSourceAppPagesProgress");

                entity.Property(e => e.ControllerPathAction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FriendlyName).HasMaxLength(100);
            });

            modelBuilder.Entity<VwResultStatusInVideoCall>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwResultStatusInVideoCall");

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CountForReasonStopped).HasColumnName("countForReasonStopped");

                entity.Property(e => e.InVideoQueueDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRetriesForCompletedTransaction>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwRetriesForCompletedTransactions");

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRetriesForPendingTransaction>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwRetriesForPendingTransactions");

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId).HasMaxLength(50);
            });

            modelBuilder.Entity<VwStatsForTransactionsInVideoCallQueue>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwStatsForTransactionsInVideoCallQueue");

                entity.Property(e => e.Agent).HasMaxLength(50);

                entity.Property(e => e.BatchStatusValue)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BatchVideoStatusValue)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DisconnectionTime).HasColumnType("datetime");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransactionTime)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.VideoCallTime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.WaitingTime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<VwTimeUntilPublish>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTimeUntilPublish");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ProcessTimeUntilPublish)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartProcessDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwTransactionsSignedButNotCompleted>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTransactionsSignedButNotCompleted");

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwTransactionsStartedStat>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTransactionsStartedStats");

                entity.Property(e => e.BatchStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GetInVideoQueueDate).HasColumnType("datetime");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.StartVideoCallDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<WebpagesMembership>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__webpages__1788CC4C9BA7726B");

                entity.ToTable("webpages_Membership");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.ConfirmationToken).HasMaxLength(128);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IsConfirmed).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastPasswordFailureDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PasswordChangedDate).HasColumnType("datetime");

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PasswordVerificationToken).HasMaxLength(128);

                entity.Property(e => e.PasswordVerificationTokenExpirationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<WebpagesOauthMembership>(entity =>
            {
                entity.HasKey(e => new { e.Provider, e.ProviderUserId })
                    .HasName("PK__webpages__F53FC0ED2E152EBD");

                entity.ToTable("webpages_OAuthMembership");

                entity.Property(e => e.Provider).HasMaxLength(30);

                entity.Property(e => e.ProviderUserId).HasMaxLength(100);
            });

            modelBuilder.Entity<WebpagesRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__webpages__8AFACE1A1EFF3380");

                entity.ToTable("webpages_Roles");

                entity.HasIndex(e => e.RoleName, "UQ__webpages__8A2B61603A3E7D6D")
                    .IsUnique();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<WebpagesUsersInRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK__webpages__AF2760ADE85AF299");

                entity.ToTable("webpages_UsersInRoles");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.WebpagesUsersInRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WebpagesUsersInRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private void EncryptUsersPerCompany()
        {
            foreach (var entityEntry in ChangeTracker.Entries()) // Iterate all made changes
            {
                if (entityEntry.Entity is Company company)
                {
                    if (entityEntry.State == EntityState.Added ||
                        entityEntry.State == EntityState.Modified)
                    {
                        company.UsersPerCompany = EncryptionHelper.EncryptString(company.UsersPerCompany);
                    }
                }
            }
        }

        public override int SaveChanges()
        {
            EncryptUsersPerCompany();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            EncryptUsersPerCompany();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
