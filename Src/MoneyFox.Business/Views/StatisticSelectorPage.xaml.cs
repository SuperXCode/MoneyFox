﻿using MoneyFox.Business.Styles;
using MoneyFox.Business.ViewModels.Statistic;
using MoneyFox.Foundation;
using MoneyFox.Foundation.Interfaces;
using MvvmCross.Platform;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyFox.Business.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StatisticSelectorPage : ContentPage
	{
		public StatisticSelectorPage ()
		{
			InitializeComponent ();

		    StatisticSelectorList.ItemTapped += (sender, args) =>
		    {
		        StatisticSelectorList.SelectedItem = null;
                ((IStatisticSelectorViewModel) BindingContext).GoToStatisticCommand.Execute(args.Item);
		    };

		    if (Mvx.Resolve<ISettingsManager>().Theme == AppTheme.Dark)
		    {
		        Resources = new AppStylesDark();
		    }
		    else
		    {
		        Resources = new AppStylesLight();
		    }
		}
    }
}