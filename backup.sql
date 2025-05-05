-- MySQL dump 10.13  Distrib 9.3.0, for Win64 (x86_64)
--
-- Host: localhost    Database: gestion_empresarial
-- ------------------------------------------------------
-- Server version	9.3.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `DpiCliente` varchar(20) COLLATE utf8mb4_general_ci NOT NULL,
  `NIT` varchar(15) COLLATE utf8mb4_general_ci NOT NULL,
  `Nombre` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `TipoCliente` tinyint(1) NOT NULL,
  `Direccion` text COLLATE utf8mb4_general_ci NOT NULL,
  `ComprasEnLaEmpresa` decimal(10,2) DEFAULT '0.00',
  `NumeroTelefonico` varchar(15) COLLATE utf8mb4_general_ci NOT NULL,
  `DescuentosFidelidad` decimal(5,2) DEFAULT '0.00',
  `ReembolsosSolicitados` int DEFAULT '0',
  `ReembolsosAprobados` int DEFAULT '0',
  PRIMARY KEY (`DpiCliente`),
  UNIQUE KEY `NIT` (`NIT`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comprasempresa`
--

DROP TABLE IF EXISTS `comprasempresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comprasempresa` (
  `IDFactura` int NOT NULL AUTO_INCREMENT,
  `TotalCompras` decimal(10,2) NOT NULL,
  `FechaEvaluada` date NOT NULL,
  `FechaAnterior` date DEFAULT NULL,
  `ComprasRealizadas` int DEFAULT '0',
  `PrecioCompra` decimal(10,2) NOT NULL,
  PRIMARY KEY (`IDFactura`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comprasempresa`
--

LOCK TABLES `comprasempresa` WRITE;
/*!40000 ALTER TABLE `comprasempresa` DISABLE KEYS */;
/*!40000 ALTER TABLE `comprasempresa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comprasinventario`
--

DROP TABLE IF EXISTS `comprasinventario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comprasinventario` (
  `IdCompra` int NOT NULL AUTO_INCREMENT,
  `FechaCompra` date NOT NULL,
  `IDProveedor` int NOT NULL,
  `NombreProveedor` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `Descripcion` text COLLATE utf8mb4_general_ci NOT NULL,
  `CantidadComprada` int NOT NULL,
  `PrecioCompra` decimal(10,2) NOT NULL,
  PRIMARY KEY (`IdCompra`),
  KEY `IDProveedor` (`IDProveedor`),
  CONSTRAINT `comprasinventario_ibfk_1` FOREIGN KEY (`IDProveedor`) REFERENCES `proveedor` (`IDProveedor`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comprasinventario`
--

LOCK TABLES `comprasinventario` WRITE;
/*!40000 ALTER TABLE `comprasinventario` DISABLE KEYS */;
/*!40000 ALTER TABLE `comprasinventario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `empleados`
--

DROP TABLE IF EXISTS `empleados`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `empleados` (
  `IDEmpleado` int NOT NULL AUTO_INCREMENT,
  `DPIEmpleado` varchar(20) COLLATE utf8mb4_general_ci NOT NULL,
  `TotalDeSalario` decimal(10,2) NOT NULL,
  `NumeroDeEmpleados` int DEFAULT '1',
  PRIMARY KEY (`IDEmpleado`),
  UNIQUE KEY `DPIEmpleado` (`DPIEmpleado`),
  CONSTRAINT `empleados_ibfk_1` FOREIGN KEY (`DPIEmpleado`) REFERENCES `rrhh` (`DPIEmpleado`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `empleados`
--

LOCK TABLES `empleados` WRITE;
/*!40000 ALTER TABLE `empleados` DISABLE KEYS */;
/*!40000 ALTER TABLE `empleados` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `finanzas`
--

DROP TABLE IF EXISTS `finanzas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `finanzas` (
  `IDFinanzas` int NOT NULL AUTO_INCREMENT,
  `IDGasto` int NOT NULL,
  `IDCompraEmpresa` int NOT NULL,
  `IDCompraInventario` int NOT NULL,
  `IDReembolso` int DEFAULT NULL,
  `UtilidadNeta` decimal(10,2) NOT NULL,
  `FechaEvaluada` date NOT NULL,
  `FechaAnterior` date DEFAULT NULL,
  `Balance` decimal(10,2) NOT NULL,
  `Impuestos` decimal(10,2) NOT NULL,
  `PagoDeIntereses` decimal(10,2) DEFAULT '0.00',
  `TotalReembolsos` decimal(10,2) DEFAULT '0.00',
  PRIMARY KEY (`IDFinanzas`),
  KEY `IDGasto` (`IDGasto`),
  KEY `IDCompraEmpresa` (`IDCompraEmpresa`),
  KEY `IDCompraInventario` (`IDCompraInventario`),
  KEY `IDReembolso` (`IDReembolso`),
  CONSTRAINT `finanzas_ibfk_1` FOREIGN KEY (`IDGasto`) REFERENCES `gastos` (`IDGasto`) ON DELETE CASCADE,
  CONSTRAINT `finanzas_ibfk_2` FOREIGN KEY (`IDCompraEmpresa`) REFERENCES `comprasempresa` (`IDFactura`) ON DELETE CASCADE,
  CONSTRAINT `finanzas_ibfk_3` FOREIGN KEY (`IDCompraInventario`) REFERENCES `comprasinventario` (`IdCompra`) ON DELETE CASCADE,
  CONSTRAINT `finanzas_ibfk_4` FOREIGN KEY (`IDReembolso`) REFERENCES `reembolsos` (`IDReembolso`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `finanzas`
--

LOCK TABLES `finanzas` WRITE;
/*!40000 ALTER TABLE `finanzas` DISABLE KEYS */;
/*!40000 ALTER TABLE `finanzas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `gastos`
--

DROP TABLE IF EXISTS `gastos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `gastos` (
  `IDGasto` int NOT NULL AUTO_INCREMENT,
  `IDIngreso` int NOT NULL,
  `IDProveedor` int NOT NULL,
  `IDEmpleado` int NOT NULL,
  `TotalGastos` decimal(10,2) NOT NULL,
  `FechaEvaluada` date NOT NULL,
  `FechaAnterior` date DEFAULT NULL,
  `Renta` decimal(10,2) NOT NULL,
  `TotalCompras` decimal(10,2) NOT NULL,
  `TotalSalarios` decimal(10,2) NOT NULL,
  `ServiciosBasicos` decimal(10,2) DEFAULT '0.00',
  `ServiciosExtra` decimal(10,2) DEFAULT '0.00',
  `TotalAmortizaciones` decimal(10,2) DEFAULT '0.00',
  PRIMARY KEY (`IDGasto`),
  KEY `IDIngreso` (`IDIngreso`),
  KEY `IDProveedor` (`IDProveedor`),
  KEY `IDEmpleado` (`IDEmpleado`),
  CONSTRAINT `gastos_ibfk_1` FOREIGN KEY (`IDIngreso`) REFERENCES `ingresos` (`IDIngreso`) ON DELETE CASCADE,
  CONSTRAINT `gastos_ibfk_2` FOREIGN KEY (`IDProveedor`) REFERENCES `proveedor` (`IDProveedor`) ON DELETE CASCADE,
  CONSTRAINT `gastos_ibfk_3` FOREIGN KEY (`IDEmpleado`) REFERENCES `empleados` (`IDEmpleado`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gastos`
--

LOCK TABLES `gastos` WRITE;
/*!40000 ALTER TABLE `gastos` DISABLE KEYS */;
/*!40000 ALTER TABLE `gastos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ingresos`
--

DROP TABLE IF EXISTS `ingresos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingresos` (
  `IDIngreso` int NOT NULL AUTO_INCREMENT,
  `TotalIngresos` decimal(10,2) NOT NULL,
  `FechaEvaluada` date NOT NULL,
  `FechaAnterior` date DEFAULT NULL,
  `TotalVentas` decimal(10,2) NOT NULL,
  `ImpuestosDeducidos` decimal(10,2) NOT NULL,
  `TotalServiciosOfrecidos` decimal(10,2) DEFAULT '0.00',
  `TotalReembolsosOtorgados` decimal(10,2) DEFAULT '0.00',
  PRIMARY KEY (`IDIngreso`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ingresos`
--

LOCK TABLES `ingresos` WRITE;
/*!40000 ALTER TABLE `ingresos` DISABLE KEYS */;
/*!40000 ALTER TABLE `ingresos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inventario`
--

DROP TABLE IF EXISTS `inventario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inventario` (
  `IDProducto` int NOT NULL AUTO_INCREMENT,
  `NombreProducto` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `Descripcion` text COLLATE utf8mb4_general_ci NOT NULL,
  `CantidadEnStock` int NOT NULL,
  `EspecificacionVehiculo` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Costo` decimal(10,2) NOT NULL,
  `Ganancia` decimal(10,2) NOT NULL,
  `Precio` decimal(10,2) NOT NULL,
  `Ano` int NOT NULL,
  `PoliticaReembolso` enum('no reembolsable','30 dias','60 dias','90 dias') COLLATE utf8mb4_general_ci DEFAULT '30 dias',
  PRIMARY KEY (`IDProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventario`
--

LOCK TABLES `inventario` WRITE;
/*!40000 ALTER TABLE `inventario` DISABLE KEYS */;
/*!40000 ALTER TABLE `inventario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jornada`
--

DROP TABLE IF EXISTS `jornada`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jornada` (
  `IDJornada` int NOT NULL AUTO_INCREMENT,
  `IDEmpleado` int NOT NULL,
  `Vacaciones` int DEFAULT '0',
  `DiasDeTrabajo` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `SePresento` tinyint(1) NOT NULL,
  `HorasLaborales` int NOT NULL,
  `HorasExtra` int DEFAULT '0',
  `PagoHoraExtra` decimal(10,2) DEFAULT '0.00',
  `DescuentoLlegadaTarde` decimal(10,2) DEFAULT '0.00',
  `LlegadaTardePermitida` int DEFAULT '0',
  PRIMARY KEY (`IDJornada`),
  KEY `IDEmpleado` (`IDEmpleado`),
  CONSTRAINT `jornada_ibfk_1` FOREIGN KEY (`IDEmpleado`) REFERENCES `rrhh` (`IDEmpleado`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jornada`
--

LOCK TABLES `jornada` WRITE;
/*!40000 ALTER TABLE `jornada` DISABLE KEYS */;
/*!40000 ALTER TABLE `jornada` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `modificacionesinventario`
--

DROP TABLE IF EXISTS `modificacionesinventario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `modificacionesinventario` (
  `IDModificacion` int NOT NULL AUTO_INCREMENT,
  `SeCompro` tinyint(1) NOT NULL,
  `IDCompraInventario` int NOT NULL,
  `MotivoModificacion` text COLLATE utf8mb4_general_ci NOT NULL,
  `QueCampoModifico` text COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IDModificacion`),
  KEY `IDCompraInventario` (`IDCompraInventario`),
  CONSTRAINT `modificacionesinventario_ibfk_1` FOREIGN KEY (`IDCompraInventario`) REFERENCES `inventario` (`IDProducto`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `modificacionesinventario`
--

LOCK TABLES `modificacionesinventario` WRITE;
/*!40000 ALTER TABLE `modificacionesinventario` DISABLE KEYS */;
/*!40000 ALTER TABLE `modificacionesinventario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `proveedor`
--

DROP TABLE IF EXISTS `proveedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proveedor` (
  `IDProveedor` int NOT NULL AUTO_INCREMENT,
  `NombreProveedor` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `ComprasAlProveedor` decimal(10,2) DEFAULT '0.00',
  PRIMARY KEY (`IDProveedor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `proveedor`
--

LOCK TABLES `proveedor` WRITE;
/*!40000 ALTER TABLE `proveedor` DISABLE KEYS */;
/*!40000 ALTER TABLE `proveedor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reembolsos`
--

DROP TABLE IF EXISTS `reembolsos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reembolsos` (
  `IDReembolso` int NOT NULL AUTO_INCREMENT,
  `IDVenta` int NOT NULL,
  `NITCliente` varchar(15) COLLATE utf8mb4_general_ci NOT NULL,
  `IDProducto` int NOT NULL,
  `MotivoReembolso` text COLLATE utf8mb4_general_ci NOT NULL,
  `CantidadReembolsada` int NOT NULL,
  `MontoReembolsado` decimal(10,2) NOT NULL,
  `FechaSolicitud` date NOT NULL,
  `FechaProcesamiento` date DEFAULT NULL,
  `Estado` enum('pendiente','aprobado','rechazado','completado') COLLATE utf8mb4_general_ci DEFAULT 'pendiente',
  `MetodoReembolso` enum('efectivo','credito','transferencia','cheque') COLLATE utf8mb4_general_ci NOT NULL,
  `Comentarios` text COLLATE utf8mb4_general_ci,
  `IDEmpleadoProcesador` int DEFAULT NULL,
  PRIMARY KEY (`IDReembolso`),
  KEY `IDVenta` (`IDVenta`),
  KEY `NITCliente` (`NITCliente`),
  KEY `IDProducto` (`IDProducto`),
  KEY `IDEmpleadoProcesador` (`IDEmpleadoProcesador`),
  CONSTRAINT `reembolsos_ibfk_1` FOREIGN KEY (`IDVenta`) REFERENCES `ventas` (`IDVenta`) ON DELETE CASCADE,
  CONSTRAINT `reembolsos_ibfk_2` FOREIGN KEY (`NITCliente`) REFERENCES `clientes` (`NIT`) ON DELETE CASCADE,
  CONSTRAINT `reembolsos_ibfk_3` FOREIGN KEY (`IDProducto`) REFERENCES `inventario` (`IDProducto`) ON DELETE CASCADE,
  CONSTRAINT `reembolsos_ibfk_4` FOREIGN KEY (`IDEmpleadoProcesador`) REFERENCES `empleados` (`IDEmpleado`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reembolsos`
--

LOCK TABLES `reembolsos` WRITE;
/*!40000 ALTER TABLE `reembolsos` DISABLE KEYS */;
/*!40000 ALTER TABLE `reembolsos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rrhh`
--

DROP TABLE IF EXISTS `rrhh`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rrhh` (
  `DPIEmpleado` varchar(20) COLLATE utf8mb4_general_ci NOT NULL,
  `IDEmpleado` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `FechaDeNacimiento` date NOT NULL,
  `Rol` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `CuentaBancaria` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `Usuario` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `Contraseña` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `Faltas` int DEFAULT '0',
  `Bonos` decimal(10,2) DEFAULT '0.00',
  `Salario` decimal(10,2) NOT NULL,
  PRIMARY KEY (`DPIEmpleado`),
  UNIQUE KEY `IDEmpleado` (`IDEmpleado`),
  UNIQUE KEY `Usuario` (`Usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rrhh`
--

LOCK TABLES `rrhh` WRITE;
/*!40000 ALTER TABLE `rrhh` DISABLE KEYS */;
/*!40000 ALTER TABLE `rrhh` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ventas`
--

DROP TABLE IF EXISTS `ventas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ventas` (
  `IDVenta` int NOT NULL AUTO_INCREMENT,
  `IDProducto` int NOT NULL,
  `NIT` varchar(15) COLLATE utf8mb4_general_ci NOT NULL,
  `NOFactura` int NOT NULL,
  `NombreProducto` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `CantidadLlevada` int NOT NULL,
  `FechaCompra` date NOT NULL,
  `PagoIndividual` decimal(10,2) NOT NULL,
  `PagoTotal` decimal(10,2) NOT NULL,
  `EsReembolsable` tinyint(1) DEFAULT '1',
  `Reembolsado` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`IDVenta`),
  UNIQUE KEY `NOFactura` (`NOFactura`),
  KEY `NIT` (`NIT`),
  KEY `IDProducto` (`IDProducto`),
  CONSTRAINT `ventas_ibfk_1` FOREIGN KEY (`NIT`) REFERENCES `clientes` (`NIT`) ON DELETE CASCADE,
  CONSTRAINT `ventas_ibfk_2` FOREIGN KEY (`IDProducto`) REFERENCES `inventario` (`IDProducto`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ventas`
--

LOCK TABLES `ventas` WRITE;
/*!40000 ALTER TABLE `ventas` DISABLE KEYS */;
/*!40000 ALTER TABLE `ventas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-05  9:51:04
