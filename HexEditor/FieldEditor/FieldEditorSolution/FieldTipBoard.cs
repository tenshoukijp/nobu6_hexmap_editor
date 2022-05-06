using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

partial class FieldViewerForm : Form
{

    PictureBox[] basePBTipBoard;
    PictureBox[] edgePBTipBoard;
    PictureBox[] ridePBTipBoard;
    PictureBox[] rolePBTipBoard;

    const int XNumInBoard = 15;

    public void RegistTipBoard()
    {
        int ibaseValidCnt = 0;

        for (int i = 0; i < baseImages.Length; i++)
        {
            if (baseImages[i] != null)
            {
                ibaseValidCnt++;
            }
        }

        int iedgeValidCnt = 0;
        // 有効なチップ素材をカウントする。
        for (int i = 0; i < edgeImages.Length; i++)
        {
            if (edgeImages[i] != null)
            {
                iedgeValidCnt++;
            }
        }

        int irideValidCnt = 0;
        // 有効なチップ素材をカウントする。
        for (int i = 0; i < rideImages.Length; i++)
        {
            if (rideImages[i] != null)
            {
                irideValidCnt++;
            }
        }

        int iroleValidCnt = 0;
        // 有効なチップ素材をカウントする。
        for (int i = 0; i < roleImages.Length; i++)
        {
            if (roleImages[i] != null)
            {
                iroleValidCnt++;
            }
        }


        // 下地
        this.basePBTipBoard = new PictureBox[ibaseValidCnt];
        // 下地
        this.edgePBTipBoard = new PictureBox[iedgeValidCnt];
        // 下地
        this.ridePBTipBoard = new PictureBox[irideValidCnt];
        // 下地
        this.rolePBTipBoard = new PictureBox[iroleValidCnt];

        {
            int iUsedIx = 0;
            // 有効なチップ素材をカウントする。
            for (int i = 0; i < baseImages.Length; i++)
            {
                if (baseImages[i] != null)
                {
                    this.basePBTipBoard[iUsedIx] = new PictureBox();
                    this.basePBTipBoard[iUsedIx].Image = baseImages[i];
                    this.basePBTipBoard[iUsedIx].Size = new Size(iTipImageSize, iTipImageSize);
                    this.basePBTipBoard[iUsedIx].MouseDown += new MouseEventHandler(basePBTipBoard_MouseDown);
                    this.basePBTipBoard[iUsedIx].BorderStyle = BorderStyle.FixedSingle;

                    this.basePBTipBoard[iUsedIx].Location = new Point(
                    iLeftStandingPos + iUsedIx % XNumInBoard * (iTipImageSize + 4),
                    iTopStandingPos + iUsedIx / XNumInBoard * 20 + 145);

                    this.Controls.Add(basePBTipBoard[iUsedIx]);

                    iUsedIx++;
                }
            }
        }

        {
            int iUsedIx = 0;
            // 有効なチップ素材をカウントする。
            for (int i = 0; i < edgeImages.Length; i++)
            {
                if (edgeImages[i] != null)
                {
                    this.edgePBTipBoard[iUsedIx] = new PictureBox();
                    this.edgePBTipBoard[iUsedIx].Image = edgeImages[i];
                    this.edgePBTipBoard[iUsedIx].Size = new Size(iTipImageSize, iTipImageSize);
                    this.edgePBTipBoard[iUsedIx].MouseDown += new MouseEventHandler(edgePBTipBoard_MouseDown);
                    this.edgePBTipBoard[iUsedIx].BorderStyle = BorderStyle.FixedSingle;

                    this.edgePBTipBoard[iUsedIx].Location = new Point(
                    iLeftStandingPos + XBetween + iUsedIx % XNumInBoard * (iTipImageSize + 4),
                    iTopStandingPos + iUsedIx / XNumInBoard * 20 + 145);

                    this.Controls.Add(edgePBTipBoard[iUsedIx]);

                    iUsedIx++;
                }
            }
        }


        {
            int iUsedIx = 0;
            // 有効なチップ素材をカウントする。
            for (int i = 0; i < rideImages.Length; i++)
            {
                if (rideImages[i] != null)
                {
                    this.ridePBTipBoard[iUsedIx] = new PictureBox();
                    this.ridePBTipBoard[iUsedIx].Image = rideImages[i];
                    this.ridePBTipBoard[iUsedIx].Size = new Size(iTipImageSize, iTipImageSize);
                    this.ridePBTipBoard[iUsedIx].MouseDown += new MouseEventHandler(ridePBTipBoard_MouseDown);
                    this.ridePBTipBoard[iUsedIx].BorderStyle = BorderStyle.FixedSingle;

                    this.ridePBTipBoard[iUsedIx].Location = new Point(
                    iLeftStandingPos + XBetween * 2 + iUsedIx % XNumInBoard * (iTipImageSize + 4),
                    iTopStandingPos + iUsedIx / XNumInBoard * 20 + 145);

                    this.Controls.Add(ridePBTipBoard[iUsedIx]);

                    iUsedIx++;
                }
            }
        }

        {
            int iUsedIx = 0;
            // 有効なチップ素材をカウントする。
            for (int i = 0; i < roleImages.Length; i++)
            {
                if (roleImages[i] != null)
                {
                    this.rolePBTipBoard[iUsedIx] = new PictureBox();
                    this.rolePBTipBoard[iUsedIx].Image = roleImages[i];
                    this.rolePBTipBoard[iUsedIx].Size = new Size(iTipImageSize, iTipImageSize);
                    this.rolePBTipBoard[iUsedIx].MouseDown += new MouseEventHandler(rolePBTipBoard_MouseDown);
                    this.rolePBTipBoard[iUsedIx].BorderStyle = BorderStyle.FixedSingle;

                    this.rolePBTipBoard[iUsedIx].Location = new Point(
                    iLeftStandingPos + iUsedIx % XNumInBoard * (iTipImageSize + 4),
                    iTopStandingPos + YBetween + iUsedIx / XNumInBoard * 20 + 145);

                    this.Controls.Add(rolePBTipBoard[iUsedIx]);

                    iUsedIx++;
                }
            }
        }

    }

