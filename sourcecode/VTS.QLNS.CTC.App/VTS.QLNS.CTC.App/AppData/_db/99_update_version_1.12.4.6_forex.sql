/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_index]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thuchien_ngansach_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thuchien_ngansach_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtri_capphat_index]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtri_capphat_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtri_capphat_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_goithau_index]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_thongtin_hopdong_goithau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_thongtin_hopdong_goithau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_get_duan_By_iID_DonViID]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qddautu_get_duan_By_iID_DonViID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qddautu_get_duan_By_iID_DonViID]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_detail]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_pheduyet_quyettoandaht_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_create]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_pheduyet_quyettoandaht_create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_create]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstnkehoachdathang_index]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_mstnkehoachdathang_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_mstnkehoachdathang_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstnkehoachdathang_bycondition]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_mstnkehoachdathang_bycondition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_mstnkehoachdathang_bycondition]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstn_khdh_delete_by_id]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_mstn_khdh_delete_by_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_mstn_khdh_delete_by_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khlcnhathau_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khlcnhathau_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hdnk_cacquyetdinh_index]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_hdnk_cacquyetdinh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_hdnk_cacquyetdinh_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_trongnuoc_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_trongnuoc_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_goithau_nguonvon_by_goithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_goithau_nguonvon_by_goithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_dutoan_in_khlcnt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_dutoan_in_khlcnt]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]    Script Date: 29/12/2022 4:07:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]
	-- Add the parameters for the stored procedure here
	 @iIDQuyetToan uniqueidentifier,
	 @devideDonViUSD float = null,
	 @devideDonViVND float = null
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
			,IIF(@devideDonViUSD is not null, round(daqd.fGiaTriUSD/@devideDonViUSD,2), daqd.fGiaTriUSD)  as FHopDongDuAnUsd
			,IIF(@devideDonViUSD is not null, round(daqd.fGiaTriVND/@devideDonViUSD,2), daqd.fGiaTriVND)  as FHopDongDuAnVnd
			,hd.fGiaTriUSD as FHopDongUsd
			,hd.fGiaTriVND  as FHopDongVnd
			, qtct.ID as Id
			, qtct.iID_ParentID as IIdParentId
			, qtct.iID_HopDongID as IIdHopDongId
			, IIF(@devideDonViUSD is not null, round(qtct.fKeHoach_TTCP_USD/@devideDonViUSD,2), qtct.fKeHoach_TTCP_USD) as FKeHoachTtcpUsd
			, IIF(@devideDonViVND is not null, round(qtct.fKeHoach_TTCP_VND/@devideDonViVND,2), qtct.fKeHoach_TTCP_VND) as FKeHoachTtcpVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fKeHoach_BQP_USD/@devideDonViUSD,2), qtct.fKeHoach_BQP_USD) as FKeHoachBqpUsd
			, IIF(@devideDonViUSD is not null, round(qtct.fKeHoach_BQP_VND/@devideDonViUSD,2), qtct.fKeHoach_BQP_VND) as FKeHoachBqpVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fQTKinhPhiDuyetCacNamTruoc_USD/@devideDonViUSD,2), qtct.fQTKinhPhiDuyetCacNamTruoc_USD) as FQtKinhPhiDuyetCacNamTruocUsd
			, IIF(@devideDonViVND is not null, round(qtct.fQTKinhPhiDuyetCacNamTruoc_VND/@devideDonViVND,2), qtct.fQTKinhPhiDuyetCacNamTruoc_VND) as FQtKinhPhiDuyetCacNamTruocVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fQTKinhPhiDuocCap_TongSo_USD/@devideDonViUSD,2), qtct.fQTKinhPhiDuocCap_TongSo_USD) as FQtKinhPhiDuocCapTongSoUsd
			, IIF(@devideDonViVND is not null, round(qtct.fQTKinhPhiDuocCap_TongSo_VND/@devideDonViVND,2), qtct.fQTKinhPhiDuocCap_TongSo_VND) as FQtKinhPhiDuocCapTongSoVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD/@devideDonViUSD,2), qtct.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD) as FQtKinhPhiDuocCapNamTruocChuyenSangUsd
			, IIF(@devideDonViVND is not null, round(qtct.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND/@devideDonViVND,2), qtct.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND) as FQtKinhPhiDuocCapNamTruocChuyenSangVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fQTKinhPhiDuocCap_NamNay_USD/@devideDonViUSD,2), qtct.fQTKinhPhiDuocCap_NamNay_USD) as FQtKinhPhiDuocCapNamNayUsd
			, IIF(@devideDonViVND is not null, round(qtct.fQTKinhPhiDuocCap_NamNay_VND/@devideDonViVND,2), qtct.fQTKinhPhiDuocCap_NamNay_VND) as FQtKinhPhiDuocCapNamNayVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fDeNghiQTNamNay_USD/@devideDonViUSD,2), qtct.fDeNghiQTNamNay_USD) as FDeNghiQtNamNayUsd
			, IIF(@devideDonViVND is not null, round(qtct.fDeNghiQTNamNay_VND/@devideDonViVND,2), qtct.fDeNghiQTNamNay_VND) as FDeNghiQtNamNayVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fDeNghiChuyenNamSau_USD/@devideDonViUSD,2), qtct.fDeNghiChuyenNamSau_USD) as FDeNghiChuyenNamSauUsd
			, IIF(@devideDonViVND is not null, round(qtct.fDeNghiChuyenNamSau_VND/@devideDonViVND,2), qtct.fDeNghiChuyenNamSau_VND) as FDeNghiChuyenNamSauVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fThuaThieuKinhPhiTrongNam_USD/@devideDonViUSD,2), qtct.fThuaThieuKinhPhiTrongNam_USD) as FThuaThieuKinhPhiTrongNamUsd
			, IIF(@devideDonViVND is not null, round(qtct.fThuaThieuKinhPhiTrongNam_VND/@devideDonViVND,2), qtct.fThuaThieuKinhPhiTrongNam_VND) as FThuaThieuKinhPhiTrongNamVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fThuaNopNSNN_USD/@devideDonViUSD,2), qtct.fThuaNopNSNN_USD) as FThuaNopNsnnUsd
			, IIF(@devideDonViVND is not null, round(qtct.fThuaNopNSNN_VND/@devideDonViVND,2), qtct.fThuaNopNSNN_VND) as FThuaNopNsnnVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fLuyKeKinhPhiDuocCap_USD/@devideDonViUSD,2), qtct.fLuyKeKinhPhiDuocCap_USD) as FLuyKeKinhPhiDuocCapUsd
			, IIF(@devideDonViVND is not null, round(qtct.fLuyKeKinhPhiDuocCap_VND/@devideDonViVND,2), qtct.fLuyKeKinhPhiDuocCap_VND) as FLuyKeKinhPhiDuocCapVnd
			, IIF(@devideDonViUSD is not null, round(qtct.fKeHoachChuaGiaiNgan_USD/@devideDonViUSD,2), qtct.fKeHoachChuaGiaiNgan_USD) as FKeHoachChuaGiaiNganUsd
			, IIF(@devideDonViVND is not null, round(qtct.fKeHoachChuaGiaiNgan_VND/@devideDonViVND,2), qtct.fKeHoachChuaGiaiNgan_VND) as FKeHoachChuaGiaiNganVnd
			, qtct.iID_KHTT_NhiemVuChiID as IIdKhttNhiemVuChiId
			, 1 as IsData
	from NH_QT_QuyetToanNienDo_ChiTiet qtct 
	left join NH_QT_QuyetToanNienDo qtnd on qtct.iID_QuyetToanNienDoID = qtnd.ID 
    left join NH_DM_NhiemVuChi dmnvc on qtct.iID_KHTT_NhiemVuChiID = dmnvc.ID
	left join NH_DA_DuAn da on  qtct.iID_DuAnId = da.ID 
	left join NH_DA_QDDauTu daqd on da.ID = daqd.iID_DuAnID AND daqd.bIsActive = 1
	left join DonVi dv on  qtnd.iID_DonViID = dv.iID_DonVi
	left join NH_DA_HopDong hd on qtct.iID_HopDongID = hd.ID
	left join NH_TT_ThanhToan_ChiTiet ttct on qtct.iID_ThanhToan_ChiTietID = ttct.ID
	left join NS_MucLucNganSach tb  on tb.iID = ttct.iID_MucLucNganSachID
	left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID
	where 
        qtct.iID_QuyetToanNienDoID = @iIDQuyetToan
END;

GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_paging]
	-- Add the parameters for the stored procedure here

  @iIDDonVi uniqueidentifier,
  @iNamKeHoach float,
  @devideDonViUSD float = null,
  @devideDonViVND float = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/* bang tam*/

select distinct  CAST(
             CASE 
                  WHEN qtct.iNamKeHoach>=gd.iGiaiDoanTu_BQP and qtct.iNamKeHoach<=gd.iGiaiDoanDen_BQP
                     THEN  qtct.fGiaTriUsd 
                  ELSE null
             END AS float) as fQTKinhPhiDuyetCacNamTruoc_USD,
			 CAST(
             CASE 
                  WHEN qtct.iNamKeHoach>=gd.iGiaiDoanTu_BQP and qtct.iNamKeHoach<=gd.iGiaiDoanDen_BQP
                     THEN qtct.fGiaTriVnd  
                  ELSE null
             END AS float) as fQTKinhPhiDuyetCacNamTruoc_VND
			 ,gd.iID_DonViThuHuongID,qtct.iID_DuAnId,qtct.iID_HopDongId,qtct.iID_KHTT_NhiemVuChiID
			  into #kpdnt
			 from  
			 (select distinct b.iGiaiDoanTu_BQP,b.iGiaiDoanDen_BQP,a.iID_DonViThuHuongID from NH_KHTongThe_NhiemVuChi a join NH_KHTongThe b on a.iID_KHTongTheID = b.ID  ) as gd 
			  join (

			 select sum(a.fGiaTriUsd) as fGiaTriUsd,sum(a.fGiaTriVnd) as fGiaTriVnd, a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId,a.iID_KHTT_NhiemVuChiID from NH_TH_TongHop a
             where  a.iNamKeHoach = @iNamKeHoach - 1 and a.bIsLog = 0 and a.iID_DonVi = @iIDDonVi and 
			 (a.sMaDich in ('311','321') and a.sMaNguon = '000' and a.sMaNguonCha in ('302','301') and a.sMaTienTrinh  in ('200','100'))
			 group by a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId,a.iID_KHTT_NhiemVuChiID

			 ) as qtct on 
			 gd.iID_DonViThuHuongID = qtct.iID_DonVi
			 where qtct.iNamKeHoach between gd.iGiaiDoanTu_BQP and gd.iGiaiDoanDen_BQP
				

    --get fQTKinhPhiDuocCap_NamTruocChuyenSang_USD

		--select (isnull(tbTong13.fGiaTriUsd,0) - isnull(tbTru2.fGiaTriUsdThuHoi,0)) as fQTKinhPhiDuocCap_NamTruocChuyenSang_USD, (isnull(tbTong13.fGiaTriVnd,0) - isnull(tbTru2.fGiaTriVndThuHoi,0)) as fQTKinhPhiDuocCap_NamTruocChuyenSang_VND  
		--, tbTong13.iID_DonVi,tbTong13.iID_DuAnId,tbTong13.iID_HopDongId,tbTong13.iNamKeHoach
  --          into #ntcs
		--	from (
		--	 select sum(isnull(a.fGiaTriUsd,0)) as fGiaTriUsd,sum(isnull(a.fGiaTriVnd,0)) as fGiaTriVnd,a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
  --           from NH_TH_TongHop a
  --           where  a.iNamKeHoach = @iNamKeHoach and a.bIsLog = 0 and   a.iID_DonVi = @iIDDonVi and
		--	 ((a.iCoQuanThanhToan = 1 and (a.sMaDich in ('112','122') or a.sMaNguon ='0' or a.sMaNguonCha = '102' and a.sMaTienTrinh  ='200'))
		--	 or
		--	 (a.iCoQuanThanhToan = 2 and (a.sMaDich = '0' or a.sMaNguon ='102' or a.sMaNguonCha is null and a.sMaTienTrinh  ='200')))
		--	 group by a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
		--	 ) as tbTong13 
		--	 left join (
		--	 select sum(isnull(a.fGiaTriUsd,0)) as fGiaTriUsdThuHoi,sum(isnull(a.fGiaTriVnd,0)) as fGiaTriVndThuHoi
		--	 ,a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
  --           from NH_TH_TongHop a
  --           where  a.iNamKeHoach = @iNamKeHoach and a.bIsLog = 0 and a.iCoQuanThanhToan = 1 and a.iID_DonVi = @iIDDonVi and 
		--	 a.sMaDich = '0' and a.sMaNguon = '122' and a.sMaNguonCha = null and a.sMaTienTrinh  ='200'
		--	 group by a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
		--	 ) as tbTru2 on tbTong13.iID_DonVi = tbTru2.iID_DonVi and tbTong13.iID_DuAnId = tbTru2.iID_DuAnId and tbTong13.iID_HopDongId = tbTru2.iID_HopDongId

	--get fQTKinhPhiDuocCap_NamNay_USD

select b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID, CAST(
				 CASE 
					  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
						 THEN a.fPheDuyetCapKyNay_USD
					  ELSE null
				 END AS float) as fQTKinhPhiDuocCap_NamNay_USD 
				 into #nnUSD
	from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
	where b.iNamNganSach = 1 and b.iNamKeHoach = @iNamKeHoach    

	--get fQTKinhPhiDuocCap_NamNay_VND


	select b.iLoaiDeNghi,b.iCoQuanThanhToan,a.ID,CAST(
				 CASE 
					  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
						 THEN a.fPheDuyetCapKyNay_VND
					  ELSE null
				 END AS float) as fQTKinhPhiDuocCap_NamNay_VND 
				 into #nnVND
				 from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
				 where b.iNamNganSach = 1 and b.iNamKeHoach = @iNamKeHoach      

	----------------------------
	select b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID, CAST(
             CASE 
                  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
                     THEN  a.fPheDuyetCapKyNay_USD
                  ELSE null
             END AS float) as fQTKinhPhiDuocCap_NamTruocChuyenSang_USD
			 into #ntcsUSD
from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
where b.iNamNganSach = 2 and b.iNamKeHoach = @iNamKeHoach    
--get fQTKinhPhiDuocCap_NamTruocChuyenSang_VND

