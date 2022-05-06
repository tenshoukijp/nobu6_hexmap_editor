using System;
using System.Drawing;
using System.Windows.Forms;

partial class FieldViewerForm : Form
{

    Label baseLabel;
    Label edgeLabel;
    Label rideLabel;
    Label roleLabel;

    private void SetLabel()
    {
        baseLabel = new Label() {
            Text = "第１層(下地)",
            Location = new Point(iLeftStandingPos, iTopStandingPos - 18),
            AutoSize = true,
        };
        edgeLabel = new Label() {
            Text = "第２層(道･川･海岸)",
            Location = new Point(iLeftStandingPos + XBetween, iTopStandingPos - 18),
            AutoSize = true,
        };
        rideLabel = new Label() {
            Text = "第３層(城･橋)",
            Location = new Point(iLeftStandingPos + XBetween * 2, iTopStandingPos - 18),
            AutoSize = true,
        };
        roleLabel = new Label() {
            Text = "役割",
            Location = new Point(iLeftStandingPos, iTopStandingPos + YBetween - 18),
            AutoSize = true,
        };

        // フォームにラベル追加
        this.Controls.Add(baseLabel);
        this.Controls.Add(edgeLabel);
        this.Controls.Add(rideLabel);
        this.Controls.Add(roleLabel);
    }
}