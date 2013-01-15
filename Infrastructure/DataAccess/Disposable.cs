using System;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
	/// <summary>
	/// Base class for disposable objects
	/// </summary>
	public class Disposable : IDisposable
	{
		private bool isDisposed;

		/// <summary>
		/// Destructor.
		/// </summary>
		~Disposable()
		{
			Dispose(false);
		}

		/// <summary>
		/// Public dispose method for manual execution.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!isDisposed && disposing)
			{
				DisposeCore();
			}

			isDisposed = true;
		}

		/// <summary>
		/// Virtual method for children objects to clean up unmanaged resources.
		/// </summary>
		protected virtual void DisposeCore()
		{
		}
	}  
}
