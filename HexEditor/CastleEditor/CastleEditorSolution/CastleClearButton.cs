using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;



partial class CastleViewerForm : Form
{

    Button baseClearButton;
    Button wallClearButton;
    Button rideClearButton;
    Button roleClearButton;
    Button highClearButton;

    void SetClearButton()
    {
        baseClearButton = new Button()
        {
            Text = "全消去",
            Location = new Point(iLeftStandingPos + 110, iTopStandingPos-25),
            AutoSize = true,
        };
        baseClearButton.Click += new EventHandler(baseClearButton_Click);
        this.Controls.Add(baseClearButton);

        wallClearButton = new Button()
        {
            Text = "全消去",
            Location = new Point(iLeftStandingPos + XBetween+ 110, iTopStandingPos - 25),
            AutoSize = true,
        };
        wallClearButton.Click += new EventHandler(wallClearButton_Click);
        this.Controls.Add(wallClearButton);

        rideClearButton = new Button()
        {
            Text = "全消去",
            Location = new Point(iLeftStandingPos + XBetween*2 + 110, iTopStandingPos - 25),
            AutoSize = true,
        };
        rideClearButton.Click += new EventHandler(rideClearButton_Click);
        this.Controls.Add(rideClearButton);

        roleClearButton = new Button()
        {
            Text = "全消去",
            Location = new Point(iLeftStandingPos + 110, iTopStandingPos+YBetween - 25),
            AutoSize = true,
        };
        roleClearButton.Click += new EventHandler(roleClearButton_Click);
        this.Controls.Add(roleClearButton);

        highClearButton = new Button()
        {
            Text = "全消去",
            Location = new Point(iLeftStandingPos + XBetween * 2 + 110, iTopStandingPos + YBetween - 25),
            AutoSize = true,
        };
        highClearButton.Click += new EventHandler(highClearButton_Click);
        this.Controls.Add(highClearButton);
    }

    void baseClearButton_Click(Object sender, EventArgs e)
    {
        // コンボボックスで選択した城の「チップリスト」を取得
        ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

        for (int i = 0; i < iColMax * iRowMax; i++)
        {
            if (i == 0)
            {
                RememberUndo(GetSelectedHexMapID(), i, (byte)csOneHexMapTipsList[i], true);
            }
            else
            {
                RememberUndo(GetSelectedHexMapID(), i, (byte)csOneHexMapTipsList[i], false);
            }
            // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
            csOneHexMapTipsList[i] = (Byte)0x15;
        }
        // より戻し。
        csAllHexMapArray[GetSelectedHexMapID()] = csOneHexMapTipsList;

        RePaintBaseTips();
        RepaintResultTips();
    }
    void wallClearButton_Click(Object sender, EventArgs e)
    {
        // コンボボックスで選択した城の「チップリスト」を取得
        ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

        for (int i = 0; i < iColMax * iRowMax; i++)
        {
            int iBinaryID = i + (iColMax * iRowMax) * 1;
            if (i == 0)
            {
                RememberUndo(GetSelectedHexMapID(), iBinaryID, (byte)csOneHexMapTipsList[iBinaryID], true);
            }
            else
            {
                RememberUndo(GetSelectedHexMapID(), iBinaryID, (byte)csOneHexMapTipsList[iBinaryID], false);
            }

            // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
            csOneHexMapTipsList[iBinaryID] = (Byte)0xFF;
        }
        // より戻し。
        csAllHexMapArray[GetSelectedHexMapID()] = csOneHexMapTipsList;

        RePaintWallTips();
        RepaintResultTips();
    }
    void rideClearButton_Click(Object sender, EventArgs e)
    {
        // コンボボックスで選択した城の「チップリスト」を取得
        ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

        for (int i = 0; i < iColMax * iRowMax; i++)
        {
            int iBinaryID = i + (iColMax * iRowMax) * 2;
            if (i == 0)
            {
                RememberUndo(GetSelectedHexMapID(), iBinaryID, (byte)csOneHexMapTipsList[iBinaryID], true);
            }
            else
            {
                RememberUndo(GetSelectedHexMapID(), iBinaryID, (byte)csOneHexMapTipsList[iBinaryID], false);
            }

            // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
            csOneHexMapTipsList[iBinaryID] = (Byte)0xFF;
        }
        // より戻し。
        csAllHexMapArray[GetSelectedHexMapID()] = csOneHexMapTipsList;

        RePaintRideTips();
        RepaintResultTips();
    }
    void roleClearButton_Click(Object sender, EventArgs e)
    {
        // コンボボックスで選択した城の「チップリスト」を取得
        ArrayList csOneHexMapRoleList = (ArrayList)csAllHexMapArray[GetSelectedRoleMapID()];

        for (int i = 0; i < iColMax * iRowMax; i++)
        {
            if (i == 0)
            {
                RememberUndo(GetSelectedRoleMapID(), i, (byte)csOneHexMapRoleList[i], true);
            }
            else
            {
                RememberUndo(GetSelectedRoleMapID(), i, (byte)csOneHexMapRoleList[i], false);
            }

            if (i % iColMax == 0 || i % iColMax == iColMax - 1)
            {

                // 左端と右端は「枠外」の値を設定
                csOneHexMapRoleList[i] = (Byte)0x00;
            }
            else
            {
                // それ以外は「城外平地」を設定
                csOneHexMapRoleList[i] = (Byte)0x12;
            }
        }
        // より戻し。
        csAllHexMapArray[GetSelectedRoleMapID()] = csOneHexMapRoleList;

        RePaintRoleTips();
        RepaintResultTips();

    }
    void highClearButton_Click(Object sender, EventArgs e)
    {
        // コンボボックスで選択した城の「チップリスト」を取得
        ArrayList csOneHexMapHighList = (ArrayList)csAllHexMapArray[GetSelectedHighMapID()]; // 214=城の数

        for (int i = 0; i < iColMax * iRowMax; i++)
        {
            if (i == 0)
            {
                RememberUndo(GetSelectedHighMapID(), i, (byte)csOneHexMapHighList[i], true);
            }
            else
            {
                RememberUndo(GetSelectedHighMapID(), i, (byte)csOneHexMapHighList[i], false);
            }
            // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
            csOneHexMapHighList[i] = (Byte)0x00;
        }
        // より戻し。
        csAllHexMapArray[GetSelectedHighMapID()] = csOneHexMapHighList;

        RePaintHighTips();
        RepaintResultTips();

    }
}
