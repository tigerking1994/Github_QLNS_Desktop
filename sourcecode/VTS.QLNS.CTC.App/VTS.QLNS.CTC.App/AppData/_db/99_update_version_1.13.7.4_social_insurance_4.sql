/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]    Script Date: 12/28/2023 10:04:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]    Script Date: 12/28/2023 10:04:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_donvi]
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
where ct.iNamChungTu=2023
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
group by  dv.iID_MaDonVi, dv.sTenDonVi ,ctct.sXauNoiMa

select
	null as STT
	, N'Khối dự toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0001%' THEN Sum(ctct.fTongTien)/ @Donvitinh  ELSE 0 END ) fTienTroCapOmDau
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0002%' THEN Sum(ctct.fTongTien)/ @Donvitinh  ELSE 0 END )fTienTroCapThaiSan
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0003%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapTaiNan
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0004%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapHuuTri
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0005%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapPhucVien
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0006%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapXuatNgu
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0007%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapThoiViec
	,  (CASE WHEN ctct.sXauNoiMa like '9010001-010-011-0008%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapTuTuat
	, 0 IsHangCha
	, 1 RowNumber
	into #temp2
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
group by ctct.iID_DonVi, ctct.iID_MaDonVi ,ctct.sXauNoiMa

select
	null as STT
	, N'Khối hạch toán' as sTenDonVi
	, ctct.iID_MaDonVi
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0001%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapOmDau
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0002%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapThaiSan
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0003%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapTaiNan
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0004%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapHuuTri
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0005%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapPhucVien
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0006%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapXuatNgu
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0007%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapThoiViec
	,  (CASE WHEN ctct.sXauNoiMa like '9010002-010-011-0008%' THEN Sum(ctct.fTongTien)/ @Donvitinh   ELSE 0 END )fTienTroCapTuTuat
	, 0 IsHangCha
	, 2 RowNumber
	into #temp3
from #tempall ctct
where ctct.iID_MaDonVi in  ( SELECT * FROM splitstring(@IdMaDonVi)) and ctct.iNamLamViec=@INamLamViec
group by ctct.iID_DonVi, ctct.iID_MaDonVi ,ctct.sXauNoiMa

SELECT  NEWID() as Id,* INTO #tempRESULT
from
(
	SELECT * FROM #temp1
	UNION ALL 
	SELECT * FROM #temp2
	UNION ALL 
	SELECT * FROM #temp3
) tempRESULT;

With #tree(id,sTenDonVi,iID_MaDonVi, STT, IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat , RowNumber,position)
as
(
	SELECT  id,sTenDonVi,iID_MaDonVi, STT,IsHangCha,fTienTroCapOmDau,fTienTroCapThaiSan,fTienTroCapTaiNan,fTienTroCapHuuTri,fTienTroCapPhucVien,fTienTroCapXuatNgu, fTienTroCapThoiViec, fTienTroCapTuTuat,RowNumber,
						CAST(ROW_NUMBER() OVER(ORDER BY STT) AS NVARCHAR(MAX)) AS position
	FROM #tempRESULT
	WHERE  IsHangCha = 1 
	UNION ALL 
	SELECT chil.Id, chil.sTenDonVi, chil.iID_MaDonVi,   chil.STT, chil.IsHangCha,chil.fTienTroCapOmDau,chil.fTienTroCapThaiSan,chil.fTienTroCapTaiNan,chil.fTienTroCapHuuTri,chil.fTienTroCapPhucVien,chil.fTienTroCapXuatNgu, chil.fTienTroCapThoiViec, chil.fTienTroCapTuTuat,chil.RowNumber,
	CONCAT(pr.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY chil.iID_MaDonVi) AS NVARCHAR(MAX))) AS position
	FROM #tempRESULT chil
	INNER JOIN #tree pr ON pr.iID_MaDonVi = chil.iID_MaDonVi AND  pr.IsHangCha = 1
	WHERE chil.STT IS NULL AND chil.IsHangCha = 0 

	
)
select *, 
		cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort

from #tree order by sort;

DROP TABLE #tempall
DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #tempRESULT
END
;
GO
