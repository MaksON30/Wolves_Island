using System;
using CourseWork.Models.Abstractions;

namespace CourseWork.Models
{
    public abstract class WolfBase : IEnergySpendable
    {
        private double _energy;
        
        public bool IsAlive { get; private set; }

        public double Energy
        {
            get => _energy;
            set
            {
                _energy = value;
                if (_energy <= 0)
                {
                    IsAlive = false;
                }
            }
        }
        protected WolfBase()
        {
            Energy = 1;
            IsAlive = true;
        }
        public void  SpentEnergy(double spentEnergy)
       {
           Energy -= spentEnergy;
       }

        public void ReproduceEnergy(double energy)
        {
            Energy = energy;
        }
    }
}