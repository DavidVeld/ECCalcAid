using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECCalcAidData.CodeTables
{
    [Serializable]
    public class ECCACodeTables
    {
        DataSet ListOfTables { get; set; }
        
        public ECCACodeTables()
        {
            //InitiateTables
            ConstructTables();
        }

        public ECCACodeTables(string fileName)
        {
            ListOfTables = new DataSet("ECCACodeTables");

            if (File.Exists(fileName))
            {
                ExcelReader myExcelReader = new ExcelReader();
                ListOfTables = myExcelReader.getExcelFile(fileName);

            }
        }

        private void ConstructTables()
        {
            //Load all tables from a excel file the excel can be monified to suit a national code
            {

            }
        }

        public bool Compile()
        {
            bool result = false;

            string _path = Assembly.GetExecutingAssembly().Location;
            string dbPth = Path.GetDirectoryName(_path) + "\\sys\\eccacodetables.bin";

            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(dbPth, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, this);
                stream.Close();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }

            return result;
        }
    }

}
