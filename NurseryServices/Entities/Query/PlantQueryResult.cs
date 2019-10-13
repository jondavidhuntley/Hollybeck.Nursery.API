using Hollybeck.Domain.Entities;

namespace NurseryServices.Entities.Query
{
    public class PlantQueryResult : BaseQueryResult
    {
        public Plant SelectedPlant { get; set; }
    }
}