select b.iLoaiDeNghi ,b.iCoQuanThanhToan,a.ID, CAST(
             CASE 
                  WHEN b.iLoaiDeNghi=1 and b.iCoQuanThanhToan = 2 or b.iLoaiDeNghi = 2 and  b.iCoQuanThanhToan = 1 or b.iLoaiDeNghi=3 and  b.iCoQuanThanhToan = 1
                     THEN a.fPheDuyetCapKyNay_VND
                  ELSE null
             END AS float) as fQTKinhPhiDuocCap_NamTruocChuyenSang_VND 
			 into #ntcsVND
from NH_TT_ThanhToan_ChiTiet a left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID
where b.iNamNganSach = 2 and b.iNamKeHoach = @iNamKeHoach  

 --select (isnull(tbTong13.fGiaTriUsd,0) - isnull(tbTru2.fGiaTriUsdThuHoi,0)) as fQTKinhPhiDuocCap_NamNay_USD, (isnull(tbTong13.fGiaTriVnd,0) - isnull(tbTru2.fGiaTriVndThuHoi,0)) as fQTKinhPhiDuocCap_NamNay_VND  
	--, tbTong13.iID_DonVi,tbTong13.iID_DuAnId,tbTong13.iID_HopDongId,tbTong13.iNamKeHoach
	--     into #ntnn
	--		from (
	--		select sum(isnull(a.fGiaTriUsd,0)) as fGiaTriUsd,sum(isnull(a.fGiaTriVnd,0)) as fGiaTriVnd,a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
 --            from NH_TH_TongHop a
 --            where  a.iNamKeHoach = @iNamKeHoach and a.bIsLog = 0 and   a.iID_DonVi = @iIDDonVi and
	--		 ((a.iCoQuanThanhToan = 1 and (a.sMaDich in ('111','121') or a.sMaNguon ='0' or a.sMaNguonCha = '101' and a.sMaTienTrinh  ='200'))
	--		 or
	--		 (a.iCoQuanThanhToan = 2 and (a.sMaDich = '0' or a.sMaNguon ='101' or a.sMaNguonCha is null and a.sMaTienTrinh  ='200')))
	--		 group by a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId) as tbTong13 
	--		 left join (
	--		 select sum(a.fGiaTriUsd) as fGiaTriUsdThuHoi,sum(a.fGiaTriVnd) as fGiaTriVndThuHoi
	--		 ,a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
 --            from NH_TH_TongHop a
 --            where  a.iNamKeHoach = @iNamKeHoach and a.bIsLog = 0 and a.iCoQuanThanhToan = 1 and a.iID_DonVi = @iIDDonVi and 
	--		 a.sMaDich = '0' and a.sMaNguon = '121' and a.sMaNguonCha = null and a.sMaTienTrinh  ='200'
	--		 group by a.iID_DonVi,a.iNamKeHoach,a.iID_DuAnId,a.iID_HopDongId
	--		 ) as tbTru2 on tbTong13.iID_DonVi = tbTru2.iID_DonVi and tbTong13.iID_DuAnId = tbTru2.iID_DuAnId and tbTong13.iID_HopDongId = tbTru2.iID_HopDongId


	--get fKuyKeKinhPhiDuocCap_USD
	 --select a.fLuyKeKinhPhiDuocCap_VND,a.fLuyKeKinhPhiDuocCap_USD,a.iID_DuAnID,a.iID_HopDongID,a.iID_KHTT_NhiemVuChiID,b.iNamKeHoach into #lknt from NH_QT_QuyetToanNienDo_ChiTiet a join NH_QT_QuyetToanNienDo b on a.iID_QuyetToanNienDoID = b.ID 
	 -- where b.iNamKeHoach = @iNamKeHoach - 1

	--   select a.fLuyKeKinhPhiDuocCap_VND,a.iID_ThanhToan_ChiTietID,a.fLuyKeKinhPhiDuocCap_USD,a.iID_DuAnID,a.iID_HopDongID,a.iID_KHTT_NhiemVuChiID,b.iNamKeHoach 
 --into #lknt from NH_QT_QuyetToanNienDo_ChiTiet a join NH_QT_QuyetToanNienDo b on a.iID_QuyetToanNienDoID = b.ID 
 -- where b.iNamKeHoach = @iNamKeHoach - 1 and a.iID_ParentID is null

      select a.fGiaTriUsd as fLuyKeKinhPhiDuocCap_USD,a.fGiaTriVnd as fLuyKeKinhPhiDuocCap_VND,a.iID_DuAnID,a.iID_HopDongID,a.iID_DonVi
	into #lknt 
	from NH_TH_TongHop a
  where a.sMaNguon = '303' 
  and a.iNamKeHoach = @iNamKeHoach - 1 and a.iID_DonVi = @iIDDonVi and a.bIsLog = 0
	/* finnal*/
	select distinct  tb.sLNS as SLNS, tb.sL as SL,tb.sK as SK,tb.sM as SM,tb.sTM as STM,tb.sTTM as STTM,dmnvc.sTenNhiemVuChi,da.sTenDuAn,dv.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi ,a.sTenNoiDungChi,b.iID_DuAnID ,khttnvc.iID_NhiemVuChiID as IIdKhttNhiemVuChiId,b.iID_HopDongID as IIdHopDongId, hd.sTenHopDong as STenHopDong,a.ID as IID_ThanhToan_ChiTietID,
             
		   IIF(@devideDonViUSD is not null, round(daqd.fGiaTriUSD/@devideDonViUSD,2), daqd.fGiaTriUSD) as FHopDongDuAnUsd,
		   IIF(@devideDonViVND is not null, round(daqd.fGiaTriVND/@devideDonViVND,2), daqd.fGiaTriVND) as FHopDongDuAnVnd,
		   IIF(@devideDonViUSD is not null, round(hd.fGiaTriUSD/@devideDonViUSD,2), hd.fGiaTriUSD) as FHopDongUsd,
		   IIF(@devideDonViVND is not null, round(hd.fGiaTriVND/@devideDonViVND,2), hd.fGiaTriVND) as FHopDongVnd,
		   IIF(@devideDonViUSD is not null, round(khttnvc.fGiaTriKH_TTCP/@devideDonViUSD,2), khttnvc.fGiaTriKH_TTCP) as FKeHoachTtcpUsd,
		   IIF(@devideDonViUSD is not null, round(khttnvc.fGiaTriKH_BQP/@devideDonViUSD,2), khttnvc.fGiaTriKH_BQP) as FKeHoachBqpUsd,
		   IIF(@devideDonViVND is not null, round(khttnvc.fGiaTriKH_BQP_VND/@devideDonViVND,2), khttnvc.fGiaTriKH_BQP_VND) as FKeHoachBqpVnd,
		   IIF(@devideDonViUSD is not null, round(gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_USD/@devideDonViUSD,2), gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_USD) as FQtKinhPhiDuyetCacNamTruocUsd,
		   IIF(@devideDonViVND is not null, round(gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_VND/@devideDonViVND,2), gdkpdnt.fQTKinhPhiDuyetCacNamTruoc_VND) as FQtKinhPhiDuyetCacNamTruocVnd,
		   IIF(@devideDonViUSD is not null, round((isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))/@devideDonViUSD,2)
		   , (isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))) as FQtKinhPhiDuocCapTongSoUsd,

		   IIF(@devideDonViVND is not null, round((isnull(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND,0))/@devideDonViVND,2)
		   , (isnull(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND,0))) as FQtKinhPhiDuocCapTongSoVnd,

		   IIF(@devideDonViUSD is not null, round(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD/@devideDonViUSD,2), ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD) as FQtKinhPhiDuocCapNamTruocChuyenSangUsd,
		   IIF(@devideDonViVND is not null, round(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND/@devideDonViVND,2), ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND) as FQtKinhPhiDuocCapNamTruocChuyenSangVnd,
		   IIF(@devideDonViUSD is not null, round(nnUSD.fQTKinhPhiDuocCap_NamNay_USD/@devideDonViUSD,2), nnUSD.fQTKinhPhiDuocCap_NamNay_USD) as FQtKinhPhiDuocCapNamNayUsd,
		   IIF(@devideDonViVND is not null, round(nnVND.fQTKinhPhiDuocCap_NamNay_VND/@devideDonViVND,2), nnVND.fQTKinhPhiDuocCap_NamNay_VND) as FQtKinhPhiDuocCapNamNayVnd,

		   IIF(@devideDonViUSD is not null, round((isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))/@devideDonViUSD,2)
		   , (isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))) as FThuaThieuKinhPhiTrongNamUsd,
		   IIF(@devideDonViVND is not null, round((isnull(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND,0))/@devideDonViVND,2)
		   , (isnull(ntcsVND.fQTKinhPhiDuocCap_NamTruocChuyenSang_VND,0) + isnull(nnVND.fQTKinhPhiDuocCap_NamNay_VND,0))) as FThuaThieuKinhPhiTrongNamVnd,

		   IIF(@devideDonViUSD is not null, round(isnull(lknt.fLuyKeKinhPhiDuocCap_USD, 0)/@devideDonViUSD,2) , isnull(lknt.fLuyKeKinhPhiDuocCap_USD, 0) ) as FLuyKeKinhPhiDuocCapUsd,
		   IIF(@devideDonViVND is not null, round(isnull(lknt.fLuyKeKinhPhiDuocCap_VND, 0)/@devideDonViVND,2) , isnull(lknt.fLuyKeKinhPhiDuocCap_VND, 0) ) as FLuyKeKinhPhiDuocCapVnd,
		   (IIF(@devideDonViUSD is not null, round(khttnvc.fGiaTriKH_BQP/@devideDonViUSD,2), khttnvc.fGiaTriKH_BQP)) 
		   - IIF(@devideDonViUSD is not null, round((isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) 
		   + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))/@devideDonViUSD,2)
		   , (isnull(ntcsUSD.fQTKinhPhiDuocCap_NamTruocChuyenSang_USD,0) + isnull(nnUSD.fQTKinhPhiDuocCap_NamNay_USD,0))) as FKeHoachChuaGiaiNganUsd
		   
		   ,b.iCoQuanThanhToan,b.iLoaiDeNghi,b.iThanhToanTheo,b.iLoaiNoiDungChi,b.iID_DonVi
		   ,a.iID_MucLucNganSachID as IIdMucLucNganSachId, 1 as IsData
	from NH_TT_ThanhToan_ChiTiet a 
	left join NH_TT_ThanhToan b on a.iID_DeNghiThanhToanID = b.ID 
	left join NH_DM_NhiemVuChi dmnvc on b.iID_NhiemVuChiID = dmnvc.ID
	left join NH_KHTongThe_NhiemVuChi khttnvc on khttnvc.iID_KHTongTheID = b.iID_KHTongTheID AND khttnvc.iID_NhiemVuChiID = dmnvc.ID
	left join NH_KHTongThe khtt on khttnvc.iID_KHTongTheID = khtt.ID 
	left join NS_MucLucNganSach tb  on tb.iID = a.iID_MucLucNganSachID 
	left join NH_DA_DuAn da on  b.iID_DuAnId = da.ID 
	left join NH_DA_QDDauTu daqd on da.ID = daqd.iID_DuAnID
	left join DonVi dv on  b.iID_DonVi = dv.iID_DonVi 
	left join NH_DA_HopDong hd on b.iID_HopDongID = hd.ID
	left join #kpdnt as gdkpdnt on (b.iID_DuAnID  is null and gdkpdnt.iID_DuAnId is null or b.iID_DuAnID= gdkpdnt.iID_DuAnId )  and (b.iID_HopDongID  is null and gdkpdnt.iID_HopDongId is null or b.iID_HopDongID= gdkpdnt.iID_HopDongId )   
	left join NH_QT_QuyetToanNienDo_ChiTiet qtct on khttnvc.ID = qtct.iID_KHTT_NhiemVuChiID 
	--left join #ntcs as ntcs on (b.iID_DonVi  is null and ntcs.iID_DonVi is null or b.iID_DonVi= ntcs.iID_DonVi )  and (b.iID_DuAnID  is null and ntcs.iID_DuAnID is null or b.iID_DuAnID= ntcs.iID_DuAnID ) and (b.iID_HopDongID  is null and ntcs.iID_HopDongID is null or b.iID_HopDongID= ntcs.iID_HopDongID ) 
 --   left join #ntnn as ntnn on (b.iID_DonVi  is null and ntnn.iID_DonVi is null or b.iID_DonVi= ntnn.iID_DonVi )  and (b.iID_DuAnID  is null and ntnn.iID_DuAnID is null or b.iID_DuAnID= ntnn.iID_DuAnID ) and (b.iID_HopDongID  is null and ntnn.iID_HopDongID is null or b.iID_HopDongID= ntnn.iID_HopDongID ) 
	left join #ntcsUSD as ntcsUSD on b.iLoaiDeNghi = ntcsUSD.iLoaiDeNghi and b.iCoQuanThanhToan = ntcsUSD.iCoQuanThanhToan and a.ID = ntcsUSD.ID
