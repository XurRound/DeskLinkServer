using System;

namespace DeskLinkServer.Logic.Helpers.StringEnum
{
    public class StatusAttribute : Attribute
    {
        public string IconName { get; }
        public bool Animated { get; }

        public StatusAttribute(string iconName, bool animated)
        {
            IconName = iconName;
            Animated = animated;
        }
    }
}