/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 1/4/2024 8:24:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]    Script Date: 1/4/2024 8:24:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi_theokhoi]    Script Date: 1/4/2024 8:24:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]    Script Date: 1/4/2024 8:24:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 1/4/2024 8:24:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_phanbo_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_dutoan_index]    Script Date: 1/4/2024 8:24:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 1/4/2024 8:24:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]    Script Date: 1/4/2024 8:24:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]    Script Date: 1/4/2024 8:24:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @sChungTuId nvarchar(255) = @ChungTuId;
	DECLARE @sLNS nvarchar(max) = @LNS;
	DECLARE @iNamLamViec int = @NamLamViec;
	DECLARE @iNamNganSach int = @NamNganSach;
	DECLARE @iNguonNganSach int = @NguonNganSach;
	DECLARE @sUserName nvarchar(100) = @UserName;
	DECLARE @isQuanly int;

	DECLARE @CountDonViCha int;
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

	select  @isQuanly =  COUNT(*) from  DonVi inner join  NguoiDung_DonVi  on  DonVi.iID_MaDonVi =  NguoiDung_DonVi.iID_MaDonVi  
	where  iLoai = 0 and NguoiDung_DonVi.iID_MaNguoiDung = @sUserName and NguoiDung_DonVi.inamlamviec = @iNamLamViec  and DonVi.iNamLamViec= @iNamLamViec;
	
	
	if @isQuanly >0
		set @isQuanly=1;
	else
		set @isQuanly=0 ;

	-- lấy ra các chứng từ phân bổ điều chỉnh trước đó
	WITH tempIdPhanBo AS (
		SELECT * FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_PhanBo = @sChungTuId
		UNION ALL
		SELECT map.* FROM NS_DT_Nhan_PhanBo_Map map
		INNER JOIN tempIdPhanBo
		ON map.iID_CTDuToan_PhanBo = tempIdPhanBo.iID_CTDuToan_Nhan
	),
	tblIdPhanBo AS (
		SELECT iID_CTDuToan_Nhan FROM tempIdPhanBo
	),
	-- lấy dữ liệu MLNS theo người dùng
	-- lấy dữ liệu MLNS theo người dùng
	tblMlns AS (
		SELECT * 
		FROM NS_MucLucNganSach
		WHERE iNamLamViec = @iNamLamViec
			AND iTrangThai = 1
			AND bHangChaDuToan is not null
			AND ((@CountDonViCha <> 0
				AND sLNS IN
					(SELECT DISTINCT VALUE
					 FROM
					   (SELECT CAST(LEFT(Item, 1) AS nvarchar(10)) LNS1,
							   CAST(LEFT(Item, 3) AS nvarchar(10)) LNS3,
							   CAST(LEFT(Item, 5) AS nvarchar(10)) LNS5,
							   CAST(Item AS nvarchar(10)) LNS
						FROM f_split(@LNS) ) LNS UNPIVOT (value
																FOR col in (LNS1, LNS3, LNS5, LNS)) un))
				OR (@CountDonViCha = 0
					AND sLNS IN
					(SELECT DISTINCT VALUE
						FROM
						(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
								CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
								CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
								CAST(sLNS AS nvarchar(10)) LNS
						FROM NS_NguoiDung_LNS
						WHERE sMaNguoiDung = @sUserName
							AND iNamLamViec = @iNamLamViec
							AND sLNS IN
							(SELECT *
								FROM f_split(@sLNS)) ) LNS UNPIVOT (value
																FOR col IN (LNS1, LNS3, LNS5, LNS)) un)))
	),
	-- lấy dữ liệu MLNS cha
	tblMlnsParent AS (
		SELECT * FROM tblMlns WHERE bHangChaDuToan = 1
	),
	-- lấy dữ liệu MLNS con
	tblMlnsChild AS (
		SELECT * FROM tblMlns WHERE bHangChaDuToan = 0
	),
	-- lấy ra chứng từ bị điều chỉnh
	tblSoQuyetDinhBiDieuChinh AS (
		SELECT sSoQuyetDinh, iID_DTChungTu AS idSoQuyetDinh 
		FROM NS_DT_ChungTu 
		WHERE iID_DTChungTu IN (
			SELECT * FROM tblIdPhanBo
		)
		AND iLoai = 1
	),
	-- tạo ra dòng tổng chi tiết iRowType = 1
	tblRowChiTietCha AS (
		SELECT 
			NEWID() AS iID_DTCTChiTiet,
			CAST(0x0 AS uniqueidentifier) AS iID_DTChungTu,
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
			'1' AS bHangCha,
			@iNamNganSach AS iNamNganSach,
			@iNguonNganSach AS iID_MaNguonNganSach,
			@iNamLamViec AS iNamLamViec,
			'' AS iID_MaDonVi,
			'' AS sTenDonVi,
			'' AS sDotPhanBoTruoc,
			'' AS sSoQuyetDinh,
			0 AS fTonKhoTruocDieuChinh,
			0 AS fTonKho,
			0 AS fTonKhoSauDieuChinh,
		    0 AS fTuChiTruocDieuChinh,
			0 AS fTuChi,
			0 AS fTuChiSauDieuChinh,
		    0 AS fHienVatTruocDieuChinh,
			0 AS fHienVat,
			0 AS fHienVatSauDieuChinh,
		    0 AS fHangNhapTruocDieuChinh,
			0 AS fHangNhap,
			0 AS fHangNhapSauDieuChinh,
		    0 AS fHangMuaTruocDieuChinh,
			0 AS fHangMua,
			0 AS fHangMuaSauDieuChinh,
		    0 AS fPhanCapTruocDieuChinh,
			0 AS fPhanCap,
			0 AS fPhanCapSauDieuChinh,
			0 AS fDuPhong,
			cast(0x0 as uniqueidentifier) AS iID_CTDuToan_Nhan,
			0 AS iPhanCap,
			'' AS sGhiChu,
			1 AS iRowType,
			cast(0 as bit) AS bEmpty,
			GetDate() AS dNgayTao,
			@sUserName AS sNguoiTao,
			GetDate() AS dNgaySua,
			@sUserName AS sNguoiSua,
			cast(1 as bit) as bHangChaDuToan
		FROM tblMlnsChild
	),
	-- lấy ra dòng cha từ bảng tblMlnsParent iRowType = 0
	tblRowCha AS (
		SELECT
			NEWID() AS iID_DTCTChiTiet,
			CAST(0x0 AS uniqueidentifier) AS iID_DTChungTu,
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
			@iNamNganSach AS iNamNganSach,
			@iNguonNganSach AS iID_MaNguonNganSach,
			@iNamLamViec AS iNamLamViec,
			'' AS iID_MaDonVi,
			'' AS sTenDonVi,
			'' AS sDotPhanBoTruoc,
			'' AS sSoQuyetDinh,
			0 AS fTonKhoTruocDieuChinh,
			0 AS fTonKho,
			0 AS fTonKhoSauDieuChinh,
		    0 AS fTuChiTruocDieuChinh,
			0 AS fTuChi,
			0 AS fTuChiSauDieuChinh,
		    0 AS fHienVatTruocDieuChinh,
			0 AS fHienVat,
			0 AS fHienVatSauDieuChinh,
		    0 AS fHangNhapTruocDieuChinh,
			0 AS fHangNhap,
			0 AS fHangNhapSauDieuChinh,
		    0 AS fHangMuaTruocDieuChinh,
			0 AS fHangMua,
			0 AS fHangMuaSauDieuChinh,
		    0 AS fPhanCapTruocDieuChinh,
			0 AS fPhanCap,
			0 AS fPhanCapSauDieuChinh,
			0 AS fDuPhong,
			cast(0x0 as uniqueidentifier) AS iID_CTDuToan_Nhan,
			0 AS iPhanCap,
			'' AS sGhiChu,
			0 AS iRowType,
			cast(0 as bit) AS bEmpty,
			GetDate() AS dNgayTao,
			@sUserName AS sNguoiTao,
			GetDate() AS dNgaySua,
			@sUserName AS sNguoiSua,
			bHangChaDuToan
		FROM tblMlnsParent
	),
	tblDataTruocDieuChinh AS (

		select vdataTruocDieuChinh.iID_MLNS,
			vdataTruocDieuChinh.iID_MLNS_Cha,
			vdataTruocDieuChinh.sXauNoiMa,
			vdataTruocDieuChinh.sLNS,
			vdataTruocDieuChinh.sL,
			vdataTruocDieuChinh.sK,
			vdataTruocDieuChinh.sM,
			vdataTruocDieuChinh.sTM,
			vdataTruocDieuChinh.sTTM,
			vdataTruocDieuChinh.sNG,
			vdataTruocDieuChinh.sTNG,
			vdataTruocDieuChinh.sTNG1,
			vdataTruocDieuChinh.sTNG2,
			vdataTruocDieuChinh.sTNG3,
			vdataTruocDieuChinh.sMoTa,
			vdataTruocDieuChinh.bHangCha,
			vdataTruocDieuChinh.iID_MaDonVi,
			vdataTruocDieuChinh.iID_CTDuToan_Nhan,
			vdataTruocDieuChinh.iPhanCap,
			sum(vdataTruocDieuChinh.fTonKho) as fTonKho,
			sum(vdataTruocDieuChinh.fTuChi) as fTuChi,
			sum(vdataTruocDieuChinh.fHienVat) as fHienVat,
			sum(vdataTruocDieuChinh.fHangNhap) as fHangNhap,
			sum(vdataTruocDieuChinh.fHangMua) as fHangMua,
			sum(vdataTruocDieuChinh.fPhancap) as fPhanCap,
			sum(vdataTruocDieuChinh.fDuPhong) as fDuPhong,
			sTenDonVi, 
			vdataTruocDieuChinh.sSoQuyetDinh
			from (
				SELECT ctct.iID_MLNS,
					ctct.iID_MLNS_Cha,
					ctct.sXauNoiMa,
					ctct.sLNS,
					ctct.sL,
					ctct.sK,
					ctct.sM,
					ctct.sTM,
					ctct.sTTM,
					ctct.sNG,
					ctct.sTNG,
					ctct.sTNG1,
					ctct.sTNG2,
					ctct.sTNG3,
					ctct.sMoTa,
					ctct.bHangCha,
					ctct.iID_MaDonVi,
					ctct.iID_CTDuToan_Nhan,
					ctct.iPhanCap,
					fTonKho,
					fTuChi,
					fHienVat,
					fHangNhap,
					fHangMua,
					fPhanCap,
					fDuPhong,
					CONCAT(dv.iID_MaDonVi,' - ',dv.sTenDonVi) AS sTenDonVi, 
					(case when @isQuanly=1 then ct.sSoQuyetDinh else '' end) AS  sSoQuyetDinh
					--ct.sSoQuyetDinh
				FROM NS_DT_ChungTuChiTiet ctct
				LEFT JOIN (SELECT * FROM DonVi WHERE iNamLamViec = @iNamLamViec) dv
				on dv.iID_MaDonVi = ctct.iID_MaDonVi
				LEFT JOIN NS_DT_ChungTu ct
				on ct.iID_DTChungTu = ctct.iID_CTDuToan_Nhan
				inner join tblSoQuyetDinhBiDieuChinh sqd
				on sqd.idSoQuyetDinh = ctct.iID_DTChungTu
				) vdataTruocDieuChinh
		group by
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
			iID_MaDonVi,
			iID_CTDuToan_Nhan,
			iPhanCap,
			sSoQuyetDinh,
			iID_MaDonVi,
			sTenDonVi
		),
	-- tạo dòng chi tiết
	tblRowChiTiet AS (
		SELECT 
			ISNULL(ctctdc.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
			ISNULL(ctctdc.iID_DTChungTu, CAST(0x0 AS uniqueidentifier)) AS iID_DTChungTu,
			ISNULL(ctctbdc.iID_MLNS, ctctdc.iID_MLNS) AS iID_MLNS,
			ISNULL(ctctbdc.iID_MLNS_Cha, ctctdc.iID_MLNS_Cha) AS iID_MLNS_Cha,
			ISNULL(ctctbdc.sXauNoiMa, ctctdc.sXauNoiMa) AS sXauNoiMa,
			ISNULL(ctctbdc.sLNS, ctctdc.sLNS) AS sLNS,
			ISNULL(ctctbdc.sL, ctctdc.sL) AS sL,
			ISNULL(ctctbdc.sK, ctctdc.sK) AS sK,
			ISNULL(ctctbdc.sM, ctctdc.sM) AS sM,
			ISNULL(ctctbdc.sTM, ctctdc.sTM) AS sTM,
			ISNULL(ctctbdc.sTTM, ctctdc.sTTM) AS sTTM,
			ISNULL(ctctbdc.sNG, ctctdc.sNG) AS sNG,
			ISNULL(ctctbdc.sTNG, ctctdc.sTNG) AS sTNG,
			ISNULL(ctctbdc.sTNG1, ctctdc.sTNG1) AS sTNG1,
			ISNULL(ctctbdc.sTNG2, ctctdc.sTNG2) AS sTNG2,
			ISNULL(ctctbdc.sTNG3, ctctdc.sTNG3) AS sTNG3,
			ISNULL(ctctbdc.sMoTa, ctctdc.sMoTa) AS sMoTa,
			ISNULL(ctctbdc.bHangCha, ctctdc.bHangCha) AS bHangCha,
			@iNamNganSach AS iNamNganSach,
			@iNguonNganSach AS iID_MaNguonNganSach,
			@iNamLamViec AS iNamLamViec,
			ISNULL(ctctbdc.iID_MaDonVi, ctctdc.iID_MaDonVi) AS iID_MaDonVi,
			ISNULL(ctctbdc.sTenDonVi, ctctdc.sTenDonVi) AS sTenDonVi,
			'' AS sDotPhanBoTruoc,
			(case when @isQuanly=1 then ISNULL(ctctbdc.sSoQuyetDinh, ctctdc.sSoQuyetDinh) else '' end)AS  sSoQuyetDinh ,
			--ISNULL(ctctbdc.sSoQuyetDinh, ctctdc.sSoQuyetDinh) AS sSoQuyetDinh,
			
			ISNULL(ctctbdc.fTonKho, 0) AS fTonKhoTruocDieuChinh,
			ISNULL(ctctdc.fTonKho, 0) AS fTonKho,
			ISNULL(ctctbdc.fTonKho, 0) + ISNULL(ctctdc.fTonKho, 0) AS fTonKhoSauDieuChinh,
		    
			ISNULL(ctctbdc.fTuChi, 0) AS fTuChiTruocDieuChinh,
			ISNULL(ctctdc.fTuChi, 0) AS fTuChi,
			ISNULL(ctctbdc.fTuChi, 0) + ISNULL(ctctdc.fTuChi, 0) AS fTuChiSauDieuChinh,

		    ISNULL(ctctbdc.fHienVat, 0) AS fHienVatTruocDieuChinh,
			ISNULL(ctctdc.fHienVat, 0) AS fHienVat,
			ISNULL(ctctbdc.fHienVat, 0) + ISNULL(ctctdc.fHienVat, 0) AS fHienVatSauDieuChinh,

		    ISNULL(ctctbdc.fHangNhap, 0) AS fHangNhapTruocDieuChinh,
			ISNULL(ctctdc.fHangNhap, 0) AS fHangNhap,
			ISNULL(ctctbdc.fHangNhap, 0) + ISNULL(ctctdc.fHangNhap, 0) AS fHangNhapSauDieuChinh,

		    ISNULL(ctctbdc.fHangMua, 0) AS fHangMuaTruocDieuChinh,
			ISNULL(ctctdc.fHangMua, 0) AS fHangMua,
			ISNULL(ctctbdc.fHangMua, 0) + ISNULL(ctctdc.fHangMua, 0) AS fHangMuaSauDieuChinh,

		    ISNULL(ctctbdc.fPhanCap, 0) AS fPhanCapTruocDieuChinh,
			ISNULL(ctctdc.fPhanCap, 0) AS fPhanCap,
			ISNULL(ctctbdc.fPhanCap, 0) + ISNULL(ctctdc.fPhanCap, 0) AS fPhanCapSauDieuChinh,

			ISNULL(ctctdc.fDuPhong, ctctbdc.fDuPhong) AS fDuPhong,

			ISNULL(ctctbdc.iID_CTDuToan_Nhan, ctctdc.iID_CTDuToan_Nhan) AS iID_CTDuToan_Nhan,
			ISNULL(ctctbdc.iPhanCap, ctctdc.iPhanCap) AS iPhanCap,
			ctctdc.sGhiChu AS sGhiChu,
			3 AS RowType,
			cast(0 as bit) AS bEmpty,
			GetDate() AS dNgayTao,
			ctctdc.sNguoiTao AS sNguoiTao,
			GetDate() AS dNgaySua,
			ctctdc.sNguoiSua AS sNguoiSua,
			cast(0 as bit) AS bHangChaDuToan
		FROM
			tblDataTruocDieuChinh ctctbdc
		full join
			(SELECT ctct.*, CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS sTenDonVi, ct.sSoQuyetDinh
			FROM NS_DT_ChungTuChiTiet ctct
			LEFT JOIN (SELECT * FROM DonVi WHERE iNamLamViec = @iNamLamViec) dv
			on dv.iID_MaDonVi = ctct.iID_MaDonVi
			LEFT JOIN NS_DT_ChungTu ct
			on ct.iID_DTChungTu = ctct.iID_CTDuToan_Nhan
			WHERE ctct.iID_DTChungTu = @sChungTuId) ctctdc
		on ctctbdc.iID_MLNS = ctctdc.iID_MLNS and ctctbdc.iID_MaDonVi = ctctdc.iID_MaDonVi and ctctbdc.iID_CTDuToan_Nhan = ctctdc.iID_CTDuToan_Nhan
	),
	-- tạo dòng trống để điền thông tin iRowType = 2
	tblRowChiTietEmpty AS (
		SELECT 
			NEWID() AS iID_DTCTChiTiet,
			CAST(0x0 AS uniqueidentifier) AS iID_DTChungTu,
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
			'0' AS bHangCha,
			@iNamNganSach AS iNamNganSach,
			@iNguonNganSach AS iID_MaNguonNganSach,
			@iNamLamViec AS iNamLamViec,
			'' AS iID_MaDonVi,
			'' AS sTenDonVi,
			'' AS sDotPhanBoTruoc,
			'' AS sSoQuyetDinh,
			0 AS fTonKhoTruocDieuChinh,
			0 AS fTonKho,
			0 AS fTonKhoSauDieuChinh,
		    0 AS fTuChiTruocDieuChinh,
			0 AS fTuChi,
			0 AS fTuChiSauDieuChinh,
		    0 AS fHienVatTruocDieuChinh,
			0 AS fHienVat,
			0 AS fHienVatSauDieuChinh,
		    0 AS fHangNhapTruocDieuChinh,
			0 AS fHangNhap,
			0 AS fHangNhapSauDieuChinh,
		    0 AS fHangMuaTruocDieuChinh,
			0 AS fHangMua,
			0 AS fHangMuaSauDieuChinh,
		    0 AS fPhanCapTruocDieuChinh,
			0 AS fPhanCap,
			0 AS fPhanCapSauDieuChinh,
			0 AS fDuPhong,
			cast(0x0 as uniqueidentifier) AS iID_CTDuToan_Nhan,
			0 AS iPhanCap,
			'' AS sGhiChu,
			3 AS RowType,
			cast(1 as bit) AS bEmpty,
			GetDate() AS dNgayTao,
			@sUserName AS sNguoiTao,
			GetDate() AS dNgaySua,
			@sUserName AS sNguoiSua,
			cast(0 as bit) as bHangChaDuToan
		FROM tblMlnsChild
		WHERE iID_MLNS NOT IN (SELECT iID_MLNS FROM tblRowChiTiet)
	)

	SELECT * FROM tblRowCha
	UNION ALL
	SELECT * FROM tblRowChiTietCha
	UNION ALL
	SELECT * FROM tblRowChiTiet
	UNION ALL
	SELECT * FROM tblRowChiTietEmpty
	order by sXauNoiMa, iID_MaDonVi, iRowType, sDotPhanBoTruoc desc
END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_luyke]    Script Date: 17/12/2021 8:09:04 AM ******/
SET ANSI_NULLS ON
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 1/4/2024 8:24:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop] @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50), 
  @IdChungTu nvarchar(100), 
  @NamLamViec int AS BEGIN INSERT INTO [dbo].BH_DTC_DieuChinhDuToanChi_ChiTiet (
    iID_BH_DTC_ChiTiet,
	iID_BH_DTC, 
    iID_MucLucNganSach,
	sM,
	sTM,
	sNoiDung,
	sXauNoiMa,
	iNamLamViec,
	iID_MaDonVi,
    fTienDuToanDuocGiao,
	fTienThucHien06ThangDauNam,
    fTienUocThucHien06ThangCuoiNam,
    fTienUocThucHienCaNam,
	fTienSoSanhTang, 
    fTienSoSanhGiam,
	dNgaySua,
	dNgayTao, 
    sNguoiSua,
	sNguoiTao
  ) 
