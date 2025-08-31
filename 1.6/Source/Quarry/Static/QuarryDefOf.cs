﻿using Verse;
using RimWorld;

namespace Quarry
{
    [DefOf]
    public static class QuarryDefOf
    {

        public static ThingDef QRY_Quarry;
        public static ThingDef QRY_MediQuarry;
        public static ThingDef QRY_MiniQuarry;
        public static ThingDef Chemfuel;
        public static ThingDef MineableComponentsIndustrial;
        public static WorkTypeDef QuarryMining;
        public static TerrainDef QRY_QuarriedGround;
        public static TerrainDef QRY_QuarriedGroundWall;
        public static TerrainDef QRY_ReclaimedSoil;

        [MayRequire("Ludeon.RimWorld.Odyssey")]
        public static TerrainDef QRY_QuarriedGroundOrbitalPlatform;
        [MayRequire("Ludeon.RimWorld.Odyssey")]
        public static TerrainDef QRY_QuarriedGroundWallOrbitalPlatform;
        [MayRequire("Ludeon.RimWorld.Odyssey")]
        public static TerrainDef QRY_QuarriedGroundMechanoidPlatform;
        [MayRequire("Ludeon.RimWorld.Odyssey")]
        public static TerrainDef QRY_QuarriedGroundWallMechanoidPlatform;

        public static JobDef QRY_MineQuarry;

        public static ResearchProjectDef Stonecutting;

        public static LetterDef CuproLetter;
        public static ConceptDef QRY_ReclaimingSoil;

        public static ThingCategoryDef StoneChunks;
        public static StuffAppearanceDef Bricks;
        public static StuffAppearanceDef Planks;

        public static RecordDef QRY_CellsMined;

        public static DesignationDef QRY_Designator_ReclaimSoil;
    }
}

