namespace SDSFinder.Modules
{
    public class ExpandKey
    {
        public static string ExpandKeyByLength(string value, int keylength, IndAppContext db)
        {
            string result = "";
            using (var command = db.Database.GetDbConnection().CreateCommand())
            {
                db.Database.OpenConnection();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT dbo.ExpandKy(" + keylength.ToString() + ",'" + value + "')";
                result = command.ExecuteScalar().ToString();
            }
            return result;
        }
    }
}