using System.Threading.Tasks;

namespace SoundHub.Web.Services
{
    public class NotificationService
    {
        public Task SendNewPostNotificationAsync(int postId)
        {
            // Stub: en producción implementar llamada a FCM u otro servicio
            return Task.CompletedTask;
        }
    }
}

