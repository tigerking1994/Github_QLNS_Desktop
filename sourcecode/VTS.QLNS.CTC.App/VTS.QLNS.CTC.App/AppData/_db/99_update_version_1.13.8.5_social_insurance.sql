/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_bh]    Script Date: 1/13/2024 5:29:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qtc_nkqk_chitiet_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 1/13/2024 5:29:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 1/13/2024 5:29:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 1/13/2024 5:29:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_nkp_ql_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_nkp_ql_chitiet]    Script Date: 1/13/2024 5:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_bh_qtc_nkp_ql_chitiet]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

SELECT   isnull(ctct.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet, @iDCT) as ID_QTC_Nam_KinhPhiQuanLy_ChiTiet,
		@iD as IID_QTC_Nam_KinhPhiQuanLy,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.sDuToanChiTietToi as SDuToanChiTietToi,
		ctct.iID_MaDonVi as IIdMaDonVi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(ctct.fTien_DuToanGiaoNamNay, 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(ctct.fTien_TongDuToanDuocGiao, 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTien_ThucChi, 
		ISNULL(ctct.fTienThua, 0)  as fTienThua, 
		ISNULL(ctct.fTienThieu, 0)  as fTienThieu, 
		ISNULL(ctct.fTiLeThucHienTrenDuToan, 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet 
				WHERE iID_QTC_Nam_KinhPhiQuanLy in
					( SELECT ID_QTC_Nam_KinhPhiQuanLy FROM BH_QTC_Nam_KinhPhiQuanLy
								WHERE iNamLamViec=@NamLamViec
								AND ID_QTC_Nam_KinhPhiQuanLy=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 1/13/2024 5:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from splitstring(@Lns))
				and danhmuc.iTrangThai=1


	---Lấy danh sách chi tiết
		select	
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,
			Sum(qtcn_ct.fTien_ThucChi) as fTien_ThucChi,

			CASE WHEN ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) >  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) THEN 
			ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) -  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) ELSE 0 END fTienThua,

			CASE WHEN ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0) <  ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) THEN 
			ISNULL(Sum(qtcn_ct.fTien_ThucChi),0)- ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0)  ELSE 0 END fTienThieu,
			--Sum(qtcn_ct.fTien_TongDuToanDuocGiao) - Sum(qtcn_ct.fTien_ThucChi)  as fTienThua,
			--Sum(qtcn_ct.fTien_ThucChi) - Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTienThieu,
			CASE WHEN ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) >0 THEN 
			ISNULL(Sum(qtcn_ct.fTien_ThucChi),0) / ISNULL(Sum(qtcn_ct.fTien_TongDuToanDuocGiao),0)  ELSE 0 END fTiLeThucHienTrenDuToan
			--Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan

		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_KCB_QuanYDonVi as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamLamViec = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sNoiDung



		---Kết quả hiển thị trả về
		select 

			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			mucluc.sDuToanChiTietToi as SDuToanChiTietToi,
			chi_tiet.fTien_DuToanNamTruocChuyenSang, 
			chi_tiet.fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTien_ThucChi,
			chi_tiet.fTienThua,
			chi_tiet.fTienThieu,
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa


		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]    Script Date: 1/13/2024 5:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chinamKCB_chitiet]
@IdChungTu uniqueidentifier,
@Lns nvarchar(max),
@INamLamViec int,
@IsTongHop4Quy bit
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from splitstring(@Lns))
				and danhmuc.iTrangThai=1
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			qtcn_ct.sXauNoiMa,
			qtcn_ct.iNamLamViec,
			qtcn_ct.iID_MaDonVi,
			qtcn_ct.fTien_DuToanNamTruocChuyenSang,
			qtcn_ct.fTien_DuToanGiaoNamNay,
			qtcn_ct.fTien_TongDuToanDuocGiao,
			qtcn_ct.fTien_ThucChi,
			(CASE  WHEN isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) > isnull(qtcn_ct.fTien_ThucChi,0) THEN isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) - isnull(qtcn_ct.fTien_ThucChi,0) ELSE 0 END ) as fTienThua,
			(CASE  WHEN isnull(qtcn_ct.fTien_ThucChi,0) > isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) THEN isnull(qtcn_ct.fTien_ThucChi,0) - isnull(qtcn_ct.fTien_TongDuToanDuocGiao,0) ELSE 0 END ) as fTienThieu
		into #tblQuyetToanNamChiTiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_KCB_QuanYDonVi  as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = @IdChungTu;

		
	---Kết quả hiển thị trả về
	select 
		mucluc.iID_MLNS,
		mucluc.iID_MLNS_Cha,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sTNG,
		mucluc.sXauNoiMa,
		mucluc.bHangCha,
		mucluc.sDuToanChiTietToi as SDuToanChiTietToi,
		chi_tiet.ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sNoiDung,
		chi_tiet.iID_MaDonVi,
		chi_tiet.iNamLamViec,
		chi_tiet.fTien_DuToanNamTruocChuyenSang, 
		chi_tiet.fTien_DuToanGiaoNamNay,
		chi_tiet.fTien_TongDuToanDuocGiao,
		chi_tiet.fTien_ThucChi,
		chi_tiet.fTienThua,
		chi_tiet.fTienThieu
	from #tblMucLucNganSach as mucluc
	left join #tblQuyetToanNamChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanNamChiTiet;
