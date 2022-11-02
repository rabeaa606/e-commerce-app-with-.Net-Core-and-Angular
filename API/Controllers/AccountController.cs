using Address = Core.Entites.Identity.Address;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenServcice _tokenServcice;
    private readonly IMapper _mapper;

    public AccountController(UserManager<AppUser> userManger,
     SignInManager<AppUser> signInManager,
     ITokenServcice tokenServcice, IMapper mapper)
    {
        _tokenServcice = tokenServcice;
        _userManager = userManger;
        _signInManager = signInManager;
        _mapper = mapper;
    }
    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {

        return await _userManager.FindByEmailAsync(email) != null;
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {

        var user = await _userManager.FindByEmailFromClaimsPrincipalAsync(User);
        return new UserDto
        {
            Email = user.Email,
            Token = _tokenServcice.CreateToken(user),
            DisplayName = user.DisplayName
        };
    }


    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AddressDto>> GetUserAddres()
    {
        var user = await _userManager.FindByEmailWithAddressAsync(User);

        return Ok(_mapper.Map<Address, AddressDto>(user.Address));
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDto>> UpdateUserAddres(AddressDto addressDto)
    {
        var user = await _userManager.FindByEmailWithAddressAsync(User);

        user.Address = _mapper.Map<AddressDto, Address>(addressDto);

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

        return BadRequest("Problem updating the user Address");
    }
    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized(new ApiResponse(401));
        }

        var result = await _signInManager
             .CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized(new ApiResponse(401));

        }
        return new UserDto
        {
            Email = user.Email,
            Token = _tokenServcice.CreateToken(user),
            DisplayName = user.DisplayName
        };


    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
        {
            return new BadRequestObjectResult(new ApiValidationErrorResponse
            {
                Errors = new[]
            { "Email addres is in use" }
            });
        }

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(new ApiResponse(400));

        //  var roleResult = await _userManager.AddToRoleAsync(user, "Member");

        return new UserDto
        {
            Email = user.Email,
            Token = _tokenServcice.CreateToken(user),
            DisplayName = user.DisplayName
        };

    }
}

