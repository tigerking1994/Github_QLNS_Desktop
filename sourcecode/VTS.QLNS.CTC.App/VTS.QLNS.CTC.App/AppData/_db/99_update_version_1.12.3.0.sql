/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 15/11/2022 6:22:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 15/11/2022 6:22:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 15/11/2022 6:22:25 PM ******/
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



	WITH tblLns AS (
		SELECT distinct sLNS FROM NS_DT_ChungTuChiTiet 
		WHERE iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)
	),
	tblIdMlns AS (
		SELECT iID_MLNS FROM NS_DT_ChungTuChiTiet 
		WHERE iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)
	),
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
	),
	-- lấy dữ liệu seting nhập từ bảng MLNS
	tblMlnsseting AS (
		SELECT sLNS, bTuChi, bHienVat, bHangNhap, bHangMua, bPhanCap, bDuPhong, bTonKho FROM tblMlns WHERE sL = ''
	),
	-- lấy dữ liệu MLNS cha
	tblMlnsParent AS (
		SELECT * FROM tblMlns WHERE bHangChaDuToan = 1
	),
	-- lấy dữ liệu MLNS con
	tblMlnsChild AS (
		SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, mlns.sLns, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, bHangChaDuToan,
			seting.bTuChi, seting.bHienVat, seting.bHangNhap, seting.bHangMua, seting.bPhanCap, seting.bDuPhong, seting.bTonKho
		FROM tblMlns mlns
		LEFT JOIN tblMlnsseting seting
		ON seting.sLNS = mlns.sLNS
		WHERE bHangChaDuToan = 0 and iID_MLNS in (SELECT * FROM tblIdMlns)
	),
	-- lấy ra đơn vị được chọn để phân bổ theo iID_ChungTu phân bổ
	tblDonViPhanBo AS (
		SELECT iID_MaDonVi AS MaDonVi, sTenDonVi
		FROM DonVi
		WHERE iNamLamViec = @iNamLamViec
			AND iID_MaDonVi IN (SELECT * FROM f_split(@sIdDonVi))
	),
	-- lấy ra số quyết định nhận được chọn để phân bổ theo iID_ChungTu phân bổ
	tblSoQuyetDinhNhan AS (
		SELECT 
			sSoQuyetDinh, 
			iID_DTChungTu AS idSoQuyetDinh 
		FROM NS_DT_ChungTu 
		WHERE iID_DTChungTu IN (
			SELECT iID_CTDuToan_Nhan 
			FROM NS_DT_Nhan_PhanBo_Map 
			WHERE iID_CTDuToan_PhanBo = @sChungTuId
		)
	),
	-- lấy dữ liệu nhận phân bổ theo so quyet dinh va don vi
	tblChungTuNhanPhanBo AS (
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
				ON dt.iID_DTChungTu = ctct.iID_DTChungTu
				WHERE dt.iID_DTChungTu IN ( SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId) 
		) vctpb

		GROUP BY vctpb.iID_MLNS,vctpb.sSoQuyetDinh, vctpb.idSoQuyetDinh
	),
	tblDaPhanBo AS (
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
				WHERE (iID_CTDuToan_Nhan <> '' and iID_CTDuToan_Nhan IN (SELECT idSoQuyetDinh FROM tblSoQuyetDinhNhan))
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
	),
	tblNhanPhanBo AS (

		SELECT iID_MLNS, sSoQuyetDinh, idSoQuyetDinh,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho
			FROM (
				SELECT 
					iID_MLNS, --sSoQuyetDinh, idSoQuyetDinh,
					
					(CASE WHEN @isQuanly=1 then sSoQuyetDinh ELSE '' end)AS  sSoQuyetDinh ,  --ct.sSoQuyetDinh, 
					(CASE WHEN @isQuanly=1 then idSoQuyetDinh ELSE CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,  --ct.iID_DTChungTu AS idSoQuyetDinh,
					fTuChi, 
					fHienVat,
					fDuPhong,
					fHangMua,
					fHangNhap,
					fPhanCap,
					fTonKho
				FROM (
				SELECT * FROM tblChungTuNhanPhanBo
				UNION ALL
				SELECT * FROM tblDaPhanBo
				) npb
			) vnhanPhanBo
		GROUP BY iID_MLNS, sSoQuyetDinh, idSoQuyetDinh
	),
	-- lấy ra dữ liệu nhận phân bổ (iRowType = 1)
	tblMlnsChildAndSqd AS (
		SELECT * FROM tblMlnsChild, tblSoQuyetDinhNhan
	),
	tblRowNhanPhanBo AS (
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
			CAST(c.idSoQuyetDinh AS NVARCHAR(MAX)) AS iID_CTDuToan_Nhan,
			'' AS sGhiChu,
			1 AS iRowType,
			CAST(1 AS bit) AS bHangChaDuToan,
			CAST(0 AS bit) AS IsEditTuChi,
			CAST(0 AS bit) AS IsEditHienVat,
			CAST(0 AS bit) AS IsEditHangNhap,
			CAST(0 AS bit) AS IsEditHangMua,
			CAST(0 AS bit) AS IsEditPhanCap,
			CAST(0 AS bit) AS IsEditDuPhong
		FROM tblMlnsChildAndSqd c
		LEFT JOIN tblNhanPhanBo p
		ON c.iID_MLNS = p.iID_MLNS and c.idSoQuyetDinh = p.idSoQuyetDinh
	),
	-- lấy ra dòng cha từ bảng tmpMlnsParent iRowType = 0
	tblRowCha AS (
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
			'' AS iID_CTDuToan_Nhan,
			'' AS sGhiChu,
			0 AS iRowType,
			bHangChaDuToan,
			CAST(0 AS bit) AS IsEditTuChi,
			CAST(0 AS bit) AS IsEditHienVat,
			CAST(0 AS bit) AS IsEditHangNhap,
			CAST(0 AS bit) AS IsEditHangMua,
			CAST(0 AS bit) AS IsEditPhanCap,
			CAST(0 AS bit) AS IsEditDuPhong
		FROM tblMlnsParent
	),
	-- lấy ra dòng cON từ bảng tmpMlnsChild và bảng NS_DT_ChungTuChiTiet iRowType = 3
	tblMlnsChildAndDvSqd AS (
		SELECT * FROM tblMlnsChild, tblDonViPhanBo, tblSoQuyetDinhNhan
	),
	tblRowChiTiet AS (
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
		    CAST(mlns.idSoQuyetDinh AS NVARCHAR(MAX)) AS iID_CTDuToan_Nhan,
		    ISNULL(ctct.sGhiChu, '') AS sGhiChu,
		    3 AS iRowType,
			mlns.bHangChaDuToan,
			ISNULL(mlns.bTuChi, CAST(0 AS bit)) AS IsEditTuChi,
			ISNULL(mlns.bHienVat, CAST(0 AS bit)) AS IsEditHienVat,
			ISNULL(mlns.bHangNhap, CAST(0 AS bit)) AS IsEditHangNhap,
			ISNULL(mlns.bHangMua, CAST(0 AS bit)) AS IsEditHangMua,
			ISNULL(mlns.bPhanCap, CAST(0 AS bit)) AS IsEditPhanCap,
			ISNULL(mlns.bDuPhong, CAST(0 AS bit)) AS IsEditDuPhong
		FROM tblMlnsChildAndDvSqd mlns
		LEFT JOIN
		(SELECT *
		   FROM NS_DT_ChungTuChiTiet
		   WHERE iID_DTChungTu = @sChungTuId) ctct 
		ON mlns.iID_MLNS = ctct.iID_MLNS and mlns.MaDonVi = ctct.iID_MaDonVi AND (mlns.idSoQuyetDinh = ctct.iID_CTDuToan_Nhan and ctct.iID_CTDuToan_Nhan <> '')
	),
	-- tính dữ liệu đã phân bổ
	tblTongRowChiTiet AS (
		SELECT 
			iID_MLNS, sSoQuyetDinh, idSoQuyetDinh,
			SUM(fTonKho) AS fTonKho,
			SUM(fTuChi) AS fTuChi,
			SUM(fHienVat) AS fHienVat,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fHangMua) AS fHangMua,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fDuPhong) AS fDuPhong
		FROM (SELECT * FROM tblRowChiTiet WHERE fTonKho <> 0 or fTuChi <> 0 or fHienVat <> 0 or fHangNhap <> 0 or fHangMua <> 0 or fPhanCap <> 0 or fDuPhong <> 0) ct
		group by iID_MLNS, sSoQuyetDinh, idSoQuyetDinh
	),
	tblRowConLai AS (
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
			CAST(npb.iID_CTDuToan_Nhan AS NVARCHAR(MAX)) AS iID_CTDuToan_Nhan,
			'' AS sGhiChu,
			2 AS iRowType,
			CAST(1 AS bit) AS bHangChaDuToan,
			CAST(0 AS bit) AS IsEditTuChi,
			CAST(0 AS bit) AS IsEditHienVat,
			CAST(0 AS bit) AS IsEditHangNhap,
			CAST(0 AS bit) AS IsEditHangMua,
			CAST(0 AS bit) AS IsEditPhanCap,
			CAST(0 AS bit) AS IsEditDuPhong
		FROM tblRowNhanPhanBo npb
		LEFT JOIN tblTongRowChiTieT rct
		ON npb.iID_MLNS = rct.iID_MLNS 
		and npb.idSoQuyetDinh = rct.idSoQuyetDinh
		
		WHERE 1 = @isQuanly
	)

	--SELECT * FROM tblMlnsChild; 
	--SELECT * FROM  tblDonViPhanBo; 
	--SELECT * FROM  tblSoQuyetDinhNhan;
	--SELECT * FROM tblMlnsChildAndDvSqd
	--SELECT * FROM tblTongRowChiTieT;
	--SELECT * FROM tblRowNhanPhanBo;

	
	SELECT * FROM tblRowCha
	
	UNION ALL 
	SELECT * FROM tblRowNhanPhanBo
	
	UNION ALL 
	SELECT * FROM tblRowConLai
	UNION ALL 
	SELECT * FROM tblRowChiTiet 

	ORDER BY sXauNoiMa
	

