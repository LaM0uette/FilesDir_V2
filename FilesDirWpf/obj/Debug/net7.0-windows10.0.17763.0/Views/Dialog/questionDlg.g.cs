﻿#pragma checksum "..\..\..\..\..\Views\Dialog\questionDlg.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3696CB0D1D3F977F076553D9D86C3B1EB21CBE43"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace FilesDirWpf.Views.Dialog {
    
    
    /// <summary>
    /// QuestionDlg
    /// </summary>
    public partial class QuestionDlg : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 58 "..\..\..\..\..\Views\Dialog\questionDlg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LblMsg;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\Views\Dialog\questionDlg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonValider;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\Views\Dialog\questionDlg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAnnuler;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FilesDirWpf;component/views/dialog/questiondlg.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Dialog\questionDlg.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LblMsg = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.ButtonValider = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\..\..\Views\Dialog\questionDlg.xaml"
            this.ButtonValider.Click += new System.Windows.RoutedEventHandler(this.ButtonOk_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonAnnuler = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\..\..\..\Views\Dialog\questionDlg.xaml"
            this.ButtonAnnuler.Click += new System.Windows.RoutedEventHandler(this.ButtonNok_OnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

