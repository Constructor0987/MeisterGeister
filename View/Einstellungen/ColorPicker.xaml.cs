using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Q42.HueApi;
using Q42.HueApi.ColorConverters.Original;
using Q42.HueApi.ColorConverters.OriginalWithModel;
using Q42.HueApi.ColorConverters.HSB;
using Q42.HueApi.ColorConverters;

namespace MeisterGeister.View.Settings
{
  public partial class ColorPicker : UserControl
  {
    #region Data

    private DrawingAttributes drawingAttributes = new DrawingAttributes();
    private Color selectedColor = Colors.Transparent;
    private Boolean IsMouseDown = false;

    public LocalHueClient Client = null;
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor that initializes the ColorPicker to Black.
        /// </summary>
        public ColorPicker()
      : this(Colors.Black)
    { }

    /// <summary>
    /// Constructor that initializes to ColorPicker to the specified color.
    /// </summary>
    /// <param name="initialColor"></param>
    public ColorPicker(Color initialColor)
    {
      InitializeComponent();
      this.selectedColor = initialColor;
    }

        #endregion

        #region Public Properties
        

    /// <summary>
    /// Gets or privately sets the Selected Color.
    /// </summary>
    public Color SelectedColor
    {
      get { return selectedColor; }
      private set
      {
        if (selectedColor != value)
        {
          this.selectedColor = value;
          CreateAlphaLinearBrush();
          UpdateTextBoxes();
          UpdateInk();
        }
      }
    }


    /// <summary>
    /// Sets the initial Selected Color.
    /// </summary>
        public Color InitialColor
    {
      set
      {
        SelectedColor = value;
        CreateAlphaLinearBrush();
        BrightnessSlider.Value = value.A;
        UpdateCursorEllipse(value);
      }
    }

    #endregion

    #region Control Events
        
    /// <summary>
    /// 
    /// </summary>
    private void BrightnessSlider_MouseWheel(object sender, MouseWheelEventArgs e)
    {
      int change = e.Delta / Math.Abs(e.Delta);
            BrightnessSlider.Value = BrightnessSlider.Value + (double)change;
    }
        

    /// <summary>
    /// Update the SelectedColor if moving the mouse with the left button down.
    /// </summary>
    private void CanvasImage_MouseMove(object sender, MouseEventArgs e)
    {
      if (IsMouseDown) UpdateColor();
    }

    /// <summary>
    /// Handle MouseDown event.
    /// </summary>
    private void CanvasImage_MouseDown(object sender, MouseButtonEventArgs e)
    {
      IsMouseDown = true;
      UpdateColor();
        if (chbxUpdateLamp.IsChecked.Value && Client != null)
        {
            //Control the lights                
            LightCommand command = new LightCommand();
            command.TurnOn().SetColor(new RGBColor(SelectedColor.R, SelectedColor.G, SelectedColor.B));
                command.Brightness = (byte)BrightnessSlider.Value;
            //Or send it to all lights
            Client.SendCommandAsync(command);
        }
}

    /// <summary>
    /// Handle MouseUp event.
    /// </summary>
    private void CanvasImage_MouseUp(object sender, MouseButtonEventArgs e)
    {
      IsMouseDown = false;
            //UpdateColor();
        }

    /// <summary>
    /// Apply the new Swatch image based on user requested swatch.
    /// </summary>
    private void Swatch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      Image img = (sender as Image);
      ColorImage.Source = img.Source;
      UpdateCursorEllipse(SelectedColor);
    }

    #endregion // Control Events

    #region Private Methods

    /// <summary>
    /// Creates a new LinearGradientBrush background for the Alpha area slider.  This is based on the current color.
    /// </summary>
    private void CreateAlphaLinearBrush()
    {
      Color startColor = Color.FromArgb((byte)0, SelectedColor.R, SelectedColor.G, SelectedColor.B);
      Color endColor = Color.FromArgb((byte)255, SelectedColor.R, SelectedColor.G, SelectedColor.B);
      LinearGradientBrush alphaBrush = new LinearGradientBrush(startColor, endColor, new Point(0, 0), new Point(1, 0));
            BrightnessBorder.Background = alphaBrush;
    }

