/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]    Script Date: 8/4/2023 4:47:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt]    Script Date: 8/4/2023 4:47:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 8/4/2023 4:47:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiao_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]    Script Date: 8/4/2023 4:47:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all]    Script Date: 8/4/2023 4:47:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]    Script Date: 8/4/2023 4:47:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 8/4/2023 4:47:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_dotnhan]    Script Date: 8/4/2023 4:47:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dt_danhsach_dotnhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dt_danhsach_dotnhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_dotnhan]    Script Date: 8/4/2023 4:47:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[sp_bh_dt_danhsach_dotnhan]
	@IdPhanBo nvarchar(100)
AS
BEGIN
	WITH tblIdPhanBo AS (
		SELECT * FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_PhanBo = @IdPhanBo
		UNION ALL
		SELECT map.* FROM NS_DT_Nhan_PhanBo_Map map
		INNER JOIN tblIdPhanBo
		ON map.iID_CTDuToan_PhanBo = tblIdPhanBo.iID_CTDuToan_Nhan
	)

	SELECT ct.*
	FROM NS_DT_ChungTu ct
	INNER JOIN tblIdPhanBo pb
	ON ct.iID_DTChungTu = pb.iID_CTDuToan_Nhan
	WHERE ct.iLoai = 0;
END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_chungtu_chitiet_luyke]    Script Date: 15/12/2021 6:35:25 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]    Script Date: 8/4/2023 4:47:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@IdDonVi NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
As
begin
	---Lấy danh sách mục lục ngân sách
	select 
	NEWID()  as iID_DTC_DuToanChiTrenGiao,
	'00000000-0000-0000-0000-000000000000' as iID_DTC_PhanBoDuToanChiTiet,
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
	0 as fTienTuChiTruocDieuChinh,
	0 as fTienHienVat,
	0 as fTienHienVatTruocDieuChinh,
	0 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	1 as bHangCha,
	0 as IsRemainRow
	into tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec;

	---Lấy danh sách đơn vị được phân bổ
	select * 
	into  tblDonVi
	from DonVi where iNamLamViec = 2023 and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;

	---Lấy danh sách chi tiết toán nhận phân bổ
	select 
			nhanpb_chitiet.ID as iIDNhan_ChiTiet,
			nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
			'' as iID_MaDonVi,
			nhanpb_chitiet.iID_MucLucNganSach,
			0 as fTuChi,
			0 as fHienVat
	into tblChiTietDuToanNhan
	from BH_DTC_DuToanChiTrenGiao_ChiTiet as nhanpb_chitiet
	inner join
		(select nhanpb.ID from BH_DTC_DuToanChiTrenGiao as nhanpb
		inner join BH_DTC_Nhan_PhanBo_Map as map on nhanpb.ID = map.iID_BHDTC_NhanPhanBo
		where map.iID_BHDTC_PhanBo = @ChungTuId) as  nhanpb_t on nhanpb_t.ID = nhanpb_chitiet.iID_DTC_DuToanChiTrenGiao

	---Lấy  danh sách chi tiết nhận phân bổ có thông tin mục lục ngân sách
	select 
		tblChiTietDuToanNhan.iID_DTC_DuToanChiTrenGiao,
		tblChiTietDuToanNhan.iID_MucLucNganSach as iID_MLNS,
		tblMucLucNganSach.iID_MLNS_Cha,
		tblMucLucNganSach.sLNS,
		tblMucLucNganSach.sL,
		tblMucLucNganSach.sK, 
		tblMucLucNganSach.sM,
		tblMucLucNganSach.sTM,
		tblMucLucNganSach.sTTM,
		tblMucLucNganSach.sNG,
		tblMucLucNganSach.sTNG,
		tblMucLucNganSach.sXauNoiMa,
		tblMucLucNganSach.sNoiDung,
		tblChiTietDuToanNhan.fTuChi as fTienTuChi ,
		tblChiTietDuToanNhan.fHienVat as fTienHienVat,
		2 as Type
	into tbl_tblChiTietDuToanNhan_MucLuc
	from tblMucLucNganSach
	inner join tblChiTietDuToanNhan on tblMucLucNganSach.iID_MLNS = tblChiTietDuToanNhan.iID_MucLucNganSach


	---Hiển thị danh sách chi tiết nhận phân bổ theo đơn vị được chọn phân bổ
	select  tbl_tblChiTietDuToanNhan_MucLuc.*,tblDonVi.iID_MaDonVi, concat(tblDonVi.iID_MaDonVi, '-', tblDonVi.sTenDonVi) as sTenDonVi, 0 as bHangCha
	into #temp
	from tbl_tblChiTietDuToanNhan_MucLuc cross join tblDonVi 

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
		chitiet_phanbo.fTienTuChi as fTienTuChiTruocDieuChinh,
		chitiet_phanbo.fTienHienVat as fTienHienVat,
		chitiet_phanbo.fTienHienVat as fTienHienVatTruocDieuChinh,
		2 as Type,
		#temp.iID_MaDonVi,
		#temp.sTenDonVi,
		0 as bHangCha,
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
	NEWID() as iID_DTC_DuToanChiTrenGiao,
	'00000000-0000-0000-0000-000000000000' as iID_DTC_PhanBoDuToanChiTiet,
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
	N'Số Chưa Phân Bổ' as sNoiDung,
	chitiet_chuaphanbo.fTuChi as fTienTuChi,
	chitiet_chuaphanbo.fTuChi as fTienTuChiTruocDieuChinh,
	chitiet_chuaphanbo.fHienVat as fTienHienVat,
	chitiet_chuaphanbo.fHienVat as fTienHienVatTruocDieuChinh,
	1 as Type,
	'' as iID_MaDonVi,
	'' as sTenDonVi,
	1 as bHangCha,
	1 as IsRemainRow
	into tblSoChuaPhanBo
	from tblMucLucNganSach as muluc_ngansach
	inner join 
	(
		select (ISNULL(ct_npb.fTienTuChi,0) - ISNULL(ct_pb_t.fTuChi,0)) as fTuChi , (ISNULL(ct_npb.fTienHienVat,0) - ISNULL(ct_pb_t.fHienVat,0)) as fHienVat, ct_npb.iID_MucLucNganSach, ct_npb.iID_DTC_DuToanChiTrenGiao
		from BH_DTC_DuToanChiTrenGiao_ChiTiet as ct_npb
		left join
			(
				select sum(fTienTuChi) as fTuChi , sum(fTienHienVat) as fHienVat , iID_MucLucNganSach
				from BH_DTC_PhanBoDuToanChi_ChiTiet as ct_pb
				where iID_DTC_PhanBoDuToanChi =  @ChungTuId
				group by  iID_MucLucNganSach
			)as ct_pb_t  
			on ct_pb_t.iID_MucLucNganSach = ct_npb.iID_MucLucNganSach) as chitiet_chuaphanbo on chitiet_chuaphanbo.iID_MucLucNganSach = muluc_ngansach.iID_MLNS 


	---Kết quả trả về 
	--select * from tblMucLucNganSach;
	--select * from tblSoChuaPhanBo;
	--select * from #temp1;
	
	select * from
	(
		SELECT * from tblMucLucNganSach
		UNION ALL
		SELECT * from tblSoChuaPhanBo
		UNION ALL
		SELECT * from #temp1
	) as test
	order by test.sXauNoiMa, Type, iID_MaDonVi


