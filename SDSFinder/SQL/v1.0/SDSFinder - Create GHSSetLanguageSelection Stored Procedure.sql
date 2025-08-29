USE [IND_App]
GO
/****** Object:  StoredProcedure [dbo].[IND_GHSSetLanguageSelection]    Script Date: 8/27/2025 1:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IND_GHSSetLanguageSelection] 
    @site SiteType,
    @job JobType,
    @lang1 varchar(40) OUTPUT,
    @lang2 varchar(40) OUTPUT,
    @lang3 varchar(40) OUTPUT,
    @lang4 varchar(40) OUTPUT,
    @lang5 varchar(40) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @job = dbo.ExpandKyByType('JobType', @job);

    SET @lang1 = (SELECT AttrValue 
                  FROM [IND_GetJobAttributeFn]('labeling', 'GHS Language: Manufacture Site Country', @site, @job, 0));

    SET @lang2 = (SELECT AttrValue 
                  FROM [IND_GetJobAttributeFn]('labeling', 'GHS Language: Destination Country', @site, @job, 0));

    SET @lang3 = (SELECT AttrValue 
                  FROM [IND_GetJobAttributeFn]('labeling', 'GHS Language: Final Destination Country', @site, @job, 0));

    SET @lang4 = (SELECT AttrValue 
                  FROM [IND_GetJobAttributeFn]('labeling', 'GHS Language: Other Country', @site, @job, 0));

    SET @lang5 = (SELECT AttrValue 
                  FROM [IND_GetJobAttributeFn]('labeling', 'GHS Language: Other Country 2', @site, @job, 0));
END
GO