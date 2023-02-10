using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace texnika
{
    class Sotrudniks
    {
        static public DataTable sotrud = new DataTable();

        /// <summary>
        /// Вывод сотрудников
        /// </summary>
        static public bool View(string fio, string stat)
        {
            try
            {
                string stroka = "";
                if(stat != "Все")
                {
                    stroka = $"and statys = '{stat}'";
                }

                DBconnect.myCommand.CommandText = $@"SELECT idSotrud, Fio, doljnost, 
telefon, adres, statys FROM sotrudniks 
where Fio like '%{fio}%' {stroka} order by Fio;";
                sotrud.Clear();
                DBconnect.myAdapter.Fill(sotrud);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе сотрудников!");
                return false;
            }
        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="fio"></param>
        /// <param name="doljnost"></param>
        /// <param name="telefon"></param>
        /// <param name="adres"></param>
        /// <returns></returns>
        static public bool Add(string fio, string doljnost, string telefon, string adres)
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

                DBconnect.myCommand.CommandText = $@"Insert into sotrudniks 
values(null, '{fio}', '{doljnost}', '{telefon}', '{adres}','Работает');";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Сотрудник успешно добавлен");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении сотрудника!");
                return false;
            }
        }

        /// <summary>
        /// Увольнение
        /// </summary>
        /// <param name="id"></param>
        static public void Yvolnenie(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Update sotrudniks
set statys = 'Уволен' where idSotrud = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Сотрудник успешно уволен");
            }
            catch
            {
                MessageBox.Show("Ошибка при уволнении сотрудника!");
            }
        }

        /// <summary>
        /// Возврат
        /// </summary>
        /// <param name="id"></param>
        static public void Vozvrat(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Update sotrudniks
set statys = 'Работает' where idSotrud = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Сотрудник успешно возвращен на работу");
            }
            catch
            {
                MessageBox.Show("Ошибка при возвращении сотрудника на работу!");
            }
        }

        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fio"></param>
        /// <param name="doljnost"></param>
        /// <param name="telefon"></param>
        /// <param name="adres"></param>
        /// <returns></returns>
        static public bool Edit(string id, string fio, string doljnost, 
            string telefon, string adres, string telefonOld)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Select idSotrud 
from  sotrudniks where telefon = '{telefon}'";
                if (DBconnect.myCommand.ExecuteScalar() != null && telefon != telefonOld)
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

                DBconnect.myCommand.CommandText = $@"Update sotrudniks
set Fio = '{fio}', doljnost = '{doljnost}',
telefon = '{telefon}', adres = '{adres}'
where idSotrud = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Информация о сотруднике успешно изменена");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении информации о сотруднике!");
                return false;
            }
        }

        static public DataTable combobox = new DataTable();

        /// <summary>
        /// Вывод сотрудников в комбобокс
        /// </summary>
        static public void ViewCombobox()
        {
            try
            {
                DBconnect.myCommand.CommandText = @"SELECT idSotrud, 
concat(Fio, ' (', telefon,')') as fio
FROM sotrudniks where statys = 'Работает' order by fio;";
                combobox.Clear();
                DBconnect.myAdapter.Fill(combobox);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе сотрудников!");
            }
        }
    }
}
