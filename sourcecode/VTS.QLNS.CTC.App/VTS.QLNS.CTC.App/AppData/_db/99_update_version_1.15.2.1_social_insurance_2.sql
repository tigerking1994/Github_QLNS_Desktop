/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 12/3/2024 10:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]    Script Date: 12/3/2024 10:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]    Script Date: 12/3/2024 10:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]    Script Date: 12/3/2024 10:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]    Script Date: 12/3/2024 10:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]    Script Date: 12/3/2024 10:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]    Script Date: 12/3/2024 10:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]    Script Date: 12/3/2024 10:10:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi]    Script Date: 12/3/2024 10:10:02 AM ******/
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
and ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung]    Script Date: 12/3/2024 10:10:02 AM ******/
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
and ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_bandau_or_bosung_theokhoi]    Script Date: 12/3/2024 10:10:02 AM ******/
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
and ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
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
 and A.iiD_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
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
 and A.iiD_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_donvi_theokhoi]    Script Date: 12/3/2024 10:10:02 AM ******/
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
and ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
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
where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
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
 and A.iiD_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
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
 and A.iiD_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]    Script Date: 12/3/2024 10:10:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	@IsMillionRound bit
	--@DotNhan int
AS
BEGIN

		select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
		right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
		where ct.iNamChungTu=@INamLamViec
		--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
		and ctct.sMaLoaiChi=@MaLoaiChi
		--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
		and ct.ID in (select * from f_split(@IdChungTu))
		--and ct.iLoaiDotNhanPhanBo=@DotNhan
		--and ct.sSoQuyetDinh=@SoQuyetdinh
		and ctct.iID_MaDonVi IN (SELECT * FROM f_split(  @IdMaDonVi));

		select 
			 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
			, dv.sTenDonVi
			, dv.iID_MaDonVi
			, SUM(CASE WHEN @IsMillionRound = 1 THEN ROUND(ctct.fTongTien/1000000, 0)* 1000000 ELSE ctct.fTongTien END) as fTongTienDuToan
			, 0 IsHangCha
			, 0 RowNumber
			into #temp1
		from 
		#tempall ctct
		left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
		where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
		and
		dv.iNamLamViec=@INamLamViec
		and
		ctct.iNamLamViec=@INamLamViec
		group by  dv.iID_MaDonVi, dv.sTenDonVi 

		select rs.fTongTienDuToan/@Donvitinh fTongTienDuToan
			, rs.iID_MaDonVi
			, rs.IsHangCha
			, rs.RowNumber
			, rs.sTenDonVi
			, rs.STT
			from #temp1 rs;
DROP TABLE #tempall
DROP TABLE #temp1
END
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]    Script Date: 12/3/2024 10:10:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bandau_or_bosung]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	@DotNhan int,
	@IsMillionRound bit
AS
BEGIN

		select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
		right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
		where ct.iNamChungTu=@INamLamViec
		--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
		and ctct.sMaLoaiChi=@MaLoaiChi
		--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
		and ct.ID in (select * from f_split(@IdChungTu))
		and ct.iLoaiDotNhanPhanBo=@DotNhan
		--and ct.sSoQuyetDinh=@SoQuyetdinh
		and ctct.iID_MaDonVi IN (SELECT * FROM f_split(  @IdMaDonVi));

		select 
			 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
			, dv.sTenDonVi
			, dv.iID_MaDonVi
			, SUM(CASE WHEN @IsMillionRound = 1 THEN ROUND(ctct.fTongTien/1000000, 0)* 1000000 ELSE ctct.fTongTien END) as fTongTienDuToan
			, 0 IsHangCha
			, 0 RowNumber
			into #temp1
		from 
		#tempall ctct
		left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
		where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
		and
		dv.iNamLamViec=@INamLamViec
		and
		ctct.iNamLamViec=@INamLamViec
		group by  dv.iID_MaDonVi, dv.sTenDonVi 

		select rs.fTongTienDuToan/@Donvitinh fTongTienDuToan
					, rs.iID_MaDonVi
					, rs.IsHangCha
					, rs.RowNumber
					, rs.sTenDonVi
					, rs.STT
					from #temp1 rs;

DROP TABLE #tempall
DROP TABLE #temp1
END
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]    Script Date: 12/3/2024 10:10:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_bd_or_bs_theokhoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	@DotNhan int,
	@IsMillionRound bit
AS
BEGIN

	select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
	right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
	where ct.iNamChungTu=@INamLamViec
	--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
	and ctct.sMaLoaiChi=@MaLoaiChi
	--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
	and ct.iLoaiDotNhanPhanBo=@DotNhan
	and ct.ID in (select * from f_split(@IdChungTu))
	--and ct.sSoQuyetDinh=@SoQuyetdinh
	and ctct.iID_MaDonVi IN (SELECT * FROM f_split(  @IdMaDonVi));

	select 
		 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
		, dv.sTenDonVi
		, dv.iID_MaDonVi
		, SUM(CASE WHEN @IsMillionRound = 1 THEN ROUND(ctct.fTongTien/1000000, 0)* 1000000 ELSE ctct.fTongTien END) as fTongTienDuToan
		, 0 IsHangCha
		, 0 RowNumber
		into #temp1
	from 
	#tempall ctct
	left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
	where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
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
	CREATE TABLE #tempDvKDT(STT VARCHAR(6), sTenDonVi nvarchar(50), iID_MaDonVi varchar(50), fTongTienDuToan float, IsHangCha int, RowNumber int)
	INSERT INTO #tempDvKDT(STT, sTenDonVi, iID_MaDonVi, fTongTienDuToan, IsHangCha, RowNumber)
		SELECT B.* 
		from #temp1 B
	LEFT JOIN DonVi A ON A.iID_MaDonVi=B.iID_MaDonVi
	where A.iNamLamViec=@INamLamViec
	 and A.iiD_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
	 and A.iKhoi=2
	 ORDER BY B.STT;
	------ Create table Stt
		Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
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
		 and A.iiD_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
		 and A.iKhoi!=2

	 ------ Create table Stt
		Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
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
		SELECT * into #tblresult
		FROM
		(
			SELECT * FROM #tempDataDVDT
			UNION ALL 
			SELECT * FROM #tempDataDVHT
		)tempDataAll

		select rs.STT
			 , rs.iID_MaDonVi
			 , rs.IsHangCha
			 , rs.RowNumber
			 , rs.sTenDonVi
			 , rs.fTongTienDuToan/@Donvitinh fTongTienDuToan
			 , rs.iLoai
		FROM  #tblresult rs
		order by rs.iLoai,rs.iID_MaDonVi

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
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]    Script Date: 12/3/2024 10:10:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_inchitieu_KPQL_KCB_Khac_donvi_theokhoi]
	@INamLamViec int,
	@IdMaDonVi nvarchar(MAX),
	@IDLoaiChi nvarchar(MAX),
	@MaLoaiChi nvarchar(MAX),
	@IdChungTu nvarchar(Max),
	--@SNgayQuyetDinh nvarchar(MAX),
	--@SoQuyetdinh nvarchar(MAX),
	@Donvitinh int,
	@DotNhan int,
	@IsMillionRound bit
