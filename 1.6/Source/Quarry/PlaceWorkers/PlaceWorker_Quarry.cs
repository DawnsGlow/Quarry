using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Quarry
{

    public class PlaceWorker_Quarry : PlaceWorker
    {

        List<IntVec3> occupiedCellsTemp = new List<IntVec3>();

        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            // God Mode allows placing the quarry without the grid restriction
            if (!DebugSettings.godMode && !QuarrySettings.placeAnywhere)
            {
                int occCells = 0;
                int rockCells = 0;
                foreach (IntVec3 c in GenAdj.CellsOccupiedBy(loc, rot, checkingDef.Size))
                {
                    occCells++;

                    // Make sure the quarry is placeable here
                    if (map.GetComponent<QuarryGrid>().GetCellBool(c))
                    {
                        rockCells++;
                    }
                }

                // Require at least 60% rocky terrain
                if ((float)(occCells - rockCells) / occCells > 0.4f)
                {
                    return Static.ReportNotEnoughStone;
                }
            }

            return true;
        }

        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            if (!DebugSettings.godMode /*&& !QuarrySettings.placeAnywhere*/)
            {
                // Draw the placement areas
                Find.CurrentMap.GetComponent<QuarryGrid>().MarkForDraw();
                // Log.Message("Drawing Cells");
                GenDraw.DrawFieldEdges(GenAdj.CellsOccupiedBy(center, rot, def.Size).ToList(), Color.green);
            }
        }

        public override void DrawMouseAttachments(BuildableDef def)
        {
            //Find.CurrentMap.GetComponent<QuarryGrid>().RenderMouseAttachments();
            Map map = Find.CurrentMap;
            IntVec3 mouseCell = UI.MouseCell();
            if (!mouseCell.InBounds(map))
                return;

            // Get all rocks under the building footprint
            var rocksUnder = QuarryUtility.RockTypesUnderArea(mouseCell, Rot4.North, def.Size, map);

            Vector2 vector = mouseCell.ToVector3().MapToUIPosition();
            GUI.color = Color.white;
            Text.Font = GameFont.Small;
            Text.Anchor = TextAnchor.MiddleLeft;

            for (int i = 0; i < rocksUnder.Count; i++)
            {
                DrawResourceRow(vector, i, rocksUnder[i].chunkDef, rocksUnder[i].rockDef.label);
            }

            Text.Anchor = TextAnchor.UpperLeft;
        }
        
        private void DrawResourceRow(Vector2 vector, int rowIndex, ThingDef thingDef, string key)
        {
            float num = (UI.CurUICellSize() - 27f) / 2f;
            Rect rect = new Rect(vector.x + num, vector.y - UI.CurUICellSize() + num - rowIndex * 31f, 27f, 27f);
            Widgets.ThingIcon(rect, thingDef);
            Widgets.Label(new Rect(rect.xMax + 4f, rect.y, 999f, 29f), key);
        }

    }
}
