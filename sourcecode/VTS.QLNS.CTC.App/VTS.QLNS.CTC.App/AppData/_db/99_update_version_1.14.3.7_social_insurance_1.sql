/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]    Script Date: 4/23/2024 9:20:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 4/23/2024 9:20:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]    Script Date: 4/23/2024 9:20:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 4/23/2024 9:20:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]    Script Date: 4/23/2024 9:20:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 4/23/2024 9:20:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 4/23/2024 9:20:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 4/23/2024 9:20:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_can_cu_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 4/23/2024 9:20:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 4/23/2024 9:20:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfWork int,
	@UserName nvarchar(100),
	@MaDonVi nvarchar(50)
AS
BEGIN
	INSERT INTO BH_QTT_BHXH_ChungTu_ChiTiet
           (iID_QTT_BHXH_ChungTu,
		  iQSBQNam,
		  fLuongChinh,
		  fPCChucVu,
		  fPCTNNghe,
		  fPCTNVuotKhung,
		  fNghiOm,
		  fHSBL,
		  fTongQTLN,
		  fDuToan,
		  fDaQuyetToan,
		  fConLai,
		  fThu_BHXH_NLD,
		  fThu_BHXH_NSD,
		  fTongSoPhaiThuBHXH,
		  fThu_BHYT_NLD,
		  fThu_BHYT_NSD,
		  fTongSoPhaiThuBHYT,
		  fThu_BHTN_NLD,
		  fThu_BHTN_NSD,
		  fTongSoPhaiThuBHTN,
		  fTongCong,
		  sGhiChu,
		  iID_MLNS,
		  iID_MLNS_Cha,
		  sXauNoiMa,
		  sLNS)
    SELECT 
			@VoucherId,
			Sum(ctct.iQSBQNam),
			Sum(ctct.fLuongChinh),
			Sum(ctct.fPCChucVu) fPhuCapChucVu,
			Sum(ctct.fPCTNNghe),
			Sum(ctct.fPCTNVuotKhung),
			Sum(ctct.fNghiOm),
			Sum(ctct.fHSBL),
			Sum(ctct.fTongQTLN),
			Sum(ctct.fDuToan),
			Sum(ctct.fDaQuyetToan),
			Sum(ctct.fConLai),

			round((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHXH_NLD)), 0) fThu_BHXH_NLD,
			round((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHXH_NSD)), 0) fThu_BHXH_NSD,
			round(((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHXH_NLD)) + (Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHXH_NSD))), 0) fTongSoPhaiThuBHXH,

			round((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHYT_NLD)), 0) fThu_BHYT_NLD,
			round((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHYT_NSD)), 0) fThu_BHYT_NSD,
			round(((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHYT_NLD)) + (Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHYT_NSD))), 0) fTongSoPhaiThuBHYT,

			round((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHTN_NLD)), 0) fThu_BHTN_NLD,
			round((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHTN_NSD)), 0) fThu_BHTN_NSD,
			round(((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHTN_NLD)) + (Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHTN_NSD))), 0) fTongSoPhaiThuBHTN,

			round((((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHXH_NLD)) + (Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHXH_NSD))) + ((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHYT_NLD)) + (Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHYT_NSD))) + ((Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHTN_NLD)) + (Sum(isnull(ctct.fTongQTLN, 0) * mlns.fTyLe_BHTN_NSD)))), 0) fTongCong,

			null,
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS
	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_DM_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa
	WHERE ctct.iID_QTT_BHXH_ChungTu IN (SELECT * FROM f_split(@VoucherIds))
	AND mlns.iNamLamViec = @YearOfWork
	GROUP BY mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS;
	-----------------
	--Insert giai thich
	INSERT INTO BH_QTT_BHXH_CTCT_GiaiThich
           (dNgayTao,
			fConPhaiNopTiep,
			fDaNop_TrongQuyNam,
			fPhaiNop_BHXH,
			fPhaiNop_QuyNamTruoc,
			fPhaiNop_TrongQuyNam,
			fQuyTienLuongCanCu,
			fSoConPhaiNop,
			fSoDaNopSau31_12,
			fSoDaNopTrongNam,
			fSoPhaiThuNop,
			fSoTienGiamDong,
			fTongSoDaNop,
			fTongTruyThu_BHXH,
			fTruyThu_BHTN_NLD,
			fTruyThu_BHTN_NSD,
			fTruyThu_BHTN_TongCong,
			fTruyThu_BHXH_NLD,
			fTruyThu_BHXH_NSD,
			fTruyThu_BHXH_TongCong,
			fTruyThu_BHYT_NLD,
			fTruyThu_BHYT_NSD,
			fTruyThu_BHYT_TongCong,
			fTruyThu_QuyNamTruoc,
			iID_MLNS,
			iID_MaDonVi,
			ILoaiGiaiThich,
			iNamLamViec,
			iQuanSo,
			iQuyNam,
			iQuyNamLoai,
			iID_QTT_BHXH_ChungTu,
			sK,
			sL,
			sLNS,
			sM,
			sNguoiTao,
			sQuyNamMoTa,
			sTM,
			sXauNoiMa,
			sNoiDung)
	SELECT 
			getdate(),
			sum(ctct.fConPhaiNopTiep),
			sum(ctct.fDaNop_TrongQuyNam),
			sum(ctct.fPhaiNop_BHXH),
			sum(ctct.fPhaiNop_QuyNamTruoc),
			sum(ctct.fPhaiNop_TrongQuyNam),
			sum(ctct.fQuyTienLuongCanCu),
			sum(ctct.fSoConPhaiNop),
			sum(ctct.fSoDaNopSau31_12),
			sum(ctct.fSoDaNopTrongNam),
			sum(ctct.fSoPhaiThuNop),
			sum(ctct.fSoTienGiamDong),
			sum(ctct.fTongSoDaNop),
			sum(ctct.fTongTruyThu_BHXH),
			sum(ctct.fTruyThu_BHTN_NLD),
			sum(ctct.fTruyThu_BHTN_NSD),
			sum(ctct.fTruyThu_BHTN_TongCong),
			sum(ctct.fTruyThu_BHXH_NLD),
			sum(ctct.fTruyThu_BHXH_NSD),
			sum(ctct.fTruyThu_BHXH_TongCong),
			sum(ctct.fTruyThu_BHYT_NLD),
			sum(ctct.fTruyThu_BHYT_NSD),
			sum(ctct.fTruyThu_BHYT_TongCong),
			sum(ctct.fTruyThu_QuyNamTruoc),
			ctct.iID_MLNS,
			@MaDonVi,
			ctct.ILoaiGiaiThich,
			@YearOfWork,
			sum(ctct.iQuanSo),
			ctct.iQuyNam,
			ctct.iQuyNamLoai,
			@VoucherId,
			ctct.sK,
			ctct.sL,
			ctct.sLNS,
			ctct.sM,
			@UserName,
			ctct.sQuyNamMoTa,
			ctct.sTM,
			ctct.sXauNoiMa,
			ctct.sNoiDung
	FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	WHERE ctct.iID_QTT_BHXH_ChungTu IN (SELECT * FROM f_split(@VoucherIds))
	GROUP BY ctct.iID_MLNS, ctct.ILoaiGiaiThich, ctct.iQuyNam, ctct.iQuyNamLoai, ctct.sK, ctct.sL, ctct.sLNS, ctct.sM, ctct.sQuyNamMoTa, ctct.sTM, ctct.sXauNoiMa, ctct.sNoiDung

	-- Danh dau chung tu da tong hop
	UPDATE BH_QTT_BHXH_ChungTu SET bDaTongHop = 1 
	WHERE iID_QTT_BHXH_ChungTu in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_nam]    Script Date: 4/23/2024 9:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_nam] 
	@MaDonVi nvarchar(50),
	@Year int
