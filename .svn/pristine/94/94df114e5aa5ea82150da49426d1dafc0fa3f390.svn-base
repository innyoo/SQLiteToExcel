using System.Data.SQLite;

namespace SQLiteToExcel.DAL
{
    public class Dal_admin
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static SQLiteDataReader GetReader(string dbPath,string sql)
        {
            //SQLiteConnection.CreateFile("MyDatabase.sqlite");
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
            m_dbConnection.Open();
            //string sql = "select * from events";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader; 
        }
    }
}
