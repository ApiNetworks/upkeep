using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Upkeep.Modules.FileExplorer.Converters
{
    public class FileSystemNodeImageConverter : IValueConverter
    {
        public ImageSource DriveImage
        {
            get; set;
        }

        public ImageSource DirectoryImage
        {
            get;set;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TreeViewItem)
            {
                var node = value as TreeViewItem;
                if (node.Tag is DriveInfo)
                {
                    return LoadBitmapFromResource("Images/diskdrive.png"); 
                }

                if (node.Tag is DirectoryInfo)
                {
                    //return DirectoryImage;
                    return LoadBitmapFromResource("Images/folder.png");
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static BitmapImage LoadBitmapFromResource(string pathInApplication, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            if (pathInApplication[0] == '/')
            {
                pathInApplication = pathInApplication.Substring(1);
            }
            return new BitmapImage(new Uri(@"pack://application:,,,/" + assembly.GetName().Name + ";component/" + pathInApplication, UriKind.Absolute));
        }
    }
}
