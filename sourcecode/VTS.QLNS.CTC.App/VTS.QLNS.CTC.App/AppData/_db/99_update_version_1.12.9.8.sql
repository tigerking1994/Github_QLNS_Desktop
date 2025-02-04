/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_duan_nguonvon_by_duan]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_all_duan_nguonvon_by_duan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_all_duan_nguonvon_by_duan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_baocaotinhhinhduan]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_baocaotinhhinhduan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_baocaotinhhinhduan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_dt_rpt_lns_thang]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_dt_rpt_lns_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_dt_rpt_lns_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_binhquan_donvi]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_rpt_binhquan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_rpt_binhquan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_nhap_so_nhu_cau]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_cd_bhxh_nhap_so_nhu_cau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_cd_bhxh_nhap_so_nhu_cau]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thongtinkehoachthu_index]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_thongtinkehoachthu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_thongtinkehoachthu_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_xh_kehoach_chi_index]    Script Date: 06/07/2023 6:46:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_xh_kehoach_chi_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_xh_kehoach_chi_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_xh_kehoach_chi_index]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi các chế dộ BHXH

-- =============================================
CREATE PROCEDURE [dbo].[bh_xh_kehoach_chi_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 CHBHXH.ID AS iID_BH_KHC_CheDoBHXH
		, CHBHXH.sSoChungTu
		, CHBHXH.dNgayChungTu
		, CHBHXH.sSoQuyetDinh
		, CHBHXH.iNamChungTu
		, CHBHXH.iID_DonVi
		, CHBHXH.iID_MaDonVi
		, CHBHXH.sMoTa
		, CHBHXH.iTongSoDaThucHienNamTruoc
		, CHBHXH.iTongSoUocThucHienNamTruoc

		, CHBHXH.iTongSoKeHoachThucHienNamNay
		, CHBHXH.iTongSoSQ
		, CHBHXH.iTongSoQNCN
		, CHBHXH.iTongSoCNVQP
		, CHBHXH.iTongSoLDHD
		, CHBHXH.iTongSoHSQBS

		, CHBHXH.fTongTienDaThucHienNamTruoc

		, CHBHXH.fTongTienUocThucHienNamTruoc
		, CHBHXH.fTongTienKeHoachThucHienNamNay

		, CHBHXH.fTongTienSQ
		, CHBHXH.fTongTienQNCN
		, CHBHXH.fTongTienCNVQP
		, CHBHXH.fTongTienLDHD
		, CHBHXH.fTongTienHSQBS

		, CHBHXH.sTongHop
		, CHBHXH.iID_TongHopID
		, CHBHXH.iLoaiTongHop
		, CHBHXH.bIsKhoa

		, CHBHXH.dNgaySua
		, CHBHXH.dNgayTao
		, CHBHXH.dNgayQuyetDinh
		, CHBHXH.sNguoiSua
		, CHBHXH.sNguoiTao
		, CHBHXH.dNgayTao
		, donVi.sTenDonVi
		, (CHBHXH.fTongTienSQ+CHBHXH.fTongTienDaThucHienNamTruoc+CHBHXH.fTongTienKeHoachThucHienNamNay+CHBHXH.fTongTienQNCN+CHBHXH.fTongTienCNVQP+CHBHXH.fTongTienLDHD+CHBHXH.fTongTienUocThucHienNamTruoc+CHBHXH.fTongTienHSQBS) as isTongTien
		-- Tong dự toán todo
	FROM BH_KHC_CheDoBHXH CHBHXH
	LEFT JOIN DonVi donVi
		ON CHBHXH.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = CHBHXH.iID_DonVi
	LEFT JOIN BH_KHC_CheDoBHXH_ChiTiet CHBHXHCT ON CHBHXH.ID=CHBHXHCT.iID_KHC_CheDoBHXH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thongtinkehoachthu_index]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_thongtinkehoachthu_index] 
	@YearOfWork int
