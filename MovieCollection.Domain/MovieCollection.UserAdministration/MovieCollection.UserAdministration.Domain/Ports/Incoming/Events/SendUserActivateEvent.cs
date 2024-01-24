namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Events
{
    public class SendUserActivateEvent : IEvent
    {
        public int UserId { get; set; }
        public string Email { get; set; }

        public SendUserActivateEvent(int userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