drop table tblMucLucNganSach;
drop table tblDonVi;
drop table tblChiTietDuToanNhan;
drop table tbl_tblChiTietDuToanNhan_MucLuc;
drop table #temp;
drop table #temp1;
drop table tblSoChuaPhanBo;

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]    Script Date: 8/4/2023 4:47:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_bh_dt_danhsach_pbdtc_chitiet_dieuchinh]
	@ChungTuId NVARCHAR(255),
	@LNS NVARCHAR(MAX),
	@NamLamViec int,
	@UserName NVARCHAR(100)
AS
begin
	---L?y danh sách m?c l?c ngân sách
	select 
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
	NULL as iID_DTC_DuToanChiTrenGiao,
	NULL as IID_DTC_PhanBoDuToanChiTiet,
	NULL as iID_MaDonVi,
	NULL as sTenDonVi,
	NULL as sSoQuyetDinh,
	0 as fTienTuChiTruocDieuChinh,
	0 as fTienTuChi,
	0 as fTienTuChiSauDieuChinh,
	0 as fTienHienVatTruocDieuChinh,
	0 as fTienHienVat,
	0 as fTienHienVatSauDieuChinh,
	bHangCha,
	0 as Type
	into tblMucLucNganSach
	from BH_DM_MucLucNganSach 
	where sLNS  IN (SELECT * FROM f_split(@LNS)) and iNamLamViec = @NamLamViec;

	---L?y danh sách ch?ng t? b? ?i?u ch?nh
	select * 
	into tblChungTuBiDieuChinh
	from BH_DTC_Nhan_PhanBo_Map
	where iID_BHDTC_PhanBo = @ChungTuId

	---L?y danh sách chi ti?t ch?ng t? b? ?i?u ch?nh
	select dc_ct.*, dc.sSoQuyetDinh
	into tblChungTuBiDieuChinh_Ct 
	from BH_DTC_PhanBoDuToanChi_ChiTiet as dc_ct  
	inner join BH_DTC_PhanBoDuToanChi as dc on dc_ct.iID_DTC_PhanBoDuToanChi = dc.ID
	where dc_ct.iID_DTC_PhanBoDuToanChi in ( select iID_BHDTC_NhanPhanBo from tblChungTuBiDieuChinh)
	

	--- Map v?i b?ng BH_DTC_PhanBoDuToanChi_ChiTiet ?? l?y thông tin s? tr??c và sau khi ?i?u ch?nh
	select  
	npb_ct.iID_MucLucNganSach,
	npb_ct.iID_MaDonVi,
	DonVi.sTenDonVi,
	npb_ct.sSoQuyetDinh,
	npb_ct.fTienTuChi as fTienTuChiTruocDieuChinh,
	npb_ct.fTienHienVat as fTienHienVatTruocDieuChinh,
	npb_ct.iID_DTC_DuToanChiTrenGiao as iID_DTC_DuToanChiTrenGiao,
	pb_ct.ID as IID_DTC_PhanBoDuToanChiTiet,
	pb_ct.fTienTuChi,
	pb_ct.fTienHienVat,
	0 as bHangCha,
	1 Type
	into tblThongTinChungTu
	from tblChungTuBiDieuChinh_Ct as npb_ct
	left join ( select * from BH_DTC_PhanBoDuToanChi_ChiTiet where iID_DTC_PhanBoDuToanChi =  @ChungTuId) as  pb_ct on npb_ct.iID_MucLucNganSach = pb_ct.iID_MucLucNganSach and npb_ct.iID_MaDonVi = pb_ct.iID_MaDonVi
	left join DonVi on npb_ct.iID_MaDonVi = DonVi.iID_MaDonVi
	where DonVi.iNamLamViec = @NamLamViec


	---Hi?n th? k?t qu? tr? v? có m?c l?c ngân sách

	select 
	tblMucLucNganSach.iID_MLNS,
	tblMucLucNganSach.iID_MLNS_Cha,
	tblMucLucNganSach.sLNS,
	tblMucLucNganSach.sL,
	tblMucLucNganSach.sK,
	tblMucLucNganSach.sM,
	tblMucLucNganSach.sTM,
	tblMucLucNganSach.sTTM,
	tblMucLucNganSach.sNG,
	tblMucLucNganSach.sTNG,
	tblMucLucNganSach.sXauNoiMa,
	tblMucLucNganSach.sNoiDung,
	tblThongTinChungTu.iID_DTC_DuToanChiTrenGiao,
	tblThongTinChungTu.IID_DTC_PhanBoDuToanChiTiet,
	tblThongTinChungTu.iID_MaDonVi,
	CONCAT(tblThongTinChungTu.iID_MaDonVi, '-', tblThongTinChungTu.sTenDonVi) as sTenDonVi,
	tblThongTinChungTu.sSoQuyetDinh,
	tblThongTinChungTu.fTienTuChiTruocDieuChinh,
	tblThongTinChungTu.fTienTuChi,
	ISNULL(tblThongTinChungTu.fTienTuChiTruocDieuChinh,0) + ISNULL(tblThongTinChungTu.fTienTuChi,0) as fTienTuChiSauDieuChinh,
	tblThongTinChungTu.fTienHienVatTruocDieuChinh,
	tblThongTinChungTu.fTienHienVat,
	ISNULL(tblThongTinChungTu.fTienHienVatTruocDieuChinh,0) + ISNULL(tblThongTinChungTu.fTienHienVat,0) as fTienHienVatSauDieuChinh,
	case when tblThongTinChungTu.Type = 1 then tblThongTinChungTu.bHangCha else tblMucLucNganSach.bHangCha end as bHangCha
	into tblThongTinChungTu_MLNS
	from tblMucLucNganSach
	left join tblThongTinChungTu on tblMucLucNganSach.iID_MLNS = tblThongTinChungTu.iID_MucLucNganSach

	select * from tblThongTinChungTu_MLNS 
	order  by sXauNoiMa, iID_MaDonVi 

	drop table tblMucLucNganSach;
	drop table tblChungTuBiDieuChinh;
	drop table tblChungTuBiDieuChinh_Ct;
	drop table tblThongTinChungTu;
	drop table tblThongTinChungTu_MLNS;


