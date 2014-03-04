using System;
using System.IO;

namespace Utilities.Common
{
    public class FileUtil
    {
        public static void WriteNewFile(FileInfo file, byte[] content)
        {
            DeleteIfExists(file);

            using (var fs = file.Create())
                fs.Write(content, 0, content.Length);
        }

        public static void DeleteIfExists(FileInfo file)
        {
            if (file.Exists)
                file.Delete();
        }

        public static DirectoryInfo CreateDirectoryIfNotExists(string path)
        {
            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
                directory.Create();

            return directory;
        }

        public static FileInfo MakeRandomNewFileName(string path, string file, string extension)
        {
            FileInfo randomFile;

            do
            {
                var fileName = string.Format("{0}-{1}.{2}", file, Guid.NewGuid(), extension);
                randomFile = new FileInfo(Path.Combine(path, fileName));
            } while (randomFile.Exists);

            return randomFile;
        }
    }
}
