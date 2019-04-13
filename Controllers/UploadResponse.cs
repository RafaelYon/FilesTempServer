using System;

namespace TempFileServer.Models
{
    public class UploadResponse
    {
        public string upload_src { get; set; }
        public File file_data { get; set; }
    }
}