AS
BEGIN

	select ctct.* into #tempall from BH_DTC_PhanBoDuToanChi ct
	right join BH_DTC_PhanBoDuToanChi_ChiTiet ctct on ct.ID=ctct.iID_DTC_PhanBoDuToanChi
	where ct.iNamChungTu=@INamLamViec
	--and ct.iID_LoaiDanhMucChi=@IDLoaiChi
	and ctct.sMaLoaiChi=@MaLoaiChi
	--and CONVERT(nvarchar ,ct.dNgayQuyetDinh,103)<= @SNgayQuyetDinh
	and ct.iLoaiDotNhanPhanBo=@DotNhan
	and ct.ID in (select * from f_split(@IdChungTu))
	--and ct.sSoQuyetDinh=@SoQuyetdinh
	and ctct.iID_MaDonVi IN (SELECT * FROM f_split(  @IdMaDonVi));

	select 
		 CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT
		, dv.sTenDonVi
		, dv.iID_MaDonVi
		, SUM(CASE WHEN @IsMillionRound = 1 THEN ROUND(ctct.fTongTien/1000000, 0)* 1000000 ELSE ctct.fTongTien END) as fTongTienDuToan
		, 0 IsHangCha
		, 0 RowNumber
		into #temp1
	from 
	#tempall ctct
	left join DonVi dv on ctct.iID_MaDonVi= dv.iID_MaDonVi
	where ctct.iID_MaDonVi in  ( SELECT * FROM f_split(@IdMaDonVi))
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
	CREATE TABLE #tempDvKDT(STT VARCHAR(6), sTenDonVi nvarchar(50), iID_MaDonVi varchar(50), fTongTienDuToan float, IsHangCha int, RowNumber int)
	INSERT INTO #tempDvKDT(STT, sTenDonVi, iID_MaDonVi, fTongTienDuToan, IsHangCha, RowNumber)
		SELECT B.* 
		from #temp1 B
	LEFT JOIN DonVi A ON A.iID_MaDonVi=B.iID_MaDonVi
	where A.iNamLamViec=@INamLamViec
	 and A.iiD_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
	 and A.iKhoi=2
	 ORDER BY B.STT;
	------ Create table Stt
		Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
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
		 and A.iiD_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))
		 and A.iKhoi!=2

	 ------ Create table Stt
		Select  CAST(ROW_NUMBER() OVER (ORDER BY dv.iID_MaDonVi) AS VARCHAR(6)) STT,
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
		SELECT * into #tblresult
		FROM
		(
			SELECT * FROM #tempDataDVDT
			UNION ALL 
			SELECT * FROM #tempDataDVHT
		)tempDataAll

	select rs.STT
		 , rs.iID_MaDonVi
		 , rs.IsHangCha
		 , rs.RowNumber
		 , rs.sTenDonVi
		 , rs.fTongTienDuToan/@Donvitinh fTongTienDuToan
		 , rs.iLoai
	FROM  #tblresult rs
	order by rs.iLoai,rs.iID_MaDonVi

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
DROP TABLE #tblresult
END
;
;
;
;
;
;
;
;
;
;
;
GO

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (newid(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_NganSach_NhaNuoc_Quy', NULL, N'rpt_ThuNop_NganSach_NhaNuoc_Quy', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP NGÂN SÁCH NHÀ NƯỚC', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm 2024 của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (newid(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_NganSach_NhaNuoc_Thang', NULL, N'rpt_ThuNop_NganSach_NhaNuoc_Thang', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP NGÂN SÁCH NHÀ NƯỚC', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm 2024 của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (newid(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_NganSach_NhaNuoc_Nam', NULL, N'rpt_ThuNop_NganSach_NhaNuoc_Nam', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP NGÂN SÁCH NHÀ NƯỚC NĂM 2024', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm 2024 của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (newid(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_QuocPhong_Thang', NULL, N'rpt_ThuNop_QuocPhong_Thang', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP BỘ QUỐC PHÒNG', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm 2024 của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (newid(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_QuocPhong_Nam', NULL, N'rpt_ThuNop_QuocPhong_Nam', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP BỘ QUỐC PHÒNG NĂM 2024', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm 2024 của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (newid(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rpt_ThuNop_QuocPhong_Quy', NULL, N'rpt_ThuNop_QuocPhong_Quy', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO CÁC KHOẢN THU NỘP BỘ QUỐC PHÒNG', NULL, N'Kèm theo Công văn số...../CTC-KHNS ngày...tháng....năm 2024 của Cục Tài chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitietHD4554]    Script Date: 12/3/2024 3:12:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_qt_chungtu_chitietHD4554]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_qt_chungtu_chitietHD4554]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_HD4554_tao_tonghop]    Script Date: 12/3/2024 3:12:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet_HD4554_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet_HD4554_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_HD4554_tao_tonghop]    Script Date: 12/3/2024 3:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_chungtu_chitiet_HD4554_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfBudget int,
	@BudgetSource int,
	@YearOfWork int,
	@Type nvarchar(10),
	@QuarterMonthType int,
	@QuarterMonth int,
	@AgencyId nvarchar(10),
	@UserName nvarchar(100)
AS
BEGIN

INSERT INTO [dbo].[TN_QuyetToan_ChungTuChiTiet_HD4554]
           ([Id]
           ,[bHangCha]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[fSoTien]
           ,[iID_MaDonVi]
           ,[iID_TN_QTChungTu]
           ,[iNamLamViec]
           ,[iNamNganSach]
           ,[iNguonNganSach]
           ,[iThangQuy]
           ,[iThangQuyLoai]
           ,[sGhiChu]
           ,[sK]
           ,[sL]
           ,[sLNS]
           ,[sM]
           ,[sNG]
           ,[sNguoiSua]
           ,[sNguoiTao]
           ,[sTM]
           ,[sTNG]
           ,[sTNG1]
           ,[sTNG2]
           ,[sTNG3]
           ,[sTTM]
           ,[sXauNoiMa]
		   ,fSoTien_DeNghi)
	 SELECT
          NEWID(),
          mlns.bHangCha,
		  null,
		  GETDATE(),
		  SUM(fSoTien),
		  @AgencyId,
          @VoucherId,
          @YearOfWork,
		  @YearOfBudget,
          @BudgetSource,
          @QuarterMonth,
          @QuarterMonthType,
          null,
           mlns.sK,
           mlns.sL,
           mlns.sLNS,
           mlns.sM,
           mlns.sNG, 
           null,
           @UserName,
           mlns.sTM, 
           mlns.sTNG, 
           mlns.sTNG1, 
           mlns.sTNG2, 
           mlns.sTNG3,
           
		   mlns.sTTM, 
           mlns.sXauNoiMa,
		   SUM(fSoTien_DeNghi)

	FROM TN_QuyetToan_ChungTuChiTiet_HD4554 ctct
	INNER JOIN NS_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa AND mlns.iNamLamViec = ctct.iNamLamViec
	INNER JOIN TN_QuyetToan_ChungTu_HD4554 ct ON ctct.iID_TN_QTChungTu = ct.Id
	WHERE ct.Id IN (SELECT * FROM f_split(@VoucherIds)) 
	GROUP BY mlns.sXauNoiMa, mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3, mlns.bHangCha;

	-- Danh dau chung tu da tong hop
	UPDATE TN_QuyetToan_ChungTu_HD4554 SET bDaTongHop = 1 
	WHERE Id in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitietHD4554]    Script Date: 12/3/2024 3:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_qt_chungtu_chitietHD4554]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int
AS
BEGIN
	SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.iID_TN_QTChungTu as IIdTnQtChungTu,
		mlns.iID_MLNS as iID_MLNS, 
		mlns.iID_MLNS_Cha as iID_MLNS_Cha,
		isnull(ctct.sNguoiTao, '') as sNguoiTao,
		isnull(ctct.sNguoiSua, '') as sNguoiSua,
		ctct.fSoTien,
		ctct.fSoTien_DeNghi as fSoTienDeNghi,
		isnull(mlns.sMoTa, '') as sNoidung,
		isnull(ctct.iThangQuyLoai, 0) as IThangQuyLoai,
		isnull(ctct.iThangQuy, 1) as IThangQuy,
		mlns.bHangCha as bHangCha,
		isnull(ctct.iNamNganSach, @YearOfBudget) as iNamNganSach,
		isnull(ctct.iNguonNganSach, @BudgetSource) as iNguonNganSach,
		mlns.iNamLamViec as iNamLamViec,
		mlns.iTrangThai as ITrangThai,
		isnull(ctct.iID_MaDonVi, '') as IIdMaDonVi,
		isnull(ctct.sGhiChu, '') as GhiChu,
		ctct.dNgayTao as dNgayTao,
		ctct.dNgaySua as dNgaySua,
		 mlns.sK as sK,
		 mlns.sLNS as sLNS,
		 mlns.sL as sL,
		 mlns.sM as sM,
		 mlns.sNG as sNG,
		 mlns.sTM as sTM,
		 mlns.sTNG as sTNG,
		 mlns.sTNG1 as sTNG1,
		 mlns.sTNG2 as sTNG2,
		 mlns.sTNG3 as sTNG3,
		 mlns.sTTM as sTTM,
		mlns.sXauNoiMa,
		mlns.sQuyetToanChiTietToi,
		mlns.sChiTietToi

	FROM  (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	LEFT JOIN (
		SELECT
			*
		FROM
			TN_QuyetToan_ChungTuChiTiet_HD4554
		WHERE
			iNamLamViec = @YearOfWork
			AND iNamNganSach = @YearOfBudget
			AND iNguonNganSach = @BudgetSource
			AND iID_TN_QTChungTu = @ChungTuId
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.sLNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sXauNoiMa
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 12/5/2024 8:18:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]    Script Date: 12/5/2024 8:18:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 12/5/2024 8:18:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getTienthuchien6thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 12/5/2024 8:18:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 12/5/2024 8:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_BH_DTC_ChiTiet ,
		ct.iID_BH_DTC ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sM,
		ct.sTM,
		ct.sNoiDung,
		ct.sXauNoiMa,
		ct.iNamLamViec,
		ct.fTienDuToanDuocGiao,
		ct.fTienThucHien06ThangDauNam,
		ct.fTienUocThucHien06ThangCuoiNam,
		(isnull(ct.fTienThucHien06ThangDauNam,0) + isnull(ct.fTienUocThucHien06ThangCuoiNam,0)) fTienUocThucHienCaNam,

		(CASE  WHEN isnull(ct.fTienUocThucHienCaNam,0) > isnull(ct.fTienDuToanDuocGiao,0) THEN isnull(ct.fTienUocThucHienCaNam,0) - isnull(ct.fTienDuToanDuocGiao,0) ELSE 0 END ) as fTienSoSanhTang,
		(CASE  WHEN isnull(ct.fTienDuToanDuocGiao,0) > isnull(ct.fTienUocThucHienCaNam,0) THEN isnull(ct.fTienDuToanDuocGiao,0) - isnull(ct.fTienUocThucHienCaNam,0) ELSE 0 END ) as fTienSoSanhGiam,

		ct.sTenDonVi,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu
	FROM
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 12/5/2024 8:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@IID_LoaiChi nvarchar(100),
	@SMaLoaiChi nvarchar(50),
	@SLNS nvarchar(100),
	@DNgayChungTu datetime
AS
BEGIN
		Create table #TempData6thang
		(
			iID_MaDonVi nvarchar(50),
			iID_MucLucNganSach uniqueidentifier,
			sM nvarchar(50),
			sTM nvarchar(50),
			sNoiDung nvarchar(max),
			iNamLamViec int ,
			fTienThucHien06ThangDauNam float
		);

		---- Get data 6 thang tu qtc quy 
		---- Data 6 thang Chi các chế độ BHXH
		 IF(@SLNS ='9010001,9010002')
		 	INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,sM,sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sLoaiTroCap sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTongTien_DeNghi) as fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_CheDoBHXH ct
				LEFT JOIN BH_QTC_Quy_CheDoBHXH_ChiTiet ctct ON ct.ID_QTC_Quy_CheDoBHXH=ctct.iID_QTC_Quy_CheDoBHXH
				WHERE ct.iNamChungTu=@NamLamViec
				AND ctct.iIDMaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sLoaiTroCap,iNamLamViec;
		 ---- Data 6 thang Chi kinh phí quản lý BHXH, BHYT
		ELSE IF(@SLNS='9010003')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KinhPhiQuanLy ct
				LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct ON ct.ID_QTC_Quy_KinhPhiQuanLy=ctct.iID_QTC_Quy_KinhPhiQuanLy
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 
		---- Data Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ
		ELSE IF(@SLNS='905,9050001,9050002')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KinhPhiQuanLy ct
				LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct ON ct.ID_QTC_Quy_KinhPhiQuanLy=ctct.iID_QTC_Quy_KinhPhiQuanLy
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Chi kinh phí KCB tại quân y đơn vị --comment 10/01/2024 		ELSE IF(@SLNS='9010004,9010005')
		ELSE IF(@SLNS='9010004')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iID_MaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KCB ct
				LEFT JOIN BH_QTC_Quy_KCB_ChiTiet ctct ON ct.ID_QTC_Quy_KCB=ctct.iID_QTC_Quy_KCB
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  ctct.iID_MaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		   ---- Data 6 thang Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân
		ELSE IF(@SLNS='9010008')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Chi hỗ trợ BHTN 
		ELSE IF(@SLNS='9010009')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Kinh phí mua sắm trang thiết bị y tế 
		   ELSE
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

			SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as IID_MucLucNganSach,
			   tbl.fTienThucHien06ThangDauNam,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #TempData6thang  tbl 
			on tbl.iID_MucLucNganSach=dm.iID_MLNS
			where dm.iNamLamViec=@NamLamViec and dm.sLNS in (select * from f_split(@SLNS))
			order by dm.sXauNoiMa
			drop table #TempData6thang
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]    Script Date: 12/5/2024 8:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet_clone]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@loaiDanhMucCapChi nvarchar(100),
	@SLNS nvarchar(100),
	@ngayChungTu date
AS
BEGIN
SELECT 
		
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec,
	
		Sum(ISNULL(ct.fTienTuChi, 0))as FTienTuChi
		into #temp1
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_PhanBoDuToanChi bh
			JOIN 
				BH_DTC_PhanBoDuToanChi_ChiTiet bhct ON bh.ID = bhct.iID_DTC_PhanBoDuToanChi 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
				AND bh.iNamChungTu=@NamLamViec
		) ct
		Group by 
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec
		;

		SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as iID_MLNS,
			   tbl.FTienTuChi as fTienTuChi,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung,
			   dm.sXauNoiMa
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #temp1  tbl 
			on dm.sXauNoiMa=tbl.sXauNoiMa
			where dm.iNamLamViec=@NamLamViec 
			and dm.sLNS in (select * from f_split(@SLNS))
			and dm.bHangChaDuToan is not null
			and dm.iTrangThai=1
			order by dm.sXauNoiMa

			drop table #temp1

END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 12/5/2024 8:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(max),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int,
	@Loai int
AS
BEGIN

	SELECT ML.*
			into #tblml
	FROM BH_DM_MucLucNganSach ML
	WHERE ML.sLNS IN  (SELECT * FROM f_split(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
			AND ML.bHangCha IS NOT NULL
			AND Ml.iTrangThai=1
	ORDER BY sXauNoiMa

				
	SELECT 
		distinct
		ct.dNgayChungTu
		, ct.iID_MaDonVi as  iID_MaDonVi
		into #tempTblNgayChungTuDonVi
	FROM BH_DTC_DieuChinhDuToanChi ct
	left join  BH_DTC_DieuChinhDuToanChi_ChiTiet ctct on ct.iID_BH_DTC=ctct.iID_BH_DTC
	where ctct.iNamLamViec=@NamLamViec
		and ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
		and ct.iID_LoaiCap=@IDLoaiChi

	--- Chung tu thuong
	IF (@Loai=0)
	SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
			LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @NamLamViec
				And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date)
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa=ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ml.sDuToanChiTietToi,
		dt.fTienTuChi
		ORDER BY sXauNoiMa
		--- Chung tu tong hop
		ELSE 
		SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangChaDuToanDieuChinh IsHangCha,
		ml.sDuToanChiTietToi,
		ct.iNamLamViec,
		ISNULL((dt.fTienTuChi),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(dt.fTienTuChi) - (SUM(ct.fTienUocThucHienCaNam)),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		LEFT JOIN (
					SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
							LEFT JOIN #tempTblNgayChungTuDonVi ngayChungTuDonVi ON  ctct.iID_MaDonVi=ngayChungTuDonVi.iID_MaDonVi
							WHERE ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
							AND BIsKhoa = 1
							And cast(ct.DngayChungTu as date) <= cast(ngayChungTuDonVi.dNgayChungTu as date)
							--AND ct.iID_LoaiDanhMucChi=@IDLoaiChi
							AND ct.iNamLamViec = @NamLamViec
							GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = ml.sXauNoiMa 
		WHERE ml.iNamLamViec=@NamLamViec
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangChaDuToanDieuChinh,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec,
		ml.sDuToanChiTietToi,
		dt.fTienTuChi
		ORDER BY sXauNoiMa;

		DROP TABLE #tblml
		drop table #tempTblNgayChungTuDonVi

END
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi_clone]    Script Date: 12/6/2024 10:11:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 12/6/2024 10:11:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]    Script Date: 12/6/2024 10:11:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_dtdc_clone]    Script Date: 12/6/2024 10:11:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_dtdc_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_dtdc_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_dtdc_clone]    Script Date: 12/6/2024 10:11:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_get_data_dtdc_clone]
	@NamLamViec int,
	@SoChungTu nvarchar(500),
	@SLNS nvarchar(500),
	@IDLoaiChi nvarchar(500),
	-- add them
	@MaDonVi nvarchar(500)
AS
BEGIN

SELECT ML.sLNS,
		ML.sL,
		ML.sk,
		ML.sM,
		dctcChitiet.fTienThucHien06ThangDauNam / 1 as fTienThucHien06ThangDauNam,
		dctcChitiet.fTienUocThucHien06ThangCuoiNam / 1 as fTienUocThucHien06ThangCuoiNam,
		dctcChitiet.iID_MaDonVi,
		dctcChitiet.sTenDonVi
		,(Case when ML.sLNS='9010001' then (ML.sLNS +'-' +
		ML.sL + '-' +
		ML.sk + '-' +
		ML.sM)
		when ML.sLNS='9010002' then (ML.sLNS +'-' +
		ML.sL + '-' +
		ML.sk + '-' +
		ML.sM)
		when ML.sLNS='9010003' then ML.sLNS
		when ML.sLNS='9010004' then ML.sLNS
		when ML.sLNS='9010006' then ML.sLNS
		when ML.sLNS='9010008' then ML.sLNS
		when ML.sLNS='9010009' then ML.sLNS
		when ML.sLNS='9010010' then ML.sLNS
		else '' end) as sXauNoiMa
		into #tempDtc_ChiTiet
		FROM BH_DM_MucLucNganSach ML
		left join (select sXauNoiMa,
							SUM(fTienThucHien06ThangDauNam) as fTienThucHien06ThangDauNam,
							SUM (fTienUocThucHien06ThangCuoiNam) fTienUocThucHien06ThangCuoiNam,
							dv.iID_MaDonVi,
							dv.sTenDonVi
							from BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
							left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
						where ctct.iID_BH_DTC in 
								(Select iID_BH_DTC from BH_DTC_DieuChinhDuToanChi
								where 	iID_MaDonVi IN 
								(SELECT * FROM f_split(@MaDonVi))
								and bDaTongHop=1
								AND sSoChungTu in (SELECT * FROM f_split(@SoChungTu))
								AND iNamLamViec=2024
								AND sMaLoaiChi in (SELECT * FROM f_split(@IDLoaiChi)))
							AND dv.iTrangThai=1
							AND dv.iNamLamViec=2024
					group by sXauNoiMa,dv.iID_MaDonVi,dv.sTenDonVi )dctcChitiet on ML.sXauNoiMa=dctcChitiet.sXauNoiMa
		WHERE ML.sLNS IN  (SELECT * FROM f_split(@SLNS))
				AND ML.iNamLamViec=2024
				AND ML.bHangChaDuToanDieuChinh is not null
				AND ML.iTrangThai=1
		ORDER BY ML.sXauNoiMa


					SELECT ctct.sXauNoiMa
					, SUM(ISNULL(ctct.fTienTuChi,0))  FTienTuChi
					, ctct.iID_MaDonVi
					into #tempPhanBoTheoDonVi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM f_split(@MaDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = 2024
				GROUP BY ctct.sXauNoiMa, ctct.iID_MaDonVi


		select 
		SUM(fTienThucHien06ThangDauNam) as fTienThucHien06ThangDauNam,
		SUM (fTienUocThucHien06ThangCuoiNam) as fTienUocThucHien06ThangCuoiNam,
		sXauNoiMa,
		iID_MaDonVi
		into #tempSumDtc_ChiTiet
		from #tempDtc_ChiTiet
		where fTienThucHien06ThangDauNam>0 or fTienUocThucHien06ThangCuoiNam>0
		group by sXauNoiMa,iID_MaDonVi

		select
		 phanbo.FTienTuChi
		, CASE WHEN ctct.fTienThucHien06ThangDauNam + ctct.fTienUocThucHien06ThangCuoiNam > FTienTuChi THEN  (ABS(FTienTuChi - ctct.fTienThucHien06ThangDauNam + ctct.fTienUocThucHien06ThangCuoiNam))
		ELSE 0 END AS fTienSoSanhGiam
		, CASE WHEN ctct.fTienThucHien06ThangDauNam + ctct.fTienUocThucHien06ThangCuoiNam < FTienTuChi THEN   ABS(ctct.fTienThucHien06ThangDauNam + ctct.fTienUocThucHien06ThangCuoiNam - FTienTuChi) 
		ELSE 0 END AS fTienSoSanhTang
		, ctct.sXauNoiMa
		, ctct.iID_MaDonVi
		into #tempResult
		from #tempSumDtc_ChiTiet ctct
		left join #tempPhanBoTheoDonVi phanbo on ctct.sXauNoiMa=phanbo.sXauNoiMa and ctct.iID_MaDonVi=phanbo.iID_MaDonVi

		Select 
		 ctct.iID_MaDonVi as IIdMaDonVi
		, ctct.sXauNoiMa
		,CASE WHEN (ISNULL(ctct.fTienSoSanhTang,0))-  (ISNULL(ctct.fTienSoSanhGiam,0)) >0 THEN (ISNULL(ctct.fTienSoSanhTang,0))-  (ISNULL(ctct.fTienSoSanhGiam,0))
				ELSE -(((ISNULL(ctct.fTienSoSanhGiam,0))-ISNULL(ctct.fTienSoSanhTang,0))) END FTienTangGiam
		from #tempResult ctct

		drop table #tempResult
		drop table #tempDtc_ChiTiet
		drop table #tempPhanBoTheoDonVi
		drop table #tempSumDtc_ChiTiet

	--select
	--	ct.iID_MaDonVi IIdMaDonVi,
	--	mlns.sXauNoiMa,
	--	mlns.iID_MLNS IID_MucLucNganSach,
	--	mlns.sLNS,
	--	CASE WHEN (ISNULL(ctct.fTienSoSanhTang,0))-  (ISNULL(ctct.fTienSoSanhGiam,0)) >0 THEN (ISNULL(ctct.fTienSoSanhTang,0))-  (ISNULL(ctct.fTienSoSanhGiam,0))
	--			ELSE -(((ISNULL(ctct.fTienSoSanhGiam,0))-ISNULL(ctct.fTienSoSanhTang,0))) END FTienTangGiam
	--	into #temp
	--from
	--BH_DM_MucLucNganSach mlns
	--left join
	--BH_DTC_DieuChinhDuToanChi_ChiTiet ctct on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	--join
	--(select * from BH_DTC_DieuChinhDuToanChi
	--	where iNamLamViec = @NamLamViec and bDaTongHop=1
	--	and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_BH_DTC = ct.iID_BH_DTC
	--where mlns.iNamLamViec=@NamLamViec AND mlns.iTrangThai=1

	----- Get data
	--select * INTO #result from
	--(
	--	--- che do bao hiem
	--	select SUBSTRING(A.sXauNoiMa,1,20) sXauNoiMa ,A.IIdMaDonVi, 
	--	SUM(A.FTienTangGiam) FTienTangGiam
	--	from #temp A
	--	where A.sLNS in (select * from splitstring('9010001,9010002'))
	--	Group by SUBSTRING(A.sXauNoiMa,1,20),A.IIdMaDonVi
	--	--- Cssk hssv va nld
	--	UNION ALL
	--	select SUBSTRING(A.sXauNoiMa,1,3) sXauNoiMa ,A.IIdMaDonVi, 
	--	SUM(A.FTienTangGiam) FTienTangGiam
	--	from #temp A
	--	where A.sLNS in (select * from splitstring('905,9050001,9050002'))
	--	Group by SUBSTRING(A.sXauNoiMa,1,3),A.IIdMaDonVi
	--	--- KPQL, KCB quan y, KCB truong sa,  KCB BHYT , TTB Y Te, BHTN
	--	UNION ALL
	--	select SUBSTRING(A.sXauNoiMa,1,7) sXauNoiMa ,A.IIdMaDonVi, 
	--	SUM(A.FTienTangGiam) FTienTangGiam
	--	from #temp A
	--	where A.sLNS in (select * from splitstring('9010004,9010003,9010006,9010008,9010009,9010010'))
	--	Group by SUBSTRING(A.sXauNoiMa,1,7),A.IIdMaDonVi

	--	) as test

	--select * from #result

	--drop table #temp
	--drop table #result

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]    Script Date: 12/6/2024 10:11:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitheodieuchinh_clone]
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50),
@DNgayChungTu datetime

AS
BEGIN
		-- Get dieu chinh
		SELECT 
			ct.sXauNoiMa,
			Sum(isnull(ct.fTienThucHien06ThangDauNam,0)) fTienThucHien06ThangDauNam,
			Sum(isnull(ct.fTienUocThucHien06ThangCuoiNam,0)) fTienUocThucHien06ThangCuoiNam
			into #tempDieuChinh
		FROM
			(
				SELECT
					--bh.iID_MaDonVi,
					bh.sMoTa,
					ddv.sTenDonVi,
					bhct.*
				FROM 
					BH_DTC_DieuChinhDuToanChi bh
				JOIN 
					BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
				LEFT JOIN 
					(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
				WHERE
					bh.iID_MaDonVi in (SELECT * FROM f_split(@IIDDonVi))
					AND	bh.iNamLamViec=@NamLamViec
					--and bh.bIsKhoa=1
					and bh.iLoaiTongHop=2
					--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
			) ct
			Group by ct.sXauNoiMa

		-- get phan bo đầu nam 
		SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)  fTienTuChi
					, ctct.iID_MaDonVi
					into  #tempNhanpbt
			FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
			JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IIDDonVi))
				AND BIsKhoa = 1
				AND ct.iNamLamViec = 2024
				GROUP BY ctct.sXauNoiMa, ctct.iID_MaDonVi
		-- get data
		SELECT 
		A.sLNS,
		A.sTM,
		A.sTTM,
		A.sM,
		A.sNG,
		A.sMoTa AS sNoiDung,
		A.sXauNoiMa,
		A.iID_MLNS as IID_MucLucNganSach,
		A.iID_MLNS_Cha as IdParent,
		A.bHangChaDuToan bHangCha,
		A.bHangChaDuToan as isHangCha,
		A.iID_MLNS AS iID_MucLucNganSach,
		C.FTienTuChi fTienDuToanDuocGiao,
		B.fTienThucHien06ThangDauNam,
		B.fTienUocThucHien06ThangCuoiNam,
		A.sCPChiTietToi,
		A.sDuToanChiTietToi,
		A.bHangCha as IsHangCha,
		A.bHangCha ,
		A.bHangChaDuToan,
		@NamLamViec iNamlamViec,
		@IIDDonVi as iID_MaDonVi
		FROM BH_DM_MucLucNganSach AS A
		LEFT JOIN #tempDieuChinh AS B
		ON A.sXauNoiMa = B.sXauNoiMa
		LEFT JOIN #tempNhanpbt AS C
		ON A.sXauNoiMa=C.sXauNoiMa
		WHERE   A.sLNS IN (SELECT * FROM f_split( @sLns))
			AND A.iNamlamViec=@NamLamViec
			AND A.iTrangThai=1
			--AND A.bHangChaDuToan IS NOT NULL
	order by A.sXauNoiMa


END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 12/6/2024 10:11:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop] @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50), 
  @IdChungTu nvarchar(100), 
  @NamLamViec int ,
  @MaDonVi nvarchar(50)
  AS 
  BEGIN
  INSERT INTO [dbo].BH_DTC_DieuChinhDuToanChi_ChiTiet (
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
  mlns.iID_MLNS, 
  mlns.sM, 
  mlns.sTM, 
  mlns.sMoTa,
  mlns.sXauNoiMa,
  @NamLamViec,
  @MaDonVi,
 sum(ctct.fTienDuToanDuocGiao), 
  sum(ctct.fTienThucHien06ThangDauNam), 
  sum(ctct.fTienUocThucHien06ThangCuoiNam), 
  sum(ctct.fTienUocThucHienCaNam), 
  sum(ctct.fTienSoSanhTang), 
  sum(ctct.fTienSoSanhGiam), 
  Null, 
  GETDATE(), 
  Null, 
  'admin' 
FROM 
  BH_DTC_DieuChinhDuToanChi_ChiTiet  ctct
  	INNER JOIN BH_DM_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa AND mlns.iNamLamViec = ctct.iNamLamViec
	INNER JOIN BH_DTC_DieuChinhDuToanChi ct ON ctct.iID_BH_DTC = ct.iID_BH_DTC
WHERE 
  ct.iID_BH_DTC in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  and ctct.iNamLamViec=@NamLamViec
GROUP BY 
  mlns.sM, 
  mlns.sTM, 
  mlns.iID_MLNS, 
  mlns.sMoTa,
  mlns.sXauNoiMa

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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi_clone]    Script Date: 12/6/2024 10:11:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_tonghop_chitiet_donvi_clone]
	@NamLamViec int,
	@IdDonVi nvarchar(max),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int,
	@Loai int,
	@MaLoaiChi nvarchar(5)
