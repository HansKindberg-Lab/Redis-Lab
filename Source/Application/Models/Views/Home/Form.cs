using System.ComponentModel.DataAnnotations;

namespace Application.Models.Views.Home
{
	public class Form
	{
		#region Properties

		[Required]
		public virtual string Key { get; set; }

		[Required]
		public virtual string Value { get; set; }

		#endregion
	}
}