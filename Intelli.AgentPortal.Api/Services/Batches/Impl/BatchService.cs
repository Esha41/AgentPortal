using AutoMapper;
using Intelli.AgentPortal.Api.DTO;
using Intelli.AgentPortal.Domain.Core.Repository;
using Intelli.AgentPortal.Domain.Database;
using Intelli.AgentPortal.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Intelli.AgentPortal.Shared.Services.Implementations;
using Intelli.AgentPortal.Shared.DTO;
using Intelli.AgentPortal.Shared.Services.Abstract;
using Intelli.AgentPortal.Shared.Mvc.DocumentClassFields;
using Intelli.AgentPortal.Domain.Repository.Impl;
using Intelli.AgentPortal.Domain.Repository;
using Intelli.AgentPortal.Shared.Mvc.Resources;
using Intelli.AgentPortal.Shared.Mvc.Extensions;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Intelli.AgentPortal.Shared.Mvc.Services.CustomerDetailsManager;
using Intelli.AgentPortal.Shared.Mvc.Controllers;
using OpenTokSDK;
using System.Globalization;
using Intelli.AgentPortal.Api.Services.BopConfigService;
using Intelli.AgentPortal.Api.Services.BatchStatusService;
using Intelli.AgentPortal.Domain.Core.Helpers;
using Intelli.AgentPortal.EventBus.RabbitMQ.Event;
using Intelli.AgentPortal.EventBus.RabbitMQ.Sender;

namespace Intelli.AgentPortal.Api.Services.Batches.Impl
{
    /// <summary>
    /// The Batch Service.
    /// </summary>
    public class BatchService : IBatchService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Batch> _repositoryBatch;
        private readonly IRepository<Company> _repositoryCompany;

