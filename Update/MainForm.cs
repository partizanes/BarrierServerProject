using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Update
{
    public partial class Update : Form
    {
        private string IpCashServer = Config.GetParametr("IpCashServer");
        private string UpdateServerBase = Config.GetParametr("UpdateServerBase");
        private string NameProgramUpdate = Config.GetParametr("NameProgramUpdate");
        private int CommandTimeout = int.Parse(Config.GetParametr("CommandTimeout"));
        private int ConnectTimeout = int.Parse(Config.GetParametr("ConnectTimeout"));
        public static bool debug = bool.Parse(Config.GetParametr("debug"));
        private bool status = true;

        public Update()
        {
            InitializeComponent();
        }

        private void MsgAdd(string str)
        {
            Log.log_write(str, "[MsgAdd]");
            (Application.OpenForms[0] as Update).Invoke((MethodInvoker)(delegate() { MainListBox.Items.Add("[" + DateTime.Now.ToShortTimeString() + "]  " + str); }));
        }

        private void Update_Shown(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate()
            {
                MsgAdd("Приложение обновления запущено.");

                ProgressBarMainForm.PerformStep();
                
                Thread.Sleep(1000);

                int version = GetVersionProgram();

                MsgAdd("Версия на сервере " + version + " .");

                if (version == 0)
                {
                    MsgAdd("Внимание не удалось получить версию обновления на сервере.Записан статус 0.Выход через 10 секунд...");

                    Config.Set("SETTINGS", "UpdateStatus", "0");

                    Thread.Sleep(5000);

                    MsgAdd("Выход через 5 секунд...");

                    Application.Exit();
                }

                StartUpdate();
            }); ;
            th.Name = "Main thread start";
            th.Start();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Проверка запросов SQL на уязвимости безопасности")]
        private int GetVersionProgram()
        {
            try 
            {
                using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=" + ConnectTimeout + ";", IpCashServer, "PrioritySail", "", UpdateServerBase)))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT ver FROM `version` WHERE `name` = '" + NameProgramUpdate + "'", conn);

                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader()) 
                    {
                        if (dr == null)
                            throw new Exception("datareader is null");

                        if (!dr.HasRows)
                            throw new Exception("datareader is not have row");

                        while (dr.Read()) 
                        { 
                            return(dr.GetInt32(0)); 
                        } 
                    }
                }
            }
            catch (System.Exception ex) { 
                MsgAdd("[GetVersionProgram] " + ex.Message);
                this.BackColor = Color.Red; return 0;
            }

            return 0;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Проверка запросов SQL на уязвимости безопасности")]
        private string GetSourceProgramUpdate()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=" + ConnectTimeout + ";", IpCashServer, "PrioritySail", "", UpdateServerBase)))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT source FROM `version` WHERE `name` = '" + NameProgramUpdate + "'", conn);

                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null)
                            throw new Exception("datareader is null");

                        if (!dr.HasRows)
                            throw new Exception("datareader is not have row");

                        while (dr.Read())
                        {
                            string p = dr.GetString(0);
                            MsgAdd("Получен путь обновления программы: " + p);
                            return (p);
                        }
                    }
                }
            }
            catch (System.Exception ex) { Log.ExcWrite("[GetSourceProgramUpdate] " + ex.Message); MsgAdd(ex.Message); this.BackColor = Color.Red; return null; }

            return null;
        }

        private void StartUpdate()
        {
            ProgressBarMainForm.PerformStep();
            Thread.Sleep(1000);

            try
            {
                string source = GetSourceProgramUpdate();
                
                if (source != null)
                    Downloader(source, "%TEMP%");
                else
                    throw new Exception("ВНИМАНИЕ! Путь к обновлению не задан на сервере");
            }
            catch (System.Exception ex)
            {
                this.BackColor = Color.Red;

                MsgAdd("[StartUpdate] " + ex.Message);
                Config.Set("SETTINGS", "UpdateStatus", "0");
                Application.Exit();
            }
        }

        private void Downloader(string _URL, string _SaveAs)
        {
            bool s = true;

            MsgAdd("Запускаю скачивание...");

            ProgressBarMainForm.PerformStep();
            Thread.Sleep(1000);

            WebClient myWebClient = new WebClient();
            string downloadFileName = System.IO.Path.GetFileName(_URL);

            try
            {
                myWebClient.DownloadFile(_URL, "_" + downloadFileName);

                while (myWebClient.IsBusy)
                {
                    Application.DoEvents();
                }
            }
            catch (System.Exception exc)
            {
                s = false;

                this.BackColor = Color.Red;

                MsgAdd(exc.Message);
                MsgAdd("[Downloader] Произошло исключение.Записан статус обновления 0");

                Log.ExcWrite("[Downloader] " + exc.Message);

                Config.Set("SETTINGS", "UpdateStatus", "0");

                MsgAdd("Неудачно!");

                Thread.Sleep(10000);
                Application.Exit();
            }

            if (s)
            {
                ProgressBarMainForm.PerformStep();
                Copy(downloadFileName);
            }
        }

        private void Copy(string name)
        {
            try
            {
                MsgAdd("Копирую...");
                ProgressBarMainForm.PerformStep();

                while (File.Exists(name))
                {
                    if (File.Exists("backup_" + name))
                    {
                        File.Delete("backup_" + name);
                        Thread.Sleep(1000);
                    }

                    Log.log_write("Делаем копию файла", "INFO");
                    MsgAdd("Делаем копию файла");
                    File.Move(name, "backup_" + name);
                    Thread.Sleep(1000);

                    Log.log_write("Удаляем оригинальный файл", "INFO");
                    MsgAdd("Удаляем оригинальный файл");
                    File.Delete(name);
                    Thread.Sleep(1000);
                }

                ProgressBarMainForm.PerformStep();

                if (File.Exists("_" + name))
                {
                    File.Copy("_" + name, name);
                    Thread.Sleep(1000);
                }

                if (File.Exists(name))
                {
                    ProgressBarMainForm.Value = 99;
                    Log.log_write("Запускаем обновленное приложение", "INFO");
                    MsgAdd("Запускаем обновленное приложение");

                    Thread.Sleep(1000);
                    System.Diagnostics.Process.Start(name);

                    Config.Set("SETTINGS", "UpdateStatus", "1");

                    if (File.Exists("_" + name))
                    {
                        File.Delete("_" + name);
                        Log.log_write("Удаляем временный файл", "INFO");
                    }

                    Log.log_write("Выходим из утилиты обновления", "INFO");

                    Application.Exit();
                }
                else
                {
                    this.BackColor = Color.Red;

                    Log.ExcWrite("[Copy] Произошло исключение." );

                    //check this
                    RevertUpdate(name);
                }
            }
            catch (System.Exception exс)
            {
                this.BackColor = Color.Red;

                Log.ExcWrite("[Copy] " + exс.Message);
                status = false;

                Config.Set("SETTINGS", "UpdateStatus", "0");
            }
            finally
            {
                if(status)
                {
                    Thread.Sleep(800);
                    Log.log_write("Успешно!", "INFO");
                    ProgressBarMainForm.Value = 100;
                    Thread.Sleep(4000);
                    Application.Exit();
                }
                else
                {
                    Thread.Sleep(5000);
                }
            }
        }

        private void RevertUpdate(string name)
        {
            try
            {
                Config.Set("SETTINGS", "UpdateStatus", "1");

                Log.log_write("Возвращаем все обратно", "INFO");
                File.Move("backup_" + name, name);
                Thread.Sleep(500);

                Log.log_write("Удаляем запасной файл", "INFO");
                File.Delete("backup" + name);
                Thread.Sleep(500);

                if (File.Exists(name))
                {
                    Thread.Sleep(500);
                    System.Diagnostics.Process.Start(name);
                    Config.Set("SETTINGS", "UpdateStatus", "1");
                }
            }
            catch (System.Exception exc)
            {
                this.BackColor = Color.Red;
                Log.ExcWrite("[RevertUpdate] " + exc.Message);
            }
            finally
            {
                Application.Exit();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.log_write("Выход из прогаммы обновления ", "INFO");
            this.Hide();
            Application.Exit();
        }
    }
}
