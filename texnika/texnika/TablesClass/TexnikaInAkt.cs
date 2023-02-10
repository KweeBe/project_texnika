using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace texnika
{
    class TexnikaInAkt
    {
        static public DataTable tex = new DataTable();

        /// <summary>
        /// Вывод техники в акте
        /// </summary>
        /// <param name="id"></param>
        static public bool View(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT nomer_zapici, texnika_in_akt.idTex ,
concat(Name_Tex, ' (',InvetarNomer,')') as name, prichina
FROM texnika, texnika_in_akt
where texnika_in_akt.idTex = texnika.idTex and texnika_in_akt.idAkta = '{id}'";
                tex.Clear();
                DBconnect.myAdapter.Fill(tex);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе техники в акте!");
                return false;
            }
        }

        static public string KolvoSpisanTexniki()
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT count(nomer_zapici)
FROM texnika_in_akt;";
                return DBconnect.myCommand.ExecuteScalar().ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе количества списаной техники!");
                return "";
            }
        }

        static public string KolvoSpisanTexniki(string dataOt, string dataDo)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT count(nomer_zapici)
FROM texnika_in_akt, akt_spisanie
where texnika_in_akt.idAkta = akt_spisanie.idAkta and akt_spisanie.data_akta >= '{dataOt}' and akt_spisanie.data_akta <= '{dataDo}'";
                return DBconnect.myCommand.ExecuteScalar().ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе количества списаной техники!");
                return "";
            }
        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="akt"></param>
        /// <param name="tex"></param>
        /// <param name="prichina"></param>
        /// <returns></returns>
        static public bool Add(string akt, string tex, string prichina)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Insert into texnika_in_akt 
values(null,'{akt}', '{tex}','{prichina}')";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Техника добавлена в акт!");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении техники в акт!");
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
                DBconnect.myCommand.CommandText = $@"Delete from texnika_in_akt 
where nomer_zapici = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении техники!");
                return false;
            }
        }

        /// <summary>
        /// Измение
        /// </summary>
        /// <param name="nomer"></param>
        /// <param name="prichina"></param>
        /// <returns></returns>
        static public bool Edit(string nomer, string prichina)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Update texnika_in_akt 
set prichina = '{prichina}'
where nomer_zapici = '{nomer}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Информация изменена!");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении техники в акт!");
                return false;
            }
        }

    }
}
