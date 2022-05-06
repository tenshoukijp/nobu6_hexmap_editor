using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

partial class CastleViewerForm : Form
{

    private void SetForm()
    {
        InitializeComponent();

        // フォームのサイズ
        this.Width = 1200;
        this.Height = 780;
        // this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.AutoScroll = true;
        this.Text = "城ヘックスエディタ";
    }


    private void SetMenu()
    {

        MenuItem miSave = new MenuItem("全て保存(&S)",
            new EventHandler(saveButton_Click));

        MenuItem miOneMapExport = new MenuItem("この城をファイルへと出力",
            new EventHandler(oneMapExport_Click));

        MenuItem miOneMapImport = new MenuItem("この城にファイルから取込",
            new EventHandler(oneMapImport_Click));

        MenuItem miFile = new MenuItem("ファイル(&F)",
            new MenuItem[] { miSave, miOneMapExport, miOneMapImport });

        Menu = new MainMenu(new MenuItem[] { miFile });

    }

    public CastleViewerForm()
    {
        this.SuspendLayout();

        if (!System.IO.File.Exists("hexmap.nb6"))
        {
            MessageBox.Show("hexmap.nb6 のファイルがありません。\nこのエディタは天翔記フォルダにて起動してください");
            Environment.Exit(1);
        }
        if (!System.IO.File.Exists("LS11DotNet.dll"))
        {
            MessageBox.Show("LS11DotNet.dll のファイルがありません。\n該当ファイルは必要となります");
            Environment.Exit(1);
        }

        // フォームサイズ
        SetForm();

        // メニュー
        SetMenu();

        // ラベルのセット
        SetLabel();

        // 全消去ボタン
        SetClearButton();

        // コンボボックス系処理
        SetAddComboBox();

        // チェックボックス追加
        SetCheckBox();

        // イメージの登録(キャッシュ的な)
        RegistImages();

        // ピクチャーボックスを多数配置。
        RegistPictureBoxes();

        // 選択用のチップボードを配置
        RegistTipBoard();

        // ヘックスマップの読み込み
        LoadHexMap();

        // 全て一端描画
        RePaintAllTips();

        // Undoデータの設定
        SetUnDoData();

        this.ResumeLayout(false);
    }

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CastleViewerForm));
        // 
        // CastleViewerForm
        // 
        this.ClientSize = new System.Drawing.Size(284, 261);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "CastleViewerForm";
    }

    [System.Security.Permissions.UIPermission(System.Security.Permissions.SecurityAction.Demand, Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
    protected override bool ProcessDialogKey(Keys keyData)
    {
        if ((keyData & Keys.KeyCode) == Keys.Z)
        {
            // 記憶が１つでもあるならば…
            if (UndoList.Count > 0)
            {
                for (int i = basePictureBox.Length; i > 0; i--)
                {

                    TUndoData undo = UndoList[UndoList.Count - 1]; // 最後の要素が最新のUndo

                    // コンボボックスで選択した城の「チップリスト」を取得
                    ArrayList csOneHexMapList = (ArrayList)csAllHexMapArray[undo.iMapIndex];

                    // 対象のチップがヘックスの選択ち一致するので、ここのIDを書き換え
                    csOneHexMapList[undo.iBinaryIndex] = (Byte)undo.iOriginalTip;

                    // より戻し。
                    csAllHexMapArray[undo.iMapIndex] = csOneHexMapList;

                    bool isEnd = undo.isUndoEnd;

                    UndoList.RemoveAt(UndoList.Count - 1); // 使ったUndo要素(最後の要素)を削除

                    if (isEnd)
                    {
                        break;
                    }
                }

                RePaintAllTips();
                return true;
            }
        }

        return base.ProcessDialogKey(keyData);
    }

}