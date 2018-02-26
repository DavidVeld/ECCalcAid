using ECCalcAidData.Elements;
using System;
using System.Collections.Generic;

namespace ECCalcAidData.Collections
{
    [Serializable]
    public class ECCABeamCollection
    {
        public IList<ECCABeam> BeamCollectionList;

        public ECCABeamCollection()
        {
            this.BeamCollectionList = new List<ECCABeam>();
        }

        public void AddBeam(ECCABeam beamToAdd)
        {
            if(IsUnique(beamToAdd))
            {
                BeamCollectionList.Add(beamToAdd);
            }
        }

        private bool IsUnique(ECCABeam beamToAdd)
        {
            //result.Contains(eccamaterial.CategoryName)

            bool result = true;

            foreach(ECCABeam eccaBeam in BeamCollectionList)
            {
                if(eccaBeam.Id == beamToAdd.Id)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}