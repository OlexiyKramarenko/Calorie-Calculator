﻿using CalorieCalculator.DTO;
using CalorieCalculator.Extensions;
using CalorieCalculator.Extensions.Functional;
using System;
using System.Collections.Generic;

namespace CalorieCalculator.POCO
{
    public class ProductModel 
    {
        #region State

        private float _proteins { get; set; }
        private float _fat { get; set; }
        private float _carbohydrates { get; set; }
        private float _kcal { get; set; }
        private float _calcium { get; set; }
        
        #endregion

        private ProductModel() { }

        public static ProductModel Create(IEnumerable<ProductAmount> productAmounts) =>
            new ProductModel().Append(productAmounts);

        public double Proteins { get => Math.Round(_proteins); }
        public double Fats { get => Math.Round(_fat); }
        public double Carbohydrates { get => Math.Round(_carbohydrates); }
        public double Kcal { get => Math.Round(_kcal); }
        public double Calcium { get => Math.Round(_calcium, 2); }

        public override string ToString() =>
           $"Proteins: {ProteinsKcalPercentage()} % {Environment.NewLine}Fats: {FatsKcalPercentage()} % {Environment.NewLine}Carbohydrates: {CarbohydratesKcalPercentage()} %";

        #region Private Methods
        
        private ProductModel Append(IEnumerable<ProductAmount> productAmounts)
        {
            productAmounts.ForEach(a => Append(a.Product, a.Amount));
            return this;
        }

        private void Append(Product product, float amount)
        {
            _proteins += product.Protein * amount / 100;
            _fat += product.Fat * amount / 100;
            _carbohydrates += product.Carbohydrates * amount / 100;
            _kcal += product.Kcal * amount / 100;
            _calcium += product.Calcium * amount / 100;
        }

        private double ProteinsKcalPercentage() =>
            Math.Floor(100 / _kcal * _proteins * 4);

        private double FatsKcalPercentage() =>
            Math.Floor(100 / _kcal * _fat * 9);

        private double CarbohydratesKcalPercentage() =>
            Math.Floor(100 / _kcal * _carbohydrates * 4);
        
        #endregion

    }
}
