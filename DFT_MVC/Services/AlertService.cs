using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DFT_MVC.Services
{
	public class AlertService : IAlertService
	{

        public List<string> TempDataAlert(string name, int action)
		{
			var create = "Dodano";
            var update = "Edytowano";
            var delete = "Usunięto";
			string prefix = action switch
			{
				1 => create,
				2 => update,
				3 => delete,
				_ => string.Empty,
			};

			var alertBody = $"{prefix} kategorię {name}{action}";

            var alert = new List<string> { alertBody };

			return alert!;
		}
	}
}
