using ImageTest.ViewModels;
using System;
using System.IO;
using Xamarin.Forms;
using Xamvvm;

namespace ImageTest.Views
{
	public partial class ImageCropViewExample : ContentPage, IBasePage<ImageCropViewExampleModel>
	{
		public ImageCropViewExample()
		{
			InitializeComponent();

			//Uri uri = new Uri("http://grubbuddy.000webhostapp.com/data/vegies216.jpg");

			//UriImageSource image = new UriImageSource();
			//image.Uri = uri;
			//cropView.Source = image;

			saveButton.Command = new BaseCommand(async (arg) =>
			{
				try
				{
					var result = await cropView.GetImageAsJpegAsync();
					byte[] bytes = null;

					using (MemoryStream ms = new MemoryStream())
					{
						result.CopyTo(ms);
						bytes = ms.ToArray();
					}

					var imageSource = ImageSource.FromStream(() =>
					{
						return new MemoryStream(bytes);
					});

					((ImageCropViewExampleModel)BindingContext).SavedImage = imageSource;
				}
				catch (System.Exception ex)
				{
					await DisplayAlert("Error", ex.Message, "Ok");
				}
			});
		}
	}
}
