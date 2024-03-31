namespace IraqWebsite.AuthManager.Models
{
    public class UserLockout
    {
        public Guid Id { get; set; }
        public bool AccountLocking { get; set; }
        public int LoginAttemptCount { get; set; }
        public int AccountLockingDuration { get; set; }
    }
}
