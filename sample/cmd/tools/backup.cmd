::コマンドを非表示
@echo off

:: 文字コードを shift_jis に変更。（shift_jisでないと robocopyが日本語対応できない）
chcp 932

:: 必要ディレクトリ
set dirname=local_backup
set target=%~dp0
set target=%target%%dirname%

echo %target%


:: フォルダ作成
if exist %target% (
echo 対象ディレクトリ有り。
) else (
echo 対象ディレクトリ無し。⇒作成。
mkdir %target%
)
echo;



echo 作業データ／顧客別データ

:: テスト
::  robocopy  "C:\_app\自作デバイス値管理アプリ\ProductsData" %target%"\デバイス値管理" /s /xo
::  robocopy  "C:\data\info" %target%"\info" /s /xo
::  robocopy  "C:\data\_顧客別データ" %target%"\顧客別データ" /s /xo
  robocopy  "C:\data\_作業データ" %target%"\作業データ" /s /xo /xd 過去\2017 /xd 過去\2018


:: test
::  robocopy  "%~dp0\test" "%~dp0%dirname%\test" /s /xo /xf *.jpeg /xf *.jpg /xd fuga

  



::7z に圧縮
::  "C:\Program Files\7-Zip\7z.exe" a %dirname%.7z %target%


:FIN
    echo 終了するには何かキーを押して下さい。
　　pause > nul
　　:: 終了コード付きで終わる　
　　exit /b 0