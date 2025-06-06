USE [SDSFinder]
GO

/****** Object:  View [dbo].[vw_GHS_Language_AttributeLookup]    Script Date: 6/6/2025 8:50:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_GHS_Language_AttributeLookup]
AS
SELECT *
FROM OPENQUERY([CSI-LIVE-DB1], '
    SELECT
           v.site_ref,
           v.AttrID,
           a.attr_desc,
           v.AttrValue,
           v.co_num,
           v.co_line,
           v.co_release
    FROM IND_App.dbo.IND_ATTR_CustomerOrderLines_Value v
    LEFT JOIN IND_App.dbo.IND_Attributes a ON v.attrID = a.AttrID
    WHERE a.attr_desc LIKE ''GHS Language%''
');
GO


