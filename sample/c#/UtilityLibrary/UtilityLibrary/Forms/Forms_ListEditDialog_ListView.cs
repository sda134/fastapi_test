using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mtec.UtilityLibrary;

namespace UtilityLibrary.Forms
{
    /*         
    // ========= 使い方 =========

    public class ListDialog :
        Mtec.UtilityLibrary.Forms.ListEditDialog_ListView<SampleClass>
        {
            // 複数選択する場合
            base.MultiSelect = true;

            // チェックボックスを使う場合
            base.listView.CheckBoxes = true;

        // 以下の方法でリストビューの列を追加する
        // ※注意点 Name プロパティに入れる値を、指定する　T (ジェネリック型) のプロパティ名と一致させる必要がある。
        base.ListViewColumns = new System.Windows.Forms.ColumnHeader[]            
        {
            new System.Windows.Forms.ColumnHeader
            {                    
                Text = "グループ名",
                Name = "GroupName",     ※ T のProperty の名前と完全に一致している必要がある
                Width = 120,                
            },　... 
     * 
     * 
     */


    public delegate object ListItemEditDelegate();

    public partial class ListEditDialog_ListView<T> : Form
    {
        // このクラスはあくまで継承して使う事を目的としているので、このメンバは proteted のままとする
        protected List<T> _items;
        //protected List<T> _items = new List<T>();


        public ListEditDialog_ListView()
        {
            this._items = new List<T>();

            InitializeComponent();
        }

