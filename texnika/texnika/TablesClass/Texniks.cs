using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace texnika
{
    class Texniks
    {
        static public DataTable texView = new DataTable();

        /// <summary>
        /// Вывод техники
        /// </summary>
        /// <param name="name"></param>
        /// <param name="vid"></param>
        /// <param name="spis"></param>
        static public bool Views(string name, string vid, string spis)
        {
            try
            {
                string stroka = "";
                if (TexniksView.flag == 1 && vid != "Все")
                {
                    stroka = stroka + $"and Name_vid ='{vid}'";
                }
                if (TexniksView.flag2 == 1 && spis != "Все")
                {
                    stroka = stroka + $"and spisanie ='{spis}'";
                }
                DBconnect.myCommand.CommandText = $@"SELECT texnika.idTex,
Name_vid,  Name_Tex, zavodNomver, InvetarNomer, cena, 
Name_postav, data_postavki, spisanie
FROM  texnika
Join vid_texniki on texnika.idVid = vid_texniki.idVid 
Join postavka on texnika.idPostavki = postavka.NomerZap 
Join postavshik on postavshik.idPostav = postavka.idPostav 
where Name_Tex like '%{name}%' {stroka}
order by Name_vid";
                texView.Clear();
                DBconnect.myAdapter.Fill(texView);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе техники!");
                return false;
            }
        }

        static public DataTable tex = new DataTable();

        /// <summary>
        /// Вывод техники в поставке
        /// </summary>
        /// <param name="id"></param>
        static public void View(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idTex, Name_Tex, 
Name_vid, zavodNomver, InvetarNomer, cena 
FROM texnika, vid_texniki
where texnika.idVid = vid_texniki.idVid and idPostavki = '{id}'";
                tex.Clear();
                DBconnect.myAdapter.Fill(tex);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе техники в поставке!");
            }
        }

        static public string KolvoPostavlenix()
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT count(idTex)
FROM texnika";
                return DBconnect.myCommand.ExecuteScalar().ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе техники в поставке!");
                return "";
            }
        }

        static public string KolvoPostavlenix(string dataOt, string dataDo)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT count(idTex)
FROM texnika, postavka
where texnika.idPostavki = postavka.NomerZap and data_postavki >= '{dataOt}' and data_postavki <= '{dataDo}'";
                return DBconnect.myCommand.ExecuteScalar().ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе техники в поставке!");
                return "";
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
                DBconnect.myCommand.CommandText = $@"SELECT nomer_zapici
FROM texnika_in_akt where idTex = '{id}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Технику списали!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"SELECT Nomer_zap
FROM raspolojenie where idTex = '{id}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("У техники есть расположение!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"SELECT nom_zapici
FROM texnika_in_merop where idTex = '{id}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Техника использовалась на мероприятии!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"Delete from texnika 
where idTex = '{id}'";
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
        /// Добавление
        /// </summary>
        /// <param name="postavka"></param>
        /// <param name="name"></param>
        /// <param name="vid"></param>
        /// <param name="nomZ"></param>
        /// <param name="nomI"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        static public bool Add(string postavka, string name, string vid, 
            string nomZ, string nomI, string price)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idTex
FROM texnika
where  zavodNomver = '{nomZ}'";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Техника с таким заводским номером уже есть!");
                    return false;
                }

                DBconnect.myCommand.CommandText = $@"SELECT idTex
FROM texnika
where  InvetarNomer = '{nomI}'";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Техника с таким инвентарныйм номером уже есть!");
                    return false;
                }

                DBconnect.myCommand.CommandText = $@"Insert into texnika 
values(null,'{postavka}','{name}','{vid}','{nomZ}', '{nomI}', 'Нет','{price}')";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Техника добавлена!");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении техники!");
                return false;
            }
        }

        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="vid"></param>
        /// <param name="nomZ"></param>
        /// <param name="nomI"></param>
        /// <param name="price"></param>
        /// <param name="nomZold"></param>
        /// <param name="nomIold"></param>
        /// <returns></returns>
        static public bool Edit(string id,  
            string name, string vid,
            string nomZ, string nomI, string price, 
            string nomZold, string nomIold)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idTex
FROM texnika
where  zavodNomver = '{nomZ}'";
                if (DBconnect.myCommand.ExecuteScalar() != null && nomZold != nomZ)
                {
                    MessageBox.Show("Техника с таким заводским номером уже есть!");
                    return false;
                }

                DBconnect.myCommand.CommandText = $@"SELECT idTex
FROM texnika
where  InvetarNomer = '{nomI}'";
                if (DBconnect.myCommand.ExecuteScalar() != null && nomIold != nomI)
                {
                    MessageBox.Show("Техника с таким инвентарныйм номером уже есть!");
                    return false;
                }

                DBconnect.myCommand.CommandText = $@"Update texnika 
set Name_Tex = '{name}', idVid = '{vid}', 
zavodNomver = '{nomZ}', InvetarNomer = '{nomI}',
cena = '{price}'
where idTex = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Техника изменена!");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении техники!");
                return false;
            }
        }



        static public DataTable comboboxVid = new DataTable();

        /// <summary>
        /// Вывод техники в комбобокс по виду
        /// </summary>
        static public void ViewComboboxPoVidy(string vid)
        {
            try
            {
                comboboxVid.Clear();
                DBconnect.myCommand.CommandText = $@"Select idTex ,
concat(Name_Tex, ' 
(',InvetarNomer,')') as name
FROM  texnika, vid_texniki
where texnika.idVid = vid_texniki.idVid 
and spisanie = 'Нет' and texnika.idVid = '{vid}'";
                DBconnect.myAdapter.Fill(comboboxVid);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе техники в комбобокс!");
            }
        }

        static public DataTable comboboxRaspoloj = new DataTable();
        
        /// <summary>
        /// Вывод в комбобокс для фильтра
        /// </summary>
        static public void ViewComboboxRaspoloj(string vid)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Select idTex ,
concat(Name_vid,' ',Name_Tex, ' 
(',InvetarNomer,')') as name
FROM  texnika, vid_texniki
where texnika.idVid = vid_texniki.idVid  and texnika.idVid = '{vid}' ";
                comboboxRaspoloj.Clear();
                DBconnect.myAdapter.Fill(comboboxRaspoloj);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе техники в комбобокс!");
            }
        }

        /// <summary>
        /// Изменение статуса списания
        /// </summary>
        /// <param name="idTex"></param>
        /// <param name="statys"></param>
        static public void Spisanie(string idTex, string statys)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Update texnika 
set spisanie = '{statys}' where idTex = '{idTex}'";
                DBconnect.myCommand.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Ошибка при изменение статуса списания техники!");
            }
        }


        static public DataTable comboboxMerop = new DataTable();

        /// <summary>
        /// Вывод техники в комбобокс
        /// </summary>
        static public void ViewCombobox(string dataMerop, string vid)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT texnika.idTex ,
concat(Name_Tex, ' 
(',InvetarNomer,')') as name
FROM  texnika, vid_texniki
where texnika.idVid = vid_texniki.idVid and texnika.idVid = '{vid}'
and idTex not in (Select idTex 
from texnika_in_merop, meropriyatiya 
where texnika_in_merop.idMerop = meropriyatiya.idMerop 
and meropriyatiya.Data_Merop = '{dataMerop}') 
and idTex in (SELECT idTex 
FROM postavka
join texnika on postavka.NomerZap = texnika.idPostavki
where data_postavki < '{dataMerop}') ";
                comboboxMerop.Clear();
                DBconnect.myAdapter.Fill(comboboxMerop);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе техники в комбобокс!");
            }
        }
    }
}
