using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace texnika
{
    class Email
    {
        /// <summary>
        /// Проверка Email на валидность
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        static public bool CheckEmail(string em)
        {
            try
            {
                int inx = em.IndexOf('@');
                if (inx == 0 || inx == em.Length - 1
                    || inx == -1 || em.IndexOf('@', inx + 1) != -1)
                {
                    return false;
                }
                if (em.IndexOf('.', inx) == -1 ||
                    em.IndexOf('.', inx) == em.Length - 1)
                {
                    return false;
                }
                string stroka = em.Substring(inx+1, em.Length - (inx +1));
                if (em.IndexOf('.') == 0 || stroka.IndexOf('.') < 2 
                    || stroka.IndexOf('.', stroka.IndexOf('.')+1) != -1)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка при проверки почты!");
                return false;
            }
        }
    }
}
