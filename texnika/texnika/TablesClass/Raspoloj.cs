using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace texnika
{
    class Raspoloj
    {
        static public DataTable raspol = new DataTable();

        /// <summary>
        /// Вывод расположения
        /// </summary>
        /// <param name="tex"></param>
        static public bool View(string tex)
        {
            try
            {
                string stroka = "";
                if (RaspolojForm.flag2 == 1)
                {
                    stroka = $"and raspolojenie.idTex = '{tex}'";
                }
                DBconnect.myCommand.CommandText = $@"SELECT Nomer_zap,texnika.idVid,
raspolojenie.idTex, concat(Name_vid,' ', Name_Tex, ' (',InvetarNomer,')') as name,
Kabinet, concat(fio, ' (',telefon,')') as fio, 
DataYstanovki, DataYdaleniya
FROM raspolojenie, texnika, sotrudniks, vid_texniki
where raspolojenie.idTex = texnika.idTex and vid_texniki.idVid = texnika.idVid
and raspolojenie.idSotrud = sotrudniks.idSotrud {stroka} order by name";
                raspol.Clear();
                DBconnect.myAdapter.Fill(raspol);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе расположения!");
                return false;
            }
        }

        /// <summary>
        /// История расположения
        /// </summary>
        /// <param name="vid"></param>
        static public void ViewVid(string vid)
        {
            try
            {
                string stroka = "";
                if (vid != "")
                {
                    stroka = $"and texnika.idVid = '{vid}'";
                }
                DBconnect.myCommand.CommandText = $@"SELECT Nomer_zap,texnika.idVid,
raspolojenie.idTex, concat(Name_vid,' ', Name_Tex, ' (',InvetarNomer,')') as name,
Kabinet, concat(fio, ' (',telefon,')') as fio, 
DataYstanovki, DataYdaleniya
FROM raspolojenie, texnika, sotrudniks, vid_texniki
where raspolojenie.idTex = texnika.idTex and vid_texniki.idVid = texnika.idVid
and raspolojenie.idSotrud = sotrudniks.idSotrud {stroka} order by name";
                raspol.Clear();
                DBconnect.myAdapter.Fill(raspol);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе расположения!");
            }
        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="idTex"></param>
        /// <param name="kab"></param>
        /// <param name="sotr"></param>
        /// <param name="dataYst"></param>
        /// <returns></returns>
        static public bool Add(string idTex, string kab, 
            string sotr, string dataYst)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT Nomer_zap 
FROM raspolojenie
where idTex = '{idTex}' and
DataYstanovki >= '{dataYst}'";
                object result = DBconnect.myCommand.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show("Техника была утсановленна до этой даты!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"SELECT data_postavki 
FROM postavka
join texnika on postavka.NomerZap = texnika.idPostavki
where idTex = '{idTex}' ";
                result = DBconnect.myCommand.ExecuteScalar();
                if (Convert.ToDateTime(result).Date > Convert.ToDateTime(dataYst).Date)
                {
                    MessageBox.Show("На выбранную дату техники небыло в организации!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"Insert into raspolojenie 
values(null,'{idTex}','{kab}','{sotr}','{dataYst}', null);";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Расположение добавлено!");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка добавлении расположения!");
                return false;
            }
        }

        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="nomer"></param>
        /// <param name="idTex"></param>
        /// <param name="kab"></param>
        /// <param name="sotr"></param>
        /// <param name="dataYst"></param>
        /// <returns></returns>
        static public bool Edit(string nomer ,string idTex, string kab,
            string sotr, string dataYst)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT Nomer_zap 
FROM raspolojenie
where idTex = '{idTex}' and
DataYstanovki >= '{dataYst}' and Nomer_zap <> '{nomer}'";
                object result = DBconnect.myCommand.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show("Техника была утсановленна до этой даты!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"SELECT data_postavki 
FROM postavka
join texnika on postavka.NomerZap = texnika.idPostavki
where idTex = '{idTex}' ";
                result = DBconnect.myCommand.ExecuteScalar();
                if (Convert.ToDateTime(result).Date > Convert.ToDateTime(dataYst).Date)
                {
                    MessageBox.Show("На выбранную дату техники небыло в организации!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"Update raspolojenie 
set idTex = '{idTex}', Kabinet = '{kab}', idSotrud = '{sotr}',
DataYstanovki = '{dataYst}'
where Nomer_zap = '{nomer}';";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Расположение изменено!");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка изменения расположения!");
                return false;
            }
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="nomer"></param>
        /// <returns></returns>
        static public bool Delete(string nomer)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Delete from raspolojenie 
where Nomer_zap = '{nomer}';";
                DBconnect.myCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка удалении расположения!");
                return false;
            }
        }

        /// <summary>
        /// Изменение даты удаления 
        /// </summary>
        /// <param name="idtex"></param>
        /// <param name="dat"></param>
        /// <returns></returns>
        static public bool DataYdaleniya(string idtex, string dat)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT Nomer_zap 
FROM raspolojenie
where idTex = '{idtex}' and
DataYdaleniya is null and DataYstanovki <> '{dat}'";
                object result = DBconnect.myCommand.ExecuteScalar();
                if (result != null)
                {
                    DBconnect.myCommand.CommandText = $@"Update raspolojenie 
set DataYdaleniya = '{dat}'
where Nomer_zap = '{result.ToString()}';";
                    DBconnect.myCommand.ExecuteNonQuery();
                    return true;
                }

                return false;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменение даты удаления расположения!");
                return false;
            }
        }

        /// <summary>
        /// Изменение даты удаленя когда ее убрали
        /// </summary>
        /// <param name="idtex"></param>
        /// <param name="dat"></param>
        /// <param name="datOld"></param>
        /// <returns></returns>
        static public bool DataYdaleniya(string idtex, string dat, string datOld)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT Nomer_zap 