AS
BEGIN
	SELECT
	KHT.iID_KHT_BHXH,
	KHT.sSoChungTu,
	KHT.iNamChungTu,
	KHT.dNgayChungTu,
	KHT.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHT.sMoTa,
	KHT.bIsKhoa,
	KHT.fTongKeHoach,
	KHT.sSoQuyetDinh,
	KHT.dNgayQuyetDinh,
	KHT.sNguoiTao,
	KHT.sNguoiSua,
	KHT.dNgayTao,
	KHT.dNgaySua,
	KHT.iLoaiTongHop,
	KHT.sTongHop,
	KHT.fThuBHXHNLDDong,
	KHT.fThuBHXHNSDDong,
	KHT.fThuBHXH,
	KHT.fThuBHYTNLDDong,
	KHT.fThuBHYTNSDDong,
	KHT.fTongBHYT,
	KHT.fThuBHTNNLDDong,
	KHT.fThuBHTNNSDDong,
	KHT.fThuBHTN,
	KHT.fTong,
	KHT.bDaTongHop
	FROM BH_KHT_BHXH KHT
	LEFT JOIN DonVi DV
	ON KHT.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	ORDER BY KHT.dNgayQuyetDinh DESC
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@UserName NVARCHAR(100)
AS
BEGIN
	DECLARE @sChungTuId NVARCHAR(255) = @ChungTuId;
	DECLARE @sLNS NVARCHAR(MAX) = @LNS;
	DECLARE @sIdDonVi NVARCHAR(MAX) = @IdDonVi;
	DECLARE @iNamLamViec int = @NamLamViec;
	DECLARE @iNamNganSach int = @NamNganSach;
	DECLARE @iNguonNganSach int = @NguonNganSach;
	DECLARE @sUserName NVARCHAR(100) = @UserName;
	DECLARE @CountDonViCha int; 
	DECLARE @isQuanly int;
	SELECT @CountDonViCha = count(*)
	FROM
	  (SELECT *
	   FROM NguoiDung_DonVi
	   WHERE iID_MaNguoiDung = @sUserName
		 AND iNamLamViec = @iNamLamViec
		 AND iTrangThai = 1) nddv
	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @iNamLamViec
		 AND iTrangThai = 1
		 AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	DECLARE @dNgayQuyetDinh AS DATETIME;
	DECLARE @dNgayChungTu AS DATETIME;
	DECLARE @dNgayPhanBo AS DATETIME;
	DECLARE @iSoChungTuIndex AS int;
	SELECT 
		@dNgayQuyetDinh = CAST(dNgayQuyetDinh AS date), 
		@dNgayChungTu = CAST(dNgayChungTu AS date),
		@iSoChungTuIndex = iSoChungTuIndex
	FROM NS_DT_ChungTu 
	WHERE iID_DTChungTu = @sChungTuId;

	SELECT  @isQuanly =  COUNT(*) FROM  DonVi inner join  NguoiDung_DonVi  ON  DonVi.iID_MaDonVi =  NguoiDung_DonVi.iID_MaDonVi  
	WHERE  iLoai = 0 and NguoiDung_DonVi.iID_MaNguoiDung = @sUserName and NguoiDung_DonVi.inamlamviec = @iNamLamViec  and DonVi.iNamLamViec= @iNamLamViec;
	IF @isQuanly >0
		SET @isQuanly=1
	ELSE
		SET @isQuanly=0 

	IF @dNgayQuyetDinh IS NULL
		SELECT @dNgayPhanBo = @dNgayChungTu;
	ELSE 
		SELECT @dNgayPhanBo = @dNgayQuyetDinh;

	SELECT DISTINCT sLNS INTO #tblLns FROM NS_DT_ChungTuChiTiet 
	WHERE iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)

	--SELECT iID_MLNS INTO #tblIdMlns FROM NS_DT_ChungTuChiTiet 
	--WHERE iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)

	SELECT iID_MLNS INTO #tblIdMlns 
	FROM (
	SELECT * FROM NS_DT_ChungTuChiTiet WHERE iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)
	UNION ALL 
	SELECT * FROM NS_DT_ChungTuChiTiet WHERE iID_DTChungTu = @sChungTuId) temp
	GROUP BY iID_MLNS;

	SELECT * INTO #tblMlns
	FROM NS_MucLucNganSach
	WHERE iNamLamViec = @iNamLamViec
		AND iTrangThai = 1
		AND bHangChaDuToan is not null
		AND ((@CountDonViCha <> 0
			AND sLNS IN
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(Item, 1) AS NVARCHAR(10)) LNS1,
							CAST(LEFT(Item, 3) AS NVARCHAR(10)) LNS3,
							CAST(LEFT(Item, 5) AS NVARCHAR(10)) LNS5,
							CAST(Item AS NVARCHAR(10)) LNS
					FROM f_split(@LNS) ) LNS UNPIVOT (value
															FOR col in (LNS1, LNS3, LNS5, LNS)) un))
			OR (@CountDonViCha = 0
				AND sLNS IN
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS NVARCHAR(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS NVARCHAR(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS NVARCHAR(10)) LNS5,
							CAST(sLNS AS NVARCHAR(10)) LNS
					FROM NS_NguoiDung_LNS
					WHERE sMaNguoiDung = @sUserName
						AND iNamLamViec = @iNamLamViec
						AND sLNS IN
						(SELECT *
							FROM f_split(@sLNS)) ) LNS UNPIVOT (value
															FOR col in (LNS1, LNS3, LNS5, LNS)) un)))

	-- lấy dữ liệu seting nhập từ bảng MLNS
	SELECT sLNS, bTuChi, bHienVat, bHangNhap, bHangMua, bPhanCap, bDuPhong, bTonKho INTO #tblMlnsseting FROM #tblMlns WHERE sL = ''
	
	-- lấy dữ liệu MLNS cha
	SELECT * INTO #tblMlnsParent FROM #tblMlns WHERE bHangChaDuToan = 1

	-- lấy dữ liệu MLNS con
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, mlns.sLns, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, bHangChaDuToan,
		seting.bTuChi, seting.bHienVat, seting.bHangNhap, seting.bHangMua, seting.bPhanCap, seting.bDuPhong, seting.bTonKho
	INTO #tblMlnsChild
	FROM #tblMlns mlns
	LEFT JOIN #tblMlnsseting seting
	ON seting.sLNS = mlns.sLNS
	WHERE bHangChaDuToan = 0 
	and (NOT EXISTS(SELECT * FROM #tblLns) OR (EXISTS(SELECT * FROM #tblLns) AND iID_MLNS in (SELECT * FROM #tblIdMlns)))

	-- lấy ra đơn vị được chọn để phân bổ theo iID_ChungTu phân bổ
	SELECT iID_MaDonVi AS MaDonVi, sTenDonVi
	INTO #tblDonViPhanBo
	FROM DonVi
	WHERE iNamLamViec = @iNamLamViec
		AND iID_MaDonVi IN (SELECT * FROM f_split(@sIdDonVi))
	-- lấy ra số quyết định nhận được chọn để phân bổ theo iID_ChungTu phân bổ
	SELECT 
		sSoQuyetDinh, 
		iID_DTChungTu AS idSoQuyetDinh 
	INTO #tblSoQuyetDinhNhan
	FROM NS_DT_ChungTu 
	WHERE iID_DTChungTu IN (
		SELECT iID_CTDuToan_Nhan 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_PhanBo = @sChungTuId
	)
	-- lấy dữ liệu nhận phân bổ theo so quyet dinh va don vi
		SELECT vctpb.iID_MLNS, 
		vctpb.sSoQuyetDinh, 
		vctpb.idSoQuyetDinh,
		SUM(fTuChi) AS fTuChi, 
		SUM(fHienVat) AS fHienVat,
		SUM(fDuPhong) AS fDuPhong,
		SUM(fHangMua) AS fHangMua,
		SUM(fHangNhap) AS fHangNhap,
		SUM(fPhanCap) AS fPhanCap,
		SUM(fTonKho) AS fTonKho 
		INTO #tblChungTuNhanPhanBo
		FROM  (
			SELECT 
				ctct.iID_MLNS, 
				(CASE WHEN @isQuanly=1 then dt.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,  --dt.sSoQuyetDinh, 
				(CASE WHEN @isQuanly=1 then dt.iID_DTChungTu ELSE CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,  --dt.iID_DTChungTu AS idSoQuyetDinh,
				fTuChi, 
				fHienVat,
				fDuPhong,
				fHangMua,
				fHangNhap,
				fPhanCap,
				fTonKho
			FROM NS_DT_ChungTuChiTiet ctct
			INNER JOIN NS_DT_ChungTu dt
			ON dt.iID_DTChungTu = ctct.iID_DTChungTu AND ctct.iDuLieuNhan = 0
			WHERE dt.iID_DTChungTu IN ( SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId) 
	) vctpb
	GROUP BY vctpb.iID_MLNS,vctpb.sSoQuyetDinh, vctpb.idSoQuyetDinh

	SELECT vdaphanbo.iID_MLNS, 
		vdaphanbo.sSoQuyetDinh, 
		vdaphanbo.idSoQuyetDinh,
		0 - SUM(fTuChi) AS fTuChi, 
		0 - SUM(fHienVat) AS fHienVat,
		0 - SUM(fDuPhong) AS fDuPhong,
		0 - SUM(fHangMua) AS fHangMua,
		0 - SUM(fHangNhap) AS fHangNhap,
		0 - SUM(fPhanCap) AS fPhanCap,
		0 - SUM(fTonKho) AS fTonKho
	INTO #tblDaPhanBo
	FROM (
		
			SELECT 
				ctct.iID_MLNS, 
				(CASE WHEN @isQuanly=1 then ct.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,  --ct.sSoQuyetDinh, 
				(CASE WHEN @isQuanly=1 then ct.iID_DTChungTu ELSE CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,  --ct.iID_DTChungTu AS idSoQuyetDinh,
				fTuChi, 
				fHienVat,
				fDuPhong,
				fHangMua,
				fHangNhap,
				fPhanCap,
				fTonKho
			FROM NS_DT_ChungTuChiTiet ctct
			LEFT JOIN NS_DT_ChungTu ct
			ON ctct.iID_CTDuToan_Nhan = ct.iID_DTChungTu
			LEFT JOIN NS_DT_ChungTu dtct
			ON ctct.iID_DTChungTu = dtct.iID_DTChungTu
			WHERE (iID_CTDuToan_Nhan is not null and iID_CTDuToan_Nhan IN (SELECT idSoQuyetDinh FROM #tblSoQuyetDinhNhan))
			AND (
				(dtct.dNgayQuyetDinh IS NOT NULL 
				AND 
				(
					(CAST(dtct.dNgayQuyetDinh AS DATE) = CAST(@dNgayPhanBo AS DATE) AND dtct.iSoChungTuIndex < @iSoChungTuIndex)
					OR 
					(CAST(dtct.dNgayQuyetDinh AS DATE) < CAST(@dNgayPhanBo AS DATE))
				))
				OR 
				(dtct.dNgayQuyetDinh IS NULL 
				AND 
				(
					CAST(dtct.dNgayChungTu AS DATE) <= CAST(@dNgayPhanBo AS DATE) AND dtct.iSoChungTuIndex < @iSoChungTuIndex)
					OR 
					(CAST(dtct.dNgayChungTu AS DATE) < CAST(@dNgayPhanBo AS DATE))
				)
			)
			AND ctct.iID_DTChungTu <> @sChungTuId
	) vdaphanbo
		
		
	GROUP BY vdaphanbo.iID_MLNS, vdaphanbo.sSoQuyetDinh, vdaphanbo.idSoQuyetDinh

	SELECT iID_MLNS, sSoQuyetDinh, idSoQuyetDinh,
		SUM(fTuChi) AS fTuChi, 
		SUM(fHienVat) AS fHienVat,
		SUM(fDuPhong) AS fDuPhong,
		SUM(fHangMua) AS fHangMua,
		SUM(fHangNhap) AS fHangNhap,
		SUM(fPhanCap) AS fPhanCap,
		SUM(fTonKho) AS fTonKho
		INTO #tblNhanPhanBo
		FROM (
			SELECT 
				iID_MLNS, --sSoQuyetDinh, idSoQuyetDinh,
					
				(CASE WHEN @isQuanly=1 then sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,  --ct.sSoQuyetDinh, 
				(CASE WHEN @isQuanly=1 then idSoQuyetDinh ELSE CAST(NULL AS UNIQUEIDENTIFIER) END) AS  idSoQuyetDinh,  --ct.iID_DTChungTu AS idSoQuyetDinh,
				fTuChi, 
				fHienVat,
				fDuPhong,
				fHangMua,
				fHangNhap,
				fPhanCap,
				fTonKho
			FROM (
			SELECT * FROM #tblChungTuNhanPhanBo
			UNION ALL
			SELECT * FROM #tblDaPhanBo
			) npb
		) vnhanPhanBo
	GROUP BY iID_MLNS, sSoQuyetDinh, idSoQuyetDinh

	-- lấy ra dữ liệu nhận phân bổ (iRowType = 1)
	SELECT * INTO #tblMlnsChildAndSqd FROM #tblMlnsChild, #tblSoQuyetDinhNhan

	SELECT 
		NEWID() AS iID_DTCTChiTiet,
		CAST(0x0 AS uniqueidentIFier) AS iID_DTChungTu,
		c.iID_MLNS,
		c.iID_MLNS_Cha,
		c.sXauNoiMa,
		c.sLNS,
		c.sL,
		c.sK,
		c.sM,
		c.sTM,
		c.sTTM,
		c.sNG,
		c.sTNG,
		c.sTNG1,
		c.sTNG2,
		c.sTNG3,
		c.sMoTa,
		'1' AS bHangCha,
		'' AS iID_MaDonVi,
		'' AS sTenDonVi,
		--c.sSoQuyetDinh,
		(CASE WHEN @isQuanly=1 then c.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,  --c.sSoQuyetDinh, 
		(CASE WHEN @isQuanly=1 then c.idSoQuyetDinh ELSE  CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh ,  --c.sSoQuyetDinh, 
		--idSoQuyetDinh

		ISNULL(p.fTonKho, 0) AS fTonKho,
		ISNULL(p.fTuChi, 0) AS fTuChi,
		ISNULL(p.fHienVat, 0) AS fHienVat,
		ISNULL(p.fHangNhap, 0) AS fHangNhap,
		ISNULL(p.fHangMua, 0) AS fHangMua,
		ISNULL(p.fPhanCap, 0) AS fPhanCap,
		ISNULL(p.fDuPhong, 0) AS fDuPhong,
		c.idSoQuyetDinh AS iID_CTDuToan_Nhan,
		'' AS sGhiChu,
		1 AS iRowType,
		CAST(1 AS bit) AS bHangChaDuToan,
		CAST(0 AS bit) AS IsEditTuChi,
		CAST(0 AS bit) AS IsEditHienVat,
		CAST(0 AS bit) AS IsEditHangNhap,
		CAST(0 AS bit) AS IsEditHangMua,
		CAST(0 AS bit) AS IsEditPhanCap,
		CAST(0 AS bit) AS IsEditDuPhong
	INTO #tblRowNhanPhanBo
	FROM #tblMlnsChildAndSqd c
	LEFT JOIN #tblNhanPhanBo p
	ON c.iID_MLNS = p.iID_MLNS and c.idSoQuyetDinh = p.idSoQuyetDinh

	-- lấy ra dòng cha từ bảng tmpMlnsParent iRowType = 0
	SELECT 
		NEWID() AS iID_DTCTChiTiet,
		CAST(0x0 AS uniqueidentIFier) AS iID_DTChungTu,
		iID_MLNS,
		iID_MLNS_Cha,
		sXauNoiMa,
		sLNS,
		sL,
		sK,
		sM,
		sTM,
		sTTM,
		sNG,
		sTNG,
		sTNG1,
		sTNG2,
		sTNG3,
		sMoTa,
		bHangCha,
		'' AS iID_MaDonVi,
		'' AS sTenDonVi,
		'' AS sSoQuyetDinh,
		CAST(null AS uniqueidentIFier) AS idSoQuyetDinh,
		0 AS fTonKho,
		0 AS fTuChi,
		0 AS fHienVat,
		0 AS fHangNhap,
		0 AS fHangMua,
		0 AS fPhanCap,
		0 AS fDuPhong,
		cast(0x0 as uniqueidentifier) AS iID_CTDuToan_Nhan,
		'' AS sGhiChu,
		0 AS iRowType,
		bHangChaDuToan,
		CAST(0 AS bit) AS IsEditTuChi,
		CAST(0 AS bit) AS IsEditHienVat,
		CAST(0 AS bit) AS IsEditHangNhap,
		CAST(0 AS bit) AS IsEditHangMua,
		CAST(0 AS bit) AS IsEditPhanCap,
		CAST(0 AS bit) AS IsEditDuPhong
	INTO #tblRowCha
	FROM #tblMlnsParent

	-- lấy ra dòng cON từ bảng tmpMlnsChild và bảng NS_DT_ChungTuChiTiet iRowType = 3
	SELECT * INTO #tblMlnsChildAndDvSqd FROM #tblMlnsChild, #tblDonViPhanBo, #tblSoQuyetDinhNhan

	SELECT 
		ISNULL(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
		ctct.iID_DTChungTu,
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sTNG1,
		mlns.sTNG2,
		mlns.sTNG3,
		mlns.sMoTa,
		mlns.bHangCha,
		mlns.MaDonVi AS iID_MaDonVi,
		mlns.MaDonVi + ' - ' + mlns.sTenDonVi AS sTenDonVi,
		--mlns.sSoQuyetDinh,
		(CASE WHEN @isQuanly=1 then mlns.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,
		(CASE WHEN @isQuanly=1 then mlns.idSoQuyetDinh ELSE  CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh ,
			
		ISNULL(ctct.fTonKho, 0) AS fTonKho,
		ISNULL(ctct.fTuChi, 0) AS fTuChi,
		ISNULL(ctct.fHienVat, 0) AS fHienVat,
		ISNULL(ctct.fHangNhap, 0) AS fHangNhap,
		ISNULL(ctct.fHangMua, 0) AS fHangMua,
		ISNULL(ctct.fPhanCap, 0) AS fPhanCap,
		ISNULL(ctct.fDuPhong, 0) AS fDuPhong,
		mlns.idSoQuyetDinh AS iID_CTDuToan_Nhan,
		ISNULL(ctct.sGhiChu, '') AS sGhiChu,
		3 AS iRowType,
		mlns.bHangChaDuToan,
		ISNULL(mlns.bTuChi, CAST(0 AS bit)) AS IsEditTuChi,
		ISNULL(mlns.bHienVat, CAST(0 AS bit)) AS IsEditHienVat,
		ISNULL(mlns.bHangNhap, CAST(0 AS bit)) AS IsEditHangNhap,
		ISNULL(mlns.bHangMua, CAST(0 AS bit)) AS IsEditHangMua,
		ISNULL(mlns.bPhanCap, CAST(0 AS bit)) AS IsEditPhanCap,
		ISNULL(mlns.bDuPhong, CAST(0 AS bit)) AS IsEditDuPhong
	INTO #tblRowChiTiet
	FROM #tblMlnsChildAndDvSqd mlns
	LEFT JOIN
	(SELECT *
		FROM NS_DT_ChungTuChiTiet
		WHERE iID_DTChungTu = @sChungTuId) ctct 
	ON mlns.iID_MLNS = ctct.iID_MLNS and mlns.MaDonVi = ctct.iID_MaDonVi AND (mlns.idSoQuyetDinh = ctct.iID_CTDuToan_Nhan and ctct.iID_CTDuToan_Nhan is not null)

	-- tính dữ liệu đã phân bổ
	SELECT 
		iID_MLNS, sSoQuyetDinh, idSoQuyetDinh,
		SUM(fTonKho) AS fTonKho,
		SUM(fTuChi) AS fTuChi,
		SUM(fHienVat) AS fHienVat,
		SUM(fHangNhap) AS fHangNhap,
		SUM(fHangMua) AS fHangMua,
		SUM(fPhanCap) AS fPhanCap,
		SUM(fDuPhong) AS fDuPhong
	INTO #tblTongRowChiTiet
	FROM (SELECT * FROM #tblRowChiTiet WHERE fTonKho <> 0 or fTuChi <> 0 or fHienVat <> 0 or fHangNhap <> 0 or fHangMua <> 0 or fPhanCap <> 0 or fDuPhong <> 0) ct
	group by iID_MLNS, sSoQuyetDinh, idSoQuyetDinh
	
	SELECT 
		NEWID() AS iID_DTCTChiTiet,
		CAST(0x0 AS uniqueidentIFier) AS iID_DTChungTu,
		npb.iID_MLNS,
		npb.iID_MLNS_Cha,
		npb.sXauNoiMa,
		npb.sLNS,
		npb.sL,
		npb.sK,
		npb.sM,
		npb.sTM,
		npb.sTTM,
		npb.sNG,
		npb.sTNG,
		npb.sTNG1,
		npb.sTNG2,
		npb.sTNG3,
		N'Số chưa phân bổ ' AS sMoTa,
		'1' AS bHangCha,
		'' AS iID_MaDonVi,
		'' AS sTenDonVi,
		--npb.sSoQuyetDinh,
		(CASE WHEN @isQuanly=1 then npb.sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh,
		(CASE WHEN @isQuanly=1 then npb.idSoQuyetDinh ELSE  CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,
		npb.fTonKho - ISNULL(rct.fTonKho, 0) AS fTonKho,
		npb.fTuChi - ISNULL(rct.fTuChi, 0) AS fTuChi,
		npb.fHienVat - ISNULL(rct.fHienVat, 0) AS fHienVat,
		npb.fHangNhap - ISNULL(rct.fHangNhap, 0) AS fHangNhap,
		npb.fHangMua - ISNULL(rct.fHangMua, 0) AS fHangMua,
		npb.fPhanCap - ISNULL(rct.fPhanCap, 0) AS fPhanCap,
		npb.fDuPhong - ISNULL(rct.fDuPhong, 0) AS fDuPhong,
		npb.iID_CTDuToan_Nhan AS iID_CTDuToan_Nhan,
		'' AS sGhiChu,
		2 AS iRowType,
		CAST(1 AS bit) AS bHangChaDuToan,
		CAST(0 AS bit) AS IsEditTuChi,
		CAST(0 AS bit) AS IsEditHienVat,
		CAST(0 AS bit) AS IsEditHangNhap,
		CAST(0 AS bit) AS IsEditHangMua,
		CAST(0 AS bit) AS IsEditPhanCap,
		CAST(0 AS bit) AS IsEditDuPhong
	INTO #tblRowConLai
	FROM #tblRowNhanPhanBo npb
	LEFT JOIN #tblTongRowChiTiet rct
	ON npb.iID_MLNS = rct.iID_MLNS 
	and npb.idSoQuyetDinh = rct.idSoQuyetDinh
		
	WHERE 1 = @isQuanly

	--SELECT * FROM tblMlnsChild; 
	--SELECT * FROM  tblDonViPhanBo; 
	--SELECT * FROM  tblSoQuyetDinhNhan;
	--SELECT * FROM tblMlnsChildAndDvSqd
	--SELECT * FROM tblTongRowChiTieT;
	--SELECT * FROM tblRowNhanPhanBo;

	
	SELECT * FROM #tblRowCha
	
	UNION ALL 
	SELECT * FROM #tblRowNhanPhanBo
	
	UNION ALL 
	SELECT * FROM #tblRowConLai
	UNION ALL 
	SELECT * FROM #tblRowChiTiet 

	ORDER BY sXauNoiMa
	

END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]    Script Date: 17/12/2021 8:08:45 AM ******/
SET ANSI_NULLS ON
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  -- Author:    <Author,,Name>
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  CREATE PROCEDURE [dbo].[sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int
  AS BEGIN 
  INSERT INTO [dbo].[BH_KHC_CheDoBHXH_ChiTiet] (
    iiD_KHC_CheDoBHXHChiTiet, iID_KHC_CheDoBHXH, 
    iID_MucLucNganSach, sLoaiTroCap, 
    iSoDaThucHienNamTruoc, iSoUocThucHienNamTruoc, 
    iSoKeHoachThucHienNamNay, iSoSQ, 
    iSoQNCN, iSoCNVQP, iSoLDHD, iSoHSQBS, 
    fTienDaThucHienNamTruoc, fTienUocThucHienNamTruoc, 
    fTienKeHoachThucHienNamNay, fTienSQ, 
    fTienQNCN, fTienCNVQP, fTienLDHD, 
    fTienHSQBS, dNgaySua, dNgayTao, 
    sNguoiSua, sNguoiTao
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sLoaiTroCap, 
  SUM(iSoDaThucHienNamTruoc), 
  SUM(iSoUocThucHienNamTruoc), 
  sum(iSoKeHoachThucHienNamNay), 
  sum(iSoSQ), 
  sum(iSoQNCN), 
  sum(iSoCNVQP), 
  sum(iSoLDHD), 
  sum(iSoHSQBS), 
  sum(fTienDaThucHienNamTruoc), 
  sum(fTienUocThucHienNamTruoc), 
  sum(fTienKeHoachThucHienNamNay), 
  sum(fTienSQ), 
  sum(fTienQNCN), 
  SUM(fTienCNVQP), 
  sum(fTienLDHD), 
  sum(fTienHSQBS), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_KHC_CheDoBHXH_ChiTiet 
WHERE 
  iID_KHC_CheDoBHXH in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sLoaiTroCap;
--danh dau chung tu da tong hop
update 
  BH_KHC_CheDoBHXH 
set 
  iLoaiTongHop = 2 
where 
  ID in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;



GO
/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_nhap_so_nhu_cau]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_khc_cd_bhxh_nhap_so_nhu_cau] 
	@LoaiTongHop int,
	@NamLamViec int,
	@UserName nvarchar(100)

AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @NamLamViec AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT
		bh.ID,
		bh.sSoQuyetDinh,
		bh.dNgayQuyetDinh,
		bh.sSoChungTu,
		bh.dNgayChungTu,
		bh.iNamChungTu,
		bh.iID_DonVi,
		bh.iID_MaDonVi,
		bh.sMoTa,
		bh.iTongSoDaThucHienNamTruoc,
		bh.iTongSoUocThucHienNamTruoc,
		bh.iTongSoKeHoachThucHienNamNay,
		bh.iTongSoSQ,
		bh.iTongSoQNCN,
		bh.iTongSoCNVQP,
		bh.iTongSoLDHD,
		bh.iTongSoHSQBS,
		bh.fTongTienDaThucHienNamTruoc,
		bh.fTongTienUocThucHienNamTruoc,
		bh.fTongTienKeHoachThucHienNamNay,
		bh.fTongTienSQ,
		bh.fTongTienQNCN,
		bh.fTongTienCNVQP,
		bh.fTongTienLDHD,
		bh.fTongTienHSQBS,
		bh.sTongHop,
		bh.iID_TongHopID,
		bh.iLoaiTongHop,
		bh.bIsKhoa,
		bh.dNgaySua,
		bh.dNgayTao,
		bh.sNguoiSua,
		bh.sNguoiTao,
		bh.sTenDonVi
		into #khcChDoBhHxChungTu
	FROM
		(
			SELECT 
				bh.*, ddv.sTenDonVi 
			FROM 
				BH_KHC_CheDoBHXH bh
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE 
				bh.iNamChungTu = @NamLamViec
				AND bh.iLoaiTongHop = @LoaiTongHop
		) bh;

	IF @CountDonViCha = 0
		SELECT 
			sktct.*
		FROM #khcChDoBhHxChungTu sktct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1) dv
		ON sktct.iID_MaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			sktct.*
		FROM #khcChDoBhHxChungTu sktct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1) dv
		ON sktct.iID_MaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND sktct.bIsKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #khcChDoBhHxChungTu;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
	@KhtBHXHId nvarchar(100),
	@NamLamViec int
AS
BEGIN
SELECT 
		ct.iID_KHT_BHXH as BhKhtBHXHId,
		ct.*
	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.iID_KHT_BHXHChiTiet,
				bhct.iID_KHT_BHXH,
				bhct.iID_LoaiDoiTuong,
				bhct.sTenLoaiDoiTuong,
				bhct.iQSBQNam,
				bhct.fLuongChinh,
				bhct.fPhuCapChucVu,
				bhct.fPCTNNghe,
				bhct.fPCTNVuotKhung,
				bhct.fNghiOm,
				bhct.fTongQuyTienLuongNam,
				bhct.fThuBHXHNguoiLaoDongDong,
				bhct.fThuBHXHNguoiSuDungLaoDongDong, 
				bhct.fTongThuBHXH, 
				bhct.fThuBHYTNguoiLaoDongDong,
				bhct.fThuBHYTNguoiSuDungLaoDongDong,
				bhct.fTongThuBHYT,
				bhct.fThuBHTNNguoiLaoDongDong,
				bhct.fThuBHTNNguoiSuDungLaoDongDong,
				bhct.fTongThuBHTN,
				bhct.fTongCong,
				bhct.iID_MucLucNganSach,
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.sNguoiTao,
				bhct.sNguoiSua
			FROM 
				BH_KHT_BHXH bh
			JOIN 
				BH_KHT_BHXH_ChiTiet bhct ON bh.iID_KHT_BHXH = bhct.iID_KHT_BHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_KHT_BHXH = @KhtBHXHId
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop] 
	@YearOfWork int,
	@daTongHop bit
AS
BEGIN
	SELECT
	KHT.iID_KHT_BHXH,
	KHT.sSoChungTu,
	KHT.iNamChungTu,
	KHT.dNgayChungTu,
	KHT.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHT.sMoTa,
	KHT.bIsKhoa,
	KHT.fTongKeHoach,
	KHT.sSoQuyetDinh,
	KHT.dNgayQuyetDinh,
	KHT.sNguoiTao,
	KHT.sNguoiSua,
	KHT.dNgayTao,
	KHT.dNgaySua,
	KHT.iLoaiTongHop,
	KHT.sTongHop,
	KHT.fThuBHXHNLDDong,
	KHT.fThuBHXHNSDDong,
	KHT.fThuBHXH,
	KHT.fThuBHYTNLDDong,
	KHT.fThuBHYTNSDDong,
	KHT.fTongBHYT,
	KHT.fThuBHTNNLDDong,
	KHT.fThuBHTNNSDDong,
	KHT.fThuBHTN,
	KHT.fTong,
	KHT.bDaTongHop
	FROM BH_KHT_BHXH KHT
	LEFT JOIN DonVi DV
	ON KHT.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHT.iNamChungTu = @YearOfWork
	AND KHT.bDaTongHop = @daTongHop
	ORDER BY KHT.dNgayQuyetDinh DESC
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_KHT_BHXH_ChiTiet (iID_KHT_BHXH, iID_MucLucNganSach, iQSBQNam, fLuongChinh , fPhuCapChucVu, fPCTNNghe, fPCTNVuotKhung, fNghiOm , fThuBHXHNguoiLaoDongDong
	, fThuBHXHNguoiSuDungLaoDongDong , fThuBHYTNguoiLaoDongDong , fThuBHYTNguoiSuDungLaoDongDong , fThuBHTNNguoiLaoDongDong , fThuBHTNNguoiSuDungLaoDongDong,fTongThuBHXH, fTongThuBHYT, fTongThuBHTN, fTongCong
	, dNgayTao, dNgaySua, sNguoiTao)
SELECT @idChungTu,
       iID_MucLucNganSach,
	   iQSBQNam,
       sum(fLuongChinh) ,
       sum(fPhuCapChucVu) ,
	   sum(fPCTNNghe) ,
	   sum(fPCTNVuotKhung) ,
	   sum(fNghiOm),
       sum(fThuBHXHNguoiLaoDongDong) ,
	   sum(fThuBHXHNguoiSuDungLaoDongDong) ,
	   sum(fThuBHYTNguoiLaoDongDong) ,
	   sum(fThuBHYTNguoiSuDungLaoDongDong) ,
	   sum(fThuBHTNNguoiLaoDongDong) ,
	   sum(fThuBHTNNguoiSuDungLaoDongDong) ,
	   sum(fTongThuBHXH) ,
	   Sum(fTongThuBHYT) ,
	   Sum(fTongThuBHTN) ,
	   Sum(fTongCong) ,
       NULL ,
       NULL ,
       NULL 
FROM BH_KHT_BHXH_ChiTiet
WHERE iID_KHT_BHXH in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY iID_MucLucNganSach,
		iQSBQNam;

--danh dau chung tu da tong hop
update BH_KHT_BHXH set bDaTongHop = 1 
where iID_KHT_BHXH in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop));

END
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_rpt_binhquan_donvi]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_rpt_binhquan_donvi] 
	@YearOfWork int,
	@AgencyId nvarchar(max),
	@Period nvarchar(100),
	@TotalMonth int
AS
BEGIN
	SELECT dv.iID_MaDonVi ,
		   sTenDonVi ,
		   MoTa ,
		   fSoThieuUy/@TotalMonth as fSoThieuUy,
		   fSoTrungUy/@TotalMonth as fSoTrungUy,
		   fSoThuongUy/@TotalMonth as fSoThuongUy,
		   fSoDaiUy/@TotalMonth as fSoDaiUy,
		   fSoThieuTa/@TotalMonth as fSoThieuTa,
		   fSoTrungTa/@TotalMonth as fSoTrungTa,
		   fSoThuongTa/@TotalMonth as fSoThuongTa,
		   fSoDaiTa/@TotalMonth as fSoDaiTa,
		   fSoTuong/@TotalMonth as fSoTuong,
		   fSoTSQ/@TotalMonth as fSoTSQ,
		   fSoBinhNhi/@TotalMonth as fSoBinhNhi,
		   fSoBinhNhat/@TotalMonth as fSoBinhNhat,
		   fSoHaSi/@TotalMonth as fSoHaSi,
		   fSoTrungSi/@TotalMonth as fSoTrungSi,
		   fSoThuongSi/@TotalMonth as fSoThuongSi,
		   fSoQNCN/@TotalMonth as fSoQNCN,
		   fSoVCQP/@TotalMonth as fSoVCQP,
		   fSoCNVQP/@TotalMonth as fSoCNVQP,
		   fSoLDHD/@TotalMonth as fSoLDHD
	FROM
	  (SELECT iID_MaDonVi ,
			  fSoThieuUy = sum(fSoThieuUy) ,
			  fSoTrungUy = sum(fSoTrungUy) ,
			  fSoThuongUy = sum(fSoThuongUy) ,
			  fSoDaiUy = sum(fSoDaiUy) ,
			  fSoThieuTa = sum(fSoThieuTa) ,
			  fSoTrungTa = sum(fSoTrungTa) ,
			  fSoThuongTa = sum(fSoThuongTa) ,
			  fSoDaiTa = sum(fSoDaiTa) ,
			  fSoTuong = sum(fSoTuong) ,
			  fSoTSQ = sum(fSoTSQ) ,
			  fSoBinhNhi = sum(fSoBinhNhi) ,
			  fSoBinhNhat = sum(fSoBinhNhat) ,
			  fSoHaSi = sum(fSoHaSi) ,
			  fSoTrungSi = sum(fSoTrungSi) ,
			  fSoThuongSi = sum(fSoThuongSi) ,
			  fSoQNCN = sum(fSoQNCN) ,
			  fSoVCQP = sum(fSoVCQP) ,
			  fSoCNVQP = sum(fSoCNVQP) ,
			  fSoLDHD = sum(fSoLDHD)
	   FROM NS_QS_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))
		 AND (@Period IS NULL
			  OR iThangQuy in
				(SELECT *
				 FROM f_split(@Period)))
		 AND sKyHieu = '700'
	   GROUP BY iID_MaDonVi)ctct -- lay ten don vi
	RIGHT JOIN
	  (SELECT iID_MaDonVi,
			  sTenDonVi,
			  MoTa = iID_MaDonVi + ' - '+ sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork
		 AND iLoai=1
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId))) ) AS dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY ctct.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_dt_rpt_lns_thang]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_dt_rpt_lns_thang]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthType int
AS
BEGIN

	SELECT DISTINCT sLNS AS LNS,
		LEFT(sLNS, 1) AS LNS1,
		LEFT(sLNS, 3) AS LNS3
	FROM (
	SELECT sLNS FROM NS_QT_ChungTuChiTiet
	WHERE iNamLamViec = @YearOfWork
      AND iNamNganSach = @YearOfBudget
	  AND iID_MaNguonNganSach = @BudgetSource
	  AND iID_MaDonVi in (SELECT * FROM f_split(@AgencyId))
	  AND (@QuarterMonth IS NULL
		   OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
	  AND (@QuarterMonthType IS NULL
		   OR iThangQuyLoai = @QuarterMonthType)
	  AND (fTuChi_DeNghi <> 0
		   OR fTuChi_PheDuyet <> 0)
	 UNION ALL
	 SELECT DISTINCT sLNS
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iPhanCap = 1		
		 AND (@AgencyId IS NULL
			  OR iID_MaDonVi in
				(SELECT *
				 FROM f_split(@AgencyId)))		
	 ) temp
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int
AS
BEGIN
declare @DataDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSDT nvarchar(50), iSoDaThucHienNamTruocDT int, iSoUocThucHienNamTruocDT int, iSoKeHoachThucHienNamNayDT int, iSoSQDT int, iSoQNCNDT float,iSoCNVQPDT int,iSoLDHDDT int,  iSoHSQBSDT int,
fTienDaThucHienNamTruocDT float,fTienUocThucHienNamTruocDT float, fTienKeHoachThucHienNamNayDT float,fTienSQDT float,fTienQNCNDT float, fTienCNVQPDT float,fTienLDHDDT float,fTienHSQBSDT float
);
declare @DataHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSHT nvarchar(50), iSoDaThucHienNamTruocHT int, iSoUocThucHienNamTruocHT int, iSoKeHoachThucHienNamNayHT int, iSoSQHT int, iSoQNCNHT float,iSoCNVQPHT int,iSoLDHDHT int,  iSoHSQBSHT int,
fTienDaThucHienNamTruocHT float,fTienUocThucHienNamTruocHT float, fTienKeHoachThucHienNamNayHT float,fTienSQHT float,fTienQNCNHT float, fTienCNVQPHT float,fTienLDHDHT float,fTienHSQBSHT float
);

INSERT INTO @DataDuToan (idDonVi,sTenDonVI , sM,sLNSDT , iSoDaThucHienNamTruocDT , iSoUocThucHienNamTruocDT , iSoKeHoachThucHienNamNayDT , iSoSQDT , iSoQNCNDT ,iSoCNVQPDT ,iSoLDHDDT ,  iSoHSQBSDT ,
fTienDaThucHienNamTruocDT ,fTienUocThucHienNamTruocDT , fTienKeHoachThucHienNamNayDT ,fTienSQDT ,fTienQNCNDT , fTienCNVQPDT ,fTienLDHDDT ,fTienHSQBSDT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,
	   SoDaThucHienNamTruoc=SUM(IsNull(A.iSoDaThucHienNamTruoc,0)),
	   SoUocThucHienNamTruoc=SUM(IsNull(A.iSoUocThucHienNamTruoc,0)),
	   SoKeHoachThucHienNamNay=SUM(IsNull(A.iSoKeHoachThucHienNamNay,0)),
	   SoSQ=SUM(IsNull(A.iSoSQ,0)),
	   SoQNCN=SUM(IsNull(A.iSoQNCN,0)),
	   SoCNVQP=SUM(IsNull(A.iSoCNVQP,0)),
	   SoLDHD=SUM(IsNull(A.iSoLDHD,0)),
	   SoHSQBS=SUM(IsNull(A.iSoHSQBS,0)),

	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0)),
	   TienSQ=SUM(IsNull(A.fTienSQ,0)),
	   TienQNCN=SUM(IsNull(A.fTienQNCN,0)),
	   TienCNVQP=SUM(IsNull(A.fTienCNVQP,0)),
	   TienLDHD=SUM(IsNull(A.fTienLDHD,0)),
	   TienHSQBS=SUM(IsNull(A.fTienHSQBS,0))

FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,
                ctct.iSoDaThucHienNamTruoc,
				ctct.iSoUocThucHienNamTruoc,
				ctct.iSoKeHoachThucHienNamNay,
				ctct.iSoSQ,
				ctct.iSoQNCN,
				ctct.iSoCNVQP,
				ctct.iSoLDHD,
				ctct.iSoHSQBS,
				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay,
				ctct.fTienSQ,
				ctct.fTienQNCN,
				ctct.fTienCNVQP,
				ctct.fTienLDHD,
				ctct.fTienHSQBS
   FROM BH_KHC_CheDoBHXH_ChiTiet ctct
   LEFT JOIN BH_KHC_CheDoBHXH ct ON ct.ID = ctct.iID_KHC_CheDoBHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = 9010001
   WHERE ct.iNamChungTu = @namLamViec--@namLamViec
	 AND ct.iLoaiTongHop = 2) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

INSERT INTO @DataHachToan (idDonVi,sTenDonVI , sM,sLNSHT , iSoDaThucHienNamTruocHT , iSoUocThucHienNamTruocHT , iSoKeHoachThucHienNamNayHT , iSoSQHT , iSoQNCNHT ,iSoCNVQPHT ,iSoLDHDHT ,  iSoHSQBSHT ,
fTienDaThucHienNamTruocHT ,fTienUocThucHienNamTruocHT , fTienKeHoachThucHienNamNayHT ,fTienSQHT ,fTienQNCNHT , fTienCNVQPHT ,fTienLDHDHT ,fTienHSQBSHT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,
	   SoDaThucHienNamTruoc=SUM(IsNull(A.iSoDaThucHienNamTruoc,0)),
	   SoUocThucHienNamTruoc=SUM(IsNull(A.iSoUocThucHienNamTruoc,0)),
	   SoKeHoachThucHienNamNay=SUM(IsNull(A.iSoKeHoachThucHienNamNay,0)),
	   SoSQ=SUM(IsNull(A.iSoSQ,0)),
	   SoQNCN=SUM(IsNull(A.iSoQNCN,0)),
	   SoCNVQP=SUM(IsNull(A.iSoCNVQP,0)),
	   SoLDHD=SUM(IsNull(A.iSoLDHD,0)),
	   SoHSQBS=SUM(IsNull(A.iSoHSQBS,0)),
	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0)),
	   TienSQ=SUM(IsNull(A.fTienSQ,0)),
	   TienQNCN=SUM(IsNull(A.fTienQNCN,0)),
	   TienCNVQP=SUM(IsNull(A.fTienCNVQP,0)),
	   TienLDHD=SUM(IsNull(A.fTienLDHD,0)),
	   TienHSQBS=SUM(IsNull(A.fTienHSQBS,0))
FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,
                ctct.iSoDaThucHienNamTruoc,
				ctct.iSoUocThucHienNamTruoc,
				ctct.iSoKeHoachThucHienNamNay,
				ctct.iSoSQ,
				ctct.iSoQNCN,
				ctct.iSoCNVQP,
				ctct.iSoLDHD,
				ctct.iSoHSQBS,
				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay,
				ctct.fTienSQ,
				ctct.fTienQNCN,
				ctct.fTienCNVQP,
				ctct.fTienLDHD,
				ctct.fTienHSQBS
   FROM BH_KHC_CheDoBHXH_ChiTiet ctct
   LEFT JOIN BH_KHC_CheDoBHXH ct ON ct.ID = ctct.iID_KHC_CheDoBHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec =@namLamViec --@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = 9010002
   WHERE ct.iNamChungTu =@namLamViec --@namLamViec
	 AND ct.iLoaiTongHop = 2) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.sM,
dt.sLNSDT,
ht.sLNSHT,
IsNull(dt.iSoDaThucHienNamTruocDT, 0) SoDaThucHienNamTruocDT, 
IsNull(ht.iSoDaThucHienNamTruocHT, 0) SoDaThucHienNamTruocHT, 

IsNull(dt.iSoUocThucHienNamTruocDT, 0) SoUocThucHienNamTruocDT, 
IsNull(ht.iSoUocThucHienNamTruocHT, 0) SoUocThucHienNamTruocHT,

