/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_2]    Script Date: 09/08/2022 6:43:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet]    Script Date: 09/08/2022 6:43:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dc_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dc_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_donvi]    Script Date: 09/08/2022 6:43:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_lns_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_muclucDanhMuc]    Script Date: 09/08/2022 6:43:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_muclucDanhMuc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_muclucDanhMuc]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns]    Script Date: 09/08/2022 6:43:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_donVi]    Script Date: 09/08/2022 6:43:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_donVi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_donVi]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_donVi]    Script Date: 09/08/2022 6:43:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_donVi]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin

	insert into DonVi 
	 ([iID_DonVi]
	  ,[bCoNSNganh]
	  ,[dNgaySua]
	  ,[dNgayTao]
	  ,[iID_MaDonVi]
	  ,[iTrangThai]
	  ,[iID_Parent]
	  ,[IsPhongBan]
	  ,[iKhoi]
	  ,[sKyHieu]
	  ,[iLoai]
	  ,[LoaiNganSach]
	  ,[Log]
	  ,[sMoTa]
	  ,[iNamLamViec]
	  ,[sNguoiSua]
	  ,[sNguoiTao]
	  ,[Tag]
	  ,[sTenDonVi]
	  ,[iCapDonVi])
	select 
		   newid()
	  ,[bCoNSNganh]
	  ,[dNgaySua]
	  ,[dNgayTao]
	  ,[iID_MaDonVi]
	  ,[iTrangThai]
	  ,[iID_Parent]
	  ,[IsPhongBan]
	  ,[iKhoi]
	  ,[sKyHieu]
	  ,[iLoai]
	  ,[LoaiNganSach]
	  ,[Log]
	  ,[sMoTa]
	  ,@dest
	  ,[sNguoiSua]
	  ,@userCreate
	  ,[Tag]
	  ,[sTenDonVi]
	  ,[iCapDonVi] from DonVi c 
	  where c.iNamLamViec= @source and (c.iID_MaDonVi not in (select b.iID_MaDonVi from DonVi b where b.iNamLamViec = @dest)) -- and c.iLoai <> '0' 
		

	update d
	set
		d.bCoNSNganh = s.bCoNSNganh,
		d.dNgaySua = getdate(),
		d.sNguoiSua = @userCreate,
		d.iTrangThai = s.iTrangThai,
		d.iID_Parent = s.iID_Parent,
		d.IsPhongBan = s.IsPhongBan,
		d.iKhoi = s.iKhoi,
		d.sKyHieu = s.sKyHieu,
		d.iLoai = s.iLoai,
		d.LoaiNganSach = s.LoaiNganSach,
		d.[Log] = s.[Log],
		d.sMoTa = s.sMoTa,
		d.[Tag] = s.[Tag],
		d.sTenDonVi = s.sTenDonVi,
		d.iCapDonVi = s.iCapDonVi

	from
	DonVi as s
	inner join DonVi as d
	on s.iID_MaDonVi = d.iID_MaDonVi
	where s.iNamLamViec = @source and d.iNamLamViec = @dest
	
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns]    Script Date: 09/08/2022 6:43:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_mlns]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	insert into NS_MucLucNganSach 
		  ([iID]
		  ,[bDuPhong]
		  ,[bHangCha]
		  ,[bHangChaDuToan]
		  ,[bHangChaQuyetToan]
		  ,[bHangMua]
		  ,[bHangNhap]
		  ,[bHienVat]
		  ,[bNgay]
		  ,[bPhanCap]
		  ,[bSoNguoi]
		  ,[bTonKho]
		  ,[bTuChi]
		  ,[sChiTietToi]
		  ,[dNgaySua]
		  ,[dNgayTao]
		  ,[iLoai]
		  ,[iLock]
		  ,[iTrangThai]
		  ,[iID_MaDonVi]
		  ,[iID_MaBQuanLy]
		  ,[sK]
		  ,[sL]
		  ,[sLNS]
		  ,[Log]
		  ,[sM]
		  ,[iID_MLNS]
		  ,[iID_MLNS_Cha]
		  ,[sMoTa]
		  ,[iNamLamViec]
		  ,[sNG]
		  ,[sCPChiTietToi]
		  ,[sDuToanChiTietToi]
		  ,[sNguoiSua]
		  ,[sNguoiTao]
		  ,[sNhapTheoTruong]
		  ,[sQuyetToanChiTietToi]
		  ,[Tag]
		  ,[sTM]
		  ,[sTNG]
		  ,[sTNG1]
		  ,[sTNG2]
		  ,[sTNG3]
		  ,[sTTM]
		  ,[sXauNoiMa])
		select 
		   newid()
		  ,[bDuPhong]
		  ,[bHangCha]
		  ,[bHangChaDuToan]
		  ,[bHangChaQuyetToan]
		  ,[bHangMua]
		  ,[bHangNhap]
		  ,[bHienVat]
		  ,[bNgay]
		  ,[bPhanCap]
		  ,[bSoNguoi]
		  ,[bTonKho]
		  ,[bTuChi]
		  ,[sChiTietToi]
		  ,[dNgaySua]
		  ,getdate()
		  ,[iLoai]
		  ,[iLock]
		  ,[iTrangThai]
		  ,[iID_MaDonVi]
		  ,[iID_MaBQuanLy]
		  ,[sK]
		  ,[sL]
		  ,[sLNS]
		  ,[Log]
		  ,[sM]
		  ,[iID_MLNS]
		  ,null
		  ,[sMoTa]
		  ,@dest
		  ,[sNG]
		  ,null
		  ,null
		  ,[sNguoiSua]
		  ,@userCreate
		  ,[sNhapTheoTruong]
		  ,null
		  ,[Tag]
		  ,[sTM]
		  ,[sTNG]
		  ,[sTNG1]
		  ,[sTNG2]
		  ,[sTNG3]
		  ,[sTTM]
		  ,[sXauNoiMa] from NS_MucLucNganSach c 
	  where c.iNamLamViec= @source and c.sXauNoiMa not in (select b.sXauNoiMa from NS_MucLucNganSach b where b.iNamLamViec = @dest)
	-- Cap nhat lai gia tri
	update d
	set
		d.[sMoTa] = s.sMoTa,
		d.[sDuToanChiTietToi] = case when (d.sChiTietToi is null or d.sChiTietToi = '') then s.sDuToanChiTietToi else d.sDuToanChiTietToi end,
		d.[sQuyetToanChiTietToi] = case when (d.sQuyetToanChiTietToi is null or d.sQuyetToanChiTietToi = '') then s.sQuyetToanChiTietToi else d.sQuyetToanChiTietToi end,
		d.[sCPChiTietToi] = case when(d.sCPChiTietToi is null or d.sCPChiTietToi = '') then s.sCPChiTietToi else s.sCPChiTietToi end
	from
	NS_MucLucNganSach as s
	inner join NS_MucLucNganSach as d
	on s.iID_MLNS = d.iID_MLNS
	where s.iNamLamViec = @source and d.iNamLamViec = @dest
	-- cap nhat parent
	update NS_MucLucNganSach
	set
	iID_MLNS_Cha = dbo.f_findParentMucLucNganSach(@dest,sXauNoiMa)
	where iNamLamViec = @dest
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_muclucDanhMuc]    Script Date: 09/08/2022 6:43:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_muclucDanhMuc]
	@source int,
	@dest int,
	@userCreate nvarchar(200),
	@type nvarchar(200),
	@isCopyGiaTri int
