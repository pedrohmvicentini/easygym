using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _IMessage;

        public ServiceMessage(IMessage iMessage)
        {
            _IMessage = iMessage;
        }

        public async Task Add(Message message)
        {
            var isValid = message.ValidateStringValue(message.Title, "Title");
            if (isValid)
            {
                message.CreatedAt = DateTime.Now;
                message.UpdatedAt = DateTime.Now;
                message.Active = true;
                await _IMessage.Add(message);
            }
        }

        public async Task Update(Message message)
        {
            var isValid = message.ValidateStringValue(message.Title, "Title");
            if (isValid)
            {
                message.UpdatedAt = DateTime.Now;
                await _IMessage.Update(message);
            }
        }

        public async Task Delete(Message message)
        {
            if (message!= null && message.Id > 0)
            {
                Message data = await _IMessage.GetEntityById(message.Id);

                if (data != null)
                {
                    data.Active = false;
                    data.DeletedAt = DateTime.Now;
                    await _IMessage.Update(data);
                }
            }
        }

        public async Task<List<Message>> ListActivesMessages()
        {
            return await _IMessage.ListMessages(n => n.Active);
        }
    }
}