IsNull(dt.iSoKeHoachThucHienNamNayDT, 0) SoKeHoachThucHienNamNayDT,
IsNull(ht.iSoKeHoachThucHienNamNayHT, 0) SoKeHoachThucHienNamNayHT,

IsNull(dt.iSoSQDT, 0) SoSQDT, 
IsNull(ht.iSoSQHT, 0) SoSQHT,

IsNull(dt.iSoQNCNDT, 0) SoQNCNDT, 
IsNull(ht.iSoQNCNHT, 0) SoQNCNHT,

IsNull(dt.iSoCNVQPDT, 0) SoCNVQPDT,
IsNull(ht.iSoCNVQPHT, 0) SoCNVQPHT,

IsNull(dt.iSoLDHDDT, 0) SoLDHDDT,
IsNull(ht.iSoLDHDHT, 0) SoLDHDHT,

IsNull(dt.iSoHSQBSDT, 0) SoHSQBSDT,
IsNull(ht.iSoHSQBSHT, 0) SoCNVQPDT,

IsNull(dt.fTienDaThucHienNamTruocDT, 0) TienDaThucHienNamTruocDT,
IsNull(ht.fTienDaThucHienNamTruocHT, 0) TienDaThucHienNamTruocHT,

IsNull(dt.fTienUocThucHienNamTruocDT, 0) TienUocThucHienNamTruocDT,
IsNull(ht.fTienUocThucHienNamTruocHT, 0) TienUocThucHienNamTruocHT,

