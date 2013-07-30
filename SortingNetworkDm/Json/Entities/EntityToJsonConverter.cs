using System;
using DynamicModel.Model;
using SortingNetworkDm.Entities;

namespace SortingNetworkDm.Json.Entities
{
    public static class EntityToJsonConverter
    {
        public static object ToJson(this IEntity entity)
        {
            switch (entity.TypeName)
            {
                case SorterResultPoolEntity.TypeName:
                    return SorterResultPoolEntityToJson.ToJson((ISorterResultPoolEntity)entity);
                case SorterPoolEntity.TypeName:
                    return SorterPoolEntityToJson.ToJson((ISorterPoolEntity)entity);
                case SwitchablePoolEntity.TypeName:
                    return SwitchablePoolEntityToJson.ToJson((ISwitchablePoolEntity)entity);
                default:
                    throw new Exception(entity.TypeName + " not handled in StepToJsonConverter.ToJson");
            }
        }
    }
}
