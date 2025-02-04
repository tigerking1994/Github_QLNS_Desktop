/****** Object:  StoredProcedure [dbo].[sp_bh_dtt_get_donvi_dtt_dttm_dtc]    Script Date: 12/18/2024 4:23:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dtt_get_donvi_dtt_dttm_dtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dtt_get_donvi_dtt_dttm_dtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dctt_get_data_quyet_toan]    Script Date: 12/18/2024 4:23:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dctt_get_data_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dctt_get_data_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dctt_get_data_quyet_toan]    Script Date: 12/18/2024 4:23:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_dctt_get_data_quyet_toan] 
	@NamLamViec INT,
	@MaDonVi NVARCHAR(max),
	@ThangQuy INT,
	@LoaiThangQuy INT
AS
BEGIN
	--Data quyet toan
	SELECT ctct.sXauNoiMa,
		ctct.iID_MaDonVi,
		sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
	INTO #temp_qt
	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_QTT_BHXH_ChungTu ct ON ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
	WHERE ct.iNamLamViec = @NamLamViec
		AND ct.iID_MaDonVi IN (SELECT *FROM f_split(@MaDonVi))
		AND ct.iQuyNam = @ThangQuy
		AND ct.iQuyNamLoai = @LoaiThangQuy
		AND ct.bIsKhoa = 1
	GROUP BY ctct.sXauNoiMa, ctct.iID_MaDonVi

	--Data giai thich
	SELECT ctct.sXauNoiMa,
		ctct.iID_MaDonVi,
		sum(isnull(ctct.fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(ctct.fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		sum(isnull(ctct.fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(ctct.fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		sum(isnull(ctct.fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(ctct.fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD
	INTO #temp_giaithich
	FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	JOIN BH_QTT_BHXH_ChungTu ct ON ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
	WHERE ct.iNamLamViec = @NamLamViec
		AND ct.iID_MaDonVi IN (SELECT *FROM f_split(@MaDonVi))
		AND ct.iQuyNam = @ThangQuy
		AND ct.iQuyNamLoai = @LoaiThangQuy
		AND ct.bIsKhoa = 1
	GROUP BY ctct.sXauNoiMa, ctct.iID_MaDonVi

	SELECT TEMP.sXauNoiMa, TEMP.iID_MaDonVi
	INTO #temp_base
	FROM (
		SELECT DISTINCT sXauNoiMa, iID_MaDonVi FROM #temp_qt
		UNION
		SELECT DISTINCT sXauNoiMa, iID_MaDonVi FROM #temp_giaithich) TEMP

	--Ket qua
	SELECT base.sXauNoiMa,
		base.iID_MaDonVi,
		isnull(qt.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) fThuBHXH_NLD_QTDauNam,
		isnull(qt.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) fThuBHXH_NSD_QTDauNam,
		isnull(qt.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) fThuBHYT_NLD_QTDauNam,
		isnull(qt.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) fThuBHYT_NSD_QTDauNam,
		isnull(qt.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) fThuBHTN_NLD_QTDauNam,
		isnull(qt.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0) fThuBHTN_NSD_QTDauNam
	FROM #temp_base base
	LEFT JOIN #temp_qt qt ON base.sXauNoiMa = qt.sXauNoiMa AND base.iID_MaDonVi = qt.iID_MaDonVi
	LEFT JOIN #temp_giaithich gt ON base.sXauNoiMa = gt.sXauNoiMa AND base.iID_MaDonVi = gt.iID_MaDonVi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dtt_get_donvi_dtt_dttm_dtc]    Script Date: 12/18/2024 4:23:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_bh_dtt_get_donvi_dtt_dttm_dtc]
	@NamLamViec int,
	@SoQuyetDinh nvarchar(max)
AS
BEGIN
	--DTT
	select distinct ctct.iID_MaDonVi
	from BH_DTT_BHXH_PhanBo_ChungTu ct
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct on ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	where ct.iNamLamViec = @NamLamViec
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and (isnull(ctct.fBHXH_NLD, 0) <> 0 or isnull(ctct.fBHXH_NSD, 0) <> 0
			or isnull(ctct.fBHYT_NLD, 0) <> 0 or isnull(ctct.fBHYT_NSD, 0) <> 0
			or isnull(ctct.fBHTN_NLD, 0) <> 0 or isnull(ctct.fBHTN_NSD, 0) <> 0)

	union
	--DTTM
	select distinct ctct.iID_MaDonVi
	from BH_DTTM_BHYT_ThanNhan_PhanBo ct
	join BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct on ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
	where ct.iNamLamViec = @NamLamViec
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and isnull(ctct.fDuToan, 0) <> 0

	union
	--DTC
	select distinct ctct.iID_MaDonVi
	from BH_DTC_PhanBoDuToanChi ct
	join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	where ct.iNamChungTu = @NamLamViec
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and (isnull(ctct.fTienHienVat, 0) <> 0 or isnull(ctct.fTienTuChi, 0) <> 0)

END
GO
--
update BH_DM_MucLucNganSach set fHeSoLayQuyLuong = 1
where iNamLamViec in (2024, 2025)
and iTrangThai = 1
and bHangCha = 0
GO