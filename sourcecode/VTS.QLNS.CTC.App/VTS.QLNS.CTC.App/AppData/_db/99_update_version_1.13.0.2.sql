/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]    Script Date: 18/07/2023 5:42:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_tonghopnguonnsdautu_tang_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khv_kehoach_von_nam_duoc_duyet_export]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_khv_kehoach_von_nam_duoc_duyet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_khv_kehoach_von_nam_duoc_duyet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet_clone]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovondonvichitietpheduyet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_kt_khoitaodulieuchitietthanhtoan_byktdl_id]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kt_khoitaodulieuchitietthanhtoan_byktdl_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kt_khoitaodulieuchitietthanhtoan_byktdl_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chi_tiet]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kinhphi_quanly_chi_tiet]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_kinhphi_quanly_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_kinhphi_quanly_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ke_hoach_thu_mua_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikinh_phiquan_ly_index]    Script Date: 18/07/2023 5:39:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_kehoach_chikinh_phiquan_ly_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_kehoach_chikinh_phiquan_ly_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikinh_phiquan_ly_index]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi các chế dộ BHXH

-- =============================================
CREATE PROCEDURE [dbo].[bh_kehoach_chikinh_phiquan_ly_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 kpql.iID_BH_KHC_KinhPhiQuanLy 
		, kpql.sSoChungTu
		, kpql.dNgayChungTu
		, kpql.sSoQuyetDinh
		, kpql.dNgayQuyetDinh
		, kpql.iNamChungTu
		, kpql.iID_DonVi
		, kpql.iID_MaDonVi
		, kpql.sMoTa
		, kpql.fTongTienDaThucHienNamTruoc
		, kpql.fTongTienUocThucHienNamTruoc
		, kpql.fTongTienKeHoachThucHienNamNay

		, kpql.fTongTienCanBo
		, kpql.fTongTienQuanLuc
		, kpql.fTongTienTaiChinh
		, kpql.fTongTienQuanY

		, kpql.sTongHop
		, kpql.iID_TongHopID
		, kpql.iLoaiTongHop
		, kpql.bIsKhoa

		, kpql.dNgaySua
		, kpql.dNgayTao
		, kpql.sNguoiSua
		, kpql.sNguoiTao
		, kpql.dNgayTao
		, donVi.sTenDonVi
		-- Tong dự toán todo
	FROM BH_KHC_KinhPhiQuanLy kpql
	LEFT JOIN DonVi donVi
		ON kpql.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = kpql.iID_DonVi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 18/07/2023 5:39:17 PM ******/
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
	@UserName NVARCHAR(100),
	@IsGetAll bit
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
	and (@IsGetAll = 1 OR NOT EXISTS(SELECT * FROM #tblLns) OR ((EXISTS(SELECT * FROM #tblLns) AND iID_MLNS in (SELECT * FROM #tblIdMlns))))

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

;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh]
	@ChungTuId nvarchar(max),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@VoucherIndex int,
	@IsLuyKe bit
AS
BEGIN
	DECLARE @tblDuToan table (iID_MLNS uniqueidentifier, fTuChi float, fHienVat float, fDuPhong float, fHangMua float, fHangNhap float, fPhanCap float, fTonKho float)
	-- số liệu dự toán
	IF @IsLuyKe = 0
		INSERT INTO @tblDuToan
		SELECT 
			iID_MLNS,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho
		FROM (
			-- nhận dự toán
			SELECT iID_MLNS,
				SUM(fTuChi) AS fTuChi, 
				SUM(fHienVat) AS fHienVat,
				SUM(fDuPhong) AS fDuPhong,
				SUM(fHangMua) AS fHangMua,
				SUM(fHangNhap) AS fHangNhap,
				SUM(fPhanCap) AS fPhanCap,
				SUM(fTonKho) AS fTonKho
			FROM NS_DT_ChungTuChiTiet
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND iID_DTChungTu in
					(SELECT DISTINCT iID_CTDuToan_Nhan
					FROM NS_DT_Nhan_PhanBo_Map
					WHERE iID_CTDuToan_PhanBo in (select * from f_split(@ChungTuId)))
				AND iDuLieuNhan = 0 
			group by iID_MLNS
			--UNION  
			---- phân bổ
			--SELECT 
			--	ctct.iID_MLNS, 
			--	0 - SUM(fTuChi) AS fTuChi, 
			--	0 - SUM(fHienVat) AS fHienVat,
			--	0 - SUM(fDuPhong) AS fDuPhong,
			--	0 - SUM(fHangMua) AS fHangMua,
			--	0 - SUM(fHangNhap) AS fHangNhap,
			--	0 - SUM(fPhanCap) AS fPhanCap,
			--	0 - SUM(fTonKho) AS fTonKho
			--FROM NS_DT_ChungTuChiTiet ctct
			--WHERE iID_DTChungTu IN 
			--	(SELECT iID_DTChungTu
			--	FROM NS_DT_ChungTu
			--	WHERE iNamLamViec = @YearOfWork
			--		and iNamNganSach = @YearOfBudget
			--		and iID_MaNguonNganSach = @BudgetSource
			--		and iLoai = 1
			--		AND 
			--		(
			--			(dNgayQuyetDinh IS NOT NULL AND (CAST(dNgayQuyetDinh AS DATE) <= cast(@VoucherDate AS DATE) AND iSoChungTuIndex < @VoucherIndex))
			--			OR 
			--			(dNgayQuyetDinh IS NULL AND (CAST(dNgayChungTu AS DATE) <= cast(@VoucherDate AS DATE) AND iSoChungTuIndex < @VoucherIndex))
			--		)
			--	)
			--GROUP BY ctct.iID_MLNS
		) dt
		GROUP BY iID_MLNS
	ELSE
		INSERT INTO @tblDuToan
		SELECT iID_MLNS,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho
		FROM NS_DT_ChungTuChiTiet
		WHERE 
			iID_DTChungTu IN 
			(
				SELECT iID_DTChungTu FROM NS_DT_ChungTu
				WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND iLoai = 0
				AND ((dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@VoucherDate AS DATE))
					OR
					(dNgayQuyetDinh IS NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)))
			)
			AND iDuLieuNhan = 0 
		GROUP BY iID_MLNS

	SELECT 
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
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
	   mlns.sChiTietToi,
	   mlns.bHangChaDuToan
	FROM NS_MucLucNganSach mlns
	LEFT JOIN @tblDuToan ctct 
		ON mlns.iID_MLNS = ctct.iID_MLNS
	WHERE mlns.iNamLamViec = @YearOfWork
	  AND bHangChaDuToan is not null
	  AND mlns.sLNS in
		(SELECT *
		 FROM dbo.f_split(@LNS))
	  AND (isnull(ctct.fTuChi, 0) > 0
		   OR isnull(ctct.fHienVat, 0) > 0
		   OR isnull(ctct.fPhanCap, 0) > 0
		   OR isnull(ctct.fHangNhap, 0) > 0
		   OR isnull(ctct.fHangMua, 0) > 0)
	ORDER BY mlns.sXauNoiMa;
