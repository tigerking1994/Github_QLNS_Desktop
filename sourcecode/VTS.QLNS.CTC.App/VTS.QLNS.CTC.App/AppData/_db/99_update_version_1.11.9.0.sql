/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_index]    Script Date: 08/09/2022 6:33:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thongtri_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thongtri_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khv_kehoach_5_nam_de_xuat_export]    Script Date: 08/09/2022 6:33:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_khv_kehoach_5_nam_de_xuat_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_khv_kehoach_5_nam_de_xuat_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_list_dexuat_chitiet]    Script Date: 08/09/2022 6:33:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khv_khth_list_dexuat_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khv_khth_list_dexuat_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_find_id_khth]    Script Date: 08/09/2022 6:33:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khv_khth_find_id_khth]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khv_khth_find_id_khth]
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet_tongso]    Script Date: 08/09/2022 6:33:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dc_chungtu_chitiet_tongso]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dc_chungtu_chitiet_tongso]
GO
/****** Object:  StoredProcedure [dbo].[rpt_dm_mlns]    Script Date: 08/09/2022 6:33:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_dm_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_dm_mlns]
GO
/****** Object:  StoredProcedure [dbo].[rpt_dm_mlns]    Script Date: 08/09/2022 6:33:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[rpt_dm_mlns] 
	-- Add the parameters for the stored procedure here
	@YearOfWork int, 
	@MLNS_ID uniqueidentifier = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	WITH tblParent AS
	(
		SELECT *
			FROM NS_MucLucNganSach WHERE ((@MLNS_ID = '00000000-0000-0000-0000-000000000000' and sLNS = '1') or iID_MLNS = @MLNS_ID) and iNamLamViec = @YearOfWork
		UNION ALL
		SELECT n.*
			FROM NS_MucLucNganSach n  JOIN tblParent  ON n.iID_MLNS = tblParent.iID_MLNS_Cha where n.iNamLamViec = @YearOfWork
			     
	),
	tblChild AS
	(
		SELECT *
			FROM NS_MucLucNganSach WHERE ((@MLNS_ID = '00000000-0000-0000-0000-000000000000' and sLNS = '1') or iID_MLNS = @MLNS_ID) and iNamLamViec = @YearOfWork
		UNION ALL
		SELECT n.* FROM NS_MucLucNganSach n  JOIN tblChild  ON n.iID_MLNS_Cha = tblChild.iID_MLNS where n.iNamLamViec = @YearOfWork
	)
	SELECT * FROM  tblParent union select * from tblChild order by sXauNoiMa;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet_tongso]    Script Date: 08/09/2022 6:33:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dc_chungtu_chitiet_tongso]
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
	DECLARE @CountChiTiet int;
	DECLARE @CountDonViCha int;

	SELECT @CountChiTiet = count(*) 
	FROM NS_DC_ChungTuChiTiet ctct 
	INNER JOIN NS_DC_ChungTu ct ON ctct.iID_DCChungTu = ct.iID_DCChungTu
	WHERE ct.iID_DCChungTu = @chungTuId;

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
	if (@CountChiTiet = 0)
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
		--and iID_MaDonVi in (select * from f_split(@donVi))
		and iID_MaDonVi in (SELECT iID_MaDonVi FROM DonVi WHERE iNamLamViec = @namLamViec AND iTrangThai = 1 AND iLoai = 0)
		and iID_DTChungTu in 
			(select 
				iID_DTChungTu 
			from NS_DT_ChungTu 
			where iNamLamViec = @namLamViec 
			and iNamNganSach = @namNganSach 
			and iID_MaNguonNganSach = @nguonNganSach
			and iLoai = 0
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
		--and iID_MaDonVi in (select * from f_split(@donVi))
		and iID_MaDonVi in (SELECT iID_MaDonVi FROM DonVi WHERE iNamLamViec = @namLamViec AND iTrangThai = 1 AND iLoai = 0)
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
			case 
			when mlns.sLNS = '1040200' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangNhap, 0) 
			when mlns.sLNS = '1040300' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangMua, 0) 
			else isnull(dtNsNam.TuChi, 0)
			end
			as DuToanNganSachNam,
			--case 
			--	when isnull(dcctct.fDuToan, 0) = 0 then 
			--		case 
			--			when mlns.sLNS = '1040200' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangNhap, 0) 
			--			when mlns.sLNS = '1040300' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangMua, 0) 
			--			else isnull(dtNsNam.TuChi, 0)
			--		end
			--	else isnull(dcctct.fDuToan, 0) 
			--	end as DuToanNganSachNam,
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

	else 

	SELECT dc.*, dv.sTenDonVi FROM (
		SELECT 
			isnull(dcctct.iID_DCCTChiTiet, NEWID()) AS iID_DCCTChiTiet,
			dcctct.iID_DCChungTu,
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
			dcctct.iID_MaDonVi,
			dcctct.sGhiChu,
			isnull(dcctct.fDuToan, 0) as DuToanNganSachNam,
			isnull(dcctct.fDuKienQtDauNam, 0) as DuKienQtDauNam,
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_find_id_khth]    Script Date: 08/09/2022 6:33:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khv_khth_find_id_khth]
	@VoucherId nvarchar(255)
AS
BEGIN
	SELECT dx.Id FROM 
	VDT_KHV_KeHoach5Nam_ChiTiet ddct
	inner join VDT_KHV_KeHoach5Nam dd
		on ddct.iID_KeHoach5NamID = dd.iID_KeHoach5NamID
	inner join VDT_DA_DuAn da
		on ddct.iID_DuAnID = da.iID_DuAnID
	inner join VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet dxct
		on da.Id_DuAnKhthDeXuat = dxct.Id
	inner join VDT_KHV_KeHoach5Nam_DeXuat dx
		on dxct.iID_KeHoach5NamID = dx.Id
	where dd.iID_KeHoach5NamID = @VoucherId
END
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_kehoach_chitiet_canbo]    Script Date: 15/12/2021 6:34:37 PM ******/
SET ANSI_NULLS ON
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_list_dexuat_chitiet]    Script Date: 08/09/2022 6:33:41 PM ******/
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
  inner join VDT_KHV_KeHoach5Nam_DeXuat dx on ctct.iID_KeHoach5NamID = dx.Id
  inner join VDT_KHV_KeHoach5Nam dd on dx.Id = dd.iID_KhthDeXuat
	WHERE 
		ctct.iID_KeHoach5NamID = @VoucherAgregate
		and ctct.iID_TongHop = @VoucherId
