/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]    Script Date: 10/25/2024 2:25:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]    Script Date: 10/25/2024 2:25:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]    Script Date: 10/25/2024 2:25:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]    Script Date: 10/25/2024 2:25:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]    Script Date: 10/25/2024 2:25:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]    Script Date: 10/25/2024 2:25:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]    Script Date: 10/25/2024 2:25:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int,
	@IsMillionRound bit
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ctct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.ID in (select * from f_split(@IdChungTu))
select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]    Script Date: 10/25/2024 2:25:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int ,
	@DotNhan int,
	@IsMillionRound bit
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ctct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.iLoaiDotNhanPhanBo=@DotNhan
and ct.ID in (select * from f_split(@IdChungTu))
select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapOmDau ELSE rs.fTienTroCapOmDau  END) fTienTroCapOmDau,
			(CASE WHEN su.iIdParent IS NOT NULL THEN su.fTienTroCapThaiSan  ELSE rs.fTienTroCapThaiSan END) fTienTroCapThaiSan,
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
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]    Script Date: 10/25/2024 2:25:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int,
	@DotNhan int,
	@IsMillionRound bit
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ctct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.ID in (select * from f_split(@IdChungTu))
and ct.iLoaiDotNhanPhanBo=@DotNhan

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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
 and  A.iKhoi !=2
 order by B.position


 -- Create bang tam stt cho Mục B: Khối hạch toán
 SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDN.iID_MaDonVi) AS VARCHAR(6))  STT,
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
	SELECT CAST(ROW_NUMBER() OVER (ORDER BY #tempDvKDT.iID_MaDonVi) AS VARCHAR(6))  STT,
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
	
) tempRESULTALL order by iLoai, iID_MaDonVi;


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
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]    Script Date: 10/25/2024 2:25:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]	
	@INamLamViec int ,
	@IdMaDonVi nvarchar(MAX) ,
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX) ,
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@IdChungTu nvarchar(4000),
	@Donvitinh int,
	@IsMillionRound bit
	--@DotNhan int
AS
BEGIN

