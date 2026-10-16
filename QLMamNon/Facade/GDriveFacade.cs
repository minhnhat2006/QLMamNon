using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace QLMamNon.Facade
{
    public static class GDriveFacade
    {
        // If modifying these scopes, delete your previously saved credentials at ~/.credentials/gdrive-qlmn.json
        static string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
        static string ApplicationName = "QLMN";

        public static string UploadFile(string targetPath, string fileToUpload)
        {
            UserCredential credential;
            // Đường dẫn tuyệt đối theo thư mục cài đặt, không phụ thuộc thư mục làm việc của shortcut.
            string clientSecretPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "client_secret.json");
            using (var stream = new FileStream(clientSecretPath, FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gdrive-qlmn.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // Target folder to upload
            string folderId = "17ExgdDHHag_np8Zy8HhjCo3rtbOcpclw";
            // Upload file
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(fileToUpload),
                Parents = new List<string>
                {
                    folderId
                }

            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(fileToUpload, FileMode.Open, FileAccess.Read))
            {
                request = service.Files.Create(fileMetadata, stream, "text/plain");
                request.Fields = "id";
                IUploadProgress uploadResult = request.Upload();

                if (uploadResult.Status != UploadStatus.Completed)
                {
                    throw new IOException("Tải file sao lưu lên Google Drive không thành công.", uploadResult.Exception);
                }
            }

            var file = request.ResponseBody;
            return file.Id;
        }
    }
}
