/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]    Script Date: 12/25/2023 8:56:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]    Script Date: 12/25/2023 8:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_pbdtc_intheolyke_KPQL_KCB_Khac_donvi]
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
where ct.iNamChungTu=@INamLamViec
and ct.iID_LoaiDanhMucChi=@IDLoaiChi
and ct.sMaLoaiChi=@MaLoaiChi
and cast(ct.dNgayQuyetDinh as date) <= convert(varchar,@SNgayQuyetDinh,101)
and ct.sSoQuyetDinh=@SoQuyetdinh
and ctct.iID_MaDonVi IN (SELECT * FROM splitstring(  @IdMaDonVi));

select 
	 CAST(ROW_NUMBER() OVER (ORDER BY dv.sTenDonVi) AS VARCHAR(6)) STT
	, dv.sTenDonVi
	, dv.iID_MaDonVi
	, SUM(ctct.fTongTien) as fTongTienDuToan
	, 0 IsHangCha
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

select * from #temp1;

DROP TABLE #tempall
DROP TABLE #temp1
END
;
;


GO
