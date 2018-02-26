using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ECCalcAidData.Elements;

namespace ECCalcAidData.Collections
{
    [Serializable]
    public class ECCAMaterialCollection
    {
        public IList<ECCAMaterial> MaterialList;

        public ECCAMaterialCollection()
        {
            MaterialList = new List<ECCAMaterial>();
        }

        public void CreateNewMaterial()
        {
            ECCAMaterial NewMaterial = new ECCAMaterial();
            if (getMaterial(NewMaterial.MaterialName) == null)
            {
                MaterialList.Add(NewMaterial);
            }
            else
            {
                MessageBox.Show("The material you are trying to import or create allready exists");
            }
        }

        public ECCAMaterial getMaterial(string materialname)
        {
            ECCAMaterial result = null;

            if(MaterialList.Count > 0)
            {
                foreach(ECCAMaterial eccaMaterial in MaterialList)
                {
                    if(eccaMaterial.MaterialName == materialname)
                    {
                        result = eccaMaterial;
                        break;
                    }
                }
            }
            return result;
        }

        internal IList<string> GetCategories()
        {
            IList<string> result = new List<string>();

            // loop through our list
            foreach (ECCAMaterial eccamaterial in MaterialList)
            {
                // if our list of car models doesn't have this model in it, add it.
                if (!result.Contains(eccamaterial.CategoryName))
                {
                    result.Add(eccamaterial.CategoryName);
                }
            }
            return result;
        }

        internal IList<ECCAMaterial> Query(string searchquery)
        {
            throw new NotImplementedException();
        }
    }
}