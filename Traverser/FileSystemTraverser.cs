using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using PhotoOrganiser.DataBase;
using System.Collections;

namespace PhotoOrganiser.Traverser
{
    internal class FileSystemTraverser
    {

       // private readonly ILogger _logger;

        private Hashtable photoHash = new Hashtable();
        private int totalPhotos = 0;
        private int totalNoExifPhotos = 0;
        private int totalDuplicatePhotos = 0;

        private Action<string, string, string> _updateValues; 
        private Action<string> _updateCurrentFolder;

        public FileSystemTraverser(Action<string,string, string> updateValues, Action<string> updateCurrentFolder)
        {
            _updateValues = updateValues;
            _updateCurrentFolder = updateCurrentFolder;
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
            _updateCurrentFolder(root.FullName);

            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    if (IsMediaFile(file.FullName))
                    {
                        StoreImageData(file);
                        _updateValues(totalPhotos.ToString(), totalDuplicatePhotos.ToString(), totalNoExifPhotos.ToString());
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
                totalNoExifPhotos++;
                return;
            }
            var name = file.Name;
            var extension = file.Extension;
            var path = file.FullName;

            if (photoHash.ContainsKey(hash))
            {
                totalDuplicatePhotos++;
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

        private void StoreImageData(FileInfo image)
        {
            GetImageExifData(image);
            totalPhotos++;
            _updateValues(totalPhotos.ToString(), totalDuplicatePhotos.ToString(), totalNoExifPhotos.ToString());
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

        public static DateTime? GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {

                var directories = ImageMetadataReader.ReadMetadata(fs);

                var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

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
