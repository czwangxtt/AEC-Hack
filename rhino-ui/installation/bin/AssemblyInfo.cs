using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Rhino.PlugIns;

[assembly: PlugInDescription(DescriptionType.Address,      "")]
[assembly: PlugInDescription(DescriptionType.Country,      "")]
[assembly: PlugInDescription(DescriptionType.Email,        "zsenarchitect@gmail.com")]
[assembly: PlugInDescription(DescriptionType.Phone,        "")]
[assembly: PlugInDescription(DescriptionType.Organization, "Sen Zhang")]
[assembly: PlugInDescription(DescriptionType.UpdateUrl,    "")]
[assembly: PlugInDescription(DescriptionType.WebSite,      "")]

[assembly: AssemblyTitle("AECedamy")]
[assembly: AssemblyDescription("no description")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Sen Zhang")]
[assembly: AssemblyProduct("AECedamy")]
[assembly: AssemblyCopyright("Copyright © Sen Zhang 2023")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]
[assembly: Guid("34977b5b-52d6-0163-c5a6-4ed73af6d1a8")]
[assembly: AssemblyVersion("0.1.8708.28286")]
[assembly: AssemblyFileVersion("0.1.8708.28286")]
[assembly: AssemblyInformationalVersion("0.1.0")]

public class CompilerPlugin : PlugIn 
{ 
  private static bool librariesLoaded = false;
  internal static void LoadLibraries()
  {
    if (librariesLoaded)
      return;
    librariesLoaded = true;

    
  }

  protected override LoadReturnCode OnLoad(ref string errorMessage)
  {
    var result = base.OnLoad(ref errorMessage);
    string message = "";
    if (!string.IsNullOrWhiteSpace(message))
    {
      Rhino.RhinoApp.WriteLine(message);
    }
    return result;
  }
}
