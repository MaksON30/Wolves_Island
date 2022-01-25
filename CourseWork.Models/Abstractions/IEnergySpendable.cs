using System;

namespace CourseWork.Models.Abstractions
{
    public interface IEnergySpendable
    {
        public double Energy { get; set; }
        public void SpentEnergy(double spentEnergy);
        public void ReproduceEnergy(double energy);

    }
}