/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_che_do]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_luong_get_che_do]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_luong_get_che_do]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_can_bo_che_do_index]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_luong_get_can_bo_che_do_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_luong_get_can_bo_che_do_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_qtt_thong_tri]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_qtt_thong_tri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_qtt_thong_tri]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 8/9/2024 2:45:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB]    Script Date: 8/9/2024 2:45:04 PM ******/
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
	SET NOCOUNT ON;
		DECLARE @iDCT uniqueidentifier='00000000-0000-0000-0000-000000000000';
		DECLARE @fSoThamDinh INT;
		SELECT @fSoThamDinh = Sum(fSoThamDinh)
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet 
		WHERE iNamLamViec = @INamLamViec - 1 and iMa = 225 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdMaDonVi))
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
			danhmuc.sDuToanChiTietToi,
			danhmuc.bHangChaDuToan
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (select * from splitstring(@Lns))
				and danhmuc.iTrangThai=1

	---Lấy danh sách chi tiết 
		select	
			qtcn_ct.sXauNoiMa,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang)/@Donvitinh as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay)/@Donvitinh as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao)/@Donvitinh as fTien_TongDuToanDuocGiao,
			Sum(qtcn_ct.fTien_ThucChi)/@Donvitinh as fTien_ThucChi,
			Sum(qtcn_ct.fTienThua)/@Donvitinh as fTienThua,
			Sum(qtcn_ct.fTienThieu)/@Donvitinh as fTienThieu,
			Sum(qtcn_ct.fTiLeThucHienTrenDuToan) as fTiLeThucHienTrenDuToan
		into #tbl_qtcn_chitiet
		from BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_KCB_QuanYDonVi as qtcn on qtcn_ct.iID_QTC_Nam_KCB_QuanYDonVi = qtcn.ID_QTC_Nam_KCB_QuanYDonVi
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamLamViec = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.sXauNoiMa,qtcn_ct.sNoiDung

		---Kết quả hiển thị trả về chung tu thuong
	if @IsTongHop=0
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
			mucluc.sDuToanChiTietToi,
			mucluc.sMoTa as sNoiDung,
			mucluc.bHangChaDuToan,
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @Donvitinh,0) ELSE 0 END as fTien_DuToanNamTruocChuyenSang, 
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @Donvitinh,0) ELSE 0 END as fDuToanNamTruocChuyenSang, 
			ROUND(daDuToan.fTienDuToan,0) as fTien_DuToanGiaoNamNay,
			ROUND(chi_tiet.fTien_TongDuToanDuocGiao,0)fTien_TongDuToanDuocGiao,
			ROUND(chi_tiet.fTien_ThucChi,0) fTien_ThucChi,
			ROUND(chi_tiet.fTienThua,0) fTienThua,
			ROUND(chi_tiet.fTienThieu,0) fTienThieu,
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_PhanBoDuToanChi_ChiTiet
		   WHERE iID_DTC_PhanBoDuToanChi IN
			   (SELECT ID
				FROM BH_DTC_PhanBoDuToanChi
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @INamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
		order by mucluc.sXauNoiMa
		---Kết quả hiển thị trả về chung tu cha
	else
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
			mucluc.sDuToanChiTietToi,
			mucluc.bHangChaDuToan,
			mucluc.sMoTa as sNoiDung,
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @Donvitinh,0) ELSE 0 END as fTien_DuToanNamTruocChuyenSang, 
			CASE WHEN mucluc.sXauNoiMa = @LNS THEN ROUND(@fSoThamDinh / @Donvitinh,0) ELSE 0 END as fDuToanNamTruocChuyenSang, 
			ROUND(daDuToan.fTienDuToan,0) as fTien_DuToanGiaoNamNay,
			ROUND(chi_tiet.fTien_TongDuToanDuocGiao,0) fTien_TongDuToanDuocGiao,
			ROUND(chi_tiet.fTien_ThucChi,0) fTien_ThucChi,
			ROUND(chi_tiet.fTienThua,0) fTienThua,
			ROUND(chi_tiet.fTienThieu,0) fTienThieu,
			chi_tiet.fTiLeThucHienTrenDuToan

		from #tblMucLucNganSach as mucluc
		left join #tbl_qtcn_chitiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
		LEFT JOIN (
			-- lấy ra dữ liệu dự toán
			SELECT 
				  SUM(cast(fTienTuChi as float)) AS fTienDuToan,
				  sXauNoiMa
		   FROM BH_DTC_DuToanChiTrenGiao_ChiTiet
		   WHERE iID_DTC_DuToanChiTrenGiao IN
			   (SELECT ID
				FROM BH_DTC_DuToanChiTrenGiao
				WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamLamViec = @INamLamViec
				  AND bIsKhoa=1)
			 AND iID_MaDonVi in  (SELECT * FROM f_split(@IdMaDonVi))-- donvi
		   GROUP BY sXauNoiMa
		) daDuToan  on mucluc.sXauNoiMa=daDuToan.sXauNoiMa
		order by mucluc.sXauNoiMa

		drop table #tblMucLucNganSach;
		drop table #tbl_qtcn_chitiet;

end
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_qtt_thong_tri]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_qtt_thong_tri]
	-- Add the parameters for the stored procedure here
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@iQuyNam int, 
	@iQuyNamLoai int,
	@DVT int
AS
BEGIN

	SELECT * INTO #result FROM 
	(
		SELECT 1 STT
				,N'Bảo hiểm xã hội' as SNoiDung
				,sum(isnull(fThu_BHXH_NLD,0)+isnull(fThu_BHXH_NSD,0))/@DVT as FSoTien  
				,1 ILevel
			FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		WHERE ctct.iID_QTT_BHXH_ChungTu in
		(
			SELECT iID_QTT_BHXH_ChungTu  FROM BH_QTT_BHXH_ChungTu 
			WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND iQuyNam=@iQuyNam
			AND iQuyNamLoai=@iQuyNamLoai
			AND iNamLamViec=@NamLamViec
		)
		AND 
		(sXauNoiMa  like '9020001-010-011-0001%' 
		OR sXauNoiMa like '9020001-010-011-0002%' 
		OR sXauNoiMa like '9020002-010-011-0001%' 
		OR sXauNoiMa like '9020002-010-011-0002%') AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		
		SELECT 2 STT
				,N'Bảo hiểm y tế' as SNoiDung
				,sum(isnull(fThu_BHYT_NLD,0)+isnull(fThu_BHYT_NSD,0))/@DVT as FSoTien 
				,1 ILevel
			FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		WHERE ctct.iID_QTT_BHXH_ChungTu in
		(
			SELECT iID_QTT_BHXH_ChungTu  FROM BH_QTT_BHXH_ChungTu 
			WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND iQuyNam=@iQuyNam
			AND iQuyNamLoai=@iQuyNamLoai
			AND iNamLamViec=@NamLamViec
		)
		AND 
		(sXauNoiMa  like  '9020001-010-011-0001%' 
		OR sXauNoiMa like '9020001-010-011-0002%' 
		OR sXauNoiMa like '9020002-010-011-0001%'
		OR sXauNoiMa like '9020002-010-011-0002%' ) 
		AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL

		SELECT 3 STT
				,N'Bảo hiểm thất nghiệp' as SNoiDung
				,sum(isnull(fThu_BHTN_NLD,0)+isnull(fThu_BHTN_NSD,0))/@DVT as FSoTien 
				,1 ILevel
			FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		WHERE ctct.iID_QTT_BHXH_ChungTu in
		(
			SELECT iID_QTT_BHXH_ChungTu  FROM BH_QTT_BHXH_ChungTu 
			WHERE iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND iQuyNam=@iQuyNam
			AND iQuyNamLoai=@iQuyNamLoai
			AND iNamLamViec=@NamLamViec
		)
		AND 
		(sXauNoiMa  like  '9020001-010-011-0001%' 
		OR sXauNoiMa like '9020001-010-011-0002%' 
		OR sXauNoiMa like '9020002-010-011-0001%' 
		OR sXauNoiMa like '9020002-010-011-0002%'  ) 
		AND iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi)) 
	) result

	select * from #result
	DROP TABLE #result;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_can_bo_che_do_index]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_luong_get_can_bo_che_do_index]
	@MaCanBo nvarchar(100)
AS
BEGIN
	select
		canbo.Id,
		chedo.bDisplay IsDisplay,
		chedo.sMaCheDo,
		chedo.sMaCheDoCha,
		chedo.sTenCheDo,
		CONCAT(chedocha.sMaCheDo, ' - ', chedocha.sTenCheDo) AS sTenCheDoCha,
		chedo.bTinhT7 ,
		chedo.bTinhCN ,
		chedo.bTinhNgayLe ,
		chedocha.sXauNoiMa,
		--case when (chedo.sMaCheDoCha = '' 
		--			or chedo.sMaCheDoCha is null 
		--			or chedo.sMaCheDo in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)) then 1 
		--		else 0 
		--end as IsHangCha,
		canbo.dDenNgay,
		canbo.dTuNgay,
		canbo.fSoNgayNghiPhep as FSoNgayNghiPhep,
		canbo.fSoNgayHuongBHXH,
		canbo.iSoNgayNghi,
		canbo.sMaCanBo,
		canbo.sMaCheDo,
		canbo.sTenCheDo,
		canbo.fSoTien,
		canbo.sSoQuyetDinh,
		canbo.dNgayQuyetDinh,
		canbo.iThangLuongCanCuDong,
		canbo.iNamCanCuDong,
		canbo.fGiaTriCanCu,
		canbo.iNam,
		canbo.iThang
	from 
	TL_DM_CheDoBHXH chedo
	left join TL_CanBo_CheDoBHXH canbo on chedo.sMaCheDo = canbo.sMaCheDo and canbo.sMaCanBo = @MaCanBo
	left join TL_DM_CheDoBHXH chedocha on chedo.sMaCheDoCha = chedocha.sMaCheDo
	where chedo.sMaCheDo <> 'SONGAYHUONG'
	and (chedo.sMaCheDoCha IN (select sMaCheDo from TL_DM_CheDoBHXH where sMaCheDoCha = '')
		or chedo.sMaCheDoCha in ('BENHDAINGAY','OMKHAC'))
	order by sTenCheDoCha

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_che_do]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_luong_get_che_do]
	@MaCanBo nvarchar(100)
AS
BEGIN
	select
		canbo.Id,
		chedo.bDisplay IsDisplay,
		chedo.sMaCheDo,
		chedo.sMaCheDoCha,
		chedo.sTenCheDo,
		chedo.sXauNoiMa,
		chedo.bTinhT7,
		chedo.bTinhCN,
		chedo.bTinhNgayLe,
		case when (chedo.sMaCheDoCha = '' 
					or chedo.sMaCheDoCha is null 
					or chedo.sMaCheDo in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)) then 1 
				else 0 
		end as IsHangCha,
		canbo.dDenNgay,
		canbo.dTuNgay,
		canbo.fSoNgayHuongBHXH,
		canbo.iSoNgayNghi,
		canbo.sMaCanBo,
		canbo.sMaCheDo,
		canbo.sTenCheDo,
		canbo.fSoTien,
		canbo.sSoQuyetDinh,
		canbo.dNgayQuyetDinh,
		canbo.iThangLuongCanCuDong,
		canbo.fGiaTriCanCu,
		canbo.iNam,
		canbo.iThang
	from 
	TL_CanBo_CheDoBHXH canbo
	right join TL_DM_CheDoBHXH chedo on canbo.sMaCheDo = chedo.sMaCheDo and canbo.sMaCanBo = @MaCanBo
	where chedo.sMaCheDo <> 'SONGAYHUONG'
	order by chedo.iLoaiCheDo, chedo.sXauNoiMa

	--select
	--	chedo.sMaCheDo,
	--	chedo.sMaCheDoCha,
	--	chedo.sTenCheDo,
	--	CONCAT(chedocha.sMaCheDo, '-', chedocha.sTenCheDo) AS ParentName,
	--	canbo.*
	--from 
	--TL_DM_CheDoBHXH chedo 
	--left join TL_CanBo_CheDoBHXH canbo on canbo.sMaCheDo = chedo.sMaCheDo and canbo.sMaCanBo = @MaCanBo
	--left join TL_DM_CheDoBHXH chedocha on chedo.sMaCheDoCha = chedocha.sMaCheDo 
	--and chedo.sMaCheDoCha IN (select sMaCheDo from TL_DM_CheDoBHXH where sMaCheDoCha = '' or sMaCheDoCha is null)
	--where chedo.sMaCheDo <> 'SONGAYHUONG'
	--Order By ParentName
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_tong_hop_chi_tiet_donvi] 
	@NamLamViec int,
	@MaDonVi nvarchar(500),
	@LoaiChungTu int,
	@DVT int
AS
BEGIN

	-- Chi tiet chung tu
	select ctct.sXauNoiMa,
	ctct.iID_NoiDung,
	ml.iID_MLNS_Cha IdParent,
	ctct.sLNS,
	('     '+dv.sTenDonVi) as sTenNoiDung ,
	ctct.iSoNguoi,
	ctct.iSoThang,
	ctct.fDinhMuc,
	ctct.fThanhTien,
	ctct.sGhiChu,
	1 Type,
	0 bHangCha,
	0 IsHangCha
	into #tempChungTu
	from BH_KHTM_BHYT_ChiTiet ctct
	left join BH_KHTM_BHYT ct on ctct.iID_KHTM_BHYT=ct.iID_KHTM_BHYT
	left join DonVi dv on ct.iID_MaDonVi=dv.iID_MaDonVi
	left join BH_DM_MucLucNganSach ml on ctct.iID_NoiDung=ml.iID_MLNS
	where  ct.iNamLamViec=@NamLamViec
	and ml.iNamLamViec=@NamLamViec
	and ml.iTrangThai=1
	and dv.iNamLamViec=@NamLamViec
	and dv.iTrangThai=1
	and dv.iID_MaDonVi in (select * from splitstring(@MaDonVi))
	order by ctct.sXauNoiMa

	-- tinh sum chung tu
	select ctct.sXauNoiMa,
	ctct.iID_NoiDung,
	ctct.sLNS,
	ctct.sTenNoiDung as sTenNoiDung ,
	SUM(ctct.iSoNguoi) iSoNguoi,
	SUM(ctct.iSoThang) iSoThang,
	SUM(ctct.fDinhMuc) fDinhMuc,
	SUM(ctct.fThanhTien) fThanhTien,
	ctct.sGhiChu,
	0 Type
	into #tempChungTuSum
	from BH_KHTM_BHYT_ChiTiet ctct
	left join BH_KHTM_BHYT ct on ctct.iID_KHTM_BHYT=ct.iID_KHTM_BHYT
	left join DonVi dv on ct.iID_MaDonVi=dv.iID_MaDonVi
	where  ct.iNamLamViec=@NamLamViec
	and dv.iNamLamViec=@NamLamViec
	and dv.iTrangThai=1
	and dv.iID_MaDonVi in (select * from splitstring(@MaDonVi))
	group by ctct.sXauNoiMa,
	ctct.iID_NoiDung,
	ctct.sLNS,
	ctct.sTenNoiDung ,
	ctct.sGhiChu
	order by ctct.sXauNoiMa

	-- join voi muc luc ngan sach
	select mlns.sXauNoiMa,
	mlns.iID_MLNS iID_NoiDung,
	mlns.iID_MLNS_Cha IdParent,
	tm.sLNS,
	mlns.sMoTa as sTenNoiDung ,
	tm.iSoNguoi,
	tm.iSoThang,
	tm.fDinhMuc,
	tm.fThanhTien,
	tm.sGhiChu,
	0 Type,
	mlns.bHangCha,
	mlns.bHangCha as IsHangCha
	into #tempmlnsChungTu
	from BH_DM_MucLucNganSach mlns
	left join #tempChungTuSum tm  on mlns.sXauNoiMa=tm.sXauNoiMa
	where mlns.sLNS like '903%'
	and mlns.iNamLamViec=@NamLamViec
	and mlns.iTrangThai=1
	order by sXauNoiMa, Type asc


	select * into #tempAll from 
	( select * from #tempmlnsChungTu
	union all 
	select * from #tempChungTu
	) temp
	
	--select fGiaTri into #tempLCS from BH_DM_CauHinhThamSo
	--where iNamLamViec=@NamLamViec and sMa='LCS' and bTrangThai=1

	--select fGiaTri into #tempHESO_BHYT from BH_DM_CauHinhThamSo
	--where iNamLamViec=@NamLamViec and sMa='HESO_BHYT' and bTrangThai=1


	select sXauNoiMa,
	iID_NoiDung ,
	IdParent,
	sTenNoiDung,
	iSoNguoi,
	iSoThang,
	fDinhMuc,
	fThanhTien,
	sGhiChu,
	sLNS,
	bHangCha,
	IsHangCha,
	Type,
	@NamLamViec iNamLamViec
	into #tempresult
	from #tempAll
	order by sXauNoiMa, Type asc

	alter table #tempresult add DHeSoLCS float, DHeSoBHYT float;

	update #tempresult
	set DHeSoLCS = (select top 1 fGiaTri from BH_DM_CauHinhThamSo where iNamLamViec = @NamLamViec and sMa = 'LCS' and bTrangThai = 1),
		DHeSoBHYT = (select top 1 fGiaTri from BH_DM_CauHinhThamSo where iNamLamViec = @NamLamViec and sMa = 'HESO_BHYT' and bTrangThai = 1)

	select * from #tempresult 
	order by sXauNoiMa, Type asc;

	drop table #tempAll;
	drop table #tempChungTu;
	drop table #tempmlnsChungTu;
	drop table #tempresult;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]
