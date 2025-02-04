/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 12/25/2024 8:47:07 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 12/25/2024 8:47:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50),
@DotPb int

AS
BEGIN


DECLARE @fTienDauNam FLOAT;
SELECT @fTienDauNam = Round(SUM(0.1*(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD)) /1000000,0) * 1000000
FROM BH_DTT_BHXH_ChungTu_ChiTiet as ctct
JOIN BH_DTT_BHXH_ChungTu as ct
ON ctct.iID_DTT_BHXH = ct.iID_DTT_BHXH
WHERE
(ctct.sXauNoiMa like '9020001-010-011-0001%' or ctct.sXauNoiMa like '9020002-010-011-0001%')
AND ctct.iNamLamViec = @NamLamViec
AND ct.iLoaiDuToan = @DotPb;



WITH CategoryHierarchy AS
(
-- Anchor member: Start with root nodes (ParentId IS NULL)
SELECT
mlns.*,
1 AS Level,
CAST('' AS NVARCHAR(MAX)) AS Space -- Start with an empty string for root
FROM BH_DM_MucLucNganSach mlns
WHERE inamlamviec = 2024 and iID_MLNS_Cha IS NULL

UNION ALL

-- Recursive member: Join on ParentId to find children
SELECT
c.*,
ch.Level + 1 AS Level,
CAST(Space + '     ' AS NVARCHAR(MAX)) AS Space
FROM (select * from BH_DM_MucLucNganSach where inamlamviec = 2024) c
INNER JOIN CategoryHierarchy ch ON c.iID_MLNS_Cha = ch.iID_MLNS
)


SELECT
A.sLNS,
A.sTM,
A.sTTM,
A.sM,
A.sNG,
Space + A.sMoTa AS sNoiDung,
A.sXauNoiMa,
A.iID_MLNS,
A.iID_MLNS_Cha,
B.ID,
B.iID_DTC_DuToanChiTrenGiao,
A.iID_MLNS AS iID_MucLucNganSach,
B.fTongTien,
B.fTienHienVat,
CASE WHEN A.sXauNoiMa = '9010004' THEN @fTienDauNam ELSE B.fTienTuChi END as fTienTuChi,
A.sCPChiTietToi,
A.sDuToanChiTietToi,
A.iNamlamViec,
@IIDDonVi as iID_MaDonVi,
B.dNgaySua,
B.dNgayTao,
B.sNguoiSua,
B.sNguoiTao,
A.Level
FROM CategoryHierarchy AS A
LEFT JOIN ( SELECT ctct.*, CT.iLoaiDotNhanPhanBo
FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
LEFT JOIN BH_DTC_DuToanChiTrenGiao CT ON ctct.iID_DTC_DuToanChiTrenGiao=CT.ID
WHERE ctct.iID_DTC_DuToanChiTrenGiao = @iDNdtctg
and ct.iID_MaDonVi=@IIDDonVi
And CT.iNamLamViec=@NamLamViec) AS B
ON B.iID_MucLucNganSach = A.iID_MLNS
WHERE A.sLNS IN (SELECT * FROM f_split( @sLns))
AND A.iNamlamViec=@NamLamViec
--AND A.bHangChaDuToan IS NOT NULL
AND A.iTrangThai=1
order by A.sXauNoiMa
END
;
;
;
;
;
;

GO
