using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // Use for create Table

namespace DataSET
{
    class Program
    {
        static void Main(string[] args)
        {
            // ********************************************
            // step 1 Create all table that are required  *
            // ********************************************

            // t1 mean Table 1 and word in double qoute is the name of Table
            DataTable t1University = new DataTable("University");
            
            // now create column of the table
            // word in double qoute is the name of the field and 2nd parameter is type of
            // the field
            DataColumn uniID = new DataColumn("ID", typeof(int));
            DataColumn uniName = new DataColumn("Name", typeof(string));

            // Make uniID column auto increment
            uniID.AutoIncrement = true;
            uniID.AutoIncrementSeed = 1; // Starting Value
            uniID.AutoIncrementStep = 1; // Specify the incerement size

            // add column to the table
            t1University.Columns.Add(uniID); // add uniID column
            t1University.Columns.Add(uniName); // add uni name Column
            t1University.PrimaryKey = new DataColumn[] { uniID };

            // 2nd Table
            DataTable t2Student = new DataTable("Student");
            DataColumn stdID = new DataColumn("ID", typeof(int));
            DataColumn stdName = new DataColumn("Name", typeof(string));
            DataColumn stdAge = new DataColumn("Age", typeof(int));
            DataColumn stdUni = new DataColumn("University", typeof(int));

            stdID.AutoIncrement = true;
            stdID.AutoIncrementSeed = 1;
            stdID.AutoIncrementStep = 1;

            t2Student.Columns.Add(stdID);
            t2Student.Columns.Add(stdName);
            t2Student.Columns.Add(stdAge);
            t2Student.Columns.Add(stdUni);
            t2Student.PrimaryKey = new DataColumn[] { stdID};
            // step 1 completed

            // *******************************************************
            // Step 2 create the dataSet and add all table into it  **
            // *******************************************************
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(t1University);
            dataSet.Tables.Add(t2Student);

            // **************************************************************
            // step 3 create relation between tables and add into data set **
            // **************************************************************
            DataRelation STD_UNI = new DataRelation("STD_UNI", t1University.Columns["ID"], t2Student.Columns["University"]);
            dataSet.Relations.Add(STD_UNI);

            // *********************************************
            // Step 4 Now add data into the table 1 by 1, **
            // *********************************************
            DataRow r1 = t1University.NewRow();
            DataRow r2 = t1University.NewRow();
            DataRow r3 = t1University.NewRow();

            // As uni Table contain only 2 column, 1 is primary key and auto updated
            r1["Name"] = "Punjab univesity";
            r2["Name"] = "Sargodha University";
            r3["Name"] = "Virtual University";

            t1University.Rows.Add(r1);
            t1University.Rows.Add(r2);
            t1University.Rows.Add(r3);

            DataRow r4 = t2Student.NewRow();
            DataRow r5 = t2Student.NewRow();
            DataRow r6 = t2Student.NewRow();
            DataRow r7 = t2Student.NewRow();
            DataRow r8 = t2Student.NewRow();
            DataRow r9 = t2Student.NewRow();
            DataRow r10 = t2Student.NewRow();
            DataRow r11 = t2Student.NewRow();
            DataRow r12 = t2Student.NewRow();
            DataRow r13 = t2Student.NewRow();


            r4["Name"] = "Tassawer Hussain";
            r4["Age"] = 23;
            r4["University"] = 1;

            r5["Name"] = "Abdullah Khan";
            r5["Age"] = 21;
            r5["University"] = 1;

            r6["Name"] = "Ehsaan Israr";
            r6["Age"] = 21;
            r6["University"] = 2;

            r7["Name"] = "Sajjad NAveed";
            r7["Age"] = 22;
            r7["University"] = 3;

            r8["Name"] = "Salman Khan";
            r8["Age"] = 22;
            r8["University"] = 1;

            r9["Name"] = "Fatima Sajjad";
            r9["Age"] = 21;
            r9["University"] = 3;

            r10["Name"] = "Marium Ramzan";
            r10["Age"] = 21;
            r10["University"] = 3;

            r11["Name"] = "Anam Ramzan";
            r11["Age"] = 22;
            r11["University"] = 2;

            r12["Name"] = "Amina Rida";
            r12["Age"] = 21;
            r12["University"] = 1;

            r13["Name"] = "Ghania Munir";
            r13["Age"] = 21;
            r13["University"] = 2;

            t2Student.Rows.Add(r4);
            t2Student.Rows.Add(r5);
            t2Student.Rows.Add(r6);
            t2Student.Rows.Add(r7);
            t2Student.Rows.Add(r8);
            t2Student.Rows.Add(r9);
            t2Student.Rows.Add(r10);
            t2Student.Rows.Add(r11);
            t2Student.Rows.Add(r12);
            t2Student.Rows.Add(r13);
            
            // ***********************************
            // Step 5 Accessing Data using loop **
            // ***********************************
            //foreach (DataRow Row in t1University.Rows)
            //{
            //    Console.WriteLine("University ID: {0}", Row[0]);
            //    Console.WriteLine("University Name: {0}", Row[1]);
            //    Console.WriteLine();
            //}

            //foreach (DataRow Row in t2Student.Rows)
            //{
            //    Console.WriteLine("Student ID: {0}", Row[0]);
            //    Console.WriteLine("Student Name: {0}", Row[1]);
            //    Console.WriteLine("Student Age: {0}", Row[2]);
            //    Console.WriteLine("Student University: {0}", Row[3]);
            //    Console.WriteLine();
            
            //}

            foreach (DataRow Row in t1University.Rows)
            {
                // Here Row contain data of each record in uni table and display it
                Console.WriteLine("University ID: {0}", Row[0]);
                Console.WriteLine("University Name: {0}", Row[1]);

                // Now get Child rows
                // mean which record is matching against this value in the second table
                DataRow[] childRows = Row.GetChildRows("STD_UNI"); // STD_UNI is relation b/w table

                Console.WriteLine("----------------------------------");
                foreach (DataRow childRow in childRows)
                {
                    Console.WriteLine("Student ID: {0}", childRow["ID"]);
                    Console.WriteLine("Student Name: {0}", childRow["Name"]);
                    Console.WriteLine("Student Age: {0}", childRow["Age"]);

                    // Getting Parents Row
                    DataRow parentRow = childRow.GetParentRow("STD_UNI");
                    Console.WriteLine("University ID: {0}", parentRow[0]);
                    Console.WriteLine("University Name: {0}", parentRow[1]);
                    Console.WriteLine();
                }
                Console.WriteLine("----------------------------------");
            }



            // Aggregations Topic
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Aggregations");
            
            //Compute is an extension method, just like Select. It is used to call SQL methods
            object Avg = t2Student.Compute("Avg(Age)", "");

            //with filter condition
            object Sum = t2Student.Compute("Sum(age)", "University=1");

            Console.WriteLine("Avg Age of Students: " + Avg);
            Console.WriteLine("Sum of Ages of Students: " + Sum);
            Console.WriteLine("----------------------------------------------");
            
            STD_UNI.ChildKeyConstraint.DeleteRule = Rule.None;

            Console.WriteLine("University Table Count = " + t1University.Rows.Count);
            Console.WriteLine("Student Table Count = " + t2Student.Rows.Count);

            Console.ReadKey();
        }
    }
}
