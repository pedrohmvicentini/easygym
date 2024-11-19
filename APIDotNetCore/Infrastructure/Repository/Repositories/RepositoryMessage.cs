using Domain.Interfaces;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryMessage : RepositoryGenerics<Message>, IMessage
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        public RepositoryMessage()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Message>> ListMessages(Expression<Func<Message, bool>> expression)
        {
            using (var contextBase = new ContextBase(_OptionsBuilder))
            {
                return await contextBase.Messages.Where(expression).AsNoTracking().ToListAsync();
            }
        }
    }
}
