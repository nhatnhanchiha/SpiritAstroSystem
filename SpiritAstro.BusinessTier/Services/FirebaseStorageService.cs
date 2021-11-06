using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Storage;
using SpiritAstro.BusinessTier.Commons.Constants;

namespace SpiritAstro.BusinessTier.Services
{
    
    public class FirebaseStorageService
    {
        public async Task UploadFile()
        {
            // FirebaseStorage.Put method accepts any type of stream.
            var stream = new MemoryStream(Encoding.ASCII.GetBytes("Hello world!"));
            //var stream = File.Open(@"C:\someFile.png", FileMode.Open);

            // of course you can login using other method, not just email+password
            // you can use CancellationTokenSource to cancel the upload midway
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                    FirebaseStorageConstant.Bucket)
                .Child("receipts")
                .Child("test")
                .Child("someFile.png")
                .PutAsync(stream, cancellation.Token);

            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            await task;
            // cancel the upload
            // cancellation.Cancel();
        }

        public async Task<string> UploadFileGolang(byte[] file)
        {
            using (var client = new HttpClient())
            {
                using (var content =
                    new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(new MemoryStream(file)), "upload[]", "upload.jpg");

                    using (
                        var message =
                            await client.PostAsync("http://localhost:8080/uploadNatalChart", content))
                    {
                        var input = await message.Content.ReadAsStringAsync();
                        return input;
                    }
                }
            }
        }
    }
}