SELECT 
  DISTINCT NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sM, 
  sTM, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec,
  iID_MaDonVi,
  sum(fTienDuToanDuocGiao), 
  sum(fTienThucHien06ThangDauNam), 
  sum(fTienUocThucHien06ThangCuoiNam), 
  sum(fTienUocThucHienCaNam), 
  sum(fTienSoSanhTang), 
  sum(fTienSoSanhGiam), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_DTC_DieuChinhDuToanChi_ChiTiet 
WHERE 
  iID_BH_DTC in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  sM, 
  sTM, 
  iID_MucLucNganSach, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec,
  iID_MaDonVi;
--danh dau chung tu da tong hop
update 
  BH_DTC_DieuChinhDuToanChi 
set 
  bDaTongHop = 1 
where 
  iID_BH_DTC in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) END;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_dutoan_index]    Script Date: 1/4/2024 8:24:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị dự toán điều chỉnh dự toán

-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_dutoan_index]
@NamLamViec int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 dcdt.iID_BH_DTC 
		, dcdt.sSoChungTu
		, dcdt.dNgayChungTu
		, dcdt.sSoQuyetDinh
		, dcdt.dNgayQuyetDinh
		, dcdt.iNamLamViec
		, dcdt.iID_DonVi
		, dcdt.iID_MaDonVi
		, dcdt.sMoTa
		, dcdt.sLNS
		, dcdt.iID_LoaiCap
		, dcdt.fTienDuToanDuocGiao
		, dcdt.fTienThucHien06ThangDauNam
		, dcdt.fTienUocThucHien06ThangCuoiNam
		, dcdt.fTienUocThucHienCaNam
		, dcdt.fTienSoSanhTang
		, dcdt.fTienSoSanhGiam
		, dcdt.sTongHop
		, dcdt.iID_TongHopID
		, dcdt.iLoaiTongHop
		, dcdt.dNgaySua
		, dcdt.dNgayTao
		, dcdt.sNguoiSua
		, dcdt.sNguoiTao
		, dcdt.dNgayTao
		, donVi.sTenDonVi
		, dcdt.iID_LoaiCap
		, dcdt.bIsKhoa
		, dcdt.bDaTongHop
		, dcdt.sMaLoaiChi
		, lc.sTenDanhMucLoaiChi
		-- Tong dự toán todo
	FROM BH_DTC_DieuChinhDuToanChi dcdt
	LEFT JOIN DonVi donVi
		ON dcdt.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = dcdt.iID_DonVi
	LEFT JOIN BH_DM_LoaiChi lc on dcdt.iID_LoaiCap=lc.iID and dcdt.iNamLamViec=lc.iNamLamViec
	where dcdt.iNamLamViec=@NamLamViec
	order by dcdt.dNgayChungTu
