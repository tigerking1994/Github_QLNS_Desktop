/****** Object:  StoredProcedure [dbo].[sp_nh_nhiemvuchi_bykehoachtongthe_donvi]    Script Date: 11/6/2023 5:37:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_nhiemvuchi_bykehoachtongthe_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_nhiemvuchi_bykehoachtongthe_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]    Script Date: 11/6/2023 5:37:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]    Script Date: 11/6/2023 5:37:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]
GO
/****** Object:  StoredProcedure [dbo].[proc_get_all_nh_baocaoquyettoanniendo_detail]    Script Date: 11/6/2023 5:37:01 PM ******/
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
			tb.sMoTa as STenNoiDungChi,
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
	left join NS_MucLucNganSach tb  on tb.iID = qtct.iID_MucLucNganSachID
	left join NH_TT_ThanhToan tt on ttct.iID_DeNghiThanhToanID = tt.ID
	where 
        qtct.iID_QuyetToanNienDoID = @iIDQuyetToan
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]    Script Date: 11/6/2023 5:37:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]
	@IdKhTongThe uniqueidentifier,
	@IdDonVi uniqueidentifier
AS
BEGIN
SELECT
    tt_nvc.ID,
    tt_nvc.iID_KHTongTheID,
    tt_nvc.iID_NhiemVuChiID,
    tt_nvc.iID_DonViThuHuongID,
    donvi.sTenDonVi AS STenDonVi,
    donvi.iID_MaDonVi AS SMaDonViThuHuong,
    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP,
    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP,
    nvc.sMaNhiemVuChi,
    nvc.sTenNhiemVuChi,
    nvc.iLoaiNhiemVuChi 
FROM NH_KHTongThe_NhiemVuChi tt_nvc
JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID 
JOIN DonVi donvi on DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID
WHERE
    tt_nvc.iID_KHTongTheID = @IdKhTongThe
    AND tt_nvc.iID_DonViThuHuongID = @IdDonVi
ORDER  BY nvc.sMaNhiemVuChi
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_nhiemvuchi_bykehoachtongthe_donvi]    Script Date: 11/6/2023 5:37:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_nhiemvuchi_bykehoachtongthe_donvi]
	@idKeHoachTongThe uniqueidentifier,
	@idDonVi uniqueidentifier
AS BEGIN

	SELECT 
		KHTTNhiemVuChi.ID as IIdKHTTNhiemVuChiId,
		NhiemVuChi.Id as Id,
		NhiemVuChi.sMaNhiemVuChi as SMaNhiemVuChi,
		NhiemVuChi.sTenNhiemVuChi as STenNhiemVuChi,
		NhiemVuChi.sMoTaChiTiet as SMoTaChiTiet,
		NhiemVuChi.iLoaiNhiemVuChi as iLoaiNhiemVuChi
	FROM NH_DM_NhiemVuChi NhiemVuChi
	INNER JOIN NH_KHTongThe_NhiemVuChi KHTTNhiemVuChi
		ON NhiemVuChi.ID = KHTTNhiemVuChi.iID_NhiemVuChiID
	INNER JOIN NH_KHTongThe NHKHTongThe
		ON KHTTNhiemVuChi.iID_KHTongTheID = NHKHTongThe.ID
	WHERE
		1=1
		AND NHKHTongThe.ID = @idKeHoachTongThe
		AND KHTTNhiemVuChi.iID_DonViThuHuongID = @idDonVi
	ORDER BY SMaNhiemVuChi
END
;
;
GO
