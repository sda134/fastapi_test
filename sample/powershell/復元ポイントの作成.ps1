

#function Pause() {
#  Write-Host "続行するには何かキーを押してください..." -NoNewLine
#  [Console]::ReadKey() | Out-Null
#}

#システムの復元をon
Enable-ComputerRestore -Drive C:


#APPLICATION_INSTALL    アプリケーションのインストール
#APPLICATION_UNINSTALL  アプリケーションのアンインストール
#DEVICE_DRIVER_INSTALL  デバイスドライバのインストール
#MODIFY_SETTINGS        システム変更
#CANCELLED_OPERATION    復元ポイント操作の取り消し

Checkpoint-Computer -Description "復元ポイントテスト" -RestorePointType MODIFY_SETTINGS

Pause