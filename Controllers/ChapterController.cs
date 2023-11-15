using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webcomic.Models.DTOs;
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

        public ChapterController(IChapterService chapterService,
            IComicService comicService,
            IMapper mapper)
        {
            _chapterService = chapterService;
            _comicService = comicService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllChapters()
        {
            var chapters = _mapper.Map<List<ChapterDto>>(_chapterService.GetAllChapters());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(chapters);
        }

        [HttpGet("{chapterId}")]
        public IActionResult GetChapterById(int chapterId)
        {
            var chapter = _mapper.Map<List<ChapterDto>>(_chapterService.GetChapterById(chapterId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(chapter);
        }

        [HttpPost]
        public IActionResult CreateChapter(int comicId, [FromBody] ChapterDto chapterCreate)
        {
            if (chapterCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chapterMap = _mapper.Map<Chapter>(chapterCreate);

            if (!_chapterService.CreateChapter(chapterMap, comicId))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{chapterId}")]
        public IActionResult UpdateChapter([FromQuery] int chapterId, [FromQuery] int comicId,
            [FromBody] ChapterDto updatedChapter)
        {
            if (updatedChapter == null)
            {
                return BadRequest(ModelState);
            }
            if (chapterId != updatedChapter.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var chapterMap = _mapper.Map<Chapter>(updatedChapter);
            if (!_chapterService.UpdateChapter(chapterMap, comicId))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete("chapterId")]
        public IActionResult DeleteChapter([FromQuery] int chapterId, [FromQuery] int comicId)
        {
            if (!_chapterService.ChapterExists(chapterId))
            {
                return NotFound();
            }

            var chapterToDelete = _chapterService.GetChapterById(chapterId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_chapterService.DeleteChapter(chapterToDelete, comicId))
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return Ok();
        }


    }
}
