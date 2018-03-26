using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressParserLib
{
    class AddressVersion : IEnumerable
    {
        
        private List<AddressObject> objects;

        /// <summary>
        /// Вероятность, что адрес правильный. Максимальное значение - 9
        /// Индекс - 3 балла.
        /// Регион - 1 балл.
        /// Район - 1 балл.
        /// Населённый пункт - 1 балл.
        /// Дом - 1 балл.
        /// Комната - 1 балл.
        /// </summary>
        public short ProbabilityCorrectAddress { get; private set; }

        public AddressVersion(List<AddressObject> objects)
        {
            this.objects = objects;

            CalcProbability();
        }

        private void CalcProbability()
        {
            //Формируем верояность корректности адреса
            bool postalCodeFinded = false;
            foreach (var obj in objects)
            {
                if (obj.Type == ObjectType.PostalCode)
                {
                    ProbabilityCorrectAddress += 3;
                    postalCodeFinded = true;
                }
            }

            if (objects.Count <= 6)
            {
                if (postalCodeFinded)
                {
                    ProbabilityCorrectAddress += (short) (objects.Count - 1);
                }
                else
                {
                    ProbabilityCorrectAddress += (short)(objects.Count);
                }
            }
            else
            {
                ProbabilityCorrectAddress += 6;
                ProbabilityCorrectAddress -= (short) (objects.Count - 6);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return objects.GetEnumerator();
        }
    }
}
