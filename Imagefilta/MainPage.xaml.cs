#region Namespaces
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using Nokia.Graphics.Imaging;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Windows.Storage.Streams;
#endregion

namespace Imagefilta
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Global Variables
            private string _fileName = string.Empty;
            private String filtername = string.Empty;
            private StreamImageSource source;
        #endregion
       
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            stkfilters.Visibility = System.Windows.Visibility.Collapsed;
        }

        #region SelectImage Method to Apply Filters - Initialization
        private async void SelectImage(object sender, PhotoResult e)
        {
            if (e.TaskResult != TaskResult.OK || e.ChosenPhoto == null)
            {
                MessageBox.Show("Invalid Operation");
                return;
            }

            try
            {
                stkfilters.Visibility = System.Windows.Visibility.Visible;

                source = new StreamImageSource(e.ChosenPhoto);                

                // Antique Filter
                using (var antiqufilters = new FilterEffect(source))
                {                    
                    var antique = new AntiqueFilter();
                    antiqufilters.Filters = new IFilter[] { antique };
                   
                    var target = new WriteableBitmap((int)Img1.ActualWidth, (int)Img1.ActualHeight);

                    using (var renderer = new WriteableBitmapRenderer(antiqufilters, target))
                    {                       
                        await renderer.RenderAsync();
                        Img1.Source = target;
                    }
                }

                //Flip Filter
                using (var flipfilters = new FilterEffect(source))
                {                   
                    var flip = new FlipFilter(FlipMode.Horizontal);                   
                    flipfilters.Filters = new IFilter[] { flip };

                    var target = new WriteableBitmap((int)Img2.ActualWidth, (int)Img2.ActualHeight);

                    using (var renderer = new WriteableBitmapRenderer(flipfilters, target))
                    {
                        await renderer.RenderAsync();
                        Img2.Source = target;
                    }
                }

                //Wrap Filter
                using (var wrapfilters = new FilterEffect(source))
                {
                    var wrap = new WarpFilter(WarpEffect.Twister, 0.7);
                    wrapfilters.Filters = new IFilter[] { wrap };

                    var target = new WriteableBitmap((int)Img3.ActualWidth, (int)Img3.ActualHeight);

                    using (var renderer = new WriteableBitmapRenderer(wrapfilters, target))
                    {                       
                        await renderer.RenderAsync();
                        Img3.Source = target;
                    }
                }

                //Sketch Filter
                using (var sketchfilters = new FilterEffect(source))
                {                   
                    var sketch = new SketchFilter(SketchMode.Gray);
                    sketchfilters.Filters = new IFilter[] { sketch };

                    var target = new WriteableBitmap((int)Img4.ActualWidth, (int)Img4.ActualHeight);
                  
                    using (var renderer = new WriteableBitmapRenderer(sketchfilters, target))
                    {                        
                        await renderer.RenderAsync();
                        Img4.Source = target;
                    }
                }

                //Sepia Filter
                using (var sepiafilters = new FilterEffect(source))
                {
                    var sepia = new SepiaFilter();
                    sepiafilters.Filters = new IFilter[] { sepia };

                    var target = new WriteableBitmap((int)Img5.ActualWidth, (int)Img5.ActualHeight);

                    using (var renderer = new WriteableBitmapRenderer(sepiafilters, target))
                    {                        
                        await renderer.RenderAsync();
                        Img5.Source = target;
                    }
                }

                //MagicPen Filter
                using (var magicpenfilters = new FilterEffect(source))
                {
                    var mpen = new MagicPenFilter();
                    magicpenfilters.Filters = new IFilter[] { mpen };

                    var target = new WriteableBitmap((int)Img6.ActualWidth, (int)Img6.ActualHeight);

                    using (var renderer = new WriteableBitmapRenderer(magicpenfilters, target))
                    {
                        await renderer.RenderAsync();
                        Img6.Source = target;
                    }
                }

                //Solarize Filter
                using (var solarfilters = new FilterEffect(source))
                {
                    var solar = new SolarizeFilter(0.7);
                    solarfilters.Filters = new IFilter[] { solar };

                    var target = new WriteableBitmap((int)Img7.ActualWidth, (int)Img7.ActualHeight);

                    using (var renderer = new WriteableBitmapRenderer(solarfilters, target))
                    {
                        await renderer.RenderAsync();
                        Img7.Source = target;
                    }
                }

                //Negative Filter
                using (var negativefilters = new FilterEffect(source))
                {
                    var negative = new NegativeFilter();
                    negativefilters.Filters = new IFilter[] { negative };

                    var target = new WriteableBitmap((int)Img8.ActualWidth, (int)Img8.ActualHeight);

                    using (var renderer = new WriteableBitmapRenderer(negativefilters, target))
                    {
                        await renderer.RenderAsync();
                        Img8.Source = target;
                    }
                }

                //Temparature and Tint Filter
                using (var temptntfilters = new FilterEffect(source))
                {
                    var temptnt = new TemperatureAndTintFilter(-0.5, 1);
                    temptntfilters.Filters = new IFilter[] { temptnt };

                    var target = new WriteableBitmap((int)Img9.ActualWidth, (int)Img9.ActualHeight);

                    using (var renderer = new WriteableBitmapRenderer(temptntfilters, target))
                    {
                        await renderer.RenderAsync();
                        Img9.Source = target;
                    }
                }

                //Stamp Filter
                using (var stampfilters = new FilterEffect(source))
                {
                    var stamp = new StampFilter(6,0.6);
                    stampfilters.Filters = new IFilter[] { stamp };

                    var target = new WriteableBitmap((int)Img10.ActualWidth, (int)Img10.ActualHeight);

                    using (var renderer = new WriteableBitmapRenderer(stampfilters, target))
                    {
                        await renderer.RenderAsync();
                        Img10.Source = target;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Some error occured, Please share feedback from Send Feedback Menu" + exception.Message.ToString());
                return;
            }
        }
        #endregion

        #region Choose Images from Phone
            private void mnuOpen_Click(object sender, EventArgs e)
            {
                PhotoChooserTask chooser = new PhotoChooserTask();
                chooser.Completed += SelectImage;
                chooser.Show();
            }
        #endregion

        #region Take Image from Camera
                private void mnuCamera_Click(object sender, EventArgs e)
                {
                    CameraCaptureTask cmrcapture = new CameraCaptureTask();
                    cmrcapture.Completed += SelectImage;
                    cmrcapture.Show();
                }
       #endregion          
        
        #region Save Filter Applied Image to Phone
        private void mnuSave_Click(object sender, EventArgs e)
        {
            SaveImageHelper smhelp = new SaveImageHelper();
            try
            {
                switch (filtername)
                {
                    case "Antique": AntiqueFilter ant = new AntiqueFilter();
                        smhelp.SaveImage(source, Img1, _fileName, ant);
                        
                        break;

                    case "Flip": FlipFilter flip = new FlipFilter(FlipMode.Horizontal);
                        smhelp.SaveImage(source, Img2, _fileName, flip);
                        
                        break;

                    case "Warp": WarpFilter warp = new WarpFilter(WarpEffect.Twister, 0.7);
                        smhelp.SaveImage(source, Img3, _fileName, warp);
                        
                        break;

                    case "Sketch": SketchFilter sktch = new SketchFilter(SketchMode.Gray);
                        smhelp.SaveImage(source, Img4, _fileName, sktch);
                        
                        break;

                    case "Sepia": SepiaFilter sepia = new SepiaFilter();
                        smhelp.SaveImage(source, Img5, _fileName, sepia);
                        
                        break;

                    case "MagicPen": MagicPenFilter mgkpen = new MagicPenFilter();
                        smhelp.SaveImage(source, Img6, _fileName, mgkpen);
                        
                        break;

                    case "Solarize": SolarizeFilter solar = new SolarizeFilter(0.7);
                        smhelp.SaveImage(source, Img7, _fileName, solar);
                        
                        break;

                    case "Negative": NegativeFilter ngtv = new NegativeFilter();
                        smhelp.SaveImage(source, Img8, _fileName, ngtv);
                        
                        break;

                    case "TempNTint": TemperatureAndTintFilter temptnt = new TemperatureAndTintFilter(-0.5, 1);
                        smhelp.SaveImage(source, Img9, _fileName, temptnt);
                        
                        break;

                    case "Stamp": StampFilter stamp = new StampFilter(6, 0.6);
                        smhelp.SaveImage(source, Img10, _fileName, stamp);
                        
                        break;
                    default:
                        break;                        
                }                   
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Occured, Unable to Save Picture" + exception.Message.ToString());
            }
                    
         }
       #endregion
               
        #region Associate Original Image with Filters

        private void ApplyToOriginal(Image img)
        {
            imgOrigin.Source = img.Source;            
        }
        private void Img1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplyToOriginal(Img1);
            filtername = "Antique";
        }

        private void Img2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplyToOriginal(Img2);
            filtername = "Flip";
        }

        private void Img3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplyToOriginal(Img3);
            filtername = "Warp";
        }

        private void Img4_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplyToOriginal(Img4);
            filtername = "Sketch";
        }

        private void Img5_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplyToOriginal(Img5);
            filtername = "Sepia";
        }

        private void Img6_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplyToOriginal(Img6);
            filtername = "MagicPen";
        }

        private void Img7_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplyToOriginal(Img7);
            filtername = "Solarize";
        }

        private void Img8_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplyToOriginal(Img8);
            filtername = "Negative";
        }

        private void Img9_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplyToOriginal(Img9);
            filtername = "TempNTint";
        }

        private void Img10_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplyToOriginal(Img10);
            filtername = "Stamp";
        }
     #endregion

        #region Share Filter Applied Images on Socials
        private void mnuShare_Click(object sender, EventArgs e)
        {
            var chooser = new Microsoft.Phone.Tasks.PhotoChooserTask();
            chooser.Completed += chooser_Completed;
            chooser.Show();
        }

        void chooser_Completed(object sender, PhotoResult e)
        {
            _fileName = e.OriginalFileName.ToString();
            if (e.TaskResult == TaskResult.OK)
            {
                ShareMediaTask shareMediaTask = new ShareMediaTask();
                shareMediaTask.FilePath = _fileName;                
                shareMediaTask.Show();
            }
        }
     #endregion
        
    }
}

