using ECCalcAidData.CodeTables;
using ECCalcAidData.Collections;
using ECCalcAidData.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECCalcAidData
{
    [Serializable]
    public class ECCALibrary
    {
        public ECCAMaterialCollection MaterialCollection;
        public ECCASectionCollection SteelSectionCollection;
        public ECCACodeTables CodeTableCollection;
        //TEst for sync 

        public ECCALibrary()
        {
            this.MaterialCollection = new ECCAMaterialCollection();
            this.SteelSectionCollection = new ECCASectionCollection();
        }

        public bool WriteLibraryToFile(string path)
        {
            bool result = false;

            string _path = Assembly.GetExecutingAssembly().Location;
            string dbPth = Path.GetDirectoryName(_path) + "\\sys\\eccadata.bin";

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

        public TreeNode AsTreeView()
        {
            TreeNode result = new TreeNode("Library");

            //TreeNode treeNodeMaterials = new TreeNode("Materials");
            TreeNode treeNodeSections = new TreeNode("Sections");
            TreeNode treeNodeMaterials = new TreeNode("Materials");

            IList<string> SteelCategoryList = SteelSectionCollection.GetCategories();
            IList<string> MaterialCategoryList = MaterialCollection.GetCategories();

            if (MaterialCollection.MaterialList.Count > 0 && SteelSectionCollection.SectionList.Count > 0)
            {
                try
                {
                //Sections
                    foreach (string eccaCategory in SteelCategoryList)
                    {
                        TreeNode treeNodeCategory = new TreeNode(eccaCategory);

                        foreach (ECCASection eccaSection in SteelSectionCollection.SectionList)
                        {
                            if (eccaCategory == eccaSection.CategoryName)
                            {
                                TreeNode treeNodeName = new TreeNode(eccaSection.Designation);

                                treeNodeCategory.Nodes.Add(treeNodeName);
                            }

                        }
                        treeNodeSections.Nodes.Add(treeNodeCategory);
                    }

                    //Materialdd
                    
                    foreach (string eccaCategory in MaterialCategoryList)
                    {
                        TreeNode treeNodeCategory = new TreeNode(eccaCategory);

                        foreach (ECCAMaterial eccaMaterial in MaterialCollection.MaterialList)
                        {
                            if (eccaCategory == eccaMaterial.MaterialName)
                            {
                                TreeNode treeNodeName = new TreeNode(eccaMaterial.MaterialName);

                                treeNodeCategory.Nodes.Add(treeNodeName);
                            }

                        }
                        treeNodeMaterials.Nodes.Add(treeNodeCategory);
                    }
                    

                    //treeNodeSections.Nodes.Add(treeNodeSections);

                    //result.Nodes.Add(treeNodeSections);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            
            result.Nodes.Add(treeNodeSections);
            result.Nodes.Add(treeNodeMaterials);


            return result;
        }

        public object GetSection(string searchquery)
        {
            ECCASection result = null;
                result = SteelSectionCollection.Query(searchquery)[0];
            return result;
        }

        public ECCAMaterial GetMAterial(string searchquery)
        {
            ECCAMaterial result = null;
                result = MaterialCollection.Query(searchquery)[0];
            return result;
        }

        //

        public ECCALibrary OpenLibraryFromFile(string path)
        {
            ECCALibrary result = null;

            string _path = Assembly.GetExecutingAssembly().Location;
            string dbPth = Path.GetDirectoryName(_path) + "\\sys\\eccadata.bin";

            try
            {
                if (File.Exists(dbPth))
                {
                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(dbPth, FileMode.Open);
                    result = (ECCALibrary)formatter.Deserialize(stream);
                    stream.Close();
                }
                else
                {
                    MessageBox.Show("Database file does not exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
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
