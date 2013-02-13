using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PrioritySales
{
    class Mysql
    {
        private MySqlCommand cmd;
        private MySqlConnection serverConn;
        private string connStr;
        private IPAddress ip = IPAddress.Parse(Config.GetParametr("IpCashServer"));
        private string BdName = (Config.GetParametr("BdName"));

        public Boolean ExecuteNonQuery(string str)
        {
            if (ip.ToString().Length == 0 || BdName.Length == 0)
            {
                MessageBox.Show("В файле конфигурации config.ini в папке с программой отсутствует параметры IpCashServer или BdName!\nУточните данные параметры и повторите запрос\nТекущий запрос отменен!");
                return false;
            }

            connStr = string.Format("server={0};uid={1};pwd={2};database={3};", ip, "pricechecker", "***REMOVED***", BdName);

            serverConn = new MySqlConnection(connStr);

            try
            {
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { Packages.mf.LabelInfo.Text = "                                                              Подключаюсь..."; }));

                Application.DoEvents();

                serverConn.Open();

                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { Packages.mf.LabelInfo.Text = ""; }));

                cmd = new MySqlCommand(str, serverConn);

                cmd.ExecuteNonQuery();

                return true;
            }

            catch (TimeoutException ex)
            {
                //TODO User msg about this
                Log.log_write("Сервер Mysql не ответил вовремя,будет произведена попытка переподключения \n Текст исключения: " + ex.Message, "Exception", "Mysql_Exception_Timeout");

                try
                {
                    while (serverConn.State != ConnectionState.Open)
                        serverConn.Open();
                }
                catch { }

                if(serverConn.State == ConnectionState.Open)
                    cmd.ExecuteNonQuery();

                return true;
            }

            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1042:
                        MessageBox.Show("Внимание!Не удалось подключиться к кассовому серверу изза отсутствия ответа от Mysql.Возможно сервер недоступен или отключен.Проверьте соединение и работоспособность сервера и повторите попытку.Так же возможно неверно указан адрес кассового сервера в файле конфигурации!");
                        return false;
                    case 1045:
                        MessageBox.Show("Внимание!Не удалось подключиться к кассовому серверу по причине неверного имени и пароля!Для устранения данной ошибки нужно создать учетную запись на Mysql сервере!Также возможно неверно указан адрес кассового сервера в файле конфигурации!");
                        return false;
                }
                return false;
            }

            catch (System.Exception ex)
            {
                Log.log_write(ex.Message, "Exception", "Exception");
                return false;
            }
            finally
            {
                if (serverConn.State == ConnectionState.Open)
                    serverConn.Close();
            }
        }

        public MySqlDataReader ExecuteReader(string str)
        {
            MySqlDataReader reader = null;
            //TODO read info about connect from config

            if (ip.ToString().Length == 0 || BdName.Length == 0)
            {
                MessageBox.Show("В файле конфигурации config.ini в папке с программой отсутствует параметры IpCashServer или BdName!\nУточните данные параметры и повторите запрос.Текущий запрос отменен!");
                return null;
            }

            connStr = string.Format("server={0};uid={1};pwd={2};database={3};", ip, "pricechecker", "***REMOVED***", BdName);

            serverConn = new MySqlConnection(connStr);

            try
            {
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { Packages.mf.LabelInfo.Text = "                                                              Подключаюсь..."; }));

                Application.DoEvents();

                serverConn.Open();

                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { Packages.mf.LabelInfo.Text = ""; }));

                cmd = new MySqlCommand(str, serverConn);

                reader = cmd.ExecuteReader();

                return reader;
            }

            catch (TimeoutException ex)
            {
                //TODO User msg about this
                //CHECK THIS EXCEPTION XP
                Log.log_write("Сервер Mysql не ответил вовремя,будет произведена попытка переподключения \n Текст исключения: " + ex.Message, "Exception", "Mysql_Exception_Timeout");
                try
                {
                    while (serverConn.State != ConnectionState.Open)
                        serverConn.Open();
                }
                catch { }

                if (serverConn.State == ConnectionState.Open)
                    reader = cmd.ExecuteReader();

                return reader;
            }

            catch(MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1042:
                        MessageBox.Show("Внимание!Не удалось подключиться к кассовому серверу изза отсутствия ответа от Mysql.Возможно сервер недоступен или отключен.Проверьте соединение и работоспособность сервера и повторите попытку.Так же возможно неверно указан адрес кассового сервера в файле конфигурации!");
                        return null;
                    case 1045:
                        MessageBox.Show("Внимание!Не удалось подключиться к кассовому серверу по причине неверного имени и пароля!Для устранения данной ошибки нужно создать учетную запись на Mysql сервере!Также возможно неверно указан адрес кассового сервера в файле конфигурации!");
                        return null;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                Log.log_write(ex.Message, "Exception", "Exception");
                return null;
            }
        }
    }
}
