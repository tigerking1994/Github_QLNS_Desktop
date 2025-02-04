/****** Object:  StoredProcedure [dbo].[sp_rollback_data_dm_chudautu_vdt]    Script Date: 11/15/2023 5:34:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rollback_data_dm_chudautu_vdt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rollback_data_dm_chudautu_vdt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rollback_data_dm_chudautu_vdt]    Script Date: 11/15/2023 5:34:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rollback_data_dm_chudautu_vdt]
AS
BEGIN

	INSERT INTO DM_ChuDauTu(iID_DonVi, iID_MaDonVi, iNamLamViec, sTenDonVi,bHangCha)
	SELECT DISTINCT *, 0 FROM
	(
		select distinct iID_ChuDauTuID, iID_MaChuDauTuID, MAX(YEAR(da.dDateCreate)) as iNamLamViec, MAX( dv.sTenDonVi) as TenDonVi
		from VDT_DA_DuAn da
		left join DonVi dv on da.iID_MaChuDauTuID = dv.iID_MaDonVi AND YEAR(da.dDateCreate) = dv.iNamLamViec
		where iID_ChuDauTuID is not null
		group by iID_ChuDauTuID, iID_MaChuDauTuID	
		UNION 
		select distinct iID_ChuDauTuID, iID_MaChuDauTuID, MAX(YEAR(da.dDateCreate)) as iNamLamViec, MAX( dv.sTenDonVi) as TenDonVi
		from VDT_DA_ChuTruongDauTu da
		left join DonVi dv on da.iID_MaChuDauTuID = dv.iID_MaDonVi AND YEAR(da.dDateCreate) = dv.iNamLamViec
		where iID_ChuDauTuID is not null
		group by iID_ChuDauTuID, iID_MaChuDauTuID
	) as d
	where d.iID_ChuDauTuID NOT IN (SELECT iID_DonVi FROM DM_ChuDauTu)

END
GO
EXEC [dbo].[sp_rollback_data_dm_chudautu_vdt]
