﻿namespace NFeature.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Linq;
    using Enumerable = System.Linq.Enumerable;

    /// <example>
    /// <![CDATA[
    /// <add name="MyFeature"
    ///      state="Enabled"
    ///      supportedTenants="TenantA"
    ///      dependencies="MyOtherFeature" />
    /// ]]>
    /// </example>
    public class FeatureConfigurationElement<TFeatureEnum, TTenantEnum> : ConfigurationElement
        where TFeatureEnum : struct
        where TTenantEnum : struct
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("description", IsRequired = false)]
        public string Description
        {
            get { return (string) this["description"]; }
            set { this["description"] = value; }
        }

        /// <summary>
        ///   NOTE: BA; defaults to Enabled, this enables 
        ///   expected behavior when 'IsRequiredByFeatureSubsystem' 
        ///   features do not have their state explicitly 
        ///   specified (they are always 'Enabled').
        ///   NOTE 1: if not defined, zero is supplied.
        /// </summary>
        [ConfigurationProperty("state", IsRequired = false)]
        public FeatureState State
        {
            get { return (int) this["state"] == 0 ? FeatureState.Enabled : (FeatureState) this["state"]; //see note 1 
            }
            set { this["state"] = Enum.GetName(typeof (FeatureState), value); }
        }

        /// <summary>
        ///   NOTE 1: BA; the user supplied enum type should use 
        ///   zero as the default value.
        /// </summary>
        [TypeConverter(typeof (CommaDelimitedStringCollectionConverter))]
        [ConfigurationProperty("supportedTenants", IsRequired = false)]
        public TTenantEnum[] SupportedTenants
        {
            get
            {
                var tenantNames = ((CommaDelimitedStringCollection) this["supportedTenants"]);

                if (tenantNames != null)
                {
                    return Enumerable.ToArray<TTenantEnum>((tenantNames.Cast<string>().Select(t => (TTenantEnum) Enum.Parse(typeof (TTenantEnum), t))));
                }

                return new[] { (TTenantEnum) Enum.ToObject(typeof(TTenantEnum), 0) }; //see note 1
            }
            set { this["supportedTenants"] = value; }
        }

        [TypeConverter(typeof (CommaDelimitedStringCollectionConverter))]
        [ConfigurationProperty("dependencies", IsRequired = false)]
        public TFeatureEnum[] Dependencies
        {
            get
            {
                var dependencies = ((CommaDelimitedStringCollection) this["dependencies"]) ??
                                   new CommaDelimitedStringCollection();

                return Enumerable.ToArray<TFeatureEnum>((dependencies.Cast<string>().Select(t => (TFeatureEnum)Enum.Parse(typeof(TFeatureEnum), t))));
            }
            set { this["dependencies"] = value; }
        }

        /// <summary>
        ///   Indicates that a feature is required for successful operation of the 
        ///   feature subsystem itself and may therefore use a different codepath to
        ///   retrieve feature settings, avoiding the circular problem of 
        ///   requiring the FeatureManifest to instantiate the FeatureManifest.
        ///   NOTE: BA; defaults to false if not specified.
        /// </summary>
        [TypeConverter(typeof (BooleanConverter))]
        [ConfigurationProperty("isRequiredByFeatureSubsystem", IsRequired = false)]
        public bool IsRequiredByFeatureSubsystem
        {
            get { return (bool) this["isRequiredByFeatureSubsystem"]; }
            set { this["isRequiredByFeatureSubsystem"] = value; }
        }

        [TypeConverter(typeof (StringToEnGBDateTimeConverter))]
        [ConfigurationProperty("startDtg", IsRequired = false)]
        public DateTime StartDtg
        {
            get { return (DateTime) this["startDtg"]; }
            set { this["startDtg"] = value; }
        }

        [TypeConverter(typeof (StringToEnGBDateTimeConverter))]
        [ConfigurationProperty("endDtg", IsRequired = false)]
        public DateTime EndDtg
        {
            get { return (DateTime) this["endDtg"]; }
            set { this["endDtg"] = value; }
        }

        [TypeConverter(typeof (JsonToStringDictionaryConverter))]
        [ConfigurationProperty("settings", IsRequired = false)]
        public Dictionary<string, string> Settings
        {
            get { return (Dictionary<string, string>) this["settings"] ?? new Dictionary<string, string>(); }
        }
    }
}