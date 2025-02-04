/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 13/12/2023 3:57:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_thamdinhquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 13/12/2023 3:57:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int,
@IIdDonVi nvarchar(max)
AS
BEGIN
	SELECT * INTO #dmtdqt
	FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = 2023 AND iTrangThai = 1
	ORDER BY iMa

	SELECT * FROM #dmtdqt dmtdqp
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTuChiTiet ctct ON dmtdqp.iMa = ctct.iMa
	LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = 2023 AND iTrangThai = 1) dv on dv.iID_MaDonVi = ctct.iID_MaDonVi

	DROP TABLE #dmtdqt;
END


GO
