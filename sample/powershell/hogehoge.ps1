ls -r "HKLM:\software\microsoft\net framework setup\ndp" | % {
    $_.GetValue("Version") | ? { $_ } | % {
        New-Object PSObject | 
            Add-Member NoteProperty -PassThru -Name "Type" -Value "Version" |
            Add-Member NoteProperty -PassThru -Name "Value" -Value $_
    }
    $_.GetValue("Release") | ? { $_ } | % {
        switch ($_) {
            "378389" { $r = "4.5" }
            "378675" { $r = "4.5.1 installed with Windows 8.1 or Windows Server 2012 R2" }
            "378758" { $r = "4.5.1 installed on Windows 8, Windows 7 SP1, or Windows Vista SP2" }
            "379893" { $r = "4.5.2" }
            "393295" { $r = "4.6 installed on Windows 10" }
            "393297" { $r = "4.6" }
            "394254" { $r = "4.6.1 installed on Windows 10 Novermber Update" }
            "394271" { $r = "4.6.1" }
            "394802" { $r = "4.6.2 installed on Windows 10 Anniversary Update" }
            "394806" { $r = "4.6.2" }
            "460798" { $r = "4.7 installed on Windows 10 Creators Update" }
            "460805" { $r = "4.7"}
            "461308" { $r = "4.7.1 installed on Windows 10 Fall Creators Update"}
            "461310" { $r = "4.7.1"}
            default { $r = "Unknown: $_" }
        }
        New-Object PSObject |
            Add-Member NoteProperty -PassThru -Name "Type" -Value "Release" |
            Add-Member NoteProperty -PassThru -Name "Value" -Value $r
    }
} | Sort -Unique -Property Value
