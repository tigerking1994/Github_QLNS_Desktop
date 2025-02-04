/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]    Script Date: 12/4/2023 1:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]    Script Date: 12/4/2023 1:53:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]
	@listTenDonVi ntext,
	@namLamViec int,
	@LNS nvarchar(200),
	@IDLoaichi uniqueidentifier,
	@Dvt int
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),
FTongTienKeHoachThucHienNamNay float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , 
FTongTienKeHoachThucHienNamNay 
)
	SELECT 
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   FTongTienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))/ @Dvt
FROM
  (SELECT 
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTienKeHoachThucHienNamNay
   FROM BH_KHC_K_ChiTiet ctct
   LEFT JOIN BH_KHC_K ct ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS IN  (SELECT * FROM f_split(@LNS))
   WHERE ct.iNamChungTu = @namLamViec
		AND ct.iIDLoaiChi=@IDLoaichi
	) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		dt_dv.iLoai;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
IsNull(dt.FTongTienKeHoachThucHienNamNay, 0) FTongTienKeHoachThucHienNamNay
FROM @DataKhoi dt
where dt.idDonVi in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;


GO
