﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;



partial class CastleViewerForm : Form
{
    ComboBox CastleListComboBox;  // コンボボックス


    void AddCastleList()
    {
        String[] list = {
	        "000:金石城",
	        "001:七尾城",
	        "002:指月城",
	        "003:益田城",
	        "004:神西城",
	        "005:白鹿城",
	        "006:尾山御坊",
	        "007:湊城",
	        "008:桧山城",
	        "009:徳山館",
	        "010:平戸城",
	        "011:門司城",
	        "012:三本松城",
	        "013:山吹城",
	        "014:月山富田城",
	        "015:北ノ庄城",
	        "016:大聖寺城",
	        "017:雑太城",
	        "018:尾浦城",
	        "019:鮭延城",
	        "020:石川城",
	        "021:立花山城",
	        "022:且山城",
	        "023:尾高城",
	        "024:八橋城",
	        "025:増山城",
	        "026:本庄城",
	        "027:山形城",
	        "028:横手城",
	        "029:角館城",
	        "030:浪岡城",
	        "031:勢福寺城",
	        "032:岩屋城",
	        "033:城井谷城",
	        "034:高嶺城",
	        "035:瀬戸山城",
	        "036:羽衣石城",
	        "037:鳥取城",
	        "038:府中城",
	        "039:一乗谷城",
	        "040:富山城",
	        "041:与板城",
	        "042:水ヶ江城",
	        "043:富田若山城",
	        "044:小倉山城",
	        "045:高田城",
	        "046:宮津城",
	        "047:魚津城",
	        "048:琵琶島城",
	        "049:新発田城",
	        "050:米沢城",
	        "051:名生城",
	        "052:不来方城",
	        "053:三戸城",
	        "054:蒲池城",
	        "055:古処山城",
	        "056:吉田郡山城",
	        "057:林野城",
	        "058:若桜鬼ヶ城",
	        "059:此隅城",
	        "060:建部山城",
	        "061:後瀬山城",
	        "062:金ヶ崎城",
	        "063:春日山城",
	        "064:栃尾城",
	        "065:津川城",
	        "066:黒川城",
	        "067:白石城",
	        "068:寺池城",
	        "069:高水寺城",
	        "070:九戸城",
	        "071:三城",
	        "072:府内城",
	        "073:鞍掛山城",
	        "074:佐東銀山城",
	        "075:鶴首城",
	        "076:松倉城",
	        "077:坂戸城",
	        "078:二本松城",
	        "079:日野江城",
	        "080:岡城",
	        "081:古高山城",
	        "082:天神山城",
	        "083:上月城",
	        "084:竹田城",
	        "085:横山城",
	        "086:大溝城",
	        "087:小谷城",
	        "088:郡上八幡城",
	        "089:飯山城",
	        "090:須賀川城",
	        "091:隈府城",
	        "092:丹生島城",
	        "093:三原城",
	        "094:猿掛城",
	        "095:沼田城",
	        "096:三春城",
	        "097:小高城",
	        "098:内牧城",
	        "099:来島城",
	        "100:神辺城",
	        "101:岡山城",
	        "102:砥石城",
	        "103:八上城",
	        "104:上平寺城",
	        "105:大垣城",
	        "106:川手城",
	        "107:林城",
	        "108:葛尾城",
	        "109:志賀城",
	        "110:宇都宮城",
	        "111:烏山城",
	        "112:小峰城",
	        "113:古麓城",
	        "114:栂牟礼城",
	        "115:大洲城",
	        "116:姫路城",
	        "117:坂本城",
	        "118:厩橋城",
	        "119:唐沢山城",
	        "120:大館城",
	        "121:水俣城",
	        "122:湯築城",
	        "123:仏殿城",
	        "124:天霧城",
	        "125:三木城",
	        "126:丹波亀山城",
	        "127:室町御所",
	        "128:佐和山城",
	        "129:犬山城",
	        "130:上原城",
	        "131:箕輪城",
	        "132:小山城",
	        "133:人吉城",
	        "134:門川城",
	        "135:松葉城",
	        "136:観音寺城",
	        "137:岩村城",
	        "138:木曾福島城",
	        "139:平井城",
	        "140:忍城",
	        "141:結城城",
	        "142:太田城",
	        "143:出水城",
	        "144:本山城",
	        "145:白地城",
	        "146:十河城",
	        "147:伊丹城",
	        "148:勝龍寺城",
	        "149:清洲城",
	        "150:高遠城",
	        "151:古河城",
	        "152:飯野城",
	        "153:都於郡城",
	        "154:中村城",
	        "155:山科本願寺",
	        "156:水口城",
	        "157:長島願証寺",
	        "158:躑躅ヶ崎館",
	        "159:松山城",
	        "160:河越城",
	        "161:小田城",
	        "162:水戸城",
	        "163:一宇治城",
	        "164:岡豊城",
	        "165:勝瑞城",
	        "166:洲本城",
	        "167:石山本願寺",
	        "168:芥川城",
	        "169:多聞山城",
	        "170:那古野城",
	        "171:飯田城",
	        "172:岩殿山城",
	        "173:岩付城",
	        "174:加治木城",
	        "175:飫肥城",
	        "176:窪川城",
	        "177:上野城",
	        "178:伊勢亀山城",
	        "179:滝山城",
	        "180:江戸城",
	        "181:土浦城",
	        "182:清水城",
	        "183:安芸城",
	        "184:牛岐城",
	        "185:堺",
	        "186:若江城",
	        "187:筒井城",
	        "188:鳴海城",
	        "189:長篠城",
	        "190:玉縄城",
	        "191:垂水城",
	        "192:海部城",
	        "193:岸和田城",
	        "194:安濃津城",
	        "195:二俣城",
	        "196:興国寺城",
	        "197:小田原城",
	        "198:千葉城",
	        "199:肝付城",
	        "200:高屋城",
	        "201:信貴山城",
	        "202:鳥羽城",
	        "203:岡崎城",
	        "204:韮山城",
	        "205:種子島城",
	        "206:岩室城",
	        "207:吉田城",
	        "208:曳馬城",
	        "209:駿府城",
	        "210:稲村城",
	        "211:真里谷城",
	        "212:高天神城",
	        "213:手取城",
	        "214:安土/大坂"
        };

        CastleListComboBox.BeginUpdate();
        // 項目の追加
        CastleListComboBox.Items.AddRange(list);

        CastleListComboBox.EndUpdate();
    }

