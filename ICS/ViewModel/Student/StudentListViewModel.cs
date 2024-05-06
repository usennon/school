﻿using ICS.BL.Facade.Interface;
using ICS.Services;
using ICS.BL.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICS.Messages;

namespace ICS.ViewModel.Student
{
    public partial class StudentListViewModel(
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
        : ViewModelBase(messengerService), IRecipient<StudentEditMessage>, IRecipient<StudentDeleteMessage>, IRecipient<StudentAddMessage>
    {
        public IEnumerable<StudentListModel> Students { get; set; } = null!;

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Students = await studentFacade.GetAsync();
        }

        [RelayCommand]
        private async Task GoToCreateAsync()
        {
            await navigationService.GoToAsync("/edit");
        }

        [RelayCommand]
        private async Task GoToDetailAsync(Guid id)
        {
            await navigationService.GoToAsync<StudentDetailViewModel>(
            new Dictionary<string, object?> { [nameof(StudentDetailViewModel.Student)] = id });
        }

        public async void Receive(StudentEditMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(StudentDeleteMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(StudentAddMessage message)
        {
            await LoadDataAsync();
        }
    }
}