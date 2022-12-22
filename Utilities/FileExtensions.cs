namespace PhotoOrganiser.Utilities
{
    public static class FileExtensions
    {

        static string[] photoExtensions = {
            ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF",
            ".RAW",".TIF", ".SVG", ".DNP"
        };

        static string[] mediaExtensions = {
            ".WAV", ".WMA",".AVI", ".MP4", ".DIVX", ".WMV", ".MP4",".MOV", ".MKV",
        };

        public static bool IsMediaFile(string path)
        {

            return -1 != Array.IndexOf(mediaExtensions, Path.GetExtension(path).ToUpperInvariant());
        }

        public static bool IsPhotoFile(string path)
        {
            return -1 != Array.IndexOf(photoExtensions, Path.GetExtension(path).ToUpperInvariant());
        }
    }
}
