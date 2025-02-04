/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]    Script Date: 8/27/2024 2:41:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_giai_thich_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 8/27/2024 2:41:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 8/27/2024 2:41:24 PM ******/
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
		  sLNS,
		  iID_MaDonVi,
		  iNamLamViec)
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
			mlns.sLNS,
			@MaDonVi,
			@YearOfWork
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
			sNoiDung,
			fLuongChinh,
			fPCChucVu,
			fPCTNNghe,
			fPCTNVuotKhung,
			fNghiOm,
			fHSBL)
	SELECT 
			getdate(),
			sum(isnull(ctct.fConPhaiNopTiep, 0)),
			sum(isnull(ctct.fDaNop_TrongQuyNam, 0)),
			sum(isnull(ctct.fPhaiNop_BHXH, 0)),
			sum(isnull(ctct.fPhaiNop_QuyNamTruoc, 0)),
			sum(isnull(ctct.fPhaiNop_TrongQuyNam, 0)),
			sum(isnull(ctct.fQuyTienLuongCanCu, 0)),
			sum(isnull(ctct.fSoConPhaiNop, 0)),
			sum(isnull(ctct.fSoDaNopSau31_12, 0)),
			sum(isnull(ctct.fSoDaNopTrongNam, 0)),
			sum(isnull(ctct.fSoPhaiThuNop, 0)),
			sum(isnull(ctct.fSoTienGiamDong, 0)),
			sum(isnull(ctct.fTongSoDaNop, 0)),
			sum(isnull(ctct.fTongTruyThu_BHXH, 0)),
			sum(isnull(ctct.fTruyThu_BHTN_NLD, 0)),
			sum(isnull(ctct.fTruyThu_BHTN_NSD, 0)),
			sum(isnull(ctct.fTruyThu_BHTN_TongCong, 0)),
			sum(isnull(ctct.fTruyThu_BHXH_NLD, 0)),
			sum(isnull(ctct.fTruyThu_BHXH_NSD, 0)),
			sum(isnull(ctct.fTruyThu_BHXH_TongCong, 0)),
			sum(isnull(ctct.fTruyThu_BHYT_NLD, 0)),
			sum(isnull(ctct.fTruyThu_BHYT_NSD, 0)),
			sum(isnull(ctct.fTruyThu_BHYT_TongCong, 0)),
			sum(isnull(ctct.fTruyThu_QuyNamTruoc, 0)),
			ctct.iID_MLNS,
			@MaDonVi,
			ctct.ILoaiGiaiThich,
			@YearOfWork,
			sum(isnull(ctct.iQuanSo, 0)),
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
			ctct.sNoiDung,
			sum(isnull(ctct.fLuongChinh, 0)),
			sum(isnull(ctct.fPCChucVu, 0)),
			sum(isnull(ctct.fPCTNNghe, 0)),
			sum(isnull(ctct.fPCTNVuotKhung, 0)),
			sum(isnull(ctct.fNghiOm, 0)),
			sum(isnull(ctct.fHSBL, 0))
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]    Script Date: 8/27/2024 2:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_truy_thu]
	@NamLamViec int,
	@VoucherID uniqueidentifier,
	@MaDonVi nvarchar(50),
	@VoucherType int
