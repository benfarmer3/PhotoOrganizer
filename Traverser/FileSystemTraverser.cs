using PhotoOrganiser.Utilities;

namespace PhotoOrganiser.Traverser
{
    internal class FileSystemTraverser
    {
        Action<string> _updateCurrentFolder;
        MediaProcessor imageProcessor;
        List<FileInfo> unsupportedFiles = new List<FileInfo>();
        bool stop = false;

        public FileSystemTraverser(Action<string, string, string> updateValues, Action<string> updateCurrentFolder, Action<string, string> updateAverages)
        {
            imageProcessor = new MediaProcessor(updateAverages, updateValues);
            _updateCurrentFolder = updateCurrentFolder;
        }

        public void StartTraversal(string folder)
        {
            stop = false;
            DirectoryInfo root = new System.IO.DirectoryInfo(folder);
            Traverse(root);
        }

        internal void Stop()
        {
            stop = true;
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
            if (stop)
            {
                return;
            }

            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    _updateCurrentFolder(file.FullName);

                    if (FileExtensions.IsPhotoFile(file.FullName))
                    {
                        imageProcessor.StoreImageData(file);
                    }
                    if (FileExtensions.IsMediaFile(file.FullName))
                    {
                        //Logic for videos
                        imageProcessor.StoreImageData(file);
                    }
                    else
                    {
                        unsupportedFiles.Add(file);
                    }

                }

                subDirs = root.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    Traverse(subDir);
                }
            }


        }


    }
}