END
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 08/12/2021 2:57:30 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	KHTM.iID_KHTM_BHYT,
	KHTM.sSoChungTu,
	KHTM.iNamChungTu,
	KHTM.dNgayChungTu,
	KHTM.iID_MaDonVi IIDMaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHTM.sMoTa,
	KHTM.bKhoa,
	KHTM.fTongKeHoach,
	KHTM.sSoQuyetDinh,
	KHTM.dNgayQuyetDinh,
	KHTM.sNguoiTao,
	KHTM.sNguoiSua,
	KHTM.dNgayTao,
	KHTM.dNgaySua,
	KHTM.iLoaiTongHop,
	KHTM.sTongHop,
	KHTM.iTongSoNguoi,
	KHTM.iTongSoThang,
	KHTM.fTongDinhMuc,
	KHTM.fTongThanhTien,
	KHTM.bDaTongHop
	FROM BH_KHTM_BHYT KHTM
	LEFT JOIN DonVi DV
	ON KHTM.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	ORDER BY KHTM.dNgayQuyetDinh DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kinhphi_quanly_chi_tiet]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_khc_kinhphi_quanly_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@iID_KHC_KinhPhiQuanLy uniqueidentifier
AS
BEGIN
SELECT 
		ct.iID_BH_KHC_KinhPhiQuanLy_ChiTiet ,
		ct.iID_KHC_KinhPhiQuanLy ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sMoTa,
		ct.sM,
		ct.sTM,
		ct.sNoiDung,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienUocThucHienNamTruoc,
		ct.fTienKeHoachThucHienNamNay,
		ct.fTienCanBo,
		ct.fTienQuanLuc,
		ct.fTienTaiChinh,
		ct.fTienQuanY,
		ct.sGhiChu,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sTenDonVi

	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_KHC_KinhPhiQuanLy bh
			JOIN 
				BH_KHC_KinhPhiQuanLy_ChiTiet bhct ON bh.iID_BH_KHC_KinhPhiQuanLy = bhct.iID_KHC_KinhPhiQuanLy 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		where ct.iID_KHC_KinhPhiQuanLy=@iID_KHC_KinhPhiQuanLy
		;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  -- Author:    <Author,,Name>
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  Create PROCEDURE [dbo].[sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int
  AS BEGIN 
  INSERT INTO [dbo].[BH_KHC_KinhPhiQuanLy_ChiTiet] (
    iID_BH_KHC_KinhPhiQuanLy_ChiTiet, iID_KHC_KinhPhiQuanLy, 
    iID_MucLucNganSach, sM, 
    sTM, sNoiDung, 
    fTienDaThucHienNamTruoc, fTienUocThucHienNamTruoc, 
    fTienKeHoachThucHienNamNay, fTienCanBo, 
    fTienQuanLuc, fTienTaiChinh, fTienQuanY, dNgaySua, dNgayTao, 
    sNguoiSua, sNguoiTao
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sM,
  sTM,
  sNoiDung, 
  sum(fTienDaThucHienNamTruoc), 
  sum(fTienUocThucHienNamTruoc), 
  sum(fTienKeHoachThucHienNamNay), 
  sum(fTienCanBo), 
  sum(fTienQuanLuc), 
  SUM(fTienTaiChinh), 
  sum(fTienQuanY), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_KHC_KinhPhiQuanLy_ChiTiet 
WHERE 
  iID_KHC_KinhPhiQuanLy in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sM,
  sTM,
  sNoiDung;
--danh dau chung tu da tong hop
update 
  BH_KHC_KinhPhiQuanLy 
set 
  iLoaiTongHop = 2 
where 
  iID_BH_KHC_KinhPhiQuanLy in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;



GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 18/07/2023 5:39:17 PM ******/
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
				bhct.fTongQTLN,
				bhct.fThu_BHXH_NLD,
				bhct.fThu_BHXH_NSD, 
				bhct.fTongThuBHXH, 
				bhct.fThu_BHYT_NLD,
				bhct.fThu_BHYT_NSD,
				bhct.fTongThuBHYT,
				bhct.fThu_BHTN_NLD,
				bhct.fThu_BHTN_NSD,
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
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]    Script Date: 18/07/2023 5:39:17 PM ******/
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
	KHT.fBHXH_NLD FThuBHXHNLDDong,
	KHT.fBHXH_NSD FThuBHXHNSDDong,
	KHT.fTongBHXH FThuBHXH,
	KHT.fBHYT_NLD FThuBHYTNLDDong,
	KHT.fBHYT_NSD FThuBHYTNSDDong,
	KHT.fTongBHYT FTongBHYT,
	KHT.fBHTN_NLD FThuBHTNNLDDong,
	KHT.fBHTN_NSD FThuBHTNNSDDong,
	KHT.fTongBHTN FThuBHTN,
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
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 18/07/2023 5:39:17 PM ******/
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
	INSERT INTO BH_KHT_BHXH_ChiTiet (iID_KHT_BHXH, iID_MucLucNganSach, iQSBQNam, fLuongChinh , fPhuCapChucVu, fPCTNNghe, fPCTNVuotKhung, fNghiOm , fThu_BHXH_NLD
	, fThu_BHXH_NSD , fThu_BHYT_NLD , fThu_BHYT_NSD , fThu_BHTN_NLD , fThu_BHTN_NSD,fTongThuBHXH, fTongThuBHYT, fTongThuBHTN, fTongCong
	, dNgayTao, dNgaySua, sNguoiTao)
SELECT @idChungTu,
       iID_MucLucNganSach,
	   iQSBQNam,
       sum(fLuongChinh) ,
       sum(fPhuCapChucVu) ,
	   sum(fPCTNNghe) ,
	   sum(fPCTNVuotKhung) ,
	   sum(fNghiOm),
       sum(fThu_BHXH_NLD) ,
	   sum(fThu_BHXH_NSD) ,
	   sum(fThu_BHYT_NLD) ,
	   sum(fThu_BHYT_NSD) ,
	   sum(fThu_BHTN_NLD) ,
	   sum(fThu_BHTN_NSD) ,
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
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chi_tiet]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chi_tiet]
	@KhtmBHYTId nvarchar(100),
	@NamLamViec int
