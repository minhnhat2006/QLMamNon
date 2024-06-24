ALTER
 ALGORITHM = MERGE
DEFINER=`root`@`localhost` 
 SQL SECURITY DEFINER
 VIEW `viewbangthutien`
 AS select `qlmamnon`.`bangthutien`.`BangThuTienId` AS `BangThuTienId`,`qlmamnon`.`bangthutien`.`HocSinhId` AS `HocSinhId`,`qlmamnon`.`bangthutien`.`LopId` AS `LopId`,`qlmamnon`.`bangthutien`.`SXThangTruoc` AS `SXThangTruoc`,`qlmamnon`.`bangthutien`.`SoTienSXThangTruoc` AS `SoTienSXThangTruoc`,`qlmamnon`.`bangthutien`.`AnSangThangTruoc` AS `AnSangThangTruoc`,`qlmamnon`.`bangthutien`.`SoTienAnSangThangTruoc` AS `SoTienAnSangThangTruoc`,`qlmamnon`.`bangthutien`.`SoTienAnSangThangNay` AS `SoTienAnSangThangNay`,`qlmamnon`.`bangthutien`.`AnToiThangTruoc` AS `AnToiThangTruoc`,`qlmamnon`.`bangthutien`.`SoTienAnToiThangTruoc` AS `SoTienAnToiThangTruoc`,`qlmamnon`.`bangthutien`.`SoTienAnToiThangNay` AS `SoTienAnToiThangNay`,`qlmamnon`.`bangthutien`.`SoTienNangKhieu` AS `SoTienNangKhieu`,`qlmamnon`.`bangthutien`.`SoTienTruyThu` AS `SoTienTruyThu`,`qlmamnon`.`bangthutien`.`SoTienDoDung` AS `SoTienDoDung`,`qlmamnon`.`bangthutien`.`SoTienDieuHoa` AS `SoTienDieuHoa`,`qlmamnon`.`bangthutien`.`NgayTinh` AS `NgayTinh`,`qlmamnon`.`bangthutien`.`STT` AS `STT`,`qlmamnon`.`bangthutien`.`IsDeleted` AS `IsDeleted`,`qlmamnon`.`bangthutien`.`DateCreated` AS `DateCreated`,100000000000000 AS `SoTienAnSangConLai`,100000000000000 AS `SoTienAnToiConLai`,100000000000000 AS `ThanhTien`,100000000000000 AS `TienAnSua`,100000000000000 AS `TienSua`,100000000000000 AS `PhuPhi`,100000000000000 AS `BanTru`,100000000000000 AS `HocPhi`,100000000000000 AS `KhoanThuChinh`,'' AS `HoTen`,NULL AS `NgayNopLan1`,100000000000000 AS `SoTienNopLan1`,NULL AS `NgayNopLan2`,100000000000000 AS `SoTienNopLan2`,`qlmamnon`.`bangthutien`.`GhiChu` AS `GhiChu`,' ' AS `Lop`,' ' AS `SoBienLai`,' ' AS `TongThu`,100000000000000 AS `TienAn` from `qlmamnon`.`bangthutien` where (`qlmamnon`.`bangthutien`.`IsDeleted` = 0)



INSERT INTO `khoanthu` (`KhoanThuId`, `Ten`, `SoTien`, `IsBatBuoc`, `DateCreated`, `DateDeleted`) VALUES ('7', 'Tiền sữa', NULL, '1', '2017-03-07 14:57:23', NULL);

