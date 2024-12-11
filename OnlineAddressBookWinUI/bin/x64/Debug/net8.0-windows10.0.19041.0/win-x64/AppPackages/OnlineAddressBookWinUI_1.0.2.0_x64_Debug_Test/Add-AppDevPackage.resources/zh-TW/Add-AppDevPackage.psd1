# Localized	12/03/2020 06:20 PM (GMT)	303:4.80.0411 	Add-AppDevPackage.psd1
# Culture = "en-US"
ConvertFrom-StringData @'
###PSLOC
PromptYesString=是(&Y)
PromptNoString=否(&N)
BundleFound=找到下列資源存放區: {0}
PackageFound=找到封裝: {0}
EncryptedBundleFound=找到加密的套件組合: {0}
EncryptedPackageFound=找到加密的套件: {0}
CertificateFound=找到憑證: {0}
DependenciesFound=找到相依性封裝: 
GettingDeveloperLicense=正在取得開發人員授權...
InstallingCertificate=正在安裝憑證...
InstallingPackage=\n正在安裝應用程式...
AcquireLicenseSuccessful=已成功取得開發人員授權。
InstallCertificateSuccessful=已成功安裝憑證。
Success=\n成功: 已成功安裝您的應用程式。
WarningInstallCert=\n您即將安裝數位憑證到您電腦的「受信任的人」憑證存放區。這會帶來嚴重的安全性風險，請務必在您信任這個數位憑證的建立者的情況下才執行這個動作。\n\n使用完這個應用程式之後，您應該手動移除相關的數位憑證。有關執行這個動作的指示，請參閱: http://go.microsoft.com/fwlink/?LinkId=243053\n\n您確定要繼續嗎?\n\n
ElevateActions=\n安裝這個應用程式之前，必須執行下列動作: 
ElevateActionDevLicense=\t- 取得開發人員授權
ElevateActionCertificate=\t- 安裝簽署憑證
ElevateActionsContinue=必須有系統管理員認證才能繼續。請接受 UAC 提示，並在系統要求時提供您的系統管理員密碼。
ErrorForceElevate=您必須提供系統管理員認證才能繼續。請在不含 -Force 參數的情況下執行這個指令碼，或從更高權限的 PowerShell 視窗執行。
ErrorForceDeveloperLicense=必須透過使用者互動才能取得開發人員授權。請在不含 -Force 參數的情況下執行指令碼。
ErrorLaunchAdminFailed=錯誤: 無法以系統管理員身分啟動新處理序。
ErrorNoScriptPath=錯誤: 您必須從檔案啟動這個指令碼。
ErrorNoPackageFound=錯誤:  在指令碼目錄中找不到任何封裝或資源存放區。請確定您要安裝的封裝或資源存放區是放在與這個指令碼相同的目錄中。
ErrorManyPackagesFound=錯誤:  在指令碼目錄中找到多個封裝或資源存放區。請確定您只將要安裝的封裝或資源存放區放在與這個指令碼相同的目錄中。
ErrorPackageUnsigned=錯誤: 封裝或資源存放區未經數位簽署，或其簽章已損毀。
ErrorNoCertificateFound=錯誤:  在指令碼目錄中找不到任何憑證。請確定用來簽署要安裝之封裝或資源存放區的憑證是放在與這個指令碼相同的目錄中。
ErrorManyCertificatesFound=錯誤:  在指令碼目錄中找到多個憑證。請確定您只將用來簽署要安裝之封裝或資源存放區的憑證放在與這個指令碼相同的目錄中。
ErrorBadCertificate=錯誤:  檔案 "{0}" 不是有效的數位憑證。CertUtil 傳回錯誤碼 {1}。
ErrorExpiredCertificate=錯誤: 開發人員憑證 "{0}" 已過期。一個可能的原因是系統時鐘未設為正確的日期和時間。如果系統設定正確，請連絡應用程式擁有者，用有效的憑證重新建立封裝或資源存放區。
ErrorCertificateMismatch=錯誤: 這個憑證與用來簽署封裝或資源存放區的憑證不符。
ErrorCertIsCA=錯誤: 憑證不可以是憑證授權單位。
ErrorBannedKeyUsage=錯誤:  憑證不可以有下列金鑰使用方法:  {0}。金鑰使用方法必須是未指定或等於 "DigitalSignature"。
ErrorBannedEKU=錯誤:  憑證不可以有下列擴充金鑰使用方法:  {0}。只允許「程式碼簽署」和「永久簽署」EKU。
ErrorNoBasicConstraints=錯誤: 憑證遺漏基本限制延伸。
ErrorNoCodeSigningEku=錯誤: 憑證遺漏程式碼簽署的擴充金鑰使用方法。
ErrorInstallCertificateCancelled=錯誤: 已取消安裝憑證。
ErrorCertUtilInstallFailed=錯誤:  無法安裝憑證。CertUtil 傳回錯誤碼 {0}。
ErrorGetDeveloperLicenseFailed=錯誤: 無法取得開發人員授權。如需詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkID=252740。
ErrorInstallCertificateFailed=錯誤: 無法安裝憑證。狀態: {0}。如需詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkID=252740。
ErrorAddPackageFailed=錯誤: 無法安裝應用程式。
ErrorAddPackageFailedWithCert=錯誤: 無法安裝應用程式。為了確保安全性，請考慮解除安裝簽署憑證，直到您可以安裝應用程式為止。如需執行此動作的指示，請參閱:\nhttp://go.microsoft.com/fwlink/?LinkId=243053
'@

