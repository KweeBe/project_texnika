using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace texnika
{
    class Documents
    {
        /// <summary>
        /// Формирование документа акт списания
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <param name="fname"></param>
        /// <returns></returns>
        static public bool AktSpisaniya(string id, string data, string fname)
        {
            try
            {
                var app = new Word.Application();
                app.Visible = false;
                var fullPath = Path.GetFullPath(@"docx\Akt.docx");
                var doc = app.Documents.Open(fullPath);
                doc.Activate();

                doc.Bookmarks["nomer"].Range.Text = id;
                doc.Bookmarks["dataAkta"].Range.Text = Convert.ToDateTime(data).ToString("D");
                doc.Bookmarks["dataForm"].Range.Text = Convert.ToDateTime(DateTime.Now).ToString("D");

                DBconnect.myCommand.CommandText = $@"SELECT 
concat(Name_vid,' ',Name_Tex, ' (',InvetarNomer,')') as name,
data_postavki, prichina
FROM texnika
join vid_texniki on vid_texniki.idVid = texnika.idVid
join texnika_in_akt on texnika.idTex = texnika_in_akt.idTex
join akt_spisanie on akt_spisanie.idAkta = texnika_in_akt.idAkta
join postavka on texnika.idPostavki = postavka.NomerZap
where texnika_in_akt.idAkta = '{id}'";
                MySqlDataReader reader = DBconnect.myCommand.ExecuteReader();
                int i = 2;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        doc.Tables[1].Rows[i].Range.Font.Bold = 0;
                        doc.Tables[1].Rows[i].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        doc.Tables[1].Cell(i, 0).Range.Text = (i - 1).ToString();
                        doc.Tables[1].Cell(i, 2).Range.Text = reader["name"].ToString();
                        doc.Tables[1].Cell(i, 3).Range.Text = Convert.ToDateTime(reader["data_postavki"]).ToString("D");
                        doc.Tables[1].Cell(i, 4).Range.Text = reader["prichina"].ToString();
                        doc.Tables[1].Rows.Add();
                        i++;
                    }
                    doc.Tables[1].Rows[i].Delete();
                    reader.Close();
                }
                reader.Close();
                doc.Saved = true;
                doc.SaveAs2(fname);
                MessageBox.Show("Данные сохранены в новом doc-файле!!!");
                doc.Close();
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при формировании документа Акт списания");
                return false;
            }
        }

        /// <summary>
        /// Формирофание документа имеющеяся техника в организации
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        static public bool TexnikaImeysh( string fname)
        {
            try
            {
                var app = new Word.Application();
                app.Visible = false;
                var fullPath = Path.GetFullPath(@"docx\Texnika.docx");
                var doc = app.Documents.Open(fullPath);
                doc.Activate();

                doc.Bookmarks["DateTime"].Range.Text = Convert.ToDateTime(DateTime.Now).ToString("D");

                int i = 2;
                int n = 1;
                NameAndKol(out string name, out string kol);
                string[] names = name.Split(';');
                string[] kols = kol.Split(';');
                for (int j = 0; j < names.Count(); j++)
                {
                    DBconnect.myCommand.CommandText = $@"SELECT texnika.idTex,
Name_vid,  Name_Tex, zavodNomver, InvetarNomer, cena, 
Name_postav, data_postavki
FROM  texnika
Join vid_texniki on texnika.idVid = vid_texniki.idVid 
Join postavka on texnika.idPostavki = postavka.NomerZap 
Join postavshik on postavshik.idPostav = postavka.idPostav 
where texnika.spisanie = 'Нет'
and Name_vid = '{names[j]}'";
                    MySqlDataReader reader = DBconnect.myCommand.ExecuteReader();
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            doc.Tables[1].Rows.Add();
                            doc.Tables[1].Rows[i].Range.Font.Bold = 0;
                            doc.Tables[1].Rows[i].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            doc.Tables[1].Cell(i, 0).Range.Text = (n).ToString();
                            doc.Tables[1].Cell(i, 2).Range.Text = reader["Name_Tex"].ToString();
                            doc.Tables[1].Cell(i, 3).Range.Text = reader["zavodNomver"].ToString();
                            doc.Tables[1].Cell(i, 4).Range.Text = reader["InvetarNomer"].ToString();
                            doc.Tables[1].Cell(i, 5).Range.Text = reader["cena"].ToString();
                            doc.Tables[1].Cell(i, 6).Range.Text = reader["Name_postav"].ToString();
                            doc.Tables[1].Cell(i, 7).Range.Text = Convert.ToDateTime(reader["data_postavki"]).ToString("D");
                            i++;
                            n++;
                        }
                        reader.Close();
                    }
                    reader.Close();
                    doc.Tables[1].Rows.Add();
                    doc.Tables[1].Cell(i, 6).Range.Text = names[j];
                    doc.Tables[1].Cell(i, 7).Range.Text = kols[j];
                    i++;
                }
                doc.Tables[1].Rows[i - 1].Delete();
                for (int j = 1; j <= doc.Tables[1].Rows.Count; j++)
                {
                    int x = doc.Tables[1].Rows[j].Cells[1].Range.Text.Length;
                    if (x == 2)
                    {
                        doc.Tables[1].Rows[j].Range.Font.Bold = 1;
                        doc.Tables[1].Rows[j].Cells[1].Merge(doc.Tables[1].Rows[j].Cells[6]);
                        doc.Tables[1].Rows[j].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        doc.Tables[1].Rows[j].Cells[2].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    }
                }
                doc.Saved = true;
                doc.SaveAs2(fname);
                MessageBox.Show("Данные сохранены в новом doc-файле!!!");
                doc.Close();
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при формировании документа имеющейся техники");
                return false;
            }
        }

        /// <summary>
        /// Получение виды техники и количество техники по виду
        /// </summary>
        /// <param name="name"></param>
        /// <param name="kolvo"></param>
        static public void NameAndKol(out string name, out string kolvo)
        {
            try
            {
                name = "";
                kolvo = "";
                DBconnect.myCommand.CommandText = $@"SELECT Name_vid , count(Name_vid) as kol
FROM  texnika
Join vid_texniki on texnika.idVid = vid_texniki.idVid 
Join postavka on texnika.idPostavki = postavka.NomerZap 
Join postavshik on postavshik.idPostav = postavka.idPostav 
where texnika.spisanie = 'Нет'
group by Name_vid order by Name_vid";
                MySqlDataReader reader = DBconnect.myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = name + reader["Name_vid"].ToString();
                        name = name + ";";
                        kolvo = kolvo + reader["kol"].ToString();
                        kolvo = kolvo + ";";
                    }
                    reader.Close();
                }
                reader.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при получение название и количества");
                name = "";
                kolvo = "";
            }
        }



        /// <summary>
        /// Формирофание списаной техники за отчетный период
        /// </summary>
        /// <param name="dataOt"></param>
        /// <param name="dataDo"></param>
        /// <param name="fname"></param>
        /// <returns></returns>
        static public bool TexnikaSpis(string dataOt, string dataDo, string fname)
        {
            try
            {
                var app = new Word.Application();
                app.Visible = false;
                var fullPath = Path.GetFullPath(@"docx\Spisannaya.docx");
                var doc = app.Documents.Open(fullPath);
                doc.Activate();

                doc.Bookmarks["ot"].Range.Text = Convert.ToDateTime(dataOt).ToString("D");
                doc.Bookmarks["do"].Range.Text = Convert.ToDateTime(dataDo).ToString("D");
                doc.Bookmarks["DateTime"].Range.Text = Convert.ToDateTime(DateTime.Now).ToString("D");

                DBconnect.myCommand.CommandText = $@"SELECT concat(Name_vid,' ',Name_Tex) as name, 
InvetarNomer, data_postavki, akt_spisanie.Data_Akta, prichina
FROM texnika
join vid_texniki on vid_texniki.idVid = texnika.idVid
join texnika_in_akt on texnika.idTex = texnika_in_akt.idTex
join akt_spisanie on akt_spisanie.idAkta = texnika_in_akt.idAkta
join postavka on texnika.idPostavki = postavka.NomerZap
where Data_Akta >= '{dataOt}' and Data_Akta <= '{dataDo}'";
                MySqlDataReader reader = DBconnect.myCommand.ExecuteReader();
                int i = 2;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        doc.Tables[1].Rows[i].Range.Font.Bold = 0;
                        doc.Tables[1].Rows[i].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        doc.Tables[1].Cell(i, 0).Range.Text = (i - 1).ToString();
                        doc.Tables[1].Cell(i, 2).Range.Text = reader["name"].ToString();
                        doc.Tables[1].Cell(i, 3).Range.Text = reader["InvetarNomer"].ToString();
                        doc.Tables[1].Cell(i, 4).Range.Text = Convert.ToDateTime(reader["data_postavki"]).ToString("D");
                        doc.Tables[1].Cell(i, 5).Range.Text = Convert.ToDateTime(reader["Data_Akta"]).ToString("D");
                        doc.Tables[1].Cell(i, 6).Range.Text = reader["prichina"].ToString();
                        doc.Tables[1].Rows.Add();
                        i++;
                    }

                    reader.Close();
                }
                reader.Close();
                doc.Tables[1].Rows[i].Cells[1].Merge(doc.Tables[1].Rows[i].Cells[5]);
                doc.Tables[1].Rows[i].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                doc.Tables[1].Cell(i, 0).Range.Text = "Кол-во:";
                DBconnect.myCommand.CommandText = $@"SELECT count(texnika.idTex)
