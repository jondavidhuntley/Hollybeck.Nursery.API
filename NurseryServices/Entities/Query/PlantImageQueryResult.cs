using Hollybeck.Domain.Entities;

namespace NurseryServices.Entities.Query
{
    public class PlantImageQueryResult : BaseQueryResult
    {
        public ImageDetail PlantImageDetails { get; set;}
    }
}