AS
BEGIN
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_y]') AND type in (N'U')) drop table tbl_cancu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_y]') AND type in (N'U')) drop table tbl_cancu_result_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_y]') AND type in (N'U')) drop table tbl_luong_can_cu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final_y]') AND type in (N'U')) drop table tbl_cancu_result_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final_y]') AND type in (N'U')) drop table tbl_luong_can_cu_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_y;
	
	select
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 sum(ctct.DieuChinh) fGiaTri
	into tbl_cancu_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi
	where isnull(ctct.MaCachTl,'') = ''
	and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.Ma_DonVi = @MaDonVi
	and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi

	 --Get so nguoi
	select
	 pc.XauNoiMa,
	 sum(ctct.SoNguoi) IQSBQNam
	 into tbl_cancu_so_nguoi_y
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
	and pc.Ma_PhuCap = 'LHT_TT'
	and ct.Nam = @Year
	and pc.Nam = @Year
	and ct.Ma_DonVi = @MaDonVi
	and ct.bKhoa = 1
	group by
	 pc.XauNoiMa
	 ------------------------------------------
	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  songuoi.IQSBQNam IQSBQNam,
	  --songuoi.IQSBQNam/12 IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL
	 into tbl_cancu_result_y
	 from tbl_cancu_y cancu
	 left join tbl_cancu_y luongchinh on cancu.XauNoiMa = luongchinh.XauNoiMa and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_cancu_y pccv on cancu.XauNoiMa = pccv.XauNoiMa and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_cancu_y pctn on cancu.XauNoiMa = pctn.XauNoiMa and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_cancu_y pctnvk on cancu.XauNoiMa = pctnvk.XauNoiMa and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_cancu_y hsbl on cancu.XauNoiMa = hsbl.XauNoiMa and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_cancu_so_nguoi_y songuoi on cancu.XauNoiMa = songuoi.XauNoiMa

	 --update tbl_cancu_result_y set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL
	 into tbl_cancu_result_final_y
	 from tbl_cancu_result_y cancu
	 group by cancu.XauNoiMa, cancu.iKhoi

	 --Luong BHXH
	 select
	  ctct.sMaCB XauNoiMa,
	  sum(ctct.nGiaTri) FNghiOm
	 into tbl_luong_can_cu_y
	 from TL_BangLuong_ThangBHXH ctct
	 join TL_DS_CapNhap_BangLuong ct on ctct.iID_Parent = ct.Id
	 where ctct.iNam = @Year
		and ctct.sMaDonVi = @MaDonVi
		and ctct.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
		and (ctct.sMaCB like '1%' or ctct.sMaCB like '2%' or ctct.sMaCB like '0%' or ctct.sMaCB = '43' or ctct.sMaCB = '423' or ctct.sMaCB = '425' or ctct.sMaCB in ('3.1', '3.2', '3.3', '413', '415'))
		and ct.KhoaBangLuong = 1
		and isnull(ct.STongHop, '') <> ''
	 group by ctct.sMaCB

	 update tbl_luong_can_cu_y set XauNoiMa = '1' where XauNoiMa like '1%'
	 update tbl_luong_can_cu_y set XauNoiMa = '2' where XauNoiMa like '2%'
	 update tbl_luong_can_cu_y set XauNoiMa = '4' where XauNoiMa like '0%'
	 update tbl_luong_can_cu_y set XauNoiMa = '3' where XauNoiMa in ('3.1', '3.2', '3.3', '413', '415')
	 update tbl_luong_can_cu_y set XauNoiMa = '43' where XauNoiMa in ('423', '425')

	 select
	  XauNoiMa,
	  sum(FNghiOm) FNghiOm
	 into tbl_luong_can_cu_final_y
	 from tbl_luong_can_cu_y
	 group by XauNoiMa

	 --result
	 select
	  luong.XauNoiMa,
	  luong.IQSBQNam,
	  luong.iKhoi,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm
	 from tbl_cancu_result_final_y luong
	 left join tbl_luong_can_cu_final_y bhxh on luong.XauNoiMa = bhxh.XauNoiMa
	 -----------------------------------------------
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_y]') AND type in (N'U')) drop table tbl_cancu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_y]') AND type in (N'U')) drop table tbl_cancu_result_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_y]') AND type in (N'U')) drop table tbl_luong_can_cu_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final_y]') AND type in (N'U')) drop table tbl_cancu_result_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final_y]') AND type in (N'U')) drop table tbl_luong_can_cu_final_y;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi_y]') AND type in (N'U')) drop table tbl_cancu_so_nguoi_y;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_can_cu_quy]    Script Date: 4/23/2024 9:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_can_cu_quy] 
	@MaDonVi nvarchar(50),
	@Year int,
	@Months nvarchar(20),
	@LoaiQuyNam int
