using System.Data;
using System.Windows.Forms;

namespace texnika
{
    class TexnikaInMerop
    {
        static public DataTable tex = new DataTable();

        /// <summary>
        /// Вывод техники на мероприятии
        /// </summary>
        /// <param name="id"></param>
        static public bool View(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT nom_zapici, texnika_in_merop.idTex ,
concat(Name_Tex, ' (',InvetarNomer,')') as name
FROM texnika, texnika_in_merop
where texnika_in_merop.idTex = texnika.idTex and texnika_in_merop.idMerop = '{id}'";
                tex.Clear();
                DBconnect.myAdapter.Fill(tex);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе техники на мероприятии!");
                return false;
            }
        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="merop"></param>
        /// <param name="tex"></param>
        /// <returns></returns>
        static public bool Add(string merop, string tex)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"Insert into texnika_in_merop 
values(null,'{merop}', '{tex}')";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Техника добавлена на мероприятие!");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении техники на мероприятие!");
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
                DBconnect.myCommand.CommandText = $@"Delete from texnika_in_merop 
where nom_zapici = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении техники!");
                return false;
            }
        }
    }
}