AS
BEGIN
	SELECT 
		ct.iID_KHTM_BHYT IdKhtmBHYT,
		ct.*
	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				ddv.iID_DonVi,
				bh.sMoTa SMoTa,
				bhct.iID_KHTM_BHYT_ChiTiet,
				bhct.iID_KHTM_BHYT,
				bhct.iID_NoiDung,
				bhct.sTenNoiDung,
				bhct.iSoNguoi,
				bhct.iSoThang,
				bhct.fDinhMuc,
				bhct.fThanhTien,
				bhct.sGhiChu,
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.sNguoiTao,
				bhct.sNguoiSua,
				bhct.sTenDonVi
			FROM 
				BH_KHTM_BHYT bh
			JOIN 
				BH_KHTM_BHYT_ChiTiet bhct ON bh.iID_KHTM_BHYT = bhct.iID_KHTM_BHYT 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_KHTM_BHYT = @khtmBHYTId
		) ct;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]
	@YearOfWork int,
	@daTongHop bit
AS
BEGIN
	SELECT
	KHTM.iID_KHTM_BHYT,
	KHTM.sSoChungTu,
	KHTM.iNamChungTu,
	KHTM.dNgayChungTu,
	KHTM.iID_MaDonVi AS IIDMaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHTM.sMoTa,
	KHTM.bKhoa,
	KHTM.fTongKeHoach,
	KHTM.sSoQuyetDinh,
	KHTM.dNgayQuyetDinh,
	KHTM.sNguoiTao,
	KHTM.sNguoiSua,
	KHTM.dNgayTao,
	KHTM.dNgaySua,
	KHTM.iLoaiTongHop,
	KHTM.sTongHop,
	KHTM.iTongSoNguoi,
	KHTM.iTongSoThang,
	KHTM.fTongDinhMuc,
	KHTM.fTongThanhTien,
	KHTM.bDaTongHop
	FROM BH_KHTM_BHYT KHTM
	LEFT JOIN DonVi DV
	ON KHTM.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHTM.iNamChungTu = @YearOfWork
	AND KHTM.bDaTongHop = @daTongHop
	ORDER BY KHTM.dNgayQuyetDinh DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_KHTM_BHYT_ChiTiet (iID_KHTM_BHYT, iID_NoiDung, sTenNoiDung, iSoNguoi, iSoThang, fDinhMuc, fThanhTien, sGhiChu, dNgayTao, dNgaySua, sNguoiTao, sNguoiSua, iID_MaDonVi, sTenDonVi)
SELECT @idChungTu,
       iID_NoiDung,
	   sTenNoiDung,
       sum(iSoNguoi),
       sum(iSoThang),
	   sum(fDinhMuc),
	   sum(fThanhTien),
	   NULL,
       NULL,
       NULL,
       NULL,
	   NULL,
	   NULL,
	   NULL
FROM BH_KHTM_BHYT_ChiTiet
WHERE iID_KHTM_BHYT in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY iID_NoiDung,
	   sTenNoiDung;