AS
BEGIN

	select
		sum(isnull(fTruyThu_BHXH_NLD, 0)) FTruyThuBHXHNLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) FTruyThuBHXHNSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) FTruyThuBHXHTongCong,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) FTruyThuBHYTNLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) FTruyThuBHYTNSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) FTruyThuBHYTTongCong,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) FTruyThuBHTNNLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) FTruyThuBHTNNSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) FTruyThuBHTNTongCong,
		((sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) + (sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) + (sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0)))) FTongTruyThuBHXH,
		sum(isnull(fLuongChinh, 0)) FLuongChinh,
		sum(isnull(fPCChucVu, 0)) FPCChucVu,
		sum(isnull(fPCTNNghe, 0)) FPCTNNghe,
		sum(isnull(fPCTNVuotKhung, 0)) FPCTNVuotKhung,
		sum(isnull(fNghiOm, 0)) FNghiOm,
		sum(isnull(fHSBL, 0)) FHSBL,
		iID_MaDonVi IIDMaDonVi,
		sXauNoiMa SXauNoiMa,
		1 IsModified
	into #MonthQuarterData
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iNamLamViec = @NamLamViec
		and iQuyNamLoai <> 2
		and iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
	group by iID_MaDonVi, sXauNoiMa
	
	select
			chungtudonvi.iID_QT_CTCT_GiaiThich,
			mlns.iID_MLNS,
			mlns.sMoTa sNoiDung,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sLNS,
			mlns.sXauNoiMa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			chungtudonvi.fTruyThu_BHXH_NLD fTruyThu_BHXH_NLD,
			chungtudonvi.fTruyThu_BHXH_NSD fTruyThu_BHXH_NSD,
			chungtudonvi.fTruyThu_BHXH_TongCong fTruyThu_BHXH_TongCong,
			chungtudonvi.fTruyThu_BHYT_NLD fTruyThu_BHYT_NLD,
			chungtudonvi.fTruyThu_BHYT_NSD fTruyThu_BHYT_NSD,
			chungtudonvi.fTruyThu_BHYT_TongCong fTruyThu_BHYT_TongCong,
			chungtudonvi.fTruyThu_BHTN_NLD fTruyThu_BHTN_NLD,
			chungtudonvi.fTruyThu_BHTN_NSD fTruyThu_BHTN_NSD,
			chungtudonvi.fTruyThu_BHTN_TongCong fTruyThu_BHTN_TongCong,
			(chungtudonvi.fTruyThu_BHXH_TongCong + chungtudonvi.fTruyThu_BHYT_TongCong + chungtudonvi.fTruyThu_BHTN_TongCong) fTongTruyThu_BHXH,
			chungtudonvi.sKienNghi,
			chungtudonvi.fPhaiNop_BHXH FPhaiNopBHXH,
			chungtudonvi.fPhaiNop_TrongQuyNam FPhaiNopTrongQuyNam,
			chungtudonvi.fTruyThu_QuyNamTruoc FTruyThuQuyNamTruoc,
			chungtudonvi.fPhaiNop_QuyNamTruoc FPhaiNopQuyNamTruoc,
			chungtudonvi.fDaNop_TrongQuyNam FDaNopTrongQuyNam,
			chungtudonvi.fConPhaiNopTiep,
			chungtudonvi.fSoPhaiThuNop,
			chungtudonvi.fSoDaNopTrongNam,
			chungtudonvi.fSoDaNopSau31_12,
			chungtudonvi.fTongSoDaNop,
			chungtudonvi.fSoConPhaiNop,
			chungtudonvi.iQuanSo,
			chungtudonvi.fQuyTienLuongCanCu,
			chungtudonvi.fSoTienGiamDong,
			chungtudonvi.dTuNgay,
			chungtudonvi.dDenNgay,
			chungtudonvi.sNguoiTao,
			chungtudonvi.dNgayTao,
			chungtudonvi.dNgaySua,
			chungtudonvi.sNguoiSua,
			case when @VoucherType = 2 and isnull(chungtudonvi.fLuongChinh, 0) = 0 then dttq.FLuongChinh
			else chungtudonvi.fLuongChinh end FLuongChinh,
			case when @VoucherType = 2 and isnull(chungtudonvi.fPCChucVu, 0) = 0 then dttq.fPCChucVu
			else chungtudonvi.fPCChucVu end FPCChucVu,
			case when @VoucherType = 2 and isnull(chungtudonvi.fPCTNNghe, 0) = 0 then dttq.fPCTNNghe
			else chungtudonvi.fPCTNNghe end FPCTNNghe,
			case when @VoucherType = 2 and isnull(chungtudonvi.fPCTNVuotKhung, 0) = 0 then dttq.fPCTNVuotKhung
			else chungtudonvi.fPCTNVuotKhung end FPCTNVuotKhung,
			case when @VoucherType = 2 and isnull(chungtudonvi.fNghiOm, 0) = 0 then dttq.fNghiOm
			else chungtudonvi.fNghiOm end FNghiOm,
			case when @VoucherType = 2 and isnull(chungtudonvi.fHSBL, 0) = 0 then dttq.fHSBL
			else chungtudonvi.fHSBL end FHSBL,
			case when @VoucherType = 2 and ((select count(1) from #MonthQuarterData) > 0) 
					and ((isnull(chungtudonvi.fLuongChinh, 0) = 0 and isnull(dttq.fLuongChinh, 0) <> 0) or (isnull(chungtudonvi.fPCChucVu, 0) = 0 and isnull(dttq.fPCChucVu, 0) <> 0) or (isnull(chungtudonvi.fPCTNNghe, 0) = 0 and isnull(dttq.fPCTNNghe, 0) <> 0) or (isnull(chungtudonvi.fPCTNVuotKhung, 0) = 0 and isnull(dttq.fPCTNVuotKhung, 0) <> 0) or (isnull(chungtudonvi.fNghiOm, 0) = 0 and isnull(dttq.fNghiOm, 0) <> 0) or (isnull(chungtudonvi.fHSBL, 0) = 0 and isnull(dttq.fHSBL, 0) <> 0)) then dttq.IsModified
			else 0 end IsModified
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
				ctgt.sKienNghi,
				ctgt.fPhaiNop_BHXH,
				ctgt.fPhaiNop_TrongQuyNam,
				ctgt.fTruyThu_QuyNamTruoc,
				ctgt.fPhaiNop_QuyNamTruoc,
				ctgt.fDaNop_TrongQuyNam,
				ctgt.fConPhaiNopTiep,
				ctgt.fTruyThu_BHXH_NLD,
				ctgt.fTruyThu_BHXH_NSD,
				ctgt.fTruyThu_BHXH_TongCong,
				ctgt.fTruyThu_BHYT_NLD,
				ctgt.fTruyThu_BHYT_NSD,
				ctgt.fTruyThu_BHYT_TongCong,
				ctgt.fTruyThu_BHTN_NLD,
				ctgt.fTruyThu_BHTN_NSD,
				ctgt.fTruyThu_BHTN_TongCong,
				ctgt.fTongTruyThu_BHXH,
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
				ctgt.iID_MLNS,
				ctgt.fLuongChinh,
				ctgt.fPCChucVu,
				ctgt.fPCTNNghe,
				ctgt.fPCTNVuotKhung,
				ctgt.fNghiOm,
				ctgt.fHSBL
				from
				BH_QTT_BHXH_ChungTu ct
				join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				join BH_QTT_BHXH_CTCT_GiaiThich ctgt on ct.iID_QTT_BHXH_ChungTu = ctgt.iID_QTT_BHXH_ChungTu
				where
					ct.iID_QTT_BHXH_ChungTu = @VoucherID
					and ctgt.iID_MaDonVi = @MaDonVi
				) chungtudonvi on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				left join #MonthQuarterData dttq on mlns.sXauNoiMa = dttq.sXauNoiMa
			order by mlns.sXauNoiMa
END
;
GO


/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_bang_loi]    Script Date: 8/30/2024 11:05:14 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_giai_thich_bang_loi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_bang_loi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_giai_thich_bang_loi]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_giai_thich_bang_loi] 
	@VoucherID uniqueidentifier
