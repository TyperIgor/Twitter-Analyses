using System;
using System.ComponentModel;

namespace TwitterAnalysis.App.Service.Common.Extension
{
    public static class EnumExtension
    {
        public static string GetDescription(this System.Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var desc = value.ToString();

            var info = value.GetType().GetField(desc);
            var attrs = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs.Length > 0)
                desc = attrs[0].Description;

            return desc;
        }
    }
}