left join #ntcsVND as ntcsVND on b.iLoaiDeNghi = ntcsUSD.iLoaiDeNghi and b.iCoQuanThanhToan = ntcsUSD.iCoQuanThanhToan and a.ID = ntcsVND.ID
--left join #ntcs as ntcs on (b.iID_DonVi  is null and ntcs.iID_DonVi is null or b.iID_DonVi= ntcs.iID_DonVi )  and (b.iID_DuAnID  is null and ntcs.iID_DuAnID is null or b.iID_DuAnID= ntcs.iID_DuAnID ) and (b.iID_HopDongID  is null and ntcs.iID_HopDongID is null or b.iID_HopDongID= ntcs.iID_HopDongID ) 
left join #nnUSD as nnUSD on b.iLoaiDeNghi = nnUSD.iLoaiDeNghi and b.iCoQuanThanhToan = nnUSD.iCoQuanThanhToan and a.ID = nnUSD.ID
left join #nnVND as nnVND on b.iLoaiDeNghi = nnVND.iLoaiDeNghi and b.iCoQuanThanhToan = nnVND.iCoQuanThanhToan and a.ID = nnVND.ID
	left join #lknt as lknt on (b.iID_DonVi  is null and lknt.iID_DonVi is null or b.iID_DonVi= lknt.iID_DonVi )  and (b.iID_DuAnID  is null and lknt.iID_DuAnID is null or b.iID_DuAnID= lknt.iID_DuAnID ) and (b.iID_HopDongID  is null and lknt.iID_HopDongID is null or b.iID_HopDongID= lknt.iID_HopDongID )  
	where b.iNamKeHoach = @iNamKeHoach AND (@iIDDonVi is   null or b.iID_DonVi in (@iIDDonVi)) AND b.iTrangThai = 2
	/* finnal*/
	DROP TABLE #kpdnt
	--DROP TABLE #ntcs
	--DROP TABLE #ntnn
	DROP TABLE #ntcsUSD
	DROP TABLE #ntcsVND
	DROP TABLE #nnUSD
	DROP TABLE #nnVND
	DROP TABLE #lknt
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_in_khlcnt]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_in_khlcnt]
@iId uniqueidentifier,
@iIdDuAnId uniqueidentifier
AS
BEGIN
	 DECLARE @iExist int = (SELECT COUNT(*) FROM NH_DA_KHLCNhaThau WHERE Id = @iId)
	 IF(@iExist = 0)
	 BEGIN
		SELECT tbl.ID, tbl.iLoaiDuToan as IdLoaiDuToan,tbl.iLoaiDuToan,tbl.fGiaTriEUR,tbl.fGiaTriNgoaiTeKhac,tbl.fGiaTriUSD,tbl.fGiaTriVND,
		tbl.iID_DonViQuanLyID,tbl.iID_TiGiaID,tbl.iID_KHTT_NhiemVuChiID,tbl.sTenChuongTrinh,tbl.fTiGiaNhap,tbl.bIsActive,tbl.bIsGoc,
		tbl.bIsKhoa,tbl.bIsXoa,tbl.dNgayQuyetDinh,tbl.dNgaySua,tbl.dNgayTao,tbl.sNguoiTao,tbl.sNguoiSua,tbl.dNgayXoa,tbl.sNguoiXoa,
		tbl.iID_DuAnID,tbl.iID_DuToanGocID,tbl.iID_MaDonViQuanLy,tbl.iID_ParentID,tbl.iID_QDDauTuID,tbl.iID_TiGiaUSD_EURID,
		tbl.iID_TiGiaUSD_NgoaiTeKhacID,tbl.iID_TiGiaUSD_VNDID,tbl.iLanDieuChinh,tbl.iLoai,tbl.sMaNgoaiTeKhac,tbl.sMota,
		 Case When iLoaiDuToan = 1 then Concat(N'Dự toán mua sắm: ',sSoQuyetDinh)  
              When iLoaiDuToan = 2 then Concat(N'Dự toán đặt hàng: ',sSoQuyetDinh)  else sSoQuyetDinh end as sSoQuyetDinh
		FROM NH_DA_DuToan as tbl
		LEFT JOIN (SELECT DISTINCT iID_DuToanID FROM NH_DA_KHLCNhaThau WHERE bIsActive = 1 AND iID_DuToanID IS NOT NULL) as dt on tbl.ID = dt.iID_DuToanID
		WHERE tbl.iID_DuAnID = @iIdDuAnId AND dt.iID_DuToanID IS NULL and tbl.bIsActive=1
	 END
	 ELSE
	 BEGIN
		SELECT dt.*
		FROM NH_DA_KHLCNhaThau as tbl
		INNER JOIN NH_DA_DuToan as dt on tbl.iID_DuToanID = dt.ID
	 END
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_dutoan_index]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_dutoan_index] 
	@YearOfWork int,
	@ILoai int
AS
BEGIN
	
	WITH SoLieuDieuChinh AS (
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_DuToan ct 
		WHERE ct.iID_ParentId IS NOT NULL 
		UNION ALL 
		SELECT 
			ct.ID,
			ct.iID_ParentId
		FROM NH_DA_DuToan ct 
		JOIN SoLieuDieuChinh ctpr ON ct.iID_ParentId = ctpr.ID 
	  WHERE 
		ct.iID_ParentId IS NOT NULL
	), 
	SoLanDieuChinh AS (
		SELECT 
			sdc.Id, 
			sdc.iID_ParentId, 
			COUNT(sdc.Id) AS iSoLanDieuChinh 
		FROM SoLieuDieuChinh sdc 
		GROUP BY 
			sdc.iID_ParentId, 
			sdc.ID
	),
	ThongTinNguonVon AS (
		SELECT 
			nguonVon.iID_DuToanID AS iID_DuToanID, 
			SUM(nguonVon.fGiaTriNgoaiTeKhac) AS fGiaTriNgoaiTeKhac,
			SUM(nguonVon.fGiaTriUSD) AS fGiaTriUSD,
			SUM(nguonVon.fGiaTriVND) AS fGiaTriVND,
			SUM(nguonVon.fGiaTriEUR) AS fGiaTriEUR
		FROM NH_DA_DuToan_NguonVon nguonVon
		GROUP BY 
			nguonVon.iID_DuToanID
	)
	
	SELECT
		duToan.ID AS Id,
		duToan.iID_QDDauTuID AS IIdQdDauTuId,
		duToan.iID_DuAnID AS IIdDuAnId,
		duToan.sSoQuyetDinh AS SSoQuyetDinh,
		duToan.dNgayQuyetDinh AS DNgayQuyetDinh,
		duToan.sMoTa AS SMoTa,
		duToan.sTenChuongTrinh AS STenChuongTrinh,
		duToan.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndId,
		duToan.iID_TiGiaUSD_EURID AS IIdTiGiaUsdEurId,
		duToan.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		duToan.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		duToan.fGiaTriUSD AS FGiaTriUsd,
		duToan.fGiaTriVND AS FGiaTriVnd,
		duToan.fGiaTriEUR AS FGiaTriEur,
		duToan.dNgayTao AS DNgayTao,
		duToan.sNguoiTao AS SNguoiTao,
		duToan.dNgaySua AS DNgaySua,
		duToan.sNguoiSua AS SNguoiSua,
		duToan.dNgayXoa AS DNgayXoa,
		duToan.sNguoiXoa AS SNguoiXoa,
		duToan.bIsActive AS BIsActive,
		duToan.bIsGoc AS BIsGoc,
		duToan.bIsKhoa AS BIsKhoa,
		duToan.bIsXoa AS BIsXoa,
		duToan.iID_DuToanGocID AS IIdDuToanGocId,
		duToan.iID_TiGiaID AS IIdTiGiaId,
		duToan.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		duToan.iID_ParentID AS IIdParentId,
		duToan.iLoaiDuToan AS IdLoaiDuToan,
		duToan.fTiGiaNhap AS FTiGiaNhap,
		--donvi.sTenDonVi AS STenDonVi, 
		CONCAT(donvi.iID_MaDonVi, ' - ', donvi.sTenDonVi) AS STenDonVi,
		donvi.iID_MaDonVi AS IIdMaDonViQuanLy,
		CONCAT(duAn.sMaDuAn, ' - ', duAn.sTenDuAn) AS STenDuAn,
		ISNULL(soLieuDieuChinh.iSoLanDieuChinh, 0) AS ILanDieuChinh,
		duToanParent.sSoQuyetDinh AS SDieuChinhTu,
		(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 406 AND ObjectId = duToan.ID) AS TotalFiles
	FROM NH_DA_DuToan duToan		
	LEFT JOIN NH_DA_DuToan duToanParent
		ON duToan.iID_ParentID = duToanParent.ID
	LEFT JOIN DonVi donVi
		ON duToan.iID_MaDonViQuanLy = donVi.iID_MaDonVi AND donVi.iNamLamViec = @YearOfWork
	LEFT JOIN NH_DA_DuAn duAn
		ON duToan.iID_DuAnID = duAn.ID
	LEFT JOIN SoLanDieuChinh soLieuDieuChinh
		ON soLieuDieuChinh.ID = duToan.ID
	LEFT JOIN ThongTinNguonVon nguonVon
		ON nguonVon.iID_DuToanID = duToan.ID
	WHERE duToan.iLoai = @ILoai 
	ORDER BY dNgayQuyetDinh DESC, sSoQuyetDinh DESC
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau]
@IdGoiThau uniqueidentifier
AS
BEGIN
	select hd.ID as Id,
	hd.sSoHopDong as SSoHopDong,
	hd.dNgayHopDong as DNgayHopDong,
	hd.iID_LoaiHopDongID as IID_LoaiHopDongID,
	gtnt.iID_NhaThauID as IID_NhaThauID,
	gtntt.fGiaTRiHopDong_USD AS FGiaTriUsd,
	gtntt.fGiaTRiHopDong_VND AS FGiaTriVnd,
	lhd.sTenLoaiHopDong AS STenLoaiHopDong,
	nt.sTenNhaThau as STenNhaThau,
	gtntt.fGiaTRiHopDong_EUR AS FGiaTriEur,
	gtntt.fGiaTriHopDong_NgoaiTeKhac AS FGiaTriNgoaiTeKhac
	FROM NH_DA_HopDong hd
	INNER JOIN NH_DA_HopDong_GoiThau_NhaThau gtnt on gtnt.iID_HopDongID=hd.ID 
	INNER JOIN nh_da_goithau gt on gt.iID_GoiThauID = gtnt.iID_GoiThauID
	LEFT JOIN (select iID_HopDongID, sum(fGiaTRiHopDong_USD) as fGiaTRiHopDong_USD
				, sum(fGiaTRiHopDong_VND) as fGiaTRiHopDong_VND
				, sum(fGiaTRiHopDong_EUR) as fGiaTRiHopDong_EUR
				, sum(fGiaTriHopDong_NgoaiTeKhac) as fGiaTriHopDong_NgoaiTeKhac
				from NH_DA_HopDong_GoiThau_NhaThau 
				where NH_DA_HopDong_GoiThau_NhaThau.isCheck=1
				group by iID_HopDongID
	      ) gtntt on hd.ID = gtntt.iID_HopDongID
	INNER JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
	INNER JOIN NH_DM_NhaThau nt  on hd.iID_NhaThauThucHienID= nt.Id
	WHERE gt.iID_GoiThauID = @idGoiThau
	order by hd.sTenHopDong
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_nguonvon_by_goithau]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_goithau_nguonvon_by_goithau]
@iIdKhlcnt uniqueidentifier
AS
BEGIN
	SELECT dt.iID_NguonVonID as IIdNguonVonID, nv.sTen as STenNguonVon, 
		ISNULL(dt.fTienGoiThau_NgoaiTeKhac, 0) as FGiaTriNgoaiTeKhac,
		ISNULL(dt.fTienGoiThau_USD, 0) as FGiaTriUSD,
		ISNULL(dt.fTienGoiThau_VND, 0) as FGiaTriVND,
		ISNULL(dt.fTienGoiThau_EUR, 0) as FGiaTriEUR,
		dt.iID_GoiThauID as IIdGoiThauID
	FROM NH_DA_GoiThau as tbl
	INNER JOIN NH_DA_GoiThau_NguonVon as dt on tbl.iID_GoiThauID = dt.iID_GoiThauID
	INNER JOIN NguonNganSach as nv on dt.iID_NguonVonID = nv.iID_MaNguonNganSach
	WHERE tbl.iID_GoiThauID = @iIdKhlcnt
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_goithau_trongnuoc_index]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_nh_goithau_trongnuoc_index]
	@ILoai int,
	@IThuocMenu int
