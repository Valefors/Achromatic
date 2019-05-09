using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AwesomeFolders
{
	/// <summary>
	/// Display a button grid with styles textures on it
	/// </summary>
	public class StyleGrid : ButtonGrid
	{
		/// <summary>
		/// Represent a grid element (button) which is a style with a name, a color, a Low & High resolution, an icon id and a creation date
		/// </summary>
		public class StyleElement : GridElement
		{
			public string Name { get; private set; }
			public Color MainColor { get; private set; }
			public Texture2D HighResTex { get; set; }
			public Texture2D LowResTex { get; set; }
			public int IconId { get; set; }

			public StyleElement(string name, Color mainColor, int iconId)
			{
				Name = name;
				MainColor = mainColor;
				IconId = iconId;
				HighResTex = null;
				LowResTex = null;
			}

			public override Texture2D GetElementTexture()
			{
				return HighResTex;
			}

			public override string GetElementName()
			{
				return Name;
			}
		}

		/// <summary>
		/// Create a new button grid
		/// </summary>
		/// <param name="path">Path to search for texture</param>
		/// <param name="size">Button size</param>
		public StyleGrid(string path, float size) : base(path, size) {}

		public override GridElement GetElementForTexture(Texture2D tex, string key, string texPath)
		{
			Regex styleRegex = new Regex(@"([\-0-9]+);([\-0-9]+);(.*)");
			Match regexData = styleRegex.Match(key);
			string styleName = regexData.Groups[3].Value;

			Color mainColor = ColorUtils.IntToColor(int.Parse(regexData.Groups[1].Value));

			return new StyleElement(styleName, mainColor, int.Parse(regexData.Groups[2].Value));
		}

		public override void OnElementFound(GridElement element, Texture2D tex)
		{
			if (tex.width == 64)
			{
				((StyleElement)element).HighResTex = tex;
			}
			else if (tex.width == 16)
			{
				((StyleElement)element).LowResTex = tex;
			}
		}

		public override void OnAllElementFound()
		{
			//elementList.Sort((a, b) => ((StyleElement)a).CreationDate.CompareTo(((StyleElement)b).CreationDate));
		}
	}
}
