/****** Object:  UserDefinedTableType [dbo].[t_tbl_tonghopdautu_v2]    Script Date: 30/12/2022 3:55:26 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_tonghopdautu_v2' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_tonghopdautu_v2]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_mlns_by_khv]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_tt_get_mlns_by_khv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_tt_get_mlns_by_khv]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_khvthuhoiung]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_tt_get_khvthuhoiung]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_tt_get_khvthuhoiung]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_khvthanhtoan]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_tt_get_khvthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_tt_get_khvthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khv_kehoach_von_nam_duoc_duyet_export]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_khv_kehoach_von_nam_duoc_duyet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_khv_kehoach_von_nam_duoc_duyet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_chitiet_chuyentiep_report]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_kehoach5nam_chitiet_chuyentiep_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_kehoach5nam_chitiet_chuyentiep_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_chitiet_chooseduan_test]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_kehoach5nam_chitiet_chooseduan_test]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_kehoach5nam_chitiet_chooseduan_test]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovonchitiet]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_insert_phanbovonchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_insert_phanbovonchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_dutoanchitiet_danhsach]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getall_dutoanchitiet_danhsach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getall_dutoanchitiet_danhsach]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_phan_bo_von_dieuchinh]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_phan_bo_von_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_phan_bo_von_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_khoi_tao_dulieu_chitiet]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_khoi_tao_dulieu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_khoi_tao_dulieu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_new_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_pheduyet]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_donvi_pheduyet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_pheduyet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_baocaodquyettoanniendo1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_baocaodquyettoanniendo1]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitiet]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovondonvichitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovondonvichitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovonchitiet]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovonchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovonchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_kehoachvonnam_donvi_pheduyet_duocduyet_dieuchinh_chitiet]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_kehoachvonnam_donvi_pheduyet_duocduyet_dieuchinh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_kehoachvonnam_donvi_pheduyet_duocduyet_dieuchinh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_du_an_by_listId]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_du_an_by_listId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_du_an_by_listId]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_insert_tonghopnguonnsdautu_tang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_insert_tonghopnguonnsdautu_tang]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet6]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_phanbovonchitiet6' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_phanbovonchitiet6]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet5]    Script Date: 12/30/2022 2:01:02 PM ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N't_tbl_phanbovonchitiet5' AND ss.name = N'dbo')
DROP TYPE [dbo].[t_tbl_phanbovonchitiet5]
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_tonghopdautu_v2]    Script Date: 30/12/2022 3:55:26 PM ******/
CREATE TYPE [dbo].[t_tbl_tonghopdautu_v2] AS TABLE(
	[Id] [uniqueidentifier] NULL,
	[iID_ChungTu] [uniqueidentifier] NULL,
	[iID_DuAnID] [uniqueidentifier] NULL,
	[sMaNguon] [nvarchar](100) NULL,
	[sMaNguonCha] [nvarchar](100) NULL,
	[sMaDich] [nvarchar](100) NULL,
	[fGiaTri] [float] NULL,
	[ILoaiUng] [int] NULL,
	[iStatus] [int] NULL,
	[bIsLog] [bit] NULL DEFAULT ((0)),
	[iId_MaNguonCha] [uniqueidentifier] NULL,
	[iThuHoiTUCheDo] [int] NULL,
	[IIDMucID] [uniqueidentifier] NULL,
	[IIDTieuMucID] [uniqueidentifier] NULL,
	[IIDTietMucID] [uniqueidentifier] NULL,
	[IIDNganhID] [uniqueidentifier] NULL,
	[IIdLoaiCongTrinh] [uniqueidentifier] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet5]    Script Date: 12/30/2022 2:01:02 PM ******/
CREATE TYPE [dbo].[t_tbl_phanbovonchitiet5] AS TABLE(
	[iID_PhanBoVonID] [uniqueidentifier] NULL,
	[iID_DuAnID] [uniqueidentifier] NULL,
	[sTrangThaiDuAnDangKy] [nvarchar](200) NULL,
	[fGiaTrDeNghi] [float] NULL,
	[fGiaTrPhanBo] [float] NULL,
	[fGiaTriThuHoi] [float] NULL,
	[iID_DonViTienTeID] [uniqueidentifier] NULL,
	[iID_TienTeID] [uniqueidentifier] NULL,
	[fTiGiaDonVi] [float] NULL,
	[fTiGia] [float] NULL,
	[sGhiChu] [nvarchar](max) NULL,
	[iID_LoaiNguonVonID] [uniqueidentifier] NULL,
	[sLNS] [nvarchar](50) NULL,
	[sL] [nvarchar](50) NULL,
	[sK] [nvarchar](50) NULL,
	[sM] [nvarchar](50) NULL,
	[sTM] [nvarchar](50) NULL,
	[sTTM] [nvarchar](50) NULL,
	[sNG] [nvarchar](50) NULL,
	[fCapPhatTaiKhoBac] [float] NULL,
	[fCapPhatBangLenhChi] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBac] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChi] [float] NULL,
	[fCapPhatTaiKhoBacDc] [float] NULL,
	[fCapPhatBangLenhChiDc] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBacDc] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChiDc] [float] NULL,
	[iID_Parent] [uniqueidentifier] NULL,
	[ILoaiDuAn] [int] NULL,
	[IIdLoaiCongTrinh] [uniqueidentifier] NULL,
	[fThanhToanDeXuat] [float] NULL,
	[iID_DuAn_HangMucID] [uniqueidentifier] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[t_tbl_phanbovonchitiet6]    Script Date: 12/30/2022 2:01:02 PM ******/
CREATE TYPE [dbo].[t_tbl_phanbovonchitiet6] AS TABLE(
	[iID_PhanBoVonID] [uniqueidentifier] NULL,
	[iID_DuAnID] [uniqueidentifier] NULL,
	[sTrangThaiDuAnDangKy] [nvarchar](200) NULL,
	[fGiaTrDeNghi] [float] NULL,
	[fGiaTrPhanBo] [float] NULL,
	[fGiaTriThuHoi] [float] NULL,
	[iID_DonViTienTeID] [uniqueidentifier] NULL,
	[iID_TienTeID] [uniqueidentifier] NULL,
	[fTiGiaDonVi] [float] NULL,
	[fTiGia] [float] NULL,
	[sGhiChu] [nvarchar](max) NULL,
	[iID_LoaiNguonVonID] [uniqueidentifier] NULL,
	[sLNS] [nvarchar](50) NULL,
	[sL] [nvarchar](50) NULL,
	[sK] [nvarchar](50) NULL,
	[sM] [nvarchar](50) NULL,
	[sTM] [nvarchar](50) NULL,
	[sTTM] [nvarchar](50) NULL,
	[sNG] [nvarchar](50) NULL,
	[fCapPhatTaiKhoBac] [float] NULL,
	[fCapPhatBangLenhChi] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBac] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChi] [float] NULL,
	[fCapPhatTaiKhoBacDc] [float] NULL,
	[fCapPhatBangLenhChiDc] [float] NULL,
	[fGiaTriThuHoiNamTruocKhoBacDc] [float] NULL,
	[fGiaTriThuHoiNamTruocLenhChiDc] [float] NULL,
	[iID_Parent] [uniqueidentifier] NULL,
	[ILoaiDuAn] [int] NULL,
	[IIdLoaiCongTrinh] [uniqueidentifier] NULL,
	[fThanhToanDeXuat] [float] NULL,
	[iID_DuAn_HangMucID] [uniqueidentifier] NULL
)
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_insert_tonghopnguonnsdautu_tang]
@sLoai nvarchar(100),
@iTypeExecute int,
@uIdQuyetDinh uniqueidentifier,
@iIDQuyetDinhOld uniqueidentifier
AS
BEGIN
	CREATE TABLE #lstMaNguon(sMaNguon nvarchar(100))

	DECLARE @RankDate DATETIME = (SELECT TOP(1) dNgayQuyetDinh FROM VDT_TongHop_NguonNSDauTu WHERE sMaNguon COLLATE DATABASE_DEFAULT = 'QUYET_TOAN' ORDER BY dNgayQuyetDinh DESC)

	IF(@sLoai = 'KHVN')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('KHVN'), ('101'), ('102'),('111'),('112')
	END
	ELSE IF(@sLoai = 'KHVU')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('KHVU'), ('121a'), ('122a')
	END
	ELSE IF(@sLoai = 'QUYET_TOAN')
	BEGIN
		INSERT INTO #lstMaNguon(sMaNguon)
		VALUES('QUYET_TOAN'), ('131'), ('132'), ('211c'), ('212c'), ('301'), ('302'), ('321a'), ('322a')
			, ('000'), ('321b'), ('322b')
	END

	IF(@iTypeExecute in (2,3,4))
	BEGIN 
		IF (@iTypeExecute in (2,3))
		BEGIN
			-- dao nguoc but toan
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTri, bIsLog, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--OUTPUT inserted.Id, inserted.sMaDich, inserted.iID_DuAnID, ISNULL(inserted.fGiaTri, 0) INTO #tmp(iId, sMaNguon, iIdDuAnId, fDaThanhToan)
			SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300',
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh 
				AND bIsLog = 0 AND sMaTienTrinh = (CASE WHEN @sLoai = 'QUYET_TOAN' THEN '100' ELSE '200' END)

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @uIdQuyetDinh
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')
		END
		ELSE IF (@iTypeExecute = 4)
		BEGIN
			-- dao nguoc but toan
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTri, bIsLog, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			--OUTPUT inserted.Id, inserted.sMaDich, inserted.iID_DuAnID, ISNULL(inserted.fGiaTri, 0) INTO #tmp(iId, sMaNguon, iIdDuAnId, fDaThanhToan)
			SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300',
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld 
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')

			-- khoa but toan da update
			UPDATE tbl 
			SET 
				bIsLog = 1
			FROM VDT_TongHop_NguonNSDauTu as tbl
			INNER JOIN #lstMaNguon as dt on tbl.sMaNguon COLLATE DATABASE_DEFAULT = dt.sMaNguon COLLATE DATABASE_DEFAULT
			WHERE tbl.iID_ChungTu = @iIDQuyetDinhOld
				AND bIsLog = 0 AND sMaTienTrinh in ('100', '200')
		END

		-- deleted thi khong xu ly nua
		IF(@iTypeExecute = 3)
		BEGIN
			RETURN
		END
	END

	IF(@sLoai = 'KHVN')
	BEGIN
		IF(@iTypeExecute = 4)
		BEGIN
			-- insert but toan moi vao 
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, 
					(CASE WHEN sMaNguon in ('101', '102') THEN '200' ELSE '100' END),
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, dt.iID_LoaiCongTrinh, 
						(CASE colName WHEN 'fCapPhatTaiKhoBacDC' THEN '101' 
									WHEN 'fCapPhatBangLenhChiDC' THEN '102'END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh , fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, 
					(CASE WHEN sMaNguon in ('101', '102') THEN '200' ELSE '100' END),
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, iID_LoaiCongTrinh,
						(CASE colName WHEN 'fCapPhatTaiKhoBacDC' THEN '111' 
									WHEN 'fCapPhatBangLenhChiDC' THEN '112'END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh , fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBacDC,fCapPhatBangLenhChiDC)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan = 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
		END
		ELSE
		BEGIN
			-- insert but toan moi vao 
			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, 
					(CASE WHEN sMaNguon in ('101', '102') THEN '200' ELSE '100' END),
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, iID_LoaiCongTrinh,
						(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '101' 
									WHEN 'fCapPhatBangLenhChi' THEN '102'END) as sMaNguon
						from 
						(select Id,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBac,fCapPhatBangLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan <> 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh

			INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
												iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG, iID_LoaiCongTrinh)
			SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
					tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon, '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, 
					(CASE WHEN sMaNguon in ('101', '102') THEN '200' ELSE '100' END),
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN (select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
						(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '111' 
									WHEN 'fCapPhatBangLenhChi' THEN '112'END) as sMaNguon
						from 
						(select Id, iID_LoaiCongTrinh,iID_PhanBoVonID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID , fCapPhatTaiKhoBac,fCapPhatBangLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
						UNPIVOT
						(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi)) as dt) as dt on dt.iID_PhanBoVonID = tbl.Id
			LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
			WHERE tbl.Id = @uIdQuyetDinh AND tbl.iLoaiDuToan = 2
			GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
					iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG, dt.iID_LoaiCongTrinh
		END
	END
	ELSE IF(@sLoai = 'KHVU')
	BEGIN
		-- insert but toan moi vao 
		INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, fGiaTri, iStatus, sMaTienTrinh,
											iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, LNS, L, K, M, TM, TTM, NG)
		SELECT dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID,
				tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon , '000', SUM(ISNULL(dt.fGiaTri, 0)), 0, '200',
				iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
		FROM VDT_KHV_KeHoachVonUng as tbl
		INNER JOIN (select Id,dt.iID_KeHoachUngID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, dt.fGiaTri, 
					(CASE colName WHEN 'fCapPhatTaiKhoBac' THEN '121a' WHEN 'fCapPhatBangLenhChi' THEN '122a' END) as sMaNguon
					from 
					(select Id,iID_KeHoachUngID,iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, fCapPhatTaiKhoBac,fCapPhatBangLenhChi from VDT_KHV_KeHoachVonUng_ChiTiet) as tbl
					UNPIVOT
					(fGiaTri FOR colName IN (fCapPhatTaiKhoBac,fCapPhatBangLenhChi)) as dt) as dt on dt.iID_KeHoachUngID = tbl.Id
		LEFT JOIN NS_MucLucNganSach as ml on (dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID)
		WHERE tbl.Id = @uIdQuyetDinh
		GROUP BY dt.iID_DuAnID, tbl.iID_MaDonViQuanLy, tbl.iID_NguonVonID, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.Id , dt.sMaNguon,
			iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, ml.sLNS, ml.sL, ml.sK, ml.sM, ml.sTM, ml.sTTM, ml.sNG
	END
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_du_an_by_listId]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_du_an_by_listId]
	@lstId nvarchar(max)
AS
BEGIN
	declare 
		@indexDuAnMax int = 1,
		@YearPlan int;

	select @indexDuAnMax =  MAX(iMaDuAnIndex) from VDT_DA_DuAn;
	select  
		@YearPlan = khndx.NamLamViec
	from VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet khnct
	inner join VDT_KHV_KeHoach5Nam_DeXuat khndx on khnct.iID_KeHoach5NamID = khndx.Id
	where khnct.Id in (select TOP 1 * from dbo.splitstring(@lstId))

	SELECT
		da.iID_DuAnID as Id,
		SUM(khthdx.fHanMucDauTu) as FHanMucDauTu,
		dv.iID_DonVi as IdDonViQuanLy,
		khthdx.sTen as STenDuAn,
		(isnull(dv.iID_MaDonVi, 'XXX') + '-' + 'XXX' + '-' + RIGHT('0000' + cast((isnull(@indexDuAnMax, 1) + cast((ROW_NUMBER() over(order by khthdx.STT)) as int)) as nvarchar), 4)) as SMaDuAn,
		khthdx.Id as IdReference,
		(@indexDuAnMax + cast((ROW_NUMBER() over(order by khthdx.STT)) as int)) as IndexDuAn,
		khthdx.SDiaDiem as SDiaDiem,
		CAST(khthdx.IGiaiDoanTu as nvarchar) as SKhoiCong,
		CAST(khthdx.IGiaiDoanDen as nvarchar) as SKetThuc,
		dv.iID_MaDonVi as SMaDonViQuanLy,
		khthdx.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId,
		khthdx.iID_NguonVonID,
		khthdx.STT
	from 
		VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet khthdx
	left join
		VDT_DM_DonViThucHienDuAn dv
	on khthdx.iID_DonViQuanLyID = dv.iID_DonVi
	left join
		VDT_DA_DuAn da
	on 
		da.Id_DuAnKhthDeXuat = khthdx.Id
	where
		khthdx.Id in (select * from dbo.splitstring(@lstId)) and khthdx.IsStatus = 2
	group by khthdx.Id, dv.iID_DonVi, khthdx.sTen, dv.iID_MaDonVi, khthdx.SDiaDiem, khthdx.IGiaiDoanTu, khthdx.IGiaiDoanDen, khthdx.STT, da.iID_DuAnID, khthdx.iID_LoaiCongTrinhID, khthdx.iID_NguonVonID, khthdx.STT
