#region Namespaces
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using Nokia.Graphics.Imaging;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Windows.Storage.Streams;
#endregion

namespace Imagefilta
{
    public class SaveImageHelper
    {
        private string _fileName = string.Empty;
        WriteableBitmap target;
       
        public async void SaveImage(StreamImageSource source, Image Img, String FilterName,IFilter Filter)
        {            
            using (var imgfilters = new FilterEffect(source))
            {
                var filt = Filter;
                imgfilters.Filters = new IFilter[] { filt };

                var target = new WriteableBitmap((int)Img.ActualWidth, (int)Img.ActualHeight);
                using (var renderer = new WriteableBitmapRenderer(imgfilters, target))
                {
                    await renderer.RenderAsync();
                    Img.Source = target;
                }

                var jpegRenderer = new JpegRenderer(imgfilters);

                IBuffer jpegOutput = await jpegRenderer.RenderAsync();

                MediaLibrary library = new MediaLibrary();
                _fileName = string.Format("ImageFilta", DateTime.Now.Second) + ".jpg";
                var picture = library.SavePicture(_fileName, jpegOutput.AsStream());
                MessageBox.Show("Image saved!");
            }           
        }

        public WriteableBitmap LoadFilters(StreamImageSource source, Image Img, String FilterName,IFilter Filter)
        {
             // Antique Filter
                using (var antiqufilters = new FilterEffect(source))
                {
                    // Initialize the filter 
                    var antique = new AntiqueFilter();

                    // Add the filter to the FilterEffect collection
                    antiqufilters.Filters = new IFilter[] { antique };

                    // Create a target where the filtered image will be rendered to
                    target = new WriteableBitmap((int)Img.ActualWidth, (int)Img.ActualHeight);                   
                }

                return target;
        }
    }
}
