/****** Object:  StoredProcedure [dbo].[sp_nh_kehoachtongthe_index]    Script Date: 11/22/2023 7:17:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_kehoachtongthe_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_kehoachtongthe_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_kehoachtongthe_index]    Script Date: 11/22/2023 7:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--ALTER TABLE NH_KHTongThe
--ADD iGiaiDoanTu_TTCP int,
--    iGiaiDoanDen_TTCP int,
--	iGiaiDoanTu_BQP int,
--	iGiaiDoanDen_BQP int;

CREATE PROC [dbo].[sp_nh_kehoachtongthe_index]
AS
BEGIN
	WITH ListAllTongThe AS (
		SELECT NEWID() AS id, NULL AS iNamKeHoach,
			NULL AS iGiaiDoanTu, NULL AS iGiaiDoanDen,
			NULL AS iGiaiDoanTu_TTCP, NULL AS iGiaiDoanDen_TTCP,
			NULL AS iGiaiDoanTu_BQP, NULL AS iGiaiDoanDen_BQP,
			NULL AS iIdParentId,
			NULL AS iIdParentAdjustId,
			NULL AS iIdGocId, t.sSoKeHoachTTCP,
			NULL AS dNgayKeHoachTTCP,
			NULL AS sMoTaChiTietKhttcp,
			SUM(t.fTongGiaTri_KHTTCP) fTongGiaTriKhttcp,
			NULL AS sSoKeHoachBQP, NULL AS dNgayKeHoachBQP,
			NULL AS sMoTaChiTietKhbqp,
			SUM(t.fTongGiaTri_KHBQP) AS fTongGiaTriKhbqp,
			SUM(t.fTongGiaTri_KHBQP_VND) AS fTongGiaTriKhbqpVnd,
			Max(t.dNgayTao) AS dNgayTao, NULL AS sNguoiTao,
			NULL AS dNgaySua, NULL AS sNguoiSua,
			NULL AS dNgayXoa, NULL AS sNguoiXoa,
			NULL AS bIsActive, NULL AS bIsGoc, NULL AS bIsKhoa,
			NULL AS iLanDieuChinh, NULL AS iLoai,
			NULL AS TotalFiles,
			NULL AS DieuChinhTu,
			NULL AS IdParentTongThe
		FROM NH_KHTongThe AS t
		GROUP BY t.sSoKeHoachTTCP
		
		UNION ALL

		SELECT  t.ID id, t.iNamKeHoach iNamKeHoach,
				t.iGiaiDoanTu, t.iGiaiDoanDen,
				t.iGiaiDoanTu_TTCP, t.iGiaiDoanDen_TTCP,
				t.iGiaiDoanTu_BQP, t.iGiaiDoanDen_BQP,
				t.iID_ParentID iIdParentId,
				t.iID_ParentAdjustID iIdParentAdjustId,
				t.iID_GocID iIdGocId, t.sSoKeHoachTTCP,
				t.dNgayKeHoachTTCP,
				t.sMoTaChiTiet_KHTTCP sMoTaChiTietKhttcp,
				t.fTongGiaTri_KHTTCP fTongGiaTriKhttcp,
				t.sSoKeHoachBQP, t.dNgayKeHoachBQP,
				t.sMoTaChiTiet_KHBQP sMoTaChiTietKhbqp,
				t.fTongGiaTri_KHBQP fTongGiaTriKhbqp,
				t.fTongGiaTri_KHBQP_VND fTongGiaTriKhbqpVnd,
				t.dNgayTao, t.sNguoiTao,
				t.dNgaySua, t.sNguoiSua,
				t.dNgayXoa, t.sNguoiXoa,
				t.bIsActive, t.bIsGoc, t.bIsKhoa,
				t.iLanDieuChinh, t.iLoai,
				(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 402 AND ObjectId = t.ID) AS TotalFiles,
				CASE WHEN t.iID_ParentAdjustID is null THEN '' ELSE ( SELECT khpr.sSoKeHoachBQP FROM NH_KHTongThe khpr WHERE khpr.ID = t.iID_ParentAdjustID ) END DieuChinhTu,
				t.ID AS IdParentTongThe
		FROM NH_KHTongThe AS t
	)
	SELECT * FROM ListAllTongThe Order by dNgayTao DESC;
END;
;
;
GO
