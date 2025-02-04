/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 4/16/2024 5:45:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]    Script Date: 4/16/2024 5:45:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_ndt_ctg_get_khc_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 4/16/2024 5:45:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 4/16/2024 5:45:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As

begin
	-- Lấy danh mục Phân bổ thu BHXH
	SELECT SUM(0.1*(pbctct.fBHYT_NLD+ pbctct.fBHYT_NSD)) as fTienPhanBo, pbctct.iID_MaDonVi INTO #temPBThuBHXH
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet as pbctct
	JOIN BH_DTT_BHXH_PhanBo_ChungTu as pbct
	ON pbctct.iID_DTT_BHXH_ChungTu = pbct.iID_DTT_BHXH_PhanBo_ChungTu
	WHERE (pbctct.sXauNoiMa like '9020001-010-011-0001%' or pbctct.sXauNoiMa like '9020002-010-011-0001%')
	AND pbctct.iNamLamViec = @NamLamViec
	AND pbctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND pbct.iLoaiDuToan = 1
	GROUP BY pbctct.iID_MaDonVi

	---Lấy danh sách mục lục ngân sách theo điều kiện @LNS
	select 
	NEWID() as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
	BH_DM_MucLucNganSach.iID_MLNS_Cha,
	BH_DM_MucLucNganSach.sLNS,
	BH_DM_MucLucNganSach.sL,
	BH_DM_MucLucNganSach.sK,
	BH_DM_MucLucNganSach.sM,
	BH_DM_MucLucNganSach.sTM,
	BH_DM_MucLucNganSach.sTTM,
	BH_DM_MucLucNganSach.sNG,
	BH_DM_MucLucNganSach.sTNG,
	BH_DM_MucLucNganSach.sXauNoiMa,
	BH_DM_MucLucNganSach.sMoTa as sNoiDung,
	0 as fTienTuChi,
	--0 as fTienTuChiTruocDieuChinh,
	--0 as fTienHienVat,
	--0 as fTienHienVatTruocDieuChinh,
	BH_DM_MucLucNganSach.sCPChiTietToi,
	BH_DM_MucLucNganSach.sDuToanChiTietToi,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	'' as sSoQuyetDinh,
	BH_DM_MucLucNganSach.bHangCha as bHangCha,
	BH_DM_MucLucNganSach.bHangChaDuToan,
	0 as IsRemainRow
	into #tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	--where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  and sLNS LIKE '901%'
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec  
	and bHangChaDuToan is not null
	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  #tblDonVi
	from DonVi where iNamLamViec = @NamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	--lấy danh sách dự toán nhận phân bổ
	select *
	into #tblChungTuNhanPhanBo
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId

	
	---Lấy danh sách chi tiết dự toán toán nhận phân bổ
	select 
			nhanpb_chitiet.ID as iIDNhan_ChiTiet,
			nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MucLucNganSach,
			nhanpb_chitiet.fTienTuChi as fTuChi,
			--nhanpb_chitiet.fTienHienVat as fHienVat,
			nhanpb.sSoQuyetDinh
	into #tblChiTietDuToanNhan
	from BH_DTC_DuToanChiTrenGiao_ChiTiet as nhanpb_chitiet
	inner join BH_DTC_DuToanChiTrenGiao as nhanpb on nhanpb.ID = nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao
	where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)

	

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		#tblChiTietDuToanNhan.iID_DTC_DuToanChiTrenGiao,
		#tblChiTietDuToanNhan.iID_MucLucNganSach as iID_MLNS,
		#tblMucLucNganSach.iID_MLNS_Cha,
		#tblMucLucNganSach.sLNS,
		#tblMucLucNganSach.sL,
		#tblMucLucNganSach.sK, 
		#tblMucLucNganSach.sM,
		#tblMucLucNganSach.sTM,
		#tblMucLucNganSach.sTTM,
		#tblMucLucNganSach.sNG,
		#tblMucLucNganSach.sTNG,
		#tblMucLucNganSach.sXauNoiMa,
		#tblMucLucNganSach.sNoiDung,
		#tblChiTietDuToanNhan.sSoQuyetDinh,
		#tblChiTietDuToanNhan.fTuChi as fTienTuChi ,
		--#tblChiTietDuToanNhan.fHienVat as fTienHienVat,
		#tblMucLucNganSach.sCPChiTietToi,
		#tblMucLucNganSach.sDuToanChiTietToi,
		3 as Type
	into #tbl_tblChiTietDuToanNhan_MucLuc
	from #tblMucLucNganSach
	inner join #tblChiTietDuToanNhan on #tblMucLucNganSach.iID_MLNS = #tblChiTietDuToanNhan.iID_MucLucNganSach

	


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  #tbl_tblChiTietDuToanNhan_MucLuc.*,#tblDonVi.iID_MaDonVi, concat(#tblDonVi.iID_MaDonVi, '-', #tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from #tbl_tblChiTietDuToanNhan_MucLuc cross join #tblDonVi 


	---Map với bảng BH_DTC_PhanBoDuToanChi_ChiTiet để lấy thông tin fTuChi đã được phân bổ
	select 
		#temp.iID_DTC_DuToanChiTrenGiao, 
		chitiet_phanbo.ID as iID_DTC_PhanBoDuToanChiTiet,
		#temp.iID_MLNS,
		#temp.iID_MLNS_Cha,
		#temp.sLNS,
		#temp.sL,
		#temp.sK,
		#temp.sM,
		#temp.sTM,
		#temp.sTTM,
		#temp.sNG,
		#temp.sTNG,
		#temp.sXauNoiMa,
		#temp.sNoiDung as sNoiDung,
		chitiet_phanbo.fTienTuChi as fTienTuChi,
		#temp.fTienTuChi as fTienTuChiTruocDieuChinh,
		--chitiet_phanbo.fTienHienVat as fTienHienVat,
		--#temp.fTienHienVat as fTienHienVatTruocDieuChinh,
		#temp.sCPChiTietToi,
		#temp.sDuToanChiTietToi,
		3 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		#temp.sSoQuyetDinh,
		0 as bHangCha,
		0 as bHangChaDuToan,
		0 as IsRemainRow
	into #temp1
	from #temp
	left join 
		(
			select * 
			from BH_DTC_PhanBoDuToanChi_ChiTiet 
			where iID_DTC_PhanBoDuToanChi = @ChungTuId
		) as chitiet_phanbo
		on chitiet_phanbo.iID_DTC_DuToanChiTrenGiao = #temp.iID_DTC_DuToanChiTrenGiao and chitiet_phanbo.iID_MaDonVi = #temp.iID_MaDonVi and chitiet_phanbo.iID_MucLucNganSach = #temp.iID_MLNS



	-----Lấy danh sách số chưa phân bổ
	select 
	npb.ID as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	muluc_ngansach.iID_MLNS as iID_MLNS,
	muluc_ngansach.iID_MLNS_Cha,
	muluc_ngansach.sLNS,
	muluc_ngansach.sL,
	muluc_ngansach.sK,
	muluc_ngansach.sM,
	muluc_ngansach.sTM,
	muluc_ngansach.sTTM,
	muluc_ngansach.sNG,
	muluc_ngansach.sTNG,
	muluc_ngansach.sXauNoiMa,
	N'Số chưa phân bổ' as sNoiDung,
	chitiet_chuaphanbo.fTuChi as fTienTuChi,
	chitiet_chuaphanbo.fTuChi as fTienTuChiTruocDieuChinh,
	--chitiet_chuaphanbo.fHienVat as fTienHienVat,
	--chitiet_chuaphanbo.fHienVat as fTienHienVatTruocDieuChinh,
	muluc_ngansach.sCPChiTietToi,
	muluc_ngansach.sDuToanChiTietToi,
	2 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	npb.sSoQuyetDinh as sSoQuyetDinh,
	1 as bHangCha,
	1 as bHangChaDuToan,
	1 as IsRemainRow
	into #tblSoChuaPhanBo
	from #tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (
		
		ISNULL(ct_npb.fTienTuChi,0) -  ISNULL(ct_pb_t.fTuChi,0) ) as fTuChi ,
		ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		--select (ISNULL(ct_npb.fTienTuChi,0) - ISNULL(ct_pb_t.fTuChi,0)) as fTuChi , (ISNULL(ct_npb.fTienHienVat,0) - ISNULL(ct_pb_t.fHienVat,0)) as fHienVat, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao 
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select  sum(  fTienTuChi) as fTuChi , iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_DuToanChiTrenGiao in (select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo)
				group by  iID_MucLucNganSach, iID_DTC_DuToanChiTrenGiao
			)as ct_pb_t  

		on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach and ct_npb.iID_DTC_DuToanChiTrenGiao = ct_pb_t.iID_DTC_DuToanChiTrenGiao) as chitiet_chuaphanbo
		on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 
	inner join BH_DTC_DuToanChiTrenGiao as npb on npb.ID = chitiet_chuaphanbo.iID_DTC_DuToanChiTrenGiao
	where npb.ID in ( select iID_BHDTC_NhanPhanBo from #tblChungTuNhanPhanBo);
	---- Lấy mục lục ngân sách có dupliate các mục lục ngân sách con
	select 
	#tblSoChuaPhanBo.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	CAST(NULL AS VARCHAR(50)) as iID_DTC_PhanBoDuToanChiTiet,
	#tblMucLucNganSach.iID_MLNS as iID_MLNS,
	#tblMucLucNganSach.iID_MLNS_Cha,
	#tblMucLucNganSach.sLNS,
	#tblMucLucNganSach.sL,
	#tblMucLucNganSach.sK,
	#tblMucLucNganSach.sM,
	#tblMucLucNganSach.sTM,
	#tblMucLucNganSach.sTTM,
	#tblMucLucNganSach.sNG,
	#tblMucLucNganSach.sTNG,
	#tblMucLucNganSach.sXauNoiMa,
	#tblMucLucNganSach.sNoiDung as sNoiDung,
	#tblSoChuaPhanBo.fTienTuChi as fTienTuChi,
	#tblSoChuaPhanBo.fTienTuChiTruocDieuChinh as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	#tblMucLucNganSach.sCPChiTietToi,
	#tblMucLucNganSach.sDuToanChiTietToi,
	case when #tblSoChuaPhanBo.Type = 2 then 2 else #tblMucLucNganSach.Type end Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	#tblSoChuaPhanBo.sSoQuyetDinh as sSoQuyetDinh,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangCha,
	case when #tblSoChuaPhanBo.Type = 2 then 1 else #tblMucLucNganSach.bHangCha end bHangChaDuToan,
	0 as IsRemainRow
	into #tblMucLucNganSach_duplicate
	from #tblMucLucNganSach
	left join #tblSoChuaPhanBo on #tblMucLucNganSach.iID_MLNS = #tblSoChuaPhanBo.iID_MLNS
	order by sXauNoiMa
	
	--select * from tblMucLucNganSach_duplicate
	--select * from tblSoChuaPhanBo
	--select * from #temp1
	---Tính lại dự toán, số đã phân bổ
	-- Dữ liệu nhận phân bổ
	declare @iiDotNhan nvarchar(500) =( select  iID_DotNhan from  BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
	select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, fTienTuChi INTO #tmpNhanDuToan from BH_DTC_DuToanChiTrenGiao_ChiTiet ct
	INNER JOIN BH_DTC_DuToanChiTrenGiao dt on dt.ID = ct.iID_DTC_DuToanChiTrenGiao
	where dt.ID IN (select * from splitstring( @iiDotNhan))


	-- Dữ liệu đã phân bổ
	declare @dNgayQuyetDinh Datetime = (select dNgayQuyetDinh from BH_DTC_PhanBoDuToanChi where ID = @ChungTuId);
	select iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao, 0 - SUM(ISNULL(fTienTuChi,0)) fTuChi   INTO #tmpDaPhanBo from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	INNER JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	where 
	ct.iNamChungTu = @NamLamViec
	AND ct.dNgayQuyetDinh < @dNgayQuyetDinh
	group by iID_MucLucNganSach,iID_DTC_DuToanChiTrenGiao;

	--Hiển thị kết quả trả về
	select * INTO #result from
	(
		SELECT * from #tblMucLucNganSach_duplicate
		UNION ALL
		SELECT * from #tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	--order by test.sXauNoiMa, test.sSoQuyetDinh, test.iID_MaDonVi,test.Type,test.IsRemainRow

	----============
	SELECT
	rs.iID_DTC_DuToanChiTrenGiao,
	rs.iID_DTC_PhanBoDuToanChiTiet,
	rs.iID_MLNS,
	rs.iID_MLNS_Cha,
	rs.sLNS,
	rs.sL,
	rs.sK,
	rs.sM,
	rs.sTM,
	rs.sTTM,
	rs.sNG,
	rs.sTNG,
	rs.sXauNoiMa,
	rs.sNoiDung as sNoiDung,

	CASE WHEN rs.sXauNoiMa = '9010004' THEN pbbhxh.fTienPhanBo
	ELSE (
		CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
		END
	)
	END as fTienTuChi,
	CASE WHEN rs.IsRemainRow = 1 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		WHEN rs.IsRemainRow = 0 AND rs.Type = 2 THEN ISNULL(dt.fTienTuChi,0) + (ISNULL(dpb.fTuChi, 0))
		ELSE rs.fTienTuChi
	END as fTienTuChiTruocDieuChinh,
	--#tblSoChuaPhanBo.fTienHienVat as fTienHienVat,
	--#tblSoChuaPhanBo.fTienHienVatTruocDieuChinh as fTienHienVatTruocDieuChinh,
	rs.sCPChiTietToi,
	rs.sDuToanChiTietToi,
	rs.Type,
	rs.iID_MaDonVi,
	rs.sTenDonVi,
	rs.sSoQuyetDinh,
	rs.bHangCha,
	rs.bHangChaDuToan,
	rs.IsRemainRow
	FROM #result rs
	LEFT JOIN #tmpNhanDuToan dt ON rs.iID_MLNS = dt.iID_MucLucNganSach 
	LEFT JOIN #tmpDaPhanBo dpb ON dpb.iID_MucLucNganSach = rs.iID_MLNS and dpb.iID_DTC_DuToanChiTrenGiao = rs.iID_DTC_DuToanChiTrenGiao
	LEFT JOIN (
	SELECT SUM(fTienTuChi) fTuChi, iID_MucLucNganSach FROM BH_DTC_PhanBoDuToanChi_ChiTiet WHERE iID_DTC_PhanBoDuToanChi = @ChungTuId GROUP BY iID_MucLucNganSach

	) ct ON ct.iID_MucLucNganSach = rs.iID_MLNS
	LEFT JOIN #temPBThuBHXH pbbhxh ON rs.iID_MaDonVi = pbbhxh.iID_MaDonVi
	order by rs.sXauNoiMa, rs.sSoQuyetDinh, rs.iID_MaDonVi,rs.Type,rs.IsRemainRow
	--SELECT * from #tblSoChuaPhanBo


drop table #tblMucLucNganSach;
drop table #tblDonVi;
drop table #tblChungTuNhanPhanBo;
drop table #tblChiTietDuToanNhan;
drop table #tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table #tblSoChuaPhanBo;
drop table #tblMucLucNganSach_duplicate;

end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]    Script Date: 4/16/2024 5:45:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_ndt_ctg_get_khc_slns]
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
	A.bHangCha,
	a.bHangChaDuToan,
	A.bHangCha as isHangCha,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	CASE WHEN A.sXauNoiMa = '9010004' THEN @fTienDauNam ELSE B.fTienKeHoachThucHienNamNay END as fTienTuChi,
	A.iNamlamViec,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	@IIDDonVi as iID_MaDonVi
	FROM BH_DM_MucLucNganSach AS A
		LEFT JOIN (
					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_CheDoBHXH_ChiTiet ctct
					left join BH_KHC_CheDoBHXH ct on ctct.iID_KHC_CheDoBHXH=ct.ID
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1
					UNION All

					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_KinhPhiQuanLy_ChiTiet ctct
					left join BH_KHC_KinhPhiQuanLy ct on ctct.iID_KHC_KinhPhiQuanLy=ct.iID_BH_KHC_KinhPhiQuanLy
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1

					UNION ALL
					select
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_KCB_ChiTiet ctct
					left join BH_KHC_KCB ct on ctct.iID_KHC_KCB=ct.iID_BH_KHC_KCB
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1

					UNION ALL
					select	
					@iDNdtctg as iID_DTC_DuToanChiTrenGiao
					, ctct.sXauNoiMa
					, ctct.fTienKeHoachThucHienNamNay
					from BH_KHC_K_ChiTiet ctct
					left join BH_KHC_K ct on ctct.iID_KHC_K=ct.iID_BH_KHC_K
					where (ct.iLoaiTongHop=2 ) and ct.iID_MaDonVi=@IIDDonVi
					and ct.iNamLamViec=@NamLamViec
					and ct.bIsKhoa=1
			
			) AS B
		ON A.sXauNoiMa = B.sXauNoiMa
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
		--AND a.bHangChaDuToan IS NOT NULL
	order by A.sXauNoiMa
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns_clone]    Script Date: 4/16/2024 5:45:00 PM ******/
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
	CASE WHEN A.sXauNoiMa = '9010004' THEN @fTienDauNam ELSE B.fTienTuChi END as fTienTuChi,
	A.sCPChiTietToi,
	A.sDuToanChiTietToi,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi,
	 B.dNgaySua,
	 B.dNgayTao,
	 B.sNguoiSua,
	 B.sNguoiTao
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN ( SELECT ctct.*
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
GO