AS
BEGIN
		SELECT ML.sLNS,
		ML.sL,
		ML.sk,
		ML.sM,
		dctcChitiet.fTienThucHien06ThangDauNam / @Dvt as fTienThucHien06ThangDauNam,
		dctcChitiet.fTienUocThucHien06ThangCuoiNam / @Dvt as fTienUocThucHien06ThangCuoiNam,
		dctcChitiet.iID_MaDonVi,
		dctcChitiet.sTenDonVi,
		(ML.sLNS +'-' +
		ML.sL + '-' +
		ML.sk + '-' +
		ML.sM) sXauNoiMa
		into #tempDtc_ChiTiet
		FROM BH_DM_MucLucNganSach ML
		left join (select sXauNoiMa,
							SUM(fTienThucHien06ThangDauNam) as fTienThucHien06ThangDauNam,
							SUM (fTienUocThucHien06ThangCuoiNam) fTienUocThucHien06ThangCuoiNam,
							dv.iID_MaDonVi,
							dv.sTenDonVi
							from BH_DTC_DieuChinhDuToanChi_ChiTiet ctct
							left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
						where ctct.iID_BH_DTC in 
								(Select iID_BH_DTC from BH_DTC_DieuChinhDuToanChi
								where 	iID_MaDonVi IN 
								(SELECT * FROM f_split(@IdDonVi))
								AND iNamLamViec=@NamLamViec
								AND sMaLoaiChi=@MaLoaiChi)
							AND dv.iTrangThai=1
							AND dv.iNamLamViec=@NamLamViec
					group by sXauNoiMa,dv.iID_MaDonVi,dv.sTenDonVi )dctcChitiet on ML.sXauNoiMa=dctcChitiet.sXauNoiMa
		WHERE ML.sLNS IN  (SELECT * FROM f_split(@SLNS))
				AND ML.iNamLamViec=@NamLamViec
				AND ML.bHangChaDuToanDieuChinh is not null
				AND ML.iTrangThai=1
		ORDER BY ML.sXauNoiMa

		--- Phan bo 
		
			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)/@Dvt  fTienTuChi
					into #tempPhanBo
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
			
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = 2024
				GROUP BY ctct.sXauNoiMa

			SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)/@Dvt  fTienTuChi
					, ctct.iID_MaDonVi
					into #tempPhanBoTheoDonVi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
			
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamChungTu = 2024
				GROUP BY ctct.sXauNoiMa, ctct.iID_MaDonVi

	-- Nhan phan bo
	SELECT ctct.sXauNoiMa
					, SUM(ctct.fTienTuChi)/ @Dvt  fTienTuChi
					, ctct.iID_MaDonVi
					into  #tempNhanPhanBoTheoDonVi
			FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
			JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
				WHERE ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
				AND BIsKhoa = 1
				AND ct.iNamLamViec = @NamLamViec
				GROUP BY ctct.sXauNoiMa, ctct.iID_MaDonVi

	-- Hang don vi
		select 
		SUM(fTienThucHien06ThangDauNam) as fTienThucHien06ThangDauNam,
		SUM (fTienUocThucHien06ThangCuoiNam) as fTienUocThucHien06ThangCuoiNam,
		sXauNoiMa
		into #tempSum
		from #tempDtc_ChiTiet
		where fTienThucHien06ThangDauNam>0 or fTienUocThucHien06ThangCuoiNam>0
		group by sXauNoiMa

		
		select 
		CAST(null as uniqueidentifier) as iID_MucLucNganSach,
		CAST(null as uniqueidentifier) as IdParent,
		CAST(0 as bit)  bHangCha,
		CAST(0 as bit)  bHangChaDuToan,
		CAST(0 as bit) bHangChaDuToanDieuChinh,
		concat('   + ', sTenDonVi) SNoiDung,
		phanbo.fTienTuChi as fTienDuToanDuocGiao,
		SUM(ml.fTienThucHien06ThangDauNam) as fTienThucHien06ThangDauNam,
		SUM (ml.fTienUocThucHien06ThangCuoiNam) as fTienUocThucHien06ThangCuoiNam,
		ml.sXauNoiMa,
		ml.iID_MaDonVi,
		sTenDonVi
		into #tempSumTheoDonViPhanBo
		from #tempDtc_ChiTiet ml
		left join #tempPhanBoTheoDonVi phanbo on ml.sXauNoiMa=phanbo.sXauNoiMa and ml.iID_MaDonVi=phanbo.iID_MaDonVi
		where fTienThucHien06ThangDauNam >0 or fTienUocThucHien06ThangCuoiNam >0
		group by ml.sXauNoiMa,ml.iID_MaDonVi,ml.sTenDonVi,phanbo.fTienTuChi
	
		select 
		CAST(null as uniqueidentifier) as iID_MucLucNganSach,
		CAST(null as uniqueidentifier) as IdParent,
		CAST(0 as bit)  bHangCha,
		CAST(0 as bit)  bHangChaDuToan,
		CAST(0 as bit) bHangChaDuToanDieuChinh,
		concat('   + ', sTenDonVi) SNoiDung,
		nhanPhanBo.fTienTuChi as fTienDuToanDuocGiao,
		SUM(ml.fTienThucHien06ThangDauNam) as fTienThucHien06ThangDauNam,
		SUM (ml.fTienUocThucHien06ThangCuoiNam) as fTienUocThucHien06ThangCuoiNam,
		ml.sXauNoiMa,
		ml.iID_MaDonVi,
		sTenDonVi
		into #tempSumTheoDonViNhanPhanBo
		from #tempDtc_ChiTiet ml
		left join #tempNhanPhanBoTheoDonVi nhanPhanBo on ml.sXauNoiMa=nhanPhanBo.sXauNoiMa and ml.iID_MaDonVi=nhanPhanBo.iID_MaDonVi
		where fTienThucHien06ThangDauNam>0 or fTienUocThucHien06ThangCuoiNam>0
		group by ml.sXauNoiMa,ml.iID_MaDonVi,ml.sTenDonVi,nhanPhanBo.fTienTuChi


		SELECT 
			ML.iID_MLNS iID_MucLucNganSach,
			ML.iID_MLNS_Cha IdParent,
			ML.bHangCha,
			ML.bHangChaDuToan,
			ML.bHangChaDuToanDieuChinh,
			ML.sMoTa as SNoiDung,
			phanbochitiet.fTienTuChi as fTienDuToanDuocGiao,
			dcdtChiTiet.fTienThucHien06ThangDauNam,
			dcdtChiTiet.fTienUocThucHien06ThangCuoiNam,
			ML.sXauNoiMa,
			'' as iID_MaDonVi,
			'' as sTenDonVi
			into #tblTable
			FROM BH_DM_MucLucNganSach ML
			left join #tempSum dcdtChiTiet on ML.sXauNoiMa=dcdtChiTiet.sXauNoiMa
			left join #tempPhanBo phanbochitiet on ML.sXauNoiMa=phanbochitiet.sXauNoiMa
			WHERE ML.sLNS IN  (SELECT * FROM f_split(@SLNS))
					AND ML.iNamLamViec=@NamLamViec
					AND ML.bHangChaDuToan is not null
					AND ML.iTrangThai=1
			ORDER BY ML.sXauNoiMa


	--In Tong hop cac don vi
		SELECT 
		ML.iID_MLNS iID_MucLucNganSach,
		ML.iID_MLNS_Cha IdParent,
		ML.bHangCha,
		ML.bHangChaDuToan,
		ML.bHangChaDuToanDieuChinh,
		ML.sMoTa as SNoiDung,
		nhanPhanBo_ChiTiet.fTienTuChi as fTienDuToanDuocGiao,
		dcdtChiTiet.fTienThucHien06ThangDauNam,
		dcdtChiTiet.fTienUocThucHien06ThangCuoiNam,
		ML.sXauNoiMa,
		'' as iID_MaDonVi,
		'' as sTenDonVi
		into #tblTable2
		FROM BH_DM_MucLucNganSach ML
		left join #tempSum dcdtChiTiet on ML.sXauNoiMa=dcdtChiTiet.sXauNoiMa
		left join #tempNhanPhanBoTheoDonVi nhanPhanBo_ChiTiet on ML.sXauNoiMa=nhanPhanBo_ChiTiet.sXauNoiMa
		WHERE ML.sLNS IN  (SELECT * FROM f_split(@SLNS))
				AND ML.iNamLamViec=@NamLamViec
				AND ML.bHangChaDuToan is not null
				AND ML.iTrangThai=1
		ORDER BY ML.sXauNoiMa

		IF @Loai=0
		Select * from (
		Select * from #tblTable
		Union all 
		select * from #tempSumTheoDonViPhanBo )
		result 
		order by result.sXauNoiMa,result.iID_MaDonVi
		Else
		Select * from (
		Select * from #tblTable2
		Union all 
		select * from #tempSumTheoDonViNhanPhanBo )
		result 
		order by result.sXauNoiMa,result.iID_MaDonVi

		Drop table #tempDtc_ChiTiet
		Drop table #tempPhanBo
		Drop table #tempPhanBoTheoDonVi
		Drop table #tempNhanPhanBoTheoDonVi
		Drop table #tempSum
		Drop table #tblTable
		Drop table #tblTable2
		Drop table #tempSumTheoDonViNhanPhanBo
		Drop table #tempSumTheoDonViPhanBo
END
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 12/6/2024 1:46:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 12/6/2024 1:46:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 12/6/2024 1:46:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@hSSV nvarchar(50),
	@luuHS nvarchar(50),
	@hVSQ nvarchar(50),
	@sQDuBi nvarchar(50),
	@soQuyetDinh nvarchar(500),
	@ngayQuyetDinh nvarchar(500),
	@dvt int,
	@IsMillionRound bit
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
		CASE WHEN @IsMillionRound = 1 THEN (ROUND(SUM(IsNull(A.ThanhTien, 0))/1000000, 0) * 1000000) ELSE SUM(ROUND((IsNull(A.ThanhTien, 0)),0)) END as ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hSSV
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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
	   CASE WHEN @IsMillionRound = 1 THEN (ROUND(SUM(IsNull(A.ThanhTien, 0))/1000000, 0) * 1000000) ELSE SUM(ROUND((IsNull(A.ThanhTien, 0)),0)) END as ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @luuHS
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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
	   CASE WHEN @IsMillionRound = 1 THEN (ROUND(SUM(IsNull(A.ThanhTien, 0))/1000000, 0) * 1000000) ELSE SUM(ROUND((IsNull(A.ThanhTien, 0)),0)) END as ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hVSQ
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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
	   CASE WHEN @IsMillionRound = 1 THEN (ROUND(SUM(IsNull(A.ThanhTien, 0))/1000000, 0) * 1000000) ELSE SUM(ROUND((IsNull(A.ThanhTien, 0)),0)) END as ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   LEFT JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @sQDuBi
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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
		result.STenDonVi, 
		result.HSSV/@dvt HSSV, 
		result.LuuHS/@dvt LuuHS,
		result.TongHSSV/@dvt TongHSSV,
		result.HVQS/@dvt HVQS,
		result.SQDuBi/@dvt SQDuBi,
		(IsNull(result.TongHSSV, 0) + IsNull(result.HVQS, 0) + IsNull(result.SQDuBi, 0))/@dvt TongCongHSSV
		FROM
		(SELECT donvi.iID_MaDonVi idDonVi, 
		donvi.STenDonVi,
		IsNull(hssv.ThanhTien_HSSV, 0) HSSV,
		IsNull(luuhs.ThanhTien_LuuHS, 0) LuuHS,
		IsNull(hssv.ThanhTien_HSSV, 0) + IsNull(luuhs.ThanhTien_LuuHS, 0) TongHSSV,
		IsNull(hvsq.ThanhTien_HVQS, 0) HVQS,
		IsNull(sqdb.ThanhTien_SQDuBi, 0) SQDuBi
		FROM DonVi donvi
		LEFT JOIN @tbl_HSSV hssv ON donvi.iID_MaDonVi = hssv.idDonVi
		LEFT JOIN @tbl_LuuHS luuhs ON donvi.iID_MaDonVi = luuhs.idDonVi
		LEFT JOIN @tbl_HVQS hvsq ON donvi.iID_MaDonVi = hvsq.idDonVi
		LEFT JOIN @tbl_SQDuBi sqdb ON donvi.iID_MaDonVi = sqdb.idDonVi
		WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) result
		order by result.idDonVi
		
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 12/6/2024 1:46:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@thanNhanQuanNhan nvarchar(50),
	@thanNhanCNVQP nvarchar(50),
	@smDuToan nvarchar(50),
	@smHachToan nvarchar(50),
	@soQuyetDinh nvarchar(500),
	@ngayQuyetDinh nvarchar(500),
	@dvt int,
	@IsMillionRound bit
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
	   CASE WHEN @IsMillionRound = 1 THEN (ROUND(SUM(IsNull(A.ThanhTien, 0))/1000000, 0) * 1000000) ELSE SUM(ROUND((IsNull(A.ThanhTien, 0)),0)) END as ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanQuanNhan
		   AND ml.sM = '0001'
		   WHERE ct.iNamLamViec = @namLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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
		CASE WHEN @IsMillionRound = 1 THEN (ROUND(SUM(IsNull(A.ThanhTien, 0))/1000000, 0) * 1000000) ELSE SUM(ROUND((IsNull(A.ThanhTien, 0)),0)) END as ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanCNVQP
		   AND ml.sM = '0000'
		   WHERE ct.iNamLamViec = @namLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
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
			CASE WHEN @IsMillionRound = 1 THEN (ROUND(SUM(IsNull(A.ThanhTien, 0))/1000000, 0) * 1000000) ELSE SUM(ROUND((IsNull(A.ThanhTien, 0)),0)) END as ThanhTien
		FROM
			 (SELECT
					  ml.sMoTa,
					  ctct.iID_MaDonVi,
					  IsNull(ctct.fDuToan, 0) ThanhTien,
					  ml.sLNS
			  FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
			  JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
			  RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			  AND ml.iNamLamViec = @namLamViec
			  AND ml.iTrangThai = 1
			  AND ml.sLNS = @thanNhanQuanNhan
			  AND ml.sM = '0002'
			  WHERE ct.iNamLamViec = @namLamViec
				AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
				AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
			  ) AS A 
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
		CASE WHEN @IsMillionRound = 1 THEN (ROUND(SUM(IsNull(A.ThanhTien, 0))/1000000, 0) * 1000000) ELSE SUM(ROUND((IsNull(A.ThanhTien, 0)),0)) END as ThanhTien
		FROM
		(SELECT
					ml.sMoTa,
					ctct.iID_MaDonVi,
					IsNull(ctct.fDuToan, 0) ThanhTien,
					ml.sLNS
			FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
			JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
			RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			AND ml.iNamLamViec = @namLamViec
			AND ml.iTrangThai = 1
			AND ml.sLNS = @thanNhanCNVQP
			AND ml.sM = '0001'
			WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
			) AS A 
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
		result.STenDonVi, 
		result.TN_QN_DT/@dvt TNQNDuToan, 
		result.TN_CNVQP_DT/@dvt TNCNVQPDuToan,
		result.TongDuToan/@dvt TongDuToan,
		result.TN_QN_HT/@dvt TNQNHachToan,
		result.TN_CNVQP_HT/@dvt TNCNVQPHachToan,
		result.TongHachToan/@dvt TongHachToan,
		(result.TongDuToan + result.TongHachToan)/@dvt TongCongThanNhan
		FROM
		(SELECT donvi.iID_MaDonVi idDonVi, 
		donvi.sTenDonVi,
		IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) TN_QN_DT,
		IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0) TN_CNVQP_DT,
		(IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) + IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0)) TongDuToan,
		IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) TN_QN_HT,
		IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0) TN_CNVQP_HT,
		(IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) + IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0)) TongHachToan
		FROM
		DonVi donvi
		LEFT JOIN @TNQN_DuToan tnqn_dt ON donvi.iID_MaDonVi = tnqn_dt.idDonVi
		LEFT JOIN @TN_CNVQP_DuToan tncn_dt ON donvi.iID_MaDonVi = tncn_dt.idDonVi
		LEFT JOIN @TNQN_HachToan tnqn_ht ON donvi.iID_MaDonVi = tnqn_ht.idDonVi
		LEFT JOIN @TN_CNVQP_HachToan tncn_ht ON donvi.iID_MaDonVi = tncn_ht.idDonVi
		WHERE donvi.iTrangThai = 1
		   AND donvi.iNamLamViec = @NamLamViec
		   AND donvi.iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) result
		order by result.idDonVi
END
;
;
;
;
;
;
GO
