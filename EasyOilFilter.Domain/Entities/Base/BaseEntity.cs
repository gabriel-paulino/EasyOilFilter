using Flunt.Notifications;

namespace EasyOilFilter.Domain.Entities.Base
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }
    }
}
