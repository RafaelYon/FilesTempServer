using System;

namespace TempFileServer.Models
{
    public class File
    {
        public File()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

        public int id { get; set; }
        public int content_type_id { get; set; }
        public int pack_id { get; set; }
        public string name { get; set; }
        public int sequence { get; set; }
        public int processed { get; set; }
        public int file_size { get; set; }
        public string file_md5 { get; set; }
        public string file_name  { get; set; }
        public string extension { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}