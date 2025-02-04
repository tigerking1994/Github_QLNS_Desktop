/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tonghop_noidung_kpql]    Script Date: 9/5/2024 2:27:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tonghop_noidung_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tonghop_noidung_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_chitiet_noidung_kpql]    Script Date: 9/5/2024 2:27:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_chitiet_noidung_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_chitiet_noidung_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dtc_get_so_quyet_dinh_kpql]    Script Date: 9/5/2024 2:27:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_dtc_get_so_quyet_dinh_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_dtc_get_so_quyet_dinh_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_dtc_get_so_quyet_dinh_kpql]    Script Date: 9/5/2024 2:27:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_bhxh_dtc_get_so_quyet_dinh_kpql]
	@NamLamViec int
AS
BEGIN
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh, iLoaiDotNhanPhanBo as iLoaiDuToan,ID as iID_DTChungTu
		from BH_DTC_PhanBoDuToanChi dtt
		where iNamChungTu = @NamLamViec
END
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_chitiet_noidung_kpql]    Script Date: 9/5/2024 2:27:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_chitiet_noidung_kpql]
@iDChungTu uniqueidentifier,
@IIDDonVi nvarchar(50),
@NamLamViec int,
@DonViTinh int ,
@IsMillionRound bit
AS
BEGIN
	SELECT * into #tblChiQuanLy FROM (
	SELECT '1' AS STT, N'Chi kinh quản lý BHXH, BHYT, BHTN'  AS SNoiDung, 0 fSoTien , 1 bHangCha
	union all
	SELECT '' AS STT,N'         Trong đó: Ngành Cán bộ' AS SNoiDung, 
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha 
	FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0001'
	union all
	SELECT '' AS STT,N'                          Ngành Quân lực' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0002'
	union all
	SELECT '' AS STT,N'                          Ngành Tài chính' AS SNoiDung,
CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0003'
	union all
	SELECT '' AS STT,N'                          Ngành Quân y' AS SNoiDung,
CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0004'
	) bhxhbhytbhtn

	UPDATE A
	SET A.fSoTien=B.fSoTien
	FROM #tblChiQuanLy A, ( SELECT SUM(fSoTien) fSoTien from #tblChiQuanLy) B
	WHERE A.bHangCha=1
	
	SELECT * INTO #tblHoTroandChi FROM (
	SELECT '2' AS STT,N'Hỗ trợ cán bộ, nhân viên làm công tác BHXH, BHYT' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	1 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-011'
	union all
	SELECT '3' AS STT,N'Chi tập huấn nghiệp vụ BHXH, BHYT, BHTN (Cơ quan Quân lực chủ trì phối hợp với cơ quan Tài chính, Cán bộ, Quân y tổ chức thực hiện)' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	1 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-012') TBL
	
	SELECT * INTO #tblHoTroThu FROM
	(
	SELECT '4' AS STT,N'Hỗ trợ quản lý thu, chi BHXH, BHYT, BHTN ở đơn vị cơ sở (do ngành Tài chính quản lý báo cáo Thủ trưởng phân cấp cho đơn vị cơ sở)' AS SNoiDung, 0 fSoTien,1 bHangCha 
	union all
	SELECT '' AS STT,N'         Trong đó: Chi thường xuyên đặc thù' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa LIKE '9010011-013-0001%'
	union all
	SELECT '' AS STT,N'                          Chi không thường xuyên' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi=@IIDDonVi AND iNamLamViec=@NamLamViec
	AND sXauNoiMa = '9010011-013-0002') TBL

	UPDATE A
	set A.fSoTien=B.fSoTien
	FROM #tblHoTroThu A ,(SELECT SUM(fSoTien) fSoTien FROM #tblHoTroThu) B
	WHERE A.bHangCha=1

	SELECT * FROM (
	SELECT * FROM #tblChiQuanLy
	UNION ALL 
	SELECT * FROM #tblHoTroandChi
	UNION ALL 
	SELECT * FROM #tblHoTroThu) TBL 

	DROP TABLE #tblChiQuanLy
	DROP TABLE #tblHoTroandChi
	DROP TABLE #tblHoTroThu
END
;
;
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tonghop_noidung_kpql]    Script Date: 9/5/2024 2:27:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_rpt_bhxh_tonghop_noidung_kpql]
@iDChungTu uniqueidentifier,
@IIDDonVi nvarchar(50),
@NamLamViec int,
@DonViTinh int ,
@IsMillionRound bit
AS
BEGIN
	SELECT * into #tblChiQuanLy FROM (
	SELECT '1' AS STT, N'Chi kinh quản lý BHXH, BHYT, BHTN'  AS SNoiDung, 0 fSoTien , 1 bHangCha
	union all
	SELECT '' AS STT,N'         Trong đó: Ngành Cán bộ' AS SNoiDung, 
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha 
	FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0001'
	union all
	SELECT '' AS STT,N'                          Ngành Quân lực' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0002'
	union all
	SELECT '' AS STT,N'                          Ngành Tài chính' AS SNoiDung,
CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0003'
	union all
	SELECT '' AS STT,N'                          Ngành Quân y' AS SNoiDung,
CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-010-0004'
	) bhxhbhytbhtn

	UPDATE A
	SET A.fSoTien=B.fSoTien
	FROM #tblChiQuanLy A, ( SELECT SUM(fSoTien) fSoTien from #tblChiQuanLy) B
	WHERE A.bHangCha=1
	
	SELECT * INTO #tblHoTroandChi FROM (
	SELECT '2' AS STT,N'Hỗ trợ cán bộ, nhân viên làm công tác BHXH, BHYT' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	1 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-011'
	union all
	SELECT '3' AS STT,N'Chi tập huấn nghiệp vụ BHXH, BHYT, BHTN (Cơ quan Quân lực chủ trì phối hợp với cơ quan Tài chính, Cán bộ, Quân y tổ chức thực hiện)' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	1 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa='9010011-012') TBL
	
	SELECT * INTO #tblHoTroThu FROM
	(
	SELECT '4' AS STT,N'Hỗ trợ quản lý thu, chi BHXH, BHYT, BHTN ở đơn vị cơ sở (do ngành Tài chính quản lý báo cáo Thủ trưởng phân cấp cho đơn vị cơ sở)' AS SNoiDung, 0 fSoTien,1 bHangCha 
	union all
	SELECT '' AS STT,N'         Trong đó: Chi thường xuyên đặc thù' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa LIKE '9010011-013-0001%'
	union all
	SELECT '' AS STT,N'                          Chi không thường xuyên' AS SNoiDung,
	CASE WHEN @IsMillionRound=1 THEN round((SUM (isnull(fSoTien,0))/ 1000000),0)*1000000/ @DonViTinh ELSE round((SUM (fSoTien)/ @DonViTinh),0) END fSoTien,
	0 bHangCha FROM BH_DuToan_CTCT_KPQL WHERE iID_ChungTu=@iDChungTu AND iID_MaDonVi in (select * from splitstring(@IIDDonVi)) AND iNamLamViec=@NamLamViec
	AND sXauNoiMa = '9010011-013-0002') TBL

	UPDATE A
	set A.fSoTien=B.fSoTien
	FROM #tblHoTroThu A ,(SELECT SUM(fSoTien) fSoTien FROM #tblHoTroThu) B
	WHERE A.bHangCha=1

	SELECT * FROM (
	SELECT * FROM #tblChiQuanLy
	UNION ALL 
	SELECT * FROM #tblHoTroandChi
	UNION ALL 
	SELECT * FROM #tblHoTroThu) TBL 

	DROP TABLE #tblChiQuanLy
	DROP TABLE #tblHoTroandChi
	DROP TABLE #tblHoTroThu
END
;
;
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql]    Script Date: 9/6/2024 9:12:03 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_phuongan_pbdtc]    Script Date: 9/6/2024 9:12:03 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_phuongan_pbdtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_phuongan_pbdtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]    Script Date: 9/6/2024 9:12:03 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_finphanbodutoan_chitiet_kpql]    Script Date: 9/6/2024 9:12:03 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_finphanbodutoan_chitiet_kpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_finphanbodutoan_chitiet_kpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_finphanbodutoan_chitiet_kpql]    Script Date: 9/6/2024 9:12:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_finphanbodutoan_chitiet_kpql]
@iDChungTu uniqueidentifier,
@IIDDonVi nvarchar(50),
@NamLamViec int
AS
BEGIN

	DECLARE @SNoiDung nvarchar(max);
	DECLARE @STenDonVi nvarchar(max);
	DECLARE @fSoTien float;
	set @SNoiDung=(select sMoTa from BH_DM_MucLucNganSach
		where iNamLamViec=@NamLamViec
		and iTrangThai=1
		and sXauNoiMa='9010003')

	set @fSoTien=(Select  isnull(ctct.fTienTuChi,0) from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
	where ctct.iID_DTC_PhanBoDuToanChi=@iDChungTu
	and ctct.iID_MaDonVi=@IIDDonVi
	and ctct.sXauNoiMa='9010003'
	and dv.iTrangThai=1
	and dv.iNamLamViec=@NamLamViec)

	set @STenDonVi= (Select sTenDonVi from DonVi
	where iNamLamViec=@NamLamViec
	and iID_MaDonVi=@IIDDonVi
	and iTrangThai=1)

		Select @SNoiDung SNoiDung, isnull(@fSoTien,0) as fSoTien, 1 IsRemainRow, 1 BHangCha ,@STenDonVi STenDonVi,@IIDDonVi iID_MaDonVi

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]    Script Date: 9/6/2024 9:12:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]
@INamLamViec int,
@IdDonVi nvarchar(max),
@DonViTinh int
AS
BEGIN
	select 
		dv.sTenDonVi as sTenDonVi,
		ct.sMoTa,
		dv.iKhoi,
		sum(ct.fOmDau)  fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 3
	into #data
	from
	(select 
	iID_MaDonVi,
	case when iMa > 182 and iMa < 191 then N'Khối dự toán'
	when iMa > 192 and iMa < 201 then N'Khối hạch toán'
	else N'Khối khác' end as sMoTa,
	case when ima = 183 or ima = 193 then fSoThamDinh/ @DonViTinh
			else 0 end as fOmDau,
	case when ima = 184 or ima = 194 then fSoThamDinh/ @DonViTinh
			else 0 end as fThaiSan,
	case when ima = 185 or ima = 195 then fSoThamDinh/ @DonViTinh
			else 0 end as fTNLDBNN,
	case when ima = 186 or ima = 196 then fSoThamDinh/ @DonViTinh
			else 0 end as fHuuTri,
	case when ima = 187 or ima = 197 then fSoThamDinh/ @DonViTinh
			else 0 end as fPhucVien,
	case when ima = 188 or ima = 198 then fSoThamDinh/ @DonViTinh
			else 0 end as fXuatNgu,
	case when ima = 189 or ima = 199 then fSoThamDinh/ @DonViTinh
			else 0 end as fThoiViec,
	case when ima = 190 or ima = 200 then fSoThamDinh/ @DonViTinh
			else 0 end as fTuTuat
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where 
	iID_MaDonVi in (select * from f_split(@IdDonVi)) and 
	iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fOmDau > 0 or fThaiSan > 0 or fTNLDBNN > 0 or fHuuTri > 0 or fPhucVien > 0 or fXuatNgu > 0 or fThoiViec > 0 or fTuTuat > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi, ct.sMoTa, dv.iKhoi

	select 
	sSTT = '',
	sTenDonVi = N'A. Đơn vị dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA
	from (select * from #data where iKhoi = 2) ct

		select 
	sSTT = '',
	sTenDonVi = N'+ Khối dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA1
	from (select * from #data where iKhoi = 2 and sMoTa = N'Khối dự toán') ct

			select 
	sSTT = '',
	sTenDonVi = N'+ Khối hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA2
	from (select * from #data where iKhoi = 2 and sMoTa = N'Khối hạch toán') ct

	select 
	sSTT = '',
	sTenDonVi = N'B. Đơn vị hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB
	from (select * from #data where iKhoi = 1) ct

		select 
	sSTT = '',
	sTenDonVi = N'+ Khối dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB1
	from (select * from #data where iKhoi = 1 and sMoTa = N'Khối dự toán') ct

			select 
	sSTT = '',
	sTenDonVi = N'+ Khối hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB2
	from (select * from #data where iKhoi = 1 and sMoTa = N'Khối hạch toán') ct

				select 
	sSTT = '',
	sTenDonVi,
	iKhoi,
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 3
	into #rowDV
	from #data ct
	group by ct.sTenDonVi, ct.sTenDonVi, ct.iKhoi

	select * 
	into #rowDV1
	from (select 
		ROW_NUMBER() OVER (ORDER BY sTenDonVi) AS sSTT,
		sTenDonVi,
		sMoTa = null,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu 
		from #rowDV where iKhoi = 2) dv1
	union all (select 
		'' AS sSTT,
		sTenDonVi,
		sMoTa,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu = 2
	from #data where iKhoi = 2) 
	order by sTenDonVi, sMoTa

	select *
	into #rowDV2
	from (select 
		ROW_NUMBER() OVER (ORDER BY sTenDonVi) AS sSTT,
		sTenDonVi,
		sMoTa = null,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
		from #rowDV where iKhoi = 1) dv2
	union all (select 
		'' AS sSTT,
		sTenDonVi,
		sMoTa,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu = 2
	from #data where iKhoi = 1) 
	order by sTenDonVi, sMoTa

	select * from #rowA
	union all select * from #rowA1
	union all select * from #rowA2
	union all (select 
		sSTT,
		isnull('       ' + sMoTa, sTenDonVi) sTenDonVi,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
	from #rowDV1)
	union all select * from #rowB
	union all select * from #rowB1
	union all select * from #rowB2
	union all (select 
			sSTT,
		isnull('       ' + sMoTa, sTenDonVi) sTenDonVi,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
	from #rowDV2)
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_phuongan_pbdtc]    Script Date: 9/6/2024 9:12:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_phuongan_pbdtc]
	@NamLamViec int,
	@SoQuyetDinh nvarchar(max),
	@NgayQuyetDinh nvarchar(max),
	@MaDonVi nvarchar(max),
	@DonViTinh int,
	@IsMillionRound bit,
	@MaLoaiChi nvarchar(max)

AS
BEGIN
		SELECT * INTO #TBLMaLoaiChi FROM splitstring(@MaLoaiChi)
		DECLARE @IsMaBHXH int;
		DECLARE @IsKPQL int;
		DECLARE @IsMaKCBQYDV int;
		DECLARE @IsMaKCBTS int;
		DECLARE @IsMaHSSVNLD int;
		DECLARE @IsMaKCBBHYT int;
		DECLARE @IsMaMSTTBYT int;
		DECLARE @IsMaBHTN  int;

		SET	@IsMaBHXH= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='01' )
		SET	@IsKPQL= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='02' )
		SET	@IsMaKCBQYDV= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='03' )
		SET	@IsMaKCBTS= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='04' )
		SET	@IsMaHSSVNLD= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='05' )
		SET	@IsMaKCBBHYT= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='06' )
		SET	@IsMaMSTTBYT= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='07' )
		SET	@IsMaBHTN= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='08' )
		 Select 
		 'I' STT,
		 '' SMaDonVi,
		 N'KHỐI DỰ TOÁN' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 1 IdParent,
		 1 Type,
		 1 child
		 into #tempKhoiDuToan

		 Select 
		 N'TỔNG CỘNG' STT,
		 '' SMaDonVi,
		 '' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 0 IdParent,
		 0 Type,
		 1 child
		 into #tempTongCong

		  Select 
		 'II' STT,
		 '' SMaDonVi,
		 N'KHỐI HẠCH TOÁN' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 2 IdParent,
		 2 Type,
		 1 child
		 into #tempKhoiHachToan

		  --- BHXH
		Select ctct.* into #tblDuToanBHXH from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		where ctct.sXauNoiMa LIKE '9010001%'
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh =@SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		Select ctct.* into #tblHachToanBHXH from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		where ctct.sXauNoiMa LIKE '9010002%'
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and  ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		-- KPQL

		Select ctct.* into #tblDuToanKPQL from BH_DuToan_CTCT_KPQL  ctct
		left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi
		left join BH_DTC_PhanBoDuToanChi ct on ctct.iID_ChungTu=ct.ID
		where ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ctct.iNamLamViec = @NamLamViec
		and dv.iNamLamViec=@NamLamViec
		and dv.iKhoi=2
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		Select ctct.* into #tblHachToanKPQL from BH_DuToan_CTCT_KPQL  ctct
		left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi
		left join BH_DTC_PhanBoDuToanChi ct on ctct.iID_ChungTu=ct.ID
		where ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ctct.iNamLamViec = @NamLamViec
		and dv.iNamLamViec=@NamLamViec
		and dv.iKhoi!=2
		and  ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		--- Khac :KCBQY,KCBTS,Quỹ KCB BHYT quân nhân, mua sắm trang thiết bị y tế,chăm sóc sức khỏe ban đầu HSSV & NLĐ,hỗ trợ BHTN
		Select ctct.* into #tblDuToanKhac from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iKhoi=2
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))


		Select ctct.* into #tblHachToanKhac from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iKhoi!=2
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh =@SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		--- Detail BHXH
		Select 
		 Case when sXauNoiMa='9010001-010-011-0001' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienTroCapOmDau,
		 Case when sXauNoiMa='9010001-010-011-0002' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThaiSan,
		 Case when sXauNoiMa='9010001-010-011-0003' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroTNLDBNN,
		 Case when sXauNoiMa='9010001-010-011-0004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapHuuTri,
		 Case when sXauNoiMa='9010001-010-011-0005' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapPhucVien,
		 Case when sXauNoiMa='9010001-010-011-0006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapXuatNgu,
		 Case when sXauNoiMa='9010001-010-011-0007' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThoiViec,
		 Case when sXauNoiMa='9010001-010-011-0008' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 INTO #tempDetailDuToanBHXH
		 from #tblDuToanBHXH
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 SELECT 
		 sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		  A.iID_MaDonVi,
		  dv.sTenDonVi
		  into #tblDuToanSumBHXH
		FROM #tempDetailDuToanBHXH A
		left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		where iNamLamViec=@NamLamViec
		and dv.iTrangThai=1
		  GROUP BY A.iID_MaDonVi,dv.sTenDonVi


		Select 
		 Case when sXauNoiMa='9010002-010-011-0001' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienTroCapOmDau,
		 Case when sXauNoiMa='9010002-010-011-0002' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThaiSan,
		 Case when sXauNoiMa='9010002-010-011-0003' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroTNLDBNN,
		 Case when sXauNoiMa='9010002-010-011-0004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapHuuTri,
		 Case when sXauNoiMa='9010002-010-011-0005' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapPhucVien,
		 Case when sXauNoiMa='9010002-010-011-0006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapXuatNgu,
		 Case when sXauNoiMa='9010002-010-011-0007' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThoiViec,
		 Case when sXauNoiMa='9010002-010-011-0008' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 INTO #tempDetailHachToanBHXH
		 from #tblHachToanBHXH
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 SELECT 
		 sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumBHXH
		 FROM #tempDetailHachToanBHXH A
		left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		where iNamLamViec=@NamLamViec
		and dv.iTrangThai=1
		  GROUP BY A.iID_MaDonVi,dv.sTenDonVi

		--- KPQL
		Select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 case when sXauNoiMa='9010011-011' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroCanBo,
		 case when sXauNoiMa='9010011-012' then Sum(isnull(fSoTien,0)) else 0 end FTienChiTapHuan,
		 case when sXauNoiMa like '9010011-010%' or sXauNoiMa like '9010011-013-0001%'  or sXauNoiMa like '9010011-013-0002%' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tblDuToanDetailKPQL
		 from #tblDuToanKPQL
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 sum(FTienChiTapHuan) FTienChiTapHuan,
		 sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblDuToanSumKPQL
		 from #tblDuToanDetailKPQL A
		 left join  DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where dv.iNamLamViec=@NamLamViec
		 and dv.iTrangThai=1
		 group by   A.iID_MaDonVi,dv.sTenDonVi


		 Select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 case when sXauNoiMa='9010011-011' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroCanBo,
		 case when sXauNoiMa='9010011-012' then Sum(isnull(fSoTien,0)) else 0 end FTienChiTapHuan,
		 case when sXauNoiMa like '9010011-010%' or sXauNoiMa like '9010011-013-0001%'  or sXauNoiMa like '9010011-013-0002%' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tblHachToanDetailKPQL
		 from #tblHachToanKPQL
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 sum(FTienChiTapHuan) FTienChiTapHuan,
		 sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumKPQL
		 from #tblHachToanDetailKPQL A
		 left join  DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where dv.iNamLamViec=@NamLamViec
		 and dv.iTrangThai=1
		 group by   A.iID_MaDonVi,dv.sTenDonVi

		 --- Khac :KCBQY,KCBTS,Quỹ KCB BHYT quân nhân, mua sắm trang thiết bị y tế,chăm sóc sức khỏe ban đầu HSSV & NLĐ,hỗ trợ BHTN
		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 case when sXauNoiMa='9010004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBQYDV,
		 case when sXauNoiMa='9010006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBTSDK,
		 case when sXauNoiMa='9010008' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienChiTNKDQKCBBHYT,
		 case when sXauNoiMa='9010009' then SUM(isnull(fTienTuChi,0)) else 0 end FTienKPMSTTBYT,
		 case when sXauNoiMa='905' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKPCSSK,
		 case when sXauNoiMa='9010010' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tempDuToanDetailKhac
		 from #tblDuToanKhac
		 group by sXauNoiMa,iID_MaDonVi

		 select 
		  0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 sum(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT)  FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		  into #tblDuToanSumKhac
		 from #tempDuToanDetailKhac A 
		 left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where iNamLamViec=@NamLamViec
		 and iTrangThai=1
		 group by A.iID_MaDonVi,dv.sTenDonVi


		  select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 case when sXauNoiMa='9010004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBQYDV,
		 case when sXauNoiMa='9010006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBTSDK,
		 case when sXauNoiMa='9010008' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienChiTNKDQKCBBHYT,
		 case when sXauNoiMa='9010009' then SUM(isnull(fTienTuChi,0)) else 0 end FTienKPMSTTBYT,
		 case when sXauNoiMa='905' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKPCSSK,
		 case when sXauNoiMa='9010010' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tempHachToanDetailKhac
		 from #tblHachToanKhac
		 group by sXauNoiMa,iID_MaDonVi

		 select 
		  0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 sum(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT)  FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumKhac
		 from #tempHachToanDetailKhac A 
		 left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where iNamLamViec=@NamLamViec
		 and iTrangThai=1
		 group by A.iID_MaDonVi,dv.sTenDonVi

		 Select * into #tblDuToanDetailAll from
		 (
			select * from #tblDuToanSumBHXH
			union all
			select * from #tblDuToanSumKPQL
			union all
			select * from #tblDuToanSumKhac
		 )tbl 


		  Select *  into #tblHachToanDetailAll from
		 (
			select * from #tblHachToanSumBHXH
			union all
			select * from #tblHachToanSumKPQL
			union all
			select * from #tblHachToanSumKhac
		 )tbl 

		 select 
		 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS STT,
		 iID_MaDonVi SMaDonVi,
		 sTenDonVi as STenDonVi,
		 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
		 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 Sum(FTienChiTapHuan) FTienChiTapHuan,
		 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 sum(FTienTongCongAll) FTienTongCongAll,
		 0 BHangCha,
		 null IdParent,
		 1 Type,
		 0 child
		 into #tempDuToanSumAll
		from 
		#tblDuToanDetailAll
		group by iID_MaDonVi ,sTenDonVi
		order by iID_MaDonVi


		select 
			CONVERT(nvarchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS STT,
		 iID_MaDonVi SMaDonVi,
		 sTenDonVi as STenDonVi,
		 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
		 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 Sum(FTienChiTapHuan) FTienChiTapHuan,
		 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 sum(FTienTongCongAll) FTienTongCongAll,
		 0 BHangCha,
		 null IdParent,
		 2 Type,
		 0 child
		 into #tempHachToanSumAll
		from 
		#tblHachToanDetailAll
		group by iID_MaDonVi ,sTenDonVi
		order by iID_MaDonVi


		select * into #tblResult from 
		(
			Select * from #tempTongCong
			union all
			Select * from #tempKhoiDuToan
			union all 
			select * from #tempDuToanSumAll
			union all
			Select * from #tempKhoiHachToan
			union all 
			select * from #tempHachToanSumAll
		)tbl

		---update du toan
		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 Type=1
		) B
		where IdParent=1
		---update hach toan
		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 Type=2
		) B
		where IdParent=2

		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 child=0
		) B
		where IdParent=0
		if(@IsMillionRound = 1)
			select STT,
				SMaDonVi,
				STenDonVi,
				CASE WHEN @IsMaBHXH=1 THEN 	round(ISNULL(FTienTroCapOmDau,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienTroCapOmDau,
				CASE WHEN @IsMaBHXH=1 THEN 	 round(ISNULL(FTienTroCapThaiSan,0)/1000000,0)*1000000/@DonViTinh  ELSE 0 END  FTienTroCapThaiSan,
				CASE WHEN @IsMaBHXH=1 THEN  round(ISNULL(FTienTroTNLDBNN,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END  FTienTroTNLDBNN,
				CASE WHEN @IsMaBHXH=1 THEN   round(ISNULL(FTienTroCapHuuTri,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienTroCapHuuTri,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapPhucVien,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END  FTienTroCapPhucVien,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapXuatNgu,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienTroCapXuatNgu,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapThoiViec,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienTroCapThoiViec,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapTuTuat,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienTroCapTuTuat,
				--round(ISNULL(FTienTongCongBHXH,0)/1000000,0)*1000000/@DonViTinh FTienTongCongBHXH,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienHoTroCanBo,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienHoTroCanBo,
				CASE WHEN @IsKPQL=1 THEN  round(ISNULL(FTienChiTapHuan,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienChiTapHuan,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienHoTroQuanLy,0)/1000000,0)*1000000/@DonViTinh  ELSE 0 END FTienHoTroQuanLy,
				--round(ISNULL(FTienTongCongKQPL,0)/1000000,0)*1000000/@DonViTinh FTienTongCongKQPL,
				CASE WHEN @IsMaKCBQYDV=1 THEN round(ISNULL(FTienChiKCBQYDV,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END  FTienChiKCBQYDV,
				CASE WHEN @IsMaKCBTS=1 THEN round(ISNULL(FTienChiKCBTSDK,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienChiKCBTSDK,
				CASE WHEN @IsMaKCBBHYT=1 THEN round(ISNULL(FTienChiTNKDQKCBBHYT,0)/1000000,0)*1000000/@DonViTinh  ELSE 0 END FTienChiTNKDQKCBBHYT,
				CASE WHEN @IsMaMSTTBYT=1 THEN round(ISNULL(FTienKPMSTTBYT,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienKPMSTTBYT,
				CASE WHEN @IsMaHSSVNLD=1 THEN round(ISNULL(FTienChiKPCSSK,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienChiKPCSSK,
				CASE WHEN @IsMaBHTN=1 THEN round(ISNULL(FTienChiHTBHTN,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END  FTienChiHTBHTN,
				--round(ISNULL(FTienTongCongAll,0)/1000000,0)*1000000/@DonViTinh FTienTongCongAll,
				BHangCha,
				Type,
				IdParent,
				child
			from #tblResult 
			else
			select STT,
				SMaDonVi,
				STenDonVi,
				CASE WHEN @IsMaBHXH=1 THEN  round(ISNULL(FTienTroCapOmDau,0)/@DonViTinh,0) ELSE 0 END FTienTroCapOmDau,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapThaiSan,0)/@DonViTinh,0) ELSE 0 END FTienTroCapThaiSan,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroTNLDBNN,0)/@DonViTinh,0) ELSE 0 END FTienTroTNLDBNN,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapHuuTri,0)/@DonViTinh,0) ELSE 0 END FTienTroCapHuuTri,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapPhucVien,0)/@DonViTinh,0) ELSE 0 END FTienTroCapPhucVien,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapXuatNgu,0)/@DonViTinh,0)  ELSE 0 END FTienTroCapXuatNgu,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapThoiViec,0)/@DonViTinh,0) ELSE 0 END FTienTroCapThoiViec,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapTuTuat,0)/@DonViTinh,0) ELSE 0 END FTienTroCapTuTuat,
				--round(ISNULL(FTienTongCongBHXH,0)/@DonViTinh,0) FTienTongCongBHXH,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienHoTroCanBo,0)/@DonViTinh,0) ELSE 0 END FTienHoTroCanBo,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienChiTapHuan,0)/@DonViTinh,0)  ELSE 0 END FTienChiTapHuan,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienHoTroQuanLy,0)/@DonViTinh,0) ELSE 0 END FTienHoTroQuanLy,
				--round(ISNULL(FTienTongCongKQPL,0)/@DonViTinh,0) FTienTongCongKQPL,
				CASE WHEN @IsMaKCBQYDV=1 THEN round(ISNULL(FTienChiKCBQYDV,0)/@DonViTinh,0) ELSE 0 END FTienChiKCBQYDV,
				CASE WHEN @IsMaKCBTS=1 THEN round(ISNULL(FTienChiKCBTSDK,0)/@DonViTinh,0) ELSE 0 END FTienChiKCBTSDK,
				CASE WHEN @IsMaKCBBHYT=1 THEN round(ISNULL(FTienChiTNKDQKCBBHYT,0)/@DonViTinh,0) ELSE 0 END FTienChiTNKDQKCBBHYT,
				CASE WHEN @IsMaMSTTBYT=1 THEN round(ISNULL(FTienKPMSTTBYT,0)/@DonViTinh,0) ELSE 0 END FTienKPMSTTBYT,
				CASE WHEN @IsMaHSSVNLD=1 THEN round(ISNULL(FTienChiKPCSSK,0)/@DonViTinh,0) ELSE 0 END FTienChiKPCSSK,
				CASE WHEN @IsMaBHTN=1 THEN round(ISNULL(FTienChiHTBHTN,0)/@DonViTinh,0) ELSE 0 END FTienChiHTBHTN,
				--round(ISNULL(FTienTongCongAll,0)/@DonViTinh,0) FTienTongCongAll,
				BHangCha,
				Type,
				IdParent,
				child
			from #tblResult 

		Drop table #tblDuToanBHXH
		Drop table #tblHachToanBHXH
		Drop table #tblDuToanKPQL
		Drop table #tblHachToanKPQL
		Drop table #tblDuToanKhac
		Drop table #tblHachToanKhac
		DROP TABLE #tempTongCong
		DROP TABLE #tempKhoiDuToan
		DROP TABLE #tempKhoiHachToan
		DROP TABLE #tempDetailDuToanBHXH
		Drop table #tblHachToanDetailKPQL
		Drop table #tblDuToanDetailKPQL
		Drop table #tempDuToanDetailKhac
		Drop table #tempHachToanDetailKhac
		Drop table #tempDetailHachToanBHXH

		Drop table #tblDuToanSumBHXH
		drop table #tblHachToanSumBHXH
		drop table #tblHachToanSumKhac
		drop table #tblDuToanSumKhac
		drop table #tblDuToanSumKPQL
		drop table #tblHachToanSumKPQL
		drop table #tblDuToanDetailAll
		drop table #tblHachToanDetailAll

		drop table #tempDuToanSumAll
		drop table #tempHachToanSumAll
		drop table #tblResult

