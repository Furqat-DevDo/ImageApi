using ImagesApi.Entity;
using ImagesApi.Model;

namespace ImagesApi.Mapper;
public static class ImageModelToEntity
{
       public static Image ToEntity(this NewImage newimage)
           => new Image(
               title: newimage.Title,
               contentType: newimage.ContentType,
               altText: newimage.ContentType,
               data: ToByte(newimage.Data)
           );
        public static byte[] ToByte(IFormFile file)
        {
            using(var fileStream = new MemoryStream())
            {
                file.CopyTo(fileStream);

                var result = new byte[fileStream.Length];
                        fileStream.Read(result, 0, result.Length);
                
                return result;
            }
        }
        
}