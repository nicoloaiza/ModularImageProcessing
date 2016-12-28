using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace ModularImageProcessing.Contracts
{
    [MetadataAttribute]
    public class MenuPluginMetaDataAttribute : Attribute, IMenuPluginMetaData
    {
        public string MenuText
        {
            get;
            private set;
        }

        public MenuPluginMetaDataAttribute(string MenuText)
        {
            this.MenuText = MenuText;
        }
    }
}
