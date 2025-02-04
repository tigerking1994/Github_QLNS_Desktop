UPDATE TL_PhuCap_MLNS
SET Ma_Cb = '3.3'
WHERE Ma_Cb = '3'

/****** Object:  StoredProcedure [dbo].[sp_skt_donvi_benhvientuchu_report]    Script Date: 21/10/2022 4:12:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_donvi_benhvientuchu_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_donvi_benhvientuchu_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]    Script Date: 21/10/2022 4:12:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_index]    Script Date: 21/10/2022 4:20:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_pheduyet_quyettoandaht_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_index]    Script Date: 21/10/2022 4:20:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_index]
	-- Add the parameters for the stored procedure here
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	pdqtdaht.ID as ID,
	pdqtdaht.bIsXoa as BIsXoa,
	pdqtdaht.dNgayPheDuyet as DNgayPheDuyet,
	pdqtdaht.dNgaySua as DNgaySua,
	pdqtdaht.dNgayTao as DNgayTao,
	pdqtdaht.dNgayXoa as DNgayXoa,
	pdqtdaht.iID_DonViID as IIDDonViId,
	pdqtdaht.iID_MaDonVi as IIDMaDonVi,
	pdqtdaht.iID_TiGiaID as IIDTiGiaId,
	pdqtdaht.iNamBaoCaoDen as INamBaoCaoDen,
	pdqtdaht.iNamBaoCaoTu as INamBaoCaoTu,
	pdqtdaht.sMoTa as SMoTa,
	pdqtdaht.sNguoiSua as SNguoiSua,
	pdqtdaht.sNguoiTao as SNguoiTao,
	pdqtdaht.sNguoiXoa as SNguoiXoa,
	pdqtdaht.sSoPheDuyet as SSoPheDuyet,
	dv.sTenDonVi,tg.sTenTiGia from NH_QT_PheDuyetQuyetToanDAHT pdqtdaht
	left join DonVi dv on pdqtdaht.iID_DonViID = dv.iID_DonVi 
	and pdqtdaht.iID_MaDonVi COLLATE SQL_Latin1_General_CP1_CI_AS = dv.iID_MaDonVi
	and dv.iNamLamViec = @YearOfWork
	left join NH_DM_TiGia tg on pdqtdaht.iID_TiGiaID = tg.ID

END
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]    Script Date: 21/10/2022 4:12:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_rpt_skt_tonghop_skt_benhvien_tuchu]
@lstDonVi nvarchar(max),
@iNamLamViec int
AS
BEGIN
	SELECT * INTO #tmpDonVi
	FROM f_split(@lstDonVi)
	
	SELECT ml.iID_MLSKTCha as IIdMlsktCha,
		ml.iID_MLSKT IIdMlskt,
		ml.sKyHieu,
		ml.sStt,
		ml.sMoTa,
		ml.bHangCha,
		SUM(ISNULL(dt.fTuChi, 0)) as TongTuChi,
		0 as TongTuChiPB,
		SUM(ISNULL(dt.fMuaHangCapHienVat, 0)) as TongMuaHangHienVat,
		SUM(ISNULL(dt.fMuaHangCapHienVat, 0)) as TongMuaHangHienVatPB,
		SUM(ISNULL(dt.fPhanCap, 0)) as TongDacThu,
		SUM(ISNULL(dt.fPhanCap, 0)) as TongDacThuPB
	FROM NS_SKT_MucLuc as ml 
	LEFT JOIN (select * from NS_SKT_ChungTuChiTiet where iLoai = 2 and iNamLamViec = @iNamLamViec and iLoaiChungTu = 1 and iID_MaDonVi in (SELECT * FROM #tmpDonVi)) as dt on ml.sKyHieu = dt.sKyHieu
	WHERE ml.iTrangThai = 1
		AND ml.iNamLamViec = @iNamLamViec
	GROUP BY ml.iID_MLSKTCha, ml.iID_MLSKT, ml.sKyHieu, ml.sStt, ml.sMoTa, ml.bHangCha
	ORDER BY sKyHieu
END


GO
/****** Object:  StoredProcedure [dbo].[sp_skt_donvi_benhvientuchu_report]    Script Date: 21/10/2022 4:12:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_skt_donvi_benhvientuchu_report]
@iNamLamViec int
AS
BEGIN
	SELECT DISTINCT dv.*
	FROM NS_SKT_ChungTu as tbl
	INNER JOIN DonVi as dv on tbl.iID_MaDonVi = dv.iID_MaDonVi 
	WHERE tbl.iLoai = 2
		AND tbl.iNamLamViec = @iNamLamViec 
		AND dv.iNamLamViec = @iNamLamViec
		AND tbl.iLoaiChungTu = 1
		AND dv.iTrangThai = 1
END
GO