@INamLamViec int,
@IdDonVi nvarchar(max),
@DonViTinh int
AS
BEGIN
	select 
		dv.sTenDonVi as sTenDonVi,
		ct.sMoTa,
		dv.iKhoi,
		sum(ct.fOmDau)/ @DonViTinh fOmDau,
		sum(ct.fThaiSan)/ @DonViTinh fThaiSan,
		sum(ct.fTNLDBNN)/ @DonViTinh fTNLDBNN,
		sum(ct.fHuuTri)/ @DonViTinh fHuuTri,
		sum(ct.fPhucVien)/ @DonViTinh fPhucVien,
		sum(ct.fXuatNgu)/ @DonViTinh fXuatNgu,
		sum(ct.fThoiViec)/ @DonViTinh fThoiViec,
		sum(ct.fTuTuat)/ @DonViTinh fTuTuat,
		iKieuChu = 3
	into #data
	from
	(select 
	iID_MaDonVi,
	case when iMa > 182 and iMa < 191 then N'Khối dự toán'
	when iMa > 192 and iMa < 201 then N'Khối hạch toán'
	else N'Khối khác' end as sMoTa,
	case when ima = 183 or ima = 193 then fSoThamDinh
			else 0 end as fOmDau,
	case when ima = 184 or ima = 194 then fSoThamDinh
			else 0 end as fThaiSan,
	case when ima = 185 or ima = 195 then fSoThamDinh
			else 0 end as fTNLDBNN,
	case when ima = 186 or ima = 196 then fSoThamDinh
			else 0 end as fHuuTri,
	case when ima = 187 or ima = 197 then fSoThamDinh
			else 0 end as fPhucVien,
	case when ima = 188 or ima = 198 then fSoThamDinh
			else 0 end as fXuatNgu,
	case when ima = 189 or ima = 199 then fSoThamDinh
			else 0 end as fThoiViec,
	case when ima = 190 or ima = 200 then fSoThamDinh
			else 0 end as fTuTuat
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where 
	iID_MaDonVi in (select * from f_split(@IdDonVi)) and 
	iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fOmDau > 0 or fThaiSan > 0 or fTNLDBNN > 0 or fHuuTri > 0 or fPhucVien > 0 or fXuatNgu > 0 or fThoiViec > 0 or fTuTuat > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi, ct.sMoTa, dv.iKhoi

	select 
	sSTT = '',
	sTenDonVi = N'A. Đơn vị dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA
	from (select * from #data where iKhoi = 2) ct

		select 
	sSTT = '',
	sTenDonVi = N'+ Khối dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA1
	from (select * from #data where iKhoi = 2 and sMoTa = N'Khối dự toán') ct

			select 
	sSTT = '',
	sTenDonVi = N'+ Khối hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA2
	from (select * from #data where iKhoi = 2 and sMoTa = N'Khối hạch toán') ct

	select 
	sSTT = '',
	sTenDonVi = N'B. Đơn vị hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB
	from (select * from #data where iKhoi = 1) ct

		select 
	sSTT = '',
	sTenDonVi = N'+ Khối dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB1
	from (select * from #data where iKhoi = 1 and sMoTa = N'Khối dự toán') ct

			select 
	sSTT = '',
	sTenDonVi = N'+ Khối hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB2
	from (select * from #data where iKhoi = 1 and sMoTa = N'Khối hạch toán') ct

				select 
	sSTT = '',
	sTenDonVi,
	iKhoi,
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 3
	into #rowDV
	from #data ct
	group by ct.sTenDonVi, ct.sTenDonVi, ct.iKhoi

	select * 
	into #rowDV1
	from (select 
		ROW_NUMBER() OVER (ORDER BY sTenDonVi) AS sSTT,
		sTenDonVi,
		sMoTa = null,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu 
		from #rowDV where iKhoi = 2) dv1
	union all (select 
		'' AS sSTT,
		sTenDonVi,
		sMoTa,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu = 2
	from #data where iKhoi = 2) 
	order by sTenDonVi, sMoTa

	select *
	into #rowDV2
	from (select 
		ROW_NUMBER() OVER (ORDER BY sTenDonVi) AS sSTT,
		sTenDonVi,
		sMoTa = null,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
		from #rowDV where iKhoi = 1) dv2
	union all (select 
		'' AS sSTT,
		sTenDonVi,
		sMoTa,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu = 2
	from #data where iKhoi = 1) 
	order by sTenDonVi, sMoTa

	select * from #rowA
	union all select * from #rowA1
	union all select * from #rowA2
	union all (select 
		sSTT,
		isnull('       ' + sMoTa, sTenDonVi) sTenDonVi,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
	from #rowDV1)
	union all select * from #rowB
	union all select * from #rowB1
	union all select * from #rowB2
	union all (select 
			sSTT,
		isnull('       ' + sMoTa, sTenDonVi) sTenDonVi,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
	from #rowDV2)
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld]
@INamLamViec int,
@IdDonVi nvarchar(max),
@Loai int,
@DonViTinh int
AS
BEGIN
	if (@Loai = 1)
	select 
		ct.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi,
		sum(ct.fDuToanNamTruoc)/@DonViTinh fDuToanNamTruoc,
		sum(ct.fDuToanNamNay)/@DonViTinh fDuToanNamNay,
		sum(ct.fQuyetToan)/@DonViTinh fQuyetToan
	from
	(select 
	iID_MaDonVi,
	case ima when 213 then fSoThamDinh
			else 0 end as fDuToanNamTruoc,
	case ima when 214 then fSoThamDinh 
			else 0 end as fDuToanNamNay,
	case ima when 215 then fSoThamDinh
			else 0 end as fQuyetToan
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where iID_MaDonVi in (select * from f_split(@IdDonVi))
	and iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fDuToanNamTruoc > 0 or fDuToanNamNay > 0 or fQuyetToan > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi
	order by ct.iID_MaDonVi

	else if (@Loai = 2)
		select 
		ct.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi,
		sum(ct.fDuToanNamTruoc)/@DonViTinh fDuToanNamTruoc,
		sum(ct.fDuToanNamNay)/@DonViTinh fDuToanNamNay,
		sum(ct.fQuyetToan)/@DonViTinh fQuyetToan
	from
	(select 
	iID_MaDonVi,
	case ima when 207 then fSoThamDinh
			else 0 end as fDuToanNamTruoc,
	case ima when 208 then fSoThamDinh 
			else 0 end as fDuToanNamNay,
	case ima when 209 then fSoThamDinh
			else 0 end as fQuyetToan
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where iID_MaDonVi in (select * from f_split(@IdDonVi))
	and iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fDuToanNamTruoc > 0 or fDuToanNamNay > 0 or fQuyetToan > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi
	order by ct.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi]
@INamLamViec int,
@IdDonVi nvarchar(max),
@DonViTinh int
AS
BEGIN
	select 
		ct.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi,
		sum(ct.fDuToanNamTruoc) /@DonViTinh fDuToanNamTruoc,
		sum(ct.fDuToanNamNay) /@DonViTinh fDuToanNamNay,
		sum(ct.fQuyetToan) /@DonViTinh fQuyetToan
	from
	(select 
	iID_MaDonVi,
	case ima when 220 then fSoThamDinh
			else 0 end as fDuToanNamTruoc,
	case ima when 221 then fSoThamDinh 
			else 0 end as fDuToanNamNay,
	case ima when 223 then fSoThamDinh
			else 0 end as fQuyetToan
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where iID_MaDonVi in (select * from f_split(@IdDonVi))
	and iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fDuToanNamTruoc > 0 or fDuToanNamNay > 0 or fQuyetToan > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi
	order by ct.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt]
@INamLamViec int,
@IdDonVi nvarchar(max),
@DonViTinh int
AS
BEGIN
	select 
		ct.iID_MaDonVi + ' - ' + dv.sTenDonVi as sTenDonVi,
		sum(ct.fDuToanNamTruoc)/@DonViTinh fDuToanNamTruoc,
		sum(ct.fDuToanNamNay)/@DonViTinh fDuToanNamNay,
		sum(ct.fQuyetToan)/@DonViTinh fQuyetToan
	from
	(select 
	iID_MaDonVi,
	case ima when 228 then fSoThamDinh
			else 0 end as fDuToanNamTruoc,
	case ima when 229 then fSoThamDinh 
			else 0 end as fDuToanNamNay,
	case ima when 231 then fSoThamDinh
			else 0 end as fQuyetToan
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where iID_MaDonVi in (select * from f_split(@IdDonVi))
	and iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fDuToanNamTruoc > 0 or fDuToanNamNay > 0 or fQuyetToan > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi
	order by ct.iID_MaDonVi
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_chi]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@Dvt int,
	@SoQuyetDinh nvarchar(200),
	@IsMillionRound bit
AS
BEGIN
	---CHI---
	select ctct.*, dv.iKhoi into TBL_DTC from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))

	--Chi các chế độ BHXH DUTOAN
	select * into TBL_CHI_CHEDO_DUTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Ốm đau' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%') and iKhoi = 2
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0001%' ) 
	union all 
	select 3 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Thai sản' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%') and iKhoi = 2
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0002%' ) 
	union all 
	select 4 rowNum, 0 bHangCha, null stt, N'- Trợ cấp tai nạn lao động, BNN' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0003%'  or sXauNoiMa like '9010002-010-011-0003%') and iKhoi = 2
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0003%'  ) 
	union all 
	select 5 rowNum, 0 bHangCha, null stt, N'- Trợ cấp hưu trí' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%') and iKhoi = 2
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0004%' ) 
	union all 
	select 6 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Phục viên' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%') and iKhoi = 2
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0005%' )
	union all 
	select 7 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Xuất ngũ' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%') and iKhoi = 2
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0006%' )
	union all 
	select 8 rowNum, 0 bHangCha, null stt, N'- Trợ cấp thôi việc' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%') and iKhoi = 2
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0007%' )
	union all 
	select 9 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Tử tuất' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%') and iKhoi = 2
	from TBL_DTC where (sXauNoiMa like '9010001-010-011-0008%' ) 
	union all 
	select 10 rowNum, 1 bHangCha, 2 stt, N'Kinh phí quản lý BHXH, BHYT' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010003%' and iKhoi = 2
	union all 
	select 11 rowNum, 1 bHangCha, 3 stt, N'Kinh phí KCB tại quân y đơn vị' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%') and iKhoi = 2
	union all 
	select 12 rowNum, 1 bHangCha, 4 stt, N'Kinh phí KCB tại Trường sa - DK' sMoTa, null fTongSo, sum(isnull(fTongTien, 0)) fDuToan, null fHachToan
	from TBL_DTC where (sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%') and iKhoi = 2
	) chidutoan



	--Chi các chế độ BHXH hạch toán
	select * into TBL_CHI_CHEDO_HACHTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Ốm đau' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%') and iKhoi <> 2
	from TBL_DTC where (sXauNoiMa like '9010002-010-011-0001%') 
	union all 
	select 3 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Thai sản' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%') and iKhoi <> 2
	from TBL_DTC where ( sXauNoiMa like '9010002-010-011-0002%') 
	union all 
	select 4 rowNum, 0 bHangCha, null stt, N'- Trợ cấp tai nạn lao động, BNN' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	--from TBL_DTC where  (sXauNoiMa like '9010001-010-011-0003%'  or sXauNoiMa like '9010002-010-011-0003%') and iKhoi <> 2
	from TBL_DTC where  ( sXauNoiMa like '9010002-010-011-0003%') 
	union all 
	select 5 rowNum, 0 bHangCha, null stt, N'- Trợ cấp hưu trí' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	--from TBL_DTC where  (sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%') and iKhoi <> 2
	from TBL_DTC where  ( sXauNoiMa like '9010002-010-011-0004%') 
	union all 
	select 6 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Phục viên' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%') and iKhoi <> 2
	from TBL_DTC where ( sXauNoiMa like '9010002-010-011-0005%') 
	union all 
	select 7 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Xuất ngũ' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%') and iKhoi <> 2
	from TBL_DTC where ( sXauNoiMa like '9010002-010-011-0006%') 
	union all 
	select 8 rowNum, 0 bHangCha, null stt, N'- Trợ cấp thôi việc' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%') and iKhoi <> 2
	from TBL_DTC where ( sXauNoiMa like '9010002-010-011-0007%') 
	union all 
	select 9 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Tử tuất' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	--from TBL_DTC where (sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%') and iKhoi <> 2
	from TBL_DTC where ( sXauNoiMa like '9010002-010-011-0008%') 
	union all 
	select 10 rowNum, 1 bHangCha, 2 stt, N'Kinh phí quản lý BHXH, BHYT' sMoTa, null fTongSo, null fDuToan,  sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where sXauNoiMa like '9010003%' and iKhoi <> 2
	union all 
	select 11 rowNum, 1 bHangCha, 3 stt, N'Kinh phí KCB tại quân y đơn vị' sMoTa, null fTongSo, null fDuToan, sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where (sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%') and iKhoi <> 2
	union all 
	select 12 rowNum, 1 bHangCha, 4 stt, N'Kinh phí KCB tại Trường sa - DK' sMoTa, null fTongSo,null fDuToan,  sum(isnull(fTongTien, 0)) fHachToan
	from TBL_DTC where (sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%') and iKhoi <> 2
	) chihachtoan

	if (@IsMillionRound = 1)
	begin
	select dt.rowNum, 
			dt.bHangCha, 
			dt.stt, 
			dt.sMoTa, 
			round((isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0)) / 1000000, 0) * 1000000 / @Dvt fTongSoChi, 
			round(dt.fDuToan / 1000000, 0) * 1000000 / @Dvt fDuToan, 
			round(ht.fHachToan / 1000000, 0) * 1000000 /@Dvt fHachToan
	into TBL_DTC_RESULT
	from TBL_CHI_CHEDO_DUTOAN dt
	left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	update TBL_DTC_RESULT set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT where bHangCha = 0),
							fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT where bHangCha = 0),
							fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT where bHangCha = 0)
						where bHangCha = 1 and stt = 1

	--result
	select * from TBL_DTC_RESULT
	end
	else 
		begin
	select dt.rowNum, 
			dt.bHangCha, 
			dt.stt, 
			dt.sMoTa, 
			(isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0))/@Dvt fTongSoChi, 
			dt.fDuToan/@Dvt fDuToan, 
			ht.fHachToan/@Dvt fHachToan
	into TBL_DTC_RESULT1
	from TBL_CHI_CHEDO_DUTOAN dt
	left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	update TBL_DTC_RESULT1 set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT1 where bHangCha = 0),
							fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT1 where bHangCha = 0),
							fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT1 where bHangCha = 0)
						where bHangCha = 1 and stt = 1

	--result
	select * from TBL_DTC_RESULT1
	end

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC]') AND type in (N'U')) drop table TBL_DTC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_DUTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_DUTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_HACHTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_HACHTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT]') AND type in (N'U')) drop table TBL_DTC_RESULT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT1]') AND type in (N'U')) drop table TBL_DTC_RESULT1;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_dieu_chinh_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@Dvt int,
	@SoQuyetDinh nvarchar(200),
	@IsMillionRound bit
AS
BEGIN
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.*, dv.iKhoi into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHXH
	select * into TBL_THU_BHXH from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Thu BHXH' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 2 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))) fTongSo, sum(fBHXH_NLD) fNLD, sum(fBHXH_NSD) fNSD, null sLoai
	--from TBL_DTT where iKhoi = 2 -- sXauNoiMa like '9020001%'
	from TBL_DTT where  sXauNoiMa like '9020001%'
	
	union all
	
	select 3 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))) fTongSo, sum(fBHXH_NLD) fNLD, sum(fBHXH_NSD) fNSD, null sLoai
	--from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	from TBL_DTT where sXauNoiMa like '9020002%'
	) thubhxh

	update TBL_THU_BHXH set fTongSo = (select sum(fTongSo) from TBL_THU_BHXH where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHXH where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHXH where bHangCha = 0)
						where bHangCha = 1

	--Thu BHTN
	select * into TBL_THU_BHTN from
	(select 4 rowNum, 1 bHangCha, '2' stt, N'Thu BHTN' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 5 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))) fTongSo, sum(fBHTN_NLD) fNLD, sum(fBHTN_NSD) fNSD, null sLoai
	--from TBL_DTT where iKhoi = 2 --sXauNoiMa like '9020001%'
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	
	select 6 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))) fTongSo, sum(fBHTN_NLD) fNLD, sum(fBHTN_NSD) fNSD, null sLoai
	--from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	from TBL_DTT where sXauNoiMa like '9020002%'
	) thubhtn

	update TBL_THU_BHTN set fTongSo = (select sum(fTongSo) from TBL_THU_BHTN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHTN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHTN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT quân nhân
	select * into TBL_THU_BHYT_QN from
	(select 7 rowNum, 1 bHangCha, '3' stt, N'Thu BHYT quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 8 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%') and iKhoi = 2
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' ) 
	union all
	
	select 9 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%') and iKhoi <> 2
	from TBL_DTT where (sXauNoiMa like '9020002-010-011-0001%' )
	) thubhytquannhan

	update TBL_THU_BHYT_QN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_QN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_NLD from
	(select 10 rowNum, 1 bHangCha, '4' stt, N'Thu BHYT người lao động' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 11 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020001-010-011-0002%') and iKhoi = 2
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' ) --and iKhoi = 2
	
	union all
	
	select 12 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020001-010-011-0002%') and iKhoi <> 2) thubhytnld
	from TBL_DTT where sXauNoiMa like '9020002-010-011-0002%') thubhytnld

	update TBL_THU_BHYT_NLD set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_NLD where bHangCha = 0)
						where bHangCha = 1
	
	--Dữ liệu phân bổ dự toán thu mua BHYT
	select ctct.*, dv.iKhoi into TBL_DTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	join  BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_TN from
	(select 13 rowNum, 1 bHangCha, '5' stt, N'Thu BHYT thân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN' sLoai
	
	union all

	select 14 rowNum, 1 bHangCha, 'a' stt, N'Quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	
	union all
	
	select 15 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(isnull(fDuToan, 0)) fTongSo, null fNLD, sum(isnull(fDuToan, 0)) fNSD, N'BHYT_THANNHAN_QN' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0000%' or sXauNoiMa like '9030001-010-011-0001%') and iKhoi = 2
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0000%' ) 
	union all
	
	select 16 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(isnull(fDuToan, 0)) fTongSo, null fNLD, sum(isnull(fDuToan, 0)) fNSD, N'BHYT_THANNHAN_QN' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0000%' or sXauNoiMa like '9030001-010-011-0001%') and iKhoi <> 2
	from TBL_DTTM where ( sXauNoiMa like '9030002-010-011-0001%')
	union all
	
	select 17 rowNum, 1 bHangCha, 'b' stt, N'Công nhân, VCQP' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	
	union all
	
	select 18 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(isnull(fDuToan, 0)) fTongSo, null fNLD, sum(isnull(fDuToan, 0)) fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%') and iKhoi = 2
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0000%') 
	union all
	
	select 19 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(isnull(fDuToan, 0)) fTongSo, null fNLD, sum(isnull(fDuToan, 0)) fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%') and iKhoi <> 2
	from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' ) 
	) thubhytnld

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_QN'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_VCQP'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN'

	select * into TBL_THU from
	(select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHXH
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHTN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_QN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_NLD
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_TN) tblthu

	--Result
	if (@IsMillionRound = 1)
	select
	rowNum, 
	bHangCha, 
	stt, 
	sMoTa, 
	round(fTongSo / 1000000, 0) * 1000000 / @Dvt fTongSo, 
	round(fNLD / 1000000, 0) * 1000000 / @Dvt fNLD, 
	round(fNSD / 1000000, 0) * 1000000 / @Dvt fNSD 
	from TBL_THU
	else 
	select
	rowNum, 
	bHangCha, 
	stt, 
	sMoTa, 
	fTongSo/@Dvt fTongSo, 
	fNLD/@Dvt fNLD, 
	fNSD/@Dvt fNSD
	from TBL_THU

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTT]') AND type in (N'U')) drop table TBL_DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHXH]') AND type in (N'U')) drop table TBL_THU_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHTN]') AND type in (N'U')) drop table TBL_THU_BHTN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_QN]') AND type in (N'U')) drop table TBL_THU_BHYT_QN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_NLD]') AND type in (N'U')) drop table TBL_THU_BHYT_NLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTTM]') AND type in (N'U')) drop table TBL_DTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_TN]') AND type in (N'U')) drop table TBL_THU_BHYT_TN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU]') AND type in (N'U')) drop table TBL_THU;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200),
	@IsMillionRound bit
