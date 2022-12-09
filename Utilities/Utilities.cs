using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System.Text;
using XSystem.Security.Cryptography;

namespace PhotoOrganiser.Utilities
{
    public static class Utilities
    {
        public static string GenerateHashFromFile(FileInfo file)
        {
            var dateTaken = GetDateTakenFromImage(file.FullName).ToString();
            if (dateTaken == null)
            {
                return null;
            }
            string imageData = dateTaken + file.Name + file.Length;
            return GetStringSha256Hash(imageData);
        }

        public static string GenerateHashFromFileNoExif(FileInfo file)
        {
            using (var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            {
                return Encoding.UTF8.GetString(new SHA1Managed().ComputeHash(fileStream));
            }
        }

        public static bool HasExif(FileInfo file)
        {
            return GetDateTakenFromImage(file.FullName).ToString() != "" ? true : false;
        }


        private static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static DateTime? GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {

                

                try
                {
                    var directories = ImageMetadataReader.ReadMetadata(fs);

                    var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                    var dateTime = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                    return dateTime;

                }
                catch
                {
                    return null;
                }

            }
        }

    }
}
