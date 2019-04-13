using System;

namespace TempFileServer.Models
{
    public class Pack
    {
        public Pack()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

        public int id { get; set; }
        public string name { get; set; }
        public int sequence { get; set; }
        public int main_pack { get; set; }
        public int? pack_id { get; set; }
        public int content_type_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}