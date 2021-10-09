using System;
using System.Text.Json.Serialization;
using Onebot.Protocol.Models.Receipts;

namespace Onebot.Protocol.Models.Actions
{
    public interface IAction
    {
        Type Output => typeof(GenericReceipt);
    }
}