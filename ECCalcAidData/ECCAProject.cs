using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECCalcAidData.Collections;
using ECCalcAidData.Elements;


namespace ECCalcAidData
{
    [Serializable]
    public class ECCAProject
    {
        public ECCAPojectInfo ProjectInfo;
        public ECCAElementCollection ElementCollection;
        public ECCABeamCollection BeamCollection;

        public string FileLocation;

        public ECCAProject()
        {
            ProjectInfo = new ECCAPojectInfo();
            ElementCollection = new ECCAElementCollection();
            BeamCollection = new ECCABeamCollection();
            FileLocation = "";
        }

        public ECCAProject(string pathName)
        {
            if (File.Exists(pathName))
            {
                ECCAProject bufferProject = DeSerializeProject(pathName);

                ProjectInfo = bufferProject.ProjectInfo;
                ElementCollection = bufferProject.ElementCollection;
                BeamCollection = bufferProject.BeamCollection;
                FileLocation = bufferProject.FileLocation;
            }
            else
            {
                                
                ProjectInfo = new ECCAPojectInfo();
                ElementCollection = new ECCAElementCollection();
                BeamCollection = new ECCABeamCollection();
                FileLocation = "";
                //A new File will be saved to requested path:      

                DialogResult var = MessageBox.Show("A projectfile could not be found, Shall I attempt to create one ?", "Warning", MessageBoxButtons.YesNo);
                if (var == DialogResult.Yes)
                {
                    bool saveallok = SerializeProject(pathName);
                    if (saveallok == true)
                        MessageBox.Show("A new project file was created");
                }                
            }
        }

        public bool SerializeProject(string pathName = "")
        {
            bool result = false;
            string savePath = "";

            try
            {
                //Clear the file location
                //IF it was deserialised FileLocations != "";
                // if it is a new project all are ""
                // iF it is a save-as only pathName = ""
                //The file should always be serialised with FileLocation =""

                if (FileLocation != "" || pathName != "")
                {
                    //This is a save
                    if(FileLocation != "" && pathName == "")
                    {
                        savePath = FileLocation;
                    }
                    //This is a save ass
                    else if(FileLocation == "" && pathName != "")
                    {
                        savePath = pathName;
                    }
                    else
                    {
                        savePath = pathName;
                    }

                    FileLocation = "";
                    ///emm
                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None);
                    formatter.Serialize(stream, this);
                    stream.Close();
                    result = true;

                    //Rset FilePath
                    FileLocation = savePath;

                }
                else
                {
                    result = false;
                    MessageBox.Show("A filePath could not be found, save project file first");
                    FileLocation = savePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }

            return result;
        }

        public ECCAProject DeSerializeProject(string projectPath)
        {
            ECCAProject newProject = new ECCAProject();
            //Reatemp
            try
            {
                if (File.Exists(projectPath))
                {
                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);


                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(projectPath, FileMode.Open);
                    newProject = (ECCAProject)formatter.Deserialize(stream);
                    stream.Close();
                }
                else
                {
                    MessageBox.Show("Project path cannot be found, a new file can be created");
                }

                //This is the only location a project path can be defined
                newProject.FileLocation = projectPath;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return newProject;

        }

        //When assembly cant be find bind to current
        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            System.Reflection.Assembly ayResult = null;
            string sShortAssemblyName = args.Name.Split(',')[0];
            System.Reflection.Assembly[] ayAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (System.Reflection.Assembly ayAssembly in ayAssemblies)
            {
                if (sShortAssemblyName == ayAssembly.FullName.Split(',')[0])
                {
                    ayResult = ayAssembly;
                    break;
                }
            }
            return ayResult;
        }
    }
}
