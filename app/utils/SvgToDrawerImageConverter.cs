using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Globalization;
using SharpVectors.Converters;
using System.Windows.Data;
using SharpVectors.Renderers.Wpf;
using System.Windows.Media;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Windows;

namespace app.utils
{


    public class SvgToDrawingImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string svgPath)
            {
                var settings = new WpfDrawingSettings
                {
                    IncludeRuntime = true,
                    TextAsGeometry = true
                };

                using var reader = new FileSvgReader(settings);
                try
                {
                    if (svgPath.StartsWith("pack://"))
                    {
                        var stream = System.Windows.Application.GetResourceStream(new Uri(svgPath))?.Stream;
                        if (stream != null)
                        {
                            var drawingGroup = reader.Read(stream);
                            return new DrawingImage(drawingGroup);
                        }
                    }
                    else if (!Path.IsPathRooted(svgPath))
                    {
                        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        var absolutePath = Path.GetFullPath(Path.Combine(baseDirectory, svgPath));
                        if (!File.Exists(absolutePath))
                            throw new FileNotFoundException($"SVG file not found: {absolutePath}");

                        var drawingGroup = reader.Read(absolutePath);
                        return new DrawingImage(drawingGroup);
                    }
                    else
                    {
                        if (!File.Exists(svgPath))
                            throw new FileNotFoundException($"SVG file not found: {svgPath}");

                        var drawingGroup = reader.Read(svgPath);
                        return new DrawingImage(drawingGroup);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("cannot");
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
