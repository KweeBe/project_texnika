using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace texnika
{
    class Postavshik
    {
        static public DataTable postav = new DataTable();

        /// <summary>
        /// Вывод поставщиков
        /// </summary>
        static public bool View(string name)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idPostav, Name_postav, 
Adress, email, telefon, Kontak_lico  
FROM postavshik where Name_postav like '%{name}%'
order by Name_postav;";
                postav.Clear();
                DBconnect.myAdapter.Fill(postav);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе поставщиков!");
                return false;
            }
        }

        /// <summary>
        /// Добавление нового поставщика
        /// </summary>
        /// <param name="name">название</param>
        /// <param name="adress">адрес</param>
        /// <param name="email">почта</param>
        /// <param name="telefon">телфон</param>
        /// <param name="kontaktLico">контактное лицо</param>
        static public bool Add(string name, string adress, string email, string telefon, string kontaktLico)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Select idSotrud 
from  sotrudniks where telefon = '{telefon}'";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Такой телефон есть у другого сотрудника!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"Select idPostav
from  postavshik where telefon = '{telefon}'";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Такой телефон есть у поставщиков!");
                    return false;
                }

                DBconnect.myCommand.CommandText = $@"Insert into postavshik 
values(null, '{name}', '{adress}', '{email}', '{telefon}', '{kontaktLico}');";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Поставщик успешно добавлен");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении поставщика!");
                return false;
            }
        }

        /// <summary>
        /// Удаление информации о поставщике
        /// </summary>
        /// <param name="id">id поставщика</param>
        static public void Delete(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idDogovora 
FROM dogovors where idPostav = '{id}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("С данным поставщиком есть договор!");
                    return;
                }
                DBconnect.myCommand.CommandText = $@"SELECT NomerZap 
FROM postavka where idPostav = '{id}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("С данным поставщиком есть поставка!");
                    return;
                }
                DBconnect.myCommand.CommandText = $@"Delete from postavshik
where idPostav = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Поставщик успешно удален");
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении вида техники!");
            }
        }

        /// <summary>
        /// Изменение информации о поставщике
        /// </summary>
        /// <param name="id">id поставщика</param>
        /// <param name="name">название</param>
        /// <param name="adress">адрес</param>
        /// <param name="email">почта</param>
        /// <param name="telefon">телфон</param>
        /// <param name="kontaktLico">контактное лицо</param>
        static public bool Edit(string id, string name, string adress, 
            string email, string telefon, string kontaktLico, string telefonOld)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Select idSotrud 
from  sotrudniks where telefon = '{telefon}'";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Такой телефон есть у другого сотрудника!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"Select idPostav
from  postavshik where telefon = '{telefon}'";
                if (DBconnect.myCommand.ExecuteScalar() != null && telefon != telefonOld)
                {
                    MessageBox.Show("Такой телефон есть у поставщиков!");
                    return false;
                }

                DBconnect.myCommand.CommandText = $@"Update postavshik
set Name_postav = '{name}', Adress = '{adress}', email = '{email}', telefon = '{telefon}', 
Kontak_lico = '{kontaktLico}' where idPostav = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Информация о поставщике успешно изменена");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении информации о поставщике!");
                return false;
            }
        }

        static public DataTable combobox = new DataTable();

        /// <summary>
        /// Вывод поставщиков в комбобокс
        /// </summary>
        static public void ViewCombobox(string data)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT *
FROM dogovors, postavshik
where dogovors.idPostav = postavshik.idPostav
and dataOkonchaniya > '{data}';";
                combobox.Clear();
                DBconnect.myAdapter.Fill(combobox);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе поставщиков!");
            }
        }
    }
}
