using AutoMapper;
using Intelli.AgentPortal.Domain;
using Intelli.AgentPortal.Domain.Model;
using System;

namespace Intelli.AgentPortal.Api.DTO.Mapping
{
    /// <summary>
    /// The mapping profile.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            // CompanyDTO
            CreateMap<Company, CompanyDTO>()
                .ForMember(dest => dest.UsersPerCompany,
                    opt => opt.MapFrom(src => EncryptionHelper.DecryptUsersPerCompany(src.UsersPerCompany)));
            CreateMap<CompanyDTO, Company>();

            // RoleDTO
            CreateMap<SystemRole, RoleDTO>();
            CreateMap<RoleDTO, SystemRole>();

            // RoleScreenDTO
            CreateMap<RoleScreen, RoleScreenDTO>()
                .ForMember(dest => dest.ScreenPriviliges, opt => opt.MapFrom(src => src.Privilege));
            CreateMap<RoleScreenDTO, RoleScreen>()
                .ForMember(dest => dest.Privilege, opt => opt.MapFrom(src => src.ScreenPriviliges))
                .ForMember(dest => dest.SystemRole, opt => opt.Ignore())
                .ForMember(dest => dest.Screen, opt => opt.Ignore());

            // RoleScreenElementDTO
            CreateMap<RoleScreenElement, RoleScreenElementDTO>()
                .ForMember(dest => dest.Priviliges, opt => opt.MapFrom(src => src.Privilege));
            CreateMap<RoleScreenElementDTO, RoleScreenElement>()
                .ForMember(dest => dest.Privilege, opt => opt.MapFrom(src => src.Priviliges))
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.ScreenElement, opt => opt.Ignore());

            // ScreenElementDTO
            CreateMap<ScreenElement, ScreenElementDTO>();
            CreateMap<ScreenElementDTO, ScreenElement>();

            // ScreensDTO
            CreateMap<Screen, ScreensDTO>();
            CreateMap<ScreensDTO, Screen>();

            // UserPreferencesDTO
            CreateMap<ResourceLanguage, UserPreferencesDTO>();
            CreateMap<UserPreferencesDTO, ResourceLanguage>();

            // UserReadDTO
            CreateMap<SystemUser, UserReadDTO>();
            CreateMap<UserReadDTO, SystemUser>();

            // UserReadPrivilegesDTO
            CreateMap<SystemUser, UserReadPrivilegesDTO>();
            CreateMap<UserReadPrivilegesDTO, SystemUser>();

            // SystemUserCompanyDTO
            CreateMap<Country, CountryDTO>();
            CreateMap<CountryDTO, Country>();

            // UserPreferencesDTO
            CreateMap<UserPreference, UserPreferencesDTO>();
            CreateMap<UserPreferencesDTO, UserPreference>();

            // DocumentsPerCompanyDTO
            CreateMap<DocumentsPerCompany, DocumentsPerCompanyDTO>();
            CreateMap<DocumentsPerCompanyDTO, DocumentsPerCompany>();

            // DocumentClassDTO
            CreateMap<DocumentClasses, DocumentClassDTO>();
            CreateMap<DocumentClassDTO, DocumentClasses>();

            // DocumentClassDTO
            CreateMap<DocumentClassField, DocumentClassFieldDTO>();
            CreateMap<DocumentClassFieldDTO, DocumentClassField>();

            // DocumentGroupNameDTO
            CreateMap<DocumentGroupName, DocumentGroupNameDTO>();
            CreateMap<DocumentGroupNameDTO, DocumentGroupName>();

            // ConfigurationDto
            CreateMap<Configuration, ConfigurationDto>();
            CreateMap<ConfigurationDto, Configuration>();

            // BatchDTO
            CreateMap<Batch, BatchDTO>();
            CreateMap<BatchDTO, Batch>();

            // BatchDTO
            CreateMap<Batch, FullBatchDto>();
            CreateMap<FullBatchDto, Batch>();

            // BatchStatusDTO
            CreateMap<BatchStatus, BatchStatusDTO>();
            CreateMap<BatchStatusDTO, BatchStatus>();

            // BatchCompaniesDTO
            CreateMap<Company, BatchCompaniesDTO>();
            CreateMap<BatchCompaniesDTO, Company>();

            // BatchMetumDTO
            CreateMap<BatchMetum, BatchMetumDTO>();
            CreateMap<BatchMetumDTO, BatchMetum>();

            // ResellersDTO
            CreateMap<AspNetReseller, ResellersDTO>();
            CreateMap<ResellersDTO, AspNetReseller>();

            // BatchHistoryDTO
            CreateMap<BatchHistory, BatchHistoriesDTO>();
            CreateMap<BatchHistoriesDTO, BatchHistory>();

            // PendingBatchesDTO
            CreateMap<BatchVideoPriority, PendingBatchesDTO>();
            CreateMap<PendingBatchesDTO, BatchVideoPriority>();

            // PendingBatchesDTO
            CreateMap<BatchSourceUploadDoc, BatchSourceUploadDocDTO>();
            CreateMap<BatchSourceUploadDocDTO, BatchSourceUploadDoc>();

            CreateMap<BatchHistory, BatchHistoriesDTO>();
            CreateMap<BatchHistoriesDTO, BatchHistory>();
        }
    }
}
