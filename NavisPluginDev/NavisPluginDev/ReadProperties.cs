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

namespace NavisPluginDev
{
    [PluginAttribute("ReadingData", "DATools", ToolTip = "ReadData", DisplayName = "ReadData")]
    public class ReadProperties : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            Document navDoc = NW.Application.ActiveDocument;
            string values = string.Empty;

            foreach(ModelItem item in navDoc.CurrentSelection.SelectedItems) 
            {
                //Leyendo Informaci[on sobre elementos seleccionados en el docuemnto
                // values += Environment.NewLine + item.DisplayName

                
                foreach(PropertyCategory pc in item.PropertyCategories)
                {
                    //leyendo y desplegando propiedades y valores
                    // values += Environment.NewLine + pc.DisplayName;

                    foreach(DataProperty dp in pc.Properties)
                    {
                        
                        values += dp.DisplayName + Environment.NewLine +  dp.Value;
                    }

                }


            }

            MessageBox.Show(values);
            return 0;
        }
    }
}
