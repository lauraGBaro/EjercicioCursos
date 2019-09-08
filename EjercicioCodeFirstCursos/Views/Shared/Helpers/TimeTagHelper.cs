using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace EjercicioCodeFirstCursos.Views.Shared.Helpers
{
	[HtmlTargetElement("time", Attributes = DateTimeAttribute)]
	public class TimeTagHelper : TagHelper
	{
		private const string DateTimeAttribute = "asp-date-time";

		[HtmlAttributeName(DateTimeAttribute)]
		public DateTime DateTime { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.Attributes.SetAttribute("datetime", DateTime.ToString("yyyy-MM-dd'T'HH:mm:ss") + "Z");
			output.Attributes.SetAttribute("title", DateTime.ToString("dddd, MMMM d, yyyy 'at' h:mm tt"));

			var childContent = await output.GetChildContentAsync();
			if (childContent.IsEmptyOrWhiteSpace)
			{
				output.TagMode = TagMode.StartTagAndEndTag;
				output.Content.SetContent(DateTime.ToString("MMMM d, yyyy h:mm tt"));
			}
		}
	}
}
