namespace OutBoxPattern.Sample.Services;

public interface IMailService
{
    bool Send(string sender, string subject, string body, bool isBodyHTML);
}