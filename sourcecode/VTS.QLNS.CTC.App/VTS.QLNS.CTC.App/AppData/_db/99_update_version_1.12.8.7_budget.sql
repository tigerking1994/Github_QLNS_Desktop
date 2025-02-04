/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi]    Script Date: 05/06/2023 5:51:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_origin]    Script Date: 05/06/2023 5:51:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet_origin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet_origin]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_luyke]    Script Date: 05/06/2023 5:51:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet_luyke]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet_luyke]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]    Script Date: 05/06/2023 5:51:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 05/06/2023 5:51:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 05/06/2023 5:51:39 PM ******/
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

	SELECT iID_MLNS INTO #tblIdMlns FROM NS_DT_ChungTuChiTiet 
	WHERE iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)


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
	WHERE bHangChaDuToan = 0 and iID_MLNS in (SELECT * FROM #tblIdMlns)

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
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]    Script Date: 05/06/2023 5:51:39 PM ******/
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
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_luyke]    Script Date: 05/06/2023 5:51:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet_luyke]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@IdDonVi nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @sChungTuId nvarchar(255) = @ChungTuId;
	DECLARE @sLNS nvarchar(max) = @LNS;
	DECLARE @sIdDonVi nvarchar(max) = @IdDonVi;
	DECLARE @iNamLamViec int = @NamLamViec;
	DECLARE @iNamNganSach int = @NamNganSach;
	DECLARE @iNguonNganSach int = @NguonNganSach;
	DECLARE @sUserName nvarchar(100) = @UserName;

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

	DECLARE @dNgayQuyetDinh as datetime;
	DECLARE @dNgayChungTu as datetime;
	DECLARE @dNgayPhanBo as datetime;
	DECLARE @iSoChungTuIndex as int;
	SELECT 
		@dNgayQuyetDinh = cast(dNgayQuyetDinh as date), 
		@dNgayChungTu = cast(dNgayChungTu as date),
		@iSoChungTuIndex = iSoChungTuIndex
	FROM NS_DT_ChungTu 
	WHERE iID_DTChungTu = @sChungTuId;

	IF @dNgayQuyetDinh IS NULL
		SELECT @dNgayPhanBo = @dNgayChungTu
	ELSE 
		SELECT @dNgayPhanBo = @dNgayQuyetDinh;



	WITH tblLns AS (
		select distinct sLNS from NS_DT_ChungTuChiTiet 
		where iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)
	),
	tblIdMlns AS (
		select iID_MLNS from NS_DT_ChungTuChiTiet 
		where iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)
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
																FOR col in (LNS1, LNS3, LNS5, LNS)) un)))
	),
	-- lấy dữ liệu setting nhập từ bảng MLNS
	tblMlnsSetting AS (
		SELECT sLNS, bTuChi, bHienVat, bHangNhap, bHangMua, bPhanCap, bDuPhong, bTonKho FROM tblMlns WHERE sL = ''
	),
	-- lấy dữ liệu MLNS cha
	tblMlnsParent AS (
		SELECT * FROM tblMlns WHERE bHangChaDuToan = 1
	),
	-- lấy dữ liệu MLNS con
	tblMlnsChild AS (
		SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, mlns.sLns, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, bHangChaDuToan,
			setting.bTuChi, setting.bHienVat, setting.bHangNhap, setting.bHangMua, setting.bPhanCap, setting.bDuPhong, setting.bTonKho
		FROM tblMlns mlns
		left join tblMlnsSetting setting
		on setting.sLNS = mlns.sLNS
		WHERE bHangChaDuToan = 0 and iID_MLNS in (select * from tblIdMlns)
	),
	-- lấy ra đơn vị được chọn để phân bổ theo iID_ChungTu phân bổ
	tblDonViPhanBo AS (
		SELECT iID_MaDonVi as MaDonVi, sTenDonVi
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
	tblSoQuyetDinhNhanLuyKe AS (
		SELECT
			distinct
			STUFF((
				SELECT ', ' + cast([sSoQuyetDinh] as nvarchar(100))
				FROM tblSoQuyetDinhNhan temp
				order by idSoQuyetDinh
				FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
			  ,1,2,'') AS sSoQuyetDinh,
			STUFF((
				SELECT ', ' + cast([idSoQuyetDinh] as nvarchar(100))
				FROM tblSoQuyetDinhNhan temp
				order by idSoQuyetDinh
				FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
			  ,1,2,'') AS idSoQuyetDinh
		FROM
			tblSoQuyetDinhNhan sqdNhan
			
	),
	-- lấy dữ liệu nhận phân bổ theo so quyet dinh va don vi
	tmpTblChungTuNhanPhanBo AS (
		SELECT ctct.*
		FROM NS_DT_ChungTuChiTiet ctct
		INNER JOIN NS_DT_ChungTu dt
		ON dt.iID_DTChungTu = ctct.iID_DTChungTu
		WHERE dt.iID_DTChungTu IN (
			SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId
		)
	),
	tblChungTuNhanPhanBo AS (
		SELECT 
			iID_MLNS,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho,
			STUFF((
				SELECT ', ' + cast([iID_DTChungTu] as nvarchar(100))
				FROM tmpTblChungTuNhanPhanBo temp
				WHERE (temp.iID_MLNS = nhanPb.iID_MLNS) 
				order by iID_DTChungTu
				FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
			  ,1,2,'') AS idSoQuyetDinh
		from tmpTblChungTuNhanPhanBo nhanPb
		GROUP BY iID_MLNS
	),
	tmpTblDaPhanBo AS (
		SELECT
			ctct.*
		FROM NS_DT_ChungTuChiTiet ctct
		LEFT JOIN NS_DT_ChungTu ct
		ON ctct.iID_CTDuToan_Nhan = ct.iID_DTChungTu
		LEFT JOIN NS_DT_ChungTu dtct
		ON ctct.iID_DTChungTu = dtct.iID_DTChungTu
		WHERE (iID_CTDuToan_Nhan is not null AND iID_CTDuToan_Nhan IN (SELECT idSoQuyetDinh FROM tblSoQuyetDinhNhanLuyKe))
		AND (
			(dtct.dNgayQuyetDinh IS NOT NULL 
			AND 
			(
				(CAST(dtct.dNgayQuyetDinh AS DATE) = cast(@dNgayPhanBo AS DATE) AND dtct.iSoChungTuIndex < @iSoChungTuIndex)
				OR 
				(CAST(dtct.dNgayQuyetDinh AS DATE) < cast(@dNgayPhanBo AS DATE))
			))
			OR 
			(dtct.dNgayQuyetDinh IS NULL 
			AND 
			(
				CAST(dtct.dNgayChungTu AS DATE) <= cast(@dNgayPhanBo AS DATE) AND dtct.iSoChungTuIndex < @iSoChungTuIndex)
				OR 
				(CAST(dtct.dNgayChungTu AS DATE) < cast(@dNgayPhanBo AS DATE))
			)
		)
		AND ctct.iID_DTChungTu <> @sChungTuId
	),
	tblDaPhanBo AS (
		SELECT 
			iID_MLNS,
			0 - SUM(fTuChi) AS fTuChi, 
			0 - SUM(fHienVat) AS fHienVat,
			0 - SUM(fDuPhong) AS fDuPhong,
			0 - SUM(fHangMua) AS fHangMua,
			0 - SUM(fHangNhap) AS fHangNhap,
			0 - SUM(fPhanCap) AS fPhanCap,
			0 - SUM(fTonKho) AS fTonKho,
			iID_CTDuToan_Nhan AS idSoQuyetDinh
		FROM tmpTblDaPhanBo daPb
		GROUP BY iID_MLNS, iID_CTDuToan_Nhan
	),
	tblNhanPhanBo AS (
		SELECT 
			iID_MLNS, 
			idSoQuyetDinh,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho
		FROM (
		SELECT * FROM tblChungTuNhanPhanBo
		UNION ALL
		SELECT * FROM tblDaPhanBo
		) npb
		GROUP BY iID_MLNS, idSoQuyetDinh
	),
	-- lấy ra dữ liệu nhận phân bổ (iRowType = 1)
	tblMlnsChildAndSqd AS (
		SELECT * FROM tblMlnsChild, tblSoQuyetDinhNhanLuyKe
	),
	tblRowNhanPhanBo AS (
		SELECT 
			NEWID() AS iID_DTCTChiTiet,
			CAST(0x0 AS uniqueidentifier) AS iID_DTChungTu,
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
			c.sSoQuyetDinh,
			isnull(p.fTonKho, 0) AS fTonKho,
		    isnull(p.fTuChi, 0) AS fTuChi,
		    isnull(p.fHienVat, 0) AS fHienVat,
		    isnull(p.fHangNhap, 0) AS fHangNhap,
		    isnull(p.fHangMua, 0) AS fHangMua,
		    isnull(p.fPhanCap, 0) AS fPhanCap,
		    isnull(p.fDuPhong, 0) AS fDuPhong,
			c.idSoQuyetDinh AS iID_CTDuToan_Nhan,
			'' AS sGhiChu,
			1 AS iRowType,
			cast(1 as bit) as bHangChaDuToan,
			cast(0 as bit) as IsEditTuChi,
			cast(0 as bit) as IsEditHienVat,
			cast(0 as bit) as IsEditHangNhap,
			cast(0 as bit) as IsEditHangMua,
			cast(0 as bit) as IsEditPhanCap,
			cast(0 as bit) as IsEditDuPhong
		FROM tblMlnsChildAndSqd c
		LEFT JOIN tblNhanPhanBo p
		ON c.iID_MLNS = p.iID_MLNS and c.idSoQuyetDinh = p.idSoQuyetDinh
	),
	-- lấy ra dòng cha từ bảng tmpMlnsParent iRowType = 0
	tblRowCha AS (
		SELECT 
			newid() AS iID_DTCTChiTiet,
			cASt(0x0 AS uniqueidentifier) AS iID_DTChungTu,
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
			cast(0 as bit) as IsEditTuChi,
			cast(0 as bit) as IsEditHienVat,
			cast(0 as bit) as IsEditHangNhap,
			cast(0 as bit) as IsEditHangMua,
			cast(0 as bit) as IsEditPhanCap,
			cast(0 as bit) as IsEditDuPhong
		FROM tblMlnsParent
	),
	-- lấy ra dòng cON từ bảng tmpMlnsChild và bảng NS_DT_ChungTuChiTiet iRowType = 3
	tblMlnsChildAndDvSqd AS (
		SELECT * FROM tblMlnsChild, tblDonViPhanBo, tblSoQuyetDinhNhanLuyKe
	),
	tblRowChiTiet AS (
		SELECT 
			isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
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
			mlns.MaDonVi + ' - ' + mlns.sTenDonVi as sTenDonVi,
		    mlns.sSoQuyetDinh,
		    isnull(ctct.fTonKho, 0) AS fTonKho,
		    isnull(ctct.fTuChi, 0) AS fTuChi,
		    isnull(ctct.fHienVat, 0) AS fHienVat,
		    isnull(ctct.fHangNhap, 0) AS fHangNhap,
		    isnull(ctct.fHangMua, 0) AS fHangMua,
		    isnull(ctct.fPhanCap, 0) AS fPhanCap,
		    isnull(ctct.fDuPhong, 0) AS fDuPhong,
		    mlns.idSoQuyetDinh as iID_CTDuToan_Nhan,
		    isnull(ctct.sGhiChu, '') AS sGhiChu,
		    3 AS iRowType,
			mlns.bHangChaDuToan,
			isnull(mlns.bTuChi, cast(0 as bit)) as IsEditTuChi,
			isnull(mlns.bHienVat, cast(0 as bit)) as IsEditHienVat,
			isnull(mlns.bHangNhap, cast(0 as bit)) as IsEditHangNhap,
			isnull(mlns.bHangMua, cast(0 as bit)) as IsEditHangMua,
			isnull(mlns.bPhanCap, cast(0 as bit)) as IsEditPhanCap,
			isnull(mlns.bDuPhong, cast(0 as bit)) as IsEditDuPhong
		FROM tblMlnsChildAndDvSqd mlns
		LEFT JOIN
		(SELECT *
		   FROM NS_DT_ChungTuChiTiet
		   WHERE iNamLamViec = @iNamLamViec
			 AND iNamNganSach = @iNamNganSach
			 AND iID_MaNguonNganSach = @iNguonNganSach
			 AND iPhanCap = 1
			 AND iID_DTChungTu = @sChungTuId) ctct 
		ON mlns.iID_MLNS = ctct.iID_MLNS and mlns.MaDonVi = ctct.iID_MaDonVi and (mlns.idSoQuyetDinh = ctct.iID_CTDuToan_Nhan and ctct.iID_CTDuToan_Nhan is not null)
	),
	-- tính dữ liệu đã phân bổ
	tblTongRowChiTiet AS (
		SELECT 
			iID_MLNS, sSoQuyetDinh,
			sum(fTonKho) AS fTonKho,
			sum(fTuChi) AS fTuChi,
			sum(fHienVat) AS fHienVat,
			sum(fHangNhap) AS fHangNhap,
			sum(fHangMua) AS fHangMua,
			sum(fPhanCap) AS fPhanCap,
			sum(fDuPhong) AS fDuPhong
		FROM (select * from tblRowChiTiet where fTonKho <> 0 or fTuChi <> 0 or fHienVat <> 0 or fHangNhap <> 0 or fHangMua <> 0 or fPhanCap <> 0 or fDuPhong <> 0) ct
		group by iID_MLNS, sSoQuyetDinh
	),
	tblRowConLai AS (
		SELECT 
			newid() AS iID_DTCTChiTiet,
			cast(0x0 AS uniqueidentifier) AS iID_DTChungTu,
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
			npb.sMoTa,
			'1' AS bHangCha,
			'' AS iID_MaDonVi,
			'' AS sTenDonVi,
			npb.sSoQuyetDinh,
			npb.fTonKho - rct.fTonKho AS fTonKho,
		    npb.fTuChi - rct.fTuChi AS fTuChi,
		    npb.fHienVat - rct.fHienVat AS fHienVat,
		    npb.fHangNhap - rct.fHangNhap AS fHangNhap,
		    npb.fHangMua - rct.fHangMua AS fHangMua,
		    npb.fPhanCap - rct.fPhanCap AS fPhanCap,
		    npb.fDuPhong - rct.fDuPhong AS fDuPhong,
			npb.iID_CTDuToan_Nhan AS iID_CTDuToan_Nhan,
			'' AS sGhiChu,
			2 AS iRowType,
			cast(1 as bit) as bHangChaDuToan,
			cast(0 as bit) as IsEditTuChi,
			cast(0 as bit) as IsEditHienVat,
			cast(0 as bit) as IsEditHangNhap,
			cast(0 as bit) as IsEditHangMua,
			cast(0 as bit) as IsEditPhanCap,
			cast(0 as bit) as IsEditDuPhong
		FROM tblRowNhanPhanBo npb
		LEFT JOIN tblTongRowChiTieT rct
		ON npb.iID_MLNS = rct.iID_MLNS and npb.sSoQuyetDinh = rct.sSoQuyetDinh
	)
	SELECT * FROM tblRowCha
	UNION ALL 
	SELECT * FROM tblRowNhanPhanBo
	UNION ALL 
	SELECT * FROM tblRowConLai
	UNION ALL 
	SELECT * FROM tblRowChiTiet 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_origin]    Script Date: 05/06/2023 5:51:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet_origin]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@IdDonVi nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @sChungTuId nvarchar(255) = @ChungTuId;
	DECLARE @sLNS nvarchar(max) = @LNS;
	DECLARE @sIdDonVi nvarchar(max) = @IdDonVi;
	DECLARE @iNamLamViec int = @NamLamViec;
	DECLARE @iNamNganSach int = @NamNganSach;
	DECLARE @iNguonNganSach int = @NguonNganSach;
	DECLARE @sUserName nvarchar(100) = @UserName;

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

	DECLARE @dNgayQuyetDinh as datetime;
	DECLARE @dNgayChungTu as datetime;
	DECLARE @dNgayPhanBo as datetime;
	DECLARE @iSoChungTuIndex as int;
	SELECT 
		@dNgayQuyetDinh = cast(dNgayQuyetDinh as date), 
		@dNgayChungTu = cast(dNgayChungTu as date),
		@iSoChungTuIndex = iSoChungTuIndex
	FROM NS_DT_ChungTu 
	WHERE iID_DTChungTu = @sChungTuId;

	IF @dNgayQuyetDinh IS NULL
		SELECT @dNgayPhanBo = @dNgayChungTu
	ELSE 
		SELECT @dNgayPhanBo = @dNgayQuyetDinh;



	WITH tblLns AS (
		select distinct sLNS from NS_DT_ChungTuChiTiet 
		where iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)
	),
	tblIdMlns AS (
		select iID_MLNS from NS_DT_ChungTuChiTiet 
		where iID_DTChungTu in (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId)
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
																FOR col in (LNS1, LNS3, LNS5, LNS)) un)))
	),
	-- lấy dữ liệu setting nhập từ bảng MLNS
	tblMlnsSetting AS (
		SELECT sLNS, bTuChi, bHienVat, bHangNhap, bHangMua, bPhanCap, bDuPhong, bTonKho FROM tblMlns WHERE sL = ''
	),
	-- lấy dữ liệu MLNS cha
	tblMlnsParent AS (
		SELECT * FROM tblMlns WHERE bHangChaDuToan = 1
	),
	-- lấy dữ liệu MLNS con
	tblMlnsChild AS (
		SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, mlns.sLns, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, bHangChaDuToan,
			setting.bTuChi, setting.bHienVat, setting.bHangNhap, setting.bHangMua, setting.bPhanCap, setting.bDuPhong, setting.bTonKho
		FROM tblMlns mlns
		left join tblMlnsSetting setting
		on setting.sLNS = mlns.sLNS
		WHERE bHangChaDuToan = 0 and iID_MLNS in (select * from tblIdMlns)
	),
	-- lấy ra đơn vị được chọn để phân bổ theo iID_ChungTu phân bổ
	tblDonViPhanBo AS (
		SELECT iID_MaDonVi as MaDonVi, sTenDonVi
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
		SELECT 
			ctct.iID_MLNS, 
			dt.sSoQuyetDinh, 
			dt.iID_DTChungTu AS idSoQuyetDinh,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho
		FROM NS_DT_ChungTuChiTiet ctct
		INNER JOIN NS_DT_ChungTu dt
		ON dt.iID_DTChungTu = ctct.iID_DTChungTu
		WHERE dt.iID_DTChungTu IN (
			SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @sChungTuId
		)
		GROUP BY ctct.iID_MLNS, dt.sSoQuyetDinh, dt.iID_DTChungTu
	),
	tblDaPhanBo AS (
		SELECT 
			ctct.iID_MLNS, 
			ct.sSoQuyetDinh, 
			ct.iID_DTChungTu AS idSoQuyetDinh,
			0 - SUM(fTuChi) AS fTuChi, 
			0 - SUM(fHienVat) AS fHienVat,
			0 - SUM(fDuPhong) AS fDuPhong,
			0 - SUM(fHangMua) AS fHangMua,
			0 - SUM(fHangNhap) AS fHangNhap,
			0 - SUM(fPhanCap) AS fPhanCap,
			0 - SUM(fTonKho) AS fTonKho
		FROM NS_DT_ChungTuChiTiet ctct
		LEFT JOIN NS_DT_ChungTu ct
		ON ctct.iID_CTDuToan_Nhan = ct.iID_DTChungTu
		LEFT JOIN NS_DT_ChungTu dtct
		ON ctct.iID_DTChungTu = dtct.iID_DTChungTu
		WHERE (iID_CTDuToan_Nhan is not null and iID_CTDuToan_Nhan IN (SELECT idSoQuyetDinh FROM tblSoQuyetDinhNhan))
		AND (
			(dtct.dNgayQuyetDinh IS NOT NULL 
			AND 
			(
				(CAST(dtct.dNgayQuyetDinh AS DATE) = cast(@dNgayPhanBo AS DATE) AND dtct.iSoChungTuIndex < @iSoChungTuIndex)
				OR 
				(CAST(dtct.dNgayQuyetDinh AS DATE) < cast(@dNgayPhanBo AS DATE))
			))
			OR 
			(dtct.dNgayQuyetDinh IS NULL 
			AND 
			(
				CAST(dtct.dNgayChungTu AS DATE) <= cast(@dNgayPhanBo AS DATE) AND dtct.iSoChungTuIndex < @iSoChungTuIndex)
				OR 
				(CAST(dtct.dNgayChungTu AS DATE) < cast(@dNgayPhanBo AS DATE))
			)
		)
		AND ctct.iID_DTChungTu <> @sChungTuId
		GROUP BY ctct.iID_MLNS, ct.sSoQuyetDinh, ct.iID_DTChungTu
	),
	tblNhanPhanBo AS (
		SELECT 
			iID_MLNS, sSoQuyetDinh, idSoQuyetDinh,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho
		FROM (
		SELECT * FROM tblChungTuNhanPhanBo
		UNION ALL
		SELECT * FROM tblDaPhanBo
		) npb
		GROUP BY iID_MLNS, sSoQuyetDinh, idSoQuyetDinh
	),
	-- lấy ra dữ liệu nhận phân bổ (iRowType = 1)
	tblMlnsChildAndSqd AS (
		SELECT * FROM tblMlnsChild, tblSoQuyetDinhNhan
	),
	tblRowNhanPhanBo AS (
		SELECT 
			NEWID() AS iID_DTCTChiTiet,
			CAST(0x0 AS uniqueidentifier) AS iID_DTChungTu,
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
			c.sSoQuyetDinh,
			isnull(p.fTonKho, 0) AS fTonKho,
		    isnull(p.fTuChi, 0) AS fTuChi,
		    isnull(p.fHienVat, 0) AS fHienVat,
		    isnull(p.fHangNhap, 0) AS fHangNhap,
		    isnull(p.fHangMua, 0) AS fHangMua,
		    isnull(p.fPhanCap, 0) AS fPhanCap,
		    isnull(p.fDuPhong, 0) AS fDuPhong,
			c.idSoQuyetDinh AS iID_CTDuToan_Nhan,
			'' AS sGhiChu,
			1 AS iRowType,
			cast(1 as bit) as bHangChaDuToan,
			cast(0 as bit) as IsEditTuChi,
			cast(0 as bit) as IsEditHienVat,
			cast(0 as bit) as IsEditHangNhap,
			cast(0 as bit) as IsEditHangMua,
			cast(0 as bit) as IsEditPhanCap,
			cast(0 as bit) as IsEditDuPhong
		FROM tblMlnsChildAndSqd c
		LEFT JOIN tblNhanPhanBo p
		ON c.iID_MLNS = p.iID_MLNS and c.idSoQuyetDinh = p.idSoQuyetDinh
	),
	-- lấy ra dòng cha từ bảng tmpMlnsParent iRowType = 0
	tblRowCha AS (
		SELECT 
			newid() AS iID_DTCTChiTiet,
			cASt(0x0 AS uniqueidentifier) AS iID_DTChungTu,
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
			cast(0 as bit) as IsEditTuChi,
			cast(0 as bit) as IsEditHienVat,
			cast(0 as bit) as IsEditHangNhap,
			cast(0 as bit) as IsEditHangMua,
			cast(0 as bit) as IsEditPhanCap,
			cast(0 as bit) as IsEditDuPhong
		FROM tblMlnsParent
	),
	-- lấy ra dòng cON từ bảng tmpMlnsChild và bảng NS_DT_ChungTuChiTiet iRowType = 3
	tblMlnsChildAndDvSqd AS (
		SELECT * FROM tblMlnsChild, tblDonViPhanBo, tblSoQuyetDinhNhan
	),
	tblRowChiTiet AS (
		SELECT 
			isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
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
			mlns.MaDonVi + ' - ' + mlns.sTenDonVi as sTenDonVi,
		    mlns.sSoQuyetDinh,
		    isnull(ctct.fTonKho, 0) AS fTonKho,
		    isnull(ctct.fTuChi, 0) AS fTuChi,
		    isnull(ctct.fHienVat, 0) AS fHienVat,
		    isnull(ctct.fHangNhap, 0) AS fHangNhap,
		    isnull(ctct.fHangMua, 0) AS fHangMua,
		    isnull(ctct.fPhanCap, 0) AS fPhanCap,
		    isnull(ctct.fDuPhong, 0) AS fDuPhong,
		    mlns.idSoQuyetDinh as iID_CTDuToan_Nhan,
		    isnull(ctct.sGhiChu, '') AS sGhiChu,
		    3 AS iRowType,
			mlns.bHangChaDuToan,
			isnull(mlns.bTuChi, cast(0 as bit)) as IsEditTuChi,
			isnull(mlns.bHienVat, cast(0 as bit)) as IsEditHienVat,
			isnull(mlns.bHangNhap, cast(0 as bit)) as IsEditHangNhap,
			isnull(mlns.bHangMua, cast(0 as bit)) as IsEditHangMua,
			isnull(mlns.bPhanCap, cast(0 as bit)) as IsEditPhanCap,
			isnull(mlns.bDuPhong, cast(0 as bit)) as IsEditDuPhong
		FROM tblMlnsChildAndDvSqd mlns
		LEFT JOIN
		(SELECT *
		   FROM NS_DT_ChungTuChiTiet
		   WHERE iID_DTChungTu = @sChungTuId) ctct 
		ON mlns.iID_MLNS = ctct.iID_MLNS and mlns.MaDonVi = ctct.iID_MaDonVi AND (mlns.idSoQuyetDinh = ctct.iID_CTDuToan_Nhan and ctct.iID_CTDuToan_Nhan is not null)
	),
	-- tính dữ liệu đã phân bổ
	tblTongRowChiTiet AS (
		SELECT 
			iID_MLNS, sSoQuyetDinh,
			sum(fTonKho) AS fTonKho,
			sum(fTuChi) AS fTuChi,
			sum(fHienVat) AS fHienVat,
			sum(fHangNhap) AS fHangNhap,
			sum(fHangMua) AS fHangMua,
			sum(fPhanCap) AS fPhanCap,
			sum(fDuPhong) AS fDuPhong
		FROM (select * from tblRowChiTiet where fTonKho <> 0 or fTuChi <> 0 or fHienVat <> 0 or fHangNhap <> 0 or fHangMua <> 0 or fPhanCap <> 0 or fDuPhong <> 0) ct
		group by iID_MLNS, sSoQuyetDinh
	),
	tblRowConLai AS (
		SELECT 
			newid() AS iID_DTCTChiTiet,
			cASt(0x0 AS uniqueidentifier) AS iID_DTChungTu,
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
			npb.sSoQuyetDinh,
			npb.fTonKho - isnull(rct.fTonKho, 0) AS fTonKho,
		    npb.fTuChi - isnull(rct.fTuChi, 0) AS fTuChi,
		    npb.fHienVat - isnull(rct.fHienVat, 0) AS fHienVat,
		    npb.fHangNhap - isnull(rct.fHangNhap, 0) AS fHangNhap,
		    npb.fHangMua - isnull(rct.fHangMua, 0) AS fHangMua,
		    npb.fPhanCap - isnull(rct.fPhanCap, 0) AS fPhanCap,
		    npb.fDuPhong - isnull(rct.fDuPhong, 0) AS fDuPhong,
			npb.iID_CTDuToan_Nhan AS iID_CTDuToan_Nhan,
			'' AS sGhiChu,
			2 AS iRowType,
			cast(1 as bit) as bHangChaDuToan,
			cast(0 as bit) as IsEditTuChi,
			cast(0 as bit) as IsEditHienVat,
			cast(0 as bit) as IsEditHangNhap,
			cast(0 as bit) as IsEditHangMua,
			cast(0 as bit) as IsEditPhanCap,
			cast(0 as bit) as IsEditDuPhong
		FROM tblRowNhanPhanBo npb
		LEFT JOIN tblTongRowChiTieT rct
		ON npb.iID_MLNS = rct.iID_MLNS and npb.sSoQuyetDinh = rct.sSoQuyetDinh
	)
	SELECT * FROM tblRowCha
	UNION ALL 
	SELECT * FROM tblRowNhanPhanBo
	UNION ALL 
	SELECT * FROM tblRowConLai
	UNION ALL 
	SELECT * FROM tblRowChiTiet 
