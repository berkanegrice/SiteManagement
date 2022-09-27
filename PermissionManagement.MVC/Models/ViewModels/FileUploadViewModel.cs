using System.Collections.Generic;

namespace PermissionManagement.MVC.Models.ViewModels
{
    public class FileUploadViewModel
    {
        public List<FileOnFileSystemModel> FilesOnFileSystem { get; set; }

        public List<FileOnDatabaseModel> FilesOnDatabase { get; set; }
    }
}