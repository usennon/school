﻿using ICS.BL.Mappers;

namespace ICS.BL.Mappers;

public abstract class ModelMapperBase<TEntity, TListModel, TDetailModel> : IModelMapper<TEntity, TListModel, TDetailModel>
{
    public abstract TListModel MapToListModel(TEntity? entity);
    public abstract TDetailModel MapToDetailModel(TEntity? entity);
    public abstract TEntity MapDetailModelToEntity(TDetailModel model);
    public IEnumerable<TListModel> MapToListModel(IEnumerable<TEntity> entities)
=> entities.Select(MapToListModel);
}
