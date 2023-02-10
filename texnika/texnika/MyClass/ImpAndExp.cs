using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace texnika
{
    class ImpAndExp
    {
        /// <summary>
        /// Копирования базы данных
        /// </summary>
        /// <param name="fName"></param>
        static public void Backup(string fName)
        {
            string connect  = $@"database = {DBconnect.nazvanieBD};
datasource = {DBconnect.adress};
user = {DBconnect.users};
password = {DBconnect.pass};
charset = utf8;";
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(fName);
                        MessageBox.Show("База данных скопированна");
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Восстановления базы данных
        /// </summary>
        /// <param name="fName"></param>
        static public void Restore(string fName)
        {

            string connect = $@"database = {DBconnect.nazvanieBD};
datasource = {DBconnect.adress};
user = {DBconnect.users};
password = {DBconnect.pass};
charset = utf8;";
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(fName);
                        MessageBox.Show("База данных восстановлена");
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Создание базы данных
        /// </summary>
        static public void CreateBD()
        {
            string connect = $@"datasource = {DBconnect.adress};
user = {DBconnect.users};
password = {DBconnect.pass};
charset = utf8;";
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = $@"CREATE SCHEMA IF NOT EXISTS `{DBconnect.nazvanieBD}` DEFAULT CHARACTER SET utf8 ;";
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
    }
}
