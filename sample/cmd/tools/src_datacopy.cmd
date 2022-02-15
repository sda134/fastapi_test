::コマンドを非表示
@echo off

:: 文字コードを shift-jis に変更。robocopyなどで日本語を使う場合。
chcp 932

:: 実行パスの取得
echo %~dp0

:: 必要ディレクトリ
set target="\data"

:: フォルダ作成
if exist %~dp0"\data" (
echo あるよ
) else (
echo ないよ
md %~dp0"\data"
)

md %~dp0"\data\doc"


echo python
  :: robocopy  "C:\data\programSource\Python\Tutorial" %target%"\prog\python\Tutorial" /s /xo
  :: ↓変更
  robocopy  "C:\data\programSource\python" %~dp0\"data\prog\python" /s /xo
  robocopy  "C:\data\programSource\git\home\python\pythonmemo" %~dp0\"data\prog\python\memo" /s /xo



echo C#
  robocopy  "C:\data\programSource\vs\Projects\Mitsubishi_MCProtocol" %~dp0\"data\prog\c#\MCProtocol" /s /xo
  robocopy  "C:\data\programSource\vs\Projects\UtilityLibrary" %~dp0\"data\prog\c#\UtilityLibrary" /s /xo
  robocopy  "C:\data\programSource\vs\倉庫\製品バックアップ\ProductDeviceValuesManager\current" %~dp0\"data\prog\c#\ProductDeviceValuesManager" /s /xo


echo html
  robocopy  "C:\data\programSource\html系" %~dp0\"data\prog\html" /s /xo


echo cmd
  robocopy  "C:\data\programSource\cmd" %~dp0\"data\prog\cmd" /s /xo


echo bash
  robocopy  "C:\data\programSource\bash" %~dp0\"data\prog\bash" /s /xo



:: 一時的(19.09.30)
::echo cmd
::  robocopy  "Z:\181031_Win10設定用ファイル" %~dp0\"data\winX" /s /xo


echo Application Data
  robocopy  "C:\_app\自作翻訳補助：TranslationConcierge" %~dp0\"data\app\TranslationConcierge" /s /xo



echo PLC
  robocopy  "C:\data\info\PLC関連" %~dp0\"data\doc\PLC関連" /s /xo

echo ソフトウェア系
  robocopy  "C:\data\info\ソフトウェア系" %~dp0\"data\doc\ソフトウェア系" /s /xo

copy  "C:\data\info\ハードウェア系\電気系情報.docx" %~dp0\"data\doc\電気系情報.docx"
copy  "C:\data\info\ハードウェア系\機械系情報.docx" %~dp0\"data\doc\機械系情報.docx"



::7z に圧縮
  "C:\Program Files\7-Zip\7z.exe" a data.7z data

echo;
echo;
echo - - - - - - - - - - - - - - - - - - - - - - - - 
echo 作業終了しました。一時フォルダを削除して下さい！
echo - - - - - - - - - - - - - - - - - - - - - - - - 
echo;


:FIN
    echo 終了するには何かキーを押して下さい。
　　pause > nul
　　:: 終了コード付きで終わる　
　　exit /b 0