/****** Object:  StoredProcedure [dbo].[sp_vdt_bctonghopthongtinduan]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_bctonghopthongtinduan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_bctonghopthongtinduan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_bcketquagiaingankinhphidautu]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_bcketquagiaingankinhphidautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_bcketquagiaingankinhphidautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_baocaotinhhinhduan]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_baocaotinhhinhduan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_baocaotinhhinhduan]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_nhan_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_mucluc_index_chungtu_bvtc]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_mucluc_index_chungtu_bvtc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_mucluc_index_chungtu_bvtc]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_report]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thuchien_ngansach_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thuchien_ngansach_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_index]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thuchien_ngansach_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thuchien_ngansach_index]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]    Script Date: 03/10/2022 4:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]    Script Date: 03/10/2022 4:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]
	-- Add the parameters for the stored procedure here
	 @iIDQuyetToan uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select  qtct.*,
			tb.sLNS as SLNS,
			tb.sL as SL,
			tb.sK as SK,
			tb.sM as SM,
			tb.sTM as STM,
			tb.sTTM as STTM,
			hd.sTenHopDong,
			dmnvc.sTenNhiemVuChi,
			da.sTenDuAn,
			ttct.sTenNoiDungChi,
			tt.iLoaiNoiDungChi,
			qtnd.iID_DonViID as IID_DonVi,
			dv.iID_MaDonVI + ' - '+ dv.sTenDonVi as sTenDonVi 
			,daqd.fGiaTriUSD  as FHopDongDuAnUsd
			,daqd.fGiaTriVND  as FHopDongDuAnVnd
			,hd.fGiaTriUSD as FHopDongUsd
			,hd.fGiaTriVND  as FHopDongVnd
			, qtct.ID as Id
			, qtct.iID_ParentID as IIdParentId
			, qtct.iID_HopDongID as IIdHopDongId
			, qtct.fKeHoach_TTCP_USD as FKeHoachTtcpUsd
			, qtct.fKeHoach_TTCP_VND as FKeHoachTtcpVnd
			, qtct.fKeHoach_BQP_USD as FKeHoachBqpUsd
			, qtct.fKeHoach_BQP_VND as FKeHoachBqpVnd
			, qtct.fQTKinhPhiDuyetCacNamTruoc_USD as FQtKinhPhiDuyetCacNamTruocUsd
			, qtct.fQTKinhPhiDuyetCacNamTruoc_VND as FQtKinhPhiDuyetCacNamTruocVnd
			, qtct.fQTKinhPhiDuocCap_TongSo_USD as FQtKinhPhiDuocCapTongSoUsd
			, qtct.fQTKinhPhiDuocCap_TongSo_VND as FQtKinhPhiDuocCapTongSoVnd
			, qtct.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD as FQtKinhPhiDuocCapNamTruocChuyenSangUsd
			, qtct.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND as FQtKinhPhiDuocCapNamTruocChuyenSangVnd
			, qtct.fQTKinhPhiDuocCap_NamNay_USD as FQtKinhPhiDuocCapNamNayUsd
			, qtct.fQTKinhPhiDuocCap_NamNay_VND as FQtKinhPhiDuocCapNamNayVnd
			, qtct.fDeNghiQTNamNay_USD as FDeNghiQtNamNayUsd
			, qtct.fDeNghiQTNamNay_VND as FDeNghiQtNamNayVnd
			, qtct.fDeNghiChuyenNamSau_USD as FDeNghiChuyenNamSauUsd
			, qtct.fDeNghiChuyenNamSau_VND as FDeNghiChuyenNamSauVnd
			, qtct.fThuaThieuKinhPhiTrongNam_USD as FThuaThieuKinhPhiTrongNamUsd
			, qtct.fThuaThieuKinhPhiTrongNam_VND as FThuaThieuKinhPhiTrongNamVnd
			, qtct.fThuaNopNSNN_USD as FThuaNopNsnnUsd
			, qtct.fThuaNopNSNN_VND as FThuaNopNsnnVnd
			, qtct.fLuyKeKinhPhiDuocCap_USD as FLuyKeKinhPhiDuocCapUsd
			, qtct.fLuyKeKinhPhiDuocCap_VND as FLuyKeKinhPhiDuocCapVnd
			, qtct.fKeHoachChuaGiaiNgan_USD as FKeHoachChuaGiaiNganUsd
			, qtct.fKeHoachChuaGiaiNgan_VND as FKeHoachChuaGiaiNganVnd
			, qtct.iID_KHTT_NhiemVuChiID as IIdKhttNhiemVuChiId
			, 1 as IsData
	from NH_QT_QuyetToanNienDo_ChiTiet qtct 
	left join NH_QT_QuyetToanNienDo qtnd on qtct.iID_QuyetToanNienDoID = qtnd.ID 
	left join  NH_KHTongThe_NhiemVuChi khttnvc on qtct.iID_KHTT_NhiemVuChiID = khttnvc.ID
	left join NH_DM_NhiemVuChi dmnvc on khttnvc.iID_NhiemVuChiID = dmnvc.ID
	left join NH_DA_DuAn da on  qtct.iID_DuAnId = da.ID 
	left join NH_DA_QDDauTu daqd on da.ID = daqd.iID_DuAnID
	left join DonVi dv on  qtnd.iID_DonViID = dv.iID_DonVi
	left join NH_DA_HopDong hd on qtct.iID_HopDongID = hd.ID
	left join NH_TT_ThanhToan_ChiTiet ttct on qtct.iID_ThanhToan_ChiTietID = ttct.ID
	left join NS_MucLucNganSach tb  on tb.iID = ttct.iID_MucLucNganSachID
	left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID
	where 
        qtct.iID_QuyetToanNienDoID = @iIDQuyetToan
END


GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]    Script Date: 03/10/2022 4:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]
	-- Add the parameters for the stored procedure here

  @iIDDonVi uniqueidentifier,
  @iNamKeHoach float,
  @devideDonVi float = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/* bang tam*/

    select   CAST(
             CASE 
                  WHEN qtct.iNamKeHoach>=gd.iGiaiDoanTu and qtct.iNamKeHoach<=gd.iGiaiDoanDen
                     THEN case when qtct.iNamKeHoach = gd.iGiaiDoanTu then null
									else qtct.fQTKinhPhiDuyetCacNamTruoc_USD end 
                  ELSE null
             END AS float) as fQTKinhPhiDuyetCacNamTruoc_USD,
			 CAST(
             CASE 
                  WHEN qtct.iNamKeHoach>=gd.iGiaiDoanTu and qtct.iNamKeHoach<=gd.iGiaiDoanDen
                     THEN case when qtct.iNamKeHoach = gd.iGiaiDoanTu then null
									else qtct.fQTKinhPhiDuyetCacNamTruoc_VND end 
                  ELSE null
             END AS float) as fQTKinhPhiDuyetCacNamTruoc_VND
			 ,gd.ID into #kpdnt from  
			 (select distinct b.iGiaiDoanTu,b.iGiaiDoanDen,a.ID from NH_KHTongThe_NhiemVuChi a join NH_KHTongThe b on b.ID = a.iID_KHTongTheID) as gd 
			 join (select a.iNamKeHoach
						 , (b.fQTKinhPhiDuyetCacNamTruoc_USD+b.fDeNghiQTNamNay_USD) as fQTKinhPhiDuyetCacNamTruoc_USD
						 ,(b.fQTKinhPhiDuyetCacNamTruoc_VND+b.fDeNghiQTNamNay_VND) as fQTKinhPhiDuyetCacNamTruoc_VND,b.iID_KHTT_NhiemVuChiID 
				   from NH_QT_QuyetToanNienDo a 
				   join NH_QT_QuyetToanNienDo_ChiTiet b on a.ID = b.iID_QuyetToanNienDoID 
				   where a.iNamKeHoach = @iNamKeHoach-1) as qtct on gd.ID = qtct.iID_KHTT_NhiemVuChiID
    --get fQTKinhPhiDuocCap_NamTruocChuyenSang_USD

	select b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID, CAST(
				 CASE 
					  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
						 THEN sum(b.fTraDonViThuHuongPheDuyet_BangSo_USD)
					  ELSE null
				 END AS float) as fQTKinhPhiDuocCap_NamTruocChuyenSang_USD
				 into #ntcsUSD
	from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
	where b.iLoaiNoiDungChi = 1 and b.iNamNganSach = 2 and b.iNamKeHoach = @iNamKeHoach  
	group by b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID

	--get fQTKinhPhiDuocCap_NamTruocChuyenSang_VND

	select b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID, CAST(
				 CASE 
					  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
						 THEN sum(b.fTraDonViThuHuongPheDuyet_BangSo_VND)
					  ELSE null
				 END AS float) as fQTKinhPhiDuocCap_NamTruocChuyenSang_VND 
				 into #ntcsVND
	from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
	where b.iLoaiNoiDungChi = 2 and b.iNamNganSach = 2 and b.iNamKeHoach = @iNamKeHoach  
	group by b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID

	--get fQTKinhPhiDuocCap_NamNay_USD

	select b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID, CAST(
				 CASE 
					  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
						 THEN sum(b.fTraDonViThuHuongPheDuyet_BangSo_USD)
					  ELSE null
				 END AS float) as fQTKinhPhiDuocCap_NamNay_USD 
				 into #nnUSD
	from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
	where b.iLoaiNoiDungChi = 1 and b.iNamNganSach = 1 and b.iNamKeHoach = @iNamKeHoach  
	group by b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID

	--get fQTKinhPhiDuocCap_NamNay_VND


	select b.iLoaiDeNghi,b.iCoQuanThanhToan,a.ID,CAST(
				 CASE 
					  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
						 THEN sum(b.fTraDonViThuHuongPheDuyet_BangSo_USD)
					  ELSE null
				 END AS float) as fQTKinhPhiDuocCap_NamNay_VND 
				  into #nnVND
				 from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
				 where b.iLoaiNoiDungChi = 2 and b.iNamNganSach = 1 and b.iNamKeHoach = @iNamKeHoach    
	group by b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID
	--get fKuyKeKinhPhiDuocCap_USD
	 select a.fLuyKeKinhPhiDuocCap_VND,a.fLuyKeKinhPhiDuocCap_USD,a.iID_DuAnID,a.iID_HopDongID,a.iID_KHTT_NhiemVuChiID,b.iNamKeHoach into #lknt from NH_QT_QuyetToanNienDo_ChiTiet a join NH_QT_QuyetToanNienDo b on a.iID_QuyetToanNienDoID = b.ID 
	  where b.iNamKeHoach = @iNamKeHoach - 1
	/* finnal*/
	select distinct  tb.sLNS as SLNS, tb.sL as SL,tb.sK as SK,tb.sM as SM,tb.sTM as STM,tb.sTTM as STTM,dmnvc.sTenNhiemVuChi,da.sTenDuAn,dv.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi ,a.sTenNoiDungChi,b.iID_DuAnID ,khttnvc.Id as IIdKhttNhiemVuChiId,b.iID_HopDongID as IIdHopDongId, hd.sTenHopDong as STenHopDong,a.ID as IID_ThanhToan_ChiTietID,
             
		   IIF(@devideDonVi is not null, round(daqd.fGiaTriUSD/@devideDonVi,2), daqd.fGiaTriUSD) as FHopDongDuAnUsd,
		   IIF(@devideDonVi is not null, round(daqd.fGiaTriVND/@devideDonVi,2), daqd.fGiaTriVND) as FHopDongDuAnVnd,
		   IIF(@devideDonVi is not null, round(hd.fGiaTriUSD/@devideDonVi,2), hd.fGiaTriUSD) as FHopDongUsd,
		   IIF(@devideDonVi is not null, round(hd.fGiaTriVND/@devideDonVi,2), hd.fGiaTriVND) as FHopDongVnd,
		   IIF(@devideDonVi is not null, round(khttnvc.fGiaTriKH_TTCP/@devideDonVi,2), khttnvc.fGiaTriKH_TTCP) as FKeHoachTtcpUsd,
		   IIF(@devideDonVi is not null, round(khttnvc.fGiaTriKH_BQP/@devideDonVi,2), khttnvc.fGiaTriKH_BQP) as FKeHoachBqpUsd,
		   IIF(@devideDonVi is not null, round(khttnvc.fGiaTriKH_BQP_VND/@devideDonVi,2), khttnvc.fGiaTriKH_BQP_VND) as FKeHoachBqpVnd,
		   IIF(@devideDonVi is not null, round(gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_USD/@devideDonVi,2), gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_USD) as FQtKinhPhiDuyetCacNamTruocUsd,
		   IIF(@devideDonVi is not null, round(gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_VND/@devideDonVi,2), gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_VND) as FQtKinhPhiDuyetCacNamTruocVnd,

		   IIF(@devideDonVi is not null, round((isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))/@devideDonVi,2)
		   , (isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))) as FQtKinhPhiDuocCapTongSoUsd,

		   IIF(@devideDonVi is not null, round((isnull(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND,0))/@devideDonVi,2)
		   , (isnull(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND,0))) as FQtKinhPhiDuocCapTongSoVnd,

		   IIF(@devideDonVi is not null, round(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD/@devideDonVi,2), ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD) as FQtKinhPhiDuocCapNamTruocChuyenSangUsd,
		   IIF(@devideDonVi is not null, round(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND/@devideDonVi,2), ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND) as FQtKinhPhiDuocCapNamTruocChuyenSangVnd,
		   IIF(@devideDonVi is not null, round(nnUSD.fQTKinhPhiDuocCap_NamNay_USD/@devideDonVi,2), nnUSD.fQTKinhPhiDuocCap_NamNay_USD) as FQtKinhPhiDuocCapNamNayUsd,
		   IIF(@devideDonVi is not null, round(nnVND.fQTKinhPhiDuocCap_NamNay_VND/@devideDonVi,2), nnVND.fQTKinhPhiDuocCap_NamNay_VND) as FQtKinhPhiDuocCapNamNayVnd,
		   IIF(@devideDonVi is not null, round(isnull(lknt.fLuyKeKinhPhiDuocCap_USD, 0)/@devideDonVi,2) + round(isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD, 0)/@devideDonVi,2), isnull(lknt.fLuyKeKinhPhiDuocCap_USD, 0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD, 0)) as FLuyKeKinhPhiDuocCapUsd,
		   IIF(@devideDonVi is not null, round(isnull(lknt.fLuyKeKinhPhiDuocCap_VND, 0)/@devideDonVi,2) + round(isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND, 0)/@devideDonVi,2), isnull(lknt.fLuyKeKinhPhiDuocCap_VND, 0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND, 0)) as FLuyKeKinhPhiDuocCapVnd,
		   (IIF(@devideDonVi is not null, round(khttnvc.fGiaTriKH_BQP/@devideDonVi,2), khttnvc.fGiaTriKH_BQP)) - IIF(@devideDonVi is not null, round((isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))/@devideDonVi,2), (isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))) as FKeHoachChuaGiaiNganUsd
		   ,b.iCoQuanThanhToan,b.iLoaiDeNghi,b.iThanhToanTheo,b.iLoaiNoiDungChi,b.iID_DonVi
		   ,a.iID_MucLucNganSachID as IIdMucLucNganSachId, 1 as IsData
	from NH_TT_ThanhToan_ChiTiet a 
	left join NH_TT_ThanhToan b 
	on a.iID_DeNghiThanhToanID = b.ID 
	left join NH_KHTongThe khtt on b.iID_KHTongTheID = khtt.ID
	left join NH_KHTongThe_NhiemVuChi khttnvc on khttnvc.iID_KHTongTheID = khtt.ID
	left join NH_DM_NhiemVuChi dmnvc on khttnvc.iID_NhiemVuChiID = dmnvc.ID
	left join NS_MucLucNganSach tb  on tb.iID = a.iID_MucLucNganSachID 
	left join NH_DA_DuAn da on  b.iID_DuAnId = da.ID 
	left join NH_DA_QDDauTu daqd on da.ID = daqd.iID_DuAnID
	left join DonVi dv on  b.iID_DonVi = dv.iID_DonVi 
	left join NH_DA_HopDong hd on b.iID_HopDongID = hd.ID
	left join #kpdnt as gdkpdnt on b.iID_NhiemVuChiID = gdkpdnt.ID
	left join NH_QT_QuyetToanNienDo_ChiTiet qtct on khttnvc.ID = qtct.iID_KHTT_NhiemVuChiID 
	left join #ntcsUSD as ntcsUSD on b.iLoaiDeNghi = ntcsUSD.iLoaiDeNghi and b.iCoQuanThanhToan = ntcsUSD.iCoQuanThanhToan and a.ID = ntcsUSD.ID
	left join #ntcsVND as ntcsVND on b.iLoaiDeNghi = ntcsUSD.iLoaiDeNghi and b.iCoQuanThanhToan = ntcsUSD.iCoQuanThanhToan and a.ID = ntcsVND.ID
	left join #nnUSD as nnUSD on b.iLoaiDeNghi = nnUSD.iLoaiDeNghi and b.iCoQuanThanhToan = nnUSD.iCoQuanThanhToan and a.ID = nnUSD.ID
	left join #nnVND as nnVND on b.iLoaiDeNghi = nnVND.iLoaiDeNghi and b.iCoQuanThanhToan = nnVND.iCoQuanThanhToan and a.ID = nnVND.ID
	left join #lknt as lknt on qtct.iID_DuAnID = lknt.iID_DuAnID or qtct.iID_HopDongID = lknt.iID_HopDongID or qtct.iID_KHTT_NhiemVuChiID = lknt.iID_KHTT_NhiemVuChiID
	where b.dNgayDeNghi between concat(@iNamKeHoach,'-01-01') and concat(@iNamKeHoach,'-12-31')   AND (@iIDDonVi is   null or b.iID_DonVi in (@iIDDonVi)) AND b.iTrangThai = 2
	/* finnal*/
	DROP TABLE #kpdnt
	DROP TABLE #ntcsVND
	DROP TABLE #ntcsUSD
	DROP TABLE #nnUSD
	DROP TABLE #nnVND
	DROP TABLE #lknt
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_index]    Script Date: 03/10/2022 4:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thuchien_ngansach_index]

