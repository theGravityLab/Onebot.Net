using System;

namespace Onebot.Protocol.Models.Actions;

public abstract record ActionBase
{
    /// <summary>
    ///     动作名称
    /// </summary>
    protected abstract string Action { get; }

    /// <summary>
    ///     回执类型
    /// </summary>
    protected abstract Type Receipt { get; }

    public string GetAction()
    {
        return Action;
    }

    public Type GetReceiptType()
    {
        return Receipt;
    }
}