end
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qtc_nkqk_chitiet_bh]    Script Date: 1/13/2024 5:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Lay chi tiet chung tu 
CREATE PROCEDURE [dbo].[sp_qtc_nkqk_chitiet_bh]
	 @NamLamViec int,
	 @iD uniqueidentifier,
	 @IdDonVi nvarchar(max),
	 @LNS nvarchar(max)
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
	-- lấy ra mlns theo phân cấp
	SELECT iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, bHangCha, sDuToanChiTietToi
			into #tblMlnsByPhanCap
	FROM BH_DM_MucLucNganSach 
	WHERE 
		iNamLamViec = @NamLamViec 
		AND iTrangThai = 1
		--AND (
		--	(@PhanCap = 'M' AND (sTM IS NULL OR sTM = ''))
		--	OR (@PhanCap = 'TM' AND (sTTM IS NULL OR sTTM = ''))
		--	OR (@PhanCap = 'NG' AND (sTNG IS NULL OR sTNG = ''))
		--	)
		AND sLNS IN (SELECT * FROM f_split(@LNS))
		--(
		--			SELECT DISTINCT VALUE
		--			FROM 
		--			(
		--				SELECT 
		--					CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--					CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--					CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--					CAST(sLNS AS nvarchar(10)) LNS 
		--				FROM
		--					NS_NguoiDung_LNS 
		--				WHERE 
		--					sMaNguoiDung = @UserName
		--					AND INamLamViec = @NamLamViec
		--					AND sLNS IN (SELECT * FROM f_split(@LNS))
		--			) LNS
		--			UNPIVOT
		--			(
		--				value
		--				FOR col in (LNS1, LNS3, LNS5, LNS)
		--			) un) 

SELECT   isnull(ctct.ID_QTC_Nam_KPK_ChiTiet, @iDCT) as ID_QTC_Nam_KPK_ChiTiet,
		@iD as iID_QTC_Nam_KPK,
		mlns.iID_MLNS as iID_MucLucNganSach,
		mlns.iID_MLNS_Cha as IdParent,
		mlns.sXauNoiMa as sXauNoiMa,
		mlns.sLNS as SLNS,
		mlns.sL as SL,
		mlns.sK as SK,
		mlns.sM as SM,
		mlns.sTM as STM,
		mlns.sTTM as STTM,
		mlns.sNG as SNG,
		mlns.sTNG as STNG,
		mlns.sMoTa as SNoiDung,
		mlns.sDuToanChiTietToi as SDuToanChiTietToi,
		@NamLamViec AS INamLamViec,
		mlns.bHangCha as IsHangCha,
		isnull(ctct.dNgayTao, getdate()) AS dNgayTao,
		isnull(ctct.dNgaySua, getdate()) AS dNgaySua,
		ctct.sNguoiTao AS sNguoiTao,
		ctct.sNguoiSua AS sNguoiSua,
		ctct.iID_MaDonVi as IIdMaDonVi,
		ISNULL(ctct.fTien_DuToanNamTruocChuyenSang, 0)  as fTien_DuToanNamTruocChuyenSang, 
		ISNULL(ctct.fTien_DuToanGiaoNamNay, 0)  as fTien_DuToanGiaoNamNay,
		ISNULL(ctct.fTien_TongDuToanDuocGiao, 0)  as fTien_TongDuToanDuocGiao, 
		ISNULL(ctct.fTien_ThucChi, 0)  as fTien_ThucChi, 
		ISNULL(ctct.fTienThua, 0)  as fTienThua, 
		ISNULL(ctct.fTienThieu, 0)  as fTienThieu, 
		ISNULL(ctct.fTiLeThucHienTrenDuToan, 0)  as fTiLeThucHienTrenDuToan
	FROM 
		#tblMlnsByPhanCap mlns
	LEFT JOIN 
		(SELECT * FROM BH_QTC_Nam_KPK_ChiTiet 
				WHERE iID_QTC_Nam_KPK in
					( SELECT ID_QTC_Nam_KPK FROM BH_QTC_Nam_KPK
								WHERE iNamLamViec=@NamLamViec
								AND iID_QTC_Nam_KPK=@iD
								AND iID_MaDonVi IN (select * from f_split(@IdDonVi))
								)) ctct
	on mlns.iID_MLNS = ctct.iID_MucLucNganSach 
	Order by mlns.sXauNoiMa
END
;
;
;
GO
