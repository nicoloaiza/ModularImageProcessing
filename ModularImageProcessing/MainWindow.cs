using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModularImageProcessing.Contracts;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace ModularImageProcessing
{
    [Export]
    public partial class MainWindow : Form, IPartImportsSatisfiedNotification
    {
        [ImportMany("Filter", AllowRecomposition = true)]
        private IEnumerable<Lazy<IMenuPlugin, IMenuPluginMetaData>> filterPlugins;
        //[ImportMany("Color", AllowRecomposition = true)]
        //private IEnumerable<Lazy<IMenuPlugin, IMenuPluginMetaData>> colorPlugins;

        private Bitmap currentImage;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeMenus()
        {
            this.menuStrip1.Items.RemoveByKey("mnuFilter");
            if (filterPlugins.Count< Lazy < IMenuPlugin, IMenuPluginMetaData>>() > 0)
            {
                var filterMenu = new ToolStripMenuItem("Filtro");
                filterMenu.Name = "mnuFilter";
                foreach (var p in filterPlugins)
                {
                    filterMenu.DropDownItems.Add(BuildMenuItem(p));
                }
                this.menuStrip1.Items.Add(filterMenu);
            }
        }

        private ToolStripMenuItem BuildMenuItem(Lazy<IMenuPlugin, IMenuPluginMetaData> itemDefinition)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = itemDefinition.Metadata.MenuText;
            menuItem.Click += MenuItem_Click;
            menuItem.Tag = itemDefinition;
            return menuItem;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            Lazy<IMenuPlugin, IMenuPluginMetaData> plugin = ((ToolStripMenuItem)sender).Tag as Lazy<IMenuPlugin, IMenuPluginMetaData>;
            Image image = plugin.Value.ProcessImage(this.currentImage);
            if (image != null)
            {
                this.imgMain.Image = image;
            }
        }

        public void OnImportsSatisfied()
        {
            InitializeMenus();
        }

        private void abrirImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            openFileDialog1.Title = "Seleccione un archivo de imagen";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.imgMain.Image = new Bitmap(openFileDialog1.OpenFile());
                this.currentImage = new Bitmap(openFileDialog1.OpenFile());
            }
        }
    }
}