--danh dau chung tu da tong hop
update BH_KHTM_BHYT set bDaTongHop = 1 
where iID_KHTM_BHYT in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop));
END
GO
/****** Object:  StoredProcedure [dbo].[sp_kt_khoitaodulieuchitietthanhtoan_byktdl_id]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_kt_khoitaodulieuchitietthanhtoan_byktdl_id]
@iId uniqueidentifier
AS
BEGIN
	SELECT 
		chld.iID_HopDongId as IIDHopDongId,
		chld.iId_KhoiTaoDuLieuChiTietId as IIdKhoiTaoDuLieuChiTietId,
		nt.sTenNhaThau as STenNhaThau,
		chld.iloai as ILoai,
		chld.iID_ChiPhiId as IIDChiPhiId,
		hd.fTienHopDong as FTienHopDong,
		chld.FLuyKeTtklhtTn_Khvn as FLuyKeTtklhtTnKhvn,
		chld.FLuyKeTUChuaThuHoiTn_Khvn as FLuyKeTUChuaThuHoiTnKhvn,
		chld.FLuyKeTtklhtNn_Khvn as FLuyKeTtklhtNnKhvn,
		chld.FLuyKeTUChuaThuHoiNn_Khvn as FLuyKeTUChuaThuHoiNnKhvn,
		chld.FLuyKeTtklhttn_Khvu as FLuyKeTtklhtTnKhvu,
		chld.FLuyKeTUChuaThuHoiTn_Khvu as FLuyKeTUChuaThuHoiTnKhvu,
		chld.FLuyKeTtklhtNn_Khvu as FLuyKeTtklhtNnKhvu,
		chld.FLuyKeTUChuaThuHoiNn_Khvu as FLuyKeTUChuaThuHoiNnKhvu
	FROM VDT_KT_KhoiTao_DuLieu as tbl 
	INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet as dt on tbl.Id = dt.iID_KhoiTaoDuLieuID 
	INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan as chld on dt.Id = chld.iId_KhoiTaoDuLieuChiTietId 
	LEFT JOIN VDT_DA_TT_HopDong as hd on chld.iID_HopDongId = hd.Id
	LEFT JOIN VDT_DM_NhaThau as nt on hd.iID_NhaThauThucHienID = nt.Id
	WHERE tbl.Id = @iId
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int
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
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
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
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),
fTienDaThucHienNamTruoc float,fTienUocThucHienNamTruoc float, fTienKeHoachThucHienNamNay float,fTienCanBo float,fTienQuanLuc float, fTienTaiChinh float,fTienQuanY float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , 
fTienDaThucHienNamTruoc ,fTienUocThucHienNamTruoc , fTienKeHoachThucHienNamNay ,fTienCanBo ,fTienQuanLuc , fTienTaiChinh ,fTienQuanY 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0)),
	   TienSQ=SUM(IsNull(A.fTienCanBo,0)),
	   TienQNCN=SUM(IsNull(A.fTienQuanLuc,0)),
	   TienCNVQP=SUM(IsNull(A.fTienTaiChinh,0)),
	   TienLDHD=SUM(IsNull(A.fTienQuanY,0))
FROM
  (SELECT 
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay,
				ctct.fTienCanBo,
				ctct.fTienQuanLuc,
				ctct.fTienTaiChinh,
				ctct.fTienQuanY
   FROM BH_KHC_KinhPhiQuanLy_ChiTiet ctct
   LEFT JOIN BH_KHC_KinhPhiQuanLy ct ON ct.iID_BH_KHC_KinhPhiQuanLy = ctct.iID_KHC_KinhPhiQuanLy
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = 9020000
   WHERE ct.iNamChungTu = @namLamViec--@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
IsNull(dt.fTienDaThucHienNamTruoc, 0) FTienDaThucHienNamTruoc,
IsNull(dt.fTienUocThucHienNamTruoc, 0) FTienUocThucHienNamTruoc,
IsNull(dt.fTienKeHoachThucHienNamNay, 0) FTienKeHoachThucHienNamNay,
IsNull(dt.fTienCanBo, 0) FTienCanBo,
IsNull(dt.fTienQuanLuc, 0) FTienQuanLuc,
IsNull(dt.fTienTaiChinh, 0) FTienTaiChinh,
IsNull(dt.fTienQuanY, 0) FTienQuanY
FROM @DataKhoi dt
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 18/07/2023 5:39:17 PM ******/
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
                IsNull(ctct.fThu_BHXH_NLD, 0) ThuBHXHNLDDong,
                IsNull(ctct.fThu_BHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fThu_BHTN_NLD, 0) ThuBHTNNLDDong,
                IsNull(ctct.fThu_BHTN_NSD, 0) ThuBHTNNSDDong
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
                IsNull(ctct.fThu_BHXH_NLD, 0) ThuBHXHNLDDong,
                IsNull(ctct.fThu_BHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fThu_BHTN_NLD, 0) ThuBHTNNLDDong,
                IsNull(ctct.fThu_BHTN_NSD, 0) ThuBHTNNSDDong
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 18/07/2023 5:39:17 PM ******/
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
           ct.iID_MaDonVi,
		   IsNull(ctct.fThu_BHYT_NSD, 0) ThuBHYTNSDDongDuToan,
		   IsNull(ctct.fThu_BHYT_NLD, 0) ThuBHYTNLDDongDuToan,
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
           ct.iID_MaDonVi,
		   IsNull(ctct.fThu_BHYT_NSD, 0) ThuBHYTNSDDongHachToan,
		   IsNull(ctct.fThu_BHYT_NLD, 0) ThuBHYTNLDDongHachToan,
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
IsNull(dt.TongBhytDuToan, 0) BHYTTongCongDuToan,
IsNull(ht.TongBhytHachToan, 0) BHYTTongCongHachToan
FROM @BhytDuToan dt
LEFT JOIN @BhytHachToan ht ON dt.idDonVi = ht.idDonVi;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
	@namLamViec int,
	@loaiChungTu int,
	@daTongHop bit,
	@lstSelectedUnit ntext,
	@hSSV nvarchar(50),
	@luuHS nvarchar(50),
	@hVSQ nvarchar(50),
	@sQDuBi nvarchar(50)