AS
BEGIN
				Select distinct ttct.*, tt.iNamKeHoach, nvc.ID as IDNhiemVuChi, da.ID as IDDuAn, tt.iLoaiNoiDungChi,hd.ID as IDHopDong,
dm_nvc.sTenNhiemVuChi , da.sTenDuAn , hd.sTenHopDong,tt.iLoaiNoiDungChi, dmCDT.sTenDonVi as sTenCDT,
Case When hd.fGiaTriUSD > 0 then hd.fGiaTriUSD Else da.fUSD End as HopDongUSD ,
Case When hd.fGiaTriVND > 0 then hd.fGiaTriVND Else da.fVND End as HopDongVND ,
nvc.fGiaTriKH_TTCP as NCVTTCP ,nvc.fGiaTriKH_BQP as NhiemVuChi , 
QTND.KinhPhiUSD as KinhPhiUSD , QTND.KinhPhiVND as KinhPhiVND,
QTNDToY.KinhPhiToYUSD as KinhPhiToYUSD ,QTNDToY.KinhPhiToYVND as KinhPhiToYVND,
KPDC.KinhPhiDaChiUSD as KinhPhiDaChiUSD , KPDCVND.KinhPhiDaChiVND as KinhPhiDaChiVND , 
KPDCToY.KinhPhiDaChiUSD as KinhPhiDaChiToYUSD , KPDCVNDToY.KinhPhiDaChiVND as KinhPhiDaChiToYVND,
QTNDCT.fLuyKeKinhPhiDuocCap_USD , QTNDCT.fLuyKeKinhPhiDuocCap_VND,
QTNDCT.fDeNghiQTNamNay_USD , QTNDCT.fDeNghiQTNamNay_VND,
KHTT.iGiaiDoanDen , KHTT.iGiaiDoanTu, tt.iID_DonVi, tt.dNgayDeNghi
from NH_TT_ThanhToan_ChiTiet ttct
left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID 
left join NH_DA_HopDong hd on hd.ID = tt.iID_HopDongID
left join NH_DA_DuAn da on da.ID = hd.iID_DuAnID
left join DM_ChuDauTu dmCDT on da.iID_ChuDauTuID = dmCDT.iID_DonVi
left join NH_KHTongThe_NhiemVuChi nvc on nvc.iID_NhiemVuChiID = tt.iID_NhiemVuChiID
left join NH_DM_NhiemVuChi dm_nvc on dm_nvc.ID = nvc.iID_NhiemVuChiID
left join NH_KHTongThe KHTT on KHTT.ID = tt.iID_KHTongTheID
left join NH_QT_QuyetToanNienDo_ChiTiet QTNDCT on QTNDCT.iID_KHTT_NhiemVuChiID = nvc.ID
left join (
		Select NDCT.iID_HopDongID, Sum(NDCT.fQTKinhPhiDuocCap_TongSo_USD) as KinhPhiUSD, sum(NDCT.fQTKinhPhiDuocCap_TongSo_VND) as KinhPhiVND
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT group by NDCT.iID_HopDongID)
	QTND on QTND.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiUSD from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 1 and ThanhToan.dNgayDeNghi > convert(datetime,(concat(year(GETDATE()),'-1-1 00:00:00.000'))) and 
			((ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-9-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 4)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-6-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 3)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-3-31 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 2)
			)
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCToY on KPDCToY.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiVND from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 2 and ThanhToan.dNgayDeNghi > convert(datetime,(concat(year(GETDATE()),'-1-1 00:00:00.000'))) and 
			((ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-9-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 4)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-6-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 3)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-3-31 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 2)
			)
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCVNDToY on KPDCVNDToY.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiUSD from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 1 and ThanhToan.iNamKeHoach = DATEPART(YEAR,GETDATE()) and ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE())-1,'-12-31 00:00:00.000')))
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDC on KPDC.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiVND from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 2 and ThanhToan.iNamKeHoach = DATEPART(YEAR,GETDATE()) and ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE())-1,'-12-31 00:00:00.000')))
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCVND on KPDCVND.iID_HopDongID = hd.ID
left join (
	Select NDCT.iID_HopDongID, Sum(NDCT.fQTKinhPhiDuocCap_TongSo_USD) as KinhPhiToYUSD, sum(NDCT.fQTKinhPhiDuocCap_TongSo_VND) as KinhPhiToYVND
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT
		left join NH_QT_QuyetToanNienDo NDCTParent on NDCTParent.ID = NDCT.iID_QuyetToanNienDoID
		Where NDCTParent.iNamKeHoach = DATEPART(YEAR,GETDATE())
		group by NDCT.iID_HopDongID) 
		QTNDToY on QTNDToY.iID_HopDongID = hd.ID

