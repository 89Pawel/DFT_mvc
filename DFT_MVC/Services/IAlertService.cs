using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DFT_MVC.Services
{
	public interface IAlertService
	{
        /// <summary>
        /// Dodaje nazwę kategorii do alertu (np. TempData) przy akcji Create, Update, Delete
        /// <para> numery ActionResult:</para>
        /// <para> 1 - Create </para>
        /// <para> 2 - Update </para>
        /// <para> 3 - Delete </para>
        /// W razie porzeby dodać więcej opcji do klasy
        /// </summary>
        /// <param name="name">nazwa kategorii</param>
        /// <param name="action">nr ActionResult</param>
        /// <returns>string</returns>
        public List<string> TempDataAlert(string name, int action);
	}
}
