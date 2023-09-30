using OutBoxPattern.Sample.Models;

namespace OutBoxPattern.Sample.Services;

public interface IEmailOutbox
{
    Task<EmailOutbox> Add(EmailOutbox emailOutbox);
    Task<EmailOutbox> Update(EmailOutbox emailOutbox);
    IEnumerable<EmailOutbox> GetAll();
}