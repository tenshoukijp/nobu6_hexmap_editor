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

    int baseSelectedPictureBox = -1;
    int wallSelectedPictureBox = -1;
    int rideSelectedPictureBox = -1;
    int roleSelectedPictureBox = -1;
    int highSelectedPictureBox = -1;

    PictureBox[] basePictureBox;
    PictureBox[] wallPictureBox;
    PictureBox[] ridePictureBox;
    PictureBox[] rolePictureBox;
    PictureBox[] highPictureBox;

    PictureBox[] resultPictureBoxStack;

    const int iColMax = 18;
    const int iRowMax = 16;

    const int iTipImageSize = 16;

    const int iLeftStandingPos = 170;
    const int iTopStandingPos = 30;

    const int iCastleNum = 214;

    const int iHexmapCastleStartID = 952;

    const int XBetween = 340; // 横間隔
    const int YBetween = 415; // 縦間隔

    // ピクチャーボックスの配置。
    // 下地・城壁・さらに上に乗るものの３層
    private void RegistPictureBoxes()
    {
        // 下地
        this.basePictureBox = new PictureBox[iColMax * iRowMax];
        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.basePictureBox[i] = new PictureBox();
                this.basePictureBox[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.basePictureBox[i].MouseDown += new MouseEventHandler(basePictureBox_MouseDown);

                if (row % 2 == 0)
                {
                    this.basePictureBox[i].Location = new Point(
                        iLeftStandingPos + col * iTipImageSize,
                        iTopStandingPos + row * iTipImageSize);
                }
                else
                {
                    this.basePictureBox[i].Location = new Point(
                        iLeftStandingPos + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + row * iTipImageSize);
                }
                this.Controls.Add(basePictureBox[i]);
            }
        }

        // 城壁
        this.wallPictureBox = new System.Windows.Forms.PictureBox[iColMax * iRowMax];
        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.wallPictureBox[i] = new PictureBox();
                this.wallPictureBox[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.wallPictureBox[i].MouseDown += new MouseEventHandler(wallPictureBox_MouseDown);

                if (row % 2 == 0)
                {
                    this.wallPictureBox[i].Location = new Point(
                        iLeftStandingPos + XBetween + col * iTipImageSize,
                        iTopStandingPos + row * iTipImageSize);
                }
                else
                {
                    this.wallPictureBox[i].Location = new Point(
                        iLeftStandingPos + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + row * iTipImageSize);
                }
                this.Controls.Add(wallPictureBox[i]);
            }
        }

        // 本丸・曲輪・茂み 等
        this.ridePictureBox = new PictureBox[iColMax * iRowMax];
        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.ridePictureBox[i] = new PictureBox();
                this.ridePictureBox[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.ridePictureBox[i].MouseDown += new MouseEventHandler(ridePictureBox_MouseDown);

                if (row % 2 == 0)
                {
                    this.ridePictureBox[i].Location = new Point(
                        iLeftStandingPos + XBetween*2 + col * iTipImageSize,
                        iTopStandingPos + row * iTipImageSize);
                }
                else
                {
                    this.ridePictureBox[i].Location = new Point(
                        iLeftStandingPos + XBetween*2 + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + row * iTipImageSize);
                }
                this.Controls.Add(ridePictureBox[i]);
            }
        }

        // 役割
        this.rolePictureBox = new PictureBox[iColMax * iRowMax];
        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.rolePictureBox[i] = new PictureBox();
                this.rolePictureBox[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.rolePictureBox[i].MouseDown += new MouseEventHandler(rolePictureBox_MouseDown);

                if (row % 2 == 0)
                {
                    this.rolePictureBox[i].Location = new Point(
                        iLeftStandingPos + col * iTipImageSize,
                        iTopStandingPos + YBetween+ row * iTipImageSize);
                }
                else
                {
                    this.rolePictureBox[i].Location = new Point(
                        iLeftStandingPos + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween+ row * iTipImageSize);
                }
                this.Controls.Add(rolePictureBox[i]);
            }
        }

        // 高さ
        this.highPictureBox = new PictureBox[iColMax * iRowMax];
        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.highPictureBox[i] = new PictureBox();
                this.highPictureBox[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.highPictureBox[i].MouseDown += new MouseEventHandler(highPictureBox_MouseDown);

                if (row % 2 == 0)
                {
                    this.highPictureBox[i].Location = new Point(
                        iLeftStandingPos + XBetween*2 + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize);
                }
                else
                {
                    this.highPictureBox[i].Location = new Point(
                        iLeftStandingPos + XBetween*2 + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize);
                }
                this.Controls.Add(highPictureBox[i]);
            }
        }


        this.resultPictureBoxStack = new PictureBox[iColMax * iRowMax];

        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.resultPictureBoxStack[i] = new PictureBox();
                this.resultPictureBoxStack[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.resultPictureBoxStack[i].AllowDrop = true;

                if (row % 2 == 0)
                {
                    this.resultPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + XBetween + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize);
                }
                else
                {
                    this.resultPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize);
                }
                this.Controls.Add(resultPictureBoxStack[i]);
            }
        }

    }


    void RePaintBaseTips()
    {

        // コンボボックスで選択した城の「チップリスト」を取得
        ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

        for (int i = 0; i < iRowMax * iColMax; i++)
        {
            // ベース
            Byte tip = (Byte)csOneHexMapTipsList[i];
            Image baseImage = baseImages[tip];
            this.basePictureBox[i].Image = baseImage;
        }

    }

 
    void RePaintWallTips()
    {

        // コンボボックスで選択した城の「チップリスト」を取得
        ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

        for (int i = 0; i < iRowMax * iColMax; i++)
        {

            // ウォール
            Byte tip = (Byte)csOneHexMapTipsList[i + (iColMax * iRowMax) * 1];
            Image wallImage = wallImages[tip];
            this.wallPictureBox[i].Image = wallImage;
        }
    }

    void RePaintRideTips()
    {

        // コンボボックスで選択した城の「チップリスト」を取得
        ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

        for (int i = 0; i < iRowMax * iColMax; i++)
        {

            // ライド
            Byte tip = (Byte)csOneHexMapTipsList[i + (iColMax * iRowMax) * 2];
            Image rideImage = rideImages[tip];
            this.ridePictureBox[i].Image = rideImage;
        }
    }


    void RePaintRoleTips()
    {
        // コンボボックスで選択した城の「チップリスト」を取得
        ArrayList csOneHexMapRoleList = (ArrayList)csAllHexMapArray[GetSelectedRoleMapID()];

        for (int i = 0; i < iRowMax * iColMax; i++)
        {
            // ロール
            Byte tip = (Byte)csOneHexMapRoleList[i];
            Image roleImage = roleImages[tip];
            this.rolePictureBox[i].Image = roleImage;
        }
    }


    void RePaintHighTips()
    {
        ArrayList csOneHexMapHighList = (ArrayList)csAllHexMapArray[GetSelectedHighMapID()];

        for (int i = 0; i < iRowMax * iColMax; i++)
        {
            // 高さ
            Byte tip = (Byte)csOneHexMapHighList[i];
            Image highImage = highImages[tip];
            this.highPictureBox[i].Image = highImage;
        }
    }

    void RepaintResultTips()
    {
        for (int i = 0; i < iRowMax * iColMax; i++)
        {

            //------------- 重ね表示
            // ベースとなるイメージ
            Bitmap ff = new Bitmap(GetType(), "CastleEditor.images.wall.FF.png");
            Bitmap result = new Bitmap(ff);
            Graphics g = Graphics.FromImage(result);
            // 土地重ね
            g.DrawImage(this.basePictureBox[i].Image, 0, 0, iTipImageSize, iTipImageSize);

            if ((int)this.wallPictureBox[i].Image.Tag != 0xFF)
            {// 透明ではない
                // 壁重ね
                g.DrawImage(this.wallPictureBox[i].Image, 0, 0, iTipImageSize, iTipImageSize);
            }
            if ((int)this.ridePictureBox[i].Image.Tag != 0xFF)
            { // 透明ではない
                // 他重ね
                g.DrawImage(this.ridePictureBox[i].Image, 0, 0, iTipImageSize, iTipImageSize);
            }
            // ロールがチェックされていれば
            if (roleCheck.Checked)
            {
                // 他重ね
                g.DrawImage(this.rolePictureBox[i].Image, 0, 0, iTipImageSize, iTipImageSize);
            }
            // ロールがチェックされていれば
            if (highCheck.Checked)
            {
                // 他重ね
                g.DrawImage(this.highPictureBox[i].Image, 0, 0, iTipImageSize, iTipImageSize);
            }
            // 重ねたので破棄
            g.Dispose();
            // もういららない
            ff.Dispose();
            this.resultPictureBoxStack[i].Image = result;
        }

    }


    void RePaintAllTips()
    {
        RePaintBaseTips();
        RePaintWallTips();
        RePaintRideTips();
        RePaintRoleTips();
        RePaintHighTips();

        RepaintResultTips();
    }


    // 選択項目が変更されたときのイベントハンドラ
    void basePictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        PictureBox pb = (PictureBox)sender;
        int x = pb.Left + 8;
        int y = pb.Top + 8;
        for (int i = 0; i < basePictureBox.Length; i++)
        {
            if (basePictureBox[i].Top < y && y < basePictureBox[i].Bottom &&
                basePictureBox[i].Left < x && x < basePictureBox[i].Right )
            {
                baseSelectedPictureBox = i;
                basePictureBox[i].BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                basePictureBox[i].BorderStyle = BorderStyle.None;
            }
        }
    }
    // 選択項目が変更されたときのイベントハンドラ
    // 選択項目が変更されたときのイベントハンドラ
    void wallPictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        PictureBox pb = (PictureBox)sender;
        int x = pb.Left + 8;
        int y = pb.Top + 8;
        for (int i = 0; i < wallPictureBox.Length; i++)
        {
            if (wallPictureBox[i].Top < y && y < wallPictureBox[i].Bottom &&
                wallPictureBox[i].Left < x && x < wallPictureBox[i].Right)
            {
                wallSelectedPictureBox = i;
                wallPictureBox[i].BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                wallPictureBox[i].BorderStyle = BorderStyle.None;
            }
        }
    }

    // 選択項目が変更されたときのイベントハンドラ
    void ridePictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        PictureBox pb = (PictureBox)sender;
        int x = pb.Left + 8;
        int y = pb.Top + 8;
        for (int i = 0; i < ridePictureBox.Length; i++)
        {
            if (ridePictureBox[i].Top < y && y < ridePictureBox[i].Bottom &&
                ridePictureBox[i].Left < x && x < ridePictureBox[i].Right)
            {
                rideSelectedPictureBox = i;
                ridePictureBox[i].BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                ridePictureBox[i].BorderStyle = BorderStyle.None;
            }
        }
    }

    // 選択項目が変更されたときのイベントハンドラ
    void rolePictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        PictureBox pb = (PictureBox)sender;
        int x = pb.Left + 8;
        int y = pb.Top + 8;
        for (int i = 0; i < rolePictureBox.Length; i++)
        {
            if (rolePictureBox[i].Top < y && y < rolePictureBox[i].Bottom &&
                rolePictureBox[i].Left < x && x < rolePictureBox[i].Right)
            {
                roleSelectedPictureBox = i;
                rolePictureBox[i].BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                rolePictureBox[i].BorderStyle = BorderStyle.None;
            }
        }
    }

    // 選択項目が変更されたときのイベントハンドラ
    void highPictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        PictureBox pb = (PictureBox)sender;
        int x = pb.Left + 8;
        int y = pb.Top + 8;
        for (int i = 0; i < highPictureBox.Length; i++)
        {
            if (highPictureBox[i].Top < y && y < highPictureBox[i].Bottom &&
                highPictureBox[i].Left < x && x < highPictureBox[i].Right)
            {
                highSelectedPictureBox = i;
                highPictureBox[i].BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                highPictureBox[i].BorderStyle = BorderStyle.None;
            }
        }
    }
    
}
