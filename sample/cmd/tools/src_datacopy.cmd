::�R�}���h���\��
@echo off

:: �����R�[�h�� shift-jis �ɕύX�Brobocopy�Ȃǂœ��{����g���ꍇ�B
chcp 932

:: ���s�p�X�̎擾
echo %~dp0

:: �K�v�f�B���N�g��
set target="\data"

:: �t�H���_�쐬
if exist %~dp0"\data" (
echo �����
) else (
echo �Ȃ���
md %~dp0"\data"
)

md %~dp0"\data\doc"


echo python
  :: robocopy  "C:\data\programSource\Python\Tutorial" %target%"\prog\python\Tutorial" /s /xo
  :: ���ύX
  robocopy  "C:\data\programSource\python" %~dp0\"data\prog\python" /s /xo
  robocopy  "C:\data\programSource\git\home\python\pythonmemo" %~dp0\"data\prog\python\memo" /s /xo



echo C#
  robocopy  "C:\data\programSource\vs\Projects\Mitsubishi_MCProtocol" %~dp0\"data\prog\c#\MCProtocol" /s /xo
  robocopy  "C:\data\programSource\vs\Projects\UtilityLibrary" %~dp0\"data\prog\c#\UtilityLibrary" /s /xo
  robocopy  "C:\data\programSource\vs\�q��\���i�o�b�N�A�b�v\ProductDeviceValuesManager\current" %~dp0\"data\prog\c#\ProductDeviceValuesManager" /s /xo


echo html
  robocopy  "C:\data\programSource\html�n" %~dp0\"data\prog\html" /s /xo


echo cmd
  robocopy  "C:\data\programSource\cmd" %~dp0\"data\prog\cmd" /s /xo


echo bash
  robocopy  "C:\data\programSource\bash" %~dp0\"data\prog\bash" /s /xo



:: �ꎞ�I(19.09.30)
::echo cmd
::  robocopy  "Z:\181031_Win10�ݒ�p�t�@�C��" %~dp0\"data\winX" /s /xo


echo Application Data
  robocopy  "C:\_app\����|��⏕�FTranslationConcierge" %~dp0\"data\app\TranslationConcierge" /s /xo



echo PLC
  robocopy  "C:\data\info\PLC�֘A" %~dp0\"data\doc\PLC�֘A" /s /xo

echo �\�t�g�E�F�A�n
  robocopy  "C:\data\info\�\�t�g�E�F�A�n" %~dp0\"data\doc\�\�t�g�E�F�A�n" /s /xo

copy  "C:\data\info\�n�[�h�E�F�A�n\�d�C�n���.docx" %~dp0\"data\doc\�d�C�n���.docx"
copy  "C:\data\info\�n�[�h�E�F�A�n\�@�B�n���.docx" %~dp0\"data\doc\�@�B�n���.docx"



::7z �Ɉ��k
  "C:\Program Files\7-Zip\7z.exe" a data.7z data

echo;
echo;
echo - - - - - - - - - - - - - - - - - - - - - - - - 
echo ��ƏI�����܂����B�ꎞ�t�H���_���폜���ĉ������I
echo - - - - - - - - - - - - - - - - - - - - - - - - 
echo;


:FIN
    echo �I������ɂ͉����L�[�������ĉ������B
�@�@pause > nul
�@�@:: �I���R�[�h�t���ŏI���@
�@�@exit /b 0