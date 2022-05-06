using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;


class FieldViewer
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new FieldViewerForm());
    }
}