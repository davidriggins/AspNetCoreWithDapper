using Microsoft.Net.Http.Headers;
using System.Net.Mime;

namespace DapperFantom.Helpers
{
    public class UploadHelpers
    {
        private readonly IWebHostEnvironment environment;

        public UploadHelpers(IWebHostEnvironment hosting)
        {
            environment = hosting;
        }

        public async Task<string> Upload(IFormFile file)
        {
            string result = "";
            if (file.Length> 0)
            {
                try
                {
                    string fileName = string.Empty;
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                    var fileExtension = Path.GetExtension(fileName);
                    var folderPath = Path.Combine(environment.WebRootPath + "/upload/");
                    var newFileName = myUniqueFileName + fileExtension;
                    fileName = Path.Combine(folderPath, newFileName);
                    using (FileStream fileStream = File.Create(fileName))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Dispose();
                    }

                    result = newFileName;
                }
                catch (Exception)
                {
                }
            }

            return result;
        }
    }
}