END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_dieuchinh]    Script Date: 17/12/2021 8:08:45 AM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi]    Script Date: 05/06/2023 5:51:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi]
	@ListIdChungTuTongHop ntext,
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoai int,
	@MaBQuanLy varchar(max),
	@loaiNNS int
AS
BEGIN

SELECT map.sSKT_KyHieu INTO #KyHieuSktBQuanLy
FROM NS_MLSKT_MLNS MAP
JOIN NS_MucLucNganSach mlns ON mlns.sXauNoiMa = map.sNS_XauNoiMa
AND map.iNamLamViec = mlns.iNamLamViec
WHERE map.iNamLamViec = @NamLamViec
AND mlns.iNamLamViec = @NamLamViec and mlns.iID_MaBQuanLy = @MaBQuanLy;

SELECT ml.sM AS M ,
       ml.sKyHieu AS KyHieu ,
       ml.sMoTa AS MoTa ,
       ml.iID_MLSKT IdMucLuc ,
       ml.iID_MLSKTCha AS IdParent ,
       ml.sSTT AS STT ,
       ml.bHangCha ,
	   ct.iID_MaDonVi AS IdDonVi,
       TuChi =ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       HuyDong =ISNULL(SUM(ct.fHuyDongTonKho), 0)/@dvt ,
       PhanCap =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       MuaHangHienVat =ISNULL(SUM(ct.fMuaHangCapHienVat), 0)/@dvt ,
       TongCongNSSD = ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       TongCongNSBD = ISNULL(SUM(ct.fTuChi), 0)/@dvt
