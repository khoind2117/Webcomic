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
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTagsAsync()
        {
            IEnumerable<Tag> tags = await _tagService.GetAllTagsAsync();
            if (tags == null || !tags.Any())
            {
                return NotFound();
            }

            List<TagDto> mappedTags = _mapper.Map<List<TagDto>>(tags);

            return Ok(mappedTags);
        }

        [HttpGet("{tagId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTagByIdAsync(int tagId)
        {
            Tag tag = await _tagService.GetTagByIdAsync(tagId);

            if (tag == null)
            {
                return NotFound();
            }

            TagDto mappedTag = _mapper.Map<TagDto>(tag);

            return Ok(mappedTag);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTagAsync([FromBody] TagDto tagToCreate)
        {
            if (tagToCreate == null)
            {
                return BadRequest("Tag data is invalid or empty.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tag mappedTag = _mapper.Map<Tag>(tagToCreate);
            bool isCreateSuccessful = await _tagService.CreateTagAsync(mappedTag);

            if (!isCreateSuccessful)
            {
                return StatusCode(500, "Failed to create tag. Please try again later.");
            }
            return StatusCode(201, "Successfully created.");
        }

        [HttpPut("{tagId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTagAsync(int tagId, [FromBody] TagDto tagToUpdate)
        {
            // Kiểm tra Id có khớp không
            if (tagId != tagToUpdate.Id)
            {
                return BadRequest("The provided ID does not match the tag to update.");
            }

            if (tagToUpdate == null)
            {
                return BadRequest("Tag data is invalid or empty.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra xem truyện tranh có tồn tại không
            bool isTagExisting = await _tagService.TagExistsAsync(tagId);
            if (!isTagExisting)
            {
                return NotFound();
            }

            // Cập nhật dữ liệu từ DTO vào truyện tranh hiện có
            Tag mappedTag = _mapper.Map<Tag>(tagToUpdate);

            // Gọi service để cập nhật truyện tranh
            bool isUpdateSuccessful = await _tagService.UpdateTagAsync(mappedTag);

            if (!isUpdateSuccessful)
            {
                return StatusCode(500, "Failed to update tag. Please try again later.");
            }

            return Ok("Successfully updated.");
        }

        [HttpDelete("comicId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTagAsync(int tagId)
        {
            bool isTagExisting = await _tagService.TagExistsAsync(tagId);
            if (!isTagExisting)
            {
                return NotFound();
            }

            Tag tagToDelete = await _tagService.GetTagByIdAsync(tagId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isDeleteSuccessful = await _tagService.DeleteTagAsync(tagToDelete);
            if (!isDeleteSuccessful)
            {
                return StatusCode(500, "Failed to delete tag. Please try again later.");
            }

            return Ok("Successfully deleted.");
        }
    }
}
