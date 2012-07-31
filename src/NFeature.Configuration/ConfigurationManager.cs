// Copyright 2012, Ben Aston (ben@bj.ma).
// 
// This file is part of NFeature.
// 
// NFeature is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NFeature is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with NFeature.  If not, see <http://www.gnu.org/licenses/>.

using System.Linq;

namespace NFeature.Configuration
{
    using System;
    using System.Configuration;

    public class ConfigurationManager<T, TFeatureEnum> : ConfigurationManager<T, TFeatureEnum, DefaultTenantEnum> where T : FeatureConfigurationSection<TFeatureEnum, DefaultTenantEnum>, new() where TFeatureEnum : struct
    {
        protected override bool Compare(ConfigurationSection sec)
        {
            return sec.ElementInformation.Type == typeof(T);
        }
    }

    public class ConfigurationManager<T, TFeatureEnum, TTenantEnum> 
        where T : FeatureConfigurationSection<TFeatureEnum, TTenantEnum>, new()
        where TFeatureEnum : struct
        where TTenantEnum : struct
    {
        public Func<Configuration> GetConfiguration = () => ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public T Section(Func<T> onMissingSection = null)
        {
            var configuration = GetConfiguration();

            
            // find section that matches type
            string sectionName = null;
            foreach (ConfigurationSection sec in configuration.Sections)
            {
                if (Compare(sec))
                {
                    sectionName = sec.SectionInformation.Name;
                    break;
                }
            }

            T section = Section(sectionName);


            if (section == null) {
                return new T().OnMissingConfiguration() as T;
            }

            return section;
        }

        protected virtual bool Compare(ConfigurationSection sec)
        {
            return sec.ElementInformation.Type == typeof(T)  || (sec.ElementInformation.Type.IsGenericType && sec.ElementInformation.Type.GetGenericTypeDefinition() == typeof(FeatureConfigurationSection<>));
        }

        public T Section(string sectionName)
        {
            return (T)ConfigurationManager.GetSection(sectionName);
        }
    }
}