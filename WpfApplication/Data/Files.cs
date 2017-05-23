using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Data
{
    public class Files
    {
        public Files(string path)
        {
            FilePath = path;
            Extension = Path.GetExtension(path);
            DirectoryPath = Path.GetDirectoryName(path);
            CurrentName = Path.GetFileNameWithoutExtension(path);
        }
        public string FilePath { get; set; }
        public string Extension { get; set; }
        public string DirectoryPath { get; set; }
        public string CurrentName { get; set; }
        public string NewName { get; set; }
    }
}
