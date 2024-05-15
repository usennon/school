﻿using CommunityToolkit.Mvvm.Input;
using ICS.BL.Facade.Interface;
using ICS.BL.Models;
using ICS.Services;
using ICS.Messages;
using ICS.ViewModel.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.AllJoyn;
using ICS.Messages.SubjectMessages;
using CommunityToolkit.Mvvm.Messaging;

namespace ICS.ViewModel.Subject;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(SubjectId), nameof(SubjectId))]
public partial class SubjectStudentViewModel(
    IStudentSubjectFacade studentSubjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<SubjectStudentAddMessage>
{
    public Guid Id { get; set; }

    public Guid SubjectId { get; set; }
    public IEnumerable<StudentSubjectListModel> Students { get; set; } = null!;


    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Students = await studentSubjectFacade.GetStudentsAsync(Id);
    }

    [RelayCommand]
    private async Task GoToAddAsync()
    {
        await navigationService.GoToAsync("/add",
        new Dictionary<string, object?> { [nameof(SubjectStudentAddViewModel.SubjectId)] = SubjectId });
    }

    [RelayCommand]
    private async Task DeleteAsync(Guid id)
    {
        try
        {
            await studentSubjectFacade.DeleteAsync(id);
            MessengerService.Send(new StudentSubjectDeleteMessage());
            await LoadDataAsync();

        }
        catch (InvalidOperationException)
        {
            await alertService.DisplayAsync("ERROR", "ERROR");
        }

    }

    public async void Receive(SubjectStudentAddMessage message)
    {
       await LoadDataAsync();
    }
    public async void Receive(SubjectEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(SubjectDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(SubjectAddMessage message)
    {
        await LoadDataAsync();
    }
}