END
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_kehoach_chitiet_canbo]    Script Date: 15/12/2021 6:34:37 PM ******/
SET ANSI_NULLS ON
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khv_kehoach_5_nam_de_xuat_export]    Script Date: 08/09/2022 6:33:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_khv_kehoach_5_nam_de_xuat_export]
	@Id nvarchar(max),
	@lct nvarchar(max),
	@IdNguonVon nvarchar(max),
	@type int,
	@MenhGiaTienTe float,
	@iNamLamViec int
AS
BEGIN
	if(@type = 1)
	begin
		select tbl.* from (

		select
			lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			lct.iID_Parent as IdLoaiCongTrinhParent,
			lct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
			3 as Loai,
			lct.sTenLoaiCongTrinh as STenDuAn,
			'' as STenDonVi,
			'' as SDiaDiem,
			'' as SThoiGianThucHien,
			0 as FHanMucDauTu,
			'' as STenNguonVon,
			0 as FTongSoNhuCau,
			0 as FTongSo,
			0 as FGiaTriNamThuNhat,
			0 as FGiaTriNamThuHai,
			0 as FGiaTriNamThuBa,
			0 as FGiaTriNamThuTu,
			0 as FGiaTriNamThuNam,
			0 as FGiaTriBoTri,
			'' as SGhiChu,
			cast(1 as bit) as IsHangCha,
			NEWID() as Id,
			case
				when lct.iID_Parent is null then 0 else 1
			end LoaiParent,
			0 as IIdNguonVon,
			0 as LuyKeVonNSQPDaBoTri,
			0 as LuyKeVonNSQPDeNghiBoTri,
			0 as TongLuyKe,
			null as DNgayQuyetDinh,
			cast(0 as float) as FHanMucDauTuDP,
			cast(0 as float) as FHanMucDauTuNN,
			cast(0 as float) as FHanMucDauTuOrther,
			cast(0 as float) as FHanMucDauTuQP,
			'' as SSoQuyetDinh
		from f_loai_cong_trinh_get_list_childrent(@lct) lct

		union all

		select 
			dmlct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			dmlct.iID_Parent as IdLoaiCongTrinhParent,
			dmlct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
			4 as Loai,
			ctct.sTen as STenDuAn,
			dv.sTenDonVi as STenDonVi,
			ctct.SDiaDiem as SDiaDiem,
			(cast(ctct.IGiaiDoanTu as nvarchar) + ' - ' + cast(ctct.IGiaiDoanDen as nvarchar)) as SThoiGianThucHien,
			ctct.fHanMucDauTu/@MenhGiaTienTe as FHanMucDauTu,
			ns.sTen as STenNguonVon,
			((isnull(ctct.fGiaTriNamThuNhat, 0) + isnull(ctct.fGiaTriNamThuHai, 0) 
				+ isnull(ctct.fGiaTriNamThuBa, 0) + isnull(ctct.fGiaTriNamThuTu, 0) 
				+ isnull(ctct.fGiaTriNamThuNam, 0) + isnull(ctct.fGiaTriBoTri, 0))/@MenhGiaTienTe) as FTongSoNhuCau,
			((isnull(ctct.fGiaTriNamThuNhat, 0) 
				+ isnull(ctct.fGiaTriNamThuHai, 0) + isnull(ctct.fGiaTriNamThuBa, 0)
				+ isnull(ctct.fGiaTriNamThuTu, 0) + isnull(ctct.fGiaTriNamThuNam, 0))/@MenhGiaTienTe) as FTongSo,
			(ctct.fGiaTriNamThuNhat/@MenhGiaTienTe) as FGiaTriNamThuNhat,
			(ctct.fGiaTriNamThuHai/@MenhGiaTienTe) as FGiaTriNamThuHai,
			(ctct.fGiaTriNamThuBa/@MenhGiaTienTe) as FGiaTriNamThuBa,
			(ctct.fGiaTriNamThuTu/@MenhGiaTienTe) as FGiaTriNamThuTu,
			(ctct.fGiaTriNamThuNam/@MenhGiaTienTe) as FGiaTriNamThuNam,
			(ctct.fGiaTriBoTri/@MenhGiaTienTe) as FGiaTriBoTri,
			ctct.sGhiChu as SGhiChu,
			cast(0 as bit) as IsHangCha,
			NEWID() as Id,
			1 as LoaiParent,
			ctct.iID_NguonVonID as IIdNguonVon,
			cast(0 as float) as LuyKeVonNSQPDaBoTri,
			cast(0 as float) as LuyKeVonNSQPDeNghiBoTri,
			cast(0 as float) as TongLuyKe,
			null as DNgayQuyetDinh,
			cast(0 as float) as FHanMucDauTuDP,
			cast(0 as float) as FHanMucDauTuNN,
			cast(0 as float) as FHanMucDauTuOrther,
			cast(0 as float) as FHanMucDauTuQP,
			'' as SSoQuyetDinh
		from 
			f_loai_cong_trinh_get_list_childrent(@lct) dmlct
		left join
			VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet ctct
		on
			dmlct.iID_LoaiCongTrinh = ctct.iID_LoaiCongTrinhID
		left join
			VDT_DM_DonViThucHienDuAn dv
		on ctct.iID_DonViQuanLyID = dv.iID_DonVi
		left join 
			NguonNganSach ns
		on ctct.iID_NguonVonID = ns.iID_MaNguonNganSach
		where
			ctct.iID_KeHoach5NamID = @Id
			--and ctct.IsStatus = 2
			--and ctct.iID_ParentModified is null
			and ctct.iID_NguonVonID in (select * from dbo.splitstring(@IdNguonVon))) as tbl
			order by tbl.IdLoaiCongTrinh, tbl.Loai
	end
	else
	if(@type = 2)
	begin
		select 
			dmlct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			dmlct.iID_Parent as IdLoaiCongTrinhParent,
			dmlct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
			5 as Loai,
			ctct.iID_KeHoach5NamID,
			ctct.sTen as STenDuAn,
			dv.sTenDonVi as STenDonVi,
			vndx.iID_MaDonViQuanLy as IdMaDonViQuanLy,
			ctct.SDiaDiem as SDiaDiem,
			(cast(ctct.IGiaiDoanTu as nvarchar) + ' - ' + cast(ctct.IGiaiDoanDen as nvarchar)) as SThoiGianThucHien,
			(ctct.fHanMucDauTu/@MenhGiaTienTe) as FHanMucDauTu,
			ns.sTen as STenNguonVon,
			--((ctct.fGiaTriKeHoach + ctct.fGiaTriNamThuNhat + ctct.fGiaTriNamThuHai + ctct.fGiaTriNamThuBa + ctct.fGiaTriNamThuTu + ctct.fGiaTriNamThuNam + ctct.fGiaTriBoTri)/@MenhGiaTienTe) as FTongSoNhuCau,
			((ctct.fGiaTriNamThuNhat + ctct.fGiaTriNamThuHai + ctct.fGiaTriNamThuBa + ctct.fGiaTriNamThuTu + ctct.fGiaTriNamThuNam + ctct.fGiaTriBoTri)/@MenhGiaTienTe) as FTongSoNhuCau,
			((ctct.fGiaTriNamThuNhat + ctct.fGiaTriNamThuHai + ctct.fGiaTriNamThuBa + ctct.fGiaTriNamThuTu + ctct.fGiaTriNamThuNam)/@MenhGiaTienTe) as FTongSo,
			(ctct.fGiaTriNamThuNhat/@MenhGiaTienTe) as FGiaTriNamThuNhat,
			(ctct.fGiaTriNamThuHai/@MenhGiaTienTe) as FGiaTriNamThuHai,
			(ctct.fGiaTriNamThuBa/@MenhGiaTienTe) as FGiaTriNamThuBa,
			(ctct.fGiaTriNamThuTu/@MenhGiaTienTe) as FGiaTriNamThuTu,
			(ctct.fGiaTriNamThuNam/@MenhGiaTienTe) as FGiaTriNamThuNam,
			(ctct.fGiaTriBoTri/@MenhGiaTienTe) as FGiaTriBoTri,
			ctct.sGhiChu as SGhiChu,
			cast(0 as bit) as IsHangCha,
			2 as LoaiParent,
			ctct.iID_NguonVonID as IIdNguonVon,
			cast(0 as float) as LuyKeVonNSQPDaBoTri,
			cast(0 as float) as LuyKeVonNSQPDeNghiBoTri,
			cast(0 as float) as TongLuyKe,
			null as DNgayQuyetDinh,
			cast(0 as float) as FHanMucDauTuDP,
			cast(0 as float) as FHanMucDauTuNN,
			cast(0 as float) as FHanMucDauTuOrther,
			cast(0 as float) as FHanMucDauTuQP,
			'' as SSoQuyetDinh
		  into #tmp
		from 
		  f_loai_cong_trinh_get_list_childrent(@lct) dmlct
		left join
		  VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet ctct
		on
		  dmlct.iID_LoaiCongTrinh = ctct.iID_LoaiCongTrinhID
		left join
			VDT_DM_DonViThucHienDuAn dv
		on ctct.iID_DonViQuanLyID = dv.iID_DonVi
		left join 
			NguonNganSach ns
		on ctct.iID_NguonVonID = ns.iID_MaNguonNganSach
		inner join VDT_KHV_KeHoach5Nam_DeXuat vndx
		on ctct.iID_KeHoach5NamID = vndx.Id
		where
		  ctct.iID_KeHoach5NamID in (select * from dbo.splitstring(@Id))
		  and ctct.iID_NguonVonID in (select * from dbo.splitstring(@IdNguonVon))

		select 
			khct.IdLoaiCongTrinh, 
			khct.IdLoaiCongTrinhParent, 
			khct.SMaLoaiCongTrinh, 
			4 as Loai, 
			khct.iID_KeHoach5NamID, 
			dv.sTenDonVi as STenDuAn,
			'' as STenDonVi,
			dv.iID_MaDonVi as IdMaDonViQuanLy,
			'' as SDiaDiem,
			'' as SThoiGianThucHien,
			Sum(khct.FHanMucDauTu) as FHanMucDauTu,
			'' as STenNguonVon,
			Sum(khct.FTongSoNhuCau) as FTongSoNhuCau,
			Sum(khct.FTongSo) as FTongSo,
			Sum(khct.FGiaTriNamThuNhat) as FGiaTriNamThuNhat,
			Sum(khct.FGiaTriNamThuHai) as FGiaTriNamThuHai,
			Sum(khct.FGiaTriNamThuBa) as FGiaTriNamThuBa,
			Sum(khct.FGiaTriNamThuTu) as FGiaTriNamThuTu,
			Sum(khct.FGiaTriNamThuNam) as FGiaTriNamThuNam,
			Sum(khct.FGiaTriBoTri) as FGiaTriBoTri,
			'' as GhiChu,
			cast(1 as bit) as IsHangCha,
			khct.LoaiParent,
			khct.IIdNguonVon as IIdNguonVon,
			cast(0 as float) as LuyKeVonNSQPDaBoTri,
			cast(0 as float) as LuyKeVonNSQPDeNghiBoTri,
			cast(0 as float) as TongLuyKe,
			null as DNgayQuyetDinh,
			cast(0 as float) as FHanMucDauTuDP,
			cast(0 as float) as FHanMucDauTuNN,
			cast(0 as float) as FHanMucDauTuOrther,
			cast(0 as float) as FHanMucDauTuQP,
			'' as SSoQuyetDinh
		  into #tmpDuAn
		  from #tmp khct
		  left join DonVi dv on khct.IdMaDonViQuanLy = dv.iID_MaDonVi and dv.iNamLamViec = @iNamLamViec
		  group by khct.IdLoaiCongTrinh, khct.IdLoaiCongTrinhParent, khct.SMaLoaiCongTrinh, khct.Loai, khct.iID_KeHoach5NamID, dv.sTenDonVi, dv.iID_MaDonVi, khct.IsHangCha,khct.LoaiParent, khct.IIdNguonVon

		select tbl_sum.* from (

			select
				lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
				lct.iID_Parent as IdLoaiCongTrinhParent,
				lct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
				3 as Loai,
				null as iID_KeHoach5NamID,
				lct.sTenLoaiCongTrinh as STenDuAn,
				'' as STenDonVi,
				'' as IdMaDonViQuanLy,
				'' as SDiaDiem,
				'' as SThoiGianThucHien,
				cast(0 as float) as FHanMucDauTu,
				'' as STenNguonVon,
				cast(0 as float) as FTongSoNhuCau,
				cast(0 as float) as FTongSo,
				cast(0 as float) as FGiaTriNamThuNhat,
				cast(0 as float) as FGiaTriNamThuHai,
				cast(0 as float) as FGiaTriNamThuBa,
				cast(0 as float) as FGiaTriNamThuTu,
				cast(0 as float) as FGiaTriNamThuNam,
				cast(0 as float) as FGiaTriBoTri,
				'' as SGhiChu,
				cast(1 as bit) as IsHangCha,
				case
					when lct.iID_Parent is null then 0 else 1
				end LoaiParent,
				cast(0 as int) as IIdNguonVon,
				cast(0 as float) as LuyKeVonNSQPDaBoTri,
				cast(0 as float) as LuyKeVonNSQPDeNghiBoTri,
				cast(0 as float) as TongLuyKe,
				null as DNgayQuyetDinh,
				cast(0 as float) as FHanMucDauTuDP,
				cast(0 as float) as FHanMucDauTuNN,
				cast(0 as float) as FHanMucDauTuOrther,
				cast(0 as float) as FHanMucDauTuQP,
				'' as SSoQuyetDinh
			from f_loai_cong_trinh_get_list_childrent(@lct) lct

			union all

			select * from #tmp
			union all
			select * from #tmpDuAn
			) as tbl_sum
			order by tbl_sum.IdLoaiCongTrinh, tbl_sum.iID_KeHoach5NamID, tbl_sum.Loai

		drop table #tmp
		drop table #tmpDuAn
	end
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_index]    Script Date: 08/09/2022 6:33:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[sp_vdt_thongtri_index]
@iIdLoaiThongTri uniqueidentifier,
@openFromPheDuyetThanhToan int