FROM texnika
join vid_texniki on vid_texniki.idVid = texnika.idVid
join texnika_in_akt on texnika.idTex = texnika_in_akt.idTex
join akt_spisanie on akt_spisanie.idAkta = texnika_in_akt.idAkta
join postavka on texnika.idPostavki = postavka.NomerZap
where Data_Akta >= '{dataOt}' and Data_Akta <= '{dataDo}'";
                object reader2 = DBconnect.myCommand.ExecuteScalar();
                doc.Tables[1].Rows[i].Cells[2].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                doc.Tables[1].Cell(i, 2).Range.Text = reader2.ToString();
                doc.Saved = true;
                doc.SaveAs2(fname);
                MessageBox.Show("Данные сохранены в новом doc-файле!!!");
                doc.Close();
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при формировании документа списанной техники за отчетный период");
                return false;
            }
        }

        /// <summary>
        /// Формирование документа мероприятие
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <param name="kab"></param>
        /// <param name="fname"></param>
        /// <returns></returns>
        static public bool Merop(string id, string name, string data, string time, string kab, string fname)
        {
            try
            {
                var app = new Word.Application();
                app.Visible = false;
                var fullPath = Path.GetFullPath(@"docx\Merop.docx");
                var doc = app.Documents.Open(fullPath);
                doc.Activate();

                doc.Bookmarks["name"].Range.Text = name;
                doc.Bookmarks["kab"].Range.Text = kab;
                doc.Bookmarks["time"].Range.Text = time;
                doc.Bookmarks["data"].Range.Text = Convert.ToDateTime(data).ToString("D");

                DBconnect.myCommand.CommandText = $@"SELECT Name_vid, Name_Tex, InvetarNomer,  Kabinet
FROM texnika
join vid_texniki on vid_texniki.idVid = texnika.idVid
join texnika_in_merop on texnika.idTex = texnika_in_merop.idTex
join raspolojenie on texnika.idTex = raspolojenie.idTex
where idMerop = '{id}' and DataYdaleniya is null";
                MySqlDataReader reader = DBconnect.myCommand.ExecuteReader();
                int i = 2;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        doc.Tables[1].Rows[i].Range.Font.Bold = 0;
                        doc.Tables[1].Rows[i].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        doc.Tables[1].Cell(i, 0).Range.Text = (i - 1).ToString();
                        doc.Tables[1].Cell(i, 2).Range.Text = reader["Name_vid"].ToString();
                        doc.Tables[1].Cell(i, 3).Range.Text = reader["Name_Tex"].ToString();
                        doc.Tables[1].Cell(i, 4).Range.Text = reader["InvetarNomer"].ToString();
                        doc.Tables[1].Cell(i, 5).Range.Text = reader["Kabinet"].ToString();
                        doc.Tables[1].Rows.Add();
                        i++;
                    }
                    reader.Close();
                }
                reader.Close();

                DBconnect.myCommand.CommandText = $@"SELECT Name_vid, Name_Tex, InvetarNomer