AS
BEGIN
	---CHI---
	select ctct.* into TBL_DTC from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	where ctct.iNamLamViec = @NamLamViec 
		and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
		and Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))

	
	--- khoi du toan 2
	select A.*
	 into #tempKhoiDuToan
	 from TBL_DTC A
	left join DonVi B on A.iID_MaDonVi=B.iID_MaDonVi and A.iNamLamViec=b.iNamLamViec
	where B.iKhoi = 2

	--Chi các chế độ BHXH DUTOAN
	select * into TBL_CHI_CHEDO_DUTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Ốm đau' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	--from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0001%' or sXauNoiMa like '9010002-010-011-0001%'
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0001%' 
	union all 
	select 3 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Thai sản' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	--from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0002%' or sXauNoiMa like '9010002-010-011-0002%'
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0002%' 
	union all 
	select 4 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp tai nạn lao động, BNN' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	--from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0003%' or sXauNoiMa like '9010002-010-011-0003%'
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0003%'

	union all 
	select 5 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp hưu trí' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	--from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0004%' or sXauNoiMa like '9010002-010-011-0004%'
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0004%' 
	union all 
	select 6 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Phục viên' sMoTa,
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	--from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0005%' or sXauNoiMa like '9010002-010-011-0005%'
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0005%' 
	union all 
	select 7 rowNum, 
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Xuất ngũ' sMoTa,
			null fTongSo,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan,
			null fHachToan
	--from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0006%' or sXauNoiMa like '9010002-010-011-0006%'
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0006%' 
	union all 
	select 8 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp thôi việc' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan,
			null fHachToan
	--from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0007%' or sXauNoiMa like '9010002-010-011-0007%'
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0007%'
	union all 
	select 9 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp Tử tuất' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	--from #tempKhoiDuToan where sXauNoiMa like '9010001-010-011-0008%' or sXauNoiMa like '9010002-010-011-0008%'
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0008%' 
	union all 
	select 10 rowNum, 
			1 bHangCha, 
			2 stt, 
			N'Kinh phí quản lý BHXH, BHYT' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010003%' 
	union all 
	select 11 rowNum, 
			1 bHangCha, 
			3 stt, 
			N'Kinh phí KCB tại quân y đơn vị' sMoTa,
			null fTongSo,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%'
	union all 
	select 12 rowNum,
			1 bHangCha, 
			4 stt, 
			N'Kinh phí KCB tại Trường sa - DK' sMoTa, 
			null fTongSo, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fDuToan, 
			null fHachToan
	from #tempKhoiDuToan where sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%'
	) chidutoan

	--- khoi du toan 1
	select A.*
	 into #tempKhoiHoachToan
	 from TBL_DTC A
	left join DonVi B on A.iID_MaDonVi=B.iID_MaDonVi and A.iNamLamViec=b.iNamLamViec
	where B.iKhoi <> 2


	--Chi các chế độ BHXH hạch toán
	select * into TBL_CHI_CHEDO_HACHTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Ốm đau' sMoTa, 
			null fTongSo, 
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from TBL_DTC where  sXauNoiMa like '9010002-010-011-0001%'
	union all 
	select 3 rowNum, 
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Thai sản' sMoTa, 
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0002%'
	union all 
	select 4 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Trợ cấp tai nạn lao động, BNN' sMoTa,
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from TBL_DTC where  sXauNoiMa like '9010002-010-011-0003%'
	union all 
	select 5 rowNum, 
			0 bHangCha,
			null stt, 
			N'- Trợ cấp hưu trí' sMoTa,
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from TBL_DTC where  sXauNoiMa like '9010002-010-011-0004%'
	union all 
	select 6 rowNum,
			0 bHangCha, 
			null stt,
			N'- Trợ cấp Phục viên' sMoTa,
			null fTongSo,
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from TBL_DTC where  sXauNoiMa like '9010002-010-011-0005%'
	union all 
	select 7 rowNum,
			0 bHangCha,
			null stt,
			N'- Trợ cấp Xuất ngũ' sMoTa,
			null fTongSo, 
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from TBL_DTC where  sXauNoiMa like '9010002-010-011-0006%'
	union all 
	select 8 rowNum,
			0 bHangCha, 
			null stt,
			N'- Trợ cấp thôi việc' sMoTa,
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from TBL_DTC where  sXauNoiMa like '9010002-010-011-0007%'
	union all 
	select 9 rowNum, 
			0 bHangCha,
			null stt,
			N'- Trợ cấp Tử tuất' sMoTa, 
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from TBL_DTC where  sXauNoiMa like '9010002-010-011-0008%'
	union all 
	select 10 rowNum,
			0 bHangCha, 
			2 stt,
			N'Kinh phí quản lý BHXH, BHYT' sMoTa, 
			null fTongSo, 
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010003%' 
	union all 
	select 11 rowNum,
			0 bHangCha, 
			3 stt, 
			N'Kinh phí KCB tại quân y đơn vị' sMoTa,
			null fTongSo,
			null fDuToan,
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%'
	union all 
	select 12 rowNum, 
			0 bHangCha, 
			4 stt, 
			N'Kinh phí KCB tại Trường sa - DK' sMoTa, 
			null fTongSo,
			null fDuToan, 
			sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fTongTien, 0)/1000000, 0)* 1000000 ELSE isnull(fTongTien, 0) END) fHachToan
	from #tempKhoiHoachToan where sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%'
	) chihachtoan

	--if (@IsMillionRound = 0)
	--begin
	select dt.rowNum, 
			dt.bHangCha, 
			dt.stt, 
			dt.sMoTa, 
			(isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0))/@DVT fTongSoChi, 
			dt.fDuToan/@DVT fDuToan, 
			ht.fHachToan/@DVT fHachToan
	into TBL_DTC_RESULT
	from TBL_CHI_CHEDO_DUTOAN dt
	left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	update TBL_DTC_RESULT set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT where bHangCha = 0),
							fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT where bHangCha = 0),
							fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT where bHangCha = 0)
						where bHangCha = 1 and stt = 1

	--result
	select * from TBL_DTC_RESULT
	--end
	--else 
	--	begin
	--select dt.rowNum, 
	--		dt.bHangCha, 
	--		dt.stt, 
	--		dt.sMoTa, 
	--		round((isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0)) / 1000000, 0) * 1000000 /@DVT fTongSoChi, 
	--		round(dt.fDuToan / 1000000, 0) * 1000000 / @DVT fDuToan, 
	--		round(dt.fHachToan / 1000000, 0) * 1000000 / @DVT fHachToan
	--into TBL_DTC_RESULT1
	--from TBL_CHI_CHEDO_DUTOAN dt
	--left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	--update TBL_DTC_RESULT1 set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT1 where bHangCha = 0),
	--						fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT1 where bHangCha = 0),
	--						fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT1 where bHangCha = 0)
	--					where bHangCha = 1 and stt = 1

	----result
	--select * from TBL_DTC_RESULT1
	--end

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC]') AND type in (N'U')) drop table TBL_DTC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_DUTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_DUTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_HACHTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_HACHTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT]') AND type in (N'U')) drop table TBL_DTC_RESULT;
	--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT1]') AND type in (N'U')) drop table TBL_DTC_RESULT1;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 8/9/2024 2:45:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200),
	@IsMillionRound bit
AS
BEGIN
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.*, dv.iKhoi into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec 
		and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
		and Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
	--Thu BHXH
	select * into TBL_THU_BHXH from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Thu BHXH' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai

	union all

	select 2 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NSD, 0) END)),0) fTongSo,
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NLD/1000000, 0)* 1000000 ELSE fBHXH_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NSD/1000000, 0)* 1000000 ELSE fBHXH_NSD END), 0) fNSD, 
			null sLoai
	--from TBL_DTT where iKhoi = 2 --sXauNoiMa like '9020001%'
	from TBL_DTT where sXauNoiMa like '9020001%'

	union all

	select 3 rowNum, 
		   0 bHangCha, 
		   null stt, 
		   N'- Đơn vị hạch toán' sMoTa, 
		   ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHXH_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHXH_NSD, 0) END)),0) fTongSo, 
		   ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NLD/1000000, 0)* 1000000 ELSE fBHXH_NLD END), 0) fNLD, 
		   ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHXH_NSD/1000000, 0)* 1000000 ELSE fBHXH_NSD END),0) fNSD,
		   null sLoai
	--from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	from TBL_DTT where sXauNoiMa like '9020002%'
	) thubhxh

	update TBL_THU_BHXH set fTongSo = (select sum(fTongSo) from TBL_THU_BHXH where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHXH where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHXH where bHangCha = 0)
						where bHangCha = 1

	--Thu BHTN
	select * into TBL_THU_BHTN from
	(select 4 rowNum, 1 bHangCha, '2' stt, N'Thu BHTN' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai

	union all

	select 5 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NLD/1000000, 0)* 1000000 ELSE fBHTN_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NSD/1000000, 0)* 1000000 ELSE fBHTN_NSD END),0) fNSD, 
			null sLoai
	--from TBL_DTT where iKhoi = 2 --sXauNoiMa like '9020001%'
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all

	select 6 rowNum, 
			0 bHangCha,
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHTN_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHTN_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NLD/1000000, 0)* 1000000 ELSE fBHTN_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHTN_NSD/1000000, 0)* 1000000 ELSE fBHTN_NSD END),0) fNSD,  
			null sLoai
	--from TBL_DTT where iKhoi <> 2 --sXauNoiMa like '9020002%'
	from TBL_DTT where sXauNoiMa like '9020002%'
	) thubhtn

	update TBL_THU_BHTN set fTongSo = (select sum(fTongSo) from TBL_THU_BHTN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHTN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHTN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT quân nhân
	select * into TBL_THU_BHYT_QN from
	(select 7 rowNum, 1 bHangCha, '3' stt, N'Thu BHYT quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai

	union all
	
	select 8 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD, 
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%')
	--					and iKhoi = 2
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' )
					
	union all
	
	select 9 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD, 
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0001%' or sXauNoiMa like '9020002-010-011-0001%')
	--					and iKhoi <> 2
	from TBL_DTT where (sXauNoiMa like '9020002-010-011-0001%' )
						) thubhytquannhan

	update TBL_THU_BHYT_QN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_QN where bHangCha = 0)
	where bHangCha = 1

	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_NLD from
	(select 10 rowNum, 1 bHangCha, '4' stt, N'Thu BHYT người lao động' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	
	union all
	
	select 11 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD,
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020002-010-011-0002%')
	--					and iKhoi = 2
	from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%')
						
	union all
	
	select 12 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND((sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NLD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NLD, 0) END) + sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(isnull(fBHYT_NSD, 0)/1000000, 0)* 1000000 ELSE isnull(fBHYT_NSD, 0) END)),0) fTongSo, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NLD/1000000, 0)* 1000000 ELSE fBHYT_NLD END),0) fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fBHYT_NSD/1000000, 0)* 1000000 ELSE fBHYT_NSD END),0) fNSD,
			null sLoai
	--from TBL_DTT where (sXauNoiMa like '9020001-010-011-0002%' or sXauNoiMa like '9020002-010-011-0002%')
	--					and iKhoi <> 2
	from TBL_DTT where ( sXauNoiMa like '9020002-010-011-0002%')
						) thubhytnld

	update TBL_THU_BHYT_NLD set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_NLD where bHangCha = 0)
						where bHangCha = 1
	
	--Dữ liệu phân bổ dự toán thu mua BHYT
	select ctct.*, dv.iKhoi into TBL_DTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	join  BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	left join DonVi dv on ctct.iid_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = @NamLamViec and dv.iTrangThai = 1
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_TN from
	(select 13 rowNum, 1 bHangCha, '5' stt, N'Thu BHYT thân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN' sLoai
	
	union all
	
	select 14 rowNum, 1 bHangCha, 'a' stt, N'Quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	union all
	select 15 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo, 
			null fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD, 
			N'BHYT_THANNHAN_QN' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' or sXauNoiMa like '9030001-010-011-0002%')
	--					and iKhoi = 2
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' )
				
	union all
	select 16 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo, 
			null fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD, 
			N'BHYT_THANNHAN_QN' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0001%' or sXauNoiMa like '9030001-010-011-0002%')
	--					and iKhoi <> 2
	from TBL_DTTM where ( sXauNoiMa like '9030002-010-011-0002%')
						
	union all

	select 17 rowNum,
			1 bHangCha, 
			'b' stt, 
			N'Công nhân, VCQP' sMoTa, 
			null fTongSo, 
			null fNLD, 
			null fNSD, 
			N'BHYT_THANNHAN_VCQP' sLoai

	union all

	select 18 rowNum,
			0 bHangCha, 
			null stt, 
			N'- Đơn vị dự toán' sMoTa, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo, 
			null fNLD, 
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD, 
			N'BHYT_THANNHAN_VCQP' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%')
	--					and iKhoi = 2
	from TBL_DTTM where (sXauNoiMa like '9030001-010-011-0000%' )

	union all

	select 19 rowNum, 
			0 bHangCha, 
			null stt, 
			N'- Đơn vị hạch toán' sMoTa,
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fTongSo,
			null fNLD,
			ROUND(sum(CASE WHEN @IsMillionRound = 1 THEN ROUND(fDuToan/1000000, 0)* 1000000 ELSE fDuToan END),0) fNSD,
			N'BHYT_THANNHAN_VCQP' sLoai
	--from TBL_DTTM where (sXauNoiMa like '9030002-010-011-0000%' or sXauNoiMa like '9030002-010-011-0001%')
	--					and iKhoi <> 2
	from TBL_DTTM where ( sXauNoiMa like '9030002-010-011-0001%')
						
	) thubhytnld

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_QN'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_VCQP'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN'

	select * into TBL_THU from
	(select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHXH
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHTN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_QN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_NLD
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_TN) tblthu

	--Result
	--if (@IsMillionRound = 1)
	--select
	--rowNum, 
	--bHangCha, 
	--stt, 
	--sMoTa, 
	--round(fTongSo / 1000000, 0) * 1000000 /@DVT fTongSo, 
	--round(fNLD / 1000000, 0) * 1000000 /@DVT fNLD, 
	--round(fNSD / 1000000, 0) * 1000000 /@DVT fNSD
	--from TBL_THU
	--else
	select
	rowNum, 
	bHangCha, 
	stt, 
	sMoTa, 
	fTongSo/@DVT fTongSo, 
	fNLD/@DVT fNLD, 
	fNSD/@DVT fNSD
	from TBL_THU

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTT]') AND type in (N'U')) drop table TBL_DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHXH]') AND type in (N'U')) drop table TBL_THU_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHTN]') AND type in (N'U')) drop table TBL_THU_BHTN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_QN]') AND type in (N'U')) drop table TBL_THU_BHYT_QN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_NLD]') AND type in (N'U')) drop table TBL_THU_BHYT_NLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTTM]') AND type in (N'U')) drop table TBL_DTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_TN]') AND type in (N'U')) drop table TBL_THU_BHYT_TN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU]') AND type in (N'U')) drop table TBL_THU;

END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 8/9/2024 2:55:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 8/9/2024 2:55:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh] 
	-- Add the parameters for the stored procedure here
 	@sXauNoiMa nvarchar(max),
	@sQuarter nvarchar(50) ,
	@Donvi  nvarchar(50),
	@NamLamViec int,
	@sLNS nvarchar(50)
	
