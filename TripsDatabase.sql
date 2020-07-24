-- Host: localhost    Database: trips
-- ----------------------------------------------------
--
-- Table structure for table `trip`
--

DROP TABLE IF EXISTS `trip`;
CREATE TABLE `trip` (
  `IDTrip` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) DEFAULT NULL,
  `Notes` varchar(800) DEFAULT NULL,
  `Rating` int(11) NOT NULL DEFAULT '1',
  `TripDate` datetime DEFAULT NULL,
  `Latitude` double DEFAULT NULL,
  `Longitude` double DEFAULT NULL,
  `ImageUrl` longtext NOT NULL,
  PRIMARY KEY (`IDTrip`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;