using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;



partial class FieldViewerForm : Form
{

    ArrayList csAllHexMapArray; // hexmap.nb6のデータ(の解凍したもの)の全て

    void saveButton_Click(Object sender, EventArgs e)
    {
        int result = LS11DotNet.Ls11.EncodePack("hexmap.nb6", csAllHexMapArray); // 0なら無事保存。それ以外はエラー
        if (result == 0)
        {
            MessageBox.Show("多分保存できたよ!");
        }
        else
        {
            MessageBox.Show("保存に「失敗」したよ!!");
        }
    }

    // hexmap.nb6の読み込み
    void LoadHexMap()
    {
        csAllHexMapArray = new ArrayList();
        int result = LS11DotNet.Ls11.DecodePack("hexmap.nb6", csAllHexMapArray);

        if (result != 0)
        {
            MessageBox.Show("hexmap.n6p の読み込みに失敗しました。");
        }
    }
}
