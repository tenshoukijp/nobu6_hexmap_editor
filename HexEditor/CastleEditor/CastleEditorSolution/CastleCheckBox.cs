using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;



partial class CastleViewerForm : Form
{

    CheckBox roleCheck;
    CheckBox highCheck;

    void SetCheckBox()
    {
        roleCheck = new CheckBox() {
            Text = "表示",
            Location = new Point(iLeftStandingPos + 248, iTopStandingPos+YBetween - 18),
            RightToLeft = RightToLeft.Yes,
            AutoSize = true,
        };
        roleCheck.CheckedChanged += new EventHandler(roleCheckBox_CheckedChange);

        highCheck = new CheckBox()
        {
            Text = "表示",
            Location = new Point(iLeftStandingPos + 248 + XBetween * 2, iTopStandingPos + YBetween - 18),
            RightToLeft = RightToLeft.Yes,
            AutoSize = true,
        };
        highCheck.CheckedChanged += new EventHandler(highCheckBox_CheckedChange);

        this.Controls.Add(roleCheck);
        this.Controls.Add(highCheck);
    }

    void roleCheckBox_CheckedChange(object sender, EventArgs e)
    {
        RepaintResultTips();
    }

    void highCheckBox_CheckedChange(object sender, EventArgs e)
    {
        RepaintResultTips();
    }

}
