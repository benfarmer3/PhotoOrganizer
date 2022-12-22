using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.QuickTime;
using System.Buffers;
using System.Text;
using XSystem.Security.Cryptography;

namespace PhotoOrganiser.Utilities
{
    public static class Utilities
    {
        public static string? GenerateHashFromFile(FileInfo file)
        {
            var dateTaken = GetDateTakenFromMedia(file);
            if (dateTaken == null)
            {
                return null;
            }
            string imageData = dateTaken + file.Name + file.Length;
            return GetStringSha256Hash(imageData);
        }

        public static string GenerateHashFromFileNoExif(FileInfo file)
        {
            var block = ArrayPool<byte>.Shared.Rent(8192);

            SHA256Managed sha = new SHA256Managed();
            int offset = 0;
            // For each block:
            offset += sha.TransformBlock(block, 0, block.Length, block, 0);

            // For last block:
            sha.TransformFinalBlock(block, 0, block.Length);

            // Get the has code
            byte[] hash = sha.Hash;
            return BitConverter.ToString(hash).Replace("-", String.Empty);

        }

        private static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static string? GetDateTakenFromMedia(FileInfo file)
        {

            try
            {
                List<DateTime> dates = new List<DateTime>();

                var directories = ImageMetadataReader.ReadMetadata(file.FullName);
                var directoriesd = directories.OfType<QuickTimeMovieHeaderDirectory>().FirstOrDefault();
                var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

                var dateDigitized = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeDigitized);
                var dateOriginal = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
                if (dateOriginal != null)
                {
                    try
                    {
                        dates.Add(DateTime.ParseExact(dateOriginal, "yyyy:MM:dd HH:mm:ss", null));
                    }
                    catch
                    {
                        return null;
                    }

                }
                if (dateDigitized != null)
                {
                    try
                    {
                        dates.Add(DateTime.ParseExact(dateDigitized, "yyyy:MM:dd HH:mm:ss", null));
                    }
                    catch
                    {
                        return null;
                    }
                }

                return dates.Count == 0 ? null : dates.Min<DateTime>().ToString();


            }
            catch (MetadataExtractor.ImageProcessingException)
            {
                return null;
            }

        }

    }
}