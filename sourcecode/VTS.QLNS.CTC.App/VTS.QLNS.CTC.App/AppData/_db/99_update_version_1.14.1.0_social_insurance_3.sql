/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh]    Script Date: 3/5/2024 8:40:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh] 
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
	
	declare @SoQuyetToanThang FLOAT = (
	select sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0))
	from BH_QTT_BHXH_ChungTu_ChiTiet ctct
	join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
	where ct.iNamLamViec = @NamLamViec
		and ct.iID_MaDonVi = @IdDonVi
		and ct.iQuyNam in (1, 2, 3, 4, 5, 6) and ct.iQuyNamLoai = 0 
		and ct.bIsKhoa = 1 )

	if @SoQuyetToanThang <> 0
		select
			ctct.sXauNoiMa,
			ctct.iID_MaDonVi,
			sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThuBHXH_NLD_QTDauNam,
			sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThuBHXH_NSD_QTDauNam,
			sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThuBHYT_NLD_QTDauNam,
			sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThuBHYT_NSD_QTDauNam,
			sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThuBHTN_NLD_QTDauNam,
			sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThuBHTN_NSD_QTDauNam
		from BH_QTT_BHXH_ChungTu_ChiTiet ctct
		join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
		where ct.iNamLamViec = @NamLamViec
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iQuyNam in (1, 2, 3, 4, 5, 6) and ct.iQuyNamLoai = 0 -- 6 thang dau nam
			and ct.bIsKhoa = 1
		group by ctct.sXauNoiMa, ctct.iID_MaDonVi
	else
		select
			ctct.sXauNoiMa,
			ctct.iID_MaDonVi,
			sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThuBHXH_NLD_QTDauNam,
			sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThuBHXH_NSD_QTDauNam,
			sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThuBHYT_NLD_QTDauNam,
			sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThuBHYT_NSD_QTDauNam,
			sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThuBHTN_NLD_QTDauNam,
			sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThuBHTN_NSD_QTDauNam
		from BH_QTT_BHXH_ChungTu_ChiTiet ctct
		join BH_QTT_BHXH_ChungTu ct on ctct.iID_QTT_BHXH_ChungTu = ct.iID_QTT_BHXH_ChungTu
		where ct.iNamLamViec = @NamLamViec
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iQuyNam in (3, 6) and ct.iQuyNamLoai = 1 -- Quy I, II
			and ct.bIsKhoa = 1
		group by ctct.sXauNoiMa, ctct.iID_MaDonVi

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 3/5/2024 8:40:11 AM ******/
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
			Sum(ctct.fThu_BHXH_NLD),
			Sum(ctct.fThu_BHXH_NSD),
			Sum(ctct.fTongSoPhaiThuBHXH),
			Sum(ctct.fThu_BHYT_NLD),
			Sum(ctct.fThu_BHYT_NSD),
			Sum(ctct.fTongSoPhaiThuBHYT),
			Sum(ctct.fThu_BHTN_NLD),
			Sum(ctct.fThu_BHTN_NSD),
			Sum(ctct.fTongSoPhaiThuBHTN),
			Sum(ctct.fTongCong),
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]
	@YearOfWork int,
	@LoaiTongHop int,
	@UserName nvarchar(100),
	@QuyNam int,
	@LoaiQuyNam int
AS
BEGIN
	DECLARE @CountDonViCha int;

	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT
		qtt.iID_QTT_BHXH_ChungTu,
		qtt.sSoChungTu,
		qtt.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	INTO #tblChungTuQTT
	FROM BH_QTT_BHXH_ChungTu qtt
		LEFT JOIN DonVi donvi
		ON qtt.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @YearOfWork
		AND qtt.iNamLamViec = @YearOfWork
		AND qtt.iLoaiTongHop = @LoaiTongHop
		AND qtt.iQuyNam = @QuyNam
		AND qtt.iQuyNamLoai = @LoaiQuyNam

	IF @CountDonViCha = 0
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND ct.bIsKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #tblChungTuQTT;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop_thang]
	@YearOfWork int,
	@LoaiTongHop int,
	@UserName nvarchar(100),
	@QuyNam int,
	@LoaiQuyNam int
AS
BEGIN
	DECLARE @CountDonViCha int;
	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@QuyNam = 3 and @LoaiQuyNam = 1) SET @SMonths = '1,2,3'
	IF (@QuyNam = 6 and @LoaiQuyNam = 1) SET @SMonths = '4,5,6'
	IF (@QuyNam = 9 and @LoaiQuyNam = 1) SET @SMonths = '7,8,9'
	IF (@QuyNam = 12 and @LoaiQuyNam = 1) SET @SMonths = '10,11,12'

	IF @LoaiQuyNam = 0 SET @SMonths = @QuyNam SET @SLoaiQuy = @LoaiQuyNam
	IF @LoaiQuyNam = 1 SET @SLoaiQuy = '0,1'

	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT
		qtt.iID_QTT_BHXH_ChungTu,
		qtt.sSoChungTu,
		qtt.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	INTO #tblChungTuQTT
	FROM BH_QTT_BHXH_ChungTu qtt
		LEFT JOIN DonVi donvi
		ON qtt.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @YearOfWork
		AND qtt.iNamLamViec = @YearOfWork
		AND qtt.iLoaiTongHop = @LoaiTongHop
		AND qtt.iQuyNam in (SELECT * FROM f_split(@SMonths))
		AND qtt.iQuyNamLoai in (SELECT * FROM f_split(@SLoaiQuy))

	IF @CountDonViCha = 0
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND ct.bIsKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #tblChungTuQTT;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh]
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	select
	mlns.iID_MLGT,
	mlns.iSTT,
	mlns.sMoTa,
	mlns.sNoiDung,
	mlns.iLoai,
	chungtudonvi.fSoPhaiThuNop,
	chungtudonvi.fSoDaNopTrongNam,
	chungtudonvi.fSoDaNopSau31_12,
	chungtudonvi.fTongSoDaNop,
	chungtudonvi.fSoConPhaiNop,
	chungtudonvi.iQuanSo,
	chungtudonvi.fQuyTienLuongCanCu,
	chungtudonvi.fSoTienGiamDong,
	chungtudonvi.dTuNgay,
	chungtudonvi.dDenNgay
	from
		(select
			iSTT,
			sNoiDung,
			iID_MLGT,
			concat(iSTT, '. ' , sNoiDung) sMoTa,
			iLoai
		from BH_QTT_MucLucGiaiThich) mlns
		left join
			(select distinct
				ctgt.iID_QT_CTCT_GiaiThich,
				ctgt.iID_QTT_BHXH_ChungTu,
				ctgt.sNguoiTao,
				ctgt.sNguoiSua,
				ctgt.dNgayTao,
				ctgt.dNgaySua,
				ctgt.iID_MaDonVi,
				ctgt.iNamLamViec,
				ctgt.iQuyNam,
				ctgt.iQuyNamLoai,
				ctgt.sQuyNamMoTa,
				ctgt.iID_MLNS,
				ctgt.sNoiDung,
				ctgt.fSoPhaiThuNop,
				ctgt.fSoDaNopTrongNam,
				ctgt.fSoDaNopSau31_12,
				ctgt.fTongSoDaNop,
				ctgt.fSoConPhaiNop,
				ctgt.iQuanSo,
				ctgt.fQuyTienLuongCanCu,
				ctgt.fSoTienGiamDong,
				ctgt.dTuNgay,
				ctgt.dDenNgay,
				ctgt.iLoaiGiaiThich
			from BH_QTT_BHXH_CTCT_GiaiThich ctgt
			where ctgt.iQuyNam = @NamLamViec
			and ctgt.iLoaiGiaiThich = 3
			and ctgt.iID_MaDonVi = @MaDonVi) chungtudonvi
		on mlns.iID_MLGT = chungtudonvi.iID_MLNS
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_truy_thu]
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dutoan_tbl]') AND type in (N'U'))
	drop table dutoan_tbl
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hachtoan_tbl]') AND type in (N'U'))
	drop table hachtoan_tbl

	--Tạo dữ liệu khói dự toán
	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sMoTa,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sXauNoiMa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		chungtudonvi.fTruyThu_BHXH_NLD,
		chungtudonvi.fTruyThu_BHXH_NSD,
		chungtudonvi.fTruyThu_BHXH_TongCong,
		chungtudonvi.fTruyThu_BHYT_NLD,
		chungtudonvi.fTruyThu_BHYT_NSD,
		chungtudonvi.fTruyThu_BHYT_TongCong,
		chungtudonvi.fTruyThu_BHTN_NLD,
		chungtudonvi.fTruyThu_BHTN_NSD,
		chungtudonvi.fTruyThu_BHTN_TongCong
	into dutoan_tbl
	from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS = '9020001' -- Khối dự toán
		and iNamLamViec = @NamLamViec
		and iID_MLNS_Cha is not null) mlns
		left join
		(select distinct
			ctgt.iID_QT_CTCT_GiaiThich,
			ctgt.iID_QTT_BHXH_ChungTu,
			ctgt.sNguoiTao,
			ctgt.sNguoiSua,
			ctgt.dNgayTao,
			ctgt.dNgaySua,
			ctgt.iID_MaDonVi,
			ctgt.iNamLamViec,
			ctgt.iQuyNam,
			ctgt.iQuyNamLoai,
			ctgt.sQuyNamMoTa,
			ctgt.sNoiDung,
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.iID_MLNS
		from BH_QTT_BHXH_CTCT_GiaiThich ctgt
		where ctgt.iLoaiGiaiThich = 2
			and ctgt.iQuyNam = @NamLamViec
			and ctgt.iID_MaDonVi = @MaDonVi) chungtudonvi 
	on mlns.iID_MLNS = chungtudonvi.iID_MLNS
---------
--Tạo dữ liệu khói hạch toán
	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sMoTa,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sXauNoiMa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		chungtudonvi.fTruyThu_BHXH_NLD,
		chungtudonvi.fTruyThu_BHXH_NSD,
		chungtudonvi.fTruyThu_BHXH_TongCong,
		chungtudonvi.fTruyThu_BHYT_NLD,
		chungtudonvi.fTruyThu_BHYT_NSD,
		chungtudonvi.fTruyThu_BHYT_TongCong,
		chungtudonvi.fTruyThu_BHTN_NLD,
		chungtudonvi.fTruyThu_BHTN_NSD,
		chungtudonvi.fTruyThu_BHTN_TongCong
	into hachtoan_tbl
	from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS = '9020002' -- Khối hạch toán
		and iNamLamViec = @NamLamViec
		and iID_MLNS_Cha is not null) mlns
		left join
		(select distinct
			ctgt.iID_QT_CTCT_GiaiThich,
			ctgt.iID_QTT_BHXH_ChungTu,
			ctgt.sNguoiTao,
			ctgt.sNguoiSua,
			ctgt.dNgayTao,
			ctgt.dNgaySua,
			ctgt.iID_MaDonVi,
			ctgt.iNamLamViec,
			ctgt.iQuyNam,
			ctgt.iQuyNamLoai,
			ctgt.sQuyNamMoTa,
			ctgt.sNoiDung,
			ctgt.fTruyThu_BHXH_NLD,
			ctgt.fTruyThu_BHXH_NSD,
			ctgt.fTruyThu_BHXH_TongCong,
			ctgt.fTruyThu_BHYT_NLD,
			ctgt.fTruyThu_BHYT_NSD,
			ctgt.fTruyThu_BHYT_TongCong,
			ctgt.fTruyThu_BHTN_NLD,
			ctgt.fTruyThu_BHTN_NSD,
			ctgt.fTruyThu_BHTN_TongCong,
			ctgt.iID_MLNS
		from BH_QTT_BHXH_CTCT_GiaiThich ctgt
		where ctgt.iLoaiGiaiThich = 2
			and ctgt.iQuyNam = @NamLamViec
			and ctgt.iID_MaDonVi = @MaDonVi) chungtudonvi 
	on mlns.iID_MLNS = chungtudonvi.iID_MLNS
------------
	select
		dt.iID_MLNS,
		dt.iID_MLNS_Cha,
		dt.sMoTa,
		dt.bHangCha,
		dt.sLNS,
		dt.sXauNoiMa,
		isnull(dt.fTruyThu_BHXH_NLD, 0) fTruyThu_BHXH_NLD_DT,
		isnull(dt.fTruyThu_BHXH_NSD, 0) fTruyThu_BHXH_NSD_DT,
		isnull(dt.fTruyThu_BHXH_TongCong, 0) fTruyThu_BHXH_TongCong_DT,
		isnull(dt.fTruyThu_BHYT_NLD, 0) fTruyThu_BHYT_NLD_DT,
		isnull(dt.fTruyThu_BHYT_NSD, 0) fTruyThu_BHYT_NSD_DT,
		isnull(dt.fTruyThu_BHYT_TongCong, 0) fTruyThu_BHYT_TongCong_DT,
		isnull(dt.fTruyThu_BHTN_NLD, 0) fTruyThu_BHTN_NLD_DT,
		isnull(dt.fTruyThu_BHTN_NSD, 0) fTruyThu_BHTN_NSD_DT,
		isnull(dt.fTruyThu_BHTN_TongCong, 0) fTruyThu_BHTN_TongCong_DT,
		
		isnull(ht.fTruyThu_BHXH_NLD, 0) fTruyThu_BHXH_NLD_HT,
		isnull(ht.fTruyThu_BHXH_NSD, 0) fTruyThu_BHXH_NSD_HT,
		isnull(ht.fTruyThu_BHXH_TongCong, 0) fTruyThu_BHXH_TongCong_HT,
		isnull(ht.fTruyThu_BHYT_NLD, 0) fTruyThu_BHYT_NLD_HT,
		isnull(ht.fTruyThu_BHYT_NSD, 0) fTruyThu_BHYT_NSD_HT,
		isnull(ht.fTruyThu_BHYT_TongCong, 0) fTruyThu_BHYT_TongCong_HT,
		isnull(ht.fTruyThu_BHTN_NLD, 0) fTruyThu_BHTN_NLD_HT,
		isnull(ht.fTruyThu_BHTN_NSD, 0) fTruyThu_BHTN_NSD_HT,
		isnull(ht.fTruyThu_BHTN_TongCong, 0) fTruyThu_BHTN_TongCong_HT,

		(isnull(dt.fTruyThu_BHXH_TongCong, 0) + isnull(ht.fTruyThu_BHXH_TongCong, 0)) fTong_TruyThu_BHXH,
		(isnull(dt.fTruyThu_BHYT_TongCong, 0) + isnull(ht.fTruyThu_BHYT_TongCong, 0)) fTong_TruyThu_BHYT,
		(isnull(dt.fTruyThu_BHTN_TongCong, 0) + isnull(ht.fTruyThu_BHTN_TongCong, 0)) fTong_TruyThu_BHTN,

		(isnull(dt.fTruyThu_BHXH_TongCong, 0) + isnull(ht.fTruyThu_BHXH_TongCong, 0) + isnull(dt.fTruyThu_BHYT_TongCong, 0) + isnull(ht.fTruyThu_BHYT_TongCong, 0) + isnull(dt.fTruyThu_BHTN_TongCong, 0) + isnull(ht.fTruyThu_BHTN_TongCong, 0)) fTong_TruyThu

	from
		dutoan_tbl dt left join
		hachtoan_tbl ht on dt.iID_MLNS = ht.iID_MLNS
	order by dt.sXauNoiMa
--------

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			  ,ctct.iID_QTT_BHXH_ChungTu
			  ,ctct.iQSBQNam
			  ,ctct.fLuongChinh
			  ,ctct.fPCChucVu
			  ,ctct.fPCTNNghe
			  ,ctct.fPCTNVuotKhung
			  ,ctct.fNghiOm
			  ,ctct.fHSBL
			  ,ctct.fTongQTLN
			  ,ctct.fDuToan
			  ,ctct.fDaQuyetToan
			  ,ctct.fConLai
			  ,ctct.fThu_BHXH_NLD
			  ,ctct.fThu_BHXH_NSD
			  ,ctct.fTongSoPhaiThuBHXH
			  ,ctct.fThu_BHYT_NLD
			  ,ctct.fThu_BHYT_NSD
			  ,ctct.fTongSoPhaiThuBHYT
			  ,ctct.fThu_BHTN_NLD
			  ,ctct.fThu_BHTN_NSD
			  ,ctct.fTongSoPhaiThuBHTN
			  ,ctct.fTongCong
			  ,ctct.sGhiChu
			  ,ctct.iID_MLNS
			  ,ctct.iID_MLNS_Cha
			  ,ctct.sXauNoiMa
			  ,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@IQuy = 3) SET @SMonths = '1,2,3'
	IF (@IQuy = 6) SET @SMonths = '4,5,6'
	IF (@IQuy = 9) SET @SMonths = '7,8,9'
	IF (@IQuy = 12) SET @SMonths = '10,11,12'

	select
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0))/3 iQSBQNam
			,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
			,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
			,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
			,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
			,sum(isnull(ctct.fHSBL, 0)) fHSBL
			,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
			,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
			,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
			,sum(isnull(ctct.fTongCong, 0)) fTongCong
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			group by
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sXauNoiMa,
			ctct.sLNS
			) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int  
AS
BEGIN
DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr 	where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit, 
sXauNoiMa nvarchar(200), 
sMoTa nvarchar(200), 
iNamLamViec int,
iQSBQNam int,
fLuongChinh float,
fPhuCapChucVu float,
fPCTNNghe float,
fPCTNVuotKhung float,
fNghiOm float,
fHSBL float,
fTongQTLN float,
fDuToan float,
fDaQuyetToan float,
fConLai float,
fThu_BHXH_NLD float,
fThu_BHXH_NSD float,
fTongSoPhaiThuBHXH float,
fThu_BHYT_NLD float,
fThu_BHYT_NSD float,
fTongSoPhaiThuBHYT float,
fThu_BHTN_NLD float,
fThu_BHTN_NSD float,
fTongSoPhaiThuBHTN float,
fTongNLD float,
fTongNSD float,
fTongCong float,
MaDonVi nvarchar(50),
TenDonVi nvarchar(50)
);

----------------END DETAIL AGENCY----------------
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu fPhuCapChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO #tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 1;

----------------END DETAIL----------------
----------------INSERT TOTAL----------------
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD) fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD) fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong,
		null,
		null
		FROM
			(select
				BH_DM_MucLucNganSach.iID_MLNS,
				BH_DM_MucLucNganSach.iID_MLNS_Cha,
				BH_DM_MucLucNganSach.bHangCha,
				BH_DM_MucLucNganSach.sLNS,
				BH_DM_MucLucNganSach.sL,
				BH_DM_MucLucNganSach.sK,
				BH_DM_MucLucNganSach.sM,
				BH_DM_MucLucNganSach.sTM,
				BH_DM_MucLucNganSach.sTTM,
				BH_DM_MucLucNganSach.sNG,
				BH_DM_MucLucNganSach.sTNG,
				BH_DM_MucLucNganSach.sXauNoiMa,
				BH_DM_MucLucNganSach.sMoTa
			from BH_DM_MucLucNganSach 
			where sLNS like '902%'
			AND iNamLamViec = @NamLamViec			
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		LEFT JOIN(
			select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVis
			and ct.iLoaiTongHop = 2
--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end			
		)chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		ORDER BY mlns.sXauNoiMa;


----------------END INSERT DETAIL----------------
----------------INSERT DETAIL----------------
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
SELECT
NULL ,
NULL ,
0 bHangCha , 
sXauNoiMa , 
dv.sTenDonVi,
#tempChiTietDonVi.iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPCChucVu fPhuCapChucVu,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
0 fTongNLD ,
0 fTongNSD ,
fTongCong ,
dv.iID_MaDonVi, 
dv.sTenDonVi as TenDonVi 
FROM #tempChiTietDonVi 
LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

DROP TABLE #tempChiTietDonVi;
DROP TABLE #result;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr 	where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iQuyNamLoai = @ILoaiQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit, 
sXauNoiMa nvarchar(200), 
sMoTa nvarchar(200), 
iNamLamViec int,
iQSBQNam int,
fLuongChinh float,
fPhuCapChucVu float,
fPCTNNghe float,
fPCTNVuotKhung float,
fNghiOm float,
fHSBL float,
fTongQTLN float,
fDuToan float,
fDaQuyetToan float,
fConLai float,
fThu_BHXH_NLD float,
fThu_BHXH_NSD float,
fTongSoPhaiThuBHXH float,
fThu_BHYT_NLD float,
fThu_BHYT_NSD float,
fTongSoPhaiThuBHYT float,
fThu_BHTN_NLD float,
fThu_BHTN_NSD float,
fTongSoPhaiThuBHTN float,
fTongNLD float,
fTongNSD float,
fTongCong float,
MaDonVi nvarchar(50),
TenDonVi nvarchar(50)
);

----------------END DETAIL AGENCY----------------
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO #tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 1;

----------------END DETAIL----------------
----------------INSERT TOTAL----------------
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD) fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD) fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong,
		null,
		null
		FROM
			(select
				BH_DM_MucLucNganSach.iID_MLNS,
				BH_DM_MucLucNganSach.iID_MLNS_Cha,
				BH_DM_MucLucNganSach.bHangCha,
				BH_DM_MucLucNganSach.sLNS,
				BH_DM_MucLucNganSach.sL,
				BH_DM_MucLucNganSach.sK,
				BH_DM_MucLucNganSach.sM,
				BH_DM_MucLucNganSach.sTM,
				BH_DM_MucLucNganSach.sTTM,
				BH_DM_MucLucNganSach.sNG,
				BH_DM_MucLucNganSach.sTNG,
				BH_DM_MucLucNganSach.sXauNoiMa,
				BH_DM_MucLucNganSach.sMoTa
			from BH_DM_MucLucNganSach 
			where sLNS like '902%'
			AND iNamLamViec = @NamLamViec) mlns
		LEFT JOIN(
			select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVis
			and ct.iLoaiTongHop = 2
--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end			
		)chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		ORDER BY mlns.sXauNoiMa;


----------------END INSERT DETAIL----------------
----------------INSERT DETAIL----------------
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
SELECT
NULL ,
NULL ,
0 bHangCha , 
sXauNoiMa , 
dv.sTenDonVi,
#tempChiTietDonVi.iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPCChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
0 fTongNLD ,
0 fTongNSD ,
fTongCong ,
dv.iID_MaDonVi, 
dv.sTenDonVi as TenDonVi 
FROM #tempChiTietDonVi 
LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