as begin
	Select DISTINCT
		goiThau.iID_GoiThauID as Id,
		goiThau.iID_GoiThauGocID as IIdGoiThauGocId,
		goiThau.iID_ParentID as IIdParentId,
		goiThau.iID_NhaThauID as IIdNhaThauId,
		case when goiThau.iCheckLuong is null then khlcnt.iID_DuToanID else   goiThau.iID_DuToanID  end  as IIdDuToanId,
		goiThau.iID_CacQuyetDinhID as IIdCacQuyetDinhId,
		goiThau.iID_ParentAdjustID as IIdParentAdjustId,
		goiThau.iId_KHLCNhaThau as IIdKhlcnhaThau,
		goiThau.iID_TiGiaUSD_NgoaiTeKhacID as IIdTiGiaUSDNgoaiTeKhacId,
		goiThau.iID_TiGiaUSD_VNDID as IIdTiGiaUSDVNDId,
		goiThau.iID_TiGiaUSD_EURID as IIdTiGiaUSDEURId,
		goiThau.fTiGiaNhap as FTiGiaNhap,
		goiThau.iID_HinhThucChonNhaThauID as IIdHinhThucChonNhaThauId,
		goiThau.iID_PhuongThucDauThauID as IIdPhuongThucDauThauId,
		goiThau.iID_LoaiHopDongID as IIdLoaiHopDongId,
		--DuAn.ID as IIdDuAnId,
		DuAn.ID as IIdDuAnId,
		goiThau.sSoQuyetDinh as SSoQuyetDinh,
		case when   goiThau.iCheckLuong is null then LCNhaThau.iID_DonViQuanLyID else  goiThau.iID_DonViQuanLyID  end  as IID_DonViQuanLyID,
		case when   goiThau.iCheckLuong is null then nvc.ID else  goiThau.iID_KHTT_NhiemVuChiID  end  as IID_KHTT_NhiemVuChiID,
		case when goiThau.iCheckLuong is null then LCNhaThau.dNgayQuyetDinh else goiThau.dNgayQuyetDinh end as DNgayQuyetDinh, --KhaiPD update 13/10/2022
		goiThau.sMaGoiThau as SMaGoiThau,
		goiThau.sTenGoiThau as STenGoiThau,
		goiThau.LoaiGoiThau as LoaiGoiThau,
		goiThau.dBatDauChonNhaThau as DBatDauChonNhaThau,
		goiThau.dKetThucChonNhaThau as DKetThucChonNhaThau,
		goiThau.iThoiGianThucHien as IThoiGianThucHien,
		goiThau.fGiaGoiThauEUR as FGiaGoiThauEUR,
		goiThau.fGiaGoiThauUSD as FGiaGoiThauUSD,
		goiThau.fGiaGoiThauVND as FGiaGoiThauVND,
		goiThau.fGiaGoiThauNgoaiTeKhac as fGiaGoiThauNgoaiTeKhac,
		goiThau.bIsGoc as BIsGoc,
		goiThau.sSoKeHoachDatHang as SSoKeHoachDatHang,
		goiThau.dNgayKeHoach as DNgayKeHoach,
		goiThau.iCheckLuong as IcheckLuong,
		goiThau.bActive as BActive,
		goiThau.iLanDieuChinh as ILanDieuChinh,
		goiThau.bIsKhoa as BIsKhoa,
		goiThau.dNgayTao as DNgayTao,
		goiThau.sNguoiTao as SNguoiTao,
		goiThau.sNguoiSua as SNguoiSua,
		goiThau.dNgaySua as DNgaySua,
		goiThau.dNgayXoa as DNgayXoa,
		goiThau.sNguoiXoa as SNguoiXoa,
		 case when goiThau.iCheckLuong is null then khlcnt.iID_TiGiaID else   goiThau.iID_TiGiaID  end  as IIdTiGiaId,
		 case when goiThau.iCheckLuong is null then khlcnt.sMaNgoaiTeKhac else   goiThau.sMaNgoaiTeKhac  end  as SMaNgoaiTeKhac,
		goiThau.bIsXoa as BIsXoa,
		 concat(DonVi.iID_MaDonVi,' -', DonVi.sTenDonVi )as TenDonVi,
		nvc.sTenNhiemVuChi as STenNhiemVuChi,
		DuAn.sTenDuAn as STenDuAn,
		HinhThucChonNhaThau.sTenHinhThucChonNhaThau as STenHinhThucChonNhaThau,
		PhuongThucChonNhaThau.sTenPhuongThucChonNhaThau as STenPhuongThucChonNhaThau,
		ChuDauTu.sTenDonVi as STenChuDauTu,
		DuAn.sDiaDiem as SDiaDiem,
		QDDauTu.fGiaTriUSD as FQDDTTongPheDuyetUSD,
		QDDauTu.fGiaTriVND as FQDDTTongPheDuyetVND,
		QDDauTu.fGiaTriEUR as FQDDTTongPheDuyetEUR,
		QDDauTu.fGiaTriNgoaiTeKhac as FQDDTTongPheDuyetNgoaiTeKhac,
		LoaiHopDong.sTenLoaiHopDong as STenHopDong,
		DuToan.fGiaTriUSD as FDTTongPheDuyetUSD,
		DuToan.fGiaTriVND as FDTTongPheDuyetVND,
		DuToan.fGiaTriEUR as FDTTongPheDuyetEUR,
		DuToan.fGiaTriNgoaiTeKhac as FDTTongPheDuyetNgoaiTeKhac,
		khttnvc.ID as IIdKHTTNhiemVuChiId,  
		( SELECT COUNT ( Id ) FROM Attachment WHERE ModuleType = 405 AND ObjectId = goiThau.iID_GoiThauID ) AS TotalFiles,
		CASE
		WHEN goiThau.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sMaGoiThau FROM NH_DA_GoiThau hdpr WHERE hdpr.iID_GoiThauID = goiThau.iID_ParentAdjustId ) 
		END DieuChinhTu ,
		LCNhaThau.sMoTa as SMota
		from  NH_DA_GoiThau goiThau
	left join NH_DA_KHLCNhaThau LCNhaThau
		on LCNhaThau.Id = GoiThau.iId_KHLCNhaThau
	left join NH_DA_DuAn DuAn
		on goiThau.iID_DuAnID = DuAn.ID
	left join DonVi on (LCNhaThau.iID_DonViQuanLyID = DonVi.iID_DonVi AND goiThau.iCheckLuong is null) OR (goiThau.iID_DonViQuanLyID = DonVi.iID_DonVi  AND goiThau.iCheckLuong = 1)
	left join DM_ChuDauTu ChuDauTu
		on DuAn.iID_ChuDauTuID = ChuDauTu.iID_DonVi
	left join NH_KHTongThe_NhiemVuChi khttnvc 
		on  (LCNhaThau.iID_KHTT_NhiemVuChiID = khttnvc.ID  AND goiThau.iCheckLuong is null) OR (goiThau.iID_KHTT_NhiemVuChiID = khttnvc.ID AND goiThau.iCheckLuong = 1) 
	left join NH_DM_NhiemVuChi nvc
		--on (khttnvc.iID_NhiemVuChiID = nvc.ID  AND goiThau.iCheckLuong is null) OR (goiThau.iID_KHTT_NhiemVuChiID = nvc.ID AND goiThau.iCheckLuong = 1) 
		on khttnvc.iID_NhiemVuChiID = nvc.ID
	left join NH_DA_QDDauTu QDDauTu
		on LCNhaThau.iID_QDDauTuID = QDDauTu.ID
	left join NH_DA_DuToan DuToan
		on LCNhaThau.iID_DuToanID = DuToan.ID
	left join NH_DM_HinhThucChonNhaThau HinhThucChonNhaThau
		on goiThau.iID_HinhThucChonNhaThauID = HinhThucChonNhaThau.ID 
	left join NH_DM_PhuongThucChonNhaThau PhuongThucChonNhaThau
		on goiThau.iID_PhuongThucDauThauID = PhuongThucChonNhaThau.ID 
	left join NH_DM_LoaiHopDong LoaiHopDong
		on goiThau.iID_LoaiHopDongID = LoaiHopDong.iID_LoaiHopDongID 
	left join NH_DA_KHLCNhaThau khlcnt
		on khlcnt.Id=goiThau.iId_KHLCNhaThau
	WHERE goiThau.iLoai = @ILoai and goiThau.iThuocMenu = @IThuocMenu
	 ORDER BY goiThau.dNgayTao DESC
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_hdnk_cacquyetdinh_index]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_hdnk_cacquyetdinh_index] 
@iLoai int = NULL
AS 
	BEGIN 
	SELECT
	cqd.ID AS Id,
		cqd.sSoQuyetDinh AS SSoQuyetDinh,
		cqd.dNgayQuyetDinh AS DNgayQuyetDinh,
		cqd.sMoTaChiTiet_QuyetDinh AS SMoTaChiTietQuyetDinh,
		cqd.iID_TiGiaID AS IIdTiGiaId,
		cqd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
		cqd.iID_DuAnID AS IIdDuAnId,
		cqd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		cqd.iLoaiQuyetDinh AS ILoaiQuyetDinh,
		cqd.iID_DonViThucHien AS IIdDonViThucHien,
		cqd.iID_DonViQuanLy AS IIdDonViQuanLy,
		cqd.fGiaTriUSD AS FGiaTriUSD,
		cqd.fGiaTriVND AS FGiaTriVND,
		cqd.fGiaTriEUR AS FGiaTriEUR,
		cqd.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		cqd.iID_GocID AS IIdGocId,
		cqd.dNgayTao AS DNgayTao,
		cqd.sNguoiTao AS SNguoiTao,
		cqd.dNgaySua AS DNgaySua,
		cqd.sNguoiSua AS SNguoiSua,
		cqd.dNgayXoa AS DNgayXoa,
		cqd.sNguoiXoa AS SNguoiXoa,
		cqd.bIsActive AS BIsActive,
		cqd.bIsGoc AS BIsGoc,
		cqd.bIsKhoa AS BIsKhoa,
		cqd.iLanDieuChinh AS ILanDieuChinh,
		cqd.bIsXoa AS BIsXoa,
		cqd.iID_ParentId AS IIdParentId,
		cqd.iID_ParentAdjustId AS IIdParentAdjustId,
		cqd.iLoai AS ILoai,
		cqd.fTiGiaNhap AS FTiGiaNhap,
		CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
		nvChi.STenChuongTrinh,
		nvChi.iID_KHTongTheID AS IIdKhTongTheId,
		cqd.iID_PhuongAnNhapKhauID AS IIdPhuongAnNhapKhauId,
		CASE
			WHEN paNhapKhau.sMoTa IS NULL THEN paNhapKhau.sSoQuyetDinh
			ELSE CONCAT(paNhapKhau.sSoQuyetDinh, ' - ', paNhapKhau.sMoTa)
		END SPhuongAnNhapKhau,
		CASE
			WHEN cqd.iID_ParentAdjustId IS NULL THEN
			'' ELSE ( SELECT TOP 1 cqdpr.sSoQuyetDinh FROM NH_HDNK_CacQuyetDinh cqdpr WHERE cqdpr.Id = cqd.iID_ParentAdjustId ) 
		END DieuChinhTu
FROM
	NH_HDNK_CacQuyetDinh cqd
LEFT JOIN DonVi donVi
ON cqd.iID_DonViQuanLy = donVi.iID_DonVi AND cqd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON cqd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
LEFT JOIN NH_HDNK_PhuongAnNhapKhau paNhapKhau
ON cqd.iID_PhuongAnNhapKhauID = paNhapKhau.ID
WHERE @iLoai IS NULL OR cqd.iLoai = @iLoai
ORDER BY
	cqd.dNgayTao DESC END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index]
	@iThuocMenu int
AS
BEGIN
	SELECT
		tbl.Id, tbl.sSoQuyetDinh AS SSoQuyetDinh, tbl.dNgayQuyetDinh AS DNgayQuyetDinh, tbl.sMoTa AS SMoTa, da.iID_DonViQuanLyID as IIdDonViQuanLy, da.iID_MaDonViQuanLy as SMaDonViQuanLy, CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS STenDonVi,tbl.fTiGiaNhap as FTiGiaNhap,
		tbl.iID_DuAnID as IIdDuAnID, da.STenDuAn, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu, tbl.iID_DonViQuanLyID as IIdDonViQuanLyId ,
		tbl.iID_MaDonViQuanLy as IidMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId
	FROM NH_DA_KHLCNhaThau as tbl
	INNER JOIN NH_DA_DuAn as da on tbl.iID_DuAnID = da.ID
	LEFT JOIN DonVi as dv on da.iID_DonViQuanLyID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	WHERE tbl.iThuocMenu = @iThuocMenu
	ORDER BY tbl.dNgayTao DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khlcnhathau_index_2]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khlcnhathau_index_2]
AS
BEGIN
	SELECT tbl.Id, tbl.sSoQuyetDinh AS SSoQuyetDinh, tbl.dNgayQuyetDinh AS DNgayQuyetDinh, tbl.sMoTa AS SMoTa, nvc.iID_DonViThuHuongID as IIdDonViQuanLy, nvc.iID_MaDonViThuHuong as SMaDonViQuanLy,CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS STenDonVi, tbl.fTiGiaNhap as FTiGiaNhap,
		tbl.iID_DuAnID as IIdDuAnID, tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID , pr.sSoQuyetDinh as SSoQuyetDinhParent,dutoan.sTenChuongTrinh as STenChuongTrinh,
		tbl.BIsKhoa, tbl.BIsActive, CAST(0 as int) as ITepDinhKem, tbl.SNguoiTao, tbl.iID_QDDauTuID as IIdQDDauTuID, tbl.iID_DuToanID as IIdDuToanID, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.iLoaiKHLCNT, tbl.iLoai, tbl.iThuocMenu, tbl.iID_DonViQuanLyID as IIdDonViQuanLyId ,
		tbl.iID_MaDonViQuanLy as IidMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId, tbl.fTiGiaNhap as FTiGiaNhap
	FROM NH_DA_KHLCNhaThau as tbl
	left join NH_KHTongThe_NhiemVuChi as nvc on tbl.iID_KHTT_NhiemVuChiID = nvc.ID  
	LEFT JOIN DonVi as dv on nvc.iID_DonViThuHuongID = dv.iID_DonVi
	LEFT JOIN NH_DA_KHLCNhaThau as pr on tbl.iID_ParentID = pr.Id
	LEFT JOIN NH_DA_DuToan as dutoan on tbl.iID_DuToanID = dutoan.ID
	--WHERE tbl.bIsActive=1
	ORDER BY tbl.dNgayTao DESC
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstn_khdh_delete_by_id]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[sp_nh_mstn_khdh_delete_by_id]
@iId uniqueidentifier
AS
BEGIN
	DELETE NH_MSTN_KeHoachDatHang_DanhMuc WHERE iID_KeHoachDatHang = @iId;
	DELETE NH_MSTN_KeHoachDatHang WHERE ID = @iId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstnkehoachdathang_bycondition]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_mstnkehoachdathang_bycondition]
	@DonviId uniqueidentifier,
	@KeHoachTongTheId uniqueidentifier,
	@ChuongTrinhId uniqueidentifier
AS
BEGIN
	SELECT tbl.Id, tbl.SSoQuyetDinh, tbl.DNgayQuyetDinh, tbl.SMoTa, tbl.iID_DonViID as IIdDonViQuanLy, dv.sTenDonVi,
		tbl.ILanDieuChinh, tbl.iID_ParentID as IIdParentID,
		tbl.BIsActive, tbl.SNguoiTao, 
		tbl.iID_TiGiaID as IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.fTiGiaNhap AS FTiGiaNhap,
		tbl.fGiaTriEUR as FGiaTriEur, tbl.fGiaTriUSD as FGiaTriUsd, tbl.fGiaTriVND as FGiaTriVnd, tbl.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac,
		tbl.iID_MaDonVi as SMaDonViQuanLy,
		tbl.iID_KHTT_NhiemVuChiID as IIdKHTTNhiemVuChiId,
		dmnvc.sTenNhiemVuChi as STenChuongTrinh,
		nvc.iID_KHTongTheID as IIdKhtongTheId
	FROM NH_MSTN_KeHoachDatHang as tbl
	LEFT JOIN DonVi as dv on tbl.iID_DonViID = dv.iID_DonVi
	LEFT JOIN NH_KHTongThe_NhiemVuChi as nvc on tbl.iID_KHTT_NhiemVuChiID = nvc.ID  
	LEFT JOIN NH_DM_NhiemVuChi as dmnvc on nvc.iID_NhiemVuChiID = dmnvc.ID
	WHERE tbl.bIsActive=1
		AND (@DonviId IS NULL OR tbl.iID_DonViID = @DonviId)
		AND (@KeHoachTongTheId IS NULL OR nvc.iID_KHTongTheID = @KeHoachTongTheId)
		AND (@ChuongTrinhId IS NULL OR tbl.iID_KHTT_NhiemVuChiID = @ChuongTrinhId)
	ORDER BY tbl.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_mstnkehoachdathang_index]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_mstnkehoachdathang_index]
