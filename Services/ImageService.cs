using ImagesApi.Data;
using ImagesApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace ImagesApi.Services
{
    public class ImageService : IimageService
    {
        private readonly ImageDbContext _cont;

        public ImageService(ImageDbContext context)
        {
           _cont=context; 
        }
        public async Task<(bool ISsuccess, Exception exception,Image image)> CreateAsync(Image image)
        {
            try
            {
            await _cont.Images.AddAsync(image);
            await _cont.SaveChangesAsync();
             return (true, null, image);
            }
            catch(Exception e)
            {
             return (false, e, null);
            }
        }

        public async Task<(bool ISsuccess, Exception exception, IEnumerable<Image> image)> CreateAsync(IEnumerable<Image> image)
        {
         try
          {
            await _cont.Images.AddRangeAsync(image);
            await _cont.SaveChangesAsync();

            return (true, null,image);
          }
          catch(Exception e)
          {
            return (false, e,null);
          }
        }

        public async Task<(bool IsSuccess, Exception exception)> DeleteAsync(Guid Id)
        {  
          var image =await GetAsync(Id);
           
            if(await ExsistAsync(image.Id))
            {
              _cont.Images.Remove(image);
              await _cont.SaveChangesAsync();
              return(true,null);
            }

            {
             return (false,new Exception("Image was not found."));
            }
        }
        public Task<bool> ExsistAsync(Guid id)
         =>_cont.Images.AnyAsync( a=>a.Id==id);

        public Task<List<Image>> GetAllAsync()
         =>_cont.Images.ToListAsync();

        public Task<Image> GetAsync(Guid id)
         =>_cont.Images.FirstOrDefaultAsync(i=> i.Id==id);
    }
}