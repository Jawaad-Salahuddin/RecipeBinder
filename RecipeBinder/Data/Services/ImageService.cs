using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;
using RecipeBinder.Data.Models;

namespace RecipeBinder.Data.Services
{
    public class ImageService
    {
        /// <summary>
        /// Converts uploaded images to byte arrays
        /// </summary>
        public async Task<List<Image>> GetByteArraysAsync(IEnumerable<IFileReference> fileReferences)
        {
            List<Image> images = new List<Image>();
            foreach (var file in fileReferences)
            {
                if (file != null)
                {
                    var fileInfo = await file.ReadFileInfoAsync();
                    if (!fileInfo.Type.StartsWith("image") || fileInfo.Size > 28000000) continue;
                    byte[] result;
                    await using Stream stream = await file.OpenReadAsync();
                    result = new byte[stream.Length];
                    await stream.ReadAsync(result, 0, (int)stream.Length);
                    images.Add(new Image { Content = result });
                }
            }
            return images;
        }

        /// <summary>
        /// Creates image source strings using byte arrays
        /// </summary>
        public async Task<List<string>> GetImageSourcesAsync(List<Image> images)
        {
            List<string> sources = new List<string>();
            await Task.Run(() =>
            {
                foreach (Image image in images)
                {
                    sources.Add($"data:image/png;base64,{Convert.ToBase64String(image.Content)}");
                }
            });
            return sources;
        }
    }
}
