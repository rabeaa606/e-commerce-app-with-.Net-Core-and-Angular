namespace API.Extensions;
public static class UserManagerExtensions
{
    public static async Task<AppUser> FindByEmailWithAddressAsync(
        this UserManager<AppUser> input,
        ClaimsPrincipal User)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        return await input.Users
                    .Include(x => x.Address)
        .SingleOrDefaultAsync(y => y.Email == email);
    }


    public static async Task<AppUser> FindByEmailFromClaimsPrincipalAsync(
        this UserManager<AppUser> input,
        ClaimsPrincipal User)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        return await input.Users
        .SingleOrDefaultAsync(y => y.Email == email);
    }

}
