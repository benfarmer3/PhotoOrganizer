using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using PhotoOrganiser.DataBase;
using System.Collections;
using System.Text;
using XSystem.Security.Cryptography;

namespace PhotoOrganiser.Traverser
{
    internal class FileSystemTraverser
    {
        private Hashtable photoHash = new Hashtable();
        private DataBaseManager dbManager;

        public FileSystemTraverser(DataBaseManager dbManager)
        {
            this.dbManager = dbManager;
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
            var directories = ImageMetadataReader.ReadMetadata(file.FullName);
            var hash = GetHash(file.FullName);
            var name = file.Name;
            var extension = file.Extension;
            var path = file.FullName;

            if (photoHash.ContainsKey(hash))
            {
                dbManager.AddImage(hash, name, extension, path, true);
                return;
            }
            dbManager.AddImage(hash, name, extension, path, false);

        }
        static string GetHash(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return Encoding.UTF8.GetString(new SHA1Managed().ComputeHash(fileStream));
            }
        }

        private void StoreImageData(FileInfo image)
        {
            GetImageExifData(image);

        }
    }
}
