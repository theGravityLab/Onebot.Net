using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Events.Message;
using Onebot.Protocol.Models.Messages;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol
{
    public class OnebotClient
    {
        public IConnection Connection { get; private set; }

        private const int TIMEOUT = 1500;

        public OnebotClient(IConnection connection)
        {
            Connection = connection;
        }

        private async Task<ReceiptBase> SendWithTimeoutAsync(ActionBase action, int limitMillisecond)
        {
            CancellationTokenSource source = new(limitMillisecond);
            try
            {
                var receipt = await Connection.SendAsync(action, source.Token);
                return receipt;
            }
            catch (OperationCanceledException e)
            {
                return null;
            }
        }

        public async Task<GetGroupInfoReceipt> GetGroupInfoAsync(string id)
        {
            var action = new GetGroupInfoAction()
            {
                GroupId = id
            };
            return await SendWithTimeoutAsync(action, TIMEOUT) as GetGroupInfoReceipt;
        }

        public async Task<GetUserInfoReceipt> GetUserInfoAsync(string id)
        {
            var action = new GetUserInfoAction()
            {
                UserId = id
            };
            return await SendWithTimeoutAsync(action, TIMEOUT) as GetUserInfoReceipt;
        }

        public async Task<GetGroupMemberInfoReceipt> GetGroupMemberInfoAsync(string groupId, string userId)
        {
            var action = new GetGroupMemberInfoAction()
            {
                GroupId = groupId,
                UserId = userId
            };
            return await SendWithTimeoutAsync(action, TIMEOUT) as GetGroupMemberInfoReceipt;
        }

        public async Task<GetGroupMemberListReceipt> GetGroupMemberListAsync(string id)
        {
            var action = new GetGroupMemberListAction()
            {
                GroupId = id
            };
            return await SendWithTimeoutAsync(action, TIMEOUT) as GetGroupMemberListReceipt;
        }

        public async Task<SendMessageReceipt> SendPrivateMessageAsync(string userId, Message message)
        {
            var action = new SendMessageAction()
            {
                DetailType = "private",
                UserId = userId,
                Message = message
            };
            return await SendWithTimeoutAsync(action, TIMEOUT) as SendMessageReceipt;
        }

        public async Task<SendMessageReceipt> SendGroupMessageAsync(string groupId, Message message)
        {
            var action = new SendMessageAction()
            {
                DetailType = "group",
                GroupId = groupId,
                Message = message
            };
            return await SendWithTimeoutAsync(action, TIMEOUT) as SendMessageReceipt;
        }

        public async Task<UploadFileReceipt> UploadFileAsync(string url)
        {
            var uri = new Uri(url);
            if (!uri.IsAbsoluteUri)
            {
                uri = new Uri(new Uri(Environment.CurrentDirectory, UriKind.Absolute), uri);
            }

            var action = new UploadFileAction()
            {
                Type = uri.Scheme switch { "file" => "path", "http" => "url", "https" => "url" },
                Url = uri.AbsoluteUri,
                Path = uri.AbsolutePath
            };
            
            return await SendWithTimeoutAsync(action, TIMEOUT) as UploadFileReceipt;
        }
    }
}