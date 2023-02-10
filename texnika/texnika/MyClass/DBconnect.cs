using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace texnika
{
    class DBconnect
    {
        static string connect = "";
        static public string nazvanieBD = "";
        static public string adress = "";
        static public string pass = "";
        static public string users = "";
        static public MySqlConnection myConnect;
        static public MySqlCommand myCommand;
        static public MySqlDataAdapter myAdapter;

        /// <summary>
        /// Подключение к бд
        /// </summary>
        /// <returns></returns>
        static public bool Connect()
        {
            try
            {
                connect = $@"database = {nazvanieBD};
datasource = {adress};
user = {users};
password = {pass};
charset = utf8;";
                myConnect = new MySqlConnection(connect);
                myConnect.Open();
                myCommand = new MySqlCommand();
                myCommand.Connection = myConnect;
                myAdapter = new MySqlDataAdapter(myCommand);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при подключении к бд!");
                return false;
            }
        }

        /// <summary>
        /// отключение от бд
        /// </summary>
        static public void CloseBd()
        {
            try
            {
                myConnect.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при отключении от бд!");
            }
        }

        /// <summary>
        /// Проверка на наличие всех таблиц в базе данных
        /// </summary>
        /// <returns></returns>
        static public int ChekTables()
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Select * from akt_spisanie";
                MySqlDataReader read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                DBconnect.myCommand.CommandText = $@"Select * from dogovors";
                read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                DBconnect.myCommand.CommandText = $@"Select * from meropriyatiya";
                read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                DBconnect.myCommand.CommandText = $@"Select * from postavka";
                read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                DBconnect.myCommand.CommandText = $@"Select * from postavshik";
                read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                DBconnect.myCommand.CommandText = $@"Select * from raspolojenie";
                read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                DBconnect.myCommand.CommandText = $@"Select * from sotrudniks";
                read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                DBconnect.myCommand.CommandText = $@"Select * from texnika";
                read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                DBconnect.myCommand.CommandText = $@"Select * from texnika_in_akt";
                read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                DBconnect.myCommand.CommandText = $@"Select * from texnika_in_merop";
                read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                DBconnect.myCommand.CommandText = $@"Select * from vid_texniki";
                read = DBconnect.myCommand.ExecuteReader();
                read.Close();

                return  0;
            }
            catch
            {
                return  1;
            }
        }

    }
}
