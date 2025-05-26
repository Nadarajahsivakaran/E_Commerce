using AutoMapper;
using E_Commerce_API.DataAccess.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Product.Models;
using Product.Models.DTO;
using Response = Product.Models.DTO.Response;

namespace Product.Controllers.Admin
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    //[Authorize(Roles = "ADMIN, SUPERADMIN")]
    public class CategoryController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private CategoryDTO _categoryDTO = new();
        private readonly Response _response = new();
        private readonly List<string> errors = [];

        #region GetCategories
        [HttpGet]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                IEnumerable<Category> categories = await _unitOfWork.Category.GetAll();
                IEnumerable<CategoryDTO> categoryDTOs = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                _response.Res = categoryDTOs;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion

        #region GetCategoryById
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                Category category = await _unitOfWork.Category.GetData(c => c.Id == id);

                if (category == null)
                    return HandleException(null, StatusCodes.Status404NotFound, $"Category with ID {id} not found.");

                _categoryDTO = _mapper.Map<CategoryDTO>(category);
                _response.Res = _categoryDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion

        #region CreateCategory
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory(CreatedCategoryDTO createdCategoryDTO)
        {
            try
            {
                bool isExit = await _unitOfWork.Category.IsValueExit(c => c.CategoryName == createdCategoryDTO.CategoryName);
                if (isExit)
                    return HandleException(null, StatusCodes.Status409Conflict, "A category with the same name already exists.");

                else
                {
                    Category category = _mapper.Map<Category>(createdCategoryDTO);
                    Category createdCategory = await _unitOfWork.Category.Create(category);
                    _categoryDTO = _mapper.Map<CategoryDTO>(createdCategory);
                    _response.Res = _categoryDTO;
                    return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, _response);
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion

        #region UpdateCategory
        [HttpPut]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                Category category = await _unitOfWork.Category.GetData(c => c.Id == categoryDTO.CategoryId);
                if (category == null)
                    return HandleException(null, StatusCodes.Status404NotFound, $"Category with ID {categoryDTO.CategoryId} not found.");

                bool isExit = await _unitOfWork.Category.IsValueExit(c => c.CategoryName == categoryDTO.CategoryName && c.Id != categoryDTO.CategoryId);
                if (isExit)
                    return HandleException(null, StatusCodes.Status409Conflict, "A category with the same name already exists.");

                Category mappedCategory = _mapper.Map<Category>(categoryDTO);
                Category updatedCategory = await _unitOfWork.Category.Update(mappedCategory);
                _categoryDTO = _mapper.Map<CategoryDTO>(updatedCategory);
                _response.Res = _categoryDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion

        #region DeleteCategory
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                Category category = await _unitOfWork.Category.GetData(c => c.Id == id);

                if (category == null)
                    return HandleException(null, StatusCodes.Status404NotFound, $"Category with ID {id} not found.");

                bool isSucceed = await _unitOfWork.Category.Delete(id);
                if (isSucceed)
                {
                    _response.Message = "Successfully Deleted!";
                    _response.Res = id;
                    return Ok(_response);
                }

                _response.IsSuccess = false;
                _response.Res = isSucceed;
                _response.Message = "Something wrong!";
                _response.StatusCode = 400;
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }
        #endregion

        #region HandleException
        private IActionResult HandleException(Exception? ex, int statusCode = StatusCodes.Status400BadRequest, object? customMessage = null)
        {
            try
            {
                if (ex != null)
                    errors.Add(ex.Message.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = statusCode;
                _response.Message = customMessage ?? errors;

                return statusCode switch
                {
                    StatusCodes.Status401Unauthorized => Unauthorized(_response),
                    StatusCodes.Status403Forbidden => Forbid(),
                    StatusCodes.Status404NotFound => NotFound(_response),
                    StatusCodes.Status409Conflict => Conflict(_response),
                    _ => BadRequest(_response)
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