END


GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_phanbo_get_donvi]    Script Date: 1/4/2024 8:24:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_phanbo_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier,
@MaLoaichi nvarchar(50),
@SoQuyetDinh nvarchar(50),
@DNgayQuyetDinh nvarchar(50)
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_PhanBoDuToanChi chungtu
LEFT JOIN DonVi donvi ON donvi.iID_MaDonVi in (SELECT * FROM splitstring(chungtu.sID_MaDonVi))
WHERE chungtu.iNamChungTu = @NamLamViec
  AND chungtu.sID_MaDonVi <> ''
  AND chungtu.sID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.sSoQuyetDinh=@SoQuyetDinh
   AND CONVERT(nvarchar ,chungtu.dNgayQuyetDinh,103) = @DNgayQuyetDinh 
  --AND chungtu.iID_LoaiDanhMucChi=@IDLoaiChi
  AND chungtu.sMaLoaiChi=@MaLoaichi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]    Script Date: 1/4/2024 8:24:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	@SNgayQuyetDinh nvarchar(MAX),
	@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int 
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
and cast(ct.dNgayQuyetDinh as date) <= @SNgayQuyetDinh
and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;

With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi_theokhoi]    Script Date: 1/4/2024 8:24:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi_theokhoi]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	@SNgayQuyetDinh nvarchar(MAX),
	@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int 
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.sMaLoaiChi=@MaLoaiChi
and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))


