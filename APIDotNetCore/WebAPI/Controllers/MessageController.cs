using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/message/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IMessage _IMessage;
        private readonly IServiceMessage _IServiceMessage;

        public MessageController(IMapper iMapper, IMessage iMessage, IServiceMessage iServiceMessage)
        {
            _IMapper = iMapper;
            _IMessage = iMessage;
            _IServiceMessage = iServiceMessage;
        }

        private async Task<string> GetLoggedUserId()
        {
            if (User != null)
            {
                var idUser = User.FindFirst("idUser");

                if (idUser != null)
                    return idUser.Value;
            }

            return string.Empty;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/message/Add")]
        public async Task<List<Notifies>> Add(MessageViewModel message)
        {
            message.UserId = await GetLoggedUserId();
            var messageMap = _IMapper.Map<Message>(message);
            await _IServiceMessage.Add(messageMap);
            return messageMap.Notifications;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/message/Update")]
        public async Task<List<Notifies>> Update(MessageViewModel message)
        {
            message.UserId = await GetLoggedUserId();
            var messageMap = _IMapper.Map<Message>(message);
            await _IServiceMessage.Update(messageMap);
            return messageMap.Notifications;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/message/Delete")]
        public async Task<List<Notifies>> Delete(MessageViewModel message)
        {
            message.UserId = await GetLoggedUserId();
            var messageMap = _IMapper.Map<Message>(message);
            await _IServiceMessage.Delete(messageMap);
            return messageMap.Notifications;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/message/GetEntityById")]
        public async Task<MessageViewModel> GetEntityById(Message message)
        {
            message = await _IMessage.GetEntityById(message.Id);
            var messageMap = _IMapper.Map<MessageViewModel>(message);
            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/message/List")]
        public async Task<List<MessageViewModel>> List()
        {
            var messages = await _IMessage.List();
            var messageMap = _IMapper.Map<List<MessageViewModel>>(messages);
            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/message/ListActivesMessages")]
        public async Task<List<MessageViewModel>> ListActivesMessages()
        {
            var messages = await _IServiceMessage.ListActivesMessages();
            var messageMap = _IMapper.Map<List<MessageViewModel>>(messages);
            return messageMap;
        }

    }
}
