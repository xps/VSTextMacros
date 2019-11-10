using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using VSTextMacros.Utilities;

namespace VSTextMacros.Model
{
	public class SavedMacro
	{
		public string Guid { get; set; }
		public string Name { get; set; }

		public SavedMacro(string guid, string name)
		{
			Guid = guid;
			Name = name;
		}

		private SavedMacro() { }
	}

	public static class SavedMacros
	{
		// Returns the saved macro lists, deserialized from List.xml
		public static List<SavedMacro> GetMacroList()
		{
			var filename = Path.Combine(VSTextMacrosPackage.Current.MacroDirectory, "List.xml");

			if (File.Exists(filename))
			{
				try
				{
					return XmlHelpers.Deserialize<List<SavedMacro>>(File.ReadAllText(filename));
				}
				catch
				{
					Debug.WriteLine("Error while loading macro list from List.xml file");
				}
			}

			return new List<SavedMacro>();
		}

		// Saves the macro list to List.xml
		public static void SaveMacroList(IEnumerable<SavedMacro> list)
		{
			var filename = Path.Combine(VSTextMacrosPackage.Current.MacroDirectory, "List.xml");
			var xml = XmlHelpers.Serialize(list);

			File.WriteAllText(filename, xml);
		}

		// Returns the saved macro from the given guid
		public static Macro GetSavedMacro(string guid)
		{
			var filename = GetMacroFilename(guid);
			if (File.Exists(filename))
				return Macro.LoadFromFile(filename);
			else
				return null;
		}

		// Saves the given macro under the given name
		public static void SaveMacro(Macro macro, string name, string guid = null)
		{
			// Get the list
			var list = GetMacroList();

			// Update the list item or add a new one
			if (guid != null && list.Any(x => x.Guid == guid))
				list.SingleOrDefault(x => x.Guid == guid).Name = name;
			else
				list.Add(new SavedMacro(guid = Guid.NewGuid().ToString(), name));

			// Save the macro
			Macro.SaveToFile(macro, GetMacroFilename(guid));

			// Update the list
			SaveMacroList(list);
		}

		// Deletes a macro
		public static void DeleteMacro(string guid)
		{
			// Delete the macro from the list
			var list = GetMacroList();
			var macro = list.SingleOrDefault(x => x.Guid == guid);
			if (macro != null)
			{
				list.Remove(macro);
				SaveMacroList(list);
			}

			// Delete the file
			var filename = GetMacroFilename(guid);
			if (File.Exists(filename))
				File.Delete(filename);
		}

		// Renames a macro
		public static void RenameMacro(string guid, string newName)
		{
			var list = GetMacroList();
			list.Single(x => x.Guid == guid).Name = newName;
			SaveMacroList(list);
		}

		// Returns the full filename that stores the macro with the given GUID
		public static string GetMacroFilename(string guid)
		{
			return Path.Combine(VSTextMacrosPackage.Current.MacroDirectory, guid + ".xml");
		}
	}
}