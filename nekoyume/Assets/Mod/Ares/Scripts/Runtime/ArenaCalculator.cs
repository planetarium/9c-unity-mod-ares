#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Lib9c.DevExtensions;
using Libplanet.Crypto;
using Nekoyume.Action;
using Nekoyume.Arena;
using Nekoyume.Extensions;
using Nekoyume.Model;
using Nekoyume.Model.State;
using Nekoyume.TableData;
using Debug = UnityEngine.Debug;
using static Nekoyume.Model.BattleStatus.Arena.ArenaLog;

namespace NineChronicles.Mod.Ares
{
    public static class ArenaCalculator
    {
        public static float ExecuteArena(
            Dictionary<Type, (Address address, ISheet sheet)> sheets,
            int? randomSeed,
            AvatarState avatarState,
            List<Guid> equipmentIds,
            List<Guid> costumeIds,
            List<RuneState> runeStates,
            AvatarState enemyAvatarState,
            List<Guid> enemyEquipmentIds,
            List<Guid> enemyCostumeIds,
            List<RuneState> enemyRuneStates,
            int playCount = 1)
        {
            var sw = new Stopwatch();
            sw.Start();
            randomSeed ??= new RandomImpl(DateTime.Now.Millisecond).Next(0, int.MaxValue);
            var random = new RandomImpl(randomSeed.Value);
            var avatarDigest = new ArenaPlayerDigest(
                avatarState,
                equipmentIds,
                costumeIds,
                runeStates);
            var enemyDigest = new ArenaPlayerDigest(
                enemyAvatarState,
                enemyEquipmentIds,
                enemyCostumeIds,
                enemyRuneStates);
            var winCount = 0;
            var arenaSimulatorSheets = sheets.GetArenaSimulatorSheets();
            for (var i = 0; i < playCount; i++)
            {
                var simulator = new ArenaSimulator(
                    new RandomImpl(random.Next()),
                    BattleArena.HpIncreasingModifier);
                simulator.Simulate(
                    challenger: avatarDigest,
                    enemy: enemyDigest,
                    arenaSimulatorSheets,
                    setExtraValueBuffBeforeGetBuffs: true);
                if (simulator.Log.Result == ArenaResult.Win)
                {
                    winCount++;
                }
            }

            var winRate = (float)winCount / playCount;
            Debug.Log($"ExecuteArena({playCount}) elapsed time: {sw.Elapsed}");
            return winRate;
        }
    }
}
