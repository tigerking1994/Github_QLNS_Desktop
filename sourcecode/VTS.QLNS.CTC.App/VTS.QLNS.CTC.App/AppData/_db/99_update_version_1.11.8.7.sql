/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 05/09/2022 8:06:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]    Script Date: 05/09/2022 8:09:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang]    Script Date: 05/09/2022 8:09:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_tonghopnguonnsdautu_tang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_tonghopnguonnsdautu_tang]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo]    Script Date: 05/09/2022 8:09:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_phanbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 05/09/2022 8:09:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_danhsach_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_danhsach_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet]    Script Date: 05/09/2022 8:09:39 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo]    Script Date: 05/09/2022 8:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@VoucherType int,
	@Date datetime
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
			   SUM(ISNULL(fTuChi, 0)) + SUM(ISNULL(fHienVat, 0)) + SUM(ISNULL(fPhanCap, 0)) + SUM(ISNULL(fHangNhap, 0)) + SUM(ISNULL(fHangMua, 0)) + SUM(ISNULL(fDuPhong, 0)) + SUM(ISNULL(fTonKho, 0)) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM NS_DT_ChungTuChiTiet dtctct
			 LEFT JOIN NS_DT_ChungTu dtct
			 on dtctct.iID_DTChungTu = dtct.iID_DTChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork 
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
		   ISNULL(npb.SoPhanBo, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo 
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTChungTu = dpb.iID_CTDuToan_Nhan
	WHERE ISNULL(npb.SoPhanBo, 0) > ISNULL(DaPhanBo, 0)
	ORDER BY npb.sSoChungTu
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang]    Script Date: 05/09/2022 8:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_insert_tonghopnguonnsdautu_tang]
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
		VALUES('KHVN'), ('101'), ('102'),('111'),('112')
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
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
			--OUTPUT inserted.Id, inserted.sMaDich, inserted.iID_DuAnID, ISNULL(inserted.fGiaTri, 0) INTO #tmp(iId, sMaNguon, iIdDuAnId, fDaThanhToan)
			SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300',
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG
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
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
			--OUTPUT inserted.Id, inserted.sMaDich, inserted.iID_DuAnID, ISNULL(inserted.fGiaTri, 0) INTO #tmp(iId, sMaNguon, iIdDuAnId, fDaThanhToan)
			SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300',
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG
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
			-- insert but toan moi vao 
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, 
					(CASE WHEN sMaNguon in ('101', '102') THEN '200' ELSE '100' END),
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBacDC' THEN '101' 
									WHEN 'fCapPhatBangLenhChiDC' THEN '102'END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG

			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, 
					(CASE WHEN sMaNguon in ('101', '102') THEN '200' ELSE '100' END),
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBacDC' THEN '111' 
									WHEN 'fCapPhatBangLenhChiDC' THEN '112'END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan = 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
		END
		ELSE
		BEGIN
			-- insert but toan moi vao 
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, 
					(CASE WHEN sMaNguon in ('101', '102') THEN '200' ELSE '100' END),
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '101' 
									WHEN 'fCapPhatBangLenhChi' THEN '102'END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG

			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, 
					(CASE WHEN sMaNguon in ('101', '102') THEN '200' ELSE '100' END),
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '111' 
									WHEN 'fCapPhatBangLenhChi' THEN '112'END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac,fGiaTriThuHoiNamTruocLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan = 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
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
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh)
		SELECT tbl.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoDeNghi, tbl.dNgayDeNghi, 
			(CASE WHEN tbl.sMaNguon = '211c' OR tbl.sMaNguon = '212c' THEN tbl.iNamKeHoach ELSE tbl.iNamKeHoach + 1 END) as iNamKeHoach
			, tbl.iID_ChungTu, tbl.sMaNguon, tbl.sMaDich, tbl.fGiaTri, tbl.iStatus, '100'
		FROM 
		(SELECT dt.iID_DuAnID, 
				tbl.iID_MaDonViQuanLy, 
				tbl.iID_NguonVonID,
				tbl.sSoDeNghi, 
				tbl.dNgayDeNghi, 
				tbl.iNamKeHoach as iNamKeHoach, 
				tbl.Id as iID_ChungTu, 
				(CASE 
					WHEN colName = 'fGiaTriTamUngDieuChinhGiam' AND dt.iCoQuanThanhToan = 1 THEN '211c'
					WHEN colName = 'fGiaTriTamUngDieuChinhGiam' AND dt.iCoQuanThanhToan = 2 THEN '212c'
				END) as sMaNguon, 
				'000' as sMaDich, 
				SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri, 
				SUM(ISNULL(dt.fGiaTri, 0)) as fSoDu, 
				0 as iStatus
		FROM VDT_QT_BCQuyetToanNienDo as tbl
		INNER JOIN (select Id, iID_BCQuyetToanNienDo, iCoQuanThanhToan, iID_DuAnID, dt.fGiaTri, colName
					from 
					(select Id, iID_BCQuyetToanNienDo, iCoQuanThanhToan, iID_DuAnID, fGiaTriTamUngDieuChinhGiam from VDT_QT_BCQuyetToanNienDo_ChiTiet_01) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN (fGiaTriTamUngDieuChinhGiam)) as dt) as dt on dt.iID_BCQuyetToanNienDo = tbl.Id
		WHERE tbl.Id = @uIdQuyetDinh
		GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoDeNghi, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.Id , dt.colName, dt.iCoQuanThanhToan
		) as tbl

		INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh)
		SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoDeNghi, tbl.dNgayDeNghi, (tbl.iNamKeHoach + 1), tbl.Id, 
			(CASE WHEN dt.iCoQuanThanhToan = 1 THEN '131' ELSE '132' END), '000', 
			SUM(ISNULL(dt.fKHUngTrcChuaThuHoiTrcNamQuyetToan, 0) - ISNULL(dt.fThuHoiUngTruoc, 0) + ISNULL(dt.fKHUngNamNay, 0) - 
				(ISNULL(dt.fLKThanhToanDenTrcNamQuyetToan_KHUng, 0) - ISNULL(dt.fGiaTriThuHoiTheoGiaiNganThucTe, 0) + ISNULL(dt.fThanhToan_KHUngNamTrcChuyenSang, 0) + ISNULL(dt.fThanhToan_KHUngNamNay, 0))), 
			2, '100'
		FROM VDT_QT_BCQuyetToanNienDo as tbl
		INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
		WHERE tbl.Id = @uIdQuyetDinh AND iLoaiThanhToan = 2
		GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoDeNghi, tbl.dNgayDeNghi, tbl.iNamKeHoach, tbl.Id, dt.iCoQuanThanhToan
	END
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]    Script Date: 05/09/2022 8:09:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt] 
@Loai varchar(MAX),
@IdDonVi varchar(MAX),
@NamLamViec int, 
@Dvt int,
@LoaiChungTu int 
AS 
BEGIN
SELECT DISTINCT mucluc.iID AS Id,
                mucluc.iID_MLSKT AS IDMucLuc,
                mucluc.sSTT AS STT,
                mucluc.sMoTa AS MoTa,
                mucluc.iID_MLSKTCha AS IdParent,
                mucluc.sSTTBC AS STTBC,
                mucluc.sM AS M,
                CAST(0 AS BIT) AS IsHangCha,
                chitiet.*
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT KyHieu ,
          QuyetToan =sum(QuyetToan)/@Dvt ,
          DuToan =sum(DuToan)/@Dvt ,
          TuChi =sum(fTuChi)/@Dvt ,
          TuChi2 =sum(TuChi2)/@Dvt
   FROM
     (SELECT mucluc.sKyHieu AS KyHieu,
             QuyetToan =0 ,
             DuToan =0 ,
             fTuChi ,
             TuChi2 =0
      FROM NS_SKT_ChungTuChiTiet chitiet
      LEFT JOIN NS_SKT_MucLuc mucluc ON chitiet.iID_MLSKT = mucluc.iID_MLSKT
      WHERE  chitiet.iNamLamViec=@NamLamViec
        AND chitiet.iLoai=@Loai
        AND chitiet.iLoaiChungTu = @LoaiChungTu
        AND (@IdDonVi IS NULL
             OR chitiet.iID_MaDonVi in
               (SELECT *
                FROM f_split(@IdDonVi)))
      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(SKT_XauNoiMa,@NamLamViec) AS KyHieu ,
                       QuyetToan ,
                       DuToan ,
                       TuChi =0 ,
                       TuChi2 =0
      FROM f_skt_cancu(@NamLamViec, @IdDonVi, cASt(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(SKT_XauNoiMa,@NamLamViec) IS NOT NULL -- lap chi tiet du toan

      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) AS KyHieu ,
                       QuyetToan =0 ,
                       DuToan =0 ,
                       TuChi =0 ,
                       TuChi2 =TuChi
      FROM f_skt_dutoan(@NamLamViec, @IdDonVi, cASt(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) IS NOT NULL ) AS a
   GROUP BY KyHieu --order by Tuchi
 ) chitiet ON mucluc.sKyHieu = chitiet.KyHieu
WHERE chitiet.KyHieu IS NOT NULL
  AND mucluc.iNamLamViec = @NamLamViec
ORDER BY sSTTBC END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 05/09/2022 8:06:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int,
	@bTongHop bit
AS
BEGIN
SELECT iID_MLSKT IIdMlskt,
       iID_MLSKTCha IIdMlsktCha,
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   dt_dv.iLoai,
       sKyHieu,
	   sSTT STT,
	   bHangCha,
       sNG,
       sMoTa,
       sNG_Cha sNgCha,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       MuaHangHienVat =SUM(IsNull(A.MuaHangHienVat,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
				ml.sSTT,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_SKT_ChungTuChiTiet ct
   LEFT JOIN NS_SKT_ChungTu chungtu ON ct.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.iID_MLSKT = ml.iID_MLSKT
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoai= @Loai
     AND ct.iLoaiChungTu = 1
	 AND (   (@bTongHop = 0)  or (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
--	WHERE (@Loai = 0 and dt_dv.iLoai != 0) or @Loai <> 0
GROUP BY iID_MLSKT,
         iID_MLSKTCha,
		 dt_dv.id,
	     dt_dv.sTenDonVi,
         sKyHieu,
		 sSTT,
		 bHangCha,
         sNG,
         sMoTa,
         sNG_Cha,
		 iLoai
		 order by sKyHieu
END
;
;
;
;
GO
