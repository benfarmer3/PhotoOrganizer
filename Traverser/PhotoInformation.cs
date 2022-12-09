using PhotoOrganiser.Utilities;

namespace PhotoOrganiser.Traverser
{
    public class PhotoInformation 
    {
        public string hash { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
        public string path { get; set; }
        public bool duplicate { get; set; }
        public bool noExif { get; set; }

        public PhotoInformation(string hash, string name, string extension, string path, bool duplicate = false, bool noExif = false)
        {
            this.name = name;
            this.extension = extension;
            this.path = path;
            this.duplicate = duplicate;
            this.noExif = noExif;
            this.hash = hash;

        } 
    }
}
