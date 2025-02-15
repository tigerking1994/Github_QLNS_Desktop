/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KHTongThe_nhiemVuChi_by_Id]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_KHTongThe_nhiemVuChi_by_Id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_KHTongThe_nhiemVuChi_by_Id]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KhTongThe_by_NvChiId]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_KhTongThe_by_NvChiId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_KhTongThe_by_NvChiId]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KHTongThe_and_DmNhiemVuChi]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_KHTongThe_and_DmNhiemVuChi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_KHTongThe_and_DmNhiemVuChi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdNhiemVuChi]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_by_IdNhiemVuChi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_by_IdNhiemVuChi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe_and_maDonVi]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_by_IdKhTongThe_and_maDonVi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe_and_maDonVi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_by_IdKhTongThe]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_all_donVi_by_KhTongTheId]    Script Date: 18/01/2023 3:19:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_find_all_donVi_by_KhTongTheId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_find_all_donVi_by_KhTongTheId]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_all_donVi_by_KhTongTheId]    Script Date: 18/01/2023 3:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_all_donVi_by_KhTongTheId]
	@IdKhTongThe uniqueidentifier
AS
BEGIN
    SELECT DISTINCT
	tt_nvc.iID_KHTongTheID,
	tt_nvc.iID_DonViThuHuongID as Id,
	DV.iID_DonVi,
	DV.iID_MaDonVi as IIDMaDonVi,
	DV.sTenDonVi as TenDonVi,
	DV.iNamLamViec as NamLamViec
	FROM NH_KHTongThe_NhiemVuChi tt_nvc
	JOIN DonVi DV ON tt_nvc.iID_MaDonViThuHuong = DV.iID_MaDonVi
	JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID 
	WHERE 
	tt_nvc.iID_KHTongTheID = @IdKhTongThe
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan]    Script Date: 18/01/2023 3:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan]
	@IdKhTongThe uniqueidentifier
AS
BEGIN
    SELECT
    tt_nvc.ID,
    tt_nvc.iID_KHTongTheID,
    tt_nvc.iID_NhiemVuChiID,
    tt_nvc.iID_DonViThuHuongID,
    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP,
    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP,
    FORMATMESSAGE( '%s - %s', DonVi.iID_MaDonVi, DonVi.sTenDonVi ) sTenDonVi,
    donvi.iID_MaDonVi AS SMaDonViThuHuong,
    nvc.sMaNhiemVuChi,
    nvc.sTenNhiemVuChi,
    nvc.iLoaiNhiemVuChi
	FROM NH_KHTongThe_NhiemVuChi tt_nvc
	JOIN NH_KHTongThe khtt ON khtt.ID = tt_nvc.iID_KHTongTheID 
	JOIN DonVi DonVi ON DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID
	JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID 
	WHERE khtt.iID_ParentID = @IdKhTongThe
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe]    Script Date: 18/01/2023 3:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe]
	@IdKhTongThe uniqueidentifier
AS
BEGIN
    SELECT
    tt_nvc.ID,
    tt_nvc.iID_KHTongTheID,
    tt_nvc.iID_NhiemVuChiID,
    tt_nvc.iID_DonViThuHuongID,
    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP,
    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP,
    FORMATMESSAGE( '%s - %s', DonVi.iID_MaDonVi, DonVi.sTenDonVi ) sTenDonVi,
    donvi.iID_MaDonVi AS SMaDonViThuHuong,
    nvc.sMaNhiemVuChi,
    nvc.sTenNhiemVuChi,
    nvc.iLoaiNhiemVuChi
	FROM NH_KHTongThe_NhiemVuChi tt_nvc
	JOIN DonVi DonVi ON DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID
	JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID 
	WHERE tt_nvc.iID_KHTongTheID = @IdKhTongThe
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe_and_IdDonVi]    Script Date: 18/01/2023 3:19:06 PM ******/
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
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdKhTongThe_and_maDonVi]    Script Date: 18/01/2023 3:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_by_IdKhTongThe_and_maDonVi]
	@IdKhTongThe uniqueidentifier,
	@MaDonVi nvarchar(MAX)
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
    AND tt_nvc.iID_MaDonViThuHuong = @MaDonVi 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_by_IdNhiemVuChi]    Script Date: 18/01/2023 3:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_by_IdNhiemVuChi]
	@idNhiemVuChi uniqueidentifier
AS
BEGIN
	SELECT *
	FROM NH_DA_GoiThau gt
	join NH_KHTongThe_NhiemVuChi nvc
	on gt.iID_KHTT_NhiemVuChiID = nvc.ID
	join NH_DM_NhiemVuChi dmnvc
	on nvc.iID_NhiemVuChiID = dmnvc.ID
	WHERE
	dmnvc.Id =@idNhiemVuChi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KHTongThe_and_DmNhiemVuChi]    Script Date: 18/01/2023 3:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_KHTongThe_and_DmNhiemVuChi]
	@idKhTongThe uniqueidentifier
