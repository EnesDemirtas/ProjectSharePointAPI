namespace PSP.Domain.Aggregates.UserAggregate {

    public class UserProfile {

        private UserProfile() {
        }

        public Guid UserProfileId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }

        // Factory Method
        public static UserProfile CreateUser(string identityId, BasicInfo basicInfo) {
            return new UserProfile {
                IdentityId = identityId,
                BasicInfo = basicInfo
            };
        }

        // public methods
        public void UpdateBasicInfo(BasicInfo newInfo) {
            BasicInfo = newInfo;
        }
    }
}