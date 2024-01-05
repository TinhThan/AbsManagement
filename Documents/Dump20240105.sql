-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: absmanagement
-- ------------------------------------------------------
-- Server version	8.1.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bangquangcao`
--

DROP TABLE IF EXISTS `bangquangcao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bangquangcao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdDiemDatQuangCao` int NOT NULL,
  `IdLoaiBangQuangCao` int NOT NULL,
  `KichThuoc` varchar(200) NOT NULL,
  `DanhSachHinhAnh` longtext NOT NULL,
  `NgayHetHan` datetime NOT NULL,
  `IdTinhTrang` varchar(50) NOT NULL,
  `NgayBatDau` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_BangQuangCao_DiemDatQuangCao` (`IdDiemDatQuangCao`),
  KEY `FK_BangQuangCao_LoaiBangQuangCao` (`IdLoaiBangQuangCao`),
  CONSTRAINT `FK_BangQuangCao_DiemDatQuangCao` FOREIGN KEY (`IdDiemDatQuangCao`) REFERENCES `diemdatquangcao` (`Id`),
  CONSTRAINT `FK_BangQuangCao_LoaiBangQuangCao` FOREIGN KEY (`IdLoaiBangQuangCao`) REFERENCES `loaibangquangcao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bangquangcao`
--

