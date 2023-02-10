using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace texnika
{
    class Postavka
    {
        static public DataTable postav = new DataTable();

        /// <summary>
        /// Вывод поставок
        /// </summary>
        static public bool View()
        {
            try
            {
                DBconnect.myCommand.CommandText = @"SELECT NomerZap, 
postavka.idPostav, Name_postav, data_postavki 
FROM postavka, postavshik 
where postavka.idPostav = postavshik.idPostav order by data_postavki;";
                postav.Clear();
                DBconnect.myAdapter.Fill(postav);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе поставок!");
                return false;
            }
        }

        static public bool View(string dataOt, string dataDo)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT NomerZap, 
postavka.idPostav, Name_postav, data_postavki 
FROM postavka, postavshik 
where postavka.idPostav = postavshik.idPostav 
and data_postavki >= '{dataOt}' and data_postavki <= '{dataDo}' order by data_postavki;";
                postav.Clear();
                DBconnect.myAdapter.Fill(postav);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе поставок!");
                return false;
            }
        }

        /// <summary>
        /// Добавление поставки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public bool Add(string id, string data)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Insert into postavka 
values(null, '{id}', '{data}');";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Поставка успешно добавлена");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении поставки!");
                return false;
            }
        }

        /// <summary>
        /// Изменение поставки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postav"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public bool Edit(string id, string postav, string data)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Update postavka
set idPostav = '{postav}', data_postavki = '{data}'
where NomerZap = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Информация о поставке успешно изменена");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении информации о поставке!");
                return false;
            }
        }

        /// <summary>
        /// Удаление поставки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public bool Delete(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idTex
FROM texnika where idPostavki = '{id}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("В выбранной поставке есть техника!");
                    return false;
                }

                DBconnect.myCommand.CommandText = $@"Delete from postavka
where NomerZap = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Информация о поставке успешно удалена");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении информации о поставке!");
                return false;
            }
        }
    }
}
