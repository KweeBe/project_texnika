using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace texnika
{
    class VidTexniki
    {
        static public DataTable vid = new DataTable();
        static public DataTable vidCombobox = new DataTable();

        /// <summary>
        /// Вывод видов техники
        /// </summary>
        static public bool View()
        {
            try
            {
                DBconnect.myCommand.CommandText = @"SELECT idVid, Name_vid 
FROM vid_texniki where Name_vid <> 'Все' order by Name_vid;";
                vid.Clear();
                DBconnect.myAdapter.Fill(vid);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе видов техники!");
                return false;
            }
        }

        /// <summary>
        /// Добавление нового вида техники
        /// </summary>
        /// <param name="name">название вида</param>
        static public bool Add(string name)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idVid
FROM vid_texniki where Name_vid = '{name}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Такой вид техники уже имееться!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"Insert into vid_texniki 
values(null, '{name}');";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Вид техники успешно добавлен");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении вида техники!");
                return false;
            }
        }

        /// <summary>
        /// Удаление вида техники
        /// </summary>
        /// <param name="id">id вида</param>
        static public void Delete(string id)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idTex FROM texnika
where idVid = '{id}';";
                if (DBconnect.myCommand.ExecuteScalar() != null)
                {
                    MessageBox.Show("Данный вид техники используеться!");
                    return;
                }
                DBconnect.myCommand.CommandText = $@"Delete from vid_texniki
where idVid = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Вид техники успешно удален");
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении вида техники!");
            }
        }

        /// <summary>
        /// Изменение информации о виде
        /// </summary>
        /// <param name="id">id вида</param>
        /// <param name="name">новое название</param>
        /// <param name="oldName">старое название</param>
        static public bool Edit(string id, string name, string oldName)
        {
            try
            {
                DBconnect.myCommand.CommandText = $@"SELECT idVid
FROM vid_texniki where Name_vid = '{name}';";
                if (DBconnect.myCommand.ExecuteScalar() != null && oldName != name)
                {
                    MessageBox.Show("Такой вид техники уже имееться!");
                    return false;
                }
                DBconnect.myCommand.CommandText = $@"Update vid_texniki
set Name_vid = '{name}' where idVid = '{id}'";
                DBconnect.myCommand.ExecuteNonQuery();
                MessageBox.Show("Информация о виде технике успешно изменена");
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении вида техники!");
                return false;
            }
        }

        /// <summary>
        /// Вывод видов техники в комбобокс
        /// </summary>
        static public void ViewCombobox()
        {
            try
            {
                DBconnect.myCommand.CommandText = @"SELECT idVid, Name_vid 
FROM vid_texniki where Name_vid <> 'Все'order by Name_vid;";
                vidCombobox.Clear();
                DBconnect.myAdapter.Fill(vidCombobox);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе видов техники в комбобокс!");
            }
        }

        static public DataTable comboboxFiltr = new DataTable();
        /// <summary>
        /// Вывод видов техники в комбобокс для фильтра
        /// </summary>
        static public void ViewComboboxFiltr()
        {
            try
            {
                comboboxFiltr.Clear();
                DBconnect.myCommand.CommandText = @"SELECT idVid, Name_vid 
FROM vid_texniki ;";
                DBconnect.myAdapter.Fill(comboboxFiltr);
            }
            catch
            {
                MessageBox.Show("Ошибка при выводе видов техники в комбобокс!");
            }
        }
    }
}