AS
BEGIN

	select sum((isnull(ctct.fThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0))) FPhaiNopTrongQuyNam,
	sum((isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0))) FTruyThuQuyNamTruoc,
	'ALL' SLoaiThu
	from BH_QTT_BHXH_ChungTu_ChiTiet ctct
	left join BH_QTT_BHXH_CTCT_GiaiThich gt on ctct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
	where ctct.iID_QTT_BHXH_ChungTu = @VoucherID

	union all

	select sum((isnull(ctct.fThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0))) FPhaiNopTrongQuyNam,
	sum((isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0))) FTruyThuQuyNamTruoc,
	'BHXH' SLoaiThu
	from BH_QTT_BHXH_ChungTu_ChiTiet ctct
	left join BH_QTT_BHXH_CTCT_GiaiThich gt on ctct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
	where ctct.iID_QTT_BHXH_ChungTu = @VoucherID

	union all

	select sum((isnull(ctct.fThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0))) FPhaiNopTrongQuyNam,
	sum((isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0))) FTruyThuQuyNamTruoc,
	'BHYT' SLoaiThu
	from BH_QTT_BHXH_ChungTu_ChiTiet ctct
	left join BH_QTT_BHXH_CTCT_GiaiThich gt on ctct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
	where ctct.iID_QTT_BHXH_ChungTu = @VoucherID

	union all

	select sum((isnull(ctct.fThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0))) FPhaiNopTrongQuyNam,
	sum((isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0))) FTruyThuQuyNamTruoc,
	'BHTN' SLoaiThu
	from BH_QTT_BHXH_ChungTu_ChiTiet ctct
	left join BH_QTT_BHXH_CTCT_GiaiThich gt on ctct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
	where ctct.iID_QTT_BHXH_ChungTu = @VoucherID

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang]
	@NamLamViec int,
	@Quy int,
	@LoaiQuy int,
	@MaDonVi nvarchar(500),
	@Donvitinh int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(500)

	IF (@Quy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@Quy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@Quy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@Quy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@Quy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@Quy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@Quy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	select temp.* from (
	select distinct
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		---ctgt.iID_MaDonVi,
		ctgt.iNamLamViec,
		sum(isnull(ctgt.fLuongChinh, 0))/@Donvitinh fLuongChinh,
		sum(isnull(ctgt.fPCChucVu, 0))/@Donvitinh fPCChucVu,
		sum(isnull(ctgt.fPCTNNghe, 0))/@Donvitinh fPCTNNghe,
		sum(isnull(ctgt.fPCTNVuotKhung, 0))/@Donvitinh fPCTNVuotKhung,
		sum(isnull(ctgt.fNghiOm, 0))/@Donvitinh fNghiOm,
		sum(isnull(ctgt.fHSBL, 0))/@Donvitinh fHSBL,
		(sum(isnull(ctgt.fLuongChinh, 0)) + sum(isnull(ctgt.fPCChucVu, 0)) + sum(isnull(ctgt.fPCTNNghe, 0)) + sum(isnull(ctgt.fPCTNVuotKhung, 0)) + sum(isnull(ctgt.fNghiOm, 0)) + sum(isnull(ctgt.fHSBL, 0)))/@Donvitinh FTongQuyLuong,
		sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0))/@Donvitinh fTruyThu_BHXH_NLD,
		sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0))/@Donvitinh fTruyThu_BHXH_NSD,
		(sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0)))/@Donvitinh fTruyThu_BHXH_TongCong,
		sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0))/@Donvitinh fTruyThu_BHYT_NLD,
		sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0))/@Donvitinh fTruyThu_BHYT_NSD,
		(sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0)))/@Donvitinh fTruyThu_BHYT_TongCong,
		sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0))/@Donvitinh fTruyThu_BHTN_NLD,
		sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0))/@Donvitinh fTruyThu_BHTN_NSD,
		(sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0)))/@Donvitinh fTruyThu_BHTN_TongCong,
		(sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0)))/@Donvitinh fTongTruyThu_BHXH
	from BH_DM_MucLucNganSach mlns
	left join BH_QTT_BHXH_CTCT_GiaiThich ctgt on mlns.iID_MLNS = ctgt.iID_MLNS
												and ctgt.iQuyNam in (SELECT * FROM f_split(@SMonths))
												and ctgt.iQuyNamLoai = 0
												and ctgt.iNamLamViec = @NamLamViec
												and ctgt.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
												and ctgt.iLoaiGiaiThich = 2
	where mlns.sLNS like '902%' and mlns.sLNS <> '902'
		and mlns.iNamLamViec = @NamLamViec
	group by mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		--ctgt.iID_MaDonVi,
		ctgt.iNamLamViec) temp
	order by temp.sXauNoiMa

	
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy]
	@NamLamViec int,
	@Quy int,
	@LoaiQuy int,
	@MaDonVi nvarchar(500),
	@Donvitinh int,
	@IsLuyKe bit
