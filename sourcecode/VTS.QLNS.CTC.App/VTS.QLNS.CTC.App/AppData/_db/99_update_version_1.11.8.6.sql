/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 05/09/2022 2:04:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 05/09/2022 2:04:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
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

	select  @isQuanly =  COUNT(*) from  DonVi inner join  NguoiDung_DonVi  on  DonVi.iID_MaDonVi =  NguoiDung_DonVi.iID_MaDonVi  
	where  iLoai = 0 and NguoiDung_DonVi.iID_MaNguoiDung = @sUserName and NguoiDung_DonVi.inamlamviec = @iNamLamViec  and DonVi.iNamLamViec= @iNamLamViec;
	if @isQuanly >0
		set @isQuanly=1
	else
		set @isQuanly=0 

	IF @dNgayQuyetDinh IS NULL
		SELECT @dNgayPhanBo = @dNgayChungTu;
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
			select vctpb.iID_MLNS, 
			vctpb.sSoQuyetDinh, 
			vctpb.idSoQuyetDinh,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho 
			from  (
				SELECT 
					ctct.iID_MLNS, 
					(case when @isQuanly=1 then dt.sSoQuyetDinh else '' end)AS  sSoQuyetDinh ,  --dt.sSoQuyetDinh, 
					(case when @isQuanly=1 then dt.iID_DTChungTu else CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,  --dt.iID_DTChungTu AS idSoQuyetDinh,
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
		select vdaphanbo.iID_MLNS, 
			vdaphanbo.sSoQuyetDinh, 
			vdaphanbo.idSoQuyetDinh,
			0 - SUM(fTuChi) AS fTuChi, 
			0 - SUM(fHienVat) AS fHienVat,
			0 - SUM(fDuPhong) AS fDuPhong,
			0 - SUM(fHangMua) AS fHangMua,
			0 - SUM(fHangNhap) AS fHangNhap,
			0 - SUM(fPhanCap) AS fPhanCap,
			0 - SUM(fTonKho) AS fTonKho
		from (
		
				SELECT 
					ctct.iID_MLNS, 
					(case when @isQuanly=1 then ct.sSoQuyetDinh else '' end)AS  sSoQuyetDinh ,  --ct.sSoQuyetDinh, 
					(case when @isQuanly=1 then ct.iID_DTChungTu else CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,  --ct.iID_DTChungTu AS idSoQuyetDinh,
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
		) vdaphanbo
		
		
		GROUP BY vdaphanbo.iID_MLNS, vdaphanbo.sSoQuyetDinh, vdaphanbo.idSoQuyetDinh
	),
	tblNhanPhanBo AS (

		select iID_MLNS, sSoQuyetDinh, idSoQuyetDinh,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho
			from (
				SELECT 
					iID_MLNS, --sSoQuyetDinh, idSoQuyetDinh,
					
					(case when @isQuanly=1 then sSoQuyetDinh else '' end)AS  sSoQuyetDinh ,  --ct.sSoQuyetDinh, 
					(case when @isQuanly=1 then idSoQuyetDinh else CAST(NULL AS UNIQUEIDENTIFIER) end)AS  idSoQuyetDinh,  --ct.iID_DTChungTu AS idSoQuyetDinh,
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
			--c.sSoQuyetDinh,
			(case when @isQuanly=1 then c.sSoQuyetDinh else '' end)AS  sSoQuyetDinh ,  --c.sSoQuyetDinh, 
			(case when @isQuanly=1 then c.idSoQuyetDinh else '' end)AS  idSoQuyetDinh ,  --c.sSoQuyetDinh, 
			--idSoQuyetDinh

			isnull(p.fTonKho, 0) AS fTonKho,
		    isnull(p.fTuChi, 0) AS fTuChi,
		    isnull(p.fHienVat, 0) AS fHienVat,
		    isnull(p.fHangNhap, 0) AS fHangNhap,
		    isnull(p.fHangMua, 0) AS fHangMua,
		    isnull(p.fPhanCap, 0) AS fPhanCap,
		    isnull(p.fDuPhong, 0) AS fDuPhong,
			cast(c.idSoQuyetDinh AS nvarchar(max)) AS iID_CTDuToan_Nhan,
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
			cASt(null AS uniqueidentifier) AS idSoQuyetDinh,
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
		    --mlns.sSoQuyetDinh,
			(case when @isQuanly=1 then mlns.sSoQuyetDinh else '' end)AS  sSoQuyetDinh ,
			(case when @isQuanly=1 then mlns.idSoQuyetDinh else '' end)AS  idSoQuyetDinh ,
			
		    isnull(ctct.fTonKho, 0) AS fTonKho,
		    isnull(ctct.fTuChi, 0) AS fTuChi,
		    isnull(ctct.fHienVat, 0) AS fHienVat,
		    isnull(ctct.fHangNhap, 0) AS fHangNhap,
		    isnull(ctct.fHangMua, 0) AS fHangMua,
		    isnull(ctct.fPhanCap, 0) AS fPhanCap,
		    isnull(ctct.fDuPhong, 0) AS fDuPhong,
		    cast(mlns.idSoQuyetDinh as nvarchar(max)) as iID_CTDuToan_Nhan,
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
		ON mlns.iID_MLNS = ctct.iID_MLNS and mlns.MaDonVi = ctct.iID_MaDonVi AND (mlns.idSoQuyetDinh = ctct.iID_CTDuToan_Nhan and ctct.iID_CTDuToan_Nhan <> '')
	),
	-- tính dữ liệu đã phân bổ
	tblTongRowChiTiet AS (
		SELECT 
			iID_MLNS, sSoQuyetDinh, idSoQuyetDinh,
			sum(fTonKho) AS fTonKho,
			sum(fTuChi) AS fTuChi,
			sum(fHienVat) AS fHienVat,
			sum(fHangNhap) AS fHangNhap,
			sum(fHangMua) AS fHangMua,
			sum(fPhanCap) AS fPhanCap,
			sum(fDuPhong) AS fDuPhong
		FROM (select * from tblRowChiTiet where fTonKho <> 0 or fTuChi <> 0 or fHienVat <> 0 or fHangNhap <> 0 or fHangMua <> 0 or fPhanCap <> 0 or fDuPhong <> 0) ct
		group by iID_MLNS, sSoQuyetDinh, idSoQuyetDinh
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
			--npb.sSoQuyetDinh,
			(case when @isQuanly=1 then npb.sSoQuyetDinh else '' end)AS  sSoQuyetDinh,
			(case when @isQuanly=1 then npb.idSoQuyetDinh else '' end)AS  idSoQuyetDinh,
			npb.fTonKho - isnull(rct.fTonKho, 0) AS fTonKho,
		    npb.fTuChi - isnull(rct.fTuChi, 0) AS fTuChi,
		    npb.fHienVat - isnull(rct.fHienVat, 0) AS fHienVat,
		    npb.fHangNhap - isnull(rct.fHangNhap, 0) AS fHangNhap,
		    npb.fHangMua - isnull(rct.fHangMua, 0) AS fHangMua,
		    npb.fPhanCap - isnull(rct.fPhanCap, 0) AS fPhanCap,
		    npb.fDuPhong - isnull(rct.fDuPhong, 0) AS fDuPhong,
			cast(npb.iID_CTDuToan_Nhan as nvarchar(max)) AS iID_CTDuToan_Nhan,
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
		ON npb.iID_MLNS = rct.iID_MLNS 
		and npb.idSoQuyetDinh = rct.idSoQuyetDinh
		
		where 1 = @isQuanly
	)

	--select * from tblMlnsChild; 
	--select * from  tblDonViPhanBo; 
	--select * from  tblSoQuyetDinhNhan;
	--select * from tblMlnsChildAndDvSqd
	--select * from tblTongRowChiTieT;
	--select * from tblRowNhanPhanBo;

	
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