Order by nvc.ID desc , dm_nvc.sTenNhiemVuChi , da.ID desc , da.sTenDuAn, tt.iLoaiNoiDungChi ,hd.ID , hd.sTenHopDong

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_report]    Script Date: 03/10/2022 4:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thuchien_ngansach_report]
	@tabindex int, 
	@iQuyPrint int,
	@iTuNamPrint int,
	@iDenNamPrint int,
	@iDonvi uniqueidentifier
AS
BEGIN
		Select distinct ttct.*, tt.iNamKeHoach, nvc.ID as IDNhiemVuChi, da.ID as IDDuAn, tt.iLoaiNoiDungChi,hd.ID as IDHopDong,
dm_nvc.sTenNhiemVuChi , da.sTenDuAn , hd.sTenHopDong,tt.iLoaiNoiDungChi, dmCDT.sTenDonVi as sTenCDT,
Case When hd.fGiaTriUSD > 0 then hd.fGiaTriUSD Else da.fUSD End as HopDongUSD ,
Case When hd.fGiaTriVND > 0 then hd.fGiaTriVND Else da.fVND End as HopDongVND ,
nvc.fGiaTriKH_TTCP as NCVTTCP ,nvc.fGiaTriKH_BQP as NhiemVuChi , 
QTND.KinhPhiUSD as KinhPhiUSD , QTND.KinhPhiVND as KinhPhiVND,
QTNDToY.KinhPhiToYUSD as KinhPhiToYUSD ,QTNDToY.KinhPhiToYVND as KinhPhiToYVND,
KPDC.KinhPhiDaChiUSD as KinhPhiDaChiUSD , KPDCVND.KinhPhiDaChiVND as KinhPhiDaChiVND , 
KPDCToY.KinhPhiDaChiUSD as KinhPhiDaChiToYUSD , KPDCVNDToY.KinhPhiDaChiVND as KinhPhiDaChiToYVND,
QTNDCT.fLuyKeKinhPhiDuocCap_USD , QTNDCT.fLuyKeKinhPhiDuocCap_VND,
KHTT.iGiaiDoanDen , KHTT.iGiaiDoanTu , tt.iID_DonVi
from NH_TT_ThanhToan_ChiTiet ttct
left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID 
left join NH_DA_HopDong hd on hd.ID = tt.iID_HopDongID
left join NH_DA_DuAn da on da.ID = hd.iID_DuAnID
left join DM_ChuDauTu dmCDT on da.iID_ChuDauTuID = dmCDT.iID_DonVi
left join NH_KHTongThe_NhiemVuChi nvc on nvc.iID_NhiemVuChiID = tt.iID_NhiemVuChiID
left join NH_DM_NhiemVuChi dm_nvc on dm_nvc.ID = nvc.iID_NhiemVuChiID
left join NH_KHTongThe KHTT on KHTT.ID = tt.iID_KHTongTheID
left join NH_QT_QuyetToanNienDo_ChiTiet QTNDCT on QTNDCT.iID_KHTT_NhiemVuChiID = nvc.ID
left join (
		Select NDCT.iID_HopDongID, Sum(NDCT.fQTKinhPhiDuocCap_TongSo_USD) as KinhPhiUSD, sum(NDCT.fQTKinhPhiDuocCap_TongSo_VND) as KinhPhiVND
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT group by NDCT.iID_HopDongID)
	QTND on QTND.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiUSD from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 1 and ThanhToan.dNgayDeNghi > convert(datetime,(concat(year(GETDATE()),'-1-1 00:00:00.000'))) and 
			((ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-9-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 4)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-6-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 3)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-3-31 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 2)
			)
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCToY on KPDCToY.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiVND from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 2 and ThanhToan.dNgayDeNghi > convert(datetime,(concat(year(GETDATE()),'-1-1 00:00:00.000'))) and 
			((ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-9-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 4)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-6-30 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 3)
			or (ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE()),'-3-31 00:00:00.000'))) and DATEPART(quarter,GETDATE()) = 2)
			)
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCVNDToY on KPDCVNDToY.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiUSD from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 1 and ThanhToan.iNamKeHoach = DATEPART(YEAR,GETDATE()) and ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE())-1,'-12-31 00:00:00.000')))
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDC on KPDC.iID_HopDongID = hd.ID
left join (
		select ThanhToan.iLoaiDeNghi , ThanhToan.iID_HopDongID , sum(ThanhToan.fChuyenKhoan_BangSo) as KinhPhiDaChiVND from NH_TT_ThanhToan ThanhToan
		join NH_TT_ThanhToan_ChiTiet b 
		on ThanhToan.ID = b.iID_DeNghiThanhToanID 
		where ThanhToan.iLoaiNoiDungChi = 2 and ThanhToan.iNamKeHoach = DATEPART(YEAR,GETDATE()) and ThanhToan.dNgayDeNghi < convert(datetime,(concat(year(GETDATE())-1,'-12-31 00:00:00.000')))
		Group BY ThanhToan.iLoaiDeNghi, ThanhToan.iID_HopDongID having ThanhToan.iLoaiDeNghi = 2 or ThanhToan.iLoaiDeNghi = 3
	) KPDCVND on KPDCVND.iID_HopDongID = hd.ID
