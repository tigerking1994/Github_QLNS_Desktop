/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 3/29/2024 4:31:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 3/29/2024 4:31:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 3/29/2024 4:31:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh] 
	-- Add the parameters for the stored procedure here
	@sXauNoiMa nvarchar(max),
	@sQuarter nvarchar(50),
	@Donvi  nvarchar(50),
	@NamLamViec int
AS
BEGIN
	DECLARE @STongHop nvarchar(max);	
	select top(1) bangluong.STongHop into #temp1  FROM TL_BangLuong_ThangBHXH bangluongchitiet 
			JOIN  TL_DS_CapNhap_BangLuong bangluong on bangluongchitiet.iID_Parent = bangluong.Id
			where
				bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
					AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
							FROM TL_DM_CheDoBHXH
							WHERE bDisplay = 1 
								AND sXauNoiMaMlnsBHXH = @sXauNoiMa )
					AND (
						bangluongchitiet.sMaCB LIKE '1%'  
						OR bangluongchitiet.sMaCB LIKE '2%'  
						OR bangluongchitiet.sMaCB = '3.1'  
						OR bangluongchitiet.sMaCB = '3.2'  
						OR bangluongchitiet.sMaCB = '3.3'
						OR bangluongchitiet.sMaCB = '413' 
						OR bangluongchitiet.sMaCB = '415' 
						OR bangluongchitiet.sMaCB = '423' 
						OR bangluongchitiet.sMaCB = '425' 
						OR bangluongchitiet.sMaCB = '43' 
						OR bangluongchitiet.sMaCB LIKE '0%'
			)
			and bangluong.Ma_CachTL = N'CACH2'
			and bangluong.Ma_CBo = @Donvi
			and bangluong.KhoaBangLuong = 1
			and bangluong.Nam = @NamLamViec
			and bangluong.STongHop is not null

	set @STongHop = (select #temp1.STongHop from #temp1);

	select * into #tblSTongHop from splitstring(@STongHop);

	-- Get bang luong chi tiet 
	select * into #temp2 from TL_BangLuong_ThangBHXH bangluongchitiet  where bangluongchitiet.sMaDonVi in (select * from #tblSTongHop)
	and bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
		AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
										FROM TL_DM_CheDoBHXH
										WHERE bDisplay = 1 
										AND sXauNoiMaMlnsBHXH = @sXauNoiMa )
	AND (
		bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
	)


	SELECT 
		@sXauNoiMa as SXauNoiMa,
		sMaHieuCanBo as SMaHieuCanBo,
		sTenCbo as STenCanBo,
		sMaCBo As SMaCanBo,
		sMaCB as SMaCapBac,
		capbac.Note aS STenCapBac,
		bangluongchitiet.sMaDonVi as ID_MaPhanHo,
		dv.Ten_DonVi as STenPhanHo,
		canbo.fSoNgayHuongBHXH as ISoNgayHuong,
		canbo.sSoQuyetDinh as SSoQuyetDinh,
		canbo.dNgayQuyetDinh as DNgayQuyetDinh,
		bangluongchitiet.nGiaTri as FSoTien,
		canbo.dTuNgay AS DTuNgay,
		canbo.dDenNgay AS DDenNgay,
		bangluongchitiet.sMaCB
	FROM #temp2 bangluongchitiet 
	LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluongchitiet.sMaCBo = canbo.sMaCanBo and bangluongchitiet.sMaCheDo=canbo.sMaCheDo
	LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluongchitiet.sMaCB
	LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluongchitiet.sMaDonVi
	where  (
			bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
		)
		and canbo.iNam=@NamLamViec

	--	SELECT 
	--	N'9010001-010-011-0001-0001-0001-01-02' as SXauNoiMa,
	--	sMaHieuCanBo as SMaHieuCanBo,
	--	sTenCbo as STenCanBo,
	--	sMaCBo As SMaCanBo,
	--	sMaCB as SMaCapBac,
	--	capbac.Note aS STenCapBac,
	--	bangluong.sMaDonVi as ID_MaPhanHo,
	--	dv.Ten_DonVi as STenPhanHo,
	--	canbo.fSoNgayHuongBHXH as ISoNgayHuong,
	--	canbo.sSoQuyetDinh as SSoQuyetDinh,
	--	canbo.dNgayQuyetDinh as DNgayQuyetDinh,
	--	bangluong.nGiaTri as FSoTien,
	--	canbo.dTuNgay AS DTuNgay,
	--	canbo.dDenNgay AS DDenNgay

	--FROM TL_BangLuong_ThangBHXH bangluong
	--LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluong.sMaCBo = canbo.sMaCanBo
	--LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluong.sMaCB
	--LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluong.sMaDonVi
	--WHERE 
	--	bangluong.iThang IN (SELECT * FROM splitstring('1,2,3'))
	--	AND bangluong.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH WHERE bDisplay = 1 AND sXauNoiMaMlnsBHXH = N'9010001-010-011-0001-0001-0001-01-02' )
	--	AND (
	--		bangluong.sMaCB LIKE '1%'  
	--		OR bangluong.sMaCB LIKE '2%'  
	--		OR bangluong.sMaCB LIKE '3.1%'  
	--		OR bangluong.sMaCB LIKE '3.2%'  
	--		OR bangluong.sMaCB LIKE '3.3%'  
	--		OR bangluong.sMaCB LIKE '43%' 
	--		OR bangluong.sMaCB LIKE '0%' 
	--	)

	-- Get data 
	--SELECT 
	--	@sXauNoiMa as SXauNoiMa,
	--	sMaHieuCanBo as SMaHieuCanBo,
	--	sTenCbo as STenCanBo,
	--	sMaCBo As SMaCanBo,
	--	sMaCB as SMaCapBac,
	--	capbac.Note aS STenCapBac,
	--	bangluongchitiet.sMaDonVi as ID_MaPhanHo,
	--	dv.sTenDonVi as STenPhanHo,
	--	canbo.fSoNgayHuongBHXH as ISoNgayHuong,
	--	canbo.sSoQuyetDinh as SSoQuyetDinh,
	--	canbo.dNgayQuyetDinh as DNgayQuyetDinh,
	--	bangluongchitiet.nGiaTri as FSoTien,
	--	canbo.dTuNgay AS DTuNgay,
	--	canbo.dDenNgay AS DDenNgay

	--FROM TL_BangLuong_ThangBHXH bangluongchitiet 
	--LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluongchitiet.sMaCBo = canbo.sMaCanBo
	--LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluongchitiet.sMaCB
	--LEFT JOIN DonVi dv On dv.iID_MaDonVi = bangluongchitiet.sMaDonVi
	--JOIN  TL_DS_CapNhap_BangLuong bangluong on bangluongchitiet.iID_Parent = bangluong.Id
	--WHERE 
	--	bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
	--	AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH WHERE bDisplay = 1 AND sXauNoiMaMlnsBHXH = @sXauNoiMa )
	--	AND (
	--		bangluongchitiet.sMaCB LIKE '1%'  
	--		OR bangluongchitiet.sMaCB LIKE '2%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.1%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.2%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.3%'  
	--		OR bangluongchitiet.sMaCB LIKE '43%' 
	--		OR bangluongchitiet.sMaCB LIKE '0%' 
	--	)
	--	and bangluong.Ma_CachTL = N'CACH2'
	--	and bangluong.Ma_CBo=@Donvi
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 3/29/2024 4:31:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn]
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@SoQuyetDinh nvarchar(200),
	@NgayQuyetDinh nvarchar(200),
	@DVT int
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
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '2', NEWID(), @IIdDTT, N'Dự toán thu BHTN', 2, 2, SUM(ISNULL(ctct.fThuBHTN, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '3', NEWID(), @IIdDTT, N'Dự toán thu BHYT quân nhân', 2, 3, SUM(ISNULL(ctct.fThuBHYT, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')

		UNION ALL
		SELECT '4', NEWID(), @IIdDTT, N'Dự toán thu BHYT người lao động', 2, 4, SUM(ISNULL(ctct.fThuBHYT, 0)) FSoTien
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
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
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND (ctct.sXauNoiMa like '9030001-010-011-0001%' OR ctct.sXauNoiMa like '9030001-010-011-0002%')

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYTTN, N'Thân nhân người lao động', 3, 1, SUM(ISNULL(ctct.fDuToan, 0)) FSoTien
		FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		join BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
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
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '01'

		UNION ALL
		SELECT '2', NEWID(), @IIdDTC, N'Dự toán chi kinh phí quản lý BHXH', 2, 2, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '02'

		UNION ALL
		SELECT '3', NEWID(), @IIdDTC, N'Dự toán chi kinh phí KCB tại quân y đơn vị', 2, 3, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '03'

		UNION ALL
		SELECT '4', NEWID(), @IIdDTC, N'Dự toán chi KCB tại Trường Sa - DK', 2, 4, SUM(ISNULL(ctct.fTienTuChi, 0)) FSoTien
		FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		WHERE ctct.iNamLamViec = @NamLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND ctct.sMaLoaiChi = '04'
			)

	SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien/@DVT FSoTien from #result

	DROP TABLE #result;

END
;
GO
