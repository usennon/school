﻿using CommunityToolkit.Mvvm.ComponentModel;
using ICS.Services;

namespace ICS.ViewModel;

    public abstract class ViewModelBase : ObservableRecipient, IViewModel
    {
        private bool _isRefreshRequired = true;

        protected readonly IMessengerService MessengerService;

        protected ViewModelBase(IMessengerService messengerService)
            : base(messengerService.Messenger)
        {
            MessengerService = messengerService;
            IsActive = true;
        }

        public async Task OnAppearingAsync()
        {
            if (_isRefreshRequired)
            {
                await LoadDataAsync();

                _isRefreshRequired = false;
            }
        }

        protected virtual Task LoadDataAsync()
            => Task.CompletedTask;
    }
