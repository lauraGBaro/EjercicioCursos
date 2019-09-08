using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace EjercicioCodeFirstCursos.Views.Shared.Helpers
{
	[HtmlTargetElement("img", Attributes = FotoAttributeBytes)]
	public class ImageTagHelper : TagHelper
	{
		private const string FotoAttributeBytes = "bytes-foto";

		[HtmlAttributeName(FotoAttributeBytes)]
		public byte[] Imagen { get; set; }


		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var base64 = Convert.ToBase64String(Imagen);
			var imgSrc = string.Format("data:image/jpeg;base64,{0}", base64);
			
			output.Attributes.SetAttribute("src", imgSrc);
			output.Attributes.SetAttribute("class", "foto");
		}

	}
}
