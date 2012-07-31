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

namespace NFeature.Configuration
{
	using System.ComponentModel;
	using System.Configuration;

	/// <summary>
	/// 	Based upon de-compilation of the connection strings configuration section.
	/// </summary>
	public class FeatureConfigurationSection<TFeatureEnum, TTenantEnum> : ConfigurationSectionBase
		where TFeatureEnum : struct
		where TTenantEnum : struct
	{
		private static readonly ConfigurationProperty ConfigurationProperties =
			new ConfigurationProperty(null,
									  typeof (
										FeatureConfigurationElementCollection<TFeatureEnum, TTenantEnum>),
									  null,
									  ConfigurationPropertyOptions.IsDefaultCollection);

		[TypeConverter(typeof (CommaDelimitedStringCollectionConverter))]
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public FeatureConfigurationElementCollection<TFeatureEnum, TTenantEnum> FeatureSettings {
			get {
				return
					((FeatureConfigurationElementCollection<TFeatureEnum, TTenantEnum>)
					 base[ConfigurationProperties]);
			}
		}
	}
}