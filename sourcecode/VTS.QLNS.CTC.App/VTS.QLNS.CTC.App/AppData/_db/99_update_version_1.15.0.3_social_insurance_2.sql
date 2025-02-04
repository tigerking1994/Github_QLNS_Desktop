/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]    Script Date: 10/28/2024 5:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]    Script Date: 10/28/2024 5:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn]    Script Date: 10/28/2024 5:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 10/28/2024 5:39:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 10/28/2024 5:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@SoQuyetDinh nvarchar(200),
	@NgayQuyetDinh nvarchar(200),
	@DVT int,
	@IsMillionRound bit
AS
BEGIN
	CREATE TABLE #result(STT nvarchar(50), IIdChungTu uniqueidentifier, IIdParent uniqueidentifier, SNoiDung nvarchar(200), ILevel int, IThuTu int, FSoTien float)
	DECLARE @IIdDTT uniqueidentifier = NewID();
	DECLARE @IIdDTC uniqueidentifier = NewID();
	DECLARE @IIdThuBHYTTN uniqueidentifier = NewID();

	INSERT INTO #result(STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien)
	(
		--A Dự toán thu
		SELECT 'A', @IIdDTT, NULL, N'Dự toán thu', 1, 1, 0
		
		UNION ALL
		SELECT '1', NEWID(), @IIdDTT, N'Dự toán thu BHXH', 2, 1, SUM(ISNULL(ctct.fThuBHXH, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))

		UNION ALL
		SELECT '2', NEWID(), @IIdDTT, N'Dự toán thu BHTN', 2, 2, SUM(ISNULL(ctct.fThuBHTN, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))

		UNION ALL
		SELECT '3', NEWID(), @IIdDTT, N'Dự toán thu BHYT quân nhân', 2, 3, SUM(ISNULL(ctct.fThuBHYT, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')

		UNION ALL
		SELECT '4', NEWID(), @IIdDTT, N'Dự toán thu BHYT người lao động', 2, 4, SUM(ISNULL(ctct.fThuBHYT, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9020001-010-011-0002%' OR ctct.sXauNoiMa like '9020002-010-011-0002%')

		UNION ALL
		SELECT '5', @IIdThuBHYTTN, @IIdDTT, N'Dự toán thu BHYT TN', 2, 5, 0 FSoTien

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYTTN, N'Thân nhân quân nhân', 3, 1, SUM(ISNULL(ctct.fDuToan, 0)) FSoTien
		FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		join BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9030001-010-011-0001%' OR ctct.sXauNoiMa like '9030001-010-011-0002%')

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYTTN, N'Thân nhân người lao động', 3, 1, SUM(ISNULL(ctct.fDuToan, 0)) FSoTien
		FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		join BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9030002-010-011-0000%' OR ctct.sXauNoiMa like '9030002-010-011-0001%')


		--B Dự toán chi
		UNION ALL
		SELECT 'B', @IIdDTC, NULL, N'Dự toán chi', 1, 2, 0

		UNION ALL
		SELECT '1', NEWID(), @IIdDTC, N'Dự toán chi các chế độ BHXH', 2, 1, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '01'

		UNION ALL
		SELECT '2', NEWID(), @IIdDTC, N'Dự toán chi kinh phí quản lý BHXH', 2, 2, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '02'

		UNION ALL
		SELECT '3', NEWID(), @IIdDTC, N'Dự toán chi kinh phí KCB tại quân y đơn vị', 2, 3, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '03'

		UNION ALL
		SELECT '4', NEWID(), @IIdDTC, N'Dự toán chi KCB tại Trường Sa - DK', 2, 4, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '04'
			)
	if (@IsMillionRound = 1)
	SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, round(FSoTien / 1000000, 0) * 1000000 / @DVT FSoTien from #result
	else 
	SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, round(FSoTien/@DVT,0) FSoTien from #result
	DROP TABLE #result;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn]    Script Date: 10/28/2024 5:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int,
	@IsMillionRound bit
AS
BEGIN
	SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   A.*
		   into #tempPhanBoDuLieu
		FROM
		  (SELECT 

				ctct.*

		   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		   WHERE ct.iNamLamViec = @NamLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id

	
	SELECT
		dt_dv.id,
		dt_dv.sTenDonVi,
		A.*
		into #tempNhanPbDuLieu
		FROM
		  (SELECT
			ctct.*
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   WHERE ct.iNamLamViec = @namLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id




		select 
		'' STT
		,N'TỔNG CỘNG' STenDonVi
		,'' iID_MaDonVi
		,0 FTienBHXHNLD
		,0 FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		,0 Type
		,1 bHangCha

		into #tempTotal

	
		select 
		'I' STT
		,N'KHỐI DỰ TOÁN' STenDonVi
		,'' iID_MaDonVi
		,0 FTienBHXHNLD
		,0 FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		,1 Type
		,1 bHangCha
		into #tempKhoiDuToan

		select 
		'II' STT
		,N'KHỐI HẠCH TOÁN' STenDonVi
		,'' iID_MaDonVi
		,0 FTienBHXHNLD
		,0 FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		,1 Type
		,1 bHangCha
		into #tempKhoiHachToan

--- Thu BHXH - NLĐ đóng
-- KHỐI DỰ TOÁN
	Select * into #tempDetailKhoiDuToan
		from
		(Select 
		sTenDonVi
		,iID_MaDonVi
		,sum(fBHXH_NLD) FTienBHXHNLD
		,0 FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from  
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020001-010-011-0001%' 
		OR sXauNoiMa like '9020001-010-011-0002%' )
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi
		Union all
	--- Thu BHXH - NSD đóng
	-- KHỐI DỰ TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,sum(fBHXH_NSD)  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020001-010-011-0002%') 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi
		Union all
	--Thu BHTN- NLĐ đóng
	-- KHỐI DỰ TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,sum(fBHTN_NLD) FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from  
		#tempPhanBoDuLieu
		where (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020001-010-011-0002%') 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--Thu BHTN - NSD đóng
	-- KHỐI DỰ TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,sum(fBHTN_NSD) FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020001-010-011-0001%' OR sXauNoiMa like '9020001-010-011-0002%') 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	-- BHYT NGƯỜI LĐ -  NLĐ đóng
	-- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,sum(fBHYT_NLD) FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu
		where sXauNoiMa like '9020001-010-011-0002%'  
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--BHYT NGƯỜI LĐ -  NSD LĐ đóng
	-- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,sum(fBHYT_NSD) FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
	 from 
		#tempPhanBoDuLieu
		where sXauNoiMa like '9020001-010-011-0002%'  
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--BHYT QN - NLĐ đóng
	--- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,sum(fBHYT_NLD) FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from 
		#tempPhanBoDuLieu 
		where sXauNoiMa like '9020001-010-011-0001%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
		--- BHYT QN - NSD LĐ đóng
		--- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,sum(fBHYT_NSD)  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempPhanBoDuLieu 
		where sXauNoiMa like '9020001-010-011-0001%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--Thẻ BHYT thân nhân - Quân nhân
	--- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,sum(fDuToan) FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempNhanPbDuLieu 
		where sXauNoiMa like '9030001-010-011-0001%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	-- Thẻ BHYT thân nhân - CN & CNVQP
	-- KHỐI DỰ TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,sum(fDuToan) FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempNhanPbDuLieu 
		where sXauNoiMa like '9030002-010-011-0000%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit)) 
		Group by sTenDonVi
		,iID_MaDonVi )KhoiDuToan

--- Thu BHXH - NLĐ đóng
-- KHỐI HẠCH TOÁN
	Select * into #tempDetailKhoiHachToan
		from
		(Select 
		sTenDonVi
		,iID_MaDonVi
		,sum(fBHXH_NLD) FTienBHXHNLD
		,0 FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from  
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020002-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0002%')
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--- Thu BHXH - NSD đóng
	-- KHỐI HẠCH TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,sum(fBHXH_NSD)  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020002-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0002%') 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--Thu BHTN- NLĐ đóng
	-- KHỐI HẠCH TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,sum(fBHTN_NLD) FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from  
		#tempPhanBoDuLieu
		where (sXauNoiMa like '9020002-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0002%')
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--Thu BHTN - NSD đóng
	-- KHỐI HẠCH TOÁN
		Select 
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,sum(fBHTN_NSD) FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu 
		where (sXauNoiMa like '9020002-010-011-0001%' OR sXauNoiMa like '9020002-010-011-0002%')
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi
	
		Union all
	-- BHYT NGƯỜI LĐ -  NLĐ đóng
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,sum(fBHYT_NLD) FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu
		where sXauNoiMa like '9020002-010-011-0002%'  
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--BHYT NGƯỜI LĐ -  NSD LĐ đóng
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,sum(fBHYT_NSD) FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL
		from 
		#tempPhanBoDuLieu
		where sXauNoiMa like '9020002-010-011-0002%'  
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--BHYT QN - NLĐ đóng
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,sum(fBHYT_NLD) FTienBHYTQNNLD
		,0 FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from 
		#tempPhanBoDuLieu 
		where sXauNoiMa like '9020002-010-011-0001%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--- BHYT QN - NSD LĐ đóng
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,sum(fBHYT_NSD)  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempPhanBoDuLieu 
		where sXauNoiMa like '9020002-010-011-0001%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	--Thẻ BHYT thân nhân - Quân nhân
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,sum(fDuToan) FTienBHYTTNQN
		,0 FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempNhanPbDuLieu 
		where sXauNoiMa like '9030001-010-011-0002%' 
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi

		Union all
	-- Thẻ BHYT thân nhân - CN & CNVQP
	--- KHỐI HẠCH TOÁN
		Select
		sTenDonVi
		,iID_MaDonVi
		,0 FTienBHXHNLD
		,0  FTienBHXHNSDLDD
		,0 FTienCongBHXH
		,0 FTienBHTNNLD
		,0 FTienBHTNNSDLDD
		,0 FTienCongBHTN
		,0 FTienBHYTNLDNLD
		,0 FTienBHYTNSDLDD
		,0 FTienCongBHYTNLD
		,0 FTienBHYTQNNLD
		,0  FTienBHYTQNNSDNLD
		,0 FTienCongBHYTQN
		,0 FTienBHYTTNQN
		,sum(fDuToan) FTienBHYTTNCNCNVQP
		,0 FTienCongBHYTTN
		,0 FTienCongALL 
		from #tempNhanPbDuLieu 
		where sXauNoiMa like '9030002-010-011-0001%'  
		AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		Group by sTenDonVi
		,iID_MaDonVi	)KhoiHachToan

		---- Sum theo DuToan
	Select
		 CAST(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi) AS NVARCHAR(MAX))  STT
		,sTenDonVi 
		,iID_MaDonVi
		,SUM(isnull(FTienBHXHNLD,0)) FTienBHXHNLD
		,SUM(isnull(FTienBHXHNSDLDD,0))  FTienBHXHNSDLDD
		,SUM(isnull(FTienCongBHXH,0)) FTienCongBHXH
		,SUM(isnull(FTienBHTNNLD,0)) FTienBHTNNLD
		,SUM(isnull(FTienBHTNNSDLDD,0)) FTienBHTNNSDLDD
		,SUM(isnull(FTienCongBHTN,0)) FTienCongBHTN
		,SUM(isnull(FTienBHYTNLDNLD,0)) FTienBHYTNLDNLD
		,SUM(isnull(FTienBHYTNSDLDD,0)) FTienBHYTNSDLDD
		,SUM(isnull(FTienCongBHYTNLD,0)) FTienCongBHYTNLD
		,SUM(isnull(FTienBHYTQNNLD,0)) FTienBHYTQNNLD
		,SUM(isnull(FTienBHYTQNNSDNLD,0))  FTienBHYTQNNSDNLD
		,SUM(isnull(FTienCongBHYTQN,0)) FTienCongBHYTQN
		,SUM(isnull(FTienBHYTTNQN,0)) FTienBHYTTNQN
		,SUM(isnull(FTienBHYTTNCNCNVQP,0))FTienBHYTTNCNCNVQP
		,SUM(isnull(FTienCongBHYTTN,0)) FTienCongBHYTTN
		,SUM(isnull(FTienCongALL,0)) FTienCongALL 
		, 2 Type
		,0 bHangCha
		into #tempSumDetailDuToan
		from #tempDetailKhoiDuToan
		group by sTenDonVi
		,iID_MaDonVi


---- Sum theo HachToan
	Select
		CAST(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi) AS NVARCHAR(MAX))  STT
		,sTenDonVi
		,iID_MaDonVi
		,SUM(isnull(FTienBHXHNLD,0)) FTienBHXHNLD
		,SUM(isnull(FTienBHXHNSDLDD,0))  FTienBHXHNSDLDD
		,SUM(isnull(FTienCongBHXH,0)) FTienCongBHXH
		,SUM(isnull(FTienBHTNNLD,0)) FTienBHTNNLD
		,SUM(isnull(FTienBHTNNSDLDD,0)) FTienBHTNNSDLDD
		,SUM(isnull(FTienCongBHTN,0)) FTienCongBHTN
		,SUM(isnull(FTienBHYTNLDNLD,0)) FTienBHYTNLDNLD
		,SUM(isnull(FTienBHYTNSDLDD,0)) FTienBHYTNSDLDD
		,SUM(isnull(FTienCongBHYTNLD,0)) FTienCongBHYTNLD
		,SUM(isnull(FTienBHYTQNNLD,0)) FTienBHYTQNNLD
		,SUM(isnull(FTienBHYTQNNSDNLD,0))  FTienBHYTQNNSDNLD
		,SUM(isnull(FTienCongBHYTQN,0)) FTienCongBHYTQN
		,SUM(isnull(FTienBHYTTNQN,0)) FTienBHYTTNQN
		,SUM(isnull(FTienBHYTTNCNCNVQP,0))FTienBHYTTNCNCNVQP
		,SUM(isnull(FTienCongBHYTTN,0)) FTienCongBHYTTN
		,SUM(isnull(FTienCongALL,0)) FTienCongALL 
		,2 Type
		,0 bHangCha
		into #tempSumDetailHachToan
		from #tempDetailKhoiHachToan
		group by sTenDonVi
		,iID_MaDonVi

		select * into #tempDuToan from (
		select * from
		#tempKhoiDuToan
		Union all 
		select * from #tempSumDetailDuToan)DuToan

		select * into #tempHachToan from 
		(select * from
		#tempKhoiHachToan
		Union all 
		select * from #tempSumDetailHachToan)HachToan

		update A
		set A.FTienBHXHNLD=B.FTienBHXHNLD,
			A.FTienBHXHNSDLDD=B.FTienBHXHNSDLDD,	
			A.FTienCongBHXH=B.FTienCongBHXH,	
			A.FTienBHTNNLD=B.FTienBHTNNLD,	
			A.FTienBHTNNSDLDD=B.FTienBHTNNSDLDD,	
			A.FTienCongBHTN=B.FTienCongBHTN,	
			A.FTienBHYTNLDNLD=B.FTienBHYTNLDNLD,	
			A.FTienBHYTNSDLDD=B.FTienBHYTNSDLDD,	
			A.FTienCongBHYTNLD=B.FTienCongBHYTNLD,	
			A.FTienBHYTQNNLD=B.FTienBHYTQNNLD,	
			A.FTienBHYTQNNSDNLD=B.FTienBHYTQNNSDNLD,
			A.FTienCongBHYTQN=B.FTienCongBHYTQN,	
			A.FTienBHYTTNQN=B.FTienBHYTTNQN,	
			A.FTienBHYTTNCNCNVQP=B.FTienBHYTTNCNCNVQP,	
			A.FTienCongBHYTTN=B.FTienCongBHYTTN,	
			A.FTienCongALL=B.FTienCongALL
		from  #tempDuToan A,
		(select 
			SUM(isnull(FTienBHXHNLD,0)) FTienBHXHNLD
			,SUM(isnull(FTienBHXHNSDLDD,0))  FTienBHXHNSDLDD
			,SUM(isnull(FTienCongBHXH,0)) FTienCongBHXH
			,SUM(isnull(FTienBHTNNLD,0)) FTienBHTNNLD
			,SUM(isnull(FTienBHTNNSDLDD,0)) FTienBHTNNSDLDD
			,SUM(isnull(FTienCongBHTN,0)) FTienCongBHTN
			,SUM(isnull(FTienBHYTNLDNLD,0)) FTienBHYTNLDNLD
			,SUM(isnull(FTienBHYTNSDLDD,0)) FTienBHYTNSDLDD
			,SUM(isnull(FTienCongBHYTNLD,0)) FTienCongBHYTNLD
			,SUM(isnull(FTienBHYTQNNLD,0)) FTienBHYTQNNLD
			,SUM(isnull(FTienBHYTQNNSDNLD,0))  FTienBHYTQNNSDNLD
			,SUM(isnull(FTienCongBHYTQN,0)) FTienCongBHYTQN
			,SUM(isnull(FTienBHYTTNQN,0)) FTienBHYTTNQN
			,SUM(isnull(FTienBHYTTNCNCNVQP,0))FTienBHYTTNCNCNVQP
			,SUM(isnull(FTienCongBHYTTN,0)) FTienCongBHYTTN
			,SUM(isnull(FTienCongALL,0)) FTienCongALL 
		from #tempDuToan
		where  Type=2
		) B
		where A.Type=1

		update A
		set A.FTienBHXHNLD=B.FTienBHXHNLD,
			A.FTienBHXHNSDLDD=B.FTienBHXHNSDLDD,	
			A.FTienCongBHXH=B.FTienCongBHXH,	
			A.FTienBHTNNLD=B.FTienBHTNNLD,	
			A.FTienBHTNNSDLDD=B.FTienBHTNNSDLDD,	
			A.FTienCongBHTN=B.FTienCongBHTN,	
			A.FTienBHYTNLDNLD=B.FTienBHYTNLDNLD,	
			A.FTienBHYTNSDLDD=B.FTienBHYTNSDLDD,	
			A.FTienCongBHYTNLD=B.FTienCongBHYTNLD,	
			A.FTienBHYTQNNLD=B.FTienBHYTQNNLD,	
			A.FTienBHYTQNNSDNLD=B.FTienBHYTQNNSDNLD,
			A.FTienCongBHYTQN=B.FTienCongBHYTQN,	
			A.FTienBHYTTNQN=B.FTienBHYTTNQN,	
			A.FTienBHYTTNCNCNVQP=B.FTienBHYTTNCNCNVQP,	
			A.FTienCongBHYTTN=B.FTienCongBHYTTN,	
			A.FTienCongALL=B.FTienCongALL
		from  #tempHachToan A,
		(select 
			SUM(isnull(FTienBHXHNLD,0)) FTienBHXHNLD
			,SUM(isnull(FTienBHXHNSDLDD,0))  FTienBHXHNSDLDD
			,SUM(isnull(FTienCongBHXH,0)) FTienCongBHXH
			,SUM(isnull(FTienBHTNNLD,0)) FTienBHTNNLD
			,SUM(isnull(FTienBHTNNSDLDD,0)) FTienBHTNNSDLDD
			,SUM(isnull(FTienCongBHTN,0)) FTienCongBHTN
			,SUM(isnull(FTienBHYTNLDNLD,0)) FTienBHYTNLDNLD
			,SUM(isnull(FTienBHYTNSDLDD,0)) FTienBHYTNSDLDD
			,SUM(isnull(FTienCongBHYTNLD,0)) FTienCongBHYTNLD
			,SUM(isnull(FTienBHYTQNNLD,0)) FTienBHYTQNNLD
			,SUM(isnull(FTienBHYTQNNSDNLD,0))  FTienBHYTQNNSDNLD
			,SUM(isnull(FTienCongBHYTQN,0)) FTienCongBHYTQN
			,SUM(isnull(FTienBHYTTNQN,0)) FTienBHYTTNQN
			,SUM(isnull(FTienBHYTTNCNCNVQP,0))FTienBHYTTNCNCNVQP
			,SUM(isnull(FTienCongBHYTTN,0)) FTienCongBHYTTN
			,SUM(isnull(FTienCongALL,0)) FTienCongALL 
		from #tempHachToan
		where  Type=2
		) B
		where A.Type=1

		select * into #tempDetailTotal from (
		select 0 IKhoi,* from #tempTotal
		Union all
		select 1 IKhoi, * from #tempDuToan
		Union all
		select 2 IKhoi, * from #tempHachToan)Total

		update A
		set A.FTienBHXHNLD=B.FTienBHXHNLD,
			A.FTienBHXHNSDLDD=B.FTienBHXHNSDLDD,	
			A.FTienCongBHXH=B.FTienCongBHXH,	
			A.FTienBHTNNLD=B.FTienBHTNNLD,	
			A.FTienBHTNNSDLDD=B.FTienBHTNNSDLDD,	
			A.FTienCongBHTN=B.FTienCongBHTN,	
			A.FTienBHYTNLDNLD=B.FTienBHYTNLDNLD,	
			A.FTienBHYTNSDLDD=B.FTienBHYTNSDLDD,	
			A.FTienCongBHYTNLD=B.FTienCongBHYTNLD,	
			A.FTienBHYTQNNLD=B.FTienBHYTQNNLD,	
			A.FTienBHYTQNNSDNLD=B.FTienBHYTQNNSDNLD,
			A.FTienCongBHYTQN=B.FTienCongBHYTQN,	
			A.FTienBHYTTNQN=B.FTienBHYTTNQN,	
			A.FTienBHYTTNCNCNVQP=B.FTienBHYTTNCNCNVQP,	
			A.FTienCongBHYTTN=B.FTienCongBHYTTN,	
			A.FTienCongALL=B.FTienCongALL
		FROM #tempDetailTotal A,
		(select 
			SUM(isnull(FTienBHXHNLD,0)) FTienBHXHNLD
			,SUM(isnull(FTienBHXHNSDLDD,0))  FTienBHXHNSDLDD
			,SUM(isnull(FTienCongBHXH,0)) FTienCongBHXH
			,SUM(isnull(FTienBHTNNLD,0)) FTienBHTNNLD
			,SUM(isnull(FTienBHTNNSDLDD,0)) FTienBHTNNSDLDD
			,SUM(isnull(FTienCongBHTN,0)) FTienCongBHTN
			,SUM(isnull(FTienBHYTNLDNLD,0)) FTienBHYTNLDNLD
			,SUM(isnull(FTienBHYTNSDLDD,0)) FTienBHYTNSDLDD
			,SUM(isnull(FTienCongBHYTNLD,0)) FTienCongBHYTNLD
			,SUM(isnull(FTienBHYTQNNLD,0)) FTienBHYTQNNLD
			,SUM(isnull(FTienBHYTQNNSDNLD,0))  FTienBHYTQNNSDNLD
			,SUM(isnull(FTienCongBHYTQN,0)) FTienCongBHYTQN
			,SUM(isnull(FTienBHYTTNQN,0)) FTienBHYTTNQN
			,SUM(isnull(FTienBHYTTNCNCNVQP,0))FTienBHYTTNCNCNVQP
			,SUM(isnull(FTienCongBHYTTN,0)) FTienCongBHYTTN
			,SUM(isnull(FTienCongALL,0)) FTienCongALL 
		from #tempDetailTotal
		where  Type=2)B
		where A.Type=0

	if (@IsMillionRound = 1)
		SELECT 
		dt.STT 
		,dt.sTenDonVi STenDonVi
		,dt.iID_MaDonVi
		,round(IsNull(dt.FTienBHXHNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHXHNLD
		,round(IsNull(dt.FTienBHXHNSDLDD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHXHNSDLDD
		--,round(IsNull(dt.FTienCongBHXH, 0) / 1000000, 0) * 1000000 / @DVT FTienCongBHXH
		,round(IsNull(dt.FTienBHTNNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHTNNLD
		,round(IsNull(dt.FTienBHTNNSDLDD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHTNNSDLDD
		--,round(IsNull(dt.FTienCongBHTN, 0) / 1000000, 0) * 1000000 / @DVT FTienCongBHTN
		,round(IsNull(dt.FTienBHYTNLDNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTNLDNLD
		,round(IsNull(dt.FTienBHYTNSDLDD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTNSDLDD
		--,round(IsNull(dt.FTienCongBHYTNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienCongBHYTNLD
		,round(IsNull(dt.FTienBHYTQNNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTQNNLD
		,round(IsNull(dt.FTienBHYTQNNSDNLD, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTQNNSDNLD
		--,round(IsNull(dt.FTienCongBHYTQN, 0) / 1000000, 0) * 1000000 / @DVT FTienCongBHYTQN
		,round(IsNull(dt.FTienBHYTTNQN, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTTNQN
		,round(IsNull(dt.FTienBHYTTNCNCNVQP, 0) / 1000000, 0) * 1000000 / @DVT FTienBHYTTNCNCNVQP
		--,round(IsNull(dt.FTienCongBHYTTN, 0) / 1000000, 0) * 1000000 / @DVT FTienCongBHYTTN
		--,round(IsNull(dt.FTienCongALL, 0) / 1000000, 0) * 1000000 / @DVT FTienCongALL
		,dt.Type
		,dt.bHangCha
		, IKhoi
		
		FROM #tempDetailTotal dt
	else
		SELECT 
		dt.STT 
		,dt.sTenDonVi STenDonVi
		,dt.iID_MaDonVi
		,round(ISNULL(dt.FTienBHXHNLD,0)/ @DVT,0) FTienBHXHNLD
		,round(IsNull(dt.FTienBHXHNLD, 0) / @DVT,0) FTienBHXHNLD
		,round(IsNull(dt.FTienBHXHNSDLDD, 0) / @DVT,0) FTienBHXHNSDLDD
		--,IsNull(dt.FTienCongBHXH, 0) / @DVT FTienCongBHXH
		,round(IsNull(dt.FTienBHTNNLD, 0)  / @DVT,0) FTienBHTNNLD
		,round(IsNull(dt.FTienBHTNNSDLDD, 0)/ @DVT,0) FTienBHTNNSDLDD
		--,IsNull(dt.FTienCongBHTN, 0)/ @DVT FTienCongBHTN
		,round(IsNull(dt.FTienBHYTNLDNLD, 0)/ @DVT,0) FTienBHYTNLDNLD
		,round(IsNull(dt.FTienBHYTNSDLDD, 0) / @DVT,0) FTienBHYTNSDLDD
		--,IsNull(dt.FTienCongBHYTNLD, 0) / @DVT FTienCongBHYTNLD                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   / @DVT FTienCongBHYTNLD
		,round(IsNull(dt.FTienBHYTQNNLD, 0) / @DVT,0) FTienBHYTQNNLD
		,round(IsNull(dt.FTienBHYTQNNSDNLD, 0) / @DVT,0) FTienBHYTQNNSDNLD
		--,IsNull(dt.FTienCongBHYTQN, 0)  / @DVT FTienCongBHYTQN
		,round(IsNull(dt.FTienBHYTTNQN, 0) / @DVT,0) FTienBHYTTNQN
		,round(IsNull(dt.FTienBHYTTNCNCNVQP, 0)  / @DVT,0) FTienBHYTTNCNCNVQP
		--,IsNull(dt.FTienCongBHYTTN, 0)  / @DVT FTienCongBHYTTN
		--,IsNull(dt.FTienCongALL, 0)  / @DVT FTienCongALL
		,dt.Type
		,dt.bHangCha BHangCha
		, IKhoi
		FROM #tempDetailTotal dt

		drop table #tempSumDetailDuToan
		drop table #tempDetailKhoiDuToan
		drop table #tempDetailKhoiHachToan
		drop table #tempSumDetailHachToan
		drop table #tempKhoiDuToan
		drop table #tempKhoiHachToan
		Drop table #tempTotal
		Drop table #tempDuToan
		Drop table #tempHachToan
		Drop table #tempDetailTotal
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]    Script Date: 10/28/2024 5:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int,
	@IsMillionRound bit
AS
BEGIN
	declare @DataDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongDuToan float, BhxhNsddDongDuToan float, BHXHTongCongDuToan float, BhtnNldDongDuToan float, BhtnNsddDongDuToan float, BHTNTongCongDuToan float);
	declare @DataHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongHachToan float, BhxhNsddDongHachToan float, BHXHTongCongHachToan float, BhtnNldDongHachToan float, BhtnNsddDongHachToan float, BHTNTongCongHachToan float);

	INSERT INTO @DataDuToan (sTenDonVI, idDonVi, BhxhNldDongDuToan, BhxhNsddDongDuToan, BHXHTongCongDuToan, BhtnNldDongDuToan, BhtnNsddDongDuToan, BHTNTongCongDuToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   BhxhNldDong = SUM(IsNull(A.ThuBHXHNLDDong,0)),
		   BhxhNsddDong = SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   SUM(IsNull(A.ThuBHXHNLDDong,0)) + SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   BhtnNldDong = SUM(IsNull(A.ThuBHTNNLDDong,0)),
		   BhtnNsddDong = SUM(IsNull(A.ThuBHTNNSDDong,0)),
		   SUM(IsNull(A.ThuBHTNNLDDong,0)) + SUM(IsNull(A.ThuBHTNNSDDong,0))
		FROM
		  (SELECT ml.iID_MLNS ,
				ml.iID_MLNS_Cha,
				ml.sNG,
				ml.sMoTa,
				ml.bHangCha,
				ml.sXauNoiMa,
				ctct.iID_MaDonVi,
				IsNull(ctct.fBHXH_NLD, 0) ThuBHXHNLDDong,
				IsNull(ctct.fBHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fBHTN_NLD, 0) ThuBHTNNLDDong,
				IsNull(ctct.fBHTN_NSD, 0) ThuBHTNNSDDong
		   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @NamLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @KhoiDuToan
		   WHERE ct.iNamLamViec = @NamLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
		
		GROUP BY dt_dv.sTenDonVi,
				dt_dv.id
		order by dt_dv.id
				;
				

	INSERT INTO @DataHachToan (sTenDonVI, idDonVi, BhxhNldDongHachToan, BhxhNsddDongHachToan, BHXHTongCongHachToan, BhtnNldDongHachToan, BhtnNsddDongHachToan, BHTNTongCongHachToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   BhxhNldDong = SUM(IsNull(A.ThuBHXHNLDDong,0)),
		   BhxhNsddDong = SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   SUM(IsNull(A.ThuBHXHNLDDong,0)) + SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   BhtnNldDong = SUM(IsNull(A.ThuBHTNNLDDong,0)),
		   BhtnNsddDong = SUM(IsNull(A.ThuBHTNNSDDong,0)),
		   SUM(IsNull(A.ThuBHTNNLDDong,0)) + SUM(IsNull(A.ThuBHTNNSDDong,0))

		FROM
		  (SELECT ml.iID_MLNS ,
				ml.iID_MLNS_Cha,
				ml.sNG,
				ml.sMoTa,
				ml.bHangCha,
				ml.sXauNoiMa,
				ctct.iID_MaDonVi,
				IsNull(ctct.fBHXH_NLD, 0) ThuBHXHNLDDong,
				IsNull(ctct.fBHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fBHTN_NLD, 0) ThuBHTNNLDDong,
				IsNull(ctct.fBHTN_NSD, 0) ThuBHTNNSDDong
		   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @NamLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @KhoiHachToan
		   WHERE ct.iNamLamViec = @NamLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
		GROUP BY dt_dv.sTenDonVi,
				dt_dv.id
		order by dt_dv.id
				;

	SELECT '' idDonVi,N'A. Khối dự toán' sTenDonVI,cast(0 as float) BhxhNldDongDuToan, cast(0 as float) BhxhNsddDongDuToan, cast(0 as float) BhxhNldDongHachToan, cast(0 as float) BhxhNsddDongHachToan, cast(0 as float) BHXHTongCongDuToan, cast(0 as float) BHXHTongCongHachToan,cast(0 as float) BhtnNldDongDuToan,cast(0 as float) BhtnNsddDongDuToan,cast(0 as float) BhtnNldDongHachToan,cast(0 as float) BhtnNsddDongHachToan,cast(0 as float) BHTNTongCongDuToan,cast(0 as float) BHTNTongCongHachToan into #tempDuToan
	SELECT '' idDonVi, N'B. Khối hạch toán' sTenDonVI,cast(0 as float) BhxhNldDongDuToan, cast(0 as float) BhxhNsddDongDuToan, cast(0 as float) BhxhNldDongHachToan, cast(0 as float) BhxhNsddDongHachToan, cast(0 as float) BHXHTongCongDuToan, cast(0 as float) BHXHTongCongHachToan,cast(0 as float) BhtnNldDongDuToan ,cast(0 as float) BhtnNsddDongDuToan,cast(0 as float) BhtnNldDongHachToan,cast(0 as float) BhtnNsddDongHachToan,cast(0 as float) BHTNTongCongDuToan,cast(0 as float) BHTNTongCongHachToan into #tempHachToan
	if (@IsMillionRound = 1)
		begin 
	-- data du toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhxhNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongDuToan, 
	round(IsNull(dt.BhxhNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongDuToan, 
	round(IsNull(ht.BhxhNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongHachToan, 
	round(IsNull(ht.BhxhNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongHachToan, 
	round(IsNull(dt.BHXHTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongDuToan, 
	round(IsNull(ht.BHXHTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongHachToan, 
	round(IsNull(dt.BhtnNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongDuToan, 
	round(IsNull(dt.BhtnNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongDuToan, 
	round(IsNull(ht.BhtnNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongHachToan, 
	round(IsNull(ht.BhtnNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongHachToan, 
	round(IsNull(dt.BHTNTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongDuToan, 
	round(IsNull(ht.BHTNTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongHachToan
	into #tempDataDuToan
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi=2
	order by donvi.iID_MaDonVi

	-- data hach toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhxhNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongDuToan, 
	round(IsNull(dt.BhxhNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongDuToan, 
	round(IsNull(ht.BhxhNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongHachToan, 
	round(IsNull(ht.BhxhNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongHachToan, 
	round(IsNull(dt.BHXHTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongDuToan, 
	round(IsNull(ht.BHXHTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongHachToan, 
	round(IsNull(dt.BhtnNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongDuToan, 
	round(IsNull(dt.BhtnNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongDuToan, 
	round(IsNull(ht.BhtnNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongHachToan, 
	round(IsNull(ht.BhtnNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongHachToan, 
	round(IsNull(dt.BHTNTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongDuToan, 
	round(IsNull(ht.BHTNTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongHachToan
	into #tempDataHachToan
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2
	order by donvi.iID_MaDonVi

	-- Gop vao du toan
	SELECT IsNull(SUM(B.BhxhNldDongDuToan),0) BhxhNldDongDuToan  ,
	IsNull(SUM(B.BhxhNsddDongDuToan),0) BhxhNsddDongDuToan,
	IsNull(SUM(B.BhxhNldDongHachToan),0) BhxhNldDongHachToan,
	IsNull(SUM(B.BhxhNsddDongHachToan),0) BhxhNsddDongHachToan,
	IsNull(SUM(B.BHXHTongCongDuToan),0) BHXHTongCongDuToan,
	IsNull(SUM(B.BHXHTongCongHachToan),0) BHXHTongCongHachToan,
	IsNull(SUM(B.BhtnNldDongDuToan),0) BhtnNldDongDuToan, 
	IsNull(SUM(B.BhtnNsddDongDuToan),0) BhtnNsddDongDuToan,
	IsNull(SUM(B.BhtnNldDongHachToan),0) BhtnNldDongHachToan,
	IsNull(SUM(B.BhtnNsddDongHachToan),0) BhtnNsddDongHachToan,
	IsNull(SUM(B.BHTNTongCongDuToan),0) BHTNTongCongDuToan, 
	IsNull(SUM(B.BHTNTongCongHachToan),0) BHTNTongCongHachToan
	INTO #TempSumDataDuToan
	FROM #tempDataDuToan B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempDuToan A, #TempSumDataDuToan B

	SELECT 1 Loai,* INTO #TempResultDuToan FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan
	) DuToan

	SELECT IsNull(SUM(B.BhxhNldDongDuToan),0) BhxhNldDongDuToan  ,
	IsNull(SUM(B.BhxhNsddDongDuToan),0) BhxhNsddDongDuToan,
	IsNull(SUM(B.BhxhNldDongHachToan),0) BhxhNldDongHachToan,
	IsNull(SUM(B.BhxhNsddDongHachToan),0) BhxhNsddDongHachToan,
	IsNull(SUM(B.BHXHTongCongDuToan),0) BHXHTongCongDuToan,
	IsNull(SUM(B.BHXHTongCongHachToan),0) BHXHTongCongHachToan,
	IsNull(SUM(B.BhtnNldDongDuToan),0) BhtnNldDongDuToan, 
	IsNull(SUM(B.BhtnNsddDongDuToan),0) BhtnNsddDongDuToan,
	IsNull(SUM(B.BhtnNldDongHachToan),0) BhtnNldDongHachToan,
	IsNull(SUM(B.BhtnNsddDongHachToan),0) BhtnNsddDongHachToan,
	IsNull(SUM(B.BHTNTongCongDuToan),0) BHTNTongCongDuToan, 
	IsNull(SUM(B.BHTNTongCongHachToan),0) BHTNTongCongHachToan
	INTO #TempSumDataHachToan
	FROM #tempDataHachToan B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempHachToan A, #TempSumDataHachToan B

	-- Gop vao Hach Toan
	SELECT 2 Loai,* INTO #TempResultHachToan FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan
	) HachToan
	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan
	UNION ALL
	SELECT * FROM #TempResultHachToan ) Result
	order by Result.Loai, Result.idDonVi

	DROP TABLE #TempSumDataDuToan
	DROP TABLE #tempSumDataHachToan
	DROP TABLE #tempDataDuToan
	DROP TABLE #tempDataHachToan
	DROP TABLE #TempResultDuToan
	DROP TABLE #TempResultHachToan

	end
	else
	begin
	-- data du toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhxhNldDongDuToan, 0)/ @DVT BhxhNldDongDuToan, 
	IsNull(dt.BhxhNsddDongDuToan, 0)/ @DVT BhxhNsddDongDuToan, 
	IsNull(ht.BhxhNldDongHachToan, 0)/ @DVT BhxhNldDongHachToan, 
	IsNull(ht.BhxhNsddDongHachToan, 0)/ @DVT BhxhNsddDongHachToan,
	IsNull(dt.BHXHTongCongDuToan, 0)/ @DVT BHXHTongCongDuToan,
	IsNull(ht.BHXHTongCongHachToan, 0)/ @DVT BHXHTongCongHachToan,
	IsNull(dt.BhtnNldDongDuToan, 0)/ @DVT BhtnNldDongDuToan, 
	IsNull(dt.BhtnNsddDongDuToan, 0)/ @DVT BhtnNsddDongDuToan,
	IsNull(ht.BhtnNldDongHachToan, 0)/ @DVT BhtnNldDongHachToan, 
	IsNull(ht.BhtnNsddDongHachToan, 0)/ @DVT BhtnNsddDongHachToan,
	IsNull(dt.BHTNTongCongDuToan, 0)/ @DVT BHTNTongCongDuToan,
	IsNull(ht.BHTNTongCongHachToan, 0)/ @DVT BHTNTongCongHachToan
	into #tempDataDuToan1
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		    AND donvi.iKhoi=2
	order by donvi.iID_MaDonVi

	-- data hach toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhxhNldDongDuToan, 0)/ @DVT BhxhNldDongDuToan, 
	IsNull(dt.BhxhNsddDongDuToan, 0)/ @DVT BhxhNsddDongDuToan, 
	IsNull(ht.BhxhNldDongHachToan, 0)/ @DVT BhxhNldDongHachToan, 
	IsNull(ht.BhxhNsddDongHachToan, 0)/ @DVT BhxhNsddDongHachToan,
	IsNull(dt.BHXHTongCongDuToan, 0)/ @DVT BHXHTongCongDuToan,
	IsNull(ht.BHXHTongCongHachToan, 0)/ @DVT BHXHTongCongHachToan,
	IsNull(dt.BhtnNldDongDuToan, 0)/ @DVT BhtnNldDongDuToan, 
	IsNull(dt.BhtnNsddDongDuToan, 0)/ @DVT BhtnNsddDongDuToan,
	IsNull(ht.BhtnNldDongHachToan, 0)/ @DVT BhtnNldDongHachToan, 
	IsNull(ht.BhtnNsddDongHachToan, 0)/ @DVT BhtnNsddDongHachToan,
	IsNull(dt.BHTNTongCongDuToan, 0)/ @DVT BHTNTongCongDuToan,
	IsNull(ht.BHTNTongCongHachToan, 0)/ @DVT BHTNTongCongHachToan
	into #tempDataHachToan1
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2
	order by donvi.iID_MaDonVi
	-- Gop vao du toan
	SELECT IsNull(SUM(B.BhxhNldDongDuToan),0) BhxhNldDongDuToan  ,
	IsNull(SUM(B.BhxhNsddDongDuToan),0) BhxhNsddDongDuToan,
	IsNull(SUM(B.BhxhNldDongHachToan),0) BhxhNldDongHachToan,
	IsNull(SUM(B.BhxhNsddDongHachToan),0) BhxhNsddDongHachToan,
	IsNull(SUM(B.BHXHTongCongDuToan),0) BHXHTongCongDuToan,
	IsNull(SUM(B.BHXHTongCongHachToan),0) BHXHTongCongHachToan,
	IsNull(SUM(B.BhtnNldDongDuToan),0) BhtnNldDongDuToan, 
	IsNull(SUM(B.BhtnNsddDongDuToan),0) BhtnNsddDongDuToan,
	IsNull(SUM(B.BhtnNldDongHachToan),0) BhtnNldDongHachToan,
	IsNull(SUM(B.BhtnNsddDongHachToan),0) BhtnNsddDongHachToan,
	IsNull(SUM(B.BHTNTongCongDuToan),0) BHTNTongCongDuToan, 
	IsNull(SUM(B.BHTNTongCongHachToan),0) BHTNTongCongHachToan
	INTO #TempSumDataDuToan1
	FROM #tempDataDuToan1 B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempDuToan A, #TempSumDataDuToan1 B

	SELECT 1 Loai,* INTO #TempResultDuToan1 FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan1
	) DuToan

	SELECT IsNull(SUM(B.BhxhNldDongDuToan),0) BhxhNldDongDuToan  ,
	IsNull(SUM(B.BhxhNsddDongDuToan),0) BhxhNsddDongDuToan,
	IsNull(SUM(B.BhxhNldDongHachToan),0) BhxhNldDongHachToan,
	IsNull(SUM(B.BhxhNsddDongHachToan),0) BhxhNsddDongHachToan,
	IsNull(SUM(B.BHXHTongCongDuToan),0) BHXHTongCongDuToan,
	IsNull(SUM(B.BHXHTongCongHachToan),0) BHXHTongCongHachToan,
	IsNull(SUM(B.BhtnNldDongDuToan),0) BhtnNldDongDuToan, 
	IsNull(SUM(B.BhtnNsddDongDuToan),0) BhtnNsddDongDuToan,
	IsNull(SUM(B.BhtnNldDongHachToan),0) BhtnNldDongHachToan,
	IsNull(SUM(B.BhtnNsddDongHachToan),0) BhtnNsddDongHachToan,
	IsNull(SUM(B.BHTNTongCongDuToan),0) BHTNTongCongDuToan, 
	IsNull(SUM(B.BHTNTongCongHachToan),0) BHTNTongCongHachToan
	INTO #TempSumDataHachToan1
	FROM #tempDataHachToan1 B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempHachToan A, #TempSumDataHachToan1 B

	-- Gop vao Hach Toan
	SELECT 2 Loai,* INTO #TempResultHachToan1 FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan1
	) HachToan
	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan1
	UNION ALL
	SELECT * FROM #TempResultHachToan1 ) Result
	order by Result.Loai, Result.idDonVi

	DROP TABLE #TempSumDataDuToan1
	DROP TABLE #TempSumDataHachToan1
	DROP TABLE #tempDataDuToan1
	DROP TABLE #tempDataHachToan1
	DROP TABLE #TempResultDuToan1
	DROP TABLE #TempResultHachToan1
	end

	DROP TABLE #tempDuToan
	DROP TABLE #tempHachToan
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]    Script Date: 10/28/2024 5:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@SM nvarchar(50),
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int,
	@IsMillionRound bit
AS
BEGIN
	declare @BhytDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50),BhytNSDDongDuToan float, BhytNLDDongDuToan float, TongBhytDuToan float);
	declare @BhytHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50), BhytNSDDongHachToan float,BhytNLDDongHachToan float, TongBhytHachToan float);

	INSERT INTO @BhytDuToan (sTenDonVI, idDonVi, sm, BhytNSDDongDuToan, BhytNLDDongDuToan, TongBhytDuToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   A.sm,
		   SUM(IsNull(A.ThuBHYTNSDDongDuToan, 0)) BhytNSDDongDuToan,
		   SUM(IsNull(A.ThuBHYTNLDDongDuToan, 0)) BhytNLDDongDuToan,
		   SUM(IsNull(A.TongBhytDuToan, 0)) TongBhytDuToan

	FROM
	  (SELECT ml.sm,
			   ml.sMoTa,
			   ctct.iID_MaDonVi,
			   IsNull(ctct.fBHYT_NSD, 0) ThuBHYTNSDDongDuToan,
			   IsNull(ctct.fBHYT_NLD, 0) ThuBHYTNLDDongDuToan,
			   IsNull(ctct.fThuBHYT, 0) TongBhytDuToan
	   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
	   AND ml.iNamLamViec = @NamLamViec
	   AND ml.iTrangThai = 1
	   AND ml.sLNS = @KhoiDuToan
	   AND ml.sM = @SM
	   WHERE ct.iNamLamViec = @NamLamViec
	   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
	   JOIN
	  (SELECT iID_MaDonVi AS id,
			  sTenDonVi, iLoai
	   FROM DonVi
	   WHERE iTrangThai = 1
	   AND iNamLamViec = @namLamViec
	   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
	GROUP BY dt_dv.sTenDonVi,
			dt_dv.id,
			A.sm;

	INSERT INTO @BhytHachToan (idDonVi, sTenDonVI, sm, BhytNSDDongHachToan, BhytNLDDongHachToan, TongBhytHachToan)
		SELECT
			dt_dv.id idDonVi,
		   dt_dv.sTenDonVi,
		   A.sm,
		   SUM(IsNull(A.ThuBHYTNSDDongHachToan, 0)) BhytNSDDongHachToan,
		   SUM(IsNull(A.ThuBHYTNLDDongHachToan, 0)) BhytNLDDongHachToan,
		   SUM(IsNull(A.TongBhytHachToan, 0)) TongBhytHachToan

	FROM
	  (SELECT ml.sm,
			   ml.sMoTa,
			   ctct.iID_MaDonVi,
			   IsNull(ctct.fBHYT_NSD, 0) ThuBHYTNSDDongHachToan,
			   IsNull(ctct.fBHYT_NLD, 0) ThuBHYTNLDDongHachToan,
			   IsNull(ctct.fThuBHYT, 0) TongBhytHachToan
	   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
	   AND ml.iNamLamViec = @NamLamViec
	   AND ml.iTrangThai = 1
	   AND ml.sLNS = @KhoiHachToan
	   AND ml.sM = @SM
	   WHERE ct.iNamLamViec = @namLamViec
	   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
	   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
	   JOIN
	  (SELECT iID_MaDonVi AS id,
			  sTenDonVi, iLoai
	   FROM DonVi
	   WHERE iTrangThai = 1
	   AND iNamLamViec = @namLamViec
	   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
	GROUP BY dt_dv.sTenDonVi,
			dt_dv.id,
			A.sm;


	SELECT '' idDonVi,N'A. Khối dự toán' sTenDonVI, cast(0 as float) BhytNldDongDuToan,cast(0 as float) BhytNsddDongDuToan, cast(0 as float) BhytNldDongHachToan,cast(0 as float) BhytNsddDongHachToan,cast(0 as float) BHYTTongCongDuToan,cast(0 as float) BHYTTongCongHachToan into #tempDuToan
	SELECT '' idDonVi,N'B. Khối hạch toán' sTenDonVI,cast(0 as float) BhytNldDongDuToan,cast(0 as float) BhytNsddDongDuToan,cast(0 as float) BhytNldDongHachToan,cast(0 as float) BhytNsddDongHachToan,cast(0 as float) BHYTTongCongDuToan,cast(0 as float) BHYTTongCongHachToan into #tempHachToan
	if (@IsMillionRound = 1)
	begin 
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhytNLDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongDuToan, 
	round(IsNull(dt.BhytNSDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongDuToan, 
	round(IsNull(ht.BhytNLDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongHachToan, 
	round(IsNull(ht.BhytNSDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongHachToan, 
	round(IsNull(dt.TongBhytDuToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongDuToan, 
	round(IsNull(ht.TongBhytHachToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongHachToan
	into #tempDataDuToan
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi=2

	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhytNLDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongDuToan, 
	round(IsNull(dt.BhytNSDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongDuToan, 
	round(IsNull(ht.BhytNLDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongHachToan, 
	round(IsNull(ht.BhytNSDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongHachToan, 
	round(IsNull(dt.TongBhytDuToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongDuToan, 
	round(IsNull(ht.TongBhytHachToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongHachToan
	into #tempDataHachToan
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2
	-- Sum Du toan
	SELECT 
	isnull(SUM(BhytNldDongDuToan),0) BhytNldDongDuToan,
	isnull(SUM(BhytNsddDongDuToan),0) BhytNsddDongDuToan,
	isnull(SUM(BhytNldDongHachToan),0) BhytNldDongHachToan,
	isnull(SUM(BhytNsddDongHachToan),0) BhytNsddDongHachToan,
	isnull(SUM(BHYTTongCongDuToan),0) BHYTTongCongDuToan,
	isnull(SUM(BHYTTongCongHachToan),0) BHYTTongCongHachToan
	INTO #tempSumDataDuToan
	FROM #tempDataDuToan

	Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempDuToan A , #tempSumDataDuToan B

	-- Sum Hach Toan
	SELECT 
	isnull(SUM(BhytNldDongDuToan),0) BhytNldDongDuToan,
	isnull(SUM(BhytNsddDongDuToan),0) BhytNsddDongDuToan,
	isnull(SUM(BhytNldDongHachToan),0) BhytNldDongHachToan,
	isnull(SUM(BhytNsddDongHachToan),0) BhytNsddDongHachToan,
	isnull(SUM(BHYTTongCongDuToan),0) BHYTTongCongDuToan,
	isnull(SUM(BHYTTongCongHachToan),0) BHYTTongCongHachToan
	INTO #tempSumDataHachToan
	FROM #tempDataHachToan

		Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempHachToan A , #tempSumDataHachToan B

	-- Gop Du toan
	SELECT 1 Loai,* INTO #TempResultDuToan FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan
	) DuToan

	-- Gop vao Hach Toan
	SELECT 2 Loai,* INTO #TempResultHachToan FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan
	) HachToan

	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan
	UNION ALL
	SELECT * FROM #TempResultHachToan ) Result
	order by Result.Loai, Result.idDonVi

	DROP TABLE #tempDataDuToan
	DROP TABLE #tempDataHachToan
	DROP TABLE #tempSumDataDuToan
	DROP TABLE #tempSumDataHachToan
	DROP TABLE #TempResultDuToan
	DROP TABLE #TempResultHachToan
	end
	else
	begin 
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhytNLDDongDuToan, 0)/@DVT BhytNldDongDuToan, 
	IsNull(dt.BhytNSDDongDuToan, 0)/@DVT BhytNsddDongDuToan, 
	IsNull(ht.BhytNLDDongHachToan, 0)/@DVT BhytNldDongHachToan, 
	IsNull(ht.BhytNSDDongHachToan, 0)/@DVT BhytNsddDongHachToan,
	IsNull(dt.TongBhytDuToan, 0)/@DVT BHYTTongCongDuToan,
	IsNull(ht.TongBhytHachToan, 0)/@DVT BHYTTongCongHachToan
	INTO #tempDataDuToan1
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi=2
	

	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhytNLDDongDuToan, 0)/@DVT BhytNldDongDuToan, 
	IsNull(dt.BhytNSDDongDuToan, 0)/@DVT BhytNsddDongDuToan, 
	IsNull(ht.BhytNLDDongHachToan, 0)/@DVT BhytNldDongHachToan, 
	IsNull(ht.BhytNSDDongHachToan, 0)/@DVT BhytNsddDongHachToan,
	IsNull(dt.TongBhytDuToan, 0)/@DVT BHYTTongCongDuToan,
	IsNull(ht.TongBhytHachToan, 0)/@DVT BHYTTongCongHachToan
	INTO #tempDataHachToan1
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2
		-- Sum Du toan
	SELECT 
	isnull(SUM(BhytNldDongDuToan),0) BhytNldDongDuToan,
	isnull(SUM(BhytNsddDongDuToan),0) BhytNsddDongDuToan,
	isnull(SUM(BhytNldDongHachToan),0) BhytNldDongHachToan,
	isnull(SUM(BhytNsddDongHachToan),0) BhytNsddDongHachToan,
	isnull(SUM(BHYTTongCongDuToan),0) BHYTTongCongDuToan,
	isnull(SUM(BHYTTongCongHachToan),0) BHYTTongCongHachToan
	INTO #tempSumDataDuToan1
	FROM #tempDataDuToan1

	Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempDuToan A , #tempSumDataDuToan1 B

	-- Sum Hach Toan
	SELECT 
	isnull(SUM(BhytNldDongDuToan),0) BhytNldDongDuToan,
	isnull(SUM(BhytNsddDongDuToan),0) BhytNsddDongDuToan,
	isnull(SUM(BhytNldDongHachToan),0) BhytNldDongHachToan,
	isnull(SUM(BhytNsddDongHachToan),0) BhytNsddDongHachToan,
	isnull(SUM(BHYTTongCongDuToan),0) BHYTTongCongDuToan,
	isnull(SUM(BHYTTongCongHachToan),0) BHYTTongCongHachToan
	INTO #tempSumDataHachToan1
	FROM #tempDataHachToan1

		Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempHachToan A , #tempSumDataHachToan1 B

	-- Gop Du toan
	SELECT 1 Loai,* INTO #TempResultDuToan1 FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan1
	) DuToan

	-- Gop vao Hach Toan
	SELECT 2 Loai,* INTO #TempResultHachToan1 FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan1
	) HachToan

	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan1
	UNION ALL
	SELECT * FROM #TempResultHachToan1 ) Result
	order by Result.Loai, Result.idDonVi

	DROP TABLE #tempDataDuToan1
	DROP TABLE #tempDataHachToan1
	DROP TABLE #tempSumDataDuToan1
	DROP TABLE #tempSumDataHachToan1
	DROP TABLE #TempResultDuToan1
	DROP TABLE #TempResultHachToan1
	end
	DROP TABLE #tempDuToan
	DROP TABLE #tempHachToan
END
;
;
;
;
;
;
;
;
GO
