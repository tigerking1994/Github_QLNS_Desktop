/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_ktsdk_chungtu_tonghop_bhxh]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_ktsdk_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_ktsdk_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kpql_chitiet]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_kpql_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_kpql_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kcbqy_chitiet]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_kcbqy_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_kcbqy_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_QLKP]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_rpt_get_donvi_QLKP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_rpt_get_donvi_QLKP]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_KCBQY]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_rpt_get_donvi_KCBQY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_rpt_get_donvi_KCBQY]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_K]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_rpt_get_donvi_K]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_rpt_get_donvi_K]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_BHXH]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_rpt_get_donvi_BHXH]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_rpt_get_donvi_BHXH]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kinhphi_quanly_chi_tiet]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_kinhphi_quanly_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_kinhphi_quanly_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_khac_chungtu_chitiet_tao_tonghop]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_khac_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_khac_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kcb_chungtu_chitiet_tao_tonghop]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_kcb_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_kcb_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kcb_chi_tiet]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_kcb_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_kcb_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_k_chi_tiet]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_k_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_k_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_chi_tiet]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_cd_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_cd_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[bh_xh_kehoach_chi_index]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_xh_kehoach_chi_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_xh_kehoach_chi_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikinh_phiquan_ly_index]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_kehoach_chikinh_phiquan_ly_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_kehoach_chikinh_phiquan_ly_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikham_chubenh_index]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_kehoach_chikham_chubenh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_kehoach_chikham_chubenh_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikhac_index]    Script Date: 12/11/2023 4:12:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_kehoach_chikhac_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_kehoach_chikhac_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikhac_index]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi các chế dộ BHXH

-- =============================================
CREATE PROCEDURE [dbo].[bh_kehoach_chikhac_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 kcb.iID_BH_KHC_K
		, kcb.sSoChungTu
		, kcb.dNgayChungTu
		, kcb.sSoQuyetDinh
		, kcb.dNgayQuyetDinh
		, kcb.iNamLamViec
		, kcb.iID_DonVi
		, kcb.iID_MaDonVi
		, kcb.sMoTa
		, kcb.iIDLoaiChi
		, kcb.fTongTienDaThucHienNamTruoc
		, kcb.fTongTienUocThucHienNamTruoc
		, kcb.fTongTienKeHoachThucHienNamNay
		, kcb.sTongHop
		, kcb.iID_TongHopID
		, kcb.iLoaiTongHop
		, kcb.bIsKhoa
		, kcb.dNgaySua
		, kcb.dNgayTao
		, kcb.sNguoiSua
		, kcb.sNguoiTao
		, kcb.dNgayTao
		, kcb.bDaTongHop
		, donVi.sTenDonVi
		, dm.sTenDanhMucLoaiChi
		-- Tong dự toán todo
	FROM BH_KHC_K kcb
	LEFT JOIN DonVi donVi
		ON kcb.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = kcb.iID_DonVi
	LEFT JOIN BH_DM_LoaiChi dm ON kcb.iIDLoaiChi=dm.iID and kcb.iNamLamViec=dm.iNamLamViec
	ORDER BY kcb.sSoChungTu
END
;

GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikham_chubenh_index]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi kham chu benh

-- =============================================
CREATE PROCEDURE [dbo].[bh_kehoach_chikham_chubenh_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 kcb.iID_BH_KHC_KCB 
		, kcb.sSoChungTu
		, kcb.dNgayChungTu
		, kcb.sSoQuyetDinh
		, kcb.dNgayQuyetDinh
		, kcb.iNamLamViec
		, kcb.iID_DonVi
		, kcb.iID_MaDonVi
		, kcb.sMoTa
		, kcb.fTongTienDaThucHienNamTruoc
		, kcb.fTongTienUocThucHienNamTruoc
		, kcb.fTongTienKeHoachThucHienNamNay
		, kcb.sTongHop
		, kcb.iID_TongHopID
		, kcb.iLoaiTongHop
		, kcb.bIsKhoa
		, kcb.dNgaySua
		, kcb.dNgayTao
		, kcb.sNguoiSua
		, kcb.sNguoiTao
		, kcb.dNgayTao
		, kcb.bDaTongHop
		, donVi.sTenDonVi
		-- Tong dự toán todo
	FROM BH_KHC_KCB  kcb
	LEFT JOIN DonVi donVi
		ON kcb.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = kcb.iID_DonVi
END
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikinh_phiquan_ly_index]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi các chế dộ BHXH

