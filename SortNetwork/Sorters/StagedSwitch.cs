using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MathUtils.Collections;
using SortNetwork.KeySets;

namespace SortNetwork.Sorters
{
    public interface IStagedSwitch : ISwitch
    {
        int StageNumber { get; }
        ISwitch Switch { get; }
    }

    public static class StagedSwitch
    {
        public static IStagedSwitch Make(ISwitch @switch, int stageNumber)
        {
            return new StagedSwitchImpl(@switch, stageNumber);
        }

        public static ISorter ToStagedSorter(this ISorter sorter)
        {
            return sorter.Switches.ToStagedSwitches().ToSorter(sorter.Guid);
        }
    }

    class StagedSwitchImpl : IStagedSwitch
    {
        public StagedSwitchImpl(ISwitch @switch, int stageNumber)
        {
            _switch = @switch;
            _stageNumber = stageNumber;
        }

        public IKeyPair KeyPair
        {
            get { return Switch.KeyPair; }
        }

        public int Index
        {
            get { return Switch.Index; }
        }

        private readonly int _stageNumber;
        public int StageNumber
        {
            get { return _stageNumber; }
        }

        private readonly ISwitch _switch;
        public ISwitch Switch
        {
            get { return _switch; }
        }
    }

    public static class SorterStager
    {

        public static IEnumerable<IStagedSwitch> ToStagedSwitches(this IEnumerable<ISwitch> switches)
        {
            var switchStack = switches.OrderByDescending(T => T.Index).ToImmutableStack();
            if (switchStack.IsEmpty)
            {
                return Enumerable.Empty<IStagedSwitch>();
            }

            var switchStages = ImmutableStack<SwitchStage>.Empty.Push(new SwitchStage(0));
            switchStages.Peek().AddSwitch(switchStack.Peek());
            switchStack = switchStack.Pop();

            while (!switchStack.IsEmpty)
            {
                var nextSwitch = switchStack.Peek();
                if (switchStages.Peek().OverlapsWith(nextSwitch))
                {
                    switchStages = switchStages.Push(new SwitchStage(switchStages.Peek().StageNumber + 1));
                    switchStages.Peek().AddSwitch(nextSwitch);
                }
                else
                {
                    PlaceSwitch(switchStages, nextSwitch);
                }

               switchStack = switchStack.Pop();
            }

            return switchStages.SelectMany(T => T.StagedSwitches).OrderBy(T => T.Index);
        }

        static void PlaceSwitch(ImmutableStack<SwitchStage> stageStack, ISwitch @switch)
        {
            var curStage = stageStack.Peek();
            stageStack = stageStack.Pop();
            if (stageStack.IsEmpty || stageStack.Peek().OverlapsWith(@switch))
            {
                curStage.AddSwitch(@switch);
                return;
            }
            PlaceSwitch(stageStack, @switch);
        }
    }


    public class SwitchStage
    {
        public SwitchStage(int stageNumber)
        {
            _stageNumber = stageNumber;
        }

        private readonly int _stageNumber;
        public int StageNumber
        {
            get { return _stageNumber; }
        }

        readonly List<IStagedSwitch> _switches = new List<IStagedSwitch>();
        public IEnumerable<IStagedSwitch> StagedSwitches
        {
            get { return _switches; }
        }

        public void AddSwitch(ISwitch @switch)
        {
            _switches.Add(StagedSwitch.Make(@switch, StageNumber));
        }

        public bool OverlapsWith(ISwitch @switch)
        {
            return _switches.Any(T => T.KeyPair.Overlaps(@switch.KeyPair));
        }

    }


}
