﻿#pragma checksum "..\..\..\..\Themes\Dialogs\MessageDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E5EEFDCDC3CE9A6FCF413001EFABEC16E1A99BB48BF47AACFE7CC38FFB6B944B"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls.Dialogs;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MahApps.Metro.Controls.Dialogs {
    
    
    /// <summary>
    /// MessageDialog
    /// </summary>
    public partial class MessageDialog : MahApps.Metro.Controls.Dialogs.BaseMetroDialog, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\Themes\Dialogs\MessageDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer PART_MessageScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Themes\Dialogs\MessageDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PART_MessageTextBlock;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Themes\Dialogs\MessageDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PART_AffirmativeButton;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Themes\Dialogs\MessageDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PART_NegativeButton;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Themes\Dialogs\MessageDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PART_FirstAuxiliaryButton;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Themes\Dialogs\MessageDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PART_SecondAuxiliaryButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MahApps.Metro;component/themes/dialogs/messagedialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Themes\Dialogs\MessageDialog.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 6 "..\..\..\..\Themes\Dialogs\MessageDialog.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.OnKeyCopyExecuted);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PART_MessageScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 3:
            this.PART_MessageTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.PART_AffirmativeButton = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.PART_NegativeButton = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.PART_FirstAuxiliaryButton = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.PART_SecondAuxiliaryButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

