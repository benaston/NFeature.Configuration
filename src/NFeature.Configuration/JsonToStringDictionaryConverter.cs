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
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Configuration;
	using System.Globalization;
	using Newtonsoft.Json;

	public sealed class JsonToStringDictionaryConverter : ConfigurationConverterBase
	{
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data) {
			return JsonConvert.DeserializeObject<Dictionary<string, object>>((string) data);
		}

		public override object ConvertTo(ITypeDescriptorContext ctx,
		                                 CultureInfo ci,
		                                 object value,
		                                 Type type) {
			ValidateType(value, typeof (Dictionary<string, string>));
			var dictionary = value as Dictionary<string, string>;
			if (dictionary != null) {
				return JsonConvert.SerializeObject(dictionary);
			}

			return null;
		}

		private static void ValidateType(object value, Type expected) {
			if ((value != null) && (value.GetType() != expected)) {
				throw new ArgumentException(string.Format("Converter unsupported value type {0}",
				                                          new {expected.Name}));
			}
		}
	}
}