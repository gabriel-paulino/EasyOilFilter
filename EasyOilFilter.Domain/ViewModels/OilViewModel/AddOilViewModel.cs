﻿using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.OilViewModel
{
    public class AddOilViewModel
    {
        public string Name { get; set; }
        public string Viscosity { get; set; }
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
        public string Type { get; set; }
        public string UnitOfMeasurement { get; set; }

        public static implicit operator Oil(AddOilViewModel model) =>
            new(
                name: model.Name,
                viscosity: model.Viscosity,
                price: model.Price,
                stockQuantity: model.StockQuantity,
                type: EnumUtility.GetEnumByDescription<OilType>(model.Type),
                unitOfMeasurement: EnumUtility.GetEnumByDescription<UoM>(model.UnitOfMeasurement)
                );
    }
}