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


namespace NavisPluginDev
{
    [PluginAttribute("SearchSets", "DATools", ToolTip = "SearchSet", DisplayName = "SearchData")]
    public class SearchSets : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            NW.Document doc = NW.Application.ActiveDocument;
            DocumentSelectionSets currentSets = doc.SelectionSets;

            NW.Search search = new NW.Search();
            search.Selection.SelectAll();

            search.SearchConditions.Add(SearchCondition.HasPropertyByDisplayName("Element","Type")
                .DisplayStringContains("Floor-Generic 300mm"));

            ModelItemCollection items = search.FindAll(NW.Application.ActiveDocument, false);
            NW.Application.ActiveDocument.CurrentSelection.CopyFrom(items);
            NW.SelectionSet set = new NW.SelectionSet(search);
            set.DisplayName = "LOSAS";

            currentSets.AddCopy(set);

            return 0;
        }
    }
}
