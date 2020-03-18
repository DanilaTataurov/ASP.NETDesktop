using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ASP.NETDesktop.Common.Extensions {
    public static class EnumExtensions {
        public static List<string> GetDescriptions<T>() {
            var attributes = typeof(T).GetMembers()
                .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>())
                .ToList();

            return attributes.Select(x => x.Description).ToList();
        }

        public static string GetEnumDescription<T>(T value) {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) {
                return attributes[0].Description;
            } else {
                return value.ToString();
            }
        }

        public static T ParseDescriptionToEnum<T>(string description) {
            Array array = Enum.GetValues(typeof(T));
            var list = new List<T>(array.Length);

            for (int i = 0; i < array.Length; i++) {
                list.Add((T)array.GetValue(i));
            }
            
            var dict = list.Select(v => new { Value = v, Description = GetEnumDescription(v) })
                .ToDictionary(x => x.Description, x => x.Value);
            return dict[description];
        }
    }
}
