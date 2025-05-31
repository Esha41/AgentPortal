Create View [SystemUsersView] As
SELECT su.[Id]
        ,su.[FullName]
        ,su.[Email]
        ,su.[IsActive]
        ,su.[CreatedAt]
        ,su.[UpdatedAt]
	    ,uc.[CompanyId]
	    ,c.[CompanyName]
FROM [SystemUsers] su
LEFT OUTER JOIN [UserCompanies] uc on su.[Id] = uc.[SystemUserId]   
LEFT OUTER JOIN [Companies] c on uc.[CompanyId] = c.[Id]