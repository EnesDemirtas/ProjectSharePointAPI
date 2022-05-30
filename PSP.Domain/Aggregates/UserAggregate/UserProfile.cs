namespace PSP.Domain.Aggregates.UserAggregate
{

    public class UserProfile
    {

        private UserProfile()
        {
        }

        public Guid UserProfileId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }

        // Factory Method
        public static UserProfile CreateUser(string identityId, BasicInfo basicInfo)
        {
            return new UserProfile
            {
                IdentityId = identityId,
                BasicInfo = basicInfo,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
        }

        // public methods
        public void UpdateBasicInfo(BasicInfo newInfo)
        {
            BasicInfo = newInfo;
            LastModified = DateTime.UtcNow;
        }
    }
}