AS
BEGIN
	SELECT tbl.Id, tbl.SSoQuyetDinh, tbl.DNgayQuyetDinh, tbl.SMoTa,
		tbl.iID_DonViID AS IIdDonViQuanLy, CONCAT(dv.iID_MaDonVi, ' - ', dv.sTenDonVi) AS STenDonVi,
		tbl.ILanDieuChinh, tbl.iID_ParentID AS IIdParentID , pr.sSoQuyetDinh AS SSoQuyetDinhParent,
		tbl.BIsActive, tbl.SNguoiTao, 
		tbl.iID_TiGiaID AS IIdTiGiaID, tbl.sMaNgoaiTeKhac, tbl.fTiGiaNhap AS FTiGiaNhap,
		tbl.fGiaTriEUR AS FGiaTriEur, tbl.fGiaTriUSD AS FGiaTriUsd, tbl.fGiaTriVND AS FGiaTriVnd, tbl.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
		tbl.iID_MaDonVi AS SMaDonViQuanLy, tbl.iID_KHTT_NhiemVuChiID AS IIdKHTTNhiemVuChiId,
		dmnvc.sTenNhiemVuChi AS STenChuongTrinh, nvc.iID_KHTongTheID AS IIdKhtongTheId,
		CONCAT(N'KHTT ', khtt.iGiaiDoanTu_BQP, '-', khtt.iGiaiDoanDen_BQP, N' - Số KH: ', khtt.sSoKeHoachBQP) AS STenKeHoachTongThe
	FROM NH_MSTN_KeHoachDatHang AS tbl
	LEFT JOIN DonVi AS dv ON tbl.iID_DonViID = dv.iID_DonVi
	LEFT JOIN NH_MSTN_KeHoachDatHang AS pr ON tbl.iID_ParentID = pr.Id
	LEFT JOIN NH_KHTongThe_NhiemVuChi AS nvc ON tbl.iID_KHTT_NhiemVuChiID = nvc.ID
	LEFT JOIN NH_KHTongThe AS khtt ON nvc.iID_KHTongTheID = khtt.ID
	LEFT JOIN NH_DM_NhiemVuChi AS dmnvc ON nvc.iID_NhiemVuChiID = dmnvc.ID  
	ORDER BY tbl.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_create]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_create]
	-- Add the parameters for the stored procedure here
      @iIDDonVi uniqueidentifier,
	  @iNamBaoCaoTu int ,
	  @iNamBaoCaoDen int ,
	  @devideDonViUSD int = null,
      @devideDonViVND int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @fromDate datetime = cast(concat(CONVERT(varchar(10), @iNamBaoCaoTu),'-01-01 00:00:00.000') as datetime);
	declare @toDate datetime = cast(concat(CONVERT(varchar(10), @iNamBaoCaoDen),'-01-01 00:00:00.000') as datetime);

    -- Insert statements for procedure here
		--bang tam
select  a.iID_ThanhToan_ChiTietID,b.iNamKeHoach,sum(a.fQTKinhPhiDuocCap_TongSo_USD) as fQTKinhPhiDuocCap_TongSo_USD,sum(fQTKinhPhiDuocCap_TongSo_VND) as fQTKinhPhiDuocCap_TongSo_VND,b.iID_DonViID
into #tmpkpdc 
from NH_QT_QuyetToanNienDo_ChiTiet a 
left join NH_QT_QuyetToanNienDo b on a.iID_QuyetToanNienDoID = b.ID
where b.iNamKeHoach between @iNamBaoCaoTu and @iNamBaoCaoDen and b.iID_DonViID = @iIDDonVi
group by b.iNamKeHoach,b.iID_DonViID, a.iID_ThanhToan_ChiTietID

--
select a.iID_ThanhToan_ChiTietID,(isnull(a.fQTKinhPhiDuyetCacNamTruoc_USD,0) + isnull(a.fDeNghiQTNamNay_USD,0)) as fQuyetToanDuocDuyet_Tong_USD,
(isnull(a.fQTKinhPhiDuyetCacNamTruoc_VND,0) + isnull(a.fDeNghiQTNamNay_VND,0)) as fQuyetToanDuocDuyet_Tong_VND 
into #qtdd
from NH_QT_QuyetToanNienDo_ChiTiet a left join NH_QT_QuyetToanNienDo b on a.iID_QuyetToanNienDoID = b.ID
where b.iNamKeHoach between @iNamBaoCaoTu and @iNamBaoCaoDen and a.iID_ParentID is null and b.iID_DonViID = @iIDDonVi 

    -- Insert statements for procedure here
	select distinct 
	ttct.sTenNoiDungChi as STenNoiDungChi
	,tt.iID_DonVi as IIDDonViId
	,dv.iID_MaDonVi + ' - '+dv.sTenDonVi as sTenDonVi
	,da.sTenDuAn as STenDuAn
	,hd.sTenHopDong as STenHopDong	
	,nvc.sTenNhiemVuChi as STenNhiemVuChi
	,tt.iLoaiNoiDungChi as ILoaiNoiDungChi
	,tt.iID_NhiemVuChiID as IIDKHTTNhiemVuChiId

	,ttct.ID  as IIDThanhToanChiTietId
	,tt.iID_DuAnID as IIDDuAnId
	,tt.iID_HopDongID as IIDHopDongId
	 ,sum(IIF(@devideDonViUSD is not null, round(da.fUSD/@devideDonViUSD,2), da.fUSD))  as FHopDongUsdDuAn
	   ,sum(IIF(@devideDonViVND is not null, round(da.fVND/@devideDonViVND,2), da.fVND))  as FHopDongVndDuAn
	   ,sum(IIF(@devideDonViUSD is not null, round(hd.fGiaTriUSD/@devideDonViUSD,2), hd.fGiaTriUSD))  as FHopDongUsdHopDong
	   ,sum(IIF(@devideDonViVND is not null, round(hd.fGiaTriVND/@devideDonViVND,2), hd.fGiaTriVND))  as FHopDongVndHopDong
	   ,tongThe.iGiaiDoanDen as INamBaoCaoDen
	   ,tongThe.iGiaiDoanTu as INamBaoCaoTu
	   ,IIF(@devideDonViUSD is not null, round(sum(tongTheNvc.fGiaTriKH_TTCP)/@devideDonViUSD,2), sum(tongTheNvc.fGiaTriKH_TTCP)) as FKeHoachTTCPUsd
	   ,IIF(@devideDonViUSD is not null,round(sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_USD)/@devideDonViUSD,2), sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_USD)) as FKinhPhiDuocCapTongUsd
	   ,IIF(@devideDonViVND is not null, round(sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_VND)/@devideDonViVND,2), sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_VND)) as FKinhPhiDuocCapTongVnd
	   ,IIF(@devideDonViUSD is not null, round(sum(isnull(qtdd.fQuyetToanDuocDuyet_Tong_USD,0))/@devideDonViUSD,2), sum(isnull(qtdd.fQuyetToanDuocDuyet_Tong_USD,0))) as FQuyetToanDuocDuyetTongUsd
	   ,IIF(@devideDonViVND is not null, round(sum(isnull(qtdd.fQuyetToanDuocDuyet_Tong_VND,0))/@devideDonViVND,2), sum(isnull(qtdd.fQuyetToanDuocDuyet_Tong_VND,0))) as FQuyetToanDuocDuyetTongVnd

	   --,IIF(@devideDonViUSD is not null, round(sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_USD)-sum(isnull(qtndct.fQTKinhPhiDuyetCacNamTruoc_USD,0)+isnull(qtndct.fDeNghiQTNamNay_USD,0))/@devideDonViUSD,2), sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_USD)-sum(isnull(qtndct.fQTKinhPhiDuyetCacNamTruoc_USD,0)+isnull(qtndct.fDeNghiQTNamNay_USD,0))) as FSoSanhKinhPhiUsd
	   --,IIF(@devideDonViVND is not null, round(sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_VND)-sum(isnull(qtndct.fQTKinhPhiDuyetCacNamTruoc_VND,0)+isnull(qtndct.fDeNghiQTNamNay_VND,0))/@devideDonViVND,2), sum(tmpkpdc.fQTKinhPhiDuocCap_TongSo_VND)-sum(isnull(qtndct.fQTKinhPhiDuyetCacNamTruoc_VND,0)+isnull(qtndct.fDeNghiQTNamNay_VND,0))) as FSoSanhKinhPhiVnd

from NH_TT_ThanhToan_ChiTiet ttct 
left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID
left join NH_KHTongThe tongThe on tt.iID_KHTongTheID = tongThe.ID
left join NH_DM_NhiemVuChi nvc on tt.iID_NhiemVuChiID = nvc.ID
left join NH_KHTongThe_NhiemVuChi tongTheNvc on nvc.ID = tongTheNvc.iID_NhiemVuChiID
left join DonVi dv on  tt.iID_DonVi = dv.iID_DonVi 
left join NH_DA_DuAn da on tt.iID_DuAnID = da.ID
left join NH_DA_HopDong hd on tt.iID_HopDongID = hd.ID
left join #tmpkpdc tmpkpdc on tt.iID_DonVi = tmpkpdc.iID_DonViID and tmpkpdc.iNamKeHoach between tongThe.iGiaiDoanTu and tongThe.iGiaiDoanDen and ttct.ID = tmpkpdc.iID_ThanhToan_ChiTietID
left join #qtdd qtdd on ttct.Id = qtdd.iID_ThanhToan_ChiTietID
left join NH_QT_QuyetToanNienDo qtnd on tongThe.iGiaiDoanDen = qtnd.iNamKeHoach
left join NH_QT_QuyetToanNienDo_ChiTiet qtndct on qtnd.ID = qtndct.iID_QuyetToanNienDoID
where 
tt.dNgayDeNghi >=
@fromDate
and 
tt.dNgayDeNghi <=
@toDate
and 
tt.iID_DonVi = @iIDDonVi
group by ttct.sTenNoiDungChi,tongThe.iGiaiDoanDen,tongThe.iGiaiDoanTu,da.sTenDuAn,tt.iID_DonVi,dv.iID_MaDonVi
,dv.sTenDonVi,hd.sTenHopDong,nvc.sTenNhiemVuChi,tt.iLoaiNoiDungChi,tt.iID_NhiemVuChiID,ttct.ID,tt.iID_HopDongID,tt.iID_DuAnID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_pheduyet_quyettoandaht_detail]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_pheduyet_quyettoandaht_detail] 
	-- Add the parameters for the stored procedure here
  @iIDDonVi uniqueidentifier,
  @iDPheDuyetQuyetToan uniqueidentifier,
  @devideDonViUSD float = null,
  @devideDonViVND float = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		select 
	    qtdact.ID as ID
       ,qtdact.iID_PheDuyetQuyetToanDAHT_ID as IIDPheDuyetQuyetToanDAHTId
	   ,Case When qtdact.iID_KHTT_NhiemVuChiID is null then '00000000-0000-0000-0000-000000000000' else qtdact.iID_KHTT_NhiemVuChiID End as IIDKHTTNhiemVuChiId
	   ,Case When qtdact.iID_DuAnID is null then '00000000-0000-0000-0000-000000000000' else qtdact.iID_DuAnID End as IIDDuAnId
	   ,qtdact.iID_HopDongID as IIDHopDongId
       ,qtdact.iID_ThanhToan_ChiTietID as IIDThanhToanChiTietId
       ,IIF(@devideDonViUSD is not null, round(qtdact.fHopDong_USD/@devideDonViUSD,2), qtdact.fHopDong_USD) as FHopDongUsd
	   ,IIF(@devideDonViVND is not null, round(qtdact.fHopDong_VND/@devideDonViVND,2), qtdact.fHopDong_VND) as FHopDongVnd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fKeHoach_TTCP_USD/@devideDonViUSD,2), qtdact.fKeHoach_TTCP_USD) as FKeHoachTTCPUsd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fKinhPhiDuocCap_Tong_USD/@devideDonViUSD,2), qtdact.fKinhPhiDuocCap_Tong_USD) as FKinhPhiDuocCapTongUsd
	   ,IIF(@devideDonViVND is not null, round(qtdact.fKinhPhiDuocCap_Tong_VND/@devideDonViVND,2), qtdact.fKinhPhiDuocCap_Tong_VND) as FKinhPhiDuocCapTongVnd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fQuyetToanDuocDuyet_Tong_USD/@devideDonViUSD,2), qtdact.fQuyetToanDuocDuyet_Tong_USD) as FQuyetToanDuocDuyetTongUsd
	   ,IIF(@devideDonViVND is not null, round(qtdact.fQuyetToanDuocDuyet_Tong_VND/@devideDonViVND,2), qtdact.fQuyetToanDuocDuyet_Tong_VND) as FQuyetToanDuocDuyetTongVnd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fSoSanhKinhPhi_USD/@devideDonViUSD,2), qtdact.fSoSanhKinhPhi_USD) as FSoSanhKinhPhiUsd
	   ,IIF(@devideDonViVND is not null, round(qtdact.fSoSanhKinhPhi_VND/@devideDonViVND,2), qtdact.fSoSanhKinhPhi_VND) as FSoSanhKinhPhiVnd
	   ,IIF(@devideDonViUSD is not null, round(qtdact.fThuaTraNSNN_USD/@devideDonViUSD,2), qtdact.fThuaTraNSNN_USD) as FThuaTraNSNNUsd
	   ,IIF(@devideDonViVND is not null, round(qtdact.fThuaTraNSNN_VND/@devideDonViVND,2), qtdact.fThuaTraNSNN_VND) as FThuaTraNSNNVnd
	   ,IIF(@devideDonViUSD is not null, round(da.fUSD/@devideDonViUSD,2), da.fUSD) as FHopDongUsdDuAn
	   ,IIF(@devideDonViVND is not null, round(da.fVND/@devideDonViVND,2), da.fVND) as FHopDongVndDuAn
	   ,IIF(@devideDonViUSD is not null, round(hd.fGiaTriUSD/@devideDonViUSD,2), hd.fGiaTriUSD) as FHopDongUsdHopDong
	   ,IIF(@devideDonViVND is not null, round(hd.fGiaTriVND/@devideDonViVND,2), hd.fGiaTriVND) as FHopDongVndHopDong

       ,qtdact.iNamBaoCaoTu as INamBaoCaoTu
       ,qtdact.iNamBaoCaoDen as INamBaoCaoDen
	   ,ttct.sTenNoiDungChi as STenNoiDungChi
	   ,tt.iID_DonVi as IIDDonViId
	   ,dv.iID_MaDonVi + ' - '+dv.sTenDonVi as STenDonVi
	   ,da.sTenDuAn as STenDuAn
	   ,hd.sTenHopDong as STenHopDong
	   ,ttct.sTenNoiDungChi as STenNoiDungChi
	   ,nvc.sTenNhiemVuChi as STenNhiemVuChi
	   ,tt.iLoaiNoiDungChi  as ILoaiNoiDungChi
	   
