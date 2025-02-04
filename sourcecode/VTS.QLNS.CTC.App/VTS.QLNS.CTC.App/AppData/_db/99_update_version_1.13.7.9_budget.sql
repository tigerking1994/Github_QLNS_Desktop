/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_quy_lns]    Script Date: 08/01/2024 3:31:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_tonghop_quy_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_tonghop_quy_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]    Script Date: 08/01/2024 3:31:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]    Script Date: 08/01/2024 3:31:01 PM ******/
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

	--select * from tblRowChiTietEmpty

	SELECT * FROM tblRowCha
	UNION ALL
	SELECT * FROM tblRowChiTietCha
	UNION ALL
	SELECT * FROM tblRowChiTiet
	--UNION ALL
	--SELECT * FROM tblRowChiTietEmpty
	order by sXauNoiMa, iID_MaDonVi, iRowType, sDotPhanBoTruoc desc
END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_luyke]    Script Date: 17/12/2021 8:09:04 AM ******/
SET ANSI_NULLS ON
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_quy_lns]    Script Date: 08/01/2024 3:31:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_tonghop_quy_lns]
	@YearOfWork int,
	@YearOfBudget nvarchar(100),
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@LoaiQuyetToan nvarchar(20),
	@UserName nvarchar(100)
AS
BEGIN

	DECLARE @CountDonViCha int;
	DECLARE @tblIdChungTuQT table (iID_QTChungTu uniqueidentifier)
	DECLARE @tblIdChungTuDT table (iID_DTChungTu uniqueidentifier)
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	IF @CountDonViCha = 0
		INSERT INTO @tblIdChungTuQT (iID_QTChungTu)
		SELECT ct.iID_QTChungTu
		FROM 
			(SELECT 
				* 
			FROM 
				NS_QT_ChungTu 
			WHERE 
				sLoai in (select * from f_split(@LoaiQuyetToan))
				AND iNamNganSach IN (SELECT * FROM f_split(@YearOfBudget))
				AND iNamLamViec = @YearOfWork
				AND iID_MaNguonNganSach = @BudgetSource
				AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
				AND EXISTS (SELECT * from f_split(sDSLNS) INTERSECT SELECT sLNS FROM NS_NguoiDung_LNS WHERE sMaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			) ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) nddv
		ON ct.iID_MaDonVi = nddv.iID_MaDonVi;
	ELSE
		INSERT INTO @tblIdChungTuQT (iID_QTChungTu)
		SELECT ct.iID_QTChungTu
		FROM 
			(SELECT 
				* 
			FROM 
				NS_QT_ChungTu 
			WHERE 
				sLoai in (select * from f_split(@LoaiQuyetToan))
				AND iNamNganSach IN (SELECT * FROM f_split(@YearOfBudget))
				AND iID_MaNguonNganSach = @BudgetSource
				AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
				AND EXISTS (SELECT * from f_split(sDSLNS) INTERSECT SELECT sLNS FROM NS_NguoiDung_LNS WHERE sMaNguoiDung = @UserName and iNamLamViec = @YearOfWork)
			) ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) nddv
		ON ct.iID_MaDonVi = nddv.iID_MaDonVi
		WHERE ((ct.iID_MaDonVi IS NULL OR ct.sNguoiTao <> @UserName) AND ct.bKhoa = 1) OR (ct.sNguoiTao = @UserName);

	INSERT INTO @tblIdChungTuDT (iID_DTChungTu)
	SELECT iID_DTChungTu
	FROM NS_DT_ChungTu
	WHERE iLoai in (0, 1, 2)
	  AND iNamNganSach IN (SELECT * FROM f_split(@YearOfBudget))
	  AND iNamLamViec = @YearOfWork
	  AND iID_MaNguonNganSach = @BudgetSource
	  AND ((@CountDonViCha = 0
			AND EXISTS
			  (SELECT *
			   FROM f_split(sDSLNS) INTERSECT SELECT sLNS
			   FROM NS_NguoiDung_LNS
			   WHERE sMaNguoiDung = @UserName
				 AND iNamLamViec = @YearOfWork)
			OR (@CountDonViCha <> 0)))
	  AND ((@CountDonViCha = 0
			AND EXISTS
			  (SELECT *
			   FROM f_split(sDSID_MaDonVi) INTERSECT SELECT iID_MaDonVi
			   FROM NguoiDung_DonVi
			   WHERE iID_MaNguoiDung = @UserName
				 AND iNamLamViec = @YearOfWork
				 AND iTrangThai = 1)
			OR (@CountDonViCha <> 0)))
	  AND ((@CountDonViCha = 0
			AND bKhoa = 1)
		   OR (@CountDonViCha <> 0))

	SELECT sLNS into #tblLNS
	FROM
	(
		SELECT sLNS
		FROM NS_QT_ChungTuChiTiet
		WHERE iID_QTChungTu IN (SELECT * FROM @tblIdChungTuQT)
			AND fTuChi_PheDuyet <> 0
			AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
		UNION
		SELECT sLNS
		FROM NS_DT_ChungTuChiTiet
		WHERE iID_DTChungTu IN (SELECT * FROM @tblIdChungTuDT)
			AND fTuChi <> 0
			AND iID_MaDonVi IN (SELECT * FROM f_split(@AgencyId))
			AND 
				(
					(@LoaiQuyetToan = '101' AND sLNS like '101%')
					OR (@LoaiQuyetToan = '1' AND sLNS like '1%' and sLNS not like '101%')
					OR (@LoaiQuyetToan = '2' AND sLNS like '2%')
					OR (@LoaiQuyetToan = '3' AND sLNS like '3%')
					OR (@LoaiQuyetToan = '4' AND sLNS like '4%')
					OR (@LoaiQuyetToan = '101,1,2,3,4' AND (sLNS like '1%' OR sLNS like '2%' OR sLNS like '3%' OR sLNS like '4%'))
				)
	) ctct
	
	SELECT sLNS as LNS, 
		sMoTa as MoTa, 
		iID_MLNS as MlnsId, 
		iID_MLNS_Cha as MlnsIdParent,
		iID_MaBQuanLy as IdPhongban
	FROM NS_MucLucNganSach 
	WHERE iNamLamViec = @YearOfWork
		AND sLNS in 
			(
				SELECT 
					DISTINCT VALUE
				FROM 
				(
					SELECT 
						CAST(LEFT(sLNS, 1) AS nvarchar(10)) sLNS1, 
						CAST(LEFT(sLNS, 3) AS nvarchar(10)) sLNS3, 
						CAST(LEFT(sLNS, 5) AS nvarchar(10)) sLNS5, 
						CAST(sLNS AS nvarchar(10)) sLNS 
					FROM
						#tblLNS
				) sLNS
				UNPIVOT
				(
					value
					FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
				) un
			)
			and sL = ''
	order by sXauNoiMa;
	drop table #tblLNS;
END
;
;
;
;
GO