AS
BEGIN
	DECLARE @COUNT INT;
	DECLARE @STongHop nvarchar(max);	
	DECLARE @XauNoiMaREPLACE nvarchar(max);
	DECLARE @SMACHEDO nvarchar(max);	
	set @XauNoiMaREPLACE =case when @sXauNoiMa like '9010001%' 
									then REPLACE(@sXauNoiMa,'9010001-' , '') 
									else REPLACE(@sXauNoiMa,'9010002-' , '') end  

	SELECT sMaCheDo 
			, bDisplay
			, case when sXauNoiMaMlnsBHXH like '9010001%' 
			then REPLACE(sXauNoiMaMlnsBHXH,'9010001-' , '')
			else REPLACE(sXauNoiMaMlnsBHXH,'9010002-' , '') end  sXauNoiMaMlnsBHXH,
			sLoaiTruyLinh
				into #tempTL_DM_CheDoBHXH
			FROM TL_DM_CheDoBHXH
			WHERE bDisplay = 1
	select * into #temp1 from TL_DS_CapNhap_BangLuong
	where Nam=@NamLamViec
	and Thang in  (select * from splitstring(@sQuarter))
	and Ma_CBo=@Donvi
	and STongHop is not null
	and Ma_CachTL='CACH2'
	and Status=1

		-- Get bang luong chi tiet tồn tai cả luong va truy linh
	SELECT bangluongchitiet.* into #temp2 from TL_BangLuong_ThangBHXH bangluongchitiet  
	inner join #temp1 bangluong on bangluong.Id=bangluongchitiet.iID_Parent and bangluong.Thang=bangluongchitiet.iThang
	where
		 bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
										FROM #tempTL_DM_CheDoBHXH
										WHERE bDisplay = 1 
										AND sXauNoiMaMlnsBHXH = @XauNoiMaREPLACE )
	AND (
		bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
	)

	SET @COUNT = (SELECT COUNT(*) FROM #temp2)
	-- Tồn tại luong va truy linh 
	IF	@COUNT>0
	BEGIN
	--- Lấy tiền truy lĩnh---
	SELECT tbl.*, t2.fSoNgayTruyLinh, t2.nGiaTriTruyLinh INTO #temp3
	FROM #temp2 tbl
	LEFT JOIN 
		(SELECT tbl.sMaCBo , tbl.sMaDonVi, tbl.iThang, tbl.iNam, SUM(ISNULL(canbo.fSoNgayHuongBHXH, 0)) fSoNgayTruyLinh, SUM(ISNULL(tbl.nGiaTri, 0)) as nGiaTriTruyLinh
		FROM TL_BangLuong_ThangBHXH tbl
		INNER JOIN #temp2 t2 ON tbl.sMaCBo = t2.sMaCBo AND tbl.sMaDonVi = t2.sMaDonVi AND t2.iThang = tbl.iThang AND tbl.iNam = t2.iNam 
		LEFT JOIN TL_CanBo_CheDoBHXH canbo ON tbl.sMaCBo = canbo.sMaCanBo and tbl.sMaCheDo=canbo.sMaCheDo
		WHERE tbl.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH where sLoaiTruyLinh IN (select sMaCheDo from #temp2 ))
		GROUP BY tbl.sMaCBo , tbl.sMaDonVi, tbl.iThang, tbl.iNam
		) as t2 ON  tbl.sMaCBo = t2.sMaCBo AND tbl.sMaDonVi = t2.sMaDonVi AND t2.iThang = tbl.iThang AND tbl.iNam = t2.iNam 
	--- Get ket qua
	SELECT 
		@sXauNoiMa as SXauNoiMa,
		sMaHieuCanBo as SMaHieuCanBo,
		sTenCbo as STenCanBo,
		sMaCBo As SMaCanBo,
		sMaCB as SMaCapBac,
		capbac.Note aS STenCapBac,
		bangluongchitiet.sMaDonVi as ID_MaPhanHo,
		dv.Ten_DonVi as STenPhanHo,
		canbo.fSoNgayHuongBHXH as ISoNgayHuong,
		canbo.sSoQuyetDinh as SSoQuyetDinh,
		canbo.dNgayQuyetDinh as DNgayQuyetDinh,
		bangluongchitiet.nGiaTri as FSoTien,
		bangluongchitiet.nGiaTriTruyLinh as FTienTruyLinh,
		bangluongchitiet.fSoNgayTruyLinh as ISoNgayTruyLinh,
		canbo.dTuNgay AS DTuNgay,
		canbo.dDenNgay AS DDenNgay,
		bangluongchitiet.sMaCB
	FROM #temp3 bangluongchitiet 
	LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluongchitiet.sMaCBo = canbo.sMaCanBo and bangluongchitiet.sMaCheDo=canbo.sMaCheDo
	LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluongchitiet.sMaCB
	LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluongchitiet.sMaDonVi
	where  (
			bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
		)
		and canbo.iNam=@NamLamViec
	DROP TABLE #temp3
	END
	-- Tồn tại truy linh 
	ELSE
	BEGIN

	SET @SMACHEDO = (SELECT sMaCheDo FROM #tempTL_DM_CheDoBHXH
										WHERE bDisplay = 1 
										AND sXauNoiMaMlnsBHXH = @XauNoiMaREPLACE)
	--- Lấy tiền truy lĩnh---
	select bangluongchitiet.* into #temp4
	from TL_BangLuong_ThangBHXH bangluongchitiet  
	inner join #temp1 bangluong on bangluong.Id=bangluongchitiet.iID_Parent and bangluong.Thang=bangluongchitiet.iThang
	where
		 bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo
										FROM TL_DM_CheDoBHXH
										WHERE bDisplay = 1 
										AND sLoaiTruyLinh = @SMACHEDO )
	AND (
		bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
	)

	--- Get ket qua 
	SELECT 
		@sXauNoiMa as SXauNoiMa,
		sMaHieuCanBo as SMaHieuCanBo,
		sTenCbo as STenCanBo,
		sMaCBo As SMaCanBo,
		sMaCB as SMaCapBac,
		capbac.Note aS STenCapBac,
		bangluongchitiet.sMaDonVi as ID_MaPhanHo,
		dv.Ten_DonVi as STenPhanHo,
		0 as ISoNgayHuong,
		canbo.sSoQuyetDinh as SSoQuyetDinh,
		canbo.dNgayQuyetDinh as DNgayQuyetDinh,
		0 as FSoTien,
		bangluongchitiet.nGiaTri as FTienTruyLinh,
		canbo.fSoNgayHuongBHXH as ISoNgayTruyLinh,
		canbo.dTuNgay AS DTuNgay,
		canbo.dDenNgay AS DDenNgay,
		bangluongchitiet.sMaCB
	FROM #temp4 bangluongchitiet 
	LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluongchitiet.sMaCBo = canbo.sMaCanBo and bangluongchitiet.sMaCheDo=canbo.sMaCheDo
	LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluongchitiet.sMaCB
	LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluongchitiet.sMaDonVi
	where  (
			bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
		)
		and canbo.iNam=@NamLamViec

		DROP TABLE #temp4
	END
DROP TABLE #temp2,#temp1,#tempTL_DM_CheDoBHXH;
	
END
;
;
;
;
;
;
GO

Update TL_DM_CheDoBHXH
set bTinhCN=1, bTinhNgayLe=1
where sMaCheDo='BENHDAINGAY_D14NGAY'

Update TL_DM_CheDoBHXH
set bTinhCN=1, bTinhNgayLe=1
where sMaCheDo='BENHDAINGAY_T14NGAY'

Update TL_DM_CheDoBHXH
set bTinhCN=1, bTinhNgayLe=1
where sMaCheDo='OMDAU_DUONGSUCPHSK'

Update TL_DM_CheDoBHXH
set bTinhCN=1, bTinhNgayLe=1
where sMaCheDo='KHHGD'

Update TL_DM_CheDoBHXH
set bTinhCN=1, bTinhNgayLe=1
where sMaCheDo='THAISAN_DUONGSUCPHSK'

Update TL_DM_CheDoBHXH
set bTinhT7=1


/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]    Script Date: 8/12/2024 3:28:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 8/12/2024 3:28:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 8/12/2024 3:28:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 8/12/2024 3:28:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 8/12/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX),
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

		DECLARE @Quy INT;
		SELECT @Quy =(SELECT top(1)  iQuyChungTu
		FROM BH_QTC_Quy_CheDoBHXH 
		WHERE  ID_QTC_Quy_CheDoBHXH IN
		(SELECT *
		FROM f_split(@IdChungTu)))

		SELECT 
			(SUM(isnull(fTienCNVCQP_DeNghi, 0)) +
			SUM (isnull(fTienHSQBS_DeNghi, 0)) +
			SUM (isnull(fTienLDHD_DeNghi, 0)) +
			SUM (isnull(fTienQNCN_DeNghi, 0)) +
			SUM (isnull(fTienSQ_DeNghi, 0)) )fTienLuyKeCuoiQuyNay,
			(SUM (isnull(iSoCNVCQP_DeNghi, 0)) +
			SUM (isnull(iSoHSQBS_DeNghi, 0)) +
			SUM (isnull(iSoLDHD_DeNghi, 0)) +
			SUM (isnull(iSoQNCN_DeNghi, 0)) +
			SUM (isnull(iSoSQ_DeNghi, 0))  )iSoLuyKeCuoiQuyNay,
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.iID_MucLucNganSach,
			sXauNoiMa
			into #temp2
		FROM
			(
				SELECT fTienCNVCQP_DeNghi, fTienHSQBS_DeNghi, fTienLDHD_DeNghi, fTienQNCN_DeNghi, fTienSQ_DeNghi, iSoCNVCQP_DeNghi, fTongTien_PheDuyet, iSoHSQBS_DeNghi,
					iSoLDHD_DeNghi,  iSoQNCN_DeNghi, iSoSQ_DeNghi, iID_QTC_Quy_CheDoBHXH, iID_MucLucNganSach,ct.sXauNoiMa
				FROM BH_QTC_Quy_CheDoBHXH_ChiTiet ct
				INNER JOIN BH_DM_MucLucNganSach dmns on ct.sXauNoiMa = dmns.sXauNoiMa
				where dmns.bHangCha = 0 and dmns.iNamLamViec = @YearOfWork) ctct
				INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
					WHERE
			ct.iQuyChungTu < @Quy 
			and ct.iNamChungTu=@YearOfWork
			and ct.iID_MaDonVi=@MaDonVi

		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.iID_MucLucNganSach,
			ctct.sXauNoiMa

		select ml.iID_MLNS
		,ml.sMoTa
		,ml.sXauNoiMa
		,T2.fTienLuyKeCuoiQuyNay
		,T2.iSoLuyKeCuoiQuyNay
		into #tblmlns
		from BH_DM_MucLucNganSach ml
		left join #temp2 T2 on ml.sXauNoiMa=t2.sXauNoiMa
		where ml.iTrangThai=1
		and(ml.sLNS='9010001' or ml.sLNS='9010002')
		and iNamLamViec=2024
		and bHangCha=0

		SELECT 
		   iID_MucLucNganSach,
		   sLoaiTroCap,
		   Sum(iSoLuyKeCuoiQuyNay) iSoLuyKeCuoiQuyNay,
		   SUM(fTienLuyKeCuoiQuyNay) fTienLuyKeCuoiQuyNay,
		   SUM(iSoSQ_DeNghi) iSoSQ_DeNghi,
		   SUM(fTienSQ_DeNghi) fTienSQ_DeNghi,
		   SUM(iSoQNCN_DeNghi) iSoQNCN_DeNghi,
		   SUM(fTienQNCN_DeNghi) fTienQNCN_DeNghi,
		   SUM(iSoCNVCQP_DeNghi) iSoCNVCQP_DeNghi,
		   SUM(fTienCNVCQP_DeNghi) fTienCNVCQP_DeNghi,
		   SUM(iSoHSQBS_DeNghi) iSoHSQBS_DeNghi,
		   SUM(fTienHSQBS_DeNghi) fTienHSQBS_DeNghi,
		   SUM(iSoLDHD_DeNghi) iSoLDHD_DeNghi,
		   SUM(fTienLDHD_DeNghi) fTienLDHD_DeNghi,
		   SUM(iTongSo_DeNghi) iTongSo_DeNghi,
		   SUM(fTongTien_DeNghi) fTongTien_DeNghi,
		   SUM(fTongTien_PheDuyet) fTongTien_PheDuyet,
		   sXauNoiMa
		   into #temp1
	FROM BH_QTC_Quy_CheDoBHXH_ChiTiet
	WHERE  iID_QTC_Quy_CheDoBHXH IN
		(SELECT *
		 FROM f_split(@IdChungTu))
	group by iID_MucLucNganSach, sLoaiTroCap,sXauNoiMa

	select 
			isnull(T2.fTienLuyKeCuoiQuyNay,0) 
			+isnull(T1.fTienCNVCQP_DeNghi,0)
			+isnull(T1.fTienHSQBS_DeNghi,0)
			+isnull(T1.fTienLDHD_DeNghi,0)
			+isnull(T1.fTienQNCN_DeNghi,0)
			+isnull(T1.fTienSQ_DeNghi,0) fTienLuyKeCuoiQuyNay
			,isnull(t2.iSoLuyKeCuoiQuyNay,0)
			+ isnull(T1.iSoCNVCQP_DeNghi,0)
			+ isnull(T1.iSoHSQBS_DeNghi,0)
			+ isnull(T1.iSoLDHD_DeNghi,0)
			+ isnull(T1.iSoQNCN_DeNghi,0)
			+ isnull(T1.iSoSQ_DeNghi,0) iSoLuyKeCuoiQuyNay
			,T1.fTienCNVCQP_DeNghi
			, T1.fTienHSQBS_DeNghi
			, T1.fTienLDHD_DeNghi
			, T1.fTienQNCN_DeNghi
			, T1.fTienSQ_DeNghi
			,T1.iSoCNVCQP_DeNghi
			,T1.iSoHSQBS_DeNghi
			,T1.iSoLDHD_DeNghi
			,T1.iSoQNCN_DeNghi
			,T1.iSoSQ_DeNghi
			,t1.fTongTien_DeNghi
			,t1.iTongSo_DeNghi
			,t1.fTongTien_PheDuyet
			,T2.sXauNoiMa
			,t2.iID_MLNS iID_MucLucNganSach
			,t2.sMoTa sLoaiTroCap
			into #tempDuLieu
			FROM 
			#tblmlns T2
			left JOIN  #temp1 T1 ON T2.sXauNoiMa=T1.sXauNoiMa AND T2.iID_MLNS=T1.iID_MucLucNganSach
			where (T1.fTienCNVCQP_DeNghi<>0 or T1.fTienHSQBS_DeNghi<>0 or T1.fTienLDHD_DeNghi<>0 or T1.fTienQNCN_DeNghi<>0 or T1.fTienSQ_DeNghi<> 0
			or T1.iSoCNVCQP_DeNghi<>0 or  T1.iSoHSQBS_DeNghi<>0 or T1.iSoQNCN_DeNghi<> 0 or  T1.iSoLDHD_DeNghi<>0 or T1.iSoSQ_DeNghi<>0
			or T2.fTienLuyKeCuoiQuyNay<>0 or t2.iSoLuyKeCuoiQuyNay<>0 or t1.fTongTien_DeNghi<>0 or t1.iTongSo_DeNghi<> 0 or t1.fTongTien_PheDuyet<>0)


INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet(
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		sLoaiTroCap,
		dNgaySua,
		dNgayTao,
		sNguoiSua,
		sNguoiTao,
		fTienDuToanDuyet,
		iSoLuyKeCuoiQuyNay,
		fTienLuyKeCuoiQuyNay,
		iSoSQ_DeNghi,
		fTienSQ_DeNghi,
		iSoQNCN_DeNghi,
		fTienQNCN_DeNghi,
		iSoCNVCQP_DeNghi,
		fTienCNVCQP_DeNghi,
		iSoHSQBS_DeNghi,
		fTienHSQBS_DeNghi,
		iSoLDHD_DeNghi,
		fTienLDHD_DeNghi,
		iTongSo_DeNghi,
		fTongTien_DeNghi,
		fTongTien_PheDuyet,
		iNamLamViec,
		sXauNoiMa,
		iIDMaDonVi
	)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sLoaiTroCap,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   null,
	   SUM(iSoLuyKeCuoiQuyNay),
	   SUM(fTienLuyKeCuoiQuyNay),
	   SUM(iSoSQ_DeNghi),
	   SUM(fTienSQ_DeNghi),
	   SUM(iSoQNCN_DeNghi),
	   SUM(fTienQNCN_DeNghi),
	   SUM(iSoCNVCQP_DeNghi),
	   SUM(fTienCNVCQP_DeNghi),
	   SUM(iSoHSQBS_DeNghi),
	   SUM(fTienHSQBS_DeNghi),
	   SUM(iSoLDHD_DeNghi),
	   SUM(fTienLDHD_DeNghi),
	   SUM(iTongSo_DeNghi),
	   SUM(fTongTien_DeNghi),
	   SUM(fTongTien_PheDuyet),
	   @YearOfWork,
	   sXauNoiMa,
	   @MaDonVi
FROM #tempDuLieu
group by iID_MucLucNganSach,sLoaiTroCap,sXauNoiMa

INSERT INTO [dbo].[BH_QTC_Quy_CTCT_GiaiThichTroCap]
           ([iiD_QTC_Quy_CTCT_GiaiThichTroCap]
           ,[dNgayQuyetDinh]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[fSoTien]
           ,[iiD_MaDonVi]
           ,[iiD_MaPhanHo]
           ,[iID_QTC_Quy_ChungTu]
           ,[iNamLamViec]
           ,[iQuy]
           ,[iSoNgayHuong]
           ,[sMa_Hieu_Can_Bo]
           ,[sNguoiSua]
           ,[sNguoiTao]
           ,[sSoQuyetDinh]
           ,[sTenCanBo]
           ,[sTenCapBac]
           ,[sTenPhanHo]
           ,[sXauNoiMa]
           ,[dDenNgay]
           ,[dTuNgay]
           ,[sSoSoBHXH]
           ,[fTienLuongThangDongBHXH]
           ,[sMaCapBac]
           ,[fTienTruyLinh]
           ,[iSoNgayTruyLinh])
	 SELECT 
	   NEWID(),
	   dNgayQuyetDinh,
       null,
	   GETDATE(),
       fSoTien,
	   @MaDonVi,
	   iiD_MaPhanHo,
	   @IdChungTuSummary,
	   @YearOfWork,
	   iQuy,
	   iSoNgayHuong,
	   sMa_Hieu_Can_Bo,
	   '',
	   @NguoiTao,
	   sSoQuyetDinh,
	   sTenCanBo,
	   sTenCapBac,
	   sTenPhanHo,
	   sXauNoiMa,
	   dDenNgay,
	   dTuNgay,
	   sSoSoBHXH,
	   fTienLuongThangDongBHXH,
	   sMaCapBac,
	   fTienTruyLinh,
	   iSoNgayTruyLinh