left join (
	Select NDCT.iID_HopDongID, Sum(NDCT.fQTKinhPhiDuocCap_TongSo_USD) as KinhPhiToYUSD, sum(NDCT.fQTKinhPhiDuocCap_TongSo_VND) as KinhPhiToYVND
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT
		left join NH_QT_QuyetToanNienDo NDCTParent on NDCTParent.ID = NDCT.iID_QuyetToanNienDoID
		Where NDCTParent.iNamKeHoach = DATEPART(YEAR,GETDATE())
		group by NDCT.iID_HopDongID) 
		QTNDToY on QTNDToY.iID_HopDongID = hd.ID

		Where ((@tabindex = 0 and ( (@iQuyPrint = -2 or (Month(tt.dNgayDeNghi) >= (@iQuyPrint - 1 )*3 + 1)) and (@iTuNamPrint = -2 or ( 
(Year(tt.dNgayDeNghi) >=  @iTuNamPrint))) )) or (@tabindex = 1 and 
((@iTuNamPrint = -2 or Year(tt.dNgayDeNghi) >=  @iTuNamPrint ) and (@iDenNamPrint = -2 or Year(tt.dNgayDeNghi) <=  @iDenNamPrint ))
)) and (tt.iID_DonVi = @iDonvi or @iDonvi is null or @iDonvi = '00000000-0000-0000-0000-000000000000')
Order by nvc.ID desc , dm_nvc.sTenNhiemVuChi , da.ID desc , da.sTenDuAn, tt.iLoaiNoiDungChi ,hd.ID , hd.sTenHopDong

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt]    Script Date: 03/10/2022 4:53:13 PM ******/
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
        AND chitiet.iLoai in (select * from f_split(@Loai))
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 03/10/2022 4:53:13 PM ******/
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
	 AND ((@bTongHop = 0)  or (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop))
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
	WHERE (@Loai = 0 and dt_dv.iLoai != 0) or @Loai <> 0
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
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_mucluc_index_chungtu_bvtc]    Script Date: 03/10/2022 4:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_mucluc_index_chungtu_bvtc]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetOfSource int,
	@VoucherId nvarchar(max),
	@Loai nvarchar(max),
	@LoaiChungTu int,
	@AgencyId nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT mucluc.iID AS Id,
       mucluc.iID_MLSKT AS IdMucLuc,
       mucluc.sKyHieu AS KyHieu,
       mucluc.sM AS M,
       mucluc.sSTT AS STT,
       mucluc.sMoTa AS MoTa,
       mucluc.bHangCha ,
       mucluc.iNamLamViec AS NamLamViec,
       mucluc.dNgayTao AS DateCreated,
       mucluc.dNguoiTao AS UserCreator,
       mucluc.dNgaySua AS DateModified,
       mucluc.dNguoiSua AS UserModifier,
       mucluc.Muc,
       '' AS LNS,
       mucluc.iID_MLSKTCha AS IdParent ,
       datachitiet.TuChi ,
       ISNULL(datachitiet.HangMua, 0) AS HangMua ,
       ISNULL(datachitiet.HangNhap, 0) AS HangNhap ,
       ISNULL(datachitiet.PhanCap, 0) AS PhanCap ,
       ISNULL(datachitiet.MuaHangHienVat, 0) AS MuaHangHienVat ,
       ISNULL(datachitiet.DacThu, 0) AS DacThu,

	   ISNULL(dutoandaunam.TuChi, 0) AS DtTuChi,
	   ISNULL(dutoandaunam.HangNhap, 0) AS DtHangNhap,
	   ISNULL(dutoandaunam.HangMua, 0) AS DtHangMua,
	   ISNULL(dutoandaunam.PhanCap, 0) AS DtPhanCap,
	   ISNULL(dutoandaunam.DuPhong, 0) AS DtDuPhong,
	   ISNULL(dutoandaunam.ChuaPhanCap, 0) AS DtChuaPhanCap
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT SUM(fTuChi) AS TuChi,
          CAST(0 AS FLOAT) AS HangMua,
          CAST(0 AS FLOAT) AS HangNhap,
          SUM(fPhanCap) AS PhanCap,
          SUM(fMuaHangCapHienVat) AS MuaHangHienVat,
          SUM(fPhanCap) AS DacThu,
          iID_MLSKT
   FROM NS_SKT_ChungTuChiTiet chitiet
   WHERE chitiet.iNamLamViec = @YearOfWork
     AND chitiet.iNamNganSach = @YearOfBudget
	 AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
     AND chitiet.iLoaiChungTu = @LoaiChungTu
     AND chitiet.iLoai in (select * from f_split(@loai))
     AND chitiet.iID_MaDonVi = @AgencyId
   GROUP BY iID_MLSKT) datachitiet ON mucluc.iID_MLSKT = datachitiet.iID_MLSKT

