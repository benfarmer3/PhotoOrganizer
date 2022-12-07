using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using PhotoOrganiser.DataBase;
using System;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using XAct.Security;
using XSystem.Security.Cryptography;

namespace PhotoOrganiser.Traverser
{
    internal class FileSystemTraverser
    {
        private Hashtable photoHash = new Hashtable();

        public FileSystemTraverser()
        {

        }

        static string[] mediaExtensions = {
            ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", 
            ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", 
            ".AVI", ".MP4", ".DIVX", ".WMV", ".MP4", ".RAW", ".MOV", ".PSD",
            ".EPS", ".PDF", ".TIF", ".SVG", ".DNP", ".MKV",
        };

        private static bool IsMediaFile(string path)
        {
            return -1 != Array.IndexOf(mediaExtensions, Path.GetExtension(path).ToUpperInvariant());
        }

        public void StartTraversal(string folder)
        {
            DirectoryInfo root = new System.IO.DirectoryInfo(folder);
            Traverse(root);
        }

        private void Traverse(DirectoryInfo root)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                files = root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                //log.Add(e.Message);
            }

            if(files != null)
            {
                foreach (FileInfo file in files)
                {
                    if (IsMediaFile(file.FullName))
                    {
                        StoreImageData(file);
                    }
                }

                subDirs = root.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    Traverse(subDir);
                }
            }
           

        }

        private void GetImageExifData(FileInfo file)
        {
            var hash = GenerateHashFromFile(file);
            if(hash == null)
            {
                // no exif data
            }
            var name = file.Name;
            var extension = file.Extension;
            var path = file.FullName;

            if (photoHash.ContainsKey(hash))
            {
                DataBaseManager.AddImage(hash, name, extension, path, true);
                return;
            }
            photoHash.Add(hash,file.Name);
            DataBaseManager.AddImage(hash, name, extension, path, false);

        }

        internal static string GetStringSha256Hash(string text)
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

        public string? GenerateHashFromFile(FileInfo file)
        {
            var dateTaken = GetDateTakenFromImage(file.FullName).ToString();
            if (dateTaken == null)
            {
                return null;
            }
            string imageData = dateTaken + file.Name + file.Length;
            return GetStringSha256Hash(imageData);
        }

        public byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private void StoreImageData(FileInfo image)
        {
            GetImageExifData(image);

        }
        private static Regex r = new Regex(":");

        public static DateTime? GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {

                var directories = ImageMetadataReader.ReadMetadata(fs);

                // Find the so-called Exif "SubIFD" (which may be null)
                var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

                // Read the DateTime tag value
                try
                {
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
