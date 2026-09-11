using Microsoft.EntityFrameworkCore;
using Naringskollen.Dtos.FoodMeasurementsDtos.In;
using Naringskollen.Models;
using Naringskollen.Repositories.IRepositories;
using Naringskollen.Services.IServices;

namespace Naringskollen.Services
{
    public class FoodMeasurementService : IFoodMeasurementService
    {
        private readonly IFoodMeasurementRepository foodMeasurementRepository;

        public FoodMeasurementService(IFoodMeasurementRepository _foodMeasurementRepository)
        {
            foodMeasurementRepository = _foodMeasurementRepository;
        }

        public async Task UpdateMeasurementsForFoodAsync(Food updateFood, List<UpdateFoodMeasurementDto> dtos)
        {
            if (!dtos.Any() || dtos == null) return;

            if (dtos.Any(fm => fm.Id <= 0 || fm == null || fm.Unit == null || fm.Grams == null))
            {
                throw new ArgumentException("Enhet får inte innehålla null-värden.");
            }

            var existingIds = updateFood.FoodMeasurements.Select(fm => fm.Id).ToList();
            var requestedIds = dtos.Select(d => d.Id).ToList();

            if (!requestedIds.All(id => existingIds.Contains(id)))
            {
                throw new KeyNotFoundException("En eller flera angivna enheter hör inte till livsmedlet.");
            }

            var existingfoodMeasurements = await foodMeasurementRepository.GetListByIdsAsync(existingIds);
            if (!existingfoodMeasurements.Any())
            {
                throw new KeyNotFoundException("Data för vald enhetsomvandling kunde inte hittas");
            }

            foreach (var measurement in existingfoodMeasurements)
            {
                var dtoItem = dtos.FirstOrDefault(fm => fm.Id == measurement.Id);
                measurement.UnitName = dtoItem.Unit!.ToString();
                measurement.GramWeight = dtoItem.Grams!.Value;
            }

            var updatedFoodMeasurements = await foodMeasurementRepository.UpdateAsync(existingfoodMeasurements);
            if (!updatedFoodMeasurements)
            {
                throw new DbUpdateException("Inga ändringar för enhetsomvandling sparades i databasen.");
            }
        }
    }
}