    /// <summary>
    /// Sets a new Selected Color based on the color of the pixel under the mouse pointer.
    /// </summary>
    private void UpdateColor()
    {
      // Test to ensure we do not get bad mouse positions along the edges
      int imageX = (int)Mouse.GetPosition(canvasImage).X;
      int imageY = (int)Mouse.GetPosition(canvasImage).Y;
      if ((imageX < 0) || (imageY < 0) || (imageX > ColorImage.Width - 1) || (imageY > ColorImage.Height - 1)) return;
      // Get the single pixel under the mouse into a bitmap and copy it to a byte array
      CroppedBitmap cb = new CroppedBitmap(ColorImage.Source as BitmapSource, new Int32Rect(imageX, imageY, 1, 1));
      byte[] pixels = new byte[4];
      cb.CopyPixels(pixels, 4, 0);
      // Update the mouse cursor position and the Selected Color
      ellipsePixel.SetValue(Canvas.LeftProperty, (double)(Mouse.GetPosition(canvasImage).X - (ellipsePixel.Width / 2.0)));
      ellipsePixel.SetValue(Canvas.TopProperty, (double)(Mouse.GetPosition(canvasImage).Y - (ellipsePixel.Width / 2.0)));
      canvasImage.InvalidateVisual();
      // Set the Selected Color based on the cursor pixel and Alpha Slider value
      SelectedColor = Color.FromArgb((byte)BrightnessSlider.Value, pixels[2], pixels[1], pixels[0]);
    }

    /// <summary>
    /// Update the mouse cursor ellipse position.
    /// </summary>
    private void UpdateCursorEllipse(Color searchColor)
    {
      // Scan the canvas image for a color which matches the search color
      CroppedBitmap cb;
      Color tempColor = new Color();
      byte[] pixels = new byte[4];
      int searchY = 0;
      int searchX = 0;
      searchColor.A = 255;
      for (searchY = 0; searchY <= canvasImage.Width - 1; searchY++)
      {
        for (searchX = 0; searchX <= canvasImage.Height - 1; searchX++)
        {
          cb = new CroppedBitmap(ColorImage.Source as BitmapSource, new Int32Rect(searchX, searchY, 1, 1));
          cb.CopyPixels(pixels, 4, 0);
          tempColor = Color.FromArgb(255, pixels[2], pixels[1], pixels[0]);
          if (tempColor == searchColor) break;
        }
        if (tempColor == searchColor) break;
      }
      // Default to the top left if no match is found
      if (tempColor != searchColor)
      {
        searchX = 0;
        searchY = 0;
      }
      // Update the mouse cursor ellipse position
      ellipsePixel.SetValue(Canvas.LeftProperty, ((double)searchX - (ellipsePixel.Width / 2.0)));
      ellipsePixel.SetValue(Canvas.TopProperty, ((double)searchY - (ellipsePixel.Width / 2.0)));
    }

    /// <summary>
    /// Update text box values based on the Selected Color.
    /// </summary>
    private void UpdateTextBoxes()
    {
      txtAlpha.Text = SelectedColor.A.ToString();
      txtAlphaHex.Text = SelectedColor.A.ToString("X2");
      txtRed.Text = SelectedColor.R.ToString();
      txtRedHex.Text = SelectedColor.R.ToString("X2");
      txtGreen.Text = SelectedColor.G.ToString();
      txtGreenHex.Text = SelectedColor.G.ToString("X2");
      txtBlue.Text = SelectedColor.B.ToString();
      txtBlueHex.Text = SelectedColor.B.ToString("X2");
      txtAll.Text = String.Format("#{0}{1}{2}{3}", txtAlphaHex.Text, txtRedHex.Text, txtGreenHex.Text, txtBlueHex.Text);
    }

    /// <summary>
    /// Updates the Ink strokes based on the Selected Color.
    /// </summary>
    private void UpdateInk()
    {
      drawingAttributes.Color = SelectedColor;
      drawingAttributes.StylusTip = StylusTip.Ellipse;
      drawingAttributes.Width = 5;
      // Update drawing attributes on previewPresenter
      foreach (Stroke s in previewPresenter.Strokes)
      {
        s.DrawingAttributes = drawingAttributes;
      }
    }

        #endregion // Update Methods

        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (chbxUpdateLamp.IsChecked.Value && Client != null)
            {
                //Control the lights                
                LightCommand command = new LightCommand();
                command.TurnOn().SetColor(new RGBColor(SelectedColor.R, SelectedColor.G, SelectedColor.B));
                command.Brightness = (byte)BrightnessSlider.Value;
                //Or send it to all lights
                Client.SendCommandAsync(command);
            }
        }



        private void chbxUpdateLamp_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