select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, 0 fTienTroCapOmDau
	, 0 fTienTroCapThaiSan
	, 0 fTienTroCapTaiNan
	, 0 fTienTroCapHuuTri
	, 0 fTienTroCapPhucVien
	, 0 fTienTroCapXuatNgu
	, 0 fTienTroCapThoiViec
	, 0 fTienTroCapTuTuat
	, 1 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND(
ctct.sXauNoiMa like '9010001-010-011-0002%'
OR ctct.sXauNoiMa like '9010001-010-011-0001%'
OR ctct.sXauNoiMa like '9010001-010-011-0003%'
OR ctct.sXauNoiMa like '9010001-010-011-0004%'
OR ctct.sXauNoiMa like '9010001-010-011-0005%'
OR ctct.sXauNoiMa like '9010001-010-011-0006%'
OR ctct.sXauNoiMa like '9010001-010-011-0007%'
OR ctct.sXauNoiMa like '9010001-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi 

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN ISNULL(ctct.fTongTien,0) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
AND (

ctct.sXauNoiMa like '9010002-010-011-0001%'
OR ctct.sXauNoiMa like '9010002-010-011-0002%'
OR ctct.sXauNoiMa like '9010002-010-011-0003%'
OR ctct.sXauNoiMa like '9010002-010-011-0004%'
OR ctct.sXauNoiMa like '9010002-010-011-0005%'
OR ctct.sXauNoiMa like '9010002-010-011-0006%'
OR ctct.sXauNoiMa like '9010002-010-011-0007%'
OR ctct.sXauNoiMa like '9010002-010-011-0008%'
)
group by ctct.iID_DonVi, ctct.iID_MaDonVi

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;


With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position, iIdParent)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position, NEWID()
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position, pr.id
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort
		INTO #result 