FROM texnika
join vid_texniki on vid_texniki.idVid = texnika.idVid
join texnika_in_merop on texnika.idTex = texnika_in_merop.idTex
where idMerop = '{id}' 
and texnika.idTex not in (select idTex from raspolojenie) ";
                MySqlDataReader reader2 = DBconnect.myCommand.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        doc.Tables[1].Rows[i].Range.Font.Bold = 0;
                        doc.Tables[1].Rows[i].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        doc.Tables[1].Cell(i, 0).Range.Text = (i - 1).ToString();
                        doc.Tables[1].Cell(i, 2).Range.Text = reader2["Name_vid"].ToString();
                        doc.Tables[1].Cell(i, 3).Range.Text = reader2["Name_Tex"].ToString();
                        doc.Tables[1].Cell(i, 4).Range.Text = reader2["InvetarNomer"].ToString();
                        doc.Tables[1].Cell(i, 5).Range.Text = "Склад";
                        doc.Tables[1].Rows.Add();
                        i++;
                    }
                    doc.Tables[1].Rows[i].Delete();
                    reader2.Close();
                }
                reader2.Close();



                doc.Saved = true;
                doc.SaveAs2(fname);
                MessageBox.Show("Данные сохранены в новом doc-файле!!!");
                doc.Close();
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при формировании документа списанной техники за отчетный период");
                return false;
            }
        }
    }
}
