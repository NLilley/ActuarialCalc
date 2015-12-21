using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ActuarialCalc
{
    class ActuarialCalculator
    {

        public static Double calculateAnnuityInArrears(Double age, Double duration, Double discountRate, LifeTable mortalityTable, Double amountPayable)
        {
            if (age + duration > mortalityTable.maxAge || age < mortalityTable.minAge)
            {

                MessageBox.Show("The age entered is out of bounds");

                return 0d;

            }
            else
            {

                Double presentValue = 0;

                if (duration == -1d)
                {
                    duration = mortalityTable.maxAge - age;
                }



                for (int i = 1; i <= duration; i++)
                {
                    presentValue += (amountPayable * discountMultiplier(discountRate, i) * mortalityTable.probOfSurvTYears(age, i));

                }

                return presentValue;
            }
        }

        public static Double calculateAssurance(Double age, Double duration, Double discountRate, LifeTable mortalityTable, Double amountPayable)
        {
            if (age + duration > mortalityTable.maxAge || age < mortalityTable.minAge)
            {

                MessageBox.Show("The age entered is out of bounds");

                return 0d;

            }
            else
            {
                Double presentValue = 0;

                for (int i = 1; i <= duration; i++)
                {
                    presentValue += amountPayable * mortalityTable.probOfSurvTYears(age, i - 1) * (1 - mortalityTable.probOfSurvTYears(age + i - 1, 1)) * discountMultiplier(discountRate, i);

                }
                return presentValue;
            }

            
        }

        public static Double discountMultiplier(Double discountRate, Double duration)
        {
            Double v = 1 / (1 + (discountRate * 0.01d));

            return Math.Pow(v, duration);

        }

    }
}