from #tree 
--temp table sum chill
SELECT  iIdParent
		,SUM(fTienTroCapOmDau) fTienTroCapOmDau
		,SUM(fTienTroCapThaiSan) fTienTroCapThaiSan
		,SUM(fTienTroCapTaiNan) fTienTroCapTaiNan
		,SUM(fTienTroCapHuuTri) fTienTroCapHuuTri
		,SUM(fTienTroCapPhucVien) fTienTroCapPhucVien
		,SUM(fTienTroCapXuatNgu) fTienTroCapXuatNgu
		,SUM(fTienTroCapThoiViec) fTienTroCapThoiViec
		,SUM(fTienTroCapTuTuat) fTienTroCapTuTuat
		INTO #tempSumParent
FROM #result
WHERE IsHangCha = 0
Group by iIdParent

Update #result
SET iIdParent = null
where IsHangCha = 1;

select id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTaiNan ELSE rs.fTienTroCapTaiNan END) fTienTroCapTaiNan,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapHuuTri ELSE rs.fTienTroCapHuuTri END) fTienTroCapHuuTri,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapPhucVien ELSE rs.fTienTroCapPhucVien END) fTienTroCapPhucVien,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapXuatNgu ELSE rs.fTienTroCapXuatNgu END) fTienTroCapXuatNgu,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThoiViec ELSE rs.fTienTroCapThoiViec END) fTienTroCapThoiViec,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapTuTuat ELSE rs.fTienTroCapTuTuat END) fTienTroCapTuTuat,
			RowNumber,
			rs.position
			into #resultAllKhoi
