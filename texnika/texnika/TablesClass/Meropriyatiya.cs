using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace texnika
{
    class Meropriyatiya
    {
        static public DataTable merop = new DataTable();

        /// <summary>
        /// Вывод мероприятий
        /// </summary>
        static public bool View()
        {
            try
            {
                DBconnect.myCommand.CommandText = @"SELECT idMerop, Name_merop,
Data_Merop, Vremya, Kabinet FROM meropriyatiya order by Data_Merop DESC;";
                merop.Clear();
                DBconnect.myAdapter.Fill(merop);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе мероприятий!");
                return false;
            }
        }

        static public void View(string data)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idMerop, Name_merop,
Data_Merop, Vremya, Kabinet FROM meropriyatiya
where Data_Merop = '{data}' order by Vremya ;";
                merop.Clear();
                DBconnect.myAdapter.Fill(merop);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе мероприятий!");
            }
        }

        /// <summary>
        /// Добавление нового мероприятия
        /// </summary>
        /// <param name="name">название</param>
        /// <param name="data">дата</param>
        /// <param name="vremya">время</param>
        /// <param name="kabinet">кабинет</param>
        static public bool Add(string name, string data, string vremya, string kabinet)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Insert into meropriyatiya 
values(null, '{name}','{data}','{vremya}','{kabinet}');";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Мероприятие успешно добавлено");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении мероприятия!");
                return false;
            }
        }

        /// <summary>
        /// Удаление мероприятия
        /// </summary>
        /// <param name="id">id мероприятия</param>
        static public void Delete(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT nom_zapici 
FROM texnika_in_merop where idMerop = '{id}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Есть информация о технике на этом мероприятии!");
                    return;
                }
                DBconnect.myCommand.CommandText = $@"Delete from meropriyatiya
where idMerop = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Мероприятие успешно удалено");
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении мероприятия!");
            }
        }

        /// <summary>
        /// Изменения информации о мероприятии
        /// </summary>
        /// <param name="id">id мероприятия</param>
        /// <param name="name">название</param>
        /// <param name="data">дата</param>
        /// <param name="vremya">время</param>
        /// <param name="kabinet">кабинет</param>
        static public bool Edit(string id, string name, string data, string vremya, string kabinet)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Update meropriyatiya
set Name_merop = '{name}', Data_Merop = '{data}', Vremya = '{vremya}',
Kabinet = '{kabinet}' where idMerop = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Информация о мероприятии успешно изменена");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении информации о мероприятии!");
                return false;
            }
        }

        static public DataTable meropPoTexnike = new DataTable();
        /// <summary>
        /// Вывод мероприятий по технике
        /// </summary>
        /// <param name="idTex"></param>
        static public void ViewMeropPoTexnike(string idTex)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT meropriyatiya.idMerop,
Name_merop, Data_Merop, Vremya, Kabinet
FROM meropriyatiya
join texnika_in_merop on meropriyatiya.idMerop = texnika_in_merop.idMerop
where idTex = '{idTex}' order by Data_Merop";
                meropPoTexnike.Clear();
                DBconnect.myAdapter.Fill(meropPoTexnike);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе мероприятий!");
            }
        }
    }
}
