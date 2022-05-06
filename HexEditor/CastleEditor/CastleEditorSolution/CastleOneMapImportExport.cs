using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

partial class CastleViewerForm : Form
{
    private void oneMapExport_Click(Object sender, EventArgs e)
    {

        // 現在選択中のマップID
        int iSelectedMapID = CastleListComboBox.SelectedIndex;

        //SaveFileDialogクラスのインスタンスを作成
        SaveFileDialog sfd = new SaveFileDialog();

        //はじめのファイル名を指定する
        sfd.FileName = String.Format("Castle{0:D3}", iSelectedMapID) + ".castle";
        //はじめに表示されるフォルダを指定する
        sfd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
        //[ファイルの種類]に表示される選択肢を指定する
        sfd.Filter =
            "城データファイル(*.castle)|*.castle";
        //[ファイルの種類]ではじめに
        //「城データファイル」が選択されているようにする
        sfd.FilterIndex = 1;
        //タイトルを設定する
        sfd.Title = "保存先のファイル名を入力してください";
        //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        sfd.RestoreDirectory = true;
        //既に存在するファイル名を指定したとき警告する
        //デフォルトでTrueなので指定する必要はない
        sfd.OverwritePrompt = true;
        //存在しないパスが指定されたとき警告を表示する
        //デフォルトでTrueなので指定する必要はない
        sfd.CheckPathExists = true;

        //ダイアログを表示する
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            //OKボタンがクリックされたとき
            //選択された名前で新しいファイルを作成し、
            //読み書きアクセス許可でそのファイルを開く
            //既存のファイルが選択されたときはデータが消える恐れあり
            System.IO.Stream stream;
            stream = sfd.OpenFile();
            if (stream != null)
            {
                // 選択しているもののグラフィック系
                ArrayList OneHexMap = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];
                ArrayList RoleHexMap = (ArrayList)csAllHexMapArray[GetSelectedRoleMapID()];
                ArrayList HighHexMap = (ArrayList)csAllHexMapArray[GetSelectedHighMapID()];

                //ファイルに書き込む
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(stream);

                // グラ系
                for (int iBinaryIndex = 0; iBinaryIndex < OneHexMap.Count; iBinaryIndex++)
                {
                    bw.Write((Byte)OneHexMap[iBinaryIndex]);
                }
                // 役割
                for (int iBinaryIndex = 0; iBinaryIndex < RoleHexMap.Count; iBinaryIndex++)
                {
                    bw.Write((Byte)RoleHexMap[iBinaryIndex]);
                }
                // 高さ
                for (int iBinaryIndex = 0; iBinaryIndex < HighHexMap.Count; iBinaryIndex++)
                {
                    bw.Write((Byte)HighHexMap[iBinaryIndex]);
                }

                //閉じる
                bw.Close();
                stream.Close();
            }
        }
    }
    private void oneMapImport_Click(Object sender, EventArgs e)
    {
        // 現在選択中のマップID
        int iSelectedMapID = CastleListComboBox.SelectedIndex;

        //OpenFileDialogクラスのインスタンスを作成
        OpenFileDialog ofd = new OpenFileDialog();

        //はじめに表示されるフォルダを指定する
        ofd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

        //[ファイルの種類]に表示される選択肢を指定する
        ofd.Filter =
            "城データファイル(*.castle)|*.castle";
        //[ファイルの種類]ではじめに
        //「城データファイル」が選択されているようにする
        ofd.FilterIndex = 1;
        //タイトルを設定する
        ofd.Title = "保存先のファイル名を入力してください";
        //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        ofd.RestoreDirectory = true;
        //存在しないパスが指定されたとき警告を表示する
        //デフォルトでTrueなので指定する必要はない
        ofd.CheckPathExists = true;

        //ダイアログを表示する
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            //OKボタンがクリックされたとき
            //選択された名前で新しいファイルを作成し、
            //読み書きアクセス許可でそのファイルを開く
            //既存のファイルが選択されたときはデータが消える恐れあり
            System.IO.Stream stream;
            stream = ofd.OpenFile();
            if (stream != null)
            {
                // 選択しているもののグラフィック系
                ArrayList OneHexMap = (ArrayList)csAllHexMapArray[GetSelectedHexMapID()];
                ArrayList RoleHexMap = (ArrayList)csAllHexMapArray[GetSelectedRoleMapID()];
                ArrayList HighHexMap = (ArrayList)csAllHexMapArray[GetSelectedHighMapID()];

                //ファイルから読み込む
                System.IO.BinaryReader br = new System.IO.BinaryReader(stream);

                // グラ系
                for (int iBinaryIndex = 0; iBinaryIndex < OneHexMap.Count; iBinaryIndex++)
                {
                    OneHexMap[iBinaryIndex] = br.ReadByte();
                }
                // 役割
                for (int iBinaryIndex = 0; iBinaryIndex < RoleHexMap.Count; iBinaryIndex++)
                {
                    RoleHexMap[iBinaryIndex] = br.ReadByte();
                }
                // 高さ
                for (int iBinaryIndex = 0; iBinaryIndex < HighHexMap.Count; iBinaryIndex++)
                {
                    HighHexMap[iBinaryIndex] = br.ReadByte();
                }

                //閉じる
                br.Close();

                stream.Close();

                // 全て一端描画
                RePaintAllTips();

            }
        }
    }

}