from #result rs
LEFT JOIN #tempSumParent su ON rs.id = su.iIdParent
order by sort;


	--Mục A: Khối dự toán
	Select 
	newid() id,
	N'Đơn vị dự toán' sTenDonVi,
	'' iID_MaDonVi,
	N'A' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	 into #tempDonViDuToan

	--Mục B: Khối hạch toán
	Select 
	newid() id,
	N'Đơn vị hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	N'B' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempDonViHachToan

	---Khoi du toán
	Select 
	newid() id,
	N'Khối dự toán' sTenDonVi,
	'' iID_MaDonVi,
	'' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempKhoiDuToan

	---Khoi hach toan
	Select 
	newid() id,
	N'Khối hạch toán' sTenDonVi,
	'' iID_MaDonVi,
	'' STT,
	1 IsHangCha,
	0 fTienTroCapOmDau,
	0 fTienTroCapThaiSan,
	0 fTienTroCapTaiNan,
	0 fTienTroCapHuuTri,
	0 fTienTroCapPhucVien,
	0 fTienTroCapXuatNgu,
	0 fTienTroCapThoiViec,
	0 fTienTroCapTuTuat,
	0 RowNumber,
	N'' position
	into #tempKhoiHachToan


--Mục B: Khối hạch toán --> Hiển thị các đơn vị có iKhoi = 1: Khối doanh nghiệp
	SELECT B.* into #tempDvKDN
	from DonVi A
LEFT JOIN #resultAllKhoi B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=1
 order by B.position


 -- Create bang tam stt cho Mục B: Khối hạch toán
 SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDN.sTenDonVi) AS VARCHAR(6))  STT,
 #tempDvKDN.sTenDonVi,
 #tempDvKDN.iID_MaDonVi
 INTO #tempSTTKDN
 FROM #tempDvKDN
 WHERE #tempDvKDN.IsHangCha=1


 --- update stt cho Mục B: Khối hạch toán
 UPDATE #tempDvKDN SET STT= A.STT
 FROM #tempSTTKDN A
 WHERE #tempDvKDN.sTenDonVi=A.sTenDonVi
	 and  #tempDvKDN.iID_MaDonVi=A.iID_MaDonVi

 --Mục A: Khối dự toán --> hiển thị các đơn vị có iKhoi = 2: Khối dự toán
	select B.* 
	into #tempDvKDT 
	from DonVi A
 LEFT JOIN #resultAllKhoi B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=2
 order by B.position

  -- Create bang tam stt cho Mục A: Khối dự toán
	SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDT.sTenDonVi) AS VARCHAR(6))  STT,
	#tempDvKDT.sTenDonVi,
	#tempDvKDT.iID_MaDonVi
	INTO #tempSTTKDT
	FROM #tempDvKDT
	WHERE #tempDvKDT.IsHangCha=1

 --- update stt cho Mục A: Khối dự toán
	UPDATE #tempDvKDT SET STT= A.STT
 FROM #tempSTTKDT A
 WHERE #tempDvKDT.sTenDonVi=A.sTenDonVi
	 and  #tempDvKDT.iID_MaDonVi=A.iID_MaDonVi


 --- Create data khoi du toan doanh nghiep ( Mục B: Khối hạch toán )
	SELECT 2 iLoai,* INTO #tempKhoiDN
from
(
	SELECT * FROM #tempDonViHachToan
	UNION ALL 
	SELECT * FROM #tempKhoiDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiHachToan
	UNION ALL 
	SELECT iD,
	sTenDonVi,
	iID_MaDonVi,
	convert(nvarchar(6), STT),
	IsHangCha,
	fTienTroCapOmDau,
	fTienTroCapThaiSan,
	fTienTroCapTaiNan,
	fTienTroCapHuuTri,
	fTienTroCapPhucVien,
	fTienTroCapXuatNgu,
	fTienTroCapThoiViec,
	fTienTroCapTuTuat, 
	RowNumber,
	position FROM #tempDvKDN
	
) tempRESULTKhoiDonViHoachToan order by  tempRESULTKhoiDonViHoachToan.position

 --- Create data khoi du toan  (Mục A: Khối dự toán)
	SELECT 1 iLoai, * INTO #tempKhoiDT
