/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]    Script Date: 9/25/2023 8:25:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]    Script Date: 9/25/2023 8:25:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]
@INamLamViec int,
@IdDonVi NVARCHAR(MAX),
@DonViTinh int,
@LNS NVARCHAR(MAX),
@IsTongHop int
as
begin
	--- Lấy danh sách các đơn vi được chọn
	select 
		ROW_NUMBER() OVER(PARTITION BY DonVi.iKhoi  ORDER BY DonVi.iID_MaDonVi ASC) AS sTT,
		DonVi.iID_DonVi,
		DonVi.iID_MaDonVi,
		DonVi.sTenDonVi,
		DonVi.iKhoi
	into  tblDonVi
	from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;


	--- Lấy danh sách mục lục ngân sách
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
		BH_DM_MucLucNganSach.bHangCha
		into tblMucLucNganSach
		from BH_DM_MucLucNganSach 
		where   iNamLamViec = @INamLamViec  and (sLNS  in ('9010001', '9020002'))


	--- hiển thị mục lục ngân sách theo đơn vị
	select  
		case when tblMucLucNganSach.sLNS = '9010001' then N'     Khối dự toán' else N'     Khối hạch toán' end sTenDonVi,
		tblMucLucNganSach.sLNS,
		tblDonVi.iID_DonVi,
		tblDonVi.iID_MaDonVi,
		tblDonVi.iKhoi
	into donvi_MLNS
	from tblMucLucNganSach cross join tblDonVi 
	where tblMucLucNganSach.iID_MLNS_Cha is null

	---Lấy thông tin quyết toán chi tiết 
	select 
		tbl_qtc.iKhoi,
		tbl_qtc.iID_MaDonVi,
		tbl_qtc.sLNS,
		tbl_qtc.sL,
		tbl_qtc.sK,
		tbl_qtc.sM,
		case when tbl_qtc.sM = 1 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapOmDau,
		case when tbl_qtc.sM = 2 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapThaiSan,
		case when tbl_qtc.sM = 3 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapTaiNanNN,
		case when tbl_qtc.sM = 4 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapHuuTri,
		case when tbl_qtc.sM = 5 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapPhucVien,
		case when tbl_qtc.sM = 6 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapXuatNgu,
		case when tbl_qtc.sM = 7 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapThoiViec,
		case when tbl_qtc.sM = 8 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapTuTuat
	into tbl_qtcn_chitiet
	from
	(
		select 
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			tblDonVi.iKhoi,
			tblDonVi.iID_MaDonVi,
			tblMucLucNganSach.sLNS,
			tblMucLucNganSach.sL,
			tblMucLucNganSach.sK,
			tblMucLucNganSach.sM

		from BH_QTC_Nam_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		inner join tblMucLucNganSach on qtcn_ct.iID_MucLucNganSach = tblMucLucNganSach.iID_MLNS
		inner join tblDonVi on qtcn.iID_MaDonVi = tblDonVi.iID_MaDonVi
		where qtcn.iNamChungTu = @INamLamViec 
		and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by tblDonVi.iKhoi, tblDonVi.iID_MaDonVi, tblMucLucNganSach.sLNS, tblMucLucNganSach.sL,tblMucLucNganSach.sK,tblMucLucNganSach.sM
	) as tbl_qtc


	--- Lấy dữ liệu cấp nhỏ nhất - cấp 4
	select 
		null as STT,
		donvi_MLNS.sTenDonVi,
		donvi_MLNS.iID_MaDonVi,
		donvi_MLNS.iKhoi,
		donvi_MLNS.sLNS,
		sum(tbl_qtcn_chitiet.fTroCapOmDau) as fTroCapOmDau,
		sum(tbl_qtcn_chitiet.fTroCapThaiSan) as fTroCapThaiSan,
		sum(tbl_qtcn_chitiet.fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(tbl_qtcn_chitiet.fTroCapHuuTri) as fTroCapHuuTri,
		sum(tbl_qtcn_chitiet.fTroCapPhucVien) as fTroCapPhucVien,
		sum(tbl_qtcn_chitiet.fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(tbl_qtcn_chitiet.fTroCapThoiViec) as fTroCapThoiViec,
		sum(tbl_qtcn_chitiet.fTroCapTuTuat) as fTroCapTuTuat,
		4 as level,
		0 as bHangCha
	into tbl_cap4
	from donvi_MLNS 
	left join tbl_qtcn_chitiet on donvi_MLNS.iID_MaDonVi = tbl_qtcn_chitiet.iID_MaDonVi and donvi_MLNS.iKhoi = tbl_qtcn_chitiet.iKhoi
	and tbl_qtcn_chitiet.sLNS = donvi_MLNS.sLNS
	group by donvi_MLNS.sTenDonVi, donvi_MLNS.iID_MaDonVi, donvi_MLNS.iKhoi, donvi_MLNS.sLNS
	order by donvi_MLNS.iKhoi,donvi_MLNS.iID_MaDonVi

	--- Lấy dữ liệu cấp 3
	select 
		tblDonVi.sTT,
		tblDonVi.sTenDonVi ,
		tblDonVi.iID_MaDonVi,
		tblDonVi.iKhoi,
		'' as sLNS, 
		sum(tbl_cap4.fTroCapOmDau) as fTroCapOmDau,
		sum(tbl_cap4.fTroCapThaiSan) as fTroCapThaiSan,
		sum(tbl_cap4.fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(tbl_cap4.fTroCapHuuTri) as fTroCapHuuTri,
		sum(tbl_cap4.fTroCapPhucVien) as fTroCapPhucVien,
		sum(tbl_cap4.fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(tbl_cap4.fTroCapThoiViec) as fTroCapThoiViec,
		sum(tbl_cap4.fTroCapTuTuat) as fTroCapTuTuat,
		3 as level,
		0 as bHangCha
	into tbl_cap3
	from tblDonVi
	left join tbl_cap4 on tblDonVi.iID_MaDonVi = tbl_cap4.iID_MaDonVi and tblDonVi.iKhoi = tbl_cap4.iKhoi
	group by tblDonVi.sTT, tblDonVi.sTenDonVi, tblDonVi.iID_MaDonVi, tblDonVi.iKhoi

	---Lấy dữ liệu đơn vị cấp 2
	select 
		null as STT,
		case when tbl_cap4.sLNS = '9010001' then N'   +Khối dự toán' else N'   +Khối hạch toán' end sTenDonVi,
		'' as iID_MaDonVi,
		tbl_cap4.iKhoi,
		tbl_cap4.sLNS as sLNS, 
		sum(fTroCapOmDau) as fTroCapOmDau,
		sum(fTroCapThaiSan) as fTroCapThaiSan,
		sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(fTroCapHuuTri) as fTroCapHuuTri,
		sum(fTroCapPhucVien) as fTroCapPhucVien,
		sum(fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(fTroCapThoiViec) as fTroCapThoiViec,
		sum(fTroCapTuTuat) as fTroCapTuTuat,
		2 as level,
		1 as bHangCha
	into tbl_cap2
	from tbl_cap4
	group by tbl_cap4.iKhoi, tbl_cap4.sLNS


	---Lấy dữ liệu đơn vị cấp 1
	select 
		null as STT,
		case when tbl_cap4.iKhoi = 2 then N'   A.Đơn vị Dự toán' else N'   B.Đơn vị Hạch toán' end sTenDonVi,
		'' as iID_MaDonVi,
		tbl_cap4.iKhoi,
		'' as sLNS, 
		sum(fTroCapOmDau) as fTroCapOmDau,
		sum(fTroCapThaiSan) as fTroCapThaiSan,
		sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(fTroCapHuuTri) as fTroCapHuuTri,
		sum(fTroCapPhucVien) as fTroCapPhucVien,
		sum(fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(fTroCapThoiViec) as fTroCapThoiViec,
		sum(fTroCapTuTuat) as fTroCapTuTuat,
		1 as level,
		1 as bHangCha
	into tbl_cap1
	from tbl_cap4
	group by tbl_cap4.iKhoi

	---Hiển thị kết quả trả về
	select 
		sTT,
		sTenDonVi,
		iID_MaDonVi,
		iKhoi,
		sLNS,
		fTroCapOmDau,
		fTroCapThaiSan,
		fTroCapTaiNanNN,
		fTroCapHuuTri,
		fTroCapPhucVien,
		fTroCapXuatNgu,
		fTroCapThoiViec,
		fTroCapTuTuat,
		level,
		bHangCha
	into tblResult
	from
		(
		select * from tbl_cap1
		union all 
		select * from tbl_cap2
		union all 
		select * from tbl_cap3
		union all 
		select * from tbl_cap4) as tblrt
	where isnull(fTroCapOmDau,0) != 0 or isnull(fTroCapThaiSan,0) != 0 or isnull(fTroCapTaiNanNN,0) != 0 or isnull(fTroCapHuuTri,0) != 0 or isnull(fTroCapPhucVien,0) != 0
			or isnull(fTroCapXuatNgu,0) != 0 or isnull(fTroCapThoiViec,0) != 0 or isnull(fTroCapTuTuat,0) != 0
	order by iKhoi desc,iID_MaDonVi,level, sLNS
	
	----insert dòng tổng cộng
	insert into tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'          Tổng cộng' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					7 as level,
					1 as bHangCha
			from tblResult
			where tblResult.level = 1
		) as tbltongcong


	---- Insert dòng dự toán
	insert into tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'        Khối Dự toán' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					8 as level,
					1 as bHangCha
			from tblResult
			where tblResult.level = 4 and tblResult.iKhoi = 2
		) as tbldutoan



	---- Insert dòng Hạch toán
	insert into tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'        Khối Hạch toán' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					9 as level,
					1 as bHangCha
			from tblResult
			where tblResult.level = 4 and tblResult.iKhoi = 1
		) as tbldutoan


	select  * from tblResult order by iKhoi desc,iID_MaDonVi,level, sLNS


	drop table tblDonVi;
	drop table  tblMucLucNganSach;
	drop table donvi_MLNS;
	drop table tbl_qtcn_chitiet;
	drop table tbl_cap4;
	drop table tbl_cap3;
	drop table tbl_cap2;
	drop table tbl_cap1;
	drop table tblResult;
end
GO