as
begin
	insert into DanhMuc
	([iID_DanhMuc]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[iID_MaDanhMuc]
      ,[iNamLamViec]
      ,[iThuTu]
      ,[iTrangThai]
      ,[Log]
      ,[NganSachNganh]
      ,[sGiaTri]
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTen]
      ,[sType]
      ,[Tag]
	)
	select
		newid() 
      ,null
      ,GETDATE()
      ,[iID_MaDanhMuc]
      ,@dest
      ,[iThuTu]
      ,[iTrangThai]
      ,[Log]
      ,[NganSachNganh]
      ,case when(@isCopyGiaTri = 1) then b.[sGiaTri] else '' end
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTen]
      ,[sType]
      ,[Tag]
	  from DanhMuc b where b.iNamLamViec = @source and b.sType= @type and (b.iID_MaDanhMuc not in (select iID_MaDanhMuc from DanhMuc c where c.iNamLamViec = @dest and c.sType= @type))

	 --UPDATE
	update d
	set
		d.dNgaySua = getdate(),
		d.iThuTu = s.iThuTu,
		d.iTrangThai = s.iTrangThai,
		d.[Log] = s.[Log],
		d.NganSachNganh = s.NganSachNganh,
		--d.sGiaTri = s.sGiaTri,
		d.sMoTa = s.sMoTa,
		d.sNguoiSua = @userCreate,
		d.sType = @type,
		d.[Tag] = s.[Tag]

	from
	DanhMuc as s
	inner join DanhMuc as d
	on s.iID_MaDanhMuc = d.iID_MaDanhMuc
	where s.iNamLamViec = @source and d.iNamLamViec = @dest and d.sType = @type and s.sType = @type