from
(
	SELECT * FROM #tempDonViDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiDuToan
	UNION ALL 
	SELECT * FROM #tempKhoiHachToan
	UNION ALL 
	SELECT iD,
	sTenDonVi,
	iID_MaDonVi,
	convert(nvarchar(6), STT),
	IsHangCha,
	fTienTroCapOmDau,
	fTienTroCapThaiSan,
	fTienTroCapTaiNan,
	fTienTroCapHuuTri,
	fTienTroCapPhucVien,
	fTienTroCapXuatNgu,
	fTienTroCapThoiViec,
	fTienTroCapTuTuat, 
	RowNumber,
	position FROM #tempDvKDT

)  tempKhoiDonViDuToan 	order by  tempKhoiDonViDuToan.position



--- Mục B: Khối hạch toán
--- Mục B: Sum khoi du toan cua khoi hach toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiDuToanOfKhoiDN 
		from  #tempKhoiDN
where sTenDonVi = N'Khối dự toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Mục B: Sum khoi du toan cua khoi hach toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiHachToanOfKhoiDN 
		from  #tempKhoiDN
where sTenDonVi = N'Khối hạch toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

	--- Mục B: Sum Don vi theo khoi hach toan(khoi doanh nghiep) 
	select 
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalForDonViKhoiDN 
		from  #tempKhoiDN
where
	iID_MaDonVi <>''
	and iID_MaDonVi is not null
	and STT is not null
	and IsHangCha <> 0

--- update Total Mục B: Đon vi khoi doanh nghiep
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalForDonViKhoiDN A
	where #tempKhoiDN.STT=N'B'
		and #tempKhoiDN.sTenDonVi=N'Đơn vị hạch toán'	
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is  null)


--- update Total Mục B: khoi du toan
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiDuToanOfKhoiDN A
	where #tempKhoiDN.sTenDonVi=N'Khối dự toán'	
		and #tempKhoiDN.STT=N''
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is null)

--- update Total Mục B:khoi hoach toan
	update #tempKhoiDN  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiHachToanOfKhoiDN A
	where #tempKhoiDN.sTenDonVi=N'Khối hạch toán'
		and #tempKhoiDN.STT=N''
		and (#tempKhoiDN.iID_MaDonVi =''
		or #tempKhoiDN.iID_MaDonVi is null)


--- Mục A: Khối dự toán
--- Sum khoi du toan cua khoi du toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiDuToanOfKhoiDT
		from  #tempKhoiDT
where sTenDonVi = N'Khối dự toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Sum khoi hach toan cua Khoi du toan
	select sTenDonVi,
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalKhoiHachToanOfKhoiDT 
		from  #tempKhoiDT
where sTenDonVi = N'Khối hạch toán'
	and (iID_MaDonVi <>''
	or iID_MaDonVi is not null)
	GROUP BY sTenDonVi

---  Sum Don vi theo khoi du toan(khoi du toan) 
	select 
		Sum(fTienTroCapOmDau) fTienTroCapOmDau,
		Sum(fTienTroCapThaiSan) fTienTroCapThaiSan,
		Sum(fTienTroCapTaiNan) fTienTroCapTaiNan,
		Sum(fTienTroCapHuuTri) fTienTroCapHuuTri,
		Sum(fTienTroCapPhucVien) fTienTroCapPhucVien,
		Sum(fTienTroCapXuatNgu) fTienTroCapXuatNgu,
		Sum(fTienTroCapThoiViec) fTienTroCapThoiViec,
		Sum(fTienTroCapTuTuat) fTienTroCapTuTuat
		into #SumTotalForDonViKhoiDT 
		from  #tempKhoiDT
where
	iID_MaDonVi <>''
	and iID_MaDonVi is not null
	and STT is not null
	and IsHangCha <> 0


--- update Total Mục B: Don vị khối dự toán
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalForDonViKhoiDT A
	where #tempKhoiDT.STT=N'A'
		and #tempKhoiDT.sTenDonVi=N'Đơn vị dự toán'	
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is  null)

--- update Total Mục B: khoi du toan
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiDuToanOfKhoiDT A
	where #tempKhoiDT.sTenDonVi=N'Khối dự toán'	
		and #tempKhoiDT.STT=N''
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is null)

--- update Total Mục B:khoi hoach toan
	update #tempKhoiDT  set fTienTroCapOmDau=A.fTienTroCapOmDau 
						, fTienTroCapThaiSan=A.fTienTroCapThaiSan
						, fTienTroCapTaiNan=A.fTienTroCapTaiNan
						, fTienTroCapHuuTri=A.fTienTroCapHuuTri
						, fTienTroCapPhucVien=A.fTienTroCapPhucVien
						, fTienTroCapXuatNgu=A.fTienTroCapXuatNgu
						, fTienTroCapThoiViec=A.fTienTroCapThoiViec
						, fTienTroCapTuTuat=A.fTienTroCapTuTuat
	from #SumTotalKhoiHachToanOfKhoiDT A
	where #tempKhoiDT.sTenDonVi=N'Khối hạch toán'
		and #tempKhoiDT.STT=N''
		and (#tempKhoiDT.iID_MaDonVi =''
		or #tempKhoiDT.iID_MaDonVi is null)

SELECT  * 
from
(
	SELECT * FROM #tempKhoiDT
	UNION ALL 
	SELECT * FROM #tempKhoiDN
	
) tempRESULTALL order by iLoai, position;


DROP TABLE #tempDonViDuToan
DROP TABLE #tempDonViHachToan
DROP TABLE #tempKhoiDuToan
DROP TABLE #tempKhoiHachToan
DROP TABLE #tempDvKDN
DROP TABLE #tempDvKDT
DROP TABLE #tempSTTKDN
DROP TABLE #tempSTTKDT
DROP TABLE #SumTotalKhoiDuToanOfKhoiDN
DROP TABLE #SumTotalKhoiHachToanOfKhoiDN
DROP TABLE #SumTotalForDonViKhoiDN
DROP TABLE #SumTotalKhoiDuToanOfKhoiDT
DROP TABLE #SumTotalKhoiHachToanOfKhoiDT
DROP TABLE #SumTotalForDonViKhoiDT
DROP TABLE #tempKhoiDN
DROP TABLE #tempKhoiDT
DROP TABLE #resultAllKhoi
DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
DROP TABLE #result
DROP TABLE #tempSumParent

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]    Script Date: 1/4/2024 8:24:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@SNgayQuyetDinh nvarchar(MAX),
	@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, SUM(ctct.fTongTien) as fTongTienDuToan
	, 0 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi 