-- =============================================
CREATE PROCEDURE [dbo].[bh_kehoach_chikinh_phiquan_ly_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 kpql.iID_BH_KHC_KinhPhiQuanLy 
		, kpql.sSoChungTu
		, kpql.dNgayChungTu
		, kpql.sSoQuyetDinh
		, kpql.dNgayQuyetDinh
		, kpql.iNamLamViec
		, kpql.iID_DonVi
		, kpql.iID_MaDonVi
		, kpql.sMoTa
		, kpql.fTongTienDaThucHienNamTruoc
		, kpql.fTongTienUocThucHienNamTruoc
		, kpql.fTongTienKeHoachThucHienNamNay

		, kpql.fTongTienCanBo
		, kpql.fTongTienQuanLuc
		, kpql.fTongTienTaiChinh
		, kpql.fTongTienQuanY

		, kpql.sTongHop
		, kpql.iID_TongHopID
		, kpql.iLoaiTongHop
		, kpql.bIsKhoa
		, kpql.bDaTongHop

		, kpql.dNgaySua
		, kpql.dNgayTao
		, kpql.sNguoiSua
		, kpql.sNguoiTao
		, kpql.dNgayTao
		, donVi.sTenDonVi
		-- Tong dự toán todo
	FROM BH_KHC_KinhPhiQuanLy kpql
	LEFT JOIN DonVi donVi
		ON kpql.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = kpql.iID_DonVi
END
;
GO
/****** Object:  StoredProcedure [dbo].[bh_xh_kehoach_chi_index]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi các chế dộ BHXH

-- =============================================
CREATE PROCEDURE [dbo].[bh_xh_kehoach_chi_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 CHBHXH.ID AS iID_BH_KHC_CheDoBHXH
		, CHBHXH.sSoChungTu
		, CHBHXH.dNgayChungTu
		, CHBHXH.sSoQuyetDinh
		, CHBHXH.iNamLamViec
		, CHBHXH.iID_DonVi
		, CHBHXH.iID_MaDonVi
		, CHBHXH.sMoTa
		, CHBHXH.iTongSoDaThucHienNamTruoc
		, CHBHXH.iTongSoUocThucHienNamTruoc

		, CHBHXH.iTongSoKeHoachThucHienNamNay
		, CHBHXH.iTongSoSQ
		, CHBHXH.iTongSoQNCN
		, CHBHXH.iTongSoCNVQP
		, CHBHXH.iTongSoLDHD
		, CHBHXH.iTongSoHSQBS

		, CHBHXH.fTongTienDaThucHienNamTruoc

		, CHBHXH.fTongTienUocThucHienNamTruoc
		, CHBHXH.fTongTienKeHoachThucHienNamNay

		, CHBHXH.fTongTienSQ
		, CHBHXH.fTongTienQNCN
		, CHBHXH.fTongTienCNVQP
		, CHBHXH.fTongTienLDHD
		, CHBHXH.fTongTienHSQBS

		, CHBHXH.sTongHop
		, CHBHXH.iID_TongHopID
		, CHBHXH.iLoaiTongHop
		, CHBHXH.bIsKhoa
		, CHBHXH.bDaTongHop

		, CHBHXH.dNgaySua
		, CHBHXH.dNgayTao
		, CHBHXH.dNgayQuyetDinh
		, CHBHXH.sNguoiSua
		, CHBHXH.sNguoiTao
		, CHBHXH.dNgayTao
		, donVi.sTenDonVi
		, (CHBHXH.fTongTienSQ+CHBHXH.fTongTienDaThucHienNamTruoc+CHBHXH.fTongTienKeHoachThucHienNamNay+CHBHXH.fTongTienQNCN+CHBHXH.fTongTienCNVQP+CHBHXH.fTongTienLDHD+CHBHXH.fTongTienUocThucHienNamTruoc+CHBHXH.fTongTienHSQBS) as isTongTien
		-- Tong dự toán todo
	FROM BH_KHC_CheDoBHXH CHBHXH
	LEFT JOIN DonVi donVi
		ON CHBHXH.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = CHBHXH.iID_DonVi
	LEFT JOIN BH_KHC_CheDoBHXH_ChiTiet CHBHXHCT ON CHBHXH.ID=CHBHXHCT.iID_KHC_CheDoBHXH
END
;

GO
/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_chi_tiet]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khc_cd_bhxh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_KHC_CheDoBHXHChiTiet ,
		ct.iID_KHC_CheDoBHXH ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sMoTa,
		ct.sLoaiTroCap,
		ct.iSoDaThucHienNamTruoc,
		ct.iSoUocThucHienNamTruoc,
		ct.iSoKeHoachThucHienNamNay,
		ct.iSoSQ,
		ct.iSoQNCN,
		ct.iSoCNVQP,
		ct.iSoHSQBS,
		ct.iSoLDHD,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienUocThucHienNamTruoc,
		ct.fTienKeHoachThucHienNamNay,
		ct.fTienSQ,
		ct.fTienQNCN,
		ct.fTienCNVQP,
		ct.fTienLDHD,
		ct.fTienHSQBS,
		ct.sTenDonVi,
		ct.sGhiChu,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.iNamLamViec, ct.sXauNoiMa
	FROM
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				--bh.iNamLamViec, 
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_KHC_CheDoBHXH bh
			JOIN 
				BH_KHC_CheDoBHXH_ChiTiet bhct ON bh.ID = bhct.iID_KHC_CheDoBHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
			and	bh.iNamLamViec=@NamLamViec
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct;

END
;
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:    <Author,,Name>
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  CREATE PROCEDURE [dbo].[sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @IDMaDonVi nvarchar(50),
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int
  AS BEGIN 
  INSERT INTO [dbo].[BH_KHC_CheDoBHXH_ChiTiet] (
    iiD_KHC_CheDoBHXHChiTiet
	, iID_KHC_CheDoBHXH
	, iID_MucLucNganSach
	, sLoaiTroCap
	, iSoDaThucHienNamTruoc
	, iSoUocThucHienNamTruoc
	, iSoKeHoachThucHienNamNay
	, iSoSQ
	, iSoQNCN
	, iSoCNVQP
	, iSoLDHD
	, iSoHSQBS
	, fTienDaThucHienNamTruoc
	, fTienUocThucHienNamTruoc
	, fTienKeHoachThucHienNamNay
	, fTienSQ
	, fTienQNCN
	, fTienCNVQP
	, fTienLDHD
	, fTienHSQBS
	, dNgaySua
	, dNgayTao
	, sNguoiSua
	, sNguoiTao
	, iNamLamViec
	, sXauNoiMa
	, iID_MaDonVi
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sLoaiTroCap, 
  SUM(iSoDaThucHienNamTruoc), 
  SUM(iSoUocThucHienNamTruoc), 
  sum(iSoKeHoachThucHienNamNay), 
  sum(iSoSQ), 
  sum(iSoQNCN), 
  sum(iSoCNVQP), 
  sum(iSoLDHD), 
  sum(iSoHSQBS), 
  sum(fTienDaThucHienNamTruoc), 
  sum(fTienUocThucHienNamTruoc), 
  sum(fTienKeHoachThucHienNamNay), 
  sum(fTienSQ), 
  sum(fTienQNCN), 
  SUM(fTienCNVQP), 
  sum(fTienLDHD), 
  sum(fTienHSQBS), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao,
  iNamLamViec,
  sXauNoiMa,
  @IDMaDonVi
  
FROM 
  BH_KHC_CheDoBHXH_ChiTiet 
WHERE 
  iID_KHC_CheDoBHXH in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sLoaiTroCap,
   iNamLamViec,
   sXauNoiMa;
--danh dau chung tu da tong hop
update 
  BH_KHC_CheDoBHXH 
set 
  bDaTongHop=1
where 
  ID in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_k_chi_tiet]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khc_k_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@iID_BH_KHC_K uniqueidentifier
AS
BEGIN
SELECT 
		ct.iID_BH_KHC_K_ChiTiet ,
		ct.iID_KHC_K ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sMoTa,
		ct.sNoiDung,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienUocThucHienNamTruoc,
		ct.fTienKeHoachThucHienNamNay,
		ct.sGhiChu,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sTenDonVi,
		ct.sXauNoiMa,
		ct.iNamLamViec
	FROM
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_KHC_K bh
			JOIN 
				BH_KHC_K_ChiTiet bhct ON bh.iID_BH_KHC_K = bhct.iID_KHC_K
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		where ct.iID_KHC_K=@iID_BH_KHC_K
			and ct.iNamLamViec=@NamLamViec
		;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kcb_chi_tiet]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_khc_kcb_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@iID_BH_KHC_KCB uniqueidentifier
AS
BEGIN
SELECT 
		ct.iID_BH_KHC_KCB_ChiTiet ,
		ct.iID_KHC_KCB ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sMoTa,
		ct.sNoiDung,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienUocThucHienNamTruoc,
		ct.fTienKeHoachThucHienNamNay,
		ct.sGhiChu,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sTenDonVi,
		ct.sXauNoiMa,
		ct.iNamLamViec

	FROM
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_KHC_KCB bh
			JOIN 
				BH_KHC_KCB_ChiTiet bhct ON bh.iID_BH_KHC_KCB = bhct.iID_KHC_KCB 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		where ct.iID_KHC_KCB=@iID_BH_KHC_KCB
			and ct.iNamLamViec=@NamLamViec
		;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kcb_chungtu_chitiet_tao_tonghop]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[sp_khc_kcb_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @IDMaDonVi nvarchar(50),
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int
  AS BEGIN 
  INSERT INTO [dbo].[BH_KHC_KCB_ChiTiet] (
    iID_BH_KHC_KCB_ChiTiet,
	iID_KHC_KCB, 
    iID_MucLucNganSach,
	sNoiDung, 
    fTienDaThucHienNamTruoc,
	fTienUocThucHienNamTruoc, 
    fTienKeHoachThucHienNamNay,
	dNgaySua,
	dNgayTao, 
    sNguoiSua,
	sNguoiTao,
	sXauNoiMa,
	iNamLamViec,
	iID_MaDonVi
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sNoiDung, 
  sum(fTienDaThucHienNamTruoc), 
  sum(fTienUocThucHienNamTruoc), 
  sum(fTienKeHoachThucHienNamNay), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao,
  sXauNoiMa,
  iNamLamViec,
  @IDMaDonVi
FROM 
  BH_KHC_KCB_ChiTiet 
WHERE 
  iID_KHC_KCB in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec;
--danh dau chung tu da tong hop
update 
  BH_KHC_KCB 
set 
  bDaTongHop = 1 
where 
  iID_BH_KHC_KCB in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_khc_khac_chungtu_chitiet_tao_tonghop]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:    <Author,,Name>
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  CREATE PROCEDURE [dbo].[sp_khc_khac_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext, 
  @IDMaDonVi nvarchar(50),
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int
  AS BEGIN 
  INSERT INTO [dbo].[BH_KHC_K_ChiTiet] (
    iID_BH_KHC_K_ChiTiet,
	iID_KHC_K, 
    iID_MucLucNganSach,
	sNoiDung, 
    fTienDaThucHienNamTruoc,
	fTienUocThucHienNamTruoc, 
    fTienKeHoachThucHienNamNay,
	dNgaySua,
	dNgayTao, 
    sNguoiSua,
	sNguoiTao,
	sXauNoiMa,
	iNamLamViec,
	iID_MaDonVi
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sNoiDung, 
  sum(fTienDaThucHienNamTruoc), 
  sum(fTienUocThucHienNamTruoc), 
  sum(fTienKeHoachThucHienNamNay), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao,
  sXauNoiMa,
  iNamLamViec,
  @IDMaDonVi
FROM 
  BH_KHC_K_ChiTiet 
WHERE 
  iID_KHC_K in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec;
--danh dau chung tu da tong hop
update 
  BH_KHC_K 
set 
  bDaTongHop = 1 
where 
  iID_BH_KHC_K in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kinhphi_quanly_chi_tiet]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khc_kinhphi_quanly_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@iID_KHC_KinhPhiQuanLy uniqueidentifier
AS
BEGIN
SELECT 
		ct.iID_BH_KHC_KinhPhiQuanLy_ChiTiet ,
		ct.iID_KHC_KinhPhiQuanLy ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sMoTa,
		ct.sM,
		ct.sTM,
		ct.sNoiDung,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienUocThucHienNamTruoc,
		ct.fTienKeHoachThucHienNamNay,
		ct.fTienCanBo,
		ct.fTienQuanLuc,
		ct.fTienTaiChinh,
		ct.fTienQuanY,
		ct.sGhiChu,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sTenDonVi,
		ct.iNamLamViec,
		ct.sXauNoiMa

	FROM
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_KHC_KinhPhiQuanLy bh
			JOIN 
				BH_KHC_KinhPhiQuanLy_ChiTiet bhct ON bh.iID_BH_KHC_KinhPhiQuanLy = bhct.iID_KHC_KinhPhiQuanLy 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		where ct.iID_KHC_KinhPhiQuanLy=@iID_KHC_KinhPhiQuanLy
		;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:    <Author,,Name>
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  CREATE PROCEDURE [dbo].[sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop] 
  @ListIdChungTuTongHop ntext,
  @IDMaDonVi nvarchar(50),
  @Nguoitao nvarchar(50),
  @IdChungTu nvarchar(100), 
  @NamLamViec int
  AS BEGIN 
  INSERT INTO [dbo].[BH_KHC_KinhPhiQuanLy_ChiTiet] (
    iID_BH_KHC_KinhPhiQuanLy_ChiTiet,
	iID_KHC_KinhPhiQuanLy, 
    iID_MucLucNganSach,
	sM, 
    sTM,
	sNoiDung,
	iNamLamViec,
	sXauNoiMa,
	iID_MaDonVi,
    fTienDaThucHienNamTruoc,
	fTienUocThucHienNamTruoc, 
    fTienKeHoachThucHienNamNay,
	fTienCanBo, 
    fTienQuanLuc,
	fTienTaiChinh,
	fTienQuanY,
	dNgaySua,
	dNgayTao, 
    sNguoiSua,
	sNguoiTao
  ) 
SELECT 
DISTINCT
  NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sM,
  sTM,
  sNoiDung,
  iNamLamViec,
  sXauNoiMa,
  @IDMaDonVi,
  sum(fTienDaThucHienNamTruoc), 
  sum(fTienUocThucHienNamTruoc), 
  sum(fTienKeHoachThucHienNamNay), 
  sum(fTienCanBo), 
  sum(fTienQuanLuc), 
  SUM(fTienTaiChinh), 
  sum(fTienQuanY), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_KHC_KinhPhiQuanLy_ChiTiet 
WHERE 
  iID_KHC_KinhPhiQuanLy in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  iID_MucLucNganSach, 
  sM,
  sTM,
  sNoiDung,
  iNamLamViec,
  sXauNoiMa;
--danh dau chung tu da tong hop
update 
  BH_KHC_KinhPhiQuanLy 
set 
  bDaTongHop=1
where 
  iID_BH_KHC_KinhPhiQuanLy in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
  
END;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_BHXH]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khc_rpt_get_donvi_BHXH]
@NamLamViec int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_KHC_CheDoBHXH chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamLamViec = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_K]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khc_rpt_get_donvi_K]
@NamLamViec int,
@IdLoaiChi uniqueidentifier
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_KHC_K chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamLamViec = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.iIDLoaiChi=@IdLoaiChi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_KCBQY]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khc_rpt_get_donvi_KCBQY]
@NamLamViec int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_KHC_KCB chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamLamViec = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_khc_rpt_get_donvi_QLKP]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khc_rpt_get_donvi_QLKP]
@NamLamViec int
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_KHC_KinhPhiQuanLy chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamLamViec = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_bhxh_chitiet]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_khc_bhxh_chitiet]
	 @NamLamViec int,
	 @IdDonVi nvarchar(max),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
		FROM BH_DM_MucLucNganSach 
		WHERE 
		iNamLamViec = 2023 
		AND iTrangThai = 1
		AND (sLNS ='9010001' OR sLNS='9010002')

		SELECT 
		tblml.iID_MLNS as IID_MucLucNganSach,
		tblml.iID_MLNS_Cha as IdParent,
		tblml.sXauNoiMa  ,
		tblml.sLNS,
		tblml.sL,
		tblml.sK,
		tblml.sM,
		tblml.sTM,
		tblml.sTTM,
		tblml.sNG,
		tblml.sTNG,
		tblml.sTNG1,
		tblml.sTNG2,
		tblml.sTNG3,
		tblml.sMoTa,
		tblml.bHangCha,
		ISNULL(chitiet.fTienCNVQP, 0)/@Dvt fTienCNVQP,
		ISNULL(chitiet.fTienDaThucHienNamTruoc, 0)/@Dvt fTienDaThucHienNamTruoc,
		ISNULL(chitiet.fTienHSQBS, 0)/@Dvt fTienHSQBS,
		ISNULL(chitiet.fTienKeHoachThucHienNamNay, 0)/@Dvt fTienKeHoachThucHienNamNay,
		ISNULL(chitiet.fTienLDHD, 0)/@Dvt fTienLDHD,
		ISNULL(chitiet.fTienQNCN, 0)/@Dvt fTienQNCN,
		ISNULL(chitiet.fTienSQ, 0)/@Dvt fTienSQ,
		ISNULL(chitiet.fTienUocThucHienNamTruoc, 0)/@Dvt fTienUocThucHienNamTruoc,
		chitiet.iSoCNVQP,
		chitiet.iSoDaThucHienNamTruoc,
		chitiet.iSoHSQBS,
		chitiet.iSoKeHoachThucHienNamNay,
		chitiet.iSoLDHD,
		chitiet.iSoQNCN,
		chitiet.iSoSQ,
		chitiet.iSoUocThucHienNamTruoc,
		chitiet.sGhiChu
		
		FROM #tblMlnsByPhanCap tblml
		LEFT JOIN
		(SELECT CTCT.* FROM
			BH_KHC_CheDoBHXH_ChiTiet CTCT
			WHERE  CTCT.iID_KHC_CheDoBHXH IN
			(
				SELECT CT.ID
						FROM BH_KHC_CheDoBHXH CT
						WHERE CT.iID_MaDonVi=@IdDonVi
							AND CT.sSoChungTu <> ''
							AND CT.sSoChungTu IS NOT NULL
							AND CT.iNamLamViec=@NamLamViec
			)) chitiet ON 

			chitiet.iID_MucLucNganSach=tblml.iID_MLNS

		ORDER BY tblml.sXauNoiMa

		drop table #tblMlnsByPhanCap
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int
AS
BEGIN
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			INTO #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @namLamViec 
		AND iTrangThai = 1
		and (sLNS='9010001' or sLNS='9010002')

		SELECT 
		mlns.iID_MLNS as IdMlns,
		mlns.iID_MLNS_Cha as IdMlnsCha,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SMoTa,
		mlns.bHangCha as IsHangCha,
		ct.fTienCNVQP,
		ct.fTienDaThucHienNamTruoc,
		ct.fTienHSQBS,
		ct.fTienKeHoachThucHienNamNay,
		ct.fTienLDHD,
		ct.fTienQNCN,
		ct.fTienSQ,
		ct.fTienUocThucHienNamTruoc,
		ct.iID_KHC_CheDoBHXH
		INTO #tempchitiet
		FROM
		#tblMlnsByPhanCap mlns
		left join 
		(SELECT * FROM BH_KHC_CheDoBHXH_ChiTiet 
				WHERE iID_KHC_CheDoBHXH in
					( SELECT ID FROM BH_KHC_CheDoBHXH
								WHERE iNamLamViec=@namLamViec
								AND iID_MaDonVi IN (select * from f_split(@listTenDonVi))
								)) ct
								On mlns.iID_MLNS=ct.iID_MucLucNganSach

		WHERE  ct.iID_KHC_CheDoBHXH is not null

		SELECT 
		dv.sTenDonVi ,
		dv.iID_MaDonVi as IDDonVi,
		tblct.SM,
		tblct.SLNS,
		SUM(tblct.fTienKeHoachThucHienNamNay) TienKeHoachThucHienNamNay
		FROM #tempchitiet tblct
		inner join BH_KHC_CheDoBHXH ct on ct.ID=tblct.iID_KHC_CheDoBHXH
		left join DonVi dv on ct.iID_DonVi=dv.iID_DonVi and ct.iID_MaDonVi=dv.iID_MaDonVi
		WHERE dv.iNamLamViec=2023
		GROUP BY dv.sTenDonVi,
				dv.iID_MaDonVi, 
				tblct.SLNS, 
				tblct.SM
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]
	@listTenDonVi ntext,
	@namLamViec int,
	@LNS nvarchar(200),
	@IDLoaichi uniqueidentifier,
	@Dvt int
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),
FTongTienKeHoachThucHienNamNay float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , 
FTongTienKeHoachThucHienNamNay 
)
	SELECT 
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   FTongTienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))/ @Dvt
FROM
  (SELECT 
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTienKeHoachThucHienNamNay
   FROM BH_KHC_K_ChiTiet ctct
   LEFT JOIN BH_KHC_K ct ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS IN  (SELECT * FROM f_split(@LNS))
   WHERE ct.iNamLamViec = @namLamViec
		AND ct.iIDLoaiChi=@IDLoaichi
	) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		dt_dv.iLoai;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
IsNull(dt.FTongTienKeHoachThucHienNamNay, 0) FTongTienKeHoachThucHienNamNay
FROM @DataKhoi dt
where dt.idDonVi in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_kcb_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int
AS
BEGIN
declare @DataDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSDT nvarchar(50), 
fTienDaThucHienNamTruocDT float,fTienUocThucHienNamTruocDT float, fTienKeHoachThucHienNamNayDT float
);
declare @DataHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSHT nvarchar(50),
fTienDaThucHienNamTruocHT float,fTienUocThucHienNamTruocHT float, fTienKeHoachThucHienNamNayHT float
);

INSERT INTO @DataDuToan (idDonVi,sTenDonVI , sM,sLNSDT ,
fTienDaThucHienNamTruocDT ,fTienUocThucHienNamTruocDT , fTienKeHoachThucHienNamNayDT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,

	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))


FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,

				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_KCB_ChiTiet ctct
   LEFT JOIN BH_KHC_KCB ct ON ct.iID_BH_KHC_KCB = ctct.iID_KHC_KCB
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = 9010004
   WHERE ct.iNamLamViec = @namLamViec--@namLamViec
	 --AND ct.iLoaiTongHop = @iLoaiTongHop
	 ) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

INSERT INTO @DataHachToan (idDonVi,sTenDonVI , sM,sLNSHT ,
fTienDaThucHienNamTruocHT ,fTienUocThucHienNamTruocHT , fTienKeHoachThucHienNamNayHT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,

	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))

FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,

				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_KCB_ChiTiet ctct
   LEFT JOIN BH_KHC_KCB ct ON ct.iID_BH_KHC_KCB = ctct.iID_KHC_KCB
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec =@namLamViec --@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = 9010005
   WHERE ct.iNamLamViec =@namLamViec --@namLamViec
	 --AND ct.iLoaiTongHop = @iLoaiTongHop
	 ) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.sM,
dt.sLNSDT,
ht.sLNSHT,

IsNull(dt.fTienDaThucHienNamTruocDT, 0) TienDaThucHienNamTruocDT,
IsNull(ht.fTienDaThucHienNamTruocHT, 0) TienDaThucHienNamTruocHT,

IsNull(dt.fTienUocThucHienNamTruocDT, 0) TienUocThucHienNamTruocDT,
IsNull(ht.fTienUocThucHienNamTruocHT, 0) TienUocThucHienNamTruocHT,

IsNull(dt.fTienKeHoachThucHienNamNayDT, 0) TienKeHoachThucHienNamNayDT,
IsNull(ht.fTienKeHoachThucHienNamNayHT, 0) TienKeHoachThucHienNamNayHT

FROM @DataDuToan dt
LEFT JOIN @DataHachToan ht ON dt.idDonVi = ht.idDonVi
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kcbqy_chitiet]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_khc_kcbqy_chitiet]
	 @NamLamViec int,
	 @IdDonVi nvarchar(max),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
		FROM BH_DM_MucLucNganSach 
		WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND (sLNS ='9010004' Or sLNS ='9010005')

		SELECT 
		tblml.iID_MLNS as IID_MucLucNganSach,
		tblml.iID_MLNS_Cha as IdParent,
		tblml.sXauNoiMa  ,
		tblml.sLNS,
		tblml.sL,
		tblml.sK,
		tblml.sM,
		tblml.sTM,
		tblml.sTTM,
		tblml.sNG,
		tblml.sTNG,
		tblml.sTNG1,
		tblml.sTNG2,
		tblml.sTNG3,
		tblml.sMoTa,
		tblml.bHangCha as IsHangCha,
		ISNULL(chitiet.fTienDaThucHienNamTruoc, 0)/@Dvt fTienDaThucHienNamTruoc,
		ISNULL(chitiet.fTienUocThucHienNamTruoc, 0)/@Dvt fTienUocThucHienNamTruoc,
		ISNULL(chitiet.fTienKeHoachThucHienNamNay, 0)/@Dvt fTienKeHoachThucHienNamNay,
		chitiet.sGhiChu
		
		FROM #tblMlnsByPhanCap tblml
		LEFT JOIN
		(SELECT CTCT.* FROM
			BH_KHC_KCB_ChiTiet CTCT
			WHERE  CTCT.iID_KHC_KCB IN
			(
				SELECT CT.iID_BH_KHC_KCB
						FROM BH_KHC_KCB CT
						WHERE CT.iID_MaDonVi=@IdDonVi
							AND CT.sSoChungTu <> ''
							AND CT.sSoChungTu IS NOT NULL
							AND CT.iNamLamViec=@NamLamViec
			)) chitiet ON 

			chitiet.iID_MucLucNganSach=tblml.iID_MLNS

		ORDER BY tblml.sXauNoiMa

		drop table #tblMlnsByPhanCap
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int,
	@listSLNS nvarchar(max)

AS
BEGIN
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,
	   fTienDaThucHienNamTruocDT=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   fTienUocThucHienNamTruocDT=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   fTienKeHoachThucHienNamNayDT=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))
	   into #temp
FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_K_ChiTiet ctct
   LEFT JOIN BH_KHC_K ct ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS IN  (SELECT *
     FROM f_split(@listSLNS))
   WHERE ct.iNamLamViec = @namLamViec--@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id  
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.sM,
dt.sLNS,
IsNull(dt.fTienDaThucHienNamTruocDT, 0) TienDaThucHienNamTruocDT,
IsNull(dt.fTienUocThucHienNamTruocDT, 0) TienUocThucHienNamTruocDT,
IsNull(dt.fTienKeHoachThucHienNamNayDT, 0) TienKeHoachThucHienNamNayDT
FROM #temp dt
WHERE dt.idDonVi in 
    (SELECT *
     FROM f_split(@listTenDonVi))	 
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_kpql_chitiet]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chung tu theo don vi và theo lns
CREATE PROCEDURE [dbo].[sp_rpt_khc_kpql_chitiet]
	 @NamLamViec int,
	 @IdDonVi nvarchar(max),
	 @Dvt int
AS
BEGIN 
	SET NOCOUNT ON;
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha
			into #tblMlnsByPhanCap
		FROM BH_DM_MucLucNganSach 
		WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		AND sLNS ='9010003'

		SELECT 
		tblml.iID_MLNS as IID_MucLucNganSach,
		tblml.iID_MLNS_Cha as IdParent,
		tblml.sXauNoiMa  ,
		tblml.sLNS,
		tblml.sL,
		tblml.sK,
		tblml.sM,
		tblml.sTM,
		tblml.sTTM,
		tblml.sNG,
		tblml.sTNG,
		tblml.sTNG1,
		tblml.sTNG2,
		tblml.sTNG3,
		tblml.sMoTa as SNoiDung,
		tblml.bHangCha,
		ISNULL(chitiet.fTienDaThucHienNamTruoc, 0)/@Dvt FTienDaThucHienNamTruoc,
		ISNULL(chitiet.fTienUocThucHienNamTruoc, 0)/@Dvt FTienUocThucHienNamTruoc,
		ISNULL(chitiet.fTienKeHoachThucHienNamNay, 0)/@Dvt FTienKeHoachThucHienNamNay,
		ISNULL(chitiet.fTienCanBo, 0)/@Dvt FTienCanBo,
		ISNULL(chitiet.fTienQuanLuc, 0)/@Dvt FTienQuanLuc,
		ISNULL(chitiet.fTienTaiChinh, 0)/@Dvt FTienTaiChinh,
		ISNULL(chitiet.fTienQuanY, 0)/@Dvt FTienQuanY,
		chitiet.sGhiChu
		
		FROM #tblMlnsByPhanCap tblml
		LEFT JOIN
		(SELECT CTCT.* FROM
			BH_KHC_KinhPhiQuanLy_ChiTiet CTCT
			WHERE  CTCT.iID_KHC_KinhPhiQuanLy IN
			(
				SELECT CT.iID_BH_KHC_KinhPhiQuanLy
						FROM BH_KHC_KinhPhiQuanLy CT
						WHERE CT.iID_MaDonVi=@IdDonVi
							AND CT.sSoChungTu <> ''
							AND CT.sSoChungTu IS NOT NULL
							AND CT.iNamLamViec=@NamLamViec
			)) chitiet ON 

			chitiet.iID_MucLucNganSach=tblml.iID_MLNS

		ORDER BY tblml.sXauNoiMa

		drop table #tblMlnsByPhanCap
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_ktsdk_chungtu_tonghop_bhxh]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_rpt_khc_ktsdk_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int,
	@sLNSDuToan nvarchar(max),
	@sLNSHachToan nvarchar(max)
AS
BEGIN
declare @DataDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSDT nvarchar(50), 
fTienDaThucHienNamTruocDT float,fTienUocThucHienNamTruocDT float, fTienKeHoachThucHienNamNayDT float
);
declare @DataHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sM nvarchar(50),sLNSHT nvarchar(50),
fTienDaThucHienNamTruocHT float,fTienUocThucHienNamTruocHT float, fTienKeHoachThucHienNamNayHT float
);

INSERT INTO @DataDuToan (idDonVi,sTenDonVI , sM,sLNSDT ,
fTienDaThucHienNamTruocDT ,fTienUocThucHienNamTruocDT , fTienKeHoachThucHienNamNayDT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,
	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))


FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,

				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_K_ChiTiet ctct
   LEFT JOIN BH_KHC_K ct ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @sLNSDuToan
   WHERE ct.iNamLamViec = @namLamViec--@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

INSERT INTO @DataHachToan (idDonVi,sTenDonVI , sM,sLNSHT ,
fTienDaThucHienNamTruocHT ,fTienUocThucHienNamTruocHT , fTienKeHoachThucHienNamNayHT 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   A.sM,
	   A.sLNS,

	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))

FROM
  (SELECT ml.iID_MLNS ,
                ml.iID_MLNS_Cha,
                ml.sXauNoiMa,
                ml.sNG,
				ml.sM,
				ml.sLNS,
                ml.sMoTa,
				ml.bHangCha,
				ct.iID_DonVi,
				ct.iID_MaDonVi,

				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay

   FROM BH_KHC_K_ChiTiet ctct
   LEFT JOIN BH_KHC_K ct ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec =@namLamViec --@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @sLNSHachToan
   WHERE ct.iNamLamViec =@namLamViec --@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		 A.sM,A.sLNS;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
dt.sM,
dt.sLNSDT,
ht.sLNSHT,

IsNull(dt.fTienDaThucHienNamTruocDT, 0) TienDaThucHienNamTruocDT,
IsNull(ht.fTienDaThucHienNamTruocHT, 0) TienDaThucHienNamTruocHT,

IsNull(dt.fTienUocThucHienNamTruocDT, 0) TienUocThucHienNamTruocDT,
IsNull(ht.fTienUocThucHienNamTruocHT, 0) TienUocThucHienNamTruocHT,

IsNull(dt.fTienKeHoachThucHienNamNayDT, 0) TienKeHoachThucHienNamNayDT,
IsNull(ht.fTienKeHoachThucHienNamNayHT, 0) TienKeHoachThucHienNamNayHT

FROM @DataDuToan dt
LEFT JOIN @DataHachToan ht ON dt.idDonVi = ht.idDonVi
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]    Script Date: 12/11/2023 4:12:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_qlkp_chungtu_tonghop_bhxh]
	@listTenDonVi ntext,
	@namLamViec int,
	@iLoaiTongHop int
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),
fTienDaThucHienNamTruoc float,fTienUocThucHienNamTruoc float, fTienKeHoachThucHienNamNay float,fTienCanBo float,fTienQuanLuc float, fTienTaiChinh float,fTienQuanY float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , 
fTienDaThucHienNamTruoc ,fTienUocThucHienNamTruoc , fTienKeHoachThucHienNamNay ,fTienCanBo ,fTienQuanLuc , fTienTaiChinh ,fTienQuanY 
)
	SELECT 
	   dt_dv.sTenDonVi,
	   dt_dv.id idDonVi,
	   TienDaThucHienNamTruoc=SUM(IsNull(A.fTienDaThucHienNamTruoc,0)),
	   TienUocThucHienNamTruoc=SUM(IsNull(A.fTienUocThucHienNamTruoc,0)),
	   TienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0)),
	   TienSQ=SUM(IsNull(A.fTienCanBo,0)),
	   TienQNCN=SUM(IsNull(A.fTienQuanLuc,0)),
	   TienCNVQP=SUM(IsNull(A.fTienTaiChinh,0)),
	   TienLDHD=SUM(IsNull(A.fTienQuanY,0))
FROM
  (SELECT 
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTienDaThucHienNamTruoc,
				ctct.fTienUocThucHienNamTruoc,
				ctct.fTienKeHoachThucHienNamNay,
				ctct.fTienCanBo,
				ctct.fTienQuanLuc,
				ctct.fTienTaiChinh,
				ctct.fTienQuanY
   FROM BH_KHC_KinhPhiQuanLy_ChiTiet ctct
   LEFT JOIN BH_KHC_KinhPhiQuanLy ct ON ct.iID_BH_KHC_KinhPhiQuanLy = ctct.iID_KHC_KinhPhiQuanLy
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS =9010003 --9020000
   WHERE ct.iNamLamViec = @namLamViec--@namLamViec
	 AND ct.iLoaiTongHop = @iLoaiTongHop) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
IsNull(dt.fTienDaThucHienNamTruoc, 0) FTienDaThucHienNamTruoc,
IsNull(dt.fTienUocThucHienNamTruoc, 0) FTienUocThucHienNamTruoc,
IsNull(dt.fTienKeHoachThucHienNamNay, 0) FTienKeHoachThucHienNamNay,
IsNull(dt.fTienCanBo, 0) FTienCanBo,
IsNull(dt.fTienQuanLuc, 0) FTienQuanLuc,
IsNull(dt.fTienTaiChinh, 0) FTienTaiChinh,
IsNull(dt.fTienQuanY, 0) FTienQuanY
FROM @DataKhoi dt
where dt.sTenDonVI in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;
;
GO
