using PhotoOrganiser.DataBase;
using PhotoOrganiser.Traverser;

namespace PhotoOrganiser.Utilities
{
    internal class MediaProcessor
    {
        StatsProcessor statsProcessor;
        private HashSet<string> photoDuplicateHash = new HashSet<string>();

        public MediaProcessor(Action<string, string> updateAverages, Action<string, string, string> updateTotal)
        {
            statsProcessor = new StatsProcessor(updateAverages, updateTotal);

        }
        public void StoreImageData(FileInfo image)
        {
            var photoInfo = GetImageInformation(image);
            DataBaseManager.AddFile(photoInfo.hash, photoInfo.name, photoInfo.extension, photoInfo.path, photoInfo.duplicate, photoInfo.noExif);
            statsProcessor.updateTotalPhotos();
        }

        private MediaInformation GetImageInformation(FileInfo file)
        {
            MediaInformation photoInfo;


            try
            {
                statsProcessor.StartHashAvg();
                var hash = Utilities.GenerateHashFromFile(file);
                statsProcessor.StopHashAvg();
                photoInfo = new MediaInformation(hash, file.Name, file.Extension, file.FullName, false, true);

            }
            catch
            {

                statsProcessor.StartHashAvg();
                statsProcessor.updateTotalNoExifPhotos();
                var hash = Utilities.GenerateHashFromFileNoExif(file);
                statsProcessor.StopHashAvgNoExif();
                photoInfo = new MediaInformation(hash, file.Name, file.Extension, file.FullName);

            }

            if (photoDuplicateHash.Contains(photoInfo.hash))
            {
                statsProcessor.updateTotalDuplicatePhotos();
                photoInfo.duplicate = true;
                return photoInfo;
            }
            photoDuplicateHash.Add(photoInfo.hash);
            return photoInfo;
        }
    }
}
