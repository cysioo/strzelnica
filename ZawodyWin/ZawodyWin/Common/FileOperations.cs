using System.IO;
using System.Text;

namespace ZawodyWin.Common
{
    public class FileOperations
    {
        public static string PrepareFilePath(string folder, string baseFileName, string extension)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            StringBuilder fileName = CreateValidFileName(baseFileName);
            var fullPath = $"{folder}\\{fileName}.{extension.TrimStart('.')}";
            return fullPath;
        }

        private static StringBuilder CreateValidFileName(string baseFileName)
        {
            var fileName = new StringBuilder(baseFileName);
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }
            return fileName;
        }
    }
}