end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all]    Script Date: 8/4/2023 4:47:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all]
@YearOfWork int ,
@Date DateTime,
@LoaiDuToanNhan int
AS
BEGIN
	DECLARE @DieuChinh int = 2;
	--Lấy danh sách dự toán nhận phân bổ
		SELECT ID as iID_DTC_DuToanChiTrenGiao,
			   sSoChungTu,
			   sLNS,
			   iID_MaDonVi,
			   dNgayChungTu,
			   sSoQuyetDinh,
			   dNgayQuyetDinh,
			   fTongTienTuChi + fTongTienHienVat AS fSoPhanBo
		INTO  tblNhanPhanBo
		FROM BH_DTC_DuToanChiTrenGiao 
		WHERE iNamChungTu = @YearOfWork 
			AND iLoaiDotNhanPhanBo = @LoaiDuToanNhan
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))

	
		--Lấy danh sách dự toán đã được phân bổ
		SELECT ISNULL(sum(pb_ct.fTienTuChi),0) + ISNULL(sum(pb_ct.fTienHienVat),0)  as fDaPhanBo, pb_ct.iID_DTC_DuToanChiTrenGiao AS iID_DTC_DuToanChiTrenGiao
		INTO tblChungTuNhanPhanBoMap
		FROM  BH_DTC_PhanBoDuToanChi_ChiTiet as pb_ct 
		WHERE pb_ct.iID_DTC_DuToanChiTrenGiao in (select iID_DTC_DuToanChiTrenGiao from  tblNhanPhanBo)
		GROUP BY pb_ct.iID_DTC_DuToanChiTrenGiao


		-----Lay danh sach du toan nhan phan bo, so phan bo, chua phan bo
		SELECT  npb.iID_DTC_DuToanChiTrenGiao as Id,
			    npb.sSoChungTu, 
				npb.sLNS,
				npb.iID_MaDonVi,
				npb.dNgayChungTu, 
				npb.sSoQuyetDinh, 
				npb.dNgayQuyetDinh, 
				npb.fSoPhanBo, 
				npbm.fDaPhanBo,
				ISNULL(npb.fSoPhanBo,0) - ISNULL(npbm.fDaPhanBo,0) AS fSoChuaPhanBo
		FROM tblNhanPhanBo AS npb
		left join tblChungTuNhanPhanBoMap AS npbm ON npb.iID_DTC_DuToanChiTrenGiao = npbm.iID_DTC_DuToanChiTrenGiao

	   DROP TABLE tblNhanPhanBo;	
       DROP TABLE tblChungTuNhanPhanBoMap;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]    Script Date: 8/4/2023 4:47:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]
