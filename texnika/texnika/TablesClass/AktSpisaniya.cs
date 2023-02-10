using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace texnika
{
    class AktSpisaniya
    {
        static public DataTable akt = new DataTable();

        /// <summary>
        /// Выывод актов списания
        /// </summary>
        static public bool View()
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idAkta, Data_Akta 
FROM akt_spisanie order by Data_Akta;";
                akt.Clear();
                DBconnect.myAdapter.Fill(akt);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе актов списания!");
                return false;
            }
        }

        /// <summary>
        /// Вывод актов списания за период
        /// </summary>
        /// <param name="dataOt"></param>
        /// <param name="dataDo"></param>
        static public void View(string dataOt, string dataDo)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idAkta, Data_Akta 
FROM akt_spisanie 
where Data_Akta >= '{dataOt}' and Data_Akta <= '{dataDo}'order by Data_Akta;";
                akt.Clear();
                DBconnect.myAdapter.Fill(akt);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе актов списания!");
            }
        }

        

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="data"></param>
        static public void Add(string data)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idAkta 
FROM akt_spisanie where Data_Akta = '{data}'";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("На сегодня акт уже существует!");
                    return;
                }

                DBconnect.myCommand.CommandText = $@"Insert into akt_spisanie 
values(null, '{data}');";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Договор успешно добавлен");
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении нового акта!");
            }
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="id"></param>
        static public void Delete(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT nomer_zapici
FROM texnika_in_akt where idAkta = '{id}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("В данном акте есть списанная техника!");
                    return;
                }

                DBconnect.myCommand.CommandText = $@"Delete from akt_spisanie
where idAkta = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении акта списания!");
            }
        }

        /// <summary>
        /// Проверка статуса акта списания
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public bool Statys(string id, string data)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idAkta 
FROM akt_spisanie
where idAkta = '{id}' and Data_Akta = '{data}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при просмотре статуса акта!");
                return false;
            }
        }
    }
}
