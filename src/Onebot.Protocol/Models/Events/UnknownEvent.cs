namespace Onebot.Protocol.Models.Events
{
    public record UnknownEvent(string RawData) : IEvent;
}