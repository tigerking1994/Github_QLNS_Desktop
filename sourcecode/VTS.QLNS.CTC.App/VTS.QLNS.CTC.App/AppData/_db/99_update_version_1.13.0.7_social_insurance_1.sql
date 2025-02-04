/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo]    Script Date: 8/4/2023 4:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet]    Script Date: 8/4/2023 4:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_dutoan_dotnhan_cua_chungtu_phanbo]    Script Date: 8/4/2023 4:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtt_bhxh_dutoan_dotnhan_cua_chungtu_phanbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtt_bhxh_dutoan_dotnhan_cua_chungtu_phanbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_dotnhan_phanbo]    Script Date: 8/4/2023 4:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtt_bhxh_dotnhan_phanbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtt_bhxh_dotnhan_phanbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]    Script Date: 8/4/2023 4:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_quan_so_binh_quan_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dtt_phan_bo_chungtu]    Script Date: 8/4/2023 4:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_dtt_phan_bo_chungtu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_dtt_phan_bo_chungtu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_delete_dtt_phan_bo_chung_tu_chi_tiet_by_dotnhan]    Script Date: 8/4/2023 4:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_delete_dtt_phan_bo_chung_tu_chi_tiet_by_dotnhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_delete_dtt_phan_bo_chung_tu_chi_tiet_by_dotnhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dtt_danhsach_dotnhan]    Script Date: 8/4/2023 4:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dtt_danhsach_dotnhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dtt_danhsach_dotnhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 8/4/2023 4:27:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]    Script Date: 8/4/2023 4:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_danh_sach_phan_bo_dtt_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
AS
BEGIN
	--Lay MLNS
	select 
	BH_DM_MucLucNganSach.iID_MLNS as IIdMlns,
	BH_DM_MucLucNganSach.iID_MLNS_Cha IIdMlnsCha,
	BH_DM_MucLucNganSach.sLNS,
	BH_DM_MucLucNganSach.sL,
	BH_DM_MucLucNganSach.sK,
	BH_DM_MucLucNganSach.sM,
	BH_DM_MucLucNganSach.sTM,
	BH_DM_MucLucNganSach.sTTM,
	BH_DM_MucLucNganSach.sNG,
	BH_DM_MucLucNganSach.sTNG,
	BH_DM_MucLucNganSach.sXauNoiMa,
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	--NULL as iID_DTC_DuToanChiTrenGiao,
	--NULL as IID_DTC_PhanBoDuToanChiTiet,
	NULL as iID_MaDonVi,
	--NULL as sTenDonVi,
	NULL as sSoQuyetDinh,
	0 as fTienTuChiTruocDieuChinh,
	0 as fTienTuChi,
	0 as fTienTuChiSauDieuChinh,
	0 as fTienHienVatTruocDieuChinh,
	0 as fTienHienVat,
	0 as fTienHienVatSauDieuChinh,
	1 as bHangCha
	into tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec;

	---Lay danh sach chung tu phan bo
	select * 
	into tblChungTuPB
	from BH_DTT_Nhan_Phan_Bo_Map
	where iID_CTDuToan_PhanBo = @ChungTuId

	---Lay danh sach chung tu phan bo chi tiet
	select dc_ct.*, dc.sSoQuyetDinh
	into tblChungTuPB_Ct 
	from BH_DTT_BHXH_PhanBo_ChungTuChiTiet as dc_ct  
	inner join BH_DTT_BHXH_PhanBo_ChungTu as dc on dc_ct.iID_DTT_BHXH_ChungTu = dc.iID_DTT_BHXH_PhanBo_ChungTu
	where dc_ct.iID_DTT_BHXH_ChungTu in ( select iID_CTDuToan_Nhan from tblChungTuPB)
	
	select
	npb_ct.iID_MLNS,
	npb_ct.iID_MaDonVi,
	DonVi.sTenDonVi,
	npb_ct.sSoQuyetDinh,
	npb_ct.fBHXH_NLD as fBHXHNLD,
	npb_ct.fBHXH_NSD as fBHXHNSD,
	
	npb_ct.fBHYT_NLD as fBHYTNLD,
	npb_ct.fBHYT_NSD as fBHYTNSD,
	
	npb_ct.fBHTN_NLD as fBHTNNLD,
	npb_ct.fBHTN_NSD as fBHTNNSD,
	
	pb_ct.iID_CTDuToan_Nhan as iIDCTDuToanNhan,
	pb_ct.iID_DTT_BHXH_ChungTu_ChiTiet,
	pb_ct.fBHXH_NLD,
	pb_ct.fBHXH_NSD,
	pb_ct.fBHYT_NLD,
	pb_ct.fBHYT_NSD,
	pb_ct.fBHTN_NLD,
	pb_ct.fBHTN_NSD
	into tblThongTinChungTu
	from tblChungTuPB_Ct as npb_ct
	left join 
	( select * from BH_DTT_BHXH_PhanBo_ChungTuChiTiet where iID_DTT_BHXH_ChungTu_ChiTiet =  @ChungTuId) as  pb_ct on npb_ct.iID_MLNS = pb_ct.iID_MLNS and npb_ct.iID_MaDonVi = pb_ct.iID_MaDonVi
	left join DonVi on npb_ct.iID_MaDonVi = DonVi.iID_MaDonVi
	where DonVi.iNamLamViec = @NamLamViec

	select 
	tblMucLucNganSach.IIdMlns,
	tblMucLucNganSach.IIdMlnsCha,
	tblMucLucNganSach.sLNS,
	tblMucLucNganSach.sL,
	tblMucLucNganSach.sK,
	tblMucLucNganSach.sM,
	tblMucLucNganSach.sTM,
	tblMucLucNganSach.sTTM,
	tblMucLucNganSach.sNG,
	tblMucLucNganSach.sTNG,
	tblMucLucNganSach.sXauNoiMa,
	tblMucLucNganSach.sNoiDung,
	--tblThongTinChungTu.iID_DTC_DuToanChiTrenGiao,
	--tblThongTinChungTu.IID_DTC_PhanBoDuToanChiTiet,
	tblThongTinChungTu.iID_MaDonVi,
	--CONCAT(tblThongTinChungTu.iID_MaDonVi, '-', tblThongTinChungTu.sTenDonVi) as sTenDonVi,
	tblThongTinChungTu.sSoQuyetDinh,
	tblThongTinChungTu.fBHXHNLD,
	tblThongTinChungTu.fBHXHNSD,
	
	tblThongTinChungTu.fBHYTNLD,
	tblThongTinChungTu.fBHYTNSD,
	
	tblThongTinChungTu.fBHTNNLD,
	tblThongTinChungTu.fBHTNNSD,
	
	0 as bHangCha
	into tblThongTinChungTu_MLNS
	from tblMucLucNganSach
	inner join tblThongTinChungTu on tblMucLucNganSach.IIdMlns = tblThongTinChungTu.IId_Mlns

	select * from tblMucLucNganSach
	select * from tblThongTinChungTu_MLNS


	select * from
	(
		SELECT * from tblMucLucNganSach
		UNION ALL
		SELECT * from tblThongTinChungTu_MLNS
		
	) as test
	order by sXauNoiMa, iID_MaDonVi

	drop table tblMucLucNganSach;
	drop table tblChungTuPB;
	drop table tblChungTuPB_Ct;
	drop table tblThongTinChungTu;
	drop table tblThongTinChungTu_MLNS;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dtt_danhsach_dotnhan]    Script Date: 8/4/2023 4:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dtt_danhsach_dotnhan]
	@IdPhanBo nvarchar(100)