LEFT JOIN 
	(
	select 
	SUM(chitiet.fTuChi) AS TuChi, 
	SUM(chitiet.fHangNhap) AS HangNhap,
	SUM(chitiet.fHangMua) AS HangMua,
	SUM(chitiet.fPhanCap) AS PhanCap,
	SUM(chitiet.fDuPhong) AS DuPhong,
	SUM(chitiet.fChuaPhanCap) AS ChuaPhanCap,
	mucluc.iID_MLSKT

	FROM NS_DTDauNam_ChungTuChiTiet chitiet
	left join (select * FROM NS_MLSKT_MLNS where iNamLamViec = @YearOfWork) map on chitiet.sXauNoiMa = map.sNS_XauNoiMa
	left join (select * FROM NS_SKT_MucLuc where iNamLamViec = @YearOfWork) mucluc on map.sSKT_KyHieu = mucluc.sKyHieu
	where
	chitiet.iNamLamViec = @YearOfWork
     AND chitiet.iNamNganSach = @YearOfBudget
	 AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
     AND chitiet.iLoaiChungTu = @LoaiChungTu
	 AND chitiet.iID_MaDonVi = @AgencyId
	 AND chitiet.iID_CTDTDauNam <> @VoucherId
	 AND mucluc.iID_MLSKT is not null
	group by mucluc.iID_MLSKT
	) dutoandaunam on dutoandaunam.iID_MLSKT = mucluc.iID_MLSKT

WHERE mucluc.iNamLamViec = @YearOfWork
  AND mucluc.iTrangThai = 1
ORDER BY mucluc.sKyHieu
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhan_so_kiem_tra]    Script Date: 03/10/2022 4:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_nhan_so_kiem_tra]
	@Loai nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiChungTu int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @NamLamViec AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT 
		ct.iID_CTSoKiemTra,
		ct.iID_MaDonVi,
		ct.SSoChungTu,
		ct.DNgayChungTu,
		ct.SSoQuyetDinh,
		ct.DNgayQuyetDinh,
		ct.SMoTa,
		ct.iLoai,
		ct.bKhoa,
		ct.iNamLamViec,
		ct.iNamNganSach,
		ct.iID_MaNguonNganSach,
		ct.DNgayTao,
		ct.DNgaySua,
		ct.SNguoiTao,
		ct.SNguoiSua,
		ct.iSoChungTuIndex,
		ct.iLoaiChungTu,
		ct.sDSSoChungTuTongHop,
		ctct.fTongTuChi,
		ctct.fTongPhanCap,
		ctct.fTongMuaHangCapHienVat,
		ct.sTenDonVi,
		ct.bDaTongHop
	FROM
		(
			SELECT 
				ct.*, ddv.sTenDonVi 
			FROM 
				NS_SKT_ChungTu ct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE 
				ct.iNamLamViec = @NamLamViec 
				AND ct.iNamNganSach = @NamNganSach
				AND ct.iID_MaNguonNganSach = @NguonNganSach
				--AND ct.iLoai = @Loai 
				AND ct.iLoai in (select * from f_split(@Loai)) 
				AND ct.iLoaiChungTu = @LoaiChungTu
				AND EXISTS (SELECT * from f_split(ct.iID_MaDonVi) INTERSECT SELECT iID_MaDonVi FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1)
				AND ((@CountDonViCha = 0 and bKhoa = 1) OR (@CountDonViCha <> 0))
		) ct
	LEFT JOIN 
		(
			SELECT 
				iID_CTSoKiemTra,
				sum(fTuChi) as fTongTuChi,
				sum(fPhanCap) as fTongPhanCap,
				sum(fMuaHangCapHienVat) as fTongMuaHangCapHienVat
			FROM 
				NS_SKT_ChungTuChiTiet ctct
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi
			WHERE 
				--ctct.iLoai = @Loai
				ctct.iLoai in (select * from f_split(@Loai)) 
				AND ctct.iNamLamViec = @NamLamViec
				AND ctct.iNamNganSach = @NamNganSach
				AND ctct.iID_MaNguonNganSach = @NguonNganSach
				AND 
				(
					(
						(dv.iLoai = '0' OR @LoaiChungTu = 1) 
						AND EXISTS (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap)))
					)
					OR 
					(
						EXISTS 
						(
							SELECT * FROM (SELECT * FROM NS_SKT_MucLuc WHERE iID_MLSKT = ctct.iID_MLSKT AND iNamLamViec = @NamLamViec AND @LoaiChungTu IN (SELECT * FROM f_split(sLoaiNhap))) ml
							JOIN DanhMuc dm ON dm.iID_MaDanhMuc = ml.sNG AND dm.sType = 'NS_Nganh' AND dm.sGiaTri = dv.iID_MaDonVi AND dm.iNamLamViec = @NamLamViec
						)
					)
				)
		GROUP BY iID_CTSoKiemTra
		) ctct
	ON ctct.iID_CTSoKiemTra =  ct.iID_CTSoKiemTra;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop]    Script Date: 03/10/2022 4:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop]
	 @NamLamViec int,													
	 @IdDonvi nvarchar(2000),
	 @LoaiChungTu nvarchar(50),
	 @DonViTinh int
