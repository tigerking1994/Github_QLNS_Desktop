/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_2]    Script Date: 10/08/2022 4:30:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet]    Script Date: 10/08/2022 4:30:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dc_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dc_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovonchitiet]    Script Date: 12/08/2022 8:54:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovonchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovonchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_list_dexuat_chitiet]    Script Date: 12/08/2022 8:54:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khv_khth_list_dexuat_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khv_khth_list_dexuat_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_list_dexuat_chitiet]    Script Date: 12/08/2022 8:54:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khv_khth_list_dexuat_chitiet]
	@VoucherId nvarchar(255)
AS
BEGIN
	declare @VoucherAgregate uniqueidentifier;
	select @VoucherAgregate = iID_TongHopParent from VDT_KHV_KeHoach5Nam_DeXuat where Id = @VoucherId;

	SELECT DISTINCT
		ctct.STT					AS STT,
		ctct.Id						AS Id,
		ctct.iID_KeHoach5NamID		AS IIdKeHoach5NamId,
		ctctdd.iID_DuAnID			AS iIdDuAnId,
		ctct.sTen					AS STen,
		ctct.SDiaDiem				AS SDiaDiem,
		ctct.IGiaiDoanTu			AS IGiaiDoanTu,
		ctct.IGiaiDoanDen			AS IGiaiDoanDen,
		ctct.iID_LoaiCongTrinhID	AS IIdLoaiCongTrinhId,
		NULL						AS STenLoaiCongTrinh,
		ctct.iID_NguonVonID			AS IIdNguonVonId,
		NULL						AS STenNguonVon,
		ctct.iID_DonViQuanLyID		AS IIdDonViId,
		ctct.iID_MaDonVi			AS IIdMaDonVi,
		NULL						AS STenDonVi,
		ctct.fHanMucDauTu			AS FHanMucDauTu,
		CAST(0 AS float)			AS FVonNSQPLuyKe,
		CAST(0 AS float)			AS FVonNSQP,
		ctct.fGiaTriNamThuNhat		AS FGiaTriNamThuNhat,
		ctct.fGiaTriNamThuHai		AS FGiaTriNamThuHai,
		ctct.fGiaTriNamThuBa		AS FGiaTriNamThuBa,
		ctct.fGiaTriNamThuTu		AS FGiaTriNamThuTu,
		ctct.fGiaTriNamThuNam		AS FGiaTriNamThuNam,
		ctct.fGiaTriBoTri			AS FGiaTriBoTri,
		ctct.fGiaTriKeHoach			AS FGiaTriKeHoach,
		CAST(0 AS float)			AS FTongSoNhuCauNSQP,
		CAST(0 AS float)			AS FGiaTriNamThuNhatOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuHaiOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuBaOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuTuOrigin,
		CAST(0 AS float)			AS FGiaTriNamThuNamOrigin,
		CAST(0 AS float)			AS FGiaTriBoTriOrigin,
		CAST(0 AS float)			AS FGiaTriKeHoachOrigin,
		CAST(0 AS float)			AS FTongSoNhuCauNSQPOrigin,
		ctct.iID_ParentModified		AS IdParentModified,
		ctct.sGhiChu				AS SGhiChu,
		CASE 
			WHEN ctct.IdParent IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
		END							AS IsHangCha,
		ctct.Level					AS Level,
		ctct.indexCode				AS IndexCode,
		ctct.SMaOrder				AS SMaOrder,
		ctct.IdReference			AS IdReference,
		ctct.IdParent				AS IdParent,
		ctct.IsParent				AS IsParent,
		ctct.IsStatus				AS IsStatus,
		ctct.sTrangThai				AS STrangThai
  FROM VDT_KHV_KeHoach5Nam_DeXuat as tbl
  inner JOIN VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet ctct on ctct.iID_KeHoach5NamID = tbl.Id																--sửa left join
  inner JOIN VDT_DA_DuAn da ON ctct.Id = da.Id_DuAnKhthDeXuat OR ctct.IdReference = da.Id_DuAnKhthDeXuat OR ctct.iID_DuAnID = da.iID_DuAnID			--sửa left join
  inner JOIN																																		--sửa inner join
  (
    SELECT ddct.iID_DuAnID, ddct.iID_NguonVonID, dd.iGiaiDoanTu, dd.iGiaiDoanDen 
    FROM VDT_KHV_KeHoach5Nam as dd 
    INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as ddct on dd.iID_KeHoach5NamID = ddct.iID_KeHoach5NamID
  ) as ctctdd  ON ctctdd.iID_DuAnID = da.iID_DuAnID AND (ctct.iID_NguonVonID = ctctdd.iID_NguonVonID OR ctct.IdReference IS NULL) AND (tbl.iGiaiDoanTu = ctctdd.iGiaiDoanTu AND tbl.iGiaiDoanDen = ctctdd.iGiaiDoanDen)
	WHERE 
		ctct.iID_KeHoach5NamID = @VoucherAgregate
		and ctct.iID_TongHop = @VoucherId