AS
BEGIN
	declare @tbl_HSSV table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HSSV float);
	declare @tbl_LuuHS table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_LuuHS float);
	declare @tbl_HVQS table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HVQS float);
	declare @tbl_SQDuBi table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_SQDuBi float);

	INSERT INTO @tbl_HSSV (IdDonVi, TenDonVI, ThanhTien_HSSV)
	SELECT 
		dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hSSV
		   WHERE ct.iNamChungTu = @namLamViec
			 AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @tbl_LuuHS (IdDonVi, TenDonVI, ThanhTien_LuuHS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @luuHS
		   WHERE ct.iNamChungTu = @namLamViec
			 AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @tbl_HVQS (IdDonVi, TenDonVI, ThanhTien_HVQS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hVSQ
		   WHERE ct.iNamChungTu = @namLamViec
			 AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @tbl_SQDuBi (IdDonVi, TenDonVI, ThanhTien_SQDuBi)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @sQDuBi
		   WHERE ct.iNamChungTu = @namLamViec
			 AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	SELECT result.idDonVi, 
		result.TenDonVI STenDonVi, 
		result.HSSV, 
		result.LuuHS,
		result.TongHSSV,
		result.HVQS,
		result.SQDuBi,
		(result.TongHSSV + result.HVQS + result.SQDuBi) TongCongHSSV
		FROM
		(SELECT hssv.idDonVi, 
		hssv.TenDonVI,
		IsNull(hssv.ThanhTien_HSSV, 0) HSSV,
		IsNull(luuhs.ThanhTien_LuuHS, 0) LuuHS,
		(IsNull(hssv.ThanhTien_HSSV + luuhs.ThanhTien_LuuHS, 0)) TongHSSV,
		IsNull(hvsq.ThanhTien_HVQS, 0) HVQS,
		IsNull(sqdb.ThanhTien_SQDuBi, 0) SQDuBi
		FROM @tbl_HSSV hssv
		LEFT JOIN @tbl_LuuHS luuhs ON hssv.idDonVi = luuhs.idDonVi
		LEFT JOIN @tbl_HVQS hvsq ON hssv.idDonVi = hvsq.idDonVi
		LEFT JOIN @tbl_SQDuBi sqdb ON hssv.idDonVi = sqdb.idDonVi) result
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
	@namLamViec int,
	@loaiChungTu int,
	@daTongHop bit,
	@lstSelectedUnit ntext,
	@thanNhanQuanNhan nvarchar(50),
	@thanNhanCNVQP nvarchar(50),
	@smDuToan nvarchar(50),
	@smHachToan nvarchar(50)
AS
BEGIN
	declare @TNQN_DuToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TNQN_DuToan float);
	declare @TN_CNVQP_DuToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TN_CNVQP_DuToan float);
	declare @TNQN_HachToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TNQN_HachToan float);
	declare @TN_CNVQP_HachToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TN_CNVQP_HachToan float);

	INSERT INTO @TNQN_DuToan (IdDonVi, TenDonVI, ThanhTien_TNQN_DuToan)
	SELECT
		dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanQuanNhan
		   AND ml.sM = @smDuToan
		   WHERE ct.iNamChungTu = @namLamViec
			 AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @TN_CNVQP_DuToan (IdDonVi, TenDonVI, ThanhTien_TN_CNVQP_DuToan)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanCNVQP
		   AND ml.sM = @smDuToan
		   WHERE ct.iNamChungTu = @namLamViec
			 AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @TNQN_HachToan (IdDonVi, TenDonVI, ThanhTien_TNQN_HachToan)
	SELECT 
			dt_dv.id,
			dt_dv.sTenDonVi,
		  SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
			 (SELECT
					  ml.sMoTa,
					  ct.iID_MaDonVi,
					  IsNull(ctct.fThanhTien, 0) ThanhTien,
					  ml.sLNS
			  FROM BH_KHTM_BHYT_ChiTiet ctct
			  LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
			  RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
			  AND ml.iNamLamViec = @namLamViec
			  AND ml.iTrangThai = 1
			  AND ml.sLNS = @thanNhanQuanNhan
			  AND ml.sM = @smHachToan
			  WHERE ct.iNamChungTu = @namLamViec
				 AND ct.bDaTongHop = @daTongHop
				 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
			   JOIN
			  (SELECT iID_MaDonVi AS id,
					  sTenDonVi, iLoai
			   FROM DonVi
			   WHERE iTrangThai = 1
			   AND iNamLamViec = @namLamViec
			   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
			GROUP BY
			dt_dv.sTenDonVi,
			dt_dv.id;

	INSERT INTO @TN_CNVQP_HachToan (IdDonVi, TenDonVI, ThanhTien_TN_CNVQP_HachToan)
	SELECT 
		dt_dv.id,
		dt_dv.sTenDonVi,
		SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		(SELECT
					ml.sMoTa,
					ct.iID_MaDonVi,
					IsNull(ctct.fThanhTien, 0) ThanhTien,
					ml.sLNS
			FROM BH_KHTM_BHYT_ChiTiet ctct
			LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
			RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
			AND ml.iNamLamViec = @namLamViec
			AND ml.iTrangThai = 1
			AND ml.sLNS = @thanNhanCNVQP
			AND ml.sM = @smHachToan
			WHERE ct.iNamChungTu = @namLamViec
				AND ct.bDaTongHop = @daTongHop
				AND ct.iLoaiTongHop = @loaiChungTu) AS A 
			JOIN
			(SELECT iID_MaDonVi AS id,
					sTenDonVi, iLoai
			FROM DonVi
			WHERE iTrangThai = 1
			AND iNamLamViec = @namLamViec
			AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;
		
		SELECT result.idDonVi, 
		result.TenDonVI STenDonVi, 
		result.TN_QN_DT TNQNDuToan, 
		result.TN_CNVQP_DT TNCNVQPDuToan,
		result.TongDuToan,
		result.TN_QN_HT TNQNHachToan,
		result.TN_CNVQP_HT TNCNVQPHachToan,
		result.TongHachToan TongHachToan,
		(result.TongDuToan + result.TongHachToan) TongCongThanNhan
		FROM
		(SELECT tnqn_dt.idDonVi, 
		tnqn_dt.TenDonVI,
		IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) TN_QN_DT,
		IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0) TN_CNVQP_DT,
		(IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) + IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0)) TongDuToan,
		IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) TN_QN_HT,
		IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0) TN_CNVQP_HT,
		(IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) + IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0)) TongHachToan
		FROM @TNQN_DuToan tnqn_dt
		LEFT JOIN @TN_CNVQP_DuToan tncn_dt ON tnqn_dt.idDonVi = tncn_dt.idDonVi
		LEFT JOIN @TNQN_HachToan tnqn_ht ON tnqn_dt.idDonVi = tnqn_ht.idDonVi
		LEFT JOIN @TN_CNVQP_HachToan tncn_ht ON tnqn_dt.idDonVi = tncn_ht.idDonVi) result
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet_clone]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet_clone]
@iIdPhanBoVon uniqueidentifier ,
@dNgayLap datetime 
AS
BEGIN
	select 
		distinct
		--pbvct.Id,
		da.iID_DuAnID,
		--pbvct.iID_PhanBoVonID,
		da.sTenDuAn,
		da.sMaDuAn,
		da.sTrangThaiDuAn,
		da.sKhoiCong,
		da.sKetThuc,
		da.sMaKetNoi,
		da.iID_MaDonViThucHienDuAnID as IIdMaDonViThucHienDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		lct.sTenLoaiCongTrinh,
		'' as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		pbvct.iID_LoaiCongTrinh as iID_LoaiCongTrinhID,
		null as iID_CapPheDuyetID,
		'' as sLNS,
		'' as sL,
		'' as sK,
		'' as sM,
		'' as sTM,
		'' as sTTM,
		'' as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		pbvct.sGhiChu as sGhiChu,
		cast(0 as float) as fChiTieuGoc,
		cast(0 as float) as FCapPhatTaiKhoBac,
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		pbvct.fGiaTriThuHoi as FGiaTriThuHoi,
		case when pbvct.fGiaTrPhanBo is not null then pbvct.fGiaTrPhanBo else pbvct.fThanhToanDeXuat end as FGiaTrPhanBo,
		--isnull(pbvct.Id, NEWID()) as IIdPhanBoVonId,
		pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID as IIdPhanBoVon,
		pbvct.ILoaiDuAn as ILoaiDuAn,
		pbv.Id as IdChungTu,
		pbv.iID_ParentId as IdChungTuParent,
		pbv.bActive as BActive,
		pbv.bIsGoc as IsGoc,
		--case when pbvct.fThanhToanDeXuat is not null then pbvct.fThanhToanDeXuat else pbvdxct.fThanhToan end as fThanhToanDeXuat
		pbvct.fThanhToanDeXuat as fThanhToanDeXuat,
		pbvct.iID_DuAn_HangMucID
	--from
	--	VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvct
	--inner join
	--	VDT_KHV_PhanBoVon_DonVi_PheDuyet pbv
	--on pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = pbv.Id
	--inner join 
	--VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvdxct on pbvdxct.iID_DuAn_HangMucID = pbvct.iID_DuAn_HangMucID
	from
		VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvct
	inner join
		VDT_KHV_PhanBoVon_DonVi_PheDuyet pbv 
	on pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = pbv.Id
	left join
		VDT_DA_DuAn da
	on
		pbvct.iID_DuAnID = da.iID_DuAnID
	left join
		VDT_DM_LoaiCongTrinh lct
	on
		pbvct.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
	where
		pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = @iIdPhanBoVon
		and pbv.bIsGoc = 1 --and pbvdxct.bActive = 1
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khv_kehoach_von_nam_duoc_duyet_export]    Script Date: 18/07/2023 5:39:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_khv_kehoach_von_nam_duoc_duyet_export]
	@lstId [nvarchar](max),
	@lct [nvarchar](max),
	@type [int],
	@loaiDuAn [int],
	@lstDonVi [nvarchar](max),
	@MenhGiaTienTe [float]
