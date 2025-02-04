/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet_tao_tonghop]    Script Date: 9/18/2024 5:38:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dc_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dc_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet]    Script Date: 9/18/2024 5:38:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dc_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dc_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet]    Script Date: 9/18/2024 5:38:04 PM ******/
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
			--iID_MaDonVi, 
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
			--and ((iLoai = 1 AND @CountDonViCon > 0) OR (iLoai = 0 AND @CountDonViCon = 0))
			and iLoaiChungTu = @loaiChungTu
			and bKhoa = 1
			and ISNULL(sSoQuyetDinh, '') <> ''
			and cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
		--group by iID_MLNS, iID_MLNS_Cha, sXauNoiMa, iID_MaDonVi, iNamLamViec, iNamNganSach, iID_MaNguonNganSach
		group by iID_MLNS, iID_MLNS_Cha, sXauNoiMa, iNamLamViec, iNamNganSach, iID_MaNguonNganSach
	),
	tblQuyetToanDauNam AS (
		select 
			iID_MLNS, 
			iID_MLNS_Cha, 
			sLNS, 
			--iID_MaDonVi,
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
			--group by iID_MLNS, iID_MLNS_Cha, sLNS, iID_MaDonVi
			group by iID_MLNS, iID_MLNS_Cha, sLNS
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
			quyetToan.TuChi
			--quyetToan.iID_MaDonVi
		FROM (SELECT * FROM NS_MucLucNganSach where iNamLamViec = @namLamViec and sLNS in (SELECT * FROM tblMlnsQuyetToanDauNam)) mlns
		LEFT JOIN
			tblQuyetToanDauNam quyetToan
		ON mlns.iID_MLNS = quyetToan.iID_MLNS
	),
	C AS  
	(  
		SELECT T.iID_MLNS,  
				T.TuChi, 
				T.iID_MLNS AS RootID
				--T.iID_MaDonVi
		FROM tblQuyetToanDauNamWithMlns T
		UNION ALL 
		SELECT T.iID_MLNS,  
				T.TuChi, 
				C.RootID
				--T.iID_MaDonVi
		FROM tblQuyetToanDauNamWithMlns T
		INNER JOIN C
		on T.iID_MLNS_Cha = C.iID_MLNS 
	),
	tblQuyetToanDauNamData AS (
		SELECT iID_MLNS, 
			iID_MLNS_Cha, 
			sum(TuChi) AS TuChi
			--iID_MaDonVi
		FROM 
		(
			SELECT T.iID_MLNS,  
				T.iID_MLNS_Cha,
				total.TuChi
				--T.iID_MaDonVi
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
		--GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi
		GROUP BY iID_MLNS, iID_MLNS_Cha
	)

	SELECT dc.*, dv.sTenDonVi FROM (
		SELECT 
			dcctct.iID_DCCTChiTiet AS iID_DCCTChiTiet,
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
			--isnull(dcctct.iID_MaDonVi, isnull(dtNsNam.iID_MaDonVi, qtdn.iID_MaDonVi)) as iID_MaDonVi,
			dcctct.iID_MaDonVi as iID_MaDonVi,
			dcctct.sGhiChu,
			case 
				when dcctct.fDuToan is null then 
					case 
						when mlns.sLNS = '1040200' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangNhap, 0) 
						when mlns.sLNS = '1040300' then isnull(dtNsNam.TuChi, 0) + isnull(dtNsNam.HangMua, 0) 
						else isnull(dtNsNam.TuChi, 0)
					end
				else dcctct.fDuToan
				end as DuToanNganSachNam,
			isnull(dcctct.fDuKienQtDauNam, qtdn.TuChi) as DuKienQtDauNam,
			isnull(dcctct.fDuKienQtCuoiNam, 0) as DuKienQtCuoiNam,
			isnull(dcctct.fDuToanChuyenNamSau, 0) as DuToanChuyenNamSau,
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
	ON dtNsNam.sXauNoiMa = mlns.sXauNoiMa
	LEFT JOIN
		tblQuyetToanDauNamData qtdn
	ON qtdn.iID_MLNS = mlns.iID_MLNS 
	LEFT JOIN
	(select * from NS_DC_ChungTuChiTiet 
		where iNamLamViec = @namLamViec
		and iNamNganSach = @namNganSach
		and iID_MaNguonNganSach = @nguonNganSach
		and iID_DCChungTu = @chungTuId) dcctct
	on dcctct.sXauNoiMa = mlns.sXauNoiMa
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
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_chungtu_chitiet_tao_tonghop]    Script Date: 9/18/2024 5:38:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dc_chungtu_chitiet_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfBudget int,
	@BudgetSource int,
	@YearOfWork int,
	@AgencyId nvarchar(10),
	@UserName nvarchar(100)
AS
BEGIN
	INSERT INTO [dbo].[NS_DC_ChungTuChiTiet]
           ([iID_DCChungTu]
           ,[iID_MLNS]
           ,[iID_MLNS_Cha]
           ,[sXauNoiMa]
           ,[sLNS]
           ,[sL]
           ,[sK]
           ,[sM]
           ,[sTM]
           ,[sTTM]
           ,[sNG]
           ,[sTNG]
           ,[sTNG1]
           ,[sTNG2]
           ,[sTNG3]
           ,[sMoTa]
           ,[bHangCha]
           ,[iNamNganSach]
           ,[iID_MaNguonNganSach]
           ,[iNamLamViec]
           ,[iID_MaDonVi]
		   ,[fDuToan]
           ,[fDuKienQtDauNam]
           ,[fDuKienQtCuoiNam]
           ,[fDuToanChuyenNamSau]
           ,[sGhiChu]
           ,[dNgayTao]
           ,[sNguoiTao]
           ,[dNgaySua]
           ,[sNguoiSua])
     SELECT
           @VoucherId
           ,iID_MLNS
           ,iID_MLNS_Cha
           ,sXauNoiMa
           ,sLNS
           ,sL
           ,sK
           ,sM
           ,sTM
           ,sTTM
           ,sNG
           ,sTNG
           ,sTNG1
           ,sTNG2
           ,sTNG3
           ,sMoTa
           ,bHangCha
           ,@YearOfBudget
           ,@BudgetSource
           ,@YearOfWork
           ,@AgencyId
		   ,null
           ,sum(isnull(fDuKienQtDauNam, 0))
           ,sum(isnull(fDuKienQtCuoiNam, 0))
           ,sum(isnull(fDuToanChuyenNamSau, 0))
           ,null
           ,getdate()
           ,@UserName
           ,null
           ,null
	FROM NS_DC_ChungTuChiTiet WHERE iID_DcChungTu IN (SELECT * FROM f_split(@VoucherIds))
	GROUP BY iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha;

	--danh dau chung tu da tong hop
	update NS_DC_ChungTu set bDaTongHop = 1 
	where iID_DCChungTu in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
;
;
;
GO