#region Reference Code
//Cropping Code
//using (var cropfilters = new FilterEffect(source))
//{
//    // Initialize the filter 
//    var CropFilter = new SpotlightFilter(new Windows.Foundation.Point(400, 300), 100, 0.5);

//    // Add the filter to the FilterEffect collection
//    cropfilters.Filters = new IFilter[] { CropFilter };

//    // Create a target where the filtered image will be rendered to
//    var croptarget = new WriteableBitmap((int)Img3.ActualWidth, (int)Img3.ActualHeight);

//    // Create a new renderer which outputs WriteableBitmaps
//    using (var renderer = new WriteableBitmapRenderer(cropfilters, croptarget))
//    {
//        // Render the image with the filter(s)
//        await renderer.RenderAsync();

//        // Set the output image to Image control as a source
//        Img3.Source = croptarget;
//    }
//}

//// Blur Filter
//using (var filters = new FilterEffect(source))
//{
//    // Initialize the filter                         
//    var sampleFilter = new BlurFilter();
//    sampleFilter.BlurRegionShape = BlurRegionShape.Elliptical;
//    sampleFilter.KernelSize = 50;

//    // Add the filter to the FilterEffect collection
//    filters.Filters = new IFilter[] { sampleFilter };

//    // Create a target where the filtered image will be rendered to
//    var target = new WriteableBitmap((int)Img2.ActualWidth, (int)Img2.ActualHeight);