AS
BEGIN
    SELECT 
    n.ID
   ,n.iID_KHTongTheID
   ,n.iID_NhiemVuChiID
   ,n.iID_DonViThuHuongID
   ,n.fGiaTriKH_TTCP AS FGiaTriKhTTCP
   ,n.fGiaTriKH_BQP AS FGiaTriKhBQP
   ,n.fGiaTriKH_BQP_VND AS FGiaTriKhBQPVnd
   ,n.iID_MaDonViThuHuong
   ,n.sMaOrder
   ,d.iID_ParentID AS ParentNhiemVuChiId
   ,d.sTenNhiemVuChi AS STenNhiemVuChi
	FROM NH_KHTongThe_NhiemVuChi AS n
	INNER JOIN NH_DM_NhiemVuChi AS d
	ON n.iID_NhiemVuChiID = d.ID
	WHERE n.iID_KHTongTheID = @idKhTongThe
	ORDER BY sMaOrder
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KhTongThe_by_NvChiId]    Script Date: 18/01/2023 3:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_KhTongThe_by_NvChiId]
	@IdNvChi uniqueidentifier
AS
BEGIN
    select * from NH_KHTongThe kh
	inner join NH_KHTongThe_NhiemVuChi nv 
	on nv.iID_KHTongTheID = kh.ID 
	where nv.ID = @IdNvChi 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_KHTongThe_nhiemVuChi_by_Id]    Script Date: 18/01/2023 3:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_KHTongThe_nhiemVuChi_by_Id]
	@Id uniqueidentifier
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
	WHERE tt_nvc.ID = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi]    Script Date: 18/01/2023 3:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi]
	@IdKhTongThe uniqueidentifier,
	@IdNhiemVuChi uniqueidentifier
AS
BEGIN
	SELECT 
     tt_nvc.ID, 
     tt_nvc.iID_KHTongTheID, 
     tt_nvc.iID_NhiemVuChiID, 
     tt_nvc.iID_DonViThuHuongID, 
     tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP, 
     tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP, 
     FORMATMESSAGE( '%s - %s', DonVi.iID_MaDonVi, DonVi.sTenDonVi ) sTenDonVi, 
     donvi.iID_MaDonVi AS SMaDonViThuHuong, 
     nvc.sMaNhiemVuChi, 
     nvc.sTenNhiemVuChi, 
     nvc.iLoaiNhiemVuChi  
 FROM NH_KHTongThe_NhiemVuChi tt_nvc 
 JOIN DonVi ON DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID 
 JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID  
 WHERE
    tt_nvc.iID_KHTongTheID = @IdKhTongThe 
    AND tt_nvc.iID_NhiemVuChiID = @IdNhiemVuChi 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]    Script Date: 18/01/2023 3:19:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu] 
	@IdChuTruongDauTu nvarchar(200),
	@MaDonVi nvarchar(200)
AS
BEGIN
	SELECT * FROM VDT_DA_DuAn duan
	INNER JOIN VDT_DM_DonViThucHienDuAn dv
		ON dv.iID_MaDonVi = duan.iID_MaDonViThucHienDuAnID
	WHERE
		duan.iID_DuAnID NOT IN   
		(
			SELECT DISTINCT iID_DuAnID FROM VDT_DA_ChuTruongDauTu WHERE iID_DuAnID = duan.iID_DuAnID
			AND iID_ChuTruongDauTuID <> @IdChuTruongDauTu
		)
		AND duan.iID_DuAnID IN 
		(
			SELECT DISTINCT iID_DuAnID from VDT_KHV_KeHoach5Nam_ChiTiet
		)
		----AND dv.iID_MaDonVi = @MaDonVi
		--AND dv.iID_MaDonVi  in (
		---- lay don vi C2
		--	select  iID_MaDonVi from VDT_DM_DonViThucHienDuAn where  iID_MaDonVi=  @MaDonVi
		--	union all  
		--	-- lay don vi C3
		--	select  iID_MaDonVi from VDT_DM_DonViThucHienDuAn where  iID_DonViCha  
		--	in (select  iID_DonVi from VDT_DM_DonViThucHienDuAn where  iID_MaDonVi=  @MaDonVi)
		--	union all  
		--	select  iID_MaDonVi from VDT_DM_DonViThucHienDuAn where  iID_DonViCha
		--	in (--lay don vi C4
		--		select  iID_DonVi from VDT_DM_DonViThucHienDuAn where  iID_DonViCha 
		--		in (select  iID_DonVi from VDT_DM_DonViThucHienDuAn where  iID_MaDonVi=  @MaDonVi)
		--	)
		--)
		AND dv.iID_MaDonVi in (select * from f_recursive_donvi(@MaDonVi));

END;
;
;
GO
