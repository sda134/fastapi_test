[Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] | Out-Null
[Windows.UI.Notifications.ToastNotification, Windows.UI.Notifications, ContentType = WindowsRuntime] | Out-Null
[Windows.Data.Xml.Dom.XmlDocument, Windows.Data.Xml.Dom.XmlDocument, ContentType = WindowsRuntime] | Out-Null

$app = '{1AC14E77-02E7-4E5D-B744-2EB1AE5198B7}\WindowsPowerShell\v1.0\powershell.exe'

$template = @"
<toast duration="long">
    <visual>
        <binding template="ToastText02">
            <text id="1">test Title</text>
            <text id="2">test content</text>
        </binding>
    </visual>
  <actions>
  <input id="snoozeTime" type="selection" defaultInput="15">
    #5‚Â‚Ü‚Å
    <selection id="3" content="3 minutes" />
    <selection id="5" content="5 minutes" />
    <selection id="10" content="10 minutes" />
    <selection id="15" content="15 minutes" />
    <selection id="30" content="30 minutes" />
  </input>
  <action activationType="system" arguments="snooze" hint-inputId="snoozeTime" content=""/>
  <action activationType="system" arguments="dismiss" content=""/>
  </actions>
</toast>
"@

$xml = New-Object Windows.Data.Xml.Dom.XmlDocument
$xml.LoadXml($template)
$toast = New-Object Windows.UI.Notifications.ToastNotification $xml
[Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier($app).Show($toast )