//    // Create a new renderer which outputs WriteableBitmaps
//    using (var renderer = new WriteableBitmapRenderer(filters, target))
//    {
//        // Render the image with the filter(s)
//        await renderer.RenderAsync();

//        // Set the output image to Image control as a source
//        Img2.Source = target;
//    }
//}

//For more filters visit : http://developer.nokia.com/resources/library/Imaging_API_Ref/nokiagraphicsimaging-namespace.html
#endregion

#region Dead Code
//if (filtername.Equals("Antique"))
//{
//    using (var antiqufilters = new FilterEffect(source))
//    {
//        var antique = new AntiqueFilter();
//        antiqufilters.Filters = new IFilter[] { antique };

//        //var target = new WriteableBitmap((int)imgOrigin.ActualWidth, (int)imgOrigin.ActualHeight);
//        //using (var renderer = new WriteableBitmapRenderer(antiqufilters, target))
//        //{
//        //    await renderer.RenderAsync();
//        //    imgOrigin.Source = target;
//        //}

//        var jpegRenderer = new JpegRenderer(antiqufilters);

//        IBuffer jpegOutput = await jpegRenderer.RenderAsync();

//        MediaLibrary library = new MediaLibrary();
//        _fileName = string.Format("ImageFilta", DateTime.Now.Date);
//        var picture = library.SavePicture(_fileName, jpegOutput.AsStream());

//        MessageBox.Show("Image saved!");
//    }
//}
//else
//{
//    MessageBox.Show("Trial Version only allows to Save Antique Filter Effect, Please Buy Application for full features");
//    return;
//}                  
#endregion