AS
BEGIN
	WITH tblIdPhanBo AS (
		SELECT * FROM BH_DTT_Nhan_Phan_Bo_Map 
		WHERE iID_CTDuToan_PhanBo = @IdPhanBo
		UNION ALL
		SELECT map.* FROM BH_DTT_Nhan_Phan_Bo_Map map
		INNER JOIN tblIdPhanBo
		ON map.iID_CTDuToan_PhanBo = tblIdPhanBo.iID_CTDuToan_Nhan
	)

	SELECT ct.*
	FROM BH_DTT_BHXH_PhanBo_ChungTu ct
	INNER JOIN tblIdPhanBo pb
	ON ct.iID_DTT_BHXH_PhanBo_ChungTu = pb.iID_CTDuToan_Nhan

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_delete_dtt_phan_bo_chung_tu_chi_tiet_by_dotnhan]    Script Date: 8/4/2023 4:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_delete_dtt_phan_bo_chung_tu_chi_tiet_by_dotnhan]
	@iID_DTChungTu AS uniqueidentifier ,
	@iID_CTDuToan_Nhan AS nvarchar(max)
AS
BEGIN
	DELETE FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet 
	WHERE iID_DTT_BHXH_ChungTu = @iID_DTChungTu 
	AND iID_CTDuToan_Nhan NOT IN (SELECT * FROM f_split(@iID_CTDuToan_Nhan))
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dtt_phan_bo_chungtu]    Script Date: 8/4/2023 4:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_dtt_phan_bo_chungtu]
	@YearOfWork int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT @CountDonViCha = count(*)
	FROM
	  (SELECT *
	   FROM NguoiDung_DonVi
	   WHERE iID_MaNguoiDung = @UserName
		 AND iNamLamViec = @YearOfWork
		 AND iTrangThai = 1) nddv
	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1
		 AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT *
	FROM BH_DTT_BHXH_PhanBo_ChungTu
	WHERE iNamLamViec = @YearOfWork
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
	ORDER BY sSoChungTu
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]    Script Date: 8/4/2023 4:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bhxh_get_quan_so_binh_quan_nam]
	@YearOfWork int
