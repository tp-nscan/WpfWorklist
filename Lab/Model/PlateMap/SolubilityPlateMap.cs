using System.Collections.Generic;

namespace Lab.Model.PlateMap
{
    /// <summary>
    /// Maps a 96 well compound plate onto two 384 well plates. 32 samples from the compound plate go two each plate.
    /// </summary>
    public class SolubilityPlateMap
    {
        public static IEnumerable<PlateMapTarget> PlateMapTargets(ISamplePlate compoundPlate, Plate1OrPlate2 plate1OrPlate2)
        {
            //yield return new PlateMapTarget()
            //                 {
            //                     new Injection(compoundPlate.Well(0,1).Sample.)
            //                 }

            yield break;
        }
    }
}