IsNull(dt.fTienKeHoachThucHienNamNayDT, 0) TienKeHoachThucHienNamNayDT,
IsNull(ht.fTienKeHoachThucHienNamNayHT, 0) TienKeHoachThucHienNamNayHT,

IsNull(dt.fTienSQDT, 0) TienSQDT,
IsNull(ht.fTienSQHT, 0) TienSQHT,

IsNull(dt.fTienQNCNDT, 0) TienQNCNDT,
IsNull(ht.fTienQNCNHT, 0) TienQNCNHT,

IsNull(dt.fTienCNVQPDT, 0) TienCNVQPDT,
IsNull(ht.fTienCNVQPHT, 0) TienCNVQPHT,

IsNull(dt.fTienLDHDDT, 0) TienLDHDDT,
IsNull(ht.fTienLDHDHT, 0) TienLDHDHT,

IsNull(dt.fTienHSQBSDT, 0) TienHSQBSDT,
IsNull(ht.fTienHSQBSHT, 0) TienHSQBSHT

FROM @DataDuToan dt
LEFT JOIN @DataHachToan ht ON dt.idDonVi = ht.idDonVi
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh] 
	@namLamViec int,
	@loaiChungTu int,
	@daTongHop bit,
	@lstSelectedUnit ntext,
	@khoiDuToan nvarchar(50),
	@khoiHachToan nvarchar(50)
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
                ct.iID_MaDonVi,
                IsNull(ctct.fThuBHXHNguoiLaoDongDong, 0) ThuBHXHNLDDong,
                IsNull(ctct.fThuBHXHNguoiSuDungLaoDongDong, 0) ThuBHXHNSDDong,
				IsNull(ctct.fThuBHTNNguoiLaoDongDong, 0) ThuBHTNNLDDong,
                IsNull(ctct.fThuBHTNNguoiSuDungLaoDongDong, 0) ThuBHTNNSDDong
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiDuToan
   WHERE ct.iNamChungTu = @namLamViec
	 AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
   JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec
   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id;

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
                ct.iID_MaDonVi,
                IsNull(ctct.fThuBHXHNguoiLaoDongDong, 0) ThuBHXHNLDDong,
                IsNull(ctct.fThuBHXHNguoiSuDungLaoDongDong, 0) ThuBHXHNSDDong,
				IsNull(ctct.fThuBHTNNguoiLaoDongDong, 0) ThuBHTNNLDDong,
                IsNull(ctct.fThuBHTNNguoiSuDungLaoDongDong, 0) ThuBHTNNSDDong
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiHachToan
   WHERE ct.iNamChungTu = @namLamViec
	 AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
   JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec
   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