FROM BH_QTC_Quy_CTCT_GiaiThichTroCap
WHERE  iID_QTC_Quy_ChungTu IN
    (SELECT *
     FROM f_split(@IdChungTu))

UPDATE BH_QTC_Quy_CheDoBHXH SET iLoaiTongHop=2 , bDaTongHop=1  WHERE ID_QTC_Quy_CheDoBHXH IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]    Script Date: 8/12/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_datafor_quaterbefore]
@IdChungTu nvarchar(MAX),
@Username nvarchar(500),
@NamLamViec int,
@Quy int,
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet
	( 
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		iNamLamViec,
		sLoaiTroCap,
		dNgayTao,
		sNguoiTao,
		iSoLuyKeCuoiQuyNay,
		fTienLuyKeCuoiQuyNay,
		sXauNoiMa,
		iIDMaDonVi
	
	)
	SELECT 
		NEWID(),
		@IdChungTu,
		qtcn_ct.iID_MucLucNganSach,
		@NamLamViec,
		qtcn_ct.sLoaiTroCap,
		GETDATE(),
		@Username,
		SUM(qtcn_ct.iSoLuyKeCuoiQuyNay),
		SUM(qtcn_ct.fTienLuyKeCuoiQuyNay),
		sXauNoiMa,
		@MaDonVi
	FROM BH_QTC_Quy_CheDoBHXH_ChiTiet qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
	WHERE qtcn.iQuyChungTu < @Quy
			AND qtcn.iID_MaDonVi = @MaDonVi
			AND qtcn.iNamChungTu = @NamLamViec
			--AND qtcn.bIsKhoa=1
		GROUP BY qtcn_ct.iID_MucLucNganSach, qtcn_ct.sLoaiTroCap,sXauNoiMa
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 8/12/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
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
			danhmuc.sDuToanChiTietToi,
			danhmuc.bHangCha,
			danhmuc.bHangChaDuToan
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		and danhmuc.iTrangThai = 1
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sXauNoiMa,
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			Sum(qtcn_ct.iSoLuyKeCuoiQuyNay) as iSoLuyKeCuoiQuyNay,
			Sum(qtcn_ct.fTienLuyKeCuoiQuyNay) as fTienLuyKeCuoiQuyNay,
			sum(qtcn_ct.iSoSQ_DeNghi) as iSoSQ_DeNghi ,
			sum(qtcn_ct.fTienSQ_DeNghi) as fTienSQ_DeNghi ,
			sum(qtcn_ct.iSoQNCN_DeNghi) as iSoQNCN_DeNghi,
			sum(qtcn_ct.fTienQNCN_DeNghi) as fTienQNCN_DeNghi ,
			sum(qtcn_ct.iSoCNVCQP_DeNghi) as iSoCNVCQP_DeNghi ,
			sum(qtcn_ct.fTienCNVCQP_DeNghi) as fTienCNVCQP_DeNghi,
			sum(qtcn_ct.iSoHSQBS_DeNghi) as iSoHSQBS_DeNghi,
			sum(qtcn_ct.fTienHSQBS_DeNghi) as fTienHSQBS_DeNghi,
			sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
			sum(qtcn_ct.fTongTien_DeNghi) as fTongTien_DeNghi,
			sum(qtcn_ct.fTongTien_PheDuyet) as fTongTien_PheDuyet,
			sum(qtcn_ct.iSoLDHD_DeNghi) as iSoLDHD_DeNghi,
			sum(qtcn_ct.fTienLDHD_DeNghi) as fTienLDHD_DeNghi
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		--and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sXauNoiMa

		--- Get tien du toan 
		SELECT ctct.sXauNoiMa,
			SUM(ctct.fTienTuChi)  fTienDuToanDuyet
			into #tempDuToan
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @INamLamViec
				GROUP BY ctct.sXauNoiMa

		--- Get tien du toan 
		SELECT ctct.sXauNoiMa,
			SUM(ctct.fTienTuChi)  fTienDuToanDuyet
			into #tempDuToanTrenGiao
			FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
			JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
				WHERE ctct.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
				AND BIsKhoa = 1
				AND ct.iNamLamViec = @INamLamViec
				GROUP BY ctct.sXauNoiMa
	--- lay luy kê quy truoc
SELECT SUM
			(isnull(fTienCNVCQP_DeNghi, 0)) fTienCNVCQP_DeNghi,
			SUM (isnull(fTienHSQBS_DeNghi, 0)) fTienHSQBS_DeNghi,
			SUM (isnull(fTienLDHD_DeNghi, 0)) fTienLDHD_DeNghi,
			SUM (isnull(fTienQNCN_DeNghi, 0)) fTienQNCN_DeNghi,
			SUM (isnull(fTienSQ_DeNghi, 0)) fTienSQ_DeNghi,
			SUM (isnull(iSoCNVCQP_DeNghi, 0)) iSoCNVCQP_DeNghi,
			SUM (isnull(ctct.fTongTien_PheDuyet, 0)) fTongTien_PheDuyet,
			SUM (isnull(iSoHSQBS_DeNghi, 0)) iSoHSQBS_DeNghi,
			SUM (isnull(iSoLDHD_DeNghi, 0)) iSoLDHD_DeNghi,
			SUM (isnull(iSoQNCN_DeNghi, 0)) iSoQNCN_DeNghi,
			SUM (isnull(iSoSQ_DeNghi, 0)) iSoSQ_DeNghi,
			ct.iNamChungTu,
			ctct.sXauNoiMa
			into #tempDuLieuQuyTruoc
		FROM
			(
				SELECT fTienCNVCQP_DeNghi, fTienHSQBS_DeNghi, fTienLDHD_DeNghi, fTienQNCN_DeNghi, fTienSQ_DeNghi, iSoCNVCQP_DeNghi, fTongTien_PheDuyet, iSoHSQBS_DeNghi,
					iSoLDHD_DeNghi,  iSoQNCN_DeNghi, iSoSQ_DeNghi, iID_QTC_Quy_CheDoBHXH, ct.sXauNoiMa
				FROM BH_QTC_Quy_CheDoBHXH_ChiTiet ct
				INNER JOIN BH_DM_MucLucNganSach dmns on ct.sXauNoiMa = dmns.sXauNoiMa
				where dmns.bHangCha = 0 and dmns.iNamLamViec = @INamLamViec  and ct.iIDMaDonVi in (select * from dbo.splitstring(@IdMaDonVi))
			)ctct
			INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
		WHERE
			ct.iQuyChungTu < @IQuy 
			and ct.iNamChungTu=@INamLamViec
		GROUP BY
			ct.iNamChungTu,
			ctct.sXauNoiMa

	IF @IsTongHop=0
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
				mucluc.sDuToanChiTietToi,
				mucluc.bHangCha,
				mucluc.iID_MLNS as iID_MucLucNganSach,
				mucluc.sMoTa as sLoaiTroCap,
				mucluc.bHangChaDuToan,
				duToan.fTienDuToanDuyet,
				(
				isnull(duLieuQuyTruoc.fTienCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienSQ_DeNghi, 0) 
				) fTienLuyKeCuoiQuyTruoc,
				(
					isnull(duLieuQuyTruoc.iSoCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoSQ_DeNghi, 0) 
				) iSoLuyKeCuoiQuyTruoc,
				chi_tiet.iSoSQ_DeNghi,
				chi_tiet.fTienSQ_DeNghi,
				chi_tiet.iSoQNCN_DeNghi,
				chi_tiet.fTienQNCN_DeNghi,
				chi_tiet.iSoCNVCQP_DeNghi,
				chi_tiet.fTienCNVCQP_DeNghi,
				chi_tiet.iSoHSQBS_DeNghi,
				chi_tiet.fTienHSQBS_DeNghi,
				chi_tiet.iTongSo_DeNghi,
				chi_tiet.fTongTien_DeNghi,
				chi_tiet.fTongTien_PheDuyet,
				chi_tiet.iSoLDHD_DeNghi,
				chi_tiet.fTienLDHD_DeNghi

			from #tblMucLucNganSach as mucluc
			left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.sXauNoiMa = chi_tiet.sXauNoiMa
			left join #tempDuToan duToan on mucluc.sXauNoiMa=duToan.sXauNoiMa
			left join #tempDuLieuQuyTruoc duLieuQuyTruoc on mucluc.sXauNoiMa=duLieuQuyTruoc.sXauNoiMa
			order by mucluc.sXauNoiMa
	ELSE
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
				mucluc.sDuToanChiTietToi,
				mucluc.bHangCha,
				mucluc.iID_MLNS as iID_MucLucNganSach,
				mucluc.sMoTa as sLoaiTroCap,
				mucluc.bHangChaDuToan,
				duToan.fTienDuToanDuyet,
				(
				isnull(duLieuQuyTruoc.fTienCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.fTienSQ_DeNghi, 0) 
				) fTienLuyKeCuoiQuyTruoc,
				(
					isnull(duLieuQuyTruoc.iSoCNVCQP_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoHSQBS_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoLDHD_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoQNCN_DeNghi, 0) + isnull(duLieuQuyTruoc.iSoSQ_DeNghi, 0) 
				) iSoLuyKeCuoiQuyTruoc,
				chi_tiet.iSoSQ_DeNghi,
				chi_tiet.fTienSQ_DeNghi,
				chi_tiet.iSoQNCN_DeNghi,
				chi_tiet.fTienQNCN_DeNghi,
				chi_tiet.iSoCNVCQP_DeNghi,
				chi_tiet.fTienCNVCQP_DeNghi,
				chi_tiet.iSoHSQBS_DeNghi,
				chi_tiet.fTienHSQBS_DeNghi,
				chi_tiet.iTongSo_DeNghi,
				chi_tiet.fTongTien_DeNghi,
				chi_tiet.fTongTien_PheDuyet,
				chi_tiet.iSoLDHD_DeNghi,
				chi_tiet.fTienLDHD_DeNghi

			from #tblMucLucNganSach as mucluc
			left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
			left join #tempDuToanTrenGiao duToan on mucluc.sXauNoiMa=duToan.sXauNoiMa
			left join #tempDuLieuQuyTruoc duLieuQuyTruoc on mucluc.sXauNoiMa=duLieuQuyTruoc.sXauNoiMa
			order by mucluc.sXauNoiMa

	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
	drop table #tempDuToan;
	drop table #tempDuLieuQuyTruoc;

end
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]    Script Date: 8/12/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_khoi]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin

---Lấy thông tin chi tiết giai thich tro cap du toan
		SELECT gt.* ,dv.sTenDonVi
			into #TBL_TroCapOmDauDuToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0001%' )
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								--AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec
								AND ct.iQuyChungTu = @IQuy 

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapOmDauHachToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						LEFT JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE ( gt.sXauNoiMa like '9010002-010-011-0001%')
								AND gt.iNamLamViec = @INamLamViec  
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								--AND dv.iKhoi=1
								AND ct.iQuyChungTu = @IQuy 

		select 
		N'I. Khối dự toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempDuToan

		-- SQ Du Toan
		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQDuToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNDuToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPDuToan

		select 
		N'4. HSQBS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSDuToan

		select 
		N'5. LDHD' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDDuToan

----- Hach Toan
		select 
		N'II. Khối hạch toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHachToan

		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQHachToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNHachToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPHachToan

		select 
		N'4. HSQBS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSHachToan

		select 
		N'5. LDHD' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgayDuoi14BenhDaiNgay
		,0 fSoTienDuoi14BenhDaiNgay
		,0 iSoNgayTren14BenhDaiNgay
		,0 fSoTienTren14BenhDaiNgay
		,0 iSoNgayDuoi14OmKhac
		,0 fSoTienDuoi14OmKhac
		,0 iSoNgayTren14OmKhac
		,0 fSoTienTren14OmKhac
		,0 iSoNgayConOm
		,0 fSoTienConOm
		,0 iSoNgayPHSK
		,0 fSoTienPHSK
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDHachToan

--- Lay thong tin giai thich theo khoi du toan
		---Du Toan SQ
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalSQDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

				
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQDuToan 
				FROM #tempDetalSQDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi

		---Du Toan QNCN
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalQNCNDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt

			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

								
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNDuToan 
				FROM #tempDetalQNCNDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi

	---Du Toan CNVCQP
		SELECT 

				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienPHSK
				INTO #tempDetalCNVCQPDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt
		
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

	SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPDuToan 
				FROM #tempDetalCNVCQPDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi

	---Du Toan HSQBS
		SELECT 

				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalHSQBSDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa


		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSDuToan 
				FROM #tempDetalHSQBSDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi

	---Du Toan LDHD
		SELECT 

				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalLDHDDuToanSum 
			FROM #TBL_TroCapOmDauDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDDuToan 
				FROM #tempDetalLDHDDuToanSum
				Group by iIDMaDonVi,
				sTenDonVi


--- Lay thong tin giai thich theo khoi hach toan
		---Hạch Toan SQ
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalSQHachToanSum
			FROM #TBL_TroCapOmDauHachToan gt

			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQHachToan 
				FROM #tempDetalSQHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi


		---Hạch Toan QNCN
		SELECT 

				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalQNCNHachToanSum
			FROM #TBL_TroCapOmDauHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNHachToan 
				FROM #tempDetalQNCNHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi
	---Hạch Toan CNVCQP
		SELECT 

				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalCNVCQPHachToanSum
			FROM #TBL_TroCapOmDauHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPHachToan 
				FROM #tempDetalCNVCQPHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi

	---Hạch Toan HSQBS
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalHSQBSHachToanSum
			FROM #TBL_TroCapOmDauHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSHachToan 
				FROM #tempDetalHSQBSHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi

	---Hạch Toan LDHD
		SELECT 
				gt.iIDMaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01' then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-01'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-01' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienDuoi14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-01-02'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-01-02' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienTren14BenhDaiNgay,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-01'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-01' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienDuoi14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayTren14OmKhac,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0001-0001-02-02'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0001-0001-02-02' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienTren14OmKhac,

				
				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayConOm,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0002'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0002' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienConOm,


				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayPHSK,

				(case when gt.sXauNoiMa = '9010001-010-011-0001-0003'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa = '9010002-010-011-0001-0003' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienPHSK

				INTO #tempDetalLDHDHachToanSum
			FROM #TBL_TroCapOmDauHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iIDMaDonVi ASC))) AS sTT,
				iIDMaDonVi iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay,
				SUM(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay,
				SUM(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay,
				SUM(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay,
				SUM(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac,
				SUM(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac,
				SUM(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac,
				SUM(fSoTienTren14OmKhac) fSoTienTren14OmKhac,

				SUM(iSoNgayConOm) iSoNgayConOm,
				SUM(fSoTienConOm) fSoTienConOm,
				SUM(iSoNgayPHSK) iSoNgayPHSK,
				SUM(fSoTienPHSK) fSoTienPHSK
				,1 as type
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDHachToan 
				FROM #tempDetalLDHDHachToanSum
				Group by iIDMaDonVi,
				sTenDonVi

		----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempSQDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalSQDuToan) Detail
			where A.type=Detail.type

			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempQNCNDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalQNCNDuToan) Detail
			where A.type=Detail.type

	update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempCNVCQPDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalCNVCQPDuToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempHSQBSDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalHSQBSDuToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempLDHDDuToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalLDHDDuToan) Detail
			where A.type=Detail.type
		----- Update Hach Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempSQHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalSQHachToan) Detail
			where A.type=Detail.type

			update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempQNCNHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalQNCNHachToan) Detail
			where A.type=Detail.type

	update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempCNVCQPHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalCNVCQPHachToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempHSQBSHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalHSQBSHachToan) Detail
			where A.type=Detail.type

		update  A 
			SET
			A.iSoNgayDuoi14BenhDaiNgay=ISNULL(Detail.iSoNgayDuoi14BenhDaiNgay,0),
			A.fSoTienDuoi14BenhDaiNgay=ISNULL(Detail.fSoTienDuoi14BenhDaiNgay,0),
			A.iSoNgayTren14BenhDaiNgay=ISNULL(Detail.iSoNgayTren14BenhDaiNgay,0),
			A.fSoTienTren14BenhDaiNgay=ISNULL(Detail.fSoTienTren14BenhDaiNgay,0),
			A.iSoNgayDuoi14OmKhac=ISNULL(Detail.iSoNgayDuoi14OmKhac,0),
			A.fSoTienDuoi14OmKhac=ISNULL(Detail.fSoTienDuoi14OmKhac,0),
			A.iSoNgayTren14OmKhac=ISNULL(Detail.iSoNgayTren14OmKhac,0),
			A.fSoTienTren14OmKhac=ISNULL(Detail.fSoTienTren14OmKhac,0),
			A.iSoNgayConOm=ISNULL(Detail.iSoNgayConOm,0),
			A.fSoTienConOm=ISNULL(Detail.fSoTienConOm,0),
			A.iSoNgayPHSK=ISNULL(Detail.iSoNgayPHSK,0),
			A.fSoTienPHSK=ISNULL(Detail.fSoTienPHSK,0)
			FROM #tempLDHDHachToan A,
			(SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 2 type
			FROM #tempDetalLDHDHachToan) Detail
			where A.type=Detail.type

---Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempSQDuToan
					UNION ALL
					SELECT * FROM  #tempDetalSQDuToan
					UNION ALL
					SELECT * FROM  #tempQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempLDHDDuToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDDuToan
			) TEMPDuToan

------ Update total khoi du toan
			SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 3 type
			INTO #tempTotalDuToan
			FROM #tempkqDuToan
			WHERE type=2

			update T1
			set T1.iSoNgayDuoi14BenhDaiNgay=T2.iSoNgayDuoi14BenhDaiNgay,
			T1.fSoTienDuoi14BenhDaiNgay=T2.fSoTienDuoi14BenhDaiNgay,
			T1.iSoNgayTren14BenhDaiNgay=T2.iSoNgayTren14BenhDaiNgay,
			T1.fSoTienTren14BenhDaiNgay=T2.fSoTienTren14BenhDaiNgay,
			T1.iSoNgayDuoi14OmKhac=T2.iSoNgayDuoi14OmKhac,
			T1.fSoTienDuoi14OmKhac=T2.fSoTienDuoi14OmKhac,
			T1.iSoNgayTren14OmKhac=T2.iSoNgayTren14OmKhac,
			T1.fSoTienTren14OmKhac=T2.fSoTienTren14OmKhac,
			T1.iSoNgayConOm=T2.iSoNgayConOm,
			T1.fSoTienConOm=T2.fSoTienConOm,
			T1.iSoNgayPHSK=T2.iSoNgayPHSK,
			T1.fSoTienPHSK=T2.fSoTienPHSK
			FROM #tempkqDuToan T1, #tempTotalDuToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'I. Khối dự toán'

---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempSQHachToan
					UNION ALL
					SELECT * FROM  #tempDetalSQHachToan
					UNION ALL
					SELECT * FROM  #tempQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempLDHDHachToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDHachToan
			) TEMPHachToan

