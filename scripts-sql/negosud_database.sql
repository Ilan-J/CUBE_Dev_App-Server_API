CREATE DATABASE  IF NOT EXISTS `negosud` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `negosud`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: negosud
-- ------------------------------------------------------
-- Server version	8.0.32

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
-- Table structure for table `article`
--

DROP TABLE IF EXISTS `article`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `article` (
  `IDArticle` int NOT NULL AUTO_INCREMENT,
  `Reference` varchar(256) DEFAULT NULL,
  `Name` varchar(256) NOT NULL,
  `WineYear` int NOT NULL,
  `Quantity` int NOT NULL,
  `MinQuantity` int NOT NULL,
  `BuyingPrice` decimal(15,2) NOT NULL,
  `PriceTTC` decimal(15,2) NOT NULL,
  `TVA` decimal(15,2) NOT NULL,
  `Description` text,
  `ImageLink` varchar(1000) DEFAULT NULL,
  `IDSupplier` int NOT NULL,
  `IDWineFamily` int NOT NULL,
  PRIMARY KEY (`IDArticle`),
  KEY `IDSupplier` (`IDSupplier`),
  KEY `IDWineFamily` (`IDWineFamily`),
  CONSTRAINT `article_ibfk_1` FOREIGN KEY (`IDSupplier`) REFERENCES `supplier` (`IDSupplier`),
  CONSTRAINT `article_ibfk_2` FOREIGN KEY (`IDWineFamily`) REFERENCES `winefamily` (`IDWineFamily`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `article`
--

LOCK TABLES `article` WRITE;
/*!40000 ALTER TABLE `article` DISABLE KEYS */;
/*!40000 ALTER TABLE `article` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client` (
  `IDClient` int NOT NULL AUTO_INCREMENT,
  `Firstname` varchar(256) NOT NULL,
  `Lastname` varchar(256) NOT NULL,
  `Address` varchar(256) NOT NULL,
  `PostalCode` varchar(40) NOT NULL,
  `Town` varchar(256) NOT NULL,
  `Country` varchar(256) NOT NULL,
  `Email` varchar(256) NOT NULL,
  `Password` varchar(256) NOT NULL,
  PRIMARY KEY (`IDClient`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client`
--

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientcommand`
--

DROP TABLE IF EXISTS `clientcommand`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientcommand` (
  `IDClientCommand` int NOT NULL AUTO_INCREMENT,
  `CommandDate` datetime NOT NULL,
  `IDClient` int NOT NULL,
  `IDCommandStatus` int NOT NULL,
  PRIMARY KEY (`IDClientCommand`),
  KEY `IDClient` (`IDClient`),
  KEY `IDCommandStatus` (`IDCommandStatus`),
  CONSTRAINT `clientcommand_ibfk_1` FOREIGN KEY (`IDClient`) REFERENCES `client` (`IDClient`),
  CONSTRAINT `clientcommand_ibfk_2` FOREIGN KEY (`IDCommandStatus`) REFERENCES `commandstatus` (`IDCommandStatus`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientcommand`
--

LOCK TABLES `clientcommand` WRITE;
/*!40000 ALTER TABLE `clientcommand` DISABLE KEYS */;
/*!40000 ALTER TABLE `clientcommand` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientcommandlist`
--

DROP TABLE IF EXISTS `clientcommandlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientcommandlist` (
  `IDArticle` int NOT NULL,
  `IDClientCommand` int NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`IDArticle`,`IDClientCommand`),
  KEY `IDClientCommand` (`IDClientCommand`),
  CONSTRAINT `clientcommandlist_ibfk_1` FOREIGN KEY (`IDArticle`) REFERENCES `article` (`IDArticle`),
  CONSTRAINT `clientcommandlist_ibfk_2` FOREIGN KEY (`IDClientCommand`) REFERENCES `clientcommand` (`IDClientCommand`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientcommandlist`
--

LOCK TABLES `clientcommandlist` WRITE;
/*!40000 ALTER TABLE `clientcommandlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `clientcommandlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `commandstatus`
--

DROP TABLE IF EXISTS `commandstatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commandstatus` (
  `IDCommandStatus` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`IDCommandStatus`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commandstatus`
--

LOCK TABLES `commandstatus` WRITE;
/*!40000 ALTER TABLE `commandstatus` DISABLE KEYS */;
INSERT INTO `commandstatus` VALUES (1,'CLOS'),(2,'EN COURS');
/*!40000 ALTER TABLE `commandstatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `commandtype`
--

DROP TABLE IF EXISTS `commandtype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commandtype` (
  `IDCommandType` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`IDCommandType`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commandtype`
--

LOCK TABLES `commandtype` WRITE;
/*!40000 ALTER TABLE `commandtype` DISABLE KEYS */;
INSERT INTO `commandtype` VALUES (1,'MANUEL'),(2,'AUTOMATIQUE');
/*!40000 ALTER TABLE `commandtype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `datautils`
--

DROP TABLE IF EXISTS `datautils`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `datautils` (
  `DataKey` varchar(256) NOT NULL,
  `DataValue` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`DataKey`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `datautils`
--

LOCK TABLES `datautils` WRITE;
/*!40000 ALTER TABLE `datautils` DISABLE KEYS */;
INSERT INTO `datautils` VALUES ('defaultmargin','30,0'),('defaulttva','20'),('password',''),('username','negosud');
/*!40000 ALTER TABLE `datautils` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplier`
--

DROP TABLE IF EXISTS `supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supplier` (
  `IDSupplier` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) NOT NULL,
  `Address` varchar(256) NOT NULL,
  `PostalCode` varchar(40) NOT NULL,
  `Town` varchar(256) NOT NULL,
  `Country` varchar(256) NOT NULL,
  `Email` varchar(256) NOT NULL,
  PRIMARY KEY (`IDSupplier`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplier`
--

LOCK TABLES `supplier` WRITE;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `suppliercommand`
--

DROP TABLE IF EXISTS `suppliercommand`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suppliercommand` (
  `IDSupplierCommand` varchar(10) NOT NULL,
  `TransportCost` decimal(15,2) NOT NULL,
  `CommandDate` datetime NOT NULL,
  `TotalCost` decimal(15,2) NOT NULL,
  `IDSupplier` int NOT NULL,
  `IDCommandStatus` int NOT NULL,
  `IDCommandType` int NOT NULL,
  PRIMARY KEY (`IDSupplierCommand`),
  KEY `IDSupplier` (`IDSupplier`),
  KEY `IDCommandStatus` (`IDCommandStatus`),
  KEY `IDCommandType` (`IDCommandType`),
  CONSTRAINT `suppliercommand_ibfk_1` FOREIGN KEY (`IDSupplier`) REFERENCES `supplier` (`IDSupplier`),
  CONSTRAINT `suppliercommand_ibfk_2` FOREIGN KEY (`IDCommandStatus`) REFERENCES `commandstatus` (`IDCommandStatus`),
  CONSTRAINT `suppliercommand_ibfk_3` FOREIGN KEY (`IDCommandType`) REFERENCES `commandtype` (`IDCommandType`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `suppliercommand`
--

LOCK TABLES `suppliercommand` WRITE;
/*!40000 ALTER TABLE `suppliercommand` DISABLE KEYS */;
/*!40000 ALTER TABLE `suppliercommand` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `suppliercommandlist`
--

DROP TABLE IF EXISTS `suppliercommandlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suppliercommandlist` (
  `IDArticle` int NOT NULL,
  `IDSupplierCommand` varchar(10) NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`IDArticle`,`IDSupplierCommand`),
  KEY `IDSupplierCommand` (`IDSupplierCommand`),
  CONSTRAINT `suppliercommandlist_ibfk_1` FOREIGN KEY (`IDArticle`) REFERENCES `article` (`IDArticle`),
  CONSTRAINT `suppliercommandlist_ibfk_2` FOREIGN KEY (`IDSupplierCommand`) REFERENCES `suppliercommand` (`IDSupplierCommand`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `suppliercommandlist`
--

LOCK TABLES `suppliercommandlist` WRITE;
/*!40000 ALTER TABLE `suppliercommandlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `suppliercommandlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `winefamily`
--

DROP TABLE IF EXISTS `winefamily`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `winefamily` (
  `IDWineFamily` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`IDWineFamily`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `winefamily`
--

LOCK TABLES `winefamily` WRITE;
/*!40000 ALTER TABLE `winefamily` DISABLE KEYS */;
INSERT INTO `winefamily` VALUES (4,'Armagnac'),(2,'Blanc'),(3,'Rosé'),(1,'Rouge');
/*!40000 ALTER TABLE `winefamily` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-02-01 21:12:14