IsNull(dt.BhxhNldDongDuToan, 0) BhxhNldDongDuToan, 
IsNull(dt.BhxhNsddDongDuToan, 0) BhxhNsddDongDuToan, 
IsNull(ht.BhxhNldDongHachToan, 0) BhxhNldDongHachToan, 
IsNull(ht.BhxhNsddDongHachToan, 0) BhxhNsddDongHachToan,
IsNull(dt.BHXHTongCongDuToan, 0) BHXHTongCongDuToan,
IsNull(ht.BHXHTongCongHachToan, 0) BHXHTongCongHachToan,
IsNull(dt.BhtnNldDongDuToan, 0) BhtnNldDongDuToan, 
IsNull(dt.BhtnNsddDongDuToan, 0) BhtnNsddDongDuToan,
IsNull(ht.BhtnNldDongHachToan, 0) BhtnNldDongHachToan, 
IsNull(ht.BhtnNsddDongHachToan, 0) BhtnNsddDongHachToan,
IsNull(dt.BHTNTongCongDuToan, 0) BHTNTongCongDuToan,
IsNull(ht.BHTNTongCongHachToan, 0) BHTNTongCongHachToan
FROM @DataDuToan dt
LEFT JOIN @DataHachToan ht ON dt.idDonVi = ht.idDonVi;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt] 
	@namLamViec int,
	@loaiChungTu int,
	@daTongHop bit,
	@lstSelectedUnit ntext,
	@khoiDuToan nvarchar(50),
	@khoiHachToan nvarchar(50),
	@sm nvarchar(50)
