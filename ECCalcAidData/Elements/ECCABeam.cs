using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECCalcAidData.Elements
{
    [Serializable]
    public class ECCABeam
    {
        public string Name;
        public double Length;
        public Int32 Id;
        public string RevitUId;
        public string Comment;
        public string Action;

        public ECCAMaterial Material;
        public ECCASection Section;
        public ECCALoads Loads;
        public ECCAResults Results;

        public ECCAPoint StartPoint;
        public ECCAPoint EndPoint;
        
        public ECCABeam()
        {
            this.Name = "";
            this.Length = 1;
            this.Material = new ECCAMaterial();
            this.Section = new ECCASection();
            this.Loads = new ECCALoads();
        }

        public ECCABeam(string BeamName, ECCAMaterial BeamMaterial, double BeamLength, string sectionName)
        {

        }
    }
}