end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_lns_rpt_thongtri_donvi]    Script Date: 09/08/2022 6:43:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_cp_lns_rpt_thongtri_donvi] 
	@NamLamViec int,
    @CapPhatId nvarchar(100),
	@DonViId nvarchar(max),
	@Dvt int,
	@ILoaiNganSach int
AS
BEGIN
SET NOCOUNT ON;
	SELECT ctct.iID_MaDonVi AS MaDonVi,
		dv.sTenDonVi AS TenDonVi,
		SUM(fTuChi) / @Dvt AS CapPhat
	FROM NS_CP_ChungTuChiTiet ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	INNER JOIN 
		(SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = 2022) ns 
	ON ctct.iID_MLNS = ns.iID_MLNS
	WHERE iID_CTCapPhat = @CapPhatId 
		AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@DonViId))
		AND (@ILoaiNganSach = -1 OR ns.iLoaiNganSach = @ILoaiNganSach)
	GROUP BY ctct.iID_MaDonVi, dv.sTenDonVi
	ORDER BY ctct.iID_MaDonVi
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet]    Script Date: 09/08/2022 6:43:45 PM ******/
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
			case 
			when mlns.sLNS = '1040200' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangNhap, 0) 
			when mlns.sLNS = '1040300' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangMua, 0) 
			else isnull(dtNsNam.TuChi, 0)
			end
			as DuToanNganSachNam,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_2]    Script Date: 09/08/2022 6:43:45 PM ******/
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
	@iCoQuanThanhToan int,
	@thanhToanId uniqueidentifier
AS
BEGIN
	DECLARE @uIdEmpty uniqueidentifier = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER)
	DECLARE @fLuyKeTTKLHTNN float
	DECLARE @fLuyKeTTKLHTTN float
	DECLARE @fLuyKeTUChuaThuHoiCheDoNN float
	DECLARE @fLuyKeTUChuaThuHoiCheDoTN float
	DECLARE @fLuyKeTUChuaThuHoiUngTruocNN float
	DECLARE @fLuyKeTUChuaThuHoiUngTruocTN float
	DECLARE @indexSoDeNghi int = (select cast(substring(sSoDeNghi, 8, 12) as int) from VDT_TT_DeNghiThanhToan where Id = @thanhToanId)

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
			  tbl.dNgayPheDuyet < @NgayDeNghi 
			  -- so de nghi: DN-xxxx
			  or (tbl.dNgayPheDuyet = @NgayDeNghi and cast(substring(tbl.sSoDeNghi, 8, 12) as int) < @indexSoDeNghi and iNamKeHoach = @NamKeHoach )
			  or (tbl.dNgayPheDuyet = @NgayDeNghi and iNamKeHoach < @NamKeHoach)
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