    void SetAddComboBox()
    {
        // キャッスルリストをコンボボックスとして
        CastleListComboBox = new ComboBox()
        {
            Location = new Point(15, iTopStandingPos),
            DropDownStyle = ComboBoxStyle.DropDownList,  // 表示形式
        };

        // コンボボックスに城名リスト追加
        AddCastleList();

        // 最初に選択される項目
        CastleListComboBox.SelectedIndex = 0;

        // どれかを選択したらイベント駆動するように
        CastleListComboBox.SelectedIndexChanged += new EventHandler(CastleListComboBox_SelectedIndexChanged);

        // フォームにコンボボックス追加
        this.Controls.Add(CastleListComboBox);

    }

    int GetSelectedHexMapID()
    {
        // 安土城/大坂城は最後にくっついている
        if (CastleListComboBox.SelectedIndex == 214)
        {
            return 1594;
        }
        else
        {
            return CastleListComboBox.SelectedIndex + iHexmapCastleStartID;
        }
    }

    int GetSelectedRoleMapID()
    {
        // 安土城/大坂城は最後にくっついている
        if (CastleListComboBox.SelectedIndex == 214)
        {
            return 1595;
        }
        else
        {
            return GetSelectedHexMapID() + iCastleNum;
        }
    }

    int GetSelectedHighMapID()
    {
        // 安土城/大坂城は最後にくっついている
        if (CastleListComboBox.SelectedIndex == 214)
        {
            return 1596;
        }
        else
        {
            return GetSelectedHexMapID() + iCastleNum * 2;
        }
    }

    // 選択項目が変更されたときのイベントハンドラ
    void CastleListComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        RePaintAllTips();
    }
}
