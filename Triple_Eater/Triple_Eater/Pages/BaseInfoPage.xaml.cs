using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triple_Eater.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BaseInfoPage : BasePage<BaseInfoPage>
	{
		public BaseInfoPage ()
		{
			InitializeComponent ();
        }
	}
}