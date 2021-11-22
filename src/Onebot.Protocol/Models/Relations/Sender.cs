namespace Onebot.Protocol.Models.Relations
{
    public record Sender(long UserId, string Nickname, string Card, Gender Sex, int Age, string Area, string Level,
        Role Role, string Title);

    public enum Gender
    {
        Male,
        Female,
        Unknown
    }

    public enum Role
    {
        Administrator,
        Owner,
        Member
    }
}