------ Update total khoi du toan
			SELECT 
			Sum(iSoNgayDuoi14BenhDaiNgay) iSoNgayDuoi14BenhDaiNgay
			, Sum(fSoTienDuoi14BenhDaiNgay) fSoTienDuoi14BenhDaiNgay
			, Sum(iSoNgayTren14BenhDaiNgay) iSoNgayTren14BenhDaiNgay
			, Sum(fSoTienTren14BenhDaiNgay) fSoTienTren14BenhDaiNgay
			, Sum(iSoNgayDuoi14OmKhac) iSoNgayDuoi14OmKhac
			, Sum(fSoTienDuoi14OmKhac) fSoTienDuoi14OmKhac
			, Sum(iSoNgayTren14OmKhac) iSoNgayTren14OmKhac
			, Sum(fSoTienTren14OmKhac) fSoTienTren14OmKhac
			, Sum(iSoNgayConOm) iSoNgayConOm
			, Sum(fSoTienConOm) fSoTienConOm
			, Sum(iSoNgayPHSK) iSoNgayPHSK
			, Sum(fSoTienPHSK) fSoTienPHSK
			, 3 type
			INTO #tempTotalHachToan
			FROM #tempkqHachToan
			WHERE type=2

			update T1
			set T1.iSoNgayDuoi14BenhDaiNgay=T2.iSoNgayDuoi14BenhDaiNgay,
			T1.fSoTienDuoi14BenhDaiNgay=T2.fSoTienDuoi14BenhDaiNgay,
			T1.iSoNgayTren14BenhDaiNgay=T2.iSoNgayTren14BenhDaiNgay,
			T1.fSoTienTren14BenhDaiNgay=T2.fSoTienTren14BenhDaiNgay,
			T1.iSoNgayDuoi14OmKhac=T2.iSoNgayDuoi14OmKhac,
			T1.fSoTienDuoi14OmKhac=T2.fSoTienDuoi14OmKhac,
			T1.iSoNgayTren14OmKhac=T2.iSoNgayTren14OmKhac,
			T1.fSoTienTren14OmKhac=T2.fSoTienTren14OmKhac,
			T1.iSoNgayConOm=T2.iSoNgayConOm,
			T1.fSoTienConOm=T2.fSoTienConOm,
			T1.iSoNgayPHSK=T2.iSoNgayPHSK,
			T1.fSoTienPHSK=T2.fSoTienPHSK
			FROM #tempkqHachToan T1, #tempTotalHachToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'II. Khối hạch toán'

			SELECT * INTO #tempKQAll FROM
			(
					SELECT * FROM #tempkqDuToan
					UNION ALL
					SELECT * FROM  #tempkqHachToan
					
			) TEMPKQAll

		select sTT
		, iID_MaDonVi
		,sTenDonVi
		,iSoNgayDuoi14BenhDaiNgay
		,fSoTienDuoi14BenhDaiNgay/@Donvitinh fSoTienDuoi14BenhDaiNgay
		,iSoNgayTren14BenhDaiNgay
		,fSoTienTren14BenhDaiNgay/@Donvitinh fSoTienTren14BenhDaiNgay
		,iSoNgayDuoi14OmKhac
		,fSoTienDuoi14OmKhac/@Donvitinh fSoTienDuoi14OmKhac
		,iSoNgayTren14OmKhac
		,fSoTienTren14OmKhac/@Donvitinh fSoTienTren14OmKhac

		,iSoNgayConOm
		,fSoTienConOm/@Donvitinh fSoTienConOm
		,iSoNgayPHSK
		,fSoTienPHSK/@Donvitinh fSoTienPHSK

		,(fSoTienDuoi14BenhDaiNgay+fSoTienTren14BenhDaiNgay+fSoTienDuoi14OmKhac+fSoTienTren14OmKhac+fSoTienConOm+fSoTienPHSK)/@Donvitinh as fTongTien
		,IsHangCha
		,BHangCha
		,type
		,IsParent
		from #tempKQAll

		drop table #TBL_TroCapOmDauDuToan;
		drop table #TBL_TroCapOmDauHachToan;
		drop table #tempDuToan
		drop table #tempSQDuToan
		drop table #tempQNCNDuToan
		drop table #tempCNVCQPDuToan
		drop table #tempHSQBSDuToan
		drop table #tempLDHDDuToan
		DROP TABLE #tempDetalSQDuToan
		DROP TABLE #tempDetalQNCNDuToan
		DROP TABLE #tempDetalCNVCQPDuToan
		DROP TABLE #tempDetalHSQBSDuToan
		DROP TABLE #tempDetalLDHDDuToan
		DROP TABLE #tempTotalDuToan
		DROP TABLE #tempDetalSQDuToanSum
		DROP TABLE #tempDetalQNCNDuToanSum
		DROP TABLE #tempDetalCNVCQPDuToanSum
		DROP TABLE #tempDetalHSQBSDuToanSum
		DROP TABLE #tempDetalLDHDDuToanSum

		drop table #tempHachToan
		drop table #tempSQHachToan
		drop table #tempQNCNHachToan
		drop table #tempCNVCQPHachToan
		drop table #tempHSQBSHachToan
		drop table #tempLDHDHachToan
		DROP TABLE #tempDetalSQHachToan
		DROP TABLE #tempDetalQNCNHachToan
		DROP TABLE #tempDetalCNVCQPHachToan
		DROP TABLE #tempDetalHSQBSHachToan
		DROP TABLE #tempDetalLDHDHachToan
		DROP TABLE #tempTotalHachToan
		DROP TABLE #tempDetalSQHachToanSum
		DROP TABLE #tempDetalQNCNHachToanSum
		DROP TABLE #tempDetalCNVCQPHachToanSum
		DROP TABLE #tempDetalHSQBSHachToanSum
		DROP TABLE #tempDetalLDHDHachToanSum

		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 8/12/2024 3:33:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 8/12/2024 3:33:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]    Script Date: 8/12/2024 3:33:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_ht_pv_tv_tt]
	@DsMaDonVi nvarchar(1000),
	@NamLamViec int,
	@Thang int,
	@DonViTinh int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI]') AND type in (N'U')) drop table TBL_HUUTRI;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_DOC]') AND type in (N'U')) drop table TBL_HUUTRI_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_DOC]') AND type in (N'U')) drop table TBL_PHUCVIEN_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_DOC]') AND type in (N'U')) drop table TBL_THOIVIEC_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_DOC]') AND type in (N'U')) drop table TBL_TUTUAT_DOC;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_SQ]') AND type in (N'U')) drop table TBL_HUUTRI_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_QNCN]') AND type in (N'U')) drop table TBL_HUUTRI_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_HSQBS]') AND type in (N'U')) drop table TBL_HUUTRI_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_VCQP]') AND type in (N'U')) drop table TBL_HUUTRI_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_LDHD]') AND type in (N'U')) drop table TBL_HUUTRI_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_SQ]') AND type in (N'U')) drop table TBL_PHUCVIEN_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_QNCN]') AND type in (N'U')) drop table TBL_PHUCVIEN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_HSQBS]') AND type in (N'U')) drop table TBL_PHUCVIEN_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_VCQP]') AND type in (N'U')) drop table TBL_PHUCVIEN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_LDHD]') AND type in (N'U')) drop table TBL_PHUCVIEN_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_SQ]') AND type in (N'U')) drop table TBL_THOIVIEC_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_QNCN]') AND type in (N'U')) drop table TBL_THOIVIEC_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_HSQBS]') AND type in (N'U')) drop table TBL_THOIVIEC_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_VCQP]') AND type in (N'U')) drop table TBL_THOIVIEC_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_LDHD]') AND type in (N'U')) drop table TBL_THOIVIEC_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_SQ]') AND type in (N'U')) drop table TBL_TUTUAT_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_QNCN]') AND type in (N'U')) drop table TBL_TUTUAT_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_HSQBS]') AND type in (N'U')) drop table TBL_TUTUAT_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_VCQP]') AND type in (N'U')) drop table TBL_TUTUAT_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_LDHD]') AND type in (N'U')) drop table TBL_TUTUAT_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_TRUYLINH]') AND type in (N'U')) drop table TBL_TUTUAT_TRUYLINH;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_RESULT]') AND type in (N'U')) drop table TBL_HUUTRI_RESULT;

	--Lay thong tin luong theo TC_HUUTRI, TC_PHUCVIEN, TC_THOIVIEC, TC_TUTUAT
	select * into TBL_HUUTRI from
	(select donvi.Ma_DonVi ,donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in ('HUUTRI_TROCAP1LAN', 'HUUTRI_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN', 'PHUCVIEN_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN', 'THOIVIEC_TROCAPKHUVUC', 'TUTUAT_TROCAP1LAN', 'TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG','TUTUAT_TROCAP1LAN_TRUYLINH','TUTUAT_TROCAPKHUVUC_TRUYLINH','TROCAPMAITANG_TRUYLINH')) HUUTRI


	-- Data tro cap Huu tri
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		HUUTRI_TROCAP1LAN.nGiaTri fHUUTRI_TROCAP1LAN,
		HUUTRI_TROCAPKHUVUC.nGiaTri fHUUTRI_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_HUUTRI_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('HUUTRI_TROCAPKHUVUC', 'HUUTRI_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'HUUTRI_TROCAPKHUVUC') HUUTRI_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = HUUTRI_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'HUUTRI_TROCAP1LAN') HUUTRI_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = HUUTRI_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = HUUTRI_TROCAP1LAN.sMaDonVi

	-- Data tro cap Phuc vien
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		PHUCVIEN_TROCAP1LAN.nGiaTri fPHUCVIEN_TROCAP1LAN,
		PHUCVIEN_TROCAPKHUVUC.nGiaTri fPHUCVIEN_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_PHUCVIEN_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('PHUCVIEN_TROCAPKHUVUC', 'PHUCVIEN_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'PHUCVIEN_TROCAPKHUVUC') PHUCVIEN_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = PHUCVIEN_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'PHUCVIEN_TROCAP1LAN') PHUCVIEN_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = PHUCVIEN_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = PHUCVIEN_TROCAP1LAN.sMaDonVi

	-- Data tro cap Thoi Viec
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		THOIVIEC_TROCAP1LAN.nGiaTri fTHOIVIEC_TROCAP1LAN,
		THOIVIEC_TROCAPKHUVUC.nGiaTri fTHOIVIEC_TROCAPKHUVUC,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_THOIVIEC_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('THOIVIEC_TROCAPKHUVUC', 'THOIVIEC_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'THOIVIEC_TROCAPKHUVUC') THOIVIEC_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = THOIVIEC_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'THOIVIEC_TROCAP1LAN') THOIVIEC_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = THOIVIEC_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = THOIVIEC_TROCAP1LAN.sMaDonVi

	-- Data tro cap Tu tuat
	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		TUTUAT_TROCAP1LAN.nGiaTri fTUTUAT_TROCAP1LAN,
		TUTUAT_TROCAPKHUVUC.nGiaTri fTUTUAT_TROCAPKHUVUC,
		TROCAPMAITANG.nGiaTri fTROCAPMAITANG,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_TUTUAT_DOC
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('TUTUAT_TROCAPKHUVUC', 'TROCAPMAITANG', 'TUTUAT_TROCAP1LAN')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'TUTUAT_TROCAPKHUVUC') TUTUAT_TROCAPKHUVUC
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAPKHUVUC.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAPKHUVUC.sMaDonVi
		left join
		(select HUUTRI1.Id, HUUTRI1.sMaDonVi, HUUTRI1.nGiaTri, HUUTRI1.sMaCB, HUUTRI1.sMaCBo, HUUTRI1.sMaCheDo, HUUTRI1.sTenCbo
		from TBL_HUUTRI HUUTRI1
		where HUUTRI1.sMaCheDo = 'TROCAPMAITANG') TROCAPMAITANG
		on TBL_HUUTRI.sMaCBo = TROCAPMAITANG.sMaCBo and TBL_HUUTRI.sMaDonVi = TROCAPMAITANG.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'TUTUAT_TROCAP1LAN') TUTUAT_TROCAP1LAN
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAP1LAN.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAP1LAN.sMaDonVi


   -- lAY TRUY LINH TU TUAT 1 LAN , KHU VUC
   	select distinct
		TBL_HUUTRI.sMaCB,
		TBL_HUUTRI.sMaCBo,
		--TBL_HUUTRI.sMaCheDo,
		TBL_HUUTRI.sTenCbo,
		TBL_HUUTRI.Ma_DonVi,
		TBL_HUUTRI.Ten_DonVi,
		TUTUAT_TROCAP1LAN_TRUYLINH.nGiaTri fTUTUAT_TROCAP1LAN_TRUYLINH,
		TUTUAT_TROCAPKHUVUC_TRUYLINH.nGiaTri fTUTUAT_TROCAPKHUVUC_TRUYLINH,
		TROCAPMAITANG_TRUYLINH.nGiaTri fTROCAPMAITANG_TRUYLINH,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh
		into TBL_TUTUAT_TRUYLINH
	from TBL_HUUTRI TBL_HUUTRI
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in ('TUTUAT_TROCAPKHUVUC_TRUYLINH', 'TROCAPMAITANG_TRUYLINH', 'TUTUAT_TROCAP1LAN_TRUYLINH')
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_HUUTRI.sMaCBo = chedocha.sMaCanBo
		left join
		(select HUUTRI.Id, HUUTRI.sMaDonVi, HUUTRI.nGiaTri, HUUTRI.sMaCB, HUUTRI.sMaCBo, HUUTRI.sMaCheDo, HUUTRI.sTenCbo
		from TBL_HUUTRI HUUTRI
		where HUUTRI.sMaCheDo = 'TUTUAT_TROCAPKHUVUC_TRUYLINH') TUTUAT_TROCAPKHUVUC_TRUYLINH
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAPKHUVUC_TRUYLINH.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAPKHUVUC_TRUYLINH.sMaDonVi
		left join
		(select HUUTRI1.Id, HUUTRI1.sMaDonVi, HUUTRI1.nGiaTri, HUUTRI1.sMaCB, HUUTRI1.sMaCBo, HUUTRI1.sMaCheDo, HUUTRI1.sTenCbo
		from TBL_HUUTRI HUUTRI1
		where HUUTRI1.sMaCheDo = 'TROCAPMAITANG_TRUYLINH') TROCAPMAITANG_TRUYLINH
		on TBL_HUUTRI.sMaCBo = TROCAPMAITANG_TRUYLINH.sMaCBo and TBL_HUUTRI.sMaDonVi = TROCAPMAITANG_TRUYLINH.sMaDonVi
		left join
		(select HUUTRI2.Id, HUUTRI2.sMaDonVi, HUUTRI2.nGiaTri, HUUTRI2.sMaCB, HUUTRI2.sMaCBo, HUUTRI2.sMaCheDo, HUUTRI2.sTenCbo
		from TBL_HUUTRI HUUTRI2
		where HUUTRI2.sMaCheDo = 'TUTUAT_TROCAP1LAN_TRUYLINH') TUTUAT_TROCAP1LAN_TRUYLINH
		on TBL_HUUTRI.sMaCBo = TUTUAT_TROCAP1LAN_TRUYLINH.sMaCBo and TBL_HUUTRI.sMaDonVi = TUTUAT_TROCAP1LAN_TRUYLINH.sMaDonVi


	--Lay tro cap Huu tri Si quan
	select * into TBL_HUUTRI_SQ from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi,null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB like '1%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_SQ) > 2
		update TBL_HUUTRI_SQ set bHasData = 1

	--Lay tro cap Huu tri QNCN
	select * into TBL_HUUTRI_QNCN from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB like '2%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_QNCN) > 2
		update TBL_HUUTRI_QNCN set bHasData = 1

	--Lay tro cap Huu tri HSQ_BS
	select * into TBL_HUUTRI_HSQBS from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB like '0%' and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_HSQBS) > 2
		update TBL_HUUTRI_HSQBS set bHasData = 1

	--Lay tro cap Huu tri VCQP
	select * into TBL_HUUTRI_VCQP from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_VCQP) > 2
		update TBL_HUUTRI_VCQP set bHasData = 1

	--Lay tro cap Huu tri HDLD
	select * into TBL_HUUTRI_LDHD from
	(select 1 bHangCha, 'I' LoaiTC, 1 RowNum, 'I' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Hưu trí' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'I' LoaiTC,  5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'I' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fHUUTRI_TROCAP1LAN fTROCAP1LAN, fHUUTRI_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_HUUTRI_DOC
	where sMaCB in ('43','423','425') and (isnull(fHUUTRI_TROCAP1LAN, 0) <> 0 or isnull(fHUUTRI_TROCAPKHUVUC, 0) <> 0)) ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_HUUTRI_LDHD) > 2
		update TBL_HUUTRI_LDHD set bHasData = 1
	-----------------------------------
	--Lay tro cap Phuc vien Si quan
	select * into TBL_PHUCVIEN_SQ from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB like '1%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_SQ) > 2
		update TBL_PHUCVIEN_SQ set bHasData = 1

	--Lay tro cap Phuc vien QNCN
	select * into TBL_PHUCVIEN_QNCN from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB like '2%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_QNCN) > 2
		update TBL_PHUCVIEN_QNCN set bHasData = 1

	--Lay tro cap Phuc vien HSQ_BS
	select * into TBL_PHUCVIEN_HSQBS from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB like '0%' and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_HSQBS) > 2
		update TBL_PHUCVIEN_HSQBS set bHasData = 1

	--Lay tro cap Phuc vien VCQP
	select * into TBL_PHUCVIEN_VCQP from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_VCQP) > 2
		update TBL_PHUCVIEN_VCQP set bHasData = 1

	--Lay tro cap Phuc vien HDLD
	select * into TBL_PHUCVIEN_LDHD from
	(select 1 bHangCha, 'II' LoaiTC, 1 RowNum, 'II' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Phục viên' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'II' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'II' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fPHUCVIEN_TROCAP1LAN fTROCAP1LAN, fPHUCVIEN_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_PHUCVIEN_DOC
	where sMaCB in ('43','423','425') and (isnull(fPHUCVIEN_TROCAP1LAN, 0) <> 0 or isnull(fPHUCVIEN_TROCAPKHUVUC, 0) <> 0)) pvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_PHUCVIEN_LDHD) > 2
		update TBL_PHUCVIEN_LDHD set bHasData = 1
	--------------------------------------------
	--Lay tro cap Thoi viec Si quan
	select * into TBL_THOIVIEC_SQ from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB like '1%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_SQ) > 2
		update TBL_THOIVIEC_SQ set bHasData = 1

	--Lay tro cap Thoi viec QNCN
	select * into TBL_THOIVIEC_QNCN from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB like '2%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_QNCN) > 2
		update TBL_THOIVIEC_QNCN set bHasData = 1

	--Lay tro cap Thoi viec HSQ_BS
	select * into TBL_THOIVIEC_HSQBS from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB like '0%' and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_HSQBS) > 2
		update TBL_THOIVIEC_HSQBS set bHasData = 1

	--Lay tro cap Thoi viec VCQP
	select * into TBL_THOIVIEC_VCQP from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvvcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_VCQP) > 2
		update TBL_THOIVIEC_VCQP set bHasData = 1

	--Lay tro cap Thoi viec HDLD
	select * into TBL_THOIVIEC_LDHD from
	(select 1 bHangCha, 'III' LoaiTC, 1 RowNum, 'III' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Thôi việc' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'III' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'III' LoaiTC, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi, Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, fTHOIVIEC_TROCAP1LAN fTROCAP1LAN, fTHOIVIEC_TROCAPKHUVUC fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	from TBL_THOIVIEC_DOC
	where sMaCB in ('43','423','425') and (isnull(fTHOIVIEC_TROCAP1LAN, 0) <> 0 or isnull(fTHOIVIEC_TROCAPKHUVUC, 0) <> 0)) tvldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_THOIVIEC_LDHD) > 2
		update TBL_THOIVIEC_LDHD set bHasData = 1
	--------------------------------------------
	--Lay tro cap Tu tuat Si quan
	select * into TBL_TUTUAT_SQ from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, null STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'SQ' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 1 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'SQ' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH
	from TBL_TUTUAT_DOC  TBL
	lefT join TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB like '1%' and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0)
	where TBL.sMaCB like '1%' ) tvsq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_SQ) > 2
		update TBL_TUTUAT_SQ set bHasData = 1

	--Lay tro cap Tu tuat QNCN
	select * into TBL_TUTUAT_QNCN from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 2 RowNum, null STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 2 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'QNCN' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH
	

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB like '2%' and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB like '2%' ) tvqncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_QNCN) > 2
		update TBL_TUTUAT_QNCN set bHasData = 1

	--Lay tro cap Tu tuat HSQ_BS
	select * into TBL_TUTUAT_HSQBS from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 3 RowNum, null STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 3 RowNum, 
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'HSQ_BS' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB like '0%' and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB like '0%') tvhsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_HSQBS) > 2
		update TBL_TUTUAT_HSQBS set bHasData = 1

	--Lay tro cap Tu tuat VCQP
	select * into TBL_TUTUAT_VCQP from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 4 RowNum, null STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'VCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 4 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'VCQP' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB  in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB  in ('3.1', '3.2', '3.3', '413', '415') ) tvvcqp
	
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_VCQP) > 2
		update TBL_TUTUAT_VCQP set bHasData = 1

	--Lay tro cap Tu tuat HDLD
	select * into TBL_TUTUAT_LDHD from
	(select 1 bHangCha, 'IV' LoaiTC, 1 RowNum, 'IV' STT, null DoiTuong, null LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'TC Tử tuất' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null fTROCAP1LAN, null fTROCAPKHUVUC, null fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 1 bHangCha, 'IV' LoaiTC, 5 RowNum, null STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, 'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null  fTROCAP1LAN, null  fTROCAPKHUVUC, null  fTROCAPMAITANG, 0 bHasData, null fTROCAP1LANTRUYLINH, null fTROCAPKHUVUCTRUYLINH, null fTROCAPMAITANGTRUYLINH
	union all
	select 0 bHangCha, 'IV' LoaiTC, 5 RowNum,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN CAST(ROW_NUMBER() OVER (ORDER BY TBL.sMaCB) AS VARCHAR(6))
		ELSE CAST(ROW_NUMBER() OVER (ORDER BY TL.sMaCB) AS VARCHAR(6))
	END AS STT,
	 '', 'LDHD' LoaiDoiTuong,
	CASE
		WHEN TBL.Ma_DonVi IS NOT NULL THEN TBL.Ma_DonVi
		ELSE TL.Ma_DonVi
	END AS Ma_DonVi,
	CASE
		WHEN TBL.Ten_DonVi IS NOT NULL THEN TBL.Ten_DonVi
		ELSE TL.Ten_DonVi
	END AS TenDonVi,
	CASE
		WHEN TBL.sMaCB IS NOT NULL THEN TBL.sMaCB
		ELSE TL.sMaCB
	END AS sMaCB,
	CASE
		WHEN TBL.sMaCBo IS NOT NULL THEN TBL.sMaCBo
		ELSE TL.sMaCBo
	END AS sMaCBo,	
	CASE
		WHEN TBL.sTenCbo IS NOT NULL THEN TBL.sTenCbo
		ELSE TL.sTenCbo
	END AS sTenCbo,
	CASE
		WHEN TBL.sSoQuyetDinh IS NOT NULL THEN TBL.sSoQuyetDinh
		ELSE TL.sSoQuyetDinh
	END AS sSoQuyetDinh,
	CASE
		WHEN TBL.dNgayQuyetDinh IS NOT NULL THEN TBL.dNgayQuyetDinh
		ELSE TL.dNgayQuyetDinh
	END AS dNgayQuyetDinh,
	 fTUTUAT_TROCAP1LAN fTROCAP1LAN,
	 fTUTUAT_TROCAPKHUVUC fTROCAPKHUVUC,
	 fTROCAPMAITANG, 0 bHasData,
	 fTUTUAT_TROCAP1LAN_TRUYLINH TROCAP1LANTRUYLINH,
	 fTUTUAT_TROCAPKHUVUC_TRUYLINH fTROCAPKHUVUCTRUYLINH,
	 fTROCAPMAITANG_TRUYLINH fTROCAPMAITANGTRUYLINH

	from TBL_TUTUAT_DOC TBL
	LEFT JOIN TBL_TUTUAT_TRUYLINH TL ON TBL.sMaCBo = TL.sMaCBo and TL.sMaCB in ('43','423','425')  and (isnull(fTUTUAT_TROCAP1LAN_TRUYLINH, 0) <> 0 or isnull(fTUTUAT_TROCAPKHUVUC_TRUYLINH, 0) <> 0 OR  isnull(fTROCAPMAITANG_TRUYLINH, 0) <> 0)
	where tbl.sMaCB in ('43','423','425') ) tvldhd
	

	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TUTUAT_LDHD) > 2
		update TBL_TUTUAT_LDHD set bHasData = 1
	----------------------------------------------
	--Ket qua
	select result.* into TBL_HUUTRI_RESULT from
	(select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_HUUTRI_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_HUUTRI_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, 
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_HUUTRI_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_HUUTRI_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_HUUTRI_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, 
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_PHUCVIEN_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_PHUCVIEN_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_THOIVIEC_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_THOIVIEC_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien
	from TBL_THOIVIEC_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_THOIVIEC_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
		from TBL_THOIVIEC_LDHD
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
		from TBL_TUTUAT_SQ
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
		from TBL_TUTUAT_QNCN
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, 
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_TUTUAT_HSQBS
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_TUTUAT_VCQP
	union all
	select bHasData, bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh,
		fTROCAP1LAN, fTROCAPKHUVUC, fTROCAPMAITANG, (isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0)) fTongSoTienTT,
		fTROCAP1LANTRUYLINH, fTROCAPKHUVUCTRUYLINH, fTROCAPMAITANGTRUYLINH, (isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTienTL,
		(isnull(fTROCAP1LAN, 0) + isnull(fTROCAPKHUVUC, 0) + isnull(fTROCAPMAITANG, 0) +isnull(fTROCAP1LANTRUYLINH, 0) + isnull(fTROCAPKHUVUCTRUYLINH, 0) + isnull(fTROCAPMAITANGTRUYLINH, 0)) fTongSoTien	
	from TBL_TUTUAT_LDHD) result

	select distinct
		LoaiTC SLoaiTC,
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb,
		sMaCBo MaCbo,
		sTenCbo TenCbo,
		sSoQuyetDinh SSoQuyetDinh,
		dNgayQuyetDinh DNgayQuyetDinh,
		Ma_DonVi MaDonVi,
		TenDonVi,
		fTROCAP1LAN/@DonViTinh FTroCap1Lan,
		fTROCAPKHUVUC/@DonViTinh FTroCapKhuVuc,
		fTROCAPMAITANG/@DonViTinh FTroCapMaiTang,
		fTongSoTienTT/@DonViTinh FTongSoTienThangNay,
		fTROCAP1LANTRUYLINH/@DonViTinh FTroCap1LanTruyLinh,
		fTROCAPKHUVUCTRUYLINH/@DonViTinh FTroCapKhuVucTruyLinh,
		fTROCAPMAITANGTRUYLINH/@DonViTinh FTroCapMaiTangTruyLinh,
		fTongSoTienTL/@DonViTinh FTongSoTienTruyLinh,
		fTongSoTien/@DonViTinh FTongSoTien,
		bHangCha IsHangCha,
		bHasData IsHasData
	from TBL_HUUTRI_RESULT
	order by LoaiTC, RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI]') AND type in (N'U')) drop table TBL_HUUTRI;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_DOC]') AND type in (N'U')) drop table TBL_HUUTRI_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_DOC]') AND type in (N'U')) drop table TBL_PHUCVIEN_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_DOC]') AND type in (N'U')) drop table TBL_THOIVIEC_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_DOC]') AND type in (N'U')) drop table TBL_TUTUAT_DOC;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_SQ]') AND type in (N'U')) drop table TBL_HUUTRI_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_QNCN]') AND type in (N'U')) drop table TBL_HUUTRI_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_HSQBS]') AND type in (N'U')) drop table TBL_HUUTRI_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_VCQP]') AND type in (N'U')) drop table TBL_HUUTRI_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_LDHD]') AND type in (N'U')) drop table TBL_HUUTRI_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_SQ]') AND type in (N'U')) drop table TBL_PHUCVIEN_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_QNCN]') AND type in (N'U')) drop table TBL_PHUCVIEN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_HSQBS]') AND type in (N'U')) drop table TBL_PHUCVIEN_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_VCQP]') AND type in (N'U')) drop table TBL_PHUCVIEN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_PHUCVIEN_LDHD]') AND type in (N'U')) drop table TBL_PHUCVIEN_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_SQ]') AND type in (N'U')) drop table TBL_THOIVIEC_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_QNCN]') AND type in (N'U')) drop table TBL_THOIVIEC_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_HSQBS]') AND type in (N'U')) drop table TBL_THOIVIEC_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_VCQP]') AND type in (N'U')) drop table TBL_THOIVIEC_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THOIVIEC_LDHD]') AND type in (N'U')) drop table TBL_THOIVIEC_LDHD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_SQ]') AND type in (N'U')) drop table TBL_TUTUAT_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_QNCN]') AND type in (N'U')) drop table TBL_TUTUAT_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_HSQBS]') AND type in (N'U')) drop table TBL_TUTUAT_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_VCQP]') AND type in (N'U')) drop table TBL_TUTUAT_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_LDHD]') AND type in (N'U')) drop table TBL_TUTUAT_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TUTUAT_TRUYLINH]') AND type in (N'U')) drop table TBL_TUTUAT_TRUYLINH;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_HUUTRI_RESULT]') AND type in (N'U')) drop table TBL_HUUTRI_RESULT;

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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld]    Script Date: 8/12/2024 3:33:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_luong_tro_cap_tnld] 
 	@DsMaDonVi nvarchar(100),
	@NamLamViec int ,
	@Thang int ,
	@DonViTinh int 
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD]') AND type in (N'U'))
	drop table TBL_TCTNLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_DOC]') AND type in (N'U'))
	drop table TBL_TCTNLD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_SQ]') AND type in (N'U'))
	drop table TBL_TCTNLD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_QNCN]') AND type in (N'U'))
	drop table TBL_TCTNLD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTNLD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_VCQP]') AND type in (N'U'))
	drop table TBL_TCTNLD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_LDHD]') AND type in (N'U'))
	drop table TBL_TCTNLD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_RESULT]') AND type in (N'U'))
	drop table TBL_TCTNLD_RESULT;

	DECLARE @MaCheDo nvarchar(1000) = 'CHIGIAMDINH,HOTRO_CDNN,HOTRO_PHONGNGUA,TAINANLD_DUONGSUCPHSK,TAINANLD_TROCAP1LAN,TROCAPCHETDOTNLD,TROCAPPHCN,TROCAPPHUCVU,TROCAPTHEOPHIEUTRUYTRA,TROCAPHANGTHANG,CHIGIAMDINH_TRUYLINH,HOTRO_CDNN_TRUYLINH,HOTRO_PHONGNGUA_TRUYLINH,TAINANLD_DUONGSUCPHSK_TRUYLINH,TAINANLD_TROCAP1LAN_TRUYLINH,TROCAPCHETDOTNLD_TRUYLINH,TROCAPPHCN_TRUYLINH,TROCAPPHUCVU_TRUYLINH,TROCAPHANGTHANG_TRUYLINH,TROCAPTHEOPHIEUTRUYTRA_TRUYLINH'
	--Lay thong tin luong theo tro cap tai nan lao dong
	select * into TBL_TCTNLD from
	(select donvi.Ma_DonVi, donvi.Ten_DonVi, luong.*
	from TL_BangLuong_ThangBHXH luong
	join TL_DM_DonVi donvi on luong.sMaDonVi = donvi.Ma_DonVi
	where 
	luong.sMaDonVi in (SELECT * FROM f_split(@DsMaDonVi))
	and luong.iNam = @NamLamViec
	and luong.iThang = @Thang
	and luong.sMaCheDo in (select * from splitstring(@MaCheDo))) tctnld
	select distinct 
		TBL_TCTNLD.sMaCB,
		TBL_TCTNLD.sMaCBo,
		--TBL_TCTNLD.sMaCheDo,
		TBL_TCTNLD.sTenCbo,
		TBL_TCTNLD.Ma_DonVi,
		TBL_TCTNLD.Ten_DonVi,
		CHIGIAMDINH.nGiaTri fChiGiamDinh,
		TROCAP1LAN.nGiaTri fTroCap1Lan,
		TROCAPTHEOPHIEUTRUYTRA.nGiaTri fTroCapTheoPhieuTruyTra,
		TROCAPHANGTHANG.nGiaTri fTroCapHangThang,
		TROCAPPHCN.nGiaTri fTroCapPHCN,
		HOTROCDNN.nGiaTri fHoTroCdnn,
		HOTROPHONGNGUA.nGiaTri fHoTroPhongNgua,
		TROCAPCHETDOTNLD.nGiaTri fTroCapChetDoTNLD,
		TAINANLD_DUONGSUCPHSK.SoNgayDuongSucTNLD,
		TAINANLD_DUONGSUCPHSK.nGiaTri fDuongSucTNLD,
		chedocha.sSoQuyetDinh,
		chedocha.dNgayQuyetDinh,
		CHIGIAMDINH_TL.nGiaTri fChiGiamDinhTruyLinh,
		TROCAP1LAN_TL.nGiaTri fTroCap1LanTruyLinh,
		HOTRO_CDNN_TRUYLINH_TL.nGiaTri fHoTroCdnnTruyLinh,
		HOTRO_PHONGNGUA_TRUYLINH_TL.nGiaTri fHoTroPhongNguaTruyLinh,
		TROCAPHANGTHANG_TL.nGiaTri fTroCapHangThangTruyLinh,
		TROCAPPHCN_TL.nGiaTri fTroCapPHCNTruyLinh,
		TROCAPCHETDOTNLD_TL.nGiaTri fTroCapChetDoTNLDTruyLinh,
		TAINANLD_DUONGSUCPHSK_TL.SoNgayDuongSucTNLD SoNgayDuongSucTNLDTruyLinh,
		TAINANLD_DUONGSUCPHSK_TL.nGiaTri fDuongSucTNLDTruyLinh
		into TBL_TCTNLD_DOC
	from TBL_TCTNLD TBL_TCTNLD
		left join (select top 1 * from TL_CanBo_CheDoBHXH 
		where sMaCheDo in (select * from splitstring(@MaCheDo))
		and ((sSoQuyetDinh is not null and sSoQuyetDinh <> '') 
		or (dNgayQuyetDinh is not null and dNgayQuyetDinh <> ''))) chedocha
		on TBL_TCTNLD.sMaCBo = chedocha.sMaCanBo
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TAINANLD_TROCAP1LAN') TROCAP1LAN
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'TROCAPTHEOPHIEUTRUYTRA') TROCAPTHEOPHIEUTRUYTRA
		on TBL_TCTNLD.sMaCBo = TROCAPTHEOPHIEUTRUYTRA.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPTHEOPHIEUTRUYTRA.sMaDonVi
		left join
		(select tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG') TROCAPHANGTHANG
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG.sMaDonVi
		left join
		(select tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN', 'TROCAPPHUCVU')
		group by tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo) TROCAPPHCN
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN.sMaDonVi
		left join
		(select tnld_3_1.sMaDonVi, sum(tnld_3_1.nGiaTri) nGiaTri, tnld_3_1.sMaCB, tnld_3_1.sMaCBo, tnld_3_1.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayHOTROCDNN
		from TBL_TCTNLD tnld_3_1 left join TL_CanBo_CheDoBHXH chedo on tnld_3_1.sMaCBo = chedo.sMaCanBo and tnld_3_1.sTenCbo = chedo.sMaCheDo
		where tnld_3_1.sMaCheDo in ('HOTRO_CDNN')
		group by tnld_3_1.sMaDonVi, tnld_3_1.sMaCB, tnld_3_1.sMaCBo, tnld_3_1.sTenCbo) HOTROCDNN
		on TBL_TCTNLD.sMaCBo = HOTROCDNN.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTROCDNN.sMaDonVi
		left join
		(select tnld_3_2.sMaDonVi, sum(tnld_3_2.nGiaTri) nGiaTri, tnld_3_2.sMaCB, tnld_3_2.sMaCBo, tnld_3_2.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayHOTROPHONGNGUA
		from TBL_TCTNLD tnld_3_2 left join TL_CanBo_CheDoBHXH chedo on tnld_3_2.sMaCBo = chedo.sMaCanBo and tnld_3_2.sTenCbo = chedo.sMaCheDo
		where tnld_3_2.sMaCheDo in ('HOTRO_PHONGNGUA')
		group by tnld_3_2.sMaDonVi, tnld_3_2.sMaCB, tnld_3_2.sMaCBo, tnld_3_2.sTenCbo) HOTROPHONGNGUA
		on TBL_TCTNLD.sMaCBo = HOTROPHONGNGUA.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTROPHONGNGUA.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD') TROCAPCHETDOTNLD
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TAINANLD_DUONGSUCPHSK') TAINANLD_DUONGSUCPHSK
		on TBL_TCTNLD.sMaCBo = TAINANLD_DUONGSUCPHSK.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLD_DUONGSUCPHSK.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH') CHIGIAMDINH
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH.sMaDonVi

		-- TRUYLINH
		left join
		(select tnld.sMaDonVi, tnld.nGiaTri, tnld.sMaCB, tnld.sMaCBo, tnld.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCap1Lan
		from TBL_TCTNLD tnld left join TL_CanBo_CheDoBHXH chedo on tnld.sMaCBo = chedo.sMaCanBo and tnld.sMaCheDo = chedo.sMaCheDo
		where tnld.sMaCheDo = 'TAINANLD_TROCAP1LAN_TRUYLINH') TROCAP1LAN_TL
		on TBL_TCTNLD.sMaCBo = TROCAP1LAN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAP1LAN_TL.sMaDonVi
		left join
		(select tnld_1.sMaDonVi, tnld_1.nGiaTri, tnld_1.sMaCB, tnld_1.sMaCBo, tnld_1.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapTheoPhieuTruyTra
		from TBL_TCTNLD tnld_1 left join TL_CanBo_CheDoBHXH chedo on tnld_1.sMaCBo = chedo.sMaCanBo and tnld_1.sMaCheDo = chedo.sMaCheDo
		where tnld_1.sMaCheDo = 'HOTRO_CDNN_TRUYLINH') HOTRO_CDNN_TRUYLINH_TL
		on TBL_TCTNLD.sMaCBo = HOTRO_CDNN_TRUYLINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTRO_CDNN_TRUYLINH_TL.sMaDonVi
		left join
		(select tnld_pn.sMaDonVi, tnld_pn.nGiaTri, tnld_pn.sMaCB, tnld_pn.sMaCBo, tnld_pn.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayHoTroPhongNgua
		from TBL_TCTNLD tnld_pn left join TL_CanBo_CheDoBHXH chedo on tnld_pn.sMaCBo = chedo.sMaCanBo and tnld_pn.sMaCheDo = chedo.sMaCheDo
		where tnld_pn.sMaCheDo = 'HOTRO_PHONGNGUA_TRUYLINH') HOTRO_PHONGNGUA_TRUYLINH_TL
		on TBL_TCTNLD.sMaCBo = HOTRO_PHONGNGUA_TRUYLINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = HOTRO_PHONGNGUA_TRUYLINH_TL.sMaDonVi
		left join
		(select tnld_2.sMaDonVi, tnld_2.nGiaTri, tnld_2.sMaCB, tnld_2.sMaCBo, tnld_2.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapHangThang
		from TBL_TCTNLD tnld_2 left join TL_CanBo_CheDoBHXH chedo on tnld_2.sMaCBo = chedo.sMaCanBo and tnld_2.sMaCheDo = chedo.sMaCheDo
		where tnld_2.sMaCheDo = 'TROCAPHANGTHANG_TRUYLINH') TROCAPHANGTHANG_TL
		on TBL_TCTNLD.sMaCBo = TROCAPHANGTHANG_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPHANGTHANG_TL.sMaDonVi
		left join
		(select tnld_3.sMaDonVi, sum(tnld_3.nGiaTri) nGiaTri, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo, sum(chedo.fSoNgayHuongBHXH) SoNgayTroCapPHCN
		from TBL_TCTNLD tnld_3 left join TL_CanBo_CheDoBHXH chedo on tnld_3.sMaCBo = chedo.sMaCanBo and tnld_3.sTenCbo = chedo.sMaCheDo
		where tnld_3.sMaCheDo in ('TROCAPPHCN_TRUYLINH', 'TROCAPPHUCVU_TRUYLINH')
		group by tnld_3.sMaDonVi, tnld_3.sMaCB, tnld_3.sMaCBo, tnld_3.sTenCbo) TROCAPPHCN_TL
		on TBL_TCTNLD.sMaCBo = TROCAPPHCN_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPPHCN_TL.sMaDonVi
		left join
		(select tnld_4.sMaDonVi, tnld_4.nGiaTri, tnld_4.sMaCB, tnld_4.sMaCBo, tnld_4.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayTroCapChetDoTNLD
		from TBL_TCTNLD tnld_4 left join TL_CanBo_CheDoBHXH chedo on tnld_4.sMaCBo = chedo.sMaCanBo and tnld_4.sMaCheDo = chedo.sMaCheDo
		where tnld_4.sMaCheDo = 'TROCAPCHETDOTNLD_TRUYLINH') TROCAPCHETDOTNLD_TL
		on TBL_TCTNLD.sMaCBo = TROCAPCHETDOTNLD_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TROCAPCHETDOTNLD_TL.sMaDonVi
		left join
		(select tnld_5.sMaDonVi, tnld_5.nGiaTri, tnld_5.sMaCB, tnld_5.sMaCBo, tnld_5.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayDuongSucTNLD
		from TBL_TCTNLD tnld_5 left join TL_CanBo_CheDoBHXH chedo on tnld_5.sMaCBo = chedo.sMaCanBo and tnld_5.sMaCheDo = chedo.sMaCheDo
		where tnld_5.sMaCheDo = 'TAINANLD_DUONGSUCPHSK_TRUYLINH ') TAINANLD_DUONGSUCPHSK_TL
		on TBL_TCTNLD.sMaCBo = TAINANLD_DUONGSUCPHSK_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = TAINANLD_DUONGSUCPHSK_TL.sMaDonVi
		left join
		(select tnld_6.sMaDonVi, tnld_6.nGiaTri, tnld_6.sMaCB, tnld_6.sMaCBo, tnld_6.sTenCbo, chedo.fSoNgayHuongBHXH SoNgayCHIGIAMDINH
		from TBL_TCTNLD tnld_6 left join TL_CanBo_CheDoBHXH chedo on tnld_6.sMaCBo = chedo.sMaCanBo and tnld_6.sMaCheDo = chedo.sMaCheDo
		where tnld_6.sMaCheDo = 'CHIGIAMDINH_TRUYLINH') CHIGIAMDINH_TL
		on TBL_TCTNLD.sMaCBo = CHIGIAMDINH_TL.sMaCBo and TBL_TCTNLD.sMaDonVi = CHIGIAMDINH_TL.sMaDonVi

	--Lấy lương Sĩ quan
	select * into TBL_TCTNLD_SQ from
	(select
		1 bHangCha, 1 RowNum, 'I' STT, 'Sĩ quan' DoiTuong, 'Sĩ quan' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'Sĩ quan' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'Sĩ quan' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '1%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		) sq
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_SQ) > 1
		update TBL_TCTNLD_SQ set bHasData = 1

	--Lấy lương QNCN
	select * into TBL_TCTNLD_QNCN from
	(select
		1 bHangCha, 2 RowNum, 'II' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'QNCN' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '2%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) qncn
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_QNCN) > 1
		update TBL_TCTNLD_QNCN set bHasData = 1

	--Lấy lương HSQ_BS
	select * into TBL_TCTNLD_HSQBS from
	(select
		1 bHangCha, 3 RowNum, 'III' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'HSQ_BS' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB like '0%' and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		)  hsqbs
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_HSQBS) > 1
		update TBL_TCTNLD_HSQBS set bHasData = 1

	--Lấy lương VCQP
	select * into TBL_TCTNLD_VCQP from
	(select
		1 bHangCha, 4 RowNum, 'IV' STT, 'CNVCQP' DoiTuong, 'CNVCQP' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'CNVCQP' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'CNVCQP' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('3.1', '3.2', '3.3', '413', '415') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0)) vcqp
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_VCQP) > 1
		update TBL_TCTNLD_VCQP set bHasData = 1

	--Lấy lương Lao Dộng hợp Dông
	select * into TBL_TCTNLD_LDHD from
	(select
		1 bHangCha, 5 RowNum, 'V' STT, 'LDHD' DoiTuong, 'LDHD' LoaiDoiTuong, null Ma_DonVi, null TenDonVi, null sMaCB, null sMaCBo, N'LDHD' sTenCbo, null sSoQuyetDinh, null dNgayQuyetDinh, null SoNgayDuongSucTNLD, null fChiGiamDinh, null fTroCap1Lan, null fTroCapTheoPhieuTruyTra, null fTroCapHangThang, null fTroCapPHCN, null fHoTroCdnn, null fHoTroPhongNgua, null fTroCapChetDoTNLD, null fDuongSucTNLD, 0 bHasData,
		null SoNgayDuongSucTNLDTruyLinh, null fChiGiamDinhTruyLinh, null fTroCap1LanTruyLinh, null fHoTroCdnnTruyLinh, null fHoTroPhongNguaTruyLinh, null fTroCapHangThangTruyLinh, null fTroCapPHCNTruyLinh, null fTroCapChetDoTNLDTruyLinh, null fDuongSucTNLDTruyLinh
	union all
	select
		0 bHangCha, 5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMaCB) AS VARCHAR(6)) STT, '', 'LDHD' LoaiDoiTuong, Ma_DonVi,  Ten_DonVi TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD, 0 bHasData,
		SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh
	from TBL_TCTNLD_DOC
	where sMaCB in ('43','423','425') and (isnull(fChiGiamDinh, 0) <> 0 or isnull(fTroCap1Lan, 0) <> 0 or isnull(fTroCapTheoPhieuTruyTra, 0) <> 0 or isnull(fTroCapHangThang, 0) <> 0 or isnull(fTroCapPHCN, 0) <> 0 or isnull(fHoTroCdnn, 0) <> 0 or isnull(fHoTroPhongNgua, 0) <> 0 or isnull(fTroCapChetDoTNLD, 0) <> 0 or isnull(fDuongSucTNLD, 0) <> 0 OR
				ISNULL(fChiGiamDinhTruyLinh,0) <> 0 OR ISNULL(fTroCap1LanTruyLinh,0) <> 0 OR ISNULL(fHoTroCdnnTruyLinh,0) <> 0 OR ISNULL(fHoTroPhongNguaTruyLinh,0) <> 0 OR ISNULL(fTroCapHangThangTruyLinh,0) <> 0 OR
				ISNULL( fTroCapPHCNTruyLinh,0) <> 0 OR ISNULL(fTroCapChetDoTNLDTruyLinh,0) <> 0 OR ISNULL( fDuongSucTNLDTruyLinh, 0) <> 0)
		)  ldhd
	--Lay du lieu luong co gia tri
	if (select count(1) from TBL_TCTNLD_LDHD) > 1
		update TBL_TCTNLD_LDHD set bHasData = 1

	--Ket qua
	select result.* into TBL_TCTNLD_RESULT from
	(select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_SQ
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_QNCN
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_HSQBS
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_VCQP
	union all
	select
	bHasData, bHangCha, RowNum, STT, DoiTuong, LoaiDoiTuong, Ma_DonVi, TenDonVi, sMaCB, sMaCBo, sTenCbo, sSoQuyetDinh, dNgayQuyetDinh, SoNgayDuongSucTNLD, fChiGiamDinh, fTroCap1Lan, fTroCapTheoPhieuTruyTra, fTroCapHangThang, fTroCapPHCN, fHoTroCdnn, fHoTroPhongNgua, fTroCapChetDoTNLD, fDuongSucTNLD,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) fTongSoTienThangNay,
	SoNgayDuongSucTNLDTruyLinh, fChiGiamDinhTruyLinh, fTroCap1LanTruyLinh, fHoTroCdnnTruyLinh, fHoTroPhongNguaTruyLinh, fTroCapHangThangTruyLinh, fTroCapPHCNTruyLinh, fTroCapChetDoTNLDTruyLinh, fDuongSucTNLDTruyLinh,
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) fTongSoTienTruyLinh,
	(isnull(fChiGiamDinh, 0) + isnull(fTroCap1Lan, 0) + isnull(fTroCapTheoPhieuTruyTra, 0) + isnull(fTroCapHangThang, 0) + isnull(fTroCapPHCN, 0) + isnull(fHoTroCdnn, 0) + isnull(fHoTroPhongNgua, 0) + isnull(fTroCapChetDoTNLD, 0) + isnull(fDuongSucTNLD, 0)) +
	(isnull(fChiGiamDinhTruyLinh, 0) + isnull(fTroCap1LanTruyLinh, 0) + isnull(fHoTroCdnnTruyLinh, 0) + isnull(fHoTroPhongNguaTruyLinh, 0) + isnull(fTroCapHangThangTruyLinh, 0) + isnull(fTroCapPHCNTruyLinh, 0) + isnull(fTroCapChetDoTNLDTruyLinh, 0) + isnull(fDuongSucTNLDTruyLinh, 0)) FTongSoTien
	from TBL_TCTNLD_LDHD) result

	select 
		RowNum,
		STT,
		DoiTuong, 
		LoaiDoiTuong,
		sMaCB MaCb, 
		sMaCBo MaCbo, 
		sTenCbo TenCbo,
		sSoQuyetDinh SSoQuyetDinh,
		dNgayQuyetDinh DNgayQuyetDinh,
		Ma_DonVi MaDonVi,
		TenDonVi,
		SoNgayDuongSucTNLD,
		fChiGiamDinh/@DonViTinh fChiGiamDinh,
		fTroCap1Lan/@DonViTinh fTroCap1Lan,
		fTroCapTheoPhieuTruyTra/@DonViTinh fTroCapTheoPhieuTruyTra,
		fTroCapHangThang/@DonViTinh fTroCapHangThang,
		fTroCapPHCN/@DonViTinh fTroCapPHCN,
		fHoTroCdnn/@DonViTinh fHoTroCdnn,
		fHoTroPhongNgua/@DonViTinh fHoTroPhongNgua,
		fTroCapChetDoTNLD/@DonViTinh fTroCapChetDoTNLD,
		fDuongSucTNLD/@DonViTinh fDuongSucTNLD,
		fTongSoTienThangNay/@DonViTinh fTongSoTienThangNay,
		bHangCha IsHangCha,
		bHasData IsHasData,
		SoNgayDuongSucTNLDTruyLinh,
		fChiGiamDinhTruyLinh/@DonViTinh fChiGiamDinhTruyLinh,
		fTroCap1LanTruyLinh/@DonViTinh fTroCap1LanTruyLinh,
		fHoTroCdnnTruyLinh/@DonViTinh fHoTroCdnnTruyLinh,
		fHoTroPhongNguaTruyLinh/@DonViTinh fHoTroPhongNguaTruyLinh,
		fTroCapHangThangTruyLinh/@DonViTinh fTroCapHangThangTruyLinh,
		fTroCapPHCNTruyLinh/@DonViTinh fTroCapPHCNTruyLinh,
		fTroCapChetDoTNLDTruyLinh/@DonViTinh fTroCapChetDoTNLDTruyLinh,
		fDuongSucTNLDTruyLinh/@DonViTinh fDuongSucTNLDTruyLinh,
		fTongSoTienTruyLinh/@DonViTinh fTongSoTienTruyLinh,
		fTongSoTien/@DonViTinh fTongSoTien

	from TBL_TCTNLD_RESULT
	order by RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD]') AND type in (N'U'))
	drop table TBL_TCTNLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_DOC]') AND type in (N'U'))
	drop table TBL_TCTNLD_DOC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_SQ]') AND type in (N'U'))
	drop table TBL_TCTNLD_SQ;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_QNCN]') AND type in (N'U'))
	drop table TBL_TCTNLD_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_HSQBS]') AND type in (N'U'))
	drop table TBL_TCTNLD_HSQBS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_VCQP]') AND type in (N'U'))
	drop table TBL_TCTNLD_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_LDHD]') AND type in (N'U'))
	drop table TBL_TCTNLD_LDHD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_TCTNLD_RESULT]') AND type in (N'U'))
	drop table TBL_TCTNLD_RESULT;

END
;
;
;
;
;
;
;
;
GO
