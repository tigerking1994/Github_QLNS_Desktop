/****** Object:  StoredProcedure [dbo].[sp_nh_chenhlechtigia_index]    Script Date: 8/1/2023 3:55:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_chenhlechtigia_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_chenhlechtigia_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_chenhlechtigia_index]    Script Date: 8/1/2023 3:55:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_nh_chenhlechtigia_index] 
@iDDonVi uniqueidentifier,
@iDChuongTrinh uniqueidentifier,
@iDHopDong uniqueidentifier,
@iIDDuAn uniqueidentifier

AS  
BEGIN
	Create table #Temp
	(
		ID uniqueidentifier,
		sTen nvarchar(MAX),
		fTienKHTTBQPCapUSD float,
		fTienKHTTBQPCapVND float,
		fTienTheoDuAnUSD float,
		fTienTheoDuAnVND float,
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
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT 
		NEWID() AS ID,
		(tt.sSoDeNghi +
			' - ' + (case when tt.iLoaiNoiDungChi = 1 then N'Chi ngoại tệ' else N'Chi trong nước' end) + 
			' - ' + (case when tt.iLoaiDeNghi = 1 then N'Cấp kinh phí'
						when tt.iLoaiDeNghi = 2 then N'Tạm ứng'
						else N'Thanh toán' end)
		) as sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,
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
		WHERE (@iDHopDong IS NULL OR (tt.iID_HopDongID IS NOT NULL AND tt.iID_HopDongID = @iDHopDong))
	) AS A

	-- Insert hợp đồng
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT 
		hd.ID,
		hd.sTenHopDong as sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,
		hd.fGiaTriUSD AS fTienTheoHopDongUSD,
		hd.fGiaTriVND AS fTienTheoHopDongVND,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) AS fKinhPhiDuocCapChoCDTUSD,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) AS fKinhPhiDuocCapChoCDTVND,
		SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD, 0)) AS fKinhPhiDaThanhToanUSD,
		SUM(ISNULL(temp.fKinhPhiDaThanhToanVND, 0)) AS fKinhPhiDaThanhToanVND,
		ISNULL(hd.fGiaTriUSD, 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) AS fTiGiaCLHopDongVsCDTUSD,
		ISNULL(hd.fGiaTriVND, 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) AS fTiGiaCLHopDongVsCDTVND,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD, 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD, 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND, 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanVND, 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		hd.iID_KHTongThe_NhiemVuChiID AS IDParent,
		CAST(1 AS bit) AS IsHangCha
		FROM NH_DA_HopDong AS hd
		INNER JOIN #Temp as temp on hd.ID = temp.IDParent
		WHERE (@iDHopDong IS NULL OR hd.ID = @iDHopDong) AND (@iDChuongTrinh IS NULL OR hd.iID_KHTongThe_NhiemVuChiID = @iDChuongTrinh)
		GROUP BY hd.ID, hd.sTenHopDong, hd.fGiaTriUSD, hd.fGiaTriVND, hd.iID_KHTongThe_NhiemVuChiID
	) AS B

	-- Insert chương trình
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT
		nvc.ID,
		dmnvc.sTenNhiemVuChi AS sTen,
		nvc.fGiaTriKH_BQP AS fTienKHTTBQPCapUSD,
		nvc.fGiaTriKH_BQP_VND AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,		
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
		WHERE (@iDDonVi IS NULL OR nvc.iID_DonViThuHuongID = @iDDonVi) AND (@iDChuongTrinh IS NULL OR nvc.ID = @iDChuongTrinh)
		GROUP BY nvc.ID,dmnvc.sTenNhiemVuChi,nvc.fGiaTriKH_BQP,nvc.fGiaTriKH_BQP_VND,nvc.iID_DonViThuHuongID
	) AS C

	-- Insert đơn vị
	insert into #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha)
	select ID, sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha
	from(
		SELECT DISTINCT
		dv.iID_DonVi AS ID,
		dv.sTenDonVi AS sTen,
		NULL AS fTienKHTTBQPCapUSD,
		NULL AS fTienKHTTBQPCapVND,
		NULL AS fTienTheoDuAnUSD,
		NULL AS fTienTheoDuAnVND,
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
		
		  -- Insert dự án
INSERT INTO #Temp (ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND,fTienTheoDuAnUSD, fTienTheoDuAnVND,fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND ,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND ,IDParent,IsHangCha)
SELECT ID,
		 sTen,
		fTienKHTTBQPCapUSD,
		fTienKHTTBQPCapVND,
		fTienTheoDuAnUSD,
		fTienTheoDuAnVND,
		fTienTheoHopDongUSD,
		fTienTheoHopDongVND,
		fKinhPhiDuocCapChoCDTUSD,
		fKinhPhiDuocCapChoCDTVND ,
		fKinhPhiDaThanhToanUSD,
		fKinhPhiDaThanhToanVND,
		fTiGiaCLHopDongVsCDTUSD,
		fTiGiaCLHopDongVsCDTVND,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		fTiGiaCLKinhPhiDuocCapVsGiaiNganVND ,
		IDParent,
		IsHangCha from
	(SELECT duan.ID AS ID,
		 duan.sTenDuAn AS sTen,
		 NULL AS fTienKHTTBQPCapUSD,
		 NULL AS fTienKHTTBQPCapVND,
		 duan.fUSD AS fTienTheoDuAnUSD,
		 duan.fVND AS fTienTheoDuAnVND,
		 NULL AS fTienTheoHopDongUSD,
		 NULL AS fTienTheoHopDongVND,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD,
		 0)) AS fKinhPhiDuocCapChoCDTUSD,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND,
		 0)) AS fKinhPhiDuocCapChoCDTVND,
		 SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD,
		 0)) AS fKinhPhiDaThanhToanUSD,
		 SUM(ISNULL(temp.fKinhPhiDaThanhToanVND,
		 0)) AS fKinhPhiDaThanhToanVND,
		 ISNULL(duan.fUSD,
		 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD,
		 0)) AS fTiGiaCLHopDongVsCDTUSD,
		 ISNULL(duan.fVND,
		 0) - SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND,
		 0)) AS fTiGiaCLHopDongVsCDTVND,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTUSD,
		 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanUSD,
		 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,
		 SUM(ISNULL(temp.fKinhPhiDuocCapChoCDTVND,
		 0)) - SUM(ISNULL(temp.fKinhPhiDaThanhToanVND,
		 0)) AS fTiGiaCLKinhPhiDuocCapVsGiaiNganVND,
		 duan.iID_KHTT_NhiemVuChiID  AS IDParent,
		 CAST(1 AS bit) AS IsHangCha
	FROM NH_DA_DuAn AS duan
	INNER JOIN #Temp AS temp
		ON duan.iID_KHTT_NhiemVuChiID = temp.ID
	WHERE (@iIDDuAn IS NULL
			OR duan.ID = @iIDDuAn)
			AND (@iDChuongTrinh IS NULL
			OR duan.iID_KHTT_NhiemVuChiID = @iDChuongTrinh)
	GROUP BY  duan.ID, duan.sTenDuAn, duan.fUSD, duan.fVND, duan.iID_KHTT_NhiemVuChiID ) AS E

	;WITH  #Tree(ID,sTen,fTienKHTTBQPCapUSD,fTienKHTTBQPCapVND, fTienTheoDuAnUSD, fTienTheoDuAnVND, fTienTheoHopDongUSD,fTienTheoHopDongVND,fKinhPhiDuocCapChoCDTUSD,fKinhPhiDuocCapChoCDTVND
			,fKinhPhiDaThanhToanUSD,fKinhPhiDaThanhToanVND,fTiGiaCLHopDongVsCDTUSD,fTiGiaCLHopDongVsCDTVND,fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD,fTiGiaCLKinhPhiDuocCapVsGiaiNganVND
			,IDParent,IsHangCha,position)
	AS (
		select temp.ID,temp.sTen,
			temp.fTienKHTTBQPCapUSD,temp.fTienKHTTBQPCapVND,
			temp.fTienTheoDuAnUSD, temp.fTienTheoDuAnVND,
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
			child.fTienTheoDuAnUSD,child.fTienTheoDuAnVND,
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
		fTienTheoDuAnUSD, fTienTheoDuAnVND,
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
;
GO
