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
    [PluginAttribute("SelectionSets", "DATools", ToolTip = "SelSet", DisplayName = "ReadData")]
    public class SelectionSet : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            NW.Document doc = NW.Application.ActiveDocument;
            DocumentSelectionSets dss = doc.SelectionSets;

            NW.SelectionSet set = new NW.SelectionSet(doc.CurrentSelection.SelectedItems);
            set.DisplayName = "API SELSET";
            
            dss.AddCopy(set);

            return 0;
        }
    }
}
