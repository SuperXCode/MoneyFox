using GalaSoft.MvvmLight;
using MediatR;
using MoneyFox.Application.Common.Extensions;
using MoneyFox.Application.Common.Facades;
using MoneyFox.Application.Common.Messages;
using MoneyFox.Application.Resources;
using MoneyFox.Ui.Shared.Commands;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace MoneyFox.Uwp.ViewModels.Statistic
{
    /// <summary>
    /// Represents the statistic view.
    /// </summary>
    public abstract class StatisticViewModel : ViewModelBase
    {
        private DateTime startDate;
        private DateTime endDate;

        protected readonly IMediator Mediator;

        /// <summary>
        /// Creates a StatisticViewModel Object and passes the first and last day of the current month     as a start
        /// and end date.
        /// </summary>
        protected StatisticViewModel(IMediator mediator, ISettingsFacade settingsManager) : this(DateTime.Today.GetFirstDayOfMonth(),
                                                                                                 DateTime.Today.GetLastDayOfMonth(),
                                                                                                 mediator,
                                                                                                 settingsManager)
        {
        }

        /// <summary>
        /// Creates a Statistic ViewModel with custom start and end date
        /// </summary>
        protected StatisticViewModel(DateTime startDate,
                                     DateTime endDate,
                                     IMediator mediator,
                                     ISettingsFacade settingsFacade)
        {
            StartDate = startDate;
            EndDate = endDate;
            Mediator = mediator;

            MessengerInstance.Register<DateSelectedMessage>(this,
                                                            async message =>
                                                            {
                                                                StartDate = message.StartDate;
                                                                EndDate = message.EndDate;
                                                                await LoadAsync();
                                                            });
        }

        public AsyncCommand LoadedCommand => new AsyncCommand(LoadAsync);

        /// <summary>
        /// Start date for a custom statistic
        /// </summary>
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                RaisePropertyChanged();
                // ReSharper disable once ExplicitCallerInfoArgument
                RaisePropertyChanged(nameof(Title));
            }
        }

        /// <summary>
        /// End date for a custom statistic
        /// </summary>
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                RaisePropertyChanged();
                // ReSharper disable once ExplicitCallerInfoArgument
                RaisePropertyChanged(nameof(Title));
            }
        }

        /// <summary>
        /// Returns the title for the CategoryViewModel view
        /// </summary>
        public string Title
                      => $"{Strings.StatisticsTimeRangeTitle} {(StartDate.ToString("d", CultureInfo.InvariantCulture))} - {(EndDate.ToString("d", CultureInfo.InvariantCulture))}";

        protected abstract Task LoadAsync();
    }
}
