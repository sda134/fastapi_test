

#function Pause() {
#  Write-Host "���s����ɂ͉����L�[�������Ă�������..." -NoNewLine
#  [Console]::ReadKey() | Out-Null
#}

#�V�X�e���̕�����on
Enable-ComputerRestore -Drive C:


#APPLICATION_INSTALL    �A�v���P�[�V�����̃C���X�g�[��
#APPLICATION_UNINSTALL  �A�v���P�[�V�����̃A���C���X�g�[��
#DEVICE_DRIVER_INSTALL  �f�o�C�X�h���C�o�̃C���X�g�[��
#MODIFY_SETTINGS        �V�X�e���ύX
#CANCELLED_OPERATION    �����|�C���g����̎�����

Checkpoint-Computer -Description "�����|�C���g�e�X�g" -RestorePointType MODIFY_SETTINGS

Pause