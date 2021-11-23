using ImagesApi.Entity;
using ImagesApi.Model;

namespace ImagesApi.Mapper;
public static class ImageModelToEntity
{
       public static Image ToEntity(NewImage newimage)
            => new Image()
            {
                Id = Guid.NewGuid(),
                Title =newimage.Title,
                ContentType=newimage.ContentType
            };
}