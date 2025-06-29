using System.Collections.Generic;

using Verse;

namespace Quarry {
	[StaticConstructorOnStartup]
	public class Building_MediQuarry : Building_Quarry {

		public override int WallThickness => 2;
		protected override int QuarryDamageMultiplier => 2;
		protected override int SinkholeFrequency => 87;

		protected override List<IntVec3> LadderOffsets {
			get {
				return new List<IntVec3>() {
					Static.LadderOffset_Med
				};
			}
		}
	}
	[StaticConstructorOnStartup]
	public class Building_MiniQuarry : Building_Quarry {

		public override int WallThickness => 1;
		protected override int QuarryDamageMultiplier => 3;
		protected override int SinkholeFrequency => 75;

		protected override List<IntVec3> LadderOffsets {
			get {
				return new List<IntVec3>() {
					Static.LadderOffset_Small
				};
			}
		}
	}
}
