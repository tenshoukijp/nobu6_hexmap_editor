using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;


partial class FieldViewerForm : Form
{
    class TUndoData
    {
        public int iMapIndex; // csAllHexMapArray上でのindex。
        public int iBinaryIndex; // 分割されたhexmap.??? 内で何バイト目なのか
        public byte iOriginalTip;       // 変更前のチップＩＤ
        public bool isUndoEnd;
    };

    List<TUndoData> UndoList;


    public void SetUnDoData()
    {
        UndoList = new List<TUndoData>();
    }

    // １つ分記憶する。
    void RememberUndo(int iSelectedHexMapID, int iBinaryIndex, Byte iOriginalTip, bool isUndoEnd)
    {
        TUndoData undo = new TUndoData();
        undo.iMapIndex = iSelectedHexMapID;
        undo.iBinaryIndex = iBinaryIndex;
        undo.iOriginalTip = iOriginalTip;
        undo.isUndoEnd = isUndoEnd;
        UndoList.Add(undo);
    }
}