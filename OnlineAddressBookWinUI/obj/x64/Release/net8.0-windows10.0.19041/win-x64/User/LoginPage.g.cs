﻿#pragma checksum "E:\Ananth\repos\OnlineAddressBookWinUI\OnlineAddressBookWinUI\User\LoginPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C747E0DDE2599F4CA55BE82753ADEB0985E8283D746582BFB68011D760B3E1FD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineAddressBookWinUI.User
{
    partial class LoginPage : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // User\LoginPage.xaml line 39
                {
                    this.submitBtn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.submitBtn).Click += this.Login;
                }
                break;
            case 3: // User\LoginPage.xaml line 49
                {
                    this.alert = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 4: // User\LoginPage.xaml line 44
                {
                    global::Microsoft.UI.Xaml.Documents.Hyperlink element4 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Documents.Hyperlink>(target);
                    ((global::Microsoft.UI.Xaml.Documents.Hyperlink)element4).Click += this.GoToSignup;
                }
                break;
            case 5: // User\LoginPage.xaml line 36
                {
                    this.passwordInput = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.PasswordBox>(target);
                }
                break;
            case 6: // User\LoginPage.xaml line 31
                {
                    this.emailInput = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }


        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2411")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
