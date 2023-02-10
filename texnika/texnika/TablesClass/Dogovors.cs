using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace texnika
{
    class Dogovors
    {
        static public DataTable dogovor = new DataTable();

        /// <summary>
        /// Вывод договоров выбранного поставщика
        /// </summary>
        /// <param name="id"></param>
        static public bool View(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idDogovora, dataZakl,
dataNachala, dataOkonchaniya  FROM dogovors where idPostav = '{id}';";
                dogovor.Clear();
                DBconnect.myAdapter.Fill(dogovor);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе договоров!");
                return false;
            }
        }

        /// <summary>
        /// Проверка действия договора
        /// </summary>
        /// <param name="idPostav"></param>
        /// <returns></returns>
        static public bool DogovorStatys(string idPostav)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idDogovora FROM dogovors
where idPostav = '{idPostav}' and dataOkonchaniya > now()";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Договор с данной организацией еще деиствует!");
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                MessageBox.Show("Ошибка при проверки договоров!");
                return false;
            }
        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="idPostav"></param>
        /// <param name="dataZakl"></param>
        /// <param name="dataNachala"></param>
        /// <param name="dataOkon"></param>
        /// <returns></returns>
        static public bool Add(string idPostav, string dataZakl, 
            string dataNachala, string dataOkon)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idDogovora 
FROM dogovors 
where dataOkonchaniya > '{dataNachala}' and idPostav = '{idPostav}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Договор в этом время уже имееться!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"Insert into dogovors 
values(null, '{idPostav}','{dataZakl}','{dataNachala}','{dataOkon}');";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Договор успешно добавлен");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении договора!");
                return false;
            }
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public bool Delete(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Delete from dogovors
where idDogovora = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Договор успешно удален");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении договора!");
                return false;
            }
        }

        /// <summary>
        /// Расторжение
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public bool Rastorgnyt(string id,string data)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT max(dataOkonchaniya) 
FROM texnika.dogovors where idPostav = '{id}';";
                object result = Convert.ToDateTime(DBconnect.myCommand.ExecuteScalar()).ToString("yyyy-MM-dd");
                DBconnect.myCommand.CommandText = $@"Update dogovors
set dataOkonchaniya = '{data}'
where idPostav = '{id}' and dataOkonchaniya = '{result}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Договор успешно расторгнут");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при расторжении договора!");
                return false;
            }
        }

        /// <summary>
        /// Изменение договора
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataZakl"></param>
        /// <param name="dataNachala"></param>
        /// <param name="dataOkon"></param>
        /// <param name="idPostav"></param>
        /// <returns></returns>
        static public bool Edit(string id, string dataZakl,
            string dataNachala, string dataOkon, string idPostav)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idDogovora 
FROM dogovors 
where dataOkonchaniya > '{dataNachala}' and idPostav = '{idPostav}';";
                object result = DBconnect.myCommand.ExecuteScalar();
                if (result != null && result.ToString() != id)
                {
                    MessageBox.Show("Договор в этом время уже имееться!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"Update dogovors
set dataZakl = '{dataZakl}',  dataNachala = '{dataNachala}',
dataOkonchaniya = '{dataOkon}'
where idDogovora = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Информация о договоре изменина");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении договора!");
                return false;
            }
        }

        /// <summary>
        /// Проверка договора
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public bool ProverkaStatysa(string id, string data)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT dataOkonchaniya 
FROM dogovors 
where idDogovora = '{id}';";
                object result = DBconnect.myCommand.ExecuteScalar();
                if (Convert.ToDateTime(result).Date < Convert.ToDateTime(data).Date)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при проверки договора!");
                return false;
            }
        }
    }
}
