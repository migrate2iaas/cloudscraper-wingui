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
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Cloudscraper Server Copy")]
        public string S1Header {
            get {
                return ((string)(this["S1Header"]));
            }
            set {
                this["S1Header"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Choose volumes to copy...")]
        public string S2Header {
            get {
                return ((string)(this["S2Header"]));
            }
            set {
                this["S2Header"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Choose Your Cloud")]
        public string S3Header {
            get {
                return ((string)(this["S3Header"]));
            }
            set {
                this["S3Header"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Cloud Options")]
        public string S4Header {
            get {
                return ((string)(this["S4Header"]));
            }
            set {
                this["S4Header"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Images Location...")]
        public string S5Header {
            get {
                return ((string)(this["S5Header"]));
            }
            set {
                this["S5Header"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Save transfer task")]
        public string S6Header {
            get {
                return ((string)(this["S6Header"]));
            }
            set {
                this["S6Header"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Transfer progress")]
        public string S7Header {
            get {
                return ((string)(this["S7Header"]));
            }
            set {
                this["S7Header"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Resume your transfer task")]
        public string R2Header {
            get {
                return ((string)(this["R2Header"]));
            }
            set {
                this["R2Header"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ya.ru")]
        public string S1Link {
            get {
                return ((string)(this["S1Link"]));
            }
            set {
                this["S1Link"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ya.ru")]
        public string S2Link {
            get {
                return ((string)(this["S2Link"]));
            }
            set {
                this["S2Link"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ya.ru")]
        public string S3Link {
            get {
                return ((string)(this["S3Link"]));
            }
            set {
                this["S3Link"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ya.ru")]
        public string S4Link {
            get {
                return ((string)(this["S4Link"]));
            }
            set {
                this["S4Link"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ya.ru")]
        public string S5Link {
            get {
                return ((string)(this["S5Link"]));
            }
            set {
                this["S5Link"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ya.ru")]
        public string S6Link {
            get {
                return ((string)(this["S6Link"]));
            }
            set {
                this["S6Link"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ya.ru")]
        public string S7Link {
            get {
                return ((string)(this["S7Link"]));
            }
            set {
                this["S7Link"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ya.ru")]
        public string R2Link {
            get {
                return ((string)(this["R2Link"]));
            }
            set {
                this["R2Link"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ya.ru")]
        public string Setting {
            get {
                return ((string)(this["Setting"]));
            }
            set {
                this["Setting"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("smtp.mail.ru")]
        public string SMTPServer {
            get {
                return ((string)(this["SMTPServer"]));
            }
            set {
                this["SMTPServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("login@mail.ru")]
        public string SMTPLogin {
            get {
                return ((string)(this["SMTPLogin"]));
            }
            set {
                this["SMTPLogin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("password")]
        public string SMTPPassword {
            get {
                return ((string)(this["SMTPPassword"]));
            }
            set {
                this["SMTPPassword"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("cloudscraper@support.assembla.com")]
        public string SupportEmail {
            get {
                return ((string)(this["SupportEmail"]));
            }
            set {
                this["SupportEmail"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("test.txt")]
        public string TextFile {
            get {
                return ((string)(this["TextFile"]));
            }
            set {
                this["TextFile"] = value;
            }
        }
    }
}