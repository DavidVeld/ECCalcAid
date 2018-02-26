using System;

namespace ECCalcAidData.Collections
{
    [Serializable]
    public class ECCAPojectInfo
    {
        //List of constant properties for project

        public string ProjectName;
        public string ProjectNr;
        public string Description;

        public string Client;
        public string ClientAddress;
        public string ProjectAddress;

        //Building Properties
        public string Building;
        public string Engineer;
        public string Checked;

        /// <summary>
        /// Design ive
        /// </summary>
        public string DesignLive;
        /// <summary>
        /// Definition of consequences classes
        /// </summary>
        public string DefinitionOfConsequences;
        public string ReliabilityIndex;

        public ECCAPojectInfo()
        {
            this.ProjectName = "New Project";
            this.ProjectNr = "00000";
            this.Engineer = "John Smith Jr";
        }
}
}