select * from #temp1;

DROP TABLE #tempall
DROP TABLE #temp1
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 1/4/2024 8:24:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi_theokhoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@SNgayQuyetDinh nvarchar(MAX),
	@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, SUM(ctct.fTongTien) as fTongTienDuToan
	, 0 IsHangCha
	, 0 RowNumber
	into #temp1
from 
#tempall ctct
left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and
dv.iNamLamViec=@INamLamViec
and
ctct.iNamLamViec=@INamLamViec
group by  dv.iID_MaDonVi, dv.sTenDonVi 

select 
N'A' STT,
N'Đơn vị dự toán' sTenDonVi,
'' iID_MaDonVi,
0 fTongTienDuToan,
1 IsHangCha,
0 RowNumber
into #tempDonViDuToan

select 
N'B' STT,
N'Đơn vị hạch toán' sTenDonVi,
'' iID_MaDonVi,
0 fTongTienDuToan,
1 IsHangCha,
0 RowNumber
into #tempDonViHachToan

------ create data don vi du toan
	SELECT B.* into #tempDvKDT
	from DonVi A
LEFT JOIN #temp1 B ON A.iID_MaDonVi=B.iID_MaDonVi
where A.iNamLamViec=@INamLamViec
 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
 and A.iKhoi=2

------ Create table Stt
	Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT,
		dv.iID_MaDonVi,
		dv.sTenDonVi
		into #tempSttKDT
		From #tempDvKDT dv
------ Update stt 
	Update #tempDvKDT set #tempDvKDT.STT=A.STT
		From #tempSttKDT A
		where #tempDvKDT.iID_MaDonVi=A.iID_MaDonVi
			and #tempDvKDT.sTenDonVi=A.sTenDonVi
------ create data don vi hach toan
	SELECT B.* into #tempDvKHT
	From DonVi A
	LEFT JOIN #temp1 B ON A.iID_MaDonVi=B.iID_MaDonVi
	where A.iNamLamViec=@INamLamViec
	 and A.iiD_MaDonVi in  (SELECT * FROM splitstring(@IdMaDonVi))
	 and A.iKhoi=1

 ------ Create table Stt
	Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT,
		dv.iID_MaDonVi,
		dv.sTenDonVi
		into #tempSttKHT
		From #tempDvKHT dv
------ Update stt 
	Update #tempDvKHT set #tempDvKHT.STT=A.STT
		From #tempSttKHT A
		where #tempDvKHT.iID_MaDonVi=A.iID_MaDonVi
			and #tempDvKHT.sTenDonVi=A.sTenDonVi

 --- Create data merge don vi du toan
	SELECT  1 iLoai, * INTO #tempDataDVDT
	FROM
	(
		SELECT * FROM #tempDonViDuToan
		UNION ALL 
		SELECT * FROM #tempDvKDT
	)tempDataDVDT

	--- Tinh tong theo don vi du toan
	SELECT SUM(fTongTienDuToan) fTongTienDuToan
	INTO #SumTotalDuToan
	FROM #tempDvKDT
	--- update tong tien don vị du toan
	UPDATE #tempDataDVDT SET #tempDataDVDT.fTongTienDuToan=A.fTongTienDuToan
	FROM #SumTotalDuToan A
	WHERE #tempDataDVDT.sTenDonVi=N'Đơn vị dự toán' 
	AND #tempDataDVDT.STT=N'A'
	
	 --- Create data merge don vi hach toan
	SELECT  2 iLoai,* INTO #tempDataDVHT
	FROM
	(
		SELECT * FROM #tempDonViHachToan
		UNION ALL 
		SELECT * FROM #tempDvKHT
	)tempDataDVHT

	--- Tinh tong theo don vi hach toan
	SELECT SUM(fTongTienDuToan) fTongTienDuToan
	INTO #SumTotalHachToan
	FROM #tempDvKHT
	--- update tong tien don vị hach toan
	UPDATE #tempDataDVHT SET #tempDataDVHT.fTongTienDuToan=A.fTongTienDuToan
	FROM #SumTotalHachToan A
	WHERE #tempDataDVHT.sTenDonVi=N'Đơn vị hạch toán'
	AND #tempDataDVHT.STT=N'B'

	--- create merge don vi du toan voi don vi hach toan vào
	SELECT * 
	FROM
	(
		SELECT * FROM #tempDataDVDT
		UNION ALL 
		SELECT * FROM #tempDataDVHT
	)tempDataAll



DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #tempDonViDuToan
DROP TABLE #tempDonViHachToan
DROP TABLE #tempDvKDT
DROP TABLE #tempDvKHT
DROP TABLE #tempSttKDT
DROP TABLE #tempSttKHT
DROP TABLE #SumTotalDuToan
DROP TABLE #SumTotalHachToan
DROP TABLE #tempDataDVDT
DROP TABLE #tempDataDVHT
END
;
;
;
GO
