using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;



partial class FieldViewerForm : Form
{

    CheckBox roleCheck;

    void SetCheckBox()
    {
        roleCheck = new CheckBox() {
            Text = "表示",
            Location = new Point(iLeftStandingPos + 218, iTopStandingPos+YBetween - 18),
            RightToLeft = RightToLeft.Yes,
            AutoSize = true,
        };
        roleCheck.CheckedChanged += new EventHandler(roleCheckBox_CheckedChange);

        this.Controls.Add(roleCheck);
   }

    void roleCheckBox_CheckedChange(object sender, EventArgs e)
    {
        RepaintResultTips();
    }
}
