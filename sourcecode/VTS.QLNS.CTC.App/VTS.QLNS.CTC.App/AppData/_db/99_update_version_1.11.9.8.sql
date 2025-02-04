/****** Object:  StoredProcedure [dbo].[sp_nh_qt_thong_tri_quyet_toan_index]    Script Date: 23/09/2022 6:43:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_qt_thong_tri_quyet_toan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_qt_thong_tri_quyet_toan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khtongthe_nhiemvuchi_bydonvi]    Script Date: 23/09/2022 6:43:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_khtongthe_nhiemvuchi_bydonvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_khtongthe_nhiemvuchi_bydonvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_du_an_info_report]    Script Date: 23/09/2022 6:43:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_du_an_info_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_du_an_info_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]    Script Date: 23/09/2022 6:43:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chenhlechtigia_index]    Script Date: 23/09/2022 6:43:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chenhlechtigia_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chenhlechtigia_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_taisan_by_id_chungtutaisan]    Script Date: 23/09/2022 6:43:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_taisan_by_id_chungtutaisan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_taisan_by_id_chungtutaisan]
GO

/****** Object:  StoredProcedure [dbo].[sp_get_taisan_by_id_chungtutaisan]    Script Date: 23/09/2022 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_get_taisan_by_id_chungtutaisan]
	-- Add the parameters for the stored procedure here
	 @iID_ChungTuTaiSan uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select 
	 qtts.ID as 	Id		
	,qtts.dNgayBatDauSuDung		as	DNgayBatDauSuDung		
	,qtts.iID_DuAnID			as	IIDDuAnId
	,qtts.iID_KHTT_NhiemVuChiID		as IIDKHTTNhiemVuChiId
	,qtts.iID_LoaiTaiSanID		as IIDLoaiTaiSan
	,qtts.iTrangThai			as	 ITrangThai
	,qtts.SMaTaiSan			as SMaTaiSan
	,qtts.sMoTaTaiSan		as SMoTaTaiSan
	,qtts.sTenTaiSan			as STenTaiSan
	,qtts.iID_ChungTuTaiSanID	as	 IIDChungTuTaiSanId
	,qtts.iLoaiTaiSan		as ILoaiTaiSan
	,qtts.fSoLuong		as FSoLuong
	,qtts.sDonViTinh		as SDonViTinh
	,qtts.fNguyenGia as FNguyenGia
	,qtts.iTinhTrangSuDung as ITinhTrangSuDung
	,qtts.iID_MaDonViID as IIDMaDonViId
	,qtts.iID_HopDongID as IIDHopDongId,
	lts.sTenLoaiTaiSan as STenLoaiTaiSan, 
	dv.sTenDonVi as STenDonVi,
	da.sTenDuAn as STenDuAn,
	hd.sTenHopDong as STenHopDong
	from nh_qt_taisan qtts 
left join nh_qt_chungtutaisan ctts on qtts.iID_ChungTuTaiSanID = ctts.ID
left join NH_DM_LoaiTaiSan lts on qtts.iID_LoaiTaiSanID=lts.ID
left join DonVi dv on qtts.iID_MaDonViID=dv.iID_DonVi
left join NH_DA_DuAn da on qtts.iID_DuAnID=da.ID 
left join NH_DA_HopDong hd on qtts.iID_HopDongID=hd.ID
where (@iID_ChungTuTaiSan is null or ctts.ID = @iID_ChungTuTaiSan)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chenhlechtigia_index]    Script Date: 23/09/2022 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_chenhlechtigia_index] 
@iDDonVi uniqueidentifier,
@iDChuongTrinh uniqueidentifier,
@iDHopDong uniqueidentifier
AS
BEGIN
	Create table #Temp
	(
		ID uniqueidentifier,
		sTen nvarchar(MAX),
		fTienKHTTBQPCapUSD float,
		fTienKHTTBQPCapVND float,
		fTienTheoHopDongUSD float,
		fTienTheoHopDongVND float,
		fKinhPhiDuocCapChoCDTUSD float,
		fKinhPhiDuocCapChoCDTVND float,
		fKinhPhiDaThanhToanUSD float,
		fKinhPhiDaThanhToanVND float,
		fTiGiaCLHopDongVsCDTUSD float,
		fTiGiaCLHopDongVsCDTVND float,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD float,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganVND float,
		IDParent uniqueidentifier,
		IsHangCha bit
	)	
	
	-- Insert đề nghị thanh toán
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT 
		NEWID() AS ID,
		(tt.sSoDeNghi +
			' - ' + (case when tt.iLoaiNoiDungChi = 1 then N'Chi ngoại tệ' else N'Chi trong nước' end) + 
			' - ' + (case when tt.iLoaiDeNghi = 1 then N'Cấp kinh phí'
						when tt.iLoaiDeNghi = 2 then N'Thanh toán'
						else N'Tạm ứng' end)
		) as sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoHopDongUSD,
		NULL AS fTienTheoHopDongVND,
		(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDuocCapChoCDTUSD 
			else (case when tt.iLoaiDeNghi = 1 then chitiet.fKinhPhiDuocCapChoCDTUSD else null end) end)
		 AS fKinhPhiDuocCapChoCDTUSD,
		(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDuocCapChoCDTVND 
			else (case when tt.iLoaiDeNghi = 1 then chitiet.fKinhPhiDuocCapChoCDTVND else null end) end)
		AS fKinhPhiDuocCapChoCDTVND,
		(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDaThanhToanUSD 
			else (case when tt.iLoaiDeNghi = 2 or tt.iLoaiDeNghi = 3 then chitiet.fKinhPhiDaThanhToanUSD else null end) end)
		AS fKinhPhiDaThanhToanUSD,
		(case when tt.iCoQuanThanhToan = 1 then chitiet.fKinhPhiDaThanhToanVND 
			else (case when tt.iLoaiDeNghi = 2 or tt.iLoaiDeNghi = 3 then chitiet.fKinhPhiDaThanhToanVND else null end) end)
		AS fKinhPhiDaThanhToanVND,
		NULL AS fTiGiaCLHopDongVsCDTUSD,
		NULL AS fTiGiaCLHopDongVsCDTVND,
		NULL AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		NULL AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		(case when tt.iID_HopDongID is null or tt.iID_HopDongID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) then tt.iID_KHTongTheID else tt.iID_HopDongID end) as IDParent,
		CAST(0 AS bit) AS IsHangCha
		FROM NH_TT_ThanhToan AS tt 
		INNER JOIN (
			SELECT 
			Sum(ISNULL(fPheDuyetCapKyNay_USD, 0)) AS fKinhPhiDuocCapChoCDTUSD, 
			Sum(ISNULL(fPheDuyetCapKyNay_VND, 0)) AS fKinhPhiDuocCapChoCDTVND, 
			Sum(ISNULL(fPheDuyetCapKyNay_USD, 0)) AS fKinhPhiDaThanhToanUSD,
			Sum(ISNULL(fPheDuyetCapKyNay_VND, 0)) AS fKinhPhiDaThanhToanVND,
			tt_ct.iID_DeNghiThanhToanID
			FROM NH_TT_ThanhToan_ChiTiet AS tt_ct
			GROUP BY tt_ct.iID_DeNghiThanhToanID
		) AS chitiet ON chitiet.iID_DeNghiThanhToanID = tt.ID
		WHERE (@iDHopDong IS NULL OR tt.iID_HopDongID = @iDHopDong)
	) AS A

	-- Insert hợp đồng
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT 
		hd.ID,
		hd.sTenHopDong as sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		hd.fGiaTriHopDongUSD AS fTienTheoHopDongUSD,
		hd.fGiaTriHopDongVND AS fTienTheoHopDongVND,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) AS fKinhPhiDuocCapChoCDTUSD,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) AS fKinhPhiDuocCapChoCDTVND,
		SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD, 0)) AS fKinhPhiDaThanhToanUSD,
		SUM(ISNULL(temp.fKinhPhiDaThanhToanVND, 0)) AS fKinhPhiDaThanhToanVND,
		ISNULL(hd.fGiaTriHopDongUSD, 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) AS fTiGiaCLHopDongVsCDTUSD,
		ISNULL(hd.fGiaTriHopDongVND, 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) AS fTiGiaCLHopDongVsCDTVND,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD, 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanVND, 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		hd.iID_KHTongThe_NhiemVuChiID AS IDParent,
		CAST(1 AS bit) AS IsHangCha
		FROM NH_DA_HopDong AS hd
		INNER JOIN #Temp as temp on hd.ID = temp.IDParent
		WHERE (@iDChuongTrinh IS NULL OR hd.iID_KHTongThe_NhiemVuChiID = @iDChuongTrinh)
		GROUP BY hd.ID, hd.sTenHopDong, hd.fGiaTriHopDongUSD, hd.fGiaTriHopDongVND, hd.iID_KHTongThe_NhiemVuChiID
	) AS B

	-- Insert chương trình
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT
		nvc.ID,
		dmnvc.sTenNhiemVuChi AS sTen,
		nvc.fGiaTriKH_BQP AS fTienKHTTBQPCapUSD,
		nvc.fGiaTriKH_BQP_VND AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoHopDongUSD,
		NULL AS fTienTheoHopDongVND,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) AS fKinhPhiDuocCapChoCDTUSD,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) AS fKinhPhiDuocCapChoCDTVND,
		SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD, 0)) AS fKinhPhiDaThanhToanUSD,
		SUM(ISNULL(temp.fKinhPhiDaThanhToanVND, 0)) AS fKinhPhiDaThanhToanVND,
		NULL AS fTiGiaCLHopDongVsCDTUSD,
		NULL AS fTiGiaCLHopDongVsCDTVND,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD, 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanVND, 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		nvc.iID_DonViThuHuongID AS IDParent,
		CAST(1 AS bit) AS IsHangCha
		FROM NH_KHTongThe_NhiemVuChi AS nvc
		LEFT JOIN NH_DM_NhiemVuChi AS dmnvc ON dmnvc.ID = nvc.iID_NhiemVuChiID
		INNER JOIN #Temp AS temp ON nvc.ID = temp.IDParent
		WHERE (@iDDonVi IS NULL OR nvc.iID_DonViThuHuongID = @iDDonVi)
		GROUP BY nvc.ID,dmnvc.sTenNhiemVuChi,nvc.fGiaTriKH_BQP,nvc.fGiaTriKH_BQP_VND,nvc.iID_DonViThuHuongID
	) AS C

	-- Insert đơn vị
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT DISTINCT
		dv.iID_DonVi AS ID,
		dv.sTenDonVi AS sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoHopDongUSD,
		NULL AS fTienTheoHopDongVND,
		NULL AS fKinhPhiDuocCapChoCDTUSD,
		NULL AS fKinhPhiDuocCapChoCDTVND,
		NULL AS fKinhPhiDaThanhToanUSD,
		NULL AS fKinhPhiDaThanhToanVND,
		NULL AS fTiGiaCLHopDongVsCDTUSD,
		NULL AS fTiGiaCLHopDongVsCDTVND,
		NULL AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		NULL AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		NULL AS IDParent,
		CAST(1 AS bit) AS IsHangCha
		FROM DonVi AS dv
		INNER JOIN #Temp as temp on dv.iID_DonVi = temp.IDParent
		WHERE (@iDDonVi IS NULL OR dv.iID_DonVi = @iDDonVi)
	) AS D
		
	;WITH  #Tree(ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha,position)
	AS (
		select temp.ID,temp.sTen,
			temp.fTienKHTTBQPCapUSD,temp.fTienKHTTBQPCapVND,
			temp.fTienTheoHopDongUSD,temp.fTienTheoHopDongVND,
			temp.fKinhPhiDuocCapChoCDTUSD,temp.fKinhPhiDuocCapChoCDTVND,
			temp.fKinhPhiDaThanhToanUSD,temp.fKinhPhiDaThanhToanVND,
			temp.fTiGiaCLHopDongVsCDTUSD,temp.fTiGiaCLHopDongVsCDTVND,
			temp.fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,temp.fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
			temp.IDParent,temp.IsHangCha,
			CAST(ROW_NUMBER() OVER(ORDER BY temp.sTen) AS NVARCHAR(MAX)) AS position
		from #Temp as temp
		where temp.IDParent is null
		UNION ALL
		select
			child.ID,child.sTen,
			child.fTienKHTTBQPCapUSD,child.fTienKHTTBQPCapVND,
			child.fTienTheoHopDongUSD,child.fTienTheoHopDongVND,
			child.fKinhPhiDuocCapChoCDTUSD,child.fKinhPhiDuocCapChoCDTVND,
			child.fKinhPhiDaThanhToanUSD,child.fKinhPhiDaThanhToanVND,
			child.fTiGiaCLHopDongVsCDTUSD,child.fTiGiaCLHopDongVsCDTVND,
			child.fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,child.fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
			child.IDParent,child.IsHangCha,
			CONCAT(parent.position,'.',CAST(ROW_NUMBER() OVER(ORDER BY child.sTen) AS NVARCHAR(MAX))) AS position
		from #Temp as child 
		inner join #Tree as parent on parent.ID = child.IDParent
	)
	SELECT * INTO #Data FROM #Tree;
	
	SELECT position,sTen,cast('/' + replace(position, '.', '/') + '/' as hierarchyid) AS sort,
		fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,
		fTienTheoHopDongUSD,fTienTheoHopDongVND,
		fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND,
		fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,
		fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		IsHangCha
	FROM #Data dt
	ORDER BY sort;

	DROP TABLE #Temp;
	DROP TABLE #Data;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]    Script Date: 23/09/2022 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1]
	@IdDuAn nvarchar(max),
	@NgayBatDau datetime,
	@NgayKetThuc datetime
AS
BEGIN
select tt.ID, tt.sSoDeNghi, tt.dNgayDeNghi, 
concat(DM_ChuDauTu.iID_MaDonVi,'-',DM_ChuDauTu.sTenDonVi) as sChuDauTu, 
nt.sTenNhaThau as TenNhaThau, 
tt.iLoaiNoiDungChi, 
tt.iCoQuanThanhToan, 
tt.iLoaiDeNghi, 
(
	select 
			distinct mlns.sXauNoiMa 
		from NS_MucLucNganSach mlns
		where 
			(mlns.iID = pdttct.iID_MucLucNganSachID or mlns.iID_MLNS = pdttct.iID_MLNS_ID)
) as Mlns,
tthd.id as IdHopDong,
tthd.sSoHopDong as SoHopDong, 
tt.fTongDeNghi_USD,
tt.fTongDeNghi_VND,
tt.fTongPheDuyet_USD,
tt.fTongPheDuyet_VND
from NH_TT_ThanhToan tt 
left join DM_ChuDauTu on tt.iID_ChuDauTuID  = DM_ChuDauTu.iID_DonVi
left join NH_DA_QDDauTu  qddt on tt.iID_NhaThauID  = qddt.ID 
left join NH_DM_NhaThau  nt on tt.iID_NhaThauID  = nt.ID 
left join NH_TT_ThanhToan_ChiTiet pdttct on pdttct.iID_DeNghiThanhToanID = tt.ID
left join NH_DA_HopDong tthd on tthd.ID = tt.iID_HopDongID
where  (@IdDuAn IS NULL  OR tt.iID_DuAnID = @IdDuAn)
	AND (@NgayBatDau is null or tt.dNgayDeNghi >= @NgayBatDau)
	AND (@NgayKetThuc is null or tt.dNgayDeNghi <= @NgayKetThuc)
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_get_du_an_info_report]    Script Date: 23/09/2022 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_get_du_an_info_report]
	@MaDonVi nvarchar(500),
	@YearOfWork int
AS
BEGIN
	SET NOCOUNT ON;
		select 
	da.*,
	cdt.sTenDonVi as sTenChuDauTu,
	da_dautu.dNgayQuyetDinh as dNgayQuyetDinhDauTu,
	da_dautu.sSoQuyetDinh as sSoQuyetDinhDauTu,
	cap_pd.sTen as sPhanCap,
	tong_muc_dau_tu.TongMucDauTuEur,
	tong_muc_dau_tu.TongMucDauTuUsd, 
	tong_muc_dau_tu.TongMucDauTuVnd, 
	tong_muc_dau_tu.TongMucDauTuNgoaiTeKhac, 
	da_dautu.sSoQuyetDinh as SoQuyetDinh,
	da_dautu.dNgayQuyetDinh as NgayThangNam
	FROM Nh_DA_DuAn da
	left join 
	(
		select 
		SUM(dautu_chiphi.fGiaTriEUR) as TongMucDauTuEur
		,SUM(dautu_chiphi.fGiaTriUSD) as TongMucDauTuUsd
		,SUM(dautu_chiphi.fGiaTriVND) as TongMucDauTuVnd
		,SUM(dautu_chiphi.fGiaTriNgoaiTeKhac) as TongMucDauTuNgoaiTeKhac
		,dautu.iID_DuAnID
		FROM Nh_DA_DuAn da 
		
		left join Nh_DA_QDDauTu dautu on da.id = dautu.iID_DuAnID
		left join Nh_DA_QDDauTu_ChiPhi dautu_chiphi on dautu.id = dautu_chiphi.iID_QDDauTuID
		group by dautu.iID_DuAnID
	) tong_muc_dau_tu on tong_muc_dau_tu.iID_DuAnID = da.id
	inner join DM_ChuDauTu cdt on da.iID_ChuDauTuID = cdt.iId_Donvi
	inner join NH_DM_PhanCapPheDuyet cap_pd on da.iID_CapPheDuyetID = cap_pd.ID
	left join Nh_DA_QDDauTu da_dautu on da.id = da_dautu.iID_DuAnID
	where da.iID_MaDonViQuanLy = @MaDonVi and da_dautu.bIsActive = 1;
END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_nh_khtongthe_nhiemvuchi_bydonvi]    Script Date: 23/09/2022 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_khtongthe_nhiemvuchi_bydonvi]
	@iDDonVi uniqueidentifier
AS BEGIN

	SELECT
	    tt_nvc.ID AS Id,
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
	WHERE @iDDonVi IS NULL OR tt_nvc.iID_DonViThuHuongID = @iDDonVi
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_qt_thong_tri_quyet_toan_index]    Script Date: 23/09/2022 6:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_qt_thong_tri_quyet_toan_index]
AS
BEGIN
	SELECT
		TTQT.ID AS ID,
		TTQT.sSoThongTri,
		TTQT.dNgayLap,
		TTQT.iID_KHTT_NhiemVuChiID,
		TTQT.iID_DonViID,
		TTQT.iID_MaDonVi,
		TTQT.iNamThongTri,
		TTQT.iLoaiThongTri,
		TTQT.iLoaiNoiDungChi,
		TTQT.fThongTri_USD,
		TTQT.fThongTri_VND,
		DM_NVC.sTenNhiemVuChi,
		CONCAT(DV.iID_MaDonVi, IIF(DV.sTenDonVi IS NULL OR DV.sTenDonVi = '', '', CONCAT(' - ', DV.sTenDonVi))) AS sTenDonVi,
		IIF(TTQT.iLoaiThongTri = 2, N'Thông tri giảm quyết toán', N'Thông tri quyết toán') AS sLoaiThongTri,
		IIF(TTQT.iLoaiNoiDungChi = 2, N'Chi bằng nội tệ', N'Chi bằng ngoại tệ') AS sLoaiNoiDungChi
	FROM NH_QT_ThongTriQuyetToan TTQT
	LEFT JOIN NH_KHTongThe_NhiemVuChi NVC ON TTQT.iID_KHTT_NhiemVuChiID = NVC.ID
	LEFT JOIN NH_DM_NhiemVuChi DM_NVC ON NVC.iID_NhiemVuChiID = DM_NVC.ID
	LEFT JOIN DonVi DV ON TTQT.iID_MaDonVi = DV.iID_MaDonVi AND TTQT.iNamThongTri = DV.iNamLamViec
END
GO
