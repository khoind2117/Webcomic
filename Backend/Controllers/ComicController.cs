using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Webcomic.Models.DTOs;
using Webcomic.Models.Entities;
using Webcomic.Services.Interfaces;

namespace Webcomic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicController : ControllerBase
    {
        private readonly IComicService _comicService;
        private readonly IMapper _mapper;

        public ComicController(IComicService comicService,
            IMapper mapper)
        {
            _comicService = comicService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllComicsAsync()
        {
            IEnumerable<Comic> comics = await _comicService.GetAllComicsAsync();
            if (comics == null || !comics.Any())
            {
                return NotFound();
            }

            List<ComicDto> mappedComics = _mapper.Map<List<ComicDto>>(comics);

            return Ok(mappedComics);
        }

        [HttpGet("{comicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComicByIdAsync(int comicId)
        {
            Comic comic = await _comicService.GetComicByIdAsync(comicId);

            if(comic == null)
            {
                return NotFound();
            }

            ComicDto mappedComic = _mapper.Map<ComicDto>(comic);

            return Ok(mappedComic);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComicAsync([FromBody] ComicDto comicToCreate)
        {
            if (comicToCreate == null)
            {
                return BadRequest("Comic data is invalid or empty.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comic mappedComic = _mapper.Map<Comic>(comicToCreate);
            bool isCreateSuccessful = await _comicService.CreateComicAsync(mappedComic);

            if (!isCreateSuccessful)
            {
                return StatusCode(500, "Failed to create comic. Please try again later.");
            }
            return StatusCode(201, "Successfully created.");
        } 

        [HttpPut("{comicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateComicAsync(int comicId, [FromBody] ComicDto comicToUpdate)
        {
            // Kiểm tra Id có khớp không
            if(comicId != comicToUpdate.Id)
            {
                return BadRequest("The provided ID does not match the comic to update.");
            }

            if (comicToUpdate == null)
            {
                return BadRequest("Comic data is invalid or empty.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra xem truyện tranh có tồn tại không
            bool isComicExisting = await _comicService.ComicExistsAsync(comicId);
            if (!isComicExisting)
            {
                return NotFound();
            }

            // Cập nhật dữ liệu từ DTO vào truyện tranh hiện có
            Comic mappedComic = _mapper.Map<Comic>(comicToUpdate);

            // Gọi service để cập nhật truyện tranh
            bool isUpdateSuccessful = await _comicService.UpdateComicAsync(mappedComic);

            if (!isUpdateSuccessful)
            {
                return StatusCode(500, "Failed to update comic. Please try again later.");
            }

            return Ok("Successfully updated.");
        }

        [HttpDelete("comicId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComicAsync(int comicId)
        {
            bool isComicExisting = await _comicService.ComicExistsAsync(comicId);
            if (!isComicExisting)
            {
                return NotFound();
            }

            Comic comicToDelete = await _comicService.GetComicByIdAsync(comicId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isDeleteSuccessful = await _comicService.DeleteComicAsync(comicToDelete);
            if (!isDeleteSuccessful)
            {
                return StatusCode(500, "Failed to delete comic. Please try again later.");
            }

            return Ok("Successfully deleted.");
        }
    }
}
