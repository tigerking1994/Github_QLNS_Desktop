/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 4/22/2024 1:00:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 4/22/2024 1:00:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

	DECLARE @fTienDauNam FLOAT;
	SELECT @fTienDauNam = SUM(0.1*(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD))
	FROM BH_DTT_BHXH_ChungTu_ChiTiet as ctct
	JOIN BH_DTT_BHXH_ChungTu as ct
	ON ctct.iID_DTT_BHXH = ct.iID_DTT_BHXH
	WHERE
	(ctct.sXauNoiMa like '9020001-010-011-0001%' or ctct.sXauNoiMa like '9020002-010-011-0001%')
	AND ctct.iNamLamViec = @NamLamViec
	AND ct.iLoaiDuToan = 1


SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangChaDuToan bHangCha,
	A.bHangChaDuToan as isHangCha,
	B.ID,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTongTien,
	B.fTienHienVat,
	CASE WHEN A.sXauNoiMa = '9010004' AND B.iLoaiDotNhanPhanBo = 1 THEN @fTienDauNam ELSE B.fTienTuChi END as fTienTuChi,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi,
	 B.dNgaySua,
	 B.dNgayTao,
	 B.sNguoiSua,
	 B.sNguoiTao
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN ( SELECT ctct.*, CT.iLoaiDotNhanPhanBo
					FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
				LEFT JOIN BH_DTC_DuToanChiTrenGiao CT ON ctct.iID_DTC_DuToanChiTrenGiao=CT.ID 
				WHERE ctct.iID_DTC_DuToanChiTrenGiao = @iDNdtctg
					 and ct.iID_MaDonVi=@IIDDonVi 
					 And CT.iNamLamViec=@NamLamViec) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split( @sLns))
		AND A.iNamlamViec=@NamLamViec
		AND A.bHangChaDuToan IS NOT NULL
	order by A.sXauNoiMa
END
;
;
;
GO