        private readonly ICustomRepository<DboVwDocumentGroupNamesForBatchHistory> _repositoryDboVwDocumentGroupNamesForBatchHistory;
        private readonly ICustomRepository<VwDocumentGroupNamesFormBatchHistory> _repositoryDocumentGroupNamesFormBatchHistory;
        private readonly ICustomRepository<VwRegisterPersonalInfoField> _repositoryVwRegisterPersonalInfoField;
        private readonly ICustomRepository<BatchHistory> _repositoryBatchHistory;
        private readonly ICustomRepository<BatchHistoryMetum> _repositoryBatchHistoryMetum;
        private readonly ICustomRepository<VwGetBatchHistoryItemField> _repositoryVwGetBatchHistoryItemField;
        private readonly ICustomRepository<VwGetBatchItemField> _repositoryVwGetBatchItemField;
        private readonly ICustomRepository<VwGetBatchHistoryMeta> _repositoryVwGetBatchHistoryMeta;
        private readonly ICustomRepository<BatchMetum> _repositoryBatchMetum;
        private readonly ICustomRepository<BatchHistoryItemPage> _repositoryBatchHistoryItemPage;
        private readonly ICustomRepository<BatchHistoryItem> _repositoryBatchHistoryItem;
        private readonly ICustomRepository<BatchHistoryItemField> _repositoryBatchHistoryItemField;
        private readonly ICustomRepository<BatchItemField> _repositoryBatchItemField;
        private readonly ICustomRepository<BatchItemPage> _repositoryBatchItemPage;
        private readonly ICustomRepository<BatchItem> _repositoryBatchItem;
        private readonly ICustomRepository<BatchVideoPriority> _repositoryBatchVideoPriority;
        private readonly ICustomRepository<BatchSourceUploadDoc> _repositoryBatchSourceUploadDoc;
        private readonly ICustomRepository<SystemUser> _repositorySystemUser;
        private readonly ICustomRepository<VwInfoDocClassField> _repositoryInfoDocClassField;
        private readonly ICustomRepository<AspNetUser> _repositoryAspNetUser;
        private readonly IDocumentClassFields _documentClassFieldService;
        private readonly IBopConfigService _bopConfigService;
        private readonly IBatchStatusService _batchStatusService;
        private readonly ICustomerDetailsManager _customerDetailsManager;
        private readonly AgentPortalContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="sender">The Event Sender.</param>
        public BatchService(AgentPortalContext context, IMapper mapper,
             ILogger<BatchService> logger,
             IConfiguration configuration,
             ICustomerDetailsManager customerDetailsManager,
             IDocumentClassFields documentClassFieldService,
             IBopConfigService bopConfigServiceService,
             IBatchStatusService batchStatusService,
             IEventSender sender)
        {
            _documentClassFieldService = documentClassFieldService;
            _bopConfigService = bopConfigServiceService;
            _batchStatusService = batchStatusService;
            _customerDetailsManager = customerDetailsManager;
            _repositoryBatch = new GenericRepository<Batch>(context);
            _repositoryAspNetUser = new CustomRepository<AspNetUser>(context);
            _repositoryBatchItem = new CustomRepository<BatchItem>(context);
            _repositoryBatchItemPage = new CustomRepository<BatchItemPage>(context);
            _repositoryBatchItemField = new CustomRepository<BatchItemField>(context);
            _repositoryBatchHistoryItemField = new CustomRepository<BatchHistoryItemField>(context);
            _repositoryCompany = new GenericRepository<Company>(context);
            _repositoryBatchHistory = new CustomRepository<BatchHistory>(context);
            _repositoryVwGetBatchHistoryMeta = new CustomRepository<VwGetBatchHistoryMeta>(context);
            _repositoryBatchHistoryMetum = new CustomRepository<BatchHistoryMetum>(context);
            _repositoryDocumentGroupNamesFormBatchHistory = new CustomRepository<VwDocumentGroupNamesFormBatchHistory>(context);
            _repositoryDboVwDocumentGroupNamesForBatchHistory = new CustomRepository<DboVwDocumentGroupNamesForBatchHistory>(context);
            _repositoryVwGetBatchHistoryItemField = new CustomRepository<VwGetBatchHistoryItemField>(context);
            _repositoryVwGetBatchItemField = new CustomRepository<VwGetBatchItemField>(context);
            _repositoryVwRegisterPersonalInfoField = new CustomRepository<VwRegisterPersonalInfoField>(context);
            _repositoryBatchHistoryItemPage = new CustomRepository<BatchHistoryItemPage>(context);
            _repositoryBatchHistoryItem = new CustomRepository<BatchHistoryItem>(context);
            _repositoryBatchMetum = new CustomRepository<BatchMetum>(context);
            _repositoryBatchSourceUploadDoc = new CustomRepository<BatchSourceUploadDoc>(context);
            _repositorySystemUser = new CustomRepository<SystemUser>(context);
            _repositoryInfoDocClassField = new CustomRepository<VwInfoDocClassField>(context);
            _repositoryBatchVideoPriority = new CustomRepository<BatchVideoPriority>(context);

            ((GenericRepository<Batch>)_repositoryBatch).AfterSave =
            ((CustomRepository<BatchMetum>)_repositoryBatchMetum).AfterSave =
            ((CustomRepository<BatchHistory>)_repositoryBatchHistory).AfterSave =
            ((CustomRepository<BatchHistoryMetum>)_repositoryBatchHistoryMetum).AfterSave =
            ((CustomRepository<BatchHistoryItemPage>)_repositoryBatchHistoryItemPage).AfterSave =
            ((CustomRepository<BatchHistoryItemField>)_repositoryBatchHistoryItemField).AfterSave =
            ((CustomRepository<BatchItemPage>)_repositoryBatchItemPage).AfterSave =
            ((CustomRepository<BatchItemField>)_repositoryBatchItemField).AfterSave = (logs) =>
                sender.SendEvent(new MQEvent<List<AuditEntry>>(BaseController.AUDIT_EVENT_NAME, (List<AuditEntry>)logs));

            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Get Batch by requestId.
        /// </summary>
        /// <param name="requestId">The batch requestId.</param>
        /// <returns>Batch.</returns>
        public async Task<Batch> GetBatch(string requestId)
        {
            return await _repositoryBatch.Query(d => d.RequestId == requestId)
                                              .Include(d => d.BatchMeta)
                                              .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Load batch and its all references by requestId.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>Batch.</returns>
        public Batch LoadBatchForSaveImage(string requestId)
        {
            return _repositoryBatch.Query(d => d.RequestId == requestId)
                                             .Include(s => s.BatchHistories)
                                             .ThenInclude(c => c.BatchHistoryItems)
                                             .ThenInclude(w => w.BatchHistoryItemPages)
                                             .Include(v => v.BatchItems)
                                             .ThenInclude(p => p.BatchItemPages)
                                             .FirstOrDefault();
        }

        /// <summary>
        /// Load batch and its all references by requestId & current user.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <param name="userId">The current user's Id.</param>
        /// <returns>Batch.</returns>
        public Batch LoadFullBatch(string requestId, int userId, bool isIndexOnlyView = false)
        {
            SystemUser systemUser = LoadUserWithUserCompanies(userId);
            var aspNetUserIds = systemUser.AspNetUsers.Select(nu => nu.Id).ToList();
            var userCompaniesIds = systemUser.UserCompanies.Select(uc => uc.CompanyId).ToList();
            Batch batch = null;

            if (systemUser != null)
            {
                batch = _repositoryBatch.Query(b => b.RequestId == requestId &&
                                                (systemUser.UserCompanies.Count() == 0 || userCompaniesIds.Contains(b.CompanyId)) &&
                                                (isIndexOnlyView || aspNetUserIds.Contains(b.LockedBy ?? 0)))
                                        .AsSplitQuery()
                                        .Include(s => s.BatchHistories)
                                        .ThenInclude(c => c.BatchHistoryItems)
                                        .ThenInclude(g => g.BatchHistoryItemFields)
                                        .Include(v => v.BatchItems)
                                        .ThenInclude(b => b.BatchItemFields)
                                        .Include(f => f.BatchMeta)
                                        .Include(c => c.Company)
                                        .FirstOrDefault();
            }
            return batch;
        }

        /// <summary>
        /// Load Batch With Batch Items by token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Batch.</returns>
        public Batch LoadBatchForBatchItemDocuments(string token)
        {
            return _repositoryBatch.Query(d => d.Token == token)
                                             .Include(v => v.BatchItems)
                                             .ThenInclude(g => g.BatchItemPages)
                                             .FirstOrDefault();
        }

        /// <summary>
        /// Get Batch by token.
        /// </summary>
        /// <param name="token">The batch token.</param>
        /// <returns>Batch.</returns>
        public async Task<Batch> GetBatchByToken(string token)
        {
            return await _repositoryBatch.Query(d => d.Token == token).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get BatchSourceUploadDoc list of specific batchSourceId.
        /// </summary>
        /// <param name="batchSourceId">The batchSourceId.</param>
        /// <returns>BatchSourceUploadDoc list.</returns>
        public List<BatchSourceUploadDocDTO> BatchSourceUploadDoc(int batchSourceId)
        {
            var batchSourceUploadDocs = _repositoryBatchSourceUploadDoc.Query(d => d.BatchSourceId == batchSourceId).ToList();
            return _mapper.Map<List<BatchSourceUploadDocDTO>>(batchSourceUploadDocs);
        }

        /// <summary>
        /// get batch history list of specific batchId.
        /// </summary>
        /// <param name="batchId">The batchId.</param>
        /// <returns>Batch history list.</returns>
        public List<BatchHistory> GetBatchHistories(int batchId)
        {
            var batchHistories = _repositoryBatchHistory.Query(d => d.BatchId == batchId).ToList();
            batchHistories = batchHistories.Where(b => b.IsLast == true || Convert.ToBoolean(b.VerificationStatus)).ToList();

            return batchHistories;
        }

        /// <summary>
        ///VwDocumentGroupNamesFormBatchHistory list.
        /// </summary>
        /// <param name="ids">batch history Id list.</param>
        /// <returns>view .</returns>
        public List<VwDocumentGroupNamesFormBatchHistory> GetBatchHistorItems(List<int> ids)
        {
            return _repositoryDocumentGroupNamesFormBatchHistory
                                        .Query(b => ids.Contains(b.BatchHistoryId))
                                        .ToList();
        }

        /// <summary>
        ///DboVwDocumentGroupNamesForBatchHistory list.
        /// </summary>
        /// <param name="batchHistoryId">The batch history Id.</param>
        /// <returns>DboVwDocumentGroupNamesForBatchHistory.</returns>
        public List<DboVwDocumentGroupNamesForBatchHistory> GetBatchHistoryItemsForBatchHistory(int batchHistoryId)
        {
            return _repositoryDboVwDocumentGroupNamesForBatchHistory
                                        .Query(b => b.BatchHistoryId == batchHistoryId)
                                        .ToList();
        }

        /// <summary>
        /// Get Batch History Item By Id.
        /// </summary>
        /// <param name="id">The batchHistoryItemId.</param>
        /// <returns>BatchHistoryItem.</returns>
        public BatchHistoryItem GetBatchHistoryItemById(int id)
        {
            return _repositoryBatchHistoryItem
                                        .Query(b => b.Id == id)
                                        .FirstOrDefault();
        }

        /// <summary>
        ///VwGetBatchHistoryItemField list.
        /// </summary>
        /// <param name="batchHistoryItemId">batchHistoryItemId.</param>
        /// <returns>view .</returns>
        public async Task<List<VwGetBatchHistoryItemField>> GetBatchHistoryItemFields(int batchHistoryItemId)
        {
            return await _repositoryVwGetBatchHistoryItemField
                                       .Query(b => b.Id == batchHistoryItemId).ToListAsync();
        }

        /// <summary>
        ///VwGetBatchItemField list.
        /// </summary>
        /// <param name="batchItemId">batchHistoryItemId.</param>
        /// <returns>view .</returns>
        public async Task<List<VwGetBatchItemField>> GetBatchItemFields(int batchItemId)
        {
            return await _repositoryVwGetBatchItemField
                                       .Query(b => b.BatchItemId == batchItemId).ToListAsync();
        }

        /// <summary>
        /// List of CustomerData by using factory pattern.
        /// </summary>
        /// <param name="batch">The batch model.</param>
        /// <returns>List of CustomerData.</returns>
        public List<CustomerData> GetCustomerData(Batch batch)
        {
            var company = GetCompanyById(batch.CompanyId);

            CompanyActionsFactory companyService = new CompanyActionsFactory();
            ICompanyActions companyAction = companyService.GetCompanyActions(company.Code);
            List<CustomerData> customerInfo = new List<CustomerData>();
            customerInfo = companyAction.AgentCustomerData(batch);

            return customerInfo;
        }

        /// <summary>
        /// Get Company By Id.
        /// </summary>
        /// <param name="id">The companyId.</param>
        /// <returns>Company.</returns>
        public Company GetCompanyById(int id)
        {
            return _repositoryCompany.Query(s => s.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Get Company By Id.
        /// </summary>
        /// <param name="id">The companyId.</param>
        /// <returns>Company.</returns>
        public Company GetCompanyByName(string name)
        {
            return _repositoryCompany.Query(s => s.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Load User With User Companies.
        /// </summary>
        /// <param name="systemUserId">The systemUserId.</param>
        /// <returns>SystemUser.</returns>
        public SystemUser LoadUserWithUserCompanies(int systemUserId)
        {
            //get SystemUser of current agent with UserCompanies and AspNetUsers
            return _repositorySystemUser.Query(su => su.Id == systemUserId)
                                        .Include(su => su.AspNetUsers)
                                        .Include(su => su.UserCompanies)
                                        .FirstOrDefault();
        }

        /// <summary>
        /// Get Batch Item By Batch Id.
        /// </summary>
        /// <param name="batchId">The batch id.</param>
        /// <returns>BatchItem list.</returns>
        public List<BatchItem> GetBatchItemByBatchId(int batchId)
        {
            return _repositoryBatchItem.Query(d => d.BatchId == batchId).ToList();
        }

        /// <summary>
        /// Get Batch Item Pages.
        /// </summary>
        /// <param name="batchItemId">The batchItemId.</param>
        /// <returns>BatchItemPage list.</returns>
        public List<BatchItemPage> GetBatchItemPages(int batchItemId)
        {
            return _repositoryBatchItemPage.Query(d => d.BatchItemId == batchItemId).ToList();
        }

        /// <summary>
        /// get batch history of specific batchId.
        /// </summary>
        /// <param name="batchId">The batchId.</param>
        /// <returns>Batch history.</returns>
        public BatchHistory GetBatchHistoryByBatchId(int batchId)
        {
            return _repositoryBatchHistory.Query(d => d.BatchId == batchId && d.IsLast).FirstOrDefault();
        }

        /// <summary>
        /// Get meta data of batchId and documentClassFieldId.
        /// </summary>
        /// <param name="batchId">The batchId.</param>
        /// <param name="documentClassFieldId">The documentClassFieldId.</param>
        /// <returns>Batch meta field value.</returns>
        public string GetBatchMeta(int batchId, int documentClassFieldId)
        {
            var batchMeta = _repositoryBatchMetum.Query(b => b.BatchId == batchId && b.DocumentClassFieldId == documentClassFieldId).FirstOrDefault();
            return batchMeta != null ? batchMeta.FieldValue : null;
        }

        /// <summary>
        /// get BatchHistoryItemPage of specific pageId.
        /// </summary>
        /// <param name="pageId">The pageId.</param>
        /// <returns>BatchHistoryItemPage list.</returns>
        public List<BatchHistoryItemPage> GetBatchHistoryItemPage(int pageId)
        {
            return _repositoryBatchHistoryItemPage.Query(b => b.Id == pageId).ToList();
        }

        /// <summary>
        /// Get Batch History Item Page by History Item Id.
        /// </summary>
        /// <param name="batchHistoryItemId">The batchHistoryItemId.</param>
        /// <returns>BatchHistoryItemPage list.</returns>
        public List<BatchHistoryItemPage> GetBatchHistoryItemPagebyHistoryItemId(int batchHistoryItemId)
        {
            return _repositoryBatchHistoryItemPage.Query(b => b.BatchHistoryItemId == batchHistoryItemId && (b.IsLast ?? false)).ToList();
        }

        /// <summary>
        /// Get All Pending Batches For Call.
        /// </summary>
        /// <returns>string result.</returns>
        public List<BatchVideoPriority> GetAllPendingBatchesForCall()
        {
            return _repositoryBatchVideoPriority.Query().ToList();
        }

        /// <summary>
        /// get pending batch by SRID.
        /// </summary>
        /// <param name="SRID">The SRID.</param>
        /// <returns>BatchVideoPriority.</returns>
        public BatchVideoPriority GetPendingBatchBySRID(string SRID)
        {
            BatchVideoPriority batchVideoPriority = _repositoryBatchVideoPriority.Query(bvp => bvp.RequestId.Equals(SRID)).FirstOrDefault();
            return batchVideoPriority;
        }

        /// <summary>
        /// Get BatchMeta List By Batch Id.
        /// </summary>
        /// <param name="batchId">The batchId.</param>
        /// <returns>Batch meta list .</returns>
        public List<BatchMetum> GetBatchMetaListByBatchId(int batchId)
        {
            return _repositoryBatchMetum.Query(b => b.BatchId == batchId).ToList();
        }

        /// <summary>
        /// Get Batch History Meta List By History Id.
        /// </summary>
        /// <param name="batchHistoryId">The batchHistoryId.</param>
        /// <returns>BatchHistoryMetum list .</returns>
        public List<BatchHistoryMetum> GetBatchHistoryMetaListByHistoryId(int batchHistoryId)
        {
            return _repositoryBatchHistoryMetum.Query(b => b.BatchHistoryId == batchHistoryId)
                                                .ToList();
        }

        /// <summary>
        /// Get Aplha Bank Company Id.
        /// </summary>
        /// <returns>int list .</returns>
        public List<int> GetAplhaBankCompanyId()
        {
            var alphaCompany = _repositoryCompany.Query(d => d.Name == CompanyKeys.AlphaBank).ToList();
            return alphaCompany.Select(s => s.Id).ToList();
        }

        /// <summary>
        /// Get GetAspnetUser by system user id.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <returns>Batch.</returns>
        public int GetAspnetUser(int userId)
        {
            return _repositoryAspNetUser.Query(d => d.SystemUserId == userId).FirstOrDefault().Id;
        }

        /// <summary>
        /// Get Batch Directory.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <returns>string.</returns>
        public string GetBatchDirectory(Batch batch)
        {
            var batchOutputPath = _bopConfigService.GetBopConfigByEnumValue(BopConfigs.BATCH_FILES_NETWORK_PATH);
            var directory = Path.Combine(batchOutputPath, batch.CreatedDate.IsoYearMonth(true), batch.CreatedDate.IsoDate(true), batch.Id.ToString());
            return directory;
        }

        /// <summary>
        /// get all pending batches.
        /// </summary>
        /// <param name="agentId">The agentId.</param>
        /// <returns>PendingBatchesDTO list.</returns>
        public async Task<PendingBatchesDTO> GetPendingBatchPage(int agentId)
        {
            //get connection string to run stored procedure
            var connectionString = _configuration.GetConnectionString("SqlConnection");

            PendingBatchesDTO dto = new PendingBatchesDTO();

            await using (SqlConnection cn = new SqlConnection(connectionString))

            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetPage_VideoPendingBatch2", cn) { CommandType = CommandType.StoredProcedure, CommandTimeout = 30000 })
            {
                cn.Open();

                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters[1].Value = agentId;

                DataSet ds = new DataSet();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd)) { adapter.Fill(ds); }
                cn.Close();

                // For each row, print the values of each column.
                foreach (DataRow row in ds.Tables[0].Rows) // BatcMeta
                {
                    PendingBatchesVideoPriorityDTO bvp = new PendingBatchesVideoPriorityDTO();
                    bvp.Id = Convert.ToInt32(row[0]);
                    bvp.Priotity = Convert.ToInt32(row[1].ToString());
                    bvp.EstimatedTimeInMin = Convert.ToInt32(row[2].ToString());
                    bvp.CompanyId = Convert.ToInt32(row[3].ToString());
                    bvp.MaxCallTime = String.IsNullOrEmpty(row[4].ToString()) ? 0 : Convert.ToInt32(row[4].ToString());
                    bvp.CreatedDate = String.IsNullOrEmpty(row[5].ToString()) ? DateTime.Now.Date : Convert.ToDateTime(row[5].ToString());
                    bvp.BatchStatusId = String.IsNullOrEmpty(row[6].ToString()) ? 0 : Convert.ToInt32(row[6].ToString());
                    bvp.RequestId = row[7].ToString();

                    if (String.IsNullOrEmpty(row[8].ToString()))
                    {
                        bvp.LockedBy = null;
                    }
                    else
                    {
                        //fill user model
                        bvp.LockedBy = Convert.ToInt32(row[8].ToString());
                        bvp.User.Id = Convert.ToInt32(bvp.LockedBy);

                        var getUserName = _repositoryAspNetUser.Query(user => user.Id.Equals(bvp.User.Id)).FirstOrDefault()?.Email;
                        bvp.User.UserName = getUserName == null ? null : getUserName;
                    }
                    if (String.IsNullOrEmpty(row[9].ToString()))
                        bvp.CountryOfOrigin = null;
                    else
                        bvp.CountryOfOrigin = row[9].ToString();

                    if (String.IsNullOrEmpty(row[3].ToString()))
                    {
                        bvp.CompanyId = null;
                    }
                    else
                    {
                        bvp.CompanyId = Convert.ToInt32(row[3].ToString());
                        bvp.Company.Id = Convert.ToInt32(bvp.CompanyId);
                        var getCompany = _repositoryCompany.Query(d => d.Id == bvp.Company.Id).FirstOrDefault();
                        bvp.Company.Name = getCompany.Name;
                        bvp.Company.Code = getCompany.Code;
                    }

                    dto.VideoPriority.Add(bvp);
                    bvp = null;
                }
            }
            return dto;
        }

        /// <summary>
        /// Get Uploaded Documents For Agent Index.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>AgentViewDTO result.</returns>
        public async Task<AgentViewDTO> GetUploadedDocumentsForAgentIndex(string requestId)
        {
            AgentViewDTO agentViewModel = new AgentViewDTO();
            //get batch by request id
            var batch = await GetBatchByToken(requestId);

            if (batch == null)
                return agentViewModel = null;

            var batchSourceUploadDocs = new List<BatchSourceUploadDocDTO>();
            var signedDocuments = new List<UploadedDocumentsDTO>();
            var batchHistoryItems = new List<DboVwDocumentGroupNamesForBatchHistory>();

            //Get needed uploaded docs for this BatchSource
            batchSourceUploadDocs = BatchSourceUploadDoc(batch.BatchSourceId);
            signedDocuments = GetAllSignedDocuments(batch)?.ToList();
            
            var getbatchHistory = GetBatchHistoryByBatchId(batch.Id);
            BatchHistoriesDTO batchHistoryLast = _mapper.Map<BatchHistoriesDTO>(getbatchHistory);

            if (batchHistoryLast != null)
                batchHistoryItems = GetBatchHistoryItemsForBatchHistory(batchHistoryLast.Id);

            //Check if all needed documents are uploaded
            var batchUploadedDocuments = batchHistoryItems.GroupBy(l => l.DocumentGroupNameId).Count();
            var batchNeedDocuments = batchSourceUploadDocs.Count();

            agentViewModel.Token = batch.Token;
            agentViewModel.AllUploadedDocumentsExist = (batchUploadedDocuments == batchNeedDocuments) ? true : false;
            agentViewModel.BatchHistoryItems = batchHistoryItems ?? new List<DboVwDocumentGroupNamesForBatchHistory>();
            agentViewModel.SignedDocuments = signedDocuments ?? new List<UploadedDocumentsDTO>();

            return agentViewModel;
        }

        /// <summary>
        /// Get Signed Documents For Agent Index.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>AgentViewDTO result.</returns>
        public async Task<AgentViewDTO> GetSignedDocumentsForAgentIndex(string requestId)
        {
            AgentViewDTO agentViewModel = new AgentViewDTO();
            //get batch by request id
            var batch = await GetBatchByToken(requestId);
            if (batch == null)
                return agentViewModel = null;

            var signedDocuments = new List<UploadedDocumentsDTO>();
            signedDocuments = GetAllSignedDocuments(batch)?.ToList();
            agentViewModel.Token = batch.Token;
            agentViewModel.SignedDocuments = signedDocuments ?? new List<UploadedDocumentsDTO>();

            return agentViewModel;
        }

        /// <summary>
        /// Get Verification Results For Agent Index.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>AgentViewDTO result.</returns>
        public async Task<AgentViewDTO> GetVerificationResultsForAgentIndex(string requestId)
        {
            AgentViewDTO agentViewModel = new AgentViewDTO();
            //get batch by request id
            var batch = await GetBatchByToken(requestId);
            if (batch == null)
                return agentViewModel = null;

            var getbatchHistory = GetBatchHistoryByBatchId(batch.Id);
            BatchHistoriesDTO batchHistoryLast = _mapper.Map<BatchHistoriesDTO>(getbatchHistory);
            var verificationRejectionReasons = new List<string>();

            string faceMatching = batchHistoryLast?.FaceMatching;
            string isAlive = batchHistoryLast?.IsAlive;
            string verificationStatus = batchHistoryLast?.VerificationStatus.ToString();
            verificationRejectionReasons = batchHistoryLast?.VerificationRejectionReason?.Split(',').Select(x => x.Trim()).ToList();

            agentViewModel.Token = batch.Token;
            agentViewModel.batch = _mapper.Map<FullBatchDto>(batch);
            agentViewModel.VerificationRejectionReasons = verificationRejectionReasons != null ? verificationRejectionReasons.Select(x => x.Trim()).Distinct().ToList() : null;
            agentViewModel.FaceMatching = faceMatching ?? "";
            agentViewModel.IsAlive = isAlive ?? "";
            agentViewModel.VerificationStatus = verificationStatus ?? "";

            return agentViewModel;
        }

        /// <summary>
        /// Get Uploaded Documents For Agent Jumio.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>AgentViewDTO result.</returns>
        public async Task<AgentViewDTO> GetUploadedDocumentsForAgentJumio(string requestId)
        {
            AgentViewDTO agentViewModel = new AgentViewDTO();
            //get batch by request id
            var batch = await GetBatchByToken(requestId);
            if (batch == null)
                return agentViewModel = null;

            var batchSourceUploadDocs = new List<BatchSourceUploadDocDTO>();
            var signedDocuments = new List<UploadedDocumentsDTO>();
            var batchHistoryItems = new List<DboVwDocumentGroupNamesForBatchHistory>();

            //Get needed uploaded docs for this BatchSource
            batchSourceUploadDocs = BatchSourceUploadDoc(batch.BatchSourceId);
            signedDocuments = GetAllSignedDocuments(batch)?.ToList();
           
            var getbatchHistory = GetBatchHistoryByBatchId(batch.Id);
            BatchHistoriesDTO batchHistoryLast = _mapper.Map<BatchHistoriesDTO>(getbatchHistory);

            if (batchHistoryLast != null)
                batchHistoryItems = GetBatchHistoryItemsForBatchHistory(batchHistoryLast.Id);
            
            //Check if all needed documents are uploaded
            var batchUploadedDocuments = batchHistoryItems.GroupBy(l => l.DocumentGroupNameId).Count();
            var batchNeedDocuments = batchSourceUploadDocs.Count();

            agentViewModel.Token = batch.Token;
            agentViewModel.AllUploadedDocumentsExist = (batchUploadedDocuments == batchNeedDocuments) ? true : false;
            agentViewModel.BatchHistoryItems = batchHistoryItems ?? new List<DboVwDocumentGroupNamesForBatchHistory>();
            agentViewModel.SignedDocuments = signedDocuments ?? new List<UploadedDocumentsDTO>();

            return agentViewModel;
        }

        /// <summary>
        /// Get Verification Results For Agent Jumio.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>AgentViewDTO result.</returns>
        public async Task<AgentViewDTO> GetVerificationResultsForAgentJumio(string requestId)
        {
            AgentViewDTO agentViewModel = new AgentViewDTO();
            //get batch by request id
            var batch = await GetBatchByToken(requestId);
            if (batch == null)
                return agentViewModel = null;

            var getbatchHistory = GetBatchHistoryByBatchId(batch.Id);
            BatchHistoriesDTO batchHistoryLast = _mapper.Map<BatchHistoriesDTO>(getbatchHistory);
            var verificationRejectionReasons = new List<string>();

            string faceMatching = batchHistoryLast?.FaceMatching;
            string isAlive = batchHistoryLast?.IsAlive;
            string verificationStatus = batchHistoryLast?.VerificationStatus.ToString();
            verificationRejectionReasons = batchHistoryLast?.VerificationRejectionReason?.Split(',').Select(x => x.Trim()).ToList();

            agentViewModel.Token = batch.Token;
            agentViewModel.batch = _mapper.Map<FullBatchDto>(batch);
            agentViewModel.VerificationRejectionReasons = verificationRejectionReasons != null ? verificationRejectionReasons.Select(x => x.Trim()).Distinct().ToList() : null;
            agentViewModel.FaceMatching = faceMatching ?? "";
            agentViewModel.IsAlive = isAlive ?? "";
            agentViewModel.VerificationStatus = verificationStatus ?? "";

            return agentViewModel;
        }

        /// <summary>
        /// get agentviewModel for agent view screen.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userName">The userName.</param>
        /// <param name="userId">TheuserId.</param>
        /// <returns>AgentViewModel.</returns>
        public AgentViewDTO GetAgentIndex(string requestId, string userName, int userId)
        {
            AgentViewDTO agentViewModel = new AgentViewDTO();

            if (String.IsNullOrEmpty(requestId))
            {
                agentViewModel = null;
                return agentViewModel;
            }

            var verificationRejectionReasons = new List<string>();
            var batchSourceUploadDocs = new List<BatchSourceUploadDocDTO>();
            var signedDocuments = new List<UploadedDocumentsDTO>();
            var batchHistoryItems = new List<DboVwDocumentGroupNamesForBatchHistory>();
            var infoDocClassFields = new List<VwInfoDocClassField>();
            var registeredDocFields = new List<VwRegisterPersonalInfoField>();

            Batch fullBatch = LoadBatchWthBatchMetas(requestId, userId);
            FullBatchDto batch = _mapper.Map<FullBatchDto>(fullBatch);
            if (batch == null)
            {
                agentViewModel = null;
                return agentViewModel;
            }

            //Get needed uploaded docs for this BatchSource
            batchSourceUploadDocs = BatchSourceUploadDoc(batch.BatchSourceId);

            signedDocuments = GetAllSignedDocuments(fullBatch)?.ToList();

            var getbatchHistory = GetBatchHistoryByBatchId(batch.Id);
            BatchHistoriesDTO batchHistoryLast = _mapper.Map<BatchHistoriesDTO>(getbatchHistory);

            if (batchHistoryLast != null)
                batchHistoryItems = GetBatchHistoryItemsForBatchHistory(batchHistoryLast.Id);

            //  Customer model
            CustomerModel customerModel = _customerDetailsManager.GetCustomerModel(fullBatch);

            //Find InfoDocClassFields
            infoDocClassFields = _repositoryInfoDocClassField.Query(d => d.BatchSourceId == batch.BatchSourceId).OrderBy(f => f.Ordering).ToList();

            //Find Registered Values
            registeredDocFields = _repositoryVwRegisterPersonalInfoField.Query(s => s.BatchId == batch.Id).ToList();

            //test binding
            List<BM> inputs = new List<BM>();
            for (var i = 0; i < infoDocClassFields.Count(); i++)
            {
                if (i < registeredDocFields.Count())
                    inputs.Add(new BM { FriendlyName = infoDocClassFields[i].EnumValue.Replace("Gr", "").Replace("En", ""), FieldValue = registeredDocFields[i].FieldValue.ToUpper() });
                else
                    inputs.Add(new BM { FriendlyName = infoDocClassFields[i].EnumValue.Replace("Gr", "").Replace("En", ""), FieldValue = "" });
            }

            verificationRejectionReasons = batchHistoryLast?.VerificationRejectionReason?.Split(',').Select(x => x.Trim()).ToList();

            //Check if all needed documents are uploaded
            var batchUploadedDocuments = batchHistoryItems.GroupBy(l => l.DocumentGroupNameId).Count();
            var batchNeedDocuments = batchSourceUploadDocs.Count();

            agentViewModel.batchHistoryLast = batchHistoryLast;
            agentViewModel.CustomerModel = customerModel;
            agentViewModel.RegisterPersonalInfoFields = registeredDocFields;
            agentViewModel.inputs = inputs;
            agentViewModel.Token = batch.Token;
            agentViewModel.batch = batch;
            agentViewModel.VerificationRejectionReasons = verificationRejectionReasons != null ? verificationRejectionReasons.Select(x => x.Trim()).Distinct().ToList() : null;
            agentViewModel.FaceMatching = batchHistoryLast?.FaceMatching ?? "";
            agentViewModel.IsAlive = batchHistoryLast?.IsAlive ?? "";
            agentViewModel.VerificationStatus = batchHistoryLast?.VerificationStatus.ToString() ?? "";
            agentViewModel.BatchSourceUploadDocs = batchSourceUploadDocs;
            agentViewModel.batchNeedDocuments = batchNeedDocuments;
            agentViewModel.AllUploadedDocumentsExist = (batchUploadedDocuments == batchNeedDocuments) ? true : false;
            agentViewModel.BatchHistoryItems = batchHistoryItems ?? new List<DboVwDocumentGroupNamesForBatchHistory>();
            agentViewModel.SignedDocuments = signedDocuments ?? new List<UploadedDocumentsDTO>();

            //open tok credentials
            string TokBoxApikey = _configuration.GetSection("projectApiKey").Value;
            var Response = CreateSessionAgent(fullBatch, userName);
            agentViewModel.TokBoxApiKey = TokBoxApikey;
            agentViewModel.TokBoxSessionId = Response.TokBoxSessionId;
            agentViewModel.TokBoxSessionToken = Response.TokBoxToken;
            agentViewModel.TokBoxSRID = requestId;

            return agentViewModel;
        }

        /// <summary>
        /// Load Batch Wth Batch Metas.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userId">The userId.</param>
        /// <param name="isIndexOnlyView">isIndexOnlyView.</param>
        /// <returns>AgentViewModel.</returns>
        public Batch LoadBatchWthBatchMetas(string requestId, int userId, bool isIndexOnlyView = false)
        {
            SystemUser systemUser = LoadUserWithUserCompanies(userId);
            var aspNetUserIds = systemUser.AspNetUsers.Select(nu => nu.Id).ToList();
            var userCompaniesIds = systemUser.UserCompanies.Select(uc => uc.CompanyId).ToList();
            Batch batch = null;

            if (systemUser != null)
            {
                batch = _repositoryBatch.Query(b => b.RequestId == requestId &&
                                                (systemUser.UserCompanies.Count() == 0 || userCompaniesIds.Contains(b.CompanyId)) &&
                                                (isIndexOnlyView || aspNetUserIds.Contains(b.LockedBy ?? 0)))
                                        .Include(f => f.BatchMeta)
                                        .Include(c => c.Company)
                                        .FirstOrDefault();
            }
            return batch;
        }

        /// <summary>
        /// get agentviewModel for agent video screen.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userName">The userName.</param>
        /// <param name="userId">TheuserId.</param>
        /// <returns>AgentViewModel.</returns>
        public AgentViewDTO GetAgentVideoIndex(string requestId, string userName, int userId)
        {
            AgentViewDTO agentViewModel = new AgentViewDTO();
            if (String.IsNullOrEmpty(requestId))
            {
                agentViewModel = null;
                return agentViewModel;
            }

            Batch fullBatch = new Batch();
            var batchHistoryLast = new BatchHistoriesDTO();
            var verificationRejectionReasons = new List<string>();
            List<BatchItemVMDTO> batchItems = new List<BatchItemVMDTO>();

            fullBatch = LoadFullBatch(requestId, userId);
            var batch = _mapper.Map<FullBatchDto>(fullBatch);

            var publishedStatusId = _batchStatusService.GetBatchStatusIdByEnumValue(BatchStatusesKeys.PUBLISHED);
            var businessRulesStatusId = _batchStatusService.GetBatchStatusIdByEnumValue(BatchStatusesKeys.BUSINESS_RULES_RUN);
            if (batch == null || batch.BatchStatusId == publishedStatusId || batch.BatchStatusId == businessRulesStatusId)
            {
                agentViewModel = null;
                return agentViewModel;
            }

            // Get Customer Registered Data
            var customerInfo = GetCustomerData(fullBatch);

            //Get Company's Prado Validations
            var pradoCheckList = GetPradoChecksForCompanyByToken(fullBatch);

            //Get Information From DB using sp_GetPage_AgentVideo
            var agentVideoPage = GetAgentVideoPage(requestId);

            // Find the Last Batch History of the transaction
            var getbatchHistory = fullBatch.BatchHistories.Where(x => x.Id == agentVideoPage.LastBatchHistoryId).FirstOrDefault();
            batchHistoryLast = _mapper.Map<BatchHistoriesDTO>(getbatchHistory);
            /* Seperate multiple rejection reasons into a List of strings  */
            verificationRejectionReasons = agentVideoPage.VerificationRejectionReason?.Split(',').Select(x => x.Trim()).ToList();
            // Batch Items extraction here
            foreach (var bi in agentVideoPage.batchItemsViewModel)
                batchItems.Add(new BatchItemVMDTO(bi, requestId));

            // Check if all needed documents are uploaded
            var batchUploadedDocuments = agentVideoPage.batchHistoryItems.GroupBy(l => l.DocumentGroupNameId).Count();
            var batchNeedDocuments = agentVideoPage.batchSourceUploadDocs.Count();

            // VideoValidationControl
            var docClassId = _documentClassFieldService.GetDocumentClassByEnumValue(DocumentClassKeys.VideoValidationControl).Id;
            var getDocClassField = _documentClassFieldService.GetDocumentClassFields(docClassId).ToList();
            var documentClassFields = _mapper.Map<List<DocumentClassFieldDTO>>(getDocClassField);

            List<VideoValidationControlModel> validationControlModelList = new List<VideoValidationControlModel>();

            documentClassFields.ForEach((item) =>
            {
                VideoValidationControlModel validationControlModel = new VideoValidationControlModel();

                validationControlModel.DocumentClassField = item;
                validationControlModel.FieldValue = "";
                validationControlModelList.Add(validationControlModel);

            });

            /* Preparation os ViewModel*/
            agentViewModel.Token = batch.Token;
            agentViewModel.CompanyLogo = agentVideoPage.Logo;
            agentViewModel.batch = batch;
            agentViewModel.batchHistoryLast = batchHistoryLast;
            agentViewModel.VerificationStatus = batchHistoryLast.VerificationStatus.ToString();
            agentViewModel.VerificationRejectionReasons = verificationRejectionReasons != null ? verificationRejectionReasons.Select(x => x.Trim()).Distinct().ToList() : null;
            agentViewModel.FaceMatching = batchHistoryLast?.FaceMatching ?? "";
            agentViewModel.IsAlive = batchHistoryLast?.IsAlive ?? "";
            agentViewModel.BatchSourceUploadDocs = _mapper.Map<List<BatchSourceUploadDocDTO>>(agentVideoPage.batchSourceUploadDocs);
            agentViewModel.BatchHistoryItems = agentVideoPage.batchHistoryItems;
            agentViewModel.BatchItems = batchItems;
            agentViewModel.CustomerInfo = customerInfo;
            agentViewModel.PradoCheckList = pradoCheckList;
            agentViewModel.AllUploadedDocumentsExist = (batchUploadedDocuments == batchNeedDocuments) ? true : false;
            agentViewModel.vvcmList = validationControlModelList;
            agentViewModel.SelfiePortraitSimilarity = agentVideoPage.SelfiePortraitSimilarity;

            string TokBoxApikey = _configuration.GetSection("projectApiKey").Value;
            var Response = CreateSessionAgent(fullBatch, userName);
            agentViewModel.TokBoxApiKey = TokBoxApikey;
            agentViewModel.TokBoxSessionId = Response.TokBoxSessionId;
            agentViewModel.TokBoxSessionToken = Response.TokBoxToken;
            agentViewModel.TokBoxSRID = requestId;

            agentViewModel.SimilarityByAgentId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.SimilarityByAgent).Id;
            agentViewModel.VerificationStatusId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VerificationStatus).Id;
            agentViewModel.VideoSessionResultId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VideoSessionResult).Id;
            agentViewModel.VideoVerificationStatusId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VideoVerificationStatus).Id;
            agentViewModel.VideoRejectionReasonId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VideoRejectionReason).Id;
            agentViewModel.VideoValidityId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VideoValidity).Id;

