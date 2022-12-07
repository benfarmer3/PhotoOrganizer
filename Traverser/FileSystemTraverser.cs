using MetadataExtractor.Formats.Exif;
using MetadataExtractor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace PhotoOrganiser.Traverser
{
    internal class FileSystemTraverser
    {
        private static readonly string[] _validExtensions = { "jpg", "bmp", "gif", "png", "jpeg", "tiff", }; //  etc
        private ImageContext? dbImageContext = new ImageContext();
        private static List<FileInfo> test = new List<FileInfo>();


        public FileSystemTraverser()
        {
            
        }


        static string[] mediaExtensions = {
            ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", //etc
            ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", //etc
            ".AVI", ".MP4", ".DIVX", ".WMV", ".MP4", ".RAW", ".MOV", ".PSD",
            ".EPS", ".PDF", ".TIF", ".SVG", ".DNP", ".MKV",
            //etc
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
                        test.Add(file);
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

            // print out all metadata
            foreach (var directory in directories)
                foreach (var tag in directory.Tags)
                    Console.WriteLine($"{directory.Name} - {tag.Name} = {tag.Description}");

           

            // access the date time
            var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            var dateTime = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTime);
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
/*
            if (dbImageContext != null)
            {
                dbImageContext.Add(image);
                dbImageContext.SaveChanges();                
            }
*/
        }
    }
}
