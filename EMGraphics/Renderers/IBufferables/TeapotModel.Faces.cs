﻿namespace EMGraphics
{
    internal partial class TeapotModel
    {
        internal struct Face
        {
            public ushort vertexId1;
            public ushort vertexId2;
            public ushort vertexId3;

            public Face(ushort vertexId1, ushort vertexId2, ushort vertexId3)
            {
                this.vertexId1 = vertexId1;
                this.vertexId2 = vertexId2;
                this.vertexId3 = vertexId3;
            }
        }

        internal static readonly ushort[] faceData = new ushort[]
        {
            457,458,459,
			459,458,468,
			468,458,473,
			473,458,478,
			478,458,483,
			483,458,488,
			488,458,493,
			493,458,498,
			498,458,503,
			503,458,508,
			508,458,513,
			513,458,518,
			518,458,523,
			523,458,528,
			528,458,533,
			533,458,538,
			538,458,543,
			543,458,548,
			548,458,553,
			553,458,558,
			558,458,563,
			563,458,568,
			568,458,573,
			573,458,457,
			2,3,4,
			4,1,2,
			1,4,5,
			6,5,4,
			5,6,7,
			8,7,6,
			10,9,8,
			7,8,9,
			12,11,10,
			9,10,11,
			14,13,12,
			11,12,13,
			3,15,16,
			16,4,3,
			4,16,6,
			17,6,16,
			6,17,8,
			18,8,17,
			19,10,18,
			8,18,10,
			20,12,19,
			10,19,12,
			21,14,20,
			12,20,14,
			15,22,23,
			23,16,15,
			16,23,17,
			24,17,23,
			17,24,18,
			25,18,24,
			26,19,25,
			18,25,19,
			27,20,26,
			19,26,20,
			28,21,27,
			20,27,21,
			22,29,23,
			30,23,29,
			23,30,24,
			31,24,30,
			24,31,32,
			32,25,24,
			33,26,25,
			25,32,33,
			34,27,26,
			26,33,34,
			35,28,27,
			27,34,35,
			29,36,30,
			37,30,36,
			30,37,38,
			38,31,30,
			31,38,39,
			39,32,31,
			40,33,32,
			32,39,40,
			41,34,33,
			33,40,41,
			42,35,34,
			34,41,42,
			36,43,37,
			44,37,43,
			37,44,45,
			45,38,37,
			38,45,46,
			46,39,38,
			47,40,39,
			39,46,47,
			48,41,40,
			40,47,48,
			49,42,41,
			41,48,49,
			43,50,51,
			51,44,43,
			44,51,45,
			52,45,51,
			45,52,46,
			53,46,52,
			54,47,53,
			46,53,47,
			55,48,54,
			47,54,48,
			56,49,55,
			48,55,49,
			50,57,58,
			58,51,50,
			51,58,52,
			59,52,58,
			52,59,53,
			60,53,59,
			61,54,60,
			53,60,54,
			62,55,61,
			54,61,55,
			63,56,62,
			55,62,56,
			57,64,65,
			65,58,57,
			58,65,66,
			66,59,58,
			59,66,60,
			67,60,66,
			68,61,67,
			60,67,61,
			69,62,68,
			61,68,62,
			70,63,69,
			62,69,63,
			64,71,65,
			72,65,71,
			65,72,73,
			73,66,65,
			66,73,74,
			74,67,66,
			75,68,67,
			67,74,75,
			76,69,68,
			68,75,76,
			77,70,69,
			69,76,77,
			71,78,72,
			79,72,78,
			72,79,80,
			80,73,72,
			73,80,81,
			81,74,73,
			82,75,74,
			74,81,82,
			83,76,75,
			75,82,83,
			84,77,76,
			76,83,84,
			78,85,79,
			86,79,85,
			79,86,87,
			87,80,79,
			80,87,88,
			88,81,80,
			89,82,81,
			81,88,89,
			90,83,82,
			82,89,90,
			91,84,83,
			83,90,91,
			85,92,93,
			93,86,85,
			86,93,94,
			94,87,86,
			87,94,88,
			95,88,94,
			96,89,95,
			88,95,89,
			97,90,96,
			89,96,90,
			98,91,97,
			90,97,91,
			92,99,100,
			100,93,92,
			93,100,94,
			101,94,100,
			94,101,95,
			102,95,101,
			103,96,102,
			95,102,96,
			104,97,103,
			96,103,97,
			105,98,104,
			97,104,98,
			99,106,107,
			107,100,99,
			100,107,108,
			108,101,100,
			101,108,102,
			109,102,108,
			110,103,109,
			102,109,103,
			111,104,110,
			103,110,104,
			112,105,111,
			104,111,105,
			106,113,107,
			114,107,113,
			107,114,115,
			115,108,107,
			108,115,116,
			116,109,108,
			117,110,109,
			109,116,117,
			118,111,110,
			110,117,118,
			119,112,111,
			111,118,119,
			113,120,114,
			121,114,120,
			114,121,122,
			122,115,114,
			115,122,123,
			123,116,115,
			124,117,116,
			116,123,124,
			125,118,117,
			117,124,125,
			126,119,118,
			118,125,126,
			120,127,121,
			128,121,127,
			121,128,129,
			129,122,121,
			122,129,130,
			130,123,122,
			131,124,123,
			123,130,131,
			132,125,124,
			124,131,132,
			133,126,125,
			125,132,133,
			127,134,135,
			135,128,127,
			128,135,129,
			136,129,135,
			129,136,130,
			137,130,136,
			138,131,137,
			130,137,131,
			139,132,138,
			131,138,132,
			140,133,139,
			132,139,133,
			134,141,142,
			142,135,134,
			135,142,136,
			143,136,142,
			136,143,137,
			144,137,143,
			145,138,144,
			137,144,138,
			146,139,145,
			138,145,139,
			147,140,146,
			139,146,140,
			141,148,149,
			149,142,141,
			142,149,143,
			150,143,149,
			143,150,144,
			151,144,150,
			152,145,151,
			144,151,145,
			153,146,152,
			145,152,146,
			154,147,153,
			146,153,147,
			148,155,149,
			156,149,155,
			149,156,150,
			157,150,156,
			150,157,158,
			158,151,150,
			159,152,151,
			151,158,159,
			160,153,152,
			152,159,160,
			161,154,153,
			153,160,161,
			155,162,156,
			163,156,162,
			156,163,164,
			164,157,156,
			157,164,165,
			165,158,157,
			166,159,158,
			158,165,166,
			167,160,159,
			159,166,167,
			168,161,160,
			160,167,168,
			162,2,163,
			1,163,2,
			163,1,164,
			5,164,1,
			164,5,7,
			7,165,164,
			9,166,165,
			165,7,9,
			11,167,166,
			166,9,11,
			13,168,167,
			167,11,13,
			170,169,14,
			13,14,169,
			172,171,170,
			169,170,171,
			174,173,172,
			171,172,173,
			176,175,174,
			173,174,175,
			178,177,176,
			175,176,177,
			180,179,178,
			177,178,179,
			181,170,21,
			14,21,170,
			182,172,181,
			170,181,172,
			183,174,182,
			172,182,174,
			184,176,183,
			174,183,176,
			185,178,184,
			176,184,178,
			186,180,185,
			178,185,180,
			187,181,28,
			21,28,181,
			188,182,187,
			181,187,182,
			189,183,188,
			182,188,183,
			190,184,189,
			183,189,184,
			191,185,190,
			184,190,185,
			192,186,191,
			185,191,186,
			193,187,28,
			28,35,193,
			194,188,187,
			187,193,194,
			195,189,188,
			188,194,195,
			196,190,189,
			189,195,196,
			197,191,190,
			190,196,197,
			198,192,191,
			191,197,198,
			199,193,35,
			35,42,199,
			200,194,193,
			193,199,200,
			201,195,194,
			194,200,201,
			202,196,195,
			195,201,202,
			203,197,196,
			196,202,203,
			204,198,197,
			197,203,204,
			205,199,42,
			42,49,205,
			206,200,199,
			199,205,206,
			207,201,200,
			200,206,207,
			208,202,201,
			201,207,208,
			209,203,202,
			202,208,209,
			210,204,203,
			203,209,210,
			211,205,56,
			49,56,205,
			212,206,211,
			205,211,206,
			213,207,212,
			206,212,207,
			214,208,213,
			207,213,208,
			215,209,214,
			208,214,209,
			216,210,215,
			209,215,210,
			217,211,63,
			56,63,211,
			218,212,217,
			211,217,212,
			219,213,218,
			212,218,213,
			220,214,219,
			213,219,214,
			221,215,220,
			214,220,215,
			222,216,221,
			215,221,216,
			223,217,70,
			63,70,217,
			224,218,223,
			217,223,218,
			225,219,224,
			218,224,219,
			226,220,225,
			219,225,220,
			227,221,226,
			220,226,221,
			228,222,227,
			221,227,222,
			229,223,70,
			70,77,229,
			230,224,223,
			223,229,230,
			231,225,224,
			224,230,231,
			232,226,225,
			225,231,232,
			233,227,226,
			226,232,233,
			234,228,227,
			227,233,234,
			235,229,77,
			77,84,235,
			236,230,229,
			229,235,236,
			237,231,230,
			230,236,237,
			238,232,231,
			231,237,238,
			239,233,232,
			232,238,239,
			240,234,233,
			233,239,240,
			241,235,84,
			84,91,241,
			242,236,235,
			235,241,242,
			243,237,236,
			236,242,243,
			244,238,237,
			237,243,244,
			245,239,238,
			238,244,245,
			246,240,239,
			239,245,246,
			247,241,98,
			91,98,241,
			248,242,247,
			241,247,242,
			249,243,248,
			242,248,243,
			250,244,249,
			243,249,244,
			251,245,250,
			244,250,245,
			252,246,251,
			245,251,246,
			253,247,105,
			98,105,247,
			254,248,253,
			247,253,248,
			255,249,254,
			248,254,249,
			256,250,255,
			249,255,250,
			257,251,256,
			250,256,251,
			258,252,257,
			251,257,252,
			259,253,112,
			105,112,253,
			260,254,259,
			253,259,254,
			261,255,260,
			254,260,255,
			262,256,261,
			255,261,256,
			263,257,262,
			256,262,257,
			264,258,263,
			257,263,258,
			265,259,112,
			112,119,265,
			266,260,259,
			259,265,266,
			267,261,260,
			260,266,267,
			268,262,261,
			261,267,268,
			269,263,262,
			262,268,269,
			270,264,263,
			263,269,270,
			271,265,119,
			119,126,271,
			272,266,265,
			265,271,272,
			273,267,266,
			266,272,273,
			274,268,267,
			267,273,274,
			275,269,268,
			268,274,275,
			276,270,269,
			269,275,276,
			277,271,126,
			126,133,277,
			278,272,271,
			271,277,278,
			279,273,272,
			272,278,279,
			280,274,273,
			273,279,280,
			281,275,274,
			274,280,281,
			282,276,275,
			275,281,282,
			283,277,140,
			133,140,277,
			284,278,283,
			277,283,278,
			285,279,284,
			278,284,279,
			286,280,285,
			279,285,280,
			287,281,286,
			280,286,281,
			288,282,287,
			281,287,282,
			289,283,147,
			140,147,283,
			290,284,289,
			283,289,284,
			291,285,290,
			284,290,285,
			292,286,291,
			285,291,286,
			293,287,292,
			286,292,287,
			294,288,293,
			287,293,288,
			295,289,154,
			147,154,289,
			296,290,295,
			289,295,290,
			297,291,296,
			290,296,291,
			298,292,297,
			291,297,292,
			299,293,298,
			292,298,293,
			300,294,299,
			293,299,294,
			301,295,154,
			154,161,301,
			302,296,295,
			295,301,302,
			303,297,296,
			296,302,303,
			304,298,297,
			297,303,304,
			305,299,298,
			298,304,305,
			306,300,299,
			299,305,306,
			307,301,161,
			161,168,307,
			308,302,301,
			301,307,308,
			309,303,302,
			302,308,309,
			310,304,303,
			303,309,310,
			311,305,304,
			304,310,311,
			312,306,305,
			305,311,312,
			169,307,168,
			168,13,169,
			171,308,307,
			307,169,171,
			173,309,308,
			308,171,173,
			175,310,309,
			309,173,175,
			177,311,310,
			310,175,177,
			179,312,311,
			311,177,179,
			314,313,179,
			179,180,314,
			316,315,313,
			313,314,316,
			318,317,315,
			315,316,318,
			320,319,317,
			317,318,320,
			322,321,319,
			319,320,322,
			324,323,321,
			321,322,324,
			325,314,180,
			180,186,325,
			326,316,314,
			314,325,326,
			327,318,316,
			316,326,327,
			328,320,318,
			318,327,328,
			329,322,320,
			320,328,329,
			330,324,322,
			322,329,330,
			331,325,186,
			186,192,331,
			332,326,325,
			325,331,332,
			333,327,326,
			326,332,333,
			334,328,327,
			327,333,334,
			335,329,328,
			328,334,335,
			336,330,329,
			329,335,336,
			337,331,198,
			192,198,331,
			338,332,337,
			331,337,332,
			339,333,338,
			332,338,333,
			340,334,339,
			333,339,334,
			341,335,340,
			334,340,335,
			342,336,341,
			335,341,336,
			343,337,204,
			198,204,337,
			344,338,343,
			337,343,338,
			345,339,344,
			338,344,339,
			346,340,345,
			339,345,340,
			347,341,346,
			340,346,341,
			348,342,347,
			341,347,342,
			349,343,210,
			204,210,343,
			350,344,349,
			343,349,344,
			351,345,350,
			344,350,345,
			352,346,351,
			345,351,346,
			353,347,352,
			346,352,347,
			354,348,353,
			347,353,348,
			355,349,210,
			210,216,355,
			356,350,349,
			349,355,356,
			357,351,350,
			350,356,357,
			358,352,351,
			351,357,358,
			359,353,352,
			352,358,359,
			360,354,353,
			353,359,360,
			361,355,216,
			216,222,361,
			362,356,355,
			355,361,362,
			363,357,356,
			356,362,363,
			364,358,357,
			357,363,364,
			365,359,358,
			358,364,365,
			366,360,359,
			359,365,366,
			367,361,222,
			222,228,367,
			368,362,361,
			361,367,368,
			369,363,362,
			362,368,369,
			370,364,363,
			363,369,370,
			371,365,364,
			364,370,371,
			372,366,365,
			365,371,372,
			373,367,234,
			228,234,367,
			374,368,373,
			367,373,368,
			375,369,374,
			368,374,369,
			376,370,375,
			369,375,370,
			377,371,376,
			370,376,371,
			378,372,377,
			371,377,372,
			379,373,240,
			234,240,373,
			380,374,379,
			373,379,374,
			381,375,380,
			374,380,375,
			382,376,381,
			375,381,376,
			383,377,382,
			376,382,377,
			384,378,383,
			377,383,378,
			385,379,246,
			240,246,379,
			386,380,385,
			379,385,380,
			387,381,386,
			380,386,381,
			388,382,387,
			381,387,382,
			389,383,388,
			382,388,383,
			390,384,389,
			383,389,384,
			391,385,246,
			246,252,391,
			392,386,385,
			385,391,392,
			393,387,386,
			386,392,393,
			394,388,387,
			387,393,394,
			395,389,388,
			388,394,395,
			396,390,389,
			389,395,396,
			397,391,252,
			252,258,397,
			398,392,391,
			391,397,398,
			399,393,392,
			392,398,399,
			400,394,393,
			393,399,400,
			401,395,394,
			394,400,401,
			402,396,395,
			395,401,402,
			403,397,258,
			258,264,403,
			404,398,397,
			397,403,404,
			405,399,398,
			398,404,405,
			406,400,399,
			399,405,406,
			407,401,400,
			400,406,407,
			408,402,401,
			401,407,408,
			409,403,270,
			264,270,403,
			410,404,409,
			403,409,404,
			411,405,410,
			404,410,405,
			412,406,411,
			405,411,406,
			413,407,412,
			406,412,407,
			414,408,413,
			407,413,408,
			415,409,276,
			270,276,409,
			416,410,415,
			409,415,410,
			417,411,416,
			410,416,411,
			418,412,417,
			411,417,412,
			419,413,418,
			412,418,413,
			420,414,419,
			413,419,414,
			421,415,282,
			276,282,415,
			422,416,421,
			415,421,416,
			423,417,422,
			416,422,417,
			424,418,423,
			417,423,418,
			425,419,424,
			418,424,419,
			426,420,425,
			419,425,420,
			427,421,282,
			282,288,427,
			428,422,421,
			421,427,428,
			429,423,422,
			422,428,429,
			430,424,423,
			423,429,430,
			431,425,424,
			424,430,431,
			432,426,425,
			425,431,432,
			433,427,288,
			288,294,433,
			434,428,427,
			427,433,434,
			435,429,428,
			428,434,435,
			436,430,429,
			429,435,436,
			437,431,430,
			430,436,437,
			438,432,431,
			431,437,438,
			439,433,294,
			294,300,439,
			440,434,433,
			433,439,440,
			441,435,434,
			434,440,441,
			442,436,435,
			435,441,442,
			443,437,436,
			436,442,443,
			444,438,437,
			437,443,444,
			445,439,306,
			300,306,439,
			446,440,445,
			439,445,440,
			447,441,446,
			440,446,441,
			448,442,447,
			441,447,442,
			449,443,448,
			442,448,443,
			450,444,449,
			443,449,444,
			451,445,312,
			306,312,445,
			452,446,451,
			445,451,446,
			453,447,452,
			446,452,447,
			454,448,453,
			447,453,448,
			455,449,454,
			448,454,449,
			456,450,455,
			449,455,450,
			313,451,179,
			312,179,451,
			315,452,313,
			451,313,452,
			317,453,315,
			452,315,453,
			319,454,317,
			453,317,454,
			321,455,319,
			454,319,455,
			323,456,321,
			455,321,456,
			457,459,460,
			461,460,459,
			460,461,462,
			463,462,461,
			462,463,464,
			465,464,463,
			464,465,466,
			467,466,465,
			466,467,323,
			456,323,467,
			459,468,461,
			469,461,468,
			461,469,463,
			470,463,469,
			463,470,465,
			471,465,470,
			465,471,467,
			472,467,471,
			467,472,456,
			450,456,472,
			468,473,469,
			474,469,473,
			469,474,470,
			475,470,474,
			470,475,471,
			476,471,475,
			471,476,472,
			477,472,476,
			472,477,450,
			444,450,477,
			473,478,479,
			479,474,473,
			474,479,480,
			480,475,474,
			475,480,481,
			481,476,475,
			476,481,482,
			482,477,476,
			477,482,438,
			438,444,477,
			478,483,484,
			484,479,478,
			479,484,485,
			485,480,479,
			480,485,486,
			486,481,480,
			481,486,487,
			487,482,481,
			482,487,432,
			432,438,482,
			483,488,489,
			489,484,483,
			484,489,490,
			490,485,484,
			485,490,491,
			491,486,485,
			486,491,492,
			492,487,486,
			487,492,426,
			426,432,487,
			488,493,489,
			494,489,493,
			489,494,490,
			495,490,494,
			490,495,491,
			496,491,495,
			491,496,492,
			497,492,496,
			492,497,426,
			420,426,497,
			493,498,494,
			499,494,498,
			494,499,495,
			500,495,499,
			495,500,496,
			501,496,500,
			496,501,497,
			502,497,501,
			497,502,420,
			414,420,502,
			498,503,499,
			504,499,503,
			499,504,500,
			505,500,504,
			500,505,501,
			506,501,505,
			501,506,502,
			507,502,506,
			502,507,414,
			408,414,507,
			503,508,509,
			509,504,503,
			504,509,510,
			510,505,504,
			505,510,511,
			511,506,505,
			506,511,512,
			512,507,506,
			507,512,402,
			402,408,507,
			508,513,514,
			514,509,508,
			509,514,515,
			515,510,509,
			510,515,516,
			516,511,510,
			511,516,517,
			517,512,511,
			512,517,396,
			396,402,512,
			513,518,519,
			519,514,513,
			514,519,520,
			520,515,514,
			515,520,521,
			521,516,515,
			516,521,522,
			522,517,516,
			517,522,390,
			390,396,517,
			518,523,519,
			524,519,523,
			519,524,520,
			525,520,524,
			520,525,521,
			526,521,525,
			521,526,522,
			527,522,526,
			522,527,390,
			384,390,527,
			523,528,524,
			529,524,528,
			524,529,525,
			530,525,529,
			525,530,526,
			531,526,530,
			526,531,527,
			532,527,531,
			527,532,384,
			378,384,532,
			528,533,529,
			534,529,533,
			529,534,530,
			535,530,534,
			530,535,531,
			536,531,535,
			531,536,532,
			537,532,536,
			532,537,378,
			372,378,537,
			533,538,539,
			539,534,533,
			534,539,540,
			540,535,534,
			535,540,541,
			541,536,535,
			536,541,542,
			542,537,536,
			537,542,366,
			366,372,537,
			538,543,544,
			544,539,538,
			539,544,545,
			545,540,539,
			540,545,546,
			546,541,540,
			541,546,547,
			547,542,541,
			542,547,360,
			360,366,542,
			543,548,549,
			549,544,543,
			544,549,550,
			550,545,544,
			545,550,551,
			551,546,545,
			546,551,552,
			552,547,546,
			547,552,354,
			354,360,547,
			548,553,549,
			554,549,553,
			549,554,550,
			555,550,554,
			550,555,551,
			556,551,555,
			551,556,552,
			557,552,556,
			552,557,354,
			348,354,557,
			553,558,554,
			559,554,558,
			554,559,555,
			560,555,559,
			555,560,556,
			561,556,560,
			556,561,557,
			562,557,561,
			557,562,348,
			342,348,562,
			558,563,559,
			564,559,563,
			559,564,560,
			565,560,564,
			560,565,561,
			566,561,565,
			561,566,562,
			567,562,566,
			562,567,342,
			336,342,567,
			563,568,569,
			569,564,563,
			564,569,570,
			570,565,564,
			565,570,571,
			571,566,565,
			566,571,572,
			572,567,566,
			567,572,330,
			330,336,567,
			568,573,574,
			574,569,568,
			569,574,575,
			575,570,569,
			570,575,576,
			576,571,570,
			571,576,577,
			577,572,571,
			572,577,324,
			324,330,572,
			573,457,460,
			460,574,573,
			574,460,462,
			462,575,574,
			575,462,464,
			464,576,575,
			576,464,466,
			466,577,576,
			577,466,323,
			323,324,577,
			581,578,580,
			579,580,578,
			583,581,582,
			580,582,581,
			585,583,584,
			582,584,583,
			587,585,584,
			584,586,587,
			589,587,586,
			586,588,589,
			591,589,588,
			588,590,591,
			593,591,592,
			590,592,591,
			595,593,594,
			592,594,593,
			597,595,596,
			594,596,595,
			599,597,596,
			596,598,599,
			601,599,598,
			598,600,601,
			603,601,600,
			600,602,603,
			605,603,604,
			602,604,603,
			607,605,606,
			604,606,605,
			609,607,608,
			606,608,607,
			611,609,608,
			608,610,611,
			613,611,610,
			610,612,613,
			615,613,612,
			612,614,615,
			617,615,616,
			614,616,615,
			619,617,618,
			616,618,617,
			621,619,620,
			618,620,619,
			623,621,620,
			620,622,623,
			625,623,622,
			622,624,625,
			578,625,624,
			624,579,578,
			627,626,581,
			578,581,626,
			629,628,627,
			626,627,628,
			631,630,629,
			628,629,630,
			633,632,631,
			630,631,632,
			635,634,633,
			632,633,634,
			637,636,635,
			634,635,636,
			638,627,583,
			581,583,627,
			639,629,638,
			627,638,629,
			640,631,639,
			629,639,631,
			641,633,640,
			631,640,633,
			642,635,641,
			633,641,635,
			643,637,642,
			635,642,637,
			644,638,585,
			583,585,638,
			645,639,644,
			638,644,639,
			646,640,645,
			639,645,640,
			647,641,646,
			640,646,641,
			648,642,647,
			641,647,642,
			649,643,648,
			642,648,643,
			650,644,585,
			585,587,650,
			651,645,644,
			644,650,651,
			652,646,645,
			645,651,652,
			653,647,646,
			646,652,653,
			654,648,647,
			647,653,654,
			655,649,648,
			648,654,655,
			656,650,587,
			587,589,656,
			657,651,650,
			650,656,657,
			658,652,651,
			651,657,658,
			659,653,652,
			652,658,659,
			660,654,653,
			653,659,660,
			661,655,654,
			654,660,661,
			662,656,589,
			589,591,662,
			663,657,656,
			656,662,663,
			664,658,657,
			657,663,664,
			665,659,658,
			658,664,665,
			666,660,659,
			659,665,666,
			667,661,660,
			660,666,667,
			668,662,593,
			591,593,662,
			669,663,668,
			662,668,663,
			670,664,669,
			663,669,664,
			671,665,670,
			664,670,665,
			672,666,671,
			665,671,666,
			673,667,672,
			666,672,667,
			674,668,595,
			593,595,668,
			675,669,674,
			668,674,669,
			676,670,675,
			669,675,670,
			677,671,676,
			670,676,671,
			678,672,677,
			671,677,672,
			679,673,678,
			672,678,673,
			680,674,597,
			595,597,674,
			681,675,680,
			674,680,675,
			682,676,681,
			675,681,676,
			683,677,682,
			676,682,677,
			684,678,683,
			677,683,678,
			685,679,684,
			678,684,679,
			686,680,597,
			597,599,686,
			687,681,680,
			680,686,687,
			688,682,681,
			681,687,688,
			689,683,682,
			682,688,689,
			690,684,683,
			683,689,690,
			691,685,684,
			684,690,691,
			692,686,599,
			599,601,692,
			693,687,686,
			686,692,693,
			694,688,687,
			687,693,694,
			695,689,688,
			688,694,695,
			696,690,689,
			689,695,696,
			697,691,690,
			690,696,697,
			698,692,601,
			601,603,698,
			699,693,692,
			692,698,699,
			700,694,693,
			693,699,700,
			701,695,694,
			694,700,701,
			702,696,695,
			695,701,702,
			703,697,696,
			696,702,703,
			704,698,605,
			603,605,698,
			705,699,704,
			698,704,699,
			706,700,705,
			699,705,700,
			707,701,706,
			700,706,701,
			708,702,707,
			701,707,702,
			709,703,708,
			702,708,703,
			710,704,607,
			605,607,704,
			711,705,710,
			704,710,705,
			712,706,711,
			705,711,706,
			713,707,712,
			706,712,707,
			714,708,713,
			707,713,708,
			715,709,714,
			708,714,709,
			716,710,609,
			607,609,710,
			717,711,716,
			710,716,711,
			718,712,717,
			711,717,712,
			719,713,718,
			712,718,713,
			720,714,719,
			713,719,714,
			721,715,720,
			714,720,715,
			722,716,609,
			609,611,722,
			723,717,716,
			716,722,723,
			724,718,717,
			717,723,724,
			725,719,718,
			718,724,725,
			726,720,719,
			719,725,726,
			727,721,720,
			720,726,727,
			728,722,611,
			611,613,728,
			729,723,722,
			722,728,729,
			730,724,723,
			723,729,730,
			731,725,724,
			724,730,731,
			732,726,725,
			725,731,732,
			733,727,726,
			726,732,733,
			734,728,613,
			613,615,734,
			735,729,728,
			728,734,735,
			736,730,729,
			729,735,736,
			737,731,730,
			730,736,737,
			738,732,731,
			731,737,738,
			739,733,732,
			732,738,739,
			740,734,617,
			615,617,734,
			741,735,740,
			734,740,735,
			742,736,741,
			735,741,736,
			743,737,742,
			736,742,737,
			744,738,743,
			737,743,738,
			745,739,744,
			738,744,739,
			746,740,619,
			617,619,740,
			747,741,746,
			740,746,741,
			748,742,747,
			741,747,742,
			749,743,748,
			742,748,743,
			750,744,749,
			743,749,744,
			751,745,750,
			744,750,745,
			752,746,621,
			619,621,746,
			753,747,752,
			746,752,747,
			754,748,753,
			747,753,748,
			755,749,754,
			748,754,749,
			756,750,755,
			749,755,750,
			757,751,756,
			750,756,751,
			758,752,621,
			621,623,758,
			759,753,752,
			752,758,759,
			760,754,753,
			753,759,760,
			761,755,754,
			754,760,761,
			762,756,755,
			755,761,762,
			763,757,756,
			756,762,763,
			764,758,623,
			623,625,764,
			765,759,758,
			758,764,765,
			766,760,759,
			759,765,766,
			767,761,760,
			760,766,767,
			768,762,761,
			761,767,768,
			769,763,762,
			762,768,769,
			626,764,625,
			625,578,626,
			628,765,764,
			764,626,628,
			630,766,765,
			765,628,630,
			632,767,766,
			766,630,632,
			634,768,767,
			767,632,634,
			636,769,768,
			768,634,636,
			770,771,773,
			772,773,771,
			774,770,775,
			773,775,770,
			777,776,774,
			774,775,777,
			779,778,777,
			776,777,778,
			781,780,779,
			778,779,780,
			783,782,781,
			780,781,782,
			773,772,785,
			784,785,772,
			775,773,786,
			785,786,773,
			777,775,787,
			786,787,775,
			788,779,777,
			777,787,788,
			789,781,788,
			779,788,781,
			790,783,789,
			781,789,783,
			785,784,792,
			791,792,784,
			786,785,793,
			792,793,785,
			787,786,794,
			793,794,786,
			795,788,787,
			787,794,795,
			796,789,795,
			788,795,789,
			797,790,796,
			789,796,790,
			792,791,799,
			798,799,791,
			793,792,800,
			799,800,792,
			794,793,801,
			800,801,793,
			802,795,794,
			794,801,802,
			803,796,802,
			795,802,796,
			804,797,803,
			796,803,797,
			799,798,806,
			805,806,798,
			800,799,807,
			806,807,799,
			801,800,808,
			807,808,800,
			809,802,801,
			801,808,809,
			810,803,809,
			802,809,803,
			811,804,810,
			803,810,804,
			806,805,813,
			812,813,805,
			807,806,814,
			813,814,806,
			815,808,814,
			807,814,808,
			816,809,815,
			808,815,809,
			817,810,816,
			809,816,810,
			818,811,817,
			810,817,811,
			819,820,813,
			813,812,819,
			820,821,814,
			814,813,820,
			822,815,814,
			814,821,822,
			823,816,815,
			815,822,823,
			824,817,816,
			816,823,824,
			825,818,817,
			817,824,825,
			826,827,820,
			820,819,826,
			827,828,821,
			821,820,827,
			828,829,822,
			822,821,828,
			830,823,829,
			822,829,823,
			831,824,823,
			823,830,831,
			832,825,824,
			824,831,832,
			833,834,827,
			827,826,833,
			834,835,828,
			828,827,834,
			835,836,829,
			829,828,835,
			837,830,836,
			829,836,830,
			838,831,830,
			830,837,838,
			839,832,831,
			831,838,839,
			840,841,834,
			834,833,840,
			841,842,835,
			835,834,841,
			842,843,836,
			836,835,842,
			844,837,843,
			836,843,837,
			845,838,837,
			837,844,845,
			846,839,838,
			838,845,846,
			847,848,841,
			841,840,847,
			848,849,842,
			842,841,848,
			849,850,843,
			843,842,849,
			851,844,850,
			843,850,844,
			852,845,844,
			844,851,852,
			853,846,845,
			845,852,853,
			771,770,848,
			848,847,771,
			770,774,849,
			849,848,770,
			776,850,774,
			849,774,850,
			778,851,850,
			850,776,778,
			780,852,851,
			851,778,780,
			782,853,852,
			852,780,782,
			855,854,783,
			782,783,854,
			857,856,854,
			854,855,857,
			859,858,856,
			856,857,859,
			861,860,858,
			858,859,861,
			863,862,860,
			860,861,863,
			865,864,862,
			862,863,865,
			866,855,783,
			783,790,866,
			867,857,855,
			855,866,867,
			868,859,857,
			857,867,868,
			869,861,859,
			859,868,869,
			870,863,861,
			861,869,870,
			871,865,870,
			863,870,865,
			872,866,790,
			790,797,872,
			873,867,866,
			866,872,873,
			874,868,867,
			867,873,874,
			875,869,874,
			868,874,869,
			876,870,875,
			869,875,870,
			877,871,876,
			870,876,871,
			878,872,797,
			797,804,878,
			879,873,872,
			872,878,879,
			880,874,873,
			873,879,880,
			881,875,880,
			874,880,875,
			882,876,881,
			875,881,876,
			883,877,882,
			876,882,877,
			884,878,804,
			804,811,884,
			885,879,878,
			878,884,885,
			886,880,879,
			879,885,886,
			887,881,880,
			880,886,887,
			888,882,881,
			881,887,888,
			889,883,888,
			882,888,883,
			890,884,811,
			811,818,890,
			891,885,884,
			884,890,891,
			892,886,885,
			885,891,892,
			893,887,886,
			886,892,893,
			894,888,887,
			887,893,894,
			895,889,888,
			888,894,895,
			896,890,825,
			818,825,890,
			897,891,896,
			890,896,891,
			898,892,897,
			891,897,892,
			899,893,898,
			892,898,893,
			900,894,899,
			893,899,894,
			901,895,900,
			894,900,895,
			902,896,832,
			825,832,896,
			903,897,902,
			896,902,897,
			904,898,903,
			897,903,898,
			905,899,904,
			898,904,899,
			906,900,905,
			899,905,900,
			907,901,900,
			900,906,907,
			908,902,839,
			832,839,902,
			909,903,908,
			902,908,903,
			910,904,909,
			903,909,904,
			911,905,904,
			904,910,911,
			912,906,905,
			905,911,912,
			913,907,906,
			906,912,913,
			914,908,846,
			839,846,908,
			915,909,914,
			908,914,909,
			916,910,915,
			909,915,910,
			917,911,910,
			910,916,917,
			918,912,911,
			911,917,918,
			919,913,912,
			912,918,919,
			920,914,853,
			846,853,914,
			921,915,920,
			914,920,915,
			922,916,921,
			915,921,916,
			923,917,922,
			916,922,917,
			924,918,923,
			917,923,918,
			925,919,918,
			918,924,925,
			854,920,853,
			853,782,854,
			856,921,854,
			920,854,921,
			858,922,856,
			921,856,922,
			860,923,858,
			922,858,923,
			862,924,860,
			923,860,924,
			864,925,862,
			924,862,925,
			927,928,929,
			929,926,927,
			926,929,931,
			931,930,926,
			930,931,933,
			933,932,930,
			932,933,935,
			935,934,932,
			934,935,937,
			937,936,934,
			936,937,938,
			939,938,937,
			940,941,928,
			929,928,941,
			929,941,942,
			942,931,929,
			931,942,943,
			943,933,931,
			933,943,944,
			944,935,933,
			935,944,937,
			945,937,944,
			937,945,939,
			946,939,945,
			947,948,940,
			941,940,948,
			941,948,949,
			949,942,941,
			942,949,943,
			950,943,949,
			943,950,944,
			951,944,950,
			944,951,945,
			952,945,951,
			945,952,946,
			953,946,952,
			954,955,947,
			948,947,955,
			948,955,956,
			956,949,948,
			949,956,950,
			957,950,956,
			950,957,951,
			958,951,957,
			951,958,952,
			959,952,958,
			952,959,953,
			960,953,959,
			954,961,962,
			962,955,954,
			955,962,956,
			963,956,962,
			956,963,957,
			964,957,963,
			957,964,958,
			965,958,964,
			958,965,959,
			966,959,965,
			959,966,960,
			967,960,966,
			961,968,962,
			969,962,968,
			962,969,963,
			970,963,969,
			963,970,964,
			971,964,970,
			964,971,965,
			972,965,971,
			965,972,966,
			973,966,972,
			966,973,967,
			974,967,973,
			968,975,976,
			976,969,968,
			969,976,977,
			977,970,969,
			970,977,978,
			978,971,970,
			971,978,979,
			979,972,971,
			972,979,980,
			980,973,972,
			973,980,981,
			981,974,973,
			975,982,976,
			983,976,982,
			976,983,984,
			984,977,976,
			977,984,985,
			985,978,977,
			978,985,986,
			986,979,978,
			979,986,987,
			987,980,979,
			980,987,988,
			988,981,980,
			983,982,989,
			989,990,983,
			983,990,984,
			991,984,990,
			984,991,992,
			992,985,984,
			985,992,993,
			993,986,985,
			986,993,994,
			994,987,986,
			987,994,995,
			995,988,987,
			990,989,996,
			996,997,990,
			990,997,991,
			998,991,997,
			991,998,999,
			999,992,991,
			992,999,1000,
			1000,993,992,
			993,1000,1001,
			1001,994,993,
			994,1001,1002,
			1002,995,994,
			997,996,1003,
			1003,1004,997,
			997,1004,998,
			1005,998,1004,
			998,1005,999,
			1006,999,1005,
			999,1006,1000,
			1007,1000,1006,
			1000,1007,1008,
			1008,1001,1000,
			1001,1008,1009,
			1009,1002,1001,
			1003,927,1004,
			926,1004,927,
			1004,926,1005,
			930,1005,926,
			1005,930,1006,
			932,1006,930,
			1006,932,1007,
			934,1007,932,
			1007,934,1008,
			936,1008,934,
			1008,936,938,
			938,1009,1008,
			938,939,1010,
			1011,1010,939,
			1010,1011,1012,
			1013,1012,1011,
			1012,1013,1014,
			1015,1014,1013,
			1017,1016,1014,
			1014,1015,1017,
			1019,1018,1016,
			1016,1017,1019,
			1021,1020,1018,
			1018,1019,1021,
			939,946,1011,
			1022,1011,946,
			1011,1022,1013,
			1023,1013,1022,
			1013,1023,1015,
			1024,1015,1023,
			1025,1017,1015,
			1015,1024,1025,
			1026,1019,1017,
			1017,1025,1026,
			1027,1021,1019,
			1019,1026,1027,
			946,953,1022,
			1028,1022,953,
			1022,1028,1023,
			1029,1023,1028,
			1023,1029,1024,
			1030,1024,1029,
			1031,1025,1024,
			1024,1030,1031,
			1032,1026,1025,
			1025,1031,1032,
			1033,1027,1026,
			1026,1032,1033,
			953,960,1028,
			1034,1028,960,
			1028,1034,1029,
			1035,1029,1034,
			1029,1035,1030,
			1036,1030,1035,
			1037,1031,1030,
			1030,1036,1037,
			1038,1032,1031,
			1031,1037,1038,
			1039,1033,1032,
			1032,1038,1039,
			960,967,1034,
			1040,1034,967,
			1034,1040,1035,
			1041,1035,1040,
			1035,1041,1036,
			1042,1036,1041,
			1043,1037,1036,
			1036,1042,1043,
			1044,1038,1037,
			1037,1043,1044,
			1045,1039,1038,
			1038,1044,1045,
			967,974,1040,
			1046,1040,974,
			1040,1046,1041,
			1047,1041,1046,
			1041,1047,1042,
			1048,1042,1047,
			1049,1043,1042,
			1042,1048,1049,
			1050,1044,1043,
			1043,1049,1050,
			1051,1045,1044,
			1044,1050,1051,
			974,981,1052,
			1052,1046,974,
			1046,1052,1053,
			1053,1047,1046,
			1047,1053,1054,
			1054,1048,1047,
			1055,1049,1054,
			1048,1054,1049,
			1056,1050,1055,
			1049,1055,1050,
			1057,1051,1056,
			1050,1056,1051,
			981,988,1058,
			1058,1052,981,
			1052,1058,1059,
			1059,1053,1052,
			1053,1059,1060,
			1060,1054,1053,
			1061,1055,1060,
			1054,1060,1055,
			1062,1056,1061,
			1055,1061,1056,
			1063,1057,1062,
			1056,1062,1057,
			988,995,1064,
			1064,1058,988,
			1058,1064,1065,
			1065,1059,1058,
			1059,1065,1066,
			1066,1060,1059,
			1067,1061,1066,
			1060,1066,1061,
			1068,1062,1067,
			1061,1067,1062,
			1069,1063,1068,
			1062,1068,1063,
			995,1002,1070,
			1070,1064,995,
			1064,1070,1071,
			1071,1065,1064,
			1065,1071,1072,
			1072,1066,1065,
			1073,1067,1072,
			1066,1072,1067,
			1074,1068,1073,
			1067,1073,1068,
			1075,1069,1074,
			1068,1074,1069,
			1002,1009,1076,
			1076,1070,1002,
			1070,1076,1077,
			1077,1071,1070,
			1071,1077,1078,
			1078,1072,1071,
			1079,1073,1078,
			1072,1078,1073,
			1080,1074,1079,
			1073,1079,1074,
			1081,1075,1080,
			1074,1080,1075,
			1009,938,1010,
			1010,1076,1009,
			1076,1010,1012,
			1012,1077,1076,
			1077,1012,1014,
			1014,1078,1077,
			1016,1079,1014,
			1078,1014,1079,
			1018,1080,1016,
			1079,1016,1080,
			1020,1081,1018,
			1080,1018,1081,
			1082,1083,1084,
			1084,1083,1093,
			1093,1083,1098,
			1098,1083,1103,
			1103,1083,1108,
			1108,1083,1113,
			1113,1083,1118,
			1118,1083,1123,
			1123,1083,1128,
			1128,1083,1133,
			1133,1083,1138,
			1138,1083,1143,
			1143,1083,1148,
			1148,1083,1153,
			1153,1083,1158,
			1158,1083,1163,
			1163,1083,1168,
			1168,1083,1173,
			1173,1083,1178,
			1178,1083,1183,
			1183,1083,1188,
			1188,1083,1193,
			1193,1083,1198,
			1198,1083,1082,
			1086,1085,1084,
			1082,1084,1085,
			1088,1087,1085,
			1085,1086,1088,
			1090,1089,1087,
			1087,1088,1090,
			1092,1091,1089,
			1089,1090,1092,
			1094,1086,1093,
			1084,1093,1086,
			1095,1088,1086,
			1086,1094,1095,
			1096,1090,1088,
			1088,1095,1096,
			1097,1092,1090,
			1090,1096,1097,
			1099,1094,1098,
			1093,1098,1094,
			1100,1095,1094,
			1094,1099,1100,
			1101,1096,1095,
			1095,1100,1101,
			1102,1097,1096,
			1096,1101,1102,
			1104,1099,1098,
			1098,1103,1104,
			1105,1100,1104,
			1099,1104,1100,
			1106,1101,1105,
			1100,1105,1101,
			1107,1102,1106,
			1101,1106,1102,
			1109,1104,1103,
			1103,1108,1109,
			1110,1105,1109,
			1104,1109,1105,
			1111,1106,1110,
			1105,1110,1106,
			1112,1107,1111,
			1106,1111,1107,
			1114,1109,1108,
			1108,1113,1114,
			1115,1110,1114,
			1109,1114,1110,
			1116,1111,1115,
			1110,1115,1111,
			1117,1112,1116,
			1111,1116,1112,
			1119,1114,1118,
			1113,1118,1114,
			1120,1115,1114,
			1114,1119,1120,
			1121,1116,1115,
			1115,1120,1121,
			1122,1117,1116,
			1116,1121,1122,
			1124,1119,1123,
			1118,1123,1119,
			1125,1120,1119,
			1119,1124,1125,
			1126,1121,1120,
			1120,1125,1126,
			1127,1122,1121,
			1121,1126,1127,
			1129,1124,1128,
			1123,1128,1124,
			1130,1125,1124,
			1124,1129,1130,
			1131,1126,1125,
			1125,1130,1131,
			1132,1127,1126,
			1126,1131,1132,
			1134,1129,1128,
			1128,1133,1134,
			1135,1130,1134,
			1129,1134,1130,
			1136,1131,1135,
			1130,1135,1131,
			1137,1132,1136,
			1131,1136,1132,
			1139,1134,1133,
			1133,1138,1139,
			1140,1135,1139,
			1134,1139,1135,
			1141,1136,1140,
			1135,1140,1136,
			1142,1137,1141,
			1136,1141,1137,
			1144,1139,1138,
			1138,1143,1144,
			1145,1140,1144,
			1139,1144,1140,
			1146,1141,1145,
			1140,1145,1141,
			1147,1142,1146,
			1141,1146,1142,
			1149,1144,1148,
			1143,1148,1144,
			1150,1145,1144,
			1144,1149,1150,
			1151,1146,1145,
			1145,1150,1151,
			1152,1147,1146,
			1146,1151,1152,
			1154,1149,1153,
			1148,1153,1149,
			1155,1150,1149,
			1149,1154,1155,
			1156,1151,1150,
			1150,1155,1156,
			1157,1152,1151,
			1151,1156,1157,
			1159,1154,1158,
			1153,1158,1154,
			1160,1155,1154,
			1154,1159,1160,
			1161,1156,1155,
			1155,1160,1161,
			1162,1157,1156,
			1156,1161,1162,
			1164,1159,1158,
			1158,1163,1164,
			1165,1160,1164,
			1159,1164,1160,
			1166,1161,1165,
			1160,1165,1161,
			1167,1162,1166,
			1161,1166,1162,
			1169,1164,1163,
			1163,1168,1169,
			1170,1165,1169,
			1164,1169,1165,
			1171,1166,1170,
			1165,1170,1166,
			1172,1167,1171,
			1166,1171,1167,
			1174,1169,1168,
			1168,1173,1174,
			1175,1170,1174,
			1169,1174,1170,
			1176,1171,1175,
			1170,1175,1171,
			1177,1172,1176,
			1171,1176,1172,
			1179,1174,1178,
			1173,1178,1174,
			1180,1175,1174,
			1174,1179,1180,
			1181,1176,1175,
			1175,1180,1181,
			1182,1177,1176,
			1176,1181,1182,
			1184,1179,1183,
			1178,1183,1179,
			1185,1180,1179,
			1179,1184,1185,
			1186,1181,1180,
			1180,1185,1186,
			1187,1182,1181,
			1181,1186,1187,
			1189,1184,1188,
			1183,1188,1184,
			1190,1185,1184,
			1184,1189,1190,
			1191,1186,1185,
			1185,1190,1191,
			1192,1187,1186,
			1186,1191,1192,
			1194,1189,1188,
			1188,1193,1194,
			1195,1190,1194,
			1189,1194,1190,
			1196,1191,1195,
			1190,1195,1191,
			1197,1192,1196,
			1191,1196,1192,
			1199,1194,1193,
			1193,1198,1199,
			1200,1195,1199,
			1194,1199,1195,
			1201,1196,1200,
			1195,1200,1196,
			1202,1197,1201,
			1196,1201,1197,
			1085,1199,1198,
			1198,1082,1085,
			1087,1200,1085,
			1199,1085,1200,
			1089,1201,1087,
			1200,1087,1201,
			1091,1202,1089,
			1201,1089,1202,
        };
    }
}