WITH EXECUTE AS CALLER
AS
BEGIN
	if(@type = 1)
	begin
		select
			'' as STT,
			lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			lct.iID_Parent as IdLoaiCongTrinhParent,
			lct.sTenLoaiCongTrinh as sTenDuAn,
			'' as sLNS,
			'' as sL,
			'' as sK,
			'' as sM,
			'' as sTM,
			'' as sTTM,
			'' as sNG,
			cast(0 as float) as FCapPhatTaiKhoBac,
			cast(0 as float) as FCapPhatBangLenhChi,
			cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
			cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
			cast(0 as float) as TongSo,
			null as IIdDuAn,

			3 as Loai,
			cast(1 as bit) as IsHangCha,
			case
				when lct.iID_Parent is null then 0 else 1
			end LoaiParent,
			1 as LoaiCongTrinh
		from f_loai_cong_trinh_get_list_childrent(@lct) lct

		union all

		select 
			'' as STT,
			ctct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			null as IdLoaiCongTrinhParent,
			da.sTenDuAn as sTenDuAn,
			ctct.LNS as sLNS,
			ctct.L as sL,
			ctct.K as sK,
			ctct.M as sM,
			ctct.TM as sTM,
			ctct.TTM as sTTM,
			ctct.NG as sNG,
			ctct.FCapPhatTaiKhoBac/@MenhGiaTienTe as FCapPhatTaiKhoBac,
			ctct.FCapPhatBangLenhChi/@MenhGiaTienTe as FCapPhatBangLenhChi,
			ctct.FGiaTriThuHoiNamTruocKhoBac/@MenhGiaTienTe as FGiaTriThuHoiNamTruocKhoBac,
			ctct.FGiaTriThuHoiNamTruocLenhChi/@MenhGiaTienTe as FGiaTriThuHoiNamTruocLenhChi,
			(isnull(ctct.FCapPhatTaiKhoBac, 0) + isnull(ctct.FCapPhatBangLenhChi, 0))/@MenhGiaTienTe as TongSo,
			ctct.iID_DuAnID as IIdDuAn,
			4 as Loai,
			cast(0 as bit) as IsHangCha,
			2 as LoaiParent,
			1 as LoaiCongTrinh
		from 
			VDT_KHV_PhanBoVon_ChiTiet ctct
		left join
			VDT_DA_DuAn da
		on ctct.iID_DuAnID = da.iID_DuAnID
		where 
			ctct.iID_PhanBoVonID in (select * from dbo.splitstring(@lstId))
			and ctct.ILoaiDuAn = @loaiDuAn
			and (ctct.iID_LoaiCongTrinh in ( select * from dbo.splitstring(@lct)) or  ctct.iID_LoaiCongTrinh in (select iID_LoaiCongTrinh from f_loai_cong_trinh_get_list_childrent(@lct)))
		order by iID_LoaiCongTrinh, Loai
	end
	else
	begin
		select
			'' as IdMaDonViQuanLy,
			'' as STT,
			lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			lct.iID_Parent as IdLoaiCongTrinhParent,
			lct.sTenLoaiCongTrinh as sTenDuAn,
			'' as sLNS,
			'' as sL,
			'' as sK,
			'' as sM,
			'' as sTM,
			'' as sTTM,
			'' as sNG,
			cast(0 as float) as FCapPhatTaiKhoBac,
			cast(0 as float) as FCapPhatBangLenhChi,
			cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
			cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
			cast(0 as float) as TongSo,
			null as IIdDuAn,

			3 as Loai,
			cast(1 as bit) as IsHangCha,
			case
				when lct.iID_Parent is null then 0 else 1
			end LoaiParent,
			1 as LoaiCongTrinh
		from f_loai_cong_trinh_get_list_childrent(@lct) lct

		union all
		
		select
			da.iID_MaDonViThucHienDuAnID as IdMaDonViQuanLy,
			'' as STT,
			lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			lct.iID_Parent as IdLoaiCongTrinhParent,
			dv.sTenDonVi as sTenDuAn,
			'' as sLNS,
			'' as sL,
			'' as sK,
			'' as sM,
			'' as sTM,
			'' as sTTM,
			'' as sNG,
			SUM(isnull(pbvct.fCapPhatTaiKhoBac/@MenhGiaTienTe, 0)) as FCapPhatTaiKhoBac,
			SUM(isnull(pbvct.fCapPhatBangLenhChi/@MenhGiaTienTe, 0)) as FCapPhatBangLenhChi,
			SUM(isnull(pbvct.fGiaTriThuHoiNamTruocKhoBac/@MenhGiaTienTe, 0)) as FGiaTriThuHoiNamTruocKhoBac,
			SUM(isnull(pbvct.fGiaTriThuHoiNamTruocLenhChi/@MenhGiaTienTe, 0)) as FGiaTriThuHoiNamTruocLenhChi,
			(SUM(isnull(pbvct.fCapPhatTaiKhoBac/@MenhGiaTienTe, 0)) + SUM(isnull(pbvct.fCapPhatBangLenhChi/@MenhGiaTienTe, 0))) as TongSo,
			null as IIdDuAn,

			3 as Loai,
			cast(1 as bit) as IsHangCha,
			2 as LoaiParent,
			1 as LoaiCongTrinh
		from 
			VDT_KHV_PhanBoVon_ChiTiet pbvct
		inner join
			VDT_DA_DuAn da
		on pbvct.iID_DuAnID = da.iID_DuAnID
		left join
			VDT_DM_LoaiCongTrinh lct
		on pbvct.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
		left join
			VDT_DM_DonViThucHienDuAn dv
		on da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
		where
			pbvct.iID_PhanBoVonID in (select * from dbo.splitstring(@lstId))
			and pbvct.ILoaiDuAn = @loaiDuAn
			and da.iID_MaDonViQuanLy in (select * from dbo.splitstring(@lstDonVi))
			and (pbvct.iID_LoaiCongTrinh in ( select * from dbo.splitstring(@lct)) or  pbvct.iID_LoaiCongTrinh in (select iID_LoaiCongTrinh from f_loai_cong_trinh_get_list_childrent(@lct)))
		group by da.iID_MaDonViThucHienDuAnID, lct.iID_LoaiCongTrinh, lct.iID_Parent, dv.sTenDonVi

		union all

		select 
			da.iID_MaDonViThucHienDuAnID as IdMaDonViQuanLy,
			'' as STT,
			ctct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			null as IdLoaiCongTrinhParent,
			da.sTenDuAn as sTenDuAn,
			ctct.LNS as sLNS,
			ctct.L as sL,
			ctct.K as sK,
			ctct.M as sM,
			ctct.TM as sTM,
			ctct.TTM as sTTM,
			ctct.NG as sNG,
			SUM(ctct.FCapPhatTaiKhoBac/@MenhGiaTienTe) as FCapPhatTaiKhoBac,
			SUM(ctct.FCapPhatBangLenhChi/@MenhGiaTienTe) as FCapPhatBangLenhChi,
			SUM(ctct.FGiaTriThuHoiNamTruocKhoBac/@MenhGiaTienTe) as FGiaTriThuHoiNamTruocKhoBac,
			SUM(ctct.FGiaTriThuHoiNamTruocLenhChi/@MenhGiaTienTe) as FGiaTriThuHoiNamTruocLenhChi,
			SUM((isnull(ctct.FCapPhatTaiKhoBac, 0) + isnull(ctct.FCapPhatBangLenhChi, 0))/@MenhGiaTienTe) as TongSo,
			ctct.iID_DuAnID as IIdDuAn,
			5 as Loai,
			cast(0 as bit) as IsHangCha,
			3 as LoaiParent,
			1 as LoaiCongTrinh
		from 
			VDT_KHV_PhanBoVon_ChiTiet ctct
		left join
			VDT_DA_DuAn da
		on ctct.iID_DuAnID = da.iID_DuAnID
		where 
			ctct.iID_PhanBoVonID in (select * from dbo.splitstring(@lstId))
			and ctct.ILoaiDuAn = @loaiDuAn
			and da.iID_MaDonViQuanLy in (select * from dbo.splitstring(@lstDonVi))
			and (ctct.iID_LoaiCongTrinh in ( select * from dbo.splitstring(@lct)) or  ctct.iID_LoaiCongTrinh in (select iID_LoaiCongTrinh from f_loai_cong_trinh_get_list_childrent(@lct)))
        
		-- group nhung ban ghi cung l,k,m,tm,ttm,n
		group by da.iID_MaDonViThucHienDuAnID, ctct.iID_LoaiCongTrinh, ctct.LNS, 	da.sTenDuAn,
			ctct.L ,
			ctct.K ,
			ctct.M ,
			ctct.TM ,
			ctct.TTM ,
			ctct.NG,
			ctct.iID_DuAnID
		order by iID_LoaiCongTrinh, Loai, IdMaDonViQuanLy
	end
