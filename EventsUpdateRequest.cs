
namespace Sabio.Models.Requests.Events
{
    public class EventsUpdateRequest: EventsAddRequest, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