from NH_QT_PheDuyetQuyetToanDAHT_ChiTiet qtdact
left join NH_QT_PheDuyetQuyetToanDAHT qtda on qtdact.iID_PheDuyetQuyetToanDAHT_ID = qtda.ID
left join  NH_KHTongThe_NhiemVuChi khbqp on qtdact.iID_KHTT_NhiemVuChiID = khbqp.ID
left join  NH_DM_NhiemVuChi nvc on khbqp.iID_NhiemVuChiID = nvc.ID
left join NH_DA_DuAn da on  qtdact.iID_DuAnId = da.ID 
left join DonVi dv on  qtda.iID_DonViID = dv.iID_DonVi 
left join NH_DA_HopDong hd on qtdact.iID_HopDongID = hd.ID
left join NH_TT_ThanhToan_ChiTiet ttct on qtdact.iID_ThanhToan_ChiTietID = ttct.ID
left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID
where (@iDPheDuyetQuyetToan IS NULL OR qtdact.ID = @iDPheDuyetQuyetToan)
  order by qtda.iID_DonViID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qddautu_get_duan_By_iID_DonViID]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_qddautu_get_duan_By_iID_DonViID]
@iIdKhlcntId uniqueidentifier,
@iId uniqueidentifier,
@iLoai int
AS
BEGIN
	SELECT DISTINCT da.*
	FROM NH_DA_QDDauTu as tbl
	INNER JOIN NH_DA_DuAn as da on tbl.iID_DuAnID = da.ID
	WHERE tbl.bIsActive = 1 AND da.iID_DonViQuanLyID = @iId AND da.iLoai=@iLoai

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_goithau_index]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_goithau_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT DISTINCT
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_KeHoachDatHangID AS IIdKeHoachDatHangId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.iThuocMenu AS IThuocMenu,
	hd.iThoiGianThucHien AS IThoiGianThucHien,
	hd.fGiaTriHopDongUSD AS FGiaTriHopDongUSD,
	hd.fGiaTriHopDongVND AS FGiaTriHopDongVND,
	hd.fGiaTriHopDongEUR AS FGiaTriHopDongEUR,
	hd.fGiaTriHopDongNgoaiTeKhac AS FGiaTriHopDongNgoaiTeKhac,
	hd.fTiGiaNhap AS FTiGiaNhap,
	gtnt.fGiaTRiHopDong_USD AS FGiaTriUsd,
	gtnt.fGiaTRiHopDong_VND AS FGiaTriVnd,
	gtnt.fGiaTRiHopDong_EUR AS FGiaTriEur,
	gtnt.fGiaTriHopDong_NgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	da.sTenDuAn AS STenDuAn,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	lhd.sMaLoaiHopDong AS SMaLoaiHopDong,
	nt.sMaNhaThau AS SMaNhaThauThucHien,
	hd.sHinhThucHopDong AS SHinhThucHopDong,
	da.sMaDuAn AS SMaDuAn,
	CASE WHEN hd.iID_ParentAdjustId IS NULL THEN '' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId) END DieuChinhTu
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da on hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN NH_DM_NhaThau nt ON hd.iID_NhaThauThucHienID = nt.Id
LEFT JOIN (SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	 FROM NH_KHTongThe_NhiemVuChi AS n 
	 INNER JOIN NH_DM_NhiemVuChi AS d 
	 ON n.iID_NhiemVuChiID = d.ID
) AS nvChi ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
LEFT JOIN (SELECT iID_HopDongID,
	SUM(fGiaTRiHopDong_USD) AS fGiaTRiHopDong_USD,
	SUM(fGiaTRiHopDong_VND) AS fGiaTRiHopDong_VND,
	SUM(fGiaTRiHopDong_EUR) AS fGiaTRiHopDong_EUR,
	SUM(fGiaTriHopDong_NgoaiTeKhac) AS fGiaTriHopDong_NgoaiTeKhac
	FROM NH_DA_HopDong_GoiThau_NhaThau 
	WHERE NH_DA_HopDong_GoiThau_NhaThau.isCheck=1
	GROUP BY iID_HopDongID
) gtnt ON hd.ID = gtnt.iID_HopDongID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY hd.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_index]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT DISTINCT
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_KeHoachDatHangID AS IIdKeHoachDatHangId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.iThuocMenu AS IThuocMenu,
	hd.iThoiGianThucHien AS IThoiGianThucHien,
	hd.fGiaTriHopDongUSD AS FGiaTriHopDongUSD,
	hd.fGiaTriHopDongVND AS FGiaTriHopDongVND,
	hd.fGiaTriHopDongEUR AS FGiaTriHopDongEUR,
	hd.fGiaTriHopDongNgoaiTeKhac AS FGiaTriHopDongNgoaiTeKhac,
	hd.fTiGiaNhap AS FTiGiaNhap,
	gtnt.fGiaTRiHopDong_USD AS FGiaTriUsd,
	gtnt.fGiaTRiHopDong_VND AS FGiaTriVnd,
	gtnt.fGiaTRiHopDong_EUR AS FGiaTriEur,
	gtnt.fGiaTriHopDong_NgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	da.sTenDuAn AS STenDuAn,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	khdh.sSoQuyetDinh AS SMaKeHoachDatHang,
	lhd.sMaLoaiHopDong AS SMaLoaiHopDong,
	nt.sMaNhaThau AS SMaNhaThauThucHien,
	hd.sHinhThucHopDong AS SHinhThucHopDong,
	da.sMaDuAn AS SMaDuAn,
	CASE WHEN hd.iID_ParentAdjustId IS NULL THEN '' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId) END DieuChinhTu
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd ON hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da ON hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN NH_MSTN_KeHoachDatHang khdh ON hd.iID_KeHoachDatHangID = khdh.ID
LEFT JOIN NH_DM_NhaThau nt ON hd.iID_NhaThauThucHienID = nt.Id
LEFT JOIN (SELECT n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
	FROM NH_KHTongThe_NhiemVuChi AS n 
	INNER JOIN NH_DM_NhiemVuChi AS d 
	ON n.iID_NhiemVuChiID = d.ID
) AS nvChi ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
LEFT JOIN (SELECT iID_HopDongID, SUM(fGiaTRiHopDong_USD) AS fGiaTRiHopDong_USD,
	SUM(fGiaTRiHopDong_VND) AS fGiaTRiHopDong_VND,
	SUM(fGiaTRiHopDong_EUR) AS fGiaTRiHopDong_EUR,
	SUM(fGiaTriHopDong_NgoaiTeKhac) AS fGiaTriHopDong_NgoaiTeKhac
	FROM NH_DA_HopDong_GoiThau_NhaThau 
	GROUP BY iID_HopDongID
) AS gtnt ON hd.ID = gtnt.iID_HopDongID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY hd.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_thongtin_hopdong_ngoaithuong_index]
@iThuocMenu INT = NULL
AS
BEGIN
SELECT DISTINCT
	hd.Id,
	hd.sSoHopDong AS SSoHopDong,
	hd.dNgayHopDong AS DNgayHopDong,
	hd.sTenHopDong AS STenHopDong,
	hd.dKhoiCongDuKien AS DKhoiCongDuKien,
	hd.dKetThucDuKien AS DKetThucDuKien,
	hd.iID_KHTongThe_NhiemVuChiID AS IIdKhTongTheNhiemVuChiId,
	hd.iID_LoaiHopDongID AS IIdLoaiHopDongId,
	hd.iID_ParentAdjustID AS IIdParentAdjustId,
	hd.iID_GoiThauID AS IIdGoiThauId,
	hd.iID_ParentID AS IIdParentId,
	hd.iID_NhaThauThucHienID AS IIdNhaThauThucHienId,
	hd.iID_TiGiaID AS IIdTiGiaId,
	hd.iID_DuAnID AS IIdDuAnId,
	hd.iLoai AS ILoai,
	hd.fGiaTriHopDongUSD AS FGiaTriUsd,
	hd.fGiaTriHopDongVND AS FGiaTriVnd,
	hd.fGiaTriHopDongEUR AS FGiaTriEur,
	hd.fGiaTriHopDongNgoaiTeKhac AS FGiaTriNgoaiTeKhac,
	hd.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
	hd.sNguoiTao AS SNguoiTao,
	hd.sNguoiSua AS SNguoiSua,
	hd.sNguoiXoa AS SNguoiXoa,
	hd.dNgaySua AS DNgaySua,
	hd.dNgayTao AS DNgayTao,
	hd.dNgayXoa AS DNgayXoa,
	hd.bIsActive AS BIsActive,
	hd.bIsGoc AS BIsGoc,
	hd.bIsKhoa AS BIsKhoa,
	hd.fTiGiaNhap as FTiGiaNhap,
	lhd.sTenLoaiHopDong AS SLoaiHopDong,
	hd.iLanDieuChinh AS ILanDieuChinh,
	nvChi.iID_KHTongTheID AS IIdKhTongTheId,
	nvChi.STenChuongTrinh AS STenChuongTrinh,
	da.sTenDuAn AS STenDuAn,
	CONCAT(donVi.iID_MaDonVi, ' - ', donVi.sTenDonVi) AS STenDonVi,
	hd.iID_DonViQuanLyID AS IIdDonViQuanLyId,
	hd.iID_MaDonViQuanLy AS IIdMaDonViQuanLy,
	CASE
		WHEN hd.iID_ParentAdjustId IS NULL THEN
		'' ELSE ( SELECT TOP 1 hdpr.sSoHopDong FROM NH_DA_HopDong hdpr WHERE hdpr.Id = hd.iID_ParentAdjustId ) 
	END DieuChinhTu
	
FROM NH_DA_HopDong hd
LEFT JOIN NH_DM_LoaiHopDong lhd on hd.iID_LoaiHopDongID = lhd.iID_LoaiHopDongID
LEFT JOIN NH_DA_DuAn da on hd.iID_DuAnID = da.ID
LEFT JOIN DonVi donVi
ON hd.iID_DonViQuanLyID = donVi.iID_DonVi AND hd.iID_MaDonViQuanLy = donVi.iID_MaDonVi
LEFT JOIN
		(SELECT  n.ID, n.iID_KHTongTheID, d.sTenNhiemVuChi AS STenChuongTrinh, n.iID_NhiemVuChiID
		 FROM NH_KHTongThe_NhiemVuChi AS n 
		 INNER JOIN NH_DM_NhiemVuChi AS d 
		 ON n.iID_NhiemVuChiID = d.ID) 
AS nvChi
ON hd.iID_KHTongThe_NhiemVuChiID = nvChi.ID
WHERE @iThuocMenu IS NULL OR hd.iThuocMenu = @iThuocMenu
ORDER BY
	hd.dNgayTao DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thongtri_capphat_index]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_thongtri_capphat_index]