AS
BEGIN
	
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;
	
	select
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi,
	 sum(ctct.DieuChinh) fGiaTri
	into tbl_cancu
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	join DonVi donvi on ct.Ma_DonVi = donvi.iID_MaDonVi
	where isnull(ctct.MaCachTl,'') = ''
		and pc.Ma_PhuCap in ('LHT_TT','PCCV_TT','PCTN_TT','PCTNVK_TT','HSBL_TT')
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.Ma_PhuCap,
	 pc.XauNoiMa,
	 donvi.iKhoi
	 
	--Get so nguoi
	select
	 pc.XauNoiMa,
	 sum(ctct.SoNguoi) IQSBQNam
	 into tbl_cancu_so_nguoi
	from TL_PhuCap_MLNS pc
	join TL_QT_ChungTuChiTiet ctct on pc.XauNoiMa = ctct.XauNoiMa
	join TL_QT_ChungTu ct on ct.ID = ctct.Id_ChungTu
	where isnull(ctct.MaCachTl,'') = ''
		and pc.Ma_PhuCap = 'LHT_TT'
		and ct.Thang in (SELECT * FROM f_split(@Months))
		and ct.Nam = @Year
		and pc.Nam = @Year
		and ct.Ma_DonVi = @MaDonVi
		and ct.bKhoa = 1
	group by
	 pc.XauNoiMa
	 ------------------------------------------
	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  case
		when @LoaiQuyNam = 0 then songuoi.IQSBQNam
		else songuoi.IQSBQNam/3
	  end as IQSBQNam,
	  luongchinh.fGiaTri FGiaTriLuongChinh,
	  pccv.fGiaTri FGiaTriPCCV,
	  pctn.fGiaTri FGiaTriPCTN,
	  pctnvk.fGiaTri FGiaTriPCTNVK,
	  hsbl.fGiaTri FGiaTriHSBL
	 into tbl_cancu_result
	 from tbl_cancu cancu
	 left join tbl_cancu luongchinh on cancu.XauNoiMa = luongchinh.XauNoiMa and luongchinh.Ma_PhuCap = 'LHT_TT'
	 left join tbl_cancu pccv on cancu.XauNoiMa = pccv.XauNoiMa and pccv.Ma_PhuCap = 'PCCV_TT'
	 left join tbl_cancu pctn on cancu.XauNoiMa = pctn.XauNoiMa and pctn.Ma_PhuCap = 'PCTN_TT'
	 left join tbl_cancu pctnvk on cancu.XauNoiMa = pctnvk.XauNoiMa and pctnvk.Ma_PhuCap = 'PCTNVK_TT'
	 left join tbl_cancu hsbl on cancu.XauNoiMa = hsbl.XauNoiMa and hsbl.Ma_PhuCap = 'HSBL_TT'
	 left join tbl_cancu_so_nguoi songuoi on cancu.XauNoiMa = songuoi.XauNoiMa

	 --update tbl_cancu_result set SMaCapBac = '3' where SMaCapBac in ('3.1','3.2','3.3')

	 select distinct
	  cancu.XauNoiMa,
	  cancu.iKhoi,
	  sum(cancu.IQSBQNam) IQSBQNam,
	  sum(cancu.FGiaTriLuongChinh) FGiaTriLuongChinh,
	  sum(cancu.FGiaTriPCCV) FGiaTriPCCV,
	  sum(cancu.FGiaTriPCTN) FGiaTriPCTN,
	  sum(cancu.FGiaTriPCTNVK) FGiaTriPCTNVK,
	  sum(cancu.FGiaTriHSBL) FGiaTriHSBL
	 into tbl_cancu_result_final
	 from tbl_cancu_result cancu
	 group by cancu.XauNoiMa, cancu.iKhoi

	 --Luong BHXH
	 select
	  ctct.sMaCB XauNoiMa,
	  sum(ctct.nGiaTri) FNghiOm
	 into tbl_luong_can_cu
	 from TL_BangLuong_ThangBHXH ctct
	 join TL_DS_CapNhap_BangLuong ct on ctct.iID_Parent = ct.Id
	 where ctct.iNam = @Year
		and ctct.sMaDonVi = @MaDonVi
		and ctct.iThang in (SELECT * FROM f_split(@Months))
		and ctct.sMaCheDo in ('BENHDAINGAY_D14NGAY', 'OMKHAC_D14NGAY')
		and (ctct.sMaCB like '1%' or ctct.sMaCB like '2%' or ctct.sMaCB like '0%' or ctct.sMaCB = '43' or ctct.sMaCB = '423' or ctct.sMaCB = '425' or ctct.sMaCB in ('3.1', '3.2', '3.3', '413', '415'))
		and ct.KhoaBangLuong = 1
		and isnull(ct.STongHop, '') <> ''
	 group by ctct.sMaCB

	update tbl_luong_can_cu set XauNoiMa = '1' where XauNoiMa like '1%'
	update tbl_luong_can_cu set XauNoiMa = '2' where XauNoiMa like '2%'
	update tbl_luong_can_cu set XauNoiMa = '4' where XauNoiMa like '0%'
	update tbl_luong_can_cu set XauNoiMa = '3' where XauNoiMa in ('3.1', '3.2', '3.3', '413', '415')
	update tbl_luong_can_cu set XauNoiMa = '43' where XauNoiMa in ('423', '425')

	select
	  XauNoiMa,
	  sum(FNghiOm) FNghiOm
	 into tbl_luong_can_cu_final
	 from tbl_luong_can_cu
	 group by XauNoiMa

	 --result
	 select
	  luong.XauNoiMa sXauNoiMa,
	  luong.IQSBQNam,
	  luong.iKhoi,
	  luong.FGiaTriLuongChinh,
	  luong.FGiaTriPCCV,
	  luong.FGiaTriPCTN,
	  luong.FGiaTriPCTNVK,
	  luong.FGiaTriHSBL,
	  CAST(bhxh.FNghiOm AS FLOAT) FNghiOm
	 from tbl_cancu_result_final luong
	 left join tbl_luong_can_cu_final bhxh on luong.XauNoiMa = bhxh.XauNoiMa
	 -----------------------------------------------
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu]') AND type in (N'U')) drop table tbl_cancu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result]') AND type in (N'U')) drop table tbl_cancu_result;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu]') AND type in (N'U')) drop table tbl_luong_can_cu;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_result_final]') AND type in (N'U')) drop table tbl_cancu_result_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_can_cu_final]') AND type in (N'U')) drop table tbl_luong_can_cu_final;
	 IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_cancu_so_nguoi]') AND type in (N'U')) drop table tbl_cancu_so_nguoi;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 4/23/2024 9:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all

	select 6 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan

	union all

	select 7 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 8 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 9 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 10 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
	union all
	select
	11 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union all
	select 12 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
	union all
	select 13 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 14 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 15 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 16 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all
	select
	18 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 19 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 20 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 21 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 22 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 23 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	24 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 25 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 26 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 27 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 28 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 29 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	36 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)
	union all
	select
	40 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		41 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		42 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	union all
	select
	43 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		44 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		45 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	-- Khối hạch toán
	union
	select
	47 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	49 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	union all
	select
		50 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		51 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

	union all
	select
	52 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		53 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		54 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
	--BHTN
	--Lấy dữ liệu khối dự toán
	union all
	select
	59 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		60 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
	union all
	select
		61 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
	union all
	select
		62 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
	union all
	select
		63 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		64 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	union all
	select
	65 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		66 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
	union all
	select
		67 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select
		68 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select
		69 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		70 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu khối hạch toán
	union all
	select
	72 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		73 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		74 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		75 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		76 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		77 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	78 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		79 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		80 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		81 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		82 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		83 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)

	union all
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)

	union all
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)

	union all
	select
	17 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)

	--BHYT
	union all
	select
	31 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	33 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	34 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	37 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and STT in (40, 43) and MaSo in (18, 19)

	union all
	select
		38 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (41,44)

	union all
	select
		39 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (42,45)

	union all
	select
	48 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and STT in (49,52) and MaSo in (23, 24)
	--BHTN
	union all
	select
	56 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	57 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	58 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	71 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	and MaSo in ('4=5+6', '7=8+9')

	union all
	select
	55 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	and MaSo in ('28=29+30', '31=32+33')

	union all
	select
	32 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	and MaSo in ('13=18+23', '14=19+24')

	union all
	select
	35 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (36, 37) and MaSo in ('16','17=18+19')
	union all
	select
	46 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (47, 48) and MaSo in ('21','22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (36)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (36)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (36)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (47)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (47)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (47)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	30 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (35, 46) and MaSo in ('15=16+17','20=21+22')
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]    Script Date: 4/23/2024 9:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all

	select 6 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan

	union all

	select 7 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 8 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 9 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 10 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
	union all
	select
	11 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union all
	select 12 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
	union all
	select 13 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum( ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 14 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 15 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 16 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all
	select
	18 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 19 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 20 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 21 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 22 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 23 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	24 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 25 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 26 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 27 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 28 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 29 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	36 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)
	union all
	select
	40 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		41 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		42 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	union all
	select
	43 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		44 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		45 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	-- Khối hạch toán
	union
	select
	47 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	49 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	union all
	select
		50 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		51 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

	union all
	select
	52 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		53 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		54 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
	--BHTN
	--Lấy dữ liệu khối dự toán
	union all
	select
	59 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		60 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
	union all
	select
		61 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
	union all
	select
		62 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
	union all
	select
		63 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		64 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	union all
	select
	65 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		66 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
	union all
	select
		67 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select
		68 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select
		69 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		70 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu khối hạch toán
	union all
	select
	72 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		73 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		74 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		75 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		76 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		77 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	78 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		79 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		80 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		81 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		82 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		83 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(pbct.fThu_BHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)

	union all
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)

	union all
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)

	union all
	select
	17 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)

	--BHYT
	union all
	select
	31 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	33 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	34 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	37 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and STT in (40, 43) and MaSo in (18, 19)

	union all
	select
		38 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (41,44)

	union all
	select
		39 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (42,45)

	union all
	select
	48 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and STT in (49,52) and MaSo in (23, 24)
	--BHTN
	union all
	select
	56 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	57 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	58 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	71 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	and MaSo in ('4=5+6', '7=8+9')

	union all
	select
	55 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	and MaSo in ('28=29+30', '31=32+33')

	union all
	select
	32 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	and MaSo in ('13=18+23', '14=19+24')

	union all
	select
	35 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (36, 37) and MaSo in ('16','17=18+19')
	union all
	select
	46 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (47, 48) and MaSo in ('21','22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (36)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (36)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (36)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (47)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (47)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (47)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	30 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (35, 46) and MaSo in ('15=16+17','20=21+22')
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 4/23/2024 9:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200)
AS
BEGIN
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.* into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	where ctct.iNamLamViec = @NamLamViec 
		and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
		and Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
	--Thu BHXH
	select * into TBL_THU_BHXH from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Thu BHXH' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, ROUND((sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))),0) fTongSo, ROUND(sum(fBHXH_NLD),0) fNLD, ROUND(sum(fBHXH_NSD), 0) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	select 3 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, ROUND((sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))),0) fTongSo, ROUND(sum(fBHXH_NLD), 0) fNLD, ROUND(sum(fBHXH_NSD),0) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002%') thubhxh

	update TBL_THU_BHXH set fTongSo = (select sum(fTongSo) from TBL_THU_BHXH where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHXH where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHXH where bHangCha = 0)
						where bHangCha = 1

	--Thu BHTN
	select * into TBL_THU_BHTN from
	(select 4 rowNum, 1 bHangCha, '2' stt, N'Thu BHTN' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 5 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, ROUND((sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))),0) fTongSo, ROUND(sum(fBHTN_NLD),0) fNLD, ROUND(sum(fBHTN_NSD),0) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	select 6 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, ROUND((sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))),0) fTongSo, ROUND(sum(fBHTN_NLD),0) fNLD, ROUND(sum(fBHTN_NSD),0) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002%') thubhtn

	update TBL_THU_BHTN set fTongSo = (select sum(fTongSo) from TBL_THU_BHTN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHTN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHTN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT quân nhân
	select * into TBL_THU_BHYT_QN from
	(select 7 rowNum, 1 bHangCha, '3' stt, N'Thu BHYT quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 8 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, ROUND((sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))),0) fTongSo, ROUND(sum(fBHYT_NLD),0) fNLD, ROUND(sum(fBHYT_NSD),0) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0001%'
	union all
	select 9 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, ROUND((sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))),0) fTongSo, ROUND(sum(fBHYT_NLD),0) fNLD, ROUND(sum(fBHYT_NSD),0) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002-010-011-0001%') thubhytquannhan

	update TBL_THU_BHYT_QN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_QN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_NLD from
	(select 10 rowNum, 1 bHangCha, '4' stt, N'Thu BHYT người lao động' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 11 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, ROUND((sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))),0) fTongSo, ROUND(sum(fBHYT_NLD),0) fNLD, ROUND(sum(fBHYT_NSD),0) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0002%'
	union all
	select 12 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, ROUND((sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))),0) fTongSo, ROUND(sum(fBHYT_NLD),0) fNLD, ROUND(sum(fBHYT_NSD),0) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002-010-011-0002%') thubhytnld

	update TBL_THU_BHYT_NLD set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_NLD where bHangCha = 0)
						where bHangCha = 1
	
	--Dữ liệu phân bổ dự toán thu mua BHYT
	select ctct.* into TBL_DTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	join  BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_TN from
	(select 13 rowNum, 1 bHangCha, '5' stt, N'Thu BHYT thân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN' sLoai
	union all
	select 14 rowNum, 1 bHangCha, 'a' stt, N'Quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	union all
	select 15 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, ROUND(sum(fDuToan),0) fTongSo, null fNLD, ROUND(sum(fDuToan),0) fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0001%'
	union all
	select 16 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, ROUND(sum(fDuToan),0) fTongSo, null fNLD, ROUND(sum(fDuToan),0) fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0002%'
	union all
	select 17 rowNum, 1 bHangCha, 'b' stt, N'Công nhân, VCQP' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	union all
	select 18 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, ROUND(sum(fDuToan),0) fTongSo, null fNLD, ROUND(sum(fDuToan),0) fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where sXauNoiMa like '9030002-010-011-0000%'
	union all
	select 19 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, ROUND(sum(fDuToan),0) fTongSo, null fNLD, ROUND(sum(fDuToan),0) fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where sXauNoiMa like '9030002-010-011-0001%'
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]    Script Date: 4/23/2024 9:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		5 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
		left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
		6 STT,
		N'6' MaSo,
		N'- Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		6 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi
	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all
	select
		8 STT,
		N'8' MaSo,
		N'- Người lao động đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		8 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
		9 STT,
		N'9' MaSo,
		N'- Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		N'HT' Khoi,
		N'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		9 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		N'HT' Khoi,
		N'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toánand dv.iTrangThai = 1
	group by dv.sTenDonVi
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union all
	select
		16 STT,
		N'16' MaSo,
		N'- BHYT quân nhân' NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in
		(
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002'  -- HSQ, BS
		)

	union all
	select
		16 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in (
		'9020001-010-011-0001-0000',  -- Sĩ quan
		'9020001-010-011-0001-0001'	, -- QNCN
		'9020001-010-011-0001-0002')  -- HSQ, BS
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
		18 STT,
		N'18' MaSo,
		N'+ Người lao động đóng' NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)

	union all
	select
		18 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in (
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001')  -- LĐHĐ
	and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
		19 STT,
		N'19' MaSo,
		N'+ Người sử dụng LĐ đóng' NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in
		(
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001'  -- LĐHĐ
		)

	union all
	select
		19 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and ctct.sXauNoiMa in (
		'9020001-010-011-0002-0000',  -- CC,CN, VCQP
		'9020001-010-011-0002-0001')  -- LĐHĐ
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	-- Khối hạch toán
	union all
	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union all
	select
		21 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
		(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
		(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
		((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'QN' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and ctct.sXauNoiMa in (
		'9020002-010-011-0001-0000',  -- Sĩ quan
		'9020002-010-011-0001-0001'	, -- QNCN
		'9020002-010-011-0001-0002')  -- HSQ, BS
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		23 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and ctct.sXauNoiMa in (
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001')  -- LĐHĐ
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		24 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and ctct.sXauNoiMa in (
		'9020002-010-011-0002-0000',  -- CC,CN, VCQP
		'9020002-010-011-0002-0001')  -- LĐHĐ
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	--BHTN
	--Lấy dữ liệu khối dự toán
	union all
	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		29 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		30 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020001' -- Khối dự toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	--Lấy dữ liệu khối hạch toán
	union all
	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		32 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi

	union all
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		33 STT,
		'' MaSo,
		'    ' + dv.sTenDonVi NoiDung,
		isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
		(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	left join DonVi dv on ctct.iID_MaDonVi = dv.iID_MaDonVi and ctct.iNamLamViec = dv.iNamLamViec
	where 
		ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sLNS = '9020002' -- Khối hạch toán
		and dv.iTrangThai = 1
	group by dv.sTenDonVi
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)
	union all
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)
	union all
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)
	union all
	select
	7 STT,
	N'7=8+9' MaSo,
	N'a) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)
	--BHYT
	union all
	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18, 19)

	union all
	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23, 24)
	--BHTN
	union all
	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	28 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	31 STT,
	N'31=32+33' MaSo,
	N'a) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union all
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	and MaSo in ('4=5+6', '7=8+9')
	union all
	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	and MaSo in ('28=29+330', '31=32+33')

	union all
	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	union all
	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (16, 17) and MaSo in ('16', '17=18+19')
	union all
	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	0 BHangCha
	from tbl_child_cat
	where STT in (21, 22) and MaSo in ('21', '22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT = 16 and MaSo = 16),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT = 16 and MaSo = 16),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT = 16 and MaSo = 16),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15 and MaSo = '15=16+17'

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15 and MaSo = '15=16+17'

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT = 21 and MaSo = 21),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT = 21 and MaSo = 21),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT = 21 and MaSo = 21),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20 and MaSo = '20=21+22'

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20 and MaSo = '20=21+22'

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union all
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union all
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha 
	from tbl_ddt_bhxh
	order by STT

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 4/23/2024 9:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union

	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union

	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union

	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	-- Khối hạch toán
	union

	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0) - isnull(sum(pbct.fBHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fBHYT_NLD), 0) + isnull(sum(pbct.fBHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	--BHTN
	--Lấy dữ liệu khối dự toán
	union

	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union

	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union

	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fBHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fBHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu pb on ctct.iNamLamViec = pb.iNamLamViec and pb.bKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_PhanBo_ChungTuChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH_PhanBo_ChungTu = pbct.iID_DTT_BHXH_ChungTu and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'

	union

	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'

	union

	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'

	union

	select
	7 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'

	--BHYT
	union

	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'

	union

	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'

	union

	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'

	union

	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18,19)

	union

	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23,24)

	--BHTN
	union

	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'

	union

	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'

	union

	select
	28 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'

	union

	select
	31 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child

	union

	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'

	union

	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'

	union

	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'

	union

	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (16, 17)

	union

	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]    Script Date: 4/23/2024 9:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union

	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union

	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NLD), 0) - (isnull(sum(ctct.fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union

	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHXH_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHXH_NSD), 0) - (isnull(sum(ctct.fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	-- Khối hạch toán
	union

	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) DttDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0) - isnull(sum(pbct.fThu_BHYT_NSD), 0))) Tang,
	((isnull(sum(pbct.fThu_BHYT_NLD), 0) + isnull(sum(pbct.fThu_BHYT_NSD), 0)) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)

	union

	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NLD), 0) - (isnull(sum(ctct.fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union

	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHYT_NSD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHYT_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHYT_NSD), 0) - (isnull(sum(ctct.fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	--BHTN
	--Lấy dữ liệu khối dự toán
	union

	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union

	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union

	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NLD), 0) DttDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NLD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NLD), 0) - (isnull(sum(ctct.fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(pbct.fThu_BHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(pbct.fThu_BHTN_NSD), 0)) Tang,
	(isnull(sum(pbct.fThu_BHTN_NSD), 0) - (isnull(sum(ctct.fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(ctct.fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	join BH_DTT_BHXH_ChungTu pb on  ctct.iNamLamViec = pb.iNamLamViec and pb.bIsKhoa = 1 and pb.sSoQuyetDinh IS NOT NULL and pb.sSoQuyetDinh <> '' and cast(pb.DngayChungTu as date) <= cast(GETDATE() as date)
	join BH_DTT_BHXH_ChungTu_ChiTiet pbct ON ctct.iID_MaDonVi = pbct.iID_MaDonVi and pb.iID_DTT_BHXH = pbct.iID_DTT_BHXH and ctct.sXauNoiMa = pbct.sXauNoiMa
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'

	union

	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'

	union

	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'

	union

	select
	7 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'

	--BHYT
	union

	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'

	union

	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'

	union

	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'

	union

	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18,19)

	union

	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23,24)

	--BHTN
	union

	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'

	union

	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'

	union

	select
	28 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'

	union

	select
	31 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child

	union

	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'

	union

	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'

	union

	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'

	union

	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (16, 17)

	union

	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
;
GO
