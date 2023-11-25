using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webcomic.Models.DTOs.TagDtos;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (tagToCreate == null)
                {
                    return BadRequest("Tag data is invalid or empty.");
                }

                // Kiểm tra xem tên tag đã tồn tại trong DB chưa
                Tag existingTag = await _tagService.GetTagByNameAsync(tagToCreate.Name);
                if (existingTag != null)
                {
                    // Nếu tên tag đã tồn tại, trả về thông báo lỗi
                    return BadRequest("Tag with the same name already exists.");
                }

                Tag mappedTag = _mapper.Map<Tag>(tagToCreate);
                
                bool isCreateSuccessful = await _tagService.CreateTagAsync(mappedTag);
                if (!isCreateSuccessful)
                {
                    return StatusCode(500, "Failed to create tag. Please try again later.");
                }

                return StatusCode(201, "Successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the tag: {ex.Message}");
            }
            
        }

        [HttpPut("{tagId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTagAsync(int tagId, [FromBody] TagDto tagToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (tagToUpdate == null)
                {
                    return BadRequest("Tag data is invalid or empty.");
                }


                // Kiểm tra xem truyện tranh có tồn tại không
                Tag existingTag = await _tagService.GetTagByIdAsync(tagId);
                if (existingTag == null)
                {
                    return NotFound();
                }

                // Cập nhật dữ liệu từ DTO vào truyện tranh hiện có
                Tag mappedTag = _mapper.Map<TagDto,Tag>(tagToUpdate, existingTag);

                // Gọi service để cập nhật truyện tranh
                bool isUpdateSuccessful = await _tagService.UpdateTagAsync(mappedTag);
                if (!isUpdateSuccessful)
                {
                    return StatusCode(500, "Failed to update tag. Please try again later.");
                }

                return Ok("Successfully updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the tag: {ex.Message}");
            }
            
        }

        [HttpDelete("comicId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTagAsync(int tagId)
        {
            try
            {
                Tag existingTag = await _tagService.GetTagByIdAsync(tagId);
                if (existingTag == null)
                {
                    return NotFound();
                }

                bool isDeleteSuccessful = await _tagService.DeleteTagAsync(existingTag);
                if (!isDeleteSuccessful)
                {
                    return StatusCode(500, "Failed to delete tag. Please try again later.");
                }

                return Ok("Successfully deleted.");
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ trong quá trình xóa
                return StatusCode(500, $"An error occurred while deleting the tag: {ex.Message}");
            }
        }
    }
}