AS
BEGIN
	SELECT 
		thongtri.ID AS Id,
		thongtri.iID_MaDonViID AS IIdMaDonViId,
		thongtri.iID_DonViID AS IIdDonViId,
		thongtri.iID_NguonVonID AS IIdNguonVonId,
		thongtri.sMaThongTri AS SMaThongTri,
		thongtri.dNgayLapThongTri AS DNgayLapThongTri,
		thongtri.iNamThucHien AS INamThucHien,
		thongtri.iID_DonViTienTeID AS IIdDonViTienTeId,
		thongtri.dNgayGhiSo AS DNgayGhiSo,
		thongtri.sTK1 AS STk1,
		thongtri.sSoCT1 AS SSoCt1,
		thongtri.sTK2 AS STk2,
		thongtri.sSoCT2 AS SSoCt2,
		thongtri.iID_TiGiaUSD_VNDID AS IIdTiGiaUsdVndid,
		thongtri.iID_TiGiaUSD_NgoaiTeKhacID AS IIdTiGiaUsdNgoaiTeKhacId,
		thongtri.fTongGiaTriNgoaiTeKhac AS FTongGiaTriNgoaiTeKhac,
		thongtri.fTongGiaTriUSD AS FTongGiaTriUsd,
		thongtri.fTongGiaTriVND AS FTongGiaTriVnd,
		thongtri.sTongGiaTri_BangChu AS STongGiaTriBangChu,
		thongtri.sNguoiTao AS SNguoiTao,
		thongtri.dNgayTao AS DNgayTao,
		thongtri.sNguoiSua AS SNguoiSua,
		thongtri.dNgaySua AS DNgaySua,
		thongtri.sNguoiXoa AS SNguoiXoa,
		thongtri.dNgayXoa AS DNgayXoa,
		thongtri.bIsActive AS BIsActive,
		thongtri.bIsGoc AS BIsGoc,
		thongtri.bIsKhoa AS BIsKhoa,
		thongtri.iLanDieuChinh AS ILanDieuChinh,
		thongtri.iID_TiGiaID AS IIdTiGiaId,
		thongtri.bIsXoa AS BIsXoa,
		thongtri.sMaNgoaiTeKhac AS SMaNgoaiTeKhac,
		CONCAT(DonVi.iID_MaDonVi, ' - ', DonVi.sTenDonVi) AS STenDonVi,
		NguonNganSach.sTen AS STenNguonVon,
		tiente.sMaTienTe AS STenTienTe
	FROM NH_TT_ThongTriCapPhat thongtri
	LEFT JOIN DonVi ON thongtri.iID_DonViID = DonVi.iID_DonVi
	LEFT JOIN NguonNganSach ON thongtri.iID_NguonVonID = NguonNganSach.iID_MaNguonNganSach
	LEFT JOIN NH_DM_LoaiTienTe tiente ON thongtri.iID_DonViTienTeID = tiente.ID
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_thuchien_ngansach_index]    Script Date: 29/12/2022 4:07:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_nh_thuchien_ngansach_index]
	(@tabTable int, 
	@iQuyList int,
	@iNam int,
	@iTuNam int,
	@iDenNam int,
	@iDonvi uniqueidentifier)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- select thanh toan su dung kinh phi tu dau nam quy nay
	SELECT  
		th.Id, th.iID_ChungTu, th.iCoQuanThanhToan,
		ckpNamTruocDGN.fGiaTriUsd as ckpNamTruocUSDDGN ,ckpNamTruocDGN.fGiaTriVnd as ckpNamTruocVNDDGN,
		ckpNamNayDGN.fGiaTriUsd as ckpNamNayUSDDGN ,ckpNamNayDGN.fGiaTriVnd as ckpNamNayVNDDGN,
		ttNamTruocDGN.fGiaTriUsd as ttNamTruocUSDDGN ,ttNamTruocDGN.fGiaTriVnd as ttNamTruocVNDDGN,
		ttNamNayDGN.fGiaTriUsd as ttNamNayUSDDGN ,ttNamNayDGN.fGiaTriVnd as ttNamNayVNDDGN,
		twNamTruocDGN.fGiaTriUsd as twNamTruocUSDDGN ,twNamTruocDGN.fGiaTriVnd as twNamTruocVNDDGN,
		twNamNayDGN.fGiaTriUsd as twNamNayUSDDGN ,twNamNayDGN.fGiaTriVnd as twNamNayVNDDGN,
		thNamTrcDGN.fGiaTriUsd as thNamTrcUSDDGN ,thNamTrcDGN.fGiaTriVnd as thNamTrcVNDDGN,
		thNamNayDGN.fGiaTriUsd as thNamNayUSDDGN ,thNamNayDGN.fGiaTriVnd as thNamNayVNDDGN,
		------------------------------------------------------------------------------------------------
		ckpNamTruoc.fGiaTriUsd as ckpNamTruocUSD ,ckpNamTruoc.fGiaTriVnd as ckpNamTruocVND,
		ckpNamNay.fGiaTriUsd as ckpNamNayUSD ,ckpNamNay.fGiaTriVnd as ckpNamNayVND,
		ttNamTruoc.fGiaTriUsd as ttNamTruocUSD ,ttNamTruoc.fGiaTriVnd as ttNamTruocVND,
		ttNamNay.fGiaTriUsd as ttNamNayUSD ,ttNamNay.fGiaTriVnd as ttNamNayVND,
		twNamTruoc.fGiaTriUsd as twNamTruocUSD ,twNamTruoc.fGiaTriVnd as twNamTruocVND,
		twNamNay.fGiaTriUsd as twNamNayUSD ,twNamNay.fGiaTriVnd as twNamNayVND,
		thNamTrc.fGiaTriUsd as thNamTrcUSD ,thNamTrc.fGiaTriVnd as thNamTrcVND,
		thNamNay.fGiaTriUsd as thNamNayUSD ,thNamNay.fGiaTriVnd as thNamNayVND
		------------------------------------------------------------------------------------------------
		--ckpNamTruocToYear.fGiaTriUsd as ckpNamTruocUSDToYear ,ckpNamTruocToYear.fGiaTriVnd as ckpNamTruocVNDToYear,
		--ckpNamNayToYear.fGiaTriUsd as ckpNamNayUSDToYear ,ckpNamNayToYear.fGiaTriVnd as ckpNamNayVNDToYear,
		--ttNamTruocToYear.fGiaTriUsd as ttNamTruocUSDToYear ,ttNamTruocToYear.fGiaTriVnd as ttNamTruocVNDToYear,
		--ttNamNayToYear.fGiaTriUsd as ttNamNayUSDToYear ,ttNamNayToYear.fGiaTriVnd as ttNamNayVNDToYear,
		--twNamTruocToYear.fGiaTriUsd as twNamTruocUSDToYear ,twNamTruocToYear.fGiaTriVnd as twNamTruocVNDToYear,
		--twNamNayToYear.fGiaTriUsd as twNamNayUSDToYear ,twNamNayToYear.fGiaTriVnd as twNamNayVNDToYear,
		--thNamTrcToYear.fGiaTriUsd as thNamTrcUSDToYear ,thNamTrcToYear.fGiaTriVnd as thNamTrcVNDToYear,
		--thNamNayToYear.fGiaTriUsd as thNamNayUSDToYear ,thNamNayToYear.fGiaTriVnd as thNamNayVNDToYear
	INTO #TongHop
	FROM NH_TH_TongHop th
		-- Đã giải ngân
		--Start Cấp kinh phí năm trước chuyển sang
		left join NH_TH_TongHop ckpNamTruocDGN on ckpNamTruocDGN.ID = th.ID and ckpNamTruocDGN.bIsLog = 0 and ckpNamTruocDGN.sMaTienTrinh != '300'
														and ckpNamTruocDGN.sMaNguon = '102' and ckpNamTruocDGN.sMaDich = '000' 
														and ckpNamTruocDGN.iNamKeHoach < @iNam
		--Start Cấp kinh phí năm nay
		left join NH_TH_TongHop ckpNamNayDGN on ckpNamNayDGN.ID = th.ID and ckpNamNayDGN.bIsLog = 0 and ckpNamNayDGN.sMaTienTrinh != '300'
														and ckpNamNayDGN.sMaNguon = '101' and ckpNamNayDGN.sMaDich = '000' 
														and ckpNamNayDGN.iNamKeHoach < @iNam
		--Start Thanh toán sử dụng kinh phí năm trước chuyển sang
		left join NH_TH_TongHop ttNamTruocDGN on ttNamTruocDGN.ID = th.ID and ttNamTruocDGN.bIsLog = 0 and ttNamTruocDGN.sMaTienTrinh != '300'
														and ttNamTruocDGN.sMaNguon = '000' and ttNamTruocDGN.sMaDich = '112' 
														and ttNamTruocDGN.iNamKeHoach < @iNam
		--Start Thanh toán sử dụng kinh phí năm nay
		left join NH_TH_TongHop ttNamNayDGN on ttNamNayDGN.ID = th.ID and ttNamNayDGN.bIsLog = 0 and ttNamNayDGN.sMaTienTrinh != '300'
														and ttNamNayDGN.sMaNguon = '000' and ttNamNayDGN.sMaDich = '111' 
														and ttNamNayDGN.iNamKeHoach < @iNam
		--Start Tạm ứng sử dụng kinh phí năm trước chuyển sang
		left join NH_TH_TongHop twNamTruocDGN on twNamTruocDGN.ID = th.ID and twNamTruocDGN.bIsLog = 0 and twNamTruocDGN.sMaTienTrinh != '300'
														and twNamTruocDGN.sMaNguon = '000' and twNamTruocDGN.sMaDich = '122' 
														and twNamTruocDGN.iNamKeHoach < @iNam
		--Start Tạm ứng sử dụng kinh phí năm nay
		left join NH_TH_TongHop twNamNayDGN on twNamNayDGN.ID = th.ID and twNamNayDGN.bIsLog = 0 and twNamNayDGN.sMaTienTrinh != '300'
														and twNamNayDGN.sMaNguon = '000' and twNamNayDGN.sMaDich = '121' 
														and twNamNayDGN.iNamKeHoach < @iNam
		--Start Thu hồi tạm ứng năm trước chuyển sang
		left join NH_TH_TongHop thNamTrcDGN on thNamTrcDGN.ID = th.ID and thNamTrcDGN.bIsLog = 0 and thNamTrcDGN.sMaTienTrinh != '300'
														and thNamTrcDGN.sMaNguon = '122' and thNamTrcDGN.sMaDich = '000' 
														and thNamTrcDGN.iNamKeHoach < @iNam
		--Start Thu hồi tạm ứng năm nay
		left join NH_TH_TongHop thNamNayDGN on thNamNayDGN.ID = th.ID and thNamNayDGN.bIsLog = 0 and thNamNayDGN.sMaTienTrinh != '300'
														and thNamNayDGN.sMaNguon = '121' and thNamNayDGN.sMaDich = '000' 
														and thNamNayDGN.iNamKeHoach < @iNam
		------------------------------------------------------------------------------------------------
		-- Kinh phí được cấp
		--Start Cấp kinh phí năm trước chuyển sang
		left join NH_TH_TongHop ckpNamTruoc on ckpNamTruoc.ID = th.ID and ckpNamTruoc.bIsLog = 0 and ckpNamTruoc.sMaTienTrinh != '300'
														and ckpNamTruoc.sMaNguon = '102' and ckpNamTruoc.sMaDich = '000' 
														and ckpNamTruoc.iNamKeHoach = @iNam and ckpNamTruoc.iQuyKeHoach <= @iQuyList
		--Start Cấp kinh phí năm nay
		left join NH_TH_TongHop ckpNamNay on ckpNamNay.ID = th.ID and ckpNamNay.bIsLog = 0 and ckpNamNay.sMaTienTrinh != '300'
														and ckpNamNay.sMaNguon = '101' and ckpNamNay.sMaDich = '000' 
														and ckpNamNay.iNamKeHoach = @iNam and ckpNamNay.iQuyKeHoach <= @iQuyList
		--Start Thanh toán sử dụng kinh phí năm trước chuyển sang
		left join NH_TH_TongHop ttNamTruoc on ttNamTruoc.ID = th.ID and ttNamTruoc.bIsLog = 0 and ttNamTruoc.sMaTienTrinh != '300'
														and ttNamTruoc.sMaNguon = '000' and ttNamTruoc.sMaDich = '112' 
														and ttNamTruoc.iNamKeHoach = @iNam and ttNamTruoc.iQuyKeHoach <= @iQuyList
		--Start Thanh toán sử dụng kinh phí năm nay
		left join NH_TH_TongHop ttNamNay on ttNamNay.ID = th.ID and ttNamNay.bIsLog = 0 and ttNamNay.sMaTienTrinh != '300'
														and ttNamNay.sMaNguon = '000' and ttNamNay.sMaDich = '111' 
														and ttNamNay.iNamKeHoach = @iNam and ttNamNay.iQuyKeHoach <= @iQuyList
		--Start Tạm ứng sử dụng kinh phí năm trước chuyển sang
		left join NH_TH_TongHop twNamTruoc on twNamTruoc.ID = th.ID and twNamTruoc.bIsLog = 0 and twNamTruoc.sMaTienTrinh != '300'
														and twNamTruoc.sMaNguon = '000' and twNamTruoc.sMaDich = '122' 
														and twNamTruoc.iNamKeHoach = @iNam and twNamTruoc.iQuyKeHoach <= @iQuyList
		--Start Tạm ứng sử dụng kinh phí năm nay
		left join NH_TH_TongHop twNamNay on twNamNay.ID = th.ID and twNamNay.bIsLog = 0 and twNamNay.sMaTienTrinh != '300'
														and twNamNay.sMaNguon = '000' and twNamNay.sMaDich = '121' 
														and twNamNay.iNamKeHoach = @iNam and twNamNay.iQuyKeHoach <= @iQuyList
		--Start Thu hồi tạm ứng năm trước chuyển sang
		left join NH_TH_TongHop thNamTrc on thNamTrc.ID = th.ID and thNamTrc.bIsLog = 0 and thNamTrc.sMaTienTrinh != '300'
														and thNamTrc.sMaNguon = '122' and thNamTrc.sMaDich = '000' 
														and thNamTrc.iNamKeHoach = @iNam and thNamTrc.iQuyKeHoach <= @iQuyList
		--Start Thu hồi tạm ứng năm nay
		left join NH_TH_TongHop thNamNay on thNamNay.ID = th.ID and thNamNay.bIsLog = 0 and thNamNay.sMaTienTrinh != '300'
														and thNamNay.sMaNguon = '121' and thNamNay.sMaDich = '000' 
														and thNamNay.iNamKeHoach = @iNam and thNamNay.iQuyKeHoach <= @iQuyList
		------------------------------------------------------------------------------------------------
		---- Kinh phí được cấp Nam Nay
		----Start Cấp kinh phí năm trước chuyển sang
		--left join NH_TH_TongHop ckpNamTruocToYear on ckpNamTruocToYear.ID = th.ID and ckpNamTruocToYear.bIsLog = 0 and ckpNamTruocToYear.sMaTienTrinh != '300'
		--												and ckpNamTruocToYear.sMaNguon = '102' and ckpNamTruocToYear.sMaDich = '000' 
		--												and ckpNamTruocToYear.iNamKeHoach = @iNam -- and ckpNamTruocToYear.iQuyKeHoach <= @iQuyList
		----Start Cấp kinh phí năm nay
		--left join NH_TH_TongHop ckpNamNayToYear on ckpNamNayToYear.ID = th.ID and ckpNamNayToYear.bIsLog = 0 and ckpNamNayToYear.sMaTienTrinh != '300'
		--												and ckpNamNayToYear.sMaNguon = '101' and ckpNamNayToYear.sMaDich = '000' 
		--												and ckpNamNayToYear.iNamKeHoach = @iNam -- and ckpNamNayToYear.iQuyKeHoach <= @iQuyList
		----Start Thanh toán sử dụng kinh phí năm trước chuyển sang
		--left join NH_TH_TongHop ttNamTruocToYear on ttNamTruocToYear.ID = th.ID and ttNamTruocToYear.bIsLog = 0 and ttNamTruocToYear.sMaTienTrinh != '300'
		--												and ttNamTruocToYear.sMaNguon = '000' and ttNamTruocToYear.sMaDich = '112' 
		--												and ttNamTruocToYear.iNamKeHoach = @iNam -- and ttNamTruocToYear.iQuyKeHoach <= @iQuyList
		----Start Thanh toán sử dụng kinh phí năm nay
		--left join NH_TH_TongHop ttNamNayToYear on ttNamNayToYear.ID = th.ID and ttNamNayToYear.bIsLog = 0 and ttNamNayToYear.sMaTienTrinh != '300'
		--												and ttNamNayToYear.sMaNguon = '000' and ttNamNayToYear.sMaDich = '111' 
		--												and ttNamNayToYear.iNamKeHoach = @iNam -- and ttNamNayToYear.iQuyKeHoach <= @iQuyList
		----Start Tạm ứng sử dụng kinh phí năm trước chuyển sang
		--left join NH_TH_TongHop twNamTruocToYear on twNamTruocToYear.ID = th.ID and twNamTruocToYear.bIsLog = 0 and twNamTruocToYear.sMaTienTrinh != '300'
		--												and twNamTruocToYear.sMaNguon = '000' and twNamTruocToYear.sMaDich = '122' 
		--												and twNamTruocToYear.iNamKeHoach = @iNam -- and twNamTruocToYear.iQuyKeHoach <= @iQuyList
		----Start Tạm ứng sử dụng kinh phí năm nay
		--left join NH_TH_TongHop twNamNayToYear on twNamNayToYear.ID = th.ID and twNamNayToYear.bIsLog = 0 and twNamNayToYear.sMaTienTrinh != '300'
		--												and twNamNayToYear.sMaNguon = '000' and twNamNayToYear.sMaDich = '121' 
		--												and twNamNayToYear.iNamKeHoach = @iNam -- and twNamNayToYear.iQuyKeHoach <= @iQuyList
		----Start Thu hồi tạm ứng năm trước chuyển sang
		--left join NH_TH_TongHop thNamTrcToYear on thNamTrcToYear.ID = th.ID and thNamTrcToYear.bIsLog = 0 and thNamTrcToYear.sMaTienTrinh != '300'
		--												and thNamTrcToYear.sMaNguon = '122' and thNamTrcToYear.sMaDich = '000' 
		--												and thNamTrcToYear.iNamKeHoach = @iNam -- and thNamTrcToYear.iQuyKeHoach <= @iQuyList
		----Start Thu hồi tạm ứng năm nay
		--left join NH_TH_TongHop thNamNayToYear on thNamNayToYear.ID = th.ID and thNamNayToYear.bIsLog = 0 and thNamNayToYear.sMaTienTrinh != '300'
		--												and thNamNayToYear.sMaNguon = '121' and thNamNayToYear.sMaDich = '000' 
		--												and thNamNayToYear.iNamKeHoach = @iNam -- and thNamNayToYear.iQuyKeHoach <= @iQuyList
    -- Insert statements for procedure here
	SELECT ttct.*, 
		tt.iQuyKeHoach,
		tt.iNamKeHoach, 
		dm_nvc.sTenNhiemVuChi,
		da.sTenDuAn,
		hd.sTenHopDong,
		tt.iLoaiNoiDungChi,
		dmCDT.sTenDonVi as sTenCDT,
		tt.iID_DonVi as IDDonVi,
		nvc.fGiaTriKH_TTCP as NCVTTCP,
		nvc.fGiaTriKH_BQP as NhiemVuChi , 
		QTNDCT.fLuyKeKinhPhiDuocCap_USD , QTNDCT.fLuyKeKinhPhiDuocCap_VND,
		QTNDCT.fDeNghiQTNamNay_USD , QTNDCT.fDeNghiQTNamNay_VND,
		KHTT.iGiaiDoanDen_TTCP as iGiaiDoanDen , KHTT.iGiaiDoanTu_TTCP as iGiaiDoanTu,
		Case When nvc.iID_NhiemVuChiID is null then '00000000-0000-0000-0000-000000000000' else nvc.iID_NhiemVuChiID End as IDNhiemVuChi,
		Case When da.ID is null then '00000000-0000-0000-0000-000000000000' else da.ID End as IDDuAn,
		Case When hd.ID is null then '00000000-0000-0000-0000-000000000000' else hd.ID End as IDHopDong,
		Case When hd.fGiaTriUSD > 0 then hd.fGiaTriUSD Else da.fUSD End as HopDongUSD ,
		Case When hd.fGiaTriVND > 0 then hd.fGiaTriVND Else da.fVND End as HopDongVND ,
		KinhPhiDuoCapCacNamTruoc.KinhPhiUSD as KinhPhiUSD ,
		KinhPhiDuoCapCacNamTruoc.KinhPhiVND as KinhPhiVND, --Kinh phí được cấp các năm trước
		KinhPhiDuoCapTuDauNam.KinhPhiDuocCapToYUSD as KinhPhiToYUSD,
		KinhPhiDuoCapTuDauNam.KinhPhiDuocCapToYVND as KinhPhiToYVND, -- Kinh phí được cấp từ đầu năm đến quý này
		KinhPhiDaGiaiNganCacNamTruoc.KinhPhiUSD as KinhPhiDaChiUSD,
		KinhPhiDaGiaiNganCacNamTruoc.KinhPhiVND as KinhPhiDaChiVND , --Kinh phí đã giải ngân các năm trước
		KinhPhiDuoCapTuDauNam.KinhPhiDaGiaiNganToYUSD as KinhPhiDaChiToYUSD,
		KinhPhiDuoCapTuDauNam.KinhPhiDaGiaiNganToYVND as KinhPhiDaChiToYVND -- Kinh phí đã giải ngân từ đầu năm đến quý này

		into #TMP
		FROM NH_TT_ThanhToan_ChiTiet ttct 
		left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID 
		left join NH_DA_HopDong hd on hd.ID = tt.iID_HopDongID
		left join NH_DA_DuAn da on da.ID = tt.iID_DuAnID
		left join DM_ChuDauTu dmCDT on da.iID_ChuDauTuID = dmCDT.iID_DonVi
		left join NH_KHTongThe_NhiemVuChi nvc on nvc.iID_NhiemVuChiID = tt.iID_NhiemVuChiID and nvc.iID_KHTongTheID = tt.iID_KHTongTheID
		left join NH_DM_NhiemVuChi dm_nvc on dm_nvc.ID = nvc.iID_NhiemVuChiID
		left join NH_KHTongThe KHTT on KHTT.ID = nvc.iID_KHTongTheID
		
