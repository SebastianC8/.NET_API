using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Implementation
{
    public class RandomImplCore : IRandomCore
    {
        private readonly int _value;

        public int Value
        {
            get => _value;
        }

        public RandomImplCore()
        {
            _value = new Random().Next(1000);
        }
    }
}
