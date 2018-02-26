using ECCalcAidData.Elements;
using System;
using System.Collections.Generic;

namespace ECCalcAidData.Collections
{
    [Serializable]
    public class ECCAElementCollection
    {
        public IList<ECCAElement> ElementCollectionList;

        public ECCAElementCollection()
        {
            this.ElementCollectionList = new List<ECCAElement>();
            for (int i = 0; i<10; i++)
            {
                ElementCollectionList.Add(new ECCAElement());
            }
        }
    }
}