using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using Webcomic.Helpers.Pagination;
using Webcomic.Models.DTOs;
using Webcomic.Models.DTOs.ChapterDtos;
using Webcomic.Models.DTOs.ComicDtos;
using Webcomic.Models.Entities;
using Webcomic.Services.Implementatiions;
using Webcomic.Services.Interfaces;

namespace Webcomic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;
        private readonly IComicService _comicService;
        private readonly IMapper _mapper;

        private const int DefaultPageSize = 10;

        public ChapterController(IChapterService chapterService,
            IComicService comicService,
            IMapper mapper)
        {
            _chapterService = chapterService;
            _comicService = comicService;
            _mapper = mapper;
        }

        //[HttpGet("{chapterId}")]
        //public IActionResult GetChapterById(int chapterId)
        //{
        //    var chapter = _mapper.Map<List<ChapterDto>>(_chapterService.GetChapterById(chapterId));

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //   }

        //    return Ok(chapter);
        //}
        [HttpGet("{comicId}/{page}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllChaptersByComicId(int comicId, int page = 1)
        {
            var chaptersQuery = _chapterService.GetAllChaptersByComicIdAsQueryable(comicId);

            var paginatedList = await PaginatedList<ListChapterDto>
                .Create<Chapter, ListChapterDto>(chaptersQuery, page, DefaultPageSize, _mapper);

            if (paginatedList == null || paginatedList.Count == 0)
            {
                return NotFound();
            }

            var result = new PageResult<ListChapterDto>
            {
                TotalCount = paginatedList.TotalCount,
                PageNumber = paginatedList.PageNumber,
                TotalPages = paginatedList.TotalPages,
                Items = paginatedList
            };

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateChapter([FromBody] CreateChapterDto chapterToCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(chapterToCreate == null)
            {
                return BadRequest("Chapter data is invalid or empty.");
            }

            int comicId = chapterToCreate.ComicId;
            // Kiểm tra xem Comic có tồn tại không
            bool isComicExisting = await _comicService.ComicExistsAsync(comicId);
            if (!isComicExisting)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy Comic
            }

            Chapter mappedChapter = _mapper.Map<Chapter>(chapterToCreate);

            bool isCreateSuccessful = await _chapterService.CreateChapterAsync(mappedChapter);
            if (!isCreateSuccessful)
            {
                return StatusCode(500, "Failed to create chapter. Please try again later.");
            }

            return StatusCode(201, "Successfully created.");
        }

        [HttpPut("{chapterId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateChapter(int chapterId, [FromBody] UpdateChapterDto chapterToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (chapterToUpdate == null)
            {
                return BadRequest("Comic data is invalid or empty.");
            }

            Chapter existingChapter = await _chapterService.GetChapterByIdAsync(chapterId);
            if(existingChapter == null)
            {
                return NotFound();
            }

            Chapter mappedChapter = _mapper.Map<UpdateChapterDto, Chapter>(chapterToUpdate, existingChapter);
            
            bool isUpdateSuccessful = await _chapterService.UpdateChapterAsync(mappedChapter);
            if (!isUpdateSuccessful)
            {
                return StatusCode(500, "Failed to update chapter. Please try again later.");
            }

            return Ok("Successfully updated.");
        }

        [HttpDelete("{chapterId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteChapter(int chapterId)
        {

            Chapter existingChapter = await _chapterService.GetChapterByIdAsync(chapterId);
            if (existingChapter == null)
            {
                return NotFound();
            }

            bool isDeleteSuccessful = await _chapterService.DeleteChapterAsync(existingChapter);
            if (!isDeleteSuccessful)
            {
                return StatusCode(500, "Failed to delete chapter. Please try again later.");
            }

            return Ok("Successfully deleted.");
        }
    }
}
