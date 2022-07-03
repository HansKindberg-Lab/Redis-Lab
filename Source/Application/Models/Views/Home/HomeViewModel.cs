using StackExchange.Redis;

namespace Application.Models.Views.Home
{
	public class HomeViewModel
	{
		#region Fields

		private Form _form;

		#endregion

		#region Properties

		public virtual bool DeleteConfirmation { get; set; }

		public virtual Form Form
		{
			get => this._form ??= new Form();
			set => this._form = value;
		}

		public virtual IDictionary<RedisKey, RedisValue> Items { get; } = new Dictionary<RedisKey, RedisValue>();
		public virtual bool SaveConfirmation { get; set; }

		#endregion
	}
}