        public int SelectedIndex
        {
            get
            {
                if (this.listView.SelectedIndices.Count < 1)
                    return -1;
                else
                {
                    return this.listView.SelectedIndices[0];
                }
            }
        }


        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public IEnumerable<int> SelectedIndecies
        {
            get
            {                
                return (from int sel in this.listView.SelectedIndices
                        select sel).ToList();
            }

            set
            {
                if (value == null) return;

                foreach (var sel in value)
                {
                    this.listView.Items[sel].Selected = true;
                }

                // 表示の更新
                this.rebuildDisplay();
            }
        }


        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public IEnumerable<T> SelectedItems
        {
            get
            {
                var ret = new List<T>();
                
                foreach (int idx in this.listView.SelectedIndices)
                {
                    ret.Add( this._items[idx]);
                }

                return ret;
            }
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public IEnumerable<T> CheckedItems
        {
            get
            {
                var ret = new List<T>();

                // 18.01.09 if文追加
                if (this.listView.CheckBoxes)
                {
                    for (int i = 0; i < this.listView.Items.Count; i++)
                    {
                        if (this.listView.Items[i].Checked)
                        {
                            int idx;

                            if (int.TryParse(this.listView.Items[i].Name, out idx))
                            {
                                ret.Add(this._items[idx]);
                            }
                        }
                    }
                }

                return ret;
            }

            set
            {
                if (!this.listView.CheckBoxes) return;


                for (int i = 0; i < this._items.Count; i++)
                {
                    var listItem = this.listView.Items[i.ToString()];

                    if(listItem != null)
                    {
                        int idx = value.ToList().FindIndex(x => x.Equals(this._items[i]));

                        listItem.Checked = idx != -1;
                    }
                }
            }

        }


        /// <summary>
        /// 継承先で、列の情報を設定する為のフィールドです。
        /// </summary>
        protected ColumnHeader[] ListViewColumns
        {
            set
            {
                this.listView.Columns.Clear();
                this.listView.Columns.AddRange(value);
            }
        }


        public bool MultiSelect
        {
            //protected set { this.listView.MultiSelect = value; }
            // ↓ protected にする理由はないと思うが 18.02.20 
            set { this.listView.MultiSelect = value; }

            get { return this.listView.MultiSelect; }
        }


        protected void rebuildDisplay()
        {
            if (this._items == null) return;

            // レコードを一度クリア
            this.listView.Items.Clear();

            // 行情報のカウンタ。項目のID として用いる
            int rowNumber = 0;

            // 一つずつ足していく
            foreach (var item in this._items)
            {
                // 今回追加される ListView の１アイテム
                var lvItem = new  ListViewItem();

                // 17.12.26 念のため ID のつもりで Name に番号を付ける
                lvItem.Name = rowNumber.ToString();

                // リフレクションを用いて値を代入していく  17.09.28 あまり使用した事がないので、問題が発生するか監視する 
                Type type = item.GetType();

                // ２列目以降は処理が変わる為に用意したカウンタ
                int clmNumber = 0;

                foreach (ColumnHeader clm in this.listView.Columns)
                {
                   System.Reflection.PropertyInfo prop = type.GetProperty(clm.Name);

                    if (prop == null)
                    {
                        /*
                        // ログの記録
                        LogHandler.WriteData(new LogFormat
                        {
                            LogType = LogTypeFlag.Error,
                            ID = "CLT-01",
                            Message = string.Format("指定プロパティなし:{0}", clm.Name)
                        });*/
                        // ↓ ライブラリ内で別ライブラリを参照するのは非常に効率が悪いので例外を投げる 18.02.19
                        throw new System.Reflection.TargetException
                        { 
                           
                        };
                    }
                    else
                    {
                        // 第二引数　index 付きのプロパティの場合にプロパティも返ってくる
                        var val = prop.GetValue(item, null);

                        if (clmNumber == 0)
                        {
                            // ListView では一列目が Text, 二列目以降が SubItem となっているらしい 17.10.02
                            if (val == null)
                            {
                                lvItem.Text = "";
                            }
                            else
                            {
                                lvItem.Text = val.ToString();
                            }
                        }
                        else
                        {
                            
                            if (val == null)
                            {
                                lvItem.SubItems.Add("");
                            }
                            else
                            {
                                lvItem.SubItems.Add(val.ToString());
                            }

                            /*
                            // 後学の為にクラス名をはっきりさせておく 17.12.26
                            var subItem = new ListViewItem.ListViewSubItem();
                            //subItem.Name = clmNumber.ToString();

                            if (val == null)
                                subItem.Text = "";
                            else
                                subItem.Text = val.ToString();
                                */
                        }
                    }

                    // カウントアップ
                    clmNumber++;
                }
                
                this.listView.Items.Add(lvItem);

                // カウントアップ
                rowNumber++;
            }
        }


        protected bool InsertItemToSelectedPoint(T item)
        {
            int id = 0;

            // 選択されているかどうか
            if (this.listView.SelectedIndices.Count < 1)
                id = this._items.Count;
            else
                id = this.listView.SelectedIndices[0];

            this._items.Insert(id, item);

            // 表示の更新
            this.rebuildDisplay();

            return true;
        }

//      protected bool RemoveItem(T item)
        protected bool RemoveSelectedItem()
        {
            // 選択されているかどうか
            if (this.listView.SelectedIndices.Count < 1) return false;


            if (this.MultiSelect)
            {
                // 18.02.20 現在、MultiSelect時は未対応
            }
            else
            {
                int i = this.listView.SelectedIndices[0];

                this._items.RemoveAt(i);
            }


            // 表示更新
            this.rebuildDisplay();

            if (this.listView.Items.Count > 0)
            {
                // 一番上の項目を選択
                listView.Items[0].Selected = true;
                listView.Select();
            }

            return true;
        }


        protected virtual void button_edit_Click(object sender, EventArgs e)
        {
        }
        protected virtual void button_add_Click(object sender, EventArgs e)
        {
        }
        protected virtual void button_delete_Click(object sender, EventArgs e)
        {
            // delete だけは、同じ動作が期待されそうなので、基底クラスで定義しておく

            if (this.listView.SelectedIndices.Count > 0)
            {
                int idx = this.listView.SelectedIndices[0];
                this._items.RemoveAt(idx);

                this.rebuildDisplay();
            }
        }

        // 19.01.10 追加
        protected virtual void 上に移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 同じ動作が期待されそうなので、基底クラスで定義しておく

            if (this.listView.SelectedIndices.Count > 0)
            {
                int idx = this.listView.SelectedIndices[0];

                // 上下限チェック
                if (idx < 1 || this.listView.Items.Count < 2) return;

                // 要素のコピー
                var current = this._items[idx];
                //var target =  (ListViewItem)this.listView.Items[idx - 1].Clone(); 19.01.10 処理の変更により不要になった

                // 入れ替え
                //this.listView.Items[idx - 1] = current;
                //this.listView.Items[idx] = target;
                // ↓　エラー発生の為処理を変える
                // 挿入
                this._items.Insert(idx - 1, current);

                // 削除 
                this._items.RemoveAt(idx + 1) ;

                this.rebuildDisplay();
            }
        }

        // 19.01.10 追加
        protected virtual void 下に移動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 同じ動作が期待されそうなので、基底クラスで定義しておく
            if (this.listView.SelectedIndices.Count > 0)
            {
                int idx = this.listView.SelectedIndices[0];

                // 上下限チェック
                if (idx == -1 ||
                    this.listView.Items.Count < 2 ||
                    idx >= (this.listView.Items.Count - 1)) return;

                // 要素のコピー
                //var current = (ListViewItem)this.listView.Items[idx].Clone(); 19.01.10 処理の変更により不要になった
                var target = this._items[idx +1];

                // 入れ替え
                //this.listView.Items[idx] = target;
                //this.listView.Items[idx + 1] = current;
                // ↓　エラー発生の為処理を変える                
                // 挿入
                this._items.Insert(idx, target);

                // 削除 
                this._items.RemoveAt(idx + 2);

                this.rebuildDisplay();
            }
        }
    }
}