END
/****** Object:  StoredProcedure [dbo].[sp_insert_tonghopnguonnsdautu_tang]    Script Date: 11/12/2021 1:30:41 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_kehoachvonnam_donvi_pheduyet_duocduyet_dieuchinh_chitiet]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_find_kehoachvonnam_donvi_pheduyet_duocduyet_dieuchinh_chitiet]
@iIdPhanBoVon uniqueidentifier
AS
BEGIN
SELECT distinct
	   da.iID_DuAnID,
       da.sTenDuAn,
       da.sMaDuAn,
       da.sTrangThaiDuAn,
       da.sKhoiCong,
       da.sKetThuc,
       da.sMaKetNoi,
       lct.sTenLoaiCongTrinh,
       '' AS sTenCapPheDuyet,
       cast(0 AS float) AS fGiaTriDauTu,
       pbvct.iID_LoaiCongTrinh AS iID_LoaiCongTrinhID,
       NULL AS iID_CapPheDuyetID,
       '' AS sLNS,
       '' AS sL,
       '' AS sK,
       '' AS sM,
       '' AS sTM,
       '' AS sTTM,
       '' AS sNG,
       cast(0 AS float) AS fVonDaBoTri,
       cast(0 AS float) AS fVonConLai,
       cast(0 AS float) AS fChiTieuBoXungTrongNam,
       cast(0 AS float) AS fNamTruocChuyenSang,
       cast(0 AS float) AS fThuUngXDCB,
       cast(0 AS float) AS fChiTieuNganSach,
       pbvct.sGhiChu AS sGhiChu,
       cast(0 AS float) AS fChiTieuGoc,
       pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID AS IIdPhanBoVon,
       cast(0 AS float) AS FCapPhatTaiKhoBacDc,
       cast(0 AS float) AS FCapPhatBangLenhChiDc,
       cast(0 AS float) AS FGiaTriThuHoiNamTruocKhoBacDc,
       cast(0 AS float) AS FGiaTriThuHoiNamTruocLenhChiDc,
       pbvct.fGiaTriThuHoi AS FGiaTriThuHoiDc,
       pbvct.fGiaTrPhanBo AS FGiaTrPhanBoDc,
       cast(0 AS float) AS FCapPhatTaiKhoBac,
       cast(0 AS float) AS FCapPhatBangLenhChi,
       cast(0 AS float) AS FGiaTriThuHoiNamTruocKhoBac,
       cast(0 AS float) AS FGiaTriThuHoiNamTruocLenhChi,
       cast(0 AS float) AS FGiaTriThuHoi,
       cast(0 AS float) AS FGiaTrPhanBo,
       pbvct.iId_Parent AS IIdParent,
       pbvct.ILoaiDuAn as ILoaiDuAn,
       dv.sTenDonVi AS STenDonViThucHienDuAn,
       dv.iID_MaDonVi AS IIdMaDonViThucHienDuAn,
		case when pbvct.fThanhToanDeXuat is not null then pbvct.fThanhToanDeXuat else pbvdxct.fThanhToan end as fThanhToanDeXuat,
		pbvct.iID_DuAn_HangMucID
FROM VDT_KHV_PhanBoVon_DonVi_PheDuyet pbv
LEFT JOIN VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvct ON pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = pbv.Id
--LEFT JOIN VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvctpr ON pbvctpr.iID_PhanBoVon_DonVi_PheDuyet_ID = pbv.iID_ParentId and (pbvct.iID_LoaiCongTrinh is null OR pbvctpr.iID_LoaiCongTrinh = pbvct.iID_LoaiCongTrinh) and (pbvct.ILoaiDuAn is null OR pbvctpr.ILoaiDuAn = pbvct.ILoaiDuAn) 
inner join 
	VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvdxct on pbvdxct.iID_DuAnID = pbvct.iID_DuAnID
LEFT JOIN VDT_DA_DuAn da ON pbvct.iID_DuAnID = da.iID_DuAnID
LEFT JOIN VDT_DM_LoaiCongTrinh lct ON pbvct.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
LEFT JOIN VDT_DM_DonViThucHienDuAn dv ON da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
WHERE pbv.Id = @iIdPhanBoVon
		--and pbvct.bActive = 1
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovonchitiet]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_find_phanbovonchitiet]
@iIdPhanBoVon uniqueidentifier,
@dNgayLap datetime
AS
BEGIN
	select 
		distinct
		--pbvct.Id,
		da.iID_DuAnID,
		--pbvct.iID_PhanBoVonID,
		da.sTenDuAn,
		da.sMaDuAn,
		da.sTrangThaiDuAn,
		da.sKhoiCong,
		da.sKetThuc,
		da.sMaKetNoi,
		da.iID_MaDonViThucHienDuAnID as IIdMaDonViThucHienDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		lct.sTenLoaiCongTrinh,
		'' as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		pbvct.iID_LoaiCongTrinh as iID_LoaiCongTrinhID,
		null as iID_CapPheDuyetID,
		pbvct.LNS as sLNS,
		pbvct.L as sL,
		pbvct.K as sK,
		pbvct.M as sM,
		pbvct.TM as sTM,
		pbvct.TTM as sTTM,
		pbvct.NG as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		pbvct.sGhiChu as sGhiChu,
		cast(0 as float) as fChiTieuGoc,
		pbvct.fCapPhatTaiKhoBac as FCapPhatTaiKhoBac,
		pbvct.fCapPhatTaiKhoBacDC as FCapPhatTaiKhoBacDC,
		pbvct.fCapPhatBangLenhChi as FCapPhatBangLenhChi,
		pbvct.fCapPhatBangLenhChiDC as FCapPhatBangLenhChiDC,
		pbvct.fGiaTriThuHoiNamTruocKhoBac as FGiaTriThuHoiNamTruocKhoBac,
		pbvct.fGiaTriThuHoiNamTruocKhoBacDC as FGiaTriThuHoiNamTruocKhoBacDC,
		pbvct.fGiaTriThuHoiNamTruocLenhChi as FGiaTriThuHoiNamTruocLenhChi,
		pbvct.fGiaTriThuHoiNamTruocLenhChiDC as FGiaTriThuHoiNamTruocLenhChiDC,
		--isnull(pbvct.Id, NEWID()) as IIdPhanBoVonId,
		pbvct.iID_PhanBoVonID as IIdPhanBoVon,
		pbvct.ILoaiDuAn as ILoaiDuAn,
		pbv.Id as IdChungTu,
		pbv.iID_ParentId as IdChungTuParent,
		pbv.bActive as BActive,
		pbv.bIsGoc as IsGoc,
		pbvct.iID_DuAn_HangMucID
	from
		VDT_KHV_PhanBoVon_ChiTiet pbvct
	inner join
		VDT_KHV_PhanBoVon pbv
	on pbvct.iID_PhanBoVonID = pbv.Id
	left join
		VDT_DA_DuAn da
	on
		pbvct.iID_DuAnID = da.iID_DuAnID
	left join
		VDT_DM_LoaiCongTrinh lct
	on
		pbvct.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
	where
		pbvct.iID_PhanBoVonID = @iIdPhanBoVon
		--and pbv.bIsGoc = 1
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitiet]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec sp_vdt_find_phanbovondonvichitiet 'dbf4f050-9f98-4d26-a6d4-2f993a43da8e'

CREATE PROCEDURE [dbo].[sp_vdt_find_phanbovondonvichitiet]
@iIdPhanBoVon uniqueidentifier
AS
BEGIN
	DECLARE @iNamKeHoach int;
	DECLARE @nguonVonID int;

	SELECT @iNamKeHoach = iNamKeHoach, @nguonVonID = iID_NguonVonID FROM VDT_KHV_PhanBoVon_DonVi WHERE Id = @iIdPhanBoVon;
	
	-- kế hoạch vốn năm nay
	SELECT
			(SUM(isnull(pbvdvct.fCapPhatTaiKhoBac, 0)) + SUM(isnull(pbvdvct.fCapPhatBangLenhChi, 0))) as fKeHoachVonDuocDuyetNamNay,
			pbvdvct.iID_DuAnID as iID_DuAnID,
			pbvdvct.iID_LoaiCongTrinh as iID_LoaiCongTrinh
			INTO #tmpKeHoachVonDuocDuyetNamNay
		FROM
			VDT_KHV_PhanBoVon_ChiTiet pbvdvct
		INNER JOIN
			VDT_KHV_PhanBoVon pbvdv
		ON pbvdvct.iID_PhanBoVonID = pbvdv.Id
		WHERE 
			--pbvdv.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			pbvdv.iID_NguonVonID = @nguonVonID and pbvdv.iNamKeHoach = (@iNamKeHoach - 1)
		GROUP BY pbvdvct.iID_DuAnID, pbvdvct.iID_LoaiCongTrinh

   -- vôn kéo dài các năm trước
	BEGIN
		SELECT
			(SUM(isnull(bcqtndct.fGiaTriNamTruocChuyenNamSau, 0)) + SUM(isnull(bcqtndct.fGiaTriNamNayChuyenNamSau, 0)) - SUM(isnull(bcqtndct.fGiaTriTamUngDieuChinhGiam, 0))) as fVonKeoDaiCacNamTruoc,
			bcqtndct.iID_DuAnID as iID_DuAnID
			INTO #tmpVonKeoDaiCacNamTruoc
		FROM
			VDT_QT_BCQuyetToanNienDo_ChiTiet_01 bcqtndct
		INNER JOIN
			VDT_QT_BCQuyetToanNienDo bcqtnd
		ON bcqtndct.iID_BCQuyetToanNienDo = bcqtnd.Id
		WHERE 
			--bcqtnd.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			bcqtnd.iID_NguonVonID = @nguonVonID and bcqtnd.iNamKeHoach < (@iNamKeHoach - 1)
		GROUP BY bcqtndct.iID_DuAnID
	END

	SELECT
		khthct.*
		INTO #KhthDuocDuyet
	FROM
		VDT_KHV_KeHoach5Nam_ChiTiet khthct
	INNER JOIN
		VDT_KHV_KeHoach5Nam khth
	ON khthct.iID_KeHoach5NamID = khth.iID_KeHoach5NamID
	WHERE @iNamKeHoach >= khth.iGiaiDoanTu and @iNamKeHoach <= khth.iGiaiDoanDen

	SELECT DISTINCT
		da.iID_DuAnID,
		da.sTenDuAn, 
		da.sMaDuAn,
		CONCAT(sKhoiCong,'-', sKetThuc) as sThoiGianThucHien, 
		da.iID_CapPheDuyetID, 
		pc.sTen as sTenCapPheDuyet, 
		dt.iID_LoaiCongTrinhID,
		lct.sTenLoaiCongTrinh, 
		cdt.sTenDonVi as sTenChuDauTu, 
		dt.fTongMucDauTuDuocDuyet,
		dt.fLuyKeVonNamTruoc,
		khvnn.fKeHoachVonDuocDuyetNamNay,
		dt.fVonKeoDaiCacNamTruoc as fVonKeoDaiCacNamTruoc,
		dt.iID_DonViTienTeID,
		dt.iID_TienTeID,
		dt.fTiGiaDonVi,
		dt.fTiGia,
		dt.sTrangThaiDuAnDangKy,
		dt.ILoaiDuAn,
		isnull(khvnct.fVonBoTriTuNamDenNam, 0) as FKeHoachTrungHanDuocDuyet,
		case
			when dt.iID_ParentID is not null
			then 
				pbvctpr.fUocThucHien
			else
				dt.fUocThucHien
		end fUocThucHien,
		case
			when dt.iID_ParentID is not null
			then 
				pbvctpr.fThuHoiVonUngTruoc
			else
				dt.fThuHoiVonUngTruoc
		end fThuHoiVonUngTruoc,
		case
			when dt.iID_ParentID is not null
			then 
				pbvctpr.fThanhToan
			else
				dt.fThanhToan
		end fThanhToan,
		dt.fUocThucHien as FUocThucHienSauDc,
		dt.fThuHoiVonUngTruoc as FThuHoiVonUngTruocSauDc,
		dt.fThanhToan as FThanhToanSauDc,
		dt.iID_ParentId as IIDParentId,
		dvthda.sTenDonVi as STenDonViThucHienDuAn,
		dt.iID_DuAn_HangMucID
		FROM VDT_KHV_PhanBoVon_DonVi as tbl
		INNER JOIN VDT_KHV_PhanBoVon_DonVi_ChiTiet as dt on tbl.Id = dt.iId_PhanBoVon_DonVi
		INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
		LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
		LEFT JOIN VDT_DM_LoaiCongTrinh as lct on dt.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh
		LEFT JOIN #KhthDuocDuyet khvnct on dt.iID_DuAnID = khvnct.iID_DuAnID and khvnct.iID_LoaiCongTrinhID = dt.iID_LoaiCongTrinhID and khvnct.bActive = 1
		LEFT JOIN VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvctpr on dt.iID_ParentId = pbvctpr.Id
		LEFT JOIn #tmpKeHoachVonDuocDuyetNamNay khvnn on dt.iID_DuAnID = khvnn.iID_DuAnID and dt.iID_LoaiCongTrinhID = khvnn.iID_LoaiCongTrinh
		LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on dvthda.iID_MaDonVi = da.iID_MaDonViThucHienDuAnID
		LEFT JOIN #tmpVonKeoDaiCacNamTruoc as vkd on dt.iID_DuAnID = vkd.iID_DuAnID
		WHERE tbl.Id = @iIdPhanBoVon

		DROP TABLE #KhthDuocDuyet;
		DROP TABLE #tmpKeHoachVonDuocDuyetNamNay;
		DROP TABLE #tmpVonKeoDaiCacNamTruoc;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]
@iIdPhanBoVon uniqueidentifier,
@dNgayLap datetime
AS
BEGIN
	select 
		distinct
		--pbvct.Id,
		da.iID_DuAnID,
		--pbvct.iID_PhanBoVonID,
		da.sTenDuAn,
		da.sMaDuAn,
		da.sTrangThaiDuAn,
		da.sKhoiCong,
		da.sKetThuc,
		da.sMaKetNoi,
		da.iID_MaDonViThucHienDuAnID as IIdMaDonViThucHienDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		lct.sTenLoaiCongTrinh,
		'' as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		pbvct.iID_LoaiCongTrinh as iID_LoaiCongTrinhID,
		null as iID_CapPheDuyetID,
		'' as sLNS,
		'' as sL,
		'' as sK,
		'' as sM,
		'' as sTM,
		'' as sTTM,
		'' as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		pbvct.sGhiChu as sGhiChu,
		cast(0 as float) as fChiTieuGoc,
		cast(0 as float) as FCapPhatTaiKhoBac,
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		pbvct.fGiaTriThuHoi as FGiaTriThuHoi,
		case when pbvct.fGiaTrPhanBo is not null then pbvct.fGiaTrPhanBo else pbvdxct.fThanhToan end as FGiaTrPhanBo,
		--isnull(pbvct.Id, NEWID()) as IIdPhanBoVonId,
		pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID as IIdPhanBoVon,
		pbvct.ILoaiDuAn as ILoaiDuAn,
		pbv.Id as IdChungTu,
		pbv.iID_ParentId as IdChungTuParent,
		pbv.bActive as BActive,
		pbv.bIsGoc as IsGoc,
		--case when pbvct.fThanhToanDeXuat is not null then pbvct.fThanhToanDeXuat else pbvdxct.fThanhToan end as fThanhToanDeXuat
		pbvdxct.fThanhToan as fThanhToanDeXuat,
		pbvct.iID_DuAn_HangMucID
	from
		VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvct
	inner join
		VDT_KHV_PhanBoVon_DonVi_PheDuyet pbv
	on pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = pbv.Id
	inner join 
	VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvdxct on pbvdxct.iID_DuAn_HangMucID = pbvct.iID_DuAn_HangMucID
	left join
		VDT_DA_DuAn da
	on
		pbvct.iID_DuAnID = da.iID_DuAnID
	left join
		VDT_DM_LoaiCongTrinh lct
	on
		pbvct.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
	where
		pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = @iIdPhanBoVon
		and pbv.bIsGoc = 1 and pbvdxct.bActive = 1
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1]
@iIdDuToanId uniqueidentifier
AS
BEGIN
	SELECT cp.iID_ChiPhiID, cp.iID_DuAn_ChiPhi, SUM(ISNULL(cp.fTienPheDuyet, 0)) as GiaTriPheDuyet , SUM(ISNULL(cp.fTienPheDuyetQDDT, 0)) as GiaTriPheDuyetQDDT INTO #tmpDuToanChiPhi
	FROM VDT_DA_DuToan as tbl
	INNER JOIN VDT_DA_DuToan_ChiPhi as cp on tbl.iID_DuToanID = cp.iID_DuToanID
	WHERE tbl.bActive = 1 AND tbl.iID_DuToanID = @iIdDuToanId
	GROUP BY cp.iID_ChiPhiID, cp.iID_DuAn_ChiPhi

	SELECT	
		NEWID() as Id,
		cp.sTenChiPhi as TenChiPhi,
		NULL as IdDuToanChiPhi,
		tmp.iID_ChiPhiID as IdChiPhi,
		NULL AS IdDuToan,
		tmp.GiaTriPheDuyetQDDT,
		tmp.GiaTriPheDuyet,
		tmp.iID_DuAn_ChiPhi as IdChiPhiDuAn,
		CAST(0 as bit) as IsHangCha,
		NULL as IsHangCha,
		(CASE WHEN cp.iID_ChiPhi_Parent IS NULL THEN CAST(1 AS bit) ELSE CAST(0 as bit) END) as IsLoaiChiPhi,
		cp.IThuTu,
		cp.iID_ChiPhi_Parent as IdChiPhiDuAnParent,
		CAST(1 as bit) as IsDuAnChiPhiOld,
		CAST(1 as bit) as IsEditHangMuc,
		CAST(cp.iID_DuAn_ChiPhi as nvarchar(max)) as MaOrder,
		NULL as FGiaTriDieuChinh,
		NULL as GiaTriTruocDieuChinh,
		CAST(1 as int) as PhanCap,
		cp.iID_DuAn_ChiPhi as ChiPhiId,
		dmcp.sMaChiPhi as MaChiPhi
	FROM #tmpDuToanChiPhi as tmp
	INNER JOIN VDT_DM_DuAn_ChiPhi as cp on tmp.iID_DuAn_ChiPhi = cp.iID_DuAn_ChiPhi
	LEFT JOIN VDT_DM_ChiPhi as dmcp on cp.iID_ChiPhi = dmcp.iID_ChiPhi
	ORDER BY cp.iThuTu

	DROP TABLE #tmpDuToanChiPhi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_get_baocaodquyettoanniendo1]
@iIdMaDonVi nvarchar(100),
@iNamKeHoach int,
@iIdNguonVon int
AS
BEGIN
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn, dt.iID_LoaiCongTrinh, CAST(0 as bit) BIsChuyenTiep INTO #tmp
	FROM VDT_KHV_PhanBoVon as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE da.iID_MaDonViThucHienDuAnID = @iIdMaDonVi AND tbl.iNamKeHoach = @iNamKeHoach AND tbl.iID_NguonVonID = @iIdNguonVon
	UNION ALL
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn, dt.iID_LoaiCongTrinh, CAST(0 as bit) BIsChuyenTiep
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE iNamKeHoach = @iNamKeHoach AND (ISNULL(dt.fGiaTriNamTruocChuyenNamSau, 0) <> 0 OR ISNULL(dt.fGiaTriNamNayChuyenNamSau, 0) <> 0)

	-- Check du an chuyen tiep
	UPDATE tmp
	SET
		BIsChuyenTiep = 1
	FROM #tmp as tmp
	INNER JOIN (
		SELECT DISTINCT dt.iID_DuAnID 
		FROM VDT_KHV_PhanBoVon as tbl 
		INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID 
		WHERE tbl.bActive = 1 AND tbl.iNamKeHoach = (@iNamKeHoach - 1)
		) as mp on tmp.IIDDuAnID = mp.iID_DuAnID

	-- Tong muc dau tu
	SELECT tmp.IIDDuAnID, SUM(ISNULL(qd.fTongMucDauTuPheDuyet, 0)) as fTongMucDauTu INTO #tmpTongMucDauTu
	FROM (SELECT DISTINCT IIDDuAnID FROM #tmp) as tmp
	INNER JOIN VDT_DA_QDDauTu as qd on tmp.IIDDuAnID = qd.iID_DuAnID
	WHERE qd.BActive = 1
	GROUP BY tmp.IIDDuAnID

	---- Kho bac
	BEGIN
		-- TongHopDuLieu nam truoc
		SELECT tbl.IIDDuAnID, 
			tbl.iID_LoaiCongTrinh,
			SUM(ISNULL(fLuyKeThanhToanNamTruoc, 0)) fLuyKeThanhToanNamTruoc,
			SUM(ISNULL(fLuyKeThanhToanNamTruocDelete, 0)) fLuyKeThanhToanNamTruocDelete,
			SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruoc, 0)) FTamUngTheoCheDoChuaThuHoiNamTruoc,
			SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) FTamUngTheoCheDoChuaThuHoiNamTruocDelete,
			SUM(ISNULL(fDieuChinhGiamNamTruoc, 0)) fDieuChinhGiamNamTruoc,
			SUM(ISNULL(fDieuChinhGiamNamTruocDelete, 0)) fDieuChinhGiamNamTruocDelete INTO #tmpNamTruocKB
		FROM
		(
			SELECT tmp.IIDDuAnID,
					tmp.iID_LoaiCongTrinh,
				   (CASE WHEN (sMaDich = '403' AND sMaNguonCha = '301' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruoc,
				   (CASE WHEN (sMaNguon = '403' AND sMaNguonCha = '301' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruocDelete,

				   (CASE WHEN (sMaDich = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
				   (CASE WHEN (sMaNguon = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruocDelete,

				   (CASE WHEN (sMaNguon = '211c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('121a', '131')) AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruoc,
				   (CASE WHEN (sMaDich = '211c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('121a', '131')) AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruocDelete
			FROM (SELECT DISTINCT IIDDuAnID, iID_LoaiCongTrinh FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh
			WHERE dt.iNamKeHoach = @iNamKeHoach-1
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, sMaTienTrinh, tmp.iID_LoaiCongTrinh
		) as tbl
		 GROUP BY tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh

		-- TongHopDuLieu nam nay
		SELECT tbl.IIDDuAnID,
			tbl.iID_LoaiCongTrinh,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNay, 0)) fTamUngNamTruocThuHoiNamNay,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNayDelete, 0)) fTamUngNamTruocThuHoiNamNayDelete,
			SUM(ISNULL(fKHVNamTruocChuyenNamNay, 0)) fKHVNamTruocChuyenNamNay,
			SUM(ISNULL(fKHVNamTruocChuyenNamNayDelete, 0)) fKHVNamTruocChuyenNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruoc, 0)) fTongThanhToanSuDungVonNamTruoc,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruocDelete, 0)) fTongThanhToanSuDungVonNamTruocDelete,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruoc, 0)) fTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruocDelete, 0)) fTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruoc, 0)) fThuHoiTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) fThuHoiTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fKHVNamNay, 0)) fKHVNamNay,
			SUM(ISNULL(fKHVNamNayDelete, 0)) fKHVNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNay, 0)) fTongThanhToanSuDungVonNamNay,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNayDelete, 0)) fTongThanhToanSuDungVonNamNayDelete,
			SUM(ISNULL(fThuHoiUngCacNamTruoc, 0)) fThuHoiUngCacNamTruoc,
			SUM(ISNULL(fThuHoiUngCacNamTruocDelete, 0)) fThuHoiUngCacNamTruocDelete,
			SUM(ISNULL(fKeHoachUngNamNay, 0)) fKeHoachUngNamNay,
			SUM(ISNULL(fKeHoachUngNamNayDelete, 0)) fKeHoachUngNamNayDelete,
			SUM(ISNULL(fTongTamUngNamNay, 0)) fTongTamUngNamNay,
			SUM(ISNULL(fTongTamUngNamNayDelete, 0)) fTongTamUngNamNayDelete,
			SUM(ISNULL(fTongThuHoiTamUngNamNay, 0)) fTongThuHoiTamUngNamNay,
			SUM(ISNULL(fTongThuHoiTamUngNamNayDelete, 0)) fTongThuHoiTamUngNamNayDelete,
			SUM(ISNULL(fThuHoiUngNamTruoc, 0)) fThuHoiUngNamTruoc,
			SUM(ISNULL(fThuHoiUngNamTruocDelete, 0)) fThuHoiUngNamTruocDelete,
			SUM(ISNULL(fTongThuHoiUngNamNay, 0)) fTongThuHoiUngNamNay,
			SUM(ISNULL(fTongThuHoiUngNamNayDelete, 0)) fTongThuHoiUngNamNayDelete INTO #tmpNamNayKB
		FROM
		(
			SELECT  tmp.IIDDuAnID, tmp.iID_LoaiCongTrinh,
					(CASE WHEN (sMaNguon = '211a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('121a', '131') AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNay,
					(CASE WHEN (sMaDich = '211a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('121a', '131') AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNayDelete,
					
					(CASE WHEN sMaNguon = '111' AND sMaDich = '000' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNay,
					(CASE WHEN sMaDich = '111' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNayDelete,
					
					(CASE WHEN (sMaDich = '201' AND sMaNguonCha = '111' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '201' AND sMaNguonCha = '111' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruocDelete,
					
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruocDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruocDelete,
					
					(CASE WHEN sMaNguon = '101' AND sMaDich = '000' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNay,
					(CASE WHEN sMaDich = '101' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNayDelete,
					
					(CASE WHEN (sMaDich = '201' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNay,
					(CASE WHEN (sMaNguon = '201' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNayDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruocDelete,
					
					(CASE WHEN (sMaDich = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNay,
					(CASE WHEN (sMaNguon = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNayDelete,

					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNay,
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNayDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNay,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruocDelete,

					(CASE WHEN (sMaDich = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = (CASE WHEN iID_NguonVonID = 1 THEN '100' ELSE '200' END) AND iID_NguonVonID = @iIdNguonVon AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNay,
				   (CASE WHEN (sMaNguon = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNayDelete
			FROM (SELECT DISTINCT IIDDuAnID, iID_LoaiCongTrinh FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh
			WHERE dt.iNamKeHoach = @iNamKeHoach
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh, iThuHoiTUCheDo, ILoaiUng, bKeHoach, tmp.iID_LoaiCongTrinh
		) as tbl
		GROUP BY tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh
	
	END
	
	-- co quan tai chinh
	BEGIN
		SELECT tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh,
				SUM(ISNULL(fLuyKeThanhToanNamTruoc, 0)) fLuyKeThanhToanNamTruoc,
				SUM(ISNULL(fLuyKeThanhToanNamTruocDelete, 0)) fLuyKeThanhToanNamTruocDelete,
				
				SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruoc, 0)) FTamUngTheoCheDoChuaThuHoiNamTruoc,
				SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) FTamUngTheoCheDoChuaThuHoiNamTruocDelete,
				SUM(ISNULL(fDieuChinhGiamNamTruoc, 0)) fDieuChinhGiamNamTruoc,
				SUM(ISNULL(fDieuChinhGiamNamTruocDelete, 0)) fDieuChinhGiamNamTruocDelete INTO #tmpNamTruoc
			FROM
			(
				-- TongHopDuLieu nam truoc
				SELECT tmp.IIDDuAnID, tmp.iID_LoaiCongTrinh,
					(CASE WHEN (sMaDich = '404' AND sMaNguonCha = '302' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruoc,
					(CASE WHEN (sMaNguon = '404' AND sMaNguonCha = '302' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruocDelete,

					

					(CASE WHEN (sMaDich = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
					(CASE WHEN (sMaNguon = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruocDelete,

					(CASE WHEN (sMaNguon = '212c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('122a', '132')) AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruoc,
					(CASE WHEN (sMaDich = '212c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('122a', '132')) AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruocDelete
				FROM (SELECT DISTINCT IIDDuAnID, iID_LoaiCongTrinh FROM #tmp) as tmp
				INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND tmp.iID_LoaiCongTrinh = dt.iID_LoaiCongTrinh
				WHERE dt.iNamKeHoach = @iNamKeHoach -1
				GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, sMaTienTrinh, tmp.iID_LoaiCongTrinh
			) as tbl
		GROUP BY tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh

		-- TongHopDuLieu nam nay
		SELECT tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNay, 0)) fTamUngNamTruocThuHoiNamNay,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNayDelete, 0)) fTamUngNamTruocThuHoiNamNayDelete,
			SUM(ISNULL(fKHVNamTruocChuyenNamNay, 0)) fKHVNamTruocChuyenNamNay,
			SUM(ISNULL(fKHVNamTruocChuyenNamNayDelete, 0)) fKHVNamTruocChuyenNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruoc, 0)) fTongThanhToanSuDungVonNamTruoc,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruocDelete, 0)) fTongThanhToanSuDungVonNamTruocDelete,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruoc, 0)) fTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruocDelete, 0)) fTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruoc, 0)) fThuHoiTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) fThuHoiTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fKHVNamNay, 0)) fKHVNamNay,
			SUM(ISNULL(fKHVNamNayDelete, 0)) fKHVNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNay, 0)) fTongThanhToanSuDungVonNamNay,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNayDelete, 0)) fTongThanhToanSuDungVonNamNayDelete,
			SUM(ISNULL(fThuHoiUngCacNamTruoc, 0)) fThuHoiUngCacNamTruoc,
			SUM(ISNULL(fThuHoiUngCacNamTruocDelete, 0)) fThuHoiUngCacNamTruocDelete,
			SUM(ISNULL(fKeHoachUngNamNay, 0)) fKeHoachUngNamNay,
			SUM(ISNULL(fKeHoachUngNamNayDelete, 0)) fKeHoachUngNamNayDelete,
			SUM(ISNULL(fTongTamUngNamNay, 0)) fTongTamUngNamNay,
			SUM(ISNULL(fTongTamUngNamNayDelete, 0)) fTongTamUngNamNayDelete,
			SUM(ISNULL(fTongThuHoiTamUngNamNay, 0)) fTongThuHoiTamUngNamNay,
			SUM(ISNULL(fTongThuHoiTamUngNamNayDelete, 0)) fTongThuHoiTamUngNamNayDelete,
			SUM(ISNULL(fThuHoiUngNamTruoc, 0)) fThuHoiUngNamTruoc,
			SUM(ISNULL(fThuHoiUngNamTruocDelete, 0)) fThuHoiUngNamTruocDelete,
			SUM(ISNULL(fTongThuHoiUngNamNay, 0)) fTongThuHoiUngNamNay,
			SUM(ISNULL(fTongThuHoiUngNamNayDelete, 0)) fTongThuHoiUngNamNayDelete INTO #tmpNamNay
		FROM
		(
			SELECT  tmp.IIDDuAnID, tmp.iID_LoaiCongTrinh,
					(CASE WHEN (sMaNguon = '212a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('122a', '132') AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNay,
					(CASE WHEN (sMaDich = '212a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('122a', '132') AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNayDelete,

					(CASE WHEN sMaNguon = '112' AND sMaDich = '000' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNay,
					(CASE WHEN sMaDich = '112' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNayDelete,

					(CASE WHEN (sMaDich = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruocDelete,

					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruocDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruocDelete,

					(CASE WHEN sMaNguon = '102' AND sMaDich = '000' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNay,
					(CASE WHEN sMaDich = '102' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNayDelete,

					(CASE WHEN (sMaDich = '202' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNay,
					(CASE WHEN (sMaNguon = '202' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNayDelete,
					
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruocDelete,
					
					(CASE WHEN (sMaDich = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNay,
					(CASE WHEN (sMaNguon = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNayDelete,

					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNay,
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNay,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruocDelete,
					
					(CASE WHEN (sMaDich = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = (CASE WHEN iID_NguonVonID = 1 THEN '100' ELSE '200' END) AND iID_NguonVonID = @iIdNguonVon  AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNay,
					(CASE WHEN (sMaNguon = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon  AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNayDelete
			FROM (SELECT DISTINCT IIDDuAnID, iID_LoaiCongTrinh FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID AND dt.iID_LoaiCongTrinh = tmp.iID_LoaiCongTrinh
			WHERE dt.iNamKeHoach = @iNamKeHoach
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh, iThuHoiTUCheDo, ILoaiUng, bKeHoach, tmp.iID_LoaiCongTrinh
		) as tbl
		GROUP BY tbl.IIDDuAnID, tbl.iID_LoaiCongTrinh
	END

	
	SELECT tmp.IIDDuAnID, tmp.SMaDuAn, tmp.SDiaDiem, tmp.STenDuAn, CAST(1 as int) as ICoQuanThanhToan, tmp.iID_LoaiCongTrinh as IIdLoaiCongTrinh, lct.STenLoaiCongTrinh, tmp.BIsChuyenTiep, lct.SMaLoaiCongTrinh,
		(ISNULL(nn.fKHVNamNay, 0) - ISNULL(nn.fKHVNamNayDelete, 0)) as FKHVNamNay, 
		(ISNULL(nn.fKHVNamTruocChuyenNamNay, 0) - ISNULL(nn.fKHVNamTruocChuyenNamNayDelete, 0)) as FKHVNamTruocChuyenNamNay, 
		(ISNULL(nn.fTamUngNamTruocThuHoiNamNay, 0) - ISNULL(nn.fTamUngNamTruocThuHoiNamNayDelete, 0)) as FTamUngNamTruocThuHoiNamNay,
		(ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) as FThuHoiTamUngNamNayDungVonNamTruoc, 
		(ISNULL(nn.fTongTamUngNamNay, 0) - ISNULL(nn.fTongTamUngNamNayDelete, 0)) as FTongTamUngNamNay, 
		(((ISNULL(nn.fTongThanhToanSuDungVonNamNay, 0) - ISNULL(nn.fThuHoiUngCacNamTruoc, 0) + ISNULL(nn.fTongThuHoiUngNamNay, 0)) 
			- (ISNULL(nn.fTongThanhToanSuDungVonNamNayDelete, 0) - ISNULL(nn.fThuHoiUngCacNamTruocDelete, 0) + ISNULL(nn.fTongThuHoiUngNamNayDelete, 0)))) as FTongThanhToanSuDungVonNamNay,
		((ISNULL(nn.fTongThanhToanSuDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiUngNamTruoc, 0)) - (ISNULL(nn.fTongThanhToanSuDungVonNamTruocDelete, 0) - ISNULL(nn.fThuHoiUngNamTruocDelete, 0))) as FTongThanhToanSuDungVonNamTruoc, 
		(ISNULL(nn.fTongThuHoiTamUngNamNay, 0) - ISNULL(nn.fTongThuHoiTamUngNamNayDelete, 0)) as FTongThuHoiTamUngNamNay, 
		(ISNULL(nn.fTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fTamUngNamNayDungVonNamTruocDelete, 0)) as FTamUngNamNayDungVonNamTruoc,
		(ISNULL(nt.fLuyKeThanhToanNamTruoc, 0) - ISNULL(nt.fLuyKeThanhToanNamTruocDelete, 0)) as FLuyKeThanhToanNamTruoc, 
		(ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruoc, 0) - ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
		ISNULL(dt.fTongMucDauTu, 0) as FTongMucDauTu,
		CAST(0 as float) as FGiaTriTamUngDieuChinhGiam,
		CAST(0 as float) as FGiaTriNamTruocChuyenNamSau,
		CAST(0 as float) as FGiaTriNamNayChuyenNamSau
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	LEFT JOIN #tmpNamNayKB as nn on tmp.IIDDuAnID = nn.IIDDuAnID
	LEFT JOIN #tmpNamTruocKB as nt on tmp.IIDDuAnID = nt.IIDDuAnID
	LEFT JOIN #tmpTongMucDauTu as dt on tmp.IIDDuAnID = dt.IIDDuAnID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on tmp.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	UNION ALL
	SELECT tmp.IIDDuAnID, tmp.SMaDuAn, tmp.SDiaDiem, tmp.STenDuAn, CAST(2 as int) as ICoQuanThanhToan, tmp.iID_LoaiCongTrinh as IIdLoaiCongTrinh, lct.STenLoaiCongTrinh, tmp.BIsChuyenTiep, lct.SMaLoaiCongTrinh,
		(ISNULL(nn.fKHVNamNay, 0) - ISNULL(nn.fKHVNamNayDelete, 0)) as FKHVNamNay, 
		(ISNULL(nn.fKHVNamTruocChuyenNamNay, 0) - ISNULL(nn.fKHVNamTruocChuyenNamNayDelete, 0)) as FKHVNamTruocChuyenNamNay, 
		(ISNULL(nn.fTamUngNamTruocThuHoiNamNay, 0) - ISNULL(nn.fTamUngNamTruocThuHoiNamNayDelete, 0)) as FTamUngNamTruocThuHoiNamNay,
		(ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) as FThuHoiTamUngNamNayDungVonNamTruoc, 
		(ISNULL(nn.fTongTamUngNamNay, 0) - ISNULL(nn.fTongTamUngNamNayDelete, 0)) as FTongTamUngNamNay, 
		(((ISNULL(nn.fTongThanhToanSuDungVonNamNay, 0) - ISNULL(nn.fThuHoiUngCacNamTruoc, 0) + ISNULL(nn.fTongThuHoiUngNamNay, 0)) 
			- (ISNULL(nn.fTongThanhToanSuDungVonNamNayDelete, 0) - ISNULL(nn.fThuHoiUngCacNamTruocDelete, 0) + ISNULL(nn.fTongThuHoiUngNamNayDelete, 0)))) as FTongThanhToanSuDungVonNamNay,
		((ISNULL(nn.fTongThanhToanSuDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiUngNamTruoc, 0)) - (ISNULL(nn.fTongThanhToanSuDungVonNamTruocDelete, 0) - ISNULL(nn.fThuHoiUngNamTruocDelete, 0))) as FTongThanhToanSuDungVonNamTruoc, 
		(ISNULL(nn.fTongThuHoiTamUngNamNay, 0) - ISNULL(nn.fTongThuHoiTamUngNamNayDelete, 0)) as FTongThuHoiTamUngNamNay, 
		(ISNULL(nn.fTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fTamUngNamNayDungVonNamTruocDelete, 0)) as FTamUngNamNayDungVonNamTruoc,
		(ISNULL(nt.fLuyKeThanhToanNamTruoc, 0) - ISNULL(nt.fLuyKeThanhToanNamTruocDelete, 0)) as FLuyKeThanhToanNamTruoc, 
		(ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruoc, 0) - ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) as FTamUngTheoCheDoChuaThuHoiNamTruoc, 
		ISNULL(dt.fTongMucDauTu, 0) as FTongMucDauTu,
		CAST(0 as float) as FGiaTriTamUngDieuChinhGiam,
		CAST(0 as float) as FGiaTriNamTruocChuyenNamSau,
		CAST(0 as float) as FGiaTriNamNayChuyenNamSau
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	LEFT JOIN #tmpNamNay as nn on tmp.IIDDuAnID = nn.IIDDuAnID
	LEFT JOIN #tmpNamTruoc as nt on tmp.IIDDuAnID = nt.IIDDuAnID
	LEFT JOIN #tmpTongMucDauTu as dt on tmp.IIDDuAnID = dt.IIDDuAnID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on tmp.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh

	DROP TABLE #tmpNamTruoc
	DROP TABLE #tmpNamNay
	DROP TABLE #tmpNamTruocKB
	DROP TABLE #tmpNamNayKB
	DROP TABLE #tmp
	DROP TABLE #tmpTongMucDauTu
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
	@iNamKeHoach int, 
	@ngayLap DateTime,
	@maDonViQuanLyId nvarchar(50),
	@nguonVonID int,
	@filterHasQDDT int
AS
Begin
	Select
		da.iID_DuAnID, 
		da.sTenDuAn,
		da.sMaDuAn,
		nv.iID_MaNguonNganSach,
		CONCAT(da.sKhoiCong,'-', da.sKetThuc) as sThoiGianThucHien,
		da.iID_CapPheDuyetID,
		pc.sTen as sTenCapPheDuyet,
		case 
			when dahm.IdLoaiCongTrinh is not null then dahm.IdLoaiCongTrinh else da.iID_LoaiCongTrinhID
		end as iID_LoaiCongTrinhID,
		lct.sTenLoaiCongTrinh, 
		cdt.sTenDonVi as sTenChuDauTu,
		da.iID_DonViTienTeID,
		da.iID_TienTeID,
		da.fTiGiaDonVi,
		da.fTiGia,
		da.sTrangThaiDuAn as sTrangThaiDuAnDangKy,
		dvthda.sTenDonVi as STenDonViThucHienDuAn,
		da.dDateCreate, 
		dahm.iID_DuAn_HangMucID INTO #tmp
		from VDT_DA_DuAn da
		inner JOIN VDT_KHV_KeHoach5Nam_ChiTiet khvct on da.iID_DuAnID = khvct.iID_DuAnID
		LEFT JOIN NguonNganSach nv ON khvct.iID_NguonVonID = nv.iID_MaNguonNganSach 
		LEFT JOIN VDT_DA_DuAn_HangMuc dahm on da.iID_DuAnID = dahm.iID_DuAnID
		LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh or dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
		LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
		LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on da.iID_MaDonViThucHienDuAnID = dvthda.iID_MaDonVi
		Where  da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@maDonViQuanLyId)) And [dbo].[fn_CheckDieuKienDuAn](da.iID_DuAnID,@ngayLap) = 1 
			And 
			( @filterHasQDDT is null  -- search all
			
			or

				( -- search có quyết định đầu tư
					(@filterHasQDDT = 1 and da.iID_DuAnID in (SELECT DISTINCT qqdt.iID_DuAnID FROM VDT_DA_QDDauTu qqdt JOIN VDT_DA_QDDauTu_NguonVon qddtnv ON qqdt.iID_QDDauTuID=qddtnv.iID_QDDauTuID 
													JOIN NguonNganSach nv ON qddtnv.iID_NguonVonID=nv.iID_MaNguonNganSach 
															
													WHERE  (@nguonVonID IS NULL OR nv.iID_MaNguonNganSach=@nguonVonID))
					) or
					-- search k có quyết định đầu tư nhưng có chủ trương đầu tư
					(@filterHasQDDT = 0 and da.iID_DuAnID in 
						(SELECT DISTINCT ctdt.iID_DuAnID FROM VDT_DA_ChuTruongDauTu ctdt 
							join VDT_DA_ChuTruongDauTu_NguonVon ctdtnv on ctdt.iID_ChuTruongDauTuID = ctdtnv.iID_ChuTruongDauTuID
							JOIN NguonNganSach nv ON ctdtnv.iID_NguonVonID=nv.iID_MaNguonNganSach 															
							WHERE  (@nguonVonID IS NULL OR nv.iID_MaNguonNganSach=@nguonVonID)
							and ctdt.iID_DuAnID not in ( select iID_DuAnID from VDT_DA_QDDauTu)
						)
					) or 
					-- search k có chủ trương đầu tư
					(
						@filterHasQDDT = 2 and da.iID_DuAnID in 
							(
								select distinct da.iID_DuAnID from VDT_DA_DuAn da where da.iID_DuAnID not in (select DuAnId as iID_DuAnID from VDT_DA_ChuTruongDauTu_NguonVon) 
							)
					)

				)
			)

			and da.iID_DuAnID not in (select iID_DuAnID from VDT_QT_QuyetToan)

	select tmp.* into #tmpData from #tmp as tmp

	--Union ALL

	--Select 
	--	da.iID_DuAnID,
	--	da.sTenDuAn,
	--	da.sMaDuAn,
	--	nv.iID_MaNguonNganSach,
	--	CONCAT(da.sKhoiCong,'-', da.sKetThuc) as sThoiGianThucHien,
	--	da.iID_CapPheDuyetID,
	--	pc.sTen as sTenCapPheDuyet,
	--	case 
	--		when dahm.IdLoaiCongTrinh is not null then dahm.IdLoaiCongTrinh else da.iID_LoaiCongTrinhID
	--	end as iID_LoaiCongTrinhID,
	--	lct.sTenLoaiCongTrinh,
	--	cdt.sTenDonVi as sTenChuDauTu,
	--	da.iID_DonViTienTeID,
	--	da.iID_TienTeID, 
	--	da.fTiGiaDonVi,
	--	da.fTiGia,
	--	da.sTrangThaiDuAn as sTrangThaiDuAnDangKy,
	--	dvthda.sTenDonVi as STenDonViThucHienDuAn,
	--	da.dDateCreate
	--from VDT_DA_DuAn da
	--	inner JOIN VDT_KHV_KeHoach5Nam_ChiTiet khvct on da.iID_DuAnID = khvct.iID_DuAnID
	--	LEFT JOIN NguonNganSach nv ON khvct.iID_NguonVonID = nv.iID_MaNguonNganSach 
	--	LEFT JOIN VDT_DA_DuAn_HangMuc dahm on da.iID_DuAnID = dahm.iID_DuAnID
	--	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh or dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
	--	LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
	--	LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
	--	LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on da.iID_MaDonViThucHienDuAnID = dvthda.iID_MaDonVi
	--Where 
	--	da.iID_DuAnID in (SELECT ctdt.iID_DuAnID FROM VDT_DA_ChuTruongDauTu ctdt)
	--	and da.iID_DuAnID not in (select tmpexisted.iID_DuAnID from #tmp tmpexisted)
	--	And da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@maDonViQuanLyId))

	-- Tong muc dau tu
	SELECT
		da.iID_DuAnID,
		CASE
			when qddt.iID_QDDauTuID is not null 
			then ISNULL(qddt.fTongMucDauTuPheDuyet, 0)
			else ISNULL(ctdt.fTMDTDuKienPheDuyet, 0)
		END fGiaTriDauTu
		INTO #tmpDataPD
	FROM
		#tmpData da
	LEFT JOIN
		VDT_DA_QDDauTu qddt
	ON
		da.iID_DuAnID = qddt.iID_DuAnID AND qddt.bActive = 1
	LEFT JOIN
		VDT_DA_ChuTruongDauTu ctdt
	ON
		da.iID_DuAnID = ctdt.iID_DuAnID AND ctdt.bActive = 1

	--luy ke von nam truoc
	BEGIN
		SELECT
			(SUM(isnull(pbvdvct.fCapPhatTaiKhoBac, 0)) + SUM(isnull(pbvdvct.fCapPhatBangLenhChi, 0))) as fLuyKeVonNamTruoc,
			--(SUM(isnull(bcqtndct.fGiaTriNamTruocChuyenNamSau, 0)) + SUM(isnull(bcqtndct.fGiaTriNamNayChuyenNamSau, 0)) - SUM(isnull(bcqtndct.fGiaTriTamUngDieuChinhGiam, 0)))) as fLuyKeVonNamTruoc,
			pbvdvct.iID_DuAnID as iID_DuAnID
			INTO #tmpLuyKeNamTruoc
		FROM
			VDT_KHV_PhanBoVon_ChiTiet pbvdvct
		INNER JOIN
			VDT_KHV_PhanBoVon pbvdv
		ON pbvdvct.iID_PhanBoVonID = pbvdv.Id
		--LEFT JOIN
		--	VDT_QT_BCQuyetToanNienDo_ChiTiet_01 bcqtndct
		--ON pbvdvct.iID_DuAnID = bcqtndct.iID_DuAnID
		WHERE 
			--pbvdv.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			pbvdv.iID_NguonVonID = @nguonVonID and pbvdv.iNamKeHoach <= (@iNamKeHoach - 2)
		GROUP BY pbvdvct.iID_DuAnID
	END

	-- kế hoạch vốn năm nay
	BEGIN
		SELECT
			(SUM(isnull(pbvdvct.fCapPhatTaiKhoBac, 0)) + SUM(isnull(pbvdvct.fCapPhatBangLenhChi, 0))) as fKeHoachVonDuocDuyetNamNay,
			pbvdvct.iID_DuAnID as iID_DuAnID,
			pbvdvct.iID_LoaiCongTrinh as iID_LoaiCongTrinh 
			INTO #tmpKeHoachVonDuocDuyetNamNay
		FROM
			VDT_KHV_PhanBoVon_ChiTiet pbvdvct
		INNER JOIN
			VDT_KHV_PhanBoVon pbvdv
		ON pbvdvct.iID_PhanBoVonID = pbvdv.Id
		WHERE 
			--pbvdv.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			pbvdv.iID_NguonVonID = @nguonVonID and pbvdv.iNamKeHoach = (@iNamKeHoach - 1)
		GROUP BY pbvdvct.iID_DuAnID, pbvdvct.iID_LoaiCongTrinh
	END

	-- vôn kéo dài các năm trước
	BEGIN
		SELECT
			(SUM(isnull(bcqtndct.fGiaTriNamTruocChuyenNamSau, 0)) + SUM(isnull(bcqtndct.fGiaTriNamNayChuyenNamSau, 0)) - SUM(isnull(bcqtndct.fGiaTriTamUngDieuChinhGiam, 0))) as fVonKeoDaiCacNamTruoc,
			bcqtndct.iID_DuAnID as iID_DuAnID
			INTO #tmpVonKeoDaiCacNamTruoc
		FROM
			VDT_QT_BCQuyetToanNienDo_ChiTiet_01 bcqtndct
		INNER JOIN
			VDT_QT_BCQuyetToanNienDo bcqtnd
		ON bcqtndct.iID_BCQuyetToanNienDo = bcqtnd.Id
		WHERE 
			--bcqtnd.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			bcqtnd.iID_NguonVonID = @nguonVonID and bcqtnd.iNamKeHoach < (@iNamKeHoach - 1)
		GROUP BY bcqtndct.iID_DuAnID
	END
	
	BEGIN
		SELECT
			khthct.*,
			khth.ILoai
			into #tmpThDd
		FROM
			VDT_KHV_KeHoach5Nam_ChiTiet khthct
		INNER JOIN
			VDT_KHV_KeHoach5Nam khth
		ON khthct.iID_KeHoach5NamID = khth.iID_KeHoach5NamID
		WHERE @iNamKeHoach >= khth.iGiaiDoanTu and @iNamKeHoach <= khth.iGiaiDoanDen
	END

	SELECT
			tmp.*,
			tbl_tmdt.fGiaTriDauTu as fTongMucDauTuDuocDuyet,
			isnull(lknt.fLuyKeVonNamTruoc, 0) as fLuyKeVonNamTruoc,
			isnull(khnn.fKeHoachVonDuocDuyetNamNay, 0) as fKeHoachVonDuocDuyetNamNay,
			isnull(vkd.fVonKeoDaiCacNamTruoc, 0) as fVonKeoDaiCacNamTruoc,
			cast(0 as float) as fUocThucHien,
			cast(0 as float) as fThuHoiVonUngTruoc,
			cast(0 as float) as fThanhToan,
			cast(0 as float) as FUocThucHienSauDc,
			cast(0 as float) as FThuHoiVonUngTruocSauDc,
			cast(0 as float) as FThanhToanSauDc,
			null as IIDParentId,
			case
				when ((da.sTrangThaiDuAn = N'THUC_HIEN') and (da.bIsKetThuc IS NULL)) then 2 else 1
			end ILoaiDuAn,
			isnull(khvnct.fVonBoTriTuNamDenNam, 0) as FKeHoachTrungHanDuocDuyet
		FROM #tmpData as tmp
		LEFT JOIN #tmpDataPD as tbl_tmdt on tmp.iID_DuAnID = tbl_tmdt.iID_DuAnID
		LEFT JOIN #tmpLuyKeNamTruoc as lknt on tmp.iID_DuAnID = lknt.iID_DuAnID
		LEFT JOIN #tmpKeHoachVonDuocDuyetNamNay as khnn on tmp.iID_DuAnID = khnn.iID_DuAnID and khnn.iID_LoaiCongTrinh = tmp.iID_LoaiCongTrinhID
		LEFT JOIN #tmpVonKeoDaiCacNamTruoc as vkd on tmp.iID_DuAnID = vkd.iID_DuAnID
		LEFT JOIN #tmpThDd khvnct on tmp.iID_DuAnID = khvnct.iID_DuAnID and tmp.iID_LoaiCongTrinhID = khvnct.iID_LoaiCongTrinhID and khvnct.iID_NguonVonID = @nguonVonID
		LEFT JOIN VDT_DA_DuAn da on tmp.iID_DuAnID = da.iID_DuAnID
		where iID_MaNguonNganSach = @nguonVonID
		ORDER BY tmp.dDateCreate desc

	drop table #tmpThDd;
	drop table #tmp;
	drop table #tmpData;
	drop table #tmpDataPD;
	drop table #tmpLuyKeNamTruoc;
	drop table #tmpKeHoachVonDuocDuyetNamNay;
	drop table #tmpVonKeoDaiCacNamTruoc;

End
;




GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_pheduyet]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_pheduyet]
	@idPhanBoVonDeXuat nvarchar(max),
	@nguonVonID int
AS
Begin
	select 
		distinct
		pbvdvct.iID_DuAnID,
		pbvdvct.iID_LoaiCongTrinhID,
		pbvdvct.iID_DuAn_HangMucID,
		case when ctpd.fThanhToanDeXuat is null then pbvdvct.fThanhToan else ctpd.fThanhToanDeXuat end as fThanhToanDeXuat
		into #tmp_duan
	from VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvdvct
	left join VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet ctpd on ctpd.iID_DuAn_HangMucID = pbvdvct.iID_DuAn_HangMucID
	where
		pbvdvct.iId_PhanBoVon_DonVi in (select  * from dbo.splitstring(@idPhanBoVonDeXuat));

	select
		distinct
		null as IdChungTu,
		null as IdChungTuParent,
		cast(1 as bit) as BActive,
		cast(1 as bit) as IsGoc,
		da.iID_DuAnID as iID_DuAnID,
		da.sTenDuAn as sTenDuAn,
		da.sMaDuAn as sMaDuAn,
		da.sTrangThaiDuAn as sTrangThaiDuAn,
		da.sKhoiCong as sKhoiCong,
		da.sKetThuc as sKetThuc,
		lct.sTenLoaiCongTrinh as sTenLoaiCongTrinh,
		da.sMaKetNoi as sMaKetNoi,
		cda.sTen as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		tmp_da.iID_LoaiCongTrinhID as iID_LoaiCongTrinhID,
		da.iID_CapPheDuyetID as iID_CapPheDuyetID,
		'' as sLNS,
		'' as sL,
		'' as sK,
		'' as sM,
		'' as sTM,
		'' as sTTM,
		'' as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		'' as sGhiChu,
		cast(0 as float) as FCapPhatTaiKhoBac,
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		cast(0 as float) as fChiTieuGoc,
		cast(0 as float) as FGiaTriThuHoi,
		cast(tmp_da.fThanhToanDeXuat as float) as FGiaTrPhanBo,
		case
			when ((da.sTrangThaiDuAn = N'THUC_HIEN') and (da.bIsKetThuc IS NULL)) then 2 else 1
		end ILoaiDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		dv.iID_MaDonVi as IIdMaDonViThucHienDuAn,
		tmp_da.fThanhToanDeXuat,
		dahm.iID_DuAn_HangMucID
	from
		VDT_DA_DuAn da
	inner join
		#tmp_duan tmp_da
	on da.iID_DuAnID = tmp_da.iID_DuAnID
	left join 
		VDT_DA_DuAn_HangMuc dahm
	on tmp_da.iID_DuAn_HangMucID = dahm.iID_DuAn_HangMucID
	left join 
		VDT_DM_PhanCapDuAn cda 
	on da.iID_CapPheDuyetID = cda.iID_PhanCapID
	left join
		VDT_DM_LoaiCongTrinh lct
	on dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on 
		da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi

    where tmp_da.iID_DuAn_HangMucID is not null

	drop table #tmp_duan

End
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]
	@idPhanBoVonDeXuat nvarchar(max),
	@nguonVonID int
AS
Begin
	select 
		distinct
		pbvdvct.iID_DuAnID,
		pbvdvct.iID_LoaiCongTrinh as iID_LoaiCongTrinh,
		pbvdvct.ILoaiDuAn as ILoaiDuAn,
		pbvdvct.iID_DuAn_HangMucID,
		--case when pbct.fThanhToanDeXuat = null then pbvdvct.fThanhToanDeXuat else pbct.fThanhToanDeXuat end as fThanhToanDeXuat
		pbvdvct.fThanhToanDeXuat as fThanhToanDeXuat
		into #tmp_duan
	from VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvdvct
	--INNER JOIN VDT_DA_DuAn as da on pbvdvct.iID_DuAnID = da.iID_DuAnID
	left join VDT_KHV_PhanBoVon_ChiTiet pbct on pbct.iID_DuAn_HangMucID = pbvdvct.iID_DuAn_HangMucID
	where
		pbvdvct.iID_PhanBoVon_DonVi_PheDuyet_ID in (select  * from dbo.splitstring(@idPhanBoVonDeXuat));

	select
		distinct
		null as IdChungTu,
		null as IdChungTuParent,
		cast(1 as bit) as BActive,
		cast(1 as bit) as IsGoc,
		da.iID_DuAnID as iID_DuAnID,
		da.sTenDuAn as sTenDuAn,
		da.sMaDuAn as sMaDuAn,
		da.sTrangThaiDuAn as sTrangThaiDuAn,
		da.sKhoiCong as sKhoiCong,
		da.sKetThuc as sKetThuc,
		lct.sTenLoaiCongTrinh as sTenLoaiCongTrinh,
		da.sMaKetNoi as sMaKetNoi,
		cda.sTen as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		tmp_da.iID_LoaiCongTrinh as iID_LoaiCongTrinhID,
		da.iID_CapPheDuyetID as iID_CapPheDuyetID,
		lct.LNS as sLNS,
		lct.L as sL,
		lct.K as sK,
		lct.M as sM,
		lct.TM as sTM,
		lct.TTM as sTTM,
		lct.NG as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		'' as sGhiChu,
		cast(0 as float) as FCapPhatTaiKhoBac,
		cast(0 as float) as FCapPhatTaiKhoBacDC,
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FCapPhatBangLenhChiDC,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBacDC,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChiDC,
		cast(0 as float) as fChiTieuGoc,
		tmp_da.ILoaiDuAn as ILoaiDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		dv.iID_MaDonVi as IIdMaDonViThucHienDuAn,
		tmp_da.fThanhToanDeXuat as fThanhToanDeXuat,
		dahm.iID_DuAn_HangMucID
	from
		VDT_DA_DuAn da
	inner join
		#tmp_duan tmp_da
	on da.iID_DuAnID = tmp_da.iID_DuAnID
	left join 
		VDT_DA_DuAn_HangMuc dahm
	on tmp_da.iID_DuAn_HangMucID = dahm.iID_DuAn_HangMucID
	left join 
		VDT_DM_PhanCapDuAn cda 
	on da.iID_CapPheDuyetID = cda.iID_PhanCapID
	left join
		VDT_DM_LoaiCongTrinh lct
	on dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on 
		da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
	drop table #tmp_duan

End
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_kehoachvon_capphatthanhtoan]
	@DuAnId [varchar](max),
	@NguonVonId [int],
	@dNgayDeNghi [date],
	@NamKeHoach [int],
	@iCoQuanThanhToan [int],
	@iIdPheDuyet [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	CREATE TABLE #tmp(
		Id uniqueidentifier, 
		sSoQuyetDinh nvarchar(100), 
		dNgayQuyetDinh datetime,
		iNamKeHoach int,
		iID_NguonVonID int,
		PhanLoai int,
		LenhChi float,
		FTongGiaTri float,
		TenLoai nvarchar(600),
		sMaNguonCha nvarchar(100)
	)

	-- Ke hoach von nam
	
	SELECT Id INTO #tmpChungTuVonNam
	FROM 
	(
		SELECT Id, ROW_NUMBER() OVER(PARTITION BY iID_PhanBoGocID ORDER BY dNgayQuyetDinh DESC, dDateCreate DESC) as rn
		FROM VDT_KHV_PhanBoVon 
		WHERE CAST(dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
			AND iNamKeHoach = @NamKeHoach
			AND iID_NguonVonID = @NguonVonId
	) as tbl 
	WHERE tbl.rn = 1

	INSERT INTO #tmp(Id, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_NguonVonID, FTongGiaTri, sMaNguonCha, TenLoai, PhanLoai)
	SELECT tbl.Id, dt.sSoQuyetDinh, dt.dNgayQuyetDinh, dt.iNamKeHoach, dt.iID_NguonVonID, SUM(ISNULL(dt.fGiaTri, 0)) as fGiaTri,
		dt.sMaNguon, N'Kế hoạch vốn năm', 1
	FROM #tmpChungTuVonNam as tbl
	INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tbl.Id = dt.iID_ChungTu AND dt.iID_DuAnID = @DuAnId AND dt.iID_NguonVonID = @NguonVonId AND dt.bIsLog = 0
										AND (dt.sMaNguon in ('101', '102')  AND dt.sMaDich = '000' AND dt.sMaTienTrinh = '200')
	GROUP BY tbl.Id,dt.sSoQuyetDinh, dt.dNgayQuyetDinh, dt.iNamKeHoach, dt.iID_NguonVonID,dt.sMaNguon

	INSERT INTO #tmp(Id, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_NguonVonID, FTongGiaTri, sMaNguonCha, TenLoai, PhanLoai)
	SELECT tbl.iID_ChungTu, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.iID_NguonVonID, SUM(ISNULL(tbl.fGiaTri, 0)), sMaNguon,
		CASE WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('121a', '122a') THEN N'Kế hoạch vốn ứng'
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('111', '112') THEN N'Kế hoạch năm trước chuyển sang'
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('131', '132') THEN N'Kế hoạch ứng trước năm trước chuyển sang' END, 
		CASE WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('121a', '122a') THEN 2
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('111', '112') THEN 3
			WHEN sMaNguon COLLATE DATABASE_DEFAULT in ('131', '132') THEN 4 END
	FROM VDT_TongHop_NguonNSDauTu as tbl
	WHERE ((tbl.sMaNguon COLLATE DATABASE_DEFAULT in ('121a', '122a') AND sMaTienTrinh COLLATE DATABASE_DEFAULT = '200') OR (tbl.sMaNguon COLLATE DATABASE_DEFAULT in ('111', '112', '131', '132') AND sMaTienTrinh COLLATE DATABASE_DEFAULT = '100'))
		AND sMaDich COLLATE DATABASE_DEFAULT = '000'
		AND bIsLog = 0
		AND tbl.iID_DuAnID = @DuAnId AND tbl.iID_NguonVonID = @NguonVonId 
		AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
		AND tbl.iNamKeHoach = @NamKeHoach
	GROUP BY tbl.iID_ChungTu, tbl.sSoQuyetDinh, tbl.dNgayQuyetDinh, tbl.iNamKeHoach, tbl.iID_NguonVonID, sMaNguon

	
	-- Luy ke da thanh toan
	SELECT tmp.id as iIdChungTu, tmp.sMaNguonCha, 
		SUM(ISNULL(CASE WHEN dt.sMaNguon = '000' THEN ISNULL(dt.fGiaTri, 0) ELSE 0 END, 0)) as fThanhToan,
		SUM(ISNULL(CASE WHEN dt.sMaDich = '000' THEN ISNULL(dt.fGiaTri, 0) ELSE 0 END, 0)) as fThuHoi INTO #tmpThanhToan
	FROM #tmp as tmp
	INNER JOIN VDT_TongHop_NguonNSDauTu as dt on dt.iId_MaNguonCha = tmp.id
												AND tmp.sMaNguonCha COLLATE DATABASE_DEFAULT = dt.sMaNguonCha COLLATE DATABASE_DEFAULT
												AND sMaTienTrinh COLLATE DATABASE_DEFAULT = '200'
												AND dt.iID_DuAnID = @DuAnId
												 AND bIsLog = 0
	WHERE dt.iID_ChungTu <> @iIdPheDuyet 
	GROUP BY tmp.id, tmp.sMaNguonCha


	CREATE TABLE #tmpMaNguon(sMaNguon nvarchar(100))
	IF(@iCoQuanThanhToan = 1)
	BEGIN
		INSERT INTO #tmpMaNguon(sMaNguon)
		VALUES('101'), ('121a'), ('111'), ('131')
		declare @counter int = (select count(Id) from #tmp );
		select ROW_NUMBER() over(order by id) as stt, Id into #tmpSTT1 from #tmp ;
		while(@counter > 0)
		begin
			UPDATE #tmp 
			SET fTongGiaTri =
			-- Kế hoạch vốn năm
			case when sMaNguonCha = '101' then 
			(SELECT top 1 fCapPhatTaiKhoBac from VDT_KHV_PhanBoVon_chitiet khvn_dd where khvn_dd.iID_PhanBoVonID=#tmp.Id)
			-- Kế hoạch vốn ứng
			else (SELECT top 1 fGiaTriUng from VDT_KHV_KeHoachVonUng_ChiTiet khvu_dd where khvu_dd.iID_KeHoachUngID=#tmp.Id) end		
			-- Kế hoạch năm trước chuyển sang (111) và kế hoạch vốn ứng năm trước chuyển sang (131) chưa clear
			WHERE Id = (SELECT top 1 Id FROM #tmpSTT1 where stt = @counter)  and sMaNguonCha in (select * from #tmpMaNguon)
			set @counter = @counter - 1;
		end 
		DROP TABLE #tmpSTT1;
	END
	ELSE
	BEGIN
		INSERT INTO #tmpMaNguon(sMaNguon)
		VALUES('102'), ('122a'), ('112'), ('132')
		set @counter = (select count(Id) from #tmpChungTuVonNam);
		select ROW_NUMBER() over(order by id) as stt, Id into #tmpSTT2 from #tmpChungTuVonNam;
		while(@counter > 0)
		begin
			UPDATE #tmp 
			SET fTongGiaTri =
			-- Kế hoạch vốn năm
			case when sMaNguonCha = '102' then 
			(SELECT top 1 fCapPhatTaiKhoBac from VDT_KHV_PhanBoVon_chitiet khvn_dd where khvn_dd.iID_PhanBoVonID=#tmp.Id)
			-- Kế hoạch vốn ứng
			else (SELECT top 1 fGiaTriUng from VDT_KHV_KeHoachVonUng_ChiTiet khvu_dd where khvu_dd.iID_KeHoachUngID=#tmp.Id) end			
			-- Kế hoạch năm trước chuyển sang (112) và kế hoạch vốn ứng năm trước chuyển sang (132) chưa clear
			WHERE Id = (SELECT top 1 Id FROM #tmpSTT2 where stt = @counter)  and sMaNguonCha in (select * from #tmpMaNguon)
			set @counter = @counter - 1;
		end 
		DROP TABLE #tmpSTT2
	END

	SELECT tmp.Id, 
		tmp.sSoQuyetDinh, 
		tmp.dNgayQuyetDinh, 
		tmp.iNamKeHoach, 
		tmp.iID_NguonVonID, 
		ISNULL(tmp.FTongGiaTri, 0) as FTongGiaTri,
		(ISNULL(dt.fThanhToan, 0) - ISNULL(dt.fThuHoi, 0)) as FLuyKeThanhToan,
		tmp.sMaNguonCha, 
		tmp.TenLoai, 
		tmp.PhanLoai,
		NULL as iID_DonViQuanLyID,
		NULL as iID_MaDonViQuanLy
	FROM #tmp as tmp
	INNER JOIN #tmpMaNguon as tbl on tmp.sMaNguonCha = tbl.sMaNguon
	LEFT JOIN #tmpThanhToan as dt on tmp.Id = dt.iIdChungTu 
		AND dt.sMaNguonCha COLLATE DATABASE_DEFAULT = tmp.sMaNguonCha COLLATE DATABASE_DEFAULT
	--WHERE (ISNULL(tmp.FTongGiaTri, 0) - ISNULL(dt.fThanhToan, 0) + ISNULL(dt.fThuHoi, 0)) != 0

	DROP TABLE #tmpMaNguon
	DROP TABLE #tmpThanhToan
	DROP TABLE #tmp
	DROP TABLE #tmpChungTuVonNam
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_khoi_tao_dulieu_chitiet]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_vdt_get_khoi_tao_dulieu_chitiet]
@KhoiTaoId nvarchar(200)
AS
Begin
	
 select 
 NEWID() as Id,
 chitiet.Id as IdDb,
 lct.SMaLoaiCongTrinh,
 chitiet.iID_LoaiCongTrinh as IIdLoaiCongTrinh,
 chitiet.iID_KhoiTaoDuLieuID as IID_KhoiTaoDuLieuID,
 chitiet.iID_DuAnID as IID_DuAnID,
 chitiet.sMaDuAn as SMaDuAn,
 duan.sTenDuAn as TenDuAn,
 chitiet.iID_NguonVonID as IIDNguonVonID,
 chitiet.iCoQuanThanhToan as ICoQuanThanhToan,

 chitiet.fKHVN_VonBoTriHetNamTruoc as FKHVN_VonBoTriHetNamTruoc,
 chitiet.fKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc as FKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc,
 chitiet.fKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi as FKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi,
 chitiet.fKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc as FKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc,
 chitiet.fKHVN_KeHoachVonKeoDaiSangNam as FKHVN_KeHoachVonKeoDaiSangNam,

 chitiet.fKHUT_VonBoTriHetNamTruoc as FKHUT_VonBoTriHetNamTruoc,
 chitiet.fKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc as FKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc,
 chitiet.fKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi as FKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi,
 chitiet.fKHUT_KeHoachUngTruocKeoDaiSangNam as FKHUT_KeHoachUngTruocKeoDaiSangNam,
 chitiet.fKHUT_KeHoachUngTruocChuaThuHoi as FKHUT_KeHoachUngTruocChuaThuHoi

 FROM VDT_KT_KhoiTao_DuLieu_ChiTiet chitiet
 left join VDT_DA_DuAn duan on chitiet.iID_DuAnID = duan.iID_DuAnID
 LEFT JOIN VDT_DM_LoaiCongTrinh as lct on chitiet.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
 where chitiet.iID_KhoiTaoDuLieuID = @KhoiTaoId
 ORDER BY chitiet.iID_DuAnID
	 
End
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_phan_bo_von_dieuchinh]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_phan_bo_von_dieuchinh]
	@idPhanBoVonDonVi nvarchar(max)
AS
BEGIN
	select 
		pbvdvct.Id as Id,
		pbvdvct.iID_DuAnID as IIdDuAn,
		pbvdvct.sMaDuAn as SMaDuAn,
		pbvdvct.fTongMucDauTuDuocDuyet as FTongMucDauTuDuocDuyet,
		pbvdvct.fLuyKeVonNamTruoc as FLuyKeVonNamTruoc,
		pbvdvct.fVonKeoDaiCacNamTruoc as FVonKeoDaiCacNamTruoc,
		pbvdvct.fUocThucHien as FUocThucHien,
		pbvdvct.fThuHoiVonUngTruoc as FThuHoiVonUngTruoc,
		pbvdvct.fThanhToan as FThanhToan,
		pbvdvct.sTrangThaiDuAnDangKy as STrangThaiDuAnDangKy,
		pbvdvct.iID_ParentId as IdParent,
		pbvdvct.ILoaiDuAn as ILoaiDuAn,
		pbvdvct.iID_LoaiCongTrinhID as IdLoaiCongTrinh,
		pbvdvct.iID_DuAn_HangMucID
	from
		VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvdvct
	inner join
		VDT_KHV_PhanBoVon_ChiTiet pbvct
	on 
		pbvdvct.iID_DuAnID = pbvct.iID_DuAnID
		and pbvdvct.iID_LoaiCongTrinhID = pbvct.iID_LoaiCongTrinh
	inner join
		VDT_KHV_PhanBoVon_DonVi pbvdv
	on 
		pbvdv.Id = pbvdvct.iId_PhanBoVon_DonVi
	inner join
		VDT_KHV_PhanBoVon pbv
	on
		pbv.Id = pbvct.iID_PhanBoVonID
		and pbv.iNamKeHoach = pbvdv.iNamKeHoach
		and pbv.bActive = 1
	where
		pbvdvct.iId_PhanBoVon_DonVi = @idPhanBoVonDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_dutoanchitiet_danhsach]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_getall_dutoanchitiet_danhsach]
	@duToanId nvarchar(500),
	@duAnChiPhiId uniqueidentifier 
	
AS
BEGIN
	;WITH  HangMucTreeCTE
AS
(
		select 
		hm.Id as Id,
		dthm.iID_DuToan_HangMuciID as IdDuToanHangMuc,
		hm.sMaHangMuc as MaHangMuc,
		hm.sTenHangMuc as TenHangMuc,
		dthm.iID_ChiPhiID as  IdChiPhi,
		dthm.iID_DuAn_ChiPhi as  IdDuAnChiPhi,
		hm.Id as  IdDuAnHangMuc,
		dthm.iID_DuToanID as  IIdDuToanId,
		ISNULL(dthm.fTienPheDuyet, 0 ) as GiaTriPheDuyet,
		ISNULL(dthm.fTienPheDuyetQDDT, 0 ) as GiaTriPheDuyetQDDT,
		hm.iID_ParentID as HangMucParentId,
		CAST(1 as bit) as IsHangMucOld,
		hm.maOrder,
		--CAST(([sMaHangMuc]) AS VARCHAR(MAX)) AS MaOrDer,
		null as TenChiPhi,
		hm.IdLoaiCongTrinh 
		from VDT_DA_DuToan_DM_HangMuc hm
		inner join VDT_DA_DuToan_HangMuc dthm ON dthm.iID_HangMucID = hm.Id AND dthm.iID_DuToanID
		in (
			select * FROM dbo.f_split(@duToanId)
		) AND  dthm.iID_DuAn_ChiPhi = @duAnChiPhiId
		
		where   hm.iID_ParentID is null 

		UNION ALL

		select 
		hm2.Id as Id,
		dthm2.iID_DuToan_HangMuciID as IdDuToanHangMuc,
		hm2.sMaHangMuc as MaHangMuc,
		hm2.sTenHangMuc as TenHangMuc,
		dthm2.iID_ChiPhiID as  IdChiPhi,
		dthm2.iID_DuAn_ChiPhi as  IdDuAnChiPhi,
		hm2.Id as  IdDuAnHangMuc,
		dthm2.iID_DuToanID as  IIdDuToanId,
		ISNULL(dthm2.fTienPheDuyet, 0 ) as GiaTriPheDuyet,
		ISNULL(dthm2.fTienPheDuyetQDDT, 0 ) as GiaTriPheDuyetQDDT,
		hm2.iID_ParentID as HangMucParentId,
		CAST(1 as bit) as IsHangMucOld,
		hm2.maOrder,
		--CAST((M.MaOrDer + '-' + hm2.sMaHangMuc) AS VARCHAR(MAX)) AS MaOrDer,
		null as TenChiPhi,
		hm2.IdLoaiCongTrinh 
		from VDT_DA_DuToan_DM_HangMuc hm2
		inner join VDT_DA_DuToan_HangMuc dthm2 ON dthm2.iID_HangMucID = hm2.Id AND dthm2.iID_DuToanID 
		in (
			select * FROM dbo.f_split(@duToanId)
		)
		AND  dthm2.iID_DuAn_ChiPhi = @duAnChiPhiId
		
				INNER JOIN HangMucTreeCTE as M ON hm2.iID_ParentID = M.Id
		
)

select tbl.*,
	null as FGiaTriDieuChinh,
	null as GiaTriTruocDieuChinh,
	null as FTienPheDuyetQDDT,
	cast(0 AS float) AS FTienChenhLech,
	lct.sTenLoaiCongTrinh as TenLoaiCT,
	isnull(cast(case when parentId.iID_ParentID is not null or tbl.HangMucParentId is null then 1 else 0 end as bit),0)  as IsHangCha
from HangMucTreeCTE tbl
	left join VDT_DM_LoaiCongTrinh lct ON lct.iID_LoaiCongTrinh = tbl.IdLoaiCongTrinh
	left join
		(
			select distinct iID_ParentID from VDT_DA_DuToan_DM_HangMuc tb1
			inner join VDT_DA_DuToan_HangMuc tb2 ON tb1.Id = tb2.iID_HangMucID AND tb2.iID_DuToanID 
			in (
			select * FROM dbo.f_split(@duToanId)
			)
			where  tb1.iID_ParentID is not null )as parentId ON parentId.iID_ParentID = tbl.Id
order by MaOrDer

END;
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_list_goithauhangmuc]    Script Date: 27/11/2021 2:27:38 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovonchitiet]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_insert_phanbovonchitiet]
@bIsEdit bit,
@sUserLogin nvarchar(100),
@tbl_PhanBoVonChiTiet t_tbl_phanbovonchitiet5 READONLY,
@sTypeError int OUTPUT
AS
BEGIN
	set @sTypeError = 0
	DECLARE @iIdPhanBoVon uniqueidentifier = (SELECT TOP(1) iID_PhanBoVonID FROM @tbl_PhanBoVonChiTiet)
	DECLARE @iNamKeHoach int = (SELECT TOP(1)iNamKeHoach FROM VDT_KHV_PhanBoVon WHERE id = @iIdPhanBoVon)
	--DECLARE @lstIdParent nvarchar(max);

	DECLARE @iCountError int =  (SELECT COUNT(*)
									FROM @tbl_PhanBoVonChiTiet as tbl
									LEFT JOIN NS_MucLucNganSach as ml on ml.sLNS = tbl.sLNS
																	AND ISNULL(tbl.sL, '') = ml.sL 
																	AND ISNULL(tbl.sK, '') = ml.sK
																	AND ISNULL(tbl.sM, '')= ml.sM 
																	AND ISNULL(tbl.sTM, '') = ml.sTM 
																	AND ISNULL(tbl.sTTM, '') = ml.sTTM
																	AND ISNULL(tbl.sNG, '') = ml.sNG 
																	AND ml.iNamLamViec = @iNamKeHoach
									WHERE ml.iID  IS NULL)

	IF(@iCountError <> 0) 
	BEGIN
		SET @sTypeError = 1
		RETURN
	END

	IF(@bIsEdit = 1)
	BEGIN 
		DELETE VDT_KHV_PhanBoVon_ChiTiet WHERE iID_PhanBoVonID = @iIdPhanBoVon
		--select @lstIdParent = (cast(pbvct1.iId_Parent as nvarchar(1000)) + ',') from VDT_KHV_PhanBoVon_ChiTiet pbvct1 WHERE iID_PhanBoVonID = @iIdPhanBoVon and pbvct1.iId_Parent is not null
		--delete VDT_KHV_PhanBoVon_ChiTiet where iID_PhanBoVonID in (select * from dbo.splitstring(@lstIdParent))
	END

	INSERT INTO VDT_KHV_PhanBoVon_ChiTiet(Id, iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, sTrangThaiDuAnDangKy, fGiaTrDeNghi, fGiaTrPhanBo, fGiaTriThuHoi, 
											--new--
											fCapPhatTaiKhoBac, fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac, fGiaTriThuHoiNamTruocLenhChi, fThanhToanDeXuat,
											iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu, LNS, L, K, M, TM, TTM, NG, fCapPhatTaiKhoBacDc, fCapPhatBangLenhChiDc, fGiaTriThuHoiNamTruocKhoBacDc,
											fGiaTriThuHoiNamTruocLenhChiDc, iId_Parent, bActive, ILoaiDuAn, iID_LoaiCongTrinh, iID_DuAn_HangMucID)
	SELECT NEWID(), iID_PhanBoVonID, iID_DuAnID, 
			(CASE WHEN ISNULL(tbl.sTM, '') = '' AND ISNULL(tbl.sTTM, '') = '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as sM, 
			(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') = '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as sTM,
			(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') <> '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as sTTM,
			(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') <> '' AND ISNULL(tbl.sNG, '') <> '' THEN ml.iID ELSE NULL END) as sNG,
			sTrangThaiDuAnDangKy, fGiaTrDeNghi, fGiaTrPhanBo, fGiaTriThuHoi, 
			--new--
			fCapPhatTaiKhoBac, fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac, fGiaTriThuHoiNamTruocLenhChi, fThanhToanDeXuat,
			iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu,
			tbl.sLNS, tbl.sL, tbl.sK, tbl.sM, tbl.sTM, tbl.sTTM, tbl.sNG,
			tbl.fCapPhatTaiKhoBacDc, tbl.fCapPhatBangLenhChiDc, tbl.fGiaTriThuHoiNamTruocKhoBacDc, tbl.fGiaTriThuHoiNamTruocLenhChiDc, tbl.iID_Parent, 1, tbl.ILoaiDuAn, tbl.IIdLoaiCongTrinh, tbl.IID_DuAn_HangMucID
	FROM @tbl_PhanBoVonChiTiet as tbl
	INNER JOIN NS_MucLucNganSach as ml on ml.sLNS = tbl.sLNS 
									AND ISNULL(tbl.sL, '') = ml.sL 
									AND ISNULL(tbl.sK, '') = ml.sK
									AND ISNULL(tbl.sM, '')= ml.sM 
									AND ISNULL(tbl.sTM, '') = ml.sTM 
									AND ISNULL(tbl.sTTM, '') = ml.sTTM 
									AND ISNULL(tbl.sNG, '') = ml.sNG 
									AND ml.iNamLamViec = @iNamKeHoach
									
	DECLARE @fGiaTriDeNghi float = (SELECT SUM(fGiaTrDeNghi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoi float = (SELECT SUM(fGiaTriThuHoi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTrPhanBo float = (SELECT SUM(fGiaTrPhanBo) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatTaiKhoBac float = (SELECT SUM(fCapPhatTaiKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatBangLenhChi float = (SELECT SUM(fCapPhatBangLenhChi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocKhoBac float = (SELECT SUM(fGiaTriThuHoiNamTruocKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocLenhChi float = (SELECT SUM(fGiaTriThuHoiNamTruocLenhChi) FROM @tbl_PhanBoVonChiTiet)

	SELECT @fGiaTriDeNghi = (@fGiaTriDeNghi + ISNULL(fGiaTrDeNghi, 0)),
			@fGiaTriThuHoi = (@fGiaTriThuHoi + ISNULL(fGiaTriThuHoi, 0)),
			@fGiaTrPhanBo = (@fGiaTrPhanBo + ISNULL(fGiaTrPhanBo, 0)),
			@fCapPhatTaiKhoBac = (@fCapPhatTaiKhoBac + ISNULL(fCapPhatTaiKhoBac, 0)),
			@fCapPhatBangLenhChi = (@fCapPhatBangLenhChi + ISNULL(fCapPhatBangLenhChi, 0)),
			@fGiaTriThuHoiNamTruocKhoBac = (@fGiaTriThuHoiNamTruocKhoBac + ISNULL(fGiaTriThuHoiNamTruocKhoBac, 0)),
			@fGiaTriThuHoiNamTruocLenhChi = (@fGiaTriThuHoiNamTruocLenhChi + ISNULL(fGiaTriThuHoiNamTruocLenhChi, 0))

	FROM VDT_KHV_PhanBoVon 
	WHERE Id = (SELECT iID_ParentId FROM VDT_KHV_PhanBoVon WHERE Id = @iIdPhanBoVon)

	UPDATE VDT_KHV_PhanBoVon
	set 
	fGiaTrDeNghi = @fGiaTriDeNghi,
	fGiaTriThuHoi = @fGiaTriThuHoi,
	fGiaTrPhanBo = @fGiaTrPhanBo,
	fCapPhatTaiKhoBac = @fCapPhatTaiKhoBac,
	fCapPhatBangLenhChi = @fCapPhatBangLenhChi,
	fGiaTriThuHoiNamTruocKhoBac = @fGiaTriThuHoiNamTruocKhoBac,
	fGiaTriThuHoiNamTruocLenhChi = @fGiaTriThuHoiNamTruocLenhChi
	WHERE Id = @iIdPhanBoVon

	UPDATE da
	SET
		sTrangThaiDuAn = 'THUC_HIEN'
	FROM @tbl_PhanBoVonChiTiet as tbl
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID
	WHERE da.sTrangThaiDuAn = 'KhoiTao'
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_insert_phanbovondonvichitietpheduyet2]
@bIsEdit bit,
@sUserLogin nvarchar(100),
@tbl_PhanBoVonChiTiet t_tbl_phanbovonchitiet6 READONLY,
@sTypeError int OUTPUT
AS
BEGIN
	set @sTypeError = 0
	DECLARE @iIdPhanBoVon uniqueidentifier = (SELECT TOP(1) iID_PhanBoVonID FROM @tbl_PhanBoVonChiTiet)
	DECLARE @iNamKeHoach int = (SELECT TOP(1)iNamKeHoach FROM VDT_KHV_PhanBoVon_DonVi_PheDuyet WHERE id = @iIdPhanBoVon)
	--DECLARE @lstIdParent nvarchar(max);

	--DECLARE @iCountError int =  (SELECT COUNT(*)
	--								FROM @tbl_PhanBoVonChiTiet as tbl
	--								LEFT JOIN NS_MucLucNganSach as ml on ml.sLNS = tbl.sLNS
	--																AND ISNULL(tbl.sL, '') = ml.sL 
	--																AND ISNULL(tbl.sK, '') = ml.sK
	--																AND ISNULL(tbl.sM, '')= ml.sM 
	--																AND ISNULL(tbl.sTM, '') = ml.sTM 
	--																AND ISNULL(tbl.sTTM, '') = ml.sTTM
	--																AND ISNULL(tbl.sNG, '') = ml.sNG 
	--																AND ml.iNamLamViec = @iNamKeHoach
	--								WHERE ml.iID  IS NULL)

	--IF(@iCountError <> 0) 
	--BEGIN
	--	SET @sTypeError = 1
	--	RETURN
	--END

	IF(@bIsEdit = 1)
	BEGIN 
		DELETE VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet WHERE iID_PhanBoVon_DonVi_PheDuyet_ID = @iIdPhanBoVon
		--select @lstIdParent = (cast(pbvct1.iId_Parent as nvarchar(1000)) + ',') from VDT_KHV_PhanBoVon_ChiTiet pbvct1 WHERE iID_PhanBoVonID = @iIdPhanBoVon and pbvct1.iId_Parent is not null
		--delete VDT_KHV_PhanBoVon_ChiTiet where iID_PhanBoVonID in (select * from dbo.splitstring(@lstIdParent))
	END

	INSERT INTO VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet(Id, iID_PhanBoVon_DonVi_PheDuyet_ID, iID_DuAnID, fGiaTrPhanBo, fGiaTriThuHoi, 
											--new--
											iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu, fThanhToanDeXuat,
											 iId_Parent, bActive, ILoaiDuAn, iID_LoaiCongTrinh, iID_DuAn_HangMucID)
	SELECT NEWID(), iID_PhanBoVonID, iID_DuAnID, fGiaTrPhanBo, fGiaTriThuHoi, 
			--new--
			iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu, fThanhToanDeXuat, tbl.iID_Parent, 1, tbl.ILoaiDuAn, tbl.IIdLoaiCongTrinh, tbl.IID_DuAn_HangMucID
	FROM @tbl_PhanBoVonChiTiet as tbl
									
	DECLARE @fGiaTriDeNghi float = (SELECT SUM(fGiaTrDeNghi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoi float = (SELECT SUM(fGiaTriThuHoi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTrPhanBo float = (SELECT SUM(fGiaTrPhanBo) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatTaiKhoBac float = (SELECT SUM(fCapPhatTaiKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatBangLenhChi float = (SELECT SUM(fCapPhatBangLenhChi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocKhoBac float = (SELECT SUM(fGiaTriThuHoiNamTruocKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocLenhChi float = (SELECT SUM(fGiaTriThuHoiNamTruocLenhChi) FROM @tbl_PhanBoVonChiTiet)

	SELECT  @fGiaTriThuHoi = (@fGiaTriThuHoi + ISNULL(fGiaTriThuHoi, 0)),
			@fGiaTrPhanBo = (@fGiaTrPhanBo + ISNULL(fGiaTrPhanBo, 0))
	FROM VDT_KHV_PhanBoVon_DonVi_PheDuyet 
	WHERE Id = (SELECT iID_ParentId FROM VDT_KHV_PhanBoVon_DonVi_PheDuyet WHERE Id = @iIdPhanBoVon)

	UPDATE VDT_KHV_PhanBoVon_DonVi_PheDuyet
	set 
	fGiaTriThuHoi = @fGiaTriThuHoi,
	fGiaTrPhanBo = @fGiaTrPhanBo
	WHERE Id = @iIdPhanBoVon

	UPDATE da
	SET
		sTrangThaiDuAn = 'THUC_HIEN'
	FROM @tbl_PhanBoVonChiTiet as tbl
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID
	WHERE da.sTrangThaiDuAn = 'KhoiTao'
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_chitiet_chooseduan_test]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_kehoach5nam_chitiet_chooseduan_test]
	--@iID_DuAn nvarchar(50)
AS
BEGIN
	SELECT tbl_sum.* FROM (
		SELECT
			da.sMaDuAn				AS SMaDuAn,
			da.sTenDuAn				AS STenDuAn,
			da.iID_DuAnID			AS IIdDuAnId,
			da.sKhoiCong			AS SKhoiCong,
			da.sKetThuc				AS SKetThuc,
			da.sMaKetNoi			AS SMaKetNoi,
			da.iID_ChuDauTuID		AS IIdChuDauTuId,
			da.iID_MaChuDauTuID		AS IIdMaChuDauTu,
			cdt.sTenDonVi			AS STenChuDauTu,
			da.sDiaDiem				AS SDiaDiem,
			danv.fThanhTien			AS FHanMucDauTu,
			da.iID_LoaiCongTrinhID	AS IIdLoaiCongTrinhId,
			lct.sTenLoaiCongTrinh	AS STenLoaiCongTrinh,
			--(CASE 
			--			WHEN (da.iID_LoaiCongTrinhID is null) THEN null						
			--			ELSE danv.iID_NguonVonID
			--		END) as IIdNguonVonId,
			danv.iID_NguonVonID		AS IIdNguonVonId,
			--(CASE 
			--			WHEN (da.iID_LoaiCongTrinhID is null) THEN null						
			--			ELSE nns.sTen
			--		END) as STenNguonVon,
			nns.sTen				AS STenNguonVon,
			NULL					AS IdDuAnNguonVon,
			--''						AS siID_NguonVonID,
			NULL					AS IIdKeHoach5NamChiTietId,
			NULL					AS IIdKeHoach5NamId,
			CAST(0 AS float)		AS FGiaTriNamThuNhat,
			CAST(0 AS float)		AS FGiaTriNamThuHai,
			CAST(0 AS float)		AS FGiaTriNamThuBa,
			CAST(0 AS float)		AS FGiaTriNamThuTu,
			CAST(0 AS float)		AS FGiaTriNamThuNam,
			CAST(0 AS float)		AS FGiaTriKeHoach,
			CAST(0 AS float)		AS FGiaTriSau5Nam,
			''						AS SGhiChu,
			dv.iID_MaDonVi			AS IIdMaDonVi,
			dv.iID_DonVi			AS IIdDonViId,
			dv.sTenDonVi			AS STenDonVi,
			NULL					AS IIdDuAnHangMucId,
			CAST(0 AS bit)			AS IsDuAnHangMuc,
			NULL					AS IIdParentHangMuc,
			''						AS SMaOrder,
			0						AS Loai,
			NULL					AS IIdLoaiDuAnId,
			da.Id_DuAnKhthDeXuat	AS IdDuAnKhthDeXuat,
			--NULL					AS IIddDuAn_HangMucId,
			da.dDateCreate			AS DDateCreate,
			1						AS ILoaiDuAn
		FROM VDT_DA_DuAn da
		LEFT JOIN DM_ChuDauTu cdt
			ON da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_DonViThucHienDuAn dv
			ON da.iID_DonViThucHienDuAnID = dv.iID_DonVi
		LEFT JOIN VDT_DM_LoaiCongTrinh lct
			ON da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh
		LEFT JOIN VDT_DA_NguonVon danv
			ON da.iID_DuAnID = danv.iID_DuAn
		LEFT JOIN NguonNganSach nns
			ON danv.iID_NguonVonID = nns.iID_MaNguonNganSach
		LEFT JOIN VDT_KHV_KeHoach5Nam_ChiTiet ctct
			ON da.iID_DuAnID = ctct.iID_DuAnID AND danv.iID_NguonVonID = ctct.iID_NguonVonID AND da.iID_LoaiCongTrinhID = ctct.iID_LoaiCongTrinhID
		WHERE 
			da.iID_DuAnID IS NOT NULL 
			AND ctct.iID_KeHoach5Nam_ChiTietID IS NULL
			

		UNION ALL

		SELECT
			da.sMaDuAn				AS SMaDuAn,
			dahm.sTenHangMuc		AS STenDuAn,
			da.iID_DuAnID			AS IIdDuAnId,
			da.sKhoiCong			AS SKhoiCong,
			da.sKetThuc				AS SKetThuc,
			da.sMaKetNoi			AS SMaKetNoi,
			da.iID_ChuDauTuID		AS IIdChuDauTuId,
			da.iID_MaChuDauTuID		AS IIdMaChuDauTu,
			cdt.sTenDonVi			AS STenChuDauTu,
			da.sDiaDiem				AS SDiaDiem,
			dahm.fHanMucDauTu		AS FHanMucDauTu,
			dahm.IdLoaiCongTrinh	AS IIdLoaiCongTrinhId,
			lct.sTenLoaiCongTrinh	AS STenLoaiCongTrinh,
			dahm.iID_NguonVonID		AS IIdNguonVonId,
			nns.sTen				AS STenNguonVon,
			danv.Id					AS IdDuAnNguonVon,
			--''						AS siID_NguonVonID,
			NULL					AS IIdKeHoach5NamChiTietId,
			NULL					AS IIdKeHoach5NamId,
			CAST(0 AS float)		AS FGiaTriNamThuNhat,
			CAST(0 AS float)		AS FGiaTriNamThuHai,
			CAST(0 AS float)		AS FGiaTriNamThuBa,
			CAST(0 AS float)		AS FGiaTriNamThuTu,
			CAST(0 AS float)		AS FGiaTriNamThuNam,
			CAST(0 AS float)		AS FGiaTriKeHoach,
			CAST(0 AS float)		AS FGiaTriSau5Nam,
			''						AS SGhiChu,
			dv.iID_MaDonVi			AS IIdMaDonVi,
			dv.iID_DonVi			AS IIdDonViId,
			dv.sTenDonVi			AS STenDonVi,
			dahm.iID_DuAn_HangMucID AS IIdDuAnHangMucId,
			CAST(0 AS bit)			AS IsDuAnHangMuc,
			NULL					AS IIdParentHangMuc,
			''						AS SMaOrder,
			0						AS Loai,
			NULL					AS IIdLoaiDuAnId,
			da.Id_DuAnKhthDeXuat	AS IdDuAnKhthDeXuat,
			--dahm.iID_DuAn_HangMucID AS IIddDuAnHangMucId,
			da.dDateCreate			AS DDateCreate,
			2						AS ILoaiDuAn
		FROM VDT_DA_DuAn_HangMuc dahm
		LEFT JOIN VDT_DA_DuAn da
			ON dahm.iID_DuAnID = da.iID_DuAnID
		LEFT JOIN DM_ChuDauTu cdt
			ON da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_LoaiCongTrinh lct
			ON dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
		LEFT JOIN NguonNganSach nns
			ON dahm.iID_NguonVonID = nns.iID_MaNguonNganSach
		LEFT JOIN VDT_DA_NguonVon danv
			ON dahm.iID_NguonVonID = danv.iID_NguonVonID and danv.iID_DuAn = da.iID_DuAnID
		LEFT JOIN VDT_DM_DonViThucHienDuAn dv
			ON da.iID_DonViThucHienDuAnID = dv.iID_DonVi
		WHERE da.iID_LoaiCongTrinhID is null
	) AS tbl_sum
	LEFT JOIN VDT_KHV_KeHoach5Nam_ChiTiet ctct
	ON tbl_sum.IIdDuAnId = ctct.iID_DuAnID AND tbl_sum.IIdNguonVonId = ctct.iID_NguonVonID AND tbl_sum.IIdLoaiCongTrinhId = ctct.iID_LoaiCongTrinhID
	WHERE
		--tbl_sum.IIdDuAnId in (select * from dbo.splitstring(@iID_DuAn))
		tbl_sum.IIdDuAnId IS NOT NULL 
		AND ctct.iID_KeHoach5Nam_ChiTietID IS NULL
END
/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan]    Script Date: 08/12/2021 9:09:21 AM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_kehoach5nam_chitiet_chuyentiep_report]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 22/10/2021
-- Description:	Kế hoạch 5 năm chi tiết dự án chuyển tiếp
-- =============================================
CREATE PROCEDURE [dbo].[sp_vdt_kehoach5nam_chitiet_chuyentiep_report]
	@lstId nvarchar(2000),
	@lstBudget nvarchar(2000),
	@lstUnit nvarchar(2000),
	@type int,
	@DonViTienTe float
AS
BEGIN
	declare @yearPlan int;

	select @yearPlan = NamLamViec from VDT_KHV_KeHoach5Nam where iID_KeHoach5NamID in (select TOP 1 * from dbo.splitstring(@lstId))

	if(@type = 1)
	begin
		select
			null as IIdMaDonVi,
			khnct.sTen as STenDuAn,
			(cast(da.sKhoiCong as nvarchar(max)) + '-' + cast(da.sKetThuc as nvarchar(max))) as STienDoThucHien,
			qddt.sSoQuyetDinh as SSoQuyetDinh,
			qddt.dNgayQuyetDinh as DNgayQuyetDinh,
			isnull(qddt.fTongMucDauTuPheDuyet, 0)/@DonViTienTe as TongMucDauTu,
			isnull(qddtnv.fTienPheDuyet, 0)/@DonViTienTe as TongMucDauTuNSQP,
			(
				select
					SUM(qddtcp.fTienPheDuyet)/@DonViTienTe
				from
					VDT_DA_QDDauTu_ChiPhi qddtcp
				where qddtcp.iID_QDDauTuID = qddt.iID_QDDauTuID
			) as ChiPhiDuPhong,
			(
				select
					(isnull(SUM(pbvct.fCapPhatTaiKhoBac), 0) + isnull(SUM(pbvct.fCapPhatBangLenhChi), 0))/@DonViTienTe as VonBoTriHetNam
				from
					VDT_KHV_PHanBoVon_ChiTiet pbvct
				inner join
					VDT_KHV_PhanBoVon pbv
				on
					pbvct.iID_PhanBoVonID = pbv.Id
				where
					pbvct.iID_DuAnID = khnct.iID_DuAnID
					and pbv.iNamKeHoach <= khn.iGiaiDoanTu

			) as VonBoTriHetNam,
			(
				select
					(isnull(SUM(pbvct.fCapPhatTaiKhoBac), 0) + isnull(SUM(pbvct.fCapPhatBangLenhChi), 0))/@DonViTienTe as VonDaBoTriNam
				from
					VDT_KHV_PHanBoVon_ChiTiet pbvct
				inner join
					VDT_KHV_PhanBoVon pbv
				on
					pbvct.iID_PhanBoVonID = pbv.Id
				where
					pbvct.iID_DuAnID = khnct.iID_DuAnID
					and pbv.iNamKeHoach = khn.iGiaiDoanTu
			) as VonDaBoTriNam,
			cast(0 as float) as TongSo,
			cast(0 as float) as TongMucDauTuPhaiBoTri,
			khnct.fVonBoTriTuNamDenNam/@DonViTienTe as KeHoachVonNamDoDuyet,
			(
				select 
					(isnull(SUM(khndxct.fGiaTriNamThuNhat), 0) + isnull(SUM(khndxct.fGiaTriNamThuHai),0) 
					+ isnull(SUM(khndxct.fGiaTriNamThuBa), 0) + isnull(SUM(khndxct.fGiaTriNamThuTu), 0) + isnull(SUM(khndxct.fGiaTriNamThuNam), 0))/@DonViTienTe
				from 
					VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet khndxct
				inner join
					VDT_KHV_KeHoach5Nam_DeXuat khndx
				on
					khndxct.iID_KeHoach5NamID = khndx.Id
				where
					khndxct.iID_DuAnID = khnct.iID_DuAnID
					and khndx.iGiaiDoanTu = khn.iGiaiDoanTu
					and khndx.iGiaiDoanDen = khn.iGiaiDoanDen
			) as KeHoachVonDeNghiBoTriNam,

			cast(0 as float) as ChenhLechSoVoiQuyetDinhBo,
			'' as SGhiChu,
			cast(0 as bit) as IsHangCha,
			khnct.iID_DuAnID,
			4 as Loai
		from
			VDT_KHV_KeHoach5Nam_ChiTiet khnct
		inner join
			VDT_KHV_KeHoach5Nam khn
		on
			khnct.iID_KeHoach5NamID = khn.iID_KeHoach5NamID
		left join
			VDT_DA_DuAn da
		on
			khnct.iID_DuAnID = da.iID_DuAnID
		left join
			VDT_DA_QDDauTu qddt
		on 
			khnct.iID_DuAnID = qddt.iID_DuAnID
		left join
			VDT_DA_QDDauTu_NguonVon qddtnv
		on 
			qddt.iID_QDDauTuID = qddtnv.iID_QDDauTuID and qddtnv.iID_NguonVonID = 1
		where 
			khnct.iID_KeHoach5NamID in (select * from dbo.splitstring(@lstId))
			and khnct.iID_NguonVonID in (select * from dbo.splitstring(@lstBudget))
	end
	else if(@type = 2)
	begin
		select
			dv.iID_MaDonVi as IIdMaDonVi,
			dv.sTenDonVi as STenDuAn,
			'' as STienDoThucHien,
			'' as SSoQuyetDinh,
			null as DNgayQuyetDinh,
			cast(0 as float) as TongMucDauTu,
			cast(0 as float) as TongMucDauTuNSQP,
			cast(0 as float) as ChiPhiDuPhong,
			cast(0 as float) as VonBoTriHetNam,
			cast(0 as float) as VonDaBoTriNam,
			cast(0 as float) as TongSo,
			cast(0 as float) as TongMucDauTuPhaiBoTri,
			cast(0 as float) as KeHoachVonNamDoDuyet,
			cast(0 as float) as KeHoachVonDeNghiBoTriNam,
			cast(0 as float) as ChenhLechSoVoiQuyetDinhBo,
			'' as SGhiChu,
			cast(1 as bit) as IsHangCha,
			null as iID_DuAnID,
			3 as Loai
		from
			DonVi dv
		where 
			dv.iID_MaDonVi in (select * from dbo.splitstring(@lstUnit)) and dv.iNamLamViec = @yearPlan

		union all

		select
			khnct.iID_MaDonVi as IIdMaDonVi,
			khnct.sTen as STenDuAn,
			(cast(da.sKhoiCong as nvarchar(max)) + '-' + cast(da.sKetThuc as nvarchar(max))) as STienDoThucHien,
			qddt.sSoQuyetDinh as SSoQuyetDinh,
			qddt.dNgayQuyetDinh as DNgayQuyetDinh,
			isnull(qddt.fTongMucDauTuPheDuyet, 0)/@DonViTienTe as TongMucDauTu,
			isnull(qddtnv.fTienPheDuyet, 0)/@DonViTienTe as TongMucDauTuNSQP,
			(
				select
					SUM(qddtcp.fTienPheDuyet)/@DonViTienTe
				from
					VDT_DA_QDDauTu_ChiPhi qddtcp
				where qddtcp.iID_QDDauTuID = qddt.iID_QDDauTuID
			) as ChiPhiDuPhong,
			(
				select
					(isnull(SUM(pbvct.fCapPhatTaiKhoBac), 0) + isnull(SUM(pbvct.fCapPhatBangLenhChi), 0))/@DonViTienTe as VonBoTriHetNam
				from
					VDT_KHV_PHanBoVon_ChiTiet pbvct
				inner join
					VDT_KHV_PhanBoVon pbv
				on
					pbvct.iID_PhanBoVonID = pbv.Id
				where
					pbvct.iID_DuAnID = khnct.iID_DuAnID
					and pbv.iNamKeHoach <= khn.iGiaiDoanTu

			) as VonBoTriHetNam,
			(
				select
					(isnull(SUM(pbvct.fCapPhatTaiKhoBac), 0) + isnull(SUM(pbvct.fCapPhatBangLenhChi), 0))/@DonViTienTe as VonDaBoTriNam
				from
					VDT_KHV_PHanBoVon_ChiTiet pbvct
				inner join
					VDT_KHV_PhanBoVon pbv
				on
					pbvct.iID_PhanBoVonID = pbv.Id
				where
					pbvct.iID_DuAnID = khnct.iID_DuAnID
					and pbv.iNamKeHoach = khn.iGiaiDoanTu
			) as VonDaBoTriNam,
			cast(0 as float) as TongSo,
			cast(0 as float) as TongMucDauTuPhaiBoTri,
			khnct.fVonBoTriTuNamDenNam/@DonViTienTe as KeHoachVonNamDoDuyet,
			(
				select 
					(isnull(SUM(khndxct.fGiaTriNamThuNhat), 0) + isnull(SUM(khndxct.fGiaTriNamThuHai),0) 
					+ isnull(SUM(khndxct.fGiaTriNamThuBa), 0) + isnull(SUM(khndxct.fGiaTriNamThuTu), 0) + isnull(SUM(khndxct.fGiaTriNamThuNam), 0))/@DonViTienTe
				from 
					VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet khndxct
				inner join
					VDT_KHV_KeHoach5Nam_DeXuat khndx
				on
					khndxct.iID_KeHoach5NamID = khndx.Id
				where
					khndxct.iID_DuAnID = khnct.iID_DuAnID
					and khndx.iGiaiDoanTu = khn.iGiaiDoanTu
					and khndx.iGiaiDoanDen = khn.iGiaiDoanDen
			) as KeHoachVonDeNghiBoTriNam,

			cast(0 as float) as ChenhLechSoVoiQuyetDinhBo,
			'' as SGhiChu,
			cast(0 as bit) as IsHangCha,
			khnct.iID_DuAnID,
			4 as Loai
		from
			VDT_KHV_KeHoach5Nam_ChiTiet khnct
		inner join
			VDT_KHV_KeHoach5Nam khn
		on
			khnct.iID_KeHoach5NamID = khn.iID_KeHoach5NamID
		left join
			VDT_DA_DuAn da
		on
			khnct.iID_DuAnID = da.iID_DuAnID
		left join
			VDT_DA_QDDauTu qddt
		on 
			khnct.iID_DuAnID = qddt.iID_DuAnID
		left join
			VDT_DA_QDDauTu_NguonVon qddtnv
		on 
			qddt.iID_QDDauTuID = qddtnv.iID_QDDauTuID and qddtnv.iID_NguonVonID = 1
		where 
			khnct.iID_KeHoach5NamID in (select * from dbo.splitstring(@lstId))
			and khnct.iID_NguonVonID in (select * from dbo.splitstring(@lstBudget))
			and khnct.iID_MaDonVi in (select * from dbo.splitstring(@lstUnit))
	order by IIdMaDonVi, Loai
	end
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khv_kehoach_von_nam_duoc_duyet_export]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_khv_kehoach_von_nam_duoc_duyet_export]
	@lstId [nvarchar](max),
	@lct [nvarchar](max),
	@type [int],
	@loaiDuAn [int],
	@lstDonVi [nvarchar](max),
	@MenhGiaTienTe [float]
WITH EXECUTE AS CALLER
AS
BEGIN
	if(@type = 1)
	begin
		select
			'' as STT,
			lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			lct.iID_Parent as IdLoaiCongTrinhParent,
			lct.sTenLoaiCongTrinh as sTenDuAn,
			'' as sLNS,
			'' as sL,
			'' as sK,
			'' as sM,
			'' as sTM,
			'' as sTTM,
			'' as sNG,
			cast(0 as float) as FCapPhatTaiKhoBac,
			cast(0 as float) as FCapPhatBangLenhChi,
			cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
			cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
			cast(0 as float) as TongSo,
			null as IIdDuAn,

			3 as Loai,
			cast(1 as bit) as IsHangCha,
			case
				when lct.iID_Parent is null then 0 else 1
			end LoaiParent,
			1 as LoaiCongTrinh
		from f_loai_cong_trinh_get_list_childrent(@lct) lct

		union all

		select 
			'' as STT,
			ctct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			null as IdLoaiCongTrinhParent,
			da.sTenDuAn as sTenDuAn,
			ctct.LNS as sLNS,
			ctct.L as sL,
			ctct.K as sK,
			ctct.M as sM,
			ctct.TM as sTM,
			ctct.TTM as sTTM,
			ctct.NG as sNG,
			ctct.FCapPhatTaiKhoBac/@MenhGiaTienTe as FCapPhatTaiKhoBac,
			ctct.FCapPhatBangLenhChi/@MenhGiaTienTe as FCapPhatBangLenhChi,
			ctct.FGiaTriThuHoiNamTruocKhoBac/@MenhGiaTienTe as FGiaTriThuHoiNamTruocKhoBac,
			ctct.FGiaTriThuHoiNamTruocLenhChi/@MenhGiaTienTe as FGiaTriThuHoiNamTruocLenhChi,
			(isnull(ctct.FCapPhatTaiKhoBac, 0) + isnull(ctct.FCapPhatBangLenhChi, 0))/@MenhGiaTienTe as TongSo,
			ctct.iID_DuAnID as IIdDuAn,
			4 as Loai,
			cast(0 as bit) as IsHangCha,
			2 as LoaiParent,
			1 as LoaiCongTrinh
		from 
			VDT_KHV_PhanBoVon_ChiTiet ctct
		left join
			VDT_DA_DuAn da
		on ctct.iID_DuAnID = da.iID_DuAnID
		where 
			ctct.iID_PhanBoVonID in (select * from dbo.splitstring(@lstId))
			and ctct.ILoaiDuAn = @loaiDuAn
			and (ctct.iID_LoaiCongTrinh in ( select * from dbo.splitstring(@lct)) or  ctct.iID_LoaiCongTrinh in (select iID_LoaiCongTrinh from f_loai_cong_trinh_get_list_childrent(@lct)))
		order by iID_LoaiCongTrinh, Loai
	end
	else
	begin
		select
			'' as IdMaDonViQuanLy,
			'' as STT,
			lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			lct.iID_Parent as IdLoaiCongTrinhParent,
			lct.sTenLoaiCongTrinh as sTenDuAn,
			'' as sLNS,
			'' as sL,
			'' as sK,
			'' as sM,
			'' as sTM,
			'' as sTTM,
			'' as sNG,
			cast(0 as float) as FCapPhatTaiKhoBac,
			cast(0 as float) as FCapPhatBangLenhChi,
			cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
			cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
			cast(0 as float) as TongSo,
			null as IIdDuAn,

			3 as Loai,
			cast(1 as bit) as IsHangCha,
			case
				when lct.iID_Parent is null then 0 else 1
			end LoaiParent,
			1 as LoaiCongTrinh
		from f_loai_cong_trinh_get_list_childrent(@lct) lct

		union all
		
		select
			da.iID_MaDonViThucHienDuAnID as IdMaDonViQuanLy,
			'' as STT,
			lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			lct.iID_Parent as IdLoaiCongTrinhParent,
			dv.sTenDonVi as sTenDuAn,
			'' as sLNS,
			'' as sL,
			'' as sK,
			'' as sM,
			'' as sTM,
			'' as sTTM,
			'' as sNG,
			SUM(isnull(pbvct.fCapPhatTaiKhoBac/@MenhGiaTienTe, 0)) as FCapPhatTaiKhoBac,
			SUM(isnull(pbvct.fCapPhatBangLenhChi/@MenhGiaTienTe, 0)) as FCapPhatBangLenhChi,
			SUM(isnull(pbvct.fGiaTriThuHoiNamTruocKhoBac/@MenhGiaTienTe, 0)) as FGiaTriThuHoiNamTruocKhoBac,
			SUM(isnull(pbvct.fGiaTriThuHoiNamTruocLenhChi/@MenhGiaTienTe, 0)) as FGiaTriThuHoiNamTruocLenhChi,
			(SUM(isnull(pbvct.fCapPhatTaiKhoBac/@MenhGiaTienTe, 0)) + SUM(isnull(pbvct.fCapPhatBangLenhChi/@MenhGiaTienTe, 0))) as TongSo,
			null as IIdDuAn,

			3 as Loai,
			cast(1 as bit) as IsHangCha,
			2 as LoaiParent,
			1 as LoaiCongTrinh
		from 
			VDT_KHV_PhanBoVon_ChiTiet pbvct
		inner join
			VDT_DA_DuAn da
		on pbvct.iID_DuAnID = da.iID_DuAnID
		left join
			VDT_DM_LoaiCongTrinh lct
		on pbvct.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
		left join
			VDT_DM_DonViThucHienDuAn dv
		on da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
		where
			pbvct.iID_PhanBoVonID in (select * from dbo.splitstring(@lstId))
			and pbvct.ILoaiDuAn = @loaiDuAn
			and da.iID_MaDonViThucHienDuAnID in (select * from dbo.splitstring(@lstDonVi))
			and (pbvct.iID_LoaiCongTrinh in ( select * from dbo.splitstring(@lct)) or  pbvct.iID_LoaiCongTrinh in (select iID_LoaiCongTrinh from f_loai_cong_trinh_get_list_childrent(@lct)))
		group by da.iID_MaDonViThucHienDuAnID, lct.iID_LoaiCongTrinh, lct.iID_Parent, dv.sTenDonVi

		union all

		select 
			da.iID_MaDonViThucHienDuAnID as IdMaDonViQuanLy,
			'' as STT,
			ctct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			null as IdLoaiCongTrinhParent,
			da.sTenDuAn as sTenDuAn,
			ctct.LNS as sLNS,
			ctct.L as sL,
			ctct.K as sK,
			ctct.M as sM,
			ctct.TM as sTM,
			ctct.TTM as sTTM,
			ctct.NG as sNG,
			SUM(ctct.FCapPhatTaiKhoBac/@MenhGiaTienTe) as FCapPhatTaiKhoBac,
			SUM(ctct.FCapPhatBangLenhChi/@MenhGiaTienTe) as FCapPhatBangLenhChi,
			SUM(ctct.FGiaTriThuHoiNamTruocKhoBac/@MenhGiaTienTe) as FGiaTriThuHoiNamTruocKhoBac,
			SUM(ctct.FGiaTriThuHoiNamTruocLenhChi/@MenhGiaTienTe) as FGiaTriThuHoiNamTruocLenhChi,
			SUM((isnull(ctct.FCapPhatTaiKhoBac, 0) + isnull(ctct.FCapPhatBangLenhChi, 0))/@MenhGiaTienTe) as TongSo,
			ctct.iID_DuAnID as IIdDuAn,
			5 as Loai,
			cast(0 as bit) as IsHangCha,
			3 as LoaiParent,
			1 as LoaiCongTrinh
		from 
			VDT_KHV_PhanBoVon_ChiTiet ctct
		left join
			VDT_DA_DuAn da
		on ctct.iID_DuAnID = da.iID_DuAnID
		where 
			ctct.iID_PhanBoVonID in (select * from dbo.splitstring(@lstId))
			and ctct.ILoaiDuAn = @loaiDuAn
			and da.iID_MaDonViThucHienDuAnID in (select * from dbo.splitstring(@lstDonVi))
			and (ctct.iID_LoaiCongTrinh in ( select * from dbo.splitstring(@lct)) or  ctct.iID_LoaiCongTrinh in (select iID_LoaiCongTrinh from f_loai_cong_trinh_get_list_childrent(@lct)))
        
		-- group nhung ban ghi cung l,k,m,tm,ttm,n
		group by da.iID_MaDonViThucHienDuAnID, ctct.iID_LoaiCongTrinh, ctct.LNS, 	da.sTenDuAn,
			ctct.L ,
			ctct.K ,
			ctct.M ,
			ctct.TM ,
			ctct.TTM ,
			ctct.NG,
			ctct.iID_DuAnID
		order by iID_LoaiCongTrinh, Loai, IdMaDonViQuanLy
	end
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_khvthanhtoan]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_tt_get_khvthanhtoan]
	@iIdDuAnId [uniqueidentifier],
	@iIdNguonVonId [int],
	@dNgayDeNghi [date],
	@iNamKeHoach [int],
	@iCoQuanThanhToan [int],
	@iIdPheDuyet [uniqueidentifier],
	@ID_DuAn_HangMuc [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	CREATE TABLE #tmp(
		IIdKeHoachVonId uniqueidentifier,
		SSoQuyetDinh nvarchar(100),
		INamKeHoach int,
		ILoaiKeHoachVon int,
		ILoaiNamKhv int,
		ICoQuanThanhToan int,
		FGiaTriKHV float,
		ID_DuAn_HangMuc uniqueidentifier
	)

	-- Ke hoach von nam
	BEGIN
		WITH tmp as 
		(
			SELECT tbl.Id, 
				ROW_NUMBER() OVER (PARTITION BY tbl.iID_PhanBoGocID ORDER BY tbl.dNgayQuyetDinh DESC) as rn
			FROM VDT_KHV_PhanBoVon as tbl
			INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
			WHERE dt.iID_DuAnID = @iIdDuAnId
				AND tbl.iID_NguonVonID = @iIdNguonVonId
				AND iNamKeHoach = @iNamKeHoach
				AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
		)
		INSERT INTO #tmp(IIdKeHoachVonId, SSoQuyetDinh, INamKeHoach, ILoaiKeHoachVon, ILoaiNamKhv, ICoQuanThanhToan, FGiaTriKHV, ID_DuAn_HangMuc)
		SELECT tmp.Id , tbl.SSoQuyetDinh, tbl.INamKeHoach, 1, (CASE WHEN tbl.iLoaiDuToan = 2 THEN 1 ELSE 2 END), @iCoQuanThanhToan,
		--SELECT tmp.Id , tbl.SSoQuyetDinh, tbl.INamKeHoach, 1, 2, @iCoQuanThanhToan,
				(CASE @iCoQuanThanhToan WHEN 1 THEN SUM(ISNULL(dt.fCapPhatTaiKhoBac, 0)) ELSE SUM(ISNULL(dt.fCapPhatBangLenhChi, 0)) END) as FGiaTri,
		dt.iID_DuAn_HangMucID
		FROM tmp as tmp
		INNER JOIN VDT_KHV_PhanBoVon as tbl on tmp.Id = tbl.Id
		INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
		WHERE tmp.rn = 1 AND dt.iID_DuAnID = @iIdDuAnId and dt.iID_DuAn_HangMucID = @ID_DuAn_HangMuc
		GROUP BY tmp.Id, tbl.SSoQuyetDinh, tbl.INamKeHoach, tbl.iLoaiDuToan, dt.iID_DuAn_HangMucID
	END

	-- Ke hoach von ung
	BEGIN
		INSERT INTO #tmp(IIdKeHoachVonId, SSoQuyetDinh, INamKeHoach, ILoaiKeHoachVon, ILoaiNamKhv, ICoQuanThanhToan, FGiaTriKHV, ID_DuAn_HangMuc)
		SELECT tbl.Id, tbl.sSoQuyetDinh, tbl.iNamKeHoach, 2 , 2, @iCoQuanThanhToan,
			(CASE WHEN @iCoQuanThanhToan = 1 THEN SUM(ISNULL(dt.fCapPhatTaiKhoBac, 0)) ELSE SUM(ISNULL(dt.fCapPhatBangLenhChi, 0)) END),
		dt.ID_DuAn_HangMuc
		FROM VDT_KHV_KeHoachVonUng as tbl
		INNER JOIN VDT_KHV_KeHoachVonUng_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
		WHERE dt.iID_DuAnID = @iIdDuAnId and dt.ID_DuAn_HangMuc = @ID_DuAn_HangMuc
			AND tbl.iID_NguonVonID = @iIdNguonVonId
			AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayDeNghi as DATE)
			AND tbl.iNamKeHoach = @iNamKeHoach
		GROUP BY tbl.Id, tbl.sSoQuyetDinh, tbl.iNamKeHoach, dt.ID_DuAn_HangMuc
	END

	-- Ke hoach nam truoc chuyen sang
	BEGIN
		INSERT INTO #tmp(IIdKeHoachVonId, SSoQuyetDinh, INamKeHoach, ILoaiKeHoachVon, ILoaiNamKhv, ICoQuanThanhToan, FGiaTriKHV)
		SELECT tbl.Id, tbl.sSoDeNghi, tbl.iNamKeHoach, (CASE WHEN iLoaiThanhToan = 1 THEN 3 ELSE 4 END), 1, tbl.iCoQuanThanhToan,
			(CASE WHEN iLoaiThanhToan = 1 THEN SUM(ISNULL(dt.fGiaTriNamTruocChuyenNamSau, 0) + ISNULL(dt.fGiaTriNamNayChuyenNamSau, 0)) 
				ELSE SUM(ISNULL(dt.FGiaTriUngChuyenNamSau, 0) - (ISNULL(dt.fLKThanhToanDenTrcNamQuyetToan_KHUng, 0) - ISNULL(dt.FGiaTriThuHoiTheoGiaiNganThucTe, 0) + ISNULL(dt.fThanhToan_KHUngNamTrcChuyenSang, 0) + ISNULL(dt.fThanhToan_KHUngNamNay, 0))) END)
		FROM VDT_QT_BCQuyetToanNienDo as tbl
		INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
		WHERE dt.iID_DuAnID = @iIdDuAnId
			AND tbl.iID_NguonVonID = @iIdNguonVonId
			AND CAST(tbl.dNgayDeNghi as DATE) < CAST(@dNgayDeNghi AS DATE)
			AND iNamKeHoach < @iNamKeHoach
			AND tbl.iCoQuanThanhToan = @iCoQuanThanhToan
		GROUP BY tbl.Id, tbl.sSoDeNghi, tbl.iNamKeHoach, iLoaiThanhToan, tbl.iCoQuanThanhToan
	END

	-- So tien da thanh toan
	SELECT tmp.IIdKeHoachVonId, tmp.ILoaiKeHoachVon, 
		SUM(ISNULL(dt.fGiaTriThanhToanNN, 0) + ISNULL(dt.fGiaTriThanhToanTN, 0)) as fThanhToan,
		SUM(ISNULL(fGiaTriThuHoiNamTruocTN, 0) + ISNULL(fGiaTriThuHoiNamTruocNN, 0) + ISNULL(fGiaTriThuHoiNamNayTN, 0) + ISNULL(fGiaTriThuHoiNamNayNN, 0)) AS fThuHoi
		INTO #tmpThanhToan
	FROM VDT_TT_DeNghiThanhToan as tbl
	INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
	INNER JOIN #tmp as tmp on dt.iID_KeHoachVonID = tmp.IIdKeHoachVonId
							 AND tmp.ILoaiKeHoachVon = dt.iLoaiKeHoachVon
	WHERE tbl.iID_DuAnId = @iIdDuAnId and tbl.ID_DuAn_HangMuc = @ID_DuAn_HangMuc
		AND tbl.iCoQuanThanhToan = @iCoQuanThanhToan
		AND CAST(dNgayDeNghi as DATE) <= CAST(@dNgayDeNghi as DATE)
		AND (@iIdPheDuyet IS NULL OR tbl.Id <> @iIdPheDuyet)
	GROUP BY tmp.IIdKeHoachVonId, tmp.ILoaiKeHoachVon

	SELECT tbl.IIdKeHoachVonId, tbl.SSoQuyetDinh, tbl.ILoaiKeHoachVon, tbl.ILoaiNamKhv, tbl.INamKeHoach, tbl.ICoQuanThanhToan, tbl.FGiaTriKHV, SUM(ISNULL(dt.fThanhToan, 0)) as FGiaTriKHVDaThanhToan, SUM(ISNULL(dt.fThuHoi, 0)) as FGiaTriKHVDaThuHoi, tbl.ID_DuAn_HangMuc INTO #tbl
	FROM #tmp as tbl
	LEFT JOIN #tmpThanhToan as dt on tbl.IIdKeHoachVonId = dt.IIdKeHoachVonId AND tbl.ILoaiKeHoachVon = dt.ILoaiKeHoachVon
	GROUP BY tbl.IIdKeHoachVonId, tbl.SSoQuyetDinh, tbl.ILoaiKeHoachVon, tbl.ILoaiNamKhv, tbl.INamKeHoach, tbl.ICoQuanThanhToan, tbl.FGiaTriKHV, tbl.ID_DuAn_HangMuc

	SELECT iID_KeHoachVonID, iLoai, iID_DeNghiThanhToanID INTO #khv
	FROM VDT_TT_DeNghiThanhToan_KHV
	WHERE iID_DeNghiThanhToanID = @iIdPheDuyet

	SELECT tbl.*, CAST(0 as float) as FGiaTriThanhToanTN, CAST(0 as float) FGiaTriThanhToanNN, CAST(0 as float) FGiaTriThuHoiTrongNuoc, CAST(0 as float) FGiaTriThuHoiNgoaiNuoc, 0 as ILoaiNamTamUng
	--FROM #tbl as tbl
	--left JOIN #khv as dt on
	FROM #khv as dt
	left JOIN #tbl as tbl on 
	tbl.IIdKeHoachVonId = dt.iID_KeHoachVonID 
	--bỏ điều kiện do luôn set ILoaiKeHoachVon = 1, không có trường hợp ILoaiKeHoachVon = 3
	--AND tbl.ILoaiKeHoachVon = dt.iLoai
	WHERE tbl.FGiaTriKHV > (tbl.FGiaTriKHVDaThanhToan - tbl.FGiaTriKHVDaThuHoi) 

	DROP TABLE #tmp
	DROP TABLE #tmpThanhToan
	DROP TABLE #tbl
	DROP TABLE #khv
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_khvthuhoiung]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_tt_get_khvthuhoiung]
	@iIdDuAnId [uniqueidentifier],
	@iIdNguonVonId [int],
	@dNgayDeNghi [date],
	@iNamKeHoach [int],
	@iCoQuanThanhToan [int],
	@iIdPheDuyet [uniqueidentifier],
	@ID_DuAn_HangMuc [uniqueidentifier]
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT Id, tbl.iNamKeHoach INTO #tmp
	FROM VDT_TT_DeNghiThanhToan as tbl
	WHERE tbl.iID_DuAnId = @iIdDuAnId and tbl.ID_DuAn_HangMuc = @ID_DuAn_HangMuc
		AND tbl.iID_NguonVonID = @iIdNguonVonId
		AND CAST(tbl.dNgayDeNghi as DATE) <= CAST(@dNgayDeNghi as DATE)
		AND tbl.iCoQuanThanhToan = @iCoQuanThanhToan
		AND tbl.iNamKeHoach <= @iNamKeHoach
		AND iLoaiThanhToan = 0

	SELECT dt.iID_KeHoachVonID as IIdKeHoachVonId,
		(CASE WHEN tmp.iNamKeHoach < @iNamKeHoach THEN 1 ELSE 2 END ) as ILoaiNamTamUng,
		(CASE WHEN khvn.Id IS NOT NULL THEN khvn.sSoQuyetDinh WHEN khvu.Id IS NOT NULL THEN khvu.sSoQuyetDinh ELSE qtnd.sSoDeNghi END) as SSoQuyetDinh,
		(CASE WHEN khvn.Id IS NOT NULL THEN khvn.iNamKeHoach WHEN khvu.Id IS NOT NULL THEN khvu.iNamKeHoach ELSE qtnd.iNamKeHoach END) as INamKeHoach,
		(CASE WHEN dt.iLoaiKeHoachVon in (1,3) THEN 1 ELSE 2 END) as iLoaiKeHoachVon,
		SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) as FGiaTriThanhToanTN,
		SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) as FGiaTriThanhToanNN INTO #tmpTamUng
	FROM #tmp as tmp
	INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tmp.Id = dt.iID_DeNghiThanhToanID
	LEFT JOIN VDT_KHV_PhanBoVon as khvn on dt.iID_KeHoachVonID = khvn.Id AND iLoaiKeHoachVon = 1
	LEFT JOIN VDT_KHV_KeHoachVonUng as khvu on dt.iID_KeHoachVonID = khvu.Id AND iLoaiKeHoachVon = 2
	LEFT JOIN VDT_QT_BCQuyetToanNienDo as qtnd on dt.iID_KeHoachVonID = qtnd.Id AND iLoaiKeHoachVon in (3,4)
	LEFT JOIN NS_MucLucNganSach as ml on ml.iID = dt.iID_MucID
										OR ml.iID = dt.iID_TieuMucID
										OR ml.iID = dt.iID_TietMucID
										OR ml.iID = dt.iID_NganhID
	GROUP BY dt.iID_KeHoachVonID, khvn.Id, khvu.Id, khvn.sSoQuyetDinh, khvu.sSoQuyetDinh, qtnd.sSoDeNghi, khvn.iNamKeHoach, khvu.iNamKeHoach, tmp.iNamKeHoach, qtnd.iNamKeHoach,
		dt.iLoaiKeHoachVon

	SELECT tmp.IIdKeHoachVonId, 
		(CASE WHEN dt.iLoaiKeHoachVon in (1,3) THEN 1 ELSE 2 END) as iLoaiKeHoachVon,
		SUM(ISNULL(dt.fGiaTriThuHoiNamTruocTN, 0) + ISNULL(dt.fGiaTriThuHoiNamNayTN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN, 0)) as FGiaTriThuHoiTrongNuoc,
		SUM(ISNULL(dt.fGiaTriThuHoiNamTruocNN, 0) + ISNULL(dt.fGiaTriThuHoiNamNayNN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocNN, 0) + ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN, 0)) as FGiaTriThuHoiNgoaiNuoc INTO #tmpThuHoi
	FROM #tmpTamUng as tmp
	INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tmp.IIdKeHoachVonId = dt.iID_KeHoachVonID
											AND ((tmp.iLoaiKeHoachVon = 1 AND dt.iLoaiKeHoachVon in (1,3)) OR (tmp.iLoaiKeHoachVon = 1 AND dt.iLoaiKeHoachVon in (2,4)))
											AND (fGiaTriThuHoiNamTruocTN IS NOT NULL OR fGiaTriThuHoiNamTruocNN IS NOT NULL 
												OR fGiaTriThuHoiNamNayTN IS NOT NULL OR fGiaTriThuHoiNamNayNN IS NOT NULL 
												OR fGiaTriThuHoiUngTruocNamNayTN IS NOT NULL OR fGiaTriThuHoiUngTruocNamNayNN IS NOT NULL
												OR fGiaTriThuHoiUngTruocNamTruocTN IS NOT NULL OR fGiaTriThuHoiUngTruocNamTruocNN IS NOT NULL)
	INNER JOIN VDT_TT_DeNghiThanhToan as tbl on dt.iID_DeNghiThanhToanID = tbl.Id
	WHERE dt.iID_DeNghiThanhToanID <> @iIdPheDuyet AND tbl.iID_DuAnId = @iIdDuAnId AND CAST(tbl.dNgayDeNghi as DATE) < CAST(@dNgayDeNghi as DATE) and tbl.ID_DuAn_HangMuc = @ID_DuAn_HangMuc
	GROUP BY tmp.IIdKeHoachVonId, dt.iLoaiKeHoachVon

	SELECT tbl.IIdKeHoachVonId, tbl.SSoQuyetDinh, tbl.INamKeHoach, tbl.ILoaiKeHoachVon, tbl.FGiaTriThanhToanTN, tbl.FGiaTriThanhToanNN, 
		(CASE WHEN tbl.INamKeHoach < @iNamKeHoach THEN 1 ELSE 2 END) as ILoaiNamKhv, tbl.ILoaiNamTamUng,
		SUM(ISNULL(dt.FGiaTriThuHoiTrongNuoc, 0 )) as FGiaTriThuHoiTrongNuoc, SUM(ISNULL(dt.FGiaTriThuHoiNgoaiNuoc, 0)) as FGiaTriThuHoiNgoaiNuoc INTO #tbl
	FROM #tmpTamUng as tbl
	LEFT JOIN #tmpThuHoi as dt on tbl.IIdKeHoachVonId = dt.IIdKeHoachVonId AND tbl.iLoaiKeHoachVon = dt.iLoaiKeHoachVon
	GROUP BY tbl.IIdKeHoachVonId, tbl.SSoQuyetDinh, tbl.INamKeHoach, tbl.ILoaiKeHoachVon, tbl.FGiaTriThanhToanTN, tbl.FGiaTriThanhToanNN, tbl.ILoaiNamTamUng
	
	SELECT iID_KeHoachVonID, iLoai INTO #khv
	FROM VDT_TT_DeNghiThanhToan_KHV
	WHERE iID_DeNghiThanhToanID = @iIdPheDuyet

	SELECT tbl.*, @iCoQuanThanhToan as ICoQuanThanhToan, 
		(ISNULL(FGiaTriThanhToanNN, 0) + ISNULL(FGiaTriThanhToanTN, 0)) as FGiaTriKHV, 
		(ISNULL(FGiaTriThanhToanNN, 0) + ISNULL(FGiaTriThanhToanTN, 0) - (ISNULL(FGiaTriThuHoiNgoaiNuoc, 0) + ISNULL(FGiaTriThuHoiTrongNuoc, 0))) FGiaTriKHVDaThanhToan
	FROM #tbl as tbl
	INNER JOIN #khv as dt on tbl.IIdKeHoachVonId = dt.iID_KeHoachVonID AND ILoaiNamTamUng = 2 AND tbl.ILoaiKeHoachVon = (CASE WHEN dt.iLoai in (1,3) THEN 1 ELSE 2 END)
	--WHERE FGiaTriThanhToanNN > FGiaTriThuHoiNgoaiNuoc OR FGiaTriThanhToanTN > FGiaTriThuHoiTrongNuoc
	UNION ALL
	SELECT tbl.*, @iCoQuanThanhToan as ICoQuanThanhToan,
		(ISNULL(FGiaTriThanhToanNN, 0) + ISNULL(FGiaTriThanhToanTN, 0)) as FGiaTriKHV, 
		(ISNULL(FGiaTriThanhToanNN, 0) + ISNULL(FGiaTriThanhToanTN, 0) - (ISNULL(FGiaTriThuHoiNgoaiNuoc, 0) + ISNULL(FGiaTriThuHoiTrongNuoc, 0))) FGiaTriKHVDaThanhToan
	FROM #tbl as tbl
	WHERE ILoaiNamTamUng = 1 --AND (FGiaTriThanhToanNN > FGiaTriThuHoiNgoaiNuoc OR FGiaTriThanhToanTN > FGiaTriThuHoiTrongNuoc)

	DROP TABLE #tbl
	DROP TABLE #tmp
	DROP TABLE #tmpTamUng
	DROP TABLE #tmpThuHoi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_tt_get_mlns_by_khv]    Script Date: 12/30/2022 2:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_tt_get_mlns_by_khv]
	@iNamLamViec [int],
	@data [dbo].[t_tbl_tonghopdautu_v2] READONLY
WITH EXECUTE AS CALLER
AS
BEGIN
	SELECT tbl.iID_ChungTu as IidKeHoachVonId, 
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN 1 ELSE 2 END) as ILoaiKeHoachVon,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_MucID ELSE khvu.iID_MucID END) as iID_MucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_TieuMucID ELSE khvu.iID_TieuMucID END) as iID_TieuMucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_TietMucID ELSE khvu.iID_TietMucID END) as iID_TietMucID,
		(CASE WHEN khvn.iID_PhanBoVonID IS NOT NULL THEN khvn.iID_NganhID ELSE khvu.iID_NganhID END) as iID_NganhID INTO #tmp
	FROM @data as tbl
	LEFT JOIN VDT_KHV_PhanBoVon_ChiTiet as khvn on tbl.iID_ChungTu = khvn.iID_PhanBoVonID AND tbl.sMaNguon = N'KHVN' AND khvn.iID_DuAnID = tbl.iID_DuAnID and khvn.iID_DuAn_HangMucID = tbl.IIdLoaiCongTrinh
	LEFT JOIN VDT_KHV_KeHoachVonUng_ChiTiet as khvu on tbl.iID_ChungTu = khvu.iID_KeHoachUngID AND tbl.sMaNguon = N'KHVU' AND khvu.iID_DuAnID = tbl.iID_DuAnID and khvu.ID_DuAn_HangMuc = tbl.IIdLoaiCongTrinh

	SELECT tmp.IidKeHoachVonId, tmp.ILoaiKeHoachVon, ml.sLNS as LNS, ml.sL as L, ml.sK as K, ml.sM as M, ml.sTM as TM, ml.sTTM as TTM, ml.sNG as NG INTO #tmpMucLuc
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach as ml on ml.iID = tmp.iID_MucID
										OR ml.iID = tmp.iID_TieuMucID
										OR ml.iID = tmp.iID_TietMucID
										OR ml.iID = tmp.iID_NganhID

	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, '' as TM, '' as TTM, '' as NG INTO #tmpResult
	FROM #tmpMucLuc
	WHERE ISNULL(M, '') <> ''
	UNION ALL
	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, '' as TTM, '' as NG
	FROM #tmpMucLuc
	WHERE ISNULL(TM, '') <> ''
	UNION ALL
	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, TTM, '' as NG
	FROM #tmpMucLuc
	WHERE ISNULL(TTM, '') <> ''
	UNION ALL
	SELECT IidKeHoachVonId, ILoaiKeHoachVon, LNS, L, K, M, TM, TTM, NG
	FROM #tmpMucLuc
	WHERE ISNULL(NG, '') <> ''

	SELECT DISTINCT tbl.*, sMoTa as SMoTa
	FROM #tmpResult as tbl
	INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @iNamLamViec
									AND tbl.LNS COLLATE DATABASE_DEFAULT = ml.sLNS COLLATE DATABASE_DEFAULT
									AND tbl.L COLLATE DATABASE_DEFAULT = ml.sL COLLATE DATABASE_DEFAULT
									AND tbl.K COLLATE DATABASE_DEFAULT = ml.sK COLLATE DATABASE_DEFAULT
									AND tbl.M COLLATE DATABASE_DEFAULT = ml.sM COLLATE DATABASE_DEFAULT
									AND tbl.TM COLLATE DATABASE_DEFAULT = ml.sTM COLLATE DATABASE_DEFAULT
									AND tbl.TTM COLLATE DATABASE_DEFAULT = ml.sTTM COLLATE DATABASE_DEFAULT
									AND tbl.NG COLLATE DATABASE_DEFAULT = ml.sNG COLLATE DATABASE_DEFAULT
	ORDER BY LNS, L, K, M, TM, TTM, NG

	 DROP TABLE #tmpResult
	 DROP TABLE #tmpMucLuc
	DROP TABLE #tmp
END
;
;
GO
