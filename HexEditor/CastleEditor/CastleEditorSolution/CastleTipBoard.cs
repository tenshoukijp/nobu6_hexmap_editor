using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

partial class CastleViewerForm : Form
{

    PictureBox[] basePBTipBoard;
    PictureBox[] wallPBTipBoard;
    PictureBox[] ridePBTipBoard;
    PictureBox[] rolePBTipBoard;
    PictureBox[] highPBTipBoard;

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

        int iwallValidCnt = 0;
        // 有効なチップ素材をカウントする。
        for (int i = 0; i < wallImages.Length; i++)
        {
            if (wallImages[i] != null)
            {
                iwallValidCnt++;
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

        int ihighValidCnt = 0;
        // 有効なチップ素材をカウントする。
        for (int i = 0; i < highImages.Length; i++)
        {
            if (highImages[i] != null)
            {
                ihighValidCnt++;
            }
        }

        // 下地
        this.basePBTipBoard = new PictureBox[ibaseValidCnt];
        // 下地
        this.wallPBTipBoard = new PictureBox[iwallValidCnt];
        // 下地
        this.ridePBTipBoard = new PictureBox[irideValidCnt];
        // 下地
        this.rolePBTipBoard = new PictureBox[iroleValidCnt];
        // 下地
        this.highPBTipBoard = new PictureBox[ihighValidCnt];

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
                    iTopStandingPos + iUsedIx / XNumInBoard * 20 + 265);

                    this.Controls.Add(basePBTipBoard[iUsedIx]);

                    iUsedIx++;
                }
            }
        }

        {
            int iUsedIx = 0;
            // 有効なチップ素材をカウントする。
            for (int i = 0; i < wallImages.Length; i++)
            {
                if (wallImages[i] != null)
                {
                    this.wallPBTipBoard[iUsedIx] = new PictureBox();
                    this.wallPBTipBoard[iUsedIx].Image = wallImages[i];
                    this.wallPBTipBoard[iUsedIx].Size = new Size(iTipImageSize, iTipImageSize);
                    this.wallPBTipBoard[iUsedIx].MouseDown += new MouseEventHandler(wallPBTipBoard_MouseDown);
                    this.wallPBTipBoard[iUsedIx].BorderStyle = BorderStyle.FixedSingle;

                    this.wallPBTipBoard[iUsedIx].Location = new Point(
                    iLeftStandingPos + XBetween+ iUsedIx % XNumInBoard * (iTipImageSize + 4),
                    iTopStandingPos + iUsedIx / XNumInBoard * 20 + 265);

                    this.Controls.Add(wallPBTipBoard[iUsedIx]);

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
                    iLeftStandingPos + XBetween*2 + iUsedIx % XNumInBoard * (iTipImageSize + 4),
                    iTopStandingPos + iUsedIx / XNumInBoard * 20 + 265);

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
                    iTopStandingPos + YBetween + iUsedIx / XNumInBoard * 20 + 265);

                    this.Controls.Add(rolePBTipBoard[iUsedIx]);

                    iUsedIx++;
                }
            }
        }

        {
            int iUsedIx = 0;
            // 有効なチップ素材をカウントする。
            for (int i = 0; i < highImages.Length; i++)
            {
                if (highImages[i] != null)
                {
                    this.highPBTipBoard[iUsedIx] = new PictureBox();
                    this.highPBTipBoard[iUsedIx].Image = highImages[i];
                    this.highPBTipBoard[iUsedIx].Size = new Size(iTipImageSize, iTipImageSize);
                    this.highPBTipBoard[iUsedIx].MouseDown += new MouseEventHandler(highPBTipBoard_MouseDown);
                    this.highPBTipBoard[iUsedIx].BorderStyle = BorderStyle.FixedSingle;

                    this.highPBTipBoard[iUsedIx].Location = new Point(
                    iLeftStandingPos + XBetween*2+ iUsedIx % XNumInBoard * (iTipImageSize + 4),
                    iTopStandingPos + YBetween + iUsedIx / XNumInBoard * 20 + 265);

                    this.Controls.Add(highPBTipBoard[iUsedIx]);

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
                csOneHexMapTipsList[baseSelectedPictureBox] = (Byte)iNewTipID;

                // より戻し。
                csAllHexMapArray[GetSelectedHexMapID()] = csOneHexMapTipsList;

                RePaintBaseTips();
                RepaintResultTips();

            }
        }
        // 選択項目が変更されたときのイベントハンドラ
        void wallPBTipBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (wallSelectedPictureBox != -1)
            { // ベース系ヘックスをどれか選択してる。

                PictureBox pb = sender as PictureBox;
                int iNewTipID = (int)pb.Image.Tag;


                // コンボボックスで選択した城の「チップリスト」を取得
                ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

                int iBinaryIndex = wallSelectedPictureBox + (iColMax * iRowMax) * 1;

                RememberUndo(GetSelectedHexMapID(), iBinaryIndex, (Byte)csOneHexMapTipsList[iBinaryIndex], true);

                // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
                csOneHexMapTipsList[iBinaryIndex] = (Byte)iNewTipID;

                // より戻し。
                csAllHexMapArray[GetSelectedHexMapID()] = csOneHexMapTipsList;

                RePaintWallTips();
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
                csOneHexMapTipsList[rideSelectedPictureBox + (iColMax * iRowMax) * 2] = (Byte)iNewTipID;

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

                RememberUndo(GetSelectedRoleMapID(), roleSelectedPictureBox, (Byte)csOneHexMapRoleList[iBinaryIndex], true);

                // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
                csOneHexMapRoleList[roleSelectedPictureBox] = (Byte)iNewTipID;

                // より戻し。
                csAllHexMapArray[GetSelectedRoleMapID()] = csOneHexMapRoleList;

                RePaintRoleTips();
                RepaintResultTips();

            }
        }

        // 選択項目が変更されたときのイベントハンドラ
        void highPBTipBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (highSelectedPictureBox != -1)
            { // ベース系ヘックスをどれか選択してる。

                PictureBox pb = sender as PictureBox;
                int iNewTipID = (int)pb.Image.Tag;


                // コンボボックスで選択した城の「チップリスト」を取得
                ArrayList csOneHexMapHighList = (ArrayList)csAllHexMapArray[GetSelectedHighMapID()]; // 214=城の数

                int iBinaryIndex = highSelectedPictureBox;

                RememberUndo(GetSelectedHighMapID(), highSelectedPictureBox, (Byte)csOneHexMapHighList[iBinaryIndex], true);

                // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
                csOneHexMapHighList[highSelectedPictureBox] = (Byte)iNewTipID;

                // より戻し。
                csAllHexMapArray[GetSelectedHighMapID()] = csOneHexMapHighList;

                RePaintHighTips();
                RepaintResultTips();

            }
        }

}