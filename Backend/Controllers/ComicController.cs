using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Webcomic.Helpers.Pagination;
using Webcomic.Models.DTOs;
using Webcomic.Models.DTOs.ComicDtos;
using Webcomic.Models.Entities;
using Webcomic.Services.Interfaces;

namespace Webcomic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicController : ControllerBase
    {
        private readonly IComicService _comicService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        private const int DefaultPageSize = 10;

        public ComicController(IComicService comicService,
            ITagService tagService,
            IMapper mapper)
        {
            _comicService = comicService;
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet("comics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllComics(int page = 1)
        {
            var comicsQuery = _comicService.GetAllComicsAsQueryable();

            var paginatedList = await PaginatedList<ListComicDto>
                .Create<Comic, ListComicDto>(comicsQuery, page, DefaultPageSize, _mapper);
            
            if (paginatedList == null || paginatedList.Count == 0)
            {
                return NotFound();
            }

            var result = new PageResult<ListComicDto>
            {
                TotalCount = paginatedList.TotalCount,
                PageNumber = paginatedList.PageNumber,
                TotalPages = paginatedList.TotalPages,
                Items = paginatedList
            };

            return Ok(result);
        }

        [HttpGet("{comicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComicById(int comicId)
        {
            Comic comic = await _comicService.GetComicByIdAsync(comicId);

            if(comic == null)
            {
                return NotFound();
            }

            DetailComicDto mappedComic = _mapper.Map<DetailComicDto>(comic);

            return Ok(mappedComic);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComic([FromBody] CreateComicDto comicToCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (comicToCreate == null)
            {
                return BadRequest("Comic data is invalid or empty.");
            }

            //Thông thường Tag hiện ra để chọn sẽ có trong db
            //Kiểm tra sự tồn tại của các TagIds trong danh sách đã chọn(Có thể bỏ)
            List<int> nonExistentTags = new List<int>();
            if (comicToCreate.SelectedTagIds != null && comicToCreate.SelectedTagIds.Any())
            {
                foreach (int tagId in comicToCreate.SelectedTagIds)
                {
                    bool tagExists = await _tagService.TagExistsAsync(tagId);
                    if (!tagExists)
                    {
                        nonExistentTags.Add(tagId);
                    }
                }
            }
            if (nonExistentTags.Any())
            {
                //Xử lý các Tag không tồn tại ở đây(ví dụ: thông báo lỗi)
                return BadRequest($"The following TagIds do not exist: {string.Join(", ", nonExistentTags)}");
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
        public async Task<IActionResult> UpdateComic(int comicId, [FromBody] UpdateComicDto comicToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (comicToUpdate == null)
            {
                return BadRequest("Comic data is invalid or empty.");
            }

            // Thông thường Tag hiện ra để chọn sẽ có trong db 
            // Kiểm tra sự tồn tại của các TagIds trong danh sách đã chọn (Có thể bỏ)
            List<int> nonExistentTags = new List<int>();
            if (comicToUpdate.SelectedTagIds != null && comicToUpdate.SelectedTagIds.Any())
            {
                foreach (int tagId in comicToUpdate.SelectedTagIds)
                {
                    bool tagExists = await _tagService.TagExistsAsync(tagId);
                    if (!tagExists)
                    {
                        nonExistentTags.Add(tagId);
                    }
                }
            }
            if (nonExistentTags.Any())
            {
                // Xử lý các Tag không tồn tại ở đây (ví dụ: thông báo lỗi)
                return BadRequest($"The following TagIds do not exist: {string.Join(", ", nonExistentTags)}");
            }

            // Lấy existingComic từ database
            Comic existingComic = await _comicService.GetComicByIdAsync(comicId);
            if (existingComic == null)
            {
                return NotFound();
            }

            // Cập nhật dữ liệu từ DTO vào truyện tranh hiện có
            Comic mappedComic = _mapper.Map<UpdateComicDto, Comic>(comicToUpdate, existingComic);

            // Gọi service để cập nhật truyện tranh
            bool isUpdateSuccessful = await _comicService.UpdateComicAsync(mappedComic);
            if (!isUpdateSuccessful)
            {
                return StatusCode(500, "Failed to update comic. Please try again later.");
            }

            return Ok("Successfully updated.");
        }

        [HttpDelete("{comicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComic(int comicId)
        {

            Comic existingComic = await _comicService.GetComicByIdAsync(comicId);
            if (existingComic == null)
            {
                return NotFound();
            }

            bool isDeleteSuccessful = await _comicService.DeleteComicAsync(existingComic);
            if (!isDeleteSuccessful)
            {
                return StatusCode(500, "Failed to delete comic. Please try again later.");
            }

            return Ok("Successfully deleted.");
        }
    }
}