END
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_kehoach_chitiet_canbo]    Script Date: 15/12/2021 6:34:37 PM ******/
SET ANSI_NULLS ON
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovonchitiet]    Script Date: 12/08/2022 8:54:16 AM ******/
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
		pbvct.fGiaTriThuHoiNamTruocKhoBac as FGiaTriThuHoiNamTruocKhoBac,
		pbvct.fGiaTriThuHoiNamTruocLenhChi as FGiaTriThuHoiNamTruocLenhChi,
		--isnull(pbvct.Id, NEWID()) as IIdPhanBoVonId,
		pbvct.iID_PhanBoVonID as IIdPhanBoVon,
		pbvct.ILoaiDuAn as ILoaiDuAn,
		pbv.Id as IdChungTu,
		pbv.iID_ParentId as IdChungTuParent,
		pbv.bActive as BActive,
		pbv.bIsGoc as IsGoc
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
		and pbv.bIsGoc = 1
END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet]    Script Date: 10/08/2022 4:30:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dc_chungtu_chitiet]
	@chungTuId nvarchar(100),
	@namLamViec int,
	@namNganSach int,
	@nguonNganSach int,
	@lns nvarchar(max),
	@donVi nvarchar(max),
	@loaiDuKien int,
	@loaiChungTu int,
	@ngayChungTu datetime,
	@userName nvarchar(50)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT @CountDonViCha = count(*)
	FROM
	  (SELECT *
	   FROM NguoiDung_DonVi
	   WHERE iID_MaNguoiDung = @userName
		 AND iNamLamViec = @namLamViec
		 AND iTrangThai = 1) nddv
	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @namLamViec
		 AND iTrangThai = 1
		 AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;
	WITH tblDuToanNganSachNam as(
		select 
			iID_MLNS, 
			iID_MLNS_Cha, 
			sXauNoiMa, 
			iID_MaDonVi, 
			iNamLamViec, 
			iNamNganSach, 
			iID_MaNguonNganSach, 
			sum(fTuChi) as TuChi, 
			sum(fHangNhap) as HangNhap, 
			sum(fHangMua) as HangMua 
		from NS_DT_ChungTuChiTiet
		where iNamLamViec = @namLamViec 
		and iNamNganSach = @namNganSach 
		and iID_MaNguonNganSach = @nguonNganSach
		and iID_MaDonVi in (select * from f_split(@donVi))
		and iID_DTChungTu in 
			(select 
				iID_DTChungTu 
			from NS_DT_ChungTu 
			where iNamLamViec = @namLamViec 
			and iNamNganSach = @namNganSach 
			and iID_MaNguonNganSach = @nguonNganSach
			and iLoai = 1 
			and iLoaiChungTu = @loaiChungTu
			and bKhoa = 1
			and sSoQuyetDinh is not null
			and sSoQuyetDinh <> ''
			and cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
		group by iID_MLNS, iID_MLNS_Cha, sXauNoiMa, iID_MaDonVi, iNamLamViec, iNamNganSach, iID_MaNguonNganSach
	),
	tblQuyetToanDauNam AS (
		select 
			iID_MLNS, 
			iID_MLNS_Cha, 
			sLNS, 
			iID_MaDonVi,
			sum(fTuChi_PheDuyet) as TuChi 
		from NS_QT_ChungTuChiTiet
		where iNamLamViec = @namLamViec 
		and iNamNganSach = @namNganSach 
		and iID_MaNguonNganSach = @nguonNganSach
		and iID_MaDonVi in (select * from f_split(@donVi))
		and iID_QTChungTu in
			(select 
				iID_QTChungTu 
			from NS_QT_ChungTu 
			where iNamLamViec = @namLamViec 
			and iNamNganSach = @namNganSach 
			and iID_MaNguonNganSach = @nguonNganSach
			and ((@loaiDuKien = 1 and iThangQuy <= 6) 
				or
				(@loaiDuKien = 2 and iThangQuy <= 9))
			and cast(dNgayChungTu as date) <= @ngayChungTu)
			group by iID_MLNS, iID_MLNS_Cha, sLNS, iID_MaDonVi
	),
	tblMlnsQuyetToanDauNam AS (
		SELECT DISTINCT VALUE
		FROM 
		(
			SELECT 
				CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
				CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
				CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
				CAST(sLNS AS nvarchar(10)) LNS 
			FROM
				tblQuyetToanDauNam
		) LNS
		UNPIVOT
		(
			value
			FOR col in (LNS1, LNS3, LNS5, LNS)
		) un
	),
	tblQuyetToanDauNamWithMlns AS (
		SELECT mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			quyetToan.TuChi,
			quyetToan.iID_MaDonVi
		FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @namLamViec and sLNS in (SELECT * FROM tblMlnsQuyetToanDauNam)) mlns
		LEFT JOIN
			tblQuyetToanDauNam quyetToan
		ON mlns.iID_MLNS = quyetToan.iID_MLNS
	),
	C AS  
	(  
		SELECT T.iID_MLNS,  
				T.TuChi, 
				T.iID_MLNS AS RootID,
				T.iID_MaDonVi
		FROM tblQuyetToanDauNamWithMlns T
		UNION ALL 
		SELECT T.iID_MLNS,  
				T.TuChi, 
				C.RootID,
				T.iID_MaDonVi
		FROM tblQuyetToanDauNamWithMlns T
		INNER JOIN C
		on T.iID_MLNS_Cha = C.iID_MLNS 
	),
	tblQuyetToanDauNamData AS (
		SELECT iID_MLNS, 
			iID_MLNS_Cha, 
			sum(TuChi) AS TuChi,
			iID_MaDonVi
		FROM 
		(
			SELECT T.iID_MLNS,  
				T.iID_MLNS_Cha,
				total.TuChi,
				T.iID_MaDonVi
			FROM tblQuyetToanDauNamWithMlns T  
			LEFT JOIN 
				(  
					SELECT RootID, sum(TuChi) AS TuChi
					FROM C
					GROUP BY RootID
				) AS total 
			ON T.iID_MLNS = total.RootID 
			WHERE total.TuChi <> 0
		) data
		GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
	)
	SELECT dc.*, dv.sTenDonVi FROM (
		SELECT 
			isnull(dcctct.iID_DCCTChiTiet, NEWID()) AS iID_DCCTChiTiet,
			case 
				when dtNsNam.TuChi is not null 
				or dtNsNam.HangNhap is not null 
				or dtNsNam.HangMua is not null
				or qtdn.TuChi is not null
				then @chungTuId
			else
				dcctct.iID_DCChungTu
			end as iID_DCChungTu,
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.sTNG,
			mlns.sTNG1,
			mlns.sTNG2,
			mlns.sTNG3,
			mlns.sMoTa,
			mlns.bHangCha,
			isnull(dcctct.iNamNganSach, @namNganSach) as iNamNganSach,
			isnull(dcctct.iID_MaNguonNganSach, @nguonNganSach) as iID_MaNguonNganSach,
			isnull(dcctct.iNamLamViec, @namLamViec) as iNamLamViec,
			isnull(dcctct.iID_MaDonVi, isnull(dtNsNam.iID_MaDonVi, qtdn.iID_MaDonVi)) as iID_MaDonVi,
			dcctct.sGhiChu,
			--case 
			--when mlns.sLNS = '1040200' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangNhap, 0) 
			--when mlns.sLNS = '1040300' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangMua, 0) 
			--else isnull(dtNsNam.TuChi, 0)
			--end
			--as DuToanNganSachNam,
			case 
				when isnull(dcctct.fDuToan, 0) = 0 then 
					case 
						when mlns.sLNS = '1040200' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangNhap, 0) 
						when mlns.sLNS = '1040300' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangMua, 0) 
						else isnull(dtNsNam.TuChi, 0)
					end
				else isnull(dcctct.fDuToan, 0) 
				end as DuToanNganSachNam,
			isnull(dcctct.fDuKienQtDauNam, qtdn.TuChi) as DuKienQtDauNam,
			isnull(dcctct.fDuKienQtCuoiNam, 0) as DuKienQtCuoiNam,
			mlns.sChiTietToi,
			mlns.bHangChaDuToan,
			dcctct.dNgayTao,
			dcctct.dNgaySua,
			dcctct.sNguoiTao,
			dcctct.sNguoiSua
		FROM
		  (SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iNamLamViec = @namLamViec
			 AND iTrangThai = 1
			 AND bHangChaDuToan is not null
			 AND ((@CountDonViCha <> 0
				   AND sLNS in
					 (SELECT DISTINCT VALUE
					  FROM
					   (SELECT CAST(LEFT(Item, 1) AS nvarchar(10)) LNS1,
							   CAST(LEFT(Item, 3) AS nvarchar(10)) LNS3,
							   CAST(LEFT(Item, 5) AS nvarchar(10)) LNS5,
							   CAST(Item AS nvarchar(10)) LNS
						FROM f_split(@LNS) ) LNS UNPIVOT (value
																FOR col in (LNS1, LNS3, LNS5, LNS)) un))
				  OR (@CountDonViCha = 0
					  AND sLNS in
						(SELECT DISTINCT VALUE
						 FROM
						   (SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
								   CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
								   CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
								   CAST(sLNS AS nvarchar(10)) LNS
							FROM NS_NguoiDung_LNS
							WHERE sMaNguoiDung = @userName
							  AND iNamLamViec = @namLamViec
							  AND sLNS IN
								(SELECT *
								 FROM f_split(@LNS)) ) LNS UNPIVOT (value
																	FOR col in (LNS1, LNS3, LNS5, LNS)) un))) ) mlns

	LEFT JOIN
		tblDuToanNganSachNam dtNsNam
	ON dtNsNam.iID_MLNS = mlns.iID_MLNS
	LEFT JOIN
		tblQuyetToanDauNamData qtdn
	ON qtdn.iID_MLNS = mlns.iID_MLNS
	LEFT JOIN
	(select * from NS_DC_ChungTuChiTiet 
		where iNamLamViec = @namLamViec
		and iNamNganSach = @namNganSach
		and iID_MaNguonNganSach = @nguonNganSach
		and iID_DCChungTu = @chungTuId) dcctct
	on dcctct.iID_MLNS = mlns.iID_MLNS
	) dc
	LEFT JOIN
		(SELECT * FROM DonVi where iNamLamViec = @namLamViec) dv
	ON dv.iID_MaDonVi = dc.iID_MaDonVi
	order by sXauNoiMa
	option (maxrecursion 0)
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_2]    Script Date: 10/08/2022 4:30:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec sp_vdt_lay_gia_tri_denghi_thanh_toan 0, '346dc9b1-5053-4ebe-a585-6765ab155f10', '2022/04/27', 1, 2022, 1

