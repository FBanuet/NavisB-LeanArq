using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NW = Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using System.Windows.Forms;
using Autodesk.Navisworks.Api;
using System.Security.Cryptography;
using Autodesk.Navisworks.Api.DocumentParts;
using Newtonsoft.Json;
using System.IO;


namespace NavisPluginDev
{
    [PluginAttribute("DataStructures", "DATools", ToolTip = "Data", DisplayName = "DataStruct")]
    public class DataStructures : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            NW.Document doc = NW.Application.ActiveDocument;

            #region reading JSON object
            var filepath = string.Empty;
            using(OpenFileDialog open = new OpenFileDialog())
            {
                open.InitialDirectory = "c:\\";
                open.Filter = "json files(*.json)|*.json|All files (*.*)|*.*";
                open.FilterIndex = 2;
                open.RestoreDirectory= true;

                if(open.ShowDialog() == DialogResult.OK) 
                {
                    filepath= open.FileName;
                }
            }
            #endregion

            ///sets jsets = new sets();
            ///
            sets jsets = JsonConvert.DeserializeObject<sets>(ReadJsonToObject(filepath));

            DocumentSelectionSets currentSets = doc.SelectionSets;
            foreach(searchSets s in jsets.searchSets)
            {
                foreach(searchSets sl in jsets.levelSets)
                {
                    NW.Search search = new NW.Search();
                    search.Selection.SelectAll();
                    search.SearchConditions.Add(NW.SearchCondition.HasPropertyByName(s.category, s.property).DisplayStringContains(s.value));
                    search.SearchConditions.Add(NW.SearchCondition.HasPropertyByName(sl.category, sl.property).DisplayStringContains(sl.value));
                    NW.ModelItemCollection items = search.FindAll(NW.Application.ActiveDocument, false);

                    if(items.Count > 0)
                    {

                        NW.SelectionSet sset = new NW.SelectionSet(search);
                        sset.DisplayName = sl.identifier + "|" + s.identifier;
                        currentSets.AddCopy(sset);

                    }
                }

            }


            

            return 0;
        }
        public string ReadJsonToObject(string text)
        {
            string selected;
            using (StreamReader str = new StreamReader(text))
            {
                selected = str.ReadToEnd();

            }
            return selected;
        }
    }


    public class searchSets
    {
        public string category { get; set; }
        public string property { get; set; }
        public string condition { get; set; }
        public string value { get; set; }

        public string name { get; set; }    

        public string identifier { get; set; }

    }

    public class sets
    {
        public List<searchSets> searchSets { get; set; }
        public List<searchSets> levelSets { get;set; }

    }

}