# SIG # Begin signature block
# MIIoRQYJKoZIhvcNAQcCoIIoNjCCKDICAQExDzANBglghkgBZQMEAgEFADB5Bgor
# BgEEAYI3AgEEoGswaTA0BgorBgEEAYI3AgEeMCYCAwEAAAQQH8w7YFlLCE63JNLG
# KX7zUQIBAAIBAAIBAAIBAAIBADAxMA0GCWCGSAFlAwQCAQUABCB9lj2s83Qdy5+s
# oe+qf03h1ejQuaOJnvPyJrYyQB920KCCDXYwggX0MIID3KADAgECAhMzAAAEBGx0
# Bv9XKydyAAAAAAQEMA0GCSqGSIb3DQEBCwUAMH4xCzAJBgNVBAYTAlVTMRMwEQYD
# VQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNy
# b3NvZnQgQ29ycG9yYXRpb24xKDAmBgNVBAMTH01pY3Jvc29mdCBDb2RlIFNpZ25p
# bmcgUENBIDIwMTEwHhcNMjQwOTEyMjAxMTE0WhcNMjUwOTExMjAxMTE0WjB0MQsw
# CQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9u
# ZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMR4wHAYDVQQDExVNaWNy
# b3NvZnQgQ29ycG9yYXRpb24wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIB
# AQC0KDfaY50MDqsEGdlIzDHBd6CqIMRQWW9Af1LHDDTuFjfDsvna0nEuDSYJmNyz
# NB10jpbg0lhvkT1AzfX2TLITSXwS8D+mBzGCWMM/wTpciWBV/pbjSazbzoKvRrNo
# DV/u9omOM2Eawyo5JJJdNkM2d8qzkQ0bRuRd4HarmGunSouyb9NY7egWN5E5lUc3
# a2AROzAdHdYpObpCOdeAY2P5XqtJkk79aROpzw16wCjdSn8qMzCBzR7rvH2WVkvF
# HLIxZQET1yhPb6lRmpgBQNnzidHV2Ocxjc8wNiIDzgbDkmlx54QPfw7RwQi8p1fy
# 4byhBrTjv568x8NGv3gwb0RbAgMBAAGjggFzMIIBbzAfBgNVHSUEGDAWBgorBgEE
# AYI3TAgBBggrBgEFBQcDAzAdBgNVHQ4EFgQU8huhNbETDU+ZWllL4DNMPCijEU4w
# RQYDVR0RBD4wPKQ6MDgxHjAcBgNVBAsTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEW
# MBQGA1UEBRMNMjMwMDEyKzUwMjkyMzAfBgNVHSMEGDAWgBRIbmTlUAXTgqoXNzci
# tW2oynUClTBUBgNVHR8ETTBLMEmgR6BFhkNodHRwOi8vd3d3Lm1pY3Jvc29mdC5j
# b20vcGtpb3BzL2NybC9NaWNDb2RTaWdQQ0EyMDExXzIwMTEtMDctMDguY3JsMGEG
# CCsGAQUFBwEBBFUwUzBRBggrBgEFBQcwAoZFaHR0cDovL3d3dy5taWNyb3NvZnQu
# Y29tL3BraW9wcy9jZXJ0cy9NaWNDb2RTaWdQQ0EyMDExXzIwMTEtMDctMDguY3J0
# MAwGA1UdEwEB/wQCMAAwDQYJKoZIhvcNAQELBQADggIBAIjmD9IpQVvfB1QehvpC
# Ge7QeTQkKQ7j3bmDMjwSqFL4ri6ae9IFTdpywn5smmtSIyKYDn3/nHtaEn0X1NBj
# L5oP0BjAy1sqxD+uy35B+V8wv5GrxhMDJP8l2QjLtH/UglSTIhLqyt8bUAqVfyfp
# h4COMRvwwjTvChtCnUXXACuCXYHWalOoc0OU2oGN+mPJIJJxaNQc1sjBsMbGIWv3
# cmgSHkCEmrMv7yaidpePt6V+yPMik+eXw3IfZ5eNOiNgL1rZzgSJfTnvUqiaEQ0X
# dG1HbkDv9fv6CTq6m4Ty3IzLiwGSXYxRIXTxT4TYs5VxHy2uFjFXWVSL0J2ARTYL
# E4Oyl1wXDF1PX4bxg1yDMfKPHcE1Ijic5lx1KdK1SkaEJdto4hd++05J9Bf9TAmi
# u6EK6C9Oe5vRadroJCK26uCUI4zIjL/qG7mswW+qT0CW0gnR9JHkXCWNbo8ccMk1
# sJatmRoSAifbgzaYbUz8+lv+IXy5GFuAmLnNbGjacB3IMGpa+lbFgih57/fIhamq
# 5VhxgaEmn/UjWyr+cPiAFWuTVIpfsOjbEAww75wURNM1Imp9NJKye1O24EspEHmb
# DmqCUcq7NqkOKIG4PVm3hDDED/WQpzJDkvu4FrIbvyTGVU01vKsg4UfcdiZ0fQ+/
# V0hf8yrtq9CkB8iIuk5bBxuPMIIHejCCBWKgAwIBAgIKYQ6Q0gAAAAAAAzANBgkq
# hkiG9w0BAQsFADCBiDELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24x
# EDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlv
# bjEyMDAGA1UEAxMpTWljcm9zb2Z0IFJvb3QgQ2VydGlmaWNhdGUgQXV0aG9yaXR5
# IDIwMTEwHhcNMTEwNzA4MjA1OTA5WhcNMjYwNzA4MjEwOTA5WjB+MQswCQYDVQQG
# EwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwG
# A1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSgwJgYDVQQDEx9NaWNyb3NvZnQg
# Q29kZSBTaWduaW5nIFBDQSAyMDExMIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIIC
# CgKCAgEAq/D6chAcLq3YbqqCEE00uvK2WCGfQhsqa+laUKq4BjgaBEm6f8MMHt03
# a8YS2AvwOMKZBrDIOdUBFDFC04kNeWSHfpRgJGyvnkmc6Whe0t+bU7IKLMOv2akr
# rnoJr9eWWcpgGgXpZnboMlImEi/nqwhQz7NEt13YxC4Ddato88tt8zpcoRb0Rrrg
# OGSsbmQ1eKagYw8t00CT+OPeBw3VXHmlSSnnDb6gE3e+lD3v++MrWhAfTVYoonpy
# 4BI6t0le2O3tQ5GD2Xuye4Yb2T6xjF3oiU+EGvKhL1nkkDstrjNYxbc+/jLTswM9
# sbKvkjh+0p2ALPVOVpEhNSXDOW5kf1O6nA+tGSOEy/S6A4aN91/w0FK/jJSHvMAh
# dCVfGCi2zCcoOCWYOUo2z3yxkq4cI6epZuxhH2rhKEmdX4jiJV3TIUs+UsS1Vz8k
# A/DRelsv1SPjcF0PUUZ3s/gA4bysAoJf28AVs70b1FVL5zmhD+kjSbwYuER8ReTB
# w3J64HLnJN+/RpnF78IcV9uDjexNSTCnq47f7Fufr/zdsGbiwZeBe+3W7UvnSSmn
# Eyimp31ngOaKYnhfsi+E11ecXL93KCjx7W3DKI8sj0A3T8HhhUSJxAlMxdSlQy90
# lfdu+HggWCwTXWCVmj5PM4TasIgX3p5O9JawvEagbJjS4NaIjAsCAwEAAaOCAe0w
# ggHpMBAGCSsGAQQBgjcVAQQDAgEAMB0GA1UdDgQWBBRIbmTlUAXTgqoXNzcitW2o
# ynUClTAZBgkrBgEEAYI3FAIEDB4KAFMAdQBiAEMAQTALBgNVHQ8EBAMCAYYwDwYD
# VR0TAQH/BAUwAwEB/zAfBgNVHSMEGDAWgBRyLToCMZBDuRQFTuHqp8cx0SOJNDBa
# BgNVHR8EUzBRME+gTaBLhklodHRwOi8vY3JsLm1pY3Jvc29mdC5jb20vcGtpL2Ny
# bC9wcm9kdWN0cy9NaWNSb29DZXJBdXQyMDExXzIwMTFfMDNfMjIuY3JsMF4GCCsG
# AQUFBwEBBFIwUDBOBggrBgEFBQcwAoZCaHR0cDovL3d3dy5taWNyb3NvZnQuY29t
# L3BraS9jZXJ0cy9NaWNSb29DZXJBdXQyMDExXzIwMTFfMDNfMjIuY3J0MIGfBgNV
# HSAEgZcwgZQwgZEGCSsGAQQBgjcuAzCBgzA/BggrBgEFBQcCARYzaHR0cDovL3d3
# dy5taWNyb3NvZnQuY29tL3BraW9wcy9kb2NzL3ByaW1hcnljcHMuaHRtMEAGCCsG
# AQUFBwICMDQeMiAdAEwAZQBnAGEAbABfAHAAbwBsAGkAYwB5AF8AcwB0AGEAdABl
# AG0AZQBuAHQALiAdMA0GCSqGSIb3DQEBCwUAA4ICAQBn8oalmOBUeRou09h0ZyKb
# C5YR4WOSmUKWfdJ5DJDBZV8uLD74w3LRbYP+vj/oCso7v0epo/Np22O/IjWll11l
# hJB9i0ZQVdgMknzSGksc8zxCi1LQsP1r4z4HLimb5j0bpdS1HXeUOeLpZMlEPXh6
# I/MTfaaQdION9MsmAkYqwooQu6SpBQyb7Wj6aC6VoCo/KmtYSWMfCWluWpiW5IP0
# wI/zRive/DvQvTXvbiWu5a8n7dDd8w6vmSiXmE0OPQvyCInWH8MyGOLwxS3OW560
# STkKxgrCxq2u5bLZ2xWIUUVYODJxJxp/sfQn+N4sOiBpmLJZiWhub6e3dMNABQam
# ASooPoI/E01mC8CzTfXhj38cbxV9Rad25UAqZaPDXVJihsMdYzaXht/a8/jyFqGa
# J+HNpZfQ7l1jQeNbB5yHPgZ3BtEGsXUfFL5hYbXw3MYbBL7fQccOKO7eZS/sl/ah
# XJbYANahRr1Z85elCUtIEJmAH9AAKcWxm6U/RXceNcbSoqKfenoi+kiVH6v7RyOA
# 9Z74v2u3S5fi63V4GuzqN5l5GEv/1rMjaHXmr/r8i+sLgOppO6/8MO0ETI7f33Vt
# Y5E90Z1WTk+/gFcioXgRMiF670EKsT/7qMykXcGhiJtXcVZOSEXAQsmbdlsKgEhr
# /Xmfwb1tbWrJUnMTDXpQzTGCGiUwghohAgEBMIGVMH4xCzAJBgNVBAYTAlVTMRMw
# EQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVN
# aWNyb3NvZnQgQ29ycG9yYXRpb24xKDAmBgNVBAMTH01pY3Jvc29mdCBDb2RlIFNp
# Z25pbmcgUENBIDIwMTECEzMAAAQEbHQG/1crJ3IAAAAABAQwDQYJYIZIAWUDBAIB
# BQCgga4wGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQwHAYKKwYBBAGCNwIBCzEO
# MAwGCisGAQQBgjcCARUwLwYJKoZIhvcNAQkEMSIEICA+SYMiNtESLrz/p/p1+z/I
# VIPCJnxHk0YWYF2XHrSnMEIGCisGAQQBgjcCAQwxNDAyoBSAEgBNAGkAYwByAG8A
# cwBvAGYAdKEagBhodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20wDQYJKoZIhvcNAQEB
# BQAEggEAYxbR1mG7/jzUwn0871sE4A4OJy/SDlDccPvNZe0c3cMUAJtPBb/HQ1Xj
# 2P59X+spQ7Bl9GMYmh7MhM238tVmGtrMF88N0jVPyKHgBIJ7nyPz6/4O4Pu6cnRz
# YKUCWJZtH69ceOk/wVKAAM2gViL3wufKbCt4jkPhmE5nvUAe7h14BFk66AHJqjx2
# 2reD5Q0e7POEObaoz66Dufe8+i9VVsVZsfRpMHbzqOzZaRl708iBT9jlOfUNWHah
# j7Ihh1HjzYWXpG5lbi638658VYZKTXm5/p/Oeov693l7fcFDJ7IBrTzfKU2Tnj0i
# IHHnQH+0Fo2kulweKLe+2SBoXA2cTKGCF68wgherBgorBgEEAYI3AwMBMYIXmzCC
# F5cGCSqGSIb3DQEHAqCCF4gwgheEAgEDMQ8wDQYJYIZIAWUDBAIBBQAwggFZBgsq
# hkiG9w0BCRABBKCCAUgEggFEMIIBQAIBAQYKKwYBBAGEWQoDATAxMA0GCWCGSAFl
# AwQCAQUABCAGw78+vZ3mn5n7dr1sbY2bO8TMAbcpHZLuNdw3nNkpvAIGZutafKTl
# GBIyMDI0MTEwNjA2MDI1My4yOFowBIACAfSggdmkgdYwgdMxCzAJBgNVBAYTAlVT
# MRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQK
# ExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xLTArBgNVBAsTJE1pY3Jvc29mdCBJcmVs
# YW5kIE9wZXJhdGlvbnMgTGltaXRlZDEnMCUGA1UECxMeblNoaWVsZCBUU1MgRVNO
# OjUyMUEtMDVFMC1EOTQ3MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBT
# ZXJ2aWNloIIR/jCCBygwggUQoAMCAQICEzMAAAIAC9eqfxsqF1YAAQAAAgAwDQYJ
# KoZIhvcNAQELBQAwfDELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24x
# EDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlv
# bjEmMCQGA1UEAxMdTWljcm9zb2Z0IFRpbWUtU3RhbXAgUENBIDIwMTAwHhcNMjQw
# NzI1MTgzMTIxWhcNMjUxMDIyMTgzMTIxWjCB0zELMAkGA1UEBhMCVVMxEzARBgNV
# BAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jv
# c29mdCBDb3Jwb3JhdGlvbjEtMCsGA1UECxMkTWljcm9zb2Z0IElyZWxhbmQgT3Bl
# cmF0aW9ucyBMaW1pdGVkMScwJQYDVQQLEx5uU2hpZWxkIFRTUyBFU046NTIxQS0w
# NUUwLUQ5NDcxJTAjBgNVBAMTHE1pY3Jvc29mdCBUaW1lLVN0YW1wIFNlcnZpY2Uw
# ggIiMA0GCSqGSIb3DQEBAQUAA4ICDwAwggIKAoICAQCvVdpp0qQ/ZOS6ehMXnvf+
# 0Xsokz0OiK/dxy/bqaqdSGa/YMzn2KQRPhadF+jluIUgdzouqsh6oKBP19P8aSzl
# Uo73RlBbZq+H88weeXSRl9f8jnN1Wihcdt1RSQ+Jl81ezNazCYv1SVajybPK/0un
# 1MC3D+hc5hMDF1hKo4HlTPPVDcDy4Kk2W6ZA0MkYNjpxKfQyVi6ReSUCsKGrqX4p
# iuXqv9ac6pdKScAGmCBKwnfegveieYOI31hQClnCOc2H0zqQNqd5LPvz9i0P/aka
# nH38tcQuhrMRQXGHKDgp2ahYY1jB1Hv+J3zWB44RHr2Xl0m/vVL+Yf4vFvovr3af
# y4SYBXDp9W8T5zzCOBhluVkI08DKcKcN25Et2TWOzAKqOo1zdf9YjMDsYazgdRLh
# gisBTHwfYD3i8M2IDwBZrtn8dLBMLIiB5VuV1dgzYG3EwqreSd5GhPbs1Dtjufxl
# NoCN7sGV+O7zeykY/9BZg1nXBjNhUZHI6l0DxabGrlXx/mvgdob3M9zKQ7ImlFnL
# 5XdEaKCEWawIlcBwzOI7voeKfAIiMiacIUoYn8hsuMfont8lepE9uD4fqtgtnxcG
# mZcEUfg9NqRlVjEH8/4RBsvks3DRkgz565/EKWNXg76ceQz7OBLHz7TsFVk8EqyW
# ih5uyaEhwE7Tvv2R6FyJgwIDAQABo4IBSTCCAUUwHQYDVR0OBBYEFIAlJNq+CSMq
# qB7nFA5effl9d3c6MB8GA1UdIwQYMBaAFJ+nFV0AXmJdg/Tl0mWnG1M1GelyMF8G
# A1UdHwRYMFYwVKBSoFCGTmh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2lvcHMv
# Y3JsL01pY3Jvc29mdCUyMFRpbWUtU3RhbXAlMjBQQ0ElMjAyMDEwKDEpLmNybDBs
# BggrBgEFBQcBAQRgMF4wXAYIKwYBBQUHMAKGUGh0dHA6Ly93d3cubWljcm9zb2Z0
# LmNvbS9wa2lvcHMvY2VydHMvTWljcm9zb2Z0JTIwVGltZS1TdGFtcCUyMFBDQSUy
# MDIwMTAoMSkuY3J0MAwGA1UdEwEB/wQCMAAwFgYDVR0lAQH/BAwwCgYIKwYBBQUH
# AwgwDgYDVR0PAQH/BAQDAgeAMA0GCSqGSIb3DQEBCwUAA4ICAQAo8Ib2eNG0ipD5
# +4QKDHNYyxA4jce9buxX0+YRYII5aVO4YIjM2LeJt0tlLRvYgMeDTIuu11W4GLcX
# FV16whe7NjKh8h79qVMF1XgOGKMtNe0Hs2A6ejsbXaI7e3qPLWE9Kq7MYuvL/aYR
# kHAixKhLYP4f7ccInE8PKHMwWo/6mWW08AIH9A3Bnur4cbJm/e/x636tBiDywXc9
# O5Z4ODd1H/OTU1rAn918UINiVY14IEIu809AFx4xhcVEUqFxJTCzuYV0gMOFmnGr
# IgoPZYPAXI0gYR7Of6d3iRdG6l40TH55KklfKVEP7V3jmFvo/M4gXsGRw+1G0Vbb
# BeCSMuq1NZaUGS/OXa419gncI9lVoPIwNppeA74foOKuwnggb2KQh33jX6ZYN6OS
# Plpif1A3pE5+j8c0eDW2KbCkWhSK+oAW7qKtZkXDlX7IuvwUtzudsxraUVKLHO73
# rN2cOw8ibPRzpK1tjKEpKUze4NGL1RbJ1IqqcRu0noyT5i7G/OmuS5ZAlhZ+k++6
# D7BOeKjKRXBzTJFVyx3jEzOeedG1kMYxJQSX20xWd5thGyHBkjIlOwGAtmYczurZ
# MUr9f33jhKQirJjbYBy4t7Qaqg18BIIhxm3Ntn/M/iVPb83SkNufZ98DONmSEj5C
# uqv3zeZBlbBl2vdOTUxgSUNOHPYPQzCCB3EwggVZoAMCAQICEzMAAAAVxedrngKb
# SZkAAAAAABUwDQYJKoZIhvcNAQELBQAwgYgxCzAJBgNVBAYTAlVTMRMwEQYDVQQI
# EwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3Nv
# ZnQgQ29ycG9yYXRpb24xMjAwBgNVBAMTKU1pY3Jvc29mdCBSb290IENlcnRpZmlj
# YXRlIEF1dGhvcml0eSAyMDEwMB4XDTIxMDkzMDE4MjIyNVoXDTMwMDkzMDE4MzIy
# NVowfDELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcT
# B1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEmMCQGA1UE
# AxMdTWljcm9zb2Z0IFRpbWUtU3RhbXAgUENBIDIwMTAwggIiMA0GCSqGSIb3DQEB
# AQUAA4ICDwAwggIKAoICAQDk4aZM57RyIQt5osvXJHm9DtWC0/3unAcH0qlsTnXI
# yjVX9gF/bErg4r25PhdgM/9cT8dm95VTcVrifkpa/rg2Z4VGIwy1jRPPdzLAEBjo
# YH1qUoNEt6aORmsHFPPFdvWGUNzBRMhxXFExN6AKOG6N7dcP2CZTfDlhAnrEqv1y
# aa8dq6z2Nr41JmTamDu6GnszrYBbfowQHJ1S/rboYiXcag/PXfT+jlPP1uyFVk3v
# 3byNpOORj7I5LFGc6XBpDco2LXCOMcg1KL3jtIckw+DJj361VI/c+gVVmG1oO5pG
# ve2krnopN6zL64NF50ZuyjLVwIYwXE8s4mKyzbnijYjklqwBSru+cakXW2dg3viS
# kR4dPf0gz3N9QZpGdc3EXzTdEonW/aUgfX782Z5F37ZyL9t9X4C626p+Nuw2TPYr
# bqgSUei/BQOj0XOmTTd0lBw0gg/wEPK3Rxjtp+iZfD9M269ewvPV2HM9Q07BMzlM
# jgK8QmguEOqEUUbi0b1qGFphAXPKZ6Je1yh2AuIzGHLXpyDwwvoSCtdjbwzJNmSL
# W6CmgyFdXzB0kZSU2LlQ+QuJYfM2BjUYhEfb3BvR/bLUHMVr9lxSUV0S2yW6r1AF
# emzFER1y7435UsSFF5PAPBXbGjfHCBUYP3irRbb1Hode2o+eFnJpxq57t7c+auIu
# rQIDAQABo4IB3TCCAdkwEgYJKwYBBAGCNxUBBAUCAwEAATAjBgkrBgEEAYI3FQIE
# FgQUKqdS/mTEmr6CkTxGNSnPEP8vBO4wHQYDVR0OBBYEFJ+nFV0AXmJdg/Tl0mWn
# G1M1GelyMFwGA1UdIARVMFMwUQYMKwYBBAGCN0yDfQEBMEEwPwYIKwYBBQUHAgEW
# M2h0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2lvcHMvRG9jcy9SZXBvc2l0b3J5
# Lmh0bTATBgNVHSUEDDAKBggrBgEFBQcDCDAZBgkrBgEEAYI3FAIEDB4KAFMAdQBi
# AEMAQTALBgNVHQ8EBAMCAYYwDwYDVR0TAQH/BAUwAwEB/zAfBgNVHSMEGDAWgBTV
# 9lbLj+iiXGJo0T2UkFvXzpoYxDBWBgNVHR8ETzBNMEugSaBHhkVodHRwOi8vY3Js
# Lm1pY3Jvc29mdC5jb20vcGtpL2NybC9wcm9kdWN0cy9NaWNSb29DZXJBdXRfMjAx
# MC0wNi0yMy5jcmwwWgYIKwYBBQUHAQEETjBMMEoGCCsGAQUFBzAChj5odHRwOi8v
# d3d3Lm1pY3Jvc29mdC5jb20vcGtpL2NlcnRzL01pY1Jvb0NlckF1dF8yMDEwLTA2
# LTIzLmNydDANBgkqhkiG9w0BAQsFAAOCAgEAnVV9/Cqt4SwfZwExJFvhnnJL/Klv
# 6lwUtj5OR2R4sQaTlz0xM7U518JxNj/aZGx80HU5bbsPMeTCj/ts0aGUGCLu6WZn
# OlNN3Zi6th542DYunKmCVgADsAW+iehp4LoJ7nvfam++Kctu2D9IdQHZGN5tggz1
# bSNU5HhTdSRXud2f8449xvNo32X2pFaq95W2KFUn0CS9QKC/GbYSEhFdPSfgQJY4
# rPf5KYnDvBewVIVCs/wMnosZiefwC2qBwoEZQhlSdYo2wh3DYXMuLGt7bj8sCXgU
# 6ZGyqVvfSaN0DLzskYDSPeZKPmY7T7uG+jIa2Zb0j/aRAfbOxnT99kxybxCrdTDF
# NLB62FD+CljdQDzHVG2dY3RILLFORy3BFARxv2T5JL5zbcqOCb2zAVdJVGTZc9d/
# HltEAY5aGZFrDZ+kKNxnGSgkujhLmm77IVRrakURR6nxt67I6IleT53S0Ex2tVdU
# CbFpAUR+fKFhbHP+CrvsQWY9af3LwUFJfn6Tvsv4O+S3Fb+0zj6lMVGEvL8CwYKi
# excdFYmNcP7ntdAoGokLjzbaukz5m/8K6TT4JDVnK+ANuOaMmdbhIurwJ0I9JZTm
# dHRbatGePu1+oDEzfbzL6Xu/OHBE0ZDxyKs6ijoIYn/ZcGNTTY3ugm2lBRDBcQZq
# ELQdVTNYs6FwZvKhggNZMIICQQIBATCCAQGhgdmkgdYwgdMxCzAJBgNVBAYTAlVT
# MRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQK
# ExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xLTArBgNVBAsTJE1pY3Jvc29mdCBJcmVs
# YW5kIE9wZXJhdGlvbnMgTGltaXRlZDEnMCUGA1UECxMeblNoaWVsZCBUU1MgRVNO
# OjUyMUEtMDVFMC1EOTQ3MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBT
# ZXJ2aWNloiMKAQEwBwYFKw4DAhoDFQCMk58tlveK+KkvexIuVYVsutaOZKCBgzCB
# gKR+MHwxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQH
# EwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xJjAkBgNV
# BAMTHU1pY3Jvc29mdCBUaW1lLVN0YW1wIFBDQSAyMDEwMA0GCSqGSIb3DQEBCwUA
# AgUA6tUdxTAiGA8yMDI0MTEwNTIyNDIxM1oYDzIwMjQxMTA2MjI0MjEzWjB3MD0G
# CisGAQQBhFkKBAExLzAtMAoCBQDq1R3FAgEAMAoCAQACAi3sAgH/MAcCAQACAhNx
# MAoCBQDq1m9FAgEAMDYGCisGAQQBhFkKBAIxKDAmMAwGCisGAQQBhFkKAwKgCjAI
# AgEAAgMHoSChCjAIAgEAAgMBhqAwDQYJKoZIhvcNAQELBQADggEBAFFL5byqBXVL
# bbXO+Di9SO19Pns/sXg6p36a1fcvuNwtUNjziGFpky4RXNe7k2QOid/DDHgcnx8h
# gEV/bVXWWmcc8GkGjQ3f5VP0uCdtPgFOtlGsPZlfxcaDZvVFR9vfz1q9ziDyj/l6
# xx3CAU6Hz7nqfjw/tBbCo7p5LBTE9bI72hQ4H/B97NpICKfa8kaDDCRAmJ7ynA49
# vrwWZ5K3jLLwOBRu2tOrI1EMOIZ5VnyLwpRn00R3nu2/4LqLKlFNOKZeubsQw8iR
# Yzq2wbftz8onKHLVW0/zhw6j3yoN5ry7M+KktszS95HKabiZfbwUH/HBQoU12o0O
# sbqU/pNjlG4xggQNMIIECQIBATCBkzB8MQswCQYDVQQGEwJVUzETMBEGA1UECBMK
# V2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0
# IENvcnBvcmF0aW9uMSYwJAYDVQQDEx1NaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0Eg
# MjAxMAITMwAAAgAL16p/GyoXVgABAAACADANBglghkgBZQMEAgEFAKCCAUowGgYJ
# KoZIhvcNAQkDMQ0GCyqGSIb3DQEJEAEEMC8GCSqGSIb3DQEJBDEiBCCcoF/1V+WH
# WLzX8WmPE9YwFYjNc3lqFKb8znUUwWxZ9TCB+gYLKoZIhvcNAQkQAi8xgeowgecw
# geQwgb0EINTI7ew1ndu6sE0MZQJXg18zaAfhpa5G50iT/0oCT9knMIGYMIGApH4w
# fDELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1Jl
# ZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEmMCQGA1UEAxMd
# TWljcm9zb2Z0IFRpbWUtU3RhbXAgUENBIDIwMTACEzMAAAIAC9eqfxsqF1YAAQAA
# AgAwIgQgVYpdhhfBRzCk0cFt0T43DONiSxORkIqB3KYA3mdDPDMwDQYJKoZIhvcN
# AQELBQAEggIAi1PukIOMVZPXOwlCa+oHlN3uqmSwJjXFBMywS5X0WLlIWOCZN+kN
# zhF5NP1CAMVi7u9W0kck8fSdAsAI42oSBtl3qNnMDPmfcguxRk+w4VOtB837gykr
# crVMGks0ks/Z0b+SCaS1ftJg0cJthhj8379RH542h7NExXYE57m0/yC4eSfP4DfC
# WbsptSIGV94PXBvij5y+x718vSHKr98E/f6nnRVgpPgC0oxeCZgMnOH5ZWTwR1nM
# PNeox5EjBNAAN9yg9b0gAJ3AKFcVRAGkNlGyo64Wa4LzuRi/dlJsRzW7jrInkUFc
# S2IIawzRLyfICP6nPzWjsJ3ho1wD9SPM4GexSecby0y+1lJ625aB53r9r13mQ1Va
# wI15gM+oI5gnqrza1p8PrUvvxH5V4zMs1DdBrdQenlL8BdnJatKqSlWKTzxA5qcT
# ax4QYOjINDDG6tcgzDBb9U+ysSYoCSpJoeyjhQIh0BsiCWBdgRH50Z1MWm2cg8+8
# 6VPPV/yZ1F304hwGnCs7BC2Dk2c5WOAPwc1LJuxJ+6LoR3aRMd1PS/rwD2DYb2r2
# xiw63xPh5ZpXtohjOBjcJOZluMzQhyy/VbzBgidB3GObhqRHjNIp+9x4BMk65nx7
# MTXP7gr8JPTA4EFX77YYteEc/FFUup3dpCKVt/U9M36HL1sUT71d3cQ=
# SIG # End signature block
