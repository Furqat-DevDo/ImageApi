using ImagesApi.Entity;

namespace ImagesApi.Services;
public interface IimageService
{
  Task <Image> GetAsync (Guid id);
  Task<List<Image>>GetAllAsync();
  Task<(bool IsSuccess,Exception exception)>DeleteAsync(Guid Id);
  Task<(bool ISsuccess, Exception exception,Image image)>CreateAsync(Image image);
  Task<(bool ISsuccess, Exception exception,IEnumerable<Image> image)>CreateAsync(IEnumerable <Image> image);
  Task<bool>ExsistAsync(Guid id);
}