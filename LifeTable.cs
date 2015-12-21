using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ActuarialCalc
{
    class LifeTable
    {
        public List<Double[]> tableData = new List<Double[]>();

        public Double minAge;
        public Double maxAge;
        
        public void AddTableData(Double age, Double lives)
        {
            Double[] yearData = new Double[2];
            yearData[0] = age;
            yearData[1] = lives;

            tableData.Add(yearData);
        }

        public Double probOfSurvTYears(Double age, Double time)
        {
         
            if (age + time > maxAge)
            {
                return probOfSurvTYears(age, maxAge - age);

            }
            else
            {
                Double[] lives1 = new Double[2];
                Double[] lives2 = new Double[2];

                lives1 = tableData[(int)(age - minAge)];
                lives2 = tableData[(int)(age + time - minAge)];

                return (lives2[1] / lives1[1]);
            }
           
        }

        public void tableBuilder(String filePath)
        {
            tableData.Clear();

            if(System.IO.File.Exists(filePath)) {

                using (System.IO.StreamReader sr = System.IO.File.OpenText(filePath))
                {
                    string s = "";
                    int lineNumber = 0;
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (lineNumber != 0)
                        {
                            Double age;
                            Double lives;

                            int i = 0;
                            String temp = "";
                            while (s[i] != ' ')
                            {
                                temp += s[i];
                                i++;
                                
                            }
                            age = Convert.ToDouble(temp);
                            
                            //Only works if we have life tables of a very specific format.

                            if (age < minAge)
                            {
                                minAge = age;
                            }
                            if (age > maxAge)
                            {
                                maxAge = age;
                            }

                            temp = "";

                            temp = s.Substring(i, s.Length - i);

                            lives = Convert.ToDouble(temp);

                            AddTableData(age, lives);

                            lineNumber++;

                            
                        }
                        else
                        {
                            lineNumber++;
                        }
                        
                    }
                }

            } else {
                Console.WriteLine("ERROR: Could not find a file at the path specified.");
            }

        }

        public void tableBuilderFromMYSQL(MySQLController cont, String databaseTable)
        {
            tableData.Clear();

            List<List<String>> queryResults = new List<List<String>>();

            queryResults = cont.performQuery("SELECT * FROM ACTUARIALDATA." + databaseTable);

            for (int i = 0; i < queryResults.Count; i++)
            {
                Double[] dataRow = new Double[2];
                for (int j = 0; j < 2; j++)
                {
                    dataRow[j] = Convert.ToDouble(queryResults[i][j]);
                }

                AddTableData(dataRow[0], dataRow[1]);

                if (dataRow[0] < minAge)
                {
                    minAge = dataRow[1];
                }
                if (dataRow[0] > maxAge)
                {
                    maxAge = dataRow[1];
                }
                
            }

            

        }
 
    }

}
