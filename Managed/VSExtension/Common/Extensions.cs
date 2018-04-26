// Copyright (C) 2018 Angel Hernandez Matos
// You can redistribute this software and/or modify it under the terms of the 
// GNU General Public License  (GPL).  This program is distributed in the hope 
// that it will be useful, but WITHOUT ANY WARRANTY; without even the implied 
// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See License.txt for more details. 

/* Visual C# compiler   : Microsoft (R) Visual C# Compiler version 2.7.0.62707 (75dfc9b3)
Creation date           : 26/04/2018
Developer               : Angel Hernandez Matos
e-m@il                  : me@angelhernandezm.com
Website                 : http://www.angelhernandezm.com
*/


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using VisualSOS.Abstractions.Common;
using VisualSOS.Common.Entities;

namespace VisualSOS.Common {
    public static class Extensions {
		/// <summary>
		/// The flags
		/// </summary>
		private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty;

		/// <summary>
		/// Serializes as XML string.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public static string SerializeAsXmlString(this BaseEntity entity) {
			var retval = string.Empty;

			if (entity != null) {
				var serializer = new XmlSerializer(entity.GetType());

				using (var ms = new MemoryStream()) {
					serializer.Serialize(ms, entity);
					retval = Encoding.Default.GetString(ms.GetBuffer());
				}
			}

			return retval;
		}

		/// <summary>
		/// Deserializes the XML string as object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entityAsXmlString">The entity as XML string.</param>
		/// <returns></returns>
		public static T DeserializeXmlStringAsObject<T>(this string entityAsXmlString) {
			var retval = default(T);

			if (!string.IsNullOrEmpty(entityAsXmlString)) {
				var serializer = new XmlSerializer(typeof(T));

				try {
					using (var sr = new StringReader(entityAsXmlString)) {
						using (var xmlReader = new XmlTextReader(sr))
							retval = (T)serializer.Deserialize(xmlReader);
					}
				} catch {
					// We swallow the exception and return null
				}

			}

			return retval;
		}



		/// <summary>
		/// Gets the specialization.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="repository">The repository.</param>
		/// <returns></returns>
		public static IMainRepository<T> GetSpecialization<T>(this IBaseRepository repository) {
			return (IMainRepository<T>)repository;
		}


		/// <summary>
		/// Adds the range.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="target">The target.</param>
		/// <param name="source">The source.</param>
		public static void AddRange<T>(this ObservableCollection<T> target, List<T> source) where T : new() {
			if (target != null && source != null)
				source.ForEach(z => {
					var t = z.GetType();
					var p = t.GetProperties(Flags).Where(x => x.Module.ToString().StartsWith("TheHive.",
														 StringComparison.OrdinalIgnoreCase)).ToList();

					if (p.Count > 0) {
						var newObj = new T();
						var newObjType = newObj.GetType();

						p.ForEach(w => {
							newObjType?.GetProperty(w?.Name, Flags).SetValue(newObj, w?.GetValue(z));
						});

						target.Add(newObj);
					}
				});
		}


		/// <summary>
		/// Strips the namespaces.
		/// </summary>
		/// <param name="root">The root.</param>
		/// <returns></returns>
		private static XElement StripNamespaces(XElement root) {
			return new XElement(
				root.Name.LocalName,
				root.HasElements ?
					root.Elements().Select(el => StripNamespaces(el)) :
					(object)root.Value
			);
		}
	}
}
