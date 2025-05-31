
ALTER PROCEDURE [dbo].[batches_count]
	@pCompanyId int = 0,
	@pFromDate char(10) = '2020-05-02',
	@pToDate char(10) = '2020-05-25'
AS
BEGIN
	SET NOCOUNT ON;

	SELECT count(*) [Count]
	, tbl.[CompanyName]
	, tbl.[CreatedDate]
	FROM (
		SELECT b.Id
		, c.[CompanyName]
		, convert(varchar, b.[CreatedDate], 101 /*[mm/dd/yyyy]*/) [CreatedDate]
		, convert(varchar, b.[CreatedDate], 111 /*[yyyy/mm/dd]*/) [CreatedDateForSort]
		FROM [Batches] b
		INNER JOIN [Companies] c ON b.[CompanyId] = c.[Id]
		WHERE (@pCompanyId = 0 OR b.CompanyId = @pCompanyId)
		AND (b.[CreatedDate] BETWEEN @pFromDate + ' 00:00:00.000' AND @pToDate + ' 23:59:59.999')
	) tbl
	GROUP BY tbl.[CompanyName]
	, tbl.[CreatedDate]
	, tbl.[CreatedDateForSort]
	ORDER BY tbl.[CreatedDateForSort]
END
	



