using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: XmlnsPrefix("http://metro.mahapps.com/winfx/xaml/controls", "mah")]
[assembly: XmlnsPrefix("http://metro.mahapps.com/winfx/xaml/shared", "mah")]

[assembly: XmlnsDefinition("http://metro.mahapps.com/winfx/xaml/shared", "MahApps.Metro.Behaviours")]
[assembly: XmlnsDefinition("http://metro.mahapps.com/winfx/xaml/shared", "MahApps.Metro.Actions")]
[assembly: XmlnsDefinition("http://metro.mahapps.com/winfx/xaml/shared", "MahApps.Metro.Converters")]
[assembly: XmlnsDefinition("http://metro.mahapps.com/winfx/xaml/controls", "MahApps.Metro")]
[assembly: XmlnsDefinition("http://metro.mahapps.com/winfx/xaml/controls", "MahApps.Metro.Controls")]
[assembly: XmlnsDefinition("http://metro.mahapps.com/winfx/xaml/controls", "MahApps.Metro.Controls.Dialogs")]

[assembly: AssemblyTitle("MahApps.Metro")]
[assembly: AssemblyCopyright("Copyright © MahApps.Metro 2011-2017")]
[assembly: AssemblyDescription("A toolkit for creating Metro / Modern UI styled WPF apps.")]
[assembly: AssemblyCompany("MahApps")]

[assembly: AssemblyVersion("1.6.0.0")]
[assembly: AssemblyFileVersion("1.6.0.0")]
[assembly: AssemblyInformationalVersion("1.6.0.0")]
[assembly: AssemblyProduct("MahApps.Metro 1.6.0")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0005:Name of PropertyChangedCallback should match registered name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0006:Name of CoerceValueCallback should match registered name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0007:Name of ValidateValueCallback should match registered name.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0036:Avoid side effects in CLR accessors.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("WpfAnalyzers.DependencyProperty", "WPF0041:Set mutable dependency properties using SetCurrentValue.")]