AS
BEGIN
	declare @LuongKeHoach table (Id uniqueidentifier,Nam int, Ma_CanBo varchar(20), Ma_PhuCap nvarchar(50), Ma_CB varchar(20), Gia_Tri numeric(15, 4));

	INSERT INTO @LuongKeHoach (Id, Nam, Ma_CanBo, Ma_PhuCap, Ma_CB, Gia_Tri)
	SELECT DISTINCT Id, Nam, Ma_CanBo, Ma_PhuCap, Ma_CB, Gia_Tri 
		FROM TL_BangLuong_KeHoach 
		WHERE Nam = 2023

		SELECT '9020001-010-011-0001-0000' XauNoiMa,
		count(1)/12 AS QSBQ 
		FROM @LuongKeHoach 
		WHERE Ma_CB LIKE '1%' --Lấy quân số bình quân năm của cấp bậc Sĩ quan
		
		UNION
		SELECT '9020001-010-011-0001-0001',
		count(1)/12 AS QSBQ_QNCN FROM @LuongKeHoach 
		where Ma_CB LIKE '2%' --Lấy quân số bình quân năm của cấp bậc Quân nhân chuyên nghiệp
		
		UNION
		SELECT '9020001-010-011-0001-0002',
		count(1)/12 AS QSBQ_HSQ FROM @LuongKeHoach 
		where Ma_CB LIKE '0%' --Lấy quân số bình quân năm của cấp bậc Hạ sĩ quan
		
		UNION
		SELECT '9020001-010-011-0002-0000',
		count(1)/12 AS QSBQ_VCQP FROM @LuongKeHoach 
		where Ma_CB in ('3.1', '3.2', '3.3') --Lấy quân số bình quân năm của cấp bậc CC, CN, VCQP
		
		UNION
		SELECT '9020001-010-011-0002-0001',
		count(1)/12 AS QSBQ_LDHD FROM @LuongKeHoach 
		where Ma_CB = '43' --Lấy quân số bình quân năm của cấp bậc LDHD
		
END
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_dotnhan_phanbo]    Script Date: 8/4/2023 4:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dtt_bhxh_dotnhan_phanbo]
	@YearOfWork int,
	@VoucherType int,
	@Date datetime
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *
		FROM BH_DTT_BHXH_PhanBo_ChungTu 
		WHERE iNamLamViec = @YearOfWork
			AND (fTongBHXH <> 0 OR fTongBHYT <> 0 OR fTongBHTN <> 0)
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM BH_DTT_Nhan_Phan_Bo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTT_BHXH_PhanBo_ChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(ISNULL(fTongCong, 0)) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet dtctct
			 LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu dtct
			 on dtctct.iID_DTT_BHXH_ChungTu = dtct.iID_DTT_BHXH_PhanBo_ChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTT_BHXH_ChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTT_BHXH_ChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)
	SELECT 
		   npb.iID_DTT_BHXH_PhanBo_ChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.fTongDuToan, 0) AS SoPhanBo,
		   ISNULL(npb.fTongDuToan, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo 
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTT_BHXH_PhanBo_ChungTu = dpb.iID_CTDuToan_Nhan
	WHERE ISNULL(npb.fTongDuToan, 0) > ISNULL(DaPhanBo, 0)
	ORDER BY npb.sSoChungTu
END
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_dutoan_dotnhan_cua_chungtu_phanbo]    Script Date: 8/4/2023 4:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtt_bhxh_dutoan_dotnhan_cua_chungtu_phanbo]
	@YearOfWork int,
	@IdNhanPhanBos nvarchar(max)
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *
		FROM BH_DTT_BHXH_PhanBo_ChungTu 
		WHERE iNamLamViec = @YearOfWork
			AND iID_DTT_BHXH_PhanBo_ChungTu in (select * from f_split(@IdNhanPhanBos))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM BH_DTT_Nhan_Phan_Bo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTT_BHXH_PhanBo_ChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(ISNULL(fTongCong, 0)) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet dtctct
			 LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu dtct
			 on dtctct.iID_DTT_BHXH_ChungTu = dtct.iID_DTT_BHXH_PhanBo_ChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTT_BHXH_ChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTT_BHXH_ChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)
	SELECT 
		   npb.iID_DTT_BHXH_PhanBo_ChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.fTongDuToan, 0) AS SoPhanBo,
		   ISNULL(npb.fTongDuToan, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo 
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTT_BHXH_PhanBo_ChungTu = dpb.iID_CTDuToan_Nhan
	ORDER BY npb.sSoChungTu
END
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet]    Script Date: 8/4/2023 4:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100)
AS
BEGIN
	SELECT isnull(ctct.iID_DTT_BHXH_ChungTu_ChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTT_BHXH_ChungTu,
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
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.fBHXH_NLD, 0) AS fBHXHNLD,
       isnull(ctct.fBHXH_NSD, 0) AS fBHXHNSD,
       isnull(ctct.fThuBHXH, 0) AS fThuBHXH,
       isnull(ctct.fBHYT_NLD, 0) AS fBHYTNLD,
       isnull(ctct.fBHYT_NSD, 0) AS fBHYTNSD,
       isnull(ctct.fThuBHYT, 0) AS fThuBHYT,
	   isnull(ctct.fBHTN_NLD, 0) AS fBHTNNLD,
	   isnull(ctct.fBHTN_NSD, 0) AS fBHTNNSD,
	   isnull(ctct.fThuBHTN, 0) AS fThuBHTN,
	   isnull(ctct.fTongCong, 0) AS fTongCong,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	FROM
	  (SELECT *
	   FROM BH_DM_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
	     AND bHangChaDuToan IS NOT NULL
		 AND iTrangThai = 1
		 AND sLNS in
		   (SELECT *
			FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT *
	   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iPhanCap = 1
		 AND iDuLieuNhan = 0
		 AND iID_DTT_BHXH_ChungTu = @ChungTuId ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sLNS,
			 mlns.sL,
			 mlns.sK,
			 mlns.sM,
			 mlns.sTM,
			 mlns.sTTM,
			 mlns.sNG,
			 mlns.sTNG;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo]    Script Date: 8/4/2023 4:27:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtt_bhxh_find_all_dutoan_dotnhan_phanbo]
	@YearOfWork int,
	@Date DateTime,
	@LoaiDuToan int
