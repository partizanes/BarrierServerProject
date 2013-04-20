using MySql.Data.MySqlClient;
using System;
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
            (Application.OpenForms[0] as Update).Invoke((MethodInvoker)(delegate() { MainListBox.Items.Add("[" + DateTime.Now.ToShortDateString() + "]  " + str); }));
        }

        private void Update_Shown(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate()
            {
                MsgAdd("Приложение обновления запущено.");

                ProgressBarMainForm.PerformStep();
                
                Thread.Sleep(1000);

                MsgAdd("Версия на сервере " + GetVersionProgram() + ".");

                StartUpdate();
            }); ;
            th.Name = "Main thread start";
            th.Start();
        }

        private string GetNameProgramUpdate() { return (Config.GetParametr("NameProgramUpdate")); }

        private int GetVersionProgram()
        {
            try 
            {
                using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=" + ConnectTimeout + ";", IpCashServer, "PrioritySail", "***REMOVED***", UpdateServerBase)))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT ver FROM `version` WHERE `name` = '" + GetNameProgramUpdate() + "'", conn);

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
            catch (System.Exception ex) { Log.ExcWrite("[GetNameProgramUpdate] " + ex.Message); MsgAdd(ex.Message); return 0; }

            return 0;
        }

        private string GetSourceProgramUpdate()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=" + ConnectTimeout + ";", IpCashServer, "PrioritySail", "***REMOVED***", UpdateServerBase)))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT source FROM `version` WHERE `name` = '" + GetNameProgramUpdate() + "'", conn);

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
            catch (System.Exception ex) { Log.ExcWrite("[GetSourceProgramUpdate] " + ex.Message); MsgAdd(ex.Message); return null; }

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
                MsgAdd(ex.Message);
                Log.ExcWrite("[StartUpdate] " + ex.Message);
                Log.log_write("Произошло исключение.Записан статус обновления 0", "[StartUpdate]", "Exception");
                Config.Set("SETTINGS", "UpdateStatus", "0");
                Application.Exit();
            }
        }

        private void Downloader(string _URL, string _SaveAs)
        {
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
                MsgAdd(exc.Message);
                Log.ExcWrite("[StartUpdate] " + exc.Message);
                Log.log_write("Произошло исключение.Записан статус обновления 0", "[StartUpdate]", "Exception");
                Config.Set("SETTINGS", "UpdateStatus", "0");
                Application.Exit();
            }
            finally
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

                    Log.log_write("Делаем копию файла", "INFO", "update");
                    MsgAdd("Делаем копию файла");
                    File.Move(name, "backup_" + name);
                    Thread.Sleep(1000);

                    Log.log_write("Удаляем оригинальный файл", "INFO", "update");
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
                    Log.log_write("Запускаем обновленное приложение", "INFO", "update");
                    MsgAdd("Запускаем обновленное приложение");

                    Thread.Sleep(1000);
                    System.Diagnostics.Process.Start(name);

                    Config.Set("SETTINGS", "UpdateStatus", "1");

                    if (File.Exists("_" + name))
                    {
                        File.Delete("_" + name);
                        Log.log_write("Удаляем временный файл", "INFO", "update");
                    }

                    Log.log_write("Выходим из утилиты обновления", "INFO", "update");

                    Application.Exit();
                }
                else
                {
                    Log.ExcWrite("[Copy] Произошло исключение." );

                    //check this
                    RevertUpdate(name);
                }
            }
            catch (System.Exception exс)
            {
                Log.ExcWrite("[Copy] " + exс.Message);
                status = false;

                Config.Set("SETTINGS", "UpdateStatus", "0");
            }
            finally
            {
                if(status)
                {
                    Thread.Sleep(800);
                    Log.log_write("Успешно!", "INFO", "update");
                    ProgressBarMainForm.Value = 100;
                    Thread.Sleep(4000);
                    Application.Exit();
                }
            }
        }

        private void RevertUpdate(string name)
        {
            try
            {
                Config.Set("SETTINGS", "UpdateStatus", "1");

                Log.log_write("Возвращаем все обратно", "INFO", "update");
                File.Move("backup_" + name, name);
                Thread.Sleep(500);

                Log.log_write("Удаляем запасной файл", "INFO", "update");
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
                Log.ExcWrite("[RevertUpdate] " + exc.Message);
            }
            finally
            {
                Application.Exit();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.log_write("Выход из прогаммы обновления ", "INFO", "update");
            this.Hide();
            Application.Exit();
        }

        private void Update_Load(object sender, EventArgs e)
        {
            Log.log_write("Программа обновления запущена ", "INFO", "update");
        }
    }
}
