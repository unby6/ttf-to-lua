export type GlyphProperty = 
	"AdvanceWidth" |
	"LeftSideBearing" | 
	"XBoundsMin" |
	"XBoundsMax" |
	"YBoundsMin" |
	"YBoundsMax"


local GlyphMapping = {
	AdvanceWidth = 1,
	LeftSideBearing = 2,
	XBoundsMin = 3,
	XBoundsMax = 4,
	YBoundsMin = 5,
	YBoundsMax = 6,
}

local function GetGlyphProperty(Glyph: {number}, Property: GlyphProperty)
	return Glyph[GlyphMapping[Property]]
end