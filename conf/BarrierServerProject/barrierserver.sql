/*
SQLyog Ultimate v8.62 
MySQL - 5.0.84-community-nt : Database - barrierserver
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`barrierserver` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `barrierserver`;

/*Table structure for table `state` */

DROP TABLE IF EXISTS `state`;

CREATE TABLE `state` (
  `barcode` tinytext NOT NULL,
  `name` tinytext NOT NULL,
  `price` mediumint(7) unsigned NOT NULL,
  `sailprice` mediumint(7) NOT NULL,
  `count` float(7,3) NOT NULL,
  `sailed` float(7,3) NOT NULL,
  `status` tinyint(1) unsigned NOT NULL,
  `date` datetime NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Data for the table `state` */

insert  into `state`(`barcode`,`name`,`price`,`sailprice`,`count`,`sailed`,`status`,`date`) values ('481006705664 ','ШОКОЛАД ДРАКОША ГОША  СЛИВОЧНЫЙ 48Г               ',4050,4050,5.000,5.000,2,'2013-03-18 22:47:30'),('481006704959 ','ШОКОЛАД ЛЮКС 20Г                                  ',2350,2000,17.000,1.000,4,'2013-03-18 22:47:30'),('481006704959 ','ШОКОЛАД ЛЮКС 20Г                                  ',2350,2350,17.000,5.000,0,'2013-03-18 22:47:30');

/*Table structure for table `tasks` */

DROP TABLE IF EXISTS `tasks`;

CREATE TABLE `tasks` (
  `id` bigint(20) unsigned NOT NULL auto_increment,
  `barcode` text NOT NULL,
  `sailprice` mediumint(7) NOT NULL,
  `group` int(2) NOT NULL,
  `text` longtext NOT NULL,
  `user_id` tinyint(4) NOT NULL,
  `priority` tinyint(4) NOT NULL,
  `date` datetime NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;

/*Data for the table `tasks` */

insert  into `tasks`(`id`,`barcode`,`sailprice`,`group`,`text`,`user_id`,`priority`,`date`) values (24,'481006705664',4050,1,'Все продано по цене очереди,необходимо прогрузить новую цену на кассу',0,2,'2013-03-18 22:47:30'),(25,'481006704959',2000,1,'Товар продается дешевле цены очерёдности!',0,4,'2013-03-18 22:47:30');

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `id` tinyint(4) NOT NULL auto_increment,
  `username` tinytext NOT NULL,
  `hash` tinytext NOT NULL,
  `group` tinyint(2) default NULL,
  `status` tinyint(1) default NULL,
  `ip` tinytext,
  `tasks_count` int(11) default NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`id`,`username`,`hash`,`group`,`status`,`ip`,`tasks_count`) values (1,'partizanes','1ed6ad31cf5f6e0d10e18f8db3f08c9f',2,0,'127.0.0.1',0),(4,'user','91ff7d7746319a2d34f93537c3e54173',1,0,'192.168.1.4',0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
