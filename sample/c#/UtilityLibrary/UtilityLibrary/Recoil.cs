using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary
{
    // MDP のデバッグファイル作成
    /*
    private void デバッグファイルの作成ToolStripMenuItem_Click(object sender, EventArgs e)
    {

#if DEBUG
        if (this._instance == null) return;

        try
        {

            string dirPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\DebugFile";
            string archivePath =
                System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\debug" + DateTime.Now.ToString("MMdd_HHmmss") + ".zip";

            // 圧縮ファイルを格納するディレクトリを作成
            System.IO.Directory.CreateDirectory(dirPath);

            // ログファイルのパスを取得
            string logPath = Mtec.UtilityLibrary.Data.LogHandler.FileNameFullPath;

            if (System.IO.File.Exists(logPath))
                System.IO.File.Copy(logPath, dirPath + @"\log.csv");

            // ダイアログの現在の状態を画像（ jpg）に保存
            #region

            //コントロールの外観を描画するBitmapの作成
            using (var bmp = new Bitmap(this.Width, this.Height))
            {
                //キャプチャする
                this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));

                //ファイルに保存する
                bmp.Save(dirPath + @"\capture.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                //後始末
                bmp.Dispose();
            }
            #endregion


            // 現在編集中のファイルをコピー
            //bool bRet = Mtec.UtilityLibrary.Data.XMLOperator<TextilePatternDataFormat>.SaveToXmlFile(dirPath + @"\currentFile.xml", this._instance);
            // ↓ 171018_3 ファイル名はそのままとする
            bool bRet = Mtec.UtilityLibrary.Data.XMLSerializeOperator<TextilePatternDataFormat>.SerializeToXmlFile(dirPath + @"\" + this._instance.FilePath_Name, this._instance);

            //ZIP書庫を作成 ※System.IO.Compression.FileSystem.dll を参照に追加する必要あり
            System.IO.Compression.ZipFile.CreateFromDirectory
                (dirPath, archivePath, System.IO.Compression.CompressionLevel.Optimal, false, System.Text.Encoding.GetEncoding("shift_jis"));

            // ディレクトリは削除
            System.IO.Directory.Delete(dirPath, true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

#endif
    } */
}