AS
BEGIN
declare @BhytDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50),BhytNSDDongDuToan float, BhytNLDDongDuToan float, tongBhytDuToan float);
declare @BhytHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50), BhytNSDDongHachToan float,BhytNLDDongHachToan float, tongBhytHachToan float);

INSERT INTO @BhytDuToan (sTenDonVI, idDonVi, sm, BhytNSDDongDuToan, BhytNLDDongDuToan, tongBhytDuToan)
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
           ct.iID_MaDonVi,
		   IsNull(ctct.fThuBHYTNguoiSuDungLaoDongDong, 0) ThuBHYTNSDDongDuToan,
		   IsNull(ctct.fThuBHYTNguoiLaoDongDong, 0) ThuBHYTNLDDongDuToan,
		   IsNull(ctct.fTongThuBHYT, 0) TongBhytDuToan
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiDuToan
   AND ml.sM = @sm
   WHERE ct.iNamChungTu = @namLamViec
	 AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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

INSERT INTO @BhytHachToan (idDonVi, sTenDonVI, sm, BhytNSDDongHachToan, BhytNLDDongHachToan, tongBhytHachToan)
	SELECT
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sm,
	   SUM(IsNull(A.ThuBHYTNSDDongHachToan, 0)) BhytNSDDongHachToan,
	   SUM(IsNull(A.ThuBHYTNLDDongHachToan, 0)) BhytNLDDongHachToan,
	   SUM(IsNull(A.TongBhytHachToan, 0)) TongBhytHachToan

