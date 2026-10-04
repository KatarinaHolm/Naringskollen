using Naringskollen.Dtos.FoodMeasurementsDtos.In;
using Naringskollen.Models;
using Naringskollen.Models.Enums;
using Naringskollen.Services.IServices;

namespace Naringskollen.Services
{
    public class FoodMeasurementService : IFoodMeasurementService
    {
        private static readonly HashSet<FoodMeasurementUnit> AllowedUnits =
        [
            FoodMeasurementUnit.styck,
            FoodMeasurementUnit.skiva,
            FoodMeasurementUnit.dl
        ];

        public void ValidateCreateMeasurements(List<CreateFoodMeasurementDto>? dtos)
        {
            if (dtos == null)
            {
                throw new ArgumentException("Måttenhetslistan måste skickas.");
            }

            ValidateMeasurements(dtos.Select(dto => (dto?.Unit, dto?.Grams)));
        }

        public void ReplaceMeasurementsForFood(Food food, List<UpdateFoodMeasurementDto>? dtos)
        {
            if (dtos == null)
            {
                throw new ArgumentException("Måttenhetslistan måste skickas.");
            }

            ValidateMeasurements(dtos.Select(dto => (dto?.Unit, dto?.Grams)));

            var currentMeasurements = food.FoodMeasurements;
            var existingById = currentMeasurements.ToDictionary(measurement => measurement.Id);
            var requestedIds = dtos
                .Where(dto => dto?.Id.HasValue == true)
                .Select(dto => dto!.Id!.Value)
                .ToList();

            if (requestedIds.Count != requestedIds.Distinct().Count())
            {
                throw new ArgumentException("Samma måttenhetsrad får inte anges flera gånger.");
            }

            if (requestedIds.Any(id => !existingById.ContainsKey(id)))
            {
                throw new ArgumentException("En eller flera måttenhetsrader hör inte till livsmedlet.");
            }

            var requestedIdSet = requestedIds.ToHashSet();
            currentMeasurements.RemoveAll(measurement => !requestedIdSet.Contains(measurement.Id));

            foreach (var dto in dtos)
            {
                var unit = dto!.Unit!.Value;
                var grams = dto.Grams!.Value;

                if (dto.Id.HasValue)
                {
                    var existingMeasurement = existingById[dto.Id.Value];
                    existingMeasurement.Unit = unit;
                    existingMeasurement.GramWeight = grams;
                }
                else
                {
                    currentMeasurements.Add(new FoodMeasurement
                    {
                        Unit = unit,
                        GramWeight = grams,
                        FoodId = food.Id
                    });
                }
            }

        }

        private static void ValidateMeasurements(IEnumerable<(FoodMeasurementUnit? Unit, decimal? Grams)> measurements)
        {
            var units = new HashSet<FoodMeasurementUnit>();

            foreach (var (unit, grams) in measurements)
            {
                if (!unit.HasValue || !grams.HasValue)
                {
                    throw new ArgumentException("Varje måttenhet måste ha en enhet och en vikt i gram.");
                }

                if (!AllowedUnits.Contains(unit.Value))
                {
                    throw new ArgumentException("Endast styck, skiva och dl kan användas som måttenheter.");
                }

                if (grams.Value < 0 || grams.Value > 10000)
                {
                    throw new ArgumentException("Vikten måste ligga mellan 0 och 1000 gram.");
                }

                if (!units.Add(unit.Value))
                {
                    throw new ArgumentException("Samma måttenhet får bara anges en gång per livsmedel.");
                }
            }
        }
    }
}
