::�R�}���h���\��
@echo off

:: �����R�[�h�� shift_jis �ɕύX�B�ishift_jis�łȂ��� robocopy�����{��Ή��ł��Ȃ��j
chcp 932

:: �K�v�f�B���N�g��
set dirname=local_backup
set target=%~dp0
set target=%target%%dirname%

echo %target%


:: �t�H���_�쐬
if exist %target% (
echo �Ώۃf�B���N�g���L��B
) else (
echo �Ώۃf�B���N�g�������B�ˍ쐬�B
mkdir %target%
)
echo;



echo ��ƃf�[�^�^�ڋq�ʃf�[�^

:: �e�X�g
::  robocopy  "C:\_app\����f�o�C�X�l�Ǘ��A�v��\ProductsData" %target%"\�f�o�C�X�l�Ǘ�" /s /xo
::  robocopy  "C:\data\info" %target%"\info" /s /xo
::  robocopy  "C:\data\_�ڋq�ʃf�[�^" %target%"\�ڋq�ʃf�[�^" /s /xo
  robocopy  "C:\data\_��ƃf�[�^" %target%"\��ƃf�[�^" /s /xo /xd �ߋ�\2017 /xd �ߋ�\2018


:: test
::  robocopy  "%~dp0\test" "%~dp0%dirname%\test" /s /xo /xf *.jpeg /xf *.jpg /xd fuga

  



::7z �Ɉ��k
::  "C:\Program Files\7-Zip\7z.exe" a %dirname%.7z %target%


:FIN
    echo �I������ɂ͉����L�[�������ĉ������B
�@�@pause > nul
�@�@:: �I���R�[�h�t���ŏI���@
�@�@exit /b 0