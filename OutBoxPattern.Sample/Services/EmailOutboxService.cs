using Microsoft.EntityFrameworkCore;
using OutBoxPattern.Sample.Models;

namespace OutBoxPattern.Sample.Services;

public class EmailOutboxService : IEmailOutbox
{
    private readonly AppDbContext _appDbContext;
    
    public EmailOutboxService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<EmailOutbox> Add(EmailOutbox emailOutbox)
    {
        if(emailOutbox != null)
        {
            await _appDbContext.EmailOutbox.AddAsync(emailOutbox);
            await _appDbContext.SaveChangesAsync(); 
        }
        return emailOutbox;
    }
    
    public IEnumerable<EmailOutbox> GetAll()
    {
        return _appDbContext.EmailOutbox.Include(o=>o.Order).OrderByDescending(o=>o.CreatedDate).Where(o=>o.Success==false).ToList();
    }
    
    public async Task<EmailOutbox> Update(EmailOutbox emailOutbox)
    {
        var model = await _appDbContext.EmailOutbox.FirstOrDefaultAsync(o=>o.Id ==emailOutbox.Id);
        if(model is not null)
        {
            model.Success = true;
            await _appDbContext.SaveChangesAsync();
        }
        return emailOutbox;
    }
}