DROP TABLE #tempChiTietDonVi;
DROP TABLE #result;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) SET @SMonths = '1,2,3'
	IF (@IQuy = 6) SET @SMonths = '4,5,6'
	IF (@IQuy = 9) SET @SMonths = '7,8,9'
	IF (@IQuy = 12) SET @SMonths = '10,11,12'

	DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr where pr.iNamLamViec = @NamLamViec
																			and pr.iQuyNam in (SELECT * FROM f_split(@SMonths))
																			and pr.iQuyNamLoai = @ILoaiQuy
																			and pr.iID_MaDonVi = @IdDonVis
																			and pr.iLoaiTongHop = 2
																			and pr.bDaTongHop = 0)

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	----------------END DETAIL AGENCY----------------
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam/3 iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO #tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
				and ct.iLoaiTongHop = 1
				and ct.bDaTongHop = 1;

	----------------END DETAIL----------------
	----------------INSERT TOTAL----------------
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(isnull(chungtudonvi.iQSBQNam, 0)) iQSBQNam,
			sum(isnull(chungtudonvi.fLuongChinh, 0))/@Donvitinh fLuongChinh,
			sum(isnull(chungtudonvi.fPCChucVu, 0))/@Donvitinh fPCChucVu,
			sum(isnull(chungtudonvi.fPCTNNghe, 0))/@Donvitinh fPCTNNghe,
			sum(isnull(chungtudonvi.fPCTNVuotKhung, 0))/@Donvitinh fPCTNVuotKhung,
			sum(isnull(chungtudonvi.fNghiOm, 0))/@Donvitinh fNghiOm,
			sum(isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fHSBL,
			sum(isnull((chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL), 0))/@Donvitinh fTongQTLN,
			sum(isnull(chungtudonvi.fDuToan, 0))/@Donvitinh fDuToan,
			sum(isnull(chungtudonvi.fDaQuyetToan, 0))/@Donvitinh fDaQuyetToan,
			sum(isnull(chungtudonvi.fConLai, 0))/@Donvitinh fConLai,
			sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0))/@Donvitinh fThu_BHXH_NLD,
			sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fThu_BHXH_NSD,
			sum(isnull((chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD), 0))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0))/@Donvitinh fThu_BHYT_NLD,
			sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fThu_BHYT_NSD,
			sum(isnull((chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD), 0))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))/@Donvitinh fThu_BHTN_NLD,
			sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fThu_BHTN_NSD,
			sum(isnull((chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD), 0))/@Donvitinh fTongSoPhaiThuBHTN,
			sum(isnull((chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD), 0))/@Donvitinh fTongNLD,
			sum(isnull((chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD), 0))/@Donvitinh fTongNSD,
			sum(isnull((chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD), 0))/@Donvitinh fTongCong,
			null,
			null
			FROM
				(select
					BH_DM_MucLucNganSach.iID_MLNS,
					BH_DM_MucLucNganSach.iID_MLNS_Cha,
					BH_DM_MucLucNganSach.bHangCha,
					BH_DM_MucLucNganSach.sLNS,
					BH_DM_MucLucNganSach.sL,
					BH_DM_MucLucNganSach.sK,
					BH_DM_MucLucNganSach.sM,
					BH_DM_MucLucNganSach.sTM,
					BH_DM_MucLucNganSach.sTTM,
					BH_DM_MucLucNganSach.sNG,
					BH_DM_MucLucNganSach.sTNG,
					BH_DM_MucLucNganSach.sXauNoiMa,
					BH_DM_MucLucNganSach.sMoTa
				from BH_DM_MucLucNganSach 
				where sLNS like '902%'
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN(
				select
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.iQSBQNam/3 iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				and ct.iID_MaDonVi = @IdDonVis
				and ct.iLoaiTongHop = 2	
			)chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				group by
					mlns.iID_MLNS,
					mlns.iID_MLNS_Cha,
					mlns.bHangCha,
					mlns.sXauNoiMa,
					mlns.sMoTa
				ORDER BY mlns.sXauNoiMa;


	----------------END INSERT DETAIL----------------
	----------------INSERT DETAIL----------------
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	SELECT
		NULL ,
		NULL ,
		0 bHangCha , 
		dt.sXauNoiMa , 
		dv.sTenDonVi,
		dt.iNamLamViec ,
		sum(isnull(dt.iQSBQNam, 0)) iQSBQNam,
		sum(isnull(dt.fLuongChinh, 0)) fLuongChinh,
		sum(isnull(dt.fPCChucVu, 0)) fPCChucVu,
		sum(isnull(dt.fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(dt.fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(dt.fNghiOm, 0)) fNghiOm,
		sum(isnull(dt.fHSBL, 0)) fHSBL,
		sum(isnull(dt.fTongQTLN, 0)) fTongQTLN,
		sum(isnull(dt.fDuToan, 0)) fDuToan,
		sum(isnull(dt.fDaQuyetToan, 0)) fDaQuyetToan,
		sum(isnull(dt.fConLai, 0)) fConLai,
		sum(isnull(dt.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(dt.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH,
		sum(isnull(dt.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(dt.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT,
		sum(isnull(dt.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(dt.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN,
		0 fTongNLD ,
		0 fTongNSD ,
		sum(isnull(dt.fTongCong, 0)) ,
		dv.iID_MaDonVi, 
		dv.sTenDonVi as TenDonVi 
	FROM #tempChiTietDonVi dt
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = dt.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
	group by
		dt.sXauNoiMa, 
		dv.sTenDonVi,
		dt.iNamLamViec,
		dv.iID_MaDonVi, 
		dv.sTenDonVi;

	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

	DROP TABLE #tempChiTietDonVi;
	DROP TABLE #result;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int
AS
BEGIN
CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit, 
sXauNoiMa nvarchar(200), 
sMoTa nvarchar(200), 
iNamLamViec int,
iQSBQNam int,
fLuongChinh float,
fPhuCapChucVu float,
fPCTNNghe float,
fPCTNVuotKhung float,
fNghiOm float,
fHSBL float,
fTongQTLN float,
fDuToan float,
fDaQuyetToan float,
fConLai float,
fThu_BHXH_NLD float,
fThu_BHXH_NSD float,
fTongSoPhaiThuBHXH float,
fThu_BHYT_NLD float,
fThu_BHYT_NSD float,
fTongSoPhaiThuBHYT float,
fThu_BHTN_NLD float,
fThu_BHTN_NSD float,
fTongSoPhaiThuBHTN float,
fTongNLD float,
fTongNSD float,
fTongCong float,
MaDonVi nvarchar(50),
TenDonVi nvarchar(50)
);

--- GET CHI TIẾT ĐƠN VỊ
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO #tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = 0;
--END chi tiet

INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
--INSERT TOTAL
select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
		sum(chungtudonvi.fPCChucVu)/@Donvitinh fPCChucVu,
		sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
		sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
		sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
		sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
		(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
		sum(chungtudonvi.fDuToan) fDuToan,
		sum(chungtudonvi.fDaQuyetToan) fDaQuyetToan,
		sum(chungtudonvi.fConLai) fConLai,
		sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
		sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
		sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
		sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
		(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
		sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
		sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
		(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))) fTongNLD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))) fTongNSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
		null,
		null
		FROM
			(select
				BH_DM_MucLucNganSach.iID_MLNS,
				BH_DM_MucLucNganSach.iID_MLNS_Cha,
				BH_DM_MucLucNganSach.bHangCha,
				BH_DM_MucLucNganSach.sLNS,
				BH_DM_MucLucNganSach.sL,
				BH_DM_MucLucNganSach.sK,
				BH_DM_MucLucNganSach.sM,
				BH_DM_MucLucNganSach.sTM,
				BH_DM_MucLucNganSach.sTTM,
				BH_DM_MucLucNganSach.sNG,
				BH_DM_MucLucNganSach.sTNG,
				BH_DM_MucLucNganSach.sXauNoiMa,
				BH_DM_MucLucNganSach.sMoTa
			from BH_DM_MucLucNganSach 
			where sLNS like '902%'
			AND iNamLamViec = @NamLamViec
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa

			) mlns

		LEFT JOIN #tempChiTietDonVi chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		GROUP BY
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa;
	--INSERT CHI TIẾT	
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
SELECT
NULL ,
NULL ,
0 bHangCha , 
sXauNoiMa , 
dv.sTenDonVi,
#tempChiTietDonVi.iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPCChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
0 fTongNLD ,
0 fTongNSD ,
fTongCong ,
dv.iID_MaDonVi, 
dv.sTenDonVi as TenDonVi 
FROM #tempChiTietDonVi 
LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;
DROP TABLE #tempChiTietDonVi;
DROP TABLE #result;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	--- GET CHI TIẾT ĐƠN VỊ
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam
				,ctct.fLuongChinh
				,ctct.fPCChucVu
				,ctct.fPCTNNghe
				,ctct.fPCTNVuotKhung
				,ctct.fNghiOm
				,ctct.fHSBL
				,ctct.fTongQTLN
				,ctct.fDuToan
				,ctct.fDaQuyetToan
				,ctct.fConLai
				,ctct.fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN
				,ctct.fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO #tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam = @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ct.iLoaiTongHop = 1
				--and ct.bDaTongHop = 0;
	--END chi tiet

	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	--INSERT TOTAL
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(chungtudonvi.iQSBQNam) iQSBQNam,
			sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
			sum(chungtudonvi.fPCChucVu)/@Donvitinh fPCChucVu,
			sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
			sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
			sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
			sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
			(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
			sum(chungtudonvi.fDuToan) fDuToan,
			sum(chungtudonvi.fDaQuyetToan) fDaQuyetToan,
			sum(chungtudonvi.fConLai) fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))) fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))) fTongNSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
			null,
			null
			FROM
				(select
					BH_DM_MucLucNganSach.iID_MLNS,
					BH_DM_MucLucNganSach.iID_MLNS_Cha,
					BH_DM_MucLucNganSach.bHangCha,
					BH_DM_MucLucNganSach.sLNS,
					BH_DM_MucLucNganSach.sL,
					BH_DM_MucLucNganSach.sK,
					BH_DM_MucLucNganSach.sM,
					BH_DM_MucLucNganSach.sTM,
					BH_DM_MucLucNganSach.sTTM,
					BH_DM_MucLucNganSach.sNG,
					BH_DM_MucLucNganSach.sTNG,
					BH_DM_MucLucNganSach.sXauNoiMa,
					BH_DM_MucLucNganSach.sMoTa
				from BH_DM_MucLucNganSach 
				where sLNS like '902%'
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN #tempChiTietDonVi chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			GROUP BY
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa;
		--INSERT CHI TIẾT	
	INSERT INTO #result
	(
	iID_MLNS ,
	iID_MLNS_Cha ,
	bHangCha , 
	sXauNoiMa , 
	sMoTa , 
	iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPhuCapChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	fTongNLD ,
	fTongNSD ,
	fTongCong ,
	MaDonVi, 
	TenDonVi 
	)
	SELECT
	NULL ,
	NULL ,
	0 bHangCha , 
	sXauNoiMa , 
	dv.sTenDonVi,
	#tempChiTietDonVi.iNamLamViec ,
	iQSBQNam ,
	fLuongChinh ,
	fPCChucVu ,
	fPCTNNghe ,
	fPCTNVuotKhung ,
	fNghiOm ,
	fHSBL ,
	fTongQTLN ,
	fDuToan ,
	fDaQuyetToan ,
	fConLai ,
	fThu_BHXH_NLD ,
	fThu_BHXH_NSD ,
	fTongSoPhaiThuBHXH ,
	fThu_BHYT_NLD ,
	fThu_BHYT_NSD ,
	fTongSoPhaiThuBHYT ,
	fThu_BHTN_NLD ,
	fThu_BHTN_NSD ,
	fTongSoPhaiThuBHTN ,
	0 fTongNLD ,
	0 fTongNSD ,
	fTongCong ,
	dv.iID_MaDonVi, 
	dv.sTenDonVi as TenDonVi 
	FROM #tempChiTietDonVi 
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;
	DROP TABLE #tempChiTietDonVi;
	DROP TABLE #result;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
	
	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) SET @SMonths = '1,2,3'
	IF (@IQuy = 6) SET @SMonths = '4,5,6'
	IF (@IQuy = 9) SET @SMonths = '7,8,9'
	IF (@IQuy = 12) SET @SMonths = '10,11,12'

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
		sXauNoiMa nvarchar(200), 
		sMoTa nvarchar(200), 
		iNamLamViec int,
		iQSBQNam int,
		fLuongChinh float,
		fPhuCapChucVu float,
		fPCTNNghe float,
		fPCTNVuotKhung float,
		fNghiOm float,
		fHSBL float,
		fTongQTLN float,
		fDuToan float,
		fDaQuyetToan float,
		fConLai float,
		fThu_BHXH_NLD float,
		fThu_BHXH_NSD float,
		fTongSoPhaiThuBHXH float,
		fThu_BHYT_NLD float,
		fThu_BHYT_NSD float,
		fTongSoPhaiThuBHYT float,
		fThu_BHTN_NLD float,
		fThu_BHTN_NSD float,
		fTongSoPhaiThuBHTN float,
		fTongNLD float,
		fTongNSD float,
		fTongCong float,
		MaDonVi nvarchar(50),
		TenDonVi nvarchar(50)
	);

	--- GET CHI TIẾT ĐƠN VỊ
			select
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam/3 iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO #tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ct.iLoaiTongHop = 1
	--END chi tiet

	INSERT INTO #result
	(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
	)
	--INSERT TOTAL
	select
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(chungtudonvi.iQSBQNam) iQSBQNam,
			sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
			sum(chungtudonvi.fPCChucVu)/@Donvitinh fPCChucVu,
			sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
			sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
			sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
			sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
			(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
			sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
			sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
			sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
			null,
			null
			FROM
				(select
					BH_DM_MucLucNganSach.iID_MLNS,
					BH_DM_MucLucNganSach.iID_MLNS_Cha,
					BH_DM_MucLucNganSach.bHangCha,
					BH_DM_MucLucNganSach.sLNS,
					BH_DM_MucLucNganSach.sL,
					BH_DM_MucLucNganSach.sK,
					BH_DM_MucLucNganSach.sM,
					BH_DM_MucLucNganSach.sTM,
					BH_DM_MucLucNganSach.sTTM,
					BH_DM_MucLucNganSach.sNG,
					BH_DM_MucLucNganSach.sTNG,
					BH_DM_MucLucNganSach.sXauNoiMa,
					BH_DM_MucLucNganSach.sMoTa
				from BH_DM_MucLucNganSach 
				where sLNS like '902%'
				AND iNamLamViec = @NamLamViec) mlns
			LEFT JOIN #tempChiTietDonVi chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			GROUP BY
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa;
		--INSERT CHI TIẾT	
	INSERT INTO #result
	(
		iID_MLNS ,
		iID_MLNS_Cha ,
		bHangCha , 
		sXauNoiMa , 
		sMoTa , 
		iNamLamViec ,
		iQSBQNam ,
		fLuongChinh ,
		fPhuCapChucVu ,
		fPCTNNghe ,
		fPCTNVuotKhung ,
		fNghiOm ,
		fHSBL ,
		fTongQTLN ,
		fDuToan ,
		fDaQuyetToan ,
		fConLai ,
		fThu_BHXH_NLD ,
		fThu_BHXH_NSD ,
		fTongSoPhaiThuBHXH ,
		fThu_BHYT_NLD ,
		fThu_BHYT_NSD ,
		fTongSoPhaiThuBHYT ,
		fThu_BHTN_NLD ,
		fThu_BHTN_NSD ,
		fTongSoPhaiThuBHTN ,
		fTongNLD ,
		fTongNSD ,
		fTongCong ,
		MaDonVi, 
		TenDonVi 
	)
	SELECT
		NULL ,
		NULL ,
		0 bHangCha , 
		dt.sXauNoiMa , 
		dv.sTenDonVi,
		dt.iNamLamViec ,
		sum(isnull(dt.iQSBQNam, 0)) iQSBQNam,
		sum(isnull(dt.fLuongChinh, 0)) fLuongChinh,
		sum(isnull(dt.fPCChucVu, 0)) fPCChucVu,
		sum(isnull(dt.fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(dt.fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(dt.fNghiOm, 0)) fNghiOm,
		sum(isnull(dt.fHSBL, 0)) fHSBL,
		sum(isnull(dt.fTongQTLN, 0)) fTongQTLN,
		sum(isnull(dt.fDuToan, 0)) fDuToan,
		sum(isnull(dt.fDaQuyetToan, 0)) fDaQuyetToan,
		sum(isnull(dt.fConLai, 0)) fConLai,
		sum(isnull(dt.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(dt.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH,
		sum(isnull(dt.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(dt.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT,
		sum(isnull(dt.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(dt.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN,
		0 fTongNLD ,
		0 fTongNSD ,
		sum(isnull(dt.fTongCong, 0)) ,
		dv.iID_MaDonVi, 
		dv.sTenDonVi as TenDonVi 
	FROM #tempChiTietDonVi dt
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = dt.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
	group by
		dt.sXauNoiMa, 
		dv.sTenDonVi,
		dt.iNamLamViec,
		dv.iID_MaDonVi, 
		dv.sTenDonVi;

	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

	DROP TABLE #tempChiTietDonVi;
	DROP TABLE #result;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int
AS
BEGIN
	select
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sM,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
		sum(chungtudonvi.fPCChucVu)/@Donvitinh fPhuCapChucVu,
		sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
		sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
		sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
		sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
		(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
		sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
		sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
		sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
		sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
		sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh FTongSoPhaiThuBHXH,
		sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
		sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
		(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
		sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
		sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
		(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = 0
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		group by
			mlns.iID_MLNS,
			mlns.sMoTa,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sM,
			mlns.sXauNoiMa
		order by mlns.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
		sum(chungtudonvi.fPCChucVu)/@Donvitinh fPhuCapChucVu,
		sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
		sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
		sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
		sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
		(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
		sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
		sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
		sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
		sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
		sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
		sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
		sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
		(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
		sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
		sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
		(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		group by
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@IQuy = 3) SET @SMonths = '1,2,3'
	IF (@IQuy = 6) SET @SMonths = '4,5,6'
	IF (@IQuy = 9) SET @SMonths = '7,8,9'
	IF (@IQuy = 12) SET @SMonths = '10,11,12'

	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam)/3 iQSBQNam,
		(sum(chungtudonvi.fLuongChinh))/@Donvitinh fLuongChinh,
		(sum(chungtudonvi.fPCChucVu))/@Donvitinh fPhuCapChucVu,
		(sum(chungtudonvi.fPCTNNghe))/@Donvitinh fPCTNNghe,
		(sum(chungtudonvi.fPCTNVuotKhung))/@Donvitinh fPCTNVuotKhung,
		(sum(chungtudonvi.fNghiOm))/@Donvitinh fNghiOm,
		(sum(chungtudonvi.fHSBL))/@Donvitinh fHSBL,
		((sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0))))/@Donvitinh fTongQTLN,
		(sum(chungtudonvi.fDuToan))/@Donvitinh fDuToan,
		(sum(chungtudonvi.fDaQuyetToan))/@Donvitinh fDaQuyetToan,
		(sum(chungtudonvi.fConLai))/@Donvitinh fConLai,
		(sum(chungtudonvi.fThu_BHXH_NLD))/@Donvitinh fThu_BHXH_NLD,
		(sum(chungtudonvi.fThu_BHXH_NSD))/@Donvitinh fThu_BHXH_NSD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHXH,
		(sum(chungtudonvi.fThu_BHYT_NLD))/@Donvitinh fThu_BHYT_NLD,
		(sum(chungtudonvi.fThu_BHYT_NSD))/@Donvitinh fThu_BHYT_NSD,
		((sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHYT,
		(sum(chungtudonvi.fThu_BHTN_NLD))/@Donvitinh fThu_BHTN_NLD,
		(sum(chungtudonvi.fThu_BHTN_NSD))/@Donvitinh fThu_BHTN_NSD,
		((sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHTN,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))))/@Donvitinh fTongNLD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongNSD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		group by
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam] 
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh FTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where --ct.iNamLamViec = @NamLamViec
			ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]    Script Date: 3/5/2024 8:40:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@IQuy = 3) SET @SMonths = '1,2,3'
	IF (@IQuy = 6) SET @SMonths = '4,5,6'
	IF (@IQuy = 9) SET @SMonths = '7,8,9'
	IF (@IQuy = 12) SET @SMonths = '10,11,12'

	select
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPhuCapChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPhuCapChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0))/3 iQSBQNam
			,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
			,sum(isnull(ctct.fPCChucVu, 0)) fPhuCapChucVu
			,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
			,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
			,sum(isnull(ctct.fHSBL, 0)) fHSBL
			,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
			,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
			,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
			,sum(isnull(ctct.fTongCong, 0)) fTongCong
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			group by
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sXauNoiMa,
			ctct.sLNS
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
END
;
;
;
GO


DELETE FROM [dbo].[TL_DM_Cach_TinhLuong_BaoHiem]
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'5a32bda8-7b73-432b-8253-0545aaf8ee93', N'PCCV_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_D14N_PCCVBH_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Dưới 14 ngày - PCCVBH thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'eb7c3ead-aedd-475b-982a-10de8d18482d', N'PCTNVK_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_D14N_PCTNVKBH_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Dưới 14 ngày - PCTNVKBH thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'aa51add2-178e-4015-9a28-143fffb12ef8', N'PCTN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_D14N_PCTNBH_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Dưới 14 ngày - PCTNBH thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'b760b3ea-41d4-4c14-a8bc-1a2699046f67', N'LCS*TAINANLD_DUONGSUCPHSK_HS*SONGAYHUONG', N'CACH2', N'TAINANLD_DUONGSUCPHSK', NULL, NULL, NULL, NULL, N'Dưỡng sức phục hồi sức khỏe sau TNLĐ', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'fa4508a9-0cda-4352-b878-245bac998c4f', N'HSBL_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_T14N_HSBLBH_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Trên 14 ngày - HSBL bảo hiểm thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'043f1c05-066b-45cd-a64d-268c48f84522', N'LHT_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_T14N_LBH_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Trên 14 ngày -  Lương bảo hiểm thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'28694f59-8070-4677-9a40-2dcce24384ed', N'PCCV_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_T14N_PCCVBH_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Trên 14 ngày - PCCVBH thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'ba53579f-f0cc-44eb-9b11-3351576b0d74', N'BHYTCN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_D14N_BHYTCN_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Dưới 14 ngày - BHYT cá nhân đóng thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'ee4c1f0b-23b9-4ac3-a640-3784b9126f26', N'NAMNGHIVIEC_LBH_TT+NAMNGHIVIEC_PCCVBH_TT+NAMNGHIVIEC_PCTNBH_TT+NAMNGHIVIEC_PCTNVKBH_TT+NAMNGHIVIEC_HSBLBH_TT', N'CACH2', N'NAMNGHIKHIVOSINHCON', NULL, NULL, NULL, NULL, N'Nam nghỉ việc khi vợ sinh con', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'29ecb50d-f51c-4429-ba10-39a589e520b4', N'OK_T14N_LBH_TT+OK_T14N_PCCVBH_TT+OK_T14N_PCTNBH_TT+OK_T14N_PCTNVKBH_TT+OK_T14N_HSBLBH_TT', N'CACH2', N'OMKHAC_T14NGAY', NULL, NULL, NULL, NULL, N'Ốm khác - Từ 14 ngày trở lên', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'4e8900ee-fa14-4295-809b-3d41caca4095', N'PCTN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'NAMNGHIVIEC_PCTNBH_TT', NULL, NULL, NULL, NULL, N'Nam nghỉ việc khi vợ sinh con - PCTN BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'932d249c-1d9d-48de-8fee-3e01ffa14644', N'PCTN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'KHHGD_PCTNBH_TT', NULL, NULL, NULL, NULL, N'KHHGD - PCTN BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'098ebe78-2a5c-44d9-9671-4228bd97320e', N'OK_D14N_LBH_TT+OK_D14N_PCCVBH_TT+OK_D14N_PCTNBH_TT+OK_D14N_PCTNVKBH_TT+OK_D14N_HSBLBH_TT', N'CACH2', N'OMKHAC_D14NGAY', NULL, NULL, NULL, NULL, N'Ốm khác - Dưới 14 ngày', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'13f557d1-4e33-429a-a2f0-495f30999666', N'HSBL_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_D14N_HSBLBH_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Dưới 14 ngày - HSBL bảo hiểm thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'eb66e110-42b7-4b0b-80d7-4ecfb62580a5', N'PCTN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_T14N_PCTNBH_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Trên 14 ngày - PCTN BH thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'49006ad8-f7d5-480a-b9a0-598620c91338', N'LHT_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'NAMNGHIVIEC_LBH_TT', NULL, NULL, NULL, NULL, N'Nam nghỉ việc khi vợ sinh con - Lương BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'9ceb8d7d-2472-495a-b358-5bc7e7c2eb0b', N'LHT_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_D14N_LBH_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Dưới 14 ngày -  Lương bảo hiểm thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'5468db58-6da6-4fbf-8953-5c93612c39b0', N'HSBL_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'NAMNGHIVIEC_HSBLBH_TT', NULL, NULL, NULL, NULL, N'Nam nghỉ việc khi vợ sinh con - HSBL BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'5ffe4e1e-0bf8-4145-9e3f-6716c252ad54', N'PCTN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_T14N_PCTNBH_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Trên14 ngày - PCTNBH thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'887b9a24-2456-445d-92ac-6fdbb4739a56', N'PCCV_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'NAMNGHIVIEC_PCCVBH_TT', NULL, NULL, NULL, NULL, N'Nam nghỉ việc khi vợ snh con - PCCV BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'6bbb863a-e26f-40ed-8af3-70703f7f4e3b', N'PCCV_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_T14N_PCCVBH_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Trên 14 ngày - PCCV BH thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'de554e8e-49da-4fc1-bfec-72b80ae0bf80', N'PCCV_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'KT_PCCVBH_TT', NULL, NULL, NULL, NULL, N'Khám thai - PCCV BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'4e5d1451-170c-480b-bdb3-86f3230c39e3', N'LHT_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'KHHGD_LBH_TT', NULL, NULL, NULL, NULL, N'KHHGĐ - Lương BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'235d2870-2fc5-46b4-a8c6-8b014daac3a3', N'BHXHCN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_D14N_BHXHCN_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Dưới 14 ngày - BHXH cá nhân đóng thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'5be4b678-15eb-41c3-9a39-8b2eb52c4cd5', N'(HSBL_TT*SONGAYHUONG/CONGCHUAN_BH)*CONOM_HS', N'CACH2', N'CONOM_HSBLBH_TT', NULL, NULL, NULL, NULL, N'Con ốm - HSBL bảo hiểm thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'3d4cf721-ca0e-4c98-afbf-8f5fe2e8926f', N'(PCCV_TT*SONGAYHUONG/CONGCHUAN_BH)*CONOM_HS', N'CACH2', N'CONOM_PCCVBH_TT', NULL, NULL, NULL, NULL, N'Con ốm - PCCVBH thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'fc7f1c8b-5f04-498a-82e9-90701053f062', N'HSBL_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'KT_HSBLBH_TT', NULL, NULL, NULL, NULL, N'Khám thai - HSBL BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'4070575f-afd1-449e-abaf-91dc943d05ed', N'LCS*TC_THAISAN_HS*SONGAYHUONG', N'CACH2', N'THAISAN_DUONGSUCPHSK', NULL, NULL, NULL, NULL, N'Dưỡng sức, PHSK sau nghỉ thai sản', 2023, 11)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'ee2037b9-6986-481a-a4c9-92d666042dd7', N'BHXHCN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_D14N_BHXHCN_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Dưới 14 ngày - BHXH cá nhân đóng thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'6ef91832-fd69-4a99-80ce-93b74c41a12b', N'BDN_T14N_LBH_TT+BDN_T14N_PCCVBH_TT+BDN_T14N_PCTNBH_TT+BDN_T14N_PCTNVKBH_TT+BDN_T14N_HSBLBH_TT', N'CACH2', N'BENHDAINGAY_T14NGAY', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Từ 14 ngày trở lên', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'8c8093ac-72ac-4216-9942-95fab3d66462', N'KT_LBH_TT+KT_PCCVBH_TT+KT_PCTNBH_TT+KT_PCTNVKBH_TT+KT_HSBLBH_TT', N'CACH2', N'KHAMTHAI', NULL, NULL, NULL, NULL, N'Khám thai', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'dba5d104-5e9b-44ee-8660-972995cf987b', N'PCCV_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_D14N_PCCVBH_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Dưới 14 ngày - PCCV BH thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'f2ebb987-3f4e-48d2-b989-9b1e35dd8e0a', N'LHT_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'KT_LBH_TT', NULL, NULL, NULL, NULL, N'Khám thai - Lương BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'eaaa172c-28b3-40c3-8f64-9f6385c61053', N'PCTNVK_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'KT_PCTNVKBH_TT', NULL, NULL, NULL, NULL, N'Khám thai - PCTNVK BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'84d24c8f-c943-4f80-9ad6-a4264505e0bd', N'BHYTCN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_D14N_BHYTCN_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Dưới 14 ngày - BHYT cá nhân đóng thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'f55aacb7-0d43-41b2-ac4e-a4b797b9a116', N'CONOM_LBH_TT+CONOM_PCCVBH_TT+CONOM_PCTNBH_TT+CONOM_PCTNVKBH_TT+CONOM_HSBLBH_TT', N'CACH2', N'CONOM', NULL, NULL, NULL, NULL, N'Con ốm', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'9c9382f0-2142-4dac-aca4-a6ea3572078f', N'HSBL_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'KHHGD_HSBLBH_TT', NULL, NULL, NULL, NULL, N'KHHGĐ - HSBL BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'e17d13a4-f81c-47bd-9970-a6fb7acfca0c', N'PCTNVK_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_D14N_PCTNVKBH_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Dưới 14 ngày - PCTNVK BH thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'b3c46cff-ab77-468c-a4ca-b6fc24ae0e1e', N'PCCV_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'KHHGD_PCCVBH_TT', NULL, NULL, NULL, NULL, N'KHHGĐ- PCCV BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'c898fc1f-0d46-40ef-b837-b995f7c13268', N'LHT_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_T14N_LBH_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Trên 14 ngày - Lương bảo hiểm thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'0c2b800a-fbc8-4593-82f4-c09c0598ef4a', N'(PCTN_TT*SONGAYHUONG/CONGCHUAN_BH)*CONOM_HS', N'CACH2', N'CONOM_PCTNBH_TT', NULL, NULL, NULL, NULL, N'Con ốm - PCTNBH thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'198328a5-655e-4804-a5f2-c8623fd1a6d0', N'KHHGD_LBH_TT+KHHGD_PCCVBH_TT+KHHGD_PCTNBH_TT+KHHGD_PCTNVKBH_TT+KHHGD_HSBLBH_TT', N'CACH2', N'KHHGD', NULL, NULL, NULL, NULL, N'KHHGĐ', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'962356c6-c3f6-404b-a214-cc89edc7b786', N'PCTNVK_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'KHHGD_PCTNVKBH_TT', NULL, NULL, NULL, NULL, N'KHHGĐ - PCTNVK BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'48354950-97cc-4f64-916f-cf0718576d8d', N'PCTN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_D14N_PCTNBH_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Dưới 14 ngày - PCTN BH thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'2b29fe7d-fd2a-4610-8a1d-d0083bf660c3', N'HSBL_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_T14N_HSBLBH_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Trên 14 ngày - HSBL bảo hiểm thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'c8babe8f-a5fa-464f-add1-d73cc0c14e78', N'PCTNVK_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_T14N_PCTNVKBH_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Trên 14 ngày - PCTNVK BH thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'f760c9da-d7f9-42fd-be34-d7e5213972c5', N'LCS*OMDAU_DUONGSUCPHSK_HS*SONGAYHUONG', N'CACH2', N'OMDAU_DUONGSUCPHSK', NULL, NULL, NULL, NULL, N'Dưỡng sức, PHSK sau nghỉ ốm đau', 2023, 11)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'71f3ca33-1563-41cb-b1a3-dccaf1c17f47', N'PCTNVK_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'NAMNGHIVIEC_PCTNVKBH_TT', NULL, NULL, NULL, NULL, N'Nam nghỉ việc khi vợ sinh con - PCTNVK BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'08a8dad2-1fd4-49f8-9ca9-e2d1908c3909', N'PCTNVK_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_T14N_PCTNVKBH_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Trên 14 ngày - PCTNVKBH thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'9d43b830-93e2-4255-943e-e56f6d1645cb', N'PCTN_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'KT_PCTNBH_TT', NULL, NULL, NULL, NULL, N'Khám thai - PCTN BH thành tiền', 2024, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'054a02ae-bdeb-4ee0-95b8-e84d1329b1b2', N'LHT_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'OK_D14N_LBH_TT', NULL, NULL, NULL, NULL, N'Ốm khác - Dưới 14 ngày - Lương bảo hiểm thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'4336bd41-3bea-4839-9f82-ec60c70cac30', N'(LHT_TT*SONGAYHUONG/CONGCHUAN_BH)*CONOM_HS', N'CACH2', N'CONOM_LBH_TT', NULL, NULL, NULL, NULL, N'Con ốm - Lương bảo hiểm thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'a0218dca-e506-4bb6-9c4e-ecfea4569b28', N'HSBL_TT*SONGAYHUONG/CONGCHUAN_BH', N'CACH2', N'BDN_D14N_HSBLBH_TT', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - Dưới 14 ngày - HSBL bảo hiểm thành tiền', 2023, 12)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'07c6bb36-f279-443a-9ec4-f13eed244356', N'(PCTNVK_TT*SONGAYHUONG/CONGCHUAN_BH)*CONOM_HS', N'CACH2', N'CONOM_PCTNVKBH_TT', NULL, NULL, NULL, NULL, N'Con ốm - PCTNVKBH thành tiền', 2023, 1)
GO
INSERT [dbo].[TL_DM_Cach_TinhLuong_BaoHiem] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [NoiDung], [Ten_CachTL], [Ten_Cot], [Nam], [Thang]) VALUES (N'8b2da2d7-b2a0-4200-8483-f36df36b2d7a', N'BDN_D14N_LBH_TT+BDN_D14N_PCCVBH_TT+BDN_D14N_PCTNBH_TT+BDN_D14N_PCTNVKBH_TT+BDN_D14N_HSBLBH_TT', N'CACH2', N'BENHDAINGAY_D14NGAY', NULL, NULL, NULL, NULL, N'Bệnh dài ngày - dưới 14 ngày', 2023, 11)
GO



DELETE FROM [dbo].[BH_DM_MucLucNganSach]
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'17d5947f-3be6-464f-a61d-00185ecaf4b0', N'9010001-010-011-0008-0001-0001-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0001', N'00', N'', N'- Mai táng phí (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:36.453' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'c1003754-545f-4a6b-acb1-ceb56a4a19c2', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'2ea5bd76-f911-4e3b-ba62-00b4da71e298', N'9010003-010-011-6950', N'9010003', N'010', N'011', N'6950', N'', N'', N'', N'', N'Mua sắm tài sản dùng cho công tác chuyên môn ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.790' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87309c2d-5489-4e52-90c2-a97779e3b5c0', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6fa6381f-0d10-4ac3-a33f-013b1d55dbf1', N'9010004-010-011-0002-0003', N'9010004', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.753' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3d2fc884-12a6-45b6-8ecc-a2744f87ef34', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'43bceb51-1192-493a-bb5c-024bb4dee68a', N'9020002-010-011-0002-0001', N'9020002', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.457' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9eb683c3-3d96-44c9-9b10-4f947da34008', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 0.015, 0.03, NULL, 0.01)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'88f8961b-fae7-41e4-8d7d-030cdb5761b0', N'9010001-010-011-0002-0001-0002-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:30.410' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'e1859298-643f-4f14-a387-231474454b25', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'35853acc-f014-4116-8852-03c14a44c0ea', N'9010001-010-011-0004', N'9010001', N'010', N'011', N'0004', N'', N'', N'', N'', N'4. Hưu trí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:34.100' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6c52487f-9da5-4010-8e76-04abb4d526bd', N'9010001-010-011-0002', N'9010001', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:29.047' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'09ef0e0c-5c71-4783-b280-646d504af7b5', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cf2a4292-97c5-4d8e-bf8e-05bb48287aa8', N'9010010-010-011-0002-0005', N'9010010', N'010', N'011', N'0002', N'0005', N'', N'', N'', N'Mức 5: 2.900.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd60de627-81e8-4052-91b0-53e6e5431eac', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'fdca872b-3229-497b-981e-0640bfc3fa61', N'9010003-010-011-7000-7049', N'9010003', N'010', N'011', N'7000', N'7049', N'', N'', N'', N'Chi phí khác ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.213' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a6aa1af6-475c-4853-836f-c0bde339c5b8', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'35d461af-b0e0-4f6f-a96f-06475090a2c4', N'9010003-010-011-6650-6652', N'9010003', N'010', N'011', N'6650', N'6652', N'', N'', N'', N'Bồi dưỡng giảng viên, báo cáo viên', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:18.393' AS DateTime), NULL, 0, NULL, NULL, NULL, N'25a9efc8-4107-42ac-b3fa-00d1875a92d1', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'3daa09df-70d9-4f1c-8dff-06b289affac5', N'9010001-010-011-0001-0003', N'9010001', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dưỡng sức, phục hồi SK (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:28.773' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'280b18fa-ca4b-4e46-bb55-1bd81e962e7c', N'63e1e32a-460b-4696-aa73-441934842ac0', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'dfc0c1a7-64a7-4cc6-ae50-06f8b209ba7f', N'9010001-010-011-0005-0001-0002-00', N'9010001', N'010', N'011', N'0005', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:35.077' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'36c7cef2-d61f-4073-9fd1-f866a1375b19', N'1da67c9f-2025-4bd0-9d24-20fa64651658', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'27e3555b-4841-4ded-b3bc-07afb2b87501', N'9010010-010-011-0002-0003', N'9010010', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'Mức 3: 2.400.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c086c298-f851-4958-bc76-4e4b37c77970', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f24e50e3-2291-4654-ac6d-08356551b87b', N'9010003-010-011-6600-6601', N'9010003', N'010', N'011', N'6600', N'6601', N'', N'', N'', N'Cước phí điện thoại (không bao gồm khoán điện thoại); thuê bao đường điện thoại; fax', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.377' AS DateTime), NULL, 0, NULL, NULL, NULL, N'943d7e3d-f8ff-4f8b-bab8-7a43dda8e71f', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'981e6bf6-6f9c-4005-9674-095158881be8', N'9010004-010-011-0002-0000', N'9010004', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.200' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8795dd68-21ff-4a22-b857-bec3d01f77cf', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'89a0fb98-3ebc-491b-b384-098e00265338', N'9010003-010-011-7750-7799', N'9010003', N'010', N'011', N'7750', N'7799', N'', N'', N'', N'Chi các khoản khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.717' AS DateTime), NULL, 0, NULL, NULL, NULL, N'65c08712-f281-4123-8efe-8848d21b220b', N'c60199d0-3566-4c28-91d5-cbde4cd4f792', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'9f9eb99f-e415-4af3-bc31-09f76a751807', N'9010002-010-011-0002-0001-0002-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:41.610' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'522cfce4-4187-45a1-b5d2-3bc01146f2a5', N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'89633c55-ede4-4e99-9ca9-0a8dab73d8e1', N'9010001-010-011-0008-0001-0003-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0003', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:36.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'ebc5a51a-320c-4466-8a04-bfeb88e3c881', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'165aba4c-a1de-46a4-ba75-0b015b94e459', N'9010003-010-011-6650-6656', N'9010003', N'010', N'011', N'6650', N'6656', N'', N'', N'', N'Thuê phiên dịch, biên dịch phục vụ hội nghị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.423' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e48cd68f-5928-4231-97eb-4eba9a644b2f', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'2d4c28db-a64d-426a-8545-0b0e4bc28753', N'9010003-010-011-7000-7049-0005', N'9010003', N'010', N'011', N'7000', N'7049', N'0005', N'', N'', N'- Chi hỗ trợ bệnh viện, bệnh xá KCB BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:25.060' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'1c72986d-0c37-4e1e-b997-75b6d26bee60', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6dba18f0-314f-44f4-b9c1-0b26d503bbde', N'9010001-010-011-0003-0001-0001-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0001', N'00', N'', N'- Chi giám định mức suy giảm KNLĐ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:32.103' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b6c195d7-cf0e-4add-8961-b2224e9f21b4', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'222f66ed-6ea5-4327-8742-0bcd0274aa6f', N'9010001-010-011-0008-0001-0002-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0002', N'00', N'', N'- Tuất 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:36.690' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'01e7c76b-294b-4440-b555-d0b33af107cc', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6c3087f2-bdce-428a-85ff-0c0d68c978c7', N'901', N'901', N'', N'', N'', N'', N'', N'', N'', N'Chi các chế độ BHXH', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:59:39.573' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'519bcd42-015f-44ad-9ff4-0c643a0c5a6e', N'9010003-010-011-6650-6657', N'9010003', N'010', N'011', N'6650', N'6657', N'', N'', N'', N'Các khoản thuê mướn khác phục vụ hội nghị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.733' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dea8ce64-0669-4f7c-9e53-09e0d5ffaa64', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c0b0f916-88eb-48a0-b3f3-0cd631467cfd', N'9010002-010-011-0005-0001-0002-00', N'9010002', N'010', N'011', N'0005', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:46.663' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'f9a5e84d-2048-4acc-9028-163e4a23a8f0', N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8990568f-cdb2-4637-a5a5-0de0da68875d', N'9010001-010-011-0002-0001-0001-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'01', N'+ Lao động nữ (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:29.470' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'b9de30f6-3364-47e3-ab8a-a476893817df', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7ae0d5ea-7a76-4d7e-9805-0dfb7c3e4c7c', N'9010004-010-011-0001-0001', N'9010004', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:41.233' AS DateTime), NULL, 0, NULL, NULL, NULL, N'48112d8e-a78f-4a4c-8492-94cf989a8f7a', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f305618b-bb50-4d22-ac5e-0e291a1b8f83', N'9010002-010-011-0004', N'9010002', N'010', N'011', N'0004', N'', N'', N'', N'', N'4. Hưu trí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:45.060' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'69183e88-7f92-473e-a228-47d369f839e7', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b3f0763f-caa1-42f4-9d34-0e419ae73359', N'9010002-010-011-0008-0001-0003-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0003', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.900' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0f9b4ef0-dd90-4833-bbf5-ebff80e2a0a3', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f3aadea5-3ad5-4fb9-9480-0e464df32e25', N'9010002-010-011-0003-0001-0001-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0001', N'00', N'', N'- Chi giám định mức suy giảm KNLĐ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.097' AS DateTime), NULL, 0, NULL, NULL, NULL, N'51e03272-5f23-4dc9-95f1-c5cd7ee125c2', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'73b2d5c5-eb5f-4ec7-bd49-0e7f478fad7d', N'9010003-010-011-6700-6749', N'9010003', N'010', N'011', N'6700', N'6749', N'', N'', N'', N'Khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.500' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7bb79ca4-d725-4cf4-a28b-e2e3445e74d1', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'38e13b78-da28-4d8f-8992-0f340e8735be', N'9010001-010-011-0002-0001-0002-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:30.147' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'b79bd552-a5b7-4336-bdaf-27e4d99a04af', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'9e0daf79-90d6-4ff0-a576-1028e6dd8c46', N'9010003-010-011-6600-6608', N'9010003', N'010', N'011', N'6600', N'6608', N'', N'', N'', N'Phim ảnh; ấn phẩm truyền thông; sách báo, tạp chí, thư viện ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.073' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6ab29c1d-3216-4124-b989-42946c7cec46', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a43677d6-b6fb-448a-b2b7-10fe8f459faa', N'9030001-010-011-0001-0002', N'9030001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'b. Bảo hiểm y tế thân nhân QNCN (thân nhân CMCY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.493' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8e4f18ba-73bc-4614-bd34-680af8bb4456', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'9bed93d1-2291-4d38-b785-11c6e915a2f3', N'9010002-010-011-0002-0001-0002-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:41.610' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'522cfce4-4187-45a1-b5d2-3bc01146f2a5', N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'3ab052bf-a24c-4d4b-a01b-12cfe980668d', N'9010003-010-011-6650-6651', N'9010003', N'010', N'011', N'6650', N'6651', N'', N'', N'', N'In, mua tài liệu', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:52.217' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3267c7d1-633c-4a4a-ac43-72be46a2d6ae', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'372d1ee3-441b-476c-b19a-13b25c11c9fd', N'9020001-010-011-0001-0000', N'9020001', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-03-07T13:55:02.310' AS DateTime), CAST(N'2023-11-29T08:59:31.220' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'e06ad864-6b29-4f0e-bd3b-3e617e9dfda3', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'1', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL, 0.175, 0.08, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'801a2e6e-d337-430c-a1ff-14f96d9d85c0', N'9010002-010-011-0003', N'9010002', N'010', N'011', N'0003', N'', N'', N'', N'', N'3. Tai nạn lao động, bệnh NN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:42.870' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'927a25e2-8a0b-4e1b-9c24-151bf11aaea2', N'9010006-010-011-0001-0003', N'9010006', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:40.710' AS DateTime), CAST(N'2023-11-29T08:59:28.967' AS DateTime), NULL, 0, NULL, NULL, NULL, N'35d2bc4b-f59b-4f9b-a96f-bf30529d48f7', N'8088e295-2f9c-45b3-a799-d88940922713', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'48ab4354-db41-4d1e-87a0-15534f07af68', N'9030005', N'9030005', N'', N'', N'', N'', N'', N'', N'', N'V. BHYT học viên đào tạo sĩ quan dự bị từ 03 tháng trở lên', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.890' AS DateTime), NULL, 0, NULL, NULL, NULL, N'68018115-052e-4d82-b8ab-e40648c1cf49', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5e02f580-d370-4118-b9cf-15607a412d98', N'9010003-010-011-7750-7799-0001', N'9010003', N'010', N'011', N'7750', N'7799', N'0001', N'', N'', N'- Chi thưởng cho tập thể, cá nhân thực hiện tốt công tác chi trả', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:26.017' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'994d7914-cf71-4767-99e4-697f10de2b78', N'65c08712-f281-4123-8efe-8848d21b220b', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e9e76cdf-4107-4bbb-b3d1-15aabd868a76', N'9010004', N'9010004', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB tại quân y đơn vị 10%', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:26.243' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'99399384-55e9-45fd-b3cc-168faba252a4', N'9010001-010-011-0007', N'9010001', N'010', N'011', N'0007', N'', N'', N'', N'', N'7. Trợ cấp thôi việc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:35.660' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'58a28603-1d42-40be-bbb3-56f7c15d69ef', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'20c8af7e-086c-45ac-9ded-16bea4344e90', N'9010001-010-011-0003-0001-0007-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0007', N'00', N'', N'- Chi hỗ trợ phòng ngừa (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:02:15.827' AS DateTime), CAST(N'2023-11-29T08:58:33.370' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8371001c-2996-42e3-8dcf-7a5ab4a0914c', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e7604c61-6f21-4d02-8980-16e717373219', N'9010002-010-011-0002-0001-0003-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'', N'- Khám thai, KHH GĐ, nam nghỉ việc khi vợ sinh con(ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:41.933' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf731c26-af8a-4708-82de-c03bd8b38715', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f1fbdb78-732b-44eb-b677-17fbae28f817', N'9010001', N'9010001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:25.503' AS DateTime), NULL, 0, NULL, NULL, NULL, N'91465483-df1b-4262-9436-d87f8808cfac', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'3a0a865f-65be-4e48-b99f-1895c322c533', N'9020002-010-011-0001', N'9020002', N'010', N'011', N'0001', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:33.020' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8639c115-bdb3-49d5-9018-a1c9a3d24896', N'72d7a943-3288-4175-be6f-cf5190d2b908', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c3086c8c-f8cb-4b26-886d-18e9306880ee', N'9010003-010-011-7000-7049-0001', N'9010003', N'010', N'011', N'7000', N'7049', N'0001', N'', N'', N'- Chi hỗ trợ cán bộ, nhân viên chuyên trách làm công tác BHXH, BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:24.427' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'302f226e-4c2d-4c58-895b-313e987860ce', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'93cd381a-6465-47a4-88ab-1be66a332c55', N'9010002-010-011-0001-0001-0001-02-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/tháng(ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:38.950' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'6d2d85ba-b319-4704-8839-2f10cfdcb670', N'0c64a47a-ae28-472f-82f7-a2d818d107ba', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ac813b7e-9662-4480-b034-1d57157580b2', N'9010003-010-011-6650-6699', N'9010003', N'010', N'011', N'6650', N'6699', N'', N'', N'', N'Chi phí khác ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.070' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9a6ab999-351c-4878-aa57-0807366a6f70', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b3dd39ee-673c-481c-8a3f-1d7952748676', N'9010003-010-011-6900', N'9010003', N'010', N'011', N'6900', N'', N'', N'', N'', N'Sửa chữa, duy trì tài sản phục vụ công tác chuyên môn và các công trình cơ sở hạ tầng ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.203' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87c7737b-0150-452a-b22e-02b356bd590f', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'eb633061-6143-402b-8648-1d82225b3078', N'9020002-010-011-0001-0002', N'9020002', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.877' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ceec043e-4468-4128-9b09-18cf4b53b025', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, 0.225, NULL, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bfb54864-171a-443e-979d-1d8728a8b622', N'9010001-010-011-0003-0001-0002-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:32.327' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'795d2728-9ff9-4213-93f9-5e4bd22903e7', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ea02d9e3-6dfc-4852-9c2e-1e050d289ad4', N'9010001-010-011-0002-0001-0001-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'', N'- Sinh con, nuôi con nuôi (tháng)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:29.343' AS DateTime), NULL, 0, NULL, NULL, NULL, N'232a6a14-37ff-40d7-9753-21a3f0131208', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1b54b3ec-989b-4828-acdd-1e36a08ed851', N'9010001-010-011-0004', N'9010001', N'010', N'011', N'0004', N'', N'', N'', N'', N'4. Hưu trí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:34.100' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bf035163-9a82-445d-9088-1ebfac19fa17', N'9010010-010-011-0002-0001', N'9010010', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'Mức 1: 1.800.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:14:36.670' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dd3587b7-ba65-4bed-9cb3-6487d54a09e1', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6969d497-3eb4-4447-b65c-1edf76a8c134', N'9010001-010-011-0004-0001-0001-00', N'9010001', N'010', N'011', N'0004', N'0001', N'0001', N'00', N'', N'-  Trợ cấp 1 lần', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:34.270' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'5cca7dd1-b4e8-4273-86cc-ea4ff207409b', N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6ca39a71-c278-4f55-934a-1fafdcb63f38', N'9010002-010-011-0001-0001-0001-01-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:37.923' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'bc6c9002-f7cc-47ad-8fe1-f80cab19ea01', N'9ebe3f5e-f6e8-4268-b744-583b24731221', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f60d23a6-ee8e-46d1-ae1a-206672fbbe95', N'9010010-010-011-0001-0003', N'9010010', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'Mức 3: 2.400.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'aea11eec-3be8-47eb-bb7c-7d7b339ba635', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bee75139-14f3-4002-a099-216e170fcd90', N'9010001-010-011-0002-0001-0001-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'02', N'+ Lao động nam (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:29.623' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'e23d1638-f729-4e97-82c3-62a1c02fd5a3', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a28c1320-589d-4344-97f3-218775f2b723', N'9010006-010-011-0002', N'9010006', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:55.943' AS DateTime), CAST(N'2023-11-29T08:59:29.490' AS DateTime), NULL, 0, NULL, NULL, NULL, N'043d854e-bca5-4ee9-905a-8b78d15b9887', N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', 2024, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'16104345-1099-43cf-81ee-226b68af03c8', N'9010003-010-011-7000-7049-0004', N'9010003', N'010', N'011', N'7000', N'7049', N'0004', N'', N'', N'- kiểm tra, xác minh, giám sát, quản lý đối tượng hưởng tại đơn vị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:24.940' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'd400e9c7-319a-4c0f-b449-a30ac04b8c41', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8f791e2e-d980-4d02-9295-23653f17885d', N'9010001-010-011-0003-0001-0001-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0001', N'00', N'', N'- Chi giám định mức suy giảm KNLĐ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:32.103' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b6c195d7-cf0e-4add-8961-b2224e9f21b4', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ae1fda9a-e813-4eba-b2cc-241d3abbadd6', N'9010002-010-011-0003-0001-0007-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0007', N'00', N'', N'- Chi hỗ trợ phòng ngừa (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:02:15.827' AS DateTime), CAST(N'2023-11-29T08:58:44.337' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8dd7347a-25eb-4be1-b097-c0c6214a40e3', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f0a7201e-87ae-477d-bb49-24d9ae92c796', N'9010002-010-011-0001-0001-0001-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'', N'+ Ốm khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T15:54:34.480' AS DateTime), CAST(N'2023-11-29T08:58:38.367' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'0c64a47a-ae28-472f-82f7-a2d818d107ba', N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'aa236ee0-94e5-445d-b90a-250815450ca2', N'9030002-010-011-0001', N'9030002', N'010', N'011', N'0001', N'', N'', N'', N'', N'2. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.243' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0989f265-2645-430c-9a78-b811b0a2f37f', N'9ad55244-03c4-4c97-ba26-27af54495842', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'fdf9723a-257b-4e0d-85d3-26294415bee0', N'9030002-010-011-0000', N'9030002', N'010', N'011', N'0000', N'', N'', N'', N'', N'1. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.127' AS DateTime), NULL, 0, NULL, NULL, NULL, N'49f6be60-0af9-41f8-8a0e-cac8b9b2dcfe', N'9ad55244-03c4-4c97-ba26-27af54495842', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'17382555-d450-4020-9c3a-26ee3b61d396', N'9030003', N'9030003', N'', N'', N'', N'', N'', N'', N'', N'III. BHYT học viên đào tạo cán bộ QS cấp xã, phường', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.467' AS DateTime), NULL, 0, NULL, NULL, NULL, N'449dec2d-4376-41c9-96fe-c054e152d4bb', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6a87caca-cca2-4865-b0fd-2758259fe9a7', N'9010003-010-011-6950', N'9010003', N'010', N'011', N'6950', N'', N'', N'', N'', N'Mua sắm tài sản dùng cho công tác chuyên môn ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.790' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87309c2d-5489-4e52-90c2-a97779e3b5c0', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ab24d077-e4b0-420c-a9eb-27e623b818d5', N'9010010-010-011-0001', N'9010010', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-06T11:10:08.130' AS DateTime), NULL, 0, NULL, NULL, NULL, N'900d7456-023a-41c3-9380-c7054e410b71', N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', 2023, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'03516089-0db2-440c-98e6-2809b0a2068c', N'9010003-010-011-6600', N'9010003', N'010', N'011', N'6600', N'', N'', N'', N'', N'Thông tin, tuyên truyền, liên lạc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'eff7df70-92d7-4db4-9ca8-2994b976272e', N'9040002', N'9040002', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT thân nhân quân nhân và người lao động', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.780' AS DateTime), NULL, 0, NULL, NULL, NULL, N'503d8588-e537-4c72-a921-a437c1845d9e', N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'824e46f2-d57a-47ea-bd1e-2a7a6883c901', N'9010002-010-011-0002-0001-0001-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'', N'- Sinh con, nuôi con nuôi (tháng)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:40.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5caff718-8b78-4152-bb10-2b353fbd41de', N'9010003-010-011-6550-6599', N'9010003', N'010', N'011', N'6550', N'6599', N'', N'', N'', N'Vật tư văn phòng khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.870' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3a14e0af-be8c-4ee3-9279-afc0eb517462', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'111293b7-91b6-4c1a-b23d-2c0f32376d53', N'9010004', N'9010004', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB tại quân y đơn vị 10%', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:26.243' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'72fe1f07-f2e8-41dd-9d72-2d0d28c4a797', N'9010006-010-011-0002-0003', N'9010006', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.490' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dd5d6fc7-35a0-41b7-a997-dd04923cfc69', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'3,2', N'', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'af41decf-e834-426b-93cb-2f8f1dfa2772', N'9010003-010-011-7000-7012', N'9010003', N'010', N'011', N'7000', N'7012', N'', N'', N'', N'Chi phí hoạt động nghiệp vụ chuyên ngành ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.937' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7b143cca-0dad-4003-8708-d479f83fdd53', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'353debe4-7d38-4d02-9172-307ddbca8beb', N'9010001-010-011-0001-0001-0001-02-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:28.103' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'd0b64142-699e-4f9e-a6f5-824b03051c0e', N'4ecb27b3-f09b-4972-9d22-965886faab0a', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b557ba35-fdef-4555-aa9b-31b152e00a92', N'9010003-010-011-6600-6608', N'9010003', N'010', N'011', N'6600', N'6608', N'', N'', N'', N'Phim ảnh; ấn phẩm truyền thông; sách báo, tạp chí, thư viện ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.073' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6ab29c1d-3216-4124-b989-42946c7cec46', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'3dcf2810-7e3f-4b04-9aa8-31ee1ef13c3b', N'9030001-010-011-0002-0001', N'9030001', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'a. Bảo hiểm y tế thân nhân sĩ quan (thân nhân hàm CY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.233' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f2303978-ca39-4c7c-8ad1-2215f1d48fab', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'9c243a97-86c9-4149-b256-32643968d4e9', N'9010003-010-011-6600-6649', N'9010003', N'010', N'011', N'6600', N'6649', N'', N'', N'', N'Khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.610' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd4edb565-5853-4cc0-8811-5fc92f1f306e', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1d185efe-1557-4ec6-b9c0-331e1019ab93', N'9010003-010-011-6550', N'9010003', N'010', N'011', N'6550', N'', N'', N'', N'', N'Vật tư văn phòng', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.157' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'04c9e8f3-a281-4567-a527-3357654662cb', N'9010003-010-011-6650', N'9010003', N'010', N'011', N'6650', N'', N'', N'', N'', N'Hội nghị', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:52.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c31a5537-d04a-4726-a71c-33f2777cc283', N'9010002-010-011-0002-0001-0001-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'02', N'+ Lao động nam (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:40.837' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0cb5283f-1f2e-4c82-b7d9-9cab5c46c2cf', N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'62f9bbb6-6851-4e2a-a836-34322693bbaf', N'9010002-010-011-0002', N'9010002', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:39.853' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'6f830377-5b2f-44d8-9a64-9d46c4270a80', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ca1fa978-66d3-4ff6-9018-3432a9f3d44b', N'9010002-010-011-0006-0001-0001-00', N'9010002', N'010', N'011', N'0006', N'0001', N'0001', N'00', N'', N'- Trợ cấp XN 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:47.160' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'fd8cb880-dd35-4db5-a71d-e47c97168672', N'd8fd23af-c23c-4b8f-8858-05fe8e304970', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'60ea82c3-fd48-4cf7-94ab-343f017bb533', N'9010010-010-011-0002-0003', N'9010010', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'Mức 3: 2.400.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c086c298-f851-4958-bc76-4e4b37c77970', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'32cc84fc-e5b6-449d-863e-350cb892bfcb', N'9010002-010-011-0001-0001-0001-01-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:38.153' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'1bf2509b-91d5-4974-aff0-7f7c426cddc4', N'9ebe3f5e-f6e8-4268-b744-583b24731221', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c3917bfd-1e6b-4f50-b5ae-35efbd4067c6', N'9010003-010-011-6750-6751', N'9010003', N'010', N'011', N'6750', N'6751', N'', N'', N'', N'Thuê phương tiện vận chuyển ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.817' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1ed591dc-2b4a-44f6-9f2f-51dbac62e209', N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'fec0a03f-c32a-4470-8a0b-36c81355b119', N'9010006-010-011-0002-0000', N'9010006', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:29.763' AS DateTime), NULL, 0, NULL, NULL, NULL, N'40d58a93-4702-4a2b-8168-81a6fe18f12d', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'0d90423a-ab94-403c-982e-3763c5987c32', N'9010002-010-011-0007-0001-0001-00', N'9010002', N'010', N'011', N'0007', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:47.957' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'2907ac93-5edc-4c3b-a86d-37336769b417', N'774d1074-d499-4f15-9ac0-40b04ad1ba17', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'fe08da97-5b24-4414-84a4-38918e4c4335', N'9030006', N'9030006', N'', N'', N'', N'', N'', N'', N'', N'VI. BHYT người nước ngoài đang học trong các trường QĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.080' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1499a32d-8556-4ad5-bff4-81a307916605', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5f72e48e-d45e-4beb-ba07-392fccff60a1', N'9010001-010-011-0001-0001-0001-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'', N'+ Thuộc DM bệnh cần chữa trị dài ngày', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-30T08:15:51.127' AS DateTime), CAST(N'2023-11-29T08:58:26.330' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', N'a655d431-de68-4238-b921-55850d8bba6b', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, N'TNG', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'eb9dc4a8-4a21-41a0-b273-395fe9f16401', N'9010003-010-011-6700-6749', N'9010003', N'010', N'011', N'6700', N'6749', N'', N'', N'', N'Khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.500' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7bb79ca4-d725-4cf4-a28b-e2e3445e74d1', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'276600cc-0fc4-4caa-aff9-3a2f8a7014ee', N'9010001-010-011-0008-0001-0003-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0003', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:36.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'ebc5a51a-320c-4466-8a04-bfeb88e3c881', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cc6daf1f-9e7e-4868-930d-3abf32a496bf', N'9010006-010-011-0002-0002', N'9010006', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.210' AS DateTime), NULL, 0, NULL, NULL, NULL, N'45438d5c-982d-41d5-aa67-d28213290663', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e8e4513e-2e6b-4b7a-b074-3b75b9292508', N'9010001-010-011-0003-0001-0006-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0006', N'00', N'', N'- Trợ cấp chết do TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:33.137' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'6a99dbd6-a4ca-49f3-b3d3-0014c20a9a2d', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c38224c8-a0e4-4e34-8ff5-3bab12ed48f9', N'9010002-010-011-0001-0001', N'9010002', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.437' AS DateTime), NULL, 0, NULL, NULL, NULL, N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'80182334-ba66-48b7-ac6c-3bcea96f3afa', N'9010010-010-011-0001-0005', N'9010010', N'010', N'011', N'0001', N'0005', N'', N'', N'', N'Mức 5: 2.900.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1e857155-0d72-4768-9fdd-7e0bb8c59200', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'358b27d0-c9ea-4e6d-913c-3c4f0bf9626b', N'9010001-010-011-0002-0001-0004-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0004', N'00', N'', N'- Dưỡng sức, phục hồi SK', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:31.680' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8e092c28-89d2-460b-ab60-5aafbf9e010a', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'3c12c48e-b0e8-49a5-bc06-3c5e0790916d', N'9040001', N'9040001', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT quân nhân', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6173fe68-9e45-4c21-92a3-309afb77f73e', N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd738e1a6-6ded-4421-acc7-3cd2556932d4', N'9030004', N'9030004', N'', N'', N'', N'', N'', N'', N'', N'IV. BHYT học sinh, sinh viên hệ dân sự', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.770' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf9bf98b-3dde-44a9-8a71-dc0e83df2b5e', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'69ffdc53-d705-44b7-9c7d-3e6663a6ce83', N'9010002-010-011-0003-0001-0003-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0003', N'00', N'', N'- Trợ cấp hàng tháng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.483' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0426ed87-f0f1-4142-8275-c21ed650e5a7', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f983ffa7-e58c-4b46-a4b4-3ea58e416a36', N'9020001-010-011-0001-0000', N'9020001', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:31.220' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e06ad864-6b29-4f0e-bd3b-3e617e9dfda3', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'1', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL, 0.175, 0.08, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ef6c2d51-9059-45f3-877d-3f2fe95ae1f8', N'9010002-010-011-0002', N'9010002', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:39.853' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'6f830377-5b2f-44d8-9a64-9d46c4270a80', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'21742a94-3c2e-4a56-b196-3fa5bc96f9f0', N'9020001-010-011-0001-0002', N'9020001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T13:34:15.757' AS DateTime), CAST(N'2023-11-29T08:59:31.870' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'9394f8c1-479d-438a-813a-22c105b5e731', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'4', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL, 0.225, NULL, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a4712ce9-78ba-43fa-9027-3fbedab35231', N'9030004', N'9030004', N'', N'', N'', N'', N'', N'', N'', N'IV. BHYT học sinh, sinh viên hệ dân sự', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.770' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf9bf98b-3dde-44a9-8a71-dc0e83df2b5e', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bf6abfc1-28e8-435d-ab83-43699600317b', N'9030002-010-011-0001', N'9030002', N'010', N'011', N'0001', N'', N'', N'', N'', N'2. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.243' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0989f265-2645-430c-9a78-b811b0a2f37f', N'9ad55244-03c4-4c97-ba26-27af54495842', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e00ea3dd-9830-4096-9dad-43ac91f6b318', N'9010003-010-011-6650-6655', N'9010003', N'010', N'011', N'6650', N'6655', N'', N'', N'', N'Thuê hội trường, phương tiện vận chuyển', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.150' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a7bfb1a0-f712-45bc-9a4d-41f850a4fd52', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'dc5444e9-93c6-4269-99d9-465d2e7f9cef', N'9010002-010-011-0002-0001-0003-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:42.340' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'01eb6c66-54b8-4f62-9b59-b0ea749cb180', N'cf731c26-af8a-4708-82de-c03bd8b38715', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f10258c4-9866-4db4-8894-46b4da8d373b', N'9010003-010-011-7000-7049-0003', N'9010003', N'010', N'011', N'7000', N'7049', N'0003', N'', N'', N'- Đối chiếu danh sách, bảng lương, đôn đốc thu', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.820' AS DateTime), NULL, 0, NULL, NULL, NULL, N'039a4045-76a1-4ae0-927d-e42c4a021223', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'24b50998-665f-4bf1-ad0d-46be0e521eb9', N'9010001-010-011-0003-0001-0003-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0003', N'00', N'', N'- Trợ cấp hàng tháng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:32.567' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'4c020fb4-019c-4296-ba41-f4bf9488a33e', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'59ebde12-19f9-4fe5-9671-47b2b70868d3', N'9010003-010-011-7000-7001', N'9010003', N'010', N'011', N'7000', N'7001', N'', N'', N'', N'Chi phí hàng hóa, vật tư ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.780' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ea128ac8-2407-4705-a4ba-5fc7c3b0c26a', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5ee996ed-22b2-422e-a621-47d6c7539044', N'9010009-010-011-0002', N'9010009', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:19:58.153' AS DateTime), CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e0756e6f-a6bf-42cb-972e-f40b1ce667e9', N'09129a72-434f-452c-8b6c-d5159b6724c2', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'89c0d0c2-b593-4be8-bc7f-4921d5ce6c2d', N'9030001-010-011-0001', N'9030001', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T18:33:52.740' AS DateTime), CAST(N'2023-11-29T08:59:34.990' AS DateTime), NULL, 0, NULL, NULL, NULL, N'800fbf3c-386c-40c7-a6c3-2b1251a94009', N'4c52e8ab-654e-4b86-afbc-165823b677a2', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6cf97360-9948-4f60-a77e-49748bf84da4', N'9010001-010-011-0001', N'9010001', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:25.797' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'63e1e32a-460b-4696-aa73-441934842ac0', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'acdb04d9-2ee2-4c3c-b9ef-4a2051a0227d', N'9020002-010-011-0002-0000', N'9020002', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.297' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3bdd2a48-b367-464b-b253-2406afc558f1', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 0.015, 0.03, NULL, 0.01)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c0e26acc-1590-456a-93ce-4c2d72e5ff97', N'9010003-010-011-6700', N'9010003', N'010', N'011', N'6700', N'', N'', N'', N'', N'Công tác phí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.353' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4af2b9e5-b4c8-44a3-97c1-4d60e0581543', N'9020002-010-011-0002-0001', N'9020002', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.457' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9eb683c3-3d96-44c9-9b10-4f947da34008', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 0.015, 0.03, NULL, 0.01)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'0c0060c6-4595-40f6-935f-4f2fe796dd16', N'9010003-010-011-6700-6704', N'9010003', N'010', N'011', N'6700', N'6704', N'', N'', N'', N'Khoán công tác phí', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.230' AS DateTime), NULL, 0, NULL, NULL, NULL, N'73da6baf-f315-4c97-ac06-346fe53255f0', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cbc103eb-65f0-41d8-9783-50860347b308', N'9010003-010-011-6900-6913', N'9010003', N'010', N'011', N'6900', N'6913', N'', N'', N'', N'Tài sản và thiết bị văn phòng ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.663' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9f4276eb-b9d2-4554-a96c-45e8c7d0ff64', N'87c7737b-0150-452a-b22e-02b356bd590f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bc4cb332-e24c-4f5f-a5fd-510ba91ee25a', N'9010002-010-011-0008-0001-0003-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0003', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:48.900' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'0f9b4ef0-dd90-4833-bbf5-ebff80e2a0a3', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd43037b4-4896-4221-b898-513cc94644a7', N'9010010-010-011-0002', N'9010010', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:16:44.000' AS DateTime), CAST(N'2023-12-06T11:13:39.497' AS DateTime), NULL, 0, NULL, NULL, NULL, N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', 2024, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'61dbbe45-bf9f-4ed5-b5d4-51f07d46bd79', N'9010006-010-011-0002-0003', N'9010006', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.490' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dd5d6fc7-35a0-41b7-a997-dd04923cfc69', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'3,2', N'', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'dde4e093-1b77-4880-ab57-5286e4bb9285', N'9030003', N'9030003', N'', N'', N'', N'', N'', N'', N'', N'III. BHYT học viên đào tạo cán bộ QS cấp xã, phường', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.467' AS DateTime), NULL, 0, NULL, NULL, NULL, N'449dec2d-4376-41c9-96fe-c054e152d4bb', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c08a38c6-dd5d-4011-95a9-529213fb7ce1', N'9010003-010-011-6600-6605', N'9010003', N'010', N'011', N'6600', N'6605', N'', N'', N'', N'Thuê bao kênh vệ tinh; thuê bao cáp truyền hình; cước phí Internet, thuê đường truyền mạng ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.823' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd6d5d9a4-d07f-406a-bf3c-08d6efb181b0', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'88b41bd0-7e76-4e17-9738-52a81e48c6e6', N'9050001-010-011-0001', N'9050001', N'010', N'011', N'0001', N'', N'', N'', N'', N'Kinh phí chăm sóc sức khỏe ban đầu người lao động ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-16T17:04:13.200' AS DateTime), CAST(N'2023-11-29T08:59:39.073' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'a519c162-3f2e-47ef-9cdf-2da72cfa40b7', N'2cdf8f93-5d04-45f8-afcc-5100068321e4', 2023, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a9ffb064-9126-4ca4-9c22-52bca0cc4c6f', N'9010006-010-011-0001-0000', N'9010006', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:30:37.537' AS DateTime), CAST(N'2023-11-29T08:59:28.380' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e3b3fc59-1030-41d6-ae90-3c3dd8fe6d1c', N'8088e295-2f9c-45b3-a799-d88940922713', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd13c13bb-beff-46b0-bd54-52e0134dda84', N'9010002-010-011-0008-0001-0001-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0001', N'00', N'', N'- Mai táng phí (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:48.577' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b66ca2a3-645d-4879-a0ca-6e4cb3c9b442', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'88ae5b21-20b5-4269-b86f-535357851b4d', N'9030001-010-011-0002-0001', N'9030001', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'a. Bảo hiểm y tế thân nhân sĩ quan (thân nhân hàm CY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.233' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f2303978-ca39-4c7c-8ad1-2215f1d48fab', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'751dba7d-6d54-4d0b-8a94-5401fa5ac8f6', N'9010002-010-011-0001-0001', N'9010002', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.437' AS DateTime), NULL, 0, NULL, NULL, NULL, N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7bc47a23-8c2b-43df-a58e-54dbe96c2be3', N'9010003-010-011-6550', N'9010003', N'010', N'011', N'6550', N'', N'', N'', N'', N'Vật tư văn phòng', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.157' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'51936b47-9812-422c-969d-550d280aa2c0', N'9010002-010-011-0002-0001-0003-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:42.340' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'01eb6c66-54b8-4f62-9b59-b0ea749cb180', N'cf731c26-af8a-4708-82de-c03bd8b38715', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'47a7d822-8faf-4ba9-b652-551c87c95c67', N'9010010-010-011-0001-0004', N'9010010', N'010', N'011', N'0001', N'0004', N'', N'', N'', N'Mức 4: 2.650.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'aa54fe2b-7318-4dca-baa9-43a4f7ac9a57', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8dd80c9c-aa3f-418d-b87a-55864f640558', N'9010003-010-011-6600-6606', N'9010003', N'010', N'011', N'6600', N'6606', N'', N'', N'', N'Tuyên truyền (phát thanh, truyền hình)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.883' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f42345ce-969b-482c-a5f5-570b0d4cdf36', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e98f7dcc-9ee3-4673-a441-5659650db6a3', N'9010001-010-011-0003-0001-0003-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0003', N'00', N'', N'- Trợ cấp hàng tháng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:32.567' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'4c020fb4-019c-4296-ba41-f4bf9488a33e', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8cd73281-f90e-40a9-bd47-56c3f86f8ad1', N'9010001-010-011-0001', N'9010001', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:25.797' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'63e1e32a-460b-4696-aa73-441934842ac0', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6c72d51b-54f5-4dc8-bc21-5720a5be6605', N'9010004-010-011-0002-0001', N'9010004', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.350' AS DateTime), NULL, 0, NULL, NULL, NULL, N'600f74a3-06c7-4c4e-8859-69a8ab9d212c', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e807ce48-f585-4ebb-9461-5734e7d59e8e', N'9010003-010-011-6750', N'9010003', N'010', N'011', N'6750', N'', N'', N'', N'', N'Chi phí thuê mướn ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.703' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8f8bc768-d3de-4f34-b8c8-57c258e92b9d', N'9010002-010-011-0003', N'9010002', N'010', N'011', N'0003', N'', N'', N'', N'', N'3. Tai nạn lao động, bệnh NN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:42.870' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'653686ff-0f35-4a68-afc2-57ed2abc139a', N'9010003-010-011-7000-7049-0002', N'9010003', N'010', N'011', N'7000', N'7049', N'0002', N'', N'', N'- Chi phối hợp kiểm tra, thanh tra, phúc tra, giám sát công tác thu, chi BHXH, BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:24.600' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'874cd699-e8bb-4d5d-96cc-fc2ef63e3f26', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'2d1277aa-8419-4d93-a730-58cfb5b93c72', N'9010003', N'9010003', N'', N'', N'', N'', N'', N'', N'', N'Chi kinh phí quản lý BHXH, BHYT', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:40.060' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'885f40fa-4c4d-4ef2-ab31-b075853b028f', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'caef417e-5e89-42da-b5dd-5919b19fb2a4', N'9010001-010-011-0003-0001-0009-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0009', N'00', N'', N'- DS, PHSK sau TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:33.873' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'77ec4558-40fd-4d3b-864d-df96aceccb53', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'3c438d1e-874b-4843-9811-5954fee00a9b', N'9010003-010-011-6650-6652', N'9010003', N'010', N'011', N'6650', N'6652', N'', N'', N'', N'Bồi dưỡng giảng viên, báo cáo viên', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:18.393' AS DateTime), NULL, 0, NULL, NULL, NULL, N'25a9efc8-4107-42ac-b3fa-00d1875a92d1', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4b9acea9-4992-4d70-b181-59d021663071', N'9010002-010-011-0007-0001-0002-00', N'9010002', N'010', N'011', N'0007', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.113' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b23ab43a-001b-4ce2-9f39-0be3ef79c50e', N'774d1074-d499-4f15-9ac0-40b04ad1ba17', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'61c22526-faac-4f83-9a70-5aaca587da4b', N'9010003-010-011-6600-6618', N'9010003', N'010', N'011', N'6600', N'6618', N'', N'', N'', N'Khoán điện thoại', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.367' AS DateTime), NULL, 0, NULL, NULL, NULL, N'781048d2-925a-4fc9-b0c7-752d42b49bd4', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7f1aadc8-a7f4-4463-96da-5b35961b15cb', N'9010002-010-011-0001-0003', N'9010002', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dưỡng sức, phục hồi SK (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:39.520' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'866cedc7-d4a3-4b0e-bbb9-ebb5b227b413', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5844a613-f59f-48e6-863d-5c22bd28bf8e', N'9020001-010-011-0001-0001', N'9020001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'2. QNCN', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:31.440' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0d4fa8e6-6deb-4b99-924d-5532cf4181a6', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'2', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL, 0.175, 0.08, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'78fbdd54-2755-4550-9aa4-5cd97f3a561f', N'9010002-010-011-0007', N'9010002', N'010', N'011', N'0007', N'', N'', N'', N'', N'7. Trợ cấp thôi việc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:47.750' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'774d1074-d499-4f15-9ac0-40b04ad1ba17', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'2af4d1d3-214d-47e6-b5af-5ce743d30fba', N'9020001-010-011-0001', N'9020001', N'010', N'011', N'0001', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.963' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'240b80d7-8604-4ef3-9a63-5d20e9b6850b', N'9010001-010-011-0006-0001-0001-00', N'9010001', N'010', N'011', N'0006', N'0001', N'0001', N'00', N'', N'- Trợ cấp XN 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:35.400' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'08c2bc4d-af7e-406e-83f3-74f9a1a353d1', N'a0d4eadb-0deb-45a6-b3fc-2f00cebd9613', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'78b5eee4-8a0d-4898-b7d2-5d6cbb8fd89f', N'9010003-010-011-6650-6651', N'9010003', N'010', N'011', N'6650', N'6651', N'', N'', N'', N'In, mua tài liệu', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:52.217' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3267c7d1-633c-4a4a-ac43-72be46a2d6ae', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e0acad29-8136-4b60-9d83-5dc5ab246c10', N'9030001-010-011-0001-0003', N'9030001', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'c. Bảo hiểm y tế thân nhân HSQ-CS (thân nhân học viên cơ yếu)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.803' AS DateTime), NULL, 0, NULL, NULL, NULL, N'100ef748-1f6f-4891-944e-4a11b59a0fcf', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'460b101e-b9f7-4fc5-bcd0-5e8c6da7850b', N'9010002-010-011-0003-0001-0004-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0004', N'00', N'', N'- Trợ cấp phục hồi chức năng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.653' AS DateTime), NULL, 0, NULL, NULL, NULL, N'46c2b17e-c44e-43df-9c08-ce6f6ffbf415', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'af4fc847-1bd1-4d47-b4f8-5f54f4129c5d', N'9020001-010-011-0001', N'9020001', N'010', N'011', N'0001', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.963' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'10882797-7fb6-4e10-b786-5fe6fb9839a8', N'9010001-010-011-0001-0001', N'9010001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-26T10:19:33.593' AS DateTime), CAST(N'2023-11-29T08:58:26.100' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'a655d431-de68-4238-b921-55850d8bba6b', N'63e1e32a-460b-4696-aa73-441934842ac0', 2024, NULL, NULL, N'admin', NULL, NULL, N'TTM', NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd29703ea-ac61-48f8-9ee3-601c4769771d', N'9020002-010-011-0001-0000', N'9020002', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.370' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87d5e78c-2370-40b6-b7a8-ec285ed08504', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, 0.175, 0.08, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bfd3a22b-8b4f-4dc0-84ef-6094a0a3bf44', N'9030001-010-011-0002-0002', N'9030001', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'b. Bảo hiểm y tế thân nhân QNCN (thân nhân CMCY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.483' AS DateTime), NULL, 0, NULL, NULL, NULL, N'adcb8096-366c-4729-8a51-1b40eaf78af2', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'090baaea-dd55-4ef3-9fa5-60c62882785a', N'9010003-010-011-7000', N'9010003', N'010', N'011', N'7000', N'', N'', N'', N'', N'Chi phí nghiệp vụ c.môn của từng ngành', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.653' AS DateTime), NULL, 0, NULL, NULL, NULL, N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'0e72edc7-a432-4548-b653-60cb9cde2351', N'9030001-010-011-0001-0003', N'9030001', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'c. Bảo hiểm y tế thân nhân HSQ-CS (thân nhân học viên cơ yếu)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.803' AS DateTime), NULL, 0, NULL, NULL, NULL, N'100ef748-1f6f-4891-944e-4a11b59a0fcf', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'60b35933-e8ff-40bc-9d8d-6250c01e06ca', N'9010002-010-011-0002-0001-0001-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'', N'- Sinh con, nuôi con nuôi (tháng)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:40.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ed189e6c-1241-43ac-81a4-626cf840cdca', N'9030001-010-011-0002-0003', N'9030001', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'c. Bảo hiểm y tế thân nhân HSQ-CS (thân nhân học viên cơ yếu)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.643' AS DateTime), NULL, 0, NULL, NULL, NULL, N'77b53c15-3232-4207-83be-759cbeeb098b', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cee7afe7-fe19-4b7c-978d-626ee5a58c97', N'9010002-010-011-0002-0001-0002-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:41.190' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f89c0f16-bd77-480d-866c-62e4f0bb7cc3', N'9010003-010-011-6600-6605', N'9010003', N'010', N'011', N'6600', N'6605', N'', N'', N'', N'Thuê bao kênh vệ tinh; thuê bao cáp truyền hình; cước phí Internet, thuê đường truyền mạng ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.823' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd6d5d9a4-d07f-406a-bf3c-08d6efb181b0', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cccb6c14-3ca5-4315-b564-63006b646f35', N'9010010', N'9010010', N'', N'', N'', N'', N'', N'', N'', N'Chi hỗ trợ người lao động tham gia BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'2004b2e2-58b0-43b9-9f87-64639eccaa4e', N'9030001', N'9030001', N'', N'', N'', N'', N'', N'', N'', N'I. Bảo hiểm y tế thân nhân quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:34.690' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4c52e8ab-654e-4b86-afbc-165823b677a2', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8a0086ec-4ac8-4f13-96a2-6483557b7fc4', N'9010006-010-011-0002-0000', N'9010006', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:29.763' AS DateTime), NULL, 0, NULL, NULL, NULL, N'40d58a93-4702-4a2b-8168-81a6fe18f12d', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'643ca4c3-4e8b-48e9-9424-648bf25ece37', N'9010001-010-011-0003-0001-0005-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0005', N'00', N'', N'- Trợ cấp phục vụ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:32.960' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'e981466a-d098-40b2-be26-78989d31bd5c', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7df6aadd-369d-4a8c-a455-6528c0117ced', N'9030001-010-011-0002-0002', N'9030001', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'b. Bảo hiểm y tế thân nhân QNCN (thân nhân CMCY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.483' AS DateTime), NULL, 0, NULL, NULL, NULL, N'adcb8096-366c-4729-8a51-1b40eaf78af2', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4512faa3-6a42-46a2-a58b-6543c3123813', N'9010002-010-011-0007', N'9010002', N'010', N'011', N'0007', N'', N'', N'', N'', N'7. Trợ cấp thôi việc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:47.750' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'774d1074-d499-4f15-9ac0-40b04ad1ba17', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'82c4c83a-31e7-4857-a040-655851115772', N'905', N'905', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí CSSK ban đầu NLĐ và HSSV', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-02T17:10:10.803' AS DateTime), CAST(N'2023-11-29T08:59:41.117' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'2cdf8f93-5d04-45f8-afcc-5100068321e4', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'M', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'22c3105a-ea01-40c4-a5cb-65ac1c7f5cc9', N'9010001-010-011-0005', N'9010001', N'010', N'011', N'0005', N'', N'', N'', N'', N'5. Trợ cấp Phục viên', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:34.693' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'1da67c9f-2025-4bd0-9d24-20fa64651658', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e8f534be-b185-4e79-bf74-66aef439bd82', N'9010002-010-011-0008', N'9010002', N'010', N'011', N'0008', N'', N'', N'', N'', N'8. Tử tuất', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:48.327' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'e56cedb7-ed03-485b-beab-c152f42ebadd', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'29355601-8477-4510-ae7c-66af347ce556', N'9010001-010-011-0001-0001-0001-02-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:27.920' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'ba487675-57b4-4b66-a1f4-27d7b6fbb495', N'4ecb27b3-f09b-4972-9d22-965886faab0a', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'92dba78d-5153-4b2e-aaff-67c2e4074aaf', N'9010004-010-011-0001-0002', N'9010004', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:58:30.947' AS DateTime), CAST(N'2023-11-29T08:59:41.627' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b7898495-5a08-4079-834f-f12d2e7939d6', N'73357a2d-1879-452f-a85f-836a3090a537', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cad86ef8-d4f7-495b-9521-67c89c5dda93', N'9010006', N'9010006', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB tại Trường Sa - DK ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:27.963' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b9013d9c-4fbb-426e-8cc8-6809657957f1', N'9010003-010-011-6750', N'9010003', N'010', N'011', N'6750', N'', N'', N'', N'', N'Chi phí thuê mướn ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.703' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'58651844-ec72-4563-bc8f-680e2889fbf4', N'9030001-010-011-0002', N'9030001', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T18:33:52.740' AS DateTime), CAST(N'2023-11-29T08:59:36.087' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a1c989de-eac9-4153-8a60-a34ac387bb88', N'4c52e8ab-654e-4b86-afbc-165823b677a2', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'39abfaca-2af1-4080-976b-690663816169', N'9010003-010-011-6650-6653', N'9010003', N'010', N'011', N'6650', N'6653', N'', N'', N'', N'Tiền vé máy bay, tàu xe (đối với đại biểu là khách)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:18.687' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'5fd8d523-3afd-45ff-85bc-80c96d602cb8', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'55fdfed6-e60a-41db-b259-6966f9e929db', N'9010002-010-011-0004', N'9010002', N'010', N'011', N'0004', N'', N'', N'', N'', N'4. Hưu trí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:45.060' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'69183e88-7f92-473e-a228-47d369f839e7', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a4066d74-973c-4709-9627-6a695941d9a8', N'9010003-010-011-7750-7756', N'9010003', N'010', N'011', N'7750', N'7756', N'', N'', N'', N'Chi các khoản phí và lệ phí ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bc5851db-3788-461b-b364-acc87fa0cca0', N'c60199d0-3566-4c28-91d5-cbde4cd4f792', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f5d1dcbe-e9ba-495b-a306-6ab17e959e2b', N'9010002-010-011-0004-0001-0002-00', N'9010002', N'010', N'011', N'0004', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:45.550' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'939834ed-9ed7-406a-8cc6-b8d4b2930c66', N'69183e88-7f92-473e-a228-47d369f839e7', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'888004a8-a4a1-40c4-a511-6b88cb1ec9b7', N'9010010-010-011-0002-0002', N'9010010', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'Mức 2: 2.100.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:16:44.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6f657c41-02bf-45e6-9e47-776c9040b46f', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7b9180d4-c30c-4662-9698-6c5e2bdea7e2', N'9010010-010-011-0001-0004', N'9010010', N'010', N'011', N'0001', N'0004', N'', N'', N'', N'Mức 4: 2.650.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'aa54fe2b-7318-4dca-baa9-43a4f7ac9a57', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'2db7cd5b-b2a0-4a18-9e9a-6cb64b7983b9', N'9010002-010-011-0001-0002', N'9010002', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Con ốm (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:39.173' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'63e1bfb4-2e48-4b3e-b773-0be4cd4a194e', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e5efb28a-4113-4468-b04d-6cd72babffe9', N'9010003-010-011-6900', N'9010003', N'010', N'011', N'6900', N'', N'', N'', N'', N'Sửa chữa, duy trì tài sản phục vụ công tác chuyên môn và các công trình cơ sở hạ tầng ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.203' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87c7737b-0150-452a-b22e-02b356bd590f', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4c38dfd8-092a-46e2-ab4f-6d3072354d94', N'9010001-010-011-0002-0001-0002-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:30.410' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'e1859298-643f-4f14-a387-231474454b25', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'685bb2a6-8467-4446-8b75-6da4fb9e7d63', N'9010001-010-011-0001-0002', N'9010001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Con ốm (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:28.497' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'a297c80b-7b65-40e6-823f-fbed19e77501', N'63e1e32a-460b-4696-aa73-441934842ac0', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ad9e8ce2-573b-4abf-83c1-6dc2a0351b6f', N'9010001-010-011-0002-0001-0001-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'01', N'+ Lao động nữ (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:29.470' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'b9de30f6-3364-47e3-ab8a-a476893817df', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'3cfeceb8-68de-4aa9-bec2-6e1f657c8b50', N'9010010-010-011-0001-0002', N'9010010', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'Mức 2: 2.100.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'884e38f9-ce43-49f6-9f77-37969ab18a81', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e0716aab-8dbc-4f7c-8f05-6e5559264c01', N'9010003-010-011-7000-7049-0004', N'9010003', N'010', N'011', N'7000', N'7049', N'0004', N'', N'', N'- kiểm tra, xác minh, giám sát, quản lý đối tượng hưởng tại đơn vị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:24.940' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'd400e9c7-319a-4c0f-b449-a30ac04b8c41', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'99590a09-90db-4e20-9057-6eb41c08d3a1', N'9010002-010-011-0005-0001-0001-00', N'9010002', N'010', N'011', N'0005', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:46.340' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'da87b860-e7a5-4a0a-a1ae-d03ea1b81de9', N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8660ec00-f284-4cb0-850f-6f93dca1a662', N'9010002-010-011-0001', N'9010002', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:37.250' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'62ab59f2-7516-408f-9c0a-71c8030ff3d4', N'9010003-010-011-6550-6552', N'9010003', N'010', N'011', N'6550', N'6552', N'', N'', N'', N'Mua sắm công cụ, dụng cụ văn phòng', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.510' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bf5ffd8c-e50f-4843-a003-156c9e2a5eba', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4d6db3fc-67ef-4332-9446-71f701301025', N'9020002-010-011-0001-0002', N'9020002', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.877' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ceec043e-4468-4128-9b09-18cf4b53b025', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, 0.225, NULL, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4a82d7a8-6e16-4d69-ab4e-72006802e6fb', N'9010001-010-011-0007-0001-0002-00', N'9010001', N'010', N'011', N'0007', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:36.067' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'f10c833a-c6a4-48d9-82ee-51682f8abe0b', N'58a28603-1d42-40be-bbb3-56f7c15d69ef', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'45af5d64-a392-4783-a9b0-723468cb02a4', N'9010001-010-011-0005-0001-0001-00', N'9010001', N'010', N'011', N'0005', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:34.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b4187139-caae-46f6-a079-343810e05db2', N'1da67c9f-2025-4bd0-9d24-20fa64651658', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'13221d72-241c-4ac5-abe6-72e69da219b5', N'9010002-010-011-0003-0001-0008-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0008', N'00', N'', N'- Chi hỗ trợ chuyển đổi nghề nghiệp (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:44.560' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'5ce55373-18e4-43c6-965d-508d72db9ab5', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6b31cd94-4946-48bc-9846-73ee72cf03fc', N'9010002', N'9010002', N'', N'', N'', N'', N'', N'', N'', N'II. KHỐI HẠCH TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:39.747' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7a097777-89ac-4d76-a856-d1534c4070a9', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cac1f3c0-0e89-4ec8-a67a-747ef0b1104d', N'9030002-010-011-0000', N'9030002', N'010', N'011', N'0000', N'', N'', N'', N'', N'1. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.127' AS DateTime), NULL, 0, NULL, NULL, NULL, N'49f6be60-0af9-41f8-8a0e-cac8b9b2dcfe', N'9ad55244-03c4-4c97-ba26-27af54495842', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'9af17729-3658-4aed-ace6-7584b68ba1f9', N'9010001-010-011-0003-0001-0008-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0008', N'00', N'', N'- Chi hỗ trợ chuyển đổi nghề nghiệp (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:33.600' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'294076d4-8afb-404d-929d-3d6ecc396771', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd85ea20c-d3ce-4bf1-ad63-76d451d2bef2', N'9010003-010-011-7000-7049', N'9010003', N'010', N'011', N'7000', N'7049', N'', N'', N'', N'Chi phí khác ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.213' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a6aa1af6-475c-4853-836f-c0bde339c5b8', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6d0c2ab0-4924-45ea-8ae3-76fae6576d32', N'9010002-010-011-0001-0001-0001-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'', N'+ Thuộc DM bệnh cần chữa trị dài ngày', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.650' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9ebe3f5e-f6e8-4268-b744-583b24731221', N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'dd513dfb-b4cb-411f-85ee-76fc182276aa', N'9020001', N'9020001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.790' AS DateTime), NULL, 0, NULL, NULL, NULL, N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1af7699f-403e-426b-88d4-77bd891214a8', N'9010001-010-011-0006-0001-0001-00', N'9010001', N'010', N'011', N'0006', N'0001', N'0001', N'00', N'', N'- Trợ cấp XN 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:35.400' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'08c2bc4d-af7e-406e-83f3-74f9a1a353d1', N'a0d4eadb-0deb-45a6-b3fc-2f00cebd9613', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'59144718-1133-4ca2-b13a-77be25d00d7c', N'9010002-010-011-0003-0001-0001-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0001', N'00', N'', N'- Chi giám định mức suy giảm KNLĐ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:43.097' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'51e03272-5f23-4dc9-95f1-c5cd7ee125c2', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'19ac0b94-9034-445a-b845-77db2b916513', N'9010003-010-011-7750-7799', N'9010003', N'010', N'011', N'7750', N'7799', N'', N'', N'', N'Chi các khoản khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.717' AS DateTime), NULL, 0, NULL, NULL, NULL, N'65c08712-f281-4123-8efe-8848d21b220b', N'c60199d0-3566-4c28-91d5-cbde4cd4f792', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b84cec52-6864-49d6-9653-77e6b2d79573', N'9010003-010-011-6700-6704', N'9010003', N'010', N'011', N'6700', N'6704', N'', N'', N'', N'Khoán công tác phí', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.230' AS DateTime), NULL, 0, NULL, NULL, NULL, N'73da6baf-f315-4c97-ac06-346fe53255f0', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a3cc3174-7e34-494c-9292-7847b2c7f133', N'9010003-010-011-6600-6649', N'9010003', N'010', N'011', N'6600', N'6649', N'', N'', N'', N'Khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.610' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd4edb565-5853-4cc0-8811-5fc92f1f306e', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'58d50836-ef9a-4a38-bbab-78cec2dcb9ee', N'9020002', N'9020002', N'', N'', N'', N'', N'', N'', N'', N'II. KHỐI HẠCH TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:32.730' AS DateTime), NULL, 0, NULL, NULL, NULL, N'72d7a943-3288-4175-be6f-cf5190d2b908', N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'40b11e43-4bcb-40ee-89e3-7973824b04b8', N'902', N'902', N'', N'', N'', N'', N'', N'', N'', N'Thu BHXH, BHYT, BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.757' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cdd4399b-5a90-4c34-8597-799be35410f1', N'9030001-010-011-0001-0002', N'9030001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'b. Bảo hiểm y tế thân nhân QNCN (thân nhân CMCY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.493' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8e4f18ba-73bc-4614-bd34-680af8bb4456', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'744fe211-8755-457b-ade7-79c85b06e461', N'9010003-010-011-6900-6913', N'9010003', N'010', N'011', N'6900', N'6913', N'', N'', N'', N'Tài sản và thiết bị văn phòng ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.663' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9f4276eb-b9d2-4554-a96c-45e8c7d0ff64', N'87c7737b-0150-452a-b22e-02b356bd590f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'19e89c79-dc76-4fd7-b5ae-7a469d0b356a', N'9020002-010-011-0001-0001', N'9020002', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'2. QNCN', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.560' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e89cbafd-a9b0-4b68-890b-d1d7db5b4824', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, 0.175, 0.08, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5c177532-2698-43ac-b573-7ab596bf8f8f', N'9010010-010-011-0002-0006', N'9010010', N'010', N'011', N'0002', N'0006', N'', N'', N'', N'Mức 6: 3.300.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4d4aa749-e38b-4b43-b3fc-fe999026fe4a', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'9c9a5719-ba19-43d0-809c-7b67d7aa3daf', N'9010003-010-011-7750-7756', N'9010003', N'010', N'011', N'7750', N'7756', N'', N'', N'', N'Chi các khoản phí và lệ phí ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bc5851db-3788-461b-b364-acc87fa0cca0', N'c60199d0-3566-4c28-91d5-cbde4cd4f792', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'85364497-e902-404a-917b-7c2c4a742944', N'9010001-010-011-0005-0001-0001-00', N'9010001', N'010', N'011', N'0005', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:34.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b4187139-caae-46f6-a079-343810e05db2', N'1da67c9f-2025-4bd0-9d24-20fa64651658', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'89468b22-3e35-4707-ab9d-7c33b54f8818', N'9010010-010-011-0001-0001', N'9010010', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'Mức 1: 1.800.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2577e235-3102-478d-8471-3bf14f0b8aca', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'35c1ab01-b901-4886-854a-7d60efeb1289', N'9010003-010-011-6750-6799', N'9010003', N'010', N'011', N'6750', N'6799', N'', N'', N'', N'Chi phí thuê mướn khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.990' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5b7a1ead-90a1-42c1-91f3-ef688f45f268', N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8cbcc0e7-7a38-495c-9300-7d7bae05b236', N'9010003-010-011-6700-6702', N'9010003', N'010', N'011', N'6700', N'6702', N'', N'', N'', N'Phụ cấp công tác phí', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.787' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4436044d-d91e-4f5f-819d-f9aa1290e0f9', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e9ecd0c6-2470-4593-8b03-7e63500f0a24', N'9010001-010-011-0004-0001-0002-00', N'9010001', N'010', N'011', N'0004', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:34.500' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'18fcd280-fd4c-405c-b1a1-25d438da44ba', N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7e5c06cf-2799-46b1-9637-7ec0a1c280bc', N'9010003-010-011-6600', N'9010003', N'010', N'011', N'6600', N'', N'', N'', N'', N'Thông tin, tuyên truyền, liên lạc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7649b9cf-001a-4192-b4c2-7f0510f6963d', N'9010003-010-011-6700-6703', N'9010003', N'010', N'011', N'6700', N'6703', N'', N'', N'', N'Tiền thuê phòng ngủ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.970' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5d24dd88-913a-4eb5-b2e6-b0e767ea80a4', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b81bb933-e1be-4ef9-8afa-7f297157e12e', N'9010001-010-011-0002-0001-0003-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'', N'- Khám thai, KHH GĐ, nam nghỉ việc khi vợ sinh con (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:58:30.657' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'2b24ed05-a417-4855-a5f4-7f4b8530e29a', N'9010002-010-011-0002-0001-0001-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'02', N'+ Lao động nam (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:40.837' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0cb5283f-1f2e-4c82-b7d9-9cab5c46c2cf', N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a2172914-986d-4f57-a2e4-7f895de6ba50', N'901', N'901', N'', N'', N'', N'', N'', N'', N'', N'Chi các chế độ BHXH', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T19:06:02.973' AS DateTime), CAST(N'2023-11-29T08:59:39.573' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'656f17cd-0836-4183-a1dc-7f94b56f3023', N'9010002-010-011-0001-0001-0001-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'', N'+ Thuộc DM bệnh cần chữa trị dài ngày', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.650' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9ebe3f5e-f6e8-4268-b744-583b24731221', N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4b1389ce-23ed-4713-82dc-8151eac0e615', N'9010002-010-011-0006', N'9010002', N'010', N'011', N'0006', N'', N'', N'', N'', N'6. Trợ cấp Xuất ngũ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:46.940' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'd8fd23af-c23c-4b8f-8858-05fe8e304970', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6374fc07-c6b7-4921-8d13-81525009c53c', N'9010002-010-011-0002-0001-0004-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0004', N'00', N'', N'- Dưỡng sức, phục hồi SK', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:42.603' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'097b3961-33a9-4e67-a9cb-10cdf604d8dc', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f2abadac-093a-4b69-b332-815faecbae8d', N'9010010-010-011-0002-0004', N'9010010', N'010', N'011', N'0002', N'0004', N'', N'', N'', N'Mức 4: 2.650.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2e2eb178-63f3-496b-8457-179787e01d72', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8ffe18f6-8353-4bc1-b947-8199627cd9aa', N'9020002-010-011-0002', N'9020002', N'010', N'011', N'0002', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.047' AS DateTime), NULL, 0, NULL, NULL, NULL, N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', N'72d7a943-3288-4175-be6f-cf5190d2b908', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6ff27db9-eb51-4eec-ac4f-8268a6801e82', N'9010006-010-011-0001-0002', N'9010006', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:30:37.537' AS DateTime), CAST(N'2023-11-29T08:59:28.750' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e1ac0849-a18f-4c20-89e2-0ce336d197bf', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd2930d1a-7b03-4916-b6d9-82b6a22fdec7', N'9050002-010-011-0002', N'9050002', N'010', N'011', N'0002', N'', N'', N'', N'', N'Kinh phí chăm sóc sức khỏe ban đầu học sinh sinh viên ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-16T17:04:13.200' AS DateTime), CAST(N'2023-11-29T08:59:39.300' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'cf3d919f-997c-4f35-abed-40d1ef5f1f51', N'2cdf8f93-5d04-45f8-afcc-5100068321e4', 2023, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cb776730-b094-4904-89ef-833d984e0680', N'9010002-010-011-0005-0001-0002-00', N'9010002', N'010', N'011', N'0005', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:46.663' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f9a5e84d-2048-4acc-9028-163e4a23a8f0', N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'045dbe48-489c-4c88-915f-84b57098e43f', N'9010001-010-011-0002-0001-0002-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:49:01.630' AS DateTime), CAST(N'2023-11-29T08:58:29.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'568c1146-2660-4db8-ad31-6e78d8c0daca', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1f349d44-40c8-4b43-9068-85d7d6503ca2', N'9010006-010-011-0001', N'9010006', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:51.650' AS DateTime), CAST(N'2023-11-29T08:59:28.160' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8088e295-2f9c-45b3-a799-d88940922713', N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5a419ea9-eff5-41c6-bfea-865abfc0742e', N'9010001-010-011-0005-0001-0002-00', N'9010001', N'010', N'011', N'0005', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:35.077' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'36c7cef2-d61f-4073-9fd1-f866a1375b19', N'1da67c9f-2025-4bd0-9d24-20fa64651658', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd6930338-4746-4b52-8c85-8864718a597c', N'9010002-010-011-0007-0001-0002-00', N'9010002', N'010', N'011', N'0007', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:48.113' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b23ab43a-001b-4ce2-9f39-0be3ef79c50e', N'774d1074-d499-4f15-9ac0-40b04ad1ba17', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cc505ac2-a200-45f5-b021-890f1fb8c020', N'9010008', N'9010008', N'', N'', N'', N'', N'', N'', N'', N'Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:23:10.140' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'73a85b99-cf80-4eab-b985-a83c70988b82', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ec5dfa16-2a81-42ae-be22-893bb13c0b05', N'9010003-010-011-6650-6658', N'9010003', N'010', N'011', N'6650', N'6658', N'', N'', N'', N'Chi bù tiền ăn', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.953' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0548f574-a5fb-4df5-a08f-a0a4cbad5b00', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd0b4eb49-1af3-49c3-9aea-8a72f808bd73', N'9010004-010-011-0002-0002', N'9010004', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.587' AS DateTime), NULL, 0, NULL, NULL, NULL, N'863a6a36-ed30-4f8f-b0f6-757aab07aee5', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4114503c-719b-4f87-b700-8a8575b406cb', N'9010002-010-011-0003-0001-0004-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0004', N'00', N'', N'- Trợ cấp phục hồi chức năng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:43.653' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'46c2b17e-c44e-43df-9c08-ce6f6ffbf415', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'55080c8a-9502-4e3c-b1d5-8aa430ee434b', N'9010001-010-011-0001-0001-0001-02-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:28.103' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'd0b64142-699e-4f9e-a6f5-824b03051c0e', N'4ecb27b3-f09b-4972-9d22-965886faab0a', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f4251289-70c0-478e-98e5-8bad0ad08693', N'9030001-010-011-0001', N'9030001', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T18:33:52.740' AS DateTime), CAST(N'2023-11-29T08:59:34.990' AS DateTime), NULL, 0, NULL, NULL, NULL, N'800fbf3c-386c-40c7-a6c3-2b1251a94009', N'4c52e8ab-654e-4b86-afbc-165823b677a2', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd9b9cc85-3fdf-4395-94a0-8c04a01147fd', N'9010001-010-011-0003-0001-0008-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0008', N'00', N'', N'- Chi hỗ trợ chuyển đổi nghề nghiệp (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:33.600' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'294076d4-8afb-404d-929d-3d6ecc396771', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd9c13760-9cd7-4f8f-831b-8c11a7428706', N'9010002-010-011-0002-0001-0002-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:41.447' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'55bbcc88-4a81-4a66-b648-b3049330001c', N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'af1ef3ac-3a40-455d-a7df-8c69adf16d08', N'9010003-010-011-6900-6912', N'9010003', N'010', N'011', N'6900', N'6912', N'', N'', N'', N'Các thiết bị công nghệ thông tin ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:02:15.827' AS DateTime), CAST(N'2023-11-29T08:59:22.427' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'da8365b6-7e62-42eb-acf5-b7978c7298c5', N'87c7737b-0150-452a-b22e-02b356bd590f', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ca728222-85db-48be-8eab-8d938a6d0fb2', N'9010004-010-011-0002', N'9010004', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:45:06.687' AS DateTime), CAST(N'2023-11-29T08:59:26.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', 2024, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'16067072-1a6e-4850-8092-8db4de2cbfa3', N'9010002-010-011-0002-0001-0002-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:41.190' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bdd3fb72-88d9-43a8-beae-8f4eca891284', N'9020001-010-011-0001-0002', N'9020001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T13:34:15.757' AS DateTime), CAST(N'2023-11-29T08:59:31.870' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'9394f8c1-479d-438a-813a-22c105b5e731', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'4', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL, 0.225, NULL, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'636461d4-6483-4a88-9e26-90bb4307c434', N'9010010', N'9010010', N'', N'', N'', N'', N'', N'', N'', N'Chi hỗ trợ người lao động tham gia BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1a2ed1f1-30e1-4b6e-b681-90f5f3b0de9f', N'9010008-010-011-0001', N'9010008', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f118fa46-bf8c-48c7-b32a-9bde405d806a', N'73a85b99-cf80-4eab-b985-a83c70988b82', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'66820df9-59cc-40da-bd28-915ed5e860b0', N'9010009', N'9010009', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí mua sắm trang thiết bị y tế', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:23:10.140' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'09129a72-434f-452c-8b6c-d5159b6724c2', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a01a344d-e34f-4354-b65c-91a9a9a735cc', N'9010003-010-011-6550-6553', N'9010003', N'010', N'011', N'6550', N'6553', N'', N'', N'', N'Khoán văn phòng phẩm', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.710' AS DateTime), NULL, 0, NULL, NULL, NULL, N'86b2f605-c16b-4158-90d5-c6a9dfd22c61', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bba4ed9e-9b42-4fb5-bc77-92709859f04d', N'9010001-010-011-0002-0001-0003-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:31.017' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'af4633bb-d405-401b-b7c9-c6108917fc89', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1e51f443-b698-4028-b0d6-92df75ce50e6', N'9010004-010-011-0001', N'9010004', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:02.047' AS DateTime), CAST(N'2023-11-29T08:59:40.577' AS DateTime), NULL, 0, NULL, NULL, NULL, N'73357a2d-1879-452f-a85f-836a3090a537', N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6b0c5934-284e-45ba-bac8-931045e71aec', N'9010001-010-011-0002-0001-0001-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'02', N'+ Lao động nam (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:29.623' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'e23d1638-f729-4e97-82c3-62a1c02fd5a3', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'fbe0e30c-a71d-4e7f-b824-932ae88c5fb6', N'9010001-010-011-0004-0001-0002-00', N'9010001', N'010', N'011', N'0004', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:34.500' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'18fcd280-fd4c-405c-b1a1-25d438da44ba', N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'054b6512-9ff8-411b-ace6-933423b28749', N'9020002-010-011-0001-0000', N'9020002', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.370' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87d5e78c-2370-40b6-b7a8-ec285ed08504', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, 0.175, 0.08, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'49a3d812-f84b-4387-84d8-939fe64e6a69', N'9010002-010-011-0003-0001-0003-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0003', N'00', N'', N'- Trợ cấp hàng tháng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:43.483' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'0426ed87-f0f1-4142-8275-c21ed650e5a7', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a33a6536-52c5-407f-bda9-947bad039ecd', N'9010003-010-011-6700-6701', N'9010003', N'010', N'011', N'6700', N'6701', N'', N'', N'', N'Tiền vé máy bay, tàu xe', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.543' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7d577619-8dea-4632-a2f5-5f53e7cdbff7', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'999a896f-9564-4a3e-a733-947ffa5d8c4a', N'9010003', N'9010003', N'', N'', N'', N'', N'', N'', N'', N'Chi kinh phí quản lý BHXH, BHYT', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:40.060' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'885f40fa-4c4d-4ef2-ab31-b075853b028f', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e02a0f52-ec10-4ef4-88a7-94d4a43070b1', N'9010001-010-011-0001-0002', N'9010001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Con ốm (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:28.497' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'a297c80b-7b65-40e6-823f-fbed19e77501', N'63e1e32a-460b-4696-aa73-441934842ac0', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'aa70686c-e442-46e8-a948-96697a10af84', N'9010001-010-011-0003-0001-0007-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0007', N'00', N'', N'- Chi hỗ trợ phòng ngừa (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:33.370' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8371001c-2996-42e3-8dcf-7a5ab4a0914c', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c3c400c6-8575-43c0-a4b2-96840e68d303', N'904', N'904', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.320' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'43bed3d4-1922-4d7c-8541-96e7f4ab2e2c', N'9010003-010-011-6650-6656', N'9010003', N'010', N'011', N'6650', N'6656', N'', N'', N'', N'Thuê phiên dịch, biên dịch phục vụ hội nghị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.423' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e48cd68f-5928-4231-97eb-4eba9a644b2f', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a4b474cb-996f-4362-90ee-97316bb6fb86', N'9010001-010-011-0001-0001-0001-02-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:27.920' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'ba487675-57b4-4b66-a1f4-27d7b6fbb495', N'4ecb27b3-f09b-4972-9d22-965886faab0a', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4699daaf-de8f-4d88-b974-985414cb47b5', N'9010003-010-011-7000-7049-0003', N'9010003', N'010', N'011', N'7000', N'7049', N'0003', N'', N'', N'- Đối chiếu danh sách, bảng lương, đôn đốc thu', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.820' AS DateTime), NULL, 0, NULL, NULL, NULL, N'039a4045-76a1-4ae0-927d-e42c4a021223', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5b3fec03-fa59-4152-a85a-987d03442990', N'9010001-010-011-0001-0003', N'9010001', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dưỡng sức, phục hồi SK (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:28.773' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'280b18fa-ca4b-4e46-bb55-1bd81e962e7c', N'63e1e32a-460b-4696-aa73-441934842ac0', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f3c0e696-7773-4867-bbcd-989f0acf6ea1', N'9020002', N'9020002', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI HẠCH TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:32.730' AS DateTime), NULL, 0, NULL, NULL, NULL, N'72d7a943-3288-4175-be6f-cf5190d2b908', N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'621001c0-6bbd-47e1-ab4d-98d4e6a3ed43', N'9010003-010-011-6550-6553', N'9010003', N'010', N'011', N'6550', N'6553', N'', N'', N'', N'Khoán văn phòng phẩm', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.710' AS DateTime), NULL, 0, NULL, NULL, NULL, N'86b2f605-c16b-4158-90d5-c6a9dfd22c61', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'39f14397-ab1a-424e-9e63-99c9a8929da1', N'903', N'903', N'', N'', N'', N'', N'', N'', N'', N'Thu BHYT thân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.933' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5a1234af-171a-44c9-98ed-9da7deff0188', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'76bda775-4cff-41d4-b187-99e8d98cf86c', N'9010003-010-011-6550-6599', N'9010003', N'010', N'011', N'6550', N'6599', N'', N'', N'', N'Vật tư văn phòng khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.870' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3a14e0af-be8c-4ee3-9279-afc0eb517462', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b0bcd4cd-243f-4961-aea7-9a424ee69529', N'9010001-010-011-0003-0001-0002-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:32.327' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'795d2728-9ff9-4213-93f9-5e4bd22903e7', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1fa5b2ed-7a33-442b-bc70-9a61a4112f4c', N'9010010-010-011-0001-0006', N'9010010', N'010', N'011', N'0001', N'0006', N'', N'', N'', N'Mức 6: 3.300.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a949a418-6684-4027-a429-974935a161f5', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b03cf3cc-a6e9-4fdc-aedb-9b0c5510fd92', N'9010002-010-011-0002-0001-0002-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:41.447' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'55bbcc88-4a81-4a66-b648-b3049330001c', N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8ae3806f-647b-4681-87fc-9b6b990dce96', N'9010002-010-011-0001-0002', N'9010002', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Con ốm (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:39.173' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'63e1bfb4-2e48-4b3e-b773-0be4cd4a194e', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4cd56d29-9e83-40ca-8666-9c64ecfccb8f', N'9010002-010-011-0003-0001-0005-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0005', N'00', N'', N'- Trợ cấp phục vụ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.910' AS DateTime), NULL, 0, NULL, NULL, NULL, N'22055e87-e713-4b77-97de-65672022c594', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'54dbac47-c1f6-4241-b8e7-9c6b3d55ce54', N'9010010-010-011-0001', N'9010010', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-06T11:10:08.130' AS DateTime), NULL, 0, NULL, NULL, NULL, N'900d7456-023a-41c3-9380-c7054e410b71', N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', 2024, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a3dcd5cd-a46a-4139-b9ea-9e45e556b33a', N'9010003-010-011-6950-6956', N'9010003', N'010', N'011', N'6950', N'6956', N'', N'', N'', N'Các thiết bị công nghệ thông tin ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.080' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5da67169-8f6e-41e6-a6e4-85d9430d8d2a', N'87309c2d-5489-4e52-90c2-a97779e3b5c0', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'14fcddbf-0401-403e-b252-9f0adfa7237b', N'9010002-010-011-0007-0001-0001-00', N'9010002', N'010', N'011', N'0007', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:47.957' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2907ac93-5edc-4c3b-a86d-37336769b417', N'774d1074-d499-4f15-9ac0-40b04ad1ba17', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e0605355-380d-4bf7-966f-a02bd52df49c', N'9010001-010-011-0003', N'9010001', N'010', N'011', N'0003', N'', N'', N'', N'', N'3. Tai nạn lao động, bệnh NN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:31.820' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f477c796-ef6f-4115-8dab-a03b14142b34', N'9020001-010-011-0002-0000', N'9020001', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T13:34:15.757' AS DateTime), CAST(N'2023-11-29T08:59:32.320' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'0bbae696-0592-4757-8431-f2f40f460b7a', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'3', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL, NULL, NULL, 0.015, 0.03, NULL, 0.01)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6c04bce4-1434-4da6-a378-a062f7f83bdb', N'9010001-010-011-0002-0001-0003-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:31.017' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'af4633bb-d405-401b-b7c9-c6108917fc89', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'de13da50-974d-4f6d-b789-a0d20f768f41', N'9010010-010-011-0001-0005', N'9010010', N'010', N'011', N'0001', N'0005', N'', N'', N'', N'Mức 5: 2.900.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1e857155-0d72-4768-9fdd-7e0bb8c59200', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'0ce1da1a-c023-40c9-adac-a19f6ca3f4ea', N'9010003-010-011-7000-7049-0001', N'9010003', N'010', N'011', N'7000', N'7049', N'0001', N'', N'', N'- Chi hỗ trợ cán bộ, nhân viên chuyên trách làm công tác BHXH, BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:24.427' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'302f226e-4c2d-4c58-895b-313e987860ce', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'350ef9f3-6644-47f9-b613-a20a4b04560b', N'9010002-010-011-0001-0001-0001-02-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/tháng(ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:38.950' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'6d2d85ba-b319-4704-8839-2f10cfdcb670', N'0c64a47a-ae28-472f-82f7-a2d818d107ba', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c17df8de-1269-47f4-9ac5-a2cc0cbcf7fb', N'9020001-010-011-0002-0000', N'9020001', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T13:34:15.757' AS DateTime), CAST(N'2023-11-29T08:59:32.320' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'0bbae696-0592-4757-8431-f2f40f460b7a', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'3', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL, NULL, NULL, 0.015, 0.03, NULL, 0.01)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'db941517-8748-452c-b4de-a2ced8acac0b', N'9010001-010-011-0001-0001-0001-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'', N'+ ốm khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:46:04.787' AS DateTime), CAST(N'2023-11-29T08:58:27.740' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'4ecb27b3-f09b-4972-9d22-965886faab0a', N'a655d431-de68-4238-b921-55850d8bba6b', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6e01500a-e977-4f36-8db9-a2d18c592d74', N'9010001-010-011-0007-0001-0001-00', N'9010001', N'010', N'011', N'0007', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:35.883' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'fcc27636-fe76-4224-bb5c-0743a91b7e6f', N'58a28603-1d42-40be-bbb3-56f7c15d69ef', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'73d652cd-3715-436e-a04b-a2db2fb019a0', N'9010004-010-011-0001-0002', N'9010004', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:58:30.947' AS DateTime), CAST(N'2023-11-29T08:59:41.627' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b7898495-5a08-4079-834f-f12d2e7939d6', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bef14723-1515-4aec-885b-a48b117fb967', N'9010004-010-011-0001-0000', N'9010004', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:02.047' AS DateTime), CAST(N'2023-11-29T08:59:41.410' AS DateTime), NULL, 0, NULL, NULL, NULL, N'443cbbb4-f504-43b2-ac0f-77acfeac84c2', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f33465aa-cc10-4e01-ba09-a49cb2f39d00', N'9010002-010-011-0008', N'9010002', N'010', N'011', N'0008', N'', N'', N'', N'', N'8. Tử tuất', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:48.327' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'e56cedb7-ed03-485b-beab-c152f42ebadd', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4ff45972-01ed-41a3-9229-a4b83810bb50', N'9020001-010-011-0001-0001', N'9020001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'2. QNCN', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:31.440' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0d4fa8e6-6deb-4b99-924d-5532cf4181a6', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'2', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL, 0.175, 0.08, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'aeb8aa28-0d1f-4bba-990c-a5352182f615', N'902', N'902', N'', N'', N'', N'', N'', N'', N'', N'Thu BHXH, BHYT, BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.757' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b18a18d3-c116-4384-a437-a5b4b059b0fd', N'9010003-010-011-6650', N'9010003', N'010', N'011', N'6650', N'', N'', N'', N'', N'Hội nghị', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:52.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5c3afcf1-0e53-4dbd-a79e-a67da2c0eb6e', N'9010006-010-011-0001-0002', N'9010006', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:30:37.537' AS DateTime), CAST(N'2023-11-29T08:59:28.750' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e1ac0849-a18f-4c20-89e2-0ce336d197bf', N'8088e295-2f9c-45b3-a799-d88940922713', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'01558ef4-1e8f-4f27-ac6a-a69bf2e4701d', N'9010001-010-011-0001-0001-0001-01-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:27.573' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'f9022301-b3c7-4279-b634-632b69936503', N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ba1931ba-dcb9-4a8e-8319-a71fa7d88219', N'9010002-010-011-0003-0001-0006-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0006', N'00', N'', N'- Trợ cấp chết do TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:44.117' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'898acb1c-49f6-4395-9452-418352b0eed1', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1b8f26b9-48d0-4be3-85fa-a73602586abe', N'9010001-010-011-0002-0001-0003-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'', N'- Khám thai, KHH GĐ, nam nghỉ việc khi vợ sinh con (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:58:30.657' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a4dbd0c0-2b6f-4ec6-898c-a78d219e6a56', N'9010003-010-011-6650-6657', N'9010003', N'010', N'011', N'6650', N'6657', N'', N'', N'', N'Các khoản thuê mướn khác phục vụ hội nghị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.733' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dea8ce64-0669-4f7c-9e53-09e0d5ffaa64', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bc02fc55-9cb4-413a-b3bd-a813853538f0', N'9010001-010-011-0003-0001-0005-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0005', N'00', N'', N'- Trợ cấp phục vụ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:32.960' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'e981466a-d098-40b2-be26-78989d31bd5c', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd9c6b79a-7604-4737-965e-a92c3f18fa0b', N'9010010-010-011-0001-0002', N'9010010', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'Mức 2: 2.100.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'884e38f9-ce43-49f6-9f77-37969ab18a81', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e1868847-3800-4f95-94f5-aa1ceb8e2da2', N'9010006-010-011-0002-0002', N'9010006', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.210' AS DateTime), NULL, 0, NULL, NULL, NULL, N'45438d5c-982d-41d5-aa67-d28213290663', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'23b9108b-fb4f-48d7-bbb3-aa383e590a5c', N'9010003-010-011-6700', N'9010003', N'010', N'011', N'6700', N'', N'', N'', N'', N'Công tác phí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.353' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'95580b2f-c61f-456b-985b-abc3729e7ed5', N'9010003-010-011-7750', N'9010003', N'010', N'011', N'7750', N'', N'', N'', N'', N'Chi khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.280' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c60199d0-3566-4c28-91d5-cbde4cd4f792', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c1b0be3c-0ae1-477c-83ea-abdc16d0ee0b', N'9010001-010-011-0003-0001-0004-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0004', N'00', N'', N'- Trợ cấp phục hồi chức năng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:32.733' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'30b10c02-7a67-41b4-afe2-a4652321a14b', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4b45fdd9-9824-43d1-9fb8-ac1eca70d9f6', N'9010002-010-011-0003-0001-0006-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0006', N'00', N'', N'- Trợ cấp chết do TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:58:44.117' AS DateTime), NULL, 0, NULL, NULL, NULL, N'898acb1c-49f6-4395-9452-418352b0eed1', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c53a568c-3f96-463e-a98e-ac2ca6474acf', N'9010002-010-011-0001-0003', N'9010002', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dưỡng sức, phục hồi SK (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:39.520' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'866cedc7-d4a3-4b0e-bbb9-ebb5b227b413', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1d0df9ae-5948-4ca6-9102-adf665979d24', N'9010003-010-011-6600-6603', N'9010003', N'010', N'011', N'6600', N'6603', N'', N'', N'', N'Cước phí bưu chính', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:28:37.960' AS DateTime), CAST(N'2023-11-29T08:58:50.620' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'd57050f2-75a6-43e1-b637-cd3a4fe8b4c6', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f8be4ca0-ea90-4332-9ab9-aefd4c17f0bb', N'9030005', N'9030005', N'', N'', N'', N'', N'', N'', N'', N'V. BHYT học viên đào tạo sĩ quan dự bị từ 03 tháng trở lên', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.890' AS DateTime), NULL, 0, NULL, NULL, NULL, N'68018115-052e-4d82-b8ab-e40648c1cf49', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'2d0da105-bd5b-4abd-8f2a-af9ac07c1f30', N'9010001-010-011-0005', N'9010001', N'010', N'011', N'0005', N'', N'', N'', N'', N'5. Trợ cấp Phục viên', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:34.693' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'1da67c9f-2025-4bd0-9d24-20fa64651658', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f4eaa5da-6c86-4a43-bf34-afed97b9aeee', N'9010001-010-011-0008', N'9010001', N'010', N'011', N'0008', N'', N'', N'', N'', N'8. Tử tuất', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:36.280' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'f707d446-bee8-4299-b361-16ec2fb1d471', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd50389c5-bc83-4b68-80da-b04e75ba73c2', N'9010003-010-011-6600-6601', N'9010003', N'010', N'011', N'6600', N'6601', N'', N'', N'', N'Cước phí điện thoại (không bao gồm khoán điện thoại); thuê bao đường điện thoại; fax', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.377' AS DateTime), NULL, 0, NULL, NULL, NULL, N'943d7e3d-f8ff-4f8b-bab8-7a43dda8e71f', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b0644b48-7539-48f4-ac15-b0a6a1e561b7', N'9010006-010-011-0001-0000', N'9010006', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:30:37.537' AS DateTime), CAST(N'2023-11-29T08:59:28.380' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e3b3fc59-1030-41d6-ae90-3c3dd8fe6d1c', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bfdcf04f-066c-420e-95e7-b0d2c49cb97c', N'9010001-010-011-0008-0001-0002-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0002', N'00', N'', N'- Tuất 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:36.690' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'01e7c76b-294b-4440-b555-d0b33af107cc', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a79e2439-6982-4785-bb78-b0d9a29b0c7a', N'9010002-010-011-0008-0001-0002-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0002', N'00', N'', N'- Tuất 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:48.770' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'af6267f2-5472-4ea7-bb18-c36596b39611', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7baa5851-b2c2-4fd5-ae00-b0e36447081f', N'9010002-010-011-0003-0001-0002-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:43.320' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'171536b5-4867-40ab-a842-7284eeca8409', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd3fbfbdb-d3f7-4865-ba04-b1a0b1e3e1fa', N'9010006-010-011-0001-0003', N'9010006', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:40.710' AS DateTime), CAST(N'2023-11-29T08:59:28.967' AS DateTime), NULL, 0, NULL, NULL, NULL, N'35d2bc4b-f59b-4f9b-a96f-bf30529d48f7', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e3413f14-7fa4-43be-8cc4-b215d8fd9451', N'9', N'9', N'', N'', N'', N'', N'', N'', N'', N'Thu, chi  BHXH, BHYT, BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T15:55:38.863' AS DateTime), CAST(N'2023-11-29T08:58:25.193' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'25aa0569-2843-4e00-bd95-b48558a6e028', NULL, 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f3478520-9504-4581-b533-b22033d085be', N'9010002', N'9010002', N'', N'', N'', N'', N'', N'', N'', N'II. KHỐI HẠCH TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:39.747' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7a097777-89ac-4d76-a856-d1534c4070a9', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c6eafe9d-815e-4705-bb3e-b248407d3a6b', N'9010001-010-011-0001-0001-0001-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'', N'+ Thuộc DM bệnh cần chữa trị dài ngày', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:46:04.787' AS DateTime), CAST(N'2023-11-29T08:58:26.330' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', N'a655d431-de68-4238-b921-55850d8bba6b', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'19ad4aed-91a2-4fdf-9dfb-b32e8fe88199', N'9040001', N'9040001', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT quân nhân', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6173fe68-9e45-4c21-92a3-309afb77f73e', N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cae8aa0d-6c5c-4591-a1b9-b344dd0e9f24', N'9020001-010-011-0002-0001', N'9020001', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:53:19.803' AS DateTime), CAST(N'2023-11-29T08:59:32.567' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'84ae6d38-5bfb-4c0e-96dc-dcdf8be8cfb7', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'43', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL, NULL, NULL, 0.015, 0.03, NULL, 0.01)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'85563473-cd99-4115-9dab-b4136e780058', N'9030001-010-011-0002-0003', N'9030001', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'c. Bảo hiểm y tế thân nhân HSQ-CS (thân nhân học viên cơ yếu)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.643' AS DateTime), NULL, 0, NULL, NULL, NULL, N'77b53c15-3232-4207-83be-759cbeeb098b', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cd2e7ef9-9195-4b71-91f7-b4892df1a257', N'9010003-010-011-6600-6603', N'9010003', N'010', N'011', N'6600', N'6603', N'', N'', N'', N'Cước phí bưu chính', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:28:37.960' AS DateTime), CAST(N'2023-11-29T08:58:50.620' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'd57050f2-75a6-43e1-b637-cd3a4fe8b4c6', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f9f441b9-3b70-4108-bbd6-b55078c7e65b', N'9010001-010-011-0003-0001-0006-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0006', N'00', N'', N'- Trợ cấp chết do TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:33.137' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'6a99dbd6-a4ca-49f3-b3d3-0014c20a9a2d', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c58bb406-8160-4aaf-91b8-b5a09a95dd52', N'9010006-010-011-0001', N'9010006', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:51.650' AS DateTime), CAST(N'2023-11-29T08:59:28.160' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8088e295-2f9c-45b3-a799-d88940922713', N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1c9bf25b-5fb1-42f9-a76e-b6f9398dd680', N'9010009-010-011-0001', N'9010009', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b45699c0-25f5-4f79-b26f-bed14c2fb846', N'09129a72-434f-452c-8b6c-d5159b6724c2', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8ef8788d-c72e-4bce-a6cc-b70f130ff33d', N'9010001-010-011-0002-0001-0002-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:30.147' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'b79bd552-a5b7-4336-bdaf-27e4d99a04af', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c132e0cf-509d-4ab6-b213-b79f46456f0b', N'9030001-010-011-0002', N'9030001', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T18:33:52.740' AS DateTime), CAST(N'2023-11-29T08:59:36.087' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a1c989de-eac9-4153-8a60-a34ac387bb88', N'4c52e8ab-654e-4b86-afbc-165823b677a2', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'362620c8-b4a7-4b48-a854-b7a5748713d9', N'9010001', N'9010001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:25.503' AS DateTime), NULL, 0, NULL, NULL, NULL, N'91465483-df1b-4262-9436-d87f8808cfac', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'963e3345-f7d5-4615-9e18-b8b1e451ba9f', N'9010001-010-011-0001-0001-0001-01-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:27:10.080' AS DateTime), CAST(N'2023-11-29T08:58:27.573' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'f9022301-b3c7-4279-b634-632b69936503', N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd75ef7ea-6511-4d30-9349-bafb398520bb', N'9010001-010-011-0007-0001-0001-00', N'9010001', N'010', N'011', N'0007', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:35.883' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'fcc27636-fe76-4224-bb5c-0743a91b7e6f', N'58a28603-1d42-40be-bbb3-56f7c15d69ef', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'99453931-e699-46c1-8aa2-bb8a3d17228a', N'9010003-010-011-6650-6653', N'9010003', N'010', N'011', N'6650', N'6653', N'', N'', N'', N'Tiền vé máy bay, tàu xe (đối với đại biểu là khách)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:18.687' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'5fd8d523-3afd-45ff-85bc-80c96d602cb8', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'9a85709b-b1db-4505-ad15-bb9aee729d38', N'9010008-010-011-0002', N'9010008', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:19:58.153' AS DateTime), CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'330680b8-3b02-4633-a591-6f71c88312bd', N'73a85b99-cf80-4eab-b985-a83c70988b82', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'483e0efb-01b5-484b-9ab8-bbbc6a04ad57', N'9010001-010-011-0001-0001-0001-01-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:27.283' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'5c1357a4-f877-4c19-9bf6-f7c85e8c2f99', N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'930fd4fd-705b-40cf-b578-bbd80f9359b2', N'9010008-010-011-0002', N'9010008', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:19:58.153' AS DateTime), CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'330680b8-3b02-4633-a591-6f71c88312bd', N'73a85b99-cf80-4eab-b985-a83c70988b82', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'87934fec-1ba0-4fd2-9e71-bc5eb23d67cf', N'9010003-010-011-6700-6701', N'9010003', N'010', N'011', N'6700', N'6701', N'', N'', N'', N'Tiền vé máy bay, tàu xe', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.543' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7d577619-8dea-4632-a2f5-5f53e7cdbff7', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'609ed863-c851-4e09-b28f-bcd595ee1d56', N'9010003-010-011-6550-6552', N'9010003', N'010', N'011', N'6550', N'6552', N'', N'', N'', N'Mua sắm công cụ, dụng cụ văn phòng', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.510' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bf5ffd8c-e50f-4843-a003-156c9e2a5eba', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'2cbeaabd-f0e6-4bb7-8796-bcd5c70cda4a', N'9010002-010-011-0003-0001-0005-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0005', N'00', N'', N'- Trợ cấp phục vụ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:43.910' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'22055e87-e713-4b77-97de-65672022c594', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4289bcd1-741a-4228-9b41-bcfda258a81e', N'9010002-010-011-0003-0001-0009-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0009', N'00', N'', N'- DS, PHSK sau TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:44.833' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8aec0461-a021-47fd-845f-c1512a792cb7', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'66588ba1-217a-43ce-bc83-bd326d78a67d', N'9010009', N'9010009', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí mua sắm trang thiết bị y tế', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:23:10.140' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'09129a72-434f-452c-8b6c-d5159b6724c2', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4045c436-1d01-40d2-9824-be074dbd85f6', N'9010010-010-011-0002-0006', N'9010010', N'010', N'011', N'0002', N'0006', N'', N'', N'', N'Mức 6: 3.300.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4d4aa749-e38b-4b43-b3fc-fe999026fe4a', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'91cdce49-2a8a-430a-b83d-be3461c4628f', N'9010004-010-011-0002-0000', N'9010004', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.200' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8795dd68-21ff-4a22-b857-bec3d01f77cf', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'fc32f6a2-ce5f-4eb7-ba79-bf1e34ecf707', N'9010001-010-011-0001-0001-0001-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'', N'+ ốm khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:46:04.787' AS DateTime), CAST(N'2023-11-29T08:58:27.740' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'4ecb27b3-f09b-4972-9d22-965886faab0a', N'a655d431-de68-4238-b921-55850d8bba6b', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e03482fe-9ff4-4b41-81d2-bf43297a17d8', N'9010008', N'9010008', N'', N'', N'', N'', N'', N'', N'', N'Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:23:10.140' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'73a85b99-cf80-4eab-b985-a83c70988b82', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'2927facf-12d0-4c9d-8c7b-c1fadee1f879', N'9030006', N'9030006', N'', N'', N'', N'', N'', N'', N'', N'VI. BHYT người nước ngoài đang học trong các trường QĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.080' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1499a32d-8556-4ad5-bff4-81a307916605', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'82c6efe7-df32-4bc7-a3ad-c234ba01a2a7', N'9010001-010-011-0002-0001-0001-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'', N'- Sinh con, nuôi con nuôi (tháng)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:29.343' AS DateTime), NULL, 0, NULL, NULL, NULL, N'232a6a14-37ff-40d7-9753-21a3f0131208', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b3410624-cb34-4d32-9f35-c2e61ff37250', N'9010006-010-011-0001-0001', N'9010006', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:58:45.493' AS DateTime), CAST(N'2023-11-29T08:59:28.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'78660144-1b68-4aff-a71c-65b41ae48bbe', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f905a872-36c8-4e1b-b1db-c347151c5ee8', N'9010002-010-011-0002-0001-0001-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'01', N'+ Lao động nữ (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:40.443' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'aaa3dd36-bf5d-4615-9e90-83db2e3cde00', N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'98a07181-5b57-4419-9f08-c355be19a957', N'9010001-010-011-0006', N'9010001', N'010', N'011', N'0006', N'', N'', N'', N'', N'6. Trợ cấp Xuất ngũ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:35.230' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'a0d4eadb-0deb-45a6-b3fc-2f00cebd9613', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'0f8488df-0fed-4d82-ade8-c3a3c58609fd', N'9010006-010-011-0001-0001', N'9010006', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:58:45.493' AS DateTime), CAST(N'2023-11-29T08:59:28.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'78660144-1b68-4aff-a71c-65b41ae48bbe', N'8088e295-2f9c-45b3-a799-d88940922713', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ca05acda-6404-4188-8683-c40dff63dcc4', N'9010010-010-011-0001-0003', N'9010010', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'Mức 3: 2.400.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'aea11eec-3be8-47eb-bb7c-7d7b339ba635', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'58ebbf85-6dce-4ce8-b136-c51b803498e2', N'9010003-010-011-7000-7012', N'9010003', N'010', N'011', N'7000', N'7012', N'', N'', N'', N'Chi phí hoạt động nghiệp vụ chuyên ngành ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.937' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7b143cca-0dad-4003-8708-d479f83fdd53', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f2b2842c-ca8f-49ab-a4fc-c61e701a7a92', N'9010010-010-011-0001-0006', N'9010010', N'010', N'011', N'0001', N'0006', N'', N'', N'', N'Mức 6: 3.300.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a949a418-6684-4027-a429-974935a161f5', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f33ad8be-3681-4824-bb35-c6fdb8ba6664', N'9010003-010-011-7000-7049-0002', N'9010003', N'010', N'011', N'7000', N'7049', N'0002', N'', N'', N'- Chi phối hợp kiểm tra, thanh tra, phúc tra, giám sát công tác thu, chi BHXH, BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:24.600' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'874cd699-e8bb-4d5d-96cc-fc2ef63e3f26', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4e40760f-144b-4ec8-83c9-c7b55a9dd4e5', N'9010006', N'9010006', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB tại Trường Sa - DK ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:27.963' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f2e0ecfa-c06f-48f3-bb74-c8fb3cf1b928', N'9010001-010-011-0001-0001', N'9010001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:26.100' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a655d431-de68-4238-b921-55850d8bba6b', N'63e1e32a-460b-4696-aa73-441934842ac0', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'379656dd-1cc2-40e7-a89e-c9998f949f53', N'904', N'904', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.320' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'dfecf400-54d3-4e5d-bbbe-c9acfb4c075e', N'9010010-010-011-0002-0004', N'9010010', N'010', N'011', N'0002', N'0004', N'', N'', N'', N'Mức 4: 2.650.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2e2eb178-63f3-496b-8457-179787e01d72', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a4e1bcb7-8858-41ab-8600-c9f8bbe47dbe', N'9030001-010-011-0001-0001', N'9030001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'a. Bảo hiểm y tế thân nhân sĩ quan (thân nhân hàm CY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.260' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f5b01bd6-57b9-4523-9ed0-b8157a89e171', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8c8e6e91-3f25-41da-b1d7-ca0ecdf246a6', N'9010001-010-011-0002', N'9010001', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:29.047' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'09ef0e0c-5c71-4783-b280-646d504af7b5', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b7bde0b4-1cd7-4f1c-a4f1-cb0c75df3a28', N'9010003-010-011-6650-6699', N'9010003', N'010', N'011', N'6650', N'6699', N'', N'', N'', N'Chi phí khác ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.070' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9a6ab999-351c-4878-aa57-0807366a6f70', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c1313f22-432a-42d0-bc1b-cb9692e77f66', N'9010001-010-011-0003', N'9010001', N'010', N'011', N'0003', N'', N'', N'', N'', N'3. Tai nạn lao động, bệnh NN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:31.820' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a4dcbfba-84b8-4fd2-b545-cbc0d8662877', N'9010010-010-011-0002-0001', N'9010010', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'Mức 1: 1.800.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:14:36.670' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dd3587b7-ba65-4bed-9cb3-6487d54a09e1', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd69242ab-afe2-4585-96c2-ccdca3afbaea', N'9010004-010-011-0002-0001', N'9010004', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.350' AS DateTime), NULL, 0, NULL, NULL, NULL, N'600f74a3-06c7-4c4e-8859-69a8ab9d212c', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ba413e5b-21d8-42e5-912a-cd14e0d31683', N'9010002-010-011-0005', N'9010002', N'010', N'011', N'0005', N'', N'', N'', N'', N'5. Trợ cấp Phục viên', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:45.767' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6b9be279-f554-4ffb-8ea5-cd3fe5e4b194', N'9010003-010-011-6550-6551', N'9010003', N'010', N'011', N'6550', N'6551', N'', N'', N'', N'Văn phòng phẩm', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.330' AS DateTime), NULL, 0, NULL, NULL, NULL, N'566a0314-25dd-4ab6-9f33-b3779c438c61', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6ea05dc4-db4a-4013-9d08-ce0a8650b510', N'9010003-010-011-6650-6654', N'9010003', N'010', N'011', N'6650', N'6654', N'', N'', N'', N'Tiền thuê phòng ngủ (đối với đại biểu là khách mời)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:18.850' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'751a5278-b174-4ea7-a278-968e3c7c8894', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1e1dcd11-5c0c-4987-88a4-cec1a09ff779', N'9050002-010-011-0002', N'9050002', N'010', N'011', N'0002', N'', N'', N'', N'', N'Kinh phí chăm sóc sức khỏe ban đầu học sinh sinh viên ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-16T19:44:54.827' AS DateTime), CAST(N'2023-11-29T08:59:39.300' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'cf3d919f-997c-4f35-abed-40d1ef5f1f51', N'2cdf8f93-5d04-45f8-afcc-5100068321e4', 2024, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'862be0cd-0488-497e-b0ec-ced5294403c6', N'9010002-010-011-0004-0001-0002-00', N'9010002', N'010', N'011', N'0004', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:45.550' AS DateTime), NULL, 0, NULL, NULL, NULL, N'939834ed-9ed7-406a-8cc6-b8d4b2930c66', N'69183e88-7f92-473e-a228-47d369f839e7', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'58fa2f20-ca8e-40f8-a37b-cf038506e233', N'9010002-010-011-0002-0001-0004-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0004', N'00', N'', N'- Dưỡng sức, phục hồi SK', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:42.603' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'097b3961-33a9-4e67-a9cb-10cdf604d8dc', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd30a8df7-014c-4632-bdf3-cf812f9b2864', N'9020001-010-011-0002', N'9020001', N'010', N'011', N'0002', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:32.103' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ce7992c-f413-434c-b785-1b99d6cbd025', N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'94015955-8d31-4063-bf17-d09ded695036', N'9010003-010-011-7000-7049-0005', N'9010003', N'010', N'011', N'7000', N'7049', N'0005', N'', N'', N'- Chi hỗ trợ bệnh viện, bệnh xá KCB BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:25.060' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'1c72986d-0c37-4e1e-b997-75b6d26bee60', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f0ab1ba4-95c0-424a-a7e5-d178e440be47', N'9010003-010-011-6650-6655', N'9010003', N'010', N'011', N'6650', N'6655', N'', N'', N'', N'Thuê hội trường, phương tiện vận chuyển', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.150' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a7bfb1a0-f712-45bc-9a4d-41f850a4fd52', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'484ec5d7-3de2-4fbb-b486-d25f18074467', N'9010003-010-011-6750-6799', N'9010003', N'010', N'011', N'6750', N'6799', N'', N'', N'', N'Chi phí thuê mướn khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.990' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5b7a1ead-90a1-42c1-91f3-ef688f45f268', N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'58c5c71c-ed7b-4f45-8cba-d2a545eadd2d', N'9010002-010-011-0001-0001-0001-02-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:38.600' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0a70e747-0f95-4925-b6bd-0b91f4cf8ed1', N'0c64a47a-ae28-472f-82f7-a2d818d107ba', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'41c3d25c-b136-4df3-bb9f-d3421936ffb8', N'9010002-010-011-0002-0001-0003-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:42.150' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'6f83cc9b-2907-4417-b19b-72f705f090cb', N'cf731c26-af8a-4708-82de-c03bd8b38715', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5be44130-a84d-49f0-bdba-d3852022326c', N'903', N'903', N'', N'', N'', N'', N'', N'', N'', N'Thu BHYT thân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.933' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5a1234af-171a-44c9-98ed-9da7deff0188', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'96d24a10-56a7-4fac-b21f-d4ac2c653241', N'9010002-010-011-0008-0001-0001-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0001', N'00', N'', N'- Mai táng phí (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.577' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b66ca2a3-645d-4879-a0ca-6e4cb3c9b442', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1e797e96-c0ff-4220-95f6-d4e9ef4b0c47', N'9010003-010-011-7000', N'9010003', N'010', N'011', N'7000', N'', N'', N'', N'', N'Chi phí nghiệp vụ c.môn của từng ngành', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.653' AS DateTime), NULL, 0, NULL, NULL, NULL, N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b26f9196-136a-4774-8090-d59846aab3c9', N'9010004-010-011-0002', N'9010004', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:45:06.687' AS DateTime), CAST(N'2023-11-29T08:59:26.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', 2023, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'1c8a28c9-87d6-4a7f-b27a-d61da4bf09f1', N'9010001-010-011-0006', N'9010001', N'010', N'011', N'0006', N'', N'', N'', N'', N'6. Trợ cấp Xuất ngũ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:35.230' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'a0d4eadb-0deb-45a6-b3fc-2f00cebd9613', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f5296931-d706-4862-8203-d6a3522694bf', N'9010008-010-011-0001', N'9010008', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f118fa46-bf8c-48c7-b32a-9bde405d806a', N'73a85b99-cf80-4eab-b985-a83c70988b82', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'113662c6-7d9c-42eb-96b0-d6be38fd62b2', N'9010004-010-011-0001-0000', N'9010004', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:02.047' AS DateTime), CAST(N'2023-11-29T08:59:41.410' AS DateTime), NULL, 0, NULL, NULL, NULL, N'443cbbb4-f504-43b2-ac0f-77acfeac84c2', N'73357a2d-1879-452f-a85f-836a3090a537', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'eb4047c0-b7df-4ecd-b116-d8c138e84d01', N'9010002-010-011-0005-0001-0001-00', N'9010002', N'010', N'011', N'0005', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:46.340' AS DateTime), NULL, 0, NULL, NULL, NULL, N'da87b860-e7a5-4a0a-a1ae-d03ea1b81de9', N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'81a4dcd8-178d-4a5b-8290-d93865af85ba', N'9040002', N'9040002', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT thân nhân quân nhân và người lao động', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.780' AS DateTime), NULL, 0, NULL, NULL, NULL, N'503d8588-e537-4c72-a921-a437c1845d9e', N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'97e7cb0b-afcf-4b9b-8dc6-d9639a67b0ef', N'9010002-010-011-0004-0001-0001-00', N'9010002', N'010', N'011', N'0004', N'0001', N'0001', N'00', N'', N'-  Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:45.277' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'3d61c53a-3c3d-4a96-8dda-3565c77a2923', N'69183e88-7f92-473e-a228-47d369f839e7', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'cf84f16e-a7ad-4556-a006-db91e788dea2', N'9020002-010-011-0002', N'9020002', N'010', N'011', N'0002', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.047' AS DateTime), NULL, 0, NULL, NULL, NULL, N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', N'72d7a943-3288-4175-be6f-cf5190d2b908', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'41779ba1-73da-41be-9e49-dbccad2e32b5', N'9010004-010-011-0001-0003', N'9010004', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:41.863' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f95d9588-9394-4f73-a57f-23a464fdd002', N'73357a2d-1879-452f-a85f-836a3090a537', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'70cce940-7eb3-47d6-8f6c-dca68cc2142f', N'9030001', N'9030001', N'', N'', N'', N'', N'', N'', N'', N'I. Bảo hiểm y tế thân nhân quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:34.690' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4c52e8ab-654e-4b86-afbc-165823b677a2', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'be595d8b-e63d-45c6-81c8-dd5284ceb5c8', N'9010006-010-011-0002', N'9010006', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:55.943' AS DateTime), CAST(N'2023-11-29T08:59:29.490' AS DateTime), NULL, 0, NULL, NULL, NULL, N'043d854e-bca5-4ee9-905a-8b78d15b9887', N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', 2023, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'51e23309-2699-41f2-93e6-de27abea37c5', N'9010003-010-011-6900-6912', N'9010003', N'010', N'011', N'6900', N'6912', N'', N'', N'', N'Các thiết bị công nghệ thông tin ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:02:15.827' AS DateTime), CAST(N'2023-11-29T08:59:22.427' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'da8365b6-7e62-42eb-acf5-b7978c7298c5', N'87c7737b-0150-452a-b22e-02b356bd590f', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a976b247-393f-46e2-af57-de6bc3e0be66', N'9010003-010-011-6600-6618', N'9010003', N'010', N'011', N'6600', N'6618', N'', N'', N'', N'Khoán điện thoại', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.367' AS DateTime), NULL, 0, NULL, NULL, NULL, N'781048d2-925a-4fc9-b0c7-752d42b49bd4', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'00d47971-16e2-48d0-9a55-df6e7ff59061', N'9010002-010-011-0003-0001-0009-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0009', N'00', N'', N'- DS, PHSK sau TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:58:44.833' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8aec0461-a021-47fd-845f-c1512a792cb7', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ffb7f995-c6f4-4c2c-8090-e0a761b7f935', N'9010001-010-011-0002-0001-0003-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:30.823' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0e323456-0843-4c30-b020-e686be1f879b', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'0479e598-32cc-49b6-b8fb-e14cfe629c60', N'9010001-010-011-0002-0001-0004-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0004', N'00', N'', N'- Dưỡng sức, phục hồi SK', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:31.680' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8e092c28-89d2-460b-ab60-5aafbf9e010a', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f4e80f28-6f2c-4f84-b13c-e1d4f49574d1', N'9010001-010-011-0002-0001-0002-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:49:01.630' AS DateTime), CAST(N'2023-11-29T08:58:29.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'568c1146-2660-4db8-ad31-6e78d8c0daca', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8e417fd9-f13e-401d-85b9-e2094532786e', N'9030002', N'9030002', N'', N'', N'', N'', N'', N'', N'', N'II. Bảo hiểm y tế thân nhân CN, VCQP(CY khác)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:36.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9ad55244-03c4-4c97-ba26-27af54495842', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a05959d0-6c8d-4b14-8d0f-e2945c765165', N'9010001-010-011-0008-0001-0001-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0001', N'00', N'', N'- Mai táng phí (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:36.453' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'c1003754-545f-4a6b-acb1-ceb56a4a19c2', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bb51e919-76a4-4455-a769-e2e46e12a7e3', N'9010003-010-011-6700-6703', N'9010003', N'010', N'011', N'6700', N'6703', N'', N'', N'', N'Tiền thuê phòng ngủ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.970' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5d24dd88-913a-4eb5-b2e6-b0e767ea80a4', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a52fb8af-ea27-4ea7-9c86-e34e5d366dea', N'9010002-010-011-0004-0001-0001-00', N'9010002', N'010', N'011', N'0004', N'0001', N'0001', N'00', N'', N'-  Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T15:55:02.850' AS DateTime), CAST(N'2023-11-29T08:58:45.277' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'3d61c53a-3c3d-4a96-8dda-3565c77a2923', N'69183e88-7f92-473e-a228-47d369f839e7', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bdb85a6f-d247-41e6-b23e-e3ac8a7dac22', N'9010010-010-011-0001-0001', N'9010010', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'Mức 1: 1.800.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2577e235-3102-478d-8471-3bf14f0b8aca', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'a683d288-54b0-436f-bfee-e4307b658f8d', N'9020002-010-011-0001', N'9020002', N'010', N'011', N'0001', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:33.020' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8639c115-bdb3-49d5-9018-a1c9a3d24896', N'72d7a943-3288-4175-be6f-cf5190d2b908', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'241f590d-0688-4413-a52b-e4656fc6f40c', N'9010002-010-011-0002-0001-0003-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:42.150' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'6f83cc9b-2907-4417-b19b-72f705f090cb', N'cf731c26-af8a-4708-82de-c03bd8b38715', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'25d00a95-e7b8-4538-9652-e46c67e91580', N'9010003-010-011-6650-6658', N'9010003', N'010', N'011', N'6650', N'6658', N'', N'', N'', N'Chi bù tiền ăn', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.953' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0548f574-a5fb-4df5-a08f-a0a4cbad5b00', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'91108b08-acea-4eaa-b65a-e511f80c12b0', N'9010002-010-011-0001-0001-0001-01-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:37.923' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'bc6c9002-f7cc-47ad-8fe1-f80cab19ea01', N'9ebe3f5e-f6e8-4268-b744-583b24731221', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'3667cc11-eb91-4299-aa62-e586ed598b2b', N'9010003-010-011-7750', N'9010003', N'010', N'011', N'7750', N'', N'', N'', N'', N'Chi khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.280' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c60199d0-3566-4c28-91d5-cbde4cd4f792', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'862859fd-5211-4b35-876d-e5efb9d081c6', N'9010003-010-011-6650-6654', N'9010003', N'010', N'011', N'6650', N'6654', N'', N'', N'', N'Tiền thuê phòng ngủ (đối với đại biểu là khách mời)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:18.850' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'751a5278-b174-4ea7-a278-968e3c7c8894', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'abb33827-4957-4f01-8262-e7431a952233', N'9010004-010-011-0001', N'9010004', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:02.047' AS DateTime), CAST(N'2023-11-29T08:59:40.577' AS DateTime), NULL, 0, NULL, NULL, NULL, N'73357a2d-1879-452f-a85f-836a3090a537', N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c4b885b4-6b44-4450-acce-e76eacbc6fcc', N'9010002-010-011-0006', N'9010002', N'010', N'011', N'0006', N'', N'', N'', N'', N'6. Trợ cấp Xuất ngũ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:46.940' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'd8fd23af-c23c-4b8f-8858-05fe8e304970', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'11a56bdf-48fd-4aa8-a00b-e7fda8b8aaf0', N'9010003-010-011-6550-6551', N'9010003', N'010', N'011', N'6550', N'6551', N'', N'', N'', N'Văn phòng phẩm', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.330' AS DateTime), NULL, 0, NULL, NULL, NULL, N'566a0314-25dd-4ab6-9f33-b3779c438c61', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7200f48b-6735-47cd-8a42-e806e86b90ca', N'9010001-010-011-0008', N'9010001', N'010', N'011', N'0008', N'', N'', N'', N'', N'8. Tử tuất', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:36.280' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'f707d446-bee8-4299-b361-16ec2fb1d471', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'94bfe02c-94f5-454a-a885-e82dbc5bd4c2', N'9010010-010-011-0002-0002', N'9010010', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'Mức 2: 2.100.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:16:44.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6f657c41-02bf-45e6-9e47-776c9040b46f', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'12a3b318-33b8-4537-9743-e8b8700b31c4', N'9010001-010-011-0001-0001-0001-01-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:27.283' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'5c1357a4-f877-4c19-9bf6-f7c85e8c2f99', N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'48311a02-fbab-492b-8ceb-e97b75248170', N'9020001', N'9020001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.790' AS DateTime), NULL, 0, NULL, NULL, NULL, N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'727498ba-b42e-4451-9e87-ea0320d16e0e', N'9010001-010-011-0003-0001-0004-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0004', N'00', N'', N'- Trợ cấp phục hồi chức năng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:32.733' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'30b10c02-7a67-41b4-afe2-a4652321a14b', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'61eb3802-d497-4fce-8a47-ebbfdb08a0e4', N'905', N'905', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí CSSK ban đầu NLĐ và HSSV', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-16T21:51:32.197' AS DateTime), CAST(N'2023-11-29T08:59:41.117' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'2cdf8f93-5d04-45f8-afcc-5100068321e4', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'edfdbaad-9af8-466d-8a1b-ecf3a6323cb2', N'9010009-010-011-0002', N'9010009', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:19:58.153' AS DateTime), CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e0756e6f-a6bf-42cb-972e-f40b1ce667e9', N'09129a72-434f-452c-8b6c-d5159b6724c2', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c4246f8b-b3e7-49bb-a074-ecf64628bd42', N'9010004-010-011-0002-0003', N'9010004', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.753' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3d2fc884-12a6-45b6-8ecc-a2744f87ef34', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'523fcf16-f6a7-4624-9b64-ed9d7dfe72d2', N'9010002-010-011-0001-0001-0001-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'', N'+ Ốm khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T15:54:34.480' AS DateTime), CAST(N'2023-11-29T08:58:38.367' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'0c64a47a-ae28-472f-82f7-a2d818d107ba', N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b7532d61-47da-4a89-9d36-edb8f19daa70', N'9', N'9', N'', N'', N'', N'', N'', N'', N'', N'Thu, chi  BHXH, BHYT, BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T15:55:38.863' AS DateTime), CAST(N'2023-11-29T08:58:25.193' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'25aa0569-2843-4e00-bd95-b48558a6e028', NULL, 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ac4198d3-4ba2-4e3c-a27c-ee521015ea50', N'9020001-010-011-0002-0001', N'9020001', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:53:19.803' AS DateTime), CAST(N'2023-11-29T08:59:32.567' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'84ae6d38-5bfb-4c0e-96dc-dcdf8be8cfb7', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'43', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL, NULL, NULL, 0.015, 0.03, NULL, 0.01)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'14c337eb-d770-4f7f-8be1-ee937ca0acc9', N'9010002-010-011-0003-0001-0007-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0007', N'00', N'', N'- Chi hỗ trợ phòng ngừa (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:44.337' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8dd7347a-25eb-4be1-b097-c0c6214a40e3', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7a99526b-a769-40cc-ade9-f02a09a57aad', N'9010010-010-011-0002', N'9010010', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:16:44.000' AS DateTime), CAST(N'2023-12-06T11:13:39.497' AS DateTime), NULL, 0, NULL, NULL, NULL, N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', 2023, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'e0af603b-1799-432e-8fbe-f03b7af8baf5', N'9030001-010-011-0001-0001', N'9030001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'a. Bảo hiểm y tế thân nhân sĩ quan (thân nhân hàm CY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.260' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f5b01bd6-57b9-4523-9ed0-b8157a89e171', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd71aa1e4-845e-412e-a6f4-f052b2e8f4b1', N'9010002-010-011-0003-0001-0008-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0008', N'00', N'', N'- Chi hỗ trợ chuyển đổi nghề nghiệp (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:58:44.560' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5ce55373-18e4-43c6-965d-508d72db9ab5', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'f73f936e-a6eb-4b13-a2d6-f05347ec10c9', N'9010002-010-011-0002-0001-0003-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'', N'- Khám thai, KHH GĐ, nam nghỉ việc khi vợ sinh con(ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:41.933' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf731c26-af8a-4708-82de-c03bd8b38715', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'22a52907-c838-404b-b7da-f0b9bbf4d72f', N'9010003-010-011-7750-7799-0001', N'9010003', N'010', N'011', N'7750', N'7799', N'0001', N'', N'', N'- Chi thưởng cho tập thể, cá nhân thực hiện tốt công tác chi trả', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:26.017' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'994d7914-cf71-4767-99e4-697f10de2b78', N'65c08712-f281-4123-8efe-8848d21b220b', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'77fb3425-855b-4ff8-b8fb-f1efce235e2b', N'9010002-010-011-0003-0001-0002-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.320' AS DateTime), NULL, 0, NULL, NULL, NULL, N'171536b5-4867-40ab-a842-7284eeca8409', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'100c6ab2-25f9-403f-9b4c-f252dd55d6d9', N'9010002-010-011-0001', N'9010002', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:37.250' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'37b84743-0317-4d12-9166-f2d18480a21a', N'9010003-010-011-6700-6702', N'9010003', N'010', N'011', N'6700', N'6702', N'', N'', N'', N'Phụ cấp công tác phí', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.787' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4436044d-d91e-4f5f-819d-f9aa1290e0f9', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'dbd4cbae-8291-49fe-82a4-f2f7ad432009', N'9010006-010-011-0002-0001', N'9010006', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e8d7397c-9105-4d17-a4c8-678332804d6c', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'6b08e069-2228-4b52-8b5e-f33cc920e0eb', N'9010003-010-011-7000-7001', N'9010003', N'010', N'011', N'7000', N'7001', N'', N'', N'', N'Chi phí hàng hóa, vật tư ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.780' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ea128ac8-2407-4705-a4ba-5fc7c3b0c26a', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c473e853-777f-484d-9888-f34af10bc814', N'9010003-010-011-6750-6751', N'9010003', N'010', N'011', N'6750', N'6751', N'', N'', N'', N'Thuê phương tiện vận chuyển ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.817' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1ed591dc-2b4a-44f6-9f2f-51dbac62e209', N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd5e8c047-a025-4347-9a9b-f4877b9e684d', N'9010001-010-011-0002-0001-0003-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:30.823' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0e323456-0843-4c30-b020-e686be1f879b', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'4ae74663-2dbc-4bae-b2de-f5da2c8f8351', N'9010004-010-011-0001-0003', N'9010004', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:41.863' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f95d9588-9394-4f73-a57f-23a464fdd002', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'5d549df5-b306-4e1a-9978-f622fb585564', N'9010010-010-011-0002-0005', N'9010010', N'010', N'011', N'0002', N'0005', N'', N'', N'', N'Mức 5: 2.900.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd60de627-81e8-4052-91b0-53e6e5431eac', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'11dbd638-89b5-495f-9816-f7a26189a9d2', N'9010002-010-011-0002-0001-0001-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'01', N'+ Lao động nữ (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:40.443' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'aaa3dd36-bf5d-4615-9e90-83db2e3cde00', N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'aba0b8ad-8c26-4e2a-9e93-f7db61714654', N'9010002-010-011-0008-0001-0002-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0002', N'00', N'', N'- Tuất 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.770' AS DateTime), NULL, 0, NULL, NULL, NULL, N'af6267f2-5472-4ea7-bb18-c36596b39611', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'3c70011d-a79a-464b-ae78-f9651b736eaf', N'9010006-010-011-0002-0001', N'9010006', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e8d7397c-9105-4d17-a4c8-678332804d6c', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'12e1b25b-ed38-412d-a1b3-f97556672caf', N'9010003-010-011-6950-6956', N'9010003', N'010', N'011', N'6950', N'6956', N'', N'', N'', N'Các thiết bị công nghệ thông tin ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.080' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5da67169-8f6e-41e6-a6e4-85d9430d8d2a', N'87309c2d-5489-4e52-90c2-a97779e3b5c0', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8f20d3ea-a972-4960-b6f1-f9d9d68ebbeb', N'9010002-010-011-0006-0001-0001-00', N'9010002', N'010', N'011', N'0006', N'0001', N'0001', N'00', N'', N'- Trợ cấp XN 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:47.160' AS DateTime), NULL, 0, NULL, NULL, NULL, N'fd8cb880-dd35-4db5-a71d-e47c97168672', N'd8fd23af-c23c-4b8f-8858-05fe8e304970', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'538a21fd-48b9-4915-b4c7-f9dd6922a43b', N'9010004-010-011-0001-0001', N'9010004', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:41.233' AS DateTime), NULL, 0, NULL, NULL, NULL, N'48112d8e-a78f-4a4c-8492-94cf989a8f7a', N'73357a2d-1879-452f-a85f-836a3090a537', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'b2523e22-b65e-4b76-9e10-fa7a415855b5', N'9010002-010-011-0001-0001-0001-01-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:38.153' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'1bf2509b-91d5-4974-aff0-7f7c426cddc4', N'9ebe3f5e-f6e8-4268-b744-583b24731221', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'bd9d4b77-6fc7-495a-80bd-fb1863ad1f87', N'9010003-010-011-6600-6606', N'9010003', N'010', N'011', N'6600', N'6606', N'', N'', N'', N'Tuyên truyền (phát thanh, truyền hình)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.883' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f42345ce-969b-482c-a5f5-570b0d4cdf36', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'7d38ea02-1baf-484b-b544-fb19d6596da3', N'9010002-010-011-0005', N'9010002', N'010', N'011', N'0005', N'', N'', N'', N'', N'5. Trợ cấp Phục viên', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:45.767' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'ea716a14-c0ed-4c7f-a554-fb4deb95497a', N'9010001-010-011-0004-0001-0001-00', N'9010001', N'010', N'011', N'0004', N'0001', N'0001', N'00', N'', N'-  Trợ cấp 1 lần', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:34.270' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'5cca7dd1-b4e8-4273-86cc-ea4ff207409b', N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'71fb9458-f068-4301-8406-fb5603cfe1c3', N'9020002-010-011-0001-0001', N'9020002', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'2. QNCN', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.560' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e89cbafd-a9b0-4b68-890b-d1d7db5b4824', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, 0.175, 0.08, 0.045, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'290247c6-691f-4bcf-8479-fbc7fe028fdf', N'9010004-010-011-0002-0002', N'9010004', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.587' AS DateTime), NULL, 0, NULL, NULL, NULL, N'863a6a36-ed30-4f8f-b0f6-757aab07aee5', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8704ccaf-4357-4dc3-94e8-fc404a98c921', N'9010001-010-011-0007-0001-0002-00', N'9010001', N'010', N'011', N'0007', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:36.067' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'f10c833a-c6a4-48d9-82ee-51682f8abe0b', N'58a28603-1d42-40be-bbb3-56f7c15d69ef', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8fb319c9-721f-453a-8195-fca1ba800625', N'9030002', N'9030002', N'', N'', N'', N'', N'', N'', N'', N'II. Bảo hiểm y tế thân nhân CN, VCQP(CY khác)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:36.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9ad55244-03c4-4c97-ba26-27af54495842', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'c1a233ad-f800-4756-ac3e-fca57f3e68d5', N'9050001-010-011-0001', N'9050001', N'010', N'011', N'0001', N'', N'', N'', N'', N'Kinh phí chăm sóc sức khỏe ban đầu người lao động ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-16T19:44:54.827' AS DateTime), CAST(N'2023-11-29T08:59:39.073' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'a519c162-3f2e-47ef-9cdf-2da72cfa40b7', N'2cdf8f93-5d04-45f8-afcc-5100068321e4', 2024, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'daa59436-3c92-402c-9158-fd48c52ad042', N'9020002-010-011-0002-0000', N'9020002', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.297' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3bdd2a48-b367-464b-b253-2406afc558f1', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 0.015, 0.03, NULL, 0.01)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'd620eaf0-476b-4cb6-98b1-fd778e6559ee', N'9010002-010-011-0001-0001-0001-02-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:38.600' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0a70e747-0f95-4925-b6bd-0b91f4cf8ed1', N'0c64a47a-ae28-472f-82f7-a2d818d107ba', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'938e06e7-57b7-4688-a745-fe0bde854a44', N'9020001-010-011-0002', N'9020001', N'010', N'011', N'0002', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:32.103' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ce7992c-f413-434c-b785-1b99d6cbd025', N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'91d541af-048b-4701-9435-fea3bf071ab1', N'9010009-010-011-0001', N'9010009', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b45699c0-25f5-4f79-b26f-bed14c2fb846', N'09129a72-434f-452c-8b6c-d5159b6724c2', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'8ab2353c-57a6-4aad-8cbb-ff54a8cdea01', N'9010001-010-011-0007', N'9010001', N'010', N'011', N'0007', N'', N'', N'', N'', N'7. Trợ cấp thôi việc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:35.660' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'58a28603-1d42-40be-bbb3-56f7c15d69ef', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh], [fTyLe_BHXH_NSD], [fTyLe_BHXH_NLD], [fTyLe_BHYT_NSD], [fTyLe_BHYT_NLD], [fTyLe_BHTN_NSD], [fTyLe_BHTN_NLD]) VALUES (N'49b9d4cc-1f73-4ddc-91ce-ffd82940aff3', N'9010001-010-011-0003-0001-0009-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0009', N'00', N'', N'- DS, PHSK sau TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:33.873' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'77ec4558-40fd-4d3b-864d-df96aceccb53', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
