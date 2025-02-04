/****** Object:  StoredProcedure [dbo].[sp_vdt_thanhtoanchiphi_detail_by_id]    Script Date: 22/06/2022 8:47:40 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thanhtoanchiphi_detail_by_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thanhtoanchiphi_detail_by_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thanhtoanchiphi_detail]    Script Date: 22/06/2022 8:47:40 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thanhtoanchiphi_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thanhtoanchiphi_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_find_goithau_by_hopdong]    Script Date: 22/06/2022 8:47:40 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_hopdong_find_goithau_by_hopdong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_hopdong_find_goithau_by_hopdong]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet]    Script Date: 22/06/2022 8:47:40 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_get_skt_chungtuchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]    Script Date: 22/06/2022 8:47:40 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]
GO
/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 22/06/2022 8:47:40 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_luong_ntn]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_luong_ntn]
GO
/****** Object:  UserDefinedFunction [dbo].[f_luong_ntn]    Script Date: 22/06/2022 8:47:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 04/05/2022
-- Description:	Tính năm thâm niên
-- =============================================
CREATE FUNCTION [dbo].[f_luong_ntn]
(
	@NgayNN DATETIME,
	@NgayXN DATETIME,
	@NgayTN DATETIME,
	@ThangTNN int,
	@Thang int,
	@Nam int
)
RETURNS int
AS
BEGIN
	DECLARE @NamThamNien int SET @NamThamNien = 0
	DECLARE @monthDiff int SET @monthDiff = 0
	DECLARE @monthDiff2 int SET @monthDiff2 = 0

	IF (@NgayNN IS NOT NULL)
	BEGIN
		IF (@NgayXN IS NULL AND @NgayTN IS NULL)
		BEGIN
			SET @monthDiff = (@Nam - YEAR(@NgayNN) * 12) + @Thang - MONTH(@NgayNN) + 1 + @ThangTNN
			IF(@monthDiff / 12 >= 1)
				BEGIN
					SET @NamThamNien = @monthDiff / 12
				END
			ELSE
				BEGIN
					SET @NamThamNien = @monthDiff / 12 - 1
				END
		END

		ELSE
		BEGIN
			IF (@NgayTN IS NULL)
			BEGIN
				SET @monthDiff = (@Nam - YEAR(@NgayNN) * 12) + @Thang - MONTH(@NgayNN) + 1 + @ThangTNN
				IF(@monthDiff / 12 >= 1)
					BEGIN
						SET @NamThamNien = @monthDiff / 12
					END
				ELSE
					BEGIN
						SET @NamThamNien = @monthDiff / 12 - 1
					END
			END

			ELSE
			BEGIN
				DECLARE @Lan1 int SET @Lan1 = 12 * (YEAR(@NgayXN) - YEAR(@NgayNN)) + MONTH(@NgayXN) - MONTH(@NgayNN) + 1
				
				IF (@Lan1 < 6)
				BEGIN
					set @monthDiff2 = 12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + @ThangTNN + 1
					IF(@monthDiff2 / 12 >= 1)
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12
						END
					ELSE
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12 - 1
						END
				END

				ELSE IF (@Lan1 >= 6 AND @Lan1 <= 12)
				BEGIN
					set @monthDiff2 = 12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN + 1
					IF(@monthDiff2 / 12 >= 1)
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12
						END
					ELSE
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12 - 1
						END
				END

				ELSE 
				BEGIN
					set @monthDiff2 = 12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + 1 + @ThangTNN + @Lan1
					IF(@monthDiff2 / 12 >= 1)
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12
						END
					ELSE
						BEGIN
							SET @NamThamNien = @monthDiff2 / 12 - 1
						END
				END
			END
		END
	END
	RETURN @NamThamNien
END
--BEGIN
--	DECLARE @NamThamNien int SET @NamThamNien = 0

--	IF (@NgayNN IS NOT NULL)
--	BEGIN
--		IF (@NgayXN IS NULL AND @NgayTN IS NULL)
--		BEGIN
--			SET @NamThamNien = (12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN) / 12
--		END

--		ELSE
--		BEGIN
--			IF (@NgayTN IS NULL)
--			BEGIN
--				SET @NamThamNien = (12 * (YEAR(@NgayXN) - YEAR(@NgayNN)) + MONTH(@NgayXN) - MONTH(@NgayNN) + @ThangTNN) / 12
--			END

--			ELSE
--			BEGIN
--				DECLARE @Lan1 int SET @Lan1 = 12 * (YEAR(@NgayXN) - YEAR(@NgayNN)) + MONTH(@NgayXN) - MONTH(@NgayNN)
				
--				IF (@Lan1 < 6)
--				BEGIN
--					SET @NamThamNien = (12 * (@Nam - YEAR(@NgayTN)) + @Thang - MONTH(@NgayTN) + @ThangTNN) / 12
--				END

--				ELSE 
--				BEGIN
--					DECLARE @ThoiGianNgoaiQD int SET @ThoiGianNgoaiQD = 12 * (YEAR(@NgayTN) - YEAR(@NgayXN)) + MONTH(@NgayTN) - MONTH(@NgayXN)

--					IF (@ThoiGianNgoaiQD <= 12)
--					BEGIN
--						SET @NamThamNien = (12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN) / 12
--					END

--					ELSE
--					BEGIN
--						SET @NamThamNien = (@Lan1 + 12 * (@Nam - YEAR(@NgayNN)) + @Thang - MONTH(@NgayNN) + @ThangTNN) / 12
--					END
--				END
--			END
--		END
--	END
--	RETURN @NamThamNien
--END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]    Script Date: 22/06/2022 8:47:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]
	@IdDonVi nvarchar(max)
AS
BEGIN
	SELECT DISTINCT dt.iID_DuAnID INTO #tmpDuAnKHTH
	FROM VDT_KHV_KeHoach5Nam as tbl
	INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as dt on tbl.iID_KeHoach5NamID = dt.iID_KeHoach5NamID
	WHERE tbl.bActive = 1

	SELECT
			duan.iID_DuAnID AS IDDuAnID,
			duan.sMaDuAn as SMaDuAn,
			duan.sTenDuAn AS STenDuAn,
			duan.sDiaDiem AS SDiaDiem,
			CAST(duan.sKhoiCong AS int) AS IGiaiDoanTu,
			CAST(duan.sKetThuc AS int) AS IGiaiDoanDen,
			duan.fHanMucDauTu AS FHanMucDauTu,
			donvi.iID_DonVi AS IIdDonViId,
			donvi.iID_MaDonVi AS IIDMaDonVi,
			donvi.sTenDonVi AS STenDonVi,
			null AS IIDLoaiCongTrinhID,
			'' AS STenLoaiCongTrinh,
			null AS IIDNguonVonID,
			'' AS STenNguonVon
		FROM VDT_DA_DuAn duan
		INNER JOIN #tmpDuAnKHTH as khth on duan.iID_DuAnID = khth.iID_DuAnID
		LEFT JOIN VDT_DM_DonViThucHienDuAn donvi
			ON duan.iID_DonViThucHienDuAnID  = donvi.iID_DonVi
		WHERE
			1=1
			--AND duan.sTrangThaiDuAn = 'THUC_HIEN'
			AND duan.bIsKetThuc IS NULL
			AND iID_MaDonViThucHienDuAnID = @IdDonVi

	DROP TABLE #tmpDuAnKHTH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_get_skt_chungtuchitiet]    Script Date: 22/06/2022 8:47:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_get_skt_chungtuchitiet]
	@YearOfWork int,
	@iID_MaBQuanLy nvarchar(200)
	
AS
BEGIN
	SET NOCOUNT ON;

	if(@iID_MaBQuanLy = '0')
		begin
			select distinct mlskt.sKyHieu sSKT_KyHieu, mlskt.sM, mlskt.sNG_Cha, mlskt.sNG 
			into #NS_MLSKT_MLNS_map_tem
			from NS_MLSKT_MLNS map
			inner join NS_MucLucNganSach ns
			on ns.sXauNoiMa = map.sNS_XauNoiMa
			inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
			where ns.sLNS in (select sLNS from  NS_MucLucNganSach)
			and ns.iNamLamViec = @YearOfWork
			and map.iNamLamViec = @YearOfWork
			and mlskt.iNamLamViec = @YearOfWork

			select distinct sSKT_KyHieu from  #NS_MLSKT_MLNS_map_tem 
			union all
			select distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem on mlskt.sNG_Cha = #NS_MLSKT_MLNS_map_tem.sNG_Cha 
			where mlskt.sNG is null or  mlskt.sNG  = '' and  mlskt.iNamLamViec = @YearOfWork
			union all
			select  distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem on mlskt.sM = #NS_MLSKT_MLNS_map_tem.sM 
			where (mlskt.sNG is null or mlskt.sNG ='')  and (mlskt.sNG_Cha is null or   mlskt.sNG_Cha = '') 
			and mlskt.iNamLamViec = @YearOfWork
			order by sSKT_KyHieu

			drop table #NS_MLSKT_MLNS_map_tem
		end
		
	if(@iID_MaBQuanLy != '0')
		begin
			select distinct mlskt.sKyHieu sSKT_KyHieu, mlskt.sM, mlskt.sNG_Cha, mlskt.sNG 
			into #NS_MLSKT_MLNS_map_tem_
			from NS_MLSKT_MLNS map
			inner join NS_MucLucNganSach ns
			on ns.sXauNoiMa = map.sNS_XauNoiMa
			inner join NS_SKT_MucLuc mlskt  on  map.sSKT_KyHieu = mlskt.sKyHieu 
			where ns.sLNS in (select sLNS from  NS_MucLucNganSach dm where dm.iID_MaBQuanLy = @iID_MaBQuanLy)
			and ns.iNamLamViec = @YearOfWork
			and map.iNamLamViec = @YearOfWork
			and mlskt.iNamLamViec = @YearOfWork

			select distinct sSKT_KyHieu from  #NS_MLSKT_MLNS_map_tem_ 
			union all
			select distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem_ on mlskt.sNG_Cha = #NS_MLSKT_MLNS_map_tem_.sNG_Cha 
			where mlskt.sNG is null or  mlskt.sNG  = '' and  mlskt.iNamLamViec = @YearOfWork
			union all
			select  distinct  mlskt.sKyHieu from  NS_SKT_MucLuc  mlskt inner join #NS_MLSKT_MLNS_map_tem_ on mlskt.sM = #NS_MLSKT_MLNS_map_tem_.sM 
			where (mlskt.sNG is null or mlskt.sNG ='')  and (mlskt.sNG_Cha is null or   mlskt.sNG_Cha = '') 
			and mlskt.iNamLamViec = @YearOfWork
			order by sSKT_KyHieu

			drop table #NS_MLSKT_MLNS_map_tem_
		end
END




GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_find_goithau_by_hopdong]    Script Date: 22/06/2022 8:47:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_vdt_hopdong_find_goithau_by_hopdong] 
	-- Add the parameters for the stored procedure here
	@IdHopDong uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select t1.iID_GoiThauID, t2.sMaGoiThau as sMaGoiThau, t2.sTenGoiThau, t2.fTienTrungThau, (t2.fTienTrungThau - ISNULL(t3.fgiatri, 0)) 
	as FGiaTriConLai, t1.Id as IdHopDongGoiThauNhaThau, t1.fGiaTri, t1.fGiaTriTrungThau, t1.fGiaTri as FGiaTriGoiThau, t1.iID_NhaThauID as NhaThauId,
    t1.fGiaTriHopDong, t2.sThoiGianThucHien
	from VDT_DA_HopDong_GoiThau_NhaThau t1
	left join VDT_DA_GOITHAU t2 on t1.iID_GoiThauID = t2.iID_GoiThauID
	left join
    (select s1.iid_goithauid, SUM(s1.fgiatri) as fgiatri from VDT_DA_HopDong_GoiThau_NhaThau s1
	JOIN vdt_da_tt_hopdong s2 on s1.iid_hopdongid = s2.id
	where s1.iID_HopDongID != @IdHopDong and s2.bactive = 1
	group by s1.iid_goithauid) t3
    on t1.iID_GoiThauID = t3.iID_GoiThauID
	where iID_HopDongID = @IdHopDong
END


GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thanhtoanchiphi_detail]    Script Date: 22/06/2022 8:47:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_thanhtoanchiphi_detail]
@iIdDuToanId uniqueidentifier
AS
BEGIN
	--SELECT dt.iID_DanhMuc_DT_chi as IIdNoiDungChi, dm.sTenDuToanChi as SNoiDungChi, CAST(0 as float) as FGiaTriDeNghi, null as SGhiChu
	--FROM VDT_KHV_PhanBoVon_ChiPhi as tbl
	--INNER JOIN VDT_KHV_PhanBoVon_ChiPhi_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVon_ChiPhi_ID
	--INNER JOIN VDT_DM_DuToanChi as dm on dt.iID_DanhMuc_DT_chi = dm.iID_DuToanChi
	--WHERE tbl.Id = @iIdDuToanId

	select pbvcpct.Id, pbvcpct.iID_DanhMuc_DT_chi as IIdNoiDungChi, pbvcpct.sNoiDung as SNoiDungChi, pbvcpct.fGiaTriPheDuyet as FGiaTriPheDuyet, CAST(0 as float) as FGiaTriDeNghi, null as SGhiChu, pbvcpct.sMaOrder, pbvcpct.iId_Parent as IIdParent
	from VDT_KHV_PhanBoVon_ChiPhi pbvcp
	inner join VDT_KHV_PhanBoVon_ChiPhi_ChiTiet as pbvcpct on pbvcp.Id = pbvcpct.iID_PhanBoVon_ChiPhi_ID
	where pbvcp.Id = @iIdDuToanId
	order by pbvcpct.sMaOrder
END

GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thanhtoanchiphi_detail_by_id]    Script Date: 22/06/2022 8:47:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_thanhtoanchiphi_detail_by_id]
@iId uniqueidentifier,
@iIdDuToanId uniqueidentifier

AS
BEGIN
	--SELECT dt.iID_NoiDungChi as IIdNoiDungChi, dm.sTenDuToanChi as SNoiDungChi, dt.FGiaTriDeNghi, dt.SGhiChu
	--FROM VDT_TT_DeNghiThanhToan_ChiPhi_ChiTiet as dt
	--INNER JOIN VDT_DM_DuToanChi as dm on dt.iID_NoiDungChi = dm.iID_DuToanChi
	--WHERE dt.iID_DeNghiThanhToan_ChiPhiID = @iId

	--select ttcpct.iID_NoiDungChi as IIdNoiDungChi, pbvcpct.sNoiDung as SNoiDungChi, pbvcpct.fGiaTriPheDuyet as FGiaTriPheDuyet, ttcpct.fGiaTriDeNghi, ttcpct.sGhiChu, pbvcpct.sMaOrder
	--from VDT_TT_DeNghiThanhToan_ChiPhi_ChiTiet as ttcpct
	--inner join VDT_TT_DeNghiThanhToan_ChiPhi as ttcp on ttcp.Id = ttcpct.iID_DeNghiThanhToan_ChiPhiID
	--inner join VDT_KHV_PhanBoVon_ChiPhi as pbvcp on pbvcp.Id = ttcp.iID_PhanBoVon_ChiPhi_ID
	--inner join VDT_KHV_PhanBoVon_ChiPhi_ChiTiet as pbvcpct on ttcpct.iID_NoiDungChi = pbvcpct.iID_DanhMuc_DT_chi and pbvcp.Id = pbvcpct.iID_PhanBoVon_ChiPhi_ID
	--where ttcpct.iID_DeNghiThanhToan_ChiPhiID = @iId
	--order by pbvcpct.sMaOrder


	--select ttcpct.Id, ttcpct.iID_NoiDungChi as IIdNoiDungChi, pbvcpct.sNoiDung as SNoiDungChi, pbvcpct.fGiaTriPheDuyet as FGiaTriPheDuyet, ttcpct.fGiaTriDeNghi, ttcpct.sGhiChu, pbvcpct.sMaOrder, pbvcpct.iId_Parent as IIdParent
	--from VDT_TT_DeNghiThanhToan_ChiPhi_ChiTiet as ttcpct
	--inner join VDT_TT_DeNghiThanhToan_ChiPhi as ttcp on ttcp.Id = ttcpct.iID_DeNghiThanhToan_ChiPhiID
	--inner join VDT_KHV_PhanBoVon_ChiPhi as pbvcp on pbvcp.Id = ttcp.iID_PhanBoVon_ChiPhi_ID
	--inner join VDT_KHV_PhanBoVon_ChiPhi_ChiTiet as pbvcpct on ttcpct.iID_NoiDungChi = pbvcpct.Id and pbvcp.Id = pbvcpct.iID_PhanBoVon_ChiPhi_ID
	--where ttcpct.iID_DeNghiThanhToan_ChiPhiID = @iId
	--order by pbvcpct.sMaOrder


	select pbvcpct.Id, ttcpct.iID_NoiDungChi as IIdNoiDungChi, pbvcpct.sNoiDung as SNoiDungChi, pbvcpct.fGiaTriPheDuyet as FGiaTriPheDuyet, ttcpct.fGiaTriDeNghi, ttcpct.sGhiChu, pbvcpct.sMaOrder, pbvcpct.iId_Parent as IIdParent
	from VDT_KHV_PhanBoVon_ChiPhi_ChiTiet as pbvcpct
	inner join VDT_KHV_PhanBoVon_ChiPhi as pbvcp on pbvcp.Id = pbvcpct.iID_PhanBoVon_ChiPhi_ID
	left join (select * from VDT_TT_DeNghiThanhToan_ChiPhi_ChiTiet as ttcpct where ttcpct.iID_DeNghiThanhToan_ChiPhiID = @iId) as ttcpct
	on ttcpct.iID_NoiDungChi = pbvcpct.Id
	where pbvcp.Id = @iIdDuToanId
	order by pbvcpct.sMaOrder

END


GO