@idDonvi nvarchar(50),
@sLns nvarchar(max)

AS
BEGIN

SELECT 
	mucluc.iID_MLNS,
	mucluc.iID_MLNS_Cha,
	mucluc.sXauNoiMa,
	mucluc.sLNS,
	mucluc.sL,
	mucluc.sK,
	mucluc.sM,
	mucluc.sTM,
	mucLuc.sTTM,
	mucluc.sNG,
	mucluc.sMoTa AS sNoiDung,
	mucluc.bHangCha,
	d.fTienHienVat,
	d.fTienTuChi,
	d.fTongTien

	FROM BH_DM_MucLucNganSach AS mucluc
		left join (SELECT  
						sum(c.fTienHienVat) AS fTienHienVat,
						sum(c.fTienTuChi) AS fTienTuChi,
						sum(c.fTongTien) AS fTongTien,
						c.iID_MucLucNganSach,
						c.iID_MaDonVi
		
						FROM 
							(SELECT 
								b.fTongTien,
								b.fTienHienVat,
								b.fTienTuChi,
								b.iID_MucLucNganSach,
								a.sLNS,
								a.iID_MaDonVi

								FROM BH_DTC_DuToanChiTrenGiao AS a
								left join BH_DTC_DuToanChiTrenGiao_ChiTiet AS b on a.ID = b.iID_DTC_DuToanChiTrenGiao
								WHERE a.iID_MaDonVi = @idDonvi and a.iNamChungTu = 2023) AS c
						GROUP BY c.iID_MucLucNganSach, c.iID_MaDonVi) AS d on  mucluc.iID_MLNS = d.iID_MucLucNganSach
	WHERE mucluc.sLNS in  (SELECT * FROM f_split(@sLns)) and mucluc.iNamLamViec = 2023
	ORDER BY mucluc.sXauNoiMa
		

