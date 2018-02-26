using ECCalcAidData.Elements;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ECCalcAidData.Collections
{
    [Serializable]
    public class ECCASectionCollection
    {
        public IList<ECCASection> SectionList;

        public ECCASectionCollection()
        {
            SectionList = new List<ECCASection>();
        }

        public void CreateNewSection()
        {
            ECCASection NewSection = new ECCASection();
            SectionList.Add(NewSection);
        }

        public DataTable ToDataTable(string searchCondition)
        {
            //PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(ECCASection));

            FieldInfo[] fieldValues = typeof(ECCASection).GetFields();

            DataTable table = new DataTable();
            for (int i = 0; i < fieldValues.Length; i++)
            {
                FieldInfo field = fieldValues[i];
                table.Columns.Add(field.Name, field.FieldType);
            }

            object[] values = new object[fieldValues.Length];

            foreach (ECCASection item in SectionList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = fieldValues[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public void CreateDummySection()
        {
            ECCASection NewSection = new ECCASection();

            NewSection.Designation = "UCTest";

            if (IsUnique(NewSection))
            {
                SectionList.Add(NewSection);
            }
        }

        public IList<string> GetCategories()
        { 
            IList<string> result = new List<string>();

            // loop through our list
            foreach (ECCASection eccasection in SectionList)
            {
                if (!result.Contains(eccasection.CategoryName))
                {
                    result.Add(eccasection.CategoryName);
                }
            }
            return result;
        }

        private bool IsUnique(ECCASection newSection)
        {
            bool result = true;
            if (getSection(newSection.Designation) == null)
            {
                result = true;
            }
            return result;
        }

        public ECCASection getSection(string newSectionDesignation)
        {
            ECCASection result = null;

            if (SectionList.Count > 0)
            {
                foreach (ECCASection eccaSection in SectionList)
                {
                    if (eccaSection.Designation == newSectionDesignation)
                    {
                        result = eccaSection;
                        break;
                    }
                }
            }
            return result;
        }

        public void AddCollection(IList<ECCASection> formattedSectionList, bool overwrite = false)
        {
            foreach(ECCASection eccasection in formattedSectionList)
            {
                if(IsUnique(eccasection))
                {
                    this.SectionList.Add(eccasection);

                }
                else
                {
                    //Update
                    if(overwrite == true)
                    {
                        this.AddAndReplace(eccasection);
                    }

                }
            }
        }

        private void AddAndReplace(ECCASection newEccasection)
        {
            for(int i =0; i<SectionList.Count; i++) 
            {
                ECCASection eccasection = SectionList[i];

                if (eccasection.Designation == newEccasection.Designation)
                {
                    eccasection = newEccasection;
                }
            }
        }

        internal IList<ECCASection> Query(string v)
        {
            IList<ECCASection> result = new List<ECCASection>();

            foreach(ECCASection eccasection in SectionList)
            {
                if (eccasection.Designation.Contains(v))
                    result.Add(eccasection);
            }
            if (result.Count > 0)
                return result;
            else
                return null;
        }
    }

}