FROM
  (SELECT ml.sm,
           ml.sMoTa,
           ct.iID_MaDonVi,
		   IsNull(ctct.fThuBHYTNguoiSuDungLaoDongDong, 0) ThuBHYTNSDDongHachToan,
		   IsNull(ctct.fThuBHYTNguoiLaoDongDong, 0) ThuBHYTNLDDongHachToan,
		   IsNull(ctct.fTongThuBHYT, 0) TongBhytHachToan
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiHachToan
   AND ml.sM = @sm
   WHERE ct.iNamChungTu = @namLamViec
	 AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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

SELECT dt.idDonVi, 
dt.sTenDonVI,

IsNull(dt.BhytNLDDongDuToan, 0) BhytNldDongDuToan, 
IsNull(dt.BhytNSDDongDuToan, 0) BhytNsddDongDuToan, 
IsNull(ht.BhytNLDDongHachToan, 0) BhytNldDongHachToan, 
IsNull(ht.BhytNSDDongHachToan, 0) BhytNsddDongHachToan,
IsNull(dt.tongBhytDuToan, 0) BHYTTongCongDuToan,
IsNull(ht.tongBhytHachToan, 0) BHYTTongCongHachToan

FROM @BhytDuToan dt
LEFT JOIN @BhytHachToan ht ON dt.idDonVi = ht.idDonVi;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_baocaotinhhinhduan]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_baocaotinhhinhduan]
@IdDuAn nvarchar(500),
@NgayDeNghi datetime
AS
BEGIN      
	SELECT  tbl.iID_HopDongId ,
		tbl.iID_NhaThauId,
		SUM(CASE WHEN iLoaiThanhToan = 1 THEN (ISNULL(dt.fGiaTriThanhToanTN, 0) + ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE 0 END) as SoThanhToan,
		SUM(CASE WHEN iLoaiThanhToan = 0 THEN (ISNULL(dt.fGiaTriThanhToanTN, 0) + ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE 0 END) as SoTamUng,
		SUM(ISNULL(dt.fGiaTriThuHoiNamNayNN, 0)+ ISNULL(dt.fGiaTriThuHoiNamNayTN, 0) + ISNULL(dt.fGiaTriThuHoiNamTruocNN, 0) + ISNULL(fGiaTriThuHoiNamTruocTN, 0) 
			+ ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocNN, 0) 
			+ ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN, 0)) as SoThuHoiTamUng,
			--Bổ sung tính số đã cấp ứng 04/07/2023--
		SUM(CASE WHEN iLoaiKeHoachVon = 2 THEN (ISNULL(dt.fGiaTriThanhToanTN, 0) + ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE 0 END) as SoDaCapUng

			INTO #tmp
	from VDT_TT_DeNghiThanhToan tbl
	inner join VDT_TT_PheDuyetThanhToan_ChiTiet dt on tbl.Id = dt.iID_DeNghiThanhToanID
	and tbl.iID_DuAnId = @IdDuAn
	and CAST(tbl.dNgayPheDuyet as DATE) <= CAST(@NgayDeNghi as DATE)
	group by  tbl.iID_HopDongId, tbl.iID_NhaThauId
	
	SELECT 
		CAST(0 as bit) as IsHangCha,
		NULL as NguonVonId,
		NULL as Loai,
		NULL as Mlns,
		nt.sTenNhaThau as TenNhaThau,
		hd.sSoHopDong as SoHopDong,
		hd.iThoiGianThucHien as ThoiGianThucHien,
		hd.fTienHopDong as TienHopDong,
		NULL as SoDeNghi,
		NULL as IdDeNghiThanhToan,
		NULL as NgayThanhToan,
		(ISNULL(tmp.SoThanhToan, 0) + ISNULL(tmp.SoTamUng, 0) - ISNULL(tmp.SoThuHoiTamUng, 0)) as TongCongGiaiNgan,
		NULL as NgayCapUng,
		--NULL as SoDaCapUng,
		tmp.*
	FROM #tmp as tmp
	LEFT JOIN VDT_DM_NhaThau as nt on tmp.iID_NhaThauId = nt.Id
	LEFT JOIN VDT_DA_TT_HopDong as hd on tmp.iID_HopDongId = hd.Id
	DROP TABLE #tmp
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_duan_nguonvon_by_duan]    Script Date: 06/07/2023 6:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_get_all_duan_nguonvon_by_duan]
	@duAnId nvarchar(500)
AS
BEGIN

	create table tmpNguonVon(nvId int)

	insert into tmpNguonVon(nvId)
		select iID_NguonVonID 
			from VDT_KHV_KeHoach5Nam as tbl
			INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as dt on tbl.iID_KeHoach5NamID = dt.iID_KeHoach5NamID
			where iID_DuAnID = @duAnId AND tbl.bActive = 1

	SELECT distinct null as Id,
	null as IdChuTruongNguonVon,
	null as IIdChuTruongDauTuId,
	nv.nvId as IIdNguonVonId,
	ns.sTen as TenNguonVon,
	null as FGiaTriDieuChinh,
	null as GiaTriTruocDieuChinh,
	(select SUM(dt.fHanMucDauTu) from VDT_KHV_KeHoach5Nam as tbl
			INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as dt on tbl.iID_KeHoach5NamID = dt.iID_KeHoach5NamID
			where iID_DuAnID = @duAnId AND tbl.bActive = 1
			and dt.iID_NguonVonID = nv.nvId) as FTienPheDuyet
	FROM tmpNguonVon nv
	INNER JOIN NguonNganSach ns ON nv.nvId = ns.iID_MaNguonNganSach
	--where danv.iID_DuAn = @duAnId

	drop table tmpNguonVon
	
END;
;
;
;
GO
