
namespace SDSFinder.Modules.Admin
{
    public class AdminModules
    {
        public static string? GetSafetyID(string fileName)
        {
            if (fileName != null)
            {
                // Remove before 2nd underscore
                int secondUnderscore = fileName.IndexOf('_', fileName.IndexOf('_') + 1) + 1;
                string middle = fileName.Substring(secondUnderscore);

                // Remove from first underscore after the cleaned start
                int trimFrom = middle.IndexOf('_');
                string trimFileName = trimFrom >= 0 ? middle.Substring(0, trimFrom) : middle;
                return trimFileName;
            }
            return null;
        }
    }
}
