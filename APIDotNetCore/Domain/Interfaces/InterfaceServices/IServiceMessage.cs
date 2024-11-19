using Entities.Entities;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceMessage
    {
        Task Add(Message message);
        Task Update(Message message);
        Task Delete(Message message);
        Task<List<Message>> ListActivesMessages();
    }
}
