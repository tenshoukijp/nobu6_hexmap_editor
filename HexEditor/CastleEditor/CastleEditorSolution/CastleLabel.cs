using System;
using System.Drawing;
using System.Windows.Forms;

partial class CastleViewerForm : Form
{

    Label baseLabel;
    Label wallLabel;
    Label rideLabel;
    Label roleLabel;
    Label highLabel;

    private void SetLabel()
    {
        baseLabel = new Label() {
            Text = "第１層(下地)",
            Location = new Point(iLeftStandingPos, iTopStandingPos - 18),
            AutoSize = true,
        };
        wallLabel = new Label() {
            Text = "第２層(城壁･柵)",
            Location = new Point(iLeftStandingPos + XBetween, iTopStandingPos - 18),
            AutoSize = true,
        };
        rideLabel = new Label() {
            Text = "第３層(門･櫓･本丸)",
            Location = new Point(iLeftStandingPos + XBetween * 2, iTopStandingPos - 18),
            AutoSize = true,
        };
        roleLabel = new Label() {
            Text = "役割",
            Location = new Point(iLeftStandingPos, iTopStandingPos + YBetween - 18),
            AutoSize = true,
        };
        highLabel = new Label()
        {
            Text = "高さ",
            Location = new Point(iLeftStandingPos + XBetween * 2, iTopStandingPos + YBetween - 18),
            AutoSize = true,
        };

        // フォームにラベル追加
        this.Controls.Add(baseLabel);
        this.Controls.Add(wallLabel);
        this.Controls.Add(rideLabel);
        this.Controls.Add(roleLabel);
        this.Controls.Add(highLabel);

    }
}