﻿using ICS.BL;
using ICS.BL.Models;
using ICS.DAL.Entities;
using System.Collections.ObjectModel;


namespace ICS.BL.Mappers;

public class StudentModelMapper(StudentSubjectModelMapper studentSubjectModelMapper) : ModelMapperBase<StudentEntity, StudentListModel, StudentDetailModel>, IStudentModelMapper
{
    public override StudentListModel MapToListModel(StudentEntity? entity)
         => entity is null
        ? StudentListModel.Empty
        : new StudentListModel 
        {
            Id = entity.Id,
            FirstName = entity.FirstName, 
            LastName = entity.LastName
        };

    public override StudentDetailModel MapToDetailModel(StudentEntity? entity)
    {
        if (entity is null)
            return StudentDetailModel.Empty;

        var detailModel = new StudentDetailModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            PhotoURL = entity.PhotoUrl
        };

        if (studentSubjectModelMapper != null)
        {              
            detailModel.Subjects = studentSubjectModelMapper.MapToListModel(entity.Subjects).ToObservableCollection();
        }
        else
        {               
            detailModel.Subjects = new ObservableCollection<StudentSubjectListModel>();
        }

        return detailModel;
    }




    public override StudentEntity MapDetailModelToEntity(StudentDetailModel model)
    {
        return new StudentEntity
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhotoUrl = model.PhotoURL
        };
    }

}
