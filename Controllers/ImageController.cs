using System.Net.Mime;
using ImagesApi.Entity;
using ImagesApi.Mapper;
using ImagesApi.Model;
using ImagesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImagesApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ImageController:ControllerBase
{
    private readonly ILogger<ImageController> _log;
    private readonly IimageService _iser;

    public ImageController ( IimageService iser,ILogger<ImageController> logger )
    {
        _log=logger;
        _iser=iser; 
    }
    
    [HttpPost]
    public async Task<IActionResult> PostImagesAsync( [FromForm]NewImage image)
    {
        var extensions = new string[] { ".jpg", ".png", ".svg", ".mp4" };
        var fileSize = 5242880; // 5MB in bytes

        if(image.Data == null)
        {
            return BadRequest();
        }
        var fileExtension = Path.GetExtension(image.Data.FileName).ToLowerInvariant();
        if(!extensions.Contains(fileExtension))
        {
            return BadRequest($"{fileExtension} format file not allowed!");
        }

        if(image.Data.Length > fileSize)
        {
            return BadRequest($"Max file size 5MB!");
        }

        var imageEntity = image.ToEntity();        
        await  _iser.CreateAsync(imageEntity);
        
        return Ok(imageEntity);
        // var filesArray = files.Select(f => 
        // {
        //     using var stream = new MemoryStream();
        //     f.CopyTo(stream);

        //     return stream.ToArray();
        // }).ToList();

        // return File(new MemoryStream(filesArray[0]), files.First().ContentType);
    }
    [HttpGet("{imageId}")]
    public async Task<IActionResult> GetImageAsync( Guid imageId)
    {
        if(!await _iser.ExsistAsync(imageId))
        {
            return NotFound();
        }

        var image = await _iser.GetAsync(imageId);

        return File(new MemoryStream(image.Data), image.ContentType);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllImageAsync()
    {
      
        var images = await _iser.GetAllAsync();

        return Ok(images.Select(i =>
        {
            return new 
            {
                ContentType = i.ContentType,
                Size = i.Data.Length,
                Id = i.Id
            };
        }));
    }
    [HttpDelete("{imageId}")]
    public async Task<IActionResult>DeleteImageAsync(Guid ImageId)
    {
      if(await _iser.ExsistAsync(ImageId))
      {
         await _iser.DeleteAsync(ImageId);
         return Ok();
      }
      return BadRequest();
    }


    





}