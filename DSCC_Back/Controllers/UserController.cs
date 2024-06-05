using AutoMapper;
using DSCC_Back.DTO;
using DSCC_Back.Interfaces;
using DSCC_Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSCC_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserReadDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserReadDto>(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateUser(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            await _userRepository.AddUserAsync(user);

            var userReadDto = _mapper.Map<UserReadDto>(user);

            return CreatedAtRoute("GetUserById", new { id = userReadDto.UserId }, userReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(userUpdateDto, user);

            await _userRepository.UpdateUserAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