    // 選択項目が変更されたときのイベントハンドラ
    void basePBTipBoard_MouseDown(object sender, MouseEventArgs e)
    {
        if (baseSelectedPictureBox != -1)
        { // ベース系ヘックスをどれか選択してる。

            PictureBox pb = sender as PictureBox;
            int iNewTipID = (int)pb.Image.Tag;

            // コンボボックスで選択した城の「チップリスト」を取得
            ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

            int iBinaryIndex = baseSelectedPictureBox;

            RememberUndo(GetSelectedHexMapID(), iBinaryIndex, (Byte)csOneHexMapTipsList[iBinaryIndex], true);

            // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
            csOneHexMapTipsList[iBinaryIndex] = (Byte)iNewTipID;

            // より戻し。
            csAllHexMapArray[GetSelectedHexMapID()] = csOneHexMapTipsList;

            RePaintBaseTips();
            RepaintResultTips();

        }
    }
    // 選択項目が変更されたときのイベントハンドラ
    void edgePBTipBoard_MouseDown(object sender, MouseEventArgs e)
    {
        if (edgeSelectedPictureBox != -1)
        { // ベース系ヘックスをどれか選択してる。

            PictureBox pb = sender as PictureBox;
            int iNewTipID = (int)pb.Image.Tag;


            // コンボボックスで選択した城の「チップリスト」を取得
            ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

            int iBinaryIndex = edgeSelectedPictureBox + (iColMax * iRowMax) * 1;

            RememberUndo(GetSelectedHexMapID(), iBinaryIndex, (Byte)csOneHexMapTipsList[iBinaryIndex], true);

            // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
            csOneHexMapTipsList[iBinaryIndex] = (Byte)iNewTipID;

            // より戻し。
            csAllHexMapArray[GetSelectedHexMapID()] = csOneHexMapTipsList;

            RePaintEdgeTips();
            RepaintResultTips();

        }
    }

    // 選択項目が変更されたときのイベントハンドラ
    void ridePBTipBoard_MouseDown(object sender, MouseEventArgs e)
    {
        if (rideSelectedPictureBox != -1)
        { // ベース系ヘックスをどれか選択してる。

            PictureBox pb = sender as PictureBox;
            int iNewTipID = (int)pb.Image.Tag;

            // コンボボックスで選択した城の「チップリスト」を取得
            ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

            int iBinaryIndex = rideSelectedPictureBox + (iColMax * iRowMax) * 2;

            RememberUndo(GetSelectedHexMapID(), iBinaryIndex, (Byte)csOneHexMapTipsList[iBinaryIndex], true);

            // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
            csOneHexMapTipsList[iBinaryIndex] = (Byte)iNewTipID;

            // より戻し。
            csAllHexMapArray[GetSelectedHexMapID()] = csOneHexMapTipsList;

            RePaintRideTips();
            RepaintResultTips();

        }
    }

    // 選択項目が変更されたときのイベントハンドラ
    void rolePBTipBoard_MouseDown(object sender, MouseEventArgs e)
    {
        if (roleSelectedPictureBox != -1)
        { // ベース系ヘックスをどれか選択してる。

            PictureBox pb = sender as PictureBox;
            int iNewTipID = (int)pb.Image.Tag;


            // コンボボックスで選択した城の「チップリスト」を取得
            ArrayList csOneHexMapRoleList = (ArrayList)csAllHexMapArray[GetSelectedRoleMapID()];

            int iBinaryIndex = roleSelectedPictureBox;

            RememberUndo(GetSelectedRoleMapID(), iBinaryIndex, (Byte)csOneHexMapRoleList[iBinaryIndex], true);

            // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
            csOneHexMapRoleList[iBinaryIndex] = (Byte)iNewTipID;

            // より戻し。
            csAllHexMapArray[GetSelectedRoleMapID()] = csOneHexMapRoleList;

            RePaintRoleTips();
            RepaintResultTips();

        }
    }
}