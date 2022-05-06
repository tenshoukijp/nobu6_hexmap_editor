using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;



partial class FieldViewerForm : Form
{
    ComboBox FieldListComboBox;  // コンボボックス


    void AddFieldList()
    {
        FieldListComboBox.BeginUpdate();
        // 項目の追加
        for (int i = 0; i < 34 * 14; i++)
        {
            FieldListComboBox.Items.Add( "エリア:"+String.Format("{0:D3}", i));
        }
        FieldListComboBox.EndUpdate();
    }

    void SetAddComboBox()
    {
        // キャッスルリストをコンボボックスとして
        FieldListComboBox = new ComboBox()
        {
            Location = new Point(15, 30),
            DropDownStyle = ComboBoxStyle.DropDownList,  // 表示形式
        };

        // コンボボックスに城名リスト追加
        AddFieldList();

        // 最初に選択される項目
        FieldListComboBox.SelectedIndex = 0;

        // どれかを選択したらイベント駆動するように
        FieldListComboBox.SelectedIndexChanged += new EventHandler(FieldListComboBox_SelectedIndexChanged);
        FieldListComboBox.KeyDown += new KeyEventHandler(FieldListComboBox_KeyDown);

        // フォームにコンボボックス追加
        this.Controls.Add(FieldListComboBox);

    }

    int GetSelectedHexMapID()
    {
        return FieldListComboBox.SelectedIndex + iHexmapFieldStartID;
    }

    int GetSelectedRoleMapID()
    {
        return GetSelectedHexMapID() + iFieldNum;
    }

    // 選択項目が変更されたときのイベントハンドラ
    void FieldListComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        RePaintAllTips();
    }

    // 上下のキーは１つずつではなくて、上や下のセル
    void FieldListComboBox_KeyDown(object sender, KeyEventArgs e)
    {
        int iSelectedIndex = FieldListComboBox.SelectedIndex;
        if (e.KeyCode == Keys.Up)
        {
            iSelectedIndex -= (iFieldCol);
            if (iSelectedIndex < 0) { iSelectedIndex = 0; }
            FieldListComboBox.SelectedIndex = iSelectedIndex;
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.Down)
        {
            iSelectedIndex += (iFieldCol);
            if (iSelectedIndex >= iFieldNum) { iSelectedIndex = iFieldNum - 1; }
            FieldListComboBox.SelectedIndex = iSelectedIndex;
            e.Handled = true;
        }
    }
}
