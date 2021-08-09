namespace Farsight.IdentityService.Models.Responses
{
    public class UserInfoUpdated
    {
        public string DisplayName { get; set; }

        public UserInfoUpdated(string displayName)
        {
            DisplayName = displayName;
        }
    }
}