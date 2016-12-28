using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;

namespace ModularImageProcessing
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            DirectoryCatalog catalog = new DirectoryCatalog(@".\", "*.*");
            CompositionContainer container = new CompositionContainer(catalog);


            MainWindow form = container.GetExportedValue<MainWindow>();
            Task timer = Task.Factory.StartNew(new Action(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(5000);
                    catalog.Refresh();
                }
            }));

            form.ShowDialog();
        }
    }
}
