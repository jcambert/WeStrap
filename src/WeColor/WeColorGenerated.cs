using System.Collections.Generic;
namespace WeC{

	public readonly partial struct WeColor{

		internal static Dictionary<string,WeColor> Colors=new Dictionary<string,WeColor>(){
			{"aliceblue",AliceBlue},
			{"antiquewhite",AntiqueWhite},
			{"aqua",Aqua},
			{"aquamarine",Aquamarine},
			{"azure",Azure},
			{"beige",Beige},
			{"bisque",Bisque},
			{"black	",Black	},
			{"blanchedalmond",BlanchedAlmond},
			{"blue",Blue},
			{"blueviolet",BlueViolet},
			{"brown",Brown},
			{"burlywood",BurlyWood},
			{"cadetblue",CadetBlue},
			{"chartreuse",Chartreuse},
			{"chocolate",Chocolate},
			{"coral	",Coral	},
			{"cornflowerblue",CornflowerBlue},
			{"cornsilk",Cornsilk},
			{"crimson",Crimson},
			{"cyan",Cyan},
			{"darkblue",DarkBlue},
			{"darkcyan",DarkCyan},
			{"darkgoldenrod",DarkGoldenRod},
			{"darkgray",DarkGray},
			{"darkgreen",DarkGreen},
			{"darkkhaki",DarkKhaki},
			{"darkmagenta",DarkMagenta},
			{"darkolivegreen",DarkOliveGreen},
			{"darkorange",DarkOrange},
			{"darkorchid",DarkOrchid},
			{"darkred",DarkRed},
			{"darksalmon",DarkSalmon},
			{"darkseagreen",DarkSeaGreen},
			{"darkslateblue",DarkSlateBlue},
			{"darkslategray",DarkSlateGray},
			{"darkturquoise",DarkTurquoise},
			{"darkviolet",DarkViolet},
			{"deeppink",DeepPink},
			{"deepskyblue",DeepSkyBlue},
			{"dimgray",DimGray},
			{"dodgerblue",DodgerBlue},
			{"firebrick",FireBrick},
			{"floralwhite",FloralWhite},
			{"forestgreen",ForestGreen},
			{"fuchsia",Fuchsia},
			{"gainsboro",Gainsboro},
			{"ghostwhite",GhostWhite},
			{"gold",Gold},
			{"goldenrod",GoldenRod},
			{"gray",Gray},
			{"green",Green},
			{"greenyellow",GreenYellow},
			{"honeydew",HoneyDew},
			{"hotpink",HotPink},
			{"indianred",IndianRed},
			{"indigo",Indigo},
			{"ivory",Ivory},
			{"khaki",Khaki},
			{"lavender",Lavender},
			{"lavenderblush",LavenderBlush},
			{"lawngreen",LawnGreen},
			{"lemonchiffon",LemonChiffon},
			{"lightblue",LightBlue},
			{"lightcoral",LightCoral},
			{"lightcyan",LightCyan},
			{"lightgoldenrodyellow",LightGoldenRodYellow},
			{"lightgray",LightGray},
			{"lightgreen",LightGreen},
			{"lightpink",LightPink},
			{"lightsalmon",LightSalmon},
			{"lightseagreen",LightSeaGreen},
			{"lightskyblue",LightSkyBlue},
			{"lightslategray",LightSlateGray},
			{"lightsteelblue",LightSteelBlue},
			{"lightyellow",LightYellow},
			{"lime",Lime},
			{"limegreen",LimeGreen},
			{"linen",Linen},
			{"magenta",Magenta},
			{"maroon",Maroon},
			{"mediumaquamarine",MediumAquaMarine},
			{"mediumblue",MediumBlue},
			{"mediumorchid",MediumOrchid},
			{"mediumpurple",MediumPurple},
			{"mediumseagreen",MediumSeaGreen},
			{"mediumslateblue",MediumSlateBlue},
			{"mediumspringgreen",MediumSpringGreen},
			{"mediumturquoise",MediumTurquoise},
			{"mediumvioletred",MediumVioletRed},
			{"midnightblue",MidnightBlue},
			{"mintcream",MintCream},
			{"mistyrose",MistyRose},
			{"moccasin",Moccasin},
			{"navajowhite",NavajoWhite},
			{"navy",Navy},
			{"oldlace",OldLace},
			{"olive",Olive},
			{"olivedrab",OliveDrab},
			{"orange",Orange},
			{"orangered",OrangeRed},
			{"orchid",Orchid},
			{"palegoldenrod",PaleGoldenRod},
			{"palegreen",PaleGreen},
			{"paleturquoise",PaleTurquoise},
			{"palevioletred",PaleVioletRed},
			{"papayawhip",PapayaWhip},
			{"peachpuff",PeachPuff},
			{"peru",Peru},
			{"pink",Pink},
			{"plum",Plum},
			{"powderblue",PowderBlue},
			{"purple",Purple},
			{"rebeccapurple",RebeccaPurple},
			{"red",Red},
			{"rosybrown",RosyBrown},
			{"royalblue",RoyalBlue},
			{"saddlebrown",SaddleBrown},
			{"salmon",Salmon},
			{"sandybrown",SandyBrown},
			{"seagreen",SeaGreen},
			{"seashell",SeaShell},
			{"sienna",Sienna},
			{"silver",Silver},
			{"skyblue",SkyBlue},
			{"slateblue",SlateBlue},
			{"slategray",SlateGray},
			{"snow",Snow},
			{"springgreen",SpringGreen},
			{"steelblue",SteelBlue},
			{"tan",Tan},
			{"teal",Teal},
			{"thistle",Thistle},
			{"tomato",Tomato},
			{"turquoise",Turquoise},
			{"violet",Violet},
			{"wheat",Wheat},
			{"white",White},
			{"whitesmoke",WhiteSmoke},
			{"yellow",Yellow},
			{"yellowgreen",YellowGreen},
			
		};
		public static WeColor AliceBlue=WeColor.From("#F0F8FF");
		public static WeColor AntiqueWhite=WeColor.From("#FAEBD7");
		public static WeColor Aqua=WeColor.From("#	00FFFF");
		public static WeColor Aquamarine=WeColor.From("#7FFFD4");
		public static WeColor Azure=WeColor.From("#F0FFFF");
		public static WeColor Beige=WeColor.From("#F5F5DC");
		public static WeColor Bisque=WeColor.From("#FFE4C4");
		public static WeColor Black	=WeColor.From("#000000");
		public static WeColor BlanchedAlmond=WeColor.From("#FFEBCD");
		public static WeColor Blue=WeColor.From("#0000FF");
		public static WeColor BlueViolet=WeColor.From("#8A2BE2");
		public static WeColor Brown=WeColor.From("#A52A2A");
		public static WeColor BurlyWood=WeColor.From("#DEB887");
		public static WeColor CadetBlue=WeColor.From("#5F9EA0");
		public static WeColor Chartreuse=WeColor.From("#7FFF00");
		public static WeColor Chocolate=WeColor.From("#D2691E");
		public static WeColor Coral	=WeColor.From("#FF7F50");
		public static WeColor CornflowerBlue=WeColor.From("#6495ED");
		public static WeColor Cornsilk=WeColor.From("#FFF8DC");
		public static WeColor Crimson=WeColor.From("#DC143C");
		public static WeColor Cyan=WeColor.From("#00FFFF");
		public static WeColor DarkBlue=WeColor.From("#00008B");
		public static WeColor DarkCyan=WeColor.From("#008B8B");
		public static WeColor DarkGoldenRod=WeColor.From("#B8860B");
		public static WeColor DarkGray=WeColor.From("#A9A9A9");
		public static WeColor DarkGreen=WeColor.From("#006400");
		public static WeColor DarkKhaki=WeColor.From("#BDB76B");
		public static WeColor DarkMagenta=WeColor.From("#8B008B");
		public static WeColor DarkOliveGreen=WeColor.From("#556B2F");
		public static WeColor DarkOrange=WeColor.From("#FF8C00");
		public static WeColor DarkOrchid=WeColor.From("#9932CC");
		public static WeColor DarkRed=WeColor.From("#8B0000");
		public static WeColor DarkSalmon=WeColor.From("#E9967A");
		public static WeColor DarkSeaGreen=WeColor.From("#8FBC8F");
		public static WeColor DarkSlateBlue=WeColor.From("#483D8B");
		public static WeColor DarkSlateGray=WeColor.From("#2F4F4F");
		public static WeColor DarkTurquoise=WeColor.From("#00CED1");
		public static WeColor DarkViolet=WeColor.From("#9400D3");
		public static WeColor DeepPink=WeColor.From("#FF1493");
		public static WeColor DeepSkyBlue=WeColor.From("#00BFFF");
		public static WeColor DimGray=WeColor.From("#696969");
		public static WeColor DodgerBlue=WeColor.From("#1E90FF");
		public static WeColor FireBrick=WeColor.From("#B22222");
		public static WeColor FloralWhite=WeColor.From("#FFFAF0");
		public static WeColor ForestGreen=WeColor.From("#228B22");
		public static WeColor Fuchsia=WeColor.From("#FF00FF");
		public static WeColor Gainsboro=WeColor.From("#DCDCDC");
		public static WeColor GhostWhite=WeColor.From("#F8F8FF");
		public static WeColor Gold=WeColor.From("#FFD700");
		public static WeColor GoldenRod=WeColor.From("#DAA520");
		public static WeColor Gray=WeColor.From("#808080");
		public static WeColor Green=WeColor.From("#008000");
		public static WeColor GreenYellow=WeColor.From("#ADFF2F");
		public static WeColor HoneyDew=WeColor.From("#F0FFF0");
		public static WeColor HotPink=WeColor.From("#FF69B4");
		public static WeColor IndianRed=WeColor.From("#CD5C5C");
		public static WeColor Indigo=WeColor.From("#4B0082");
		public static WeColor Ivory=WeColor.From("#FFFFF0");
		public static WeColor Khaki=WeColor.From("#F0E68C");
		public static WeColor Lavender=WeColor.From("#E6E6FA");
		public static WeColor LavenderBlush=WeColor.From("#FFF0F5");
		public static WeColor LawnGreen=WeColor.From("#7CFC00");
		public static WeColor LemonChiffon=WeColor.From("#FFFACD");
		public static WeColor LightBlue=WeColor.From("#ADD8E6");
		public static WeColor LightCoral=WeColor.From("#F08080");
		public static WeColor LightCyan=WeColor.From("#E0FFFF");
		public static WeColor LightGoldenRodYellow=WeColor.From("#FAFAD2");
		public static WeColor LightGray=WeColor.From("#D3D3D3");
		public static WeColor LightGreen=WeColor.From("#90EE90");
		public static WeColor LightPink=WeColor.From("#FFB6C1");
		public static WeColor LightSalmon=WeColor.From("#FFA07A");
		public static WeColor LightSeaGreen=WeColor.From("#20B2AA");
		public static WeColor LightSkyBlue=WeColor.From("#87CEFA");
		public static WeColor LightSlateGray=WeColor.From("#778899");
		public static WeColor LightSteelBlue=WeColor.From("#B0C4DE");
		public static WeColor LightYellow=WeColor.From("#FFFFE0");
		public static WeColor Lime=WeColor.From("#00FF00");
		public static WeColor LimeGreen=WeColor.From("#32CD32");
		public static WeColor Linen=WeColor.From("#FAF0E6");
		public static WeColor Magenta=WeColor.From("#FF00FF");
		public static WeColor Maroon=WeColor.From("#800000");
		public static WeColor MediumAquaMarine=WeColor.From("#66CDAA");
		public static WeColor MediumBlue=WeColor.From("#0000CD");
		public static WeColor MediumOrchid=WeColor.From("#BA55D3");
		public static WeColor MediumPurple=WeColor.From("#9370DB");
		public static WeColor MediumSeaGreen=WeColor.From("#3CB371");
		public static WeColor MediumSlateBlue=WeColor.From("#7B68EE");
		public static WeColor MediumSpringGreen=WeColor.From("#00FA9A");
		public static WeColor MediumTurquoise=WeColor.From("#48D1CC");
		public static WeColor MediumVioletRed=WeColor.From("#C71585");
		public static WeColor MidnightBlue=WeColor.From("#191970");
		public static WeColor MintCream=WeColor.From("#F5FFFA");
		public static WeColor MistyRose=WeColor.From("#FFE4E1");
		public static WeColor Moccasin=WeColor.From("#FFE4B5");
		public static WeColor NavajoWhite=WeColor.From("#FFDEAD");
		public static WeColor Navy=WeColor.From("#000080");
		public static WeColor OldLace=WeColor.From("#FDF5E6");
		public static WeColor Olive=WeColor.From("#808000");
		public static WeColor OliveDrab=WeColor.From("#6B8E23");
		public static WeColor Orange=WeColor.From("#FFA500");
		public static WeColor OrangeRed=WeColor.From("#FF4500");
		public static WeColor Orchid=WeColor.From("#DA70D6");
		public static WeColor PaleGoldenRod=WeColor.From("#EEE8AA");
		public static WeColor PaleGreen=WeColor.From("#98FB98");
		public static WeColor PaleTurquoise=WeColor.From("#AFEEEE");
		public static WeColor PaleVioletRed=WeColor.From("#DB7093");
		public static WeColor PapayaWhip=WeColor.From("#FFEFD5");
		public static WeColor PeachPuff=WeColor.From("#FFDAB9");
		public static WeColor Peru=WeColor.From("#CD853F");
		public static WeColor Pink=WeColor.From("#FFC0CB");
		public static WeColor Plum=WeColor.From("#DDA0DD");
		public static WeColor PowderBlue=WeColor.From("#B0E0E6");
		public static WeColor Purple=WeColor.From("#800080");
		public static WeColor RebeccaPurple=WeColor.From("#663399");
		public static WeColor Red=WeColor.From("#FF0000");
		public static WeColor RosyBrown=WeColor.From("#BC8F8F");
		public static WeColor RoyalBlue=WeColor.From("#4169E1");
		public static WeColor SaddleBrown=WeColor.From("#8B4513");
		public static WeColor Salmon=WeColor.From("#FA8072");
		public static WeColor SandyBrown=WeColor.From("#F4A460");
		public static WeColor SeaGreen=WeColor.From("#2E8B57");
		public static WeColor SeaShell=WeColor.From("#FFF5EE");
		public static WeColor Sienna=WeColor.From("#A0522D	");
		public static WeColor Silver=WeColor.From("#C0C0C0");
		public static WeColor SkyBlue=WeColor.From("#87CEEB");
		public static WeColor SlateBlue=WeColor.From("#6A5ACD");
		public static WeColor SlateGray=WeColor.From("#708090");
		public static WeColor Snow=WeColor.From("#FFFAFA");
		public static WeColor SpringGreen=WeColor.From("#00FF7F");
		public static WeColor SteelBlue=WeColor.From("#4682B4");
		public static WeColor Tan=WeColor.From("#D2B48C");
		public static WeColor Teal=WeColor.From("#008080");
		public static WeColor Thistle=WeColor.From("#D8BFD8");
		public static WeColor Tomato=WeColor.From("#FF6347");
		public static WeColor Turquoise=WeColor.From("#40E0D0");
		public static WeColor Violet=WeColor.From("#EE82EE");
		public static WeColor Wheat=WeColor.From("#F5DEB3");
		public static WeColor White=WeColor.From("#FFFFFF");
		public static WeColor WhiteSmoke=WeColor.From("#F5F5F5");
		public static WeColor Yellow=WeColor.From("#FFFF00");
		public static WeColor YellowGreen=WeColor.From("#9ACD32");
		
	}
}