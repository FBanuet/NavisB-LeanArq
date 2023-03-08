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
using Autodesk.Navisworks.Api.Timeliner;

namespace NavisPluginDev
{
    [PluginAttribute("Timeliner", "DATools", ToolTip = "CreateTasks", DisplayName = "Create Tasks")]
    public class TimeLiner : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            NW.Document DOC = NW.Application.ActiveDocument;
            DocumentTimeliner docTLiner = DOC.GetTimeliner();

            NW.SavedItemCollection selectionSets = DOC.SelectionSets.RootItem.Children;
            int index = 1;

            foreach(NW.SavedItem sitem in selectionSets)
            {
                TimelinerTask task = new TimelinerTask()
                {
                    DisplayName = sitem.DisplayName,
                    PlannedStartDate = new DateTime(2023,03,03).AddDays(index),
                    PlannedEndDate= new DateTime(2024,25,08).AddDays(index+1),
                    SimulationTaskTypeName = "Construct"
                };
                NW.SelectionSet selSet = sitem as NW.SelectionSet;
                NW.SelectionSource selSource = DOC.SelectionSets.CreateSelectionSource(selSet);
                NW.SelectionSourceCollection selSourceCollec = new NW.SelectionSourceCollection() { selSource};
                task.Selection.CopyFrom(selSourceCollec);
                docTLiner.TaskAddCopy(task);
                index++;
            }


            return 0;



        }
    }
}