select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
inner join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
where ct.iNamChungTu=@INamLamViec
and ctct.sMaLoaiChi=@MaLoaiChi
--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103) <= convert(varchar,@SNgayQuyetDinh,101)
--and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi))
and ct.ID in (select * from f_split(@IdChungTu))
--and ct.iLoaiDotNhanPhanBo=@DotNhan

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapOmDau
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThaiSan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTaiNan
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapHuuTri
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapPhucVien
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapXuatNgu
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapThoiViec
	,SUM(CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN (CASE WHEN @IsMillionRound = 1 THEN ROUND(ISNULL(ctct.fTongTien,0)/1000000, 0) * 1000000 ELSE ISNULL(ctct.fTongTien,0) END) ELSE 0 END)/@Donvitinh AS fTienTroCapTuTuat
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
 and A.iKhoi! =2
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
	
) tempRESULTALL order by iLoai, iID_MaDonVi;


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
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh]    Script Date: 10/25/2024 2:25:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhxh] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int,
	@IsMillionRound bit
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
				ctct.iID_MaDonVi,
				IsNull(ctct.fBHXH_NLD, 0) ThuBHXHNLDDong,
				IsNull(ctct.fBHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fBHTN_NLD, 0) ThuBHTNNLDDong,
				IsNull(ctct.fBHTN_NSD, 0) ThuBHTNNSDDong
		   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @NamLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @KhoiDuToan
		   WHERE ct.iNamLamViec = @NamLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
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
				ctct.iID_MaDonVi,
				IsNull(ctct.fBHXH_NLD, 0) ThuBHXHNLDDong,
				IsNull(ctct.fBHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fBHTN_NLD, 0) ThuBHTNNLDDong,
				IsNull(ctct.fBHTN_NSD, 0) ThuBHTNNSDDong
		   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @NamLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @KhoiHachToan
		   WHERE ct.iNamLamViec = @NamLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
		GROUP BY dt_dv.sTenDonVi,
				dt_dv.id;

	SELECT '' idDonVi,N'A. Khối dự toán' sTenDonVI,0 BhxhNldDongDuToan, 0 BhxhNsddDongDuToan, 0 BhxhNldDongHachToan, 0 BhxhNsddDongHachToan, 0 BHXHTongCongDuToan, 0 BHXHTongCongHachToan,0 BhtnNldDongDuToan,0 BhtnNsddDongDuToan,0 BhtnNldDongHachToan,0 BhtnNsddDongHachToan,0 BHTNTongCongDuToan,0 BHTNTongCongHachToan into #tempDuToan
	SELECT '' idDonVi, N'B. Khối hạch toán' sTenDonVI,0 BhxhNldDongDuToan, 0 BhxhNsddDongDuToan, 0 BhxhNldDongHachToan, 0 BhxhNsddDongHachToan, 0 BHXHTongCongDuToan, 0 BHXHTongCongHachToan,0 BhtnNldDongDuToan ,0 BhtnNsddDongDuToan,0 BhtnNldDongHachToan,0 BhtnNsddDongHachToan,0 BHTNTongCongDuToan,0 BHTNTongCongHachToan into #tempHachToan
	if (@IsMillionRound = 1)
		begin 
	-- data du toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhxhNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongDuToan, 
	round(IsNull(dt.BhxhNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongDuToan, 
	round(IsNull(ht.BhxhNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongHachToan, 
	round(IsNull(ht.BhxhNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongHachToan, 
	round(IsNull(dt.BHXHTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongDuToan, 
	round(IsNull(ht.BHXHTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongHachToan, 
	round(IsNull(dt.BhtnNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongDuToan, 
	round(IsNull(dt.BhtnNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongDuToan, 
	round(IsNull(ht.BhtnNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongHachToan, 
	round(IsNull(ht.BhtnNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongHachToan, 
	round(IsNull(dt.BHTNTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongDuToan, 
	round(IsNull(ht.BHTNTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongHachToan
	into #tempDataDuToan
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi=2
	order by donvi.iID_MaDonVi

	-- data hach toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhxhNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongDuToan, 
	round(IsNull(dt.BhxhNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongDuToan, 
	round(IsNull(ht.BhxhNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNldDongHachToan, 
	round(IsNull(ht.BhxhNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhxhNsddDongHachToan, 
	round(IsNull(dt.BHXHTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongDuToan, 
	round(IsNull(ht.BHXHTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHXHTongCongHachToan, 
	round(IsNull(dt.BhtnNldDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongDuToan, 
	round(IsNull(dt.BhtnNsddDongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongDuToan, 
	round(IsNull(ht.BhtnNldDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNldDongHachToan, 
	round(IsNull(ht.BhtnNsddDongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BhtnNsddDongHachToan, 
	round(IsNull(dt.BHTNTongCongDuToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongDuToan, 
	round(IsNull(ht.BHTNTongCongHachToan, 0) / 1000000, 0) * 1000000 /  @DVT BHTNTongCongHachToan
	into #tempDataHachToan
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2
	order by donvi.iID_MaDonVi

	-- Gop vao du toan
	SELECT IsNull(SUM(B.BhxhNldDongDuToan),0) BhxhNldDongDuToan  ,
	IsNull(SUM(B.BhxhNsddDongDuToan),0) BhxhNsddDongDuToan,
	IsNull(SUM(B.BhxhNldDongHachToan),0) BhxhNldDongHachToan,
	IsNull(SUM(B.BhxhNsddDongHachToan),0) BhxhNsddDongHachToan,
	IsNull(SUM(B.BHXHTongCongDuToan),0) BHXHTongCongDuToan,
	IsNull(SUM(B.BHXHTongCongHachToan),0) BHXHTongCongHachToan,
	IsNull(SUM(B.BhtnNldDongDuToan),0) BhtnNldDongDuToan, 
	IsNull(SUM(B.BhtnNsddDongDuToan),0) BhtnNsddDongDuToan,
	IsNull(SUM(B.BhtnNldDongHachToan),0) BhtnNldDongHachToan,
	IsNull(SUM(B.BhtnNsddDongHachToan),0) BhtnNsddDongHachToan,
	IsNull(SUM(B.BHTNTongCongDuToan),0) BHTNTongCongDuToan, 
	IsNull(SUM(B.BHTNTongCongHachToan),0) BHTNTongCongHachToan
	INTO #TempSumDataDuToan
	FROM #tempDataDuToan B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempDuToan A, #TempSumDataDuToan B

	SELECT * INTO #TempResultDuToan FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan
	) DuToan

	SELECT IsNull(SUM(B.BhxhNldDongDuToan),0) BhxhNldDongDuToan  ,
	IsNull(SUM(B.BhxhNsddDongDuToan),0) BhxhNsddDongDuToan,
	IsNull(SUM(B.BhxhNldDongHachToan),0) BhxhNldDongHachToan,
	IsNull(SUM(B.BhxhNsddDongHachToan),0) BhxhNsddDongHachToan,
	IsNull(SUM(B.BHXHTongCongDuToan),0) BHXHTongCongDuToan,
	IsNull(SUM(B.BHXHTongCongHachToan),0) BHXHTongCongHachToan,
	IsNull(SUM(B.BhtnNldDongDuToan),0) BhtnNldDongDuToan, 
	IsNull(SUM(B.BhtnNsddDongDuToan),0) BhtnNsddDongDuToan,
	IsNull(SUM(B.BhtnNldDongHachToan),0) BhtnNldDongHachToan,
	IsNull(SUM(B.BhtnNsddDongHachToan),0) BhtnNsddDongHachToan,
	IsNull(SUM(B.BHTNTongCongDuToan),0) BHTNTongCongDuToan, 
	IsNull(SUM(B.BHTNTongCongHachToan),0) BHTNTongCongHachToan
	INTO #TempSumDataHachToan
	FROM #tempDataHachToan B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempHachToan A, #TempSumDataHachToan B

	-- Gop vao Hach Toan
	SELECT * INTO #TempResultHachToan FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan
	) HachToan
	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan
	UNION ALL
	SELECT * FROM #TempResultHachToan ) Result
	DROP TABLE #TempSumDataDuToan
	DROP TABLE #tempSumDataHachToan
	DROP TABLE #tempDataDuToan
	DROP TABLE #tempDataHachToan
	DROP TABLE #TempResultDuToan
	DROP TABLE #TempResultHachToan

	end
	else
	begin
	-- data du toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhxhNldDongDuToan, 0)/ @DVT BhxhNldDongDuToan, 
	IsNull(dt.BhxhNsddDongDuToan, 0)/ @DVT BhxhNsddDongDuToan, 
	IsNull(ht.BhxhNldDongHachToan, 0)/ @DVT BhxhNldDongHachToan, 
	IsNull(ht.BhxhNsddDongHachToan, 0)/ @DVT BhxhNsddDongHachToan,
	IsNull(dt.BHXHTongCongDuToan, 0)/ @DVT BHXHTongCongDuToan,
	IsNull(ht.BHXHTongCongHachToan, 0)/ @DVT BHXHTongCongHachToan,
	IsNull(dt.BhtnNldDongDuToan, 0)/ @DVT BhtnNldDongDuToan, 
	IsNull(dt.BhtnNsddDongDuToan, 0)/ @DVT BhtnNsddDongDuToan,
	IsNull(ht.BhtnNldDongHachToan, 0)/ @DVT BhtnNldDongHachToan, 
	IsNull(ht.BhtnNsddDongHachToan, 0)/ @DVT BhtnNsddDongHachToan,
	IsNull(dt.BHTNTongCongDuToan, 0)/ @DVT BHTNTongCongDuToan,
	IsNull(ht.BHTNTongCongHachToan, 0)/ @DVT BHTNTongCongHachToan
	into #tempDataDuToan1
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		    AND donvi.iKhoi=2


	-- data hach toan
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhxhNldDongDuToan, 0)/ @DVT BhxhNldDongDuToan, 
	IsNull(dt.BhxhNsddDongDuToan, 0)/ @DVT BhxhNsddDongDuToan, 
	IsNull(ht.BhxhNldDongHachToan, 0)/ @DVT BhxhNldDongHachToan, 
	IsNull(ht.BhxhNsddDongHachToan, 0)/ @DVT BhxhNsddDongHachToan,
	IsNull(dt.BHXHTongCongDuToan, 0)/ @DVT BHXHTongCongDuToan,
	IsNull(ht.BHXHTongCongHachToan, 0)/ @DVT BHXHTongCongHachToan,
	IsNull(dt.BhtnNldDongDuToan, 0)/ @DVT BhtnNldDongDuToan, 
	IsNull(dt.BhtnNsddDongDuToan, 0)/ @DVT BhtnNsddDongDuToan,
	IsNull(ht.BhtnNldDongHachToan, 0)/ @DVT BhtnNldDongHachToan, 
	IsNull(ht.BhtnNsddDongHachToan, 0)/ @DVT BhtnNsddDongHachToan,
	IsNull(dt.BHTNTongCongDuToan, 0)/ @DVT BHTNTongCongDuToan,
	IsNull(ht.BHTNTongCongHachToan, 0)/ @DVT BHTNTongCongHachToan
	into #tempDataHachToan1
	FROM 
	DonVi donvi
	LEFT JOIN @DataDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @DataHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2

	-- Gop vao du toan
	SELECT IsNull(SUM(B.BhxhNldDongDuToan),0) BhxhNldDongDuToan  ,
	IsNull(SUM(B.BhxhNsddDongDuToan),0) BhxhNsddDongDuToan,
	IsNull(SUM(B.BhxhNldDongHachToan),0) BhxhNldDongHachToan,
	IsNull(SUM(B.BhxhNsddDongHachToan),0) BhxhNsddDongHachToan,
	IsNull(SUM(B.BHXHTongCongDuToan),0) BHXHTongCongDuToan,
	IsNull(SUM(B.BHXHTongCongHachToan),0) BHXHTongCongHachToan,
	IsNull(SUM(B.BhtnNldDongDuToan),0) BhtnNldDongDuToan, 
	IsNull(SUM(B.BhtnNsddDongDuToan),0) BhtnNsddDongDuToan,
	IsNull(SUM(B.BhtnNldDongHachToan),0) BhtnNldDongHachToan,
	IsNull(SUM(B.BhtnNsddDongHachToan),0) BhtnNsddDongHachToan,
	IsNull(SUM(B.BHTNTongCongDuToan),0) BHTNTongCongDuToan, 
	IsNull(SUM(B.BHTNTongCongHachToan),0) BHTNTongCongHachToan
	INTO #TempSumDataDuToan1
	FROM #tempDataDuToan1 B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempDuToan A, #TempSumDataDuToan1 B

	SELECT * INTO #TempResultDuToan1 FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan1
	) DuToan

	SELECT IsNull(SUM(B.BhxhNldDongDuToan),0) BhxhNldDongDuToan  ,
	IsNull(SUM(B.BhxhNsddDongDuToan),0) BhxhNsddDongDuToan,
	IsNull(SUM(B.BhxhNldDongHachToan),0) BhxhNldDongHachToan,
	IsNull(SUM(B.BhxhNsddDongHachToan),0) BhxhNsddDongHachToan,
	IsNull(SUM(B.BHXHTongCongDuToan),0) BHXHTongCongDuToan,
	IsNull(SUM(B.BHXHTongCongHachToan),0) BHXHTongCongHachToan,
	IsNull(SUM(B.BhtnNldDongDuToan),0) BhtnNldDongDuToan, 
	IsNull(SUM(B.BhtnNsddDongDuToan),0) BhtnNsddDongDuToan,
	IsNull(SUM(B.BhtnNldDongHachToan),0) BhtnNldDongHachToan,
	IsNull(SUM(B.BhtnNsddDongHachToan),0) BhtnNsddDongHachToan,
	IsNull(SUM(B.BHTNTongCongDuToan),0) BHTNTongCongDuToan, 
	IsNull(SUM(B.BHTNTongCongHachToan),0) BHTNTongCongHachToan
	INTO #TempSumDataHachToan1
	FROM #tempDataHachToan1 B

	UPDATE A
	SET A.BhxhNldDongDuToan=(B.BhxhNldDongDuToan),
		A.BhxhNsddDongDuToan=(B.BhxhNsddDongDuToan),
		A.BhxhNldDongHachToan=(B.BhxhNldDongHachToan),
		A.BhxhNsddDongHachToan=(B.BhxhNsddDongHachToan),
		A.BHXHTongCongDuToan=(B.BHXHTongCongDuToan),
		A.BHXHTongCongHachToan=(B.BHXHTongCongHachToan),
		A.BhtnNldDongDuToan=(B.BhtnNldDongDuToan),
		A.BhtnNsddDongDuToan=(B.BhtnNsddDongDuToan),
		A.BhtnNldDongHachToan=(B.BhtnNldDongHachToan),
		A.BhtnNsddDongHachToan=(B.BhtnNsddDongHachToan),
		A.BHTNTongCongDuToan=(B.BHTNTongCongDuToan),
		A.BHTNTongCongHachToan=(B.BHTNTongCongHachToan)
	FROM #tempHachToan A, #TempSumDataHachToan1 B

	-- Gop vao Hach Toan
	SELECT * INTO #TempResultHachToan1 FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan1
	) HachToan
	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan1
	UNION ALL
	SELECT * FROM #TempResultHachToan1 ) Result

	DROP TABLE #TempSumDataDuToan1
	DROP TABLE #TempSumDataHachToan1
	DROP TABLE #tempDataDuToan1
	DROP TABLE #tempDataHachToan1
	DROP TABLE #TempResultDuToan1
	DROP TABLE #TempResultHachToan1
	end

	DROP TABLE #tempDuToan
	DROP TABLE #tempHachToan
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt]    Script Date: 10/25/2024 2:25:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_theokhoi_bhyt] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@SM nvarchar(50),
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int,
	@IsMillionRound bit
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
			   ctct.iID_MaDonVi,
			   IsNull(ctct.fBHYT_NSD, 0) ThuBHYTNSDDongDuToan,
			   IsNull(ctct.fBHYT_NLD, 0) ThuBHYTNLDDongDuToan,
			   IsNull(ctct.fThuBHYT, 0) TongBhytDuToan
	   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
	   AND ml.iNamLamViec = @NamLamViec
	   AND ml.iTrangThai = 1
	   AND ml.sLNS = @KhoiDuToan
	   AND ml.sM = @SM
	   WHERE ct.iNamLamViec = @NamLamViec
	   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
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
			   ctct.iID_MaDonVi,
			   IsNull(ctct.fBHYT_NSD, 0) ThuBHYTNSDDongHachToan,
			   IsNull(ctct.fBHYT_NLD, 0) ThuBHYTNLDDongHachToan,
			   IsNull(ctct.fThuBHYT, 0) TongBhytHachToan
	   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
	   AND ml.iNamLamViec = @NamLamViec
	   AND ml.iTrangThai = 1
	   AND ml.sLNS = @KhoiHachToan
	   AND ml.sM = @SM
	   WHERE ct.iNamLamViec = @namLamViec
	   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
	   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
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


	SELECT '' idDonVi,N'A. Khối dự toán' sTenDonVI,0 BhytNldDongDuToan,0 BhytNsddDongDuToan,0 BhytNldDongHachToan,0 BhytNsddDongHachToan,0 BHYTTongCongDuToan,0 BHYTTongCongHachToan into #tempDuToan
	SELECT '' idDonVi,N'B. Khối hạch toán' sTenDonVI,0 BhytNldDongDuToan,0 BhytNsddDongDuToan,0 BhytNldDongHachToan,0 BhytNsddDongHachToan,0 BHYTTongCongDuToan,0 BHYTTongCongHachToan into #tempHachToan
	if (@IsMillionRound = 1)
	begin 
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhytNLDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongDuToan, 
	round(IsNull(dt.BhytNSDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongDuToan, 
	round(IsNull(ht.BhytNLDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongHachToan, 
	round(IsNull(ht.BhytNSDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongHachToan, 
	round(IsNull(dt.TongBhytDuToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongDuToan, 
	round(IsNull(ht.TongBhytHachToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongHachToan
	into #tempDataDuToan
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi=2

	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	round(IsNull(dt.BhytNLDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongDuToan, 
	round(IsNull(dt.BhytNSDDongDuToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongDuToan, 
	round(IsNull(ht.BhytNLDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNldDongHachToan, 
	round(IsNull(ht.BhytNSDDongHachToan, 0) / 1000000, 0) * 1000000 /@DVT BhytNsddDongHachToan, 
	round(IsNull(dt.TongBhytDuToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongDuToan, 
	round(IsNull(ht.TongBhytHachToan, 0) / 1000000, 0) * 1000000 /@DVT BHYTTongCongHachToan
	into #tempDataHachToan
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2
	-- Sum Du toan
	SELECT 
	isnull(SUM(BhytNldDongDuToan),0) BhytNldDongDuToan,
	isnull(SUM(BhytNsddDongDuToan),0) BhytNsddDongDuToan,
	isnull(SUM(BhytNldDongHachToan),0) BhytNldDongHachToan,
	isnull(SUM(BhytNsddDongHachToan),0) BhytNsddDongHachToan,
	isnull(SUM(BHYTTongCongDuToan),0) BHYTTongCongDuToan,
	isnull(SUM(BHYTTongCongHachToan),0) BHYTTongCongHachToan
	INTO #tempSumDataDuToan
	FROM #tempDataDuToan

	Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempDuToan A , #tempSumDataDuToan B

	-- Sum Hach Toan
	SELECT 
	isnull(SUM(BhytNldDongDuToan),0) BhytNldDongDuToan,
	isnull(SUM(BhytNsddDongDuToan),0) BhytNsddDongDuToan,
	isnull(SUM(BhytNldDongHachToan),0) BhytNldDongHachToan,
	isnull(SUM(BhytNsddDongHachToan),0) BhytNsddDongHachToan,
	isnull(SUM(BHYTTongCongDuToan),0) BHYTTongCongDuToan,
	isnull(SUM(BHYTTongCongHachToan),0) BHYTTongCongHachToan
	INTO #tempSumDataHachToan
	FROM #tempDataHachToan

		Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempHachToan A , #tempSumDataHachToan B

	-- Gop Du toan
	SELECT * INTO #TempResultDuToan FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan
	) DuToan

	-- Gop vao Hach Toan
	SELECT * INTO #TempResultHachToan FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan
	) HachToan

	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan
	UNION ALL
	SELECT * FROM #TempResultHachToan ) Result

	DROP TABLE #tempDataDuToan
	DROP TABLE #tempDataHachToan
	DROP TABLE #tempSumDataDuToan
	DROP TABLE #tempSumDataHachToan
	DROP TABLE #TempResultDuToan
	DROP TABLE #TempResultHachToan
	end
	else
	begin 
	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhytNLDDongDuToan, 0)/@DVT BhytNldDongDuToan, 
	IsNull(dt.BhytNSDDongDuToan, 0)/@DVT BhytNsddDongDuToan, 
	IsNull(ht.BhytNLDDongHachToan, 0)/@DVT BhytNldDongHachToan, 
	IsNull(ht.BhytNSDDongHachToan, 0)/@DVT BhytNsddDongHachToan,
	IsNull(dt.TongBhytDuToan, 0)/@DVT BHYTTongCongDuToan,
	IsNull(ht.TongBhytHachToan, 0)/@DVT BHYTTongCongHachToan
	INTO #tempDataDuToan1
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi=2
	

	SELECT donvi.iID_MaDonVi idDonVi, 
	donvi.sTenDonVI, 
	IsNull(dt.BhytNLDDongDuToan, 0)/@DVT BhytNldDongDuToan, 
	IsNull(dt.BhytNSDDongDuToan, 0)/@DVT BhytNsddDongDuToan, 
	IsNull(ht.BhytNLDDongHachToan, 0)/@DVT BhytNldDongHachToan, 
	IsNull(ht.BhytNSDDongHachToan, 0)/@DVT BhytNsddDongHachToan,
	IsNull(dt.TongBhytDuToan, 0)/@DVT BHYTTongCongDuToan,
	IsNull(ht.TongBhytHachToan, 0)/@DVT BHYTTongCongHachToan
	INTO #tempDataHachToan1
	FROM
	DonVi donvi
	LEFT JOIN @BhytDuToan dt ON donvi.iID_MaDonVi = dt.idDonVi
	LEFT JOIN @BhytHachToan ht ON donvi.iID_MaDonVi = ht.idDonVi
	WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))
		   AND donvi.iKhoi!=2
		-- Sum Du toan
	SELECT 
	isnull(SUM(BhytNldDongDuToan),0) BhytNldDongDuToan,
	isnull(SUM(BhytNsddDongDuToan),0) BhytNsddDongDuToan,
	isnull(SUM(BhytNldDongHachToan),0) BhytNldDongHachToan,
	isnull(SUM(BhytNsddDongHachToan),0) BhytNsddDongHachToan,
	isnull(SUM(BHYTTongCongDuToan),0) BHYTTongCongDuToan,
	isnull(SUM(BHYTTongCongHachToan),0) BHYTTongCongHachToan
	INTO #tempSumDataDuToan1
	FROM #tempDataDuToan1

	Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempDuToan A , #tempSumDataDuToan1 B

	-- Sum Hach Toan
	SELECT 
	isnull(SUM(BhytNldDongDuToan),0) BhytNldDongDuToan,
	isnull(SUM(BhytNsddDongDuToan),0) BhytNsddDongDuToan,
	isnull(SUM(BhytNldDongHachToan),0) BhytNldDongHachToan,
	isnull(SUM(BhytNsddDongHachToan),0) BhytNsddDongHachToan,
	isnull(SUM(BHYTTongCongDuToan),0) BHYTTongCongDuToan,
	isnull(SUM(BHYTTongCongHachToan),0) BHYTTongCongHachToan
	INTO #tempSumDataHachToan1
	FROM #tempDataHachToan1

		Update A
	SET A.BhytNldDongDuToan=B.BhytNldDongDuToan,
		A.BhytNsddDongDuToan=B.BhytNsddDongDuToan,
		A.BhytNldDongHachToan=B.BhytNldDongHachToan,
		A.BhytNsddDongHachToan=B.BhytNsddDongHachToan,
		A.BHYTTongCongDuToan=B.BHYTTongCongDuToan,
		A.BHYTTongCongHachToan=B.BHYTTongCongHachToan
	FROM #tempHachToan A , #tempSumDataHachToan1 B

	-- Gop Du toan
	SELECT * INTO #TempResultDuToan1 FROM 
	( 
	SELECT * FROM #tempDuToan
	Union all
	SELECT * FROM #tempDataDuToan1
	) DuToan

	-- Gop vao Hach Toan
	SELECT * INTO #TempResultHachToan1 FROM 
	(
	SELECT * FROM #tempHachToan
	Union all
	SELECT * FROM #tempDataHachToan1
	) HachToan

	-- Tra ra kết quả Result
	SELECT * FROM 
	( 
	SELECT * FROM #TempResultDuToan1
	UNION ALL
	SELECT * FROM #TempResultHachToan1 ) Result

	DROP TABLE #tempDataDuToan1
	DROP TABLE #tempDataHachToan1
	DROP TABLE #tempSumDataDuToan1
	DROP TABLE #tempSumDataHachToan1
	DROP TABLE #TempResultDuToan1
	DROP TABLE #TempResultHachToan1
	end
	DROP TABLE #tempDuToan
	DROP TABLE #tempHachToan
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 10/25/2024 6:25:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 10/25/2024 6:25:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200),
	@IsMillionRound bit
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTT]') AND type in (N'U')) drop table TBL_DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHXH]') AND type in (N'U')) drop table TBL_THU_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHTN]') AND type in (N'U')) drop table TBL_THU_BHTN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_QN]') AND type in (N'U')) drop table TBL_THU_BHYT_QN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_NLD]') AND type in (N'U')) drop table TBL_THU_BHYT_NLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTTM]') AND type in (N'U')) drop table TBL_DTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_TN]') AND type in (N'U')) drop table TBL_THU_BHYT_TN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU]') AND type in (N'U')) drop table TBL_THU;
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.*, dv.iKhoi into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec 
		and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
		and Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
	--Thu BHXH
	select * into TBL_THU_BHXH from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Thu BHXH' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai

	union all

	select 2 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NSD, 0)) END)),0) fTongSo,
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NSD,0)) END), 0) fNSD, 
			null sLoai
	--from TBL_DTT where iKhoi = 2 --sXauNoiMa like '9020001%'
	from TBL_DTT where sXauNoiMa like '9020001%'

	union all

	select 3 rowNum, 
		   0 bHangCha, 
		   null stt, 
		   N'- Đơn vị hạch toán' sMoTa, 
		   ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(Sum(isnull(fBHXH_NSD, 0))/1000000, 0)* 1000000 ELSE Sum(isnull(fBHXH_NSD, 0)) END)),0) fTongSo, 
		   ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NLD,0)) END), 0) fNLD, 
		   ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHXH_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHXH_NSD,0)) END),0) fNSD,
		   null sLoai
	--from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	from TBL_DTT where sXauNoiMa like '9020002%'
	) thubhxh

	update TBL_THU_BHXH set fTongSo = (select sum(fTongSo) from TBL_THU_BHXH where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHXH where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHXH where bHangCha = 0)
						where bHangCha = 1

	--Thu BHTN
	select * into TBL_THU_BHTN from
	(select 4 rowNum, 1 bHangCha, '2' stt, N'Thu BHTN' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai

	union all

	select 5 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NSD,0)) END),0) fNSD, 
			null sLoai
	--from TBL_DTT where iKhoi = 2 --sXauNoiMa like '9020001%'
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all

	select 6 rowNum, 
			0 bHangCha,
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHTN_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHTN_NSD,0)) END),0) fNSD,  
			null sLoai
	--from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	from TBL_DTT where sXauNoiMa like '9020002%'
	) thubhtn

	update TBL_THU_BHTN set fTongSo = (select sum(fTongSo) from TBL_THU_BHTN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHTN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHTN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT quân nhân
	select * into TBL_THU_BHYT_QN from
	(select 7 rowNum, 1 bHangCha, '3' stt, N'Thu BHYT quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai

	union all
	
	select 8 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD,0)) END),0) fNSD, 
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%')
	--					and iKhoi = 2
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' )
					
	union all
	
	select 9 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(Sum(isnull(fBHYT_NSD, 0))/1000000, 0)* 1000000 ELSE Sum(isnull(fBHYT_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD,0)) END),0) fNSD, 
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%')
	--					and iKhoi <> 2
	from TBL_DTT where (sXauNoiMa like '9020002-010-011-0001%' )
						) thubhytquannhan

	update TBL_THU_BHYT_QN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_QN where bHangCha = 0)
	where bHangCha = 1

	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_NLD from
	(select 10 rowNum, 1 bHangCha, '4' stt, N'Thu BHYT người lao động' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 11 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD,0)) END),0) fNSD,
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020002-010-011-0002%')
	--					and iKhoi = 2
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%')
						
	union all
	
	select 12 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND(((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD, 0)) END) + (CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD, 0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD, 0)) END)),0) fTongSo, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NLD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NLD,0)) END),0) fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(isnull(fBHYT_NSD,0))/1000000, 0)* 1000000 ELSE sum(isnull(fBHYT_NSD,0)) END),0) fNSD,
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020002-010-011-0002%')
	--					and iKhoi <> 2
	from TBL_DTT where ( sXauNoiMa like '9020002-010-011-0002%')
						) thubhytnld

	update TBL_THU_BHYT_NLD set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_NLD where bHangCha = 0)
						where bHangCha = 1
	
	--Dữ liệu phân bổ dự toán thu mua BHYT
	select ctct.*, dv.iKhoi into TBL_DTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	join  BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_TN from
	(select 13 rowNum, 1 bHangCha, '5' stt, N'Thu BHYT thân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN' sLoai
	
	union all
	
	select 14 rowNum, 1 bHangCha, 'a' stt, N'Quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	union all
	select 15 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fTongSo, 
			null fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fNSD, 
			N'BHYT_THANNHAN_QN' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' or sXauNoiMa like '9030001-010-011-0002%')
	--					and iKhoi = 2
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' )
				
	union all
	select 16 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fTongSo, 
			null fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fNSD, 
			N'BHYT_THANNHAN_QN' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' or sXauNoiMa like '9030001-010-011-0002%')
	--					and iKhoi <> 2
	from TBL_DTTM where ( sXauNoiMa like '9030002-010-011-0002%')
						
	union all

	select 17 rowNum,
			1 bHangCha, 
			'b' stt, 
			N'Công nhân, VCQP' sMoTa, 
			null fTongSo, 
			null fNLD, 
			null fNSD, 
			N'BHYT_THANNHAN_VCQP' sLoai

	union all

	select 18 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fTongSo, 
			null fNLD, 
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fNSD, 
			N'BHYT_THANNHAN_VCQP' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%')
	--					and iKhoi = 2
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0000%' )

	union all

	select 19 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa,
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fTongSo,
			null fNLD,
			ROUND((CASE WHEN @IsMillionRound = 1 THEN ROUND(sum(fDuToan)/1000000, 0)* 1000000 ELSE sum(fDuToan) END),0) fNSD,
			N'BHYT_THANNHAN_VCQP' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%')
	--					and iKhoi <> 2
	from TBL_DTTM where ( sXauNoiMa like '9030002-010-011-0001%')
						
	) thubhytnld

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_QN'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_VCQP'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN'

	select * into TBL_THU from
	(select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHXH
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHTN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_QN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_NLD
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_TN) tblthu

	--Result
	--if (@IsMillionRound = 1)
	--select
	--rowNum, 
	--bHangCha, 
	--stt, 
	--sMoTa, 
	--round(fTongSo / 1000000, 0) * 1000000 /@DVT fTongSo, 
	--round(fNLD / 1000000, 0) * 1000000 /@DVT fNLD, 
	--round(fNSD / 1000000, 0) * 1000000 /@DVT fNSD
	--from TBL_THU
	--else
	select
	rowNum, 
	bHangCha, 
	stt, 
	sMoTa, 
	fTongSo/@DVT fTongSo, 
	fNLD/@DVT fNLD, 
	fNSD/@DVT fNSD
	from TBL_THU

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTT]') AND type in (N'U')) drop table TBL_DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHXH]') AND type in (N'U')) drop table TBL_THU_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHTN]') AND type in (N'U')) drop table TBL_THU_BHTN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_QN]') AND type in (N'U')) drop table TBL_THU_BHYT_QN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_NLD]') AND type in (N'U')) drop table TBL_THU_BHYT_NLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTTM]') AND type in (N'U')) drop table TBL_DTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_TN]') AND type in (N'U')) drop table TBL_THU_BHYT_TN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU]') AND type in (N'U')) drop table TBL_THU;

END
;
;
;
;
;
;
;
;
GO