END
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]    Script Date: 18/07/2023 5:42:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_insert_tonghopnguonnsdautu_tang_new]
@sLoai nvarchar(100),
@iTypeExecute int,
@uIdQuyetDinh uniqueidentifier,
@iIDQuyetDinhOld uniqueidentifier
AS
BEGIN
	CREATE TABLE #lstMaNguon(sMaNguon nvarchar(100))

	DECLARE @RankDate DATETIME = (SELECT TOP(1) dNgayQuyetDinh FROM VDT_TongHop_NguonNSDauTu WHERE sMaNguon COLLATE DATABASE_DEFAULT = 'QUYET_TOAN' ORDER BY dNgayQuyetDinh DESC)

	IF(@sLoai = 'KHVN')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('KHVN'), ('101'), ('102'),('111'),('112'),('000'),('211a'),('212a')
	END
	ELSE IF(@sLoai = 'KHVU')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('KHVU'), ('121a'), ('122a')
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('QUYET_TOAN'), ('131'), ('132'), ('211c'), ('212c'), ('301'), ('302'), ('321a'), ('322a')
			, ('000'), ('321b'), ('322b')
	END

	IF(@iTypeExecute in (2,3,4))
	BEGIN 
		IF (@iTypeExecute in (2,3))
		BEGIN
			-- dao nguoc but toan
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTri, bIsLog, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--OUTPUT inserted.Id, inserted.sMaDich, inserted.iID_DuAnID, ISNULL(inserted.fGiaTri, 0) INTO #tmp(iId, sMaNguon, iIdDuAnId, fDaThanhToan)
			SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300',
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
				AND bIsLog = 0 AND sMaTienTrinh = (CASE WHEN @sLoai = 'QUYET_TOAN' THEN '100' ELSE '200' END)

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')
		END
		ELSE IF (@iTypeExecute = 4)
		BEGIN
			-- dao nguoc but toan
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTri, bIsLog, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--OUTPUT inserted.Id, inserted.sMaDich, inserted.iID_DuAnID, ISNULL(inserted.fGiaTri, 0) INTO #tmp(iId, sMaNguon, iIdDuAnId, fDaThanhToan)
			SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300',
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld 
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')
		END

		-- deleted thi khong xu ly nua
		IF(@iTypeExecute = 3)
		BEGIN
			RETURN
		END
	END

	IF(@sLoai = 'KHVN')
	BEGIN
		IF(@iTypeExecute = 4)
		BEGIN

		-- Tao data insert vao but toan
			--  Du toan dau nam, nam nay
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					(CASE dt.sMaNguon WHEN '101' THEN '000' 
									  WHEN '102' THEN '000'
									  WHEN '000' THEN '121a'
									  WHEN '555' THEN '122a'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','000','555') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsertDC

			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, iID_LoaiCongTrinh,
						(CASE colName WHEN 'fCapPhatTaiKhoBacDC' THEN '101' 
									  WHEN 'fCapPhatBangLenhChiDC' THEN '102'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBacDC' THEN '000'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChiDC' THEN '555'
									  END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			--DuToan nam truoc chuyen sang
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					(CASE dt.sMaNguon WHEN '101' THEN '000' 
									  WHEN '102' THEN '000'
									  WHEN '211a' THEN '000'
									  WHEN '212a' THEN '000'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','000') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsert2DC
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBacDC' THEN '111' 
									  WHEN 'fCapPhatBangLenhChiDC' THEN '112'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBacDC' THEN '211a'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChiDC' THEN '212a'
									  END) as sMaNguon
						from 
						(select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBacDC,fGiaTriThuHoiNamTruocLenhChiDC)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan = 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			-- doi lai sMaNguon cua thuhoitruoclenhchi dung
			update #tmpDataInsertDC
			set sMaNguon = '000'
			where sMaNguon = '555'
			--LoaiDuToan = 2
			update #tmpDataInsert2DC
			set sMaNguon = '000'
			where sMaNguon = '555'

			-- insert but toan moi vao 
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--Insert data vua tao vao but toan--
			SELECT * FROM #tmpDataInsertDC

			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--Insert data vua tao vao but toan--
			SELECT * FROM #tmpDataInsert2DC
			DROP TABLE #tmpDataInsertDC;
			DROP TABLE #tmpDataInsert2DC;
		END
		ELSE
		BEGIN

			-- Tao data insert vao but toan
			--  Du toan dau nam, nam nay
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					(CASE dt.sMaNguon WHEN '101' THEN '000' 
									  WHEN '102' THEN '000'
									  WHEN '000' THEN '121a'
									  WHEN '555' THEN '122a'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','000','555') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsert

			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, iID_LoaiCongTrinh,
						(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '101' 
									  WHEN 'fCapPhatBangLenhChi' THEN '102'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBac' THEN '000'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChi' THEN '555'
									  END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			--DuToan nam truoc chuyen sang
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					(CASE dt.sMaNguon WHEN '101' THEN '000' 
									  WHEN '102' THEN '000'
									  WHEN '211a' THEN '000'
									  WHEN '212a' THEN '000'
									  END) as sMaDich, 
					SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 0 as iStatus, 
					(CASE WHEN sMaNguon in ('101', '102','000') THEN '200' ELSE '100' END) as sMaTienTrinh,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
					INTO #tmpDataInsert2
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '111' 
									  WHEN 'fCapPhatBangLenhChi' THEN '112'
									  WHEN 'fGiaTriThuHoiNamTruocKhoBac' THEN '211a'
									  WHEN 'fGiaTriThuHoiNamTruocLenhChi' THEN '212a'
									  END) as sMaNguon
						from 
						(select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan = 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			-- doi lai sMaNguon cua thuhoitruoclenhchi dung
			update #tmpDataInsert
			set sMaNguon = '000'
			where sMaNguon = '555'
			--LoaiDuToan = 2
			update #tmpDataInsert2
			set sMaNguon = '000'
			where sMaNguon = '555'
			-- insert but toan moi vao 
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--Data Insert
			SELECT * FROM #tmpDataInsert

			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			Select * from #tmpDataInsert2
			

			DROP TABLE #tmpDataInsert;
			DROP TABLE #tmpDataInsert2;

		END
	END
	ELSE IF(@sLoai = 'KHVU')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
											iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
		SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
				tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon , '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, '200',
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
		FROM VDT_KHV_KeHoachVonUng as tbl
		INNER JOIN (select Id,dt.iID_KeHoachUngID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
					(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '121a' WHEN 'fCapPhatBangLenhChi' THEN '122a' END) as sMaNguon
					from 
					(select Id,iID_KeHoachUngID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, fCapPhatTaiKhoBac,fCapPhatBangLenhChi from VDT_KHV_KeHoachVonUng_ChiTiet) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi)) as dt) as dt on dt.iID_KeHoachUngID = tbl.Id
		LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
		WHERE tbl.Id = @uIdQuyetDinh
		GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
			iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
	END
END
;
;
GO
