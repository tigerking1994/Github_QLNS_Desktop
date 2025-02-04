/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 12/19/2023 4:23:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_tong_so_ngay_huong]    Script Date: 12/19/2023 4:23:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_luong_get_tong_so_ngay_huong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_luong_get_tong_so_ngay_huong]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_can_bo_che_do_chi_tiet]    Script Date: 12/19/2023 4:23:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_luong_get_can_bo_che_do_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_luong_get_can_bo_che_do_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm_tong_hop]    Script Date: 12/19/2023 4:23:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khtm_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khtm_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht_tong_hop]    Script Date: 12/19/2023 4:23:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_kht_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_kht_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht_tong_hop]    Script Date: 12/19/2023 4:23:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_get_data_kht_tong_hop] 
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	
	select
		ct.iID_MaDonVi,
		mlns.sXauNoiMa XauNoiMa,
		ctct.fThu_BHXH_NLD,
		ctct.fThu_BHXH_NSD,
		ctct.fThu_BHYT_NLD,
		ctct.fThu_BHYT_NSD,
		ctct.fThu_BHTN_NLD,
		ctct.fThu_BHTN_NSD
	from BH_KHT_BHXH_ChiTiet ctct
	join
	(select top 1 * from BH_KHT_BHXH
		where INamChungTu = @NamLamViec
		and iID_MaDonVi = @MaDonVi
		and sTongHop is not null
		and bIsKhoa = 1) ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm_tong_hop]    Script Date: 12/19/2023 4:23:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_get_data_khtm_tong_hop]
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select
		ct.iID_MaDonVi,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		ctct.fThanhTien
	from BH_KHTM_BHYT_ChiTiet ctct
	join
	(select top 1 * from BH_KHTM_BHYT
		where INamChungTu = @NamLamViec
		and iID_MaDonVi = @MaDonVi
		and sTongHop is not null
		and bKhoa = 1) ct on ctct.iID_KHTM_BHYT = ct.iID_KHTM_BHYT
	join BH_DM_MucLucNganSach mlns on ctct.iID_NoiDung = mlns.iID_MLNS

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_can_bo_che_do_chi_tiet]    Script Date: 12/19/2023 4:23:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_luong_get_can_bo_che_do_chi_tiet]
	@MaCanBo nvarchar(100),
	@MaCheDo nvarchar(100),
	@Thang int,
	@Nam int
AS
BEGIN
	
	SELECT Id,
		sMaCanBo,
		sMaCheDo,
		sTenCheDo,
		dTuNgay,
		dDenNgay,
		fSoNgayHuongBHXH,
		iThang,
		iNam,
		bTrangThai
	FROM TL_CanBo_CheDoBHXH_ChiTiet
	WHERE sMaCanBo = @MaCanBo
		AND sMaCheDo = @MaCheDo
		AND iThang = @Thang
		AND iNam = @Nam
		--AND bTrangThai = 0

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_tong_so_ngay_huong]    Script Date: 12/19/2023 4:23:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_luong_get_tong_so_ngay_huong]
	@MaCanBo nvarchar(100),
	@MaCheDo nvarchar(100),
	@Thang int,
	@Nam int
AS
BEGIN
	
	SELECT
		sMaCanBo,
		sMaCheDo,
		sTenCheDo,
		(SELECT TOP 1 dTuNgay FROM TL_CanBo_CheDoBHXH_ChiTiet
			WHERE sMaCanBo = @MaCanBo
			AND sMaCheDo = @MaCheDo
			AND iThang = @Thang
			AND iNam = @Nam
			AND dTuNgay is not null
			ORDER BY dTuNgay ASC) dTuNgay,
		(SELECT TOP 1 dDenNgay FROM TL_CanBo_CheDoBHXH_ChiTiet
			WHERE sMaCanBo = @MaCanBo
			AND sMaCheDo = @MaCheDo
			AND iThang = @Thang
			AND iNam = @Nam
			AND dDenNgay is not null
			ORDER BY dDenNgay DESC) dDenNgay,
		sum(fSoNgayHuongBHXH) fSoNgayHuongBHXH,
		iThang,
		iNam
	FROM TL_CanBo_CheDoBHXH_ChiTiet
	WHERE sMaCanBo = @MaCanBo
		AND sMaCheDo = @MaCheDo
		AND iThang = @Thang
		AND iNam = @Nam
		--AND bTrangThai = 0
	GROUP BY
		sMaCanBo,
		sMaCheDo,
		sTenCheDo,
		iThang,
		iNam

END
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 12/19/2023 4:23:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@NamLamViec int
AS
BEGIN
	INSERT INTO BH_KHT_BHXH_ChiTiet (iID_KHT_BHXH, iID_MucLucNganSach, iQSBQNam, fLuongChinh , fPhuCapChucVu, fPCTNNghe, fPCTNVuotKhung, fNghiOm, fHSBL, fThu_BHXH_NLD
	, fThu_BHXH_NSD , fThu_BHYT_NLD , fThu_BHYT_NSD , fThu_BHTN_NLD , fThu_BHTN_NSD,fTongThuBHXH, fTongThuBHYT, fTongThuBHTN, fTongCong
	, dNgayTao, dNgaySua, sNguoiTao)
SELECT @idChungTu,
       iID_MucLucNganSach,
	   sum(iQSBQNam),
       sum(fLuongChinh) ,
       sum(fPhuCapChucVu) ,
	   sum(fPCTNNghe) ,
	   sum(fPCTNVuotKhung) ,
	   sum(fNghiOm),
	   sum(fHSBL),
       sum(fThu_BHXH_NLD) ,
	   sum(fThu_BHXH_NSD) ,
	   sum(fThu_BHYT_NLD) ,
	   sum(fThu_BHYT_NSD) ,
	   sum(fThu_BHTN_NLD) ,
	   sum(fThu_BHTN_NSD) ,
	   sum(fTongThuBHXH) ,
	   Sum(fTongThuBHYT) ,
	   Sum(fTongThuBHTN) ,
	   Sum(fTongCong) ,
       NULL ,
       NULL ,
       NULL 
FROM BH_KHT_BHXH_ChiTiet
WHERE iID_KHT_BHXH in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY iID_MucLucNganSach

--danh dau chung tu da tong hop
update BH_KHT_BHXH set bDaTongHop = 1 
where iID_KHT_BHXH in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop));

END
;
GO