AS
BEGIN
	SELECT tbl.Id,sMaThongTri,dNgayThongTri,iNamThongTri,sNguoiLap,sTruongPhong,sThuTruongDonVi,sMaNguonVon,iID_DonViID,iID_MaDonViID,tbl.sMoTa,sUserCreate,dDateCreate,sUserUpdate,dDateUpdate,sUserDelete,dDateDelete,
	tbl.sMaLoaiCongTrinh,iID_LoaiThongTriID,iID_NhomQuanLyID,bIsCanBoDuyet,bIsDuyet,bThanhToan,ISNULL(tbl.ILoaiThongTri, 1) as ILoaiThongTri, dv.sTenDonVi as sTenDonVi, NULL as dNgayLapGanNhat, lct.sTenLoaiCongTrinh, tbl.INamNganSach
	FROM VDT_ThongTri as tbl
	LEFT JOIN DonVi as dv on tbl.iID_DonViID = dv.iID_DonVi
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on tbl.sMaLoaiCongTrinh = lct.sMaLoaiCongTrinh
	WHERE dDateDelete IS NULL AND tbl.iID_LoaiThongTriID = @iIdLoaiThongTri
	-- thông tri quyết toán
	and (@openFromPheDuyetThanhToan = 2 
	or (
		-- thông tri mở phê duyệt thanh toán -> thanh toán or tạm ứng
		(@openFromPheDuyetThanhToan = 1 and iLoaiThongTri in (1,2))
		or 
		-- thông tri mở từ quản lý thông tri -> kinh phí or cấp hợp thức
		(@openFromPheDuyetThanhToan = 0 and iLoaiThongTri in (3,4))
	))
	ORDER BY dDateCreate DESC
END
;
GO