END
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql]    Script Date: 9/6/2024 9:12:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_phuongan_pbdtc_tachchi_kpql]
	@NamLamViec int,
	@SoQuyetDinh nvarchar(max),
	@NgayQuyetDinh nvarchar(max),
	@MaDonVi nvarchar(max),
	@DonViTinh int,
	@IsMillionRound bit,
	@MaLoaiChi nvarchar(max)

AS
BEGIN
		SELECT * INTO #TBLMaLoaiChi FROM splitstring(@MaLoaiChi)
		DECLARE @IsMaBHXH int;
		DECLARE @IsKPQL int;
		DECLARE @IsMaKCBQYDV int;
		DECLARE @IsMaKCBTS int;
		DECLARE @IsMaHSSVNLD int;
		DECLARE @IsMaKCBBHYT int;
		DECLARE @IsMaMSTTBYT int;
		DECLARE @IsMaBHTN  int;

		SET	@IsMaBHXH= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='01' )
		SET	@IsKPQL= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='02' )
		SET	@IsMaKCBQYDV= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='03' )
		SET	@IsMaKCBTS= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='04' )
		SET	@IsMaHSSVNLD= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='05' )
		SET	@IsMaKCBBHYT= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='06' )
		SET	@IsMaMSTTBYT= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='07' )
		SET	@IsMaBHTN= (SELECT COUNT(*) FROM #TBLMaLoaiChi WHERE Name='08' )

		 Select 
		 'I' STT,
		 '' SMaDonVi,
		 N'KHỐI DỰ TOÁN' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 1 IdParent,
		 1 Type,
		 1 child
		 into #tempKhoiDuToan

		 Select 
		 N'TỔNG CỘNG' STT,
		 '' SMaDonVi,
		 '' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 0 IdParent,
		 0 Type,
		 1 child
		 into #tempTongCong

		  Select 
		 'II' STT,
		 '' SMaDonVi,
		 N'KHỐI HẠCH TOÁN' as STenDonVi,
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 1 BHangCha,
		 2 IdParent,
		 2 Type,
		 1 child
		 into #tempKhoiHachToan

		  --- BHXH
		Select ctct.* into #tblDuToanBHXH from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		where ctct.sXauNoiMa LIKE '9010001%'
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh =@SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		Select ctct.* into #tblHachToanBHXH from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		where ctct.sXauNoiMa LIKE '9010002%'
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and  ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		-- KPQL

		Select ctct.* into #tblDuToanKPQL from BH_DuToan_CTCT_KPQL  ctct
		left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi
		left join BH_DTC_PhanBoDuToanChi ct on ctct.iID_ChungTu=ct.ID
		where ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ctct.iNamLamViec = @NamLamViec
		and dv.iNamLamViec=@NamLamViec
		and dv.iKhoi=2
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		Select ctct.* into #tblHachToanKPQL from BH_DuToan_CTCT_KPQL  ctct
		left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi
		left join BH_DTC_PhanBoDuToanChi ct on ctct.iID_ChungTu=ct.ID
		where ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ctct.iNamLamViec = @NamLamViec
		and dv.iNamLamViec=@NamLamViec
		and dv.iKhoi!=2
		and  ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		--- Khac :KCBQY,KCBTS,Quỹ KCB BHYT quân nhân, mua sắm trang thiết bị y tế,chăm sóc sức khỏe ban đầu HSSV & NLĐ,hỗ trợ BHTN
		Select ctct.* into #tblDuToanKhac from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iKhoi=2
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh = @SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))


		Select ctct.* into #tblHachToanKhac from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
		join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
		left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
		where dv.iKhoi!=2
		and (ct.iLoaiDotNhanPhanBo =1 or ct.iLoaiDotNhanPhanBo=2)
		and ct.sSoQuyetDinh =@SoQuyetDinh
		and  Convert(varchar, ct.dNgayQuyetDinh, 101)=@NgayQuyetDinh
		and ctct.iNamLamViec = @NamLamViec
		and ctct.iID_MaDonVi in (select * from splitstring(@MaDonVi))

		--- Detail BHXH
		Select 
		 Case when sXauNoiMa='9010001-010-011-0001' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienTroCapOmDau,
		 Case when sXauNoiMa='9010001-010-011-0002' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThaiSan,
		 Case when sXauNoiMa='9010001-010-011-0003' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroTNLDBNN,
		 Case when sXauNoiMa='9010001-010-011-0004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapHuuTri,
		 Case when sXauNoiMa='9010001-010-011-0005' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapPhucVien,
		 Case when sXauNoiMa='9010001-010-011-0006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapXuatNgu,
		 Case when sXauNoiMa='9010001-010-011-0007' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThoiViec,
		 Case when sXauNoiMa='9010001-010-011-0008' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienTongCongKQPL,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 INTO #tempDetailDuToanBHXH
		 from #tblDuToanBHXH
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 SELECT 
		 sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		  A.iID_MaDonVi,
		  dv.sTenDonVi
		  into #tblDuToanSumBHXH
		FROM #tempDetailDuToanBHXH A
		left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		where iNamLamViec=@NamLamViec
		and dv.iTrangThai=1
		  GROUP BY A.iID_MaDonVi,dv.sTenDonVi


		Select 
		 Case when sXauNoiMa='9010002-010-011-0001' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienTroCapOmDau,
		 Case when sXauNoiMa='9010002-010-011-0002' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThaiSan,
		 Case when sXauNoiMa='9010002-010-011-0003' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroTNLDBNN,
		 Case when sXauNoiMa='9010002-010-011-0004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapHuuTri,
		 Case when sXauNoiMa='9010002-010-011-0005' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapPhucVien,
		 Case when sXauNoiMa='9010002-010-011-0006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapXuatNgu,
		 Case when sXauNoiMa='9010002-010-011-0007' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapThoiViec,
		 Case when sXauNoiMa='9010002-010-011-0008' then SUM(isnull(fTienTuChi,0)) else 0 end FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 INTO #tempDetailHachToanBHXH
		 from #tblHachToanBHXH
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 SELECT 
		 sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumBHXH
		 FROM #tempDetailHachToanBHXH A
		left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		where iNamLamViec=@NamLamViec
		and dv.iTrangThai=1
		  GROUP BY A.iID_MaDonVi,dv.sTenDonVi

		--- KPQL
		Select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 case when sXauNoiMa='9010011-011' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroCanBo,
		 case when sXauNoiMa='9010011-012' then Sum(isnull(fSoTien,0)) else 0 end FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 case when sXauNoiMa = '9010011-010-0001' or sXauNoiMa = '9010011-013-0001-0001' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhCB,
		 case when sXauNoiMa = '9010011-010-0002' or sXauNoiMa = '9010011-013-0001-0002' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhQL,
		 case when sXauNoiMa = '9010011-010-0003' or sXauNoiMa = '9010011-013-0001-0003' or sXauNoiMa = '9010011-013-0002' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhTC,
		 case when sXauNoiMa = '9010011-010-0004' or sXauNoiMa = '9010011-013-0001-0004' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tblDuToanDetailKPQL
		 from #tblDuToanKPQL
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 sum(FTienChiTapHuan) FTienChiTapHuan,
		 sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
		 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
		 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
		 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblDuToanSumKPQL
		 from #tblDuToanDetailKPQL A
		 left join  DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where dv.iNamLamViec=@NamLamViec
		 and dv.iTrangThai=1
		 group by   A.iID_MaDonVi,dv.sTenDonVi


		 Select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 case when sXauNoiMa='9010011-011' then Sum(isnull(fSoTien,0)) else 0 end FTienHoTroCanBo,
		 case when sXauNoiMa='9010011-012' then Sum(isnull(fSoTien,0)) else 0 end FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 case when sXauNoiMa = '9010011-010-0001' or sXauNoiMa = '9010011-013-0001-0001' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhCB,
		 case when sXauNoiMa = '9010011-010-0002' or sXauNoiMa = '9010011-013-0001-0002' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhQL,
		 case when sXauNoiMa = '9010011-010-0003' or sXauNoiMa = '9010011-013-0001-0003' or sXauNoiMa = '9010011-013-0002' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhTC,
		 case when sXauNoiMa = '9010011-010-0004' or sXauNoiMa = '9010011-013-0001-0004' then Sum(isnull(fSoTien,0)) else 0 end FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tblHachToanDetailKPQL
		 from #tblHachToanKPQL
		 GROUP BY sXauNoiMa,iID_MaDonVi

		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 sum(FTienChiTapHuan) FTienChiTapHuan,
		 sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
		 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
		 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
		 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 0 FTienChiKCBQYDV,
		 0 FTienChiKCBTSDK,
		 0 FTienChiTNKDQKCBBHYT,
		 0 FTienKPMSTTBYT,
		 0 FTienChiKPCSSK,
		 0 FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumKPQL
		 from #tblHachToanDetailKPQL A
		 left join  DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where dv.iNamLamViec=@NamLamViec
		 and dv.iTrangThai=1
		 group by   A.iID_MaDonVi,dv.sTenDonVi

		 --- Khac :KCBQY,KCBTS,Quỹ KCB BHYT quân nhân, mua sắm trang thiết bị y tế,chăm sóc sức khỏe ban đầu HSSV & NLĐ,hỗ trợ BHTN
		 select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 case when sXauNoiMa='9010004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBQYDV,
		 case when sXauNoiMa='9010006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBTSDK,
		 case when sXauNoiMa='9010008' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienChiTNKDQKCBBHYT,
		 case when sXauNoiMa='9010009' then SUM(isnull(fTienTuChi,0)) else 0 end FTienKPMSTTBYT,
		 case when sXauNoiMa='905' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKPCSSK,
		 case when sXauNoiMa='9010010' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tempDuToanDetailKhac
		 from #tblDuToanKhac
		 group by sXauNoiMa,iID_MaDonVi

		 select 
		  0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 sum(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT)  FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		  into #tblDuToanSumKhac
		 from #tempDuToanDetailKhac A 
		 left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where iNamLamViec=@NamLamViec
		 and iTrangThai=1
		 group by A.iID_MaDonVi,dv.sTenDonVi


		  select 
		 0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 case when sXauNoiMa='9010004' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBQYDV,
		 case when sXauNoiMa='9010006' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKCBTSDK,
		 case when sXauNoiMa='9010008' then SUM(isnull(fTienTuChi,0)) else 0 end  FTienChiTNKDQKCBBHYT,
		 case when sXauNoiMa='9010009' then SUM(isnull(fTienTuChi,0)) else 0 end FTienKPMSTTBYT,
		 case when sXauNoiMa='905' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiKPCSSK,
		 case when sXauNoiMa='9010010' then SUM(isnull(fTienTuChi,0)) else 0 end FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 iID_MaDonVi
		 into #tempHachToanDetailKhac
		 from #tblHachToanKhac
		 group by sXauNoiMa,iID_MaDonVi

		 select 
		  0 FTienTroCapOmDau,
		 0 FTienTroCapThaiSan,
		 0 FTienTroTNLDBNN,
		 0 FTienTroCapHuuTri,
		 0 FTienTroCapPhucVien,
		 0 FTienTroCapXuatNgu,
		 0 FTienTroCapThoiViec,
		 0 FTienTroCapTuTuat,
		 0 FTienTongCongBHXH,
		 0 FTienHoTroCanBo,
		 0 FTienChiTapHuan,
		 0 FTienHoTroQuanLy,
		 0 FTienQuanLyNganhCB,
		 0 FTienQuanLyNganhQL,
		 0 FTienQuanLyNganhTC,
		 0 FTienQuanLyNganhQY,
		 0 FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 sum(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT)  FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 0 FTienTongCongAll,
		 A.iID_MaDonVi,
		 dv.sTenDonVi
		 into #tblHachToanSumKhac
		 from #tempHachToanDetailKhac A 
		 left join DonVi dv on A.iID_MaDonVi=dv.iID_MaDonVi
		 where iNamLamViec=@NamLamViec
		 and iTrangThai=1
		 group by A.iID_MaDonVi,dv.sTenDonVi

		 Select * into #tblDuToanDetailAll from
		 (
			select * from #tblDuToanSumBHXH
			union all
			select * from #tblDuToanSumKPQL
			union all
			select * from #tblDuToanSumKhac
		 )tbl 


		  Select *  into #tblHachToanDetailAll from
		 (
			select * from #tblHachToanSumBHXH
			union all
			select * from #tblHachToanSumKPQL
			union all
			select * from #tblHachToanSumKhac
		 )tbl 

		 select 
		 CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS STT,
		 iID_MaDonVi SMaDonVi,
		 sTenDonVi as STenDonVi,
		 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
		 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 Sum(FTienChiTapHuan) FTienChiTapHuan,
		 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
		 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
		 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
		 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
		 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 sum(FTienTongCongAll) FTienTongCongAll,
		 0 BHangCha,
		 null IdParent,
		 1 Type,
		 0 child
		 into #tempDuToanSumAll
		from 
		#tblDuToanDetailAll
		group by iID_MaDonVi ,sTenDonVi
		order by iID_MaDonVi


		select 
			CONVERT(nvarchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS STT,
		 iID_MaDonVi SMaDonVi,
		 sTenDonVi as STenDonVi,
		 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
		 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
		 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
		 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
		 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
		 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
		 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
		 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
		 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
		 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
		 Sum(FTienChiTapHuan) FTienChiTapHuan,
		 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
		 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
		 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
		 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
		 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
		 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
		 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
		 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
		 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
		 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
		 sum(FTienChiKPCSSK) FTienChiKPCSSK,
		 sum(FTienChiHTBHTN) FTienChiHTBHTN,
		 sum(FTienTongCongAll) FTienTongCongAll,
		 0 BHangCha,
		 null IdParent,
		 2 Type,
		 0 child
		 into #tempHachToanSumAll
		from 
		#tblHachToanDetailAll
		group by iID_MaDonVi ,sTenDonVi
		order by iID_MaDonVi


		select * into #tblResult from 
		(
			Select * from #tempTongCong
			union all
			Select * from #tempKhoiDuToan
			union all 
			select * from #tempDuToanSumAll
			union all
			Select * from #tempKhoiHachToan
			union all 
			select * from #tempHachToanSumAll
		)tbl

		---update du toan
		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienQuanLyNganhCB= B.FTienQuanLyNganhCB,
			 A.FTienQuanLyNganhQL= B.FTienQuanLyNganhQL,
			 A.FTienQuanLyNganhTC= B.FTienQuanLyNganhTC,
			 A.FTienQuanLyNganhQY= B.FTienQuanLyNganhQY,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
			 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
			 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
			 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 Type=1
		) B
		where IdParent=1
		---update hach toan
		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienQuanLyNganhCB= B.FTienQuanLyNganhCB,
			 A.FTienQuanLyNganhQL= B.FTienQuanLyNganhQL,
			 A.FTienQuanLyNganhTC= B.FTienQuanLyNganhTC,
			 A.FTienQuanLyNganhQY= B.FTienQuanLyNganhQY,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
			 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
			 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
			 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 Type=2
		) B
		where IdParent=2

		update A 
		set  A.FTienTroCapOmDau= B.FTienTroCapOmDau,
			 A.FTienTroCapThaiSan= B.FTienTroCapThaiSan,
			 A.FTienTroTNLDBNN= B.FTienTroTNLDBNN,
			 A.FTienTroCapHuuTri= B.FTienTroCapHuuTri,
			 A.FTienTroCapPhucVien= B.FTienTroCapPhucVien,
			 A.FTienTroCapXuatNgu= B.FTienTroCapXuatNgu,
			 A.FTienTroCapThoiViec= B.FTienTroCapThoiViec,
			 A.FTienTroCapTuTuat= B.FTienTroCapTuTuat,
			 A.FTienTongCongBHXH= B.FTienTongCongBHXH,
			 A.FTienHoTroCanBo= B.FTienHoTroCanBo,
			 A.FTienChiTapHuan= B.FTienChiTapHuan,
			 A.FTienHoTroQuanLy= B.FTienHoTroQuanLy,
			 A.FTienQuanLyNganhCB= B.FTienQuanLyNganhCB,
			 A.FTienQuanLyNganhQL= B.FTienQuanLyNganhQL,
			 A.FTienQuanLyNganhTC= B.FTienQuanLyNganhTC,
			 A.FTienQuanLyNganhQY= B.FTienQuanLyNganhQY,
			 A.FTienTongCongKQPL= B.FTienTongCongKQPL,
			 A.FTienChiKCBQYDV= B.FTienChiKCBQYDV,
			 A.FTienChiKCBTSDK= B.FTienChiKCBTSDK,
			 A.FTienChiTNKDQKCBBHYT= B.FTienChiTNKDQKCBBHYT,
			 A.FTienKPMSTTBYT= B.FTienKPMSTTBYT,
			 A.FTienChiKPCSSK= B.FTienChiKPCSSK,
			 A.FTienChiHTBHTN= B.FTienChiHTBHTN,
			 A.FTienTongCongAll= B.FTienTongCongAll
		from #tblResult A,
		(
			select 
			 Sum(FTienTroCapOmDau) FTienTroCapOmDau,
			 Sum(FTienTroCapThaiSan) FTienTroCapThaiSan,
			 Sum(FTienTroTNLDBNN) FTienTroTNLDBNN,
			 Sum(FTienTroCapHuuTri) FTienTroCapHuuTri,
			 Sum(FTienTroCapPhucVien) FTienTroCapPhucVien,
			 Sum(FTienTroCapXuatNgu) FTienTroCapXuatNgu,
			 Sum(FTienTroCapThoiViec) FTienTroCapThoiViec,
			 Sum(FTienTroCapTuTuat) FTienTroCapTuTuat,
			 Sum(FTienTongCongBHXH) FTienTongCongBHXH,
			 Sum(FTienHoTroCanBo) FTienHoTroCanBo,
			 Sum(FTienChiTapHuan) FTienChiTapHuan,
			 Sum(FTienHoTroQuanLy) FTienHoTroQuanLy,
			 sum(FTienQuanLyNganhCB) FTienQuanLyNganhCB,
			 sum(FTienQuanLyNganhQL) FTienQuanLyNganhQL,
			 sum(FTienQuanLyNganhTC) FTienQuanLyNganhTC,
			 sum(FTienQuanLyNganhQY) FTienQuanLyNganhQY,
			 Sum(FTienTongCongKQPL) FTienTongCongKQPL,
			 sum(FTienChiKCBQYDV) FTienChiKCBQYDV,
			 SUM(FTienChiKCBTSDK) FTienChiKCBTSDK,
			 sum(FTienChiTNKDQKCBBHYT) FTienChiTNKDQKCBBHYT,
			 sum(FTienKPMSTTBYT) FTienKPMSTTBYT,
			 sum(FTienChiKPCSSK) FTienChiKPCSSK,
			 sum(FTienChiHTBHTN) FTienChiHTBHTN,
			 sum(FTienTongCongAll) FTienTongCongAll
			 from #tblResult
			 where 
			 child=0
		) B
		where IdParent=0
		if(@IsMillionRound = 1)
			select STT,
				SMaDonVi,
				STenDonVi,
				CASE WHEN @IsMaBHXH=1 THEN 	round(ISNULL(FTienTroCapOmDau,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienTroCapOmDau,
				CASE WHEN @IsMaBHXH=1 THEN 	 round(ISNULL(FTienTroCapThaiSan,0)/1000000,0)*1000000/@DonViTinh  ELSE 0 END  FTienTroCapThaiSan,
				CASE WHEN @IsMaBHXH=1 THEN  round(ISNULL(FTienTroTNLDBNN,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END  FTienTroTNLDBNN,
				CASE WHEN @IsMaBHXH=1 THEN   round(ISNULL(FTienTroCapHuuTri,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienTroCapHuuTri,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapPhucVien,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END  FTienTroCapPhucVien,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapXuatNgu,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienTroCapXuatNgu,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapThoiViec,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienTroCapThoiViec,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapTuTuat,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienTroCapTuTuat,
				--round(ISNULL(FTienTongCongBHXH,0)/1000000,0)*1000000/@DonViTinh FTienTongCongBHXH,
				CASE WHEN @IsKPQL=1 THEN  round(ISNULL(FTienHoTroCanBo,0)/1000000,0)*1000000/@DonViTinh  ELSE 0 END FTienHoTroCanBo,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienChiTapHuan,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienChiTapHuan,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienHoTroQuanLy,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienHoTroQuanLy,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienQuanLyNganhCB,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienQuanLyNganhCB,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienQuanLyNganhQL,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienQuanLyNganhQL,
				CASE WHEN @IsKPQL=1 THEN  round(ISNULL(FTienQuanLyNganhTC,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienQuanLyNganhTC,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienQuanLyNganhQY,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienQuanLyNganhQY,
				--round(ISNULL(FTienTongCongKQPL,0)/1000000,0)*1000000/@DonViTinh FTienTongCongKQPL,
				CASE WHEN @IsMaKCBQYDV=1 THEN round(ISNULL(FTienChiKCBQYDV,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END  FTienChiKCBQYDV,
				CASE WHEN @IsMaKCBTS=1 THEN round(ISNULL(FTienChiKCBTSDK,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienChiKCBTSDK,
				CASE WHEN @IsMaKCBBHYT=1 THEN round(ISNULL(FTienChiTNKDQKCBBHYT,0)/1000000,0)*1000000/@DonViTinh  ELSE 0 END FTienChiTNKDQKCBBHYT,
				CASE WHEN @IsMaMSTTBYT=1 THEN round(ISNULL(FTienKPMSTTBYT,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienKPMSTTBYT,
				CASE WHEN @IsMaHSSVNLD=1 THEN round(ISNULL(FTienChiKPCSSK,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END FTienChiKPCSSK,
				CASE WHEN @IsMaBHTN=1 THEN round(ISNULL(FTienChiHTBHTN,0)/1000000,0)*1000000/@DonViTinh ELSE 0 END  FTienChiHTBHTN,
				--round(ISNULL(FTienTongCongAll,0)/1000000,0)*1000000/@DonViTinh FTienTongCongAll,
				BHangCha,
				Type,
				IdParent,
				child
			from #tblResult 
			else
			select STT,
				SMaDonVi,
				STenDonVi,
				CASE WHEN @IsMaBHXH=1 THEN  round(ISNULL(FTienTroCapOmDau,0)/@DonViTinh,0) ELSE 0 END FTienTroCapOmDau,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapThaiSan,0)/@DonViTinh,0) ELSE 0 END FTienTroCapThaiSan,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroTNLDBNN,0)/@DonViTinh,0) ELSE 0 END FTienTroTNLDBNN,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapHuuTri,0)/@DonViTinh,0) ELSE 0 END FTienTroCapHuuTri,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapPhucVien,0)/@DonViTinh,0) ELSE 0 END FTienTroCapPhucVien,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapXuatNgu,0)/@DonViTinh,0)  ELSE 0 END FTienTroCapXuatNgu,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapThoiViec,0)/@DonViTinh,0) ELSE 0 END FTienTroCapThoiViec,
				CASE WHEN @IsMaBHXH=1 THEN round(ISNULL(FTienTroCapTuTuat,0)/@DonViTinh,0) ELSE 0 END FTienTroCapTuTuat,
				--round(ISNULL(FTienTongCongBHXH,0)/@DonViTinh,0) FTienTongCongBHXH,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienHoTroCanBo,0)/@DonViTinh,0)  ELSE 0 END FTienHoTroCanBo,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienChiTapHuan,0)/@DonViTinh,0)  ELSE 0 END FTienChiTapHuan,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienHoTroQuanLy,0)/@DonViTinh,0)  ELSE 0 END FTienHoTroQuanLy,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienQuanLyNganhCB,0)/@DonViTinh,0)  ELSE 0 END FTienQuanLyNganhCB,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienQuanLyNganhQL,0)/@DonViTinh,0) ELSE 0 END FTienQuanLyNganhQL,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienQuanLyNganhTC,0)/@DonViTinh,0)  ELSE 0 END FTienQuanLyNganhTC,
				CASE WHEN @IsKPQL=1 THEN round(ISNULL(FTienQuanLyNganhQY,0)/@DonViTinh,0) ELSE 0 END FTienQuanLyNganhQY,
				--round(ISNULL(FTienTongCongKQPL,0)/@DonViTinh,0) FTienTongCongKQPL,
				CASE WHEN @IsMaKCBQYDV=1 THEN round(ISNULL(FTienChiKCBQYDV,0)/@DonViTinh,0) ELSE 0 END FTienChiKCBQYDV,
				CASE WHEN @IsMaKCBTS=1 THEN round(ISNULL(FTienChiKCBTSDK,0)/@DonViTinh,0) ELSE 0 END FTienChiKCBTSDK,
				CASE WHEN @IsMaKCBBHYT=1 THEN round(ISNULL(FTienChiTNKDQKCBBHYT,0)/@DonViTinh,0) ELSE 0 END FTienChiTNKDQKCBBHYT,
				CASE WHEN @IsMaMSTTBYT=1 THEN round(ISNULL(FTienKPMSTTBYT,0)/@DonViTinh,0) ELSE 0 END FTienKPMSTTBYT,
				CASE WHEN @IsMaHSSVNLD=1 THEN round(ISNULL(FTienChiKPCSSK,0)/@DonViTinh,0) ELSE 0 END FTienChiKPCSSK,
				CASE WHEN @IsMaBHTN=1 THEN round(ISNULL(FTienChiHTBHTN,0)/@DonViTinh,0) ELSE 0 END FTienChiHTBHTN,
				--round(ISNULL(FTienTongCongAll,0)/@DonViTinh,0) FTienTongCongAll,
				BHangCha,
				Type,
				IdParent,
				child
			from #tblResult 

		Drop table #tblDuToanBHXH
		Drop table #tblHachToanBHXH
		Drop table #tblDuToanKPQL
		Drop table #tblHachToanKPQL
		Drop table #tblDuToanKhac
		Drop table #tblHachToanKhac
		DROP TABLE #tempTongCong
		DROP TABLE #tempKhoiDuToan
		DROP TABLE #tempKhoiHachToan
		DROP TABLE #tempDetailDuToanBHXH
		Drop table #tblHachToanDetailKPQL
		Drop table #tblDuToanDetailKPQL
		Drop table #tempDuToanDetailKhac
		Drop table #tempHachToanDetailKhac
		Drop table #tempDetailHachToanBHXH

		Drop table #tblDuToanSumBHXH
		drop table #tblHachToanSumBHXH
		drop table #tblHachToanSumKhac
		drop table #tblDuToanSumKhac
		drop table #tblDuToanSumKPQL
		drop table #tblHachToanSumKPQL
		drop table #tblDuToanDetailAll
		drop table #tblHachToanDetailAll

		drop table #tempDuToanSumAll
		drop table #tempHachToanSumAll
		drop table #tblResult

END
;
;
;

GO
