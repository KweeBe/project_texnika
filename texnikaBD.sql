-- MySQL dump 10.13  Distrib 8.0.26, for Win64 (x86_64)
--
-- Host: localhost    Database: texnika
-- ------------------------------------------------------
-- Server version	8.0.26

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
-- Table structure for table `akt_spisanie`
--

DROP TABLE IF EXISTS `akt_spisanie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `akt_spisanie` (
  `idAkta` int NOT NULL AUTO_INCREMENT,
  `Data_Akta` date DEFAULT NULL,
  PRIMARY KEY (`idAkta`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `akt_spisanie`
--

LOCK TABLES `akt_spisanie` WRITE;
/*!40000 ALTER TABLE `akt_spisanie` DISABLE KEYS */;
INSERT INTO `akt_spisanie` VALUES (6,'2022-04-10'),(8,'2022-04-12'),(9,'2022-04-22'),(10,'2022-05-22');
/*!40000 ALTER TABLE `akt_spisanie` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dogovors`
--

DROP TABLE IF EXISTS `dogovors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dogovors` (
  `idDogovora` int NOT NULL AUTO_INCREMENT,
  `idPostav` int DEFAULT NULL,
  `dataZakl` date DEFAULT NULL,
  `dataNachala` date DEFAULT NULL,
  `dataOkonchaniya` date DEFAULT NULL,
  PRIMARY KEY (`idDogovora`),
  KEY `fk_postav_idx` (`idPostav`),
  CONSTRAINT `fk_postav` FOREIGN KEY (`idPostav`) REFERENCES `postavshik` (`idPostav`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dogovors`
--

LOCK TABLES `dogovors` WRITE;
/*!40000 ALTER TABLE `dogovors` DISABLE KEYS */;
INSERT INTO `dogovors` VALUES (1,1,'2009-05-10','2009-05-20','2012-05-30'),(2,3,'2008-05-10','2008-05-12','2010-05-10'),(3,3,'2011-04-01','2011-04-10','2013-10-12'),(7,3,'2014-03-02','2014-03-09','2015-05-20'),(8,3,'2016-03-04','2016-03-20','2018-03-24'),(10,7,'2016-02-01','2016-02-14','2018-04-20'),(11,7,'2019-02-06','2019-02-14','2021-02-14'),(12,7,'2022-04-10','2022-04-13','2024-10-28'),(13,3,'2020-03-28','2020-04-01','2022-06-12'),(15,8,'2020-01-30','2020-02-01','2021-01-31'),(16,8,'2021-10-08','2021-11-01','2022-10-31'),(17,1,'2014-03-07','2014-03-17','2018-03-18'),(18,1,'2019-08-21','2019-09-02','2022-05-18');
/*!40000 ALTER TABLE `dogovors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `meropriyatiya`
--

DROP TABLE IF EXISTS `meropriyatiya`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `meropriyatiya` (
  `idMerop` int NOT NULL AUTO_INCREMENT,
  `Name_merop` varchar(200) DEFAULT NULL,
  `Data_Merop` date DEFAULT NULL,
  `Vremya` time DEFAULT NULL,
  `Kabinet` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`idMerop`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `meropriyatiya`
--

LOCK TABLES `meropriyatiya` WRITE;
/*!40000 ALTER TABLE `meropriyatiya` DISABLE KEYS */;
INSERT INTO `meropriyatiya` VALUES (12,'Конференции','2022-04-01','12:00:00','412'),(13,'«Школьная лига РОСНАНО»','2022-04-06','11:20:00','220'),(14,'«КРОНА JUNIOR»','2022-04-09','12:00:00','431'),(15,'Первый открытый корпоративный чемпионат JuniorMasters','2018-11-19','14:00:00','312'),(16,'Международный природоведческая игра-конкурс «Астра»','2020-10-12','10:30:00','241'),(17,'Региональный конкурс социальных проектов «Умная, Молодая, Креативная Арктика» (У.М.К.А.)','2020-10-15','14:00:00','325'),(18,'«КРОНА JUNIOR»','2021-05-20','12:00:00','231'),(19,'Конференции','2020-11-01','14:00:00','312'),(20,'Конференции','2021-09-01','12:30:00','421'),(21,'Конференции','2021-05-01','12:30:00','412'),(23,'Конференция','2022-05-22','11:00:00','312'),(24,'Конференция','2022-06-09','14:00:00','412');
/*!40000 ALTER TABLE `meropriyatiya` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `postavka`
--

DROP TABLE IF EXISTS `postavka`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `postavka` (
  `NomerZap` int NOT NULL AUTO_INCREMENT,
  `idPostav` int DEFAULT NULL,
  `data_postavki` date DEFAULT NULL,
  PRIMARY KEY (`NomerZap`),
  KEY `fk_id_postav_idx` (`idPostav`),
  CONSTRAINT `fk_id_postav` FOREIGN KEY (`idPostav`) REFERENCES `postavshik` (`idPostav`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `postavka`
--

LOCK TABLES `postavka` WRITE;
/*!40000 ALTER TABLE `postavka` DISABLE KEYS */;
INSERT INTO `postavka` VALUES (6,7,'2016-09-15'),(7,3,'2017-03-12'),(8,1,'2009-09-04'),(9,1,'2012-03-08'),(10,8,'2020-07-17');
/*!40000 ALTER TABLE `postavka` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `postavshik`
--

DROP TABLE IF EXISTS `postavshik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `postavshik` (
  `idPostav` int NOT NULL AUTO_INCREMENT,
  `Name_postav` varchar(100) DEFAULT NULL,
  `Adress` varchar(100) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `telefon` varchar(11) DEFAULT NULL,
  `Kontak_lico` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`idPostav`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `postavshik`
--

LOCK TABLES `postavshik` WRITE;
/*!40000 ALTER TABLE `postavshik` DISABLE KEYS */;
INSERT INTO `postavshik` VALUES (1,'Солнышко','Софьи Перовской 12','solnishko-texnika@gmail.ru','82222312412','Соколова Н. В. '),(3,'Ромашка','Челюскинцев 19','romashka-tex@gmail.com','83412451516','Козлов В. А.'),(7,'ДНС','Героев-североморцев 33А','dns@mail.ru','85162315125','Морозова К. А'),(8,'Мвидео','Проспект Ленина 32','mvideo@gmail.com','82425123124','Суханов К. П.');
/*!40000 ALTER TABLE `postavshik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `raspolojenie`
--

DROP TABLE IF EXISTS `raspolojenie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `raspolojenie` (
  `Nomer_zap` int NOT NULL AUTO_INCREMENT,
  `idTex` int DEFAULT NULL,
  `Kabinet` varchar(100) DEFAULT NULL,
  `idSotrud` int DEFAULT NULL,
  `DataYstanovki` date DEFAULT NULL,
  `DataYdaleniya` date DEFAULT NULL,
  PRIMARY KEY (`Nomer_zap`),
  KEY `fk_idTexnika_idx` (`idTex`),
  KEY `fk_idSotrud_idx` (`idSotrud`),
  CONSTRAINT `fk_idSotrud` FOREIGN KEY (`idSotrud`) REFERENCES `sotrudniks` (`idSotrud`),
  CONSTRAINT `fk_idTexnika` FOREIGN KEY (`idTex`) REFERENCES `texnika` (`idTex`)
) ENGINE=InnoDB AUTO_INCREMENT=98 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `raspolojenie`
--

LOCK TABLES `raspolojenie` WRITE;
/*!40000 ALTER TABLE `raspolojenie` DISABLE KEYS */;
INSERT INTO `raspolojenie` VALUES (35,6,'412',7,'2022-03-01','2022-04-09'),(36,10,'214',9,'2022-03-30','2022-04-08'),(37,6,'315',8,'2022-04-09',NULL),(38,9,'521',7,'2022-04-02','2022-04-07'),(39,12,'412',6,'2022-03-28','2022-04-10'),(40,12,'315',5,'2022-04-10',NULL),(41,10,'412',8,'2022-04-08','2022-04-12'),(42,9,'412',7,'2022-04-07','2022-04-12'),(43,13,'312',5,'2022-04-12',NULL),(45,15,'412',29,'2014-02-06',NULL),(46,16,'314',8,'2015-02-11','2022-04-25'),(47,9,'312',27,'2022-04-12',NULL),(48,62,'214',17,'2020-12-17',NULL),(49,27,'412',23,'2019-02-20','2022-04-12'),(50,26,'412',23,'2019-02-20',NULL),(51,28,'412',23,'2019-02-20',NULL),(52,29,'412',23,'2019-02-20',NULL),(53,30,'412',23,'2019-02-20',NULL),(54,44,'324',24,'2015-06-24','2018-06-24'),(55,40,'324',24,'2015-06-24','2018-06-24'),(56,41,'324',24,'2015-06-24','2018-06-24'),(57,42,'324',24,'2015-06-24','2018-06-24'),(58,43,'324',24,'2015-06-24','2018-06-24'),(59,45,'324',24,'2015-06-24','2018-06-24'),(60,46,'324',24,'2015-06-24','2018-06-24'),(61,47,'324',24,'2015-06-24','2018-06-24'),(62,48,'324',24,'2015-06-24','2018-06-24'),(63,40,'315',29,'2018-06-24',NULL),(64,41,'315',29,'2018-06-24','2022-05-22'),(65,42,'315',29,'2018-06-24',NULL),(66,43,'315',29,'2018-06-24','2022-04-12'),(67,44,'315',29,'2018-06-24',NULL),(68,45,'315',29,'2018-06-24','2022-04-12'),(69,46,'315',29,'2018-06-24',NULL),(70,47,'315',29,'2018-06-24',NULL),(71,48,'315',29,'2018-06-24',NULL),(72,49,'324',24,'2015-06-24','2018-06-24'),(73,50,'324',24,'2015-06-24','2018-06-24'),(74,51,'324',24,'2015-06-24','2018-06-24'),(75,52,'324',24,'2015-06-24','2018-06-24'),(76,53,'324',24,'2015-06-24','2018-06-24'),(77,54,'324',24,'2015-06-24','2018-06-24'),(78,55,'324',24,'2015-06-24','2018-06-24'),(79,56,'324',24,'2015-06-24','2018-06-24'),(80,57,'324',24,'2015-06-24','2018-06-24'),(81,50,'315',29,'2018-06-24',NULL),(82,51,'315',29,'2018-06-24','2022-04-12'),(83,52,'315',29,'2018-06-24','2022-04-12'),(84,53,'315',29,'2018-06-24','2022-04-12'),(85,54,'315',29,'2018-06-24',NULL),(86,55,'315',29,'2018-06-24',NULL),(87,56,'315',29,'2018-06-24',NULL),(88,57,'315',29,'2018-06-24',NULL),(89,20,'312',17,'2022-04-22','2022-06-08'),(91,20,'321',14,'2022-06-08','2022-06-17'),(93,18,'412',17,'2022-06-07','2022-06-08'),(97,20,'421',17,'2022-06-17',NULL);
/*!40000 ALTER TABLE `raspolojenie` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sotrudniks`
--

DROP TABLE IF EXISTS `sotrudniks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sotrudniks` (
  `idSotrud` int NOT NULL AUTO_INCREMENT,
  `Fio` varchar(100) DEFAULT NULL,
  `doljnost` varchar(100) DEFAULT NULL,
  `telefon` varchar(11) DEFAULT NULL,
  `adres` varchar(100) DEFAULT NULL,
  `statys` varchar(8) DEFAULT NULL,
  PRIMARY KEY (`idSotrud`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sotrudniks`
--

LOCK TABLES `sotrudniks` WRITE;
/*!40000 ALTER TABLE `sotrudniks` DISABLE KEYS */;
INSERT INTO `sotrudniks` VALUES (5,'Куликов','Учитель','84125156112','Мира 34','Работает'),(6,'Комарова','Учитель','85123515612','Аскольдовцев 21','Работает'),(7,'Крылов','Бухгалтер','84135151621','Чумбарово-Лучинского 33','Работает'),(8,'Смирнов','Техник','83124125125','Карла Марска 7','Работает'),(9,'Лебедев','Главный бухгалтер','85312551612','Проспект Ленина 8','Работает'),(10,'Гусев','Учитель','85512316123','Капитана Егорова 15','Работает'),(11,'Осипова','Электроник','83123251243','Капитана Буркова 22','Работает'),(12,'Маркова','Учитель','81312451234','Беринга 34','Работает'),(13,'Мухин','Бухгалтер','83125612354','Капитана Маклакова 13','Работает'),(14,'Воронова','Учитель','82425123123','Карла Марска 25','Работает'),(15,'Маркова','Начальник общего отдела','83126647732','Кильдинская 24','Работает'),(16,'Петухов','Техник','89327512374','Кооперативная 51','Работает'),(17,'Бобров','Учитель','83125712374','Аскольдовцев 33','Работает'),(18,'Петухов','Электроник','87261364721','Марата 34','Работает'),(19,'Юдин','Учитель','83758127374','Кильдинская 41','Работает'),(20,'Фомина','Заведующий учебным отделом','89472136512','Проспект Ленина 32','Работает'),(21,'Громов','Учитель','82193158123','Капитана Егорова 21','Работает'),(22,'Котова','Бухгалтер','82392173747','Капитана Буркова 26','Работает'),(23,'Лазарева','Начальник хозяйственного отдела','83477238512','Кооперативная 32','Работает'),(24,'Павлов','Учитель','87124858214','Чапаева 31','Работает'),(25,'Егоров','Техник','88512731844','Чапаева 42','Работает'),(26,'Жуков','Учитель','83251239485','Чумбарово-Лучинского 37','Работает'),(27,'Данилова','Бухгалтер','81273571275','Капитана Маклакова 33','Работает'),(28,'Захаров','Учитель','83127475571','Капитана Егорова 34','Работает'),(29,'Крапов','Учитель','83127417254','Марата 24','Работает');
/*!40000 ALTER TABLE `sotrudniks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `texnika`
--

DROP TABLE IF EXISTS `texnika`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `texnika` (
  `idTex` int NOT NULL AUTO_INCREMENT,
  `idPostavki` int DEFAULT NULL,
  `Name_Tex` varchar(100) DEFAULT NULL,
  `idVid` int DEFAULT NULL,
  `zavodNomver` varchar(50) DEFAULT NULL,
  `InvetarNomer` varchar(50) DEFAULT NULL,
  `spisanie` varchar(3) DEFAULT NULL,
  `cena` decimal(9,2) DEFAULT NULL,
  PRIMARY KEY (`idTex`),
  KEY `fk_idVid_idx` (`idVid`),
  KEY `fk_idPostavki_idx` (`idPostavki`),
  CONSTRAINT `fk_idPostavki` FOREIGN KEY (`idPostavki`) REFERENCES `postavka` (`NomerZap`),
  CONSTRAINT `fk_idVid` FOREIGN KEY (`idVid`) REFERENCES `vid_texniki` (`idVid`)
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `texnika`
--

LOCK TABLES `texnika` WRITE;
/*!40000 ALTER TABLE `texnika` DISABLE KEYS */;
INSERT INTO `texnika` VALUES (6,6,'NEC VT 46',8,'PRN23123124','1010480971','Нет',31475.00),(7,6,'acer e5620-1a1g16',5,'NAC41412412','1010490234','Нет',27284.00),(8,6,'acer e5620-1a1g16',5,'NAC51251231','1010490238','Да',27284.00),(9,7,'ASUS',5,'NAS5123151','1010490308','Нет',30080.00),(10,7,'ASUS',5,'NAS5123122','1010490309','Да',30080.00),(11,7,'Acer A5315-1A2G12MI',5,'NAC1235125','1010490517','Да',17999.00),(12,7,'HP LaserJet 1018',6,'PrHP5123124','1010490237','Нет',3756.00),(13,6,'DEPO Neos 480MD',4,'PK31245123','1013400525','Нет',31560.00),(15,8,'HP LaserJet Pro V125ra',12,'MFYHP3124124','1013400318','Нет',8100.00),(16,8,'HP LaserJet Pro V125ra',12,'MFYHP3124132','1013400314','Нет',8100.00),(17,8,'HP LaserJet Pro V125ra',12,'MFYHP3124133','1013400323','Нет',8100.00),(18,8,'Intuos Pro 2 Large Paper Edition',18,'GRPL41241243','1013450235','Нет',43000.00),(19,8,'Intuos Pro 2 Large Paper Edition',18,'GRPL41241232','1013450221','Нет',43000.00),(20,8,'Logitech ConferenceCam BCC950 HD',19,'WEBK312321412','1013450237','Нет',18100.00),(21,8,'Logitech ConferenceCam BCC950 HD',19,'WEBK312321321','1013450238','Нет',18100.00),(22,8,'Logitech ConferenceCam BCC950 HD',19,'WEBK312321512','1013450239','Да',18100.00),(23,6,'DEPO Neos 480MD',4,'PK31245312','1013400526','Нет',31560.00),(24,6,'DEPO Neos 480MD',4,'PK31245412','1013400527','Нет',31560.00),(25,6,'DEPO Neos 480MD',4,'PK31245123','1013400528','Нет',31560.00),(26,7,'acer veriton z4820g',20,'MONB31241244','1013451155','Нет',31900.00),(27,7,'acer veriton z4820g',20,'MONB31241243','1013451156','Да',31900.00),(28,7,'acer veriton z4820g',20,'MONB31241232','1013451157','Нет',31900.00),(29,7,'acer veriton z4820g',20,'MONB31241231','1013451158','Нет',31900.00),(30,7,'acer veriton z4820g',20,'MONB31241251','1013451159','Нет',31900.00),(40,9,'DEPO Neos 288MN',4,'PK31245312','1013400486','Нет',37045.00),(41,9,'DEPO Neos 288MN',4,'PK31245512','1013400487','Да',37045.00),(42,9,'DEPO Neos 288MN',4,'PK31245621','1013400491','Нет',37045.00),(43,9,'DEPO Neos 288MN',4,'PK31245622','1013400492','Да',37045.00),(44,9,'DEPO Neos 288MN',4,'PK31245623','1013400493','Нет',37045.00),(45,9,'DEPO Neos 288MN',4,'PK31245624','1013400494','Да',37045.00),(46,9,'DEPO Neos 288MN',4,'PK31245625','1013400495','Нет',37045.00),(47,9,'DEPO Neos 288MN',4,'PK31245626','1013400496','Нет',37045.00),(48,9,'DEPO Neos 288MN',4,'PK31245627','1013400497','Нет',37045.00),(49,9,'Liyama 27 \"ProLite',17,'MONIT312311','1013450225','Нет',17407.67),(50,9,'Liyama 27 \"ProLite',17,'MONIT312313','1013450226','Нет',17407.67),(51,9,'Liyama 27 \"ProLite',17,'MONIT312314','1013450227','Да',17407.67),(52,9,'Liyama 27 \"ProLite',17,'MONIT312315','1013450228','Да',17407.67),(53,9,'Liyama 27 \"ProLite',17,'MONIT312316','1013450229','Да',17407.67),(54,9,'Liyama 27 \"ProLite',17,'MONIT312332','1013450230','Нет',17407.67),(55,9,'Liyama 27 \"ProLite',17,'MONIT312333','1013450231','Нет',17407.67),(56,9,'Liyama 27 \"ProLite',17,'MONIT312334','1013450232','Нет',17407.67),(57,9,'Liyama 27 \"ProLite',17,'MONIT312335','1013450233','Нет',17407.67),(58,10,'Canon LBP 2900 A4',14,'PRLAZ312413','1010490536','Нет',3800.00),(59,10,'Canon LBP 2900 A4',14,'PRLAZ312414','1010490537','Нет',3800.00),(60,10,'Canon LBP 2900 A4',14,'PRLAZ312415','1010490538','Нет',3800.00),(61,10,'Canon LBP 2900 A4',14,'PRLAZ312416','1010490539','Нет',3800.00),(62,10,'Powerman BlackStar 1000Plus',15,'IBP3145123','1010490631','Нет',4139.99),(63,10,'Powerman BlackStar 1000Plus',15,'IBP3145124','1010490632','Нет',4139.99),(64,10,'Powerman BlackStar 1000Plus',15,'IBP3145125','1010490633','Нет',4139.99),(65,10,'Powerman BlackStar 1000Plus',15,'IBP3145126','1010490634','Нет',4139.99),(66,10,'Powerman BlackStar 1000Plus',15,'IBP3145127','1010490635','Нет',4139.99),(67,10,'Powerman BlackStar 1000Plus',15,'IBP3145128','1010490636','Нет',4139.99),(68,10,'HP DesignJet T930 Printer',16,'SHIRPR312341','1012400154','Нет',200000.00),(69,10,'Brother HL-L2340 2375к',6,'PRIN3124124','1013400341','Нет',7800.00),(70,10,'Brother HL-L2340 2375к',6,'PRIN3124124','1013400342','Нет',7800.00),(71,10,'Brother HL-L2340 2375к',6,'PRIN3124124','1013400343','Нет',7800.00),(72,10,'Brother HL-L2340 2375к',6,'PRIN3124124','1013400344','Нет',7800.00),(73,10,'Brother HL-L2340 2375к',6,'PRIN3124124','1013400345','Нет',7800.00),(74,10,'logitech CinfCam BCC950',19,'WEBK31234125','1013451150','Нет',22500.00),(75,10,'logitech CinfCam BCC950',19,'WEBK31234125','1013451151','Нет',22500.00);
/*!40000 ALTER TABLE `texnika` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `texnika_in_akt`
--

DROP TABLE IF EXISTS `texnika_in_akt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `texnika_in_akt` (
  `nomer_zapici` int NOT NULL AUTO_INCREMENT,
  `idAkta` int DEFAULT NULL,
  `idTex` int DEFAULT NULL,
  `prichina` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`nomer_zapici`),
  KEY `fk_idAkt_idx` (`idAkta`),
  KEY `fk_id_texnika_idx` (`idTex`),
  CONSTRAINT `fk_id_texnika` FOREIGN KEY (`idTex`) REFERENCES `texnika` (`idTex`),
  CONSTRAINT `fk_idAkt` FOREIGN KEY (`idAkta`) REFERENCES `akt_spisanie` (`idAkta`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `texnika_in_akt`
--

LOCK TABLES `texnika_in_akt` WRITE;
/*!40000 ALTER TABLE `texnika_in_akt` DISABLE KEYS */;
INSERT INTO `texnika_in_akt` VALUES (20,6,11,'Неустранимая поломка'),(21,6,8,'Неустранимая поломка'),(22,8,43,'Неустранимая поломка'),(23,8,52,'Неустранимая поломка'),(24,8,53,'Неустранимая поломка'),(25,8,27,'Неустранимая поломка'),(26,8,45,'Неустранимая поломка'),(27,8,51,'Неустранимая поломка'),(28,8,10,'Неустранимая поломка'),(29,9,22,'Не пригодна к использованию'),(30,10,41,'Не пригоден к использованию');
/*!40000 ALTER TABLE `texnika_in_akt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `texnika_in_merop`
--

DROP TABLE IF EXISTS `texnika_in_merop`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `texnika_in_merop` (
  `nom_zapici` int NOT NULL AUTO_INCREMENT,
  `idMerop` int DEFAULT NULL,
  `idTex` int DEFAULT NULL,
  PRIMARY KEY (`nom_zapici`),
  KEY `fk_idMerop_idx` (`idMerop`),
  KEY `fk_id_tex_idx` (`idTex`),
  CONSTRAINT `fk_id_tex` FOREIGN KEY (`idTex`) REFERENCES `texnika` (`idTex`),
  CONSTRAINT `fk_idMerop` FOREIGN KEY (`idMerop`) REFERENCES `meropriyatiya` (`idMerop`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `texnika_in_merop`
--

LOCK TABLES `texnika_in_merop` WRITE;
/*!40000 ALTER TABLE `texnika_in_merop` DISABLE KEYS */;
INSERT INTO `texnika_in_merop` VALUES (9,14,8),(10,13,11),(11,12,7),(12,12,11),(13,14,7),(14,13,6),(15,12,6),(16,15,21),(17,15,22),(18,18,13),(19,18,23),(20,18,24),(21,18,25),(22,18,51),(23,18,50),(24,18,54),(25,18,52),(26,20,7),(27,20,10),(28,20,6),(29,20,20),(30,21,41),(31,21,56),(32,21,6),(33,21,21),(34,14,26),(35,14,28),(36,14,29),(37,19,28),(38,19,74),(39,19,6),(40,16,11),(41,17,52),(42,17,24),(43,15,26),(44,15,27),(45,15,28),(46,23,22),(47,23,74),(48,23,10),(49,23,6);
/*!40000 ALTER TABLE `texnika_in_merop` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vid_texniki`
--

DROP TABLE IF EXISTS `vid_texniki`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vid_texniki` (
  `idVid` int NOT NULL AUTO_INCREMENT,
  `Name_vid` varchar(60) DEFAULT NULL,
  PRIMARY KEY (`idVid`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vid_texniki`
--

LOCK TABLES `vid_texniki` WRITE;
/*!40000 ALTER TABLE `vid_texniki` DISABLE KEYS */;
INSERT INTO `vid_texniki` VALUES (1,'Все'),(4,'ПК'),(5,'Ноутбук'),(6,'Принтер'),(7,'Сканер'),(8,'Проектор'),(12,'МФУ'),(13,'Копировальный апарат'),(14,'Принтер лазерный'),(15,'ИПБ'),(16,'Широкоформатный принтер'),(17,'Монитор'),(18,'Графический планшет'),(19,'WEB-Камера'),(20,'Моноблок'),(21,'Объектив');
/*!40000 ALTER TABLE `vid_texniki` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-06-23 13:26:11