AS
BEGIN 
	SET NOCOUNT ON;
SELECT chitiet.*, mlns.iID_MLNS AS MlnsId, iID_MLNS_Cha AS MlnsIdParent FROM (	

SELECT 
    LNS1 = Left(sLNS, 1),
    LNS3 = Left(sLNS, 3),
    LNS5 = Left(sLNS, 5),
    sLNS AS LNS,
    sL AS L,
    sK AS K,
    sM AS M,
    sTM AS TM,
    sTTM AS TTM,
    sNG AS NG,
    sMoTa AS MoTa,
    sXauNoiMa AS XauNoiMa,
    QuyetToan = SUM(ISNULL(QuyetToan, 0)) / @DonViTinh,
    DuToan = SUM(isnull(DuToan, 0)) / @DonViTinh,
    TuChi = SUM(TuChi) / @DonViTinh,
	UocThucHien = SUM(fUocThucHien) / @DonViTinh

FROM
 (SELECT 
    sLNS, sL, sK, sM, sTM, sTTM, sNG,
    sMoTa,
    sXauNoiMa,
    QuyetToan = 0,
    DuToan = 0,
    CASE
      WHEN @LoaiChungTu = '1' THEN fTuChi
      WHEN @LoaiChungTu = '2' THEN fHangNhap + fHangMua + fPhanCap
      ELSE 0
    END AS TuChi,
    fUocThucHien
  FROM NS_DTDauNam_ChungTuChiTiet
  WHERE iNamLamViec = @NamLamViec
    AND iLoai = 3
    AND iLoaiChungTu = @LoaiChungTu
    AND (@IdDonvi IS NULL OR iID_MaDonVi IN (SELECT * FROM f_split(@IdDonvi)))

  UNION ALL SELECT LNS, L, K, M, TM, TTM, NG,
          MoTa,
          XauNoiMa,
          QuyetToan,
          DuToan,
          TuChi = 0,
		  UocThucHien = 0
  FROM f_skt_dulieu(@NamLamViec, @IdDonvi, @LoaiChungTu)) AS dt


GROUP BY sLNS, sL, sK, sM, sTM, sTTM, sNG, sMoTa, sXauNoiMa
HAVING 
SUM(QuyetToan) <> 0
OR SUM(TuChi) <> 0
OR SUM(DuToan) <> 0
OR SUM(fUocThucHien) <> 0) chitiet 
LEFT JOIN (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @NamLamViec) mlns ON chitiet.XauNoiMa = mlns.sXauNoiMa

END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_baocaotinhhinhduan]    Script Date: 03/10/2022 4:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_baocaotinhhinhduan]
@IdDuAn nvarchar(500),
@NgayDeNghi datetime
AS
BEGIN      
	SELECT  tbl.iID_HopDongId ,
		tbl.iID_NhaThauId,
		SUM(CASE WHEN iLoaiThanhToan = 1 THEN (ISNULL(dt.fGiaTriThanhToanTN, 0) + ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE 0 END) as SoThanhToan,
		SUM(CASE WHEN iLoaiThanhToan = 0 THEN (ISNULL(dt.fGiaTriThanhToanTN, 0) + ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE 0 END) as SoTamUng,
		SUM(ISNULL(dt.fGiaTriThuHoiNamNayNN, 0)+ ISNULL(dt.fGiaTriThuHoiNamNayTN, 0) + ISNULL(dt.fGiaTriThuHoiNamTruocNN, 0) + ISNULL(fGiaTriThuHoiNamTruocTN, 0) 
			+ ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocNN, 0) 
			+ ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN, 0)) as SoThuHoiTamUng INTO #tmp
	from VDT_TT_DeNghiThanhToan tbl
	inner join VDT_TT_PheDuyetThanhToan_ChiTiet dt on tbl.Id = dt.iID_DeNghiThanhToanID
	and tbl.iID_DuAnId = @IdDuAn
	and CAST(tbl.dNgayPheDuyet as DATE) <= CAST(@NgayDeNghi as DATE)
	group by  tbl.iID_HopDongId, tbl.iID_NhaThauId
	
	SELECT 
		CAST(0 as bit) as IsHangCha,
		NULL as NguonVonId,
		NULL as Loai,
		NULL as Mlns,
		nt.sTenNhaThau as TenNhaThau,
		hd.sSoHopDong as SoHopDong,
		hd.iThoiGianThucHien as ThoiGianThucHien,
		hd.fTienHopDong as TienHopDong,
		NULL as SoDeNghi,
		NULL as IdDeNghiThanhToan,
		NULL as NgayThanhToan,
		(ISNULL(tmp.SoThanhToan, 0) + ISNULL(tmp.SoTamUng, 0) - ISNULL(tmp.SoThuHoiTamUng, 0)) as TongCongGiaiNgan,
		NULL as NgayCapUng,
		NULL as SoDaCapUng,
		tmp.*
	FROM #tmp as tmp
	LEFT JOIN VDT_DM_NhaThau as nt on tmp.iID_NhaThauId = nt.Id
	LEFT JOIN VDT_DA_TT_HopDong as hd on tmp.iID_HopDongId = hd.Id
	DROP TABLE #tmp
END


GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_bcketquagiaingankinhphidautu]    Script Date: 03/10/2022 4:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_bcketquagiaingankinhphidautu]
@iIdMaDonVi nvarchar(100),
@iNamKeHoach int,
@iIdNganSach int
AS
BEGIN

	SELECT
		tbl.iID_MaDonViQuanLy,
		SUM(CASE WHEN tbl.sMaNguon in ('111', '112') AND tbl.sMaTienTrinh = '100' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fDuToanDeNghiChuyen,
		SUM(CASE WHEN tbl.sMaDich in ('111', '112') AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fDuToanDeNghiChuyenRevert,

		SUM(CASE WHEN tbl.sMaNguon in ('101', '102') AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fNamNay,
		SUM(CASE WHEN tbl.sMaDich in ('101', '102') AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fNamNayRevert,

		SUM(CASE WHEN tbl.sMaNguon in ('102') AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fKHVNLenhChi,
		SUM(CASE WHEN tbl.sMaDich in ('102') AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fKHVNLenhChiRevert,

		SUM(CASE WHEN tbl.sMaNguon in ('112') AND tbl.sMaTienTrinh = '100' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fKHVNamTruocChuyenSangLenhChi,
		SUM(CASE WHEN tbl.sMaDich in ('112') AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fKHVNamTruocChuyenSangLenhChiRevert,

		SUM(CASE WHEN tbl.sMaDich in ('202') AND tbl.sMaNguonCha = '102' AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNLenhChi,
		SUM(CASE WHEN tbl.sMaNguon in ('202') AND tbl.sMaNguonCha = '102' AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNLenhChiRevert,

		SUM(CASE WHEN tbl.sMaDich in ('122a') AND tbl.sMaNguonCha = '102' AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiVonNamTruoc,
		SUM(CASE WHEN tbl.sMaNguon in ('122a') AND tbl.sMaNguonCha = '102' AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiVonNamTruocRevert,

		SUM(CASE WHEN tbl.sMaNguon in ('212a') AND tbl.sMaNguonCha = '102' AND tbl.iLoaiUng = 1 AND tbl.iThuHoiTUCheDo = 1 AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamTruocLenhChi,
		SUM(CASE WHEN tbl.sMaDich in ('212a')  AND tbl.sMaNguonCha = '102' AND tbl.iLoaiUng = 1 AND tbl.iThuHoiTUCheDo = 1 AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamTruocLenhChiRevert,

		SUM(CASE WHEN tbl.sMaDich in ('212a') AND tbl.sMaNguonCha = '102' AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fTamUngLenhChi,
		SUM(CASE WHEN tbl.sMaNguon in ('212a') AND tbl.sMaNguonCha = '102' AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fTamUngLenhChiRevert,

		SUM(CASE WHEN tbl.sMaNguon in ('212a') AND tbl.sMaNguonCha = '102' AND tbl.iLoaiUng = 1 AND tbl.iThuHoiTUCheDo = 2 AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamNayLenhChi,
		SUM(CASE WHEN tbl.sMaDich in ('212a')  AND tbl.sMaNguonCha = '102' AND tbl.iLoaiUng = 1 AND tbl.iThuHoiTUCheDo = 2 AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamNayLenhChiRevert,

		SUM(CASE WHEN tbl.sMaNguon in ('101') AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fKHVNKhoBac,
		SUM(CASE WHEN tbl.sMaDich in ('101') AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fKHVNKhoBacRevert,

		SUM(CASE WHEN tbl.sMaNguon in ('111') AND tbl.sMaTienTrinh = '100' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fKHVNamTruocChuyenSangKhoBac,
		SUM(CASE WHEN tbl.sMaDich in ('111') AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fKHVNamTruocChuyenSangKhoBacRevert,

		SUM(CASE WHEN tbl.sMaDich in ('201') AND tbl.sMaNguonCha = '101' AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNKhoBac,
		SUM(CASE WHEN tbl.sMaNguon in ('201') AND tbl.sMaNguonCha = '101' AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThanhToanKHVNKhoBacRevert,

		SUM(CASE WHEN tbl.sMaDich in ('121a') AND tbl.sMaNguonCha = '101' AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiVonNamTruocKhoBac,
		SUM(CASE WHEN tbl.sMaNguon in ('121a') AND tbl.sMaNguonCha = '101' AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiVonNamTruocKhoBacRevert,

		SUM(CASE WHEN tbl.sMaNguon in ('212a') AND tbl.sMaNguonCha = '102' AND tbl.iLoaiUng = 1 AND tbl.iThuHoiTUCheDo = 1 AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamTruocKhoBac,
		SUM(CASE WHEN tbl.sMaDich in ('212a')  AND tbl.sMaNguonCha = '102' AND tbl.iLoaiUng = 1 AND tbl.iThuHoiTUCheDo = 1 AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamTruocKhoBacRevert,

		SUM(CASE WHEN tbl.sMaDich in ('212a') AND tbl.sMaNguonCha = '102' AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fTamUngKhoBac,
		SUM(CASE WHEN tbl.sMaNguon in ('212a') AND tbl.sMaNguonCha = '102' AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fTamUngKhoBacRevert,

		SUM(CASE WHEN tbl.sMaNguon in ('212a') AND tbl.sMaNguonCha = '102' AND tbl.iLoaiUng = 1 AND tbl.iThuHoiTUCheDo = 2 AND tbl.sMaTienTrinh = '200' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamNayKhoBac,
		SUM(CASE WHEN tbl.sMaDich in ('212a')  AND tbl.sMaNguonCha = '102' AND tbl.iLoaiUng = 1 AND tbl.iThuHoiTUCheDo = 2 AND tbl.sMaTienTrinh = '300' THEN ISNULL(tbl.fGiaTri, 0) ELSE 0 END) as fThuHoiUngNamNayKhoBacRevert

		INTO #tmp
	FROM VDT_TongHop_NguonNSDauTu as tbl
	WHERE tbl.iID_MaDonViQuanLy = @iIdMaDonVi AND tbl.iID_NguonVonID = @iIdNganSach AND tbl.iNamKeHoach = @iNamKeHoach
	GROUP BY tbl.iID_MaDonViQuanLy


	SELECT dv.sTenDonVi as sTenDonVi,
		(ISNULL(fDuToanDeNghiChuyen, 0) - ISNULL(fDuToanDeNghiChuyenRevert, 0)) as fDuToanDeNghiChuyen,
		(ISNULL(fNamNay, 0) - ISNULL(fNamNayRevert, 0)) as fNamNay,
		((ISNULL(fKHVNLenhChi, 0) - ISNULL(fKHVNLenhChiRevert, 0)) + (ISNULL(fKHVNamTruocChuyenSangLenhChi, 0) - ISNULL(fKHVNamTruocChuyenSangLenhChiRevert, 0)))fDuToanDuocThongBao,
		CAST(0 as float) as fCucTaiChinhThanhToanTrucTiep,
		((ISNULL(tmp.fThanhToanKHVNLenhChi, 0) - ISNULL(tmp.fThanhToanKHVNLenhChiRevert, 0)) + (ISNULL(tmp.fThuHoiVonNamTruoc, 0) - ISNULL(tmp.fThuHoiVonNamTruocRevert, 0))
			- (ISNULL(tmp.fThuHoiUngNamTruocLenhChi, 0) - ISNULL(tmp.fThuHoiUngNamTruocLenhChiRevert, 0))) fThanhToanKLHTBQP,
		((ISNULL(tmp.fTamUngLenhChi, 0) - ISNULL(tmp.fTamUngLenhChiRevert, 0)) - (ISNULL(tmp.fThuHoiUngNamNayLenhChi, 0) - ISNULL(tmp.fThuHoiUngNamNayLenhChiRevert, 0))) as fTamUngBQP,
		((ISNULL(tmp.fKHVNKhoBac, 0) - ISNULL(tmp.fKHVNKhoBacRevert, 0)) + (ISNULL(tmp.fKHVNamTruocChuyenSangKhoBac, 0) - ISNULL(tmp.fKHVNamTruocChuyenSangKhoBacRevert, 0))) as fDuToanDuocThongBaoKhoBac,
		((ISNULL(tmp.fThanhToanKHVNKhoBac, 0) - ISNULL(tmp.fThanhToanKHVNKhoBacRevert, 0)) + (ISNULL(tmp.fThuHoiVonNamTruocKhoBac, 0) - ISNULL(tmp.fThuHoiVonNamTruocKhoBacRevert, 0))
			- (ISNULL(tmp.fThuHoiUngNamTruocKhoBac, 0) - ISNULL(tmp.fThuHoiUngNamTruocKhoBac, 0))) as fThanhToanKLHTKhoBac,
		((ISNULL(tmp.fTamUngKhoBac, 0) - ISNULL(tmp.fTamUngKhoBacRevert, 0)) - (ISNULL(tmp.fThuHoiUngNamNayKhoBac, 0) - ISNULL(tmp.fThuHoiUngNamNayKhoBacRevert, 0))) as fTamUngKhoBac
	FROM #tmp as tmp
	LEFT JOIN DonVi as dv on tmp.iID_MaDonViQuanLy = dv.iID_MaDonVi AND iNamLamViec = @iNamKeHoach
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_bctonghopthongtinduan]    Script Date: 03/10/2022 4:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_bctonghopthongtinduan]
@iIDMaDonVi nvarchar(100),
@iNamKeHoach int
AS
BEGIN
	SELECT tmp.iID_DuAnID as IIdDuAnId,
		SUM(CASE WHEN sMaNguon in ('301', '302') AND sMaTienTrinh = '100' AND iNamKeHoach = (@iNamKeHoach - 1) THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fLuyKeKHVN,
		SUM(CASE WHEN sMaDich in ('301', '302') AND sMaTienTrinh = '300' AND iNamKeHoach = (@iNamKeHoach - 1) THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fLuyKeKHVNRevert,

		SUM(CASE WHEN sMaNguon in ('101', '102') AND sMaTienTrinh = '200' AND iNamKeHoach = @iNamKeHoach THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fKHVNNay,
		SUM(CASE WHEN sMaDich in ('101', '102') AND sMaTienTrinh = '300' AND iNamKeHoach = @iNamKeHoach THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fKHVNNayRevert,

		SUM(CASE WHEN sMaDich in ('201', '202') AND sMaNguonCha in ('101', '102') AND sMaTienTrinh = '200' AND iNamKeHoach = @iNamKeHoach THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThanhToanKHV,
		SUM(CASE WHEN sMaNguon in ('201', '202') AND sMaNguonCha in ('101', '102') AND sMaTienTrinh = '300' AND iNamKeHoach = @iNamKeHoach THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThanhToanKHVRevert,

		SUM(CASE WHEN sMaDich in ('211a', '212a') AND sMaNguonCha in ('101', '102') AND sMaTienTrinh = '200' AND iNamKeHoach = @iNamKeHoach THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngKHVN,
		SUM(CASE WHEN sMaNguon in ('211a', '212a') AND sMaNguonCha in ('101', '102') AND sMaTienTrinh = '300' AND iNamKeHoach = @iNamKeHoach THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fTamUngKHVNRevert,

		SUM(CASE WHEN sMaNguon in ('211a', '212a') AND sMaNguonCha in ('101', '102') AND sMaTienTrinh = '200' AND iNamKeHoach = @iNamKeHoach AND iLoaiUng = 1 THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThuHoiUngKHVN,
		SUM(CASE WHEN sMaDich in ('211a', '212a') AND sMaNguonCha in ('101', '102') AND sMaTienTrinh = '300' AND iNamKeHoach = @iNamKeHoach AND iLoaiUng = 1 THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThuHoiUngKHVNRevert,

		SUM(CASE WHEN sMaDich in ('121a', '122a') AND sMaNguonCha in ('101', '102') AND sMaTienTrinh = '200' AND iNamKeHoach = @iNamKeHoach THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThuHoiUngTruocKHVN,
		SUM(CASE WHEN sMaNguon in ('121a', '122a') AND sMaNguonCha in ('101', '102') AND sMaTienTrinh = '300' AND iNamKeHoach = @iNamKeHoach THEN ISNULL(fGiaTri, 0) ELSE 0 END) as fThuHoiUngTruocKHVNRevert	
			INTO #tmp
	FROM VDT_TongHop_NguonNSDauTu as tmp
	INNER JOIN VDT_DA_DuAn as da on tmp.iID_DuAnID = da.iID_DuAnID
	WHERE da.iID_MaDonViThucHienDuAnID = @iIDMaDonVi 
		AND (tmp.iNamKeHoach = @iNamKeHoach OR tmp.iNamKeHoach = (@iNamKeHoach - 1))
	GROUP BY tmp.iID_DuAnID

	SELECT tmp.IIdDuAnId,
		da.sTenDuAn as STenDuAn,
		NULL as TenDonVi,
		null as TienTe,
		ct.sSoQuyetDinh as SoQuyetDinhChuTruong,
		ct.dNgayQuyetDinh as NgayQuyetDinhChuTruong,
		ISNULL(ct.fTMDTDuKienPheDuyet, 0) as GiaTriDauTu,
		qd.sSoQuyetDinh as SoQuyetDinhDauTu,
		qd.dNgayQuyetDinh as NgayQuyetDinhDauTu,
		ISNULL(qd.fTongMucDauTuPheDuyet, 0) as TongMucDauTu,
		(ISNULL(tmp.fLuyKeKHVN, 0) - ISNULL(tmp.fLuyKeKHVNRevert, 0)) as LuyKeVonNamTruoc,
		(ISNULL(tmp.fKHVNNay, 0) - ISNULL(tmp.fKHVNNayRevert, 0)) as KeHoachVonNamNay,
		((ISNULL(tmp.fThanhToanKHV, 0) - ISNULL(tmp.fThanhToanKHVRevert, 0)) + (ISNULL(tmp.fTamUngKHVN, 0) - ISNULL(tmp.fTamUngKHVNRevert, 0))
			- (ISNULL(tmp.fThuHoiUngKHVN, 0) - ISNULL(tmp.fThuHoiUngKHVNRevert, 0)) + (ISNULL(tmp.fThuHoiUngTruocKHVN, 0) - ISNULL(tmp.fThuHoiUngTruocKHVNRevert, 0))) as DaThanhToan,
		CAST(0 as float) as GiaTriQuyetToan,
		CAST(0 as float) as ChenhLechQTThanhToan,
		NULL as SoQuyetDinhQuyetToan,
		NULL as NgayQuyetDinhQuyetToan,
		NULL as SGhiChu
	FROM #tmp as tmp
	INNER JOIN VDT_DA_DuAn as da on tmp.IIdDuAnId = da.iID_DuAnID
	LEFT JOIN VDT_DA_ChuTruongDauTu as ct on tmp.IIdDuAnId = ct.iID_DuAnID AND ct.bActive = 1
	LEFT JOIN VDT_DA_QDDauTu as qd on tmp.IIdDuAnId = qd.iID_DuAnID AND qd.bActive = 1


	DROP TABLE #tmp
END
GO


UPDATE TL_DM_Cach_TinhLuong_Chuan set CongThuc ='GTKHAC_TT+TRICHLUONG_TT'
where Ma_Cot='PHAITRUKHAC_SUM'
update TL_DM_PhuCap set Is_Formula = 1 where Ma_PhuCap = 'PCKVCS_TT'
update TL_DM_PhuCap set Parent = 'LPC_HS', Xau_Noi_Ma = 'LPC_HS-PCTEMTHU_TT' where Ma_PhuCap = 'PCTEMTHU_TT'
delete TL_Bao_Cao where Ma_BaoCao = 4 and Note = N'Bỏ'
update TL_DM_PhuCap set bSaoChep = 0 where bSaoChep is null
update TL_DM_PhuCap set Is_Readonly = 0 where Is_Readonly is null
update TL_DM_PhuCap set bHuongPc_Sn = 0 where bHuongPc_Sn is null
update TL_DM_PhuCap set bGiaTri = 0 where bGiaTri is null