-- Tính cho giai đoạn lấy từ QTND chi tiết
	left join (
		Select NDCT.iID_ThanhToan_ChiTietID, 
		ISNULL(SUM(NDCT.fLuyKeKinhPhiDuocCap_USD),0) as fLuyKeKinhPhiDuocCap_USD, 
		ISNULL(SUM(NDCT.fLuyKeKinhPhiDuocCap_VND),0) as fLuyKeKinhPhiDuocCap_VND,
		ISNULL(SUM(NDCT.fDeNghiQTNamNay_USD),0) as fDeNghiQTNamNay_USD,
		ISNULL(SUM(NDCT.fDeNghiQTNamNay_VND),0) as fDeNghiQTNamNay_VND,
		ISNULL(SUM(NDCT.fQTKinhPhiDuyetCacNamTruoc_USD),0) as fQTKinhPhiDuyetCacNamTruoc_USD,
		ISNULL(SUM(NDCT.fQTKinhPhiDuyetCacNamTruoc_VND),0) as fQTKinhPhiDuyetCacNamTruoc_VND
		from NH_QT_QuyetToanNienDo_ChiTiet NDCT group by NDCT.iID_ThanhToan_ChiTietID
	)QTNDCT on QTNDCT.iID_ThanhToan_ChiTietID = ttct.ID

	left join (
		Select tt.ID, ISNULL(SUM(th.fGiaTriUsd),0) as KinhPhiUSD , ISNULL(SUM(th.fGiaTriVnd),0) as KinhPhiVND from NH_TT_ThanhToan tt
		left join NH_TH_TongHop th on th.bIsLog = 0 and th.sMaTienTrinh != '300' and th.iNamKeHoach = (@iNam - 1) and th.sMaDich = '000' and th.sMaNguon = '303' 
					and th.iID_DonVi = tt.iID_DonVi
					and (th.iID_DuAnId = tt.iID_DuAnId or ( th.iID_DuAnId is null and tt.iID_DuAnId is null))
					and (th.iID_HopDongId = tt.iID_HopDongId or ( th.iID_HopDongId is null and tt.iID_HopDongId is null))
		Group By tt.ID
	)KinhPhiDuoCapCacNamTruoc on KinhPhiDuoCapCacNamTruoc.ID = tt.ID

	left join (
		Select tt.ID, ISNULL(SUM(th.fGiaTriUsd),0) as KinhPhiUSD , ISNULL(SUM(th.fGiaTriVnd),0) as KinhPhiVND from NH_TT_ThanhToan tt
		left join NH_TH_TongHop th on th.bIsLog = 0 and th.sMaTienTrinh != '300' and th.iNamKeHoach = (@iNam - 1) and th.sMaDich = '311' and th.sMaNguon = '000' 
					and th.iID_DonVi = tt.iID_DonVi
					and (th.iID_DuAnId = tt.iID_DuAnId or ( th.iID_DuAnId is null and tt.iID_DuAnId is null))
					and (th.iID_HopDongId = tt.iID_HopDongId or ( th.iID_HopDongId is null and tt.iID_HopDongId is null))
		Group By tt.ID
	)KinhPhiDaGiaiNganCacNamTruoc on KinhPhiDaGiaiNganCacNamTruoc.ID = tt.ID
	--Start
	left join (
		SELECT tt.ID, 
			Case When th.iCoQuanThanhToan = 1 then (ISNULL(SUM(th.ttNamTruocUSD),0) + 
													ISNULL(SUM(th.ttNamNayUSD),0) + 
													ISNULL(SUM(th.twNamTruocUSD),0) + 
													ISNULL(SUM(th.twNamNayUSD),0) - 
													ISNULL(SUM(th.thNamTrcUSD),0) - 
													ISNULL(SUM(th.thNamNayUSD),0)) 
				When th.iCoQuanThanhToan = 2 then (ISNULL(SUM(th.ckpNamTruocUSD),0) + 
													ISNULL(SUM(th.ckpNamNayUSD),0))
				Else 0 End as KinhPhiDuocCapToYUSD,
			Case When th.iCoQuanThanhToan = 1 then (ISNULL(SUM(th.ttNamTruocVND),0) + 
													ISNULL(SUM(th.ttNamNayVND),0) + 
													ISNULL(SUM(th.twNamTruocVND),0) + 
													ISNULL(SUM(th.twNamNayVND),0) - 
													ISNULL(SUM(th.thNamTrcVND),0) - 
													ISNULL(SUM(th.thNamNayVND),0)) 
				When th.iCoQuanThanhToan = 2 then (ISNULL(SUM(th.ckpNamTruocVND),0) + 
													ISNULL(SUM(th.ckpNamNayVND),0))
				Else 0 End as KinhPhiDuocCapToYVND,
			(ISNULL(SUM(th.ttNamTruocUSDDGN),0) + 
			ISNULL(SUM(th.ttNamNayUSDDGN),0) + 
			ISNULL(SUM(th.twNamTruocUSDDGN),0) + 
			ISNULL(SUM(th.twNamNayUSDDGN),0) - 
			ISNULL(SUM(th.thNamTrcUSDDGN),0) - 
			ISNULL(SUM(th.thNamNayUSDDGN),0)) as KinhPhiDaGiaiNganToYUSD,
			(ISNULL(SUM(th.ttNamTruocVNDDGN),0) + 
			ISNULL(SUM(th.ttNamNayVNDDGN),0) + 
			ISNULL(SUM(th.twNamTruocVNDDGN),0) + 
			ISNULL(SUM(th.twNamNayVNDDGN),0) - 
			ISNULL(SUM(th.thNamTrcVNDDGN),0) - 
			ISNULL(SUM(th.thNamNayVNDDGN),0)) as KinhPhiDaGiaiNganToYVND
		FROM NH_TT_ThanhToan tt
			left join #TongHop th on th.iID_ChungTu = tt.id
			Group By tt.ID, th.iCoQuanThanhToan
	)KinhPhiDuoCapTuDauNam on KinhPhiDuoCapTuDauNam.ID = tt.ID

Where tt.iID_DonVi = @iDonvi or @iDonvi is null or @iDonvi = '00000000-0000-0000-0000-000000000000'
Order by tt.iID_NhiemVuChiID , dm_nvc.sTenNhiemVuChi , da.ID , da.sTenDuAn, tt.iLoaiNoiDungChi ,hd.ID , hd.sTenHopDong
SELECT * into #Main FROM #TMP tt
Where ((@tabTable = 0 and (tt.iQuyKeHoach = @iQuyList or @iQuyList = 0) and tt.iNamKeHoach = @iNam ) 
		or (@tabTable = 1 and @iTuNam <= tt.iNamKeHoach and tt.iNamKeHoach <= @iDenNam)) 
		and (tt.IDDonVi = @iDonvi or @iDonvi is null or @iDonvi = '00000000-0000-0000-0000-000000000000')
SELECT distinct tt.*, 
	--TMPMain.KinhPhiUSDSelect as KinhPhiUSD , TMPMain.KinhPhiVNDSelect as KinhPhiVND , 
	TMPMain.KinhPhiToYUSDSelect as KinhPhiToYUSD , TMPMain.KinhPhiToYVNDSelect as KinhPhiToYVND , 
	TMPMain.KinhPhiDaChiUSDSelect as KinhPhiDaChiUSD , TMPMain.KinhPhiDaChiVNDSelect as KinhPhiDaChiVND , 
	TMPMain.KinhPhiDaChiToYUSDSelect as KinhPhiDaChiToYUSD , TMPMain.KinhPhiDaChiToYVNDSelect as KinhPhiDaChiToYVND 
FROM #Main tt
Left join (
	Select TMP.IDNhiemVuChi , TMP.IDDuAn , TMP.sTenHopDong , TMP.IDHopDong , 
	--SUM(ISNULL(TMP.KinhPhiUSD, 0)) as KinhPhiUSDSelect , SUM(ISNULL(TMP.KinhPhiVND, 0)) as KinhPhiVNDSelect ,
	SUM(ISNULL(TMP.KinhPhiToYUSD, 0)) as KinhPhiToYUSDSelect , SUM(ISNULL(TMP.KinhPhiToYVND, 0)) as KinhPhiToYVNDSelect , 
	SUM(ISNULL(TMP.KinhPhiDaChiUSD, 0)) as KinhPhiDaChiUSDSelect , SUM(ISNULL(TMP.KinhPhiDaChiVND, 0)) as KinhPhiDaChiVNDSelect , 
	SUM(ISNULL(TMP.KinhPhiDaChiToYUSD, 0)) as KinhPhiDaChiToYUSDSelect , SUM(ISNULL(TMP.KinhPhiDaChiToYVND, 0)) as KinhPhiDaChiToYVNDSelect 
	from #TMP TMP
	where TMP.IDNhiemVuChi is not null 
	Group by  TMP.IDNhiemVuChi , TMP.IDDuAn , TMP.IDHopDong ,TMP.sTenHopDong
) TMPMain on TMPMain.IDNhiemVuChi = tt.IDNhiemVuChi and ((tt.IDDuAN is null and TMPMain.IDNhiemVuChi is null) or TMPMain.IDDuAn = tt.IDDuAn) 
and ((tt.IDHopDong is null and TMPMain.IDDuAn is null) or TMPMain.IDHopDong	 = tt.IDHopDong) 
Order by tt.IDNhiemVuChi , tt.sTenNhiemVuChi , tt.IDDuAn , tt.sTenDuAn, tt.iLoaiNoiDungChi ,tt.IDHopDong , tt.sTenHopDong
DROP TABLE #TMP
DROP TABLE #Main
DROP TABLE #TongHop



END
GO