END

GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 8/4/2023 4:47:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiao_index] 
	@YearOfWork int
AS
BEGIN
	SELECT
	DTC.ID,
	DTC.sSoChungTu,
	DTC.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	DTC.dNgayChungTu,
	DTC.sSoQuyetDinh,
	DTC.dNgayQuyetDinh,
	DTC.sLNS,
	DTC.fTongTienTuChi,
	DTC.fTongTienHienVat,
	DTC.fTongTien,
	DTC.iLoaiDotNhanPhanBo,
	DTC.sMoTa,
	DTC.iNamChungTu,
	DTC.sNguoiTao,
	DTC.bIsKhoa
	FROM BH_DTC_DuToanChiTrenGiao DTC
	LEFT JOIN DonVi DV
	ON DTC.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	ORDER BY DTC.dNgayQuyetDinh DESC
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt]    Script Date: 8/4/2023 4:47:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt]
@iDNdtctg uniqueidentifier

AS
BEGIN
	SELECT 
	B.ID,
	B.sLNS,
	B.iID_DTC_DuToanChiTrenGiao,
	B.iID_MucLucNganSach,
	B.fTongTien,
	B.fTienHienVat,
	B.fTienTuChi
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet AS B
	WHERE B.iID_DTC_DuToanChiTrenGiao = @iDNdtctg
	
	END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]    Script Date: 8/4/2023 4:47:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max)

AS
BEGIN

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
	B.ID,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTongTien,
	B.fTienHienVat,
	B.fTienTuChi
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN ( SELECT * FROM BH_DTC_DuToanChiTrenGiao_ChiTiet WHERE iID_DTC_DuToanChiTrenGiao = @iDNdtctg) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
	order by A.sXauNoiMa



END
GO
