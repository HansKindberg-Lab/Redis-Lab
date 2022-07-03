namespace Application.Models.Hosting.Extensions
{
	public static class HostEnvironmentExtension
	{
		#region Methods

		public static bool IsLocalDevelopment(this IHostEnvironment hostEnvironment)
		{
			return hostEnvironment.IsLocalDevelopmentDocker() || hostEnvironment.IsLocalDevelopmentKestrel();
		}

		public static bool IsLocalDevelopmentDocker(this IHostEnvironment hostEnvironment)
		{
			return hostEnvironment.IsEnvironment(Environments.LocalDevelopmentDocker);
		}

		public static bool IsLocalDevelopmentKestrel(this IHostEnvironment hostEnvironment)
		{
			return hostEnvironment.IsEnvironment(Environments.LocalDevelopmentKestrel);
		}

		#endregion
	}
}