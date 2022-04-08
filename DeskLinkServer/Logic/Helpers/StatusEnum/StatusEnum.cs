using System;
using System.Reflection;

namespace DeskLinkServer.Logic.Helpers.StringEnum
{
    public class StatusEnum
    {
        private static StatusAttribute[] GetAttributes(Enum enumeration)
        {
            FieldInfo fi = enumeration.GetType().GetField(enumeration.ToString());
            StatusAttribute[] attrs = fi.GetCustomAttributes(typeof(StatusAttribute), false) as StatusAttribute[];
            return attrs.Length > 0 ? attrs : null;
        }

        public static string GetIconName(Enum enumeration)
        {
            return GetAttributes(enumeration)?[0].IconName;
        }

        public static bool GetAnimated(Enum enumeration)
        {
            StatusAttribute[] attrs = GetAttributes(enumeration);
            return attrs != null && attrs[0].Animated;
        }
    }
}