AS
BEGIN

	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
	select temp.* from (
	select distinct
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		--ctgt.iID_MaDonVi,
		ctgt.iNamLamViec,
		sum(isnull(ctgt.fLuongChinh, 0))/@Donvitinh fLuongChinh,
		sum(isnull(ctgt.fPCChucVu, 0))/@Donvitinh fPCChucVu,
		sum(isnull(ctgt.fPCTNNghe, 0))/@Donvitinh fPCTNNghe,
		sum(isnull(ctgt.fPCTNVuotKhung, 0))/@Donvitinh fPCTNVuotKhung,
		sum(isnull(ctgt.fNghiOm, 0))/@Donvitinh fNghiOm,
		sum(isnull(ctgt.fHSBL, 0))/@Donvitinh fHSBL,
		(sum(isnull(ctgt.fLuongChinh, 0)) + sum(isnull(ctgt.fPCChucVu, 0)) + sum(isnull(ctgt.fPCTNNghe, 0)) + sum(isnull(ctgt.fPCTNVuotKhung, 0)) + sum(isnull(ctgt.fNghiOm, 0)) + sum(isnull(ctgt.fHSBL, 0)))/@Donvitinh FTongQuyLuong,
		sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0))/@Donvitinh fTruyThu_BHXH_NLD,
		sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0))/@Donvitinh fTruyThu_BHXH_NSD,
		(sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0)))/@Donvitinh fTruyThu_BHXH_TongCong,
		sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0))/@Donvitinh fTruyThu_BHYT_NLD,
		sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0))/@Donvitinh fTruyThu_BHYT_NSD,
		(sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0)))/@Donvitinh fTruyThu_BHYT_TongCong,
		sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0))/@Donvitinh fTruyThu_BHTN_NLD,
		sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0))/@Donvitinh fTruyThu_BHTN_NSD,
		(sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0)))/@Donvitinh fTruyThu_BHTN_TongCong,
		(sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0)))/@Donvitinh fTongTruyThu_BHXH
	from BH_DM_MucLucNganSach mlns
	left join BH_QTT_BHXH_CTCT_GiaiThich ctgt on mlns.iID_MLNS = ctgt.iID_MLNS
												and ctgt.iQuyNam = @Quy
												and ctgt.iQuyNamLoai = @LoaiQuy
												and ctgt.iNamLamViec = @NamLamViec
												and ctgt.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
												and ctgt.iLoaiGiaiThich = 2
	where mlns.sLNS like '902%' and mlns.sLNS <> '902'
		and mlns.iNamLamViec = @NamLamViec
	group by
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		--ctgt.iID_MaDonVi,
		ctgt.iNamLamViec,
		mlns.sXauNoiMa) temp
	order by temp.sXauNoiMa

	end
	-- In luy ke
	else
	begin
	select temp.* from (
	select distinct
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		--ctgt.iID_MaDonVi,
		ctgt.iNamLamViec,
		sum(isnull(ctgt.fLuongChinh, 0))/@Donvitinh fLuongChinh,
		sum(isnull(ctgt.fPCChucVu, 0))/@Donvitinh fPCChucVu,
		sum(isnull(ctgt.fPCTNNghe, 0))/@Donvitinh fPCTNNghe,
		sum(isnull(ctgt.fPCTNVuotKhung, 0))/@Donvitinh fPCTNVuotKhung,
		sum(isnull(ctgt.fNghiOm, 0))/@Donvitinh fNghiOm,
		sum(isnull(ctgt.fHSBL, 0))/@Donvitinh fHSBL,
		(sum(isnull(ctgt.fLuongChinh, 0)) + sum(isnull(ctgt.fPCChucVu, 0)) + sum(isnull(ctgt.fPCTNNghe, 0)) + sum(isnull(ctgt.fPCTNVuotKhung, 0)) + sum(isnull(ctgt.fNghiOm, 0)) + sum(isnull(ctgt.fHSBL, 0)))/@Donvitinh FTongQuyLuong,
		sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0))/@Donvitinh fTruyThu_BHXH_NLD,
		sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0))/@Donvitinh fTruyThu_BHXH_NSD,
		(sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0)))/@Donvitinh fTruyThu_BHXH_TongCong,
		sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0))/@Donvitinh fTruyThu_BHYT_NLD,
		sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0))/@Donvitinh fTruyThu_BHYT_NSD,
		(sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0)))/@Donvitinh fTruyThu_BHYT_TongCong,
		sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0))/@Donvitinh fTruyThu_BHTN_NLD,
		sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0))/@Donvitinh fTruyThu_BHTN_NSD,
		(sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0)))/@Donvitinh fTruyThu_BHTN_TongCong,
		(sum(isnull(ctgt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctgt.fTruyThu_BHTN_NSD, 0)))/@Donvitinh fTongTruyThu_BHXH
	from BH_DM_MucLucNganSach mlns
	left join BH_QTT_BHXH_CTCT_GiaiThich ctgt on mlns.iID_MLNS = ctgt.iID_MLNS
												and ctgt.iQuyNam <= @Quy
												and ctgt.iQuyNamLoai = @LoaiQuy
												and ctgt.iNamLamViec = @NamLamViec
												and ctgt.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
												and ctgt.iLoaiGiaiThich = 2
	where mlns.sLNS like '902%' and mlns.sLNS <> '902'
		and mlns.iNamLamViec = @NamLamViec
	group by
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		--ctgt.iID_MaDonVi,
		ctgt.iNamLamViec,
		mlns.sXauNoiMa) temp
	order by temp.sXauNoiMa

	end
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
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
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempchungtudonvi]') AND type in (N'U')) drop table tempchungtudonvi;

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVi;

	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,ctct.iQSBQNam
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
			,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
			,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
			,(isnull(ctct.fPCTNVuotKhung, 0) +isnull( gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
			,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
		into tempchungtudonvi
		from
			BH_QTT_BHXH_ChungTu ct
			join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
		where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			--and ct.iID_MaDonVi = @IdDonVi
			and ((@isCha = 0 and ctct.iID_MaDonVi = @IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi = @IdDonVi))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
	end
	-- In luy ke
	else
	begin
		select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
			,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPCChucVu
			,(sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
			,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
			,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
			,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(ctct.fHSBL, 0))) fHSBL
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) + sum(isnull(ctct.fHSBL, 0)) + sum(isnull(ctct.fHSBL, 0))) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
			,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
			,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0))+ sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
			,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0))+ sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			into tempchungtudonvi
			from
				BH_QTT_BHXH_ChungTu ct
				join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
			where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam <= @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi = @IdDonVi
				and ((@isCha = 0 and ctct.iID_MaDonVi = @IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi = @IdDonVi))
				and ct.iLoaiTongHop = 1
				--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
			group by
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.sGhiChu,
				ctct.iID_MLNS,
				ctct.iID_MLNS_Cha,
				ctct.sXauNoiMa,
				ctct.sLNS
		end

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
		left join tempchungtudonvi chungtudonvi 
		on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa

		IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempchungtudonvi]') AND type in (N'U')) drop table tempchungtudonvi;
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]
	@NamLamViec int,
	@IdDonVi nvarchar(500),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(500)
	DECLARE @SLoaiQuy NVARCHAR(500)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVi;

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
		(select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
			,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPCChucVu
			,(sum(isnull(ctct.fPCTNNghe, 0))+ sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
			,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
			,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
			,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fHSBL
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0))+ sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) + sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
			,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
			,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
			,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
		from
		BH_QTT_BHXH_ChungTu ct
		join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
		where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi = @IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi = @IdDonVi))
			--and ct.iID_MaDonVi = @IdDonVi
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;
	DECLARE @sSoChungTuTH nvarchar(1000)

	--Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		set @sSoChungTuTH = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iQuyNamLoai = @ILoaiQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
	end
	--In luy ke
	else
	begin
	set @sSoChungTuTH = (SELECT SUBSTRING(( SELECT ',' + pr.sTongHop  AS [text()] FROM BH_QTT_BHXH_ChungTu pr
														WHERE pr.iNamLamViec = @NamLamViec
															and pr.iQuyNam <= @IQuy
															and pr.iQuyNamLoai = @ILoaiQuy
															and pr.iID_MaDonVi = @IdDonVis
															and pr.iLoaiTongHop = 2
															and pr.bDaTongHop = 0
														FOR XML PATH (''), TYPE
													).value('text()[1]','nvarchar(max)'), 2, 1000) )
	end

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
--Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
			,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
			,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
			,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
			,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 1;
	end
	--In luy ke
	else
	begin
		select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
			,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPCChucVu
			,(sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
			,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
			,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
			,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fHSBL
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) + sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
			,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
			,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
			,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam <= @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 1
			group by
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
	end
