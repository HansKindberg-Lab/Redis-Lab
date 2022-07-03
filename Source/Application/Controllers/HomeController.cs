using Application.Models.Views.Home;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Application.Controllers
{
	public class HomeController : Controller
	{
		#region Constructors

		public HomeController(IConnectionMultiplexer connectionMultiplexer, ILoggerFactory loggerFactory)
		{
			this.ConnectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
			this.Logger = (loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory))).CreateLogger(this.GetType());
		}

		#endregion

		#region Properties

		protected internal virtual IConnectionMultiplexer ConnectionMultiplexer { get; }
		protected internal virtual ILogger Logger { get; }

		#endregion

		#region Methods

		protected internal virtual async Task<HomeViewModel> CreateHomeViewModelAsync()
		{
			var database = this.ConnectionMultiplexer.GetDatabase();
			var model = new HomeViewModel();

			foreach(var key in this.GetKeys().OrderBy(key => (string)key))
			{
				model.Items.Add(key, await database.StringGetAsync(key));
			}

			return model;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual async Task<IActionResult> Delete(string key)
		{
			if(key == null)
				throw new ArgumentNullException(nameof(key));

			var value = await this.ConnectionMultiplexer.GetDatabase().StringGetDeleteAsync(key);

			var model = await this.CreateHomeViewModelAsync();

			if(!value.IsNull)
				model.DeleteConfirmation = true;

			return this.View("Index", model);
		}

		protected internal virtual IEnumerable<RedisKey> GetKeys()
		{
			return this.ConnectionMultiplexer.GetEndPoints().SelectMany(endpoint => this.ConnectionMultiplexer.GetServer(endpoint).Keys()).ToArray();
		}

		[HttpGet]
		public virtual async Task<IActionResult> Index()
		{
			return this.View(await this.CreateHomeViewModelAsync());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual async Task<IActionResult> Save(Form form)
		{
			if(form == null)
				throw new ArgumentNullException(nameof(form));

			var saved = false;

			if(this.ModelState.IsValid)
			{
				var database = this.ConnectionMultiplexer.GetDatabase();

				await database.StringSetAsync(form.Key, form.Value);

				saved = true;
			}

			var model = await this.CreateHomeViewModelAsync();

			if(saved)
			{
				this.ModelState.Clear();
				model.SaveConfirmation = true;
			}
			else
			{
				model.Form = form;
			}

			return this.View("Index", model);
		}

		#endregion
	}
}