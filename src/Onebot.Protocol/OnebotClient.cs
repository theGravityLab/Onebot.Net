using System;
using System.Threading;
using System.Threading.Tasks;
using Onebot.Protocol.Models.Actions;
using Onebot.Protocol.Models.Messages;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol;

public class OnebotClient
{
    private const int TIMEOUT = 15000;

    public OnebotClient(IConnection connection)
    {
        Connection = connection;
    }

    public IConnection Connection { get; }

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

    public async Task<GetSupportedActionsReceipt> GetSupportedActionsAsync()
    {
        var action = new GetSupportedActionsAction();
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetSupportedActionsReceipt;
    }

    public async Task<GetStatusReceipt> GetStatusAsync()
    {
        var action = new GetStatusAction();
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetStatusReceipt;
    }

    public async Task<GetVersionReceipt> GetVersionAsync()
    {
        var action = new GetVersionAction();
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetVersionReceipt;
    }

    public async Task<GetSelfInfoReceipt> GetSelfInfoAsync()
    {
        var action = new GetSelfInfoAction();
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetSelfInfoReceipt;
    }

    public async Task<GetGroupInfoReceipt> GetGroupInfoAsync(string id)
    {
        var action = new GetGroupInfoAction
        {
            GroupId = id
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetGroupInfoReceipt;
    }

    public async Task<LeaveGroupReceipt> LeaveGroupAsync(string groupId)
    {
        var action = new LeaveGroupAction
        {
            GroupId = groupId
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as LeaveGroupReceipt;
    }

    public async Task<KickGroupMemberReceipt> KickGroupMemberAsync(string groupId, string userId)
    {
        var action = new KickGroupMemberAction
        {
            GroupId = groupId,
            UserId = userId
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as KickGroupMemberReceipt;
    }

    public async Task<BanGroupMemberReceipt> BanGroupMemberAsync(string groupId, string userId)
    {
        var action = new BanGroupMemberAction
        {
            GroupId = groupId,
            UserId = userId
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as BanGroupMemberReceipt;
    }

    public async Task<UnbanGroupMemberReceipt> UnbanGroupMemberAsync(string groupId, string userId)
    {
        var action = new UnbanGroupMemberAction
        {
            GroupId = groupId,
            UserId = userId
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as UnbanGroupMemberReceipt;
    }

    public async Task<SetGroupAdminReceipt> SetGroupAdminAsync(string groupId, string userId)
    {
        var action = new SetGroupAdminAction
        {
            GroupId = groupId,
            UserId = userId
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as SetGroupAdminReceipt;
    }

    public async Task<UnsetGroupAdminReceipt> UnsetGroupAdminAsync(string groupId, string userId)
    {
        var action = new UnsetGroupAdminAction
        {
            GroupId = groupId,
            UserId = userId
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as UnsetGroupAdminReceipt;
    }

    public async Task<SetGroupNameReceipt> SetGroupNameAsync(string groupId, string groupName)
    {
        var action = new SetGroupNameAction
        {
            GroupId = groupId,
            GroupName = groupName
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as SetGroupNameReceipt;
    }

    public async Task<GetGroupListReceipt> GetGroupListAsync()
    {
        var action = new GetGroupListAction();
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetGroupListReceipt;
        ;
    }

    public async Task<GetUserInfoReceipt> GetUserInfoAsync(string id)
    {
        var action = new GetUserInfoAction
        {
            UserId = id
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetUserInfoReceipt;
    }

    public async Task<GetFriendListReceipt> GetFriendListAsync()
    {
        var action = new GetFriendListAction();
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetFriendListReceipt;
    }

    public async Task<GetGroupMemberInfoReceipt> GetGroupMemberInfoAsync(string groupId, string userId)
    {
        var action = new GetGroupMemberInfoAction
        {
            GroupId = groupId,
            UserId = userId
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetGroupMemberInfoReceipt;
    }

    public async Task<GetGroupMemberListReceipt> GetGroupMemberListAsync(string id)
    {
        var action = new GetGroupMemberListAction
        {
            GroupId = id
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetGroupMemberListReceipt;
    }

    public async Task<SendMessageReceipt> SendPrivateMessageAsync(string userId, Message message)
    {
        var action = new SendMessageAction
        {
            DetailType = "private",
            UserId = userId,
            Message = message
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as SendMessageReceipt;
    }

    public async Task<SendMessageReceipt> SendGroupMessageAsync(string groupId, Message message)
    {
        var action = new SendMessageAction
        {
            DetailType = "group",
            GroupId = groupId,
            Message = message
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as SendMessageReceipt;
    }

    public async Task<DeleteMessageReceipt> DeleteMessageAsync(string messageId)
    {
        var action = new DeleteMessageAction
        {
            MessageId = messageId
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as DeleteMessageReceipt;
    }

    public async Task<UploadFileReceipt> UploadFileAsync(string url)
    {
        var uri = new Uri(url);
        if (!uri.IsAbsoluteUri) uri = new Uri(new Uri(Environment.CurrentDirectory, UriKind.Absolute), uri);

        var action = new UploadFileAction
        {
            Type = uri.Scheme switch { "file" => "path", "http" => "url", "https" => "url", "base64" => "data" },
            Url = uri.AbsoluteUri,
            Path = uri.AbsoluteUri,
            Data = uri.ToString(),
            Name = Guid.NewGuid().ToString()
        };

        return await SendWithTimeoutAsync(action, TIMEOUT) as UploadFileReceipt;
    }

    public async Task<GetFileReceipt> GetFileAsync(string fileId, string type = "url")
    {
        var action = new GetFileAction
        {
            FileId = fileId,
            Type = type
        };
        return await SendWithTimeoutAsync(action, TIMEOUT) as GetFileReceipt;
    }
}