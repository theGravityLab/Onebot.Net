using System;
using Onebot.Protocol.Models.Actions;

namespace Onebot.Protocol.Communications.Serialization
{
    public class ActionWrapper
    {
        public string Action { get; set; }
        public object Params { get; set; }
        public string Echo { get; set; }
    }
}