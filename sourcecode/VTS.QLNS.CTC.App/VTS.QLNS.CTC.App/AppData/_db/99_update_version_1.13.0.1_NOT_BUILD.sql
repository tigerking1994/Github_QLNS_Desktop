/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 12/07/2023 2:47:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kinhphi_quanly_chi_tiet]    Script Date: 12/07/2023 2:47:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khc_kinhphi_quanly_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khc_kinhphi_quanly_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 12/07/2023 2:47:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ke_hoach_thu_mua_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikinh_phiquan_ly_index]    Script Date: 12/07/2023 2:47:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_kehoach_chikinh_phiquan_ly_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_kehoach_chikinh_phiquan_ly_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikinh_phiquan_ly_index]    Script Date: 12/07/2023 2:47:37 PM ******/
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
		, kpql.iNamChungTu
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
GO
/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 12/07/2023 2:47:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	KHTM.iID_KHTM_BHYT,
	KHTM.sSoChungTu,
	KHTM.iNamChungTu,
	KHTM.dNgayChungTu,
	KHTM.iID_MaDonVi IIDMaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHTM.sMoTa,
	KHTM.bKhoa,
	KHTM.fTongKeHoach,
	KHTM.sSoQuyetDinh,
	KHTM.dNgayQuyetDinh,
	KHTM.sNguoiTao,
	KHTM.sNguoiSua,
	KHTM.dNgayTao,
	KHTM.dNgaySua,
	KHTM.iLoaiTongHop,
	KHTM.sTongHop,
	KHTM.iTongSoNguoi,
	KHTM.iTongSoThang,
	KHTM.fTongDinhMuc,
	KHTM.fTongThanhTien,
	KHTM.bDaTongHop
	FROM BH_KHTM_BHYT KHTM
	LEFT JOIN DonVi DV
	ON KHTM.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	ORDER BY KHTM.dNgayQuyetDinh DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_khc_kinhphi_quanly_chi_tiet]    Script Date: 12/07/2023 2:47:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_khc_kinhphi_quanly_chi_tiet]
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
		ct.sTenDonVi

	FROM
		(
			SELECT
				bh.iID_MaDonVi,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 12/07/2023 2:47:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_KHTM_BHYT_ChiTiet (iID_KHTM_BHYT, iID_NoiDung, sTenNoiDung, iSoNguoi, iSoThang, fDinhMuc, fThanhTien, sGhiChu, dNgayTao, dNgaySua, sNguoiTao, sNguoiSua, iID_MaDonVi, sTenDonVi)
SELECT @idChungTu,
       iID_NoiDung,
	   sTenNoiDung,
       sum(iSoNguoi),
       sum(iSoThang),
	   sum(fDinhMuc),
	   sum(fThanhTien),
	   NULL,
       NULL,
       NULL,
       NULL,
	   NULL,
	   NULL,
	   NULL
FROM BH_KHTM_BHYT_ChiTiet
WHERE iID_KHTM_BHYT in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY iID_NoiDung,
	   sTenNoiDung;

--danh dau chung tu da tong hop
update BH_KHTM_BHYT set bDaTongHop = 1 
where iID_KHTM_BHYT in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop));
END
GO
