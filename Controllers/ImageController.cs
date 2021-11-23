using System.Net.Mime;
using ImagesApi.Entity;
using ImagesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImagesApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ImageController:ControllerBase
{
    private readonly IimageService _iser;

    public ImageController ( IimageService iser )
    {
        _iser=iser; 
    }
    
    [HttpPost]
    public async Task<IActionResult> PostImagesAsync( IEnumerable<IFormFile> files)
    {
        var extensions = new string[] { ".jpg", ".png", ".svg", ".mp4" };
        var fileSize = 5242880; // 5MB in bytes

        if(files.Count() < 1 || files.Count() > 5)
        {
            return BadRequest("Can upload 1~5 files at a time.");
        }

        // extension validation
        foreach(var file in files)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if(!extensions.Contains(fileExtension))
            {
                return BadRequest($"{fileExtension} format file not allowed!");
            }

            if(file.Length > fileSize)
            {
                return BadRequest($"Max file size 5MB!");
            }
        }

        var images = files.Select(f => 
        {
            using var stream = new MemoryStream();
            f.CopyTo(stream);

            return new Image()
            {
                Id = Guid.NewGuid(),
                Data = stream.ToArray(),
                Title="IlmHub",
                AltText = "Some examples from ilmhub oj"  
            };
        }).ToList();

        await _iser.CreateAsync(images);

        return Ok(images);
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