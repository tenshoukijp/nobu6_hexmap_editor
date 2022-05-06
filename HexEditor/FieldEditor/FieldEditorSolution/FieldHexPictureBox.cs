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

    int baseSelectedPictureBox = -1;
    int edgeSelectedPictureBox = -1;
    int rideSelectedPictureBox = -1;
    int roleSelectedPictureBox = -1;

    PictureBox[] basePictureBox;
    PictureBox[] edgePictureBox;
    PictureBox[] ridePictureBox;
    PictureBox[] rolePictureBox;

    PictureBox[] resultPictureBoxStack;
    PictureBox[] resultLUPictureBoxStack;
    PictureBox[] resultCUPictureBoxStack;
    PictureBox[] resultRUPictureBoxStack;
    PictureBox[] resultLLPictureBoxStack;
    PictureBox[] resultRRPictureBoxStack;
    PictureBox[] resultLDPictureBoxStack;
    PictureBox[] resultCDPictureBoxStack;
    PictureBox[] resultRDPictureBoxStack;

    const int iColMax = 15;
    const int iRowMax = 8;
    const int iWarColMax = 21;
    const int iWarRowMax = 12;

    const int iTipImageSize = 16;

    const int iLeftStandingPos = 170;
    const int iTopStandingPos = 230;

    const int iFieldCol = 34;
    const int iFieldRow = 14;
    const int iFieldNum = iFieldCol * iFieldRow;

    const int iHexmapFieldStartID = 0;

    const int XBetween = 340; // 横間隔
    const int YBetween = 340; // 縦間隔

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
        this.edgePictureBox = new PictureBox[iColMax * iRowMax];
        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.edgePictureBox[i] = new PictureBox();
                this.edgePictureBox[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.edgePictureBox[i].MouseDown += new MouseEventHandler(edgePictureBox_MouseDown);

                if (row % 2 == 0)
                {
                    this.edgePictureBox[i].Location = new Point(
                        iLeftStandingPos + XBetween + col * iTipImageSize,
                        iTopStandingPos + row * iTipImageSize);
                }
                else
                {
                    this.edgePictureBox[i].Location = new Point(
                        iLeftStandingPos + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + row * iTipImageSize);
                }
                this.Controls.Add(edgePictureBox[i]);
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
                        iLeftStandingPos + XBetween * 2 + col * iTipImageSize,
                        iTopStandingPos + row * iTipImageSize);
                }
                else
                {
                    this.ridePictureBox[i].Location = new Point(
                        iLeftStandingPos + XBetween * 2 + (iTipImageSize / 2) + col * iTipImageSize,
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
                        iTopStandingPos + YBetween + row * iTipImageSize);
                }
                else
                {
                    this.rolePictureBox[i].Location = new Point(
                        iLeftStandingPos + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize);
                }
                this.Controls.Add(rolePictureBox[i]);
            }
        }

        // 結果
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
                        iLeftStandingPos + 200 + XBetween + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100);
                }
                else
                {
                    this.resultPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100);
                }
                this.Controls.Add(resultPictureBoxStack[i]);
            }
        }


        // 左上
        this.resultLUPictureBoxStack = new PictureBox[iColMax * iRowMax];

        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.resultLUPictureBoxStack[i] = new PictureBox();
                this.resultLUPictureBoxStack[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.resultLUPictureBoxStack[i].AllowDrop = true;

                if (col % iColMax >= iColMax - 2)
                {
                    continue;
                }

                if (row % 2 == 0)
                {
                    this.resultLUPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 - (iColMax - 2) * iTipImageSize + XBetween + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 - 16 * iRowMax);
                }
                else
                {
                    this.resultLUPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 - (iColMax - 2) * iTipImageSize + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 - 16 * iRowMax);
                }
                this.Controls.Add(resultLUPictureBoxStack[i]);
            }
        }

        // 上
        this.resultCUPictureBoxStack = new PictureBox[iColMax * iRowMax];

        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.resultCUPictureBoxStack[i] = new PictureBox();
                this.resultCUPictureBoxStack[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.resultCUPictureBoxStack[i].AllowDrop = true;

                if (row % 2 == 0)
                {
                    this.resultCUPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + XBetween + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 - 16 * iRowMax);
                }
                else
                {
                    this.resultCUPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 - 16 * iRowMax);
                }
                this.Controls.Add(resultCUPictureBoxStack[i]);
            }
        }

        // 右上
        this.resultRUPictureBoxStack = new PictureBox[iColMax * iRowMax];

        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.resultRUPictureBoxStack[i] = new PictureBox();
                this.resultRUPictureBoxStack[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.resultRUPictureBoxStack[i].AllowDrop = true;

                if (col % iColMax <= 1)
                {
                    continue;
                }

                if (row % 2 == 0)
                {
                    this.resultRUPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + (iColMax - 2) * iTipImageSize + XBetween + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 - 16 * iRowMax);
                }
                else
                {
                    this.resultRUPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + (iColMax - 2) * iTipImageSize + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 - 16 * iRowMax);
                }
                this.Controls.Add(resultRUPictureBoxStack[i]);
            }
        }

        // 左
        this.resultLLPictureBoxStack = new PictureBox[iColMax * iRowMax];

        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.resultLLPictureBoxStack[i] = new PictureBox();
                this.resultLLPictureBoxStack[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.resultLLPictureBoxStack[i].AllowDrop = true;

                if (col % iColMax >= iColMax - 2)
                {
                    continue;
                }

                if (row % 2 == 0)
                {
                    this.resultLLPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 - (iColMax - 2) * iTipImageSize + XBetween + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100);
                }
                else
                {
                    this.resultLLPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 - (iColMax - 2) * iTipImageSize + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100);
                }
                this.Controls.Add(resultLLPictureBoxStack[i]);
            }
        }

        // 右
        this.resultRRPictureBoxStack = new PictureBox[iColMax * iRowMax];

        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.resultRRPictureBoxStack[i] = new PictureBox();
                this.resultRRPictureBoxStack[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.resultRRPictureBoxStack[i].AllowDrop = true;

                if (col % iColMax <= 1)
                {
                    continue;
                }

                if (row % 2 == 0)
                {
                    this.resultRRPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + (iColMax - 2) * iTipImageSize + XBetween + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100);
                }
                else
                {
                    this.resultRRPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + (iColMax - 2) * iTipImageSize + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100);
                }
                this.Controls.Add(resultRRPictureBoxStack[i]);
            }
        }

        // 左下
        this.resultLDPictureBoxStack = new PictureBox[iColMax * iRowMax];

        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.resultLDPictureBoxStack[i] = new PictureBox();
                this.resultLDPictureBoxStack[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.resultLDPictureBoxStack[i].AllowDrop = true;

                if (col % iColMax >= iColMax - 2)
                {
                    continue;
                }

                if (row % 2 == 0)
                {
                    this.resultLDPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 - (iColMax - 2) * iTipImageSize + XBetween + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 + 16 * iRowMax);
                }
                else
                {
                    this.resultLDPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 - (iColMax - 2) * iTipImageSize + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 + 16 * iRowMax);
                }
                this.Controls.Add(resultLDPictureBoxStack[i]);
            }
        }

        // 下
        this.resultCDPictureBoxStack = new PictureBox[iColMax * iRowMax];

        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.resultCDPictureBoxStack[i] = new PictureBox();
                this.resultCDPictureBoxStack[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.resultCDPictureBoxStack[i].AllowDrop = true;

                if (row % 2 == 0)
                {
                    this.resultCDPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + XBetween + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 + 16 * iRowMax);
                }
                else
                {
                    this.resultCDPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 + 16 * iRowMax);
                }
                this.Controls.Add(resultCDPictureBoxStack[i]);
            }
        }

        // 右下
        this.resultRDPictureBoxStack = new PictureBox[iColMax * iRowMax];

        for (int row = 0; row < iRowMax; row++)
        {
            for (int col = 0; col < iColMax; col++)
            {
                int i = iColMax * row + col; // 縦横の位置からplaneなindex

                this.resultRDPictureBoxStack[i] = new PictureBox();
                this.resultRDPictureBoxStack[i].Size = new Size(iTipImageSize, iTipImageSize);
                this.resultRDPictureBoxStack[i].AllowDrop = true;

                if (col % iColMax <= 1)
                {
                    continue;
                }

                if (row % 2 == 0)
                {
                    this.resultRDPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + (iColMax - 2) * iTipImageSize + XBetween + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 + 16 * iRowMax);
                }
                else
                {
                    this.resultRDPictureBoxStack[i].Location = new Point(
                        iLeftStandingPos + 200 + (iColMax - 2) * iTipImageSize + XBetween + (iTipImageSize / 2) + col * iTipImageSize,
                        iTopStandingPos + YBetween + row * iTipImageSize + 100 + 16 * iRowMax);
                }
                this.Controls.Add(resultRDPictureBoxStack[i]);
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


    void RePaintEdgeTips()
    {

        // コンボボックスで選択した城の「チップリスト」を取得
        ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];

        for (int i = 0; i < iRowMax * iColMax; i++)
        {

            // ウォール
            Byte tip = (Byte)csOneHexMapTipsList[i + (iColMax * iRowMax) * 1];
            Image edgeImage = edgeImages[tip];
            this.edgePictureBox[i].Image = edgeImage;
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


    void RepaintResultTips()
    {
        for (int i = 0; i < iRowMax * iColMax; i++)
        {

            //------------- 重ね表示
            // ベースとなるイメージ
            Bitmap ff = new Bitmap(GetType(), "FieldEditor.images.edge.FF.png");
            Bitmap result = new Bitmap(ff);
            Graphics g = Graphics.FromImage(result);
            // 土地重ね
            g.DrawImage(this.basePictureBox[i].Image, 0, 0, iTipImageSize, iTipImageSize);

            if ((int)this.edgePictureBox[i].Image.Tag != 0xFF)
            {// 透明ではない
                // 壁重ね
                g.DrawImage(this.edgePictureBox[i].Image, 0, 0, iTipImageSize, iTipImageSize);
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

            // 重ねたので破棄
            g.Dispose();
            // もういららない
            ff.Dispose();
            this.resultPictureBoxStack[i].Image = result;
        }

        RepaintJapanMap();
    }

    void RepaintResultSideTips(int iTargetFieldID, PictureBox[] PBTargetPictureBox)
    {
        // -----------------半透明オブジェクト
        //ColorMatrixオブジェクトの作成
        System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix();
        //ColorMatrixの行列の値を変更して、アルファ値が0.5に変更されるようにする
        cm.Matrix00 = 1;
        cm.Matrix11 = 1;
        cm.Matrix22 = 1;
        cm.Matrix33 = 0.4F;
        cm.Matrix44 = 1;

        //ImageAttributesオブジェクトの作成
        System.Drawing.Imaging.ImageAttributes ia = new System.Drawing.Imaging.ImageAttributes();
        //ColorMatrixを設定する
        ia.SetColorMatrix(cm);


        // 上
        // コンボボックスで選択した城の「チップリスト」を取得
        if (0 <= iTargetFieldID && iTargetFieldID < iFieldNum)
        {
            ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[iTargetFieldID];

            for (int i = 0; i < iRowMax * iColMax; i++)
            {
                //------------- 重ね表示
                // ベースとなるイメージ
                Bitmap ff = new Bitmap(GetType(), "FieldEditor.images.edge.FF.png");
                Bitmap result = new Bitmap(ff);
                Graphics g = Graphics.FromImage(result);

                Byte tip = (Byte)csOneHexMapTipsList[i];
                Image baseImage = baseImages[tip];

                // 土地重ね
                g.DrawImage(baseImage, 0, 0, iTipImageSize, iTipImageSize);

                // ベース
                tip = (Byte)csOneHexMapTipsList[i + (iColMax * iRowMax) * 1];
                Image edgeImage = edgeImages[tip];

                // 透明ではない
                if ((int)edgeImage.Tag != 0xFF)
                {
                    // 壁重ね
                    g.DrawImage(edgeImage, 0, 0, iTipImageSize, iTipImageSize);
                }

                // ベース
                tip = (Byte)csOneHexMapTipsList[i + (iColMax * iRowMax) * 2];
                Image rideImage = rideImages[tip];

                // 透明ではない
                if ((int)rideImage.Tag != 0xFF)
                {
                    // 壁重ね
                    g.DrawImage(rideImage, 0, 0, iTipImageSize, iTipImageSize);
                }

                // 重ねたので破棄
                g.Dispose();

                ff = new Bitmap(GetType(), "FieldEditor.images.edge.FF.png");
                Bitmap new_result = new Bitmap(ff);
                g = Graphics.FromImage(new_result);
                Image allimage = new Bitmap(result);
                g.DrawImage(allimage, new Rectangle(0, 0, iTipImageSize, iTipImageSize), 0, 0, iTipImageSize, iTipImageSize, GraphicsUnit.Pixel, ia);
                // 重ねたので破棄
                g.Dispose();

                // もういららない
                ff.Dispose();
                PBTargetPictureBox[i].Image = new_result;
            }
        }
        else
        {
            for (int i = 0; i < iRowMax * iColMax; i++)
            {
                PBTargetPictureBox[i].Image = new Bitmap(GetType(), "FieldEditor.images.edge.FF.png");
            }
        }
    }


    void RePaintAllTips()
    {

        RePaintBaseTips();
        RePaintEdgeTips();
        RePaintRideTips();
        RePaintRoleTips();

        RepaintResultTips();
        // 一番左である。
        if (GetSelectedHexMapID() % iFieldCol == 0)
        {
            RepaintResultSideTips(-1, resultLUPictureBoxStack);
        }
        else
        {
            RepaintResultSideTips(GetSelectedHexMapID() - iFieldCol - 1, resultLUPictureBoxStack);
        }

        // 真ん中
        RepaintResultSideTips(GetSelectedHexMapID() - iFieldCol, resultCUPictureBoxStack);

        // 一番右である。
        if (GetSelectedHexMapID() % iFieldCol == iFieldCol - 1)
        {
            RepaintResultSideTips(-1, resultRUPictureBoxStack);
        }
        else
        {
            RepaintResultSideTips(GetSelectedHexMapID() - iFieldCol + 1, resultRUPictureBoxStack);
        }

        // 一番左である。
        if (GetSelectedHexMapID() % iFieldCol == 0)
        {
            RepaintResultSideTips(-1, resultLLPictureBoxStack);
        }
        else
        {
            RepaintResultSideTips(GetSelectedHexMapID() - 1, resultLLPictureBoxStack);
        }

        // 一番右である。
        if (GetSelectedHexMapID() % iFieldCol == iFieldCol - 1)
        {
            RepaintResultSideTips(-1, resultRRPictureBoxStack);
        }
        else
        {
            RepaintResultSideTips(GetSelectedHexMapID() + 1, resultRRPictureBoxStack);
        }

        // 一番左である。
        if (GetSelectedHexMapID() % iFieldCol == 0)
        {
            RepaintResultSideTips(-1, resultLDPictureBoxStack);
        }
        else
        {
            RepaintResultSideTips(GetSelectedHexMapID() + iFieldCol - 1, resultLDPictureBoxStack);
        }

        // 真ん中
        RepaintResultSideTips(GetSelectedHexMapID() + iFieldCol, resultCDPictureBoxStack);

        // 一番右である。
        if (GetSelectedHexMapID() % iFieldCol == iFieldCol - 1)
        {
            RepaintResultSideTips(-1, resultRDPictureBoxStack);
        }
        else
        {
            RepaintResultSideTips(GetSelectedHexMapID() + iFieldCol + 1, resultRDPictureBoxStack);
        }
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
                basePictureBox[i].Left < x && x < basePictureBox[i].Right)
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
    void edgePictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        PictureBox pb = (PictureBox)sender;
        int x = pb.Left + 8;
        int y = pb.Top + 8;
        for (int i = 0; i < edgePictureBox.Length; i++)
        {
            if (edgePictureBox[i].Top < y && y < edgePictureBox[i].Bottom &&
                edgePictureBox[i].Left < x && x < edgePictureBox[i].Right)
            {
                edgeSelectedPictureBox = i;
                edgePictureBox[i].BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                edgePictureBox[i].BorderStyle = BorderStyle.None;
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

}
