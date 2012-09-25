// 3.4.4.5
using System.Reflection;
using System.Runtime.CompilerServices;
using System.EnterpriseServices;

// COM+ Information
#if !DEBUG
[assembly:ApplicationActivation(ActivationOption.Server)]
[assembly:ApplicationName("Backend")]
[assembly:Description("Backend")]
[assembly:ApplicationAccessControl(false)]
#endif

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle("")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]		

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion("1.0.0.0")]
