using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using LiteDB;

namespace Kanaban
{
    public static class WPFEx
    {
        public static T GetChildOfType<T>(this DependencyObject depObj)
            where T : DependencyObject
        {
            if (depObj == null) return null;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        public static void Foreach<T>(this LiteCollection<T> col , Action<T> ac)
        {
            var en = col.FindAll().GetEnumerator();
            while (en.MoveNext())
            {
                ac.Invoke(en.Current);
            }
            en.Dispose();
        }
    }
}
