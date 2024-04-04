﻿using ICS.DAL;
using ICS.BL;
using Xunit;
using System.Threading.Tasks;
using System;
using ICS.BL.Facade.Interface;
using ICS.BL.Facade;
using ICS.BL.Models;
using System.Collections.ObjectModel;
using ICS.DAL.Context;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit.Abstractions;
using ICS.Common.Tests2.Seeds;

namespace ICS.BL.Tests;

[Collection("SQLite Tests")]
public sealed class SubjectFacadeTests : FacadeTestsBase, IAsyncLifetime
{
    private readonly ISubjectFacade _subjectFacadeSUT;
    //private SchoolTestingContext _context;
    //private IDbContextTransaction _transaction;

    public SubjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _subjectFacadeSUT = new SubjectFacade(UnitOfWorkFactory, SubjectModelMapper);
    }
    /*
    public async Task InitializeAsync()
    {
        // Инициализация контекста и начало транзакции
        var options = DbContextOptionsConfigurer.ConfigureSqliteOptions();
        _context = new SchoolTestingContext(options, true);
        await _context.Database.EnsureCreatedAsync();
        _transaction = await _context.Database.BeginTransactionAsync();
    }
    
    public async Task DisposeAsync()
    {
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        await _context.DisposeAsync();
    }*/


    [Fact]
    public async Task Create_WithNonExistingStudent_Throws()
    {
        var model = new SubjectDetailModel()
        {
            Id = Guid.Empty,
            name = "Database Systems",
            abbreviation = "IDS",
            students = new ObservableCollection<StudentListModel>()
            {
                new StudentListModel
                {
                    Id = Guid.NewGuid(),
                    firstName = "Pavel",
                    lastName = "Maslov"
                }
            }
        };

        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _subjectFacadeSUT.SaveAsync(model));
    }

    [Fact]
    public async Task GetAll_FromSeeded_ContainsSeeded()
    {
        //Arrange
        var listModel = SubjectModelMapper.MapToListModel(SubjectSeeds.potions);

        //Act
        var returnedModel = await _subjectFacadeSUT.GetAsync();

        //Assert
        Assert.Contains(listModel, returnedModel);
    }
}