FROM NS_SKT_MucLuc ml
JOIN NS_SKT_ChungTuChiTiet ct ON ml.iID_MLSKT = ct.iID_MLSKT
JOIN NS_SKT_ChungTu chungTu ON ct.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
AND ml.iTrangThai=1
AND (@loaiNNS = 0 OR chungTu.iLoaiNguonNganSach = @loaiNNS)
AND ml.iNamLamViec=@NamLamViec
AND ct.iNamLamViec = @NamLamViec
AND ct.iNamNganSach = @NamNganSach
AND ct.iID_MaNguonNganSach = @NguonNganSach
AND ct.iLoaiChungTu =@LoaiChungTu
AND ct.iLoai=@iLoai
AND (ct.iID_MaDonVi in
       (SELECT *
        FROM f_split(@ListIdChungTuTongHop)))
AND (@MaBQuanLy = '0' OR ml.sKyHieu in (SELECT * FROM #KyHieuSktBQuanLy))
GROUP BY ml.sM ,
         ml.sKyHieu ,
         ml.sMoTa ,
         ml.iID_MLSKT ,
         ml.iID_MLSKTCha ,
         ml.sSTT ,
         ml.bHangCha,
		 ct.iID_MaDonVi
ORDER BY ml.sKyHieu;

drop table #KyHieuSktBQuanLy;
END
;
;
GO