FROM raspolojenie
where idTex = '{idtex}' and
DataYdaleniya = '{datOld}'";
                object result = DBconnect.myCommand.ExecuteScalar();
                if (result != null)
                {
                    DBconnect.myCommand.CommandText = $@"SELECT Nomer_zap 
FROM raspolojenie
where idTex = '{idtex}' and
DataYstanovki > '{dat}'";
                    object result2 = DBconnect.myCommand.ExecuteScalar();
                    if (result2 != null)
                    {
                        MessageBox.Show("Техника была утсановленна до этой даты!");
                        return false;
                    }
                        DBconnect.myCommand.CommandText = $@"Update raspolojenie 
set DataYdaleniya = '{dat}'
where Nomer_zap = '{result.ToString()}';";
                        DBconnect.myCommand.ExecuteNonQuery();
                        return true;
                }

                return false;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменение даты удаления расположения!");
                return false;
            }
        }

        /// <summary>
        /// Изменение даты удаления на нул
        /// </summary>
        /// <param name="idtex"></param>
        /// <param name="dat"></param>
        /// <returns></returns>
        static public bool DataYdaleniyaIsNull(string idtex, string dat)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT Nomer_zap 
FROM raspolojenie
where idTex = '{idtex}' and
DataYdaleniya = '{dat}'";
                object result = DBconnect.myCommand.ExecuteScalar();
                if (result != null)
                {
                    DBconnect.myCommand.CommandText = $@"Update raspolojenie 
set DataYdaleniya = null
where Nomer_zap = '{result.ToString()}';";
                    DBconnect.myCommand.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменение даты удаления расположения!");
                return false;
            }
        }

        /// <summary>
        /// Изменение даты удаления при увольнении сотрудника
        /// </summary>
        /// <param name="sotr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public bool DataYdaleniyaYvolSotr(string sotr, string data)
        {
            try
            {

                DBconnect.myCommand.CommandText = $@"Update raspolojenie 
set DataYdaleniya = '{data}'
where idSotrud = '{sotr}' and DataYdaleniya is null;";
                DBconnect.myCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменение даты удаления расположения!");
                return false;
            }
        }

        /// <summary>
        /// изменение даты удалени
        /// </summary>
        /// <param name="nomer"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public bool NaSklad(string nomer, string data)
        {
            try
            {

                DBconnect.myCommand.CommandText = $@"Update raspolojenie 
set DataYdaleniya = '{data}'
where Nomer_zap = '{nomer}';";
                DBconnect.myCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменение даты удаления расположения!");
                return false;
            }
        }

    }
}
