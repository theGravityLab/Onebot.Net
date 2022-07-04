using System;

namespace Onebot.Protocol.Models.Actions;

public abstract record ActionBase
{
    /// <summary>
    ///     动作名称
    /// </summary>
    internal abstract string Action { get; }

    /// <summary>
    ///     回执类型
    /// </summary>
    internal abstract Type Receipt { get; }
}