using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Webcomic.Helpers.Pagination;
using Webcomic.Models.DTOs;
using Webcomic.Models.DTOs.AppUserFavoriteComic;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        private const int DefaultPageSize = 10;

        public ComicController(IComicService comicService,
            ITagService tagService,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _comicService = comicService;
            _tagService = tagService;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("get-all-comics")]
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

        // Lấy truyện theo Id
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

        // Thêm truyện
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComic([FromForm] CreateComicDto comicToCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (comicToCreate == null || comicToCreate.ImageFile == null || comicToCreate.ImageFile.Length == 0)
            {
                return BadRequest("Comic data or image file is invalid or empty.");
            }

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
                return BadRequest($"The following TagIds do not exist: {string.Join(", ", nonExistentTags)}");
            }

            string imgName = string.Empty;
            if (comicToCreate.ImageFile != null && comicToCreate.ImageFile.Length > 0)
            {
                string path = Path.Combine(_hostEnvironment.ContentRootPath, "Resources", "Images");
                imgName = Guid.NewGuid().ToString("N") + ".png";

                using (var fileStream = new FileStream(Path.Combine(path, imgName), FileMode.Create))
                {
                    await comicToCreate.ImageFile.CopyToAsync(fileStream);
                }
            }

            Comic mappedComic = _mapper.Map<Comic>(comicToCreate);
            mappedComic.Image = imgName;

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
        public async Task<IActionResult> UpdateComic(int comicId, [FromForm] UpdateComicDto comicToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (comicToUpdate == null)
            {
                return BadRequest("Comic data is invalid or empty.");
            }

            // Kiểm tra sự tồn tại của các TagIds trong danh sách đã chọn
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
                return BadRequest($"The following TagIds do not exist: {string.Join(", ", nonExistentTags)}");
            }

            // Lấy existingComic từ database
            Comic existingComic = await _comicService.GetComicByIdAsync(comicId);
            if (existingComic == null)
            {
                return NotFound();
            }

            string oldImgName = existingComic.Image; // Lưu tên ảnh cũ

            string imgName = string.Empty;
            if (comicToUpdate.ImageFile != null && comicToUpdate.ImageFile.Length > 0)
            {
                string path = Path.Combine(_hostEnvironment.ContentRootPath, "Resources", "Images");
                imgName = Guid.NewGuid().ToString("N") + ".png";

                using (var fileStream = new FileStream(Path.Combine(path, imgName), FileMode.Create))
                {
                    await comicToUpdate.ImageFile.CopyToAsync(fileStream);
                }
            }

            // Xóa ảnh cũ từ thư mục
            if (!string.IsNullOrEmpty(oldImgName))
            {
                string oldImgPath = Path.Combine(_hostEnvironment.ContentRootPath, "Resources", "Images", oldImgName);
                if (System.IO.File.Exists(oldImgPath))
                {
                    System.IO.File.Delete(oldImgPath);
                }
            }

            // Cập nhật dữ liệu từ DTO vào truyện tranh hiện có
            Comic mappedComic = _mapper.Map<UpdateComicDto, Comic>(comicToUpdate, existingComic);
            mappedComic.Image = imgName;

            // Gọi service để cập nhật truyện tranh
            bool isUpdateSuccessful = await _comicService.UpdateComicAsync(mappedComic);
            if (!isUpdateSuccessful)
            {
                return StatusCode(500, "Failed to update comic. Please try again later.");
            }

            return Ok("Successfully updated.");
        }

        // Xóa truyện
        [HttpDelete("{comicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComic(int comicId)
        {
            Comic existingComic = await _comicService.GetComicByIdAsync(comicId);
            if (existingComic == null)
            {
                return NotFound();
            }

            string imgName = existingComic.Image; // Lưu tên ảnh cần xóa

            bool isDeleteSuccessful = await _comicService.DeleteComicAsync(existingComic);
            if (!isDeleteSuccessful)
            {
                return StatusCode(500, "Failed to delete comic. Please try again later.");
            }

            // Xóa ảnh từ thư mục
            if (!string.IsNullOrEmpty(imgName))
            {
                string imgPath = Path.Combine(_hostEnvironment.ContentRootPath, "Resources", "Images", imgName);
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
            }

            return Ok("Successfully deleted.");
        }

        // Lấy 15 comic không trùng nhau có chương mới cập nhật
        [Route("get-15-comics-with-lastest-chapter")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLatestComicsWithLatestChapter()
        {
            try
            {
                IEnumerable<Comic> comics = await _comicService.GetLatestComicsWithLatestChapter();

                if (comics == null)
                {
                    return NotFound();
                }

                List<ListComicDto> mappedComic = _mapper.Map<List<ListComicDto>>(comics);

                return Ok(mappedComic);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Lấy tất cả truyện theo thể loại và phân trang (10 items / 1 page)
        [Route("get-all-comics-by-tag")]
        [HttpGet]
        public async Task<IActionResult> GetAllComicByTag(int tagId, int page = 1)
        {
            try
            {
                var comicsQuery = _comicService.GetAllComicByTagAsQueryable(tagId);

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Yêu thích truyện
        [Route("save-favorite-comic")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SaveFavoriteComic(AppUserFavoriteComicDto favoriteComicDto)
        {
            try
            {
                if (string.IsNullOrEmpty(favoriteComicDto.AppUserId) || favoriteComicDto.ComicId == 0)
                {
                    return BadRequest("Invalid data for favorite comic");
                }

                bool isComicExisting = await _comicService.ComicExistsAsync(favoriteComicDto.ComicId);
                if (!isComicExisting)
                {
                    return NotFound("Comic does not exist");
                }

                AppUserFavoriteComic favoriteComic = _mapper.Map<AppUserFavoriteComic>(favoriteComicDto);

                // Call service to save favorite comic
                bool isSavingSuccessful = await _comicService.SaveFavoriteComic(favoriteComic);

                if (isSavingSuccessful)
                {
                    return Ok("Saved favorite comic successfully.");
                }
                else
                {
                    return NotFound("User or comic not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Hủy yêu thích truyện
        [Route("unsave-favorite-comic")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnsaveFavoriteComic(AppUserFavoriteComicDto favoriteComicDto)
        {
            try
            {
                if (string.IsNullOrEmpty(favoriteComicDto.AppUserId) || favoriteComicDto.ComicId == 0)
                {
                    return BadRequest("Invalid data for favorite comic");
                }

                bool isComicExisting = await _comicService.ComicExistsAsync(favoriteComicDto.ComicId);
                if (!isComicExisting)
                {
                    return NotFound("Comic does not exist");
                }

                AppUserFavoriteComic favoriteComic = _mapper.Map<AppUserFavoriteComic>(favoriteComicDto);

                // Call service to unsave favorite comic
                bool isSavingSuccessful = await _comicService.UnsaveFavoriteComic(favoriteComic);

                if (isSavingSuccessful)
                {
                    return Ok("Unsaved favorite comic successfully.");
                }
                else
                {
                    return NotFound("User or comic not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