END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]    Script Date: 17/12/2021 8:08:45 AM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 15/11/2022 6:22:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@VoucherType int,
	@Date datetime,
	@Index int
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *, 
			   fTongTuChi + fTongHienVat + fTongPhanCap + fTongHangNhap + fTongHangMua + fTongDuPhong + fTongTonKho AS SoPhanBo
		FROM NS_DT_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iNamNganSach = @YearOfBudget 
			AND iID_MaNguonNganSach = @BudgetSource
			AND iLoai = @Type 
			AND iLoaiChungTu = @VoucherType
			AND (fTongTuChi <> 0 OR fTongHienVat <> 0 OR fTongPhanCap <> 0 OR fTongHangNhap <> 0 OR fTongHangMua <> 0 OR fTongDuPhong <> 0 OR fTongTonKho <> 0)
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(fTuChi) + SUM(fHienVat) + SUM(fPhanCap) + SUM(fHangNhap) + SUM(fHangMua) + SUM(fDuPhong) + SUM(fTonKho) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM NS_DT_ChungTuChiTiet dtctct
			 LEFT JOIN NS_DT_ChungTu dtct
			 ON dtctct.iID_DTChungTu = dtct.iID_DTChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork 
				   AND dtct.iLoai = 1
				   AND dtct.iLoaiChungTu = 1
				   AND (dNgayQuyetDinh IS NOT NULL AND (CAST(dNgayQuyetDinh AS DATE) < CAST(@Date AS DATE) OR (CAST(dNgayQuyetDinh AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex))) 
				   OR (dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND (CAST(dNgayChungTu AS DATE) < CAST(@Date AS DATE) OR (CAST(dNgayChungTu AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex)))
				   AND dtctct.iNamNganSach = @YearOfBudget 
				   AND dtctct.iID_MaNguonNganSach = @BudgetSource) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)

	SELECT 
		   npb.iID_DTChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.SoPhanBo, 0) AS SoPhanBo,
		   ISNULL(DaPhanBo, 0) AS DaPhanBo,
		   ISNULL(npb.SoPhanBo, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo 
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTChungTu = dpb.iID_CTDuToan_Nhan
	WHERE ISNULL(npb.SoPhanBo, 0) > ISNULL(DaPhanBo, 0)
	ORDER BY npb.sSoChungTu

END
;
;
;
;
;
GO
