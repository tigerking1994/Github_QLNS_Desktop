/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpql]    Script Date: 11/20/2023 12:23:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qkpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qkpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpk]    Script Date: 11/20/2023 12:23:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qkpk]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qkpk]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkcb]    Script Date: 11/20/2023 12:23:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qkcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qkcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qbhxh]    Script Date: 11/20/2023 12:23:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_qbhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_qbhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpql]    Script Date: 11/20/2023 12:23:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkpql]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpql]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpk]    Script Date: 11/20/2023 12:23:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkpk]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpk]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkcb]    Script Date: 11/20/2023 12:23:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nkcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nkcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nbhxh]    Script Date: 11/20/2023 12:23:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_get_lns_for_nbhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_get_lns_for_nbhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nbhxh]    Script Date: 11/20/2023 12:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_nbhxh]
	@namLamViec int,
	@donVi nvarchar(max),
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ct
				WHERE ct.iID_QTC_Nam_CheDoBHXH in 
					(SELECT ID_QTC_Nam_CheDoBHXH 
					FROM BH_QTC_Nam_CheDoBHXH 
					WHERE iNamChungTu = @namLamViec 
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkcb]    Script Date: 11/20/2023 12:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_nkcb]
	@namLamViec int,
	@donVi nvarchar(max),
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ct
				WHERE ct.iID_QTC_Nam_KCB_QuanYDonVi in 
					(SELECT ID_QTC_Nam_KCB_QuanYDonVi 
					FROM BH_QTC_Nam_KCB_QuanYDonVi 
					WHERE iNamChungTu = @namLamViec 
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpk]    Script Date: 11/20/2023 12:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpk]
	@namLamViec int,
	@donVi nvarchar(max),
	@ngayChungTu datetime,
	@userName nvarchar(100),
	@LoaiChi uniqueidentifier
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Nam_KPK_ChiTiet ct
				WHERE ct.iID_QTC_Nam_KPK in 
					(SELECT ID_QTC_Nam_KPK 
					FROM BH_QTC_Nam_KPK 
					WHERE iNamChungTu = @namLamViec 
					AND iID_LoaiChi=@LoaiChi
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_nkpql]    Script Date: 11/20/2023 12:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_nkpql]
	@namLamViec int,
	@donVi nvarchar(max),
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ct
				WHERE ct.iID_QTC_Nam_KinhPhiQuanLy in 
					(SELECT ID_QTC_Nam_KinhPhiQuanLy 
					FROM BH_QTC_Nam_KinhPhiQuanLy 
					WHERE iNamLamViec = @namLamViec 
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;


select * from BH_QTC_Nam_KinhPhiQuanLy
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qbhxh]    Script Date: 11/20/2023 12:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_qbhxh]
	@namLamViec int,
	@donVi nvarchar(max),
	@Quy int,
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Quy_CheDoBHXH_ChiTiet ct
				WHERE ct.iID_QTC_Quy_CheDoBHXH in 
					(SELECT ID_QTC_Quy_CheDoBHXH 
					FROM BH_QTC_Quy_CheDoBHXH 
					WHERE iNamChungTu = @namLamViec 
					AND iQuyChungTu=@Quy
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayChungTu as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkcb]    Script Date: 11/20/2023 12:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_qkcb]
	@namLamViec int,
	@donVi nvarchar(max),
	@Quy int,
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Quy_KCB_ChiTiet ct
				WHERE ct.iID_QTC_Quy_KCB in 
					(SELECT ID_QTC_Quy_KCB 
					FROM BH_QTC_Quy_KCB 
					WHERE iNamChungTu = @namLamViec 
					AND iQuyChungTu=@Quy
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayChungTu as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpk]    Script Date: 11/20/2023 12:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_qkpk]
	@namLamViec int,
	@donVi nvarchar(max),
	@Quy int,
	@ngayChungTu datetime,
	@userName nvarchar(100),
	@LoaiChi uniqueidentifier
AS
BEGIN
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Quy_KPK_ChiTiet ct
				WHERE ct.iID_QTC_Quy_KPK in 
					(SELECT ID_QTC_Quy_KPK 
					FROM BH_QTC_Quy_KPK 
					WHERE iNamChungTu = @namLamViec 
					AND iQuyChungTu=@Quy
					AND iID_LoaiChi=@LoaiChi
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayChungTu as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_get_lns_for_qkpql]    Script Date: 11/20/2023 12:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qtc_get_lns_for_qkpql]
	@namLamViec int,
	@donVi nvarchar(max),
	@Quy int,
	@ngayChungTu datetime,
	@userName nvarchar(100)
AS
BEGIN
	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	--), 

	--WITH tblLNS AS (
	--	SELECT DISTINCT sLNS
	--	FROM BH_DM_MucLucNganSach ml
	--	WHERE iID_MLNS in 
	--			(
	--				SELECT  ct.iID_MucLucNganSach
	--				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
	--				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
	--				(SELECT ID_QTC_Quy_KinhPhiQuanLy 
	--				FROM BH_QTC_Quy_KinhPhiQuanLy 
	--				WHERE iNamChungTu = @namLamViec 
	--					AND iQuyChungTu=@Quy
	--					AND iID_MaDonVi IN (select * from f_split(@donVi))
	--					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date)))
	--),
		WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DM_MucLucNganSach
		WHERE iID_MLNS in 
				(SELECT  ct.iID_MucLucNganSach
				FROM BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ct
				WHERE ct.iID_QTC_Quy_KinhPhiQuanLy in 
					(SELECT ID_QTC_Quy_KinhPhiQuanLy 
					FROM BH_QTC_Quy_KinhPhiQuanLy 
					WHERE iNamChungTu = @namLamViec 
					AND iQuyChungTu=@Quy
					AND iID_MaDonVi IN (select * from f_split(@donVi))
					AND cast(dNgayChungTu as date) <= cast(@ngayChungTu as date)))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	ORDER BY sXauNoiMa
END
;
;
GO
