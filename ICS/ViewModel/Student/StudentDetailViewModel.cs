﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICS.BL.Facade.Interface;
using ICS.BL.Models;
using ICS.Messages;
using ICS.Messages.SubjectMessages;
using ICS.Services;


namespace ICS.ViewModel.Student
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class StudentDetailViewModel(
        IStudentFacade studentFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : ViewModelBase(messengerService), IRecipient<StudentEditMessage>
    {
        public Guid Id { get; set; }
        public StudentDetailModel? Student { get; private set; }
  

        [RelayCommand]
        public async Task DeleteAsync()
        {
            if (Student is not null)
            {
                try
                {
                    await studentFacade.DeleteAsync(Student.Id);
                    MessengerService.Send(new StudentDeleteMessage());
                    navigationService.SendBackButtonPressed();
                }
                catch (InvalidOperationException)
                {
                    await alertService.DisplayAsync("ERROR", "ERROR");
                }
            }
        }

        [RelayCommand]
        public async Task GoToEditAsync()
        {
            await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(StudentEditViewModel.Student)] = Student });
        }
        [RelayCommand]
        public async Task GoToSubjectsAsync()
        {
            await navigationService.GoToAsync("/subjects",
            new Dictionary<string, object?> { [nameof(StudentSubjectViewModel.Id)] = Student!.Id });
        }



        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Student = await studentFacade.GetAsync(Id);

            if (Student is null)
            {
                await alertService.DisplayAsync("Error", "Student was not found or deleted");
                MessengerService.Send(new StudentDeleteMessage());
                navigationService.SendBackButtonPressed();
            }
        }

        public async void Receive(StudentEditMessage message)
        {
            if (message.StudentId == Student?.Id)
            {
                await LoadDataAsync();
            }
        }
 
    }

}
