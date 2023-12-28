using RimWorld;
using System;
using System.Linq;
using UnityEngine;
using Verse;

namespace MedievalProsthetics
{
    public class MedProSettings : ModSettings
    {
        public int workAmountTest;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref workAmountTest, "workAmountTest");
        }
    }


    public class MedProMod : Mod
    {
        public MedProSettings settings;
        public static MedProMod mod;

        public MedProMod(ModContentPack con) : base(con)
        {
            settings = GetSettings<MedProSettings>();
            mod = this;
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.Gap(24f);

            listing.Settings_IntegerBox("WorkAmountTestLabel".Translate(), ref mod.settings.workAmountTest, 500f, 24f, min: 1, max: 999999);
            listing.End();

            base.DoSettingsWindowContents(inRect);
        }

        public override void WriteSettings()
        {
            UpdateChanges();

            base.WriteSettings();
        }

        public override string SettingsCategory()
        {
            return "MenuTitle".Translate();
        }

        public static void UpdateChanges()
        {
            DefDatabase<ThingDef>.GetNamed("MechanicalMed").statBases.Where((StatModifier statBase) => statBase.stat == StatDefOf.WorkToMake).First().value = MedProMod.mod.settings.workAmountTest;
        }
    }
}

