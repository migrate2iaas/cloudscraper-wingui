﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.17929
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudScraper.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>m1.medium/M1 Medium</string>
  <string>m1.small/M1 Small</string>
  <string>m1.large/M1 Large</string>
  <string>m1.xlarge/M1 Extra Large</string>
  <string>m3.2xlarge/M3 Double Extra Large</string>
  <string>m3.xlarge/M3 Extra Large</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection ServerTypes {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["ServerTypes"]));
            }
            set {
                this["ServerTypes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>us-east-1/US East (Northern Virginia)</string>
  <string>us-west-2/US West (Oregon)</string>
  <string>us-west-1/US West (Northern California)</string>
  <string>eu-west-1/EU (Ireland)</string>
  <string>ap-southeast-1/Asia Pacific (Singapore)</string>
  <string>ap-southeast-2/Asia Pacific (Sydney)</string>
  <string>ap-northeast-1/Asia Pacific (Tokyo)</string>
  <string>sa-east-1/South America (Sao Paulo)</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection Regions {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Regions"]));
            }
            set {
                this["Regions"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("/")]
        public char Separator {
            get {
                return ((char)(this["Separator"]));
            }
            set {
                this["Separator"] = value;
            }
        }
    }
}
