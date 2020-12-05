using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers.Player.Upgrades
{
    public class RandomUpgradeContainer : MonoBehaviour
    {
        public UpgradeType[] upgrades;
        [SerializeField] private UpgradeType[] upgradesAlwaysPresent;
        [SerializeField] private int numUpgrades = 3;

        private void Awake()
        {
            Generate(numUpgrades);
        }

        public void Generate(int n)
        {
            const int numAllUpgrades = (int) UpgradeType.Count;

            if (n <= 0 || n >= numAllUpgrades) throw new System.Exception("Argument out of range!");

            var numToChoose = n - upgradesAlwaysPresent.Length;
            var numOfChoices = numAllUpgrades - upgradesAlwaysPresent.Length;

            // The list of possible upgrades always includes the guaranteed ones first.
            var possibleUpgrades = new List<UpgradeType>(upgradesAlwaysPresent);

            // Now place the non-guaranteed ones after.
            for (var i = 0; i < numAllUpgrades; ++i)
            {
                var upgrade = (UpgradeType) i;

                if (!upgradesAlwaysPresent.Contains(upgrade)) possibleUpgrades.Add(upgrade);
            }

            // Shuffle the non-guaranteed upgrades.
            for (var i = 0; i < numToChoose; ++i)
            {
                // Pick a random element out of the remaining elements, and swap it into place.
                var j = upgradesAlwaysPresent.Length + i;
                var index = j + Random.Range(0, numOfChoices - i);
                var tmp = possibleUpgrades[index];
                possibleUpgrades[index] = possibleUpgrades[j];
                possibleUpgrades[j] = tmp;
            }

            upgrades = possibleUpgrades.Take(n).ToArray();
        }
    }
}