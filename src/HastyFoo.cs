using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stLib_CS {
    namespace HastyFoo {
        static public class Dialog {
            public static string GetFileName() {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = "c:\\";
                if( dlg.ShowDialog() == DialogResult.OK ) {
                    return dlg.FileName;
                }
                return null;
            }

            public static string GetFolderPath() {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if( dlg.ShowDialog() == DialogResult.OK ) {
                    return dlg.SelectedPath;
                }
                return null;
            }
        }
    }
}
