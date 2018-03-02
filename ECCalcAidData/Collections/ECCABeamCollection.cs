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

        public void AddBeam(ECCABeam beamToAdd, out bool isUnique)
        {
            isUnique = IsUnique(beamToAdd);

            if (isUnique == true)
            {
                BeamCollectionList.Add(beamToAdd);
            }
            else
            {
                //updateOnlyBasics
            }
        }

        public bool IsUnique(ECCABeam beamToAdd)
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

        public ECCABeam GetBeam(ECCABeam requestedBeam)
        {
            ECCABeam result = null;

            foreach (ECCABeam eccaBeam in BeamCollectionList)
            {
                if (eccaBeam.Id == requestedBeam.Id)
                {
                    result = eccaBeam;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Get Beam from a Id
        /// </summary>
        /// <param name="beamId"></param>
        /// <returns></returns>
        public ECCABeam GetBeam(int beamId)
        {
            ECCABeam result = new ECCABeam();

            foreach (ECCABeam eccaBeam in BeamCollectionList)
            {
                if (eccaBeam.Id == beamId)
                {
                    result = eccaBeam;
                    break;
                }
            }

            return result;
        }
    }
}