            return agentViewModel;
        }

        /// <summary>
        /// get agentviewModel for agent video With Jumio screen.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userName">The userName.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>AgentViewModel.</returns>
        public AgentViewDTO GetAgentVideoWithJumio(string requestId, string userName, int userId)
        {
            AgentViewDTO agentViewModel = new AgentViewDTO();

            if (String.IsNullOrEmpty(requestId))
            {
                agentViewModel = null;
                return agentViewModel;
            }

            var verificationRejectionReasons = new List<string>();
            var signedDocuments = new List<UploadedDocumentsDTO>();
            var batchHistoryItems = new List<DboVwDocumentGroupNamesForBatchHistory>();
            var batchSourceUploadDocs = new List<BatchSourceUploadDocDTO>();

            Batch fullBatch = LoadFullBatch(requestId, userId);
            FullBatchDto batch = _mapper.Map<FullBatchDto>(fullBatch);
            if (batch == null)
            {
                agentViewModel = null;
                return agentViewModel;
            }

            //Get needed uploaded docs for this BatchSource
            batchSourceUploadDocs = BatchSourceUploadDoc(batch.BatchSourceId);

            signedDocuments = GetAllSignedDocuments(fullBatch)?.ToList();

            var getbatchHistory = GetBatchHistoryByBatchId(batch.Id);
            BatchHistoriesDTO batchHistoryLast = _mapper.Map<BatchHistoriesDTO>(getbatchHistory);

            if (batchHistoryLast != null)
                batchHistoryItems = GetBatchHistoryItemsForBatchHistory(batchHistoryLast.Id);

            //  Customer model
            CustomerModel customerModel = _customerDetailsManager.GetCustomerModel(fullBatch);

            // Get Customer Registered Data
            var customerInfo = GetCustomerData(fullBatch);

            //Get Company's Prado Validations
            var pradoCheckList = GetPradoChecksForCompanyByToken(fullBatch);

            verificationRejectionReasons = batchHistoryLast?.VerificationRejectionReason?.Split(',').Select(x => x.Trim()).ToList();

            if (verificationRejectionReasons != null)
            {
                if (verificationRejectionReasons.Where(x => x.Trim() == "R029" || x.Trim() == "R030" || x.Trim() == "R031").Count() > 0) //POA RejectionReasons
                {
                    batchHistoryItems.Where(x => x.DocumentGroupNameId == (int)DocumentGroupNames.PoA).ToList().ForEach(c => c.IsValid = false);
                }
                if (verificationRejectionReasons.Where(x => x.Trim() == "R026" || x.Trim() == "R027" || x.Trim() == "R028").Count() > 0) //Identity RejectionReasons
                {
                    batchHistoryItems.Where(x => x.DocumentGroupNameId == (int)DocumentGroupNames.Identity).ToList().ForEach(c => c.IsValid = false);
                }
                if (verificationRejectionReasons.Where(x => x.Trim() == "R023" || x.Trim() == "R024" || x.Trim() == "R025").Count() > 0) //Tax RejectionReasons
                {
                    batchHistoryItems.Where(x => x.DocumentGroupNameId == (int)DocumentGroupNames.TaxCode).ToList().ForEach(c => c.IsValid = false);
                }
                if (verificationRejectionReasons.Where(x => x.Trim() == "R038" || x.Trim() == "R039" || x.Trim() == "R040").Count() > 0) //Tax RejectionReasons
                {
                    batchHistoryItems.Where(x => x.DocumentGroupNameId == (int)DocumentGroupNames.Other).ToList().ForEach(c => c.IsValid = false);
                }
            }

            //Check if all needed documents are uploaded
            var batchUploadedDocuments = batchHistoryItems.GroupBy(l => l.DocumentGroupNameId).Count();
            var batchNeedDocuments = batchSourceUploadDocs.Count();

            //for jumio results
            var batchHistoryMetas = _repositoryVwGetBatchHistoryMeta.Query(s => s.Id == getbatchHistory.Id).ToList();

            /* Preparation os ViewModel*/
            agentViewModel.batchHistoryLast = batchHistoryLast;
            agentViewModel.BatchHistoryMetas = batchHistoryMetas;
            agentViewModel.CustomerModel = customerModel;
            agentViewModel.Token = batch.Token;
            agentViewModel.batch = batch;
            agentViewModel.VerificationRejectionReasons = verificationRejectionReasons != null ? verificationRejectionReasons.Select(x => x.Trim()).Distinct().ToList() : null;
            agentViewModel.FaceMatching = batchHistoryLast?.FaceMatching ?? "";
            agentViewModel.IsAlive = batchHistoryLast?.IsAlive ?? "";
            agentViewModel.VerificationStatus = batchHistoryLast.VerificationStatus.ToString();
            agentViewModel.BatchSourceUploadDocs = batchSourceUploadDocs;
            agentViewModel.CustomerInfo = customerInfo;
            agentViewModel.PradoCheckList = pradoCheckList;
            agentViewModel.AllUploadedDocumentsExist = (batchUploadedDocuments == batchNeedDocuments) ? true : false;
            agentViewModel.BatchHistoryItems = batchHistoryItems ?? new List<DboVwDocumentGroupNamesForBatchHistory>();
            agentViewModel.SignedDocuments = signedDocuments ?? new List<UploadedDocumentsDTO>();
            agentViewModel.ClientConcentId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.Consent).Id;
            agentViewModel.LivenessId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.IsAlive).Id;
            agentViewModel.VideoSessionResultId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.VideoSessionResult).Id;
            agentViewModel.FeeWillId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.FreeWill).Id;

            string TokBoxApikey = _configuration.GetSection("projectApiKey").Value;
            var Response = CreateSessionAgent(fullBatch, userName);
            agentViewModel.TokBoxApiKey = TokBoxApikey;
            agentViewModel.TokBoxSessionId = Response.TokBoxSessionId;
            agentViewModel.TokBoxSessionToken = Response.TokBoxToken;
            agentViewModel.TokBoxSRID = requestId;

            return agentViewModel;
        }

        /// <summary>
        /// Create opentok Session for Agent.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="userName">The userName.</param>
        /// <returns>TokBoxSession.</returns>
        public TokBoxSession CreateSessionAgent(Batch batch, string userName)
        {
            TokBoxSession tokBoxResponse = new TokBoxSession();
            OpenTokSDK.Session session = null;

            int projectApiKey = Convert.ToInt32(_configuration.GetSection("projectApiKey").Value);
            string projectSecret = _configuration.GetSection("projectSecret").Value;
            OpenTok opentok = new OpenTok(projectApiKey, projectSecret);

            string tokBoxSessionId = GetBatchMeta(batch.Id, _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.TokBoxSessionId).Id);
            string tokBoxTokenAgent = GetBatchMeta(batch.Id, _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.TokBoxTokenAgent).Id);
            string tokBoxRegisteredDate = GetBatchMeta(batch.Id, _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.TokBoxRegisterDate).Id);

            if (string.IsNullOrEmpty(tokBoxSessionId))
            {
                opentok = new OpenTok(projectApiKey, projectSecret);
                session = opentok.CreateSession("", MediaMode.ROUTED, ArchiveMode.ALWAYS);
                tokBoxSessionId = session.Id;
            }
            if (DaysPassedForExpiration(tokBoxRegisteredDate) || string.IsNullOrEmpty(tokBoxTokenAgent))
            {
                tokBoxTokenAgent = opentok.GenerateToken(tokBoxSessionId, role: Role.MODERATOR, expireTime: 0); //0 == 24hours

                InsertBatchMeta(new BatchMetum { BatchId = batch.Id, FieldValue = DateTime.Now.ToString(), DocumentClassFieldId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.TokBoxRegisterDate).Id }, userName, batch.RequestId);
                InsertBatchMeta(new BatchMetum { BatchId = batch.Id, FieldValue = tokBoxSessionId, DocumentClassFieldId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.TokBoxSessionId).Id }, userName, batch.RequestId);
                InsertBatchMeta(new BatchMetum { BatchId = batch.Id, FieldValue = tokBoxTokenAgent, DocumentClassFieldId = _documentClassFieldService.GetDocumentClasseFieldsByEnumValue(DocumentClassFieldKeys.TokBoxTokenAgent).Id }, userName, batch.RequestId);
            }
            tokBoxResponse.TokBoxSessionId = tokBoxSessionId;
            tokBoxResponse.TokBoxToken = tokBoxTokenAgent;
            tokBoxResponse.Token = batch.Token;
            tokBoxResponse.SRID = batch.RequestId;

            return tokBoxResponse;
        }

        /// <summary>
        /// Insert BatchMeta of tok box credentials.
        /// </summary>
        /// <param name="batchMeta">The batchMeta.</param>
        /// <param name="username">The userName.</param>
        /// <param name="requestId">The requestId.</param>
        /// <returns>void.</returns>
        public void InsertBatchMeta(BatchMetum batchMeta, string username, string requestId)
        {
            BatchMetum tmpBatchMeta = GetBatchMetaData(batchMeta.BatchId, batchMeta.DocumentClassFieldId);

            if (tmpBatchMeta != null)
            {
                var updatebatchMeta = _repositoryBatchMetum.Query(f => f.Id == tmpBatchMeta.Id).FirstOrDefault();
                updatebatchMeta.FieldValue = batchMeta.FieldValue;
                _repositoryBatchMetum.Update(updatebatchMeta);
            }
            else
            {
                _repositoryBatchMetum.Insert(batchMeta);
            }
            _repositoryBatchMetum.SaveChanges(username, requestId);
        }

        /// <summary>
        /// Get Batch MetaData by  batch id.
        /// </summary>
        /// <param name="batchId">The batchId.</param>
        /// <param name="documentClassFieldId">The documentClassFieldId.</param>
        /// <returns>BatchMetum.</returns>
        private BatchMetum GetBatchMetaData(int batchId, int documentClassFieldId)
        {
            List<BatchMetum> batchMeta = GetBatchMetaListByBatchId(batchId);
            if (batchMeta != null && batchMeta.Count > 0)
                return batchMeta.Where(x => x.DocumentClassFieldId == documentClassFieldId).FirstOrDefault();

            return null;
        }

        /// <summary>
        /// tokbox credentials Expiration result.
        /// </summary>
        /// <param name="registered">The registered.</param>
        /// <returns>bool.</returns>
        private bool DaysPassedForExpiration(string registered)
        {
            if (string.IsNullOrEmpty(registered) || registered == "Result Is Empty")
                return true;

            return (DateTime.Now - DateTime.Parse(registered)).TotalDays > Convert.ToInt32(_configuration.GetSection("DaysToExpireOnTokBox").Value);
        }

        /// <summary>
        /// Get Document Classes Per Company By Sp.
        /// </summary>
        /// <param name="companyId">The companyId.</param>
        /// <returns>DocumentClasses list.</returns>
        private List<DocumentClasses> GetDocumentClassesPerCompanyBySp(int companyId)
        {
            var documentclassList = new List<DocumentClasses>();
            var connectionString = _configuration.GetConnectionString("SqlConnection");

            // Using a new SQL connection
            using (SqlConnection cn = new SqlConnection(connectionString))
            // Create a SQL command for a stored procedure with name sp_GetPage_PleaseWait
            using (SqlCommand cmd = new SqlCommand("sp_GetDocumentClassesPerCompany", cn) { CommandType = CommandType.StoredProcedure, CommandTimeout = 30000 })
            {
                // Open SQL connection
                cn.Open();
                // Add Parameters
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters[1].Value = companyId;
                // Retrieve returned data into a dataset
                DataSet ds = new DataSet();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd)) { adapter.Fill(ds); }
                // Close the SQL connection
                cn.Close();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    documentclassList.Add(new DocumentClasses()
                    {
                        Id = Convert.ToInt32(row[0]),
                        DocumentClass = row[1].ToString(),
                        EnumValue = row[2].ToString(),
                        RecognitionMappedName = row[3].ToString(),
                        DocumentGroupNameId = Convert.ToInt32(row[4])
                    });
                }
            }
            return documentclassList;
        }

        /// <summary>
        /// Get company spcific prado checks.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <returns>PradoCheck list.</returns>
        public List<PradoCheck> GetPradoChecksForCompanyByToken(Batch batch)
        {
            var company = GetCompanyById(batch.CompanyId);
            //     var query = _context.DocumentClasses.FromSqlInterpolated($"EXEC dbo.sp_GetDocumentClassesPerCompany {Company.Id}");
            var pradoChecksList = new List<PradoCheck>();

            var documentclassList = GetDocumentClassesPerCompanyBySp(batch.CompanyId);

            if (documentclassList.Count() > 0)
            {
                var identityDocsList = documentclassList.Where(x => x.DocumentGroupNameId == (int)DocumentGroupNames.Identity).Select(x => x.Id).ToList();
                var identityDocId = (batch.BatchHistories.Where(x => x.IsLast).FirstOrDefault().BatchHistoryItems.Where(z => identityDocsList.Contains((int)z.DocumentClassId))?.FirstOrDefault())?.DocumentClassId;

                CompanyActionsFactory companyService = new CompanyActionsFactory();
                ICompanyActions companyAction = companyService.GetCompanyActions(company.Code);
                pradoChecksList = companyAction.PradoChecks(identityDocId);
            }
            return pradoChecksList;
        }

        /// <summary>
        /// GetAgentVideoPage stored procedure for document result.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>bool result.</returns>
        public AgentVideoPageDTO GetAgentVideoPage(string requestId)
        {
            // Create the object to return
            AgentVideoPageDTO agentVideoPage = new AgentVideoPageDTO();

            var connectionString = _configuration.GetConnectionString("SqlConnection");

            // Using a new SQL connection
            using (SqlConnection cn = new SqlConnection(connectionString))
            // Create a SQL command for a stored procedure with name sp_GetPage_PleaseWait
            using (SqlCommand cmd = new SqlCommand("sp_GetPage_AgentVideo", cn) { CommandType = CommandType.StoredProcedure, CommandTimeout = 30000 })
            {
                // Open SQL connection
                cn.Open();
                // Add Parameters
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters[1].Value = requestId;
                // Retrieve returned data into a dataset
                DataSet ds = new DataSet();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd)) { adapter.Fill(ds); }
                // Close the SQL connection
                cn.Close();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row[0] != DBNull.Value)
                    {
                        agentVideoPage.batchSourceUploadDocs.Add(new BatchSourceUploadDoc()
                        {
                            Id = Convert.ToInt32(row[0]),
                            BatchSourceId = Convert.ToInt32(row[1]),
                            DocumentGroupNameId = Convert.ToInt32(row[2]),
                            Mandatory = bool.Parse(row[3].ToString())
                        });
                    }
                }

                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    if (row[0] != DBNull.Value)
                    {
                        agentVideoPage.batchHistoryItems.Add(new DboVwDocumentGroupNamesForBatchHistory()
                        {
                            Id = Convert.ToInt32(row[0]),
                            BatchHistoryId = Convert.ToInt32(row[1]),
                            RegisterDate = Convert.ToDateTime(row[2].ToString()),
                            DocumentGroupName = row[3].ToString(),
                            IsValid = bool.Parse(row[4].ToString()),
                            FileName = row[5].ToString(),
                            PageId = String.IsNullOrEmpty(row[6].ToString()) ? 0 : Convert.ToInt32(row[6]),
                            DocumentGroupNameId = Convert.ToInt32(row[7]),
                            IsLast = String.IsNullOrEmpty(row[6].ToString()) ? false : bool.Parse(row[8].ToString())
                        });
                    }
                }


                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    agentVideoPage.LastBatchHistoryId = Convert.ToInt32(row[0]);

                    if (row[1] == DBNull.Value)
                        agentVideoPage.VerificationStatus = null;
                    else
                        agentVideoPage.VerificationStatus = Convert.ToInt32(row[1]) == 1 ? true : false;

                    agentVideoPage.VerificationRejectionReason = row[2].ToString();

                    agentVideoPage.FaceMatching = row[3].ToString();

                    agentVideoPage.IsAlive = row[4].ToString();

                    agentVideoPage.Logo = row[5].ToString();

                    agentVideoPage.SelfiePortraitSimilarity = row[6].ToString();
                }

                foreach (DataRow row in ds.Tables[3].Rows)
                {
                    if (row[0] != DBNull.Value)
                    {
                        agentVideoPage.batchItemsViewModel.Add(new BatchItemViewDTO()
                        {
                            BatchItemId = Convert.ToInt32(row[0]),
                            BatchId = Convert.ToInt32(row[1]),
                            BatchSourceCode = row[2].ToString(),
                            DocumentGroupName = row[3].ToString(),
                            DocumentGroupId = Convert.ToInt32(row[4]),
                            FileName = row[5].ToString(),
                            CreatedDate = Convert.ToDateTime(row[6].ToString()),
                            DocumentClassId = Convert.ToInt32(row[7]),
                            IsValid = bool.Parse(row[8].ToString()),
                        });
                    }
                }

            }

            // Return the object filled with the results
            return agentVideoPage;
        }

        /// <summary>
        /// update batch lockedBy field by agent Id.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userId">The agent Id.</param>
        /// <param name="UserName">The agent name.</param>
        /// <returns>bool result.</returns>
        public async Task<bool> UpdateBatchUnlocked(string requestId, int userId, string UserName)
        {
            var batch = await GetBatchByToken(requestId);
            var aspNetUserId = GetAspnetUser(userId);
             
            if (batch.LockedBy != aspNetUserId)
                return false;

            batch.LockedBy = null;
            _repositoryBatch.Update(batch);
            _repositoryBatch.SaveChanges(UserName, null, requestId);
            return true;
        }

        /// <summary>
        /// update batch lockedBy field by agent Id.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="userId">The agent Id.</param>
        /// <param name="UserName">The agent name.</param>
        /// <returns>bool result.</returns>
        public async Task<bool> UpdateBatchLocked(string requestId, int userId, string UserName)
        {
            var batch = await GetBatchByToken(requestId);

            if (batch.LockedBy != null)
                return false;

            var aspNetUserId = GetAspnetUser(userId);
            batch.LockedBy = aspNetUserId;
            _repositoryBatch.Update(batch);
            _repositoryBatch.SaveChanges(UserName, null, requestId);
            return true;
        }

        /// <summary>
        /// Update specific Batch Status.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="UserName">The agent name.</param>
        /// <param name="batchStatus">The batchStatus.</param>
        /// <returns>bool result.</returns>
        public async Task<bool> UpdateBatchStatus(string requestId, string batchStatus, string UserName)
        {
            var batch = await GetBatchByToken(requestId);
            var newBatchStatusId = _batchStatusService.GetBatchStatusIdByEnumValue(batchStatus);

            if (newBatchStatusId < batch.BatchStatusId)
                return false;

            var verifiedBatchStatusId = _batchStatusService.GetBatchStatusIdByEnumValue(BatchStatusesKeys.VERIFIED);
            var publishedBatchStatusId = _batchStatusService.GetBatchStatusIdByEnumValue(BatchStatusesKeys.PUBLISHED);

            if (newBatchStatusId == verifiedBatchStatusId)
                batch.VerifiedDate = DateTime.Now;

            if (newBatchStatusId == publishedBatchStatusId)
            {
                var dateAsString = Convert.ToDateTime(DateTime.Now.ToSqlDateTimeString()).ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                batch.PublishedDate = Convert.ToDateTime(dateAsString);
            }

            batch.BatchStatusId = newBatchStatusId;
            _repositoryBatch.Update(batch);
            _repositoryBatch.SaveChanges(UserName, null, requestId);
            return true;
        }

        /// <summary>
        /// Verify Batch For Agent Jumio.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="username">The agent name.</param>
        /// <returns>bool result.</returns>
        public async Task<bool> VerifyBatchForAgentJumio(string requestId, string username)
        {
            var batch = await GetBatchByToken(requestId);
            var batchStatusCallVerifiedId = _batchStatusService.GetBatchStatusIdByEnumValue(BatchStatusesKeys.CALL_VERIFIED);

            if (batch.BatchStatusId <= batchStatusCallVerifiedId)
            {
                if (await UpdateBatchStatus(requestId, BatchStatusesKeys.BUSINESS_RULES_RUN, username))
                    return true;
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Update Batch Meta With Batch Id.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="UserName">The agent name.</param>
        /// <param name="batchMetaValue">The batchMetaValue.</param>
        /// <returns>bool result.</returns>
        public bool UpdateBatchMetaWithBatchId(string batchMetaValue, int batchMetaDocumentClassFieldId, int batchHistoryId, string UserName, string requestId)
        {
            var batchHistoryMetum = new BatchHistoryMetum();

            //get All Batch History Metas
            var batchHistoryMetas = GetBatchHistoryMetaListByHistoryId(batchHistoryId);
            if (batchHistoryMetas == null)
                return false;

            //If Document Class Field 100008 exists then update it, else create it
            if (batchHistoryMetas.Count(x => x.DocumentClassFieldId == batchMetaDocumentClassFieldId) > 0)
            {
                batchHistoryMetum = batchHistoryMetas.Where(x => x.DocumentClassFieldId == batchMetaDocumentClassFieldId).FirstOrDefault();
                batchHistoryMetum.FieldValue = batchMetaValue;

                _repositoryBatchHistoryMetum.Update(batchHistoryMetum);
            }
            else
            {
                batchHistoryMetum.FieldValue = batchMetaValue;
                batchHistoryMetum.BatchHistoryId = batchHistoryId;
                batchHistoryMetum.DocumentClassFieldId = batchMetaDocumentClassFieldId;

                _repositoryBatchHistoryMetum.Insert(batchHistoryMetum);
            }
            _repositoryBatchHistoryMetum.SaveChanges(UserName, requestId);
            return true;
        }

        /// <summary>
        ///Save Captured Image in server.
        /// </summary>
        /// <param name="capturedImageDTO">The CapturedImageDTO dto.</param>
        /// <param name="username">login username.</param>
        /// <returns>boolean.</returns>
        public bool SaveCapturedImage(CapturedImageDTO capturedImageDTO, string username)
        {
            //Load batch
            Batch batch = LoadBatchForSaveImage(capturedImageDTO.RequestId);

            if (batch == null)
                return false;

            //Get Last History
            UploadedDocumentsDTO base64Object = new UploadedDocumentsDTO();
            base64Object.Name = capturedImageDTO.ImageName + ".jpg";
            base64Object.Base64String = capturedImageDTO.ImageData;
            base64Object.NameWithoutExtention = capturedImageDTO.ImageName;

            var supportedDocumentTypes = GetValidDocuments(batch.RequestId, batch.BatchHistories.Where(bh => bh.IsLast).FirstOrDefault().Id);
            var identityDocument = supportedDocumentTypes.Where(b => b.DocumentGroupNameId == DocumentGroupNames.Identity && b.IncludeInOnboarding == true).FirstOrDefault();
            string batchDir = GetBatchDirectory(batch);

            if (!(bool)identityDocument.IsBatchItem)
            {
                BatchHistory lastBatchHistory = batch.BatchHistories.Where(x => x.IsLast == true).FirstOrDefault();
                bool historyExists = false;

                if (lastBatchHistory.BatchHistoryItems.Count() > 0)
                {
                    batchDir = Path.Combine(batchDir, lastBatchHistory.Id.ToString());
                    historyExists = true;
                }
                else
                    batchDir = Path.Combine(batchDir, "AgentDocuments");

                if (!Directory.Exists(batchDir))
                    Directory.CreateDirectory(batchDir);

                var filePath = batchDir + "\\" + base64Object.Name;
                if (File.Exists(filePath))
                {
                    string time = DateTime.Now.ToString("MMddyyyy-HHmmss");

                    string fileNewName = $"{time}-{base64Object.Name}";
                    var fileNewPath = $"{batchDir}\\{fileNewName}";
                    File.Move(filePath, fileNewPath);
                }

                File.WriteAllBytes(filePath, Convert.FromBase64String(base64Object.Base64String));

                if ((base64Object.Name.ToLower() == "frontside.jpg" || base64Object.Name.ToLower() == "backside.jpg" || base64Object.Name.ToLower() == "selfie.jpg" || base64Object.Name.ToLower() == "profil.jpg") && historyExists)
                {
                    if (lastBatchHistory.BatchHistoryItems.Where(bhi => (int)bhi.DocumentClassId == identityDocument.DocumentClassId).Count() > 0)
                    {
                        var lastBatchHistoryItem = lastBatchHistory.BatchHistoryItems.Where(bhi => (int)bhi.DocumentClassId == identityDocument.DocumentClassId).FirstOrDefault();
                        var itemId = lastBatchHistoryItem.Id;

                        BatchHistoryItemPage batchHistoryItemPageObj = new BatchHistoryItemPage();

                        if (base64Object.Name.ToLower() != "selfie.jpg")
                            batchHistoryItemPageObj = lastBatchHistoryItem.BatchHistoryItemPages.Where(p => p.OriginalFileName == base64Object.Name && Convert.ToBoolean(p.IsLast)).FirstOrDefault();
                        else
                            batchHistoryItemPageObj = lastBatchHistoryItem.BatchHistoryItemPages.Where(p => (p.OriginalFileName == base64Object.Name || p.OriginalFileName == "") && Convert.ToBoolean(p.IsLast)).FirstOrDefault();

                        //insert data in batch history item page
                        if (batchHistoryItemPageObj == null)
                        {
                            BatchHistoryItemPage batchHistoryItemPage_ = new BatchHistoryItemPage();
                            batchHistoryItemPage_.BatchHistoryItemId = itemId;
                            batchHistoryItemPage_.FileName = base64Object.Name;
                            batchHistoryItemPage_.OriginalFileName = base64Object.Name;
                            batchHistoryItemPage_.IsLast = true;
                            batchHistoryItemPage_.Number = lastBatchHistoryItem.BatchHistoryItemPages.Count() + 1;

                            _repositoryBatchHistoryItemPage.Insert(batchHistoryItemPage_);
                        }
                        else
                        {
                            batchHistoryItemPageObj.FileName = base64Object.Name; batchHistoryItemPageObj.IsLast = true;

                            _repositoryBatchHistoryItemPage.Update(batchHistoryItemPageObj);
                        }
                        _repositoryBatchHistoryItemPage.SaveChanges(username, capturedImageDTO.RequestId);
                    }
                }
            }
            else
            {
                // batchDir = Path.Combine(BatchOutputPath(), batch.CreatedDate.IsoYearMonth(true), batch.CreatedDate.IsoDate(true), batch.Id.ToString());

                bool exists = System.IO.Directory.Exists(batchDir);
                if (!exists)
                    System.IO.Directory.CreateDirectory(batchDir);

                var filePath = batchDir + "\\" + base64Object.Name;

                if (File.Exists(filePath))
                {
                    string time = DateTime.Now.ToString("MMddyyyy-HHmmss");

                    string fileNewPath = $"{batchDir}\\{time}-{base64Object.Name}";
                    File.Move(filePath, fileNewPath);
                }

                File.WriteAllBytes(filePath, Convert.FromBase64String(base64Object.Base64String));

                if (base64Object.Name.ToLower() == "frontside.jpg" || base64Object.Name.ToLower() == "backside.jpg" || base64Object.Name.ToLower() == "selfie.jpg" || base64Object.Name.ToLower() == "profil.jpg")
                {
                    if (batch.BatchItems.Where(bi => (int)bi.DocumentClassId == identityDocument.DocumentClassId).Count() > 0)
                    {
                        int? docClassId = identityDocument.DocumentClassId;
                        var itemId = batch.BatchItems.Where(bi => (int)bi.DocumentClassId == identityDocument.DocumentClassId).FirstOrDefault().Id;

                        //insert data in batch item page
                        BatchItemPage getBatchItemPage = batch.BatchItems.Where(x => x.DocumentClassId == (int)docClassId).FirstOrDefault().BatchItemPages.Where(p => p.OriginalName == base64Object.Name).FirstOrDefault();
                        if (getBatchItemPage == null)
                        {
                            BatchItemPage batchItemPageObj = new BatchItemPage();
                            batchItemPageObj.BatchItemId = itemId;
                            batchItemPageObj.FileName = base64Object.Name;
                            batchItemPageObj.OriginalName = base64Object.Name;

                            _repositoryBatchItemPage.Insert(batchItemPageObj);
                        }
                        else
                        {
                            getBatchItemPage.FileName = base64Object.Name;

                            _repositoryBatchItemPage.Update(getBatchItemPage);
                        }
                        _repositoryBatchItemPage.SaveChanges(username, capturedImageDTO.RequestId);
                    }
                }
            }
            return true;
        }

        /// <summary>
        ///get files list in Base64.
        /// </summary>
        /// <param name="batch">The batch model.</param>
        /// <returns>List of UploadedDocumentsDTO.</returns>
        public List<UploadedDocumentsDTO> GetAllSignedDocuments(Batch batch)
        {
            var batchOutputPath = _bopConfigService.GetBopConfigByEnumValue(BopConfigs.BATCH_FILES_NETWORK_PATH);
            var batchDir = Path.Combine(batchOutputPath, batch.CreatedDate.IsoYearMonth(true), batch.CreatedDate.IsoDate(true), batch.Id.ToString(), "PDF");

            string[] filePaths = null;
            string[] signedfilePaths = null;
            if (Directory.Exists(batchDir))
            {
                filePaths = System.IO.Directory.GetFiles(batchDir, "*.pdf", SearchOption.AllDirectories).Where(s => !s.Contains("Signed")).ToArray();
                signedfilePaths = System.IO.Directory.GetFiles(batchDir, "*_Signed.pdf", SearchOption.AllDirectories);
            }

            List<UploadedDocumentsDTO> base64FilesList = new List<UploadedDocumentsDTO>();

            if (signedfilePaths != null && signedfilePaths.Count() != 0)
            {
                foreach (string RFile in signedfilePaths)
                {
                    UploadedDocumentsDTO Document = new UploadedDocumentsDTO();

                    byte[] bytes = System.IO.File.ReadAllBytes(RFile);
                    string base64String = Convert.ToBase64String(bytes);

                    Document.Base64String = base64String;
                    Document.NameWithoutExtention = Path.GetFileNameWithoutExtension(RFile);
                    Document.Name = Path.GetFileName(RFile);
                    Document.CreatedDate = new FileInfo(RFile).CreationTime;
                    Document.LabelName = Path.GetFileNameWithoutExtension(RFile) + "_OK";
                    Document.IsSigned = true;

                    base64FilesList.Add(Document);
                }
            }

            if (filePaths != null && filePaths.Count() != 0)
            {
                foreach (string RFile in filePaths)
                {
                    if (signedfilePaths.Where(x => x.Contains(Path.GetFileNameWithoutExtension(RFile))).Count() == 0)
                    {
                        UploadedDocumentsDTO Document = new UploadedDocumentsDTO();

                        byte[] bytes = System.IO.File.ReadAllBytes(RFile);
                        string base64String = Convert.ToBase64String(bytes);

                        Document.Base64String = base64String;
                        Document.NameWithoutExtention = Path.GetFileNameWithoutExtension(RFile);
                        Document.Name = Path.GetFileName(RFile);
                        Document.CreatedDate = new FileInfo(RFile).CreationTime;
                        Document.LabelName = Path.GetFileNameWithoutExtension(RFile) + "_NOK";
                        Document.IsSigned = false;

                        base64FilesList.Add(Document);
                    }
                }
            }

            var agentDocumentsbatchDir = Path.Combine(batchOutputPath, batch.CreatedDate.IsoYearMonth(true), batch.CreatedDate.IsoDate(true), batch.Id.ToString(), "AgentDocuments");
            string[] agentDocumentsfilePaths = null;
            if (Directory.Exists(agentDocumentsbatchDir))
            {
                agentDocumentsfilePaths = System.IO.Directory.GetFiles(agentDocumentsbatchDir, "*.*", SearchOption.AllDirectories);

                foreach (string RFile in agentDocumentsfilePaths)
                {
                    UploadedDocumentsDTO Document = new UploadedDocumentsDTO();

                    byte[] bytes = System.IO.File.ReadAllBytes(RFile);
                    string base64String = Convert.ToBase64String(bytes);
                    Document.Base64String = base64String;
                    Document.NameWithoutExtention = Path.GetFileNameWithoutExtension(RFile);
                    Document.Name = Path.GetFileName(RFile);
                    Document.CreatedDate = new FileInfo(RFile).CreationTime;
                    Document.LabelName = Path.GetFileNameWithoutExtension(RFile);

                    base64FilesList.Add(Document);
                }
            }

            return base64FilesList;
        }

        /// <summary>
        ///Get Valid Documents for storing image.
        /// </summary>
        /// <param name="requestId">The batch requestId.</param>
        /// <param name="BatchHistoryId">the BatchHistoryId.</param>
        /// <returns>list of ValidDocumentsDTO</returns>
        private List<ValidDocumentsDTO> GetValidDocuments(string requestId, int? BatchHistoryId = null)
        {
            List<ValidDocumentsDTO> validDocuments = new List<ValidDocumentsDTO>();

            //get connection string to run stored procedure
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            using (SqlConnection cn = new SqlConnection(connectionString))

            using (SqlCommand cmd = new SqlCommand("sp_Service_GetValidDocuments", cn) { CommandType = CommandType.StoredProcedure, CommandTimeout = 30000 })
            {
                cn.Open();

                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters[1].Value = requestId;

                if (BatchHistoryId != null)
                    cmd.Parameters[2].Value = BatchHistoryId;

                DataSet ds = new DataSet();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd)) { adapter.Fill(ds); }
                cn.Close();

                // For each row, print the values of each column.
                foreach (DataRow row in ds.Tables[0].Rows) // BatcMeta
                {
                    ValidDocumentsDTO documentObject = new ValidDocumentsDTO();

                    if (row["DocumentClassId"] == DBNull.Value)
                        documentObject.DocumentClassId = null;
                    else
                        documentObject.DocumentClassId = Convert.ToInt32(row["DocumentClassId"]);

                    documentObject.isValid = Convert.ToInt32(row["IsValid"]) == 1 ? true : false;

                    if (row["IsBatchItem"] == DBNull.Value)
                        documentObject.IsBatchItem = null;
                    else
                        documentObject.IsBatchItem = Convert.ToInt32(row["IsBatchItem"]) == 1 ? true : false;

                    if (row["IncludeInOnboarding"] == DBNull.Value)
                        documentObject.IncludeInOnboarding = null;
                    else
                        documentObject.IncludeInOnboarding = Convert.ToInt32(row["IncludeInOnboarding"]) == 1 ? true : false;

                    if (row["RecognitionMappedName"] == DBNull.Value)
                        documentObject.RecognitionMappedName = null;
                    else
                        documentObject.RecognitionMappedName = row["RecognitionMappedName"].ToString();


                    documentObject.DocumentGroupNameId = Convert.ToInt32(row["DocumentGroupNameId"]);

                    documentObject.Mandatory = Convert.ToInt32(row["Mandatory"]) == 1 ? true : false;

                    documentObject.Code = row["Code"].ToString();

                    validDocuments.Add(documentObject);
                }
            }
            return validDocuments;
        }

        /// <summary>
        /// Get Edit rejection verification Result.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="batchHistoryItemId">The batchHistoryItemId.</param>
        /// <returns>GovDocumentEditorVM dto.</returns>
        public async Task<GetVerificationResultDTO> GetEditVerificationResult(string requestId, int batchHistoryItemId)
        {
            GetVerificationResultDTO verificationResultDTO = new GetVerificationResultDTO();
            //Get company id from batch
            var batch = await GetBatchByToken(requestId);

            //get batch history item
            var batchHistoryItem = GetBatchHistoryItemById(batchHistoryItemId);

            if (batch == null || batchHistoryItem == null)
                return null;

            //get supported document classes of company
            var documentclassList = GetDocumentClassesPerCompanyBySp(batch.CompanyId);

            //get related document class
            var relatedDocumentClass = documentclassList.FirstOrDefault(dc => dc.Id == batchHistoryItem.DocumentClassId);

            //Get all the batch history item fields of the batch history item
            var batchHistoryItemFields = await GetBatchHistoryItemFields(batchHistoryItemId);

            //Get batch history item pages by batch History Item
            var batchHistoryItemPages = GetBatchHistoryItemPagebyHistoryItemId(batchHistoryItemId);

            List<DocumentBase64DTO> itemPagesbase64 = new List<DocumentBase64DTO>();
            foreach (var item in batchHistoryItemPages)
            {
                DocumentBase64DTO base64Obj = new DocumentBase64DTO();
                var base64String = await GetDocumentBase64StringbyPageId(requestId, item.FileName);
                if (base64String == null)
                    continue;

                base64Obj.ItemPagesbase64 = base64String;
                base64Obj.FileName = item.FileName;
                itemPagesbase64.Add(base64Obj);
            }
            verificationResultDTO.RelatedDocumentClass = _mapper.Map<DocumentClassDTO>(relatedDocumentClass);
            verificationResultDTO.BatchHistoryItemFieldList = batchHistoryItemFields;
            verificationResultDTO.Base64Images = itemPagesbase64 ?? new List<DocumentBase64DTO>();
            verificationResultDTO.CompayIdsIsoCode2Digit = GetAplhaBankCompanyId();
            verificationResultDTO.CompanyId = batch.CompanyId;
            verificationResultDTO.BatchHistoryItemId = batchHistoryItemId;
            verificationResultDTO.Token = requestId;

            return verificationResultDTO;
        }

        /// <summary>
        /// Get document in base64.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="pageId">The pageId</param>
        /// <returns>base64 string.</returns>
        public async Task<string> GetDocumentBase64StringbyPageId(string token, string pageId)
        {
            int number = 0;
            bool result = Int32.TryParse(pageId, out number);
            var lastBatchHistory = new BatchHistory();
            var batch = await GetBatchByToken(token);

            if (!result)
                lastBatchHistory = GetBatchHistoryByBatchId(batch.Id);
            else
                lastBatchHistory = GetBatchHistoryByPageId(Convert.ToInt32(pageId));

            if (batch == null || batch.Id != lastBatchHistory.BatchId)
                return null;

            var batchOutputPath = _bopConfigService.GetBopConfigByEnumValue(BopConfigs.BATCH_FILES_NETWORK_PATH);
            var batchDir = Path.Combine(batchOutputPath, batch.CreatedDate.IsoYearMonth(true), batch.CreatedDate.IsoDate(true), batch.Id.ToString(), lastBatchHistory.Id.ToString());

            if (!Directory.Exists(batchDir))
                Directory.CreateDirectory(batchDir);

            string[] filePaths = new List<string>().ToArray();
            var fileName = "";

            if (!result)
                fileName = pageId;
            else
                fileName = GetBatchHistoryItemPage(Convert.ToInt32(pageId))?.FirstOrDefault().FileName;

            string filePath = Path.Combine(batchDir, fileName);

            if (!File.Exists(filePath))
                return null;

            return Convert.ToBase64String(File.ReadAllBytes(filePath));
        }

        /// <summary>
        /// get batch history of specific pageId.
        /// </summary>
        /// <param name="pageId">The pageId.</param>
        /// <returns>Batch history.</returns>
        public BatchHistory GetBatchHistoryByPageId(int pageId)
        {
            var batchHistoryItemPage = _repositoryBatchHistoryItemPage.Query(b => b.Id == pageId).FirstOrDefault();
            var batchHistoryItem = _repositoryBatchHistoryItem.Query(b => b.Id == batchHistoryItemPage.BatchHistoryItemId).FirstOrDefault();
            var batchHistory = _repositoryBatchHistory.Query(b => b.Id == batchHistoryItem.BatchHistoryId).FirstOrDefault();

            return batchHistory;
        }

        /// <summary>
        /// Get document in base64.
        /// </summary>
        /// <param name="requestId">The batch requestId.</param>
        /// <param name="batchItemId">The batchItemId.</param>
        /// <returns>base64 string.</returns>
        public async Task<GetVerificationTxnDataDTO> GetVerificationTxnData(string requestId, int batchItemId)
        {
            GetVerificationTxnDataDTO verificationResultDTO = new GetVerificationTxnDataDTO();

            var batch = await GetBatchByToken(requestId);

            if (batch == null)
                return null;

            var batchItemList = GetBatchItemByBatchId(batch.Id);
            var batchItem = batchItemList.FirstOrDefault();
            var batchItemPages = GetBatchItemPages(batchItemId);

            if (batchItemPages == null)
                return null;

            var batchItemFieldsView = await GetBatchItemFields(batchItemId);

            List<DocumentBase64DTO> itemPagesbase64 = new List<DocumentBase64DTO>();
            foreach (var item in batchItemPages)
            {
                DocumentBase64DTO base64Obj = new DocumentBase64DTO();
                var base64String = GetBatchItemPageBase64(batch, batchItemList, item.FileName);
                if (base64String == null)
                    continue;

                base64Obj.ItemPagesbase64 = base64String;
                base64Obj.FileName = item.FileName;
                itemPagesbase64.Add(base64Obj);
            }
            verificationResultDTO.BatchItemFieldList = batchItemFieldsView.ToList();
            verificationResultDTO.Base64Images = itemPagesbase64 ?? new List<DocumentBase64DTO>();
            verificationResultDTO.CompayIdsIsoCode2Digit = GetAplhaBankCompanyId();
            verificationResultDTO.BatchItemId = batchItem.Id;
            verificationResultDTO.CompanyId = batch.CompanyId;
            verificationResultDTO.Token = requestId;

            return verificationResultDTO;
        }

        /// <summary>
        /// Get document in base64.
        /// </summary>
        /// <param name="batch">The batch model.</param>
        /// <param name="fileName">The filename.</param>
        /// <param name="batchItemList">The batchItemList.</param>
        /// <returns>base64 string.</returns>
        public string GetBatchItemPageBase64(Batch batch, List<BatchItem> batchItemList, string fileName)
        {
            var pageOfInterest = batchItemList.SelectMany(bi => bi.BatchItemPages).FirstOrDefault(p => p.FileName == fileName);

            if (pageOfInterest == null)
                return null;

            var batchOutputPath = _bopConfigService.GetBopConfigByEnumValue(BopConfigs.BATCH_FILES_NETWORK_PATH);
            var batchDir = Path.Combine(batchOutputPath, batch.CreatedDate.IsoYearMonth(true), batch.CreatedDate.IsoDate(true), batch.Id.ToString());

            if (!Directory.Exists(batchDir))
                return null;

            string filePath = Path.Combine(batchDir, pageOfInterest.FileName);

            if (!File.Exists(filePath))
                return null;

            return Convert.ToBase64String(File.ReadAllBytes(filePath));
        }

        /// <summary>
        /// UpdateBatchHistoryItemField
        /// </summary>
        /// <param name="updatedFields">The updatedFields list.</param>
        /// <param name="username">The username.</param>
        public bool UpdateBatchHistoryItemField(List<EditVerificationResultDTO> updatedFields, string username)
        {
            var updatedStatus = false;
            //get batchItem field list from db
            var previousFields = _repositoryBatchHistoryItemField.Query(c => updatedFields.Select(f => f.BatchHistoryItemFieldId).Contains(c.Id)).ToList();

            //  this is check is to make sure all the updated Field ids exist in db, if yes then update otherwise not
            if (previousFields != null && previousFields.Count > 0)
            {
                foreach (var field in updatedFields.Where(d => d.Selected).ToList())
                {
                    var batchItemField = previousFields.Where(v => v.Id == field.BatchHistoryItemFieldId).FirstOrDefault();
                    if (batchItemField != null)
                    {
                        batchItemField.RegisteredFieldValue = field.RegisteredFieldValue;
                        _repositoryBatchHistoryItemField.Update(batchItemField);
                    }
                }
                _repositoryBatchHistoryItemField.SaveChanges(username);
                updatedStatus = true;
            }
            return updatedStatus;
        }

        /// <summary>
        /// UpdateBatchHisUpdateBatchItemFieldstoryItemField
        /// </summary>
        /// <param name="updatedFields">The updatedFields list.</param>
        /// <param name="username">The username.</param>
        public bool UpdateBatchItemFields(List<EditTxnVerificationResultDTO> updatedFields, string username)
        {
            var updatedStatus = false;
            //get batchItem field list from db
            var previousFields = _repositoryBatchItemField.Query(c => updatedFields.Select(f => f.Id).Contains(c.Id)).ToList();

            //  this is check is to make sure all the updated Field ids exist in db, if yes then update otherwise not
            if (previousFields != null && previousFields.Count > 0)
            {
                foreach (var field in updatedFields)
                {
                    var batchItemField = previousFields.Where(v => v.Id == field.Id).FirstOrDefault();
                    if (batchItemField != null)
                    {
                        batchItemField.RegisteredFieldValue = field.RegisteredFieldValue;
                        _repositoryBatchItemField.Update(batchItemField);
                    }
                }
                _repositoryBatchItemField.SaveChanges(username);
                updatedStatus = true;
            }
            return updatedStatus;
        }

        /// <summary>
        /// Update Batch status For AgentIndex view.
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <param name="username">The agent name.</param>
        /// <returns>bool result.</returns>
        public async Task<bool> UpdateBatchForAgentIndex(string requestId, string username)
        {
            var updatedStatus = false;
            var batch = await GetBatchByToken(requestId);
            var batchHistory = GetBatchHistoryByBatchId(batch.Id);

            if (batchHistory != null && batchHistory.ResponseDate != null)
            {
                batchHistory.AgentVerificationStatus = true;
                batchHistory.AgentVerificationStatusDate = DateTime.Now;
                batchHistory.Agent = "aaa";
                _repositoryBatchHistory.Update(batchHistory);
                _repositoryBatchHistory.SaveChanges(username, requestId);
                updatedStatus = true;
            }
            return updatedStatus;
        }

        /// <summary>
        /// Update Batch history Meta With Batch history Id.
        /// </summary>
        /// <param name="requestId">The batch request Id.</param>
        /// <param name="UserName">The agent name.</param>
        /// <param name="historyMetaId">The historyMetaId.</param>
        /// <returns>bool result.</returns>
        public async Task<bool> UpdateBatchHistoryMetaByHistoryId(string historyMetaValue, int historyMetaId, string UserName, string requestId)
        {
            var updatedStatus = false;
            var batchHistoryMeta = await _repositoryBatchHistoryMetum.Query(d => d.Id == historyMetaId).FirstOrDefaultAsync();

            if (batchHistoryMeta != null)
            {
                batchHistoryMeta.FieldValue = historyMetaValue;
                _repositoryBatchHistoryMetum.Update(batchHistoryMeta);
                _repositoryBatchHistoryMetum.SaveChanges(UserName, requestId);
                updatedStatus = true;
            }
            return updatedStatus;
        }
        
        /// <summary>
        /// get the list of document access(company specific).
        /// </summary>
        /// <param name="requestId">The requestId.</param>
        /// <returns>KeyValuePair list.</returns>
        public async Task<List<KeyValuePair<string,string>>> RequestDocumentsAccess(string requestId)
        {
            var batch =await GetBatchByToken(requestId);
            var company = GetCompanyById(batch.CompanyId);
             
            CompanyActionsFactory companyService = new CompanyActionsFactory();
            ICompanyActions companyAction = companyService.GetCompanyActions(company.Code);
            List<KeyValuePair<string, string>> documentsLinkList = new List<KeyValuePair<string, string>>();
            documentsLinkList = companyAction.GetDocumentsDownloadAccess(requestId).ToList();

            return documentsLinkList;
        }
    }
}