----------------END DETAIL----------------
----------------INSERT TOTAL----------------
--Ko In luy ke
	if (@IsLuyKe = 0)
	begin
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
				(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
				chungtudonvi.fDuToan,
				chungtudonvi.fDaQuyetToan,
				chungtudonvi.fConLai,
				chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
				chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
				chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
				chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
				(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
				chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
				chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
				(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)) fTongNLD,
				(isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)) fTongNSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong,
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
					select distinct
					ct.iID_MaDonVi,
					ct.iNamLamViec,
					ctct.iID_QTT_BHXH_ChungTu_ChiTiet
					,ctct.iID_QTT_BHXH_ChungTu
					,ctct.iQSBQNam
					,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
					,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
					,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
					,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
					,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
					,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
					,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
					,ctct.fDuToan
					,ctct.fDaQuyetToan
					,ctct.fConLai
					,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
					,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
					,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
					,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
					,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
					,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
					,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
					,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
					,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
					,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
					from
					BH_QTT_BHXH_ChungTu ct
					join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
					left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
					where ct.iNamLamViec = @NamLamViec
					and ct.iQuyNam = @IQuy
					and ct.iQuyNamLoai = @ILoaiQuy
					and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
					--and ct.iID_MaDonVi = @IdDonVis
					and ct.iLoaiTongHop = 2
		--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end			
				)chungtudonvi 
					on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				ORDER BY mlns.sXauNoiMa;
		end
		--In luy ke
		else
		begin
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
				(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
				chungtudonvi.fDuToan,
				chungtudonvi.fDaQuyetToan,
				chungtudonvi.fConLai,
				chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
				chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
				chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
				chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
				(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
				chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
				chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
				(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)) fTongNLD,
				(isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)) fTongNSD,
				(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong,
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
				LEFT JOIN (
					select distinct
					ct.iID_MaDonVi,
					ct.iNamLamViec
					,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
					,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
					,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPCChucVu
					,(sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
					,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
					,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
					,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fHSBL
					,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) + sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fTongQTLN
					,sum(isnull(ctct.fDuToan, 0)) fDuToan
					,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
					,sum(isnull(ctct.fConLai, 0)) fConLai
					,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
					,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
					,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
					,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
					,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
					,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
					,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
					,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
					,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
					,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
					from
					BH_QTT_BHXH_ChungTu ct
					join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
					left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
					where ct.iNamLamViec = @NamLamViec
					and ct.iQuyNam <= @IQuy
					and ct.iQuyNamLoai = @ILoaiQuy
					and ((@isCha = 0 and ctct.iID_MaDonVi = @IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi = @IdDonVis))
					--and ct.iID_MaDonVi = @IdDonVis
					and ct.iLoaiTongHop = 2
		--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end	
					group by 
					ct.iID_MaDonVi,
					ct.iNamLamViec
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
				)chungtudonvi 
					on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				ORDER BY mlns.sXauNoiMa;
		end

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
	'   ' + dv.sTenDonVi,
	tempChiTietDonVi.iNamLamViec ,
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
	FROM tempChiTietDonVi 
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

DROP TABLE #result;
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

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
			select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam iQSBQNam
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
				,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
				,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
				,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
				,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO #tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
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
			sum((isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0))/@Donvitinh fThu_BHYT_NLD,
			sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fThu_BHYT_NSD,
			sum((isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))/@Donvitinh fThu_BHTN_NLD,
			sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fThu_BHTN_NSD,
			sum((isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			sum((isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			sum((isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
			sum((isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
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
				select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,ctct.iQSBQNam iQSBQNam
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
				,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
				,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
				,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
				,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
				where ct.iNamLamViec = @NamLamViec
					and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
					and ct.iQuyNamLoai = @ILoaiQuy
					--and ct.iID_MaDonVi = @IdDonVis
					and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
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
		'   ' + dv.sTenDonVi,
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
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

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
	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
			select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
				,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
				,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
				,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
				,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
				,ctct.fDuToan
				,ctct.fDaQuyetToan
				,ctct.fConLai
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam = @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
				--and ct.bDaTongHop = 0;
		end
		--In luy ke
		else
		begin
			select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
				,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
				,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPCChucVu
				,(sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
				,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
				,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
				,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fHSBL
				,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) + sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fTongQTLN
				,sum(isnull(ctct.fDuToan, 0)) fDuToan
				,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
				,sum(isnull(ctct.fConLai, 0)) fConLai
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
				,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
				,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
				,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
				,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
				,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
				,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
				,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				INTO tempChiTietDonVi
				from
				BH_QTT_BHXH_ChungTu ct
				INNER JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam <= @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
				group by 
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
				--and ct.bDaTongHop = 0;
		end
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
			LEFT JOIN tempChiTietDonVi chungtudonvi 
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
	'   ' + dv.sTenDonVi,
	tempChiTietDonVi.iNamLamViec ,
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
	FROM tempChiTietDonVi 
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;
	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;


	DROP TABLE #result;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempChiTietDonVi]') AND type in (N'U')) drop table tempChiTietDonVi;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN
	
	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

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
			select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam iQSBQNam
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
				,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
				,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
				,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
				,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				,mlns.sMoTa
			INTO #tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
			where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
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
		'   ' + dv.sTenDonVi,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN
	
	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
		select distinct
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
			(select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec,
				ctct.iID_QTT_BHXH_ChungTu_ChiTiet
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
				,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
				,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
				,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
				,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
				,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
				,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
				,ctct.fDuToan
				,ctct.fDaQuyetToan
				,ctct.fConLai
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
				,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
				,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
				,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
				,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
				,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
				,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam = @IQuy
				and ct.iQuyNamLoai = @ILoaiQuy
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
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
	end
	-- In luy ke
	else
	begin
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
			(select distinct
				ct.iID_MaDonVi,
				ct.iNamLamViec
				,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
				,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
				,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPCChucVu
				,(sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
				,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
				,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
				,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fHSBL
				,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) + sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fTongQTLN
				,sum(isnull(ctct.fDuToan, 0)) fDuToan
				,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
				,sum(isnull(ctct.fConLai, 0)) fConLai
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
				,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
				,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
				,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
				,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
				,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
				,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
				,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
				,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
				,ctct.sGhiChu
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
				where ct.iNamLamViec = @NamLamViec
					and ct.iQuyNam <= @IQuy
					and ct.iQuyNamLoai = @ILoaiQuy
					and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
					and ct.iLoaiTongHop = 1
				group by 
					ct.iID_MaDonVi,
					ct.iNamLamViec
					,ctct.sGhiChu
					,ctct.iID_MLNS
					,ctct.iID_MLNS_Cha
					,ctct.sXauNoiMa
					,ctct.sLNS
					) chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
			group by
				mlns.iID_MLNS,
				mlns.iID_MLNS_Cha,
				mlns.bHangCha,
				mlns.sXauNoiMa,
				mlns.sMoTa
			order by mlns.sXauNoiMa
	end

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
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
		(select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
			,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
			,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
			,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
			,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 8/30/2024 11:05:14 AM ******/
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
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	-- Ko In luy ke
	if (@IsLuyKe = 0)
	begin
	select distinct
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
		(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong
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
		(select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0)) fLuongChinh
			,(isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0)) fPCChucVu
			,(isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0)) fPCTNNghe
			,(isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,(isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0)) fNghiOm
			,(isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fHSBL
			,(isnull(ctct.fLuongChinh, 0) + isnull(gt.fLuongChinh, 0) + isnull(ctct.fPCChucVu, 0) + isnull(gt.fPCChucVu, 0) + isnull(ctct.fPCTNNghe, 0) + isnull(gt.fPCTNNghe, 0) + isnull(ctct.fPCTNVuotKhung, 0) + isnull(gt.fPCTNVuotKhung, 0) + isnull(ctct.fNghiOm, 0) + isnull(gt.fNghiOm, 0) + isnull(ctct.fHSBL, 0) + isnull(gt.fHSBL, 0)) fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,(isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0)) fTongSoPhaiThuBHXH
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,(isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,(isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0)) fTongSoPhaiThuBHYT
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,(isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,(isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongSoPhaiThuBHTN
			,(isnull(ctct.fThu_BHXH_NLD, 0) + isnull(gt.fTruyThu_BHXH_NLD, 0) + isnull(ctct.fThu_BHXH_NSD, 0) + isnull(gt.fTruyThu_BHXH_NSD, 0) + isnull(ctct.fThu_BHYT_NLD, 0) + isnull(gt.fTruyThu_BHYT_NLD, 0) + isnull(ctct.fThu_BHYT_NSD, 0) + isnull(gt.fTruyThu_BHYT_NSD, 0) + isnull(ctct.fThu_BHTN_NLD, 0) + isnull(gt.fTruyThu_BHTN_NLD, 0) + isnull(ctct.fThu_BHTN_NSD, 0) + isnull(gt.fTruyThu_BHTN_NSD, 0)) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
		from
		BH_QTT_BHXH_ChungTu ct
		join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
		where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
		) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
	end
	-- In luy ke
	else
	begin
		select distinct
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
		(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong
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
		(select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
			,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPCChucVu
			,(sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
			,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
			,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
			,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fHSBL
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) + sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
			,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
			,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
			,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam <= @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
			group by 
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
		) chungtudonvi on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
	end
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]    Script Date: 8/30/2024 11:05:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int,
	@IsLuyKe bit
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@IQuy = 3) BEGIN SET @SMonths = '1,2,3' END
	ELSE IF (@IQuy = 6 AND @IsLuyKe = 0) BEGIN SET @SMonths = '4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 0) BEGIN SET @SMonths = '7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 0) BEGIN SET @SMonths = '10,11,12' END

	ELSE IF (@IQuy = 6 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6' END
	ELSE IF (@IQuy = 9 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9' END
	ELSE IF (@IQuy = 12 AND @IsLuyKe = 1) BEGIN SET @SMonths = '1,2,3,4,5,6,7,8,9,10,11,12' END

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
		(select distinct
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0))) fLuongChinh
			,(sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0))) fPhuCapChucVu
			,(sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0))) fPCTNNghe
			,(sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0))) fPCTNVuotKhung
			,(sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0))) fNghiOm
			,(sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fHSBL
			,(sum(isnull(ctct.fLuongChinh, 0)) + sum(isnull(gt.fLuongChinh, 0)) + sum(isnull(ctct.fPCChucVu, 0)) + sum(isnull(gt.fPCChucVu, 0)) + sum(isnull(ctct.fPCTNNghe, 0)) + sum(isnull(gt.fPCTNNghe, 0)) + sum(isnull(ctct.fPCTNVuotKhung, 0)) + sum(isnull(gt.fPCTNVuotKhung, 0)) + sum(isnull(ctct.fNghiOm, 0)) + sum(isnull(gt.fNghiOm, 0)) +sum(isnull(ctct.fHSBL, 0)) + sum(isnull(gt.fHSBL, 0))) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0))) fThu_BHXH_NLD
			,(sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fThu_BHXH_NSD
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0))) fTongSoPhaiThuBHXH
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0))) fThu_BHYT_NLD
			,(sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fThu_BHYT_NSD
			,(sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0))) fTongSoPhaiThuBHYT
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0))) fThu_BHTN_NLD
			,(sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fThu_BHTN_NSD
			,(sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongSoPhaiThuBHTN
			,(sum(isnull(ctct.fThu_BHXH_NLD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NLD, 0)) + sum(isnull(ctct.fThu_BHXH_NSD, 0)) + sum(isnull(gt.fTruyThu_BHXH_NSD, 0)) + sum(isnull(ctct.fThu_BHYT_NLD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NLD, 0)) + sum(isnull(ctct.fThu_BHYT_NSD, 0)) + sum(isnull(gt.fTruyThu_BHYT_NSD, 0)) + sum(isnull(ctct.fThu_BHTN_NLD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NLD, 0)) + sum(isnull(ctct.fThu_BHTN_NSD, 0)) + sum(isnull(gt.fTruyThu_BHTN_NSD, 0))) fTongCong
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
		from
		BH_QTT_BHXH_ChungTu ct
			join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			left join BH_QTT_BHXH_CTCT_GiaiThich gt on ct.iID_QTT_BHXH_ChungTu = gt.iID_QTT_BHXH_ChungTu and gt.ILoaiGiaiThich = 2 and ctct.iID_MLNS = gt.iID_MLNS
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
GO
