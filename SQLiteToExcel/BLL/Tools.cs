using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiteToExcel.BLL
{
    public class Tools
    {
        public static bool DbExists(String path)  //判断db文件是否正确
        {
            if (File.Exists(path))
            {
                if (Path.GetExtension(path) == ".db")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool FileExistDelete(String path)     //如果存在该文件则删除之
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    return true;
                }
                catch
                {
                    MessageBox.Show(path+"文件被占用！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

            }
            else
            {
                return true;
            }
        }

        public static Object IsNone(Object value)
        {
            Object tabvalue;
            if (Convert.ToString(value) == "None"|| Convert.ToString(value) == "Null"|| String.IsNullOrEmpty(Convert.ToString(value))!=false)
            {
                tabvalue = "--";
            }
            else  
            {
                tabvalue = Convert.ToString(value);
            }
               
            return tabvalue;
        }

        public static Object IsError(Object value)
        {
            Object tabvalue;
            if (Convert.ToString(value) == "None" || Convert.ToString(value) == "Null" || String.IsNullOrEmpty(Convert.ToString(value)) != false || Convert.ToString(value) == "255"|| Convert.ToString(value) == "-1")
            {
                tabvalue = "--";
            }
            else
            {
                tabvalue = Convert.ToString(value);
            }

            return tabvalue;
        }

    }
}
