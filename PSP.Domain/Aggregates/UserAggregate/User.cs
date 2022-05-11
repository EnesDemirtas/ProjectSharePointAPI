using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Domain.Aggregates.UserAggregate {

    public class User {

        private User() {
        }

        public Guid UserId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }

        // Factory Method
        public static User CreateUser(string identityId, BasicInfo basicInfo) {
            return new User {
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