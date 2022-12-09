using PhotoOrganiser.DataBase;
using System.Diagnostics;

namespace PhotoOrganiser.Traverser
{
    internal class FileSystemTraverser
    {


        // private readonly ILogger _logger;
        private Stopwatch hashTime = new Stopwatch();
        List<TimeSpan> averageNoExif = new List<TimeSpan>();
        List<TimeSpan> averageExif = new List<TimeSpan>();

        private HashSet<string> photoDuplicateHash = new HashSet<string>();
        private int totalPhotos = 0;
        private int totalNoExifPhotos = 0;
        private int totalDuplicatePhotos = 0;

        private Action<string, string, string> _updateValues; 
        private Action<string> _updateCurrentFolder;
        private Action<string, string> _updateAverages;

        public FileSystemTraverser(Action<string,string, string> updateValues, Action<string> updateCurrentFolder, Action<string, string> updateAverages)
        {
            _updateValues = updateValues;
            _updateCurrentFolder = updateCurrentFolder;
            _updateAverages = updateAverages;
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

            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    if (IsMediaFile(file.FullName))
                    {
                        _updateCurrentFolder(file.FullName);

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

        private void StoreImageData(FileInfo image)
        {
            var photoInfo = GetImageInformation(image);
            DataBaseManager.AddImage(photoInfo.hash, photoInfo.name, photoInfo.extension, photoInfo.path, photoInfo.duplicate, photoInfo.noExif);
            totalPhotos++;
            _updateValues(totalPhotos.ToString(), totalDuplicatePhotos.ToString(), totalNoExifPhotos.ToString());
        }

        private PhotoInformation GetImageInformation(FileInfo file)
        {
            PhotoInformation photoInfo;

            if (Utilities.Utilities.HasExif(file))
            {
                hashTime.Start();
                var hash = Utilities.Utilities.GenerateHashFromFile(file);
                hashTime.Stop();
                averageExif.Add(hashTime.Elapsed);
                hashTime.Restart();
                photoInfo = new PhotoInformation(hash, file.Name, file.Extension, file.FullName, false, true);
            }
            else
            {
                totalNoExifPhotos++;
                hashTime.Start();
                var hash = Utilities.Utilities.GenerateHashFromFileNoExif(file);
                hashTime.Stop();
                averageNoExif.Add(hashTime.Elapsed);
                hashTime.Restart();

                photoInfo = new PhotoInformation(hash, file.Name, file.Extension, file.FullName);
            }

            if (photoDuplicateHash.Contains(photoInfo.hash))
            {
                totalDuplicatePhotos++;
                photoInfo.duplicate = true;
                return photoInfo;
            }
            _updateAverages(Mean(averageExif).ToString(), Mean(averageNoExif).ToString());
            photoDuplicateHash.Add(photoInfo.hash);
            return photoInfo;
        }


        public TimeSpan Mean(IEnumerable<TimeSpan> source) => TimeSpan.FromTicks(source.Aggregate((m: 0L, r: 0L, n: source.Count()), (tm, s) => {
            var r = tm.r + s.Ticks % tm.n;
            return (tm.m + s.Ticks / tm.n + r / tm.n, r % tm.n, tm.n);
        }).m);


    }
}
