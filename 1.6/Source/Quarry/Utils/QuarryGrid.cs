using RimWorld;
using UnityEngine;
using Verse;

namespace Quarry {

	public sealed class QuarryGrid : MapComponent, ICellBoolGiver, IExposable {

		private BoolGrid boolGrid;
		private CellBoolDrawer drawer;

		public Color Color => Color.green; 
		public bool GetCellBool(int index) => !map.fogGrid.IsFogged(index) && boolGrid[index] && !(map.edificeGrid[index] is Building_Quarry);
		public bool GetCellBool(IntVec3 c) => boolGrid[c];
		public Color GetCellExtraColor(int index) => Color.white;

		private CellBoolDrawer Drawer 
		{
			get 
			{
				if (drawer == null) 
				{
					drawer = new CellBoolDrawer(this, map.Size.x, map.Size.z);
				}
				return drawer;
			}
		}


		public QuarryGrid(Map map) : base(map) 
		{
			boolGrid = new BoolGrid(map);
		}


		public override void FinalizeInit() 
		{
			base.FinalizeInit();
			// Create a new boolGrid - this also gets called for old saves where
			// there wasn't a QuarryGrid present
			if (boolGrid.TrueCount == 0) 
			{
				ProcessBoolGrid();
			}
		}


		private void ProcessBoolGrid() 
		{
			foreach (IntVec3 c in map.AllCells) 
			{
				boolGrid.Set(c, QuarryUtility.IsValidQuarryTerrain(map.terrainGrid.TerrainAt(c)));
			}
			Drawer.SetDirty();
		}


		public void RemoveFromGrid(CellRect rect) 
		{
			foreach (IntVec3 c in rect.Cells) 
			{
				boolGrid.Set(c, false);
			}
			Drawer.SetDirty();
		}
		public void Notify_FogGridUpdate()
		{
			Drawer.SetDirty();
		}

		public void RemoveFromGrid(IntVec3 c)
		{
			boolGrid.Set(c, false);
			Drawer.SetDirty();
		}


		public void AddToGrid(CellRect rect) 
		{
			foreach (IntVec3 c in rect.Cells) 
			{
				boolGrid.Set(c, true);
			}
			Drawer.SetDirty();
		}
		
		public void AddToGrid(IntVec3 c)
		{
			boolGrid.Set(c, true);
			Drawer.SetDirty();
		}


		public override void MapComponentUpdate() 
		{
			base.MapComponentUpdate();
			Drawer.CellBoolDrawerUpdate();
		}


		public override void ExposeData() 
		{
			Scribe_Deep.Look(ref boolGrid, "boolGrid", new object[0]);
		}


		public void MarkForDraw() 
		{
			if (map == Find.CurrentMap) 
			{
				Drawer.MarkForDraw();
			}
		}

        public void RenderMouseAttachments()
        {
            IntVec3 c = UI.MouseCell();
            if (!c.InBounds(map)) 
				return;

            if (QuarryUtility.IsValidQuarryRock(map.terrainGrid.TerrainAt(c), out QuarryRockType rockType, out string key))
            {
                Vector2 vector = c.ToVector3().MapToUIPosition();
                GUI.color = Color.white;
                Text.Font = GameFont.Small;
                Text.Anchor = TextAnchor.MiddleLeft;

                DrawResourceRow(vector, 0, rockType.chunkDef, key);

                Text.Anchor = TextAnchor.UpperLeft;
            }
        }

        private void DrawResourceRow(Vector2 vector, int rowIndex, ThingDef thingDef, string key)
        {
            float num = (UI.CurUICellSize() - 27f) / 2f;
            Rect rect = new Rect(vector.x + num, vector.y - UI.CurUICellSize() + num + rowIndex * 31f, 27f, 27f);
            Widgets.ThingIcon(rect, thingDef);
            Widgets.Label(new Rect(rect.xMax + 4f, rect.y, 999f, 29f), key);
        }

    }
}