AS
BEGIN
	DECLARE @DieuChinh int = 2;
	--Lấy danh sách dự toán nhận phân bổ
		SELECT iID_DTT_BHXH,
			   sSoChungTu,
			   sDSLNS,
			   iID_MaDonVi,
			   dNgayChungTu,
			   sSoQuyetDinh,
			   dNgayQuyetDinh,
			   fDuToan AS fSoPhanBo
		INTO  tblNhanPhanBo
		FROM BH_DTT_BHXH_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iLoaiDuToan = @LoaiDuToan
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))

	--Lấy danh sách dự toán đã được phân bô
		SELECT pbmap.iID_CTDuToan_Nhan, fTongCong fDaPhanBo
		INTO tblChungTuNhanPhanBoMap
		FROM BH_DTT_Nhan_Phan_Bo_Map AS pbmap
		LEFT JOIN 
			(	SELECT sum(pbdtct.fTongCong) fTongCong, pbdt.iID_DTT_BHXH_PhanBo_ChungTu
				FROM BH_DTT_BHXH_PhanBo_ChungTu AS pbdt
				LEFT JOIN  BH_DTT_BHXH_PhanBo_ChungTuChiTiet AS pbdtct ON pbdt.iID_DTT_BHXH_PhanBo_ChungTu = pbdtct.iID_DTT_BHXH_ChungTu
				GROUP BY pbdt.iID_DTT_BHXH_PhanBo_ChungTu) AS tblDaPhanBo 
				ON pbmap.iID_CTDuToan_PhanBo = tblDaPhanBo.iID_DTT_BHXH_PhanBo_ChungTu

	---Lay danh sach du toan nhan phan bo, so phan bo, chua phan bo
		SELECT distinct npb.iID_DTT_BHXH as Id,
			    npb.sSoChungTu, 
				npb.sDSLNS,
				npb.iID_MaDonVi,
				npb.dNgayChungTu, 
				npb.sSoQuyetDinh, 
				npb.dNgayQuyetDinh, 
				npb.fSoPhanBo, 
				npbm.fDaPhanBo,
				(npb.fSoPhanBo - npbm.fDaPhanBo) AS fSoChuaPhanBo
		FROM tblNhanPhanBo AS npb
		left join tblChungTuNhanPhanBoMap AS npbm ON npb.iID_DTT_BHXH = npbm.iID_CTDuToan_Nhan

	   DROP TABLE tblNhanPhanBo;	
       DROP TABLE tblChungTuNhanPhanBoMap;
END
GO
