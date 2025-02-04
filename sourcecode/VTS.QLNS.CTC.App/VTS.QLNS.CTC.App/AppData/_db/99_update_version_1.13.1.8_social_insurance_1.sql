/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 9/18/2023 2:07:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 9/18/2023 2:07:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
	@ListIdChungTuTongHop ntext, 
	@Nguoitao nvarchar(50), 
	@IdChungTu nvarchar(100), 
	@NamLamViec int 
AS 
BEGIN 
	INSERT INTO [dbo].BH_DTT_BHXH_DieuChinh_ChiTiet (
    iID_DTT_BHXH_DieuChinh_ChiTiet, iID_DTT_BHXH_DieuChinh, iID_MucLucNganSach, sLNS, sNoiDung,
	fThuBHXH_NLD, fThuBHXH_NSD, fThuBHYT_NLD, fThuBHYT_NSD, fThuBHTN_NLD, fThuBHTN_NSD,
	fThuBHXH_NLD_QTDauNam, fThuBHXH_NSD_QTDauNam, fThuBHYT_NLD_QTDauNam, fThuBHYT_NSD_QTDauNam, fThuBHTN_NLD_QTDauNam, fThuBHTN_NSD_QTDauNam,
	fThuBHXH_NLD_QTCuoiNam, fThuBHXH_NSD_QTCuoiNam, fThuBHYT_NLD_QTCuoiNam, fThuBHYT_NSD_QTCuoiNam, fThuBHTN_NLD_QTCuoiNam, fThuBHTN_NSD_QTCuoiNam,
	fTongThuBHXH_NLD, fTongThuBHXH_NSD, fTongThuBHYT_NLD, fTongThuBHYT_NSD, fTongThuBHTN_NLD, fTongThuBHTN_NSD,
	fThuBHXH_NLD_Tang, fThuBHXH_NSD_Tang, fThuBHXH_Tang, fThuBHYT_NLD_Tang, fThuBHYT_NSD_Tang, fThuBHYT_Tang, fThuBHTN_NLD_Tang, fThuBHTN_NSD_Tang, fThuBHTN_Tang,
	fThuBHXH_NLD_Giam, fThuBHXH_NSD_Giam, fThuBHXH_Giam, fThuBHYT_NLD_Giam, fThuBHYT_NSD_Giam, fThuBHYT_Giam, fThuBHTN_NLD_Giam, fThuBHTN_NSD_Giam, fThuBHTN_Giam,
    dNgaySua, dNgayTao, sNguoiSua, sNguoiTao)

	SELECT 
	DISTINCT NEWID(), @idChungTu, iID_MucLucNganSach, sLNS, sNoiDung, 
	sum(fThuBHXH_NLD), sum(fThuBHXH_NSD), sum(fThuBHYT_NLD), sum(fThuBHYT_NSD), sum(fThuBHTN_NLD), sum(fThuBHTN_NSD),
	sum(fThuBHXH_NLD_QTDauNam), sum(fThuBHXH_NSD_QTDauNam), sum(fThuBHYT_NLD_QTDauNam), sum(fThuBHYT_NSD_QTDauNam), sum(fThuBHTN_NLD_QTDauNam), sum(fThuBHTN_NSD_QTDauNam),
	sum(fThuBHXH_NLD_QTCuoiNam), sum(fThuBHXH_NSD_QTCuoiNam), sum(fThuBHYT_NLD_QTCuoiNam), sum(fThuBHYT_NSD_QTCuoiNam), sum(fThuBHTN_NLD_QTCuoiNam), sum(fThuBHTN_NSD_QTCuoiNam),
	sum(fTongThuBHXH_NLD), sum(fTongThuBHXH_NSD), sum(fTongThuBHYT_NLD), sum(fTongThuBHYT_NSD), sum(fTongThuBHTN_NLD), sum(fTongThuBHTN_NSD),
	sum(fThuBHXH_NLD_Tang), sum(fThuBHXH_NSD_Tang), sum(fThuBHXH_Tang), sum(fThuBHYT_NLD_Tang), sum(fThuBHYT_NSD_Tang), sum(fThuBHYT_Tang), sum(fThuBHTN_NLD_Tang), sum(fThuBHTN_NSD_Tang), sum(fThuBHTN_Tang),
	sum(fThuBHXH_NLD_Giam), sum(fThuBHXH_NSD_Giam), sum(fThuBHXH_Giam), sum(fThuBHYT_NLD_Giam), sum(fThuBHYT_NSD_Giam), sum(fThuBHYT_Giam), sum(fThuBHTN_NLD_Giam), sum(fThuBHTN_NSD_Giam), sum(fThuBHTN_Giam),
	Null, GETDATE(), Null, @Nguoitao 
	FROM 
	  BH_DTT_BHXH_DieuChinh_ChiTiet 
	WHERE 
	  iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop)) 
	GROUP BY 
	  sLNS,
	  iID_MucLucNganSach, 
	  sNoiDung;

	  --danh dau chung tu da tong hop
		update 
		  BH_DTT_BHXH_DieuChinh 
		set 
		  bDaTongHop = 1
		where 
		  iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop))
END

GO
