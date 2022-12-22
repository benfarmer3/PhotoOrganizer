using System.Diagnostics;

namespace PhotoOrganiser.Utilities
{
    internal class StatsProcessor
    {
        public StatsProcessor(Action<string, string> updateAverage, Action<string, string, string> updateValues)
        {
            _updateAverages = updateAverage;
            _updateValues = updateValues;
        }

        private Action<string, string> _updateAverages;
        private Action<string, string, string> _updateValues;

        private int totalPhotos = 0;
        private int totalNoExifPhotos = 0;
        private int totalDuplicatePhotos = 0;

        private Stopwatch hashTime = new Stopwatch();
        List<TimeSpan> averageNoExif = new List<TimeSpan>();
        List<TimeSpan> averageExif = new List<TimeSpan>();

        private string currentAverage = string.Empty;
        private string currentAverageNoExif = string.Empty;

        public void updateTotalPhotos()
        {
            totalPhotos++;
            _updateValues(totalPhotos.ToString(), totalDuplicatePhotos.ToString(), totalNoExifPhotos.ToString());

        }
        public void updateTotalNoExifPhotos()
        {
            totalNoExifPhotos++;
            _updateValues(totalPhotos.ToString(), totalDuplicatePhotos.ToString(), totalNoExifPhotos.ToString());

        }

        public void updateTotalDuplicatePhotos()
        {
            totalDuplicatePhotos++;
            _updateValues(totalPhotos.ToString(), totalDuplicatePhotos.ToString(), totalNoExifPhotos.ToString());

        }

        public void StartHashAvg()
        {
            if (hashTime.IsRunning)
            {
                hashTime.Reset();
            }
            hashTime.Start();
        }

        public void StopHashAvgNoExif()
        {
            hashTime.Stop();
            averageNoExif.Add(hashTime.Elapsed);
            hashTime.Reset();
        }

        public void StopHashAvg()
        {
            hashTime.Stop();
            averageExif.Add(hashTime.Elapsed);
            hashTime.Reset();
            _updateAverages(GetExifAvg(), GetNoExifAvg());


        }

        public string GetNoExifAvg()
        {
            return Mean(averageNoExif).ToString();

        }
        public string GetExifAvg()
        {
            return Mean(averageExif).ToString();
        }


        public TimeSpan Mean(IEnumerable<TimeSpan> source) => TimeSpan.FromTicks(source.Aggregate((m: 0L, r: 0L, n: source.Count()), (tm, s) =>
        {
            var r = tm.r + s.Ticks % tm.n;
            return (tm.m + s.Ticks / tm.n + r / tm.n, r % tm.n, tm.n);
        }).m);





    }
}
