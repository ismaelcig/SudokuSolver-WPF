﻿#pragma checksum "..\..\Sudoku.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D421C723FE76702C2E2DF1780FA87685"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using SudokuSolver;
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


namespace SudokuSolver {
    
    
    /// <summary>
    /// Sudoku
    /// </summary>
    public partial class Sudoku : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\Sudoku.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SudokuGrid;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Sudoku.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SudokuSolver.Box box00;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Sudoku.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SudokuSolver.Box box01;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Sudoku.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SudokuSolver.Box box02;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Sudoku.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SudokuSolver.Box box10;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Sudoku.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SudokuSolver.Box box11;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Sudoku.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SudokuSolver.Box box12;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Sudoku.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SudokuSolver.Box box20;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Sudoku.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SudokuSolver.Box box21;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Sudoku.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SudokuSolver.Box box22;
        
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
            System.Uri resourceLocater = new System.Uri("/SudokuSolver;component/sudoku.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Sudoku.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.SudokuGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.box00 = ((SudokuSolver.Box)(target));
            return;
            case 3:
            this.box01 = ((SudokuSolver.Box)(target));
            return;
            case 4:
            this.box02 = ((SudokuSolver.Box)(target));
            return;
            case 5:
            this.box10 = ((SudokuSolver.Box)(target));
            return;
            case 6:
            this.box11 = ((SudokuSolver.Box)(target));
            return;
            case 7:
            this.box12 = ((SudokuSolver.Box)(target));
            return;
            case 8:
            this.box20 = ((SudokuSolver.Box)(target));
            return;
            case 9:
            this.box21 = ((SudokuSolver.Box)(target));
            return;
            case 10:
            this.box22 = ((SudokuSolver.Box)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