LOCK TABLES `bangquangcao` WRITE;
/*!40000 ALTER TABLE `bangquangcao` DISABLE KEYS */;
REPLACE  IGNORE INTO `bangquangcao` (`Id`, `IdDiemDatQuangCao`, `IdLoaiBangQuangCao`, `KichThuoc`, `DanhSachHinhAnh`, `NgayHetHan`, `IdTinhTrang`, `NgayBatDau`) VALUES (2,1008,3,'10mx30cm','[\"bien-quang-cao-dep-01 (1).jpg\",\"download (1).jpg\"]','2024-12-26 00:00:00','HoanThanh','2024-01-02 07:06:08'),(3,1,2,'50cmx20cm','[]','2023-11-11 00:00:00','HoanThanh','2023-11-11 00:00:00');
/*!40000 ALTER TABLE `bangquangcao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `baocaovipham`
--

DROP TABLE IF EXISTS `baocaovipham`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `baocaovipham` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdCanBoXuLy` int DEFAULT NULL,
  `IdDiemDatQuangCao` int DEFAULT NULL,
  `IdBangQuangCao` int DEFAULT NULL,
  `HoTen` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Email` varchar(200) NOT NULL,
  `SoDienThoai` varchar(50) NOT NULL,
  `IdHinhThucBaoCao` int NOT NULL,
  `NoiDungXuLy` longtext,
  `NoiDung` longtext NOT NULL,
  `ViTri` varchar(50) NOT NULL,
  `DanhSachHinhAnh` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `IdTinhTrang` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `CreateDate` datetime NOT NULL,
  `DiaChi` varchar(2000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Phuong` varchar(2000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Quan` varchar(2000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `ApproveDate` datetime DEFAULT NULL,
  `DanhSachHinhAnhXuLy` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_BaoCaoViPham_BangQuangCao` (`IdBangQuangCao`),
  KEY `FK_BaoCaoViPham_CanBo` (`IdCanBoXuLy`),
  KEY `FK_BaoCaoViPham_DiemDatQuangCao` (`IdDiemDatQuangCao`),
  KEY `FK_BaoCaoViPham_HinhThucBaoCao` (`IdHinhThucBaoCao`),
  CONSTRAINT `baocaovipham_ibfk_1` FOREIGN KEY (`IdCanBoXuLy`) REFERENCES `canbo` (`Id`),
  CONSTRAINT `baocaovipham_ibfk_2` FOREIGN KEY (`IdDiemDatQuangCao`) REFERENCES `diemdatquangcao` (`Id`),
  CONSTRAINT `baocaovipham_ibfk_3` FOREIGN KEY (`IdBangQuangCao`) REFERENCES `bangquangcao` (`Id`),
  CONSTRAINT `baocaovipham_ibfk_4` FOREIGN KEY (`IdHinhThucBaoCao`) REFERENCES `hinhthucbaocao` (`Id`),
  CONSTRAINT `FK_BaoCaoViPham_BangQuangCao` FOREIGN KEY (`IdBangQuangCao`) REFERENCES `bangquangcao` (`Id`),
  CONSTRAINT `FK_BaoCaoViPham_CanBo` FOREIGN KEY (`IdCanBoXuLy`) REFERENCES `canbo` (`Id`),
  CONSTRAINT `FK_BaoCaoViPham_DiemDatQuangCao` FOREIGN KEY (`IdDiemDatQuangCao`) REFERENCES `diemdatquangcao` (`Id`),
  CONSTRAINT `FK_BaoCaoViPham_HinhThucBaoCao` FOREIGN KEY (`IdHinhThucBaoCao`) REFERENCES `hinhthucbaocao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `baocaovipham`
--

LOCK TABLES `baocaovipham` WRITE;
/*!40000 ALTER TABLE `baocaovipham` DISABLE KEYS */;
REPLACE  IGNORE INTO `baocaovipham` (`Id`, `IdCanBoXuLy`, `IdDiemDatQuangCao`, `IdBangQuangCao`, `HoTen`, `Email`, `SoDienThoai`, `IdHinhThucBaoCao`, `NoiDungXuLy`, `NoiDung`, `ViTri`, `DanhSachHinhAnh`, `IdTinhTrang`, `CreateDate`, `DiaChi`, `Phuong`, `Quan`, `ApproveDate`, `DanhSachHinhAnhXuLy`) VALUES (1,1,NULL,NULL,'Thân Văn Đức Tính','ductinh280499@gmail.com','9281928192',1,'Update','<p>Nhập nội dung tại đây</p>','[10.762900,106.684563]','[\"vnf-quang-cao-la-gi.jpg\"]','DangXuLy','2023-12-24 21:47:37',NULL,NULL,NULL,'2024-01-03 11:41:25','[]'),(2,NULL,NULL,NULL,'Thân văn đức tính','ductinh280499@gmail.com','0921929129',2,NULL,'<p>Nhập nội dung tại đ&acirc;y</p>','[0.0,10.744517]','[\"Buổi 4 và 5 28_2_2023.jpg\"]','ChuaXuLy','2023-12-27 15:51:10','Fiori Coffee, 125 Đường Số 17, Quận 7, Ho Chi Minh City, 756700, Vietnam','72711','72900',NULL,NULL),(3,NULL,NULL,NULL,'Thân văn đức tính','ductinh280499@gmail.com','0921929129',4,NULL,'<p>Nhập nội dung tại đ&acirc;y</p>\n<p>&nbsp;</p>\n<p>Dạng pro</p>','[106.710484,10.747047]','[\"Buổi 4 và 5 28_2_2023.jpg\"]','ChuaXuLy','2023-12-27 15:56:13','Daina\'s Collection, NO 83, Ho Chi Minh City, 756800, Vietnam update','72711','72900',NULL,NULL),(6,NULL,1,NULL,'Thân văn đức tính','ductinh280499@gmail.com','09219291291',5,NULL,'<p>Nhập nội dung tại đ&acirc;y</p>','[106.68246164917946,10.76432400792406]','[]','ChuaXuLy','2023-12-27 16:18:04','Daina\'s Collection, NO 83, Ho Chi Minh City, 756800, Vietnam update','71014','71000',NULL,NULL);
/*!40000 ALTER TABLE `baocaovipham` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `canbo`
--

DROP TABLE IF EXISTS `canbo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `canbo` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(200) NOT NULL,
  `HoTen` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `SoDienThoai` char(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `NgaySinh` datetime NOT NULL,
  `Role` varchar(50) NOT NULL,
  `MatKhau` longtext NOT NULL,
  `RefreshToken` longtext,
  `RefreshTokenExpiryTime` datetime DEFAULT NULL,
  `NoiCongTac` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `NgayCapNhat` datetime DEFAULT NULL,
  `EmailVerified` int DEFAULT NULL,
  `PasswordResetOTP` varchar(1000) DEFAULT NULL,
  `PasswordResetOTPExpiration` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `canbo`
--

LOCK TABLES `canbo` WRITE;
/*!40000 ALTER TABLE `canbo` DISABLE KEYS */;
REPLACE  IGNORE INTO `canbo` (`Id`, `Email`, `HoTen`, `SoDienThoai`, `NgaySinh`, `Role`, `MatKhau`, `RefreshToken`, `RefreshTokenExpiryTime`, `NoiCongTac`, `NgayCapNhat`, `EmailVerified`, `PasswordResetOTP`, `PasswordResetOTPExpiration`) VALUES (1,'ductinh@gmail.com','Đức Tính','093203921111','2023-04-20 00:00:00','CanBoSo','x+tceDNc0SCdiaVEdvSmII/z/Wmwyk2p10MdaZFTEtE=','bt7vCAlLLpYo/Mru1auN/XwpAci92TcnG2DbxxWOY+15AoZ2G5+LfQbcG/jDpkknNVdSJEpK/0lSjmlcxE6zaQ==','2024-01-10 04:39:44','[]',NULL,NULL,NULL,NULL),(2,'22424018@student.hcmus.edu.vn','Thân Văn Đức Tính','9281928192','2023-11-01 15:14:09','CanBoPhuong','x+tceDNc0SCdiaVEdvSmII/z/Wmwyk2p10MdaZFTEtE=','3GGgdRUPgwZ0UtNExUQpSd/hddMwI+K8MQ/Yx1OnjPHk/4EuQw/i/xJ+nuexeiAihNx+Jgv+jrnhZRPoOnXo8g==','2024-01-09 08:26:43','[\"71200\",\"71210\"]',NULL,NULL,NULL,NULL),(3,'ductinh280499@gmail.com','Đức Tính','98219281','2023-11-09 15:46:58','CanBoQuan','x+tceDNc0SCdiaVEdvSmII/z/Wmwyk2p10MdaZFTEtE=','7BeJzSboQF4p5KYe2AN8jNn8/sUbNW7rhDlY54XkVhuoeymrG+ir/ie2XjRCZXn7+OTiz4amlzuWnanoam/oVA==','2024-01-10 02:46:02','[72700]',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `canbo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `diemdatquangcao`
--

DROP TABLE IF EXISTS `diemdatquangcao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `diemdatquangcao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `DiaChi` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Phuong` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Quan` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ViTri` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `IdLoaiViTri` int NOT NULL,
  `IdHinhThucQuangCao` int NOT NULL,
  `DanhSachHinhAnh` varchar(200) NOT NULL,
  `IdTinhTrang` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_DiemDatQuangCao_HinhThucQuangCao` (`IdHinhThucQuangCao`),
  KEY `FK_DiemDatQuangCao_LoaiViTri` (`IdLoaiViTri`),
  CONSTRAINT `diemdatquangcao_ibfk_1` FOREIGN KEY (`IdLoaiViTri`) REFERENCES `loaivitri` (`Id`),
  CONSTRAINT `diemdatquangcao_ibfk_2` FOREIGN KEY (`IdHinhThucQuangCao`) REFERENCES `hinhthucquangcao` (`Id`),
  CONSTRAINT `FK_DiemDatQuangCao_HinhThucQuangCao` FOREIGN KEY (`IdHinhThucQuangCao`) REFERENCES `hinhthucquangcao` (`Id`),
  CONSTRAINT `FK_DiemDatQuangCao_LoaiViTri` FOREIGN KEY (`IdLoaiViTri`) REFERENCES `loaivitri` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1013 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `diemdatquangcao`
--

LOCK TABLES `diemdatquangcao` WRITE;
/*!40000 ALTER TABLE `diemdatquangcao` DISABLE KEYS */;
REPLACE  IGNORE INTO `diemdatquangcao` (`Id`, `DiaChi`, `Phuong`, `Quan`, `ViTri`, `IdLoaiViTri`, `IdHinhThucQuangCao`, `DanhSachHinhAnh`, `IdTinhTrang`) VALUES (1,'Photocopy Tan Cuu Long, 217 Nguyễn Văn Cừ St., Dist. 5, Ho Chi Minh City, 748400, Vietnam','72711','72700','[106.681216,10.762520]',1,1,'[]','ChuaQuyHoach'),(2,'Texas Chicken - Nguyễn Văn Cừ, 217B Nguyễn Văn Cừ, Quận 5, Ho Chi Minh City, 711600, Vietnam','71014','71000','[106.684083,10.762015]',5,2,'[\"bien-quang-cao-dep-01.jpg\",\"demo_billboard_step-2_logoembosse_180509_dayview.jpg\"]','DaQuyHoach'),(1004,'Đại Học Sư Phạm TPHCM, 280 An Duong Vuong St., District 5, Ho Chi Minh City, 748400, Vietnam','72711','72700','[106.682158,10.761175]',4,2,'[\"bien-quang-cao-dep-01 (1).jpg\",\"demo_billboard_step-2_logoembosse_180509_dayview.jpg\",\"115116732.jpg\"]','DaQuyHoach'),(1005,'The Coffee Bean & Tea Leaf, Now Zone Shopping Mall, 235 Nguyễn Văn Cừ St., District 1, Ho Chi Minh City, 748400, Vietnam','72711','72700','[106.682027,10.763213]',3,2,'[\"115116732.jpg\",\"A(5) logo(1).jpg\"]','ChoDuyet'),(1006,'Chùa Long An, 106 Nguyễn Văn Cừ, Quận 1, Ho Chi Minh City, 711600, Vietnam','71014','71000','[106.685180,10.758631]',4,3,'[\"bang-quang-cao-ngoai-troi-9.jpg\"]','DaQuyHoach'),(1007,'Pool @ Orchids Saigon Hotel, Ho Chi Minh City, 722400, Vietnam','72407','72400','[106.695769,10.780958]',2,3,'[\"115116732.jpg\"]','DaQuyHoach'),(1008,'Urban Station Nguyen Trai, Ho Chi Minh City, 748200, Vietnam','72710','72700','[106.683179,10.759500]',3,4,'[]','DaQuyHoach'),(1009,'Vitamin Smile, 96 Đường Đề Thám, Ho Chi Minh City, 712100, Vietnam','71013','71000','[106.695423,10.764446]',4,4,'[]','DaQuyHoach'),(1010,'Đại Học Sư Phạm TPHCM, 280 An Duong Vuong St., District 5, Ho Chi Minh City, 748400, Vietnam','72711','72700','[106.681194,10.762003]',3,2,'[\"download.jpg\"]','DaQuyHoach'),(1011,'The Faceshop, 94 Ng Trai Q. 5, Ho Chi Minh City, 748200, Vietnam','72709','72700','[106.679631,10.757721]',5,4,'[\"115116732.jpg\",\"A(5) logo(1).jpg\"]','DaQuyHoach'),(1012,'Hủ Tíu Nam Vang Tường Phát, Trần Phú, P4, Q5, Ho Chi Minh City, 748300, Vietnam','72711','72700','[106.675683,10.759744]',4,3,'[]','DaQuyHoach');
/*!40000 ALTER TABLE `diemdatquangcao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hinhthucbaocao`
--

DROP TABLE IF EXISTS `hinhthucbaocao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hinhthucbaocao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Ma` varchar(50) NOT NULL,
  `Ten` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hinhthucbaocao`
--

LOCK TABLES `hinhthucbaocao` WRITE;
/*!40000 ALTER TABLE `hinhthucbaocao` DISABLE KEYS */;
REPLACE  IGNORE INTO `hinhthucbaocao` (`Id`, `Ma`, `Ten`) VALUES (1,'DangKyNoiDung','Đăng ký nội dung'),(2,'DongGopYKien','Đóng góp ý kiến'),(3,'GiaiDapThacMac','Giải đáp thắc mắc'),(4,'ToGiacSaiPham 1','Tố giác sai phạm'),(5,'ToGiacSaiPham','Tố giác sai phạm');
/*!40000 ALTER TABLE `hinhthucbaocao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hinhthucquangcao`
--

DROP TABLE IF EXISTS `hinhthucquangcao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hinhthucquangcao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Ma` varchar(50) NOT NULL,
  `Ten` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hinhthucquangcao`
--

LOCK TABLES `hinhthucquangcao` WRITE;
/*!40000 ALTER TABLE `hinhthucquangcao` DISABLE KEYS */;
REPLACE  IGNORE INTO `hinhthucquangcao` (`Id`, `Ma`, `Ten`) VALUES (1,'CoDongChinhTri','Cổ động chính trị'),(2,'QuangCaoThuongMai','Quảng cáo thương mại'),(3,'XaHoiHoa','Xã hội hóa'),(4,'CONGNGHE update','Công nghệ update');
/*!40000 ALTER TABLE `hinhthucquangcao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `loaibangquangcao`
--

DROP TABLE IF EXISTS `loaibangquangcao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `loaibangquangcao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Ma` varchar(50) NOT NULL,
  `Ten` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `loaibangquangcao`
--

LOCK TABLES `loaibangquangcao` WRITE;
/*!40000 ALTER TABLE `loaibangquangcao` DISABLE KEYS */;
REPLACE  IGNORE INTO `loaibangquangcao` (`Id`, `Ma`, `Ten`) VALUES (1,'CongChao','Cổng chào 2'),(2,'DienTuOpTuong','Màn hình điện tử ốp tường'),(3,'Hiflex','Trụ bảng hiflex'),(4,'HiflexOpTuong','Bảng hiflex ốp tường'),(5,'Led','Trụ màn hình điện tử LED'),(6,'Pano','Trụ/Cụm pano update'),(7,'TruHopDen','Trụ hộp đèn'),(8,'TrungTamThuongMai','Trung tâm thương mại'),(9,'TruTreoBangRonDoc','Trụ treo băng rôn dọc'),(10,'TruTreoBangRonNang','Trụ treo băng rôn ngang'),(11,'Panno2','Trụ pano 02');
/*!40000 ALTER TABLE `loaibangquangcao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `loaivitri`
--

DROP TABLE IF EXISTS `loaivitri`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `loaivitri` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Ma` varchar(50) NOT NULL,
  `Ten` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `loaivitri`
--

LOCK TABLES `loaivitri` WRITE;
/*!40000 ALTER TABLE `loaivitri` DISABLE KEYS */;
REPLACE  IGNORE INTO `loaivitri` (`Id`, `Ma`, `Ten`) VALUES (1,'CayXang','Cây xăng'),(2,'Cho','Chợ update'),(3,'DatCong','Đất công/Công viên/Hành lang an toàn giao thông'),(4,'DatTuNhan','Đất tư nhân/Nhà ở riêng lẻ'),(5,'NhaChoXeBuyt','Nhà chờ xe buýt'),(6,'TrungTamThuongMai','Trung tâm thương mại'),(7,'CHOXANG','Chợ xăng');
/*!40000 ALTER TABLE `loaivitri` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `phieucapphepquangcao`
--

DROP TABLE IF EXISTS `phieucapphepquangcao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `phieucapphepquangcao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdDiemDatQuangCao` int NOT NULL,
  `IdLoaiBangQuangCao` int NOT NULL,
  `KichThuoc` varchar(200) NOT NULL,
  `DanhSachHinhAnh` text NOT NULL,
  `NgayHetHan` datetime NOT NULL,
  `IdTinhTrang` varchar(50) NOT NULL,
  `NgayBatDau` datetime NOT NULL,
  `TenCongTy` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Email` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `SoDienThoai` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `DiaChi` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `Fk_PhieuCapPhepQuangCao_DiemDatQuangCao` (`IdDiemDatQuangCao`),
  KEY `Fk_PhieuCapPhepQuangCao_LoaiBangQuangCao` (`IdLoaiBangQuangCao`),
  CONSTRAINT `Fk_PhieuCapPhepQuangCao_DiemDatQuangCao` FOREIGN KEY (`IdDiemDatQuangCao`) REFERENCES `diemdatquangcao` (`Id`),
  CONSTRAINT `Fk_PhieuCapPhepQuangCao_LoaiBangQuangCao` FOREIGN KEY (`IdLoaiBangQuangCao`) REFERENCES `loaibangquangcao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `phieucapphepquangcao`
--

LOCK TABLES `phieucapphepquangcao` WRITE;
/*!40000 ALTER TABLE `phieucapphepquangcao` DISABLE KEYS */;
REPLACE  IGNORE INTO `phieucapphepquangcao` (`Id`, `IdDiemDatQuangCao`, `IdLoaiBangQuangCao`, `KichThuoc`, `DanhSachHinhAnh`, `NgayHetHan`, `IdTinhTrang`, `NgayBatDau`, `TenCongTy`, `Email`, `SoDienThoai`, `DiaChi`) VALUES (1,1009,2,'10mx30cm','[\"bien-quang-cao-dep-01 (1).jpg\",\"bang-hieu-quang-cao-chuyen-hinh-rolling-bang-hieu-quang-cao-hieu-qua.jpg\"]','2024-01-09 08:59:21','ChoDuyet','2024-01-03 08:59:19','KHTN','ductinh280499@gmail.com','0921929129','Quận 12');
/*!40000 ALTER TABLE `phieucapphepquangcao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `phieucapphepsuaquangcao`
--

DROP TABLE IF EXISTS `phieucapphepsuaquangcao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `phieucapphepsuaquangcao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdDiemDat` int DEFAULT NULL,
  `IdBangQuangCao` int DEFAULT NULL,
  `NoiDung` varchar(500) NOT NULL,
  `NgayGui` datetime NOT NULL,
  `TinhTrang` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `Fk_PhieuCapPhepSuaQuangCao_BangQuangCao` (`IdBangQuangCao`),
  KEY `Fk_PhieuCapPhepSuaQuangCao_DiemDatQuangCao` (`IdDiemDat`),
  CONSTRAINT `Fk_PhieuCapPhepSuaQuangCao_BangQuangCao` FOREIGN KEY (`IdBangQuangCao`) REFERENCES `bangquangcao` (`Id`),
  CONSTRAINT `Fk_PhieuCapPhepSuaQuangCao_DiemDatQuangCao` FOREIGN KEY (`IdDiemDat`) REFERENCES `diemdatquangcao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `phieucapphepsuaquangcao`
--

LOCK TABLES `phieucapphepsuaquangcao` WRITE;
/*!40000 ALTER TABLE `phieucapphepsuaquangcao` DISABLE KEYS */;
REPLACE  IGNORE INTO `phieucapphepsuaquangcao` (`Id`, `IdDiemDat`, `IdBangQuangCao`, `NoiDung`, `NgayGui`, `TinhTrang`) VALUES (1,1004,NULL,'{\"DiaChi\":\"S.Mart - Supermarket, 240 Tran Binh Trong Str., District 5, Ho Chi Minh City, 748300, Vietnam\",\"Phuong\":\"72711\",\"Quan\":\"72700\",\"DanhSachViTri\":[106.679260,10.760889],\"IdLoaiViTri\":4,\"IdHinhThucQuangCao\":2,\"DanhSachHinhAnh\":[\"bien-quang-cao-dep-01 (1).jpg\",\"demo_billboard_step-2_logoembosse_180509_dayview.jpg\",\"115116732.jpg\"]}','2024-01-05 02:59:38','ChoDuyet'),(2,1005,NULL,'{\"DiaChi\":\"The Coffee Bean & Tea Leaf, Now Zone Shopping Mall, 235 Nguyễn Văn Cừ St., District 1, Ho Chi Minh City, 748400, Vietnam\",\"Phuong\":\"72711\",\"Quan\":\"72700\",\"DanhSachViTri\":[106.682027,10.763213],\"IdLoaiViTri\":3,\"IdHinhThucQuangCao\":4,\"DanhSachHinhAnh\":[\"115116732.jpg\",\"A(5) logo(1).jpg\"]}','2024-01-05 03:04:05','ChoDuyet');
/*!40000 ALTER TABLE `phieucapphepsuaquangcao` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-05 16:16:23
