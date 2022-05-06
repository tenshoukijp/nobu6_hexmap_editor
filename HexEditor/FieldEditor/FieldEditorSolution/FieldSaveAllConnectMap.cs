using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

partial class FieldViewerForm : Form
{
    bool withCastle = true;
    Image smallAllConnectMap;

    Bitmap MakeAllConnectMap()
    {
        Bitmap white = new Bitmap(iTipImageSize * iFieldCol * (iColMax-2) + iTipImageSize*3, iTipImageSize * iFieldRow * iRowMax); // 縦×横のみ
        Bitmap result = new Bitmap(white);
        Graphics g = Graphics.FromImage(result);

        for (int iSelectedMapID = 0; iSelectedMapID < iFieldNum; iSelectedMapID++)
        {
            // 
            ArrayList csOneHexMapTipsList = (ArrayList)csAllHexMapArray[iSelectedMapID];

            for (int i = 0; i < iRowMax * iColMax; i++)
            {

                int iBinaryId = i;

                //------------- 重ね表示
                // ベースとなるイメージ
                // 土地重ね
                int posx_base = (iSelectedMapID % iFieldCol) * ((iColMax-2) * iTipImageSize); // 該当グリッドの左上のＸ座標
                int posx = posx_base + (iTipImageSize * (iBinaryId % iColMax)); // 該当グリッドの中での座標を足し込み
                int posy_base = (iSelectedMapID / iFieldCol) * (iRowMax * iTipImageSize); // 該当グリッドの左上のＹ座標
                int posy = posy_base + (iTipImageSize * (iBinaryId / iColMax)); // 該当グリッドの中での座標の足し込み

                Byte btip = (Byte)csOneHexMapTipsList[iBinaryId];
                Image base_tip = baseImages[btip];

                Byte etip = (Byte)csOneHexMapTipsList[iBinaryId + (iColMax * iRowMax) * 1];
                Image edge_tip = edgeImages[etip];

                Byte rtip = (Byte)csOneHexMapTipsList[iBinaryId + (iColMax * iRowMax) * 2];
                Image ride_tip = rideImages[rtip];

                // 偶数段目
                if ((iBinaryId / iColMax) % 2 == 0)
                {
                    if (btip != 0xFF)
                    {
                        g.DrawImage(base_tip, posx, posy, iTipImageSize, iTipImageSize);
                    }
                    if (etip != 0xFF)
                    {
                        g.DrawImage(edge_tip, posx, posy, iTipImageSize, iTipImageSize);
                    }
                    if (rtip != 0xFF)
                    {
                        // 城無し
                        if (!withCastle && (rtip == 0xED || rtip == 0xEE || rtip == 0xEC))
                        {
                            ;
                        }
                        else
                        {
                            g.DrawImage(ride_tip, posx, posy, iTipImageSize, iTipImageSize);
                        }
                    }
                }
                // 奇数段目
                else
                {
                    if (btip != 0xFF)
                    {
                        g.DrawImage(base_tip, posx + (iTipImageSize / 2), posy, iTipImageSize, iTipImageSize);
                    }
                    if (etip != 0xFF)
                    {
                        g.DrawImage(edge_tip, posx + (iTipImageSize / 2), posy, iTipImageSize, iTipImageSize);
                    }
                    if (rtip != 0xFF)
                    {
                        // 城無し
                        if (!withCastle && (rtip == 0xED || rtip == 0xEE || rtip == 0xEC))
                        {
                            ;
                        }
                        else
                        {
                            g.DrawImage(ride_tip, posx + (iTipImageSize / 2), posy, iTipImageSize, iTipImageSize);
                        }
                    }
                }
            }
        }

        white.Dispose();

        // 重ねたので破棄
        g.Dispose();

        return result;
    }

    void saveAllConnectMapBtn_Click(Object sender, EventArgs e)
    {
        Bitmap result = MakeAllConnectMap();

        result.Save("全連結図.png");

        // 元のものは破棄
        result.Dispose();

        MessageBox.Show("全連結図.png に保存出来たかも");
    }


    public static Bitmap ResizeBitmapImage(Bitmap image, int w, int h)
    {

        Bitmap result = new Bitmap(w, h);
        Graphics g = Graphics.FromImage(result);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.DrawImage(image, 0, 0, result.Width, result.Height);

        return result;
    }

    void saveAllConnectMapBtnWtCastle_Click(Object sender, EventArgs e)
    {
        withCastle = true;

        saveAllConnectMapBtn_Click(sender, e);
    }

    void saveAllConnectMapBtnWoCastle_Click(Object sender, EventArgs e)
    {
        withCastle = false;

        saveAllConnectMapBtn_Click(sender, e);
    }

}
