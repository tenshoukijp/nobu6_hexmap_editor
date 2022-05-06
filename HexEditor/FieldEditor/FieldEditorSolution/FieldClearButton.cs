using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;



partial class FieldViewerForm : Form
{

    Button baseClearButton;
    Button edgeClearButton;
    Button rideClearButton;
    Button roleClearButton;

    void SetClearButton()
    {
        baseClearButton = new Button()
        {
            Text = "全消去",
            Location = new Point(iLeftStandingPos + 110, iTopStandingPos-25),
            TabStop = false,
            AutoSize = true,
        };
        baseClearButton.Click += new EventHandler(baseClearButton_Click);
        this.Controls.Add(baseClearButton);

        edgeClearButton = new Button()
        {
            Text = "全消去",
            Location = new Point(iLeftStandingPos + XBetween+ 110, iTopStandingPos - 25),
            TabStop = false,
            AutoSize = true,
        };
        edgeClearButton.Click += new EventHandler(edgeClearButton_Click);
        this.Controls.Add(edgeClearButton);

        rideClearButton = new Button()
        {
            Text = "全消去",
            Location = new Point(iLeftStandingPos + XBetween*2 + 110, iTopStandingPos - 25),
            TabStop = false,
            AutoSize = true,
        };
        rideClearButton.Click += new EventHandler(rideClearButton_Click);
        this.Controls.Add(rideClearButton);

        roleClearButton = new Button()
        {
            Text = "全消去",
            Location = new Point(iLeftStandingPos + 110, iTopStandingPos+YBetween - 25),
            TabStop = false,
            AutoSize = true,
        };
        roleClearButton.Click += new EventHandler(roleClearButton_Click);
        this.Controls.Add(roleClearButton);

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
            csOneHexMapTipsList[i] = (Byte)0x00;
        }
        // より戻し。
        csAllHexMapArray[GetSelectedHexMapID()] = csOneHexMapTipsList;

        RePaintBaseTips();
        RepaintResultTips();
    }
    void edgeClearButton_Click(Object sender, EventArgs e)
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

        RePaintEdgeTips();
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

            // それ以外は「城外平地」を設定
            csOneHexMapRoleList[i] = (Byte)0x1;
        }
        // より戻し。
        csAllHexMapArray[GetSelectedRoleMapID()] = csOneHexMapRoleList;

        RePaintRoleTips();
        RepaintResultTips();

    }

}
