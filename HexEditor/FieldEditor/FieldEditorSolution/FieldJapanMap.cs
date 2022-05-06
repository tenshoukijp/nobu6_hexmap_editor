using System;
using System.Drawing;
using System.Windows.Forms;

partial class FieldViewerForm : Form
{
    PictureBox PBJapanMap;
    const int waku_width = 20;
    const int waku_height = 12;
    const int japan_map_width = 680;
    const int japan_map_height = 168;

    int iPreUndoListSize = -1;

    private void SetJapanMap()
    {

        smallAllConnectMap = ResizeBitmapImage(MakeAllConnectMap(), japan_map_width, japan_map_height);
        Bitmap base_map = new Bitmap(smallAllConnectMap);
        Bitmap result = new Bitmap(base_map);
        Graphics g = Graphics.FromImage(result);

        Image select_waku = new Bitmap(GetType(), "FieldEditor.images.map.select_waku.png");

        // 枠重ね
        g.DrawImage(select_waku, 0, 0, waku_width, waku_height);

        Image japan_map  = result;
        // もういららない
        select_waku.Dispose();
        base_map.Dispose();

        PBJapanMap = new PictureBox()
        {
            Image = japan_map,
            Location = new Point(iLeftStandingPos, 15),
            Width = japan_map_width,
            Height = japan_map_height,
        };
        PBJapanMap.MouseClick += new MouseEventHandler(PBJapanMap_MouseClick);
        this.Controls.Add(PBJapanMap);
   
    }

    private void PBJapanMap_MouseClick(Object sender, MouseEventArgs e)
    {
        int X = e.Location.X;
        int Y = e.Location.Y;

        int iSelectedIndex = (X / (PBJapanMap.Width/34) ) % 34 + (Y / (PBJapanMap.Height/14) ) * 34;
        FieldListComboBox.SelectedIndex = iSelectedIndex;
    }

    private void RepaintJapanMap()
    {
        withCastle = true;


        // UndoListのサイズが変化していない限り、再描画する必要はない。
        if (UndoList.Count != iPreUndoListSize)
        {
            iPreUndoListSize = UndoList.Count;

            Bitmap bigMap = MakeAllConnectMap();
            // 小さい方のイメージデータ更新
            smallAllConnectMap = ResizeBitmapImage(bigMap, PBJapanMap.Image.Width, PBJapanMap.Image.Height);

            // 元のものは破棄
            bigMap.Dispose();
        }


        //------------- 重ね表示
        // ベースとなるイメージ

        Bitmap base_map;
        base_map = new Bitmap(smallAllConnectMap);

        Bitmap result = new Bitmap(base_map);
        Graphics g = Graphics.FromImage(result);

        Image select_waku = new Bitmap(GetType(), "FieldEditor.images.map.select_waku.png");

        // 変更後の選択ＩＤを得る
        int iSelectedID = FieldListComboBox.SelectedIndex;

        int waku_x = (iSelectedID % iFieldCol) * waku_width;
        int waku_y = (iSelectedID / iFieldCol) * waku_height;

        // 土地重ね
        g.DrawImage(select_waku, waku_x, waku_y, waku_width, waku_height);

        Image japan_map = result;
        base_map.Dispose();
        select_waku.Dispose();

        PBJapanMap.Image = japan_map;
    }

}