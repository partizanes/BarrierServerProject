/*
SQLyog Ultimate v8.62 
MySQL - 5.0.84-community-nt : Database - barrierserver2
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`barrierserver2` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `barrierserver2`;

/*Table structure for table `operations` */

DROP TABLE IF EXISTS `operations`;

CREATE TABLE `operations` (
  `id` bigint(20) unsigned NOT NULL,
  `operation` tinyint(2) unsigned NOT NULL,
  `count` float(7,3) unsigned NOT NULL,
  `price` mediumint(7) unsigned NOT NULL,
  `inactive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `operations` */

/*Table structure for table `priority` */

DROP TABLE IF EXISTS `priority`;

CREATE TABLE `priority` (
  `id` bigint(20) NOT NULL auto_increment,
  `bar` tinytext NOT NULL,
  `name` tinytext NOT NULL,
  `turn_price` mediumint(7) unsigned NOT NULL,
  `count` float(7,3) NOT NULL,
  `sailed` float(7,3) NOT NULL,
  `status` tinyint(2) NOT NULL,
  `status_text` tinytext NOT NULL,
  `current_price_ukm` mediumint(7) NOT NULL,
  `date` datetime NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8;

/*Data for the table `priority` */

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `id` tinyint(4) NOT NULL auto_increment,
  `username` tinytext NOT NULL,
  `hash` tinytext NOT NULL,
  `group` tinyint(2) default NULL,
  `online` tinyint(1) default NULL,
  `ip` tinytext,
  `tasks_count` int(11) default NULL,
  `tasks_failed` int(11) default NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`id`,`username`,`hash`,`group`,`online`,`ip`,`tasks_count`,`tasks_failed`) values (1,'partizanes','1ed6ad31cf5f6e0d10e18f8db3f08c9f',2,1,'127.0.0.1',0,0),(4,'user','91ff7d7746319a2d34f93537c3e54173',1,0,'127.0.0.1',0,0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
