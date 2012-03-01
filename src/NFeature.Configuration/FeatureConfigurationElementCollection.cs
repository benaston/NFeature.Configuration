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
	using System.Configuration;

	public class FeatureConfigurationElementCollection<TFeatureEnum, TTenant> :
		ConfigurationElementCollection
		where TFeatureEnum : struct
		where TTenant : struct
	{
		public override ConfigurationElementCollectionType CollectionType {
			get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
		}

		public FeatureConfigurationElement<TFeatureEnum, TTenant> this[int index] {
			get { return (FeatureConfigurationElement<TFeatureEnum, TTenant>) BaseGet(index); }
			set {
				if (BaseGet(index) != null) {
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		public void Add(FeatureConfigurationElement<TFeatureEnum, TTenant> element) {
			BaseAdd(element);
		}

		public void Clear() {
			BaseClear();
		}

		protected override ConfigurationElement CreateNewElement() {
			return new FeatureConfigurationElement<TFeatureEnum, TTenant>();
		}

		protected override object GetElementKey(ConfigurationElement element) {
			return ((FeatureConfigurationElement<TFeatureEnum, TTenant>) element).Name;
		}

		public void Remove(FeatureConfigurationElement<TFeatureEnum, TTenant> element) {
			BaseRemove(element.Name);
		}

		public void Remove(string name) {
			BaseRemove(name);
		}

		public void RemoveAt(int index) {
			BaseRemoveAt(index);
		}
	}
}