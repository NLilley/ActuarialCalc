using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActuarialCalc
{
    class DatabaseInitializer
    {
        MySQLController controller;

        public DatabaseInitializer(MySQLController cont)
        {
            controller = cont;
        }

        public void fillTable (LifeTable table) {

            table.tableBuilder("ELT15(FEMALES).txt");
            for (int i = 0; i < table.tableData.Count; i++)
            {
                String command;
                command = "INSERT INTO ACTUARIALDATA.ELT15FEMALES VALUES ("+ table.tableData[i][0] + "," + table.tableData[i][1] + ")";

                controller.performNonQuery(command);
            }

        }

        //public void fillTable(String filePath)
        //{

        //    if (System.IO.File.Exists(filePath))
        //    {
        //        using (System.IO.StreamReader sr = System.IO.File.OpenText(filePath))
        //        {

        //            String s = "";
        //            while ((s = sr.ReadLine()) != null)
        //            {
                        
        //            }

        //        }
        //    }

        //}

    }
}