CREATE PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_2]
	@bThanhToanTheoHopDong bit,
	@iIdChungTu varchar(max),
	@NgayDeNghi datetime,
	@NguonVonId int,
	@NamKeHoach int,
	@iCoQuanThanhToan int
AS
BEGIN
	DECLARE @uIdEmpty uniqueidentifier = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER)
	DECLARE @fLuyKeTTKLHTNN float
	DECLARE @fLuyKeTTKLHTTN float
	DECLARE @fLuyKeTUChuaThuHoiCheDoNN float
	DECLARE @fLuyKeTUChuaThuHoiCheDoTN float
	DECLARE @fLuyKeTUChuaThuHoiUngTruocNN float
	DECLARE @fLuyKeTUChuaThuHoiUngTruocTN float

	SELECT
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as ThanhToanTN,
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as ThanhToanNN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiNamTruocTN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiNamNayTN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiNamTruocTN, 0) + ISNULL(fGiaTriThuHoiNamNayTN, 0)) ELSE SUM(0) END) as ThuHoiUngTN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiNamTruocNN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiNamNayNN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiNamTruocNN, 0) + ISNULL(fGiaTriThuHoiNamNayNN, 0)) ELSE SUM(0) END) as ThuHoiUngNN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiUngTruocNamNayTN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocTN, 0)) ELSE SUM(0) END) as ThuHoiUngUngTruocTN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocNN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiUngTruocNamNayNN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocNN, 0)) ELSE SUM(0) END) as ThuHoiUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngUngTruocTN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngCheDoTN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngCheDoNN INTO #tmp
	FROM VDT_TT_DeNghiThanhToan tbl
	INNER JOIN VDT_TT_DeNghiThanhToan_KHV as khv on tbl.Id = khv.iID_DeNghiThanhToanID
	LEFT JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
	WHERE @iIdChungTu = (CASE WHEN @bThanhToanTheoHopDong = 1 THEN tbl.iID_HopDongID ELSE tbl.iID_ChiPhiID END)
		  AND 
		  (
			  tbl.dNgayPheDuyet <= @NgayDeNghi 
			  and iNamKeHoach <= @NamKeHoach
		  )
		  AND tbl.iCoQuanThanhToan = @iCoQuanThanhToan
		  -- AND tbl.iID_NguonVonID = @NguonVonId
	GROUP BY iLoaiThanhToan,dt.fGiaTriThanhToanTN, dt.fGiaTriThanhToanNN, dt.fGiaTriThuHoiNamTruocTN, dt.fGiaTriThuHoiNamNayTN, dt.fGiaTriThuHoiNamTruocNN, dt.fGiaTriThuHoiNamNayNN, khv.iLoai,
		fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamNayNN, fGiaTriThuHoiUngTruocNamTruocNN

	SELECT @fLuyKeTTKLHTNN = SUM(ISNULL(tt.fLuyKeTTKLHTNN_KHVN, 0) + ISNULL(tt.fLuyKeTTKLHTNN_KHVU, 0)),
		@fLuyKeTTKLHTTN = SUM(ISNULL(tt.fLuyKeTTKLHTTN_KHVN, 0) + ISNULL(tt.fLuyKeTTKLHTTN_KHVU, 0)) ,
		@fLuyKeTUChuaThuHoiCheDoNN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiNN_KHVN, 0)) ,
		@fLuyKeTUChuaThuHoiCheDoTN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiTN_KHVN, 0)) ,
		@fLuyKeTUChuaThuHoiUngTruocNN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiNN_KHVU, 0)),
		@fLuyKeTUChuaThuHoiUngTruocTN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiTN_KHVU, 0))
	FROM VDT_KT_KhoiTao_DuLieu as tbl
	INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet as dt on tbl.Id = dt.iID_KhoiTaoDuLieuID
	INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan as tt on dt.Id = tt.iId_KhoiTaoDuLieuChiTietId
	WHERE @iIdChungTu = (CASE WHEN @bThanhToanTheoHopDong = 1 THEN tt.iID_HopDongID ELSE @uIdEmpty END)
		-- AND dt.iID_NguonVonID = @NguonVonId
		AND dt.iCoQuanThanhToan = @iCoQuanThanhToan
	

	SELECT (ISNULL(@fLuyKeTTKLHTTN, 0) + ISNULL(SUM(ISNULL(ThanhToanTN, 0)), 0)) as ThanhToanTN,
			(ISNULL(@fLuyKeTTKLHTNN, 0) + ISNULL(SUM(ISNULL(ThanhToanNN, 0)), 0)) as ThanhToanNN,
			ISNULL(SUM(ISNULL(ThuHoiUngTN, 0)), 0) as ThuHoiUngTN,
			ISNULL(SUM(ISNULL(ThuHoiUngNN, 0)), 0) as ThuHoiUngNN,
			(ISNULL(@fLuyKeTUChuaThuHoiCheDoTN, 0) + ISNULL(SUM(ISNULL(TamUngCheDoTN, 0)), 0)) as TamUngTN,
			(ISNULL(@fLuyKeTUChuaThuHoiCheDoNN, 0) +ISNULL(SUM(ISNULL(TamUngCheDoNN, 0)), 0)) as TamUngNN,
			ISNULL(SUM(ISNULL(ThuHoiUngUngTruocNN, 0)), 0) as ThuHoiUngUngTruocNN,
			ISNULL(SUM(ISNULL(ThuHoiUngUngTruocTN, 0)), 0) as ThuHoiUngUngTruocTN,
			(ISNULL(@fLuyKeTUChuaThuHoiUngTruocTN, 0) + ISNULL(SUM(ISNULL(TamUngUngTruocTN, 0)), 0)) as TamUngUngTruocTN,
			(ISNULL(@fLuyKeTUChuaThuHoiUngTruocNN, 0) +ISNULL(SUM(ISNULL(TamUngUngTruocNN, 0)), 0)) as TamUngUngTruocNN

	FROM  #tmp 
	DROP